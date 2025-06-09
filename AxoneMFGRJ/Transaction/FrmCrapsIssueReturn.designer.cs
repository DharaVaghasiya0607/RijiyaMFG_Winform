namespace AxoneMFGRJ.Transaction
{
    partial class FrmCrapsIssueReturn
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
            this.PanelHeader = new AxonContLib.cPanel(this.components);
            this.lblSrNo = new AxonContLib.cLabel(this.components);
            this.lblKapanName = new AxonContLib.cLabel(this.components);
            this.RbtReturn = new System.Windows.Forms.RadioButton();
            this.RbtIssue = new System.Windows.Forms.RadioButton();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.lblBalanceCtsValue = new AxonContLib.cLabel(this.components);
            this.lblBalanceCarat = new AxonContLib.cLabel(this.components);
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtLossCarat = new AxonContLib.cTextBox(this.components);
            this.txtReadyCarat = new AxonContLib.cTextBox(this.components);
            this.txtSrNo = new AxonContLib.cTextBox(this.components);
            this.txtIssueCarat = new AxonContLib.cTextBox(this.components);
            this.txtKapanName = new AxonContLib.cTextBox(this.components);
            this.lblLossCarat = new AxonContLib.cLabel(this.components);
            this.lblReadyCarat = new AxonContLib.cLabel(this.components);
            this.lblIssueCarat = new AxonContLib.cLabel(this.components);
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.lblToDate = new AxonContLib.cLabel(this.components);
            this.lblFromDate = new AxonContLib.cLabel(this.components);
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.cPanel1 = new AxonContLib.cPanel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.txtMarker = new AxonContLib.cTextBox(this.components);
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            this.cPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelHeader.Controls.Add(this.lblSrNo);
            this.PanelHeader.Controls.Add(this.lblKapanName);
            this.PanelHeader.Controls.Add(this.RbtReturn);
            this.PanelHeader.Controls.Add(this.RbtIssue);
            this.PanelHeader.Controls.Add(this.cLabel1);
            this.PanelHeader.Controls.Add(this.lblBalanceCtsValue);
            this.PanelHeader.Controls.Add(this.lblBalanceCarat);
            this.PanelHeader.Controls.Add(this.BtnClear);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.txtLossCarat);
            this.PanelHeader.Controls.Add(this.txtReadyCarat);
            this.PanelHeader.Controls.Add(this.txtSrNo);
            this.PanelHeader.Controls.Add(this.txtIssueCarat);
            this.PanelHeader.Controls.Add(this.txtKapanName);
            this.PanelHeader.Controls.Add(this.lblLossCarat);
            this.PanelHeader.Controls.Add(this.lblReadyCarat);
            this.PanelHeader.Controls.Add(this.lblIssueCarat);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1162, 78);
            this.PanelHeader.TabIndex = 0;
            this.PanelHeader.TabStop = true;
            // 
            // lblSrNo
            // 
            this.lblSrNo.AutoSize = true;
            this.lblSrNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSrNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSrNo.ForeColor = System.Drawing.Color.Black;
            this.lblSrNo.Location = new System.Drawing.Point(153, 30);
            this.lblSrNo.Name = "lblSrNo";
            this.lblSrNo.Size = new System.Drawing.Size(50, 16);
            this.lblSrNo.TabIndex = 18;
            this.lblSrNo.Text = "PktNo";
            this.lblSrNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSrNo.ToolTips = "";
            // 
            // lblKapanName
            // 
            this.lblKapanName.AutoSize = true;
            this.lblKapanName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblKapanName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKapanName.ForeColor = System.Drawing.Color.Black;
            this.lblKapanName.Location = new System.Drawing.Point(18, 30);
            this.lblKapanName.Name = "lblKapanName";
            this.lblKapanName.Size = new System.Drawing.Size(31, 16);
            this.lblKapanName.TabIndex = 13;
            this.lblKapanName.Text = "Lot";
            this.lblKapanName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKapanName.ToolTips = "";
            // 
            // RbtReturn
            // 
            this.RbtReturn.AutoSize = true;
            this.RbtReturn.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.RbtReturn.Location = new System.Drawing.Point(107, 4);
            this.RbtReturn.Name = "RbtReturn";
            this.RbtReturn.Size = new System.Drawing.Size(79, 21);
            this.RbtReturn.TabIndex = 2;
            this.RbtReturn.TabStop = true;
            this.RbtReturn.Tag = "RETURN";
            this.RbtReturn.Text = "Return";
            this.RbtReturn.UseVisualStyleBackColor = true;
            this.RbtReturn.CheckedChanged += new System.EventHandler(this.RbtIssue_CheckedChanged);
            // 
            // RbtIssue
            // 
            this.RbtIssue.AutoSize = true;
            this.RbtIssue.Checked = true;
            this.RbtIssue.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.RbtIssue.Location = new System.Drawing.Point(37, 4);
            this.RbtIssue.Name = "RbtIssue";
            this.RbtIssue.Size = new System.Drawing.Size(69, 21);
            this.RbtIssue.TabIndex = 1;
            this.RbtIssue.TabStop = true;
            this.RbtIssue.Tag = "ISSUE";
            this.RbtIssue.Text = "Issue";
            this.RbtIssue.UseVisualStyleBackColor = true;
            this.RbtIssue.CheckedChanged += new System.EventHandler(this.RbtIssue_CheckedChanged);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(18, 20);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(197, 16);
            this.cLabel1.TabIndex = 25;
            this.cLabel1.Text = "---------------------------";
            this.cLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel1.ToolTips = "";
            // 
            // lblBalanceCtsValue
            // 
            this.lblBalanceCtsValue.AutoSize = true;
            this.lblBalanceCtsValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBalanceCtsValue.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceCtsValue.ForeColor = System.Drawing.Color.Green;
            this.lblBalanceCtsValue.Location = new System.Drawing.Point(344, 8);
            this.lblBalanceCtsValue.Name = "lblBalanceCtsValue";
            this.lblBalanceCtsValue.Size = new System.Drawing.Size(19, 16);
            this.lblBalanceCtsValue.TabIndex = 24;
            this.lblBalanceCtsValue.Text = "#";
            this.lblBalanceCtsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBalanceCtsValue.ToolTips = "";
            // 
            // lblBalanceCarat
            // 
            this.lblBalanceCarat.AutoSize = true;
            this.lblBalanceCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBalanceCarat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceCarat.ForeColor = System.Drawing.Color.Green;
            this.lblBalanceCarat.Location = new System.Drawing.Point(225, 8);
            this.lblBalanceCarat.Name = "lblBalanceCarat";
            this.lblBalanceCarat.Size = new System.Drawing.Size(119, 16);
            this.lblBalanceCarat.TabIndex = 23;
            this.lblBalanceCarat.Text = "Balance Carat :";
            this.lblBalanceCarat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBalanceCarat.ToolTips = "";
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseForeColor = true;
            this.BtnClear.Location = new System.Drawing.Point(591, 36);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(84, 35);
            this.BtnClear.TabIndex = 6;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "&Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.Location = new System.Drawing.Point(505, 36);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(84, 35);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnTransfer_Click);
            // 
            // txtLossCarat
            // 
            this.txtLossCarat.ActivationColor = true;
            this.txtLossCarat.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtLossCarat.AllowTabKeyOnEnter = false;
            this.txtLossCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLossCarat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLossCarat.Format = "";
            this.txtLossCarat.IsComplusory = false;
            this.txtLossCarat.Location = new System.Drawing.Point(409, 48);
            this.txtLossCarat.Name = "txtLossCarat";
            this.txtLossCarat.SelectAllTextOnFocus = true;
            this.txtLossCarat.Size = new System.Drawing.Size(90, 23);
            this.txtLossCarat.TabIndex = 4;
            this.txtLossCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLossCarat.ToolTips = "";
            this.txtLossCarat.WaterMarkText = null;
            // 
            // txtReadyCarat
            // 
            this.txtReadyCarat.ActivationColor = true;
            this.txtReadyCarat.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtReadyCarat.AllowTabKeyOnEnter = false;
            this.txtReadyCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReadyCarat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadyCarat.Format = "";
            this.txtReadyCarat.IsComplusory = false;
            this.txtReadyCarat.Location = new System.Drawing.Point(317, 48);
            this.txtReadyCarat.Name = "txtReadyCarat";
            this.txtReadyCarat.SelectAllTextOnFocus = true;
            this.txtReadyCarat.Size = new System.Drawing.Size(90, 23);
            this.txtReadyCarat.TabIndex = 3;
            this.txtReadyCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtReadyCarat.ToolTips = "";
            this.txtReadyCarat.WaterMarkText = null;
            this.txtReadyCarat.Validated += new System.EventHandler(this.txtReadyCarat_Validated);
            // 
            // txtSrNo
            // 
            this.txtSrNo.ActivationColor = true;
            this.txtSrNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSrNo.AllowTabKeyOnEnter = false;
            this.txtSrNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSrNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrNo.Format = "";
            this.txtSrNo.IsComplusory = false;
            this.txtSrNo.Location = new System.Drawing.Point(153, 48);
            this.txtSrNo.Name = "txtSrNo";
            this.txtSrNo.SelectAllTextOnFocus = true;
            this.txtSrNo.Size = new System.Drawing.Size(59, 23);
            this.txtSrNo.TabIndex = 2;
            this.txtSrNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSrNo.ToolTips = "";
            this.txtSrNo.WaterMarkText = null;
            this.txtSrNo.Validated += new System.EventHandler(this.txtSrNo_Validated);
            // 
            // txtIssueCarat
            // 
            this.txtIssueCarat.ActivationColor = true;
            this.txtIssueCarat.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtIssueCarat.AllowTabKeyOnEnter = false;
            this.txtIssueCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIssueCarat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIssueCarat.Format = "";
            this.txtIssueCarat.IsComplusory = false;
            this.txtIssueCarat.Location = new System.Drawing.Point(225, 48);
            this.txtIssueCarat.Name = "txtIssueCarat";
            this.txtIssueCarat.SelectAllTextOnFocus = true;
            this.txtIssueCarat.Size = new System.Drawing.Size(90, 23);
            this.txtIssueCarat.TabIndex = 1;
            this.txtIssueCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIssueCarat.ToolTips = "";
            this.txtIssueCarat.WaterMarkText = null;
            this.txtIssueCarat.Validated += new System.EventHandler(this.txtIssueCarat_Validated);
            // 
            // txtKapanName
            // 
            this.txtKapanName.ActivationColor = true;
            this.txtKapanName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtKapanName.AllowTabKeyOnEnter = false;
            this.txtKapanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKapanName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKapanName.Format = "";
            this.txtKapanName.IsComplusory = false;
            this.txtKapanName.Location = new System.Drawing.Point(18, 48);
            this.txtKapanName.Name = "txtKapanName";
            this.txtKapanName.SelectAllTextOnFocus = true;
            this.txtKapanName.Size = new System.Drawing.Size(133, 23);
            this.txtKapanName.TabIndex = 0;
            this.txtKapanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKapanName.ToolTips = "";
            this.txtKapanName.WaterMarkText = null;
            this.txtKapanName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKapanName_KeyPress);
            this.txtKapanName.Validated += new System.EventHandler(this.txtKapanName_Validated);
            // 
            // lblLossCarat
            // 
            this.lblLossCarat.AutoSize = true;
            this.lblLossCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblLossCarat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLossCarat.ForeColor = System.Drawing.Color.Black;
            this.lblLossCarat.Location = new System.Drawing.Point(409, 30);
            this.lblLossCarat.Name = "lblLossCarat";
            this.lblLossCarat.Size = new System.Drawing.Size(69, 16);
            this.lblLossCarat.TabIndex = 22;
            this.lblLossCarat.Text = "Loss Cts";
            this.lblLossCarat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLossCarat.ToolTips = "";
            // 
            // lblReadyCarat
            // 
            this.lblReadyCarat.AutoSize = true;
            this.lblReadyCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReadyCarat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReadyCarat.ForeColor = System.Drawing.Color.Black;
            this.lblReadyCarat.Location = new System.Drawing.Point(317, 30);
            this.lblReadyCarat.Name = "lblReadyCarat";
            this.lblReadyCarat.Size = new System.Drawing.Size(62, 16);
            this.lblReadyCarat.TabIndex = 20;
            this.lblReadyCarat.Text = "Rec Cts";
            this.lblReadyCarat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReadyCarat.ToolTips = "";
            // 
            // lblIssueCarat
            // 
            this.lblIssueCarat.AutoSize = true;
            this.lblIssueCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblIssueCarat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssueCarat.ForeColor = System.Drawing.Color.Black;
            this.lblIssueCarat.Location = new System.Drawing.Point(225, 30);
            this.lblIssueCarat.Name = "lblIssueCarat";
            this.lblIssueCarat.Size = new System.Drawing.Size(58, 16);
            this.lblIssueCarat.TabIndex = 16;
            this.lblIssueCarat.Text = "Iss Cts";
            this.lblIssueCarat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIssueCarat.ToolTips = "";
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 116);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1162, 357);
            this.MainGrid.TabIndex = 1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 8.75F, System.Drawing.FontStyle.Bold);
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
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn32,
            this.gridColumn33,
            this.gridColumn7,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn8});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDet_CellValueChanging);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = "TRN_ID";
            this.gridColumn1.FieldName = "TRN_ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "KAPAN_ID";
            this.gridColumn2.FieldName = "KAPAN_ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.Caption = "Kapan";
            this.gridColumn3.FieldName = "KAPANNAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 99;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.Caption = "PktNo";
            this.gridColumn6.FieldName = "PACKETNO";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn11.AppearanceCell.Options.UseFont = true;
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.Caption = "IssPcs";
            this.gridColumn11.FieldName = "ISSUEPCS";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 59;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn12.AppearanceCell.Options.UseFont = true;
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn12.AppearanceHeader.Options.UseFont = true;
            this.gridColumn12.Caption = "IssCts";
            this.gridColumn12.FieldName = "ISSUECARAT";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 5;
            this.gridColumn12.Width = 82;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn13.AppearanceCell.ForeColor = System.Drawing.Color.DarkGreen;
            this.gridColumn13.AppearanceCell.Options.UseFont = true;
            this.gridColumn13.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn13.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn13.AppearanceHeader.Options.UseFont = true;
            this.gridColumn13.Caption = "RdyPcs";
            this.gridColumn13.FieldName = "READYPCS";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 6;
            this.gridColumn13.Width = 58;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn14.AppearanceCell.ForeColor = System.Drawing.Color.DarkGreen;
            this.gridColumn14.AppearanceCell.Options.UseFont = true;
            this.gridColumn14.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn14.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn14.AppearanceHeader.Options.UseFont = true;
            this.gridColumn14.Caption = "RdyCts";
            this.gridColumn14.FieldName = "READYCARAT";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 7;
            this.gridColumn14.Width = 79;
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn17.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.gridColumn17.AppearanceCell.Options.UseFont = true;
            this.gridColumn17.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn17.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn17.AppearanceHeader.Options.UseFont = true;
            this.gridColumn17.Caption = "LossPcs";
            this.gridColumn17.FieldName = "LOSSPCS";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn17.Width = 59;
            // 
            // gridColumn18
            // 
            this.gridColumn18.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn18.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.gridColumn18.AppearanceCell.Options.UseFont = true;
            this.gridColumn18.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn18.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn18.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn18.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn18.AppearanceHeader.Options.UseFont = true;
            this.gridColumn18.Caption = "LossCts";
            this.gridColumn18.FieldName = "LOSSCARAT";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 8;
            // 
            // gridColumn32
            // 
            this.gridColumn32.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn32.AppearanceCell.Options.UseFont = true;
            this.gridColumn32.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn32.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn32.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn32.AppearanceHeader.Options.UseFont = true;
            this.gridColumn32.Caption = "TOPROCESS_ID";
            this.gridColumn32.FieldName = "TOPROCESS_ID";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn33
            // 
            this.gridColumn33.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn33.AppearanceCell.Options.UseFont = true;
            this.gridColumn33.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn33.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn33.AppearanceHeader.Options.UseFont = true;
            this.gridColumn33.Caption = "Process";
            this.gridColumn33.FieldName = "TOPROCESSNAME";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.OptionsColumn.AllowEdit = false;
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 9;
            this.gridColumn33.Width = 148;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.Caption = "Prc";
            this.gridColumn7.FieldName = "TYPE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 47;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.Caption = "IssueDateTime";
            this.gridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "IssueDateTime";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 10;
            this.gridColumn4.Width = 135;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.Caption = "ReturnDateTime";
            this.gridColumn5.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "ReturnDateTime";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 11;
            this.gridColumn5.Width = 135;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Marker";
            this.gridColumn8.FieldName = "MARKERNAME";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(260, 6);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(125, 23);
            this.DTPToDate.TabIndex = 8;
            this.DTPToDate.ToolTips = "";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(93, 6);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(125, 23);
            this.DTPFromDate.TabIndex = 7;
            this.DTPFromDate.ToolTips = "";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblToDate.ForeColor = System.Drawing.Color.Navy;
            this.lblToDate.Location = new System.Drawing.Point(227, 9);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(25, 16);
            this.lblToDate.TabIndex = 25;
            this.lblToDate.Text = "To";
            this.lblToDate.ToolTips = "";
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
            // btnShow
            // 
            this.btnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnShow.Appearance.Options.UseFont = true;
            this.btnShow.Appearance.Options.UseForeColor = true;
            this.btnShow.Location = new System.Drawing.Point(680, 5);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(81, 26);
            this.btnShow.TabIndex = 9;
            this.btnShow.Text = "Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // cPanel1
            // 
            this.cPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cPanel1.Controls.Add(this.cLabel2);
            this.cPanel1.Controls.Add(this.txtMarker);
            this.cPanel1.Controls.Add(this.btnShow);
            this.cPanel1.Controls.Add(this.DTPToDate);
            this.cPanel1.Controls.Add(this.DTPFromDate);
            this.cPanel1.Controls.Add(this.lblFromDate);
            this.cPanel1.Controls.Add(this.lblToDate);
            this.cPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cPanel1.Location = new System.Drawing.Point(0, 78);
            this.cPanel1.Name = "cPanel1";
            this.cPanel1.Size = new System.Drawing.Size(1162, 38);
            this.cPanel1.TabIndex = 26;
            this.cPanel1.TabStop = true;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.cLabel2.ForeColor = System.Drawing.Color.Navy;
            this.cLabel2.Location = new System.Drawing.Point(391, 10);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(58, 16);
            this.cLabel2.TabIndex = 27;
            this.cLabel2.Text = "Marker";
            this.cLabel2.ToolTips = "";
            // 
            // txtMarker
            // 
            this.txtMarker.ActivationColor = true;
            this.txtMarker.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtMarker.AllowTabKeyOnEnter = false;
            this.txtMarker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMarker.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarker.Format = "";
            this.txtMarker.IsComplusory = false;
            this.txtMarker.Location = new System.Drawing.Point(455, 6);
            this.txtMarker.Name = "txtMarker";
            this.txtMarker.SelectAllTextOnFocus = true;
            this.txtMarker.Size = new System.Drawing.Size(220, 23);
            this.txtMarker.TabIndex = 26;
            this.txtMarker.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMarker.ToolTips = "";
            this.txtMarker.WaterMarkText = null;
            this.txtMarker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMarker_KeyPress);
            // 
            // FrmCrapsIssueReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 473);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.cPanel1);
            this.Controls.Add(this.PanelHeader);
            this.Name = "FrmCrapsIssueReturn";
            this.Text = "CRAPS ISSUE RETURN";
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            this.cPanel1.ResumeLayout(false);
            this.cPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel PanelHeader;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private System.Windows.Forms.RadioButton RbtIssue;
        private System.Windows.Forms.RadioButton RbtReturn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnClear;
        private AxonContLib.cTextBox txtKapanName;
        private AxonContLib.cLabel lblKapanName;
        private AxonContLib.cTextBox txtLossCarat;
        private AxonContLib.cLabel lblLossCarat;
        private AxonContLib.cTextBox txtReadyCarat;
        private AxonContLib.cLabel lblReadyCarat;
        private AxonContLib.cTextBox txtSrNo;
        private AxonContLib.cLabel lblSrNo;
        private AxonContLib.cTextBox txtIssueCarat;
        private AxonContLib.cLabel lblIssueCarat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel lblToDate;
        private AxonContLib.cLabel lblFromDate;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private AxonContLib.cPanel cPanel1;
        private AxonContLib.cLabel lblBalanceCtsValue;
        private AxonContLib.cLabel lblBalanceCarat;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtMarker;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}