namespace AxoneMFGRJ.ReportGrid
{
	partial class FrmGradingAnalysis
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new AxonContLib.cPanel();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.CmbMonthGroup = new AxonContLib.cComboBox(this.components);
            this.CmbLab = new AxonContLib.cComboBox(this.components);
            this.CmbType = new AxonContLib.cComboBox(this.components);
            this.CmbGroup = new AxonContLib.cComboBox(this.components);
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.lblRed = new AxonContLib.cLabel(this.components);
            this.lblGreen = new AxonContLib.cLabel(this.components);
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBestFit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.lblmonth = new AxonContLib.cLabel(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.XtraPgCol = new DevExpress.XtraTab.XtraTabPage();
            this.XtraPgCla = new DevExpress.XtraTab.XtraTabPage();
            this.XtraPgCut = new DevExpress.XtraTab.XtraTabPage();
            this.XtraPgPol = new DevExpress.XtraTab.XtraTabPage();
            this.XtraPgSym = new DevExpress.XtraTab.XtraTabPage();
            this.XtraPgFl = new DevExpress.XtraTab.XtraTabPage();
            this.XtraPgCps = new DevExpress.XtraTab.XtraTabPage();
            this.MainGrdCut = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField66 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField16 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField17 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField19 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField18 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pDataFieldPcs = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pDataFieldPer = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField22 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField23 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField24 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdCut)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.progressPanel1);
            this.panel1.Controls.Add(this.CmbMonthGroup);
            this.panel1.Controls.Add(this.CmbLab);
            this.panel1.Controls.Add(this.CmbType);
            this.panel1.Controls.Add(this.CmbGroup);
            this.panel1.Controls.Add(this.BtnClear);
            this.panel1.Controls.Add(this.lblRed);
            this.panel1.Controls.Add(this.lblGreen);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnExcel);
            this.panel1.Controls.Add(this.BtnPrint);
            this.panel1.Controls.Add(this.BtnBestFit);
            this.panel1.Controls.Add(this.BtnRefresh);
            this.panel1.Controls.Add(this.lblmonth);
            this.panel1.Controls.Add(this.cLabel6);
            this.panel1.Controls.Add(this.cLabel4);
            this.panel1.Controls.Add(this.cLabel2);
            this.panel1.Controls.Add(this.cLabel1);
            this.panel1.Controls.Add(this.CmbKapan);
            this.panel1.Controls.Add(this.cLabel3);
            this.panel1.Controls.Add(this.DTPToDate);
            this.panel1.Controls.Add(this.DTPFromDate);
            this.panel1.Controls.Add(this.cLabel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(863, 107);
            this.panel1.TabIndex = 1;
            // 
            // progressPanel1
            // 
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanel1.AppearanceCaption.Options.UseFont = true;
            this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel1.AppearanceDescription.Options.UseFont = true;
            this.progressPanel1.Location = new System.Drawing.Point(697, 35);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.Size = new System.Drawing.Size(154, 51);
            this.progressPanel1.TabIndex = 206;
            this.progressPanel1.Text = "progressPanel1";
            this.progressPanel1.Visible = false;
            // 
            // CmbMonthGroup
            // 
            this.CmbMonthGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMonthGroup.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbMonthGroup.ForeColor = System.Drawing.Color.Black;
            this.CmbMonthGroup.FormattingEnabled = true;
            this.CmbMonthGroup.Items.AddRange(new object[] {
            "DON\'T GROUP",
            "MONTHLY"});
            this.CmbMonthGroup.Location = new System.Drawing.Point(542, 35);
            this.CmbMonthGroup.Name = "CmbMonthGroup";
            this.CmbMonthGroup.Size = new System.Drawing.Size(149, 22);
            this.CmbMonthGroup.TabIndex = 7;
            this.CmbMonthGroup.ToolTips = "";
            // 
            // CmbLab
            // 
            this.CmbLab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbLab.ForeColor = System.Drawing.Color.Black;
            this.CmbLab.FormattingEnabled = true;
            this.CmbLab.Items.AddRange(new object[] {
            "",
            "GIA",
            "IGI",
            "HRD"});
            this.CmbLab.Location = new System.Drawing.Point(270, 35);
            this.CmbLab.Name = "CmbLab";
            this.CmbLab.Size = new System.Drawing.Size(169, 22);
            this.CmbLab.TabIndex = 6;
            this.CmbLab.ToolTips = "";
            // 
            // CmbType
            // 
            this.CmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbType.ForeColor = System.Drawing.Color.Black;
            this.CmbType.FormattingEnabled = true;
            this.CmbType.Items.AddRange(new object[] {
            "GRD",
            "LAB"});
            this.CmbType.Location = new System.Drawing.Point(684, 6);
            this.CmbType.Name = "CmbType";
            this.CmbType.Size = new System.Drawing.Size(87, 22);
            this.CmbType.TabIndex = 4;
            this.CmbType.ToolTips = "";
            // 
            // CmbGroup
            // 
            this.CmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGroup.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbGroup.ForeColor = System.Drawing.Color.Black;
            this.CmbGroup.FormattingEnabled = true;
            this.CmbGroup.Items.AddRange(new object[] {
            "DON\'T GROUP",
            "GRADER",
            "POLISH CHECKER",
            "MANAGER",
            "ENGINEER"});
            this.CmbGroup.Location = new System.Drawing.Point(51, 35);
            this.CmbGroup.Name = "CmbGroup";
            this.CmbGroup.Size = new System.Drawing.Size(174, 22);
            this.CmbGroup.TabIndex = 5;
            this.CmbGroup.ToolTips = "";
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseTextOptions = true;
            this.BtnClear.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnClear.Location = new System.Drawing.Point(487, 65);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(92, 34);
            this.BtnClear.TabIndex = 18;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblRed.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRed.ForeColor = System.Drawing.Color.Black;
            this.lblRed.Location = new System.Drawing.Point(39, 84);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(46, 13);
            this.lblRed.TabIndex = 204;
            this.lblRed.Text = "DOWN";
            this.lblRed.ToolTips = "";
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(202)))));
            this.lblGreen.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreen.ForeColor = System.Drawing.Color.Black;
            this.lblGreen.Location = new System.Drawing.Point(5, 84);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(24, 13);
            this.lblGreen.TabIndex = 205;
            this.lblGreen.Text = "UP";
            this.lblGreen.ToolTips = "";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseTextOptions = true;
            this.BtnExit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(586, 65);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(92, 34);
            this.BtnExit.TabIndex = 17;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "Exit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnExcel
            // 
            this.BtnExcel.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExcel.Appearance.Options.UseFont = true;
            this.BtnExcel.Appearance.Options.UseTextOptions = true;
            this.BtnExcel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExcel.Location = new System.Drawing.Point(291, 65);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(92, 34);
            this.BtnExcel.TabIndex = 15;
            this.BtnExcel.TabStop = false;
            this.BtnExcel.Text = "Excel";
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.Appearance.Options.UseTextOptions = true;
            this.BtnPrint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrint.Location = new System.Drawing.Point(684, 63);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(92, 34);
            this.BtnPrint.TabIndex = 201;
            this.BtnPrint.TabStop = false;
            this.BtnPrint.Text = "Print";
            this.BtnPrint.Visible = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnBestFit
            // 
            this.BtnBestFit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBestFit.Appearance.Options.UseFont = true;
            this.BtnBestFit.Appearance.Options.UseTextOptions = true;
            this.BtnBestFit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnBestFit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBestFit.Location = new System.Drawing.Point(389, 65);
            this.BtnBestFit.Name = "BtnBestFit";
            this.BtnBestFit.Size = new System.Drawing.Size(92, 34);
            this.BtnBestFit.TabIndex = 16;
            this.BtnBestFit.TabStop = false;
            this.BtnBestFit.Text = "Best Fit";
            this.BtnBestFit.Click += new System.EventHandler(this.BtnBestFit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.Appearance.Options.UseFont = true;
            this.BtnRefresh.Appearance.Options.UseTextOptions = true;
            this.BtnRefresh.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefresh.Location = new System.Drawing.Point(193, 65);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(92, 34);
            this.BtnRefresh.TabIndex = 14;
            this.BtnRefresh.Text = "Refresh (F5)";
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblmonth
            // 
            this.lblmonth.AutoSize = true;
            this.lblmonth.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.lblmonth.ForeColor = System.Drawing.Color.Black;
            this.lblmonth.Location = new System.Drawing.Point(445, 40);
            this.lblmonth.Name = "lblmonth";
            this.lblmonth.Size = new System.Drawing.Size(89, 13);
            this.lblmonth.TabIndex = 12;
            this.lblmonth.Text = "Month Group";
            this.lblmonth.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(233, 40);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(30, 13);
            this.cLabel6.TabIndex = 10;
            this.cLabel6.Text = "Lab";
            this.cLabel6.ToolTips = "";
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(3, 40);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(46, 13);
            this.cLabel4.TabIndex = 8;
            this.cLabel4.Text = "Group";
            this.cLabel4.ToolTips = "";
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(639, 11);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(39, 13);
            this.cLabel2.TabIndex = 6;
            this.cLabel2.Text = "Type";
            this.cLabel2.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(445, 11);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(57, 13);
            this.cLabel1.TabIndex = 4;
            this.cLabel1.Text = "To Date";
            this.cLabel1.ToolTips = "";
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(51, 6);
            this.CmbKapan.Name = "CmbKapan";
            this.CmbKapan.Properties.AllowMultiSelect = true;
            this.CmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.Appearance.Options.UseFont = true;
            this.CmbKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbKapan.Properties.DropDownRows = 20;
            this.CmbKapan.Properties.IncrementalSearch = true;
            this.CmbKapan.Size = new System.Drawing.Size(174, 22);
            this.CmbKapan.TabIndex = 1;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(3, 11);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(47, 13);
            this.cLabel3.TabIndex = 0;
            this.cLabel3.Text = "Kapan";
            this.cLabel3.ToolTips = "";
            // 
            // DTPToDate
            // 
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPToDate.Location = new System.Drawing.Point(509, 5);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(124, 24);
            this.DTPToDate.TabIndex = 3;
            this.DTPToDate.ToolTips = "";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(309, 5);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(130, 24);
            this.DTPFromDate.TabIndex = 2;
            this.DTPFromDate.ToolTips = "";
            this.DTPFromDate.Value = new System.DateTime(2019, 4, 9, 0, 0, 0, 0);
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(233, 11);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(75, 13);
            this.cLabel5.TabIndex = 2;
            this.cLabel5.Text = "From Date";
            this.cLabel5.ToolTips = "";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.xtraTabControl1.AppearancePage.HeaderActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseForeColor = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 107);
            this.xtraTabControl1.LookAndFeel.SkinName = "McSkin";
            this.xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl1.MultiLine = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.XtraPgCol;
            this.xtraTabControl1.Size = new System.Drawing.Size(863, 30);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.XtraPgCol,
            this.XtraPgCla,
            this.XtraPgCut,
            this.XtraPgPol,
            this.XtraPgSym,
            this.XtraPgFl,
            this.XtraPgCps});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // XtraPgCol
            // 
            this.XtraPgCol.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.XtraPgCol.Appearance.Header.Options.UseFont = true;
            this.XtraPgCol.Appearance.HeaderActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.XtraPgCol.Appearance.HeaderActive.ForeColor = System.Drawing.Color.Red;
            this.XtraPgCol.Appearance.HeaderActive.Options.UseFont = true;
            this.XtraPgCol.Appearance.HeaderActive.Options.UseForeColor = true;
            this.XtraPgCol.Name = "XtraPgCol";
            this.XtraPgCol.Size = new System.Drawing.Size(855, 0);
            this.XtraPgCol.Text = "    COLOR    ";
            // 
            // XtraPgCla
            // 
            this.XtraPgCla.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.XtraPgCla.Appearance.Header.Options.UseFont = true;
            this.XtraPgCla.Appearance.HeaderActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.XtraPgCla.Appearance.HeaderActive.ForeColor = System.Drawing.Color.Red;
            this.XtraPgCla.Appearance.HeaderActive.Options.UseFont = true;
            this.XtraPgCla.Appearance.HeaderActive.Options.UseForeColor = true;
            this.XtraPgCla.Name = "XtraPgCla";
            this.XtraPgCla.Size = new System.Drawing.Size(855, 0);
            this.XtraPgCla.Text = "    CLARITY    ";
            // 
            // XtraPgCut
            // 
            this.XtraPgCut.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.XtraPgCut.Appearance.Header.Options.UseFont = true;
            this.XtraPgCut.Appearance.HeaderActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.XtraPgCut.Appearance.HeaderActive.ForeColor = System.Drawing.Color.Red;
            this.XtraPgCut.Appearance.HeaderActive.Options.UseFont = true;
            this.XtraPgCut.Appearance.HeaderActive.Options.UseForeColor = true;
            this.XtraPgCut.Name = "XtraPgCut";
            this.XtraPgCut.Size = new System.Drawing.Size(855, 0);
            this.XtraPgCut.Text = "    CUT    ";
            // 
            // XtraPgPol
            // 
            this.XtraPgPol.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.XtraPgPol.Appearance.Header.Options.UseFont = true;
            this.XtraPgPol.Appearance.HeaderActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.XtraPgPol.Appearance.HeaderActive.ForeColor = System.Drawing.Color.Red;
            this.XtraPgPol.Appearance.HeaderActive.Options.UseFont = true;
            this.XtraPgPol.Appearance.HeaderActive.Options.UseForeColor = true;
            this.XtraPgPol.Name = "XtraPgPol";
            this.XtraPgPol.Size = new System.Drawing.Size(855, 0);
            this.XtraPgPol.Text = "    POL    ";
            // 
            // XtraPgSym
            // 
            this.XtraPgSym.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.XtraPgSym.Appearance.Header.Options.UseFont = true;
            this.XtraPgSym.Appearance.HeaderActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.XtraPgSym.Appearance.HeaderActive.ForeColor = System.Drawing.Color.Red;
            this.XtraPgSym.Appearance.HeaderActive.Options.UseFont = true;
            this.XtraPgSym.Appearance.HeaderActive.Options.UseForeColor = true;
            this.XtraPgSym.Name = "XtraPgSym";
            this.XtraPgSym.Size = new System.Drawing.Size(855, 0);
            this.XtraPgSym.Text = "    SYM    ";
            // 
            // XtraPgFl
            // 
            this.XtraPgFl.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.XtraPgFl.Appearance.Header.Options.UseFont = true;
            this.XtraPgFl.Appearance.HeaderActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.XtraPgFl.Appearance.HeaderActive.ForeColor = System.Drawing.Color.Red;
            this.XtraPgFl.Appearance.HeaderActive.Options.UseFont = true;
            this.XtraPgFl.Appearance.HeaderActive.Options.UseForeColor = true;
            this.XtraPgFl.Name = "XtraPgFl";
            this.XtraPgFl.Size = new System.Drawing.Size(855, 0);
            this.XtraPgFl.Text = "    FL    ";
            // 
            // XtraPgCps
            // 
            this.XtraPgCps.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.XtraPgCps.Appearance.Header.Options.UseFont = true;
            this.XtraPgCps.Appearance.HeaderActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.XtraPgCps.Appearance.HeaderActive.ForeColor = System.Drawing.Color.Red;
            this.XtraPgCps.Appearance.HeaderActive.Options.UseFont = true;
            this.XtraPgCps.Appearance.HeaderActive.Options.UseForeColor = true;
            this.XtraPgCps.Name = "XtraPgCps";
            this.XtraPgCps.Size = new System.Drawing.Size(855, 0);
            this.XtraPgCps.Text = "    CPS    ";
            // 
            // MainGrdCut
            // 
            this.MainGrdCut.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.MainGrdCut.Appearance.Cell.Options.UseFont = true;
            this.MainGrdCut.Appearance.Cell.Options.UseTextOptions = true;
            this.MainGrdCut.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrdCut.Appearance.CustomTotalCell.Options.UseTextOptions = true;
            this.MainGrdCut.Appearance.CustomTotalCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrdCut.Appearance.DataHeaderArea.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.MainGrdCut.Appearance.DataHeaderArea.Options.UseFont = true;
            this.MainGrdCut.Appearance.DataHeaderArea.Options.UseTextOptions = true;
            this.MainGrdCut.Appearance.DataHeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrdCut.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.MainGrdCut.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.MainGrdCut.Appearance.FieldValueGrandTotal.Options.UseTextOptions = true;
            this.MainGrdCut.Appearance.FieldValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrdCut.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.MainGrdCut.Appearance.FocusedCell.Options.UseFont = true;
            this.MainGrdCut.Appearance.FocusedCell.Options.UseTextOptions = true;
            this.MainGrdCut.Appearance.FocusedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrdCut.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.MainGrdCut.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.MainGrdCut.Appearance.GrandTotalCell.Options.UseBackColor = true;
            this.MainGrdCut.Appearance.GrandTotalCell.Options.UseFont = true;
            this.MainGrdCut.Appearance.GrandTotalCell.Options.UseTextOptions = true;
            this.MainGrdCut.Appearance.GrandTotalCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrdCut.Appearance.Lines.BackColor = System.Drawing.Color.Gray;
            this.MainGrdCut.Appearance.Lines.Options.UseBackColor = true;
            this.MainGrdCut.Appearance.SelectedCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.MainGrdCut.Appearance.SelectedCell.Options.UseFont = true;
            this.MainGrdCut.Appearance.SelectedCell.Options.UseTextOptions = true;
            this.MainGrdCut.Appearance.SelectedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrdCut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrdCut.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField66,
            this.pivotGridField16,
            this.pivotGridField17,
            this.pivotGridField19,
            this.pivotGridField18,
            this.pDataFieldPcs,
            this.pDataFieldPer,
            this.pivotGridField22,
            this.pivotGridField23,
            this.pivotGridField24});
            this.MainGrdCut.Location = new System.Drawing.Point(0, 137);
            this.MainGrdCut.Name = "MainGrdCut";
            this.MainGrdCut.OptionsView.ShowColumnHeaders = false;
            this.MainGrdCut.OptionsView.ShowCustomTotalsForSingleValues = true;
            this.MainGrdCut.OptionsView.ShowDataHeaders = false;
            this.MainGrdCut.OptionsView.ShowFilterHeaders = false;
            this.MainGrdCut.OptionsView.ShowRowGrandTotalHeader = false;
            this.MainGrdCut.Size = new System.Drawing.Size(863, 470);
            this.MainGrdCut.TabIndex = 204;
            this.MainGrdCut.CustomFieldSort += new DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventHandler(this.MainGrdCut_CustomFieldSort);
            this.MainGrdCut.CustomCellDisplayText += new DevExpress.XtraPivotGrid.PivotCellDisplayTextEventHandler(this.MainGrdCut_CustomCellDisplayText);
            this.MainGrdCut.CustomCellValue += new System.EventHandler<DevExpress.XtraPivotGrid.PivotCellValueEventArgs>(this.MainGrdCut_CustomCellValue);
            this.MainGrdCut.CellDoubleClick += new DevExpress.XtraPivotGrid.PivotCellEventHandler(this.MainGrdCut_CellDoubleClick);
            this.MainGrdCut.CustomDrawCell += new DevExpress.XtraPivotGrid.PivotCustomDrawCellEventHandler(this.MainGrdCut_CustomDrawCell);
            // 
            // pivotGridField66
            // 
            this.pivotGridField66.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.pivotGridField66.Appearance.Header.Options.UseFont = true;
            this.pivotGridField66.Appearance.Header.Options.UseTextOptions = true;
            this.pivotGridField66.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField66.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pivotGridField66.Appearance.Value.Options.UseFont = true;
            this.pivotGridField66.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField66.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField66.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField66.AreaIndex = 1;
            this.pivotGridField66.Caption = "Year-Month";
            this.pivotGridField66.FieldName = "ENTRYDATE";
            this.pivotGridField66.Name = "pivotGridField66";
            // 
            // pivotGridField16
            // 
            this.pivotGridField16.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.pivotGridField16.Appearance.Header.Options.UseFont = true;
            this.pivotGridField16.Appearance.Header.Options.UseTextOptions = true;
            this.pivotGridField16.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField16.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pivotGridField16.Appearance.Value.Options.UseFont = true;
            this.pivotGridField16.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField16.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField16.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField16.AreaIndex = 2;
            this.pivotGridField16.Caption = "Size";
            this.pivotGridField16.FieldName = "SIZE";
            this.pivotGridField16.Name = "pivotGridField16";
            // 
            // pivotGridField17
            // 
            this.pivotGridField17.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.pivotGridField17.Appearance.Header.Options.UseFont = true;
            this.pivotGridField17.Appearance.Header.Options.UseTextOptions = true;
            this.pivotGridField17.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField17.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.6F, System.Drawing.FontStyle.Bold);
            this.pivotGridField17.Appearance.Value.Options.UseFont = true;
            this.pivotGridField17.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField17.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField17.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField17.AreaIndex = 0;
            this.pivotGridField17.Caption = "Group Type";
            this.pivotGridField17.FieldName = "GROUPTYPE";
            this.pivotGridField17.Name = "pivotGridField17";
            this.pivotGridField17.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
            // 
            // pivotGridField19
            // 
            this.pivotGridField19.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.pivotGridField19.Appearance.Header.Options.UseFont = true;
            this.pivotGridField19.Appearance.Header.Options.UseTextOptions = true;
            this.pivotGridField19.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField19.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.6F, System.Drawing.FontStyle.Bold);
            this.pivotGridField19.Appearance.Value.Options.UseFont = true;
            this.pivotGridField19.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField19.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField19.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField19.AreaIndex = 3;
            this.pivotGridField19.Caption = "Mumbai";
            this.pivotGridField19.FieldName = "MUM";
            this.pivotGridField19.Name = "pivotGridField19";
            this.pivotGridField19.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // pivotGridField18
            // 
            this.pivotGridField18.Appearance.Cell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pivotGridField18.Appearance.Cell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pivotGridField18.Appearance.Cell.Options.UseBackColor = true;
            this.pivotGridField18.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pivotGridField18.Appearance.Value.Options.UseFont = true;
            this.pivotGridField18.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField18.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField18.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField18.AreaIndex = 1;
            this.pivotGridField18.Caption = "Lab";
            this.pivotGridField18.FieldName = "LAB";
            this.pivotGridField18.Name = "pivotGridField18";
            this.pivotGridField18.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // pDataFieldPcs
            // 
            this.pDataFieldPcs.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.pDataFieldPcs.Appearance.Cell.Options.UseFont = true;
            this.pDataFieldPcs.Appearance.Cell.Options.UseTextOptions = true;
            this.pDataFieldPcs.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pDataFieldPcs.Appearance.CellTotal.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pDataFieldPcs.Appearance.CellTotal.ForeColor = System.Drawing.Color.Navy;
            this.pDataFieldPcs.Appearance.CellTotal.Options.UseFont = true;
            this.pDataFieldPcs.Appearance.CellTotal.Options.UseForeColor = true;
            this.pDataFieldPcs.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pDataFieldPcs.Appearance.Header.Options.UseFont = true;
            this.pDataFieldPcs.Appearance.Header.Options.UseTextOptions = true;
            this.pDataFieldPcs.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pDataFieldPcs.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pDataFieldPcs.Appearance.Value.Options.UseFont = true;
            this.pDataFieldPcs.Appearance.Value.Options.UseTextOptions = true;
            this.pDataFieldPcs.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pDataFieldPcs.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pDataFieldPcs.AreaIndex = 0;
            this.pDataFieldPcs.Caption = "Pcs";
            this.pDataFieldPcs.FieldName = "PCS";
            this.pDataFieldPcs.Name = "pDataFieldPcs";
            this.pDataFieldPcs.Width = 77;
            // 
            // pDataFieldPer
            // 
            this.pDataFieldPer.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.pDataFieldPer.Appearance.Cell.Options.UseFont = true;
            this.pDataFieldPer.Appearance.Cell.Options.UseTextOptions = true;
            this.pDataFieldPer.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pDataFieldPer.Appearance.CellTotal.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pDataFieldPer.Appearance.CellTotal.ForeColor = System.Drawing.Color.Navy;
            this.pDataFieldPer.Appearance.CellTotal.Options.UseFont = true;
            this.pDataFieldPer.Appearance.CellTotal.Options.UseForeColor = true;
            this.pDataFieldPer.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pDataFieldPer.Appearance.Header.Options.UseFont = true;
            this.pDataFieldPer.Appearance.Header.Options.UseTextOptions = true;
            this.pDataFieldPer.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pDataFieldPer.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.pDataFieldPer.Appearance.Value.Options.UseFont = true;
            this.pDataFieldPer.Appearance.Value.Options.UseTextOptions = true;
            this.pDataFieldPer.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pDataFieldPer.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pDataFieldPer.AreaIndex = 1;
            this.pDataFieldPer.Caption = "%";
            this.pDataFieldPer.FieldName = "PER";
            this.pDataFieldPer.Name = "pDataFieldPer";
            this.pDataFieldPer.Width = 79;
            // 
            // pivotGridField22
            // 
            this.pivotGridField22.Appearance.Value.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.pivotGridField22.Appearance.Value.Options.UseFont = true;
            this.pivotGridField22.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField22.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField22.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField22.AreaIndex = 0;
            this.pivotGridField22.Caption = "Employee";
            this.pivotGridField22.FieldName = "EMPCODE";
            this.pivotGridField22.Name = "pivotGridField22";
            // 
            // pivotGridField23
            // 
            this.pivotGridField23.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField23.AreaIndex = 2;
            this.pivotGridField23.Caption = "LABSEQNO";
            this.pivotGridField23.FieldName = "LABSEQNOCUT";
            this.pivotGridField23.Name = "pivotGridField23";
            this.pivotGridField23.Visible = false;
            // 
            // pivotGridField24
            // 
            this.pivotGridField24.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField24.AreaIndex = 2;
            this.pivotGridField24.Caption = "MUMSEQNO";
            this.pivotGridField24.FieldName = "MUMSEQNOCUT";
            this.pivotGridField24.Name = "pivotGridField24";
            this.pivotGridField24.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // FrmGradingAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 607);
            this.Controls.Add(this.MainGrdCut);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmGradingAnalysis";
            this.Text = "GRIDING ANALYSIS";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGridingAnalysis_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdCut)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private AxonContLib.cPanel panel1;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage XtraPgCol;
		private DevExpress.XtraTab.XtraTabPage XtraPgCla;
		private DevExpress.XtraTab.XtraTabPage XtraPgCut;
		private DevExpress.XtraTab.XtraTabPage XtraPgPol;
		private DevExpress.XtraTab.XtraTabPage XtraPgSym;
		private DevExpress.XtraTab.XtraTabPage XtraPgFl;
		private DevExpress.XtraTab.XtraTabPage XtraPgCps;
		private AxonContLib.cDateTimePicker DTPToDate;
		private AxonContLib.cDateTimePicker DTPFromDate;
		private AxonContLib.cLabel cLabel5;
		private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
		private AxonContLib.cLabel cLabel3;
		private AxonContLib.cLabel cLabel1;
		private AxonContLib.cLabel lblmonth;
		private AxonContLib.cLabel cLabel6;
		private AxonContLib.cLabel cLabel4;
        private AxonContLib.cLabel cLabel2;
		private DevExpress.XtraEditors.SimpleButton BtnRefresh;
		private DevExpress.XtraEditors.SimpleButton BtnExit;
		private DevExpress.XtraEditors.SimpleButton BtnExcel;
		private DevExpress.XtraEditors.SimpleButton BtnPrint;
		private DevExpress.XtraEditors.SimpleButton BtnBestFit;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private AxonContLib.cLabel lblRed;
        private AxonContLib.cLabel lblGreen;
		private DevExpress.XtraPivotGrid.PivotGridControl MainGrdCut;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField16;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField17;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField18;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField19;
		private DevExpress.XtraPivotGrid.PivotGridField pDataFieldPcs;
		private DevExpress.XtraPivotGrid.PivotGridField pDataFieldPer;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField22;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField23;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField24;
		private DevExpress.XtraEditors.SimpleButton BtnClear;
		private AxonContLib.cComboBox CmbMonthGroup;
		private AxonContLib.cComboBox CmbLab;
		private AxonContLib.cComboBox CmbType;
        private AxonContLib.cComboBox CmbGroup;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField66;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
	}
}