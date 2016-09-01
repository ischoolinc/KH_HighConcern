namespace ClassLock_KH
{
    partial class SendDataForm
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnSend = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.txtComment = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtDocNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkNUnLock = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtEDoc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnUploadFile = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 21);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(127, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "編班委員會會議日期";
            // 
            // dtDate
            // 
            this.dtDate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtDate.ButtonDropDown.Visible = true;
            this.dtDate.IsPopupCalendarOpen = false;
            this.dtDate.Location = new System.Drawing.Point(145, 19);
            // 
            // 
            // 
            this.dtDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtDate.MonthCalendar.DisplayMonth = new System.DateTime(2014, 7, 1, 0, 0, 0, 0);
            this.dtDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDate.MonthCalendar.TodayButtonVisible = true;
            this.dtDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(181, 25);
            this.dtDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtDate.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSend.AutoSize = true;
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSend.Location = new System.Drawing.Point(145, 182);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 25);
            this.btnSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "傳送";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(239, 182);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtComment
            // 
            // 
            // 
            // 
            this.txtComment.Border.Class = "TextBoxBorder";
            this.txtComment.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtComment.Location = new System.Drawing.Point(145, 54);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(181, 25);
            this.txtComment.TabIndex = 1;
            this.txtComment.WatermarkText = "請填入體育班或美術班等";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(96, 56);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 21);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "備註";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(96, 91);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(34, 21);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "文號";
            // 
            // txtDocNo
            // 
            // 
            // 
            // 
            this.txtDocNo.Border.Class = "TextBoxBorder";
            this.txtDocNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDocNo.Location = new System.Drawing.Point(145, 89);
            this.txtDocNo.Name = "txtDocNo";
            this.txtDocNo.Size = new System.Drawing.Size(181, 25);
            this.txtDocNo.TabIndex = 2;
            // 
            // chkNUnLock
            // 
            this.chkNUnLock.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkNUnLock.BackgroundStyle.Class = "";
            this.chkNUnLock.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkNUnLock.Location = new System.Drawing.Point(145, 152);
            this.chkNUnLock.Name = "chkNUnLock";
            this.chkNUnLock.Size = new System.Drawing.Size(100, 23);
            this.chkNUnLock.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkNUnLock.TabIndex = 5;
            this.chkNUnLock.Text = "不自動解鎖";
            // 
            // txtEDoc
            // 
            // 
            // 
            // 
            this.txtEDoc.Border.Class = "TextBoxBorder";
            this.txtEDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEDoc.Location = new System.Drawing.Point(145, 121);
            this.txtEDoc.Name = "txtEDoc";
            this.txtEDoc.Size = new System.Drawing.Size(100, 25);
            this.txtEDoc.TabIndex = 3;
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
            this.labelX4.Location = new System.Drawing.Point(16, 123);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(114, 21);
            this.labelX4.TabIndex = 9;
            this.labelX4.Text = "相關證明文件網址";
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUploadFile.AutoSize = true;
            this.btnUploadFile.BackColor = System.Drawing.Color.Transparent;
            this.btnUploadFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUploadFile.Location = new System.Drawing.Point(251, 121);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(75, 25);
            this.btnUploadFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUploadFile.TabIndex = 4;
            this.btnUploadFile.Text = " 上傳檔案";
            this.btnUploadFile.Click += new System.EventHandler(this.btnUploadFile_Click);
            // 
            // SendDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 222);
            this.Controls.Add(this.btnUploadFile);
            this.Controls.Add(this.txtEDoc);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.chkNUnLock);
            this.Controls.Add(this.txtDocNo);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "SendDataForm";
            this.Text = "班級鎖定";
            this.Load += new System.EventHandler(this.SendDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtDate;
        private DevComponents.DotNetBar.ButtonX btnSend;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtComment;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDocNo;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNUnLock;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEDoc;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnUploadFile;
    }
}