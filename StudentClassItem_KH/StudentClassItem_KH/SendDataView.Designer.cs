namespace StudentClassItem_KH
{
    partial class SendDataView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.dtBeginDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtEndDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEDoc = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colR1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colR2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.btnEdit = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
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
            this.labelX1.Location = new System.Drawing.Point(50, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "開始日期";
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
            this.labelX2.Location = new System.Drawing.Point(310, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 21);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "結束日期";
            // 
            // dtBeginDate
            // 
            this.dtBeginDate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtBeginDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtBeginDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBeginDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtBeginDate.ButtonDropDown.Visible = true;
            this.dtBeginDate.IsPopupCalendarOpen = false;
            this.dtBeginDate.Location = new System.Drawing.Point(116, 11);
            // 
            // 
            // 
            this.dtBeginDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtBeginDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtBeginDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtBeginDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBeginDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBeginDate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtBeginDate.MonthCalendar.DisplayMonth = new System.DateTime(2014, 7, 1, 0, 0, 0, 0);
            this.dtBeginDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtBeginDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBeginDate.MonthCalendar.TodayButtonVisible = true;
            this.dtBeginDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtBeginDate.Name = "dtBeginDate";
            this.dtBeginDate.Size = new System.Drawing.Size(137, 25);
            this.dtBeginDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtBeginDate.TabIndex = 2;
            // 
            // dtEndDate
            // 
            this.dtEndDate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtEndDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtEndDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtEndDate.ButtonDropDown.Visible = true;
            this.dtEndDate.IsPopupCalendarOpen = false;
            this.dtEndDate.Location = new System.Drawing.Point(376, 11);
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtEndDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtEndDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndDate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtEndDate.MonthCalendar.DisplayMonth = new System.DateTime(2014, 7, 1, 0, 0, 0, 0);
            this.dtEndDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtEndDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndDate.MonthCalendar.TodayButtonVisible = true;
            this.dtEndDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(137, 25);
            this.dtEndDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtEndDate.TabIndex = 3;
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate,
            this.colAction,
            this.colContent,
            this.colEDoc,
            this.colR1,
            this.colR2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(14, 47);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(820, 222);
            this.dgData.TabIndex = 4;
            this.dgData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellContentClick);
            this.dgData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgData_MouseDoubleClick);
            // 
            // colDate
            // 
            this.colDate.HeaderText = "日期時間";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Width = 150;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "動作";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            // 
            // colContent
            // 
            this.colContent.HeaderText = "摘要";
            this.colContent.Name = "colContent";
            this.colContent.ReadOnly = true;
            this.colContent.Width = 200;
            // 
            // colEDoc
            // 
            this.colEDoc.HeaderText = "相關證明文件網址";
            this.colEDoc.Name = "colEDoc";
            this.colEDoc.ReadOnly = true;
            this.colEDoc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colEDoc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colR1
            // 
            this.colR1.HeaderText = "審核結果";
            this.colR1.Name = "colR1";
            this.colR1.ReadOnly = true;
            // 
            // colR2
            // 
            this.colR2.HeaderText = "局端備註";
            this.colR2.Name = "colR2";
            this.colR2.ReadOnly = true;
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.AutoSize = true;
            this.btnQuery.BackColor = System.Drawing.Color.Transparent;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Location = new System.Drawing.Point(668, 279);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 25);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查詢";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(759, 279);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.AutoSize = true;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(14, 279);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 25);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "匯出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMsg.BackgroundStyle.Class = "";
            this.lblMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMsg.Location = new System.Drawing.Point(96, 280);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(297, 23);
            this.lblMsg.TabIndex = 8;
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.AutoSize = true;
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEdit.Location = new System.Drawing.Point(514, 279);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(145, 25);
            this.btnEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "修改相關證明文件網址";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // SendDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 315);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.dtBeginDate);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "SendDataView";
            this.Text = "查詢傳送局端備查資料";
            this.Load += new System.EventHandler(this.SendDataView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtBeginDate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtEndDate;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.LabelX lblMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContent;
        private System.Windows.Forms.DataGridViewLinkColumn colEDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colR1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colR2;
        private DevComponents.DotNetBar.ButtonX btnEdit;
    }
}