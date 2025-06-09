namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmSingleMemoAnalysisOther
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.BtnDirectPDFExport = new DevExpress.XtraEditors.SimpleButton();
            this.txtKapanName = new AxonContLib.cTextBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtPurchaseRate = new AxonContLib.cTextBox(this.components);
            this.txtSaleRate = new AxonContLib.cTextBox(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.txtExtraRate = new AxonContLib.cTextBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.txtOnOutRate = new AxonContLib.cTextBox(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.txtMajuri = new AxonContLib.cTextBox(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.majuriRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainGrdMajuri = new DevExpress.XtraGrid.GridControl();
            this.GrdMajuri = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn62 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtSize = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn63 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtFAmt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtTAmt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpttxtRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.lblMajuriHeader = new AxonContLib.cLabel(this.components);
            this.lblMasParamete = new AxonContLib.cLabel(this.components);
            this.PnlRateDetail = new System.Windows.Forms.GroupBox();
            this.txtPassWord = new AxonContLib.cTextBox(this.components);
            this.cLabel15 = new AxonContLib.cLabel(this.components);
            this.PanelProgress = new DevExpress.XtraWaitForm.ProgressPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.CmbPrdType = new AxonContLib.cComboBox(this.components);
            this.BtnReCall = new DevExpress.XtraEditors.SimpleButton();
            this.lblDetailSaveMessage = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdMajuri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdMajuri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtFAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtTAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpttxtRate)).BeginInit();
            this.PnlRateDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSearch.Location = new System.Drawing.Point(400, 35);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(124, 32);
            this.BtnSearch.TabIndex = 12;
            this.BtnSearch.Text = "Generate Report";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(682, 35);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 32);
            this.BtnExit.TabIndex = 14;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseForeColor = true;
            serializableAppearanceObject1.Options.UseTextOptions = true;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(27, 38);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(47, 13);
            this.cLabel2.TabIndex = 0;
            this.cLabel2.Text = "Kapan";
            this.cLabel2.ToolTips = "";
            // 
            // BtnDirectPDFExport
            // 
            this.BtnDirectPDFExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDirectPDFExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDirectPDFExport.Appearance.Options.UseFont = true;
            this.BtnDirectPDFExport.Appearance.Options.UseForeColor = true;
            this.BtnDirectPDFExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDirectPDFExport.Location = new System.Drawing.Point(527, 35);
            this.BtnDirectPDFExport.Name = "BtnDirectPDFExport";
            this.BtnDirectPDFExport.Size = new System.Drawing.Size(152, 32);
            this.BtnDirectPDFExport.TabIndex = 13;
            this.BtnDirectPDFExport.Text = "Direct .Pdf Export";
            this.BtnDirectPDFExport.Click += new System.EventHandler(this.BtnDirectPDFExport_Click);
            // 
            // txtKapanName
            // 
            this.txtKapanName.ActivationColor = true;
            this.txtKapanName.AllowTabKeyOnEnter = true;
            this.txtKapanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKapanName.ComplusoryMsg = null;
            this.txtKapanName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKapanName.Format = "";
            this.txtKapanName.IsComplusory = false;
            this.txtKapanName.Location = new System.Drawing.Point(82, 35);
            this.txtKapanName.Name = "txtKapanName";
            this.txtKapanName.RequiredChars = "";
            this.txtKapanName.SelectAllTextOnFocus = true;
            this.txtKapanName.ShowToolTipOnFocus = false;
            this.txtKapanName.Size = new System.Drawing.Size(297, 22);
            this.txtKapanName.TabIndex = 1;
            this.txtKapanName.ToolTips = "";
            this.txtKapanName.WaterMarkText = null;
            this.txtKapanName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKapanName_KeyPress);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(7, 43);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(114, 13);
            this.cLabel1.TabIndex = 8;
            this.cLabel1.Text = "Purchase Rate :-";
            this.cLabel1.ToolTips = "";
            // 
            // txtPurchaseRate
            // 
            this.txtPurchaseRate.ActivationColor = false;
            this.txtPurchaseRate.AllowTabKeyOnEnter = true;
            this.txtPurchaseRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseRate.ComplusoryMsg = null;
            this.txtPurchaseRate.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtPurchaseRate.Format = "######0.00";
            this.txtPurchaseRate.IsComplusory = false;
            this.txtPurchaseRate.Location = new System.Drawing.Point(121, 41);
            this.txtPurchaseRate.Name = "txtPurchaseRate";
            this.txtPurchaseRate.RequiredChars = "0123456789.";
            this.txtPurchaseRate.SelectAllTextOnFocus = true;
            this.txtPurchaseRate.ShowToolTipOnFocus = false;
            this.txtPurchaseRate.Size = new System.Drawing.Size(65, 20);
            this.txtPurchaseRate.TabIndex = 9;
            this.txtPurchaseRate.Text = "0";
            this.txtPurchaseRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPurchaseRate.ToolTips = "";
            this.txtPurchaseRate.WaterMarkText = null;
            this.txtPurchaseRate.Validating += new System.ComponentModel.CancelEventHandler(this.txtPurchaseRate_Validating);
            // 
            // txtSaleRate
            // 
            this.txtSaleRate.ActivationColor = false;
            this.txtSaleRate.AllowTabKeyOnEnter = true;
            this.txtSaleRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaleRate.ComplusoryMsg = null;
            this.txtSaleRate.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtSaleRate.Format = "######0.00";
            this.txtSaleRate.IsComplusory = false;
            this.txtSaleRate.Location = new System.Drawing.Point(121, 19);
            this.txtSaleRate.Name = "txtSaleRate";
            this.txtSaleRate.RequiredChars = "0123456789.";
            this.txtSaleRate.SelectAllTextOnFocus = true;
            this.txtSaleRate.ShowToolTipOnFocus = false;
            this.txtSaleRate.Size = new System.Drawing.Size(65, 20);
            this.txtSaleRate.TabIndex = 5;
            this.txtSaleRate.Text = "0";
            this.txtSaleRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSaleRate.ToolTips = "";
            this.txtSaleRate.WaterMarkText = null;
            this.txtSaleRate.Validating += new System.ComponentModel.CancelEventHandler(this.txtSaleRate_Validating);
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(7, 21);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(82, 13);
            this.cLabel3.TabIndex = 4;
            this.cLabel3.Text = "Sale Rate :-";
            this.cLabel3.ToolTips = "";
            // 
            // txtExtraRate
            // 
            this.txtExtraRate.ActivationColor = false;
            this.txtExtraRate.AllowTabKeyOnEnter = true;
            this.txtExtraRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExtraRate.ComplusoryMsg = null;
            this.txtExtraRate.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtExtraRate.Format = "######0.00";
            this.txtExtraRate.IsComplusory = false;
            this.txtExtraRate.Location = new System.Drawing.Point(294, 17);
            this.txtExtraRate.Name = "txtExtraRate";
            this.txtExtraRate.RequiredChars = "0123456789.";
            this.txtExtraRate.SelectAllTextOnFocus = true;
            this.txtExtraRate.ShowToolTipOnFocus = false;
            this.txtExtraRate.Size = new System.Drawing.Size(65, 20);
            this.txtExtraRate.TabIndex = 7;
            this.txtExtraRate.Text = "0";
            this.txtExtraRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExtraRate.ToolTips = "";
            this.txtExtraRate.WaterMarkText = null;
            this.txtExtraRate.Validating += new System.ComponentModel.CancelEventHandler(this.txtExtraRate_Validating);
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(201, 19);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(89, 13);
            this.cLabel4.TabIndex = 6;
            this.cLabel4.Text = "Extra Rate :-";
            this.cLabel4.ToolTips = "";
            // 
            // txtOnOutRate
            // 
            this.txtOnOutRate.ActivationColor = false;
            this.txtOnOutRate.AllowTabKeyOnEnter = true;
            this.txtOnOutRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOnOutRate.ComplusoryMsg = null;
            this.txtOnOutRate.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtOnOutRate.Format = "######0.00";
            this.txtOnOutRate.IsComplusory = false;
            this.txtOnOutRate.Location = new System.Drawing.Point(294, 39);
            this.txtOnOutRate.Name = "txtOnOutRate";
            this.txtOnOutRate.RequiredChars = "0123456789.";
            this.txtOnOutRate.SelectAllTextOnFocus = true;
            this.txtOnOutRate.ShowToolTipOnFocus = false;
            this.txtOnOutRate.Size = new System.Drawing.Size(65, 20);
            this.txtOnOutRate.TabIndex = 11;
            this.txtOnOutRate.Text = "0";
            this.txtOnOutRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOnOutRate.ToolTips = "";
            this.txtOnOutRate.WaterMarkText = null;
            this.txtOnOutRate.Validating += new System.ComponentModel.CancelEventHandler(this.txtOnOutRate_Validating);
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(201, 41);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(93, 13);
            this.cLabel5.TabIndex = 10;
            this.cLabel5.Text = "OnOut Rate :-";
            this.cLabel5.ToolTips = "";
            // 
            // txtMajuri
            // 
            this.txtMajuri.ActivationColor = false;
            this.txtMajuri.AllowTabKeyOnEnter = true;
            this.txtMajuri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMajuri.ComplusoryMsg = null;
            this.txtMajuri.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtMajuri.Format = "######0.00";
            this.txtMajuri.IsComplusory = false;
            this.txtMajuri.Location = new System.Drawing.Point(633, 84);
            this.txtMajuri.Name = "txtMajuri";
            this.txtMajuri.RequiredChars = "0123456789.";
            this.txtMajuri.SelectAllTextOnFocus = true;
            this.txtMajuri.ShowToolTipOnFocus = false;
            this.txtMajuri.Size = new System.Drawing.Size(65, 20);
            this.txtMajuri.TabIndex = 3;
            this.txtMajuri.Text = "0";
            this.txtMajuri.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMajuri.ToolTips = "";
            this.txtMajuri.Visible = false;
            this.txtMajuri.WaterMarkText = null;
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(565, 86);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(62, 13);
            this.cLabel6.TabIndex = 2;
            this.cLabel6.Text = "Majuri :-";
            this.cLabel6.ToolTips = "";
            this.cLabel6.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.majuriRateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 26);
            // 
            // majuriRateToolStripMenuItem
            // 
            this.majuriRateToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.majuriRateToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.majuriRateToolStripMenuItem.Name = "majuriRateToolStripMenuItem";
            this.majuriRateToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.majuriRateToolStripMenuItem.Text = "Delete Record";
            this.majuriRateToolStripMenuItem.Click += new System.EventHandler(this.majuriRateToolStripMenuItem_Click);
            // 
            // MainGrdMajuri
            // 
            this.MainGrdMajuri.ContextMenuStrip = this.contextMenuStrip1;
            this.MainGrdMajuri.Location = new System.Drawing.Point(30, 204);
            this.MainGrdMajuri.MainView = this.GrdMajuri;
            this.MainGrdMajuri.Name = "MainGrdMajuri";
            this.MainGrdMajuri.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpttxtRate,
            this.repTxtSize,
            this.reptxtFAmt,
            this.reptxtTAmt});
            this.MainGrdMajuri.Size = new System.Drawing.Size(533, 298);
            this.MainGrdMajuri.TabIndex = 0;
            this.MainGrdMajuri.TabStop = false;
            this.MainGrdMajuri.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdMajuri});
            // 
            // GrdMajuri
            // 
            this.GrdMajuri.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdMajuri.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdMajuri.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GrdMajuri.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdMajuri.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GrdMajuri.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdMajuri.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdMajuri.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdMajuri.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdMajuri.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdMajuri.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdMajuri.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdMajuri.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdMajuri.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdMajuri.Appearance.GroupRow.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdMajuri.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.GrdMajuri.Appearance.GroupRow.Options.UseFont = true;
            this.GrdMajuri.Appearance.GroupRow.Options.UseForeColor = true;
            this.GrdMajuri.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdMajuri.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdMajuri.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdMajuri.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdMajuri.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.GrdMajuri.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.GrdMajuri.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdMajuri.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdMajuri.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdMajuri.Appearance.Row.Options.UseFont = true;
            this.GrdMajuri.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GrdMajuri.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GrdMajuri.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdMajuri.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdMajuri.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdMajuri.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdMajuri.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdMajuri.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdMajuri.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdMajuri.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdMajuri.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdMajuri.ColumnPanelRowHeight = 25;
            this.GrdMajuri.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn62,
            this.gridColumn48,
            this.gridColumn63,
            this.gridColumn50,
            this.gridColumn1});
            this.GrdMajuri.FooterPanelHeight = 20;
            this.GrdMajuri.GridControl = this.MainGrdMajuri;
            this.GrdMajuri.Name = "GrdMajuri";
            this.GrdMajuri.OptionsFilter.AllowFilterEditor = false;
            this.GrdMajuri.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdMajuri.OptionsPrint.ExpandAllGroups = false;
            this.GrdMajuri.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdMajuri.OptionsSelection.MultiSelect = true;
            this.GrdMajuri.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdMajuri.OptionsView.ColumnAutoWidth = false;
            this.GrdMajuri.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdMajuri.OptionsView.ShowAutoFilterRow = true;
            this.GrdMajuri.OptionsView.ShowFooter = true;
            this.GrdMajuri.OptionsView.ShowGroupPanel = false;
            this.GrdMajuri.RowHeight = 23;
            this.GrdMajuri.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdMajuri_CellValueChanged);
            // 
            // gridColumn62
            // 
            this.gridColumn62.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn62.AppearanceCell.Options.UseFont = true;
            this.gridColumn62.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn62.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn62.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn62.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn62.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn62.Caption = "MAJURI_ID";
            this.gridColumn62.FieldName = "MAJURI_ID";
            this.gridColumn62.Name = "gridColumn62";
            this.gridColumn62.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn48
            // 
            this.gridColumn48.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn48.AppearanceCell.Options.UseFont = true;
            this.gridColumn48.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn48.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn48.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn48.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn48.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn48.Caption = "Size";
            this.gridColumn48.ColumnEdit = this.repTxtSize;
            this.gridColumn48.FieldName = "SIZENAME";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.OptionsColumn.AllowEdit = false;
            this.gridColumn48.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn48.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 2;
            this.gridColumn48.Width = 200;
            // 
            // repTxtSize
            // 
            this.repTxtSize.AutoHeight = false;
            this.repTxtSize.Name = "repTxtSize";
            this.repTxtSize.Validating += new System.ComponentModel.CancelEventHandler(this.repTxtSize_Validating);
            // 
            // gridColumn63
            // 
            this.gridColumn63.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn63.AppearanceCell.Options.UseFont = true;
            this.gridColumn63.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn63.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn63.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn63.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn63.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn63.Caption = "FAmt";
            this.gridColumn63.ColumnEdit = this.reptxtFAmt;
            this.gridColumn63.FieldName = "FROMAMOUNT";
            this.gridColumn63.Name = "gridColumn63";
            this.gridColumn63.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn63.Visible = true;
            this.gridColumn63.VisibleIndex = 0;
            this.gridColumn63.Width = 110;
            // 
            // reptxtFAmt
            // 
            this.reptxtFAmt.AutoHeight = false;
            this.reptxtFAmt.Name = "reptxtFAmt";
            this.reptxtFAmt.Validating += new System.ComponentModel.CancelEventHandler(this.reptxtFAmt_Validating);
            // 
            // gridColumn50
            // 
            this.gridColumn50.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn50.AppearanceCell.Options.UseFont = true;
            this.gridColumn50.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn50.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn50.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn50.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn50.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn50.Caption = "TAmt";
            this.gridColumn50.ColumnEdit = this.reptxtTAmt;
            this.gridColumn50.FieldName = "TOAMOUNT";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn50.Visible = true;
            this.gridColumn50.VisibleIndex = 1;
            this.gridColumn50.Width = 110;
            // 
            // reptxtTAmt
            // 
            this.reptxtTAmt.AutoHeight = false;
            this.reptxtTAmt.Name = "reptxtTAmt";
            this.reptxtTAmt.Validating += new System.ComponentModel.CancelEventHandler(this.reptxtTAmt_Validating);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Rate";
            this.gridColumn1.ColumnEdit = this.rpttxtRate;
            this.gridColumn1.FieldName = "RATE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            // 
            // rpttxtRate
            // 
            this.rpttxtRate.AutoHeight = false;
            this.rpttxtRate.Name = "rpttxtRate";
            // 
            // lblMajuriHeader
            // 
            this.lblMajuriHeader.BackColor = System.Drawing.Color.Black;
            this.lblMajuriHeader.Font = new System.Drawing.Font("Verdana", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblMajuriHeader.ForeColor = System.Drawing.Color.White;
            this.lblMajuriHeader.Location = new System.Drawing.Point(30, 183);
            this.lblMajuriHeader.Name = "lblMajuriHeader";
            this.lblMajuriHeader.Size = new System.Drawing.Size(533, 19);
            this.lblMajuriHeader.TabIndex = 16;
            this.lblMajuriHeader.Text = "---: Majuri :---";
            this.lblMajuriHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMajuriHeader.ToolTips = "";
            // 
            // lblMasParamete
            // 
            this.lblMasParamete.AutoSize = true;
            this.lblMasParamete.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMasParamete.ForeColor = System.Drawing.Color.Black;
            this.lblMasParamete.Location = new System.Drawing.Point(34, 503);
            this.lblMasParamete.Name = "lblMasParamete";
            this.lblMasParamete.Size = new System.Drawing.Size(63, 13);
            this.lblMasParamete.TabIndex = 17;
            this.lblMasParamete.Text = "Message";
            this.lblMasParamete.ToolTips = "";
            // 
            // PnlRateDetail
            // 
            this.PnlRateDetail.Controls.Add(this.txtOnOutRate);
            this.PnlRateDetail.Controls.Add(this.cLabel1);
            this.PnlRateDetail.Controls.Add(this.txtPurchaseRate);
            this.PnlRateDetail.Controls.Add(this.cLabel3);
            this.PnlRateDetail.Controls.Add(this.txtSaleRate);
            this.PnlRateDetail.Controls.Add(this.cLabel5);
            this.PnlRateDetail.Controls.Add(this.cLabel4);
            this.PnlRateDetail.Controls.Add(this.txtExtraRate);
            this.PnlRateDetail.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlRateDetail.Location = new System.Drawing.Point(31, 113);
            this.PnlRateDetail.Name = "PnlRateDetail";
            this.PnlRateDetail.Size = new System.Drawing.Size(363, 67);
            this.PnlRateDetail.TabIndex = 51;
            this.PnlRateDetail.TabStop = false;
            this.PnlRateDetail.Text = "Rate Detail";
            // 
            // txtPassWord
            // 
            this.txtPassWord.ActivationColor = false;
            this.txtPassWord.AllowTabKeyOnEnter = false;
            this.txtPassWord.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPassWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassWord.ComplusoryMsg = null;
            this.txtPassWord.Format = "";
            this.txtPassWord.IsComplusory = false;
            this.txtPassWord.Location = new System.Drawing.Point(402, 17);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.RequiredChars = "";
            this.txtPassWord.SelectAllTextOnFocus = true;
            this.txtPassWord.ShowToolTipOnFocus = false;
            this.txtPassWord.Size = new System.Drawing.Size(45, 14);
            this.txtPassWord.TabIndex = 157;
            this.txtPassWord.TabStop = false;
            this.txtPassWord.Tag = "AXONE";
            this.txtPassWord.ToolTips = "";
            this.txtPassWord.WaterMarkText = null;
            this.txtPassWord.TextChanged += new System.EventHandler(this.txtPassWord_TextChanged);
            // 
            // cLabel15
            // 
            this.cLabel15.AutoSize = true;
            this.cLabel15.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel15.ForeColor = System.Drawing.Color.Black;
            this.cLabel15.Location = new System.Drawing.Point(29, 65);
            this.cLabel15.Name = "cLabel15";
            this.cLabel15.Size = new System.Drawing.Size(61, 13);
            this.cLabel15.TabIndex = 158;
            this.cLabel15.Text = "PrdType";
            this.cLabel15.ToolTips = "";
            // 
            // PanelProgress
            // 
            this.PanelProgress.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.PanelProgress.Appearance.Options.UseBackColor = true;
            this.PanelProgress.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 12F);
            this.PanelProgress.AppearanceCaption.Options.UseFont = true;
            this.PanelProgress.AppearanceDescription.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.PanelProgress.AppearanceDescription.Options.UseFont = true;
            this.PanelProgress.Location = new System.Drawing.Point(449, 68);
            this.PanelProgress.LookAndFeel.UseDefaultLookAndFeel = false;
            this.PanelProgress.Name = "PanelProgress";
            this.PanelProgress.ShowCaption = false;
            this.PanelProgress.ShowDescription = false;
            this.PanelProgress.Size = new System.Drawing.Size(37, 41);
            this.PanelProgress.TabIndex = 201;
            this.PanelProgress.Text = "progressPanel1";
            this.PanelProgress.TextHorzOffset = 10;
            this.PanelProgress.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // CmbPrdType
            // 
            this.CmbPrdType.AllowTabKeyOnEnter = false;
            this.CmbPrdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPrdType.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbPrdType.ForeColor = System.Drawing.Color.Black;
            this.CmbPrdType.FormattingEnabled = true;
            this.CmbPrdType.Items.AddRange(new object[] {
            "FINAL MAKABLE PREDICTION",
            "GRADING",
            "MUMBAI GRADING",
            "LAB GRADING"});
            this.CmbPrdType.Location = new System.Drawing.Point(95, 61);
            this.CmbPrdType.Name = "CmbPrdType";
            this.CmbPrdType.Size = new System.Drawing.Size(284, 22);
            this.CmbPrdType.TabIndex = 202;
            this.CmbPrdType.ToolTips = "";
            // 
            // BtnReCall
            // 
            this.BtnReCall.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnReCall.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnReCall.Appearance.Options.UseFont = true;
            this.BtnReCall.Appearance.Options.UseForeColor = true;
            this.BtnReCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnReCall.Location = new System.Drawing.Point(760, 35);
            this.BtnReCall.Name = "BtnReCall";
            this.BtnReCall.Size = new System.Drawing.Size(75, 32);
            this.BtnReCall.TabIndex = 203;
            this.BtnReCall.TabStop = false;
            this.BtnReCall.Text = "R&eCall";
            this.BtnReCall.Click += new System.EventHandler(this.BtnReCall_Click);
            // 
            // lblDetailSaveMessage
            // 
            this.lblDetailSaveMessage.AutoSize = true;
            this.lblDetailSaveMessage.Font = new System.Drawing.Font("Verdana", 7F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailSaveMessage.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblDetailSaveMessage.Location = new System.Drawing.Point(96, 87);
            this.lblDetailSaveMessage.Name = "lblDetailSaveMessage";
            this.lblDetailSaveMessage.Size = new System.Drawing.Size(31, 12);
            this.lblDetailSaveMessage.TabIndex = 204;
            this.lblDetailSaveMessage.Text = "Date";
            this.lblDetailSaveMessage.ToolTips = "";
            // 
            // FrmSingleMemoAnalysisOther
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 674);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.lblDetailSaveMessage);
            this.Controls.Add(this.BtnReCall);
            this.Controls.Add(this.CmbPrdType);
            this.Controls.Add(this.PanelProgress);
            this.Controls.Add(this.cLabel15);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.PnlRateDetail);
            this.Controls.Add(this.lblMasParamete);
            this.Controls.Add(this.lblMajuriHeader);
            this.Controls.Add(this.MainGrdMajuri);
            this.Controls.Add(this.txtMajuri);
            this.Controls.Add(this.cLabel6);
            this.Controls.Add(this.txtKapanName);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.cLabel2);
            this.Controls.Add(this.BtnDirectPDFExport);
            this.Controls.Add(this.BtnSearch);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmSingleMemoAnalysisOther";
            this.Text = "SINGLE MEMO ANALYSIS REPORT1";
            this.Load += new System.EventHandler(this.FrmSingleMemoAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdMajuri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdMajuri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtFAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtTAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpttxtRate)).EndInit();
            this.PnlRateDetail.ResumeLayout(false);
            this.PnlRateDetail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraEditors.SimpleButton BtnDirectPDFExport;
        private AxonContLib.cTextBox txtKapanName;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtPurchaseRate;
        private AxonContLib.cTextBox txtSaleRate;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cTextBox txtExtraRate;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cTextBox txtOnOutRate;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cTextBox txtMajuri;
        private AxonContLib.cLabel cLabel6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem majuriRateToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl MainGrdMajuri;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdMajuri;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn62;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtSize;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn63;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtFAmt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtTAmt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpttxtRate;
        private AxonContLib.cLabel lblMajuriHeader;
        private AxonContLib.cLabel lblMasParamete;
        private System.Windows.Forms.GroupBox PnlRateDetail;
        private AxonContLib.cTextBox txtPassWord;
        private AxonContLib.cLabel cLabel15;
        private DevExpress.XtraWaitForm.ProgressPanel PanelProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private AxonContLib.cComboBox CmbPrdType;
        private DevExpress.XtraEditors.SimpleButton BtnReCall;
        private AxonContLib.cLabel lblDetailSaveMessage;
    }
}