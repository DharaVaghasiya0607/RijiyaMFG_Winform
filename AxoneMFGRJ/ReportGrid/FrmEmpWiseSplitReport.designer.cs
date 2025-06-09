namespace AxoneMFGRJ.ReportGrid
{
	partial class FrmEmpWiseSplitReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmpWiseSplitReport));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.PanelHeader = new AxonContLib.cPanel(this.components);
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.CmbEmployee = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.MainGrd = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.Date = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField4 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.lblPrintSummary = new System.Windows.Forms.Label();
            this.lblExportSummary = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.MainGrdDetail = new DevExpress.XtraGrid.GridControl();
            this.GrdDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LblDetailPrint = new System.Windows.Forms.Label();
            this.LblExportDetail = new System.Windows.Forms.Label();
            this.PanelHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.White;
            this.PanelHeader.Controls.Add(this.panel1);
            this.PanelHeader.Controls.Add(this.CmbEmployee);
            this.PanelHeader.Controls.Add(this.cLabel1);
            this.PanelHeader.Controls.Add(this.DTPToDate);
            this.PanelHeader.Controls.Add(this.DTPFromDate);
            this.PanelHeader.Controls.Add(this.cLabel8);
            this.PanelHeader.Controls.Add(this.cLabel5);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1091, 56);
            this.PanelHeader.TabIndex = 198;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnClear);
            this.panel1.Controls.Add(this.BtnSearch);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(763, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 56);
            this.panel1.TabIndex = 157;
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseForeColor = true;
            this.BtnClear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnClear.ImageOptions.SvgImage")));
            this.BtnClear.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnClear.Location = new System.Drawing.Point(125, 14);
            this.BtnClear.LookAndFeel.SkinName = "Stardust";
            this.BtnClear.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 32);
            this.BtnClear.TabIndex = 200;
            this.BtnClear.Text = "&Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSearch.ImageOptions.SvgImage")));
            this.BtnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSearch.Location = new System.Drawing.Point(11, 14);
            this.BtnSearch.LookAndFeel.SkinName = "Stardust";
            this.BtnSearch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(109, 32);
            this.BtnSearch.TabIndex = 0;
            this.BtnSearch.Text = "&Search (F5)";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExit.ImageOptions.SvgImage")));
            this.BtnExit.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExit.Location = new System.Drawing.Point(206, 14);
            this.BtnExit.LookAndFeel.SkinName = "Stardust";
            this.BtnExit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 32);
            this.BtnExit.TabIndex = 5;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // CmbEmployee
            // 
            this.CmbEmployee.Location = new System.Drawing.Point(496, 17);
            this.CmbEmployee.Name = "CmbEmployee";
            this.CmbEmployee.Properties.AllowMultiSelect = true;
            this.CmbEmployee.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbEmployee.Properties.Appearance.Options.UseFont = true;
            this.CmbEmployee.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbEmployee.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbEmployee.Properties.DropDownRows = 20;
            this.CmbEmployee.Properties.IncrementalSearch = true;
            this.CmbEmployee.Size = new System.Drawing.Size(229, 20);
            this.CmbEmployee.TabIndex = 5;
            this.CmbEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbEmployee_KeyDown);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(419, 20);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(71, 13);
            this.cLabel1.TabIndex = 4;
            this.cLabel1.Text = "Employee";
            this.cLabel1.ToolTips = "";
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(284, 14);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(129, 24);
            this.DTPToDate.TabIndex = 3;
            this.DTPToDate.ToolTips = "";
            this.DTPToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPToDate_KeyDown);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(90, 14);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(129, 24);
            this.DTPFromDate.TabIndex = 1;
            this.DTPFromDate.ToolTips = "";
            this.DTPFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFromDate_KeyDown);
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(225, 20);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(53, 13);
            this.cLabel8.TabIndex = 2;
            this.cLabel8.Text = "ToDate";
            this.cLabel8.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(9, 20);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(75, 13);
            this.cLabel5.TabIndex = 0;
            this.cLabel5.Text = "From Date";
            this.cLabel5.ToolTips = "";
            // 
            // MainGrd
            // 
            this.MainGrd.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.MainGrd.Appearance.Cell.Options.UseFont = true;
            this.MainGrd.Appearance.Cell.Options.UseTextOptions = true;
            this.MainGrd.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrd.Appearance.CustomTotalCell.Options.UseTextOptions = true;
            this.MainGrd.Appearance.CustomTotalCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrd.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.MainGrd.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.MainGrd.Appearance.FieldValueGrandTotal.Options.UseTextOptions = true;
            this.MainGrd.Appearance.FieldValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrd.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.MainGrd.Appearance.FocusedCell.Options.UseFont = true;
            this.MainGrd.Appearance.FocusedCell.Options.UseTextOptions = true;
            this.MainGrd.Appearance.FocusedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrd.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.MainGrd.Appearance.GrandTotalCell.BackColor2 = System.Drawing.Color.White;
            this.MainGrd.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.MainGrd.Appearance.GrandTotalCell.Options.UseBackColor = true;
            this.MainGrd.Appearance.GrandTotalCell.Options.UseFont = true;
            this.MainGrd.Appearance.GrandTotalCell.Options.UseTextOptions = true;
            this.MainGrd.Appearance.GrandTotalCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrd.Appearance.Lines.BackColor = System.Drawing.Color.Gray;
            this.MainGrd.Appearance.Lines.Options.UseBackColor = true;
            this.MainGrd.Appearance.SelectedCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.MainGrd.Appearance.SelectedCell.Options.UseFont = true;
            this.MainGrd.Appearance.SelectedCell.Options.UseTextOptions = true;
            this.MainGrd.Appearance.SelectedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MainGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrd.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.Date,
            this.pivotGridField2,
            this.pivotGridField3,
            this.pivotGridField4});
            this.MainGrd.Location = new System.Drawing.Point(2, 21);
            this.MainGrd.Name = "MainGrd";
            this.MainGrd.OptionsView.ShowColumnGrandTotalHeader = false;
            this.MainGrd.OptionsView.ShowColumnGrandTotals = false;
            this.MainGrd.OptionsView.ShowColumnHeaders = false;
            this.MainGrd.OptionsView.ShowColumnTotals = false;
            this.MainGrd.OptionsView.ShowCustomTotalsForSingleValues = true;
            this.MainGrd.OptionsView.ShowDataHeaders = false;
            this.MainGrd.OptionsView.ShowFilterHeaders = false;
            this.MainGrd.OptionsView.ShowRowGrandTotalHeader = false;
            this.MainGrd.Size = new System.Drawing.Size(1087, 323);
            this.MainGrd.TabIndex = 199;
            this.MainGrd.CellDoubleClick += new DevExpress.XtraPivotGrid.PivotCellEventHandler(this.MainGrd_CellDoubleClick);
            this.MainGrd.CustomDrawFieldValue += new DevExpress.XtraPivotGrid.PivotCustomDrawFieldValueEventHandler(this.PvtGrdColor_CustomDrawFieldValue_1);
            this.MainGrd.Click += new System.EventHandler(this.MainGrd_Click);
            // 
            // Date
            // 
            this.Date.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.Date.Appearance.Cell.Options.UseFont = true;
            this.Date.Appearance.Cell.Options.UseTextOptions = true;
            this.Date.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Date.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.Date.Appearance.Header.Options.UseFont = true;
            this.Date.Appearance.Header.Options.UseTextOptions = true;
            this.Date.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Date.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.Date.Appearance.Value.Options.UseFont = true;
            this.Date.Appearance.Value.Options.UseTextOptions = true;
            this.Date.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Date.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.Date.AreaIndex = 0;
            this.Date.Caption = "Date";
            this.Date.FieldName = "ENTDATE";
            this.Date.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.Date;
            this.Date.Name = "Date";
            this.Date.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.Date.UnboundFieldName = "Date";
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotGridField2.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField2.Appearance.Cell.Options.UseTextOptions = true;
            this.pivotGridField2.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField2.Appearance.Header.Options.UseTextOptions = true;
            this.pivotGridField2.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField2.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotGridField2.Appearance.Value.Options.UseFont = true;
            this.pivotGridField2.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField2.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField2.AreaIndex = 0;
            this.pivotGridField2.Caption = "EmpCode";
            this.pivotGridField2.FieldName = "EMPCODE";
            this.pivotGridField2.Name = "pivotGridField2";
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.pivotGridField3.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField3.Appearance.Cell.Options.UseTextOptions = true;
            this.pivotGridField3.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField3.Appearance.Header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.pivotGridField3.Appearance.Header.Options.UseFont = true;
            this.pivotGridField3.Appearance.Header.Options.UseTextOptions = true;
            this.pivotGridField3.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField3.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotGridField3.Appearance.Value.Options.UseFont = true;
            this.pivotGridField3.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField3.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField3.Appearance.ValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotGridField3.Appearance.ValueGrandTotal.Options.UseFont = true;
            this.pivotGridField3.Appearance.ValueTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotGridField3.Appearance.ValueTotal.Options.UseFont = true;
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField3.AreaIndex = 0;
            this.pivotGridField3.Caption = "Pcs";
            this.pivotGridField3.FieldName = "LSPCS";
            this.pivotGridField3.Name = "pivotGridField3";
            this.pivotGridField3.Width = 58;
            // 
            // pivotGridField4
            // 
            this.pivotGridField4.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.pivotGridField4.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField4.Appearance.Cell.Options.UseTextOptions = true;
            this.pivotGridField4.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField4.Appearance.Header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.pivotGridField4.Appearance.Header.Options.UseFont = true;
            this.pivotGridField4.Appearance.Header.Options.UseTextOptions = true;
            this.pivotGridField4.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField4.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotGridField4.Appearance.Value.Options.UseFont = true;
            this.pivotGridField4.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField4.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField4.AreaIndex = 1;
            this.pivotGridField4.Caption = "Carat";
            this.pivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField4.FieldName = "LSCARAT";
            this.pivotGridField4.Name = "pivotGridField4";
            this.pivotGridField4.Width = 57;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 56);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupControl5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1091, 612);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.TabIndex = 199;
            // 
            // groupControl5
            // 
            this.groupControl5.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl5.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl5.AppearanceCaption.Options.UseFont = true;
            this.groupControl5.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl5.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl5.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl5.Controls.Add(this.MainGrd);
            this.groupControl5.Controls.Add(this.lblPrintSummary);
            this.groupControl5.Controls.Add(this.lblExportSummary);
            this.groupControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl5.Location = new System.Drawing.Point(0, 0);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(1091, 346);
            this.groupControl5.TabIndex = 196;
            this.groupControl5.Text = "Summary";
            // 
            // lblPrintSummary
            // 
            this.lblPrintSummary.AutoSize = true;
            this.lblPrintSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPrintSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblPrintSummary.ForeColor = System.Drawing.Color.Blue;
            this.lblPrintSummary.Location = new System.Drawing.Point(71, 5);
            this.lblPrintSummary.Name = "lblPrintSummary";
            this.lblPrintSummary.Size = new System.Drawing.Size(38, 13);
            this.lblPrintSummary.TabIndex = 189;
            this.lblPrintSummary.Text = "Print";
            this.lblPrintSummary.Click += new System.EventHandler(this.lblPrintSummary_Click);
            // 
            // lblExportSummary
            // 
            this.lblExportSummary.AutoSize = true;
            this.lblExportSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExportSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblExportSummary.ForeColor = System.Drawing.Color.Blue;
            this.lblExportSummary.Location = new System.Drawing.Point(9, 5);
            this.lblExportSummary.Name = "lblExportSummary";
            this.lblExportSummary.Size = new System.Drawing.Size(50, 13);
            this.lblExportSummary.TabIndex = 190;
            this.lblExportSummary.Text = "Export";
            this.lblExportSummary.Click += new System.EventHandler(this.lblExportSummary_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.MainGrdDetail);
            this.groupControl1.Controls.Add(this.LblDetailPrint);
            this.groupControl1.Controls.Add(this.LblExportDetail);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1091, 262);
            this.groupControl1.TabIndex = 197;
            this.groupControl1.Text = "Detail";
            // 
            // MainGrdDetail
            // 
            this.MainGrdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.MainGrdDetail.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGrdDetail.Location = new System.Drawing.Point(2, 21);
            this.MainGrdDetail.MainView = this.GrdDetail;
            this.MainGrdDetail.Name = "MainGrdDetail";
            this.MainGrdDetail.Size = new System.Drawing.Size(1087, 239);
            this.MainGrdDetail.TabIndex = 193;
            this.MainGrdDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetail});
            // 
            // GrdDetail
            // 
            this.GrdDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GrdDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetail.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.GrdDetail.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDetail.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.GrdDetail.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetail.Appearance.HorzLine.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.GrdDetail.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetail.Appearance.HorzLine.Options.UseFont = true;
            this.GrdDetail.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.GrdDetail.Appearance.Preview.Options.UseFont = true;
            this.GrdDetail.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.GrdDetail.Appearance.Row.Options.UseFont = true;
            this.GrdDetail.Appearance.Row.Options.UseTextOptions = true;
            this.GrdDetail.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.Appearance.RowSeparator.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.GrdDetail.Appearance.RowSeparator.Options.UseFont = true;
            this.GrdDetail.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.GrdDetail.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDetail.Appearance.TopNewRow.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetail.Appearance.TopNewRow.Options.UseFont = true;
            this.GrdDetail.Appearance.TopNewRow.Options.UseTextOptions = true;
            this.GrdDetail.Appearance.TopNewRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetail.Appearance.VertLine.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.GrdDetail.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetail.Appearance.VertLine.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetail.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetail.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDetail.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.GrdDetail.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDetail.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetail.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetail.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDetail.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetail.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDetail.ColumnPanelRowHeight = 25;
            this.GrdDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.bandedGridColumn34,
            this.bandedGridColumn33,
            this.bandedGridColumn32,
            this.bandedGridColumn22,
            this.bandedGridColumn26,
            this.bandedGridColumn27,
            this.bandedGridColumn28,
            this.bandedGridColumn29,
            this.bandedGridColumn30});
            this.GrdDetail.GridControl = this.MainGrdDetail;
            this.GrdDetail.Name = "GrdDetail";
            this.GrdDetail.OptionsBehavior.Editable = false;
            this.GrdDetail.OptionsView.AllowCellMerge = true;
            this.GrdDetail.OptionsView.ColumnAutoWidth = false;
            this.GrdDetail.OptionsView.ShowAutoFilterRow = true;
            this.GrdDetail.OptionsView.ShowFooter = true;
            this.GrdDetail.OptionsView.ShowGroupPanel = false;
            this.GrdDetail.RowHeight = 23;
            // 
            // bandedGridColumn34
            // 
            this.bandedGridColumn34.Caption = "Kapan";
            this.bandedGridColumn34.FieldName = "KAPANPKTTAG";
            this.bandedGridColumn34.Name = "bandedGridColumn34";
            this.bandedGridColumn34.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.bandedGridColumn34.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn34.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.bandedGridColumn34.Visible = true;
            this.bandedGridColumn34.VisibleIndex = 0;
            this.bandedGridColumn34.Width = 79;
            // 
            // bandedGridColumn33
            // 
            this.bandedGridColumn33.Caption = "Jangad No";
            this.bandedGridColumn33.FieldName = "JANGADNO";
            this.bandedGridColumn33.Name = "bandedGridColumn33";
            this.bandedGridColumn33.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn33.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn33.Visible = true;
            this.bandedGridColumn33.VisibleIndex = 2;
            this.bandedGridColumn33.Width = 76;
            // 
            // bandedGridColumn32
            // 
            this.bandedGridColumn32.Caption = "Return Date";
            this.bandedGridColumn32.FieldName = "RETDATE";
            this.bandedGridColumn32.Name = "bandedGridColumn32";
            this.bandedGridColumn32.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn32.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn32.Visible = true;
            this.bandedGridColumn32.VisibleIndex = 8;
            this.bandedGridColumn32.Width = 87;
            // 
            // bandedGridColumn22
            // 
            this.bandedGridColumn22.Caption = "Emp";
            this.bandedGridColumn22.FieldName = "EMPCODE";
            this.bandedGridColumn22.Name = "bandedGridColumn22";
            this.bandedGridColumn22.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn22.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn22.Visible = true;
            this.bandedGridColumn22.VisibleIndex = 1;
            // 
            // bandedGridColumn26
            // 
            this.bandedGridColumn26.Caption = "Issue Cts";
            this.bandedGridColumn26.FieldName = "ISSUECARAT";
            this.bandedGridColumn26.Name = "bandedGridColumn26";
            this.bandedGridColumn26.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn26.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn26.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.bandedGridColumn26.Visible = true;
            this.bandedGridColumn26.VisibleIndex = 3;
            // 
            // bandedGridColumn27
            // 
            this.bandedGridColumn27.Caption = "Return Cts";
            this.bandedGridColumn27.FieldName = "READYCARAT";
            this.bandedGridColumn27.Name = "bandedGridColumn27";
            this.bandedGridColumn27.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn27.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn27.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.bandedGridColumn27.Visible = true;
            this.bandedGridColumn27.VisibleIndex = 4;
            this.bandedGridColumn27.Width = 78;
            // 
            // bandedGridColumn28
            // 
            this.bandedGridColumn28.Caption = "Second Cts";
            this.bandedGridColumn28.FieldName = "SECONDCARAT";
            this.bandedGridColumn28.Name = "bandedGridColumn28";
            this.bandedGridColumn28.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn28.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn28.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.bandedGridColumn28.Visible = true;
            this.bandedGridColumn28.VisibleIndex = 5;
            this.bandedGridColumn28.Width = 82;
            // 
            // bandedGridColumn29
            // 
            this.bandedGridColumn29.Caption = "Extra Cts";
            this.bandedGridColumn29.FieldName = "EXTRACARAT";
            this.bandedGridColumn29.Name = "bandedGridColumn29";
            this.bandedGridColumn29.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn29.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn29.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.bandedGridColumn29.Visible = true;
            this.bandedGridColumn29.VisibleIndex = 6;
            // 
            // bandedGridColumn30
            // 
            this.bandedGridColumn30.Caption = "Loss Cts";
            this.bandedGridColumn30.FieldName = "LOSSCARAT";
            this.bandedGridColumn30.Name = "bandedGridColumn30";
            this.bandedGridColumn30.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn30.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn30.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.bandedGridColumn30.Visible = true;
            this.bandedGridColumn30.VisibleIndex = 7;
            // 
            // LblDetailPrint
            // 
            this.LblDetailPrint.AutoSize = true;
            this.LblDetailPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LblDetailPrint.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.LblDetailPrint.ForeColor = System.Drawing.Color.Blue;
            this.LblDetailPrint.Location = new System.Drawing.Point(70, 2);
            this.LblDetailPrint.Name = "LblDetailPrint";
            this.LblDetailPrint.Size = new System.Drawing.Size(38, 13);
            this.LblDetailPrint.TabIndex = 191;
            this.LblDetailPrint.Text = "Print";
            this.LblDetailPrint.Click += new System.EventHandler(this.LblDetailPrint_Click);
            // 
            // LblExportDetail
            // 
            this.LblExportDetail.AutoSize = true;
            this.LblExportDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LblExportDetail.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.LblExportDetail.ForeColor = System.Drawing.Color.Blue;
            this.LblExportDetail.Location = new System.Drawing.Point(9, 2);
            this.LblExportDetail.Name = "LblExportDetail";
            this.LblExportDetail.Size = new System.Drawing.Size(50, 13);
            this.LblExportDetail.TabIndex = 192;
            this.LblExportDetail.Text = "Export";
            this.LblExportDetail.Click += new System.EventHandler(this.LblExportDetail_Click);
            // 
            // FrmEmpWiseSplitReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 668);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmEmpWiseSplitReport";
            this.Text = "EMPLOYEE WISE SPLIT REPORT";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEmpWiseSplitReport_KeyDown);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CmbEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetail)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private AxonContLib.cPanel PanelHeader;
		private AxonContLib.cDateTimePicker DTPToDate;
		private DevExpress.XtraEditors.SimpleButton BtnSearch;
		private AxonContLib.cDateTimePicker DTPFromDate;
		private DevExpress.XtraEditors.SimpleButton BtnExit;
		private AxonContLib.cLabel cLabel8;
		private AxonContLib.cLabel cLabel5;
		private AxonContLib.cLabel cLabel1;
		private AxonContLib.cPanel panel1;
		private DevExpress.XtraEditors.CheckedComboBoxEdit CmbEmployee;
		private DevExpress.XtraPivotGrid.PivotGridControl MainGrd;
		private DevExpress.XtraPivotGrid.PivotGridField Date;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
		private DevExpress.XtraPivotGrid.PivotGridField pivotGridField4;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraGrid.GridControl MainGrdDetail;
		private System.Windows.Forms.Label LblDetailPrint;
		private System.Windows.Forms.Label LblExportDetail;
		private DevExpress.XtraEditors.GroupControl groupControl5;
		private System.Windows.Forms.Label lblPrintSummary;
		private System.Windows.Forms.Label lblExportSummary;
		private DevExpress.XtraEditors.SimpleButton BtnClear;
		private DevExpress.XtraGrid.Views.Grid.GridView GrdDetail;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn34;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn33;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn32;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn22;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn26;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn27;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn28;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn29;
		private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn30;
	}
}