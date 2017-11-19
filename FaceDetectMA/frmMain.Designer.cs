namespace FaceDetectMA
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.cbDriver = new System.Windows.Forms.ComboBox();
            this.bStart = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.pbCamera = new System.Windows.Forms.PictureBox();
            this.bFilterColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSensivity = new System.Windows.Forms.TrackBar();
            this.lSensivity = new System.Windows.Forms.Label();
            this.pbCameraSobel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSensivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCameraSobel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Driver : ";
            // 
            // cbDriver
            // 
            this.cbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDriver.FormattingEnabled = true;
            this.cbDriver.Location = new System.Drawing.Point(106, 18);
            this.cbDriver.Name = "cbDriver";
            this.cbDriver.Size = new System.Drawing.Size(183, 24);
            this.cbDriver.TabIndex = 1;
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(295, 12);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 34);
            this.bStart.TabIndex = 2;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.Location = new System.Drawing.Point(376, 12);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(75, 34);
            this.bStop.TabIndex = 3;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // pbCamera
            // 
            this.pbCamera.BackColor = System.Drawing.Color.White;
            this.pbCamera.Location = new System.Drawing.Point(106, 52);
            this.pbCamera.Name = "pbCamera";
            this.pbCamera.Size = new System.Drawing.Size(320, 240);
            this.pbCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCamera.TabIndex = 4;
            this.pbCamera.TabStop = false;
            this.pbCamera.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCamera_MouseDown);
            // 
            // bFilterColor
            // 
            this.bFilterColor.Location = new System.Drawing.Point(106, 298);
            this.bFilterColor.Name = "bFilterColor";
            this.bFilterColor.Size = new System.Drawing.Size(99, 34);
            this.bFilterColor.TabIndex = 7;
            this.bFilterColor.UseVisualStyleBackColor = true;
            this.bFilterColor.Click += new System.EventHandler(this.bFilterColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 307);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Filter Color : ";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(211, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 34);
            this.label4.TabIndex = 9;
            this.label4.Text = "Filter Color click button or picture box.\r\n";
            // 
            // tbSensivity
            // 
            this.tbSensivity.Location = new System.Drawing.Point(26, 77);
            this.tbSensivity.Maximum = 99;
            this.tbSensivity.Minimum = 1;
            this.tbSensivity.Name = "tbSensivity";
            this.tbSensivity.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbSensivity.Size = new System.Drawing.Size(56, 215);
            this.tbSensivity.TabIndex = 10;
            this.tbSensivity.Value = 10;
            this.tbSensivity.ValueChanged += new System.EventHandler(this.tbSensivity_ValueChanged);
            // 
            // lSensivity
            // 
            this.lSensivity.Location = new System.Drawing.Point(12, 52);
            this.lSensivity.Name = "lSensivity";
            this.lSensivity.Size = new System.Drawing.Size(88, 22);
            this.lSensivity.TabIndex = 11;
            this.lSensivity.Text = "Sensivity 0,1";
            this.lSensivity.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbCameraSobel
            // 
            this.pbCameraSobel.BackColor = System.Drawing.Color.White;
            this.pbCameraSobel.Location = new System.Drawing.Point(432, 52);
            this.pbCameraSobel.Name = "pbCameraSobel";
            this.pbCameraSobel.Size = new System.Drawing.Size(320, 240);
            this.pbCameraSobel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCameraSobel.TabIndex = 12;
            this.pbCameraSobel.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(764, 343);
            this.Controls.Add(this.pbCameraSobel);
            this.Controls.Add(this.lSensivity);
            this.Controls.Add(this.tbSensivity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bFilterColor);
            this.Controls.Add(this.pbCamera);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.cbDriver);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Face Detect MA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSensivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCameraSobel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDriver;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.PictureBox pbCamera;
        private System.Windows.Forms.Button bFilterColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbSensivity;
        private System.Windows.Forms.Label lSensivity;
        private System.Windows.Forms.PictureBox pbCameraSobel;
    }
}

