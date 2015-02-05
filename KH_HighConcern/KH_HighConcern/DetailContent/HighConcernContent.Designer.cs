namespace KH_HighConcern.DetailContent
{
    partial class HighConcernContent
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.chkHighConcern = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtDocNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtEDoc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(194, 25);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 21);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "減免人數";
            // 
            // chkHighConcern
            // 
            this.chkHighConcern.AutoSize = true;
            // 
            // 
            // 
            this.chkHighConcern.BackgroundStyle.Class = "";
            this.chkHighConcern.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkHighConcern.Location = new System.Drawing.Point(24, 24);
            this.chkHighConcern.Name = "chkHighConcern";
            this.chkHighConcern.Size = new System.Drawing.Size(161, 21);
            this.chkHighConcern.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkHighConcern.TabIndex = 2;
            this.chkHighConcern.Text = "是特殊生或高關懷學生";
            this.chkHighConcern.CheckedChanged += new System.EventHandler(this.chkHighConcern_CheckedChanged);
            // 
            // txtCount
            // 
            // 
            // 
            // 
            this.txtCount.Border.Class = "TextBoxBorder";
            this.txtCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCount.Location = new System.Drawing.Point(261, 23);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(40, 25);
            this.txtCount.TabIndex = 3;
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(318, 25);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 21);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "文號";
            // 
            // txtDocNo
            // 
            // 
            // 
            // 
            this.txtDocNo.Border.Class = "TextBoxBorder";
            this.txtDocNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDocNo.Location = new System.Drawing.Point(357, 23);
            this.txtDocNo.Name = "txtDocNo";
            this.txtDocNo.Size = new System.Drawing.Size(179, 25);
            this.txtDocNo.TabIndex = 5;
            this.txtDocNo.TextChanged += new System.EventHandler(this.txtDocNo_TextChanged);
            // 
            // txtEDoc
            // 
            // 
            // 
            // 
            this.txtEDoc.Border.Class = "TextBoxBorder";
            this.txtEDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEDoc.Location = new System.Drawing.Point(144, 58);
            this.txtEDoc.Name = "txtEDoc";
            this.txtEDoc.Size = new System.Drawing.Size(393, 25);
            this.txtEDoc.TabIndex = 7;
            this.txtEDoc.TextChanged += new System.EventHandler(this.txtEDoc_TextChanged);
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(24, 60);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(114, 21);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "相關證明文件網址";
            // 
            // HighConcernContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtEDoc);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtDocNo);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.chkHighConcern);
            this.Controls.Add(this.labelX1);
            this.Name = "HighConcernContent";
            this.Size = new System.Drawing.Size(550, 100);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkHighConcern;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCount;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDocNo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEDoc;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}
