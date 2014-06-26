namespace StudentTransStudBase_KH{
    partial class AddTransStudBase
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
            this.cboClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cboSeatNo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.gpNew = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtNewEngName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtNewBirthPlace = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboNewGender = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtNewTel = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboNewNationality = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtNewBirthday = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.txtNewSSN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtNewName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX15 = new DevComponents.DotNetBar.LabelX();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.labelX17 = new DevComponents.DotNetBar.LabelX();
            this.labelX18 = new DevComponents.DotNetBar.LabelX();
            this.labelX19 = new DevComponents.DotNetBar.LabelX();
            this.labelX20 = new DevComponents.DotNetBar.LabelX();
            this.labelX21 = new DevComponents.DotNetBar.LabelX();
            this.labelX22 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbotStudentNumber = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.gpOld = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtEngName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtBirthPlace = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboGender = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtTel = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboNationality = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtBirthDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.txtSSN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lblStudentNum = new DevComponents.DotNetBar.LabelX();
            this.lblClassName = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lblSeatNo = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBefore = new DevComponents.DotNetBar.ButtonX();
            this.dtMeetting = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX23 = new DevComponents.DotNetBar.LabelX();
            this.txtMemo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX24 = new DevComponents.DotNetBar.LabelX();
            this.gpNew.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtNewBirthday)).BeginInit();
            this.groupPanel2.SuspendLayout();
            this.gpOld.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtBirthDate)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetting)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(6, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(42, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "班級";
            // 
            // cboClass
            // 
            this.cboClass.DisplayMember = "Text";
            this.cboClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboClass.FormattingEnabled = true;
            this.cboClass.ItemHeight = 17;
            this.cboClass.Location = new System.Drawing.Point(48, 3);
            this.cboClass.Name = "cboClass";
            this.cboClass.Size = new System.Drawing.Size(304, 23);
            this.cboClass.TabIndex = 9;
            this.cboClass.SelectedIndexChanged += new System.EventHandler(this.cboClass_SelectedIndexChanged);
            this.cboClass.TextChanged += new System.EventHandler(this.cboClassName_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 32);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(42, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "座號";
            // 
            // cboSeatNo
            // 
            this.cboSeatNo.DisplayMember = "Text";
            this.cboSeatNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSeatNo.FormattingEnabled = true;
            this.cboSeatNo.ItemHeight = 17;
            this.cboSeatNo.Location = new System.Drawing.Point(48, 32);
            this.cboSeatNo.Name = "cboSeatNo";
            this.cboSeatNo.Size = new System.Drawing.Size(63, 23);
            this.cboSeatNo.TabIndex = 10;
            this.cboSeatNo.TextChanged += new System.EventHandler(this.cboSeatNo_TextChanged);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(176, 32);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(41, 23);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "學號";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(338, 368);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "下一步";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gpNew
            // 
            this.gpNew.BackColor = System.Drawing.Color.Transparent;
            this.gpNew.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpNew.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpNew.Controls.Add(this.groupPanel4);
            this.gpNew.Controls.Add(this.groupPanel2);
            this.gpNew.Location = new System.Drawing.Point(8, 12);
            this.gpNew.Name = "gpNew";
            this.gpNew.Size = new System.Drawing.Size(396, 345);
            // 
            // 
            // 
            this.gpNew.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpNew.Style.BackColorGradientAngle = 90;
            this.gpNew.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpNew.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpNew.Style.BorderBottomWidth = 1;
            this.gpNew.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpNew.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpNew.Style.BorderLeftWidth = 1;
            this.gpNew.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpNew.Style.BorderRightWidth = 1;
            this.gpNew.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpNew.Style.BorderTopWidth = 1;
            this.gpNew.Style.Class = "";
            this.gpNew.Style.CornerDiameter = 4;
            this.gpNew.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpNew.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpNew.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpNew.StyleMouseDown.Class = "";
            this.gpNew.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpNew.StyleMouseOver.Class = "";
            this.gpNew.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpNew.TabIndex = 10;
            this.gpNew.Text = "轉入資料";
            // 
            // groupPanel4
            // 
            this.groupPanel4.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.txtNewEngName);
            this.groupPanel4.Controls.Add(this.txtNewBirthPlace);
            this.groupPanel4.Controls.Add(this.cboNewGender);
            this.groupPanel4.Controls.Add(this.txtNewTel);
            this.groupPanel4.Controls.Add(this.cboNewNationality);
            this.groupPanel4.Controls.Add(this.dtNewBirthday);
            this.groupPanel4.Controls.Add(this.txtNewSSN);
            this.groupPanel4.Controls.Add(this.txtNewName);
            this.groupPanel4.Controls.Add(this.labelX15);
            this.groupPanel4.Controls.Add(this.labelX16);
            this.groupPanel4.Controls.Add(this.labelX17);
            this.groupPanel4.Controls.Add(this.labelX18);
            this.groupPanel4.Controls.Add(this.labelX19);
            this.groupPanel4.Controls.Add(this.labelX20);
            this.groupPanel4.Controls.Add(this.labelX21);
            this.groupPanel4.Controls.Add(this.labelX22);
            this.groupPanel4.Location = new System.Drawing.Point(9, 3);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new System.Drawing.Size(376, 147);
            // 
            // 
            // 
            this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.Class = "";
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseDown.Class = "";
            this.groupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseOver.Class = "";
            this.groupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel4.TabIndex = 1;
            this.groupPanel4.Text = "基本資料";
            // 
            // txtNewEngName
            // 
            // 
            // 
            // 
            this.txtNewEngName.Border.Class = "TextBoxBorder";
            this.txtNewEngName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewEngName.Location = new System.Drawing.Point(245, 88);
            this.txtNewEngName.Name = "txtNewEngName";
            this.txtNewEngName.Size = new System.Drawing.Size(122, 23);
            this.txtNewEngName.TabIndex = 8;
            // 
            // txtNewBirthPlace
            // 
            // 
            // 
            // 
            this.txtNewBirthPlace.Border.Class = "TextBoxBorder";
            this.txtNewBirthPlace.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewBirthPlace.Location = new System.Drawing.Point(246, 59);
            this.txtNewBirthPlace.Name = "txtNewBirthPlace";
            this.txtNewBirthPlace.Size = new System.Drawing.Size(122, 23);
            this.txtNewBirthPlace.TabIndex = 6;
            // 
            // cboNewGender
            // 
            this.cboNewGender.DisplayMember = "Text";
            this.cboNewGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNewGender.FormattingEnabled = true;
            this.cboNewGender.ItemHeight = 17;
            this.cboNewGender.Location = new System.Drawing.Point(246, 30);
            this.cboNewGender.Name = "cboNewGender";
            this.cboNewGender.Size = new System.Drawing.Size(121, 23);
            this.cboNewGender.TabIndex = 4;
            // 
            // txtNewTel
            // 
            // 
            // 
            // 
            this.txtNewTel.Border.Class = "TextBoxBorder";
            this.txtNewTel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewTel.Location = new System.Drawing.Point(48, 90);
            this.txtNewTel.Name = "txtNewTel";
            this.txtNewTel.Size = new System.Drawing.Size(122, 23);
            this.txtNewTel.TabIndex = 7;
            // 
            // cboNewNationality
            // 
            this.cboNewNationality.DisplayMember = "Text";
            this.cboNewNationality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNewNationality.FormattingEnabled = true;
            this.cboNewNationality.ItemHeight = 17;
            this.cboNewNationality.Location = new System.Drawing.Point(49, 61);
            this.cboNewNationality.Name = "cboNewNationality";
            this.cboNewNationality.Size = new System.Drawing.Size(121, 23);
            this.cboNewNationality.TabIndex = 5;
            // 
            // dtNewBirthday
            // 
            // 
            // 
            // 
            this.dtNewBirthday.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtNewBirthday.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtNewBirthday.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtNewBirthday.ButtonDropDown.Visible = true;
            this.dtNewBirthday.IsPopupCalendarOpen = false;
            this.dtNewBirthday.Location = new System.Drawing.Point(48, 32);
            // 
            // 
            // 
            this.dtNewBirthday.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtNewBirthday.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtNewBirthday.MonthCalendar.BackgroundStyle.Class = "";
            this.dtNewBirthday.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtNewBirthday.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtNewBirthday.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtNewBirthday.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtNewBirthday.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtNewBirthday.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtNewBirthday.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtNewBirthday.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtNewBirthday.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtNewBirthday.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtNewBirthday.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtNewBirthday.MonthCalendar.DisplayMonth = new System.DateTime(2009, 5, 1, 0, 0, 0, 0);
            this.dtNewBirthday.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtNewBirthday.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtNewBirthday.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtNewBirthday.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtNewBirthday.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtNewBirthday.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtNewBirthday.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtNewBirthday.MonthCalendar.TodayButtonVisible = true;
            this.dtNewBirthday.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtNewBirthday.Name = "dtNewBirthday";
            this.dtNewBirthday.Size = new System.Drawing.Size(122, 23);
            this.dtNewBirthday.TabIndex = 3;
            // 
            // txtNewSSN
            // 
            // 
            // 
            // 
            this.txtNewSSN.Border.Class = "TextBoxBorder";
            this.txtNewSSN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewSSN.Enabled = false;
            this.txtNewSSN.Location = new System.Drawing.Point(246, 1);
            this.txtNewSSN.Name = "txtNewSSN";
            this.txtNewSSN.Size = new System.Drawing.Size(122, 23);
            this.txtNewSSN.TabIndex = 2;
            // 
            // txtNewName
            // 
            // 
            // 
            // 
            this.txtNewName.Border.Class = "TextBoxBorder";
            this.txtNewName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewName.Location = new System.Drawing.Point(48, 3);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(122, 23);
            this.txtNewName.TabIndex = 1;
            this.txtNewName.Leave += new System.EventHandler(this.txtNewName_Leave);
            // 
            // labelX15
            // 
            // 
            // 
            // 
            this.labelX15.BackgroundStyle.Class = "";
            this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX15.Location = new System.Drawing.Point(176, 88);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new System.Drawing.Size(64, 23);
            this.labelX15.TabIndex = 7;
            this.labelX15.Text = "英文姓名";
            // 
            // labelX16
            // 
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.Class = "";
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Location = new System.Drawing.Point(191, 59);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(49, 23);
            this.labelX16.TabIndex = 6;
            this.labelX16.Text = "出生地";
            // 
            // labelX17
            // 
            // 
            // 
            // 
            this.labelX17.BackgroundStyle.Class = "";
            this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX17.Location = new System.Drawing.Point(200, 30);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new System.Drawing.Size(40, 23);
            this.labelX17.TabIndex = 5;
            this.labelX17.Text = "性別";
            // 
            // labelX18
            // 
            // 
            // 
            // 
            this.labelX18.BackgroundStyle.Class = "";
            this.labelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX18.Location = new System.Drawing.Point(176, 1);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new System.Drawing.Size(64, 23);
            this.labelX18.TabIndex = 4;
            this.labelX18.Text = "身分證號";
            // 
            // labelX19
            // 
            // 
            // 
            // 
            this.labelX19.BackgroundStyle.Class = "";
            this.labelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX19.Location = new System.Drawing.Point(8, 90);
            this.labelX19.Name = "labelX19";
            this.labelX19.Size = new System.Drawing.Size(40, 23);
            this.labelX19.TabIndex = 3;
            this.labelX19.Text = "電話";
            // 
            // labelX20
            // 
            // 
            // 
            // 
            this.labelX20.BackgroundStyle.Class = "";
            this.labelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX20.Location = new System.Drawing.Point(8, 61);
            this.labelX20.Name = "labelX20";
            this.labelX20.Size = new System.Drawing.Size(40, 23);
            this.labelX20.TabIndex = 2;
            this.labelX20.Text = "國籍";
            // 
            // labelX21
            // 
            // 
            // 
            // 
            this.labelX21.BackgroundStyle.Class = "";
            this.labelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX21.Location = new System.Drawing.Point(8, 32);
            this.labelX21.Name = "labelX21";
            this.labelX21.Size = new System.Drawing.Size(40, 23);
            this.labelX21.TabIndex = 1;
            this.labelX21.Text = "生日";
            // 
            // labelX22
            // 
            // 
            // 
            // 
            this.labelX22.BackgroundStyle.Class = "";
            this.labelX22.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX22.Location = new System.Drawing.Point(8, 3);
            this.labelX22.Name = "labelX22";
            this.labelX22.Size = new System.Drawing.Size(40, 23);
            this.labelX22.TabIndex = 0;
            this.labelX22.Text = "姓名";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.txtMemo);
            this.groupPanel2.Controls.Add(this.labelX24);
            this.groupPanel2.Controls.Add(this.dtMeetting);
            this.groupPanel2.Controls.Add(this.labelX23);
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.cbotStudentNumber);
            this.groupPanel2.Controls.Add(this.cboClass);
            this.groupPanel2.Controls.Add(this.labelX3);
            this.groupPanel2.Controls.Add(this.labelX2);
            this.groupPanel2.Controls.Add(this.cboSeatNo);
            this.groupPanel2.Location = new System.Drawing.Point(9, 156);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(376, 156);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 12;
            this.groupPanel2.Text = "班級資料";
            // 
            // cbotStudentNumber
            // 
            this.cbotStudentNumber.DisplayMember = "Text";
            this.cbotStudentNumber.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbotStudentNumber.FormattingEnabled = true;
            this.cbotStudentNumber.ItemHeight = 17;
            this.cbotStudentNumber.Location = new System.Drawing.Point(218, 32);
            this.cbotStudentNumber.Name = "cbotStudentNumber";
            this.cbotStudentNumber.Size = new System.Drawing.Size(134, 23);
            this.cbotStudentNumber.TabIndex = 11;
            this.cbotStudentNumber.TextChanged += new System.EventHandler(this.cbotStudentNumber_TextChanged);
            // 
            // gpOld
            // 
            this.gpOld.BackColor = System.Drawing.Color.Transparent;
            this.gpOld.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpOld.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpOld.Controls.Add(this.groupPanel3);
            this.gpOld.Controls.Add(this.groupPanel1);
            this.gpOld.Location = new System.Drawing.Point(5, 12);
            this.gpOld.Name = "gpOld";
            this.gpOld.Size = new System.Drawing.Size(389, 345);
            // 
            // 
            // 
            this.gpOld.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpOld.Style.BackColorGradientAngle = 90;
            this.gpOld.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpOld.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpOld.Style.BorderBottomWidth = 1;
            this.gpOld.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpOld.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpOld.Style.BorderLeftWidth = 1;
            this.gpOld.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpOld.Style.BorderRightWidth = 1;
            this.gpOld.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpOld.Style.BorderTopWidth = 1;
            this.gpOld.Style.Class = "";
            this.gpOld.Style.CornerDiameter = 4;
            this.gpOld.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpOld.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpOld.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpOld.StyleMouseDown.Class = "";
            this.gpOld.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpOld.StyleMouseOver.Class = "";
            this.gpOld.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpOld.TabIndex = 11;
            this.gpOld.Text = "原始資料";
            // 
            // groupPanel3
            // 
            this.groupPanel3.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.txtEngName);
            this.groupPanel3.Controls.Add(this.txtBirthPlace);
            this.groupPanel3.Controls.Add(this.cboGender);
            this.groupPanel3.Controls.Add(this.txtTel);
            this.groupPanel3.Controls.Add(this.cboNationality);
            this.groupPanel3.Controls.Add(this.dtBirthDate);
            this.groupPanel3.Controls.Add(this.txtSSN);
            this.groupPanel3.Controls.Add(this.txtName);
            this.groupPanel3.Controls.Add(this.labelX7);
            this.groupPanel3.Controls.Add(this.labelX8);
            this.groupPanel3.Controls.Add(this.labelX9);
            this.groupPanel3.Controls.Add(this.labelX10);
            this.groupPanel3.Controls.Add(this.labelX11);
            this.groupPanel3.Controls.Add(this.labelX12);
            this.groupPanel3.Controls.Add(this.labelX13);
            this.groupPanel3.Controls.Add(this.labelX14);
            this.groupPanel3.Location = new System.Drawing.Point(3, 3);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(376, 147);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.Class = "";
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.Class = "";
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.Class = "";
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 12;
            this.groupPanel3.Text = "基本資料";
            // 
            // txtEngName
            // 
            // 
            // 
            // 
            this.txtEngName.Border.Class = "TextBoxBorder";
            this.txtEngName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEngName.Enabled = false;
            this.txtEngName.Location = new System.Drawing.Point(245, 88);
            this.txtEngName.Name = "txtEngName";
            this.txtEngName.Size = new System.Drawing.Size(122, 23);
            this.txtEngName.TabIndex = 15;
            // 
            // txtBirthPlace
            // 
            // 
            // 
            // 
            this.txtBirthPlace.Border.Class = "TextBoxBorder";
            this.txtBirthPlace.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBirthPlace.Enabled = false;
            this.txtBirthPlace.Location = new System.Drawing.Point(246, 59);
            this.txtBirthPlace.Name = "txtBirthPlace";
            this.txtBirthPlace.Size = new System.Drawing.Size(122, 23);
            this.txtBirthPlace.TabIndex = 14;
            // 
            // cboGender
            // 
            this.cboGender.DisplayMember = "Text";
            this.cboGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGender.Enabled = false;
            this.cboGender.FormattingEnabled = true;
            this.cboGender.ItemHeight = 17;
            this.cboGender.Location = new System.Drawing.Point(246, 30);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(121, 23);
            this.cboGender.TabIndex = 13;
            // 
            // txtTel
            // 
            // 
            // 
            // 
            this.txtTel.Border.Class = "TextBoxBorder";
            this.txtTel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTel.Enabled = false;
            this.txtTel.Location = new System.Drawing.Point(48, 90);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(122, 23);
            this.txtTel.TabIndex = 12;
            // 
            // cboNationality
            // 
            this.cboNationality.DisplayMember = "Text";
            this.cboNationality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNationality.Enabled = false;
            this.cboNationality.FormattingEnabled = true;
            this.cboNationality.ItemHeight = 17;
            this.cboNationality.Location = new System.Drawing.Point(49, 61);
            this.cboNationality.Name = "cboNationality";
            this.cboNationality.Size = new System.Drawing.Size(121, 23);
            this.cboNationality.TabIndex = 11;
            // 
            // dtBirthDate
            // 
            // 
            // 
            // 
            this.dtBirthDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtBirthDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBirthDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtBirthDate.ButtonDropDown.Visible = true;
            this.dtBirthDate.Enabled = false;
            this.dtBirthDate.IsPopupCalendarOpen = false;
            this.dtBirthDate.Location = new System.Drawing.Point(48, 32);
            // 
            // 
            // 
            this.dtBirthDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtBirthDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtBirthDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtBirthDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBirthDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtBirthDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtBirthDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtBirthDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtBirthDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtBirthDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtBirthDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtBirthDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtBirthDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBirthDate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtBirthDate.MonthCalendar.DisplayMonth = new System.DateTime(2009, 5, 1, 0, 0, 0, 0);
            this.dtBirthDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtBirthDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtBirthDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtBirthDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtBirthDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtBirthDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtBirthDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBirthDate.MonthCalendar.TodayButtonVisible = true;
            this.dtBirthDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtBirthDate.Name = "dtBirthDate";
            this.dtBirthDate.Size = new System.Drawing.Size(122, 23);
            this.dtBirthDate.TabIndex = 10;
            // 
            // txtSSN
            // 
            // 
            // 
            // 
            this.txtSSN.Border.Class = "TextBoxBorder";
            this.txtSSN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSSN.Enabled = false;
            this.txtSSN.Location = new System.Drawing.Point(246, 1);
            this.txtSSN.Name = "txtSSN";
            this.txtSSN.Size = new System.Drawing.Size(122, 23);
            this.txtSSN.TabIndex = 9;
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(48, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(122, 23);
            this.txtName.TabIndex = 8;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(176, 88);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(64, 23);
            this.labelX7.TabIndex = 7;
            this.labelX7.Text = "英文姓名";
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(191, 59);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(49, 23);
            this.labelX8.TabIndex = 6;
            this.labelX8.Text = "出生地";
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(200, 30);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(40, 23);
            this.labelX9.TabIndex = 5;
            this.labelX9.Text = "性別";
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(176, 1);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(64, 23);
            this.labelX10.TabIndex = 4;
            this.labelX10.Text = "身分證號";
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.Class = "";
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(8, 90);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(40, 23);
            this.labelX11.TabIndex = 3;
            this.labelX11.Text = "電話";
            // 
            // labelX12
            // 
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.Class = "";
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Location = new System.Drawing.Point(8, 61);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(40, 23);
            this.labelX12.TabIndex = 2;
            this.labelX12.Text = "國籍";
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.Class = "";
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(8, 32);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(40, 23);
            this.labelX13.TabIndex = 1;
            this.labelX13.Text = "生日";
            // 
            // labelX14
            // 
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.Class = "";
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Location = new System.Drawing.Point(8, 3);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(40, 23);
            this.labelX14.TabIndex = 0;
            this.labelX14.Text = "姓名";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.lblStudentNum);
            this.groupPanel1.Controls.Add(this.lblClassName);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Controls.Add(this.lblSeatNo);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Location = new System.Drawing.Point(3, 156);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(376, 156);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 11;
            this.groupPanel1.Text = "班級資料";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(14, 3);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(35, 23);
            this.labelX4.TabIndex = 5;
            this.labelX4.Text = "班級";
            // 
            // lblStudentNum
            // 
            this.lblStudentNum.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblStudentNum.BackgroundStyle.Class = "";
            this.lblStudentNum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblStudentNum.Location = new System.Drawing.Point(240, 32);
            this.lblStudentNum.Name = "lblStudentNum";
            this.lblStudentNum.Size = new System.Drawing.Size(115, 23);
            this.lblStudentNum.TabIndex = 10;
            // 
            // lblClassName
            // 
            this.lblClassName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblClassName.BackgroundStyle.Class = "";
            this.lblClassName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblClassName.Location = new System.Drawing.Point(55, 3);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(300, 23);
            this.lblClassName.TabIndex = 8;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(201, 32);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(41, 23);
            this.labelX6.TabIndex = 7;
            this.labelX6.Text = "學號";
            // 
            // lblSeatNo
            // 
            this.lblSeatNo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblSeatNo.BackgroundStyle.Class = "";
            this.lblSeatNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSeatNo.Location = new System.Drawing.Point(55, 32);
            this.lblSeatNo.Name = "lblSeatNo";
            this.lblSeatNo.Size = new System.Drawing.Size(55, 23);
            this.lblSeatNo.TabIndex = 9;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(16, 32);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(42, 23);
            this.labelX5.TabIndex = 6;
            this.labelX5.Text = "座號";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnBefore);
            this.panel1.Controls.Add(this.gpNew);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(397, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 400);
            this.panel1.TabIndex = 12;
            // 
            // btnBefore
            // 
            this.btnBefore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBefore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBefore.BackColor = System.Drawing.Color.Transparent;
            this.btnBefore.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBefore.Location = new System.Drawing.Point(268, 368);
            this.btnBefore.Name = "btnBefore";
            this.btnBefore.Size = new System.Drawing.Size(60, 23);
            this.btnBefore.TabIndex = 21;
            this.btnBefore.Text = "上一步";
            this.btnBefore.Click += new System.EventHandler(this.btnBefore_Click);
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
            this.dtMeetting.Location = new System.Drawing.Point(95, 66);
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
            this.dtMeetting.Size = new System.Drawing.Size(257, 23);
            this.dtMeetting.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtMeetting.TabIndex = 12;
            // 
            // labelX23
            // 
            this.labelX23.AutoSize = true;
            this.labelX23.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX23.BackgroundStyle.Class = "";
            this.labelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX23.Location = new System.Drawing.Point(8, 69);
            this.labelX23.Name = "labelX23";
            this.labelX23.Size = new System.Drawing.Size(81, 20);
            this.labelX23.TabIndex = 13;
            this.labelX23.Text = "編班會議日期";
            // 
            // txtMemo
            // 
            // 
            // 
            // 
            this.txtMemo.Border.Class = "TextBoxBorder";
            this.txtMemo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMemo.Location = new System.Drawing.Point(48, 95);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(304, 25);
            this.txtMemo.TabIndex = 14;
            // 
            // labelX24
            // 
            this.labelX24.AutoSize = true;
            this.labelX24.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX24.BackgroundStyle.Class = "";
            this.labelX24.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX24.Location = new System.Drawing.Point(6, 97);
            this.labelX24.Name = "labelX24";
            this.labelX24.Size = new System.Drawing.Size(31, 20);
            this.labelX24.TabIndex = 15;
            this.labelX24.Text = "備註";
            // 
            // AddTransStudBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(807, 400);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gpOld);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "AddTransStudBase";
            this.Text = "轉入學生資料(New)";
            this.Load += new System.EventHandler(this.AddTransStudBase_Load);
            this.gpNew.ResumeLayout(false);
            this.groupPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtNewBirthday)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.gpOld.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtBirthDate)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboClass;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSeatNo;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.GroupPanel gpNew;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbotStudentNumber;
        private DevComponents.DotNetBar.Controls.GroupPanel gpOld;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX lblStudentNum;
        private DevComponents.DotNetBar.LabelX lblSeatNo;
        private DevComponents.DotNetBar.LabelX lblClassName;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEngName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBirthPlace;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGender;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNationality;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtBirthDate;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSSN;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewEngName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewBirthPlace;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNewGender;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewTel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNewNationality;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtNewBirthday;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewSSN;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewName;
        private DevComponents.DotNetBar.LabelX labelX15;
        private DevComponents.DotNetBar.LabelX labelX16;
        private DevComponents.DotNetBar.LabelX labelX17;
        private DevComponents.DotNetBar.LabelX labelX18;
        private DevComponents.DotNetBar.LabelX labelX19;
        private DevComponents.DotNetBar.LabelX labelX20;
        private DevComponents.DotNetBar.LabelX labelX21;
        private DevComponents.DotNetBar.LabelX labelX22;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnBefore;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMemo;
        private DevComponents.DotNetBar.LabelX labelX24;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtMeetting;
        private DevComponents.DotNetBar.LabelX labelX23;
    }
}