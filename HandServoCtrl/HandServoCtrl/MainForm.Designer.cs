namespace HandServoCtrl
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            msMain = new MenuStrip();
            connectToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            configToolStripMenuItem = new ToolStripMenuItem();
            tbServo1 = new TrackBar();
            tbServo4 = new TrackBar();
            tbServo2 = new TrackBar();
            tbServo3 = new TrackBar();
            tbServo5 = new TrackBar();
            statusStrip1 = new StatusStrip();
            tssStatus = new ToolStripStatusLabel();
            msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbServo1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbServo4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbServo2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbServo3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbServo5).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // msMain
            // 
            msMain.ImageScalingSize = new Size(20, 20);
            msMain.Items.AddRange(new ToolStripItem[] { connectToolStripMenuItem, aboutToolStripMenuItem, configToolStripMenuItem });
            msMain.Location = new Point(0, 0);
            msMain.Name = "msMain";
            msMain.Size = new Size(574, 28);
            msMain.TabIndex = 1;
            msMain.Text = "menuStrip1";
            // 
            // connectToolStripMenuItem
            // 
            connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            connectToolStripMenuItem.Size = new Size(77, 24);
            connectToolStripMenuItem.Text = "Connect";
            connectToolStripMenuItem.Click += connectToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(64, 24);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // configToolStripMenuItem
            // 
            configToolStripMenuItem.Name = "configToolStripMenuItem";
            configToolStripMenuItem.Size = new Size(67, 24);
            configToolStripMenuItem.Text = "Config";
            configToolStripMenuItem.Click += configToolStripMenuItem_Click;
            // 
            // tbServo1
            // 
            tbServo1.Location = new Point(20, 50);
            tbServo1.Maximum = 180;
            tbServo1.Name = "tbServo1";
            tbServo1.Size = new Size(541, 56);
            tbServo1.TabIndex = 2;
            tbServo1.Value = 90;
            tbServo1.Scroll += tbServo1_Scroll;
            // 
            // tbServo4
            // 
            tbServo4.Location = new Point(21, 236);
            tbServo4.Maximum = 180;
            tbServo4.Name = "tbServo4";
            tbServo4.Size = new Size(540, 56);
            tbServo4.TabIndex = 5;
            tbServo4.Value = 90;
            tbServo4.Scroll += tbServo4_Scroll;
            // 
            // tbServo2
            // 
            tbServo2.Location = new Point(21, 112);
            tbServo2.Maximum = 180;
            tbServo2.Name = "tbServo2";
            tbServo2.Size = new Size(540, 56);
            tbServo2.TabIndex = 3;
            tbServo2.Value = 90;
            tbServo2.Scroll += tbServo2_Scroll;
            // 
            // tbServo3
            // 
            tbServo3.Location = new Point(21, 174);
            tbServo3.Maximum = 180;
            tbServo3.Name = "tbServo3";
            tbServo3.Size = new Size(540, 56);
            tbServo3.TabIndex = 4;
            tbServo3.Value = 90;
            tbServo3.Scroll += tbServo3_Scroll;
            // 
            // tbServo5
            // 
            tbServo5.Location = new Point(21, 298);
            tbServo5.Maximum = 180;
            tbServo5.Name = "tbServo5";
            tbServo5.Size = new Size(540, 56);
            tbServo5.TabIndex = 0;
            tbServo5.Value = 90;
            tbServo5.Scroll += tbServo5_Scroll;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tssStatus });
            statusStrip1.Location = new Point(0, 341);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(574, 26);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // tssStatus
            // 
            tssStatus.Name = "tssStatus";
            tssStatus.Size = new Size(62, 20);
            tssStatus.Text = "STATUS:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(574, 367);
            Controls.Add(statusStrip1);
            Controls.Add(tbServo5);
            Controls.Add(tbServo3);
            Controls.Add(tbServo2);
            Controls.Add(tbServo4);
            Controls.Add(tbServo1);
            Controls.Add(msMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Handle servo control";
            msMain.ResumeLayout(false);
            msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbServo1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbServo4).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbServo2).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbServo3).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbServo5).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip msMain;
        private ToolStripMenuItem connectToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TrackBar tbServo1;
        private TrackBar tbServo4;
        private TrackBar tbServo2;
        private TrackBar tbServo3;
        private TrackBar tbServo5;
        private ToolStripMenuItem configToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tssStatus;
    }
}
