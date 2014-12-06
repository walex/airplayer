namespace aTVPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddFile = new System.Windows.Forms.Button();
            this.listViewMediaToTranscode = new System.Windows.Forms.ListView();
            this.hMediaFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.listViewMp4 = new System.Windows.Forms.ListView();
            this.hMp4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.chkVideoPass = new System.Windows.Forms.CheckBox();
            this.chkAudioPass = new System.Windows.Forms.CheckBox();
            this.chkUseSrt = new System.Windows.Forms.CheckBox();
            this.btnScanMediaServerFolder = new System.Windows.Forms.Button();
            this.lbMediaServerFolder = new System.Windows.Forms.Label();
            this.txtAirplayIP = new System.Windows.Forms.TextBox();
            this.lbAirplayIP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbNI = new System.Windows.Forms.ComboBox();
            this.txtMSPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFFMPEGPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchFFMPEG = new System.Windows.Forms.Button();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(519, 12);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(79, 29);
            this.btnAddFile.TabIndex = 0;
            this.btnAddFile.Text = "add file";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // listViewMediaToTranscode
            // 
            this.listViewMediaToTranscode.CheckBoxes = true;
            this.listViewMediaToTranscode.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hMediaFile});
            this.listViewMediaToTranscode.FullRowSelect = true;
            this.listViewMediaToTranscode.Location = new System.Drawing.Point(12, 12);
            this.listViewMediaToTranscode.Name = "listViewMediaToTranscode";
            this.listViewMediaToTranscode.Size = new System.Drawing.Size(501, 168);
            this.listViewMediaToTranscode.TabIndex = 1;
            this.listViewMediaToTranscode.UseCompatibleStateImageBehavior = false;
            this.listViewMediaToTranscode.View = System.Windows.Forms.View.Details;
            // 
            // hMediaFile
            // 
            this.hMediaFile.Text = "Media files";
            this.hMediaFile.Width = 497;
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(519, 47);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(79, 29);
            this.btnAddFolder.TabIndex = 2;
            this.btnAddFolder.Text = "scan folder";
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // listViewMp4
            // 
            this.listViewMp4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hMp4});
            this.listViewMp4.Location = new System.Drawing.Point(12, 261);
            this.listViewMp4.MultiSelect = false;
            this.listViewMp4.Name = "listViewMp4";
            this.listViewMp4.Size = new System.Drawing.Size(501, 154);
            this.listViewMp4.TabIndex = 3;
            this.listViewMp4.UseCompatibleStateImageBehavior = false;
            this.listViewMp4.View = System.Windows.Forms.View.Details;
            // 
            // hMp4
            // 
            this.hMp4.Text = "Mp4 media files";
            this.hMp4.Width = 494;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(519, 338);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(79, 29);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(519, 303);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(79, 29);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(519, 82);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(79, 29);
            this.btnConvert.TabIndex = 6;
            this.btnConvert.Text = "start";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.bnEncode_Click);
            // 
            // chkVideoPass
            // 
            this.chkVideoPass.AutoSize = true;
            this.chkVideoPass.Checked = true;
            this.chkVideoPass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVideoPass.Location = new System.Drawing.Point(519, 117);
            this.chkVideoPass.Name = "chkVideoPass";
            this.chkVideoPass.Size = new System.Drawing.Size(116, 17);
            this.chkVideoPass.TabIndex = 7;
            this.chkVideoPass.Text = "video pass-through";
            this.chkVideoPass.UseVisualStyleBackColor = true;
            // 
            // chkAudioPass
            // 
            this.chkAudioPass.AutoSize = true;
            this.chkAudioPass.Enabled = false;
            this.chkAudioPass.Location = new System.Drawing.Point(519, 140);
            this.chkAudioPass.Name = "chkAudioPass";
            this.chkAudioPass.Size = new System.Drawing.Size(116, 17);
            this.chkAudioPass.TabIndex = 8;
            this.chkAudioPass.Text = "audio pass-through";
            this.chkAudioPass.UseVisualStyleBackColor = true;
            // 
            // chkUseSrt
            // 
            this.chkUseSrt.AutoSize = true;
            this.chkUseSrt.Checked = true;
            this.chkUseSrt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSrt.Location = new System.Drawing.Point(519, 163);
            this.chkUseSrt.Name = "chkUseSrt";
            this.chkUseSrt.Size = new System.Drawing.Size(111, 17);
            this.chkUseSrt.TabIndex = 9;
            this.chkUseSrt.Text = "add str if available";
            this.chkUseSrt.UseVisualStyleBackColor = true;
            // 
            // btnScanMediaServerFolder
            // 
            this.btnScanMediaServerFolder.Location = new System.Drawing.Point(519, 261);
            this.btnScanMediaServerFolder.Name = "btnScanMediaServerFolder";
            this.btnScanMediaServerFolder.Size = new System.Drawing.Size(79, 36);
            this.btnScanMediaServerFolder.TabIndex = 11;
            this.btnScanMediaServerFolder.Text = "scan folder";
            this.btnScanMediaServerFolder.UseVisualStyleBackColor = true;
            this.btnScanMediaServerFolder.Click += new System.EventHandler(this.btnScanMediaServerFolder_Click);
            // 
            // lbMediaServerFolder
            // 
            this.lbMediaServerFolder.AutoSize = true;
            this.lbMediaServerFolder.Location = new System.Drawing.Point(12, 245);
            this.lbMediaServerFolder.Name = "lbMediaServerFolder";
            this.lbMediaServerFolder.Size = new System.Drawing.Size(0, 13);
            this.lbMediaServerFolder.TabIndex = 12;
            // 
            // txtAirplayIP
            // 
            this.txtAirplayIP.Location = new System.Drawing.Point(117, 425);
            this.txtAirplayIP.Name = "txtAirplayIP";
            this.txtAirplayIP.Size = new System.Drawing.Size(232, 20);
            this.txtAirplayIP.TabIndex = 13;
            this.txtAirplayIP.Text = "192.168.1.43";
            // 
            // lbAirplayIP
            // 
            this.lbAirplayIP.AutoSize = true;
            this.lbAirplayIP.Location = new System.Drawing.Point(12, 428);
            this.lbAirplayIP.Name = "lbAirplayIP";
            this.lbAirplayIP.Size = new System.Drawing.Size(91, 13);
            this.lbAirplayIP.TabIndex = 14;
            this.lbAirplayIP.Text = "Airplay Device IP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Media Server NI:";
            // 
            // cbNI
            // 
            this.cbNI.FormattingEnabled = true;
            this.cbNI.Location = new System.Drawing.Point(117, 451);
            this.cbNI.Name = "cbNI";
            this.cbNI.Size = new System.Drawing.Size(232, 21);
            this.cbNI.TabIndex = 16;
            this.cbNI.SelectedIndexChanged += new System.EventHandler(this.cbNI_SelectedIndexChanged);
            // 
            // txtMSPort
            // 
            this.txtMSPort.Location = new System.Drawing.Point(117, 478);
            this.txtMSPort.Name = "txtMSPort";
            this.txtMSPort.Size = new System.Drawing.Size(232, 20);
            this.txtMSPort.TabIndex = 17;
            this.txtMSPort.Text = "8090";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 481);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Media Server Port:";
            // 
            // txtFFMPEGPath
            // 
            this.txtFFMPEGPath.Location = new System.Drawing.Point(86, 193);
            this.txtFFMPEGPath.Name = "txtFFMPEGPath";
            this.txtFFMPEGPath.Size = new System.Drawing.Size(381, 20);
            this.txtFFMPEGPath.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "ffmpeg path:";
            // 
            // btnSearchFFMPEG
            // 
            this.btnSearchFFMPEG.Location = new System.Drawing.Point(473, 188);
            this.btnSearchFFMPEG.Name = "btnSearchFFMPEG";
            this.btnSearchFFMPEG.Size = new System.Drawing.Size(40, 28);
            this.btnSearchFFMPEG.TabIndex = 21;
            this.btnSearchFFMPEG.Text = "...";
            this.btnSearchFFMPEG.UseVisualStyleBackColor = true;
            this.btnSearchFFMPEG.Click += new System.EventHandler(this.btnSearchFFMPEG_Click);
            // 
            // txtStartTime
            // 
            this.txtStartTime.Location = new System.Drawing.Point(117, 504);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(52, 20);
            this.txtStartTime.TabIndex = 22;
            this.txtStartTime.Text = "0.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 507);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Offset ( % ):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 534);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.btnSearchFFMPEG);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFFMPEGPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMSPort);
            this.Controls.Add(this.cbNI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbAirplayIP);
            this.Controls.Add(this.txtAirplayIP);
            this.Controls.Add(this.lbMediaServerFolder);
            this.Controls.Add(this.btnScanMediaServerFolder);
            this.Controls.Add(this.chkUseSrt);
            this.Controls.Add(this.chkAudioPass);
            this.Controls.Add(this.chkVideoPass);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.listViewMp4);
            this.Controls.Add(this.btnAddFolder);
            this.Controls.Add(this.listViewMediaToTranscode);
            this.Controls.Add(this.btnAddFile);
            this.Name = "Form1";
            this.Text = "Apple TV Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.ListView listViewMediaToTranscode;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.ListView listViewMp4;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.CheckBox chkVideoPass;
        private System.Windows.Forms.CheckBox chkAudioPass;
        private System.Windows.Forms.ColumnHeader hMediaFile;
        private System.Windows.Forms.CheckBox chkUseSrt;
        private System.Windows.Forms.ColumnHeader hMp4;
        private System.Windows.Forms.Button btnScanMediaServerFolder;
        private System.Windows.Forms.Label lbMediaServerFolder;
        private System.Windows.Forms.TextBox txtAirplayIP;
        private System.Windows.Forms.Label lbAirplayIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbNI;
        private System.Windows.Forms.TextBox txtMSPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFFMPEGPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchFFMPEG;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label label4;
    }
}

