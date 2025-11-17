namespace HandServoCtrl
{
    partial class UartForm
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
            cbPortName = new ComboBox();
            cbBaudRate = new ComboBox();
            cbParity = new ComboBox();
            cbDataBits = new ComboBox();
            btOk = new Button();
            groupBox1 = new GroupBox();
            cbStopBits = new ComboBox();
            lbPortName = new Label();
            lbStopBits = new Label();
            lbDataBits = new Label();
            lbParity = new Label();
            lbBaudRate = new Label();
            btCancel = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // cbPortName
            // 
            cbPortName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPortName.FormattingEnabled = true;
            cbPortName.Location = new Point(115, 26);
            cbPortName.Name = "cbPortName";
            cbPortName.Size = new Size(151, 28);
            cbPortName.TabIndex = 0;
            // 
            // cbBaudRate
            // 
            cbBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBaudRate.Enabled = false;
            cbBaudRate.FormattingEnabled = true;
            cbBaudRate.Location = new Point(115, 60);
            cbBaudRate.Name = "cbBaudRate";
            cbBaudRate.Size = new Size(151, 28);
            cbBaudRate.TabIndex = 1;
            // 
            // cbParity
            // 
            cbParity.DropDownStyle = ComboBoxStyle.DropDownList;
            cbParity.Enabled = false;
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(115, 94);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(151, 28);
            cbParity.TabIndex = 2;
            // 
            // cbDataBits
            // 
            cbDataBits.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDataBits.Enabled = false;
            cbDataBits.FormattingEnabled = true;
            cbDataBits.Location = new Point(115, 128);
            cbDataBits.Name = "cbDataBits";
            cbDataBits.Size = new Size(151, 28);
            cbDataBits.TabIndex = 3;
            // 
            // btOk
            // 
            btOk.Location = new Point(190, 220);
            btOk.Name = "btOk";
            btOk.Size = new Size(94, 29);
            btOk.TabIndex = 6;
            btOk.Text = "Ok";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cbStopBits);
            groupBox1.Controls.Add(lbPortName);
            groupBox1.Controls.Add(lbStopBits);
            groupBox1.Controls.Add(lbDataBits);
            groupBox1.Controls.Add(lbParity);
            groupBox1.Controls.Add(lbBaudRate);
            groupBox1.Controls.Add(cbPortName);
            groupBox1.Controls.Add(cbBaudRate);
            groupBox1.Controls.Add(cbParity);
            groupBox1.Controls.Add(cbDataBits);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(272, 202);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Robo Handle port";
            // 
            // cbStopBits
            // 
            cbStopBits.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStopBits.Enabled = false;
            cbStopBits.FormattingEnabled = true;
            cbStopBits.Location = new Point(115, 162);
            cbStopBits.Name = "cbStopBits";
            cbStopBits.Size = new Size(151, 28);
            cbStopBits.TabIndex = 12;
            // 
            // lbPortName
            // 
            lbPortName.AutoSize = true;
            lbPortName.Location = new Point(6, 29);
            lbPortName.Name = "lbPortName";
            lbPortName.Size = new Size(79, 20);
            lbPortName.TabIndex = 11;
            lbPortName.Text = "Port Name";
            // 
            // lbStopBits
            // 
            lbStopBits.AutoSize = true;
            lbStopBits.Location = new Point(6, 165);
            lbStopBits.Name = "lbStopBits";
            lbStopBits.Size = new Size(68, 20);
            lbStopBits.TabIndex = 9;
            lbStopBits.Text = "Stop Bits";
            // 
            // lbDataBits
            // 
            lbDataBits.AutoSize = true;
            lbDataBits.Location = new Point(6, 131);
            lbDataBits.Name = "lbDataBits";
            lbDataBits.Size = new Size(69, 20);
            lbDataBits.TabIndex = 8;
            lbDataBits.Text = "Data Bits";
            // 
            // lbParity
            // 
            lbParity.AutoSize = true;
            lbParity.Location = new Point(6, 97);
            lbParity.Name = "lbParity";
            lbParity.Size = new Size(45, 20);
            lbParity.TabIndex = 7;
            lbParity.Text = "Parity";
            // 
            // lbBaudRate
            // 
            lbBaudRate.AutoSize = true;
            lbBaudRate.Location = new Point(6, 63);
            lbBaudRate.Name = "lbBaudRate";
            lbBaudRate.Size = new Size(77, 20);
            lbBaudRate.TabIndex = 6;
            lbBaudRate.Text = "Baud Rate";
            // 
            // btCancel
            // 
            btCancel.Location = new Point(90, 220);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(94, 29);
            btCancel.TabIndex = 8;
            btCancel.Text = "Cancel";
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += btCancel_Click_1;
            // 
            // UartForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(296, 261);
            Controls.Add(btCancel);
            Controls.Add(groupBox1);
            Controls.Add(btOk);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UartForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Uart";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbPortName;
        private ComboBox cbBaudRate;
        private ComboBox cbParity;
        private ComboBox cbDataBits;
        private Button button1;
        private Button btOk;
        private GroupBox groupBox1;
        private Label lbPortName;
        private Label lbStopBits;
        private Label lbDataBits;
        private Label lbParity;
        private Label lbBaudRate;
        private ComboBox cbStopBits;
        private Button btCancel;
    }
}