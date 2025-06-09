namespace AxoneMFGRJ.Account
{
    partial class FrmLedgerReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLedgerReport));
            this.xtraScrollableControl2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new AxonContLib.cLabel(this.components);
            this.txtRefLedger = new AxonContLib.cTextBox();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PanelClient = new AxonContLib.cPanel();
            this.ChkCashBank = new AxonContLib.cCheckBox(this.components);
            this.BtnBestFit = new System.Windows.Forms.Button();
            this.ChkLanguage = new AxonContLib.cCheckBox(this.components);
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnShow = new System.Windows.Forms.Button();
            this.BtnExcelExport = new System.Windows.Forms.Button();
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.txtAccount = new AxonContLib.cTextBox();
            this.labelControl1 = new AxonContLib.cLabel(this.components);
            this.labelControl3 = new AxonContLib.cLabel(this.components);
            this.lblReceiveDate = new AxonContLib.cLabel(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xtraScrollableControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.PanelClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraScrollableControl2
            // 
            this.xtraScrollableControl2.Controls.Add(this.groupControl1);
            this.xtraScrollableControl2.Controls.Add(this.PanelClient);
            this.xtraScrollableControl2.Controls.Add(this.label3);
            this.xtraScrollableControl2.Controls.Add(this.label4);
            this.xtraScrollableControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl2.Name = "xtraScrollableControl2";
            this.xtraScrollableControl2.Size = new System.Drawing.Size(1008, 412);
            this.xtraScrollableControl2.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtRefLedger);
            this.groupControl1.Controls.Add(this.MainGrid);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 88);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1008, 324);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Ledger Report";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.labelControl2.ForeColor = System.Drawing.Color.Navy;
            this.labelControl2.Location = new System.Drawing.Point(417, 158);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 16);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Ref Acct";
            this.labelControl2.ToolTips = "";
            this.labelControl2.Visible = false;
            // 
            // txtRefLedger
            // 
            this.txtRefLedger.ActivationColor = false;
            this.txtRefLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRefLedger.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtRefLedger.Format = "";
            this.txtRefLedger.Location = new System.Drawing.Point(482, 154);
            this.txtRefLedger.Name = "txtRefLedger";
            this.txtRefLedger.RequiredChars = "";
            this.txtRefLedger.Size = new System.Drawing.Size(255, 23);
            this.txtRefLedger.TabIndex = 7;
            this.txtRefLedger.ToolTips = "";
            this.txtRefLedger.Visible = false;
            this.txtRefLedger.WaterMarkText = null;
            this.txtRefLedger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRefLedger_KeyPress);
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(2, 22);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1004, 300);
            this.MainGrid.TabIndex = 0;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 11F);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.bandedGridColumn1,
            this.bandedGridColumn4,
            this.bandedGridColumn5,
            this.bandedGridColumn6,
            this.bandedGridColumn7,
            this.bandedGridColumn8,
            this.bandedGridColumn9,
            this.bandedGridColumn10,
            this.bandedGridColumn13,
            this.bandedGridColumn12,
            this.gridColumn1,
            this.gridColumn2});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn1.Caption = "Trn ID";
            this.bandedGridColumn1.FieldName = "TRN_ID";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn4.Caption = "Book";
            this.bandedGridColumn4.FieldName = "BOOKTYPEFULL";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.VisibleIndex = 2;
            this.bandedGridColumn4.Width = 146;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn5.Caption = "Date";
            this.bandedGridColumn5.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt";
            this.bandedGridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.bandedGridColumn5.FieldName = "VOUCHERDATE";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.VisibleIndex = 1;
            this.bandedGridColumn5.Width = 122;
            // 
            // bandedGridColumn6
            // 
            this.bandedGridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn6.Caption = "Sr.";
            this.bandedGridColumn6.FieldName = "VOUCHERSTR";
            this.bandedGridColumn6.Name = "bandedGridColumn6";
            this.bandedGridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn6.Visible = true;
            this.bandedGridColumn6.VisibleIndex = 0;
            this.bandedGridColumn6.Width = 71;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn7.Caption = "Ledger";
            this.bandedGridColumn7.FieldName = "LEDGERNAME";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn8.Caption = "Account";
            this.bandedGridColumn8.FieldName = "REFLEDGERNAME";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.VisibleIndex = 3;
            this.bandedGridColumn8.Width = 185;
            // 
            // bandedGridColumn9
            // 
            this.bandedGridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.bandedGridColumn9.Caption = "Credit";
            this.bandedGridColumn9.FieldName = "CREDITAMOUNTLOCAL";
            this.bandedGridColumn9.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.bandedGridColumn9.Name = "bandedGridColumn9";
            this.bandedGridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn9.Visible = true;
            this.bandedGridColumn9.VisibleIndex = 7;
            this.bandedGridColumn9.Width = 136;
            // 
            // bandedGridColumn10
            // 
            this.bandedGridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.bandedGridColumn10.Caption = "Debit";
            this.bandedGridColumn10.FieldName = "DEBITAMOUNTLOCAL";
            this.bandedGridColumn10.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.bandedGridColumn10.Name = "bandedGridColumn10";
            this.bandedGridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn10.Visible = true;
            this.bandedGridColumn10.VisibleIndex = 8;
            this.bandedGridColumn10.Width = 131;
            // 
            // bandedGridColumn13
            // 
            this.bandedGridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.bandedGridColumn13.Caption = "Running";
            this.bandedGridColumn13.FieldName = "RUNNINGBALANCE";
            this.bandedGridColumn13.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.bandedGridColumn13.Name = "bandedGridColumn13";
            this.bandedGridColumn13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn13.Visible = true;
            this.bandedGridColumn13.VisibleIndex = 9;
            this.bandedGridColumn13.Width = 117;
            // 
            // bandedGridColumn12
            // 
            this.bandedGridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.bandedGridColumn12.Caption = "Note";
            this.bandedGridColumn12.FieldName = "NOTE";
            this.bandedGridColumn12.Name = "bandedGridColumn12";
            this.bandedGridColumn12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn12.Visible = true;
            this.bandedGridColumn12.VisibleIndex = 5;
            this.bandedGridColumn12.Width = 161;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ref Doc";
            this.gridColumn1.FieldName = "REFDOC";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 120;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Trn Type";
            this.gridColumn2.FieldName = "TRNTYPE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 154;
            // 
            // PanelClient
            // 
            this.PanelClient.Controls.Add(this.ChkCashBank);
            this.PanelClient.Controls.Add(this.BtnBestFit);
            this.PanelClient.Controls.Add(this.ChkLanguage);
            this.PanelClient.Controls.Add(this.BtnExit);
            this.PanelClient.Controls.Add(this.BtnPrint);
            this.PanelClient.Controls.Add(this.BtnShow);
            this.PanelClient.Controls.Add(this.BtnExcelExport);
            this.PanelClient.Controls.Add(this.DTPToDate);
            this.PanelClient.Controls.Add(this.DTPFromDate);
            this.PanelClient.Controls.Add(this.txtAccount);
            this.PanelClient.Controls.Add(this.labelControl1);
            this.PanelClient.Controls.Add(this.labelControl3);
            this.PanelClient.Controls.Add(this.lblReceiveDate);
            this.PanelClient.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelClient.Location = new System.Drawing.Point(0, 0);
            this.PanelClient.Name = "PanelClient";
            this.PanelClient.Size = new System.Drawing.Size(1008, 88);
            this.PanelClient.TabIndex = 0;
            // 
            // ChkCashBank
            // 
            this.ChkCashBank.AutoSize = true;
            this.ChkCashBank.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ChkCashBank.Location = new System.Drawing.Point(412, 52);
            this.ChkCashBank.Name = "ChkCashBank";
            this.ChkCashBank.Size = new System.Drawing.Size(109, 20);
            this.ChkCashBank.TabIndex = 45;
            this.ChkCashBank.Text = "Cash + Bank";
            this.ChkCashBank.ToolTips = "";
            this.ChkCashBank.UseVisualStyleBackColor = true;
            this.ChkCashBank.CheckedChanged += new System.EventHandler(this.ChkCashBank_CheckedChanged);
            // 
            // BtnBestFit
            // 
            this.BtnBestFit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnBestFit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBestFit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBestFit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBestFit.ForeColor = System.Drawing.Color.White;
            this.BtnBestFit.Location = new System.Drawing.Point(784, 48);
            this.BtnBestFit.Name = "BtnBestFit";
            this.BtnBestFit.Size = new System.Drawing.Size(119, 34);
            this.BtnBestFit.TabIndex = 44;
            this.BtnBestFit.TabStop = false;
            this.BtnBestFit.Text = "Grid Auto Fit ";
            this.BtnBestFit.UseVisualStyleBackColor = false;
            this.BtnBestFit.Click += new System.EventHandler(this.BtnBestFit_Click);
            // 
            // ChkLanguage
            // 
            this.ChkLanguage.AutoSize = true;
            this.ChkLanguage.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ChkLanguage.Location = new System.Drawing.Point(841, 15);
            this.ChkLanguage.Name = "ChkLanguage";
            this.ChkLanguage.Size = new System.Drawing.Size(138, 20);
            this.ChkLanguage.TabIndex = 8;
            this.ChkLanguage.Text = "Convert To ગુજરાતી";
            this.ChkLanguage.ToolTips = "";
            this.ChkLanguage.UseVisualStyleBackColor = true;
            this.ChkLanguage.CheckedChanged += new System.EventHandler(this.ChkLanguage_CheckedChanged);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(54)))), ((int)(((byte)(16)))));
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Location = new System.Drawing.Point(906, 48);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(73, 34);
            this.BtnExit.TabIndex = 43;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(632, 48);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(73, 34);
            this.BtnPrint.TabIndex = 42;
            this.BtnPrint.TabStop = false;
            this.BtnPrint.Text = "&GPrint";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.BtnShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnShow.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.ForeColor = System.Drawing.Color.White;
            this.BtnShow.Location = new System.Drawing.Point(557, 48);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(73, 34);
            this.BtnShow.TabIndex = 9;
            this.BtnShow.Text = "&Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnExcelExport
            // 
            this.BtnExcelExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.BtnExcelExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExcelExport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExcelExport.ForeColor = System.Drawing.Color.White;
            this.BtnExcelExport.Location = new System.Drawing.Point(707, 48);
            this.BtnExcelExport.Name = "BtnExcelExport";
            this.BtnExcelExport.Size = new System.Drawing.Size(73, 34);
            this.BtnExcelExport.TabIndex = 41;
            this.BtnExcelExport.TabStop = false;
            this.BtnExcelExport.Text = "&Excel";
            this.BtnExcelExport.UseVisualStyleBackColor = false;
            this.BtnExcelExport.Click += new System.EventHandler(this.BtnExcelExport_Click);
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(210, 14);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(129, 23);
            this.DTPToDate.TabIndex = 3;
            this.DTPToDate.ToolTips = "";
            this.DTPToDate.Value = new System.DateTime(2016, 6, 4, 19, 23, 5, 0);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(54, 14);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(129, 23);
            this.DTPFromDate.TabIndex = 1;
            this.DTPFromDate.ToolTips = "";
            this.DTPFromDate.Value = new System.DateTime(2016, 6, 4, 19, 23, 5, 0);
            // 
            // txtAccount
            // 
            this.txtAccount.ActivationColor = false;
            this.txtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccount.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtAccount.Format = "";
            this.txtAccount.Location = new System.Drawing.Point(412, 14);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.RequiredChars = "";
            this.txtAccount.Size = new System.Drawing.Size(407, 23);
            this.txtAccount.TabIndex = 5;
            this.txtAccount.ToolTips = "";
            this.txtAccount.WaterMarkText = null;
            this.txtAccount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccount_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.labelControl1.ForeColor = System.Drawing.Color.Navy;
            this.labelControl1.Location = new System.Drawing.Point(186, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(25, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "To";
            this.labelControl1.ToolTips = "";
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSize = true;
            this.labelControl3.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.labelControl3.ForeColor = System.Drawing.Color.Navy;
            this.labelControl3.Location = new System.Drawing.Point(346, 17);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(63, 16);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Account";
            this.labelControl3.ToolTips = "";
            // 
            // lblReceiveDate
            // 
            this.lblReceiveDate.AutoSize = true;
            this.lblReceiveDate.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblReceiveDate.ForeColor = System.Drawing.Color.Navy;
            this.lblReceiveDate.Location = new System.Drawing.Point(12, 17);
            this.lblReceiveDate.Name = "lblReceiveDate";
            this.lblReceiveDate.Size = new System.Drawing.Size(40, 16);
            this.lblReceiveDate.TabIndex = 0;
            this.lblReceiveDate.Text = "From";
            this.lblReceiveDate.ToolTips = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(-12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(-12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "*";
            // 
            // FrmLedgerReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.xtraScrollableControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLedgerReport";
            this.Text = "LEDGER REPORT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLedgerReport_FormClosing);
            this.xtraScrollableControl2.ResumeLayout(false);
            this.xtraScrollableControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.PanelClient.ResumeLayout(false);
            this.PanelClient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private AxonContLib.cPanel PanelClient;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private AxonContLib.cLabel labelControl1;
        private AxonContLib.cLabel lblReceiveDate;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.Button BtnExcelExport;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn12;
        private System.Windows.Forms.Button BtnBestFit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private AxonContLib.cCheckBox ChkLanguage;
        private AxonContLib.cTextBox txtRefLedger;
        private AxonContLib.cTextBox txtAccount;
        private AxonContLib.cLabel labelControl2;
        private AxonContLib.cLabel labelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private AxonContLib.cCheckBox ChkCashBank;

    }
}