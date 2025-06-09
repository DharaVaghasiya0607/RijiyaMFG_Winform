namespace AxoneMFGRJ.Rapaport
{
    partial class FrmDiscountDiff 
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDiscountDiff));
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtFromPcs = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtToPcs = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtFromCarat = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtToCarat = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtFromAmt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtToAmt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.pnlHead = new AxonContLib.cPanel(this.components);
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.txtDiscount_ID = new AxonContLib.cTextBox(this.components);
            this.DTPRapDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.txtDiscountDiff = new AxonContLib.cTextBox(this.components);
            this.txtFL = new AxonContLib.cTextBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtToCarat = new AxonContLib.cTextBox(this.components);
            this.cLabel10 = new AxonContLib.cLabel(this.components);
            this.txtFromCarat = new AxonContLib.cTextBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.txtClarity = new AxonContLib.cTextBox(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.txtColor = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.txtShape = new AxonContLib.cTextBox(this.components);
            this.pnlGrid = new AxonContLib.cPanel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtFromPcs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtToPcs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtFromCarat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtToCarat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtFromAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtToAmt)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.MainGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.MainGrid.Location = new System.Drawing.Point(0, 0);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTxtRate,
            this.repTxtFromPcs,
            this.repTxtToPcs,
            this.repTxtFromCarat,
            this.repTxtToCarat,
            this.repTxtFromAmt,
            this.repTxtToAmt});
            this.MainGrid.Size = new System.Drawing.Size(1082, 380);
            this.MainGrid.TabIndex = 0;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
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
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Discount ID";
            this.gridColumn1.FieldName = "DISCOUNT_ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Shape";
            this.gridColumn3.FieldName = "S_NAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 94;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "S_CODE";
            this.gridColumn2.FieldName = "S_CODE";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Color";
            this.gridColumn5.FieldName = "C_NAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "C_CODE";
            this.gridColumn4.FieldName = "C_CODE";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Clarity";
            this.gridColumn7.FieldName = "Q_NAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 100;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Q_CODE";
            this.gridColumn6.FieldName = "Q_CODE";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "FL";
            this.gridColumn9.FieldName = "FL_NAME";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 85;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "FL_CODE";
            this.gridColumn8.FieldName = "FL_CODE";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Cut";
            this.gridColumn10.FieldName = "CUT_CODE";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "Pol";
            this.gridColumn11.FieldName = "POL_CODE";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "Sym";
            this.gridColumn12.FieldName = "SYM_CODE";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "RapDate";
            this.gridColumn13.FieldName = "RAPDATE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            this.gridColumn13.Width = 107;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn14.Caption = "F Carat";
            this.gridColumn14.FieldName = "F_CARAT";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 5;
            this.gridColumn14.Width = 91;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn15.Caption = "T Carat";
            this.gridColumn15.FieldName = "T_CARAT";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 6;
            this.gridColumn15.Width = 93;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn16.Caption = "Disc Diff";
            this.gridColumn16.FieldName = "DISCOUNTDIFF";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 7;
            this.gridColumn16.Width = 84;
            // 
            // repTxtRate
            // 
            this.repTxtRate.AutoHeight = false;
            this.repTxtRate.Mask.EditMask = "[0-9]{0,6}\\.[0-9]{0,3}";
            this.repTxtRate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repTxtRate.Name = "repTxtRate";
            // 
            // repTxtFromPcs
            // 
            this.repTxtFromPcs.AutoHeight = false;
            this.repTxtFromPcs.Mask.EditMask = "[0-9]{0,6}";
            this.repTxtFromPcs.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repTxtFromPcs.Name = "repTxtFromPcs";
            // 
            // repTxtToPcs
            // 
            this.repTxtToPcs.AutoHeight = false;
            this.repTxtToPcs.Mask.EditMask = "[0-9]{0,6}";
            this.repTxtToPcs.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repTxtToPcs.Name = "repTxtToPcs";
            // 
            // repTxtFromCarat
            // 
            this.repTxtFromCarat.AutoHeight = false;
            this.repTxtFromCarat.Mask.EditMask = "[0-9]{0,6}\\.[0-9]{0,3}";
            this.repTxtFromCarat.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repTxtFromCarat.Name = "repTxtFromCarat";
            // 
            // repTxtToCarat
            // 
            this.repTxtToCarat.AutoHeight = false;
            this.repTxtToCarat.Mask.EditMask = "[0-9]{0,6}\\.[0-9]{0,3}";
            this.repTxtToCarat.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repTxtToCarat.Name = "repTxtToCarat";
            // 
            // repTxtFromAmt
            // 
            this.repTxtFromAmt.AutoHeight = false;
            this.repTxtFromAmt.Mask.EditMask = "[0-9]{0,6}\\.[0-9]{0,3}";
            this.repTxtFromAmt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repTxtFromAmt.Name = "repTxtFromAmt";
            // 
            // repTxtToAmt
            // 
            this.repTxtToAmt.AutoHeight = false;
            this.repTxtToAmt.Mask.EditMask = "[0-9]{0,6}\\.[0-9]{0,3}";
            this.repTxtToAmt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repTxtToAmt.Name = "repTxtToAmt";
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.BtnDelete);
            this.pnlHead.Controls.Add(this.txtDiscount_ID);
            this.pnlHead.Controls.Add(this.DTPRapDate);
            this.pnlHead.Controls.Add(this.cLabel5);
            this.pnlHead.Controls.Add(this.BtnExport);
            this.pnlHead.Controls.Add(this.BtnSave);
            this.pnlHead.Controls.Add(this.BtnAdd);
            this.pnlHead.Controls.Add(this.cLabel6);
            this.pnlHead.Controls.Add(this.cLabel9);
            this.pnlHead.Controls.Add(this.BtnBack);
            this.pnlHead.Controls.Add(this.txtDiscountDiff);
            this.pnlHead.Controls.Add(this.txtFL);
            this.pnlHead.Controls.Add(this.cLabel1);
            this.pnlHead.Controls.Add(this.txtToCarat);
            this.pnlHead.Controls.Add(this.cLabel10);
            this.pnlHead.Controls.Add(this.txtFromCarat);
            this.pnlHead.Controls.Add(this.cLabel4);
            this.pnlHead.Controls.Add(this.txtClarity);
            this.pnlHead.Controls.Add(this.cLabel3);
            this.pnlHead.Controls.Add(this.txtColor);
            this.pnlHead.Controls.Add(this.cLabel2);
            this.pnlHead.Controls.Add(this.txtShape);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1082, 93);
            this.pnlHead.TabIndex = 0;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDelete.ImageOptions.SvgImage")));
            this.BtnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnDelete.Location = new System.Drawing.Point(621, 49);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 35);
            this.BtnDelete.TabIndex = 11;
            this.BtnDelete.Text = "&Delete";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // txtDiscount_ID
            // 
            this.txtDiscount_ID.ActivationColor = true;
            this.txtDiscount_ID.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtDiscount_ID.AllowTabKeyOnEnter = true;
            this.txtDiscount_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscount_ID.Font = new System.Drawing.Font("Tahoma", 3F);
            this.txtDiscount_ID.Format = "";
            this.txtDiscount_ID.IsComplusory = false;
            this.txtDiscount_ID.Location = new System.Drawing.Point(54, 2);
            this.txtDiscount_ID.MaxLength = 4;
            this.txtDiscount_ID.Name = "txtDiscount_ID";
            this.txtDiscount_ID.SelectAllTextOnFocus = true;
            this.txtDiscount_ID.Size = new System.Drawing.Size(76, 12);
            this.txtDiscount_ID.TabIndex = 36;
            this.txtDiscount_ID.ToolTips = "";
            this.txtDiscount_ID.Visible = false;
            this.txtDiscount_ID.WaterMarkText = null;
            // 
            // DTPRapDate
            // 
            this.DTPRapDate.AllowTabKeyOnEnter = true;
            this.DTPRapDate.CustomFormat = "dd/MM/yyyy";
            this.DTPRapDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPRapDate.ForeColor = System.Drawing.Color.Black;
            this.DTPRapDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPRapDate.Location = new System.Drawing.Point(835, 16);
            this.DTPRapDate.Name = "DTPRapDate";
            this.DTPRapDate.Size = new System.Drawing.Size(113, 22);
            this.DTPRapDate.TabIndex = 6;
            this.DTPRapDate.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(795, 20);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(37, 14);
            this.cLabel5.TabIndex = 34;
            this.cLabel5.Text = "Date";
            this.cLabel5.ToolTips = "";
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExport.ImageOptions.SvgImage")));
            this.BtnExport.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExport.Location = new System.Drawing.Point(838, 49);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(103, 35);
            this.BtnExport.TabIndex = 13;
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
            this.BtnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSave.ImageOptions.SvgImage")));
            this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSave.Location = new System.Drawing.Point(512, 49);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 35);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAdd.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Appearance.Options.UseFont = true;
            this.BtnAdd.Appearance.Options.UseForeColor = true;
            this.BtnAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnAdd.ImageOptions.SvgImage")));
            this.BtnAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnAdd.Location = new System.Drawing.Point(730, 49);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(103, 35);
            this.BtnAdd.TabIndex = 12;
            this.BtnAdd.TabStop = false;
            this.BtnAdd.Text = "&Clear";
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 9F);
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(952, 20);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(20, 14);
            this.cLabel6.TabIndex = 33;
            this.cLabel6.Text = "%";
            this.cLabel6.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 9F);
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(690, 20);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(21, 14);
            this.cLabel9.TabIndex = 33;
            this.cLabel9.Text = "FL";
            this.cLabel9.ToolTips = "";
            // 
            // BtnBack
            // 
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBack.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Appearance.Options.UseForeColor = true;
            this.BtnBack.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnBack.ImageOptions.SvgImage")));
            this.BtnBack.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnBack.Location = new System.Drawing.Point(946, 49);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 14;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // txtDiscountDiff
            // 
            this.txtDiscountDiff.ActivationColor = true;
            this.txtDiscountDiff.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtDiscountDiff.AllowTabKeyOnEnter = true;
            this.txtDiscountDiff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscountDiff.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtDiscountDiff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtDiscountDiff.Format = "############0.000";
            this.txtDiscountDiff.IsComplusory = false;
            this.txtDiscountDiff.Location = new System.Drawing.Point(974, 16);
            this.txtDiscountDiff.MaxLength = 4;
            this.txtDiscountDiff.Name = "txtDiscountDiff";
            this.txtDiscountDiff.SelectAllTextOnFocus = true;
            this.txtDiscountDiff.Size = new System.Drawing.Size(75, 22);
            this.txtDiscountDiff.TabIndex = 7;
            this.txtDiscountDiff.Text = "0";
            this.txtDiscountDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscountDiff.ToolTips = "";
            this.txtDiscountDiff.WaterMarkText = null;
            // 
            // txtFL
            // 
            this.txtFL.ActivationColor = true;
            this.txtFL.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtFL.AllowTabKeyOnEnter = true;
            this.txtFL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFL.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtFL.Format = "";
            this.txtFL.IsComplusory = false;
            this.txtFL.Location = new System.Drawing.Point(713, 16);
            this.txtFL.MaxLength = 4;
            this.txtFL.Name = "txtFL";
            this.txtFL.SelectAllTextOnFocus = true;
            this.txtFL.Size = new System.Drawing.Size(75, 22);
            this.txtFL.TabIndex = 5;
            this.txtFL.ToolTips = "";
            this.txtFL.WaterMarkText = null;
            this.txtFL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFL_KeyPress);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9F);
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(539, 20);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(60, 14);
            this.cLabel1.TabIndex = 33;
            this.cLabel1.Text = "To Carat";
            this.cLabel1.ToolTips = "";
            // 
            // txtToCarat
            // 
            this.txtToCarat.ActivationColor = true;
            this.txtToCarat.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtToCarat.AllowTabKeyOnEnter = true;
            this.txtToCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToCarat.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtToCarat.Format = "############0.0000";
            this.txtToCarat.IsComplusory = false;
            this.txtToCarat.Location = new System.Drawing.Point(603, 16);
            this.txtToCarat.MaxLength = 4;
            this.txtToCarat.Name = "txtToCarat";
            this.txtToCarat.SelectAllTextOnFocus = true;
            this.txtToCarat.Size = new System.Drawing.Size(75, 22);
            this.txtToCarat.TabIndex = 4;
            this.txtToCarat.Text = "0";
            this.txtToCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtToCarat.ToolTips = "";
            this.txtToCarat.WaterMarkText = null;
            // 
            // cLabel10
            // 
            this.cLabel10.AutoSize = true;
            this.cLabel10.Font = new System.Drawing.Font("Verdana", 9F);
            this.cLabel10.ForeColor = System.Drawing.Color.Black;
            this.cLabel10.Location = new System.Drawing.Point(375, 20);
            this.cLabel10.Name = "cLabel10";
            this.cLabel10.Size = new System.Drawing.Size(77, 14);
            this.cLabel10.TabIndex = 33;
            this.cLabel10.Text = "From Carat";
            this.cLabel10.ToolTips = "";
            // 
            // txtFromCarat
            // 
            this.txtFromCarat.ActivationColor = true;
            this.txtFromCarat.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtFromCarat.AllowTabKeyOnEnter = true;
            this.txtFromCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromCarat.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtFromCarat.Format = "###########0.0000";
            this.txtFromCarat.IsComplusory = false;
            this.txtFromCarat.Location = new System.Drawing.Point(455, 16);
            this.txtFromCarat.MaxLength = 4;
            this.txtFromCarat.Name = "txtFromCarat";
            this.txtFromCarat.SelectAllTextOnFocus = true;
            this.txtFromCarat.Size = new System.Drawing.Size(75, 22);
            this.txtFromCarat.TabIndex = 3;
            this.txtFromCarat.Text = "0";
            this.txtFromCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFromCarat.ToolTips = "";
            this.txtFromCarat.WaterMarkText = null;
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 9F);
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(245, 20);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(47, 14);
            this.cLabel4.TabIndex = 33;
            this.cLabel4.Text = "Clarity";
            this.cLabel4.ToolTips = "";
            // 
            // txtClarity
            // 
            this.txtClarity.ActivationColor = true;
            this.txtClarity.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtClarity.AllowTabKeyOnEnter = true;
            this.txtClarity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClarity.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtClarity.Format = "";
            this.txtClarity.IsComplusory = false;
            this.txtClarity.Location = new System.Drawing.Point(294, 16);
            this.txtClarity.MaxLength = 4;
            this.txtClarity.Name = "txtClarity";
            this.txtClarity.SelectAllTextOnFocus = true;
            this.txtClarity.Size = new System.Drawing.Size(75, 22);
            this.txtClarity.TabIndex = 2;
            this.txtClarity.ToolTips = "";
            this.txtClarity.WaterMarkText = null;
            this.txtClarity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClarity_KeyPress);
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9F);
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(138, 20);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(40, 14);
            this.cLabel3.TabIndex = 33;
            this.cLabel3.Text = "Color";
            this.cLabel3.ToolTips = "";
            // 
            // txtColor
            // 
            this.txtColor.ActivationColor = true;
            this.txtColor.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtColor.AllowTabKeyOnEnter = true;
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtColor.Format = "";
            this.txtColor.IsComplusory = false;
            this.txtColor.Location = new System.Drawing.Point(179, 16);
            this.txtColor.MaxLength = 4;
            this.txtColor.Name = "txtColor";
            this.txtColor.SelectAllTextOnFocus = true;
            this.txtColor.Size = new System.Drawing.Size(58, 22);
            this.txtColor.TabIndex = 1;
            this.txtColor.ToolTips = "";
            this.txtColor.WaterMarkText = null;
            this.txtColor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtColor_KeyPress);
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F);
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(6, 20);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(47, 14);
            this.cLabel2.TabIndex = 33;
            this.cLabel2.Text = "Shape";
            this.cLabel2.ToolTips = "";
            // 
            // txtShape
            // 
            this.txtShape.ActivationColor = true;
            this.txtShape.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtShape.AllowTabKeyOnEnter = true;
            this.txtShape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShape.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtShape.Format = "";
            this.txtShape.IsComplusory = false;
            this.txtShape.Location = new System.Drawing.Point(54, 16);
            this.txtShape.MaxLength = 4;
            this.txtShape.Name = "txtShape";
            this.txtShape.SelectAllTextOnFocus = true;
            this.txtShape.Size = new System.Drawing.Size(76, 22);
            this.txtShape.TabIndex = 0;
            this.txtShape.ToolTips = "";
            this.txtShape.WaterMarkText = null;
            this.txtShape.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShape_KeyPress);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.MainGrid);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 93);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1082, 380);
            this.pnlGrid.TabIndex = 32;
            // 
            // FrmDiscountDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmDiscountDiff";
            this.Text = "DISCOUNT DIFF";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLedger_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtFromPcs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtToPcs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtFromCarat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtToCarat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtFromAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtToAmt)).EndInit();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private AxonContLib.cPanel pnlHead;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private AxonContLib.cTextBox txtShape;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtFromPcs;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtToPcs;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtFromCarat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtToCarat;
        private AxonContLib.cPanel pnlGrid;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtFromAmt;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtToAmt;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cTextBox txtColor;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cTextBox txtClarity;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cTextBox txtFL;
        private AxonContLib.cDateTimePicker DTPRapDate;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cTextBox txtDiscountDiff;
        private AxonContLib.cLabel cLabel10;
        private AxonContLib.cTextBox txtFromCarat;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtToCarat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private AxonContLib.cTextBox txtDiscount_ID;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;


    }
}