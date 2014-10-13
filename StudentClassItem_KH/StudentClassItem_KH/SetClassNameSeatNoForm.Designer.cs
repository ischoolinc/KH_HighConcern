namespace StudentClassItem_KH
{
    partial class SetClassNameSeatNoForm
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cboClassName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSeatNo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dtMeetting = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtMemo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSend = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.cboGradeYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtEDoc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetting)).BeginInit();
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
            this.lblMsg.Size = new System.Drawing.Size(194, 21);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "說明：班級修改傳送至局端備查";
            this.lblMsg.WordWrap = true;
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
            this.labelX1.Location = new System.Drawing.Point(137, 51);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(34, 21);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "班級";
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
            this.labelX2.Location = new System.Drawing.Point(328, 51);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 21);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "座號";
            // 
            // cboClassName
            // 
            this.cboClassName.DisplayMember = "Text";
            this.cboClassName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClassName.FormattingEnabled = true;
            this.cboClassName.ItemHeight = 19;
            this.cboClassName.Location = new System.Drawing.Point(177, 49);
            this.cboClassName.Name = "cboClassName";
            this.cboClassName.Size = new System.Drawing.Size(136, 25);
            this.cboClassName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboClassName.TabIndex = 1;
            this.cboClassName.SelectedIndexChanged += new System.EventHandler(this.cboClassName_SelectedIndexChanged);
            // 
            // cboSeatNo
            // 
            this.cboSeatNo.DisplayMember = "Text";
            this.cboSeatNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSeatNo.FormattingEnabled = true;
            this.cboSeatNo.ItemHeight = 19;
            this.cboSeatNo.Location = new System.Drawing.Point(366, 49);
            this.cboSeatNo.Name = "cboSeatNo";
            this.cboSeatNo.Size = new System.Drawing.Size(61, 25);
            this.cboSeatNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSeatNo.TabIndex = 2;
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
            this.labelX3.Location = new System.Drawing.Point(13, 95);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(127, 21);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "編班委員會會議日期";
            // 
            // dtMeetting
            // 
            this.dtMeetting.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtMeetting.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtMeetting.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtMeetting.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtMeetting.ButtonDropDown.Visible = true;
            this.dtMeetting.IsPopupCalendarOpen = false;
            this.dtMeetting.Location = new System.Drawing.Point(137, 93);
            // 
            // 
            // 
            this.dtMeetting.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtMeetting.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtMeetting.MonthCalendar.BackgroundStyle.Class = "";
            this.dtMeetting.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtMeetting.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtMeetting.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtMeetting.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtMeetting.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtMeetting.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtMeetting.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtMeetting.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtMeetting.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtMeetting.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtMeetting.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtMeetting.MonthCalendar.DisplayMonth = new System.DateTime(2014, 6, 1, 0, 0, 0, 0);
            this.dtMeetting.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtMeetting.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtMeetting.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtMeetting.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtMeetting.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtMeetting.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtMeetting.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtMeetting.MonthCalendar.TodayButtonVisible = true;
            this.dtMeetting.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtMeetting.Name = "dtMeetting";
            this.dtMeetting.Size = new System.Drawing.Size(292, 25);
            this.dtMeetting.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtMeetting.TabIndex = 3;
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
            this.labelX4.Location = new System.Drawing.Point(12, 136);
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
            this.txtMemo.Location = new System.Drawing.Point(54, 134);
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
            this.btnSend.Location = new System.Drawing.Point(269, 208);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 25);
            this.btnSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "確定";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(350, 208);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cboGradeYear
            // 
            this.cboGradeYear.DisplayMember = "Text";
            this.cboGradeYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGradeYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGradeYear.FormattingEnabled = true;
            this.cboGradeYear.ItemHeight = 19;
            this.cboGradeYear.Location = new System.Drawing.Point(54, 49);
            this.cboGradeYear.Name = "cboGradeYear";
            this.cboGradeYear.Size = new System.Drawing.Size(61, 25);
            this.cboGradeYear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboGradeYear.TabIndex = 0;
            this.cboGradeYear.SelectedIndexChanged += new System.EventHandler(this.cboGradeYear_SelectedIndexChanged);
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(13, 51);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(34, 21);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "年級";
            // 
            // txtEDoc
            // 
            // 
            // 
            // 
            this.txtEDoc.Border.Class = "TextBoxBorder";
            this.txtEDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEDoc.Location = new System.Drawing.Point(131, 171);
            this.txtEDoc.Multiline = true;
            this.txtEDoc.Name = "txtEDoc";
            this.txtEDoc.Size = new System.Drawing.Size(295, 25);
            this.txtEDoc.TabIndex = 10;
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
            this.labelX6.Location = new System.Drawing.Point(11, 173);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(114, 21);
            this.labelX6.TabIndex = 11;
            this.labelX6.Text = "相關證明文件網址";
            // 
            // SetClassNameSeatNoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 237);
            this.Controls.Add(this.txtEDoc);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.cboGradeYear);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.dtMeetting);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cboSeatNo);
            this.Controls.Add(this.cboClassName);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblMsg);
            this.DoubleBuffered = true;
            this.Name = "SetClassNameSeatNoForm";
            this.Text = "調整班級";
            this.Load += new System.EventHandler(this.SetClassNameSeatNoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblMsg;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboClassName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSeatNo;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtMeetting;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMemo;
        private DevComponents.DotNetBar.ButtonX btnSend;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGradeYear;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEDoc;
        private DevComponents.DotNetBar.LabelX labelX6;
    }
}