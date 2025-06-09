namespace AxoneMFGRJ.Transaction
{
    partial class FrmRoughPurchaseRolling
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.BtnImportUpdate = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rptxtCts = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BtnCreateKapan = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.BtnRejection = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel4 = new AxonContLib.cPanel();
            this.ChkDisplayAllLot = new DevExpress.XtraEditors.CheckEdit();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAutoFit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new AxonContLib.cPanel();
            ((System.ComponentModel.ISupportInitialize)(this.BtnImportUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptxtCts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCreateKapan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkDisplayAllLot.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnImportUpdate
            // 
            this.BtnImportUpdate.AutoHeight = false;
            this.BtnImportUpdate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)});
            this.BtnImportUpdate.Name = "BtnImportUpdate";
            this.BtnImportUpdate.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // rptxtCts
            // 
            this.rptxtCts.AutoHeight = false;
            this.rptxtCts.Mask.EditMask = "########.###";
            this.rptxtCts.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rptxtCts.Name = "rptxtCts";
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 0);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rptxtCts,
            this.BtnCreateKapan,
            this.BtnRejection,
            this.BtnImportUpdate,
            this.BtnRejectionTransfer});
            this.MainGrid.Size = new System.Drawing.Size(984, 423);
            this.MainGrid.TabIndex = 2;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn15,
            this.bandedGridColumn14,
            this.bandedGridColumn16,
            this.bandedGridColumn17,
            this.bandedGridColumn28,
            this.bandedGridColumn31,
            this.bandedGridColumn32,
            this.bandedGridColumn33,
            this.bandedGridColumn7,
            this.bandedGridColumn22,
            this.bandedGridColumn1,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.Caption = "Carat";
            this.gridColumn15.DisplayFormat.FormatString = "{0:N4}";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "CARAT";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 1;
            this.gridColumn15.Width = 90;
            // 
            // bandedGridColumn14
            // 
            this.bandedGridColumn14.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn14.AppearanceCell.ForeColor = System.Drawing.Color.Navy;
            this.bandedGridColumn14.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn14.AppearanceCell.Options.UseForeColor = true;
            this.bandedGridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn14.Caption = "Pur Rate";
            this.bandedGridColumn14.DisplayFormat.FormatString = "{0:N2}";
            this.bandedGridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn14.FieldName = "RATE";
            this.bandedGridColumn14.Name = "bandedGridColumn14";
            this.bandedGridColumn14.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn14.Visible = true;
            this.bandedGridColumn14.VisibleIndex = 10;
            // 
            // bandedGridColumn16
            // 
            this.bandedGridColumn16.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn16.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn16.Caption = "Rough Cost";
            this.bandedGridColumn16.DisplayFormat.FormatString = "{0:N2}";
            this.bandedGridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn16.FieldName = "ROUGHCOST";
            this.bandedGridColumn16.Name = "bandedGridColumn16";
            this.bandedGridColumn16.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn16.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn16.Visible = true;
            this.bandedGridColumn16.VisibleIndex = 11;
            // 
            // bandedGridColumn17
            // 
            this.bandedGridColumn17.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn17.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn17.Caption = "Net Amt";
            this.bandedGridColumn17.DisplayFormat.FormatString = "{0:N2}";
            this.bandedGridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn17.FieldName = "NETAMOUNT";
            this.bandedGridColumn17.Name = "bandedGridColumn17";
            this.bandedGridColumn17.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn17.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn17.Visible = true;
            this.bandedGridColumn17.VisibleIndex = 12;
            // 
            // bandedGridColumn28
            // 
            this.bandedGridColumn28.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn28.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn28.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn28.Caption = "Main R Size";
            this.bandedGridColumn28.FieldName = "MSIZENAME";
            this.bandedGridColumn28.Name = "bandedGridColumn28";
            this.bandedGridColumn28.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn28.Visible = true;
            this.bandedGridColumn28.VisibleIndex = 8;
            this.bandedGridColumn28.Width = 120;
            // 
            // bandedGridColumn31
            // 
            this.bandedGridColumn31.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn31.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn31.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn31.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn31.Caption = "Pcs";
            this.bandedGridColumn31.FieldName = "PCS";
            this.bandedGridColumn31.Name = "bandedGridColumn31";
            this.bandedGridColumn31.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn31.Visible = true;
            this.bandedGridColumn31.VisibleIndex = 2;
            // 
            // bandedGridColumn32
            // 
            this.bandedGridColumn32.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn32.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn32.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn32.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn32.Caption = "Size Avg";
            this.bandedGridColumn32.FieldName = "SIZEAVG";
            this.bandedGridColumn32.Name = "bandedGridColumn32";
            this.bandedGridColumn32.Visible = true;
            this.bandedGridColumn32.VisibleIndex = 3;
            // 
            // bandedGridColumn33
            // 
            this.bandedGridColumn33.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn33.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn33.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn33.Caption = "Rough Type";
            this.bandedGridColumn33.FieldName = "ROUGHTYPE";
            this.bandedGridColumn33.Name = "bandedGridColumn33";
            this.bandedGridColumn33.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn33.Visible = true;
            this.bandedGridColumn33.VisibleIndex = 4;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn7.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn7.Caption = "Due";
            this.bandedGridColumn7.FieldName = "DUEDAYS";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.Visible = true;
            this.bandedGridColumn7.VisibleIndex = 13;
            // 
            // bandedGridColumn22
            // 
            this.bandedGridColumn22.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn22.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn22.Caption = "Date";
            this.bandedGridColumn22.FieldName = "ROUGHSTATUSDATE";
            this.bandedGridColumn22.Name = "bandedGridColumn22";
            this.bandedGridColumn22.Visible = true;
            this.bandedGridColumn22.VisibleIndex = 0;
            this.bandedGridColumn22.Width = 102;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.bandedGridColumn1.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn1.Caption = "R Mines";
            this.bandedGridColumn1.FieldName = "ROUGHMINES";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.VisibleIndex = 5;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "R Name";
            this.gridColumn1.FieldName = "ARTICLENAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Company";
            this.gridColumn2.FieldName = "COMPANYNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 9;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.Caption = " ";
            this.gridColumn3.FieldName = "STATUS";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Kapan";
            this.gridColumn4.FieldName = "KAPANNAME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 7;
            // 
            // BtnCreateKapan
            // 
            this.BtnCreateKapan.Appearance.ForeColor = System.Drawing.Color.Green;
            this.BtnCreateKapan.Appearance.Options.UseForeColor = true;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject1.ForeColor = System.Drawing.Color.Green;
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseForeColor = true;
            serializableAppearanceObject1.Options.UseTextOptions = true;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnCreateKapan.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Kapan Create", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.BtnCreateKapan.Name = "BtnCreateKapan";
            this.BtnCreateKapan.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.BtnCreateKapan.Click += new System.EventHandler(this.BtnCreateKapan_Click);
            // 
            // BtnRejection
            // 
            this.BtnRejection.AutoHeight = false;
            serializableAppearanceObject2.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject2.Options.UseFont = true;
            this.BtnRejection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rejection", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.BtnRejection.Name = "BtnRejection";
            this.BtnRejection.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject3.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject3.Options.UseFont = true;
            serializableAppearanceObject3.Options.UseForeColor = true;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej x\'Fer", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.BtnRejectionTransfer.Click += new System.EventHandler(this.BtnRejectionTransfer_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ChkDisplayAllLot);
            this.panel4.Controls.Add(this.BtnPrint);
            this.panel4.Controls.Add(this.BtnSearch);
            this.panel4.Controls.Add(this.BtnAutoFit);
            this.panel4.Controls.Add(this.BtnExit);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(984, 50);
            this.panel4.TabIndex = 0;
            // 
            // ChkDisplayAllLot
            // 
            this.ChkDisplayAllLot.Location = new System.Drawing.Point(367, 18);
            this.ChkDisplayAllLot.Name = "ChkDisplayAllLot";
            this.ChkDisplayAllLot.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDisplayAllLot.Properties.Appearance.Options.UseFont = true;
            this.ChkDisplayAllLot.Properties.Caption = "Display Lots With Balance 0";
            this.ChkDisplayAllLot.Size = new System.Drawing.Size(210, 20);
            this.ChkDisplayAllLot.TabIndex = 153;
            this.ChkDisplayAllLot.TabStop = false;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.Appearance.Options.UseForeColor = true;
            this.BtnPrint.Location = new System.Drawing.Point(181, 9);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(80, 35);
            this.BtnPrint.TabIndex = 152;
            this.BtnPrint.TabStop = false;
            this.BtnPrint.Text = "Export";
            this.BtnPrint.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Location = new System.Drawing.Point(9, 9);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(80, 35);
            this.BtnSearch.TabIndex = 149;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnAutoFit
            // 
            this.BtnAutoFit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAutoFit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAutoFit.Appearance.Options.UseFont = true;
            this.BtnAutoFit.Appearance.Options.UseForeColor = true;
            this.BtnAutoFit.Location = new System.Drawing.Point(95, 9);
            this.BtnAutoFit.Name = "BtnAutoFit";
            this.BtnAutoFit.Size = new System.Drawing.Size(80, 35);
            this.BtnAutoFit.TabIndex = 150;
            this.BtnAutoFit.TabStop = false;
            this.BtnAutoFit.Text = "Best Fit";
            this.BtnAutoFit.Click += new System.EventHandler(this.BtnAutoFit_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Location = new System.Drawing.Point(267, 9);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 35);
            this.BtnExit.TabIndex = 151;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MainGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 423);
            this.panel2.TabIndex = 19;
            // 
            // FrmRoughPurchaseRolling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Name = "FrmRoughPurchaseRolling";
            this.Text = "ROUGH PURCHASE ROLLING";
            this.Load += new System.EventHandler(this.FrmPurchaseLiveStock_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPurchaseLiveStock_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.BtnImportUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptxtCts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCreateKapan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChkDisplayAllLot.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel panel4;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnImportUpdate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnCreateKapan;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejection;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rptxtCts;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private DevExpress.XtraEditors.SimpleButton BtnAutoFit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.CheckEdit ChkDisplayAllLot;
    }
}