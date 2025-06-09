namespace AxoneMFGRJ.View
{
    partial class FrmMyAgingReport
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
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3 = new AxonContLib.cPanel();
            this.panel1 = new AxonContLib.cPanel();
            this.MainGridPacketDetail = new DevExpress.XtraGrid.GridControl();
            this.GrdDetPacketDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel6 = new AxonContLib.cPanel();
            this.lblTest = new AxonContLib.cLabel(this.components);
            this.txtPassForUpdateExtraMints = new AxonContLib.cTextBox(this.components);
            this.lblTotalAgingOverDuePkts = new AxonContLib.cLabel(this.components);
            this.cLabel15 = new AxonContLib.cLabel(this.components);
            this.lblTotalAgingRunningPkts = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.lblTotalAgingPackets = new AxonContLib.cLabel(this.components);
            this.panel10 = new AxonContLib.cPanel();
            this.CmbDepartment = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.ChkUpdateExtramin = new AxonContLib.cCheckBox(this.components);
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBestFit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.txtEmployee = new AxonContLib.cTextBox(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.txtMinutes = new AxonContLib.cTextBox(this.components);
            this.lblmin = new AxonContLib.cLabel(this.components);
            this.lblApplyAll = new AxonContLib.cLabel(this.components);
            this.cLabel10 = new AxonContLib.cLabel(this.components);
            this.RdbOverDue = new AxonContLib.cRadioButton(this.components);
            this.RdbRunning = new AxonContLib.cRadioButton(this.components);
            this.RdbAll = new AxonContLib.cRadioButton(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.DTPSearchToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPSearchFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridPacketDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetPacketDetail)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1210, 704);
            this.panel3.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MainGridPacketDetail);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1210, 635);
            this.panel1.TabIndex = 179;
            // 
            // MainGridPacketDetail
            // 
            this.MainGridPacketDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridPacketDetail.Location = new System.Drawing.Point(0, 0);
            this.MainGridPacketDetail.MainView = this.GrdDetPacketDetail;
            this.MainGridPacketDetail.Name = "MainGridPacketDetail";
            this.MainGridPacketDetail.Size = new System.Drawing.Size(1210, 550);
            this.MainGridPacketDetail.TabIndex = 178;
            this.MainGridPacketDetail.ToolTipController = this.toolTipController1;
            this.MainGridPacketDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetPacketDetail});
            // 
            // GrdDetPacketDetail
            // 
            this.GrdDetPacketDetail.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetPacketDetail.Appearance.FixedLine.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetPacketDetail.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetPacketDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetPacketDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetPacketDetail.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.GrdDetPacketDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDetPacketDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetPacketDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetPacketDetail.Appearance.HorzLine.BackColor = System.Drawing.Color.Black;
            this.GrdDetPacketDetail.Appearance.HorzLine.BackColor2 = System.Drawing.Color.Black;
            this.GrdDetPacketDetail.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetPacketDetail.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetPacketDetail.Appearance.Row.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetPacketDetail.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.VertLine.BackColor = System.Drawing.Color.Black;
            this.GrdDetPacketDetail.Appearance.VertLine.BackColor2 = System.Drawing.Color.Black;
            this.GrdDetPacketDetail.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetPacketDetail.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetPacketDetail.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPacketDetail.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDetPacketDetail.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.Lines.BackColor = System.Drawing.Color.DarkGray;
            this.GrdDetPacketDetail.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDetPacketDetail.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetPacketDetail.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPacketDetail.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDetPacketDetail.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPacketDetail.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDetPacketDetail.ColumnPanelRowHeight = 25;
            this.GrdDetPacketDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn11,
            this.gridColumn16,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn7,
            this.gridColumn10,
            this.gridColumn17,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22});
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
            this.GrdDetPacketDetail.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.GrdDetPacketDetail.GridControl = this.MainGridPacketDetail;
            this.GrdDetPacketDetail.Name = "GrdDetPacketDetail";
            this.GrdDetPacketDetail.OptionsCustomization.AllowQuickHideColumns = false;
            this.GrdDetPacketDetail.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetPacketDetail.OptionsFilter.AllowMRUFilterList = false;
            this.GrdDetPacketDetail.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
            this.GrdDetPacketDetail.OptionsMenu.EnableColumnMenu = false;
            this.GrdDetPacketDetail.OptionsMenu.EnableFooterMenu = false;
            this.GrdDetPacketDetail.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDetPacketDetail.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDetPacketDetail.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDetPacketDetail.OptionsSelection.EnableAppearanceHideSelection = false;
            this.GrdDetPacketDetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDetPacketDetail.OptionsView.ColumnAutoWidth = false;
            this.GrdDetPacketDetail.OptionsView.ShowAutoFilterRow = true;
            this.GrdDetPacketDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdDetPacketDetail.OptionsView.ShowFooter = true;
            this.GrdDetPacketDetail.OptionsView.ShowGroupPanel = false;
            this.GrdDetPacketDetail.RowHeight = 23;
            this.GrdDetPacketDetail.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.GrdDetPacketDetail.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GrdDetPacketDetail_RowStyle);
            this.GrdDetPacketDetail.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDetPacketDetail_CellValueChanged);
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "TrnAging_ID";
            this.gridColumn18.FieldName = "TRNAGING_ID";
            this.gridColumn18.Name = "gridColumn18";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = "Kapan";
            this.gridColumn1.FieldName = "KAPANNAME";
            this.gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 93;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.Caption = "Pkt";
            this.gridColumn4.FieldName = "PACKETTAG";
            this.gridColumn4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 71;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.Caption = "LOTCARAT";
            this.gridColumn5.FieldName = "LOTCARAT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.Caption = "Cts";
            this.gridColumn6.FieldName = "ISSUECARAT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 74;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.Caption = "Dept";
            this.gridColumn8.FieldName = "DEPARTMENTNAME";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 120;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.Caption = "Process";
            this.gridColumn9.FieldName = "PROCESSNAME";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            this.gridColumn9.Width = 119;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn11.AppearanceCell.Options.UseFont = true;
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.Caption = "Emp";
            this.gridColumn11.FieldName = "EMPLOYEECODE";
            this.gridColumn11.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            this.gridColumn11.Width = 80;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn16.AppearanceCell.Options.UseFont = true;
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn16.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn16.AppearanceHeader.Options.UseFont = true;
            this.gridColumn16.Caption = "Emp Name";
            this.gridColumn16.FieldName = "EMPLOYEENAME";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn12.AppearanceCell.Options.UseFont = true;
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn12.AppearanceHeader.Options.UseFont = true;
            this.gridColumn12.Caption = "Issue Date";
            this.gridColumn12.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn12.FieldName = "ISSUEDATETIME";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 180;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn13.AppearanceCell.Options.UseFont = true;
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn13.AppearanceHeader.Options.UseFont = true;
            this.gridColumn13.Caption = "Working Minute";
            this.gridColumn13.FieldName = "TOTALWORKINGMINUTE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn13.Width = 105;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn14.AppearanceCell.Options.UseFont = true;
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn14.AppearanceHeader.Options.UseFont = true;
            this.gridColumn14.Caption = "Aging Mints.";
            this.gridColumn14.FieldName = "AGINGMINUTES";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 7;
            this.gridColumn14.Width = 115;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn15.AppearanceHeader.Options.UseFont = true;
            this.gridColumn15.Caption = "AgingType";
            this.gridColumn15.FieldName = "AGINGTYPE";
            this.gridColumn15.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn15.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 9;
            this.gridColumn15.Width = 130;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "Running / OverDue";
            this.gridColumn2.DisplayFormat.FormatString = "HH:mm:ss";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn2.FieldName = "RUNNINGOVERDUEMINUTES";
            this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 11;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.Caption = "Status";
            this.gridColumn3.FieldName = "PACKETSAGINGSTATUS";
            this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 10;
            this.gridColumn3.Width = 85;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.Caption = "Approx RetnTime";
            this.gridColumn7.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn7.FieldName = "APPROXRETURNTIME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 8;
            this.gridColumn7.Width = 180;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "PktsAgingMinuts";
            this.gridColumn10.FieldName = "PACKETSAGINGMINUTES";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn17.AppearanceCell.Options.UseFont = true;
            this.gridColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn17.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn17.AppearanceHeader.Options.UseFont = true;
            this.gridColumn17.Caption = "Extra Mints.";
            this.gridColumn17.FieldName = "EXTRAPLUSMINUTES";
            this.gridColumn17.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 12;
            this.gridColumn17.Width = 85;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn19.AppearanceCell.Options.UseFont = true;
            this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn19.AppearanceHeader.Options.UseFont = true;
            this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.Caption = "Total Extra Min";
            this.gridColumn19.FieldName = "TOTALEXTRAMIN";
            this.gridColumn19.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 13;
            this.gridColumn19.Width = 107;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Temp Min";
            this.gridColumn20.FieldName = "TEMPMIN";
            this.gridColumn20.Name = "gridColumn20";
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "WORKINGENDTIME";
            this.gridColumn21.FieldName = "ISWORKINGENDTIME";
            this.gridColumn21.Name = "gridColumn21";
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "REMAININGPACKETAGINGMIN";
            this.gridColumn22.FieldName = "REMAININGPACKETAGINGMIN";
            this.gridColumn22.Name = "gridColumn22";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblTest);
            this.panel6.Controls.Add(this.txtPassForUpdateExtraMints);
            this.panel6.Controls.Add(this.lblTotalAgingOverDuePkts);
            this.panel6.Controls.Add(this.cLabel15);
            this.panel6.Controls.Add(this.lblTotalAgingRunningPkts);
            this.panel6.Controls.Add(this.cLabel1);
            this.panel6.Controls.Add(this.cLabel4);
            this.panel6.Controls.Add(this.lblTotalAgingPackets);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 550);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1210, 85);
            this.panel6.TabIndex = 14;
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.lblTest.ForeColor = System.Drawing.Color.Black;
            this.lblTest.Location = new System.Drawing.Point(752, 50);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(80, 17);
            this.lblTest.TabIndex = 43;
            this.lblTest.Text = "00:00:00";
            this.lblTest.ToolTips = "";
            this.lblTest.Visible = false;
            // 
            // txtPassForUpdateExtraMints
            // 
            this.txtPassForUpdateExtraMints.ActivationColor = false;
            this.txtPassForUpdateExtraMints.AllowTabKeyOnEnter = false;
            this.txtPassForUpdateExtraMints.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPassForUpdateExtraMints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassForUpdateExtraMints.ComplusoryMsg = null;
            this.txtPassForUpdateExtraMints.Format = "";
            this.txtPassForUpdateExtraMints.IsComplusory = false;
            this.txtPassForUpdateExtraMints.Location = new System.Drawing.Point(749, 6);
            this.txtPassForUpdateExtraMints.Name = "txtPassForUpdateExtraMints";
            this.txtPassForUpdateExtraMints.PasswordChar = '*';
            this.txtPassForUpdateExtraMints.RequiredChars = "";
            this.txtPassForUpdateExtraMints.SelectAllTextOnFocus = true;
            this.txtPassForUpdateExtraMints.ShowToolTipOnFocus = false;
            this.txtPassForUpdateExtraMints.Size = new System.Drawing.Size(100, 14);
            this.txtPassForUpdateExtraMints.TabIndex = 157;
            this.txtPassForUpdateExtraMints.TabStop = false;
            this.txtPassForUpdateExtraMints.Tag = "AXONE";
            this.txtPassForUpdateExtraMints.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassForUpdateExtraMints.ToolTips = "";
            this.txtPassForUpdateExtraMints.WaterMarkText = null;
            this.txtPassForUpdateExtraMints.TextChanged += new System.EventHandler(this.txtPassForUpdateExtraMints_TextChanged);
            this.txtPassForUpdateExtraMints.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassForUpdateExtraMints_Validating);
            // 
            // lblTotalAgingOverDuePkts
            // 
            this.lblTotalAgingOverDuePkts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTotalAgingOverDuePkts.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalAgingOverDuePkts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(114)))), ((int)(((byte)(14)))));
            this.lblTotalAgingOverDuePkts.Location = new System.Drawing.Point(502, 32);
            this.lblTotalAgingOverDuePkts.Name = "lblTotalAgingOverDuePkts";
            this.lblTotalAgingOverDuePkts.Size = new System.Drawing.Size(243, 50);
            this.lblTotalAgingOverDuePkts.TabIndex = 6;
            this.lblTotalAgingOverDuePkts.Text = "OverDue : 0";
            this.lblTotalAgingOverDuePkts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalAgingOverDuePkts.ToolTips = "";
            // 
            // cLabel15
            // 
            this.cLabel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cLabel15.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel15.ForeColor = System.Drawing.Color.White;
            this.cLabel15.Location = new System.Drawing.Point(503, 3);
            this.cLabel15.Name = "cLabel15";
            this.cLabel15.Size = new System.Drawing.Size(243, 26);
            this.cLabel15.TabIndex = 7;
            this.cLabel15.Text = "Total OverDue Packets";
            this.cLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel15.ToolTips = "";
            // 
            // lblTotalAgingRunningPkts
            // 
            this.lblTotalAgingRunningPkts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblTotalAgingRunningPkts.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalAgingRunningPkts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(114)))), ((int)(((byte)(14)))));
            this.lblTotalAgingRunningPkts.Location = new System.Drawing.Point(253, 32);
            this.lblTotalAgingRunningPkts.Name = "lblTotalAgingRunningPkts";
            this.lblTotalAgingRunningPkts.Size = new System.Drawing.Size(243, 50);
            this.lblTotalAgingRunningPkts.TabIndex = 6;
            this.lblTotalAgingRunningPkts.Text = "Running : 0";
            this.lblTotalAgingRunningPkts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalAgingRunningPkts.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.White;
            this.cLabel1.Location = new System.Drawing.Point(254, 3);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(243, 26);
            this.cLabel1.TabIndex = 7;
            this.cLabel1.Text = "Total Running Packets";
            this.cLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel1.ToolTips = "";
            // 
            // cLabel4
            // 
            this.cLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.White;
            this.cLabel4.Location = new System.Drawing.Point(5, 3);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(243, 26);
            this.cLabel4.TabIndex = 7;
            this.cLabel4.Text = "All Ageing Packets";
            this.cLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel4.ToolTips = "";
            // 
            // lblTotalAgingPackets
            // 
            this.lblTotalAgingPackets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTotalAgingPackets.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalAgingPackets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(114)))), ((int)(((byte)(14)))));
            this.lblTotalAgingPackets.Location = new System.Drawing.Point(4, 32);
            this.lblTotalAgingPackets.Name = "lblTotalAgingPackets";
            this.lblTotalAgingPackets.Size = new System.Drawing.Size(243, 50);
            this.lblTotalAgingPackets.TabIndex = 6;
            this.lblTotalAgingPackets.Text = "Total : 0";
            this.lblTotalAgingPackets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalAgingPackets.ToolTips = "";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Silver;
            this.panel10.Controls.Add(this.CmbDepartment);
            this.panel10.Controls.Add(this.cLabel6);
            this.panel10.Controls.Add(this.ChkUpdateExtramin);
            this.panel10.Controls.Add(this.BtnPrint);
            this.panel10.Controls.Add(this.BtnBestFit);
            this.panel10.Controls.Add(this.BtnExport);
            this.panel10.Controls.Add(this.txtEmployee);
            this.panel10.Controls.Add(this.cLabel3);
            this.panel10.Controls.Add(this.CmbKapan);
            this.panel10.Controls.Add(this.cLabel5);
            this.panel10.Controls.Add(this.txtMinutes);
            this.panel10.Controls.Add(this.lblmin);
            this.panel10.Controls.Add(this.lblApplyAll);
            this.panel10.Controls.Add(this.cLabel10);
            this.panel10.Controls.Add(this.RdbOverDue);
            this.panel10.Controls.Add(this.RdbRunning);
            this.panel10.Controls.Add(this.RdbAll);
            this.panel10.Controls.Add(this.cLabel2);
            this.panel10.Controls.Add(this.DTPSearchToDate);
            this.panel10.Controls.Add(this.DTPSearchFromDate);
            this.panel10.Controls.Add(this.BtnSearch);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1210, 69);
            this.panel10.TabIndex = 6;
            // 
            // CmbDepartment
            // 
            this.CmbDepartment.Location = new System.Drawing.Point(611, 40);
            this.CmbDepartment.Name = "CmbDepartment";
            this.CmbDepartment.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbDepartment.Properties.Appearance.Options.UseFont = true;
            this.CmbDepartment.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbDepartment.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbDepartment.Properties.DropDownRows = 20;
            this.CmbDepartment.Properties.IncrementalSearch = true;
            this.CmbDepartment.Size = new System.Drawing.Size(212, 22);
            this.CmbDepartment.TabIndex = 168;
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(560, 44);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(42, 16);
            this.cLabel6.TabIndex = 167;
            this.cLabel6.Text = "Dept";
            this.cLabel6.ToolTips = "";
            // 
            // ChkUpdateExtramin
            // 
            this.ChkUpdateExtramin.AutoSize = true;
            this.ChkUpdateExtramin.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkUpdateExtramin.Location = new System.Drawing.Point(830, 44);
            this.ChkUpdateExtramin.Name = "ChkUpdateExtramin";
            this.ChkUpdateExtramin.Size = new System.Drawing.Size(137, 17);
            this.ChkUpdateExtramin.TabIndex = 166;
            this.ChkUpdateExtramin.Text = "Update Extra Min";
            this.ChkUpdateExtramin.ToolTips = "";
            this.ChkUpdateExtramin.UseVisualStyleBackColor = true;
            this.ChkUpdateExtramin.CheckedChanged += new System.EventHandler(this.ChkUpdateExtramin_CheckedChanged);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.Appearance.ForeColor = System.Drawing.Color.Green;
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.Appearance.Options.UseForeColor = true;
            this.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrint.Location = new System.Drawing.Point(1008, 3);
            this.BtnPrint.LookAndFeel.SkinName = "McSkin";
            this.BtnPrint.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(92, 35);
            this.BtnPrint.TabIndex = 165;
            this.BtnPrint.Text = "Print";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click_1);
            // 
            // BtnBestFit
            // 
            this.BtnBestFit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBestFit.Appearance.ForeColor = System.Drawing.Color.Green;
            this.BtnBestFit.Appearance.Options.UseFont = true;
            this.BtnBestFit.Appearance.Options.UseForeColor = true;
            this.BtnBestFit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBestFit.Location = new System.Drawing.Point(916, 3);
            this.BtnBestFit.LookAndFeel.SkinName = "McSkin";
            this.BtnBestFit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnBestFit.Name = "BtnBestFit";
            this.BtnBestFit.Size = new System.Drawing.Size(92, 35);
            this.BtnBestFit.TabIndex = 164;
            this.BtnBestFit.Text = "Best Fit";
            this.BtnBestFit.Click += new System.EventHandler(this.BtnBestFit_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Green;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExport.Location = new System.Drawing.Point(824, 3);
            this.BtnExport.LookAndFeel.SkinName = "McSkin";
            this.BtnExport.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(92, 35);
            this.BtnExport.TabIndex = 163;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // txtEmployee
            // 
            this.txtEmployee.ActivationColor = false;
            this.txtEmployee.AllowTabKeyOnEnter = false;
            this.txtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployee.ComplusoryMsg = null;
            this.txtEmployee.Enabled = false;
            this.txtEmployee.Format = "";
            this.txtEmployee.IsComplusory = false;
            this.txtEmployee.Location = new System.Drawing.Point(347, 41);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.RequiredChars = "";
            this.txtEmployee.SelectAllTextOnFocus = true;
            this.txtEmployee.ShowToolTipOnFocus = false;
            this.txtEmployee.Size = new System.Drawing.Size(209, 21);
            this.txtEmployee.TabIndex = 162;
            this.txtEmployee.ToolTips = "";
            this.txtEmployee.WaterMarkText = null;
            this.txtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployee_KeyPress);
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(268, 44);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(80, 16);
            this.cLabel3.TabIndex = 161;
            this.cLabel3.Text = "Employee";
            this.cLabel3.ToolTips = "";
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(611, 10);
            this.CmbKapan.Name = "CmbKapan";
            this.CmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.Appearance.Options.UseFont = true;
            this.CmbKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbKapan.Properties.DropDownRows = 20;
            this.CmbKapan.Properties.IncrementalSearch = true;
            this.CmbKapan.Size = new System.Drawing.Size(118, 22);
            this.CmbKapan.TabIndex = 160;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(560, 14);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(53, 16);
            this.cLabel5.TabIndex = 159;
            this.cLabel5.Text = "Kapan";
            this.cLabel5.ToolTips = "";
            // 
            // txtMinutes
            // 
            this.txtMinutes.ActivationColor = true;
            this.txtMinutes.AllowTabKeyOnEnter = false;
            this.txtMinutes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinutes.ComplusoryMsg = null;
            this.txtMinutes.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.txtMinutes.Format = "";
            this.txtMinutes.IsComplusory = false;
            this.txtMinutes.Location = new System.Drawing.Point(1105, 13);
            this.txtMinutes.MaxLength = 30;
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.RequiredChars = "";
            this.txtMinutes.SelectAllTextOnFocus = true;
            this.txtMinutes.ShowToolTipOnFocus = false;
            this.txtMinutes.Size = new System.Drawing.Size(60, 21);
            this.txtMinutes.TabIndex = 45;
            this.txtMinutes.ToolTips = "";
            this.txtMinutes.WaterMarkText = null;
            // 
            // lblmin
            // 
            this.lblmin.AutoSize = true;
            this.lblmin.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.lblmin.ForeColor = System.Drawing.Color.Black;
            this.lblmin.Location = new System.Drawing.Point(1120, 1);
            this.lblmin.Name = "lblmin";
            this.lblmin.Size = new System.Drawing.Size(30, 12);
            this.lblmin.TabIndex = 47;
            this.lblmin.Text = "Mins";
            this.lblmin.ToolTips = "";
            // 
            // lblApplyAll
            // 
            this.lblApplyAll.AutoSize = true;
            this.lblApplyAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblApplyAll.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblApplyAll.ForeColor = System.Drawing.Color.Navy;
            this.lblApplyAll.Location = new System.Drawing.Point(1103, 34);
            this.lblApplyAll.Name = "lblApplyAll";
            this.lblApplyAll.Size = new System.Drawing.Size(65, 13);
            this.lblApplyAll.TabIndex = 46;
            this.lblApplyAll.Text = "Apply All";
            this.lblApplyAll.ToolTips = "";
            this.lblApplyAll.Click += new System.EventHandler(this.lblApplyAll_Click);
            // 
            // cLabel10
            // 
            this.cLabel10.BackColor = System.Drawing.Color.Black;
            this.cLabel10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel10.ForeColor = System.Drawing.Color.White;
            this.cLabel10.Location = new System.Drawing.Point(262, 0);
            this.cLabel10.Name = "cLabel10";
            this.cLabel10.Size = new System.Drawing.Size(3, 69);
            this.cLabel10.TabIndex = 42;
            this.cLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel10.ToolTips = "";
            // 
            // RdbOverDue
            // 
            this.RdbOverDue.AutoSize = true;
            this.RdbOverDue.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.RdbOverDue.ForeColor = System.Drawing.Color.Black;
            this.RdbOverDue.Location = new System.Drawing.Point(161, 24);
            this.RdbOverDue.Name = "RdbOverDue";
            this.RdbOverDue.Size = new System.Drawing.Size(102, 25);
            this.RdbOverDue.TabIndex = 41;
            this.RdbOverDue.Text = "OverDue";
            this.RdbOverDue.ToolTips = "";
            this.RdbOverDue.UseVisualStyleBackColor = false;
            // 
            // RdbRunning
            // 
            this.RdbRunning.AutoSize = true;
            this.RdbRunning.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.RdbRunning.ForeColor = System.Drawing.Color.Black;
            this.RdbRunning.Location = new System.Drawing.Point(63, 24);
            this.RdbRunning.Name = "RdbRunning";
            this.RdbRunning.Size = new System.Drawing.Size(99, 25);
            this.RdbRunning.TabIndex = 40;
            this.RdbRunning.Text = "Running";
            this.RdbRunning.ToolTips = "";
            this.RdbRunning.UseVisualStyleBackColor = false;
            // 
            // RdbAll
            // 
            this.RdbAll.AutoSize = true;
            this.RdbAll.Checked = true;
            this.RdbAll.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.RdbAll.ForeColor = System.Drawing.Color.Black;
            this.RdbAll.Location = new System.Drawing.Point(9, 24);
            this.RdbAll.Name = "RdbAll";
            this.RdbAll.Size = new System.Drawing.Size(52, 25);
            this.RdbAll.TabIndex = 39;
            this.RdbAll.TabStop = true;
            this.RdbAll.Text = "All";
            this.RdbAll.ToolTips = "";
            this.RdbAll.UseVisualStyleBackColor = false;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(268, 13);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(43, 17);
            this.cLabel2.TabIndex = 38;
            this.cLabel2.Text = "Date";
            this.cLabel2.ToolTips = "";
            // 
            // DTPSearchToDate
            // 
            this.DTPSearchToDate.AllowTabKeyOnEnter = false;
            this.DTPSearchToDate.Checked = false;
            this.DTPSearchToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPSearchToDate.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.DTPSearchToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPSearchToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPSearchToDate.Location = new System.Drawing.Point(435, 12);
            this.DTPSearchToDate.Name = "DTPSearchToDate";
            this.DTPSearchToDate.ShowCheckBox = true;
            this.DTPSearchToDate.Size = new System.Drawing.Size(121, 21);
            this.DTPSearchToDate.TabIndex = 37;
            this.DTPSearchToDate.ToolTips = "";
            // 
            // DTPSearchFromDate
            // 
            this.DTPSearchFromDate.AllowTabKeyOnEnter = false;
            this.DTPSearchFromDate.Checked = false;
            this.DTPSearchFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPSearchFromDate.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.DTPSearchFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPSearchFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPSearchFromDate.Location = new System.Drawing.Point(313, 12);
            this.DTPSearchFromDate.Name = "DTPSearchFromDate";
            this.DTPSearchFromDate.ShowCheckBox = true;
            this.DTPSearchFromDate.Size = new System.Drawing.Size(121, 21);
            this.DTPSearchFromDate.TabIndex = 36;
            this.DTPSearchFromDate.ToolTips = "";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Green;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSearch.Location = new System.Drawing.Point(732, 3);
            this.BtnSearch.LookAndFeel.SkinName = "McSkin";
            this.BtnSearch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(92, 35);
            this.BtnSearch.TabIndex = 35;
            this.BtnSearch.Text = "Refresh(F5)";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // FrmMyAgingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 704);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMyAgingReport";
            this.Text = "DASHBOARD";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAdminDashboard_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridPacketDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetPacketDetail)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private AxonContLib.cPanel panel3;
        private AxonContLib.cPanel panel10;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private AxonContLib.cDateTimePicker DTPSearchFromDate;
        private AxonContLib.cDateTimePicker DTPSearchToDate;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraGrid.GridControl MainGridPacketDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetPacketDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cLabel cLabel15;
        private AxonContLib.cLabel lblTotalAgingOverDuePkts;
        private AxonContLib.cPanel panel6;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cLabel lblTotalAgingPackets;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel lblTotalAgingRunningPkts;
        private AxonContLib.cRadioButton RdbAll;
        private AxonContLib.cRadioButton RdbRunning;
        private AxonContLib.cRadioButton RdbOverDue;
        private AxonContLib.cLabel cLabel10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private AxonContLib.cLabel lblTest;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private AxonContLib.cTextBox txtPassForUpdateExtraMints;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private AxonContLib.cLabel lblApplyAll;
        private AxonContLib.cTextBox txtMinutes;
        private AxonContLib.cLabel lblmin;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cTextBox txtEmployee;
        private AxonContLib.cLabel cLabel3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraEditors.SimpleButton BtnBestFit;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbDepartment;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cCheckBox ChkUpdateExtramin;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;



    }
}