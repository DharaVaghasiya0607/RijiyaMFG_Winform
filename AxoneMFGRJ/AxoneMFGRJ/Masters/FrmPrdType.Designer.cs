namespace AxoneMFGRJ.Masters
{
    partial class FrmPrdType
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
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedAmountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepCmbDesignation = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtParaName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkIsActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepCmbPrdType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCmbCtsChkPrdType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCmbCtsChkBreakingType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtParaCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtSeqNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.CmbLabourType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repCmbProcessGroup = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.panel4 = new AxonContLib.cPanel(this.components);
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new AxonContLib.cPanel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbDesignation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbPrdType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbCtsChkPrdType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbCtsChkBreakingType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSeqNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbLabourType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbProcessGroup)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 50);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkIsActive,
            this.repTxtRemark,
            this.repTxtParaName,
            this.repTxtParaCode,
            this.repTxtSeqNo,
            this.CmbLabourType,
            this.repCmbProcessGroup,
            this.RepCmbPrdType,
            this.repCmbCtsChkPrdType,
            this.repCmbCtsChkBreakingType,
            this.RepCmbDesignation});
            this.MainGrid.Size = new System.Drawing.Size(1082, 373);
            this.MainGrid.TabIndex = 0;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedAmountToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(214, 26);
            // 
            // deleteSelectedAmountToolStripMenuItem
            // 
            this.deleteSelectedAmountToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.deleteSelectedAmountToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.deleteSelectedAmountToolStripMenuItem.Name = "deleteSelectedAmountToolStripMenuItem";
            this.deleteSelectedAmountToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.deleteSelectedAmountToolStripMenuItem.Text = "Delete Selected Item";
            this.deleteSelectedAmountToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedAmountToolStripMenuItem_Click);
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn21,
            this.gridColumn6,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsCustomization.AllowFilter = false;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
            this.GrdDet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdDet_KeyDown);
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Designation";
            this.gridColumn21.ColumnEdit = this.RepCmbDesignation;
            this.gridColumn21.FieldName = "DESIGNATION_ID";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 14;
            this.gridColumn21.Width = 201;
            // 
            // RepCmbDesignation
            // 
            this.RepCmbDesignation.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbDesignation.Appearance.Options.UseFont = true;
            this.RepCmbDesignation.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbDesignation.AppearanceDropDown.Options.UseFont = true;
            this.RepCmbDesignation.AppearanceFocused.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbDesignation.AppearanceFocused.Options.UseFont = true;
            this.RepCmbDesignation.AutoHeight = false;
            this.RepCmbDesignation.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepCmbDesignation.Name = "RepCmbDesignation";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Name";
            this.gridColumn6.ColumnEdit = this.repTxtParaName;
            this.gridColumn6.FieldName = "PRDTYPENAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 258;
            // 
            // repTxtParaName
            // 
            this.repTxtParaName.AutoHeight = false;
            this.repTxtParaName.MaxLength = 100;
            this.repTxtParaName.Name = "repTxtParaName";
            this.repTxtParaName.Validating += new System.ComponentModel.CancelEventHandler(this.repTxtParaName_Validating);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Active";
            this.gridColumn4.ColumnEdit = this.repChkIsActive;
            this.gridColumn4.FieldName = "ISACTIVE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 16;
            this.gridColumn4.Width = 59;
            // 
            // repChkIsActive
            // 
            this.repChkIsActive.AutoHeight = false;
            this.repChkIsActive.Caption = "Check";
            this.repChkIsActive.Name = "repChkIsActive";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Remark";
            this.gridColumn3.ColumnEdit = this.repTxtRemark;
            this.gridColumn3.FieldName = "REMARK";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Width = 263;
            // 
            // repTxtRemark
            // 
            this.repTxtRemark.AutoHeight = false;
            this.repTxtRemark.Name = "repTxtRemark";
            this.repTxtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repTxtRemark_KeyDown);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "PRDTYPE_ID";
            this.gridColumn5.FieldName = "PRDTYPE_ID";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Kapan";
            this.gridColumn1.FieldName = "ISKAPAN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "SrNo";
            this.gridColumn2.FieldName = "ISPACKETNO";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 70;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Tag";
            this.gridColumn7.FieldName = "ISTAG";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 70;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Emp";
            this.gridColumn8.FieldName = "ISEMPLOYEE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Width = 70;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Manager";
            this.gridColumn9.FieldName = "ISMANAGER";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn9.Width = 70;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Graph";
            this.gridColumn10.FieldName = "ISGRAPH";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 70;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Exp";
            this.gridColumn11.FieldName = "ISEXP";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 6;
            this.gridColumn11.Width = 70;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Mak";
            this.gridColumn12.FieldName = "ISMAK";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 7;
            this.gridColumn12.Width = 70;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Pol";
            this.gridColumn13.FieldName = "ISPOL";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 8;
            this.gridColumn13.Width = 70;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "SeqNo";
            this.gridColumn14.FieldName = "SEQUENCENO";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 10;
            this.gridColumn14.Width = 64;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Code";
            this.gridColumn15.FieldName = "PRDTYPECODE";
            this.gridColumn15.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 0;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Required Prediction";
            this.gridColumn16.ColumnEdit = this.RepCmbPrdType;
            this.gridColumn16.FieldName = "REQPRDTYPE_ID";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 11;
            this.gridColumn16.Width = 268;
            // 
            // RepCmbPrdType
            // 
            this.RepCmbPrdType.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbPrdType.Appearance.Options.UseFont = true;
            this.RepCmbPrdType.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbPrdType.AppearanceDropDown.Options.UseFont = true;
            this.RepCmbPrdType.AppearanceFocused.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbPrdType.AppearanceFocused.Options.UseFont = true;
            this.RepCmbPrdType.AutoHeight = false;
            this.RepCmbPrdType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepCmbPrdType.DropDownRows = 20;
            this.RepCmbPrdType.Name = "RepCmbPrdType";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "TFlg";
            this.gridColumn17.FieldName = "TFLAG";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 9;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Amt Check Prediction";
            this.gridColumn18.ColumnEdit = this.repCmbCtsChkPrdType;
            this.gridColumn18.FieldName = "PARAMCHECKPRDTYPE_ID";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 12;
            this.gridColumn18.Width = 250;
            // 
            // repCmbCtsChkPrdType
            // 
            this.repCmbCtsChkPrdType.AutoHeight = false;
            this.repCmbCtsChkPrdType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbCtsChkPrdType.Name = "repCmbCtsChkPrdType";
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Amt Check Breaking Type";
            this.gridColumn19.ColumnEdit = this.repCmbCtsChkBreakingType;
            this.gridColumn19.FieldName = "PARAMCHECKBREAKINGTYPE_ID";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 13;
            this.gridColumn19.Width = 220;
            // 
            // repCmbCtsChkBreakingType
            // 
            this.repCmbCtsChkBreakingType.AutoHeight = false;
            this.repCmbCtsChkBreakingType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbCtsChkBreakingType.Name = "repCmbCtsChkBreakingType";
            
            // 
            // gridColumn20
            // 
            this.gridColumn20.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn20.AppearanceCell.Options.UseFont = true;
            this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn20.Caption = "RapCalc %";
            this.gridColumn20.FieldName = "RAPCALCPER";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 15;
            this.gridColumn20.Width = 90;
            // 
            // repTxtParaCode
            // 
            this.repTxtParaCode.AutoHeight = false;
            this.repTxtParaCode.MaxLength = 15;
            this.repTxtParaCode.Name = "repTxtParaCode";
            // 
            // repTxtSeqNo
            // 
            this.repTxtSeqNo.AutoHeight = false;
            this.repTxtSeqNo.Mask.EditMask = "f0";
            this.repTxtSeqNo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repTxtSeqNo.Name = "repTxtSeqNo";
            // 
            // CmbLabourType
            // 
            this.CmbLabourType.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbLabourType.Appearance.Options.UseFont = true;
            this.CmbLabourType.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbLabourType.AppearanceDropDown.Options.UseFont = true;
            this.CmbLabourType.AppearanceFocused.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbLabourType.AppearanceFocused.Options.UseFont = true;
            this.CmbLabourType.AutoHeight = false;
            this.CmbLabourType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbLabourType.Items.AddRange(new object[] {
            "",
            "PCS",
            "CARAT"});
            this.CmbLabourType.Name = "CmbLabourType";
            this.CmbLabourType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repCmbProcessGroup
            // 
            this.repCmbProcessGroup.AutoHeight = false;
            this.repCmbProcessGroup.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbProcessGroup.Items.AddRange(new object[] {
            "CLV",
            "MFG"});
            this.repCmbProcessGroup.Name = "repCmbProcessGroup";
            this.repCmbProcessGroup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.Controls.Add(this.BtnExport);
            this.panel4.Controls.Add(this.BtnSave);
            this.panel4.Controls.Add(this.BtnAdd);
            this.panel4.Controls.Add(this.BtnBack);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 423);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 50);
            this.panel4.TabIndex = 0;
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Location = new System.Drawing.Point(223, 7);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(103, 35);
            this.BtnExport.TabIndex = 33;
            this.BtnExport.TabStop = false;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.Location = new System.Drawing.Point(7, 7);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 35);
            this.BtnSave.TabIndex = 30;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAdd.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Appearance.Options.UseFont = true;
            this.BtnAdd.Appearance.Options.UseForeColor = true;
            this.BtnAdd.Location = new System.Drawing.Point(115, 7);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(103, 35);
            this.BtnAdd.TabIndex = 31;
            this.BtnAdd.TabStop = false;
            this.BtnAdd.Text = "&Clear";
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBack.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Appearance.Options.UseForeColor = true;
            this.BtnBack.Location = new System.Drawing.Point(331, 7);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 32;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 50);
            this.panel1.TabIndex = 30;
            // 
            // FrmPrdType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Name = "FrmPrdType";
            this.Text = "PREDICTION TYPE";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLedger_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbDesignation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbPrdType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbCtsChkPrdType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbCtsChkBreakingType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSeqNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbLabourType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbProcessGroup)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private AxonContLib.cPanel panel4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkIsActive;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtParaCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtParaName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedAmountToolStripMenuItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtSeqNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox CmbLabourType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repCmbProcessGroup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit RepCmbPrdType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repCmbCtsChkPrdType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repCmbCtsChkBreakingType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit RepCmbDesignation;


    }
}