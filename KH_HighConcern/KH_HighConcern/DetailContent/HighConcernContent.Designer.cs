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
            this.labelX1.Location = new System.Drawing.Point(147, 25);
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
            this.chkHighConcern.Size = new System.Drawing.Size(107, 21);
            this.chkHighConcern.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkHighConcern.TabIndex = 2;
            this.chkHighConcern.Text = "是高關懷學生";
            // 
            // txtCount
            // 
            // 
            // 
            // 
            this.txtCount.Border.Class = "TextBoxBorder";
            this.txtCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCount.Location = new System.Drawing.Point(214, 23);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(40, 25);
            this.txtCount.TabIndex = 3;
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // HighConcernContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.chkHighConcern);
            this.Controls.Add(this.labelX1);
            this.Name = "HighConcernContent";
            this.Size = new System.Drawing.Size(550, 145);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkHighConcern;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCount;
    }
}
