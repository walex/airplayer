using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace aTVPlayer
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        // airplay info
        static int g_airPlayCmdPort = 7000;

        // playback thread
        static System.Threading.Thread g_playbackThread = null;
        static System.Threading.Mutex g_playbackMutex = new System.Threading.Mutex(true);
        static bool g_poolingForPlayBackInfo = false;

        // webservice thread
        static System.Threading.Thread g_mediaServerThread = null;
        static System.Threading.Mutex g_mediaServerMutex;
        static IntPtr g_mediaServerProcMutex = IntPtr.Zero;

        static string g_mediaServerPath = "";
        static string g_mediaServerNI = "";
        static int g_mediaServerPort = 0;

        // transcode thread
        static System.Threading.Thread g_transcodeThread = new System.Threading.Thread(ConvertThread);
        static System.Threading.Mutex g_transcodeMutex = new System.Threading.Mutex(true);
        static int g_transcodeProcessId = -1;
        static bool g_transcodingInProgress = false;
        static object g_transcodingInProgressLock = new object();
        static bool video_pass_through = true;

        class ConvOptions
        {
            public System.Collections.Queue queue = new System.Collections.Queue();
            public bool useSubtitles = false;
            public string inFile;
            public string outPath;
            public string ffmpegPath;
        }
    
        public Form1(string[] args)
        {
            // set path where web server resides
            if (args.Length > 1)
            {
                g_mediaServerPath = args[1];
            }
            else
            {
                g_mediaServerPath = Application.StartupPath;
            }
            InitializeComponent();            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            bool bCreatedNew;
            System.Threading.Mutex mtx = new System.Threading.Mutex(true, "Global\\atv_player_instance_mtx", out bCreatedNew);            
            if (!bCreatedNew)
            {
                mtx.Dispose();
                MessageBox.Show("Another instance of the program is already running");
                this.Close();
            }
            

            // set media server folder, this is where mp4 files must be placed, also the folder must be published
            lbMediaServerFolder.Text = g_mediaServerPath;
            // load network interfaces
            if (LoadNI() == false)
            {
                MessageBox.Show("Please connect a network interface");
                this.Close();
            }
            // check media server path
            if (g_mediaServerPath.Length == 0 || Directory.Exists(g_mediaServerPath) == false)
            {
                MessageBox.Show("Please set media server folder");
                this.Close();
            }
            // show files at media server folder
            listFiles(lbMediaServerFolder.Text, new string[] { "*.mp4" }, listViewMp4);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // stop trasncoding
            lock (g_transcodingInProgressLock)
            {
                if (g_transcodingInProgress == true && g_transcodeProcessId != -1)
                {
                    Process.GetProcessById(g_transcodeProcessId).Kill();
                }
            }            

            // stop playback
            StopPlayBack(txtAirplayIP.Text.Trim());           
            
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            // adds files for transcoding
            OpenFileDialog openFileDialog1 = new OpenFileDialog();            
            openFileDialog1.Filter = "Media files (.mkv;.avi)|*.mkv; *.avi";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.CheckFileExists=true;
            openFileDialog1.ValidateNames=true;
            openFileDialog1.Title="Select media file";
            openFileDialog1.Multiselect = true;            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    listViewMediaToTranscode.Items.Add(new ListViewItem(fileName));
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            // add folder to transcoding
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                listFiles(fbd.SelectedPath, new string[] { "*.mkv", "*.avi" }, listViewMediaToTranscode);
            }
        }

        private void bnEncode_Click(object sender, EventArgs e)
        {
            // if already encoding the stop the process and exit
            lock (g_transcodingInProgressLock)
            {
                if (g_transcodingInProgress == true)
                {
                    Process.GetProcessById(g_transcodeProcessId).Kill();
                    g_transcodeThread.Join();
                    btnConvert.Text = "start";
                    return;
                }
            }

            // check for files selected
            if (listViewMediaToTranscode.Items.Count == 0)
            {
                MessageBox.Show("No files to convert selected");
                return;
            }            
            
            if (File.Exists(txtFFMPEGPath.Text.Trim()) == false)
            {
                MessageBox.Show("Invalid ffmpeg path");
                return;
            }

            // enqueue file names
            System.Collections.Queue queue = new System.Collections.Queue();
            foreach (ListViewItem lv in listViewMediaToTranscode.Items)
            {
                if (lv.Checked == true)
                {
                    ConvOptions opt = new ConvOptions();
                    opt.useSubtitles = chkUseSrt.Checked;
                    opt.outPath = g_mediaServerPath;
                    opt.inFile = lv.Text;
                    opt.ffmpegPath = txtFFMPEGPath.Text.Trim();
                    queue.Enqueue(opt);
                }
            }
            if (queue.Count == 0)
            {
                MessageBox.Show("No files to convert selected");
                return;
            }            
            // start transcoding
            video_pass_through = chkVideoPass.Checked;
            try
            {
                g_transcodeThread.Start(queue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
            btnConvert.Text = "stop";
        }        

        private void btnPlay_Click(object sender, EventArgs e)
        {

            // check for media file selected
            if (listViewMp4.SelectedIndices.Count == 0) { return; }

            // check for a valid airplay ip
            if (ValidateIp(txtAirplayIP.Text) == false)
            {
                MessageBox.Show("Invalid airplay device IP");
                return;
            }

            //check media server port
            if (int.TryParse(txtMSPort.Text.Trim(), out g_mediaServerPort) == false)
            {
                MessageBox.Show("Invalid media server port");
                return;
            }

            string ipAddr = txtAirplayIP.Text.Trim();
            string path = listViewMp4.Items[listViewMp4.SelectedIndices[0]].Text;
            // check for media server folder and media file
            if (Directory.Exists(Path.GetDirectoryName(path)) == false)
            {
                MessageBox.Show("Invalid media folder");
                return;
            }

            if (File.Exists(path) == false)
            {
                MessageBox.Show("Invalid media file");
                return;
            }

            float startTime;
            if (float.TryParse(txtStartTime.Text.Trim(), out startTime) == false || startTime < 0.0f || startTime > 1.0f)
            {
                MessageBox.Show("Please set a decimal number between 0 and 1 for plaback start field");
                return;
            }
            // start media server
            g_mediaServerThread = new System.Threading.Thread(MediaServerThread);
            try
            {
                g_mediaServerMutex = new System.Threading.Mutex(true);
                g_mediaServerThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
            int tries = 0;
            bool tup = false;
            while (tries < 20)
            {
                tries++;
                if ((tup = g_mediaServerThread.IsAlive) == true)
                    break;
                System.Threading.Thread.Sleep(100);
            }
            if ( tup == false)
            {
                try { g_mediaServerThread.Abort(); }
                catch { }
                MessageBox.Show("Error initializing media server");
                Application.Exit();
            }
            
            
            // send command to airplay device            
            try
            {
                string fileName = Path.GetFileName(path);
                using (var client = new System.Net.WebClient())
                {
                    string args = string.Format("Content-Location: http://{0}:{1}/{2}\nStart-Position: {3}\n", g_mediaServerNI, g_mediaServerPort, fileName, startTime);
                    client.UseDefaultCredentials = true;
                    client.UploadData(string.Format("http://{0}:{1}/play", ipAddr, g_airPlayCmdPort), "PUT", Encoding.ASCII.GetBytes(args));

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
            
            // start playback thread
            try
            {
                g_playbackThread = new System.Threading.Thread(PlaybackTimerThread);
                g_playbackThread.Start(ipAddr);                
            }
            catch (Exception ex)
            {
                StopPlayBack(ipAddr);
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
            
            txtAirplayIP.Enabled = false;
            txtMSPort.Enabled = false;
            txtStartTime.Enabled = false;
            cbNI.Enabled = false;
            btnPlay.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnScanMediaServerFolder_Click(object sender, EventArgs e)
        {
            listFiles(lbMediaServerFolder.Text, new string[] { "*.mp4" }, listViewMp4);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPlayBack(txtAirplayIP.Text.Trim());
            txtAirplayIP.Enabled = true;
            txtMSPort.Enabled = true;
            txtStartTime.Enabled = true;
            cbNI.Enabled = true;
            btnPlay.Enabled = true;            
            btnStop.Enabled = false;
        }        

        private void cbNI_SelectedIndexChanged(object sender, EventArgs e)
        {
            g_mediaServerNI = cbNI.Items[cbNI.SelectedIndex].ToString();
        }

        private void listFiles(string path, string[] filter, ListView lv)
        {
            lv.Items.Clear();
            foreach (string flt in filter)
            {
                string[] files = System.IO.Directory.GetFiles(path, flt);
                foreach (string file in files)
                {
                    lv.Items.Add(new ListViewItem(file));

                }
            }
        }

        private static void convertToMp4(ConvOptions opt)
        {
            // parse args
            string destPath = opt.outPath;
            string inFile = opt.inFile.ToLower();
            string ext = Path.GetExtension(inFile).Remove(0,1);
            // subtitles option
            string subPath = inFile.Replace(ext, "srt");
            string subtitlesOption = opt.useSubtitles == true && System.IO.File.Exists(subPath) == true ? string.Format("-sub_charenc WINDOWS-1252 -i {0} -map 1 -scodec mov_text -metadata:s:s:0 language=spa", string.Format("\"{0}\"", subPath)) : "";

            // output file
            string outFile = Path.Combine(destPath, Path.GetFileName(inFile.Replace(ext, "mp4")).Replace(" ", "."));
            string video_codec = video_pass_through == true ? "-c:v copy" : "-vcodec libx264";
            string args = string.Format("-y -i {0} {1} -map 0:0 -map 0:1 {2} -c:a ac3 -ac 6 -b:a 640k {3}", string.Format("\"{0}\"", inFile), subtitlesOption, video_codec, string.Format("\"{0}\"", outFile));

            // ffmpeg path
            string ffmpegURL = Path.Combine(Application.StartupPath, opt.ffmpegPath);
            
            // setup process to be launched
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = ffmpegURL;
            startInfo.Arguments = args;
            startInfo.WorkingDirectory = Application.StartupPath;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            // launch process and wait for it finish
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                try
                {
                    process.Start();
                    g_transcodeProcessId = process.Id;
                    lock (g_transcodingInProgressLock)
                    {
                        g_transcodingInProgress = true;
                    }
                    process.WaitForExit();
                    lock (g_transcodingInProgressLock)
                    {
                        g_transcodingInProgress = false;
                    }
                }
                catch (Exception ex)
                {                    
                    MessageBox.Show(ex.Message);
                    Application.Exit();
                }
                finally
                {
                    g_transcodeProcessId = -1;
                }
            }
            
        }

        bool LoadNI()
        {
            // scan network interfaces
            System.Net.IPAddress[] localIPs = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ip in localIPs)
            {

                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    cbNI.Items.Add(ip.ToString());
            }

            if ( cbNI.Items.Count > 0 )
            {
                cbNI.SelectedIndex = 0;
                g_mediaServerNI = cbNI.Items[0].ToString();
                return true;
            }
            return false;
        }       
        

        static bool ValidateIp(string ipAddr)
        {
            try
            {
                System.Net.IPAddress.Parse(ipAddr.Trim());
            }
            catch
            { return false; }
            return true;
        }       

        
        private static void StopPlayBack(string deviceIP)
        {
            // check for plaback thread and stop it
            if (g_playbackThread != null && g_playbackThread.IsAlive == true)
            {
                g_poolingForPlayBackInfo = false;
                g_playbackThread.Join();
                g_playbackThread = null;
            }
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.UseDefaultCredentials = true;
                    client.UploadData(string.Format("http://{0}:7000/stop", deviceIP), "PUT", new byte[] { });
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // stop media server
            if (g_mediaServerThread != null && g_mediaServerThread.IsAlive == true)
            {                
                g_mediaServerMutex.ReleaseMutex();
                g_mediaServerThread.Join();
                g_mediaServerThread = null;
                g_mediaServerMutex.Dispose();
                g_mediaServerMutex = null;
            }
        }        

        private static void PlaybackTimerThread(object param)
        { 
            // send keep alive to airplay device every 1 second
            string ipAddr = (string)param;
            g_poolingForPlayBackInfo = true;
            while (g_poolingForPlayBackInfo == true)
            {

                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(string.Format("http://{0}:7000/playback-info", ipAddr));
                request.Method = "GET";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                response.Dispose();                
                System.Threading.Thread.Sleep(2000);
            }
            g_poolingForPlayBackInfo = false;
        }

        private static void ConvertThread(object param)
        {
            System.Collections.Queue queue = (System.Collections.Queue)param;            
            while (queue.Count > 0)
            {
                convertToMp4((ConvOptions)queue.Dequeue());
            }            
            MessageBox.Show("Conversion completed!");

        }

        private static void MediaServerThread()
        {            
            string mediaServerPath = Path.Combine(g_mediaServerPath, "webserver.exe");

            // setup media server process info
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = mediaServerPath;
            startInfo.Arguments = string.Format("-listening_port {0} -client_process_id {1} -client_process_name \"{2}\"", g_mediaServerPort, Process.GetCurrentProcess().Id, Application.ExecutablePath.ToLower());
            startInfo.WorkingDirectory = Application.StartupPath;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;            
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            // launch media server
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                try
                {
                    process.Start();                    
                    g_mediaServerMutex.WaitOne();                    
                    process.Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Exit();
                }
                finally
                {
                }
            }

        }

        private void btnSearchFFMPEG_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();            
            openFileDialog1.Filter = "ffmpeg encoder|ffmpeg.exe";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.CheckFileExists=true;
            openFileDialog1.ValidateNames=true;
            openFileDialog1.Title="ffmpeg encoder";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFFMPEGPath.Text = openFileDialog1.FileName;
            }
            
        }
        
    }
}
