
namespace AxoneMFGRJ.Transaction
{
    partial class FrmManualEntryLiveStock
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
            this.cPanel1 = new AxonContLib.cPanel(this.components);
            this.TxtEmployee = new AxonContLib.cTextBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.TxtToPkt = new AxonContLib.cTextBox(this.components);
            this.txtFromPacketNo = new AxonContLib.cTextBox(this.components);
            this.txtKapan = new AxonContLib.cTextBox(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.lblFromDate = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.lblToDate = new AxonContLib.cLabel(this.components);
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtPartyName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtKapan = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtEmployee = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtEmployeeCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtCarat = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.cPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtPartyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtKapan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtEmployeeCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtCarat)).BeginInit();
            this.SuspendLayout();
            // 
            // cPanel1
            // 
            this.cPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cPanel1.Controls.Add(this.TxtEmployee);
            this.cPanel1.Controls.Add(this.cLabel4);
            this.cPanel1.Controls.Add(this.TxtToPkt);
            this.cPanel1.Controls.Add(this.txtFromPacketNo);
            this.cPanel1.Controls.Add(this.txtKapan);
            this.cPanel1.Controls.Add(this.DTPToDate);
            this.cPanel1.Controls.Add(this.DTPFromDate);
            this.cPanel1.Controls.Add(this.BtnExport);
            this.cPanel1.Controls.Add(this.BtnExit);
            this.cPanel1.Controls.Add(this.BtnClear);
            this.cPanel1.Controls.Add(this.btnShow);
            this.cPanel1.Controls.Add(this.cLabel2);
            this.cPanel1.Controls.Add(this.cLabel1);
            this.cPanel1.Controls.Add(this.lblFromDate);
            this.cPanel1.Controls.Add(this.cLabel3);
            this.cPanel1.Controls.Add(this.lblToDate);
            this.cPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cPanel1.Location = new System.Drawing.Point(0, 0);
            this.cPanel1.Name = "cPanel1";
            this.cPanel1.Size = new System.Drawing.Size(810, 67);
            this.cPanel1.TabIndex = 27;
            this.cPanel1.TabStop = true;
            // 
            // TxtEmployee
            // 
            this.TxtEmployee.ActivationColor = true;
            this.TxtEmployee.ActivationColorCode = System.Drawing.Color.Empty;
            this.TxtEmployee.AllowTabKeyOnEnter = false;
            this.TxtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmployee.Font = new System.Drawing.Font("Verdana", 9F);
            this.TxtEmployee.Format = "";
            this.TxtEmployee.IsComplusory = false;
            this.TxtEmployee.Location = new System.Drawing.Point(477, 6);
            this.TxtEmployee.Name = "TxtEmployee";
            this.TxtEmployee.SelectAllTextOnFocus = true;
            this.TxtEmployee.Size = new System.Drawing.Size(220, 22);
            this.TxtEmployee.TabIndex = 2;
            this.TxtEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtEmployee.ToolTips = "";
            this.TxtEmployee.WaterMarkText = null;
            this.TxtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtEmployee_KeyPress);
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.cLabel4.ForeColor = System.Drawing.Color.Navy;
            this.cLabel4.Location = new System.Drawing.Point(394, 10);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(80, 16);
            this.cLabel4.TabIndex = 31;
            this.cLabel4.Text = "Employee";
            this.cLabel4.ToolTips = "";
            // 
            // TxtToPkt
            // 
            this.TxtToPkt.ActivationColor = true;
            this.TxtToPkt.ActivationColorCode = System.Drawing.Color.Empty;
            this.TxtToPkt.AllowTabKeyOnEnter = false;
            this.TxtToPkt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtToPkt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtToPkt.Format = "######";
            this.TxtToPkt.IsComplusory = false;
            this.TxtToPkt.Location = new System.Drawing.Point(330, 38);
            this.TxtToPkt.Name = "TxtToPkt";
            this.TxtToPkt.SelectAllTextOnFocus = true;
            this.TxtToPkt.Size = new System.Drawing.Size(73, 22);
            this.TxtToPkt.TabIndex = 5;
            this.TxtToPkt.Text = "0";
            this.TxtToPkt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtToPkt.ToolTips = "";
            this.TxtToPkt.WaterMarkText = null;
            // 
            // txtFromPacketNo
            // 
            this.txtFromPacketNo.ActivationColor = true;
            this.txtFromPacketNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtFromPacketNo.AllowTabKeyOnEnter = false;
            this.txtFromPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromPacketNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromPacketNo.Format = "######";
            this.txtFromPacketNo.IsComplusory = false;
            this.txtFromPacketNo.Location = new System.Drawing.Point(229, 38);
            this.txtFromPacketNo.Name = "txtFromPacketNo";
            this.txtFromPacketNo.SelectAllTextOnFocus = true;
            this.txtFromPacketNo.Size = new System.Drawing.Size(73, 22);
            this.txtFromPacketNo.TabIndex = 4;
            this.txtFromPacketNo.Text = "0";
            this.txtFromPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFromPacketNo.ToolTips = "";
            this.txtFromPacketNo.WaterMarkText = null;
            // 
            // txtKapan
            // 
            this.txtKapan.ActivationColor = true;
            this.txtKapan.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtKapan.AllowTabKeyOnEnter = false;
            this.txtKapan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKapan.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKapan.Format = "";
            this.txtKapan.IsComplusory = false;
            this.txtKapan.Location = new System.Drawing.Point(96, 38);
            this.txtKapan.Name = "txtKapan";
            this.txtKapan.SelectAllTextOnFocus = true;
            this.txtKapan.Size = new System.Drawing.Size(82, 22);
            this.txtKapan.TabIndex = 3;
            this.txtKapan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKapan.ToolTips = "";
            this.txtKapan.WaterMarkText = null;
            this.txtKapan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKapan_KeyPress);
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(258, 6);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.ShowCheckBox = true;
            this.DTPToDate.Size = new System.Drawing.Size(129, 24);
            this.DTPToDate.TabIndex = 1;
            this.DTPToDate.ToolTips = "";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(96, 6);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.ShowCheckBox = true;
            this.DTPFromDate.Size = new System.Drawing.Size(129, 24);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ToolTips = "";
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Location = new System.Drawing.Point(503, 36);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(81, 26);
            this.BtnExport.TabIndex = 8;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Location = new System.Drawing.Point(677, 36);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(81, 26);
            this.BtnExit.TabIndex = 8;
            this.BtnExit.Text = "Exit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseForeColor = true;
            this.BtnClear.Location = new System.Drawing.Point(590, 36);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(81, 26);
            this.BtnClear.TabIndex = 7;
            this.BtnClear.Text = "Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnShow
            // 
            this.btnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnShow.Appearance.Options.UseFont = true;
            this.btnShow.Appearance.Options.UseForeColor = true;
            this.btnShow.Location = new System.Drawing.Point(416, 36);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(81, 26);
            this.btnShow.TabIndex = 6;
            this.btnShow.Text = "Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.cLabel2.ForeColor = System.Drawing.Color.Navy;
            this.cLabel2.Location = new System.Drawing.Point(179, 41);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(50, 16);
            this.cLabel2.TabIndex = 23;
            this.cLabel2.Text = "PktNo";
            this.cLabel2.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.cLabel1.ForeColor = System.Drawing.Color.Navy;
            this.cLabel1.Location = new System.Drawing.Point(40, 41);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(53, 16);
            this.cLabel1.TabIndex = 23;
            this.cLabel1.Text = "Kapan";
            this.cLabel1.ToolTips = "";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblFromDate.ForeColor = System.Drawing.Color.Navy;
            this.lblFromDate.Location = new System.Drawing.Point(9, 10);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(84, 16);
            this.lblFromDate.TabIndex = 23;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.cLabel3.ForeColor = System.Drawing.Color.Navy;
            this.cLabel3.Location = new System.Drawing.Point(304, 41);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(25, 16);
            this.cLabel3.TabIndex = 25;
            this.cLabel3.Text = "To";
            this.cLabel3.ToolTips = "";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblToDate.ForeColor = System.Drawing.Color.Navy;
            this.lblToDate.Location = new System.Drawing.Point(230, 10);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(25, 16);
            this.lblToDate.TabIndex = 25;
            this.lblToDate.Text = "To";
            this.lblToDate.ToolTips = "";
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 67);
            this.MainGrid.MainView = this.GrdData;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTxtPartyName,
            this.repTxtEmployee,
            this.repTxtKapan,
            this.repTxtEmployeeCode,
            this.repTxtCarat});
            this.MainGrid.Size = new System.Drawing.Size(810, 327);
            this.MainGrid.TabIndex = 28;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdData});
            // 
            // GrdData
            // 
            this.GrdData.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdData.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdData.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdData.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 8.75F, System.Drawing.FontStyle.Bold);
            this.GrdData.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdData.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdData.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdData.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdData.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdData.Appearance.Row.Options.UseFont = true;
            this.GrdData.Appearance.Row.Options.UseTextOptions = true;
            this.GrdData.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdData.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdData.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdData.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdData.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdData.ColumnPanelRowHeight = 25;
            this.GrdData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn17,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22});
            this.GrdData.GridControl = this.MainGrid;
            this.GrdData.Name = "GrdData";
            this.GrdData.OptionsBehavior.Editable = false;
            this.GrdData.OptionsCustomization.AllowSort = false;
            this.GrdData.OptionsFilter.AllowFilterEditor = false;
            this.GrdData.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdData.OptionsPrint.ExpandAllGroups = false;
            this.GrdData.OptionsSelection.MultiSelect = true;
            this.GrdData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdData.OptionsView.ColumnAutoWidth = false;
            this.GrdData.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdData.OptionsView.ShowAutoFilterRow = true;
            this.GrdData.OptionsView.ShowFooter = true;
            this.GrdData.OptionsView.ShowGroupPanel = false;
            this.GrdData.RowHeight = 25;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Party Name";
            this.gridColumn1.ColumnEdit = this.repTxtPartyName;
            this.gridColumn1.FieldName = "PARTYNAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 121;
            // 
            // repTxtPartyName
            // 
            this.repTxtPartyName.AutoHeight = false;
            this.repTxtPartyName.Name = "repTxtPartyName";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Kapan";
            this.gridColumn2.ColumnEdit = this.repTxtKapan;
            this.gridColumn2.FieldName = "KAPANNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 84;
            // 
            // repTxtKapan
            // 
            this.repTxtKapan.AutoHeight = false;
            this.repTxtKapan.Name = "repTxtKapan";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Emp Name";
            this.gridColumn3.ColumnEdit = this.repTxtEmployee;
            this.gridColumn3.FieldName = "TOEMPLOYEENAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Width = 143;
            // 
            // repTxtEmployee
            // 
            this.repTxtEmployee.AutoHeight = false;
            this.repTxtEmployee.Name = "repTxtEmployee";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Iss Pcs";
            this.gridColumn4.FieldName = "ISSUEPCS";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 8;
            this.gridColumn4.Width = 59;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Iss Carat";
            this.gridColumn5.FieldName = "ISSUECARAT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 9;
            this.gridColumn5.Width = 72;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Party_ID";
            this.gridColumn6.FieldName = "PARTY_ID";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ToEmployee_ID";
            this.gridColumn7.FieldName = "TOEMPLOYEE_ID";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Emp Code";
            this.gridColumn11.ColumnEdit = this.repTxtEmployeeCode;
            this.gridColumn11.FieldName = "TOEMPLOYEECODE";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 106;
            // 
            // repTxtEmployeeCode
            // 
            this.repTxtEmployeeCode.AutoHeight = false;
            this.repTxtEmployeeCode.Name = "repTxtEmployeeCode";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Kapan_ID";
            this.gridColumn12.FieldName = "KAPAN_ID";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "ToDepartment_ID";
            this.gridColumn13.FieldName = "TODEPARTMENT_ID";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "ToManager_ID";
            this.gridColumn14.FieldName = "TOMANAGER_ID";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Party Code";
            this.gridColumn17.FieldName = "PARTYCODE";
            this.gridColumn17.Name = "gridColumn17";
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Dept";
            this.gridColumn23.FieldName = "TODEPARTMENTNAME";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 6;
            this.gridColumn23.Width = 102;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "Mangr";
            this.gridColumn24.FieldName = "TOMANAGERNAME";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.OptionsColumn.AllowEdit = false;
            this.gridColumn24.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 7;
            this.gridColumn24.Width = 105;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Pkt No";
            this.gridColumn8.FieldName = "PACKETNO";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Rdy Pcs";
            this.gridColumn9.FieldName = "READYPCS";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 10;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Rdy Carat";
            this.gridColumn10.FieldName = "READYCARAT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 11;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Loss Cts";
            this.gridColumn15.FieldName = "LOSSCARAT";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn15.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 12;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Process";
            this.gridColumn16.FieldName = "PROCESS";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 13;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Iss DateTime";
            this.gridColumn18.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn18.FieldName = "ISSUEDATETIME";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 14;
            this.gridColumn18.Width = 102;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Ret DateTime";
            this.gridColumn19.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
            this.gridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn19.FieldName = "RETURNDATETIME";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 15;
            this.gridColumn19.Width = 115;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "PartyCode";
            this.gridColumn20.FieldName = "PARTYCODE";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 2;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Emp.Name";
            this.gridColumn21.FieldName = "TOEMPLOYEENAME";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 5;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "EntryType";
            this.gridColumn22.FieldName = "ENTRYTYPE";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 16;
            // 
            // repTxtCarat
            // 
            this.repTxtCarat.AutoHeight = false;
            this.repTxtCarat.Name = "repTxtCarat";
            // 
            // FrmManualEntryLiveStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 394);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.cPanel1);
            this.Name = "FrmManualEntryLiveStock";
            this.Text = "MANUAL ENTRY LIVE STOCK";
            this.cPanel1.ResumeLayout(false);
            this.cPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtPartyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtKapan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtEmployeeCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtCarat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel cPanel1;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private AxonContLib.cLabel lblFromDate;
        private AxonContLib.cLabel lblToDate;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtKapan;
        private AxonContLib.cTextBox TxtToPkt;
        private AxonContLib.cTextBox txtFromPacketNo;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cTextBox TxtEmployee;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtPartyName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtKapan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtCarat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtEmployeeCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.SimpleButton BtnClear;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
    }
}