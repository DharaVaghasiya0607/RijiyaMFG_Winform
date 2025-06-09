namespace AxoneMFGRJ.View
{
    partial class FrmBreakingDiffrenceDetailReport
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.SHAPECODE = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.COLORNAME = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CLARITYNAME = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CUTCODE = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.POLCODE = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.SYMCODE = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.FLNAME = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.LABNAME = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.BRKPROCS_SHAPE = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_COLOR = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_CLARITY = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_CUT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_POL = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_SYM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_FL = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_LAB = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_CTS = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BRKPROCS_AMT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.PanelHeader = new AxonContLib.cPanel();
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.lblISPCN = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.lblRapDate = new AxonContLib.cLabel(this.components);
            this.lblLab = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel12 = new AxonContLib.cLabel(this.components);
            this.lblBombay = new AxonContLib.cLabel(this.components);
            this.cLabel11 = new AxonContLib.cLabel(this.components);
            this.lblGrading = new AxonContLib.cLabel(this.components);
            this.cLabel10 = new AxonContLib.cLabel(this.components);
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.aLabel2 = new AxonContLib.cLabel(this.components);
            this.txtBreakingType = new AxonContLib.cTextBox(this.components);
            this.txtKapanName = new AxonContLib.cTextBox(this.components);
            this.txtPacketNo = new AxonContLib.cTextBox(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.MainGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGrid.Location = new System.Drawing.Point(0, 42);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.MainGrid.Size = new System.Drawing.Size(1252, 620);
            this.MainGrid.TabIndex = 18;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.BandPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.BandPanel.Options.UseFont = true;
            this.GrdDet.Appearance.BandPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GrdDet.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GrdDet.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GrdDet.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GrdDet.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand3,
            this.gridBand1,
            this.gridBand2});
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumn15,
            this.bandedGridColumn3,
            this.bandedGridColumn2,
            this.bandedGridColumn5,
            this.bandedGridColumn4,
            this.bandedGridColumn1,
            this.gridColumn11,
            this.SHAPECODE,
            this.COLORNAME,
            this.CLARITYNAME,
            this.CUTCODE,
            this.POLCODE,
            this.SYMCODE,
            this.FLNAME,
            this.LABNAME,
            this.gridColumn23,
            this.gridColumn27,
            this.gridColumn4,
            this.BRKPROCS_SHAPE,
            this.BRKPROCS_COLOR,
            this.BRKPROCS_CLARITY,
            this.BRKPROCS_CUT,
            this.BRKPROCS_POL,
            this.BRKPROCS_SYM,
            this.BRKPROCS_FL,
            this.BRKPROCS_LAB,
            this.BRKPROCS_CTS,
            this.BRKPROCS_AMT,
            this.bandedGridColumn6});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.GroupCount = 2;
            this.GrdDet.GroupRowHeight = 25;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.AllowCellMerge = true;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Standard;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.bandedGridColumn3, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.bandedGridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.GrdDet.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.GrdDet_CellMerge);
            this.GrdDet.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GrdDet_RowStyle);
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "..";
            this.gridBand3.Columns.Add(this.bandedGridColumn1);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 0;
            this.gridBand3.Width = 75;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn1.Caption = "Tag";
            this.bandedGridColumn1.FieldName = "TAG";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn1.Visible = true;
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "Before Detail";
            this.gridBand1.Columns.Add(this.gridColumn11);
            this.gridBand1.Columns.Add(this.SHAPECODE);
            this.gridBand1.Columns.Add(this.COLORNAME);
            this.gridBand1.Columns.Add(this.CLARITYNAME);
            this.gridBand1.Columns.Add(this.CUTCODE);
            this.gridBand1.Columns.Add(this.POLCODE);
            this.gridBand1.Columns.Add(this.SYMCODE);
            this.gridBand1.Columns.Add(this.FLNAME);
            this.gridBand1.Columns.Add(this.LABNAME);
            this.gridBand1.Columns.Add(this.gridColumn23);
            this.gridBand1.Columns.Add(this.gridColumn27);
            this.gridBand1.Columns.Add(this.bandedGridColumn6);
            this.gridBand1.Columns.Add(this.gridColumn4);
            this.gridBand1.Columns.Add(this.gridColumn15);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 1;
            this.gridBand1.Width = 717;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn11.AppearanceCell.Options.UseFont = true;
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "Prd";
            this.gridColumn11.FieldName = "PRDTYPE";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn11.Width = 137;
            // 
            // SHAPECODE
            // 
            this.SHAPECODE.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.SHAPECODE.AppearanceCell.Options.UseFont = true;
            this.SHAPECODE.AppearanceCell.Options.UseTextOptions = true;
            this.SHAPECODE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SHAPECODE.Caption = "Shp";
            this.SHAPECODE.FieldName = "SHAPENAME";
            this.SHAPECODE.Name = "SHAPECODE";
            this.SHAPECODE.OptionsColumn.AllowEdit = false;
            this.SHAPECODE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.SHAPECODE.Visible = true;
            this.SHAPECODE.Width = 60;
            // 
            // COLORNAME
            // 
            this.COLORNAME.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.COLORNAME.AppearanceCell.Options.UseFont = true;
            this.COLORNAME.AppearanceCell.Options.UseTextOptions = true;
            this.COLORNAME.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.COLORNAME.Caption = "Col";
            this.COLORNAME.FieldName = "COLORNAME";
            this.COLORNAME.Name = "COLORNAME";
            this.COLORNAME.OptionsColumn.AllowEdit = false;
            this.COLORNAME.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.COLORNAME.Visible = true;
            this.COLORNAME.Width = 60;
            // 
            // CLARITYNAME
            // 
            this.CLARITYNAME.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.CLARITYNAME.AppearanceCell.Options.UseFont = true;
            this.CLARITYNAME.AppearanceCell.Options.UseTextOptions = true;
            this.CLARITYNAME.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CLARITYNAME.Caption = "Cla";
            this.CLARITYNAME.FieldName = "CLARITYNAME";
            this.CLARITYNAME.Name = "CLARITYNAME";
            this.CLARITYNAME.OptionsColumn.AllowEdit = false;
            this.CLARITYNAME.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.CLARITYNAME.Visible = true;
            this.CLARITYNAME.Width = 60;
            // 
            // CUTCODE
            // 
            this.CUTCODE.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.CUTCODE.AppearanceCell.Options.UseFont = true;
            this.CUTCODE.AppearanceCell.Options.UseTextOptions = true;
            this.CUTCODE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CUTCODE.Caption = "Cut";
            this.CUTCODE.FieldName = "CUTNAME";
            this.CUTCODE.Name = "CUTCODE";
            this.CUTCODE.OptionsColumn.AllowEdit = false;
            this.CUTCODE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.CUTCODE.Visible = true;
            this.CUTCODE.Width = 60;
            // 
            // POLCODE
            // 
            this.POLCODE.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.POLCODE.AppearanceCell.Options.UseFont = true;
            this.POLCODE.AppearanceCell.Options.UseTextOptions = true;
            this.POLCODE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.POLCODE.Caption = "Pol";
            this.POLCODE.FieldName = "POLNAME";
            this.POLCODE.Name = "POLCODE";
            this.POLCODE.OptionsColumn.AllowEdit = false;
            this.POLCODE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.POLCODE.Visible = true;
            this.POLCODE.Width = 60;
            // 
            // SYMCODE
            // 
            this.SYMCODE.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.SYMCODE.AppearanceCell.Options.UseFont = true;
            this.SYMCODE.AppearanceCell.Options.UseTextOptions = true;
            this.SYMCODE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SYMCODE.Caption = "Sym";
            this.SYMCODE.FieldName = "SYMNAME";
            this.SYMCODE.Name = "SYMCODE";
            this.SYMCODE.OptionsColumn.AllowEdit = false;
            this.SYMCODE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.SYMCODE.Visible = true;
            this.SYMCODE.Width = 60;
            // 
            // FLNAME
            // 
            this.FLNAME.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FLNAME.AppearanceCell.Options.UseFont = true;
            this.FLNAME.AppearanceCell.Options.UseTextOptions = true;
            this.FLNAME.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FLNAME.Caption = "FL";
            this.FLNAME.FieldName = "FLNAME";
            this.FLNAME.Name = "FLNAME";
            this.FLNAME.OptionsColumn.AllowEdit = false;
            this.FLNAME.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.FLNAME.Visible = true;
            this.FLNAME.Width = 60;
            // 
            // LABNAME
            // 
            this.LABNAME.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.LABNAME.AppearanceCell.Options.UseFont = true;
            this.LABNAME.AppearanceCell.Options.UseTextOptions = true;
            this.LABNAME.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LABNAME.Caption = "Lab";
            this.LABNAME.FieldName = "LABNAME";
            this.LABNAME.Name = "LABNAME";
            this.LABNAME.OptionsColumn.AllowEdit = false;
            this.LABNAME.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.LABNAME.Visible = true;
            this.LABNAME.Width = 60;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn23.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.gridColumn23.AppearanceCell.Options.UseFont = true;
            this.gridColumn23.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn23.Caption = "Cts";
            this.gridColumn23.FieldName = "CARAT";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn23.Visible = true;
            this.gridColumn23.Width = 60;
            // 
            // gridColumn27
            // 
            this.gridColumn27.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn27.AppearanceCell.Options.UseFont = true;
            this.gridColumn27.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn27.Caption = "$ Amt";
            this.gridColumn27.FieldName = "AMOUNT";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.OptionsColumn.AllowEdit = false;
            this.gridColumn27.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn27.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn27.Visible = true;
            this.gridColumn27.Width = 102;
            // 
            // bandedGridColumn6
            // 
            this.bandedGridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bandedGridColumn6.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn6.Caption = "Total $";
            this.bandedGridColumn6.FieldName = "TOTALAMOUNT";
            this.bandedGridColumn6.Name = "bandedGridColumn6";
            this.bandedGridColumn6.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.bandedGridColumn6.Visible = true;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "RDate";
            this.gridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "RAPDATE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.Caption = "ECode";
            this.gridColumn15.FieldName = "EMPLOYEECODE";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "Before Brk Process";
            this.gridBand2.Columns.Add(this.BRKPROCS_SHAPE);
            this.gridBand2.Columns.Add(this.BRKPROCS_COLOR);
            this.gridBand2.Columns.Add(this.BRKPROCS_CLARITY);
            this.gridBand2.Columns.Add(this.BRKPROCS_CUT);
            this.gridBand2.Columns.Add(this.BRKPROCS_POL);
            this.gridBand2.Columns.Add(this.BRKPROCS_SYM);
            this.gridBand2.Columns.Add(this.BRKPROCS_FL);
            this.gridBand2.Columns.Add(this.BRKPROCS_LAB);
            this.gridBand2.Columns.Add(this.BRKPROCS_CTS);
            this.gridBand2.Columns.Add(this.BRKPROCS_AMT);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Visible = false;
            this.gridBand2.VisibleIndex = -1;
            this.gridBand2.Width = 750;
            // 
            // BRKPROCS_SHAPE
            // 
            this.BRKPROCS_SHAPE.Caption = "Shp";
            this.BRKPROCS_SHAPE.Name = "BRKPROCS_SHAPE";
            this.BRKPROCS_SHAPE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_SHAPE.Visible = true;
            // 
            // BRKPROCS_COLOR
            // 
            this.BRKPROCS_COLOR.Caption = "Col";
            this.BRKPROCS_COLOR.Name = "BRKPROCS_COLOR";
            this.BRKPROCS_COLOR.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_COLOR.Visible = true;
            // 
            // BRKPROCS_CLARITY
            // 
            this.BRKPROCS_CLARITY.Caption = "Cla";
            this.BRKPROCS_CLARITY.Name = "BRKPROCS_CLARITY";
            this.BRKPROCS_CLARITY.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_CLARITY.Visible = true;
            // 
            // BRKPROCS_CUT
            // 
            this.BRKPROCS_CUT.Caption = "Cut";
            this.BRKPROCS_CUT.Name = "BRKPROCS_CUT";
            this.BRKPROCS_CUT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_CUT.Visible = true;
            // 
            // BRKPROCS_POL
            // 
            this.BRKPROCS_POL.Caption = "Pol";
            this.BRKPROCS_POL.Name = "BRKPROCS_POL";
            this.BRKPROCS_POL.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_POL.Visible = true;
            // 
            // BRKPROCS_SYM
            // 
            this.BRKPROCS_SYM.Caption = "Sym";
            this.BRKPROCS_SYM.Name = "BRKPROCS_SYM";
            this.BRKPROCS_SYM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_SYM.Visible = true;
            // 
            // BRKPROCS_FL
            // 
            this.BRKPROCS_FL.Caption = "FL";
            this.BRKPROCS_FL.Name = "BRKPROCS_FL";
            this.BRKPROCS_FL.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_FL.Visible = true;
            // 
            // BRKPROCS_LAB
            // 
            this.BRKPROCS_LAB.Caption = "Lab";
            this.BRKPROCS_LAB.Name = "BRKPROCS_LAB";
            this.BRKPROCS_LAB.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_LAB.Visible = true;
            // 
            // BRKPROCS_CTS
            // 
            this.BRKPROCS_CTS.Caption = "Cts";
            this.BRKPROCS_CTS.Name = "BRKPROCS_CTS";
            this.BRKPROCS_CTS.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_CTS.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.BRKPROCS_CTS.Visible = true;
            // 
            // BRKPROCS_AMT
            // 
            this.BRKPROCS_AMT.Caption = "$ Amt";
            this.BRKPROCS_AMT.Name = "BRKPROCS_AMT";
            this.BRKPROCS_AMT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.BRKPROCS_AMT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.BRKPROCS_AMT.Visible = true;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = ":";
            this.bandedGridColumn3.FieldName = "REPORTTYPE";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn3.Visible = true;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = ":";
            this.bandedGridColumn2.FieldName = "BRK_ENTRYTYPE";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn2.Visible = true;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.Caption = "Kapan";
            this.bandedGridColumn5.FieldName = "KAPANNAME";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.Caption = "PktNO";
            this.bandedGridColumn4.FieldName = "PACKETNO";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.PictureChecked = global::AxoneMFGRJ.Properties.Resources.Checked;
            this.repositoryItemCheckEdit1.PictureUnchecked = global::AxoneMFGRJ.Properties.Resources.Unchecked;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.White;
            this.PanelHeader.Controls.Add(this.cLabel4);
            this.PanelHeader.Controls.Add(this.lblISPCN);
            this.PanelHeader.Controls.Add(this.cLabel3);
            this.PanelHeader.Controls.Add(this.lblRapDate);
            this.PanelHeader.Controls.Add(this.lblLab);
            this.PanelHeader.Controls.Add(this.cLabel1);
            this.PanelHeader.Controls.Add(this.cLabel12);
            this.PanelHeader.Controls.Add(this.lblBombay);
            this.PanelHeader.Controls.Add(this.cLabel11);
            this.PanelHeader.Controls.Add(this.lblGrading);
            this.PanelHeader.Controls.Add(this.cLabel10);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.aLabel2);
            this.PanelHeader.Controls.Add(this.txtBreakingType);
            this.PanelHeader.Controls.Add(this.txtKapanName);
            this.PanelHeader.Controls.Add(this.txtPacketNo);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1252, 42);
            this.PanelHeader.TabIndex = 0;
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(553, 15);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(32, 13);
            this.cLabel4.TabIndex = 27;
            this.cLabel4.Text = "PCN";
            this.cLabel4.ToolTips = "";
            // 
            // lblISPCN
            // 
            this.lblISPCN.BackColor = System.Drawing.Color.PaleTurquoise;
            this.lblISPCN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblISPCN.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblISPCN.ForeColor = System.Drawing.Color.Black;
            this.lblISPCN.Location = new System.Drawing.Point(527, 12);
            this.lblISPCN.Name = "lblISPCN";
            this.lblISPCN.Size = new System.Drawing.Size(25, 20);
            this.lblISPCN.TabIndex = 26;
            this.lblISPCN.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(204, 15);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(29, 13);
            this.cLabel3.TabIndex = 25;
            this.cLabel3.Text = "Brk";
            this.cLabel3.ToolTips = "";
            // 
            // lblRapDate
            // 
            this.lblRapDate.AutoSize = true;
            this.lblRapDate.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRapDate.ForeColor = System.Drawing.Color.Black;
            this.lblRapDate.Location = new System.Drawing.Point(407, 15);
            this.lblRapDate.Name = "lblRapDate";
            this.lblRapDate.Size = new System.Drawing.Size(61, 13);
            this.lblRapDate.TabIndex = 24;
            this.lblRapDate.Text = "RapDate";
            this.lblRapDate.ToolTips = "";
            // 
            // lblLab
            // 
            this.lblLab.BackColor = System.Drawing.Color.LightYellow;
            this.lblLab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLab.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLab.ForeColor = System.Drawing.Color.Black;
            this.lblLab.Location = new System.Drawing.Point(769, 9);
            this.lblLab.Name = "lblLab";
            this.lblLab.Size = new System.Drawing.Size(25, 20);
            this.lblLab.TabIndex = 17;
            this.lblLab.ToolTips = "";
            this.lblLab.Visible = false;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(340, 15);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(69, 13);
            this.cLabel1.TabIndex = 23;
            this.cLabel1.Text = "RapDate :";
            this.cLabel1.ToolTips = "";
            // 
            // cLabel12
            // 
            this.cLabel12.AutoSize = true;
            this.cLabel12.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel12.ForeColor = System.Drawing.Color.Black;
            this.cLabel12.Location = new System.Drawing.Point(796, 13);
            this.cLabel12.Name = "cLabel12";
            this.cLabel12.Size = new System.Drawing.Size(30, 13);
            this.cLabel12.TabIndex = 20;
            this.cLabel12.Text = "Lab";
            this.cLabel12.ToolTips = "";
            this.cLabel12.Visible = false;
            // 
            // lblBombay
            // 
            this.lblBombay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(240)))));
            this.lblBombay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBombay.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBombay.ForeColor = System.Drawing.Color.Black;
            this.lblBombay.Location = new System.Drawing.Point(679, 9);
            this.lblBombay.Name = "lblBombay";
            this.lblBombay.Size = new System.Drawing.Size(25, 20);
            this.lblBombay.TabIndex = 18;
            this.lblBombay.ToolTips = "";
            this.lblBombay.Visible = false;
            // 
            // cLabel11
            // 
            this.cLabel11.AutoSize = true;
            this.cLabel11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel11.ForeColor = System.Drawing.Color.Black;
            this.cLabel11.Location = new System.Drawing.Point(704, 13);
            this.cLabel11.Name = "cLabel11";
            this.cLabel11.Size = new System.Drawing.Size(59, 13);
            this.cLabel11.TabIndex = 21;
            this.cLabel11.Text = "Bombay";
            this.cLabel11.ToolTips = "";
            this.cLabel11.Visible = false;
            // 
            // lblGrading
            // 
            this.lblGrading.BackColor = System.Drawing.Color.PaleTurquoise;
            this.lblGrading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGrading.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrading.ForeColor = System.Drawing.Color.Black;
            this.lblGrading.Location = new System.Drawing.Point(588, 9);
            this.lblGrading.Name = "lblGrading";
            this.lblGrading.Size = new System.Drawing.Size(25, 20);
            this.lblGrading.TabIndex = 19;
            this.lblGrading.ToolTips = "";
            this.lblGrading.Visible = false;
            // 
            // cLabel10
            // 
            this.cLabel10.AutoSize = true;
            this.cLabel10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel10.ForeColor = System.Drawing.Color.Black;
            this.cLabel10.Location = new System.Drawing.Point(615, 13);
            this.cLabel10.Name = "cLabel10";
            this.cLabel10.Size = new System.Drawing.Size(58, 13);
            this.cLabel10.TabIndex = 22;
            this.cLabel10.Text = "Grading";
            this.cLabel10.ToolTips = "";
            this.cLabel10.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseTextOptions = true;
            this.BtnExit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(828, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(74, 34);
            this.BtnExit.TabIndex = 16;
            this.BtnExit.Text = "Exit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // aLabel2
            // 
            this.aLabel2.AutoSize = true;
            this.aLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aLabel2.ForeColor = System.Drawing.Color.Black;
            this.aLabel2.Location = new System.Drawing.Point(9, 15);
            this.aLabel2.Name = "aLabel2";
            this.aLabel2.Size = new System.Drawing.Size(28, 13);
            this.aLabel2.TabIndex = 8;
            this.aLabel2.Text = "Pkt";
            this.aLabel2.ToolTips = "";
            // 
            // txtBreakingType
            // 
            this.txtBreakingType.ActivationColor = false;
            this.txtBreakingType.AllowTabKeyOnEnter = false;
            this.txtBreakingType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBreakingType.ComplusoryMsg = null;
            this.txtBreakingType.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBreakingType.Format = "";
            this.txtBreakingType.IsComplusory = false;
            this.txtBreakingType.Location = new System.Drawing.Point(234, 10);
            this.txtBreakingType.Name = "txtBreakingType";
            this.txtBreakingType.ReadOnly = true;
            this.txtBreakingType.RequiredChars = "";
            this.txtBreakingType.SelectAllTextOnFocus = true;
            this.txtBreakingType.ShowToolTipOnFocus = false;
            this.txtBreakingType.Size = new System.Drawing.Size(73, 23);
            this.txtBreakingType.TabIndex = 11;
            this.txtBreakingType.TabStop = false;
            this.txtBreakingType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBreakingType.ToolTips = "";
            this.txtBreakingType.WaterMarkText = null;
            // 
            // txtKapanName
            // 
            this.txtKapanName.ActivationColor = false;
            this.txtKapanName.AllowTabKeyOnEnter = false;
            this.txtKapanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKapanName.ComplusoryMsg = null;
            this.txtKapanName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKapanName.Format = "";
            this.txtKapanName.IsComplusory = false;
            this.txtKapanName.Location = new System.Drawing.Point(37, 10);
            this.txtKapanName.Name = "txtKapanName";
            this.txtKapanName.ReadOnly = true;
            this.txtKapanName.RequiredChars = "";
            this.txtKapanName.SelectAllTextOnFocus = true;
            this.txtKapanName.ShowToolTipOnFocus = false;
            this.txtKapanName.Size = new System.Drawing.Size(79, 23);
            this.txtKapanName.TabIndex = 9;
            this.txtKapanName.TabStop = false;
            this.txtKapanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKapanName.ToolTips = "";
            this.txtKapanName.WaterMarkText = null;
            // 
            // txtPacketNo
            // 
            this.txtPacketNo.ActivationColor = false;
            this.txtPacketNo.AllowTabKeyOnEnter = false;
            this.txtPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPacketNo.ComplusoryMsg = null;
            this.txtPacketNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacketNo.Format = "";
            this.txtPacketNo.IsComplusory = false;
            this.txtPacketNo.Location = new System.Drawing.Point(122, 10);
            this.txtPacketNo.Name = "txtPacketNo";
            this.txtPacketNo.ReadOnly = true;
            this.txtPacketNo.RequiredChars = "";
            this.txtPacketNo.SelectAllTextOnFocus = true;
            this.txtPacketNo.ShowToolTipOnFocus = false;
            this.txtPacketNo.Size = new System.Drawing.Size(65, 23);
            this.txtPacketNo.TabIndex = 10;
            this.txtPacketNo.TabStop = false;
            this.txtPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPacketNo.ToolTips = "";
            this.txtPacketNo.WaterMarkText = null;
            // 
            // FrmBreakingDiffrenceDetailReport
            // 
            this.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 662);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.PanelHeader);
            this.KeyPreview = true;
            this.Name = "FrmBreakingDiffrenceDetailReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BREAKING PACKET DETAIL";
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGrid;
        private AxonContLib.cPanel PanelHeader;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private AxonContLib.cLabel aLabel2;
        private AxonContLib.cTextBox txtBreakingType;
        private AxonContLib.cTextBox txtPacketNo;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private AxonContLib.cLabel lblLab;
        private AxonContLib.cLabel cLabel12;
        private AxonContLib.cLabel lblBombay;
        private AxonContLib.cLabel cLabel11;
        private AxonContLib.cLabel lblGrading;
        private AxonContLib.cLabel cLabel10;
        private AxonContLib.cTextBox txtKapanName;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel lblRapDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn23;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn27;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn SHAPECODE;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CLARITYNAME;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn COLORNAME;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CUTCODE;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn POLCODE;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn SYMCODE;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn FLNAME;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn LABNAME;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn15;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_SHAPE;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_COLOR;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_CLARITY;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_CUT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_POL;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_SYM;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_FL;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_LAB;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_CTS;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BRKPROCS_AMT;
        private AxonContLib.cLabel cLabel3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private AxonContLib.cLabel lblISPCN;
        private AxonContLib.cLabel cLabel4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;


    }
}