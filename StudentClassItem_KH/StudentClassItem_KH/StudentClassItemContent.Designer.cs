namespace StudentClassItem_KH
{
    partial class StudentClassItemContent
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.txtStudentNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label41 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblClassName = new DevComponents.DotNetBar.LabelX();
            this.lblSeatNo = new DevComponents.DotNetBar.LabelX();
            this.lnkSend = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtStudentNumber
            // 
            // 
            // 
            // 
            this.txtStudentNumber.Border.Class = "TextBoxBorder";
            this.txtStudentNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtStudentNumber.Location = new System.Drawing.Point(409, 11);
            this.txtStudentNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtStudentNumber.Name = "txtStudentNumber";
            this.txtStudentNumber.Size = new System.Drawing.Size(119, 25);
            this.txtStudentNumber.TabIndex = 4;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.Location = new System.Drawing.Point(354, 15);
            this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(47, 17);
            this.label41.TabIndex = 205;
            this.label41.Text = "學　號";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(23, 15);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(47, 17);
            this.label38.TabIndex = 204;
            this.label38.Text = "班　級";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(211, 15);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(47, 17);
            this.label37.TabIndex = 207;
            this.label37.Text = "座　號";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClassName
            // 
            // 
            // 
            // 
            this.lblClassName.BackgroundStyle.Class = "";
            this.lblClassName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblClassName.Location = new System.Drawing.Point(78, 12);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(126, 23);
            this.lblClassName.TabIndex = 208;
            // 
            // lblSeatNo
            // 
            // 
            // 
            // 
            this.lblSeatNo.BackgroundStyle.Class = "";
            this.lblSeatNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSeatNo.Location = new System.Drawing.Point(266, 12);
            this.lblSeatNo.Name = "lblSeatNo";
            this.lblSeatNo.Size = new System.Drawing.Size(59, 23);
            this.lblSeatNo.TabIndex = 209;
            // 
            // lnkSend
            // 
            this.lnkSend.AutoSize = true;
            this.lnkSend.Location = new System.Drawing.Point(23, 38);
            this.lnkSend.Name = "lnkSend";
            this.lnkSend.Size = new System.Drawing.Size(60, 17);
            this.lnkSend.TabIndex = 210;
            this.lnkSend.TabStop = true;
            this.lnkSend.Text = "班級調整";
            this.lnkSend.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSend_LinkClicked);
            // 
            // StudentClassItemContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.lnkSend);
            this.Controls.Add(this.lblSeatNo);
            this.Controls.Add(this.lblClassName);
            this.Controls.Add(this.txtStudentNumber);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label37);
            this.Name = "StudentClassItemContent";
            this.Size = new System.Drawing.Size(550, 65);
            this.Load += new System.EventHandler(this.StudentClassItemContent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevComponents.DotNetBar.Controls.TextBoxX txtStudentNumber;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private DevComponents.DotNetBar.LabelX lblClassName;
        private DevComponents.DotNetBar.LabelX lblSeatNo;
        private System.Windows.Forms.LinkLabel lnkSend;
    }
}
