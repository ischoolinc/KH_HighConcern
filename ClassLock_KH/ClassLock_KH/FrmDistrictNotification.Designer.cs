namespace ClassLock_KH
{
    partial class FrmDistrictNotification
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
            this.btnSend = new DevComponents.DotNetBar.ButtonX();
            this.chkNotShow = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.AutoSize = true;
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSend.Location = new System.Drawing.Point(590, 138);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 25);
            this.btnSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "確定";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // chkNotShow
            // 
            this.chkNotShow.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkNotShow.BackgroundStyle.Class = "";
            this.chkNotShow.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkNotShow.Location = new System.Drawing.Point(12, 138);
            this.chkNotShow.Name = "chkNotShow";
            this.chkNotShow.Size = new System.Drawing.Size(171, 23);
            this.chkNotShow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkNotShow.TabIndex = 8;
            this.chkNotShow.Text = "不再顯示通知";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(22, 25);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(643, 27);
            this.labelX1.TabIndex = 9;
            this.labelX1.Text = "高雄局端通知:";
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(22, 52);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(0, 0);
            this.labelX2.TabIndex = 10;
            // 
            // FrmDistrictNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 173);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.chkNotShow);
            this.Controls.Add(this.btnSend);
            this.DoubleBuffered = true;
            this.Name = "FrmDistrictNotification";
            this.Text = "高雄局端通知";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmDistrictNotification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSend;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNotShow;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}