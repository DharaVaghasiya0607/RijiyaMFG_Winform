namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmMarkerGradingComparisionPopupDetail
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
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn23 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn28 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn13 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn17 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn18 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn21 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn25 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn29 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn27 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn16 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn19 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn20 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn22 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn24 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn26 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.GrpPacketSearch = new DevExpress.XtraEditors.GroupControl();
            this.lblDeptPrint = new System.Windows.Forms.Label();
            this.lblDeptExport = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrpPacketSearch)).BeginInit();
            this.GrpPacketSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(2, 25);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1125, 467);
            this.MainGrid.TabIndex = 177;
            this.MainGrid.ToolTipController = this.toolTipController1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            this.MainGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.MainGrid_Paint);
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.BandPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.BandPanel.Options.UseFont = true;
            this.GrdDet.Appearance.BandPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.FixedLine.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
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
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.HorzLine.BackColor2 = System.Drawing.Color.Black;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.VertLine.BackColor2 = System.Drawing.Color.Black;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.DarkGray;
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand3,
            this.gridBand4});
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn1,
            this.bandedGridColumn2,
            this.bandedGridColumn3,
            this.bandedGridColumn28,
            this.bandedGridColumn29,
            this.bandedGridColumn4,
            this.bandedGridColumn5,
            this.bandedGridColumn6,
            this.bandedGridColumn7,
            this.bandedGridColumn8,
            this.bandedGridColumn9,
            this.bandedGridColumn10,
            this.bandedGridColumn11,
            this.bandedGridColumn12,
            this.bandedGridColumn13,
            this.bandedGridColumn14,
            this.bandedGridColumn15,
            this.bandedGridColumn16,
            this.bandedGridColumn17,
            this.bandedGridColumn18,
            this.bandedGridColumn19,
            this.bandedGridColumn20,
            this.bandedGridColumn21,
            this.bandedGridColumn22,
            this.bandedGridColumn23,
            this.bandedGridColumn24,
            this.bandedGridColumn25,
            this.bandedGridColumn26,
            this.bandedGridColumn27});
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
            this.GrdDet.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsCustomization.AllowQuickHideColumns = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsFilter.AllowMRUFilterList = false;
            this.GrdDet.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
            this.GrdDet.OptionsMenu.EnableColumnMenu = false;
            this.GrdDet.OptionsMenu.EnableFooterMenu = false;
            this.GrdDet.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDet.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsSelection.EnableAppearanceHideSelection = false;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "General";
            this.gridBand1.Columns.Add(this.bandedGridColumn1);
            this.gridBand1.Columns.Add(this.bandedGridColumn2);
            this.gridBand1.Columns.Add(this.bandedGridColumn3);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 225;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn1.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn1.Caption = "Kapan";
            this.bandedGridColumn1.FieldName = "KAPANNAME";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn1.Visible = true;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn2.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn2.Caption = "Pkt";
            this.bandedGridColumn2.FieldName = "PACKETNO";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.bandedGridColumn2.Visible = true;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn3.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn3.Caption = "Tag";
            this.bandedGridColumn3.FieldName = "TAG";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn3.Visible = true;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "Expected";
            this.gridBand3.Columns.Add(this.bandedGridColumn23);
            this.gridBand3.Columns.Add(this.bandedGridColumn28);
            this.gridBand3.Columns.Add(this.bandedGridColumn4);
            this.gridBand3.Columns.Add(this.bandedGridColumn5);
            this.gridBand3.Columns.Add(this.bandedGridColumn7);
            this.gridBand3.Columns.Add(this.bandedGridColumn9);
            this.gridBand3.Columns.Add(this.bandedGridColumn11);
            this.gridBand3.Columns.Add(this.bandedGridColumn13);
            this.gridBand3.Columns.Add(this.bandedGridColumn15);
            this.gridBand3.Columns.Add(this.bandedGridColumn17);
            this.gridBand3.Columns.Add(this.bandedGridColumn18);
            this.gridBand3.Columns.Add(this.bandedGridColumn21);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 1;
            this.gridBand3.Width = 900;
            // 
            // bandedGridColumn23
            // 
            this.bandedGridColumn23.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn23.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn23.Caption = "Code";
            this.bandedGridColumn23.FieldName = "EMPCODE";
            this.bandedGridColumn23.Name = "bandedGridColumn23";
            this.bandedGridColumn23.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn23.Visible = true;
            // 
            // bandedGridColumn28
            // 
            this.bandedGridColumn28.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn28.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn28.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn28.Caption = "PrdType";
            this.bandedGridColumn28.FieldName = "PRDTYPENAME";
            this.bandedGridColumn28.Name = "bandedGridColumn28";
            this.bandedGridColumn28.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn28.Visible = true;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn4.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn4.Caption = "Shp";
            this.bandedGridColumn4.FieldName = "SHAPENAME";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn4.Visible = true;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn5.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn5.Caption = "Col";
            this.bandedGridColumn5.FieldName = "COLORNAME";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn5.Visible = true;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn7.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn7.Caption = "Cla";
            this.bandedGridColumn7.FieldName = "CLARITYNAME";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn7.Visible = true;
            // 
            // bandedGridColumn9
            // 
            this.bandedGridColumn9.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn9.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn9.Caption = "Cut";
            this.bandedGridColumn9.FieldName = "CUTNAME";
            this.bandedGridColumn9.Name = "bandedGridColumn9";
            this.bandedGridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn9.Visible = true;
            // 
            // bandedGridColumn11
            // 
            this.bandedGridColumn11.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn11.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn11.Caption = "Pol";
            this.bandedGridColumn11.FieldName = "POLNAME";
            this.bandedGridColumn11.Name = "bandedGridColumn11";
            this.bandedGridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn11.Visible = true;
            // 
            // bandedGridColumn13
            // 
            this.bandedGridColumn13.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn13.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn13.Caption = "Sym";
            this.bandedGridColumn13.FieldName = "SYMNAME";
            this.bandedGridColumn13.Name = "bandedGridColumn13";
            this.bandedGridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn13.Visible = true;
            // 
            // bandedGridColumn15
            // 
            this.bandedGridColumn15.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn15.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn15.Caption = "Cts";
            this.bandedGridColumn15.FieldName = "CARAT";
            this.bandedGridColumn15.Name = "bandedGridColumn15";
            this.bandedGridColumn15.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn15.Visible = true;
            // 
            // bandedGridColumn17
            // 
            this.bandedGridColumn17.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn17.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn17.Caption = "$/Cts";
            this.bandedGridColumn17.FieldName = "PRICEPERCARAT";
            this.bandedGridColumn17.Name = "bandedGridColumn17";
            this.bandedGridColumn17.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn17.Visible = true;
            // 
            // bandedGridColumn18
            // 
            this.bandedGridColumn18.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn18.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn18.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn18.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn18.Caption = "Amt";
            this.bandedGridColumn18.FieldName = "AMOUNT";
            this.bandedGridColumn18.Name = "bandedGridColumn18";
            this.bandedGridColumn18.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn18.Visible = true;
            // 
            // bandedGridColumn21
            // 
            this.bandedGridColumn21.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn21.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn21.Caption = "Date";
            this.bandedGridColumn21.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.bandedGridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.bandedGridColumn21.FieldName = "ENTRYDATE";
            this.bandedGridColumn21.Name = "bandedGridColumn21";
            this.bandedGridColumn21.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn21.Visible = true;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "Grading";
            this.gridBand4.Columns.Add(this.bandedGridColumn25);
            this.gridBand4.Columns.Add(this.bandedGridColumn29);
            this.gridBand4.Columns.Add(this.bandedGridColumn27);
            this.gridBand4.Columns.Add(this.bandedGridColumn6);
            this.gridBand4.Columns.Add(this.bandedGridColumn8);
            this.gridBand4.Columns.Add(this.bandedGridColumn10);
            this.gridBand4.Columns.Add(this.bandedGridColumn12);
            this.gridBand4.Columns.Add(this.bandedGridColumn14);
            this.gridBand4.Columns.Add(this.bandedGridColumn16);
            this.gridBand4.Columns.Add(this.bandedGridColumn19);
            this.gridBand4.Columns.Add(this.bandedGridColumn20);
            this.gridBand4.Columns.Add(this.bandedGridColumn22);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 2;
            this.gridBand4.Width = 900;
            // 
            // bandedGridColumn25
            // 
            this.bandedGridColumn25.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn25.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn25.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn25.Caption = "Code";
            this.bandedGridColumn25.FieldName = "GEMPCODE";
            this.bandedGridColumn25.Name = "bandedGridColumn25";
            this.bandedGridColumn25.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn25.Visible = true;
            // 
            // bandedGridColumn29
            // 
            this.bandedGridColumn29.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn29.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn29.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn29.Caption = "PrdType";
            this.bandedGridColumn29.FieldName = "GPRDTYPE";
            this.bandedGridColumn29.Name = "bandedGridColumn29";
            this.bandedGridColumn29.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn29.Visible = true;
            // 
            // bandedGridColumn27
            // 
            this.bandedGridColumn27.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn27.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn27.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn27.Caption = "Shp";
            this.bandedGridColumn27.FieldName = "GSHAPENAME";
            this.bandedGridColumn27.Name = "bandedGridColumn27";
            this.bandedGridColumn27.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn27.Visible = true;
            // 
            // bandedGridColumn6
            // 
            this.bandedGridColumn6.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn6.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn6.Caption = "Col";
            this.bandedGridColumn6.FieldName = "GCOLORNAME";
            this.bandedGridColumn6.Name = "bandedGridColumn6";
            this.bandedGridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn6.Visible = true;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn8.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn8.Caption = "Cla";
            this.bandedGridColumn8.FieldName = "GCLARITYNAME";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn8.Visible = true;
            // 
            // bandedGridColumn10
            // 
            this.bandedGridColumn10.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn10.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn10.Caption = "Cut";
            this.bandedGridColumn10.FieldName = "GCUTNAME";
            this.bandedGridColumn10.Name = "bandedGridColumn10";
            this.bandedGridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn10.Visible = true;
            // 
            // bandedGridColumn12
            // 
            this.bandedGridColumn12.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn12.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn12.Caption = "Pol";
            this.bandedGridColumn12.FieldName = "GPOLNAME";
            this.bandedGridColumn12.Name = "bandedGridColumn12";
            this.bandedGridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn12.Visible = true;
            // 
            // bandedGridColumn14
            // 
            this.bandedGridColumn14.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn14.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn14.Caption = "Sym";
            this.bandedGridColumn14.FieldName = "GSYMNAME";
            this.bandedGridColumn14.Name = "bandedGridColumn14";
            this.bandedGridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn14.Visible = true;
            // 
            // bandedGridColumn16
            // 
            this.bandedGridColumn16.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn16.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn16.Caption = "Cts";
            this.bandedGridColumn16.FieldName = "GCARAT";
            this.bandedGridColumn16.Name = "bandedGridColumn16";
            this.bandedGridColumn16.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn16.Visible = true;
            // 
            // bandedGridColumn19
            // 
            this.bandedGridColumn19.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn19.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn19.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn19.Caption = "$/Cts";
            this.bandedGridColumn19.FieldName = "GPRICEPERCARAT";
            this.bandedGridColumn19.Name = "bandedGridColumn19";
            this.bandedGridColumn19.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn19.Visible = true;
            // 
            // bandedGridColumn20
            // 
            this.bandedGridColumn20.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn20.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn20.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn20.Caption = "Amt";
            this.bandedGridColumn20.FieldName = "GAMOUNT";
            this.bandedGridColumn20.Name = "bandedGridColumn20";
            this.bandedGridColumn20.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn20.Visible = true;
            // 
            // bandedGridColumn22
            // 
            this.bandedGridColumn22.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn22.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn22.Caption = "Date";
            this.bandedGridColumn22.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.bandedGridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.bandedGridColumn22.FieldName = "GENTRYDATE";
            this.bandedGridColumn22.Name = "bandedGridColumn22";
            this.bandedGridColumn22.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn22.Visible = true;
            // 
            // bandedGridColumn24
            // 
            this.bandedGridColumn24.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn24.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn24.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn24.Caption = "EMPNAME";
            this.bandedGridColumn24.FieldName = "EMPNAME";
            this.bandedGridColumn24.Name = "bandedGridColumn24";
            this.bandedGridColumn24.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn24.Visible = true;
            // 
            // bandedGridColumn26
            // 
            this.bandedGridColumn26.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.bandedGridColumn26.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn26.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn26.Caption = "GEMPNAME";
            this.bandedGridColumn26.FieldName = "GEMPNAME";
            this.bandedGridColumn26.Name = "bandedGridColumn26";
            this.bandedGridColumn26.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn26.Visible = true;
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // GrpPacketSearch
            // 
            this.GrpPacketSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrpPacketSearch.Appearance.Options.UseFont = true;
            this.GrpPacketSearch.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 11F);
            this.GrpPacketSearch.AppearanceCaption.Options.UseFont = true;
            this.GrpPacketSearch.AppearanceCaption.Options.UseTextOptions = true;
            this.GrpPacketSearch.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrpPacketSearch.Controls.Add(this.MainGrid);
            this.GrpPacketSearch.Controls.Add(this.lblDeptPrint);
            this.GrpPacketSearch.Controls.Add(this.lblDeptExport);
            this.GrpPacketSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpPacketSearch.Location = new System.Drawing.Point(0, 0);
            this.GrpPacketSearch.LookAndFeel.SkinName = "Whiteprint";
            this.GrpPacketSearch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GrpPacketSearch.Name = "GrpPacketSearch";
            this.GrpPacketSearch.Size = new System.Drawing.Size(1129, 494);
            this.GrpPacketSearch.TabIndex = 179;
            this.GrpPacketSearch.Text = "Department Wise Stock Summary";
            // 
            // lblDeptPrint
            // 
            this.lblDeptPrint.AutoSize = true;
            this.lblDeptPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeptPrint.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblDeptPrint.ForeColor = System.Drawing.Color.Blue;
            this.lblDeptPrint.Location = new System.Drawing.Point(74, 5);
            this.lblDeptPrint.Name = "lblDeptPrint";
            this.lblDeptPrint.Size = new System.Drawing.Size(38, 13);
            this.lblDeptPrint.TabIndex = 178;
            this.lblDeptPrint.Text = "Print";
            this.lblDeptPrint.Click += new System.EventHandler(this.lblPrint_Click);
            // 
            // lblDeptExport
            // 
            this.lblDeptExport.AutoSize = true;
            this.lblDeptExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeptExport.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblDeptExport.ForeColor = System.Drawing.Color.Blue;
            this.lblDeptExport.Location = new System.Drawing.Point(12, 5);
            this.lblDeptExport.Name = "lblDeptExport";
            this.lblDeptExport.Size = new System.Drawing.Size(50, 13);
            this.lblDeptExport.TabIndex = 178;
            this.lblDeptExport.Text = "Export";
            this.lblDeptExport.Click += new System.EventHandler(this.lblDeptExport_Click);
            // 
            // FrmMarkerGradingComparisionPopupDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1129, 494);
            this.Controls.Add(this.GrpPacketSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmMarkerGradingComparisionPopupDetail";
            this.Text = "GRADING COMPARISION DETAIL";
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrpPacketSearch)).EndInit();
            this.GrpPacketSearch.ResumeLayout(false);
            this.GrpPacketSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraEditors.GroupControl GrpPacketSearch;
        private System.Windows.Forms.Label lblDeptExport;
        private System.Windows.Forms.Label lblDeptPrint;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn23;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn13;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn15;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn17;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn18;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn21;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn25;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn27;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn12;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn16;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn19;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn20;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn22;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn24;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn26;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn28;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn29;
    }
}