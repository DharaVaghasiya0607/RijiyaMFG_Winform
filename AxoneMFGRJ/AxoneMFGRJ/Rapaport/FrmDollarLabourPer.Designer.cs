namespace AxoneMFGRJ.Rapaport
{
    partial class FrmDollarLabourPer
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
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CmbDollarType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtShapeName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtCutName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtPolName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtSymName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panel4 = new AxonContLib.cPanel();
            this.panel5 = new AxonContLib.cPanel();
            this.CmbShapeType = new AxonContLib.cComboBox(this.components);
            this.lblShapeType = new AxonContLib.cLabel(this.components);
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.PnlDollarLabourType = new AxonContLib.cPanel();
            this.CmbLabourType = new AxonContLib.cComboBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.panel2 = new AxonContLib.cPanel();
            this.txtYear = new AxonContLib.cTextBox(this.components);
            this.txtMonth = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.panel1 = new AxonContLib.cPanel();
            this.BtnLeft = new DevExpress.XtraEditors.SimpleButton();
            this.PnlCopyPaste = new AxonContLib.cPanel();
            this.ChkCmbMonth = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.txtCopyToYear = new AxonContLib.cTextBox(this.components);
            this.BtnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbDollarType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtShapeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtCutName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtPolName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSymName)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.PnlDollarLabourType.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PnlCopyPaste.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbMonth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 44);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTxtRemark,
            this.CmbDollarType,
            this.repTxtShapeName,
            this.repTxtCutName,
            this.repTxtPolName,
            this.repTxtSymName});
            this.MainGrid.Size = new System.Drawing.Size(1082, 379);
            this.MainGrid.TabIndex = 1;
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
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn6,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn9,
            this.gridColumn8,
            this.gridColumn11,
            this.gridColumn10,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ID";
            this.gridColumn2.FieldName = "LABOUR_ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn2.Width = 86;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Dollar Type";
            this.gridColumn1.ColumnEdit = this.CmbDollarType;
            this.gridColumn1.FieldName = "DOLLARLABOURTYPE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 8;
            this.gridColumn1.Width = 276;
            // 
            // CmbDollarType
            // 
            this.CmbDollarType.AutoHeight = false;
            this.CmbDollarType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbDollarType.Items.AddRange(new object[] {
            "Plus Dollar",
            "Minus Dollar",
            "Final DF +",
            "Final DF -",
            "Checker DF +",
            "Checker DF -",
            "Plan Variation",
            "Breaking"});
            this.CmbDollarType.Name = "CmbDollarType";
            this.CmbDollarType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "From Amt";
            this.gridColumn5.FieldName = "FROMAMOUNT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 100;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "To Amt";
            this.gridColumn7.FieldName = "TOAMOUNT";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 100;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "From Carat";
            this.gridColumn6.FieldName = "FROMCARAT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 134;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "To Carat";
            this.gridColumn4.FieldName = "TOCARAT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 7;
            this.gridColumn4.Width = 118;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Per";
            this.gridColumn3.ColumnEdit = this.repTxtRemark;
            this.gridColumn3.FieldName = "PER";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 9;
            this.gridColumn3.Width = 129;
            // 
            // repTxtRemark
            // 
            this.repTxtRemark.AutoHeight = false;
            this.repTxtRemark.Name = "repTxtRemark";
            this.repTxtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repTxtRemark_KeyDown);
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "Shape_ID";
            this.gridColumn9.FieldName = "SHAPE_ID";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Shape";
            this.gridColumn8.ColumnEdit = this.repTxtShapeName;
            this.gridColumn8.FieldName = "SHAPENAME";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            // 
            // repTxtShapeName
            // 
            this.repTxtShapeName.AutoHeight = false;
            this.repTxtShapeName.Name = "repTxtShapeName";
            this.repTxtShapeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repTxtShapeName_KeyPress);
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "Cut_ID";
            this.gridColumn11.FieldName = "CUT_ID";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Cut";
            this.gridColumn10.ColumnEdit = this.repTxtCutName;
            this.gridColumn10.FieldName = "CUTNAME";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            // 
            // repTxtCutName
            // 
            this.repTxtCutName.AutoHeight = false;
            this.repTxtCutName.Name = "repTxtCutName";
            this.repTxtCutName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repTxtCutName_KeyPress);
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "Pol_ID";
            this.gridColumn12.FieldName = "POL_ID";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "Pol";
            this.gridColumn13.ColumnEdit = this.repTxtPolName;
            this.gridColumn13.FieldName = "POLNAME";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            // 
            // repTxtPolName
            // 
            this.repTxtPolName.AutoHeight = false;
            this.repTxtPolName.Name = "repTxtPolName";
            this.repTxtPolName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repTxtPolName_KeyPress);
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "Sym_ID";
            this.gridColumn14.FieldName = "SYM_ID";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.Caption = "Sym";
            this.gridColumn15.ColumnEdit = this.repTxtSymName;
            this.gridColumn15.FieldName = "SYMNAME";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 3;
            // 
            // repTxtSymName
            // 
            this.repTxtSymName.AutoHeight = false;
            this.repTxtSymName.Name = "repTxtSymName";
            this.repTxtSymName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repTxtSymName_KeyPress);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.PnlDollarLabourType);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 44);
            this.panel4.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.CmbShapeType);
            this.panel5.Controls.Add(this.lblShapeType);
            this.panel5.Controls.Add(this.BtnShow);
            this.panel5.Controls.Add(this.cLabel3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(523, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(559, 44);
            this.panel5.TabIndex = 149;
            // 
            // CmbShapeType
            // 
            this.CmbShapeType.AllowTabKeyOnEnter = true;
            this.CmbShapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbShapeType.Font = new System.Drawing.Font("Verdana", 11.25F);
            this.CmbShapeType.ForeColor = System.Drawing.Color.Black;
            this.CmbShapeType.FormattingEnabled = true;
            this.CmbShapeType.Items.AddRange(new object[] {
            "Round",
            "Fancy"});
            this.CmbShapeType.Location = new System.Drawing.Point(52, 9);
            this.CmbShapeType.MaxDropDownItems = 12;
            this.CmbShapeType.Name = "CmbShapeType";
            this.CmbShapeType.Size = new System.Drawing.Size(165, 26);
            this.CmbShapeType.TabIndex = 10;
            this.CmbShapeType.ToolTips = "";
            // 
            // lblShapeType
            // 
            this.lblShapeType.AutoSize = true;
            this.lblShapeType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShapeType.ForeColor = System.Drawing.Color.Black;
            this.lblShapeType.Location = new System.Drawing.Point(2, 15);
            this.lblShapeType.Name = "lblShapeType";
            this.lblShapeType.Size = new System.Drawing.Size(47, 14);
            this.lblShapeType.TabIndex = 149;
            this.lblShapeType.Text = "Shape";
            this.lblShapeType.ToolTips = "";
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.Appearance.Options.UseForeColor = true;
            this.BtnShow.Location = new System.Drawing.Point(223, 7);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(103, 31);
            this.BtnShow.TabIndex = 11;
            this.BtnShow.Text = "Show";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(471, 24);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(252, 14);
            this.cLabel3.TabIndex = 145;
            this.cLabel3.Text = "Month Wise Pcs Production Entry Module";
            this.cLabel3.ToolTips = "";
            // 
            // PnlDollarLabourType
            // 
            this.PnlDollarLabourType.Controls.Add(this.CmbLabourType);
            this.PnlDollarLabourType.Controls.Add(this.cLabel4);
            this.PnlDollarLabourType.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlDollarLabourType.Location = new System.Drawing.Point(235, 0);
            this.PnlDollarLabourType.Name = "PnlDollarLabourType";
            this.PnlDollarLabourType.Size = new System.Drawing.Size(288, 44);
            this.PnlDollarLabourType.TabIndex = 148;
            // 
            // CmbLabourType
            // 
            this.CmbLabourType.AllowTabKeyOnEnter = true;
            this.CmbLabourType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLabourType.Font = new System.Drawing.Font("Verdana", 11.25F);
            this.CmbLabourType.ForeColor = System.Drawing.Color.Black;
            this.CmbLabourType.FormattingEnabled = true;
            this.CmbLabourType.Items.AddRange(new object[] {
            "Checker DF-",
            "Checker DF+",
            "Labour Per",
            "Minus Dollar",
            "Plus Dollar",
            "Worker Plus Dollar",
            "Worker Minus Dollar",
            "Worker DF-",
            "Worker DF+",
            "Worker Shape Per",
            "Worker CutPolSym Per",
            "ChiefArtist Plus Dollar",
            "ChiefArtist Minus Dollar",
            "ChiefArtist DF+",
            "ChiefArtist DF-"});
            this.CmbLabourType.Location = new System.Drawing.Point(44, 9);
            this.CmbLabourType.MaxDropDownItems = 12;
            this.CmbLabourType.Name = "CmbLabourType";
            this.CmbLabourType.Size = new System.Drawing.Size(237, 26);
            this.CmbLabourType.TabIndex = 5;
            this.CmbLabourType.ToolTips = "";
            this.CmbLabourType.SelectedIndexChanged += new System.EventHandler(this.CmbLabourType_SelectedIndexChanged);
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(7, 15);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(37, 14);
            this.cLabel4.TabIndex = 147;
            this.cLabel4.Text = "Type";
            this.cLabel4.ToolTips = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtYear);
            this.panel2.Controls.Add(this.txtMonth);
            this.panel2.Controls.Add(this.cLabel2);
            this.panel2.Controls.Add(this.cLabel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(235, 44);
            this.panel2.TabIndex = 3;
            // 
            // txtYear
            // 
            this.txtYear.ActivationColor = true;
            this.txtYear.AllowTabKeyOnEnter = true;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.ComplusoryMsg = null;
            this.txtYear.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtYear.Format = "######";
            this.txtYear.IsComplusory = false;
            this.txtYear.Location = new System.Drawing.Point(43, 11);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.RequiredChars = "0123456789.";
            this.txtYear.SelectAllTextOnFocus = true;
            this.txtYear.ShowToolTipOnFocus = false;
            this.txtYear.Size = new System.Drawing.Size(86, 23);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.ToolTips = "";
            this.txtYear.WaterMarkText = null;
            // 
            // txtMonth
            // 
            this.txtMonth.ActivationColor = true;
            this.txtMonth.AllowTabKeyOnEnter = true;
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.ComplusoryMsg = null;
            this.txtMonth.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtMonth.Format = "######";
            this.txtMonth.IsComplusory = false;
            this.txtMonth.Location = new System.Drawing.Point(185, 11);
            this.txtMonth.MaxLength = 4;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.RequiredChars = "0123456789.";
            this.txtMonth.SelectAllTextOnFocus = true;
            this.txtMonth.ShowToolTipOnFocus = false;
            this.txtMonth.Size = new System.Drawing.Size(46, 23);
            this.txtMonth.TabIndex = 3;
            this.txtMonth.Text = "0";
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.ToolTips = "";
            this.txtMonth.WaterMarkText = null;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(6, 15);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(35, 14);
            this.cLabel2.TabIndex = 0;
            this.cLabel2.Text = "Year";
            this.cLabel2.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(136, 15);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(46, 14);
            this.cLabel1.TabIndex = 2;
            this.cLabel1.Text = "Month";
            this.cLabel1.ToolTips = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.BtnLeft);
            this.panel1.Controls.Add(this.PnlCopyPaste);
            this.panel1.Controls.Add(this.BtnExport);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnAdd);
            this.panel1.Controls.Add(this.BtnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 50);
            this.panel1.TabIndex = 2;
            // 
            // BtnLeft
            // 
            this.BtnLeft.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnLeft.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnLeft.Appearance.Options.UseFont = true;
            this.BtnLeft.Appearance.Options.UseForeColor = true;
            this.BtnLeft.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.BtnLeft.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnLeft.Image = global::AxoneMFGRJ.Properties.Resources.A2;
            this.BtnLeft.Location = new System.Drawing.Point(520, 0);
            this.BtnLeft.Name = "BtnLeft";
            this.BtnLeft.Size = new System.Drawing.Size(39, 50);
            this.BtnLeft.TabIndex = 35;
            this.BtnLeft.TabStop = false;
            this.BtnLeft.Click += new System.EventHandler(this.BtnLeft_Click);
            // 
            // PnlCopyPaste
            // 
            this.PnlCopyPaste.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PnlCopyPaste.Controls.Add(this.ChkCmbMonth);
            this.PnlCopyPaste.Controls.Add(this.cLabel5);
            this.PnlCopyPaste.Controls.Add(this.cLabel6);
            this.PnlCopyPaste.Controls.Add(this.txtCopyToYear);
            this.PnlCopyPaste.Controls.Add(this.BtnCopy);
            this.PnlCopyPaste.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlCopyPaste.Location = new System.Drawing.Point(559, 0);
            this.PnlCopyPaste.Name = "PnlCopyPaste";
            this.PnlCopyPaste.Size = new System.Drawing.Size(523, 50);
            this.PnlCopyPaste.TabIndex = 34;
            // 
            // ChkCmbMonth
            // 
            this.ChkCmbMonth.EditValue = "";
            this.ChkCmbMonth.Location = new System.Drawing.Point(308, 14);
            this.ChkCmbMonth.Name = "ChkCmbMonth";
            this.ChkCmbMonth.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ChkCmbMonth.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbMonth.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(1, "Jan"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(2, "Fab"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(3, "March"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(4, "April"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(5, "May"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("6", "June"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((short)(7)), "July"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(8, "Aug"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(9, "Sep"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(10, "Oct"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(11, "Nov"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(12, "Dec")});
            this.ChkCmbMonth.Size = new System.Drawing.Size(96, 22);
            this.ChkCmbMonth.TabIndex = 10;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(202, 18);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(104, 14);
            this.cLabel5.TabIndex = 9;
            this.cLabel5.Text = "Copy To Month";
            this.cLabel5.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(6, 18);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(96, 14);
            this.cLabel6.TabIndex = 7;
            this.cLabel6.Text = "Copy To Year";
            this.cLabel6.ToolTips = "";
            // 
            // txtCopyToYear
            // 
            this.txtCopyToYear.ActivationColor = true;
            this.txtCopyToYear.AllowTabKeyOnEnter = true;
            this.txtCopyToYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCopyToYear.ComplusoryMsg = null;
            this.txtCopyToYear.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtCopyToYear.Format = "######";
            this.txtCopyToYear.IsComplusory = false;
            this.txtCopyToYear.Location = new System.Drawing.Point(104, 14);
            this.txtCopyToYear.MaxLength = 4;
            this.txtCopyToYear.Name = "txtCopyToYear";
            this.txtCopyToYear.RequiredChars = "0123456789.";
            this.txtCopyToYear.SelectAllTextOnFocus = true;
            this.txtCopyToYear.ShowToolTipOnFocus = false;
            this.txtCopyToYear.Size = new System.Drawing.Size(90, 23);
            this.txtCopyToYear.TabIndex = 1;
            this.txtCopyToYear.Text = "0";
            this.txtCopyToYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCopyToYear.ToolTips = "";
            this.txtCopyToYear.WaterMarkText = null;
            // 
            // BtnCopy
            // 
            this.BtnCopy.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnCopy.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCopy.Appearance.Options.UseFont = true;
            this.BtnCopy.Appearance.Options.UseForeColor = true;
            this.BtnCopy.Location = new System.Drawing.Point(411, 8);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(103, 32);
            this.BtnCopy.TabIndex = 3;
            this.BtnCopy.Text = "Copy To";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Location = new System.Drawing.Point(225, 8);
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
            this.BtnSave.Location = new System.Drawing.Point(9, 8);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 35);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAdd.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Appearance.Options.UseFont = true;
            this.BtnAdd.Appearance.Options.UseForeColor = true;
            this.BtnAdd.Location = new System.Drawing.Point(117, 8);
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
            this.BtnBack.Location = new System.Drawing.Point(333, 8);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 32;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // FrmDollarLabourPer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Name = "FrmDollarLabourPer";
            this.Text = "DOLLAR LABOUR PER";
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbDollarType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtShapeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtCutName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtPolName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSymName)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.PnlDollarLabourType.ResumeLayout(false);
            this.PnlDollarLabourType.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.PnlCopyPaste.ResumeLayout(false);
            this.PnlCopyPaste.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbMonth.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private AxonContLib.cPanel panel4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRemark;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedAmountToolStripMenuItem;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtMonth;
        private AxonContLib.cTextBox txtYear;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private AxonContLib.cLabel cLabel3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox CmbDollarType;
        private AxonContLib.cPanel PnlCopyPaste;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cTextBox txtCopyToYear;
        private DevExpress.XtraEditors.SimpleButton BtnCopy;
        private DevExpress.XtraEditors.SimpleButton BtnLeft;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private AxonContLib.cComboBox CmbLabourType;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cPanel panel2;
        private AxonContLib.cPanel PnlDollarLabourType;
        private AxonContLib.cPanel panel5;
        private AxonContLib.cComboBox CmbShapeType;
        private AxonContLib.cLabel lblShapeType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtShapeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtCutName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtPolName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtSymName;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbMonth;


    }
}