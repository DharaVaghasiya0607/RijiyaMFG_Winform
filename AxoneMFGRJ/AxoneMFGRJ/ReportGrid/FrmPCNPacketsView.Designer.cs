namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmPCNPacketsView
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.PanelHeader = new AxonContLib.cPanel();
            this.MainGridSummary = new DevExpress.XtraGrid.GridControl();
            this.GrdDetSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.PnlTop = new AxonContLib.cPanel();
            this.txtYear = new AxonContLib.cTextBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.CmbBaseCompare = new AxonContLib.cComboBox(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel11 = new AxonContLib.cLabel(this.components);
            this.cLabel7 = new AxonContLib.cLabel(this.components);
            this.RbtPktNotCreated = new AxonContLib.cRadioButton(this.components);
            this.txtFromPacketNo = new AxonContLib.cTextBox(this.components);
            this.RbtPktCreated = new AxonContLib.cRadioButton(this.components);
            this.txtToPacketNo = new AxonContLib.cTextBox(this.components);
            this.RbtAll = new AxonContLib.cRadioButton(this.components);
            this.txtTag = new AxonContLib.cTextBox(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.CmbPrdType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel14 = new AxonContLib.cLabel(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.cLabel13 = new AxonContLib.cLabel(this.components);
            this.txtEmployee = new AxonContLib.cTextBox(this.components);
            this.BtnKapanLiveStockExcelExport = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.BandGeneral = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.cLabel10 = new AxonContLib.cLabel(this.components);
            this.lblUp = new AxonContLib.cLabel(this.components);
            this.cLabel12 = new AxonContLib.cLabel(this.components);
            this.lblDown = new AxonContLib.cLabel(this.components);
            this.PanelBottom = new AxonContLib.cPanel();
            this.panel2 = new AxonContLib.cPanel();
            this.lblDefaultLayout = new AxonContLib.cLabel(this.components);
            this.lblSaveLayout = new AxonContLib.cLabel(this.components);
            this.lblMessage = new AxonContLib.cLabel(this.components);
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            this.PnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbPrdType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.PanelBottom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelHeader.Controls.Add(this.MainGridSummary);
            this.PanelHeader.Controls.Add(this.PnlTop);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1264, 107);
            this.PanelHeader.TabIndex = 0;
            // 
            // MainGridSummary
            // 
            this.MainGridSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.MainGridSummary.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGridSummary.Location = new System.Drawing.Point(907, 0);
            this.MainGridSummary.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MainGridSummary.MainView = this.GrdDetSummary;
            this.MainGridSummary.Name = "MainGridSummary";
            this.MainGridSummary.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemButtonEdit2});
            this.MainGridSummary.Size = new System.Drawing.Size(355, 105);
            this.MainGridSummary.TabIndex = 182;
            this.MainGridSummary.TabStop = false;
            this.MainGridSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetSummary});
            // 
            // GrdDetSummary
            // 
            this.GrdDetSummary.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetSummary.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetSummary.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetSummary.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetSummary.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetSummary.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetSummary.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetSummary.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetSummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn17});
            this.GrdDetSummary.GridControl = this.MainGridSummary;
            this.GrdDetSummary.Name = "GrdDetSummary";
            this.GrdDetSummary.OptionsBehavior.Editable = false;
            this.GrdDetSummary.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetSummary.OptionsView.ColumnAutoWidth = false;
            this.GrdDetSummary.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn2.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "Type";
            this.gridColumn2.FieldName = "PRDTYPENAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 178;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn3.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.Caption = "Pcs";
            this.gridColumn3.FieldName = "PCS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 67;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn5.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn5.Caption = "Cts";
            this.gridColumn5.FieldName = "CARAT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 82;
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn17.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.gridColumn17.AppearanceCell.Options.UseFont = true;
            this.gridColumn17.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn17.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn17.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn17.AppearanceHeader.Options.UseFont = true;
            this.gridColumn17.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn17.Caption = "Amt";
            this.gridColumn17.FieldName = "AMOUNT";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn17.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 3;
            this.gridColumn17.Width = 78;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject1.Options.UseFont = true;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Print", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            serializableAppearanceObject2.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject2.Options.UseFont = true;
            serializableAppearanceObject2.Options.UseForeColor = true;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // PnlTop
            // 
            this.PnlTop.BackColor = System.Drawing.Color.MistyRose;
            this.PnlTop.Controls.Add(this.txtYear);
            this.PnlTop.Controls.Add(this.cLabel1);
            this.PnlTop.Controls.Add(this.cLabel3);
            this.PnlTop.Controls.Add(this.BtnExit);
            this.PnlTop.Controls.Add(this.cLabel2);
            this.PnlTop.Controls.Add(this.CmbBaseCompare);
            this.PnlTop.Controls.Add(this.cLabel6);
            this.PnlTop.Controls.Add(this.BtnSearch);
            this.PnlTop.Controls.Add(this.cLabel11);
            this.PnlTop.Controls.Add(this.cLabel7);
            this.PnlTop.Controls.Add(this.RbtPktNotCreated);
            this.PnlTop.Controls.Add(this.txtFromPacketNo);
            this.PnlTop.Controls.Add(this.RbtPktCreated);
            this.PnlTop.Controls.Add(this.txtToPacketNo);
            this.PnlTop.Controls.Add(this.RbtAll);
            this.PnlTop.Controls.Add(this.txtTag);
            this.PnlTop.Controls.Add(this.DTPToDate);
            this.PnlTop.Controls.Add(this.CmbPrdType);
            this.PnlTop.Controls.Add(this.DTPFromDate);
            this.PnlTop.Controls.Add(this.CmbKapan);
            this.PnlTop.Controls.Add(this.cLabel14);
            this.PnlTop.Controls.Add(this.cLabel8);
            this.PnlTop.Controls.Add(this.cLabel9);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(907, 105);
            this.PnlTop.TabIndex = 0;
            // 
            // txtYear
            // 
            this.txtYear.ActivationColor = true;
            this.txtYear.AllowTabKeyOnEnter = false;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.ComplusoryMsg = null;
            this.txtYear.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtYear.Format = "######";
            this.txtYear.IsComplusory = false;
            this.txtYear.Location = new System.Drawing.Point(468, 37);
            this.txtYear.MaxLength = 6;
            this.txtYear.Name = "txtYear";
            this.txtYear.RequiredChars = "0123456789.";
            this.txtYear.SelectAllTextOnFocus = true;
            this.txtYear.ShowToolTipOnFocus = false;
            this.txtYear.Size = new System.Drawing.Size(115, 24);
            this.txtYear.TabIndex = 13;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.ToolTips = "";
            this.txtYear.WaterMarkText = null;
            this.txtYear.Validated += new System.EventHandler(this.txtYear_Validated);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(3, 13);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(47, 13);
            this.cLabel1.TabIndex = 0;
            this.cLabel1.Text = "Kapan";
            this.cLabel1.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(310, 13);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(28, 13);
            this.cLabel3.TabIndex = 2;
            this.cLabel3.Text = "Pkt";
            this.cLabel3.ToolTips = "";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Location = new System.Drawing.Point(826, 68);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 32);
            this.BtnExit.TabIndex = 153;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(412, 13);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(23, 13);
            this.cLabel2.TabIndex = 4;
            this.cLabel2.Text = "To";
            this.cLabel2.ToolTips = "";
            // 
            // CmbBaseCompare
            // 
            this.CmbBaseCompare.AllowTabKeyOnEnter = false;
            this.CmbBaseCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBaseCompare.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbBaseCompare.ForeColor = System.Drawing.Color.Black;
            this.CmbBaseCompare.FormattingEnabled = true;
            this.CmbBaseCompare.Items.AddRange(new object[] {
            "FINAL MAKABLE PREDICTION",
            "GRADING",
            "MUMBAI GRADING",
            "LAB GRADING"});
            this.CmbBaseCompare.Location = new System.Drawing.Point(683, 8);
            this.CmbBaseCompare.Name = "CmbBaseCompare";
            this.CmbBaseCompare.Size = new System.Drawing.Size(215, 22);
            this.CmbBaseCompare.TabIndex = 9;
            this.CmbBaseCompare.ToolTips = "";
            this.CmbBaseCompare.SelectedIndexChanged += new System.EventHandler(this.CmbBaseCompare_SelectedIndexChanged);
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(3, 43);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(40, 13);
            this.cLabel6.TabIndex = 10;
            this.cLabel6.Text = "Prdic";
            this.cLabel6.ToolTips = "";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Location = new System.Drawing.Point(745, 68);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 32);
            this.BtnSearch.TabIndex = 20;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // cLabel11
            // 
            this.cLabel11.AutoSize = true;
            this.cLabel11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel11.ForeColor = System.Drawing.Color.Black;
            this.cLabel11.Location = new System.Drawing.Point(585, 13);
            this.cLabel11.Name = "cLabel11";
            this.cLabel11.Size = new System.Drawing.Size(98, 13);
            this.cLabel11.TabIndex = 8;
            this.cLabel11.Text = "Compare With";
            this.cLabel11.ToolTips = "";
            // 
            // cLabel7
            // 
            this.cLabel7.AutoSize = true;
            this.cLabel7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel7.ForeColor = System.Drawing.Color.Black;
            this.cLabel7.Location = new System.Drawing.Point(506, 13);
            this.cLabel7.Name = "cLabel7";
            this.cLabel7.Size = new System.Drawing.Size(31, 13);
            this.cLabel7.TabIndex = 6;
            this.cLabel7.Text = "Tag";
            this.cLabel7.ToolTips = "";
            // 
            // RbtPktNotCreated
            // 
            this.RbtPktNotCreated.AutoSize = true;
            this.RbtPktNotCreated.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtPktNotCreated.ForeColor = System.Drawing.Color.Black;
            this.RbtPktNotCreated.Location = new System.Drawing.Point(161, 72);
            this.RbtPktNotCreated.Name = "RbtPktNotCreated";
            this.RbtPktNotCreated.Size = new System.Drawing.Size(102, 17);
            this.RbtPktNotCreated.TabIndex = 19;
            this.RbtPktNotCreated.Text = "Not Created";
            this.RbtPktNotCreated.ToolTips = "";
            this.RbtPktNotCreated.UseVisualStyleBackColor = true;
            this.RbtPktNotCreated.CheckedChanged += new System.EventHandler(this.RbtAll_CheckedChanged);
            // 
            // txtFromPacketNo
            // 
            this.txtFromPacketNo.ActivationColor = true;
            this.txtFromPacketNo.AllowTabKeyOnEnter = false;
            this.txtFromPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromPacketNo.ComplusoryMsg = null;
            this.txtFromPacketNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromPacketNo.Format = "######";
            this.txtFromPacketNo.IsComplusory = false;
            this.txtFromPacketNo.Location = new System.Drawing.Point(343, 8);
            this.txtFromPacketNo.Name = "txtFromPacketNo";
            this.txtFromPacketNo.RequiredChars = "0123456789.";
            this.txtFromPacketNo.SelectAllTextOnFocus = true;
            this.txtFromPacketNo.ShowToolTipOnFocus = false;
            this.txtFromPacketNo.Size = new System.Drawing.Size(63, 22);
            this.txtFromPacketNo.TabIndex = 3;
            this.txtFromPacketNo.Text = "0";
            this.txtFromPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFromPacketNo.ToolTips = "";
            this.txtFromPacketNo.WaterMarkText = null;
            // 
            // RbtPktCreated
            // 
            this.RbtPktCreated.AutoSize = true;
            this.RbtPktCreated.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtPktCreated.ForeColor = System.Drawing.Color.Black;
            this.RbtPktCreated.Location = new System.Drawing.Point(56, 72);
            this.RbtPktCreated.Name = "RbtPktCreated";
            this.RbtPktCreated.Size = new System.Drawing.Size(101, 17);
            this.RbtPktCreated.TabIndex = 18;
            this.RbtPktCreated.Text = "Pkt Created";
            this.RbtPktCreated.ToolTips = "";
            this.RbtPktCreated.UseVisualStyleBackColor = true;
            this.RbtPktCreated.CheckedChanged += new System.EventHandler(this.RbtAll_CheckedChanged);
            // 
            // txtToPacketNo
            // 
            this.txtToPacketNo.ActivationColor = true;
            this.txtToPacketNo.AllowTabKeyOnEnter = false;
            this.txtToPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToPacketNo.ComplusoryMsg = null;
            this.txtToPacketNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPacketNo.Format = "######";
            this.txtToPacketNo.IsComplusory = false;
            this.txtToPacketNo.Location = new System.Drawing.Point(437, 8);
            this.txtToPacketNo.Name = "txtToPacketNo";
            this.txtToPacketNo.RequiredChars = "0123456789.";
            this.txtToPacketNo.SelectAllTextOnFocus = true;
            this.txtToPacketNo.ShowToolTipOnFocus = false;
            this.txtToPacketNo.Size = new System.Drawing.Size(63, 22);
            this.txtToPacketNo.TabIndex = 5;
            this.txtToPacketNo.Text = "0";
            this.txtToPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtToPacketNo.ToolTips = "";
            this.txtToPacketNo.WaterMarkText = null;
            // 
            // RbtAll
            // 
            this.RbtAll.AutoSize = true;
            this.RbtAll.Checked = true;
            this.RbtAll.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtAll.ForeColor = System.Drawing.Color.Black;
            this.RbtAll.Location = new System.Drawing.Point(8, 72);
            this.RbtAll.Name = "RbtAll";
            this.RbtAll.Size = new System.Drawing.Size(42, 17);
            this.RbtAll.TabIndex = 17;
            this.RbtAll.TabStop = true;
            this.RbtAll.Text = "All";
            this.RbtAll.ToolTips = "";
            this.RbtAll.UseVisualStyleBackColor = true;
            this.RbtAll.CheckedChanged += new System.EventHandler(this.RbtAll_CheckedChanged);
            // 
            // txtTag
            // 
            this.txtTag.ActivationColor = true;
            this.txtTag.AllowTabKeyOnEnter = false;
            this.txtTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTag.ComplusoryMsg = null;
            this.txtTag.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTag.Format = "";
            this.txtTag.IsComplusory = false;
            this.txtTag.Location = new System.Drawing.Point(538, 8);
            this.txtTag.Name = "txtTag";
            this.txtTag.RequiredChars = "";
            this.txtTag.SelectAllTextOnFocus = true;
            this.txtTag.ShowToolTipOnFocus = false;
            this.txtTag.Size = new System.Drawing.Size(45, 22);
            this.txtTag.TabIndex = 7;
            this.txtTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTag.ToolTips = "";
            this.txtTag.WaterMarkText = null;
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(754, 37);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.ShowCheckBox = true;
            this.DTPToDate.Size = new System.Drawing.Size(122, 24);
            this.DTPToDate.TabIndex = 16;
            this.DTPToDate.ToolTips = "";
            // 
            // CmbPrdType
            // 
            this.CmbPrdType.Location = new System.Drawing.Point(52, 38);
            this.CmbPrdType.Name = "CmbPrdType";
            this.CmbPrdType.Properties.AllowMultiSelect = true;
            this.CmbPrdType.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbPrdType.Properties.Appearance.Options.UseFont = true;
            this.CmbPrdType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbPrdType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbPrdType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbPrdType.Properties.DropDownRows = 20;
            this.CmbPrdType.Properties.IncrementalSearch = true;
            this.CmbPrdType.Size = new System.Drawing.Size(332, 22);
            this.CmbPrdType.TabIndex = 11;
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(628, 37);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.ShowCheckBox = true;
            this.DTPFromDate.Size = new System.Drawing.Size(122, 24);
            this.DTPFromDate.TabIndex = 15;
            this.DTPFromDate.ToolTips = "";
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(52, 8);
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
            this.CmbKapan.Size = new System.Drawing.Size(255, 22);
            this.CmbKapan.TabIndex = 1;
            // 
            // cLabel14
            // 
            this.cLabel14.AutoSize = true;
            this.cLabel14.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel14.ForeColor = System.Drawing.Color.Black;
            this.cLabel14.Location = new System.Drawing.Point(514, 64);
            this.cLabel14.Name = "cLabel14";
            this.cLabel14.Size = new System.Drawing.Size(69, 12);
            this.cLabel14.TabIndex = 12;
            this.cLabel14.Text = "i.e. 201904";
            this.cLabel14.ToolTips = "";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(389, 43);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(80, 13);
            this.cLabel8.TabIndex = 12;
            this.cLabel8.Text = "Year Month";
            this.cLabel8.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(587, 43);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(37, 13);
            this.cLabel9.TabIndex = 14;
            this.cLabel9.Text = "Date";
            this.cLabel9.ToolTips = "";
            // 
            // cLabel13
            // 
            this.cLabel13.AutoSize = true;
            this.cLabel13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel13.ForeColor = System.Drawing.Color.Black;
            this.cLabel13.Location = new System.Drawing.Point(167, 236);
            this.cLabel13.Name = "cLabel13";
            this.cLabel13.Size = new System.Drawing.Size(35, 13);
            this.cLabel13.TabIndex = 16;
            this.cLabel13.Text = "Emp";
            this.cLabel13.ToolTips = "";
            this.cLabel13.Visible = false;
            // 
            // txtEmployee
            // 
            this.txtEmployee.ActivationColor = true;
            this.txtEmployee.AllowTabKeyOnEnter = false;
            this.txtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployee.ComplusoryMsg = null;
            this.txtEmployee.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtEmployee.Format = "";
            this.txtEmployee.IsComplusory = false;
            this.txtEmployee.Location = new System.Drawing.Point(216, 231);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.RequiredChars = "";
            this.txtEmployee.SelectAllTextOnFocus = true;
            this.txtEmployee.ShowToolTipOnFocus = false;
            this.txtEmployee.Size = new System.Drawing.Size(332, 22);
            this.txtEmployee.TabIndex = 17;
            this.txtEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmployee.ToolTips = "";
            this.txtEmployee.Visible = false;
            this.txtEmployee.WaterMarkText = null;
            this.txtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployee_KeyPress);
            // 
            // BtnKapanLiveStockExcelExport
            // 
            this.BtnKapanLiveStockExcelExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnKapanLiveStockExcelExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnKapanLiveStockExcelExport.Appearance.Options.UseFont = true;
            this.BtnKapanLiveStockExcelExport.Appearance.Options.UseForeColor = true;
            this.BtnKapanLiveStockExcelExport.Location = new System.Drawing.Point(214, 5);
            this.BtnKapanLiveStockExcelExport.Name = "BtnKapanLiveStockExcelExport";
            this.BtnKapanLiveStockExcelExport.Size = new System.Drawing.Size(111, 32);
            this.BtnKapanLiveStockExcelExport.TabIndex = 153;
            this.BtnKapanLiveStockExcelExport.TabStop = false;
            this.BtnKapanLiveStockExcelExport.Text = "Excel Export";
            this.BtnKapanLiveStockExcelExport.Click += new System.EventHandler(this.BtnKapanLiveStockExcelExport_Click);
            // 
            // cLabel4
            // 
            this.cLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(9, 12);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(20, 20);
            this.cLabel4.TabIndex = 6;
            this.cLabel4.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(31, 16);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(123, 13);
            this.cLabel5.TabIndex = 4;
            this.cLabel5.Text = "Un-Splited Packet";
            this.cLabel5.ToolTips = "";
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject3.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject3.Options.UseFont = true;
            serializableAppearanceObject3.Options.UseForeColor = true;
            serializableAppearanceObject3.Options.UseTextOptions = true;
            serializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 107);
            this.MainGrid.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1264, 323);
            this.MainGrid.TabIndex = 19;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            this.MainGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.MainGrid_Paint);
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.BandPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.BandPanel.Options.UseFont = true;
            this.GrdDet.Appearance.BandPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.BandPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.BandPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.BandPanelRowHeight = 25;
            this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.BandGeneral});
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDet.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.GrdDet_CustomDrawCell);
            this.GrdDet.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GrdDet_RowCellStyle);
            this.GrdDet.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.GrdDet_CustomSummaryCalculate);
            this.GrdDet.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDet_CustomColumnDisplayText);
            // 
            // BandGeneral
            // 
            this.BandGeneral.Caption = "General";
            this.BandGeneral.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.BandGeneral.Name = "BandGeneral";
            this.BandGeneral.VisibleIndex = 0;
            this.BandGeneral.Width = 71;
            // 
            // cLabel10
            // 
            this.cLabel10.AutoSize = true;
            this.cLabel10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel10.ForeColor = System.Drawing.Color.Black;
            this.cLabel10.Location = new System.Drawing.Point(186, 16);
            this.cLabel10.Name = "cLabel10";
            this.cLabel10.Size = new System.Drawing.Size(24, 13);
            this.cLabel10.TabIndex = 4;
            this.cLabel10.Text = "Up";
            this.cLabel10.ToolTips = "";
            // 
            // lblUp
            // 
            this.lblUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(255)))), ((int)(((byte)(210)))));
            this.lblUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUp.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUp.ForeColor = System.Drawing.Color.Black;
            this.lblUp.Location = new System.Drawing.Point(160, 12);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(20, 20);
            this.lblUp.TabIndex = 6;
            this.lblUp.ToolTips = "";
            // 
            // cLabel12
            // 
            this.cLabel12.AutoSize = true;
            this.cLabel12.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel12.ForeColor = System.Drawing.Color.Black;
            this.cLabel12.Location = new System.Drawing.Point(239, 16);
            this.cLabel12.Name = "cLabel12";
            this.cLabel12.Size = new System.Drawing.Size(42, 13);
            this.cLabel12.TabIndex = 4;
            this.cLabel12.Text = "Down";
            this.cLabel12.ToolTips = "";
            // 
            // lblDown
            // 
            this.lblDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.lblDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDown.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDown.ForeColor = System.Drawing.Color.Black;
            this.lblDown.Location = new System.Drawing.Point(216, 12);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(20, 20);
            this.lblDown.TabIndex = 6;
            this.lblDown.ToolTips = "";
            // 
            // PanelBottom
            // 
            this.PanelBottom.Controls.Add(this.panel2);
            this.PanelBottom.Controls.Add(this.lblMessage);
            this.PanelBottom.Controls.Add(this.cLabel12);
            this.PanelBottom.Controls.Add(this.cLabel5);
            this.PanelBottom.Controls.Add(this.cLabel10);
            this.PanelBottom.Controls.Add(this.cLabel4);
            this.PanelBottom.Controls.Add(this.lblUp);
            this.PanelBottom.Controls.Add(this.lblDown);
            this.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBottom.Location = new System.Drawing.Point(0, 430);
            this.PanelBottom.Name = "PanelBottom";
            this.PanelBottom.Size = new System.Drawing.Size(1264, 43);
            this.PanelBottom.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblDefaultLayout);
            this.panel2.Controls.Add(this.BtnKapanLiveStockExcelExport);
            this.panel2.Controls.Add(this.lblSaveLayout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(931, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(333, 43);
            this.panel2.TabIndex = 154;
            // 
            // lblDefaultLayout
            // 
            this.lblDefaultLayout.AutoSize = true;
            this.lblDefaultLayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDefaultLayout.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultLayout.ForeColor = System.Drawing.Color.Navy;
            this.lblDefaultLayout.Location = new System.Drawing.Point(102, 15);
            this.lblDefaultLayout.Name = "lblDefaultLayout";
            this.lblDefaultLayout.Size = new System.Drawing.Size(97, 13);
            this.lblDefaultLayout.TabIndex = 155;
            this.lblDefaultLayout.Text = "Delete Layout";
            this.lblDefaultLayout.ToolTips = "";
            this.lblDefaultLayout.Click += new System.EventHandler(this.lblDefaultLayout_Click);
            // 
            // lblSaveLayout
            // 
            this.lblSaveLayout.AutoSize = true;
            this.lblSaveLayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSaveLayout.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveLayout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSaveLayout.Location = new System.Drawing.Point(9, 15);
            this.lblSaveLayout.Name = "lblSaveLayout";
            this.lblSaveLayout.Size = new System.Drawing.Size(87, 13);
            this.lblSaveLayout.TabIndex = 156;
            this.lblSaveLayout.Text = "Save Layout";
            this.lblSaveLayout.ToolTips = "";
            this.lblSaveLayout.Click += new System.EventHandler(this.lblSaveLayout_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMessage.Location = new System.Drawing.Point(313, 16);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(63, 13);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message";
            this.lblMessage.ToolTips = "";
            // 
            // FrmPCNPacketsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 473);
            this.Controls.Add(this.cLabel13);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.PanelBottom);
            this.Controls.Add(this.PanelHeader);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmPCNPacketsView";
            this.Text = "PCN PACKETS VIEW";
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            this.PnlTop.ResumeLayout(false);
            this.PnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbPrdType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.PanelBottom.ResumeLayout(false);
            this.PanelBottom.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxonContLib.cPanel PanelHeader;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnKapanLiveStockExcelExport;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private AxonContLib.cTextBox txtToPacketNo;
        private AxonContLib.cTextBox txtFromPacketNo;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cLabel cLabel3;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private AxonContLib.cTextBox txtTag;
        private AxonContLib.cLabel cLabel7;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BandGeneral;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbPrdType;
        private AxonContLib.cLabel cLabel6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cRadioButton RbtPktCreated;
        private AxonContLib.cRadioButton RbtAll;
        private AxonContLib.cRadioButton RbtPktNotCreated;
        private AxonContLib.cLabel lblUp;
        private AxonContLib.cLabel cLabel10;
        private AxonContLib.cLabel lblDown;
        private AxonContLib.cLabel cLabel12;
        private AxonContLib.cLabel cLabel11;
        private AxonContLib.cComboBox CmbBaseCompare;
        private AxonContLib.cPanel PanelBottom;
        private AxonContLib.cLabel lblMessage;
        private DevExpress.XtraGrid.GridControl MainGridSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetSummary;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private AxonContLib.cPanel PnlTop;
        private AxonContLib.cLabel cLabel13;
        private AxonContLib.cTextBox txtEmployee;
        private AxonContLib.cPanel panel2;
        private AxonContLib.cTextBox txtYear;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel cLabel14;
        private AxonContLib.cLabel lblDefaultLayout;
        private AxonContLib.cLabel lblSaveLayout;


    }
}