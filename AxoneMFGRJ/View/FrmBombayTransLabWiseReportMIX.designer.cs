namespace AxoneMFGRJ.View
{
    partial class FrmBombayTransLabWiseReportMIX
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
			DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
			DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
			DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition3 = new DevExpress.XtraGrid.StyleFormatCondition();
			DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition4 = new DevExpress.XtraGrid.StyleFormatCondition();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBombayTransLabWiseReportMIX));
			this.BtnRefresh = new DevExpress.XtraEditors.SimpleButton();
			this.panel1 = new AxonContLib.cPanel();
			this.DtpToDate = new System.Windows.Forms.DateTimePicker();
			this.cLabel3 = new AxonContLib.cLabel(this.components);
			this.cLabel2 = new AxonContLib.cLabel(this.components);
			this.DtpFromDate = new System.Windows.Forms.DateTimePicker();
			this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
			this.BtnBestFit = new DevExpress.XtraEditors.SimpleButton();
			this.GrpPacketSearch = new DevExpress.XtraEditors.GroupControl();
			this.MainGrdDetail = new DevExpress.XtraGrid.GridControl();
			this.GrdDetDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn52 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn55 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn56 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn58 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn59 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn60 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.lblDeptPrint = new System.Windows.Forms.Label();
			this.lblDeptExport = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.MainGrd = new DevExpress.XtraGrid.GridControl();
			this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
			this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.txtEmployee = new AxonContLib.cTextBox(this.components);
			this.lblPrintSummary = new System.Windows.Forms.Label();
			this.lblExportSummary = new System.Windows.Forms.Label();
			this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GrpPacketSearch)).BeginInit();
			this.GrpPacketSearch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainGrdDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GrdDetDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainGrd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// BtnRefresh
			// 
			this.BtnRefresh.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
			this.BtnRefresh.Appearance.Options.UseFont = true;
			this.BtnRefresh.Appearance.Options.UseTextOptions = true;
			this.BtnRefresh.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BtnRefresh.Location = new System.Drawing.Point(329, 9);
			this.BtnRefresh.Name = "BtnRefresh";
			this.BtnRefresh.Size = new System.Drawing.Size(102, 30);
			this.BtnRefresh.TabIndex = 4;
			this.BtnRefresh.Text = "Refresh (F5)";
			this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.DtpToDate);
			this.panel1.Controls.Add(this.cLabel3);
			this.panel1.Controls.Add(this.cLabel2);
			this.panel1.Controls.Add(this.DtpFromDate);
			this.panel1.Controls.Add(this.BtnExit);
			this.panel1.Controls.Add(this.BtnBestFit);
			this.panel1.Controls.Add(this.BtnRefresh);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1150, 47);
			this.panel1.TabIndex = 0;
			// 
			// DtpToDate
			// 
			this.DtpToDate.CustomFormat = "dd/MM/YYYY";
			this.DtpToDate.Font = new System.Drawing.Font("Verdana", 9F);
			this.DtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.DtpToDate.Location = new System.Drawing.Point(221, 13);
			this.DtpToDate.Name = "DtpToDate";
			this.DtpToDate.Size = new System.Drawing.Size(100, 22);
			this.DtpToDate.TabIndex = 3;
			// 
			// cLabel3
			// 
			this.cLabel3.AutoSize = true;
			this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cLabel3.ForeColor = System.Drawing.Color.Black;
			this.cLabel3.Location = new System.Drawing.Point(194, 18);
			this.cLabel3.Name = "cLabel3";
			this.cLabel3.Size = new System.Drawing.Size(23, 13);
			this.cLabel3.TabIndex = 2;
			this.cLabel3.Text = "To";
			this.cLabel3.ToolTips = "";
			// 
			// cLabel2
			// 
			this.cLabel2.AutoSize = true;
			this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cLabel2.ForeColor = System.Drawing.Color.Black;
			this.cLabel2.Location = new System.Drawing.Point(3, 18);
			this.cLabel2.Name = "cLabel2";
			this.cLabel2.Size = new System.Drawing.Size(75, 13);
			this.cLabel2.TabIndex = 0;
			this.cLabel2.Text = "From Date";
			this.cLabel2.ToolTips = "";
			// 
			// DtpFromDate
			// 
			this.DtpFromDate.CustomFormat = "dd/MM/YYYY";
			this.DtpFromDate.Font = new System.Drawing.Font("Verdana", 9F);
			this.DtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.DtpFromDate.Location = new System.Drawing.Point(83, 13);
			this.DtpFromDate.Name = "DtpFromDate";
			this.DtpFromDate.Size = new System.Drawing.Size(105, 22);
			this.DtpFromDate.TabIndex = 1;
			// 
			// BtnExit
			// 
			this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
			this.BtnExit.Appearance.Options.UseFont = true;
			this.BtnExit.Appearance.Options.UseTextOptions = true;
			this.BtnExit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BtnExit.Location = new System.Drawing.Point(510, 9);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new System.Drawing.Size(56, 30);
			this.BtnExit.TabIndex = 6;
			this.BtnExit.TabStop = false;
			this.BtnExit.Text = "Exit";
			this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// BtnBestFit
			// 
			this.BtnBestFit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
			this.BtnBestFit.Appearance.Options.UseFont = true;
			this.BtnBestFit.Appearance.Options.UseTextOptions = true;
			this.BtnBestFit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.BtnBestFit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BtnBestFit.Location = new System.Drawing.Point(435, 9);
			this.BtnBestFit.Name = "BtnBestFit";
			this.BtnBestFit.Size = new System.Drawing.Size(71, 30);
			this.BtnBestFit.TabIndex = 5;
			this.BtnBestFit.TabStop = false;
			this.BtnBestFit.Text = "Best Fit";
			this.BtnBestFit.Click += new System.EventHandler(this.BtnBestFit_Click);
			// 
			// GrpPacketSearch
			// 
			this.GrpPacketSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F);
			this.GrpPacketSearch.Appearance.Options.UseFont = true;
			this.GrpPacketSearch.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.GrpPacketSearch.AppearanceCaption.Options.UseFont = true;
			this.GrpPacketSearch.AppearanceCaption.Options.UseTextOptions = true;
			this.GrpPacketSearch.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GrpPacketSearch.Controls.Add(this.MainGrdDetail);
			this.GrpPacketSearch.Controls.Add(this.lblDeptPrint);
			this.GrpPacketSearch.Controls.Add(this.lblDeptExport);
			this.GrpPacketSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GrpPacketSearch.Location = new System.Drawing.Point(0, 0);
			this.GrpPacketSearch.LookAndFeel.SkinName = "Whiteprint";
			this.GrpPacketSearch.LookAndFeel.UseDefaultLookAndFeel = false;
			this.GrpPacketSearch.Name = "GrpPacketSearch";
			this.GrpPacketSearch.Size = new System.Drawing.Size(1150, 307);
			this.GrpPacketSearch.TabIndex = 180;
			this.GrpPacketSearch.Text = "Detail ";
			// 
			// MainGrdDetail
			// 
			this.MainGrdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainGrdDetail.Location = new System.Drawing.Point(2, 22);
			this.MainGrdDetail.MainView = this.GrdDetDetail;
			this.MainGrdDetail.Name = "MainGrdDetail";
			this.MainGrdDetail.Size = new System.Drawing.Size(1146, 283);
			this.MainGrdDetail.TabIndex = 180;
			this.MainGrdDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetDetail});
			// 
			// GrdDetDetail
			// 
			this.GrdDetDetail.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.GrdDetDetail.Appearance.FixedLine.Options.UseFont = true;
			this.GrdDetDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
			this.GrdDetDetail.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
			this.GrdDetDetail.Appearance.FocusedCell.Options.UseBackColor = true;
			this.GrdDetDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
			this.GrdDetDetail.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
			this.GrdDetDetail.Appearance.FocusedRow.Options.UseFont = true;
			this.GrdDetDetail.Appearance.FocusedRow.Options.UseForeColor = true;
			this.GrdDetDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
			this.GrdDetDetail.Appearance.FooterPanel.Options.UseFont = true;
			this.GrdDetDetail.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
			this.GrdDetDetail.Appearance.GroupFooter.Options.UseFont = true;
			this.GrdDetDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
			this.GrdDetDetail.Appearance.HeaderPanel.Options.UseFont = true;
			this.GrdDetDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.GrdDetDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GrdDetDetail.Appearance.HorzLine.BackColor = System.Drawing.Color.Black;
			this.GrdDetDetail.Appearance.HorzLine.BackColor2 = System.Drawing.Color.Black;
			this.GrdDetDetail.Appearance.HorzLine.Options.UseBackColor = true;
			this.GrdDetDetail.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
			this.GrdDetDetail.Appearance.Row.Options.UseFont = true;
			this.GrdDetDetail.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.GrdDetDetail.Appearance.SelectedRow.Options.UseFont = true;
			this.GrdDetDetail.Appearance.VertLine.BackColor = System.Drawing.Color.Black;
			this.GrdDetDetail.Appearance.VertLine.BackColor2 = System.Drawing.Color.Black;
			this.GrdDetDetail.Appearance.VertLine.Options.UseBackColor = true;
			this.GrdDetDetail.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
			this.GrdDetDetail.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
			this.GrdDetDetail.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.GrdDetDetail.AppearancePrint.EvenRow.Options.UseFont = true;
			this.GrdDetDetail.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
			this.GrdDetDetail.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.GrdDetDetail.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
			this.GrdDetDetail.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.GrdDetDetail.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
			this.GrdDetDetail.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.GrdDetDetail.AppearancePrint.Lines.BackColor = System.Drawing.Color.DarkGray;
			this.GrdDetDetail.AppearancePrint.Lines.Options.UseBackColor = true;
			this.GrdDetDetail.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
			this.GrdDetDetail.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
			this.GrdDetDetail.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.GrdDetDetail.AppearancePrint.OddRow.Options.UseFont = true;
			this.GrdDetDetail.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
			this.GrdDetDetail.AppearancePrint.Row.Options.UseFont = true;
			this.GrdDetDetail.ColumnPanelRowHeight = 25;
			this.GrdDetDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn49,
            this.gridColumn50,
            this.gridColumn51,
            this.gridColumn52,
            this.gridColumn53,
            this.gridColumn54,
            this.gridColumn55,
            this.gridColumn56,
            this.gridColumn57,
            this.gridColumn58,
            this.gridColumn59,
            this.gridColumn60});
			styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			styleFormatCondition1.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
			styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Black;
			styleFormatCondition1.Appearance.Options.UseBackColor = true;
			styleFormatCondition1.Appearance.Options.UseFont = true;
			styleFormatCondition1.Appearance.Options.UseForeColor = true;
			styleFormatCondition1.ApplyToRow = true;
			styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
			styleFormatCondition1.Expression = "[SEL]=1";
			styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			styleFormatCondition2.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
			styleFormatCondition2.Appearance.Options.UseBackColor = true;
			styleFormatCondition2.Appearance.Options.UseFont = true;
			styleFormatCondition2.ApplyToRow = true;
			styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
			styleFormatCondition2.Expression = "([CONF_DATE]=\'\' OR [CONF_DATE] IS NULL)\r\nAND [SEL] = \'True\'";
			this.GrdDetDetail.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
			this.GrdDetDetail.GridControl = this.MainGrdDetail;
			this.GrdDetDetail.Name = "GrdDetDetail";
			this.GrdDetDetail.OptionsBehavior.Editable = false;
			this.GrdDetDetail.OptionsFilter.AllowFilterEditor = false;
			this.GrdDetDetail.OptionsMenu.EnableColumnMenu = false;
			this.GrdDetDetail.OptionsMenu.EnableFooterMenu = false;
			this.GrdDetDetail.OptionsPrint.EnableAppearanceEvenRow = true;
			this.GrdDetDetail.OptionsPrint.EnableAppearanceOddRow = true;
			this.GrdDetDetail.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.GrdDetDetail.OptionsSelection.EnableAppearanceHideSelection = false;
			this.GrdDetDetail.OptionsSelection.MultiSelect = true;
			this.GrdDetDetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
			this.GrdDetDetail.OptionsView.ColumnAutoWidth = false;
			this.GrdDetDetail.OptionsView.ShowAutoFilterRow = true;
			this.GrdDetDetail.OptionsView.ShowFooter = true;
			this.GrdDetDetail.OptionsView.ShowGroupPanel = false;
			this.GrdDetDetail.RowHeight = 23;
			this.GrdDetDetail.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn3.AppearanceCell.Options.UseFont = true;
			this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn3.AppearanceHeader.Options.UseFont = true;
			this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.Caption = "Kapan";
			this.gridColumn3.FieldName = "KAPANNAME";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 0;
			this.gridColumn3.Width = 88;
			// 
			// gridColumn8
			// 
			this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn8.AppearanceCell.Options.UseFont = true;
			this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn8.AppearanceHeader.Options.UseFont = true;
			this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.Caption = "PktNo";
			this.gridColumn8.FieldName = "PACKETNO";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 1;
			this.gridColumn8.Width = 49;
			// 
			// gridColumn9
			// 
			this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn9.AppearanceCell.Options.UseFont = true;
			this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn9.AppearanceHeader.Options.UseFont = true;
			this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.Caption = "Tag";
			this.gridColumn9.FieldName = "TAG";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 2;
			this.gridColumn9.Width = 35;
			// 
			// gridColumn49
			// 
			this.gridColumn49.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn49.AppearanceCell.Options.UseFont = true;
			this.gridColumn49.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn49.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn49.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn49.AppearanceHeader.Options.UseFont = true;
			this.gridColumn49.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn49.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn49.Caption = "PrdType";
			this.gridColumn49.FieldName = "PRDTYPE";
			this.gridColumn49.Name = "gridColumn49";
			this.gridColumn49.OptionsColumn.AllowEdit = false;
			this.gridColumn49.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn49.Visible = true;
			this.gridColumn49.VisibleIndex = 4;
			this.gridColumn49.Width = 86;
			// 
			// gridColumn50
			// 
			this.gridColumn50.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn50.AppearanceCell.Options.UseFont = true;
			this.gridColumn50.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn50.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn50.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn50.AppearanceHeader.Options.UseFont = true;
			this.gridColumn50.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn50.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn50.Caption = "Date";
			this.gridColumn50.FieldName = "TRANSDATE";
			this.gridColumn50.Name = "gridColumn50";
			this.gridColumn50.OptionsColumn.AllowEdit = false;
			this.gridColumn50.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn50.Visible = true;
			this.gridColumn50.VisibleIndex = 3;
			// 
			// gridColumn51
			// 
			this.gridColumn51.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn51.AppearanceCell.Options.UseFont = true;
			this.gridColumn51.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn51.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn51.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn51.AppearanceHeader.Options.UseFont = true;
			this.gridColumn51.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn51.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn51.Caption = "Shp";
			this.gridColumn51.FieldName = "SHAPE";
			this.gridColumn51.Name = "gridColumn51";
			this.gridColumn51.OptionsColumn.AllowEdit = false;
			this.gridColumn51.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn51.Visible = true;
			this.gridColumn51.VisibleIndex = 6;
			this.gridColumn51.Width = 43;
			// 
			// gridColumn52
			// 
			this.gridColumn52.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn52.AppearanceCell.Options.UseFont = true;
			this.gridColumn52.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn52.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn52.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn52.AppearanceHeader.Options.UseFont = true;
			this.gridColumn52.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn52.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn52.Caption = "Cla";
			this.gridColumn52.FieldName = "CLARITY";
			this.gridColumn52.Name = "gridColumn52";
			this.gridColumn52.OptionsColumn.AllowEdit = false;
			this.gridColumn52.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn52.Visible = true;
			this.gridColumn52.VisibleIndex = 7;
			this.gridColumn52.Width = 41;
			// 
			// gridColumn53
			// 
			this.gridColumn53.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn53.AppearanceCell.Options.UseFont = true;
			this.gridColumn53.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn53.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn53.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn53.AppearanceHeader.Options.UseFont = true;
			this.gridColumn53.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn53.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn53.Caption = "Col";
			this.gridColumn53.FieldName = "COLOR";
			this.gridColumn53.Name = "gridColumn53";
			this.gridColumn53.OptionsColumn.AllowEdit = false;
			this.gridColumn53.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn53.Visible = true;
			this.gridColumn53.VisibleIndex = 8;
			this.gridColumn53.Width = 39;
			// 
			// gridColumn54
			// 
			this.gridColumn54.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn54.AppearanceCell.Options.UseFont = true;
			this.gridColumn54.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn54.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn54.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn54.AppearanceHeader.Options.UseFont = true;
			this.gridColumn54.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn54.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn54.Caption = "Cut";
			this.gridColumn54.FieldName = "CUT";
			this.gridColumn54.Name = "gridColumn54";
			this.gridColumn54.OptionsColumn.AllowEdit = false;
			this.gridColumn54.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn54.Visible = true;
			this.gridColumn54.VisibleIndex = 9;
			this.gridColumn54.Width = 43;
			// 
			// gridColumn55
			// 
			this.gridColumn55.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn55.AppearanceCell.Options.UseFont = true;
			this.gridColumn55.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn55.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn55.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn55.AppearanceHeader.Options.UseFont = true;
			this.gridColumn55.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn55.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn55.Caption = "Pol";
			this.gridColumn55.FieldName = "POL";
			this.gridColumn55.Name = "gridColumn55";
			this.gridColumn55.OptionsColumn.AllowEdit = false;
			this.gridColumn55.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn55.Visible = true;
			this.gridColumn55.VisibleIndex = 10;
			this.gridColumn55.Width = 38;
			// 
			// gridColumn56
			// 
			this.gridColumn56.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn56.AppearanceCell.Options.UseFont = true;
			this.gridColumn56.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn56.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn56.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn56.AppearanceHeader.Options.UseFont = true;
			this.gridColumn56.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn56.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn56.Caption = "Sym";
			this.gridColumn56.FieldName = "SYM";
			this.gridColumn56.Name = "gridColumn56";
			this.gridColumn56.OptionsColumn.AllowEdit = false;
			this.gridColumn56.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn56.Visible = true;
			this.gridColumn56.VisibleIndex = 11;
			this.gridColumn56.Width = 40;
			// 
			// gridColumn57
			// 
			this.gridColumn57.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn57.AppearanceCell.Options.UseFont = true;
			this.gridColumn57.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn57.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn57.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn57.AppearanceHeader.Options.UseFont = true;
			this.gridColumn57.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn57.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn57.Caption = "FL";
			this.gridColumn57.FieldName = "FL";
			this.gridColumn57.Name = "gridColumn57";
			this.gridColumn57.OptionsColumn.AllowEdit = false;
			this.gridColumn57.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn57.Visible = true;
			this.gridColumn57.VisibleIndex = 12;
			this.gridColumn57.Width = 37;
			// 
			// gridColumn58
			// 
			this.gridColumn58.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn58.AppearanceCell.Options.UseFont = true;
			this.gridColumn58.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn58.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn58.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn58.AppearanceHeader.Options.UseFont = true;
			this.gridColumn58.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn58.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn58.Caption = "Cts";
			this.gridColumn58.FieldName = "CARAT";
			this.gridColumn58.Name = "gridColumn58";
			this.gridColumn58.OptionsColumn.AllowEdit = false;
			this.gridColumn58.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn58.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
			this.gridColumn58.Visible = true;
			this.gridColumn58.VisibleIndex = 13;
			this.gridColumn58.Width = 53;
			// 
			// gridColumn59
			// 
			this.gridColumn59.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn59.AppearanceCell.Options.UseFont = true;
			this.gridColumn59.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn59.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn59.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn59.AppearanceHeader.Options.UseFont = true;
			this.gridColumn59.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn59.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn59.Caption = "Srt Exp Amt";
			this.gridColumn59.FieldName = "MIX_SURATEXPAMOUNT";
			this.gridColumn59.Name = "gridColumn59";
			this.gridColumn59.OptionsColumn.AllowEdit = false;
			this.gridColumn59.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn59.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
			this.gridColumn59.Visible = true;
			this.gridColumn59.VisibleIndex = 14;
			this.gridColumn59.Width = 95;
			// 
			// gridColumn60
			// 
			this.gridColumn60.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.gridColumn60.AppearanceCell.Options.UseFont = true;
			this.gridColumn60.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn60.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn60.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridColumn60.AppearanceHeader.Options.UseFont = true;
			this.gridColumn60.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn60.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn60.Caption = "Process";
			this.gridColumn60.FieldName = "PROCESS";
			this.gridColumn60.Name = "gridColumn60";
			this.gridColumn60.OptionsColumn.AllowEdit = false;
			this.gridColumn60.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn60.Visible = true;
			this.gridColumn60.VisibleIndex = 5;
			this.gridColumn60.Width = 58;
			// 
			// lblDeptPrint
			// 
			this.lblDeptPrint.AutoSize = true;
			this.lblDeptPrint.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblDeptPrint.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
			this.lblDeptPrint.ForeColor = System.Drawing.Color.Blue;
			this.lblDeptPrint.Location = new System.Drawing.Point(74, 1);
			this.lblDeptPrint.Name = "lblDeptPrint";
			this.lblDeptPrint.Size = new System.Drawing.Size(38, 13);
			this.lblDeptPrint.TabIndex = 178;
			this.lblDeptPrint.Text = "Print";
			this.lblDeptPrint.Click += new System.EventHandler(this.lblDeptPrint_Click);
			// 
			// lblDeptExport
			// 
			this.lblDeptExport.AutoSize = true;
			this.lblDeptExport.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblDeptExport.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
			this.lblDeptExport.ForeColor = System.Drawing.Color.Blue;
			this.lblDeptExport.Location = new System.Drawing.Point(12, 1);
			this.lblDeptExport.Name = "lblDeptExport";
			this.lblDeptExport.Size = new System.Drawing.Size(50, 13);
			this.lblDeptExport.TabIndex = 178;
			this.lblDeptExport.Text = "Export";
			this.lblDeptExport.Click += new System.EventHandler(this.lblDeptExport_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 47);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.GrpPacketSearch);
			this.splitContainer1.Size = new System.Drawing.Size(1150, 613);
			this.splitContainer1.SplitterDistance = 302;
			this.splitContainer1.TabIndex = 182;
			// 
			// groupControl1
			// 
			this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
			this.groupControl1.Appearance.Options.UseBackColor = true;
			this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
			this.groupControl1.AppearanceCaption.Options.UseFont = true;
			this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
			this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
			this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.groupControl1.Controls.Add(this.MainGrd);
			this.groupControl1.Controls.Add(this.txtEmployee);
			this.groupControl1.Controls.Add(this.lblPrintSummary);
			this.groupControl1.Controls.Add(this.lblExportSummary);
			this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl1.Location = new System.Drawing.Point(0, 0);
			this.groupControl1.LookAndFeel.SkinName = "Whiteprint";
			this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(1150, 302);
			this.groupControl1.TabIndex = 181;
			this.groupControl1.Text = "Summary";
			// 
			// MainGrd
			// 
			this.MainGrd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainGrd.Location = new System.Drawing.Point(2, 22);
			this.MainGrd.MainView = this.GrdDet;
			this.MainGrd.Name = "MainGrd";
			this.MainGrd.Size = new System.Drawing.Size(1146, 278);
			this.MainGrd.TabIndex = 179;
			this.MainGrd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet,
            this.gridView1});
			this.MainGrd.Paint += new System.Windows.Forms.PaintEventHandler(this.MainGrd_Paint);
			// 
			// GrdDet
			// 
			this.GrdDet.Appearance.BandPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.GrdDet.Appearance.BandPanel.Options.UseFont = true;
			this.GrdDet.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.GrdDet.Appearance.FixedLine.Options.UseFont = true;
			this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
			this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
			this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 7F);
			this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
			this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
			this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 7F);
			this.GrdDet.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
			this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
			this.GrdDet.Appearance.FocusedRow.Options.UseForeColor = true;
			this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
			this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
			this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
			this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
			this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
			this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
			this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GrdDet.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
			this.GrdDet.Appearance.HorzLine.BackColor2 = System.Drawing.Color.Gray;
			this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
			this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
			this.GrdDet.Appearance.Row.Options.UseFont = true;
			this.GrdDet.Appearance.Row.Options.UseTextOptions = true;
			this.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.GrdDet.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
			this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
			this.GrdDet.Appearance.SelectedRow.Options.UseForeColor = true;
			this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
			this.GrdDet.Appearance.VertLine.BackColor2 = System.Drawing.Color.Gray;
			this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
			this.GrdDet.AppearancePrint.BandPanel.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.GrdDet.AppearancePrint.BandPanel.Options.UseFont = true;
			this.GrdDet.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
			this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 8F);
			this.GrdDet.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.GrdDet.AppearancePrint.EvenRow.Options.UseFont = true;
			this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
			this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
			this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.GrdDet.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.GrdDet.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GrdDet.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.DarkGray;
			this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
			this.GrdDet.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
			this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
			this.GrdDet.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
			this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 8F);
			this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
			this.GrdDet.AppearancePrint.Row.Options.UseTextOptions = true;
			this.GrdDet.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand7});
			this.GrdDet.ColumnPanelRowHeight = 35;
			this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn1,
            this.bandedGridColumn6,
            this.bandedGridColumn7,
            this.bandedGridColumn8});
			this.GrdDet.FooterPanelHeight = 25;
			styleFormatCondition3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			styleFormatCondition3.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
			styleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.Black;
			styleFormatCondition3.Appearance.Options.UseBackColor = true;
			styleFormatCondition3.Appearance.Options.UseFont = true;
			styleFormatCondition3.Appearance.Options.UseForeColor = true;
			styleFormatCondition3.ApplyToRow = true;
			styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
			styleFormatCondition3.Expression = "[SEL]=1";
			styleFormatCondition4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			styleFormatCondition4.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
			styleFormatCondition4.Appearance.Options.UseBackColor = true;
			styleFormatCondition4.Appearance.Options.UseFont = true;
			styleFormatCondition4.ApplyToRow = true;
			styleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
			styleFormatCondition4.Expression = "([CONF_DATE]=\'\' OR [CONF_DATE] IS NULL)\r\nAND [SEL] = \'True\'";
			this.GrdDet.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition3,
            styleFormatCondition4});
			this.GrdDet.GridControl = this.MainGrd;
			this.GrdDet.Name = "GrdDet";
			this.GrdDet.OptionsBehavior.Editable = false;
			this.GrdDet.OptionsCustomization.AllowQuickHideColumns = false;
			this.GrdDet.OptionsCustomization.AllowRowSizing = true;
			this.GrdDet.OptionsCustomization.AllowSort = false;
			this.GrdDet.OptionsFilter.AllowFilterEditor = false;
			this.GrdDet.OptionsFilter.AllowMRUFilterList = false;
			this.GrdDet.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
			this.GrdDet.OptionsMenu.EnableColumnMenu = false;
			this.GrdDet.OptionsMenu.EnableFooterMenu = false;
			this.GrdDet.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.GrdDet.OptionsSelection.EnableAppearanceHideSelection = false;
			this.GrdDet.OptionsSelection.MultiSelect = true;
			this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
			this.GrdDet.OptionsView.ColumnAutoWidth = false;
			this.GrdDet.OptionsView.ShowAutoFilterRow = true;
			this.GrdDet.OptionsView.ShowGroupPanel = false;
			this.GrdDet.RowHeight = 23;
			this.GrdDet.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
			this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
			this.GrdDet.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GrdDet_RowCellStyle);
			this.GrdDet.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GrdDet_RowStyle);
			this.GrdDet.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDet_CustomColumnDisplayText);
			// 
			// bandedGridColumn1
			// 
			this.bandedGridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.bandedGridColumn1.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.bandedGridColumn1.AppearanceHeader.Options.UseFont = true;
			this.bandedGridColumn1.Caption = "Date";
			this.bandedGridColumn1.CustomizationCaption = "dd/MM/YYYY";
			this.bandedGridColumn1.FieldName = "TRANSDATE";
			this.bandedGridColumn1.Name = "bandedGridColumn1";
			this.bandedGridColumn1.Visible = true;
			// 
			// bandedGridColumn6
			// 
			this.bandedGridColumn6.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.bandedGridColumn6.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.bandedGridColumn6.AppearanceHeader.Options.UseFont = true;
			this.bandedGridColumn6.Caption = "Pcs";
			this.bandedGridColumn6.FieldName = "MIX_PCS";
			this.bandedGridColumn6.Name = "bandedGridColumn6";
			this.bandedGridColumn6.Visible = true;
			// 
			// bandedGridColumn7
			// 
			this.bandedGridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.bandedGridColumn7.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.bandedGridColumn7.AppearanceHeader.Options.UseFont = true;
			this.bandedGridColumn7.Caption = "Cts";
			this.bandedGridColumn7.FieldName = "MIX_CARAT";
			this.bandedGridColumn7.Name = "bandedGridColumn7";
			this.bandedGridColumn7.Visible = true;
			// 
			// bandedGridColumn8
			// 
			this.bandedGridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
			this.bandedGridColumn8.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.bandedGridColumn8.AppearanceHeader.Options.UseFont = true;
			this.bandedGridColumn8.Caption = "Surat Exp Amt";
			this.bandedGridColumn8.FieldName = "MIX_SURATEXPAMOUNT";
			this.bandedGridColumn8.Name = "bandedGridColumn8";
			this.bandedGridColumn8.Visible = true;
			// 
			// gridView1
			// 
			this.gridView1.GridControl = this.MainGrd;
			this.gridView1.Name = "gridView1";
			// 
			// txtEmployee
			// 
			this.txtEmployee.ActivationColor = true;
			this.txtEmployee.AllowTabKeyOnEnter = false;
			this.txtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEmployee.ComplusoryMsg = null;
			this.txtEmployee.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmployee.Format = "";
			this.txtEmployee.IsComplusory = false;
			this.txtEmployee.Location = new System.Drawing.Point(191, 131);
			this.txtEmployee.Name = "txtEmployee";
			this.txtEmployee.RequiredChars = "";
			this.txtEmployee.SelectAllTextOnFocus = true;
			this.txtEmployee.ShowToolTipOnFocus = false;
			this.txtEmployee.Size = new System.Drawing.Size(166, 23);
			this.txtEmployee.TabIndex = 3;
			this.txtEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtEmployee.ToolTips = "";
			this.txtEmployee.Visible = false;
			this.txtEmployee.WaterMarkText = null;
			this.txtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployee_KeyPress);
			// 
			// lblPrintSummary
			// 
			this.lblPrintSummary.AutoSize = true;
			this.lblPrintSummary.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblPrintSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
			this.lblPrintSummary.ForeColor = System.Drawing.Color.Blue;
			this.lblPrintSummary.Location = new System.Drawing.Point(68, 3);
			this.lblPrintSummary.Name = "lblPrintSummary";
			this.lblPrintSummary.Size = new System.Drawing.Size(38, 13);
			this.lblPrintSummary.TabIndex = 178;
			this.lblPrintSummary.Text = "Print";
			this.lblPrintSummary.Click += new System.EventHandler(this.lblPrintSummary_Click);
			// 
			// lblExportSummary
			// 
			this.lblExportSummary.AutoSize = true;
			this.lblExportSummary.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblExportSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
			this.lblExportSummary.ForeColor = System.Drawing.Color.Blue;
			this.lblExportSummary.Location = new System.Drawing.Point(6, 3);
			this.lblExportSummary.Name = "lblExportSummary";
			this.lblExportSummary.Size = new System.Drawing.Size(50, 13);
			this.lblExportSummary.TabIndex = 178;
			this.lblExportSummary.Text = "Export";
			this.lblExportSummary.Click += new System.EventHandler(this.lblExportSummary_Click);
			// 
			// gridBand1
			// 
			this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.gridBand1.AppearanceHeader.Options.UseFont = true;
			this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
			this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridBand1.Caption = "...";
			this.gridBand1.Columns.Add(this.bandedGridColumn1);
			this.gridBand1.Name = "gridBand1";
			this.gridBand1.VisibleIndex = 0;
			this.gridBand1.Width = 75;
			// 
			// gridBand7
			// 
			this.gridBand7.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.55F, System.Drawing.FontStyle.Bold);
			this.gridBand7.AppearanceHeader.Options.UseFont = true;
			this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
			this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridBand7.Caption = "MIX";
			this.gridBand7.Columns.Add(this.bandedGridColumn6);
			this.gridBand7.Columns.Add(this.bandedGridColumn7);
			this.gridBand7.Columns.Add(this.bandedGridColumn8);
			this.gridBand7.Name = "gridBand7";
			this.gridBand7.VisibleIndex = 1;
			this.gridBand7.Width = 225;
			// 
			// FrmBombayTransLabWiseReportMix
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1150, 660);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmBombayTransLabWiseReportMix";
			this.Text = "Bombay Transfer Lab Wise Report(Mix)";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMarkerRollingReport_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.GrpPacketSearch)).EndInit();
			this.GrpPacketSearch.ResumeLayout(false);
			this.GrpPacketSearch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainGrdDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GrdDetDetail)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			this.groupControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainGrd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnRefresh;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnBestFit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
		private DevExpress.XtraEditors.GroupControl GrpPacketSearch;
        private System.Windows.Forms.Label lblDeptPrint;
		private System.Windows.Forms.Label lblDeptExport;
		private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl MainGrd;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private AxonContLib.cTextBox txtEmployee;
        private System.Windows.Forms.Label lblPrintSummary;
        private System.Windows.Forms.Label lblExportSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.DateTimePicker DtpToDate;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel2;
        private System.Windows.Forms.DateTimePicker DtpFromDate;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
		private DevExpress.XtraGrid.GridControl MainGrdDetail;
		private DevExpress.XtraGrid.Views.Grid.GridView GrdDetDetail;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn52;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn55;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn56;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn58;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn59;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn60;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
    }
}