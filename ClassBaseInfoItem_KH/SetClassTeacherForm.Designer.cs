namespace ClassBaseInfoItem_KH
{
    partial class SetClassTeacherForm
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
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtMemo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSend = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.txtEDoc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.UploadFile = new DevComponents.DotNetBar.ButtonX();
            this.cboTeacherName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMsg.BackgroundStyle.Class = "";
            this.lblMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMsg.Location = new System.Drawing.Point(13, 13);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(221, 21);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "說明：班級導師修改傳送至局端備查";
            this.lblMsg.WordWrap = true;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(14, 99);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(34, 21);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "備註";
            // 
            // txtMemo
            // 
            // 
            // 
            // 
            this.txtMemo.Border.Class = "TextBoxBorder";
            this.txtMemo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMemo.Location = new System.Drawing.Point(66, 95);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(373, 25);
            this.txtMemo.TabIndex = 4;
            // 
            // btnSend
            // 
            this.btnSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSend.AutoSize = true;
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSend.Location = new System.Drawing.Point(281, 169);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 25);
            this.btnSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "確定";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(362, 169);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtEDoc
            // 
            // 
            // 
            // 
            this.txtEDoc.Border.Class = "TextBoxBorder";
            this.txtEDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEDoc.Location = new System.Drawing.Point(143, 132);
            this.txtEDoc.Multiline = true;
            this.txtEDoc.Name = "txtEDoc";
            this.txtEDoc.Size = new System.Drawing.Size(213, 25);
            this.txtEDoc.TabIndex = 5;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(13, 136);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(114, 21);
            this.labelX6.TabIndex = 11;
            this.labelX6.Text = "相關證明文件網址";
            // 
            // UploadFile
            // 
            this.UploadFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.UploadFile.AutoSize = true;
            this.UploadFile.BackColor = System.Drawing.Color.Transparent;
            this.UploadFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.UploadFile.Location = new System.Drawing.Point(361, 132);
            this.UploadFile.Name = "UploadFile";
            this.UploadFile.Size = new System.Drawing.Size(75, 25);
            this.UploadFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.UploadFile.TabIndex = 6;
            this.UploadFile.Text = "上傳檔案";
            this.UploadFile.Click += new System.EventHandler(this.UploadFile_Click);
            // 
            // cboTeacherName
            // 
            this.cboTeacherName.DisplayMember = "Text";
            this.cboTeacherName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTeacherName.FormattingEnabled = true;
            this.cboTeacherName.ItemHeight = 19;
            this.cboTeacherName.Location = new System.Drawing.Point(66, 49);
            this.cboTeacherName.Name = "cboTeacherName";
            this.cboTeacherName.Size = new System.Drawing.Size(110, 25);
            this.cboTeacherName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTeacherName.TabIndex = 12;
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(13, 53);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(47, 21);
            this.labelX7.TabIndex = 13;
            this.labelX7.Text = "班導師";
            // 
            // SetClassTeacherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 199);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.cboTeacherName);
            this.Controls.Add(this.UploadFile);
            this.Controls.Add(this.txtEDoc);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lblMsg);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(467, 238);
            this.MinimumSize = new System.Drawing.Size(467, 238);
            this.Name = "SetClassTeacherForm";
            this.Text = "班級導師調整";
            this.Load += new System.EventHandler(this.SetClassTeacherForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblMsg;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMemo;
        private DevComponents.DotNetBar.ButtonX btnSend;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEDoc;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonX UploadFile;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTeacherName;
        private DevComponents.DotNetBar.LabelX labelX7;
    }
}