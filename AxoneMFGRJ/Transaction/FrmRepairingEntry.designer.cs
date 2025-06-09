
namespace AxoneMFGRJ.Transaction
{
    partial class FrmRepairingEntry
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
            this.CmbReturnType = new AxonContLib.cComboBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.TxtParty = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.TxtPcs = new AxonContLib.cTextBox(this.components);
            this.lblSrNo = new AxonContLib.cLabel(this.components);
            this.lblKapanName = new AxonContLib.cLabel(this.components);
            this.RbtReturn = new System.Windows.Forms.RadioButton();
            this.RbtIssue = new System.Windows.Forms.RadioButton();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
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
            this.cPanel1 = new AxonContLib.cPanel(this.components);
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.lblFromDate = new AxonContLib.cLabel(this.components);
            this.lblToDate = new AxonContLib.cLabel(this.components);
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
            this.PanelHeader.SuspendLayout();
            this.cPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelHeader.Controls.Add(this.CmbReturnType);
            this.PanelHeader.Controls.Add(this.cLabel4);
            this.PanelHeader.Controls.Add(this.cLabel3);
            this.PanelHeader.Controls.Add(this.TxtParty);
            this.PanelHeader.Controls.Add(this.cLabel2);
            this.PanelHeader.Controls.Add(this.TxtPcs);
            this.PanelHeader.Controls.Add(this.lblSrNo);
            this.PanelHeader.Controls.Add(this.lblKapanName);
            this.PanelHeader.Controls.Add(this.RbtReturn);
            this.PanelHeader.Controls.Add(this.RbtIssue);
            this.PanelHeader.Controls.Add(this.cLabel1);
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
            this.PanelHeader.Size = new System.Drawing.Size(1000, 88);
            this.PanelHeader.TabIndex = 1;
            this.PanelHeader.TabStop = true;
            // 
            // CmbReturnType
            // 
            this.CmbReturnType.AllowTabKeyOnEnter = false;
            this.CmbReturnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbReturnType.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbReturnType.ForeColor = System.Drawing.Color.Black;
            this.CmbReturnType.FormattingEnabled = true;
            this.CmbReturnType.Items.AddRange(new object[] {
            "TESTING",
            "ABC"});
            this.CmbReturnType.Location = new System.Drawing.Point(518, 8);
            this.CmbReturnType.Name = "CmbReturnType";
            this.CmbReturnType.Size = new System.Drawing.Size(136, 22);
            this.CmbReturnType.TabIndex = 1;
            this.CmbReturnType.ToolTips = "";
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(420, 11);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(95, 16);
            this.cLabel4.TabIndex = 30;
            this.cLabel4.Text = "Return Type";
            this.cLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel4.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(227, 11);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(47, 16);
            this.cLabel3.TabIndex = 29;
            this.cLabel3.Text = "Party";
            this.cLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel3.ToolTips = "";
            // 
            // TxtParty
            // 
            this.TxtParty.ActivationColor = true;
            this.TxtParty.ActivationColorCode = System.Drawing.Color.Empty;
            this.TxtParty.AllowTabKeyOnEnter = false;
            this.TxtParty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtParty.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtParty.Format = "";
            this.TxtParty.IsComplusory = false;
            this.TxtParty.Location = new System.Drawing.Point(279, 8);
            this.TxtParty.Name = "TxtParty";
            this.TxtParty.SelectAllTextOnFocus = true;
            this.TxtParty.Size = new System.Drawing.Size(133, 23);
            this.TxtParty.TabIndex = 0;
            this.TxtParty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtParty.ToolTips = "";
            this.TxtParty.WaterMarkText = null;
            this.TxtParty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtParty_KeyPress);
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(214, 39);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(33, 16);
            this.cLabel2.TabIndex = 27;
            this.cLabel2.Text = "Pcs";
            this.cLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel2.ToolTips = "";
            // 
            // TxtPcs
            // 
            this.TxtPcs.ActivationColor = true;
            this.TxtPcs.ActivationColorCode = System.Drawing.Color.Empty;
            this.TxtPcs.AllowTabKeyOnEnter = false;
            this.TxtPcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPcs.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPcs.Format = "";
            this.TxtPcs.IsComplusory = false;
            this.TxtPcs.Location = new System.Drawing.Point(214, 57);
            this.TxtPcs.Name = "TxtPcs";
            this.TxtPcs.SelectAllTextOnFocus = true;
            this.TxtPcs.Size = new System.Drawing.Size(59, 23);
            this.TxtPcs.TabIndex = 4;
            this.TxtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtPcs.ToolTips = "";
            this.TxtPcs.WaterMarkText = null;
            this.TxtPcs.Validated += new System.EventHandler(this.TxtPcs_Validated);
            // 
            // lblSrNo
            // 
            this.lblSrNo.AutoSize = true;
            this.lblSrNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSrNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSrNo.ForeColor = System.Drawing.Color.Black;
            this.lblSrNo.Location = new System.Drawing.Point(153, 39);
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
            this.lblKapanName.Location = new System.Drawing.Point(18, 39);
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
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseForeColor = true;
            this.BtnClear.Location = new System.Drawing.Point(643, 48);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(84, 35);
            this.BtnClear.TabIndex = 9;
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
            this.BtnSave.Location = new System.Drawing.Point(556, 48);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(84, 35);
            this.BtnSave.TabIndex = 8;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.txtLossCarat.Location = new System.Drawing.Point(459, 57);
            this.txtLossCarat.Name = "txtLossCarat";
            this.txtLossCarat.SelectAllTextOnFocus = true;
            this.txtLossCarat.Size = new System.Drawing.Size(90, 23);
            this.txtLossCarat.TabIndex = 7;
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
            this.txtReadyCarat.Location = new System.Drawing.Point(367, 57);
            this.txtReadyCarat.Name = "txtReadyCarat";
            this.txtReadyCarat.SelectAllTextOnFocus = true;
            this.txtReadyCarat.Size = new System.Drawing.Size(90, 23);
            this.txtReadyCarat.TabIndex = 6;
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
            this.txtSrNo.Location = new System.Drawing.Point(153, 57);
            this.txtSrNo.Name = "txtSrNo";
            this.txtSrNo.SelectAllTextOnFocus = true;
            this.txtSrNo.Size = new System.Drawing.Size(59, 23);
            this.txtSrNo.TabIndex = 3;
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
            this.txtIssueCarat.Location = new System.Drawing.Point(275, 57);
            this.txtIssueCarat.Name = "txtIssueCarat";
            this.txtIssueCarat.SelectAllTextOnFocus = true;
            this.txtIssueCarat.Size = new System.Drawing.Size(90, 23);
            this.txtIssueCarat.TabIndex = 5;
            this.txtIssueCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIssueCarat.ToolTips = "";
            this.txtIssueCarat.WaterMarkText = null;
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
            this.txtKapanName.Location = new System.Drawing.Point(18, 57);
            this.txtKapanName.Name = "txtKapanName";
            this.txtKapanName.SelectAllTextOnFocus = true;
            this.txtKapanName.Size = new System.Drawing.Size(133, 23);
            this.txtKapanName.TabIndex = 2;
            this.txtKapanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKapanName.ToolTips = "";
            this.txtKapanName.WaterMarkText = null;
            this.txtKapanName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKapanName_KeyPress);
            // 
            // lblLossCarat
            // 
            this.lblLossCarat.AutoSize = true;
            this.lblLossCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblLossCarat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLossCarat.ForeColor = System.Drawing.Color.Black;
            this.lblLossCarat.Location = new System.Drawing.Point(459, 39);
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
            this.lblReadyCarat.Location = new System.Drawing.Point(367, 39);
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
            this.lblIssueCarat.Location = new System.Drawing.Point(275, 39);
            this.lblIssueCarat.Name = "lblIssueCarat";
            this.lblIssueCarat.Size = new System.Drawing.Size(58, 16);
            this.lblIssueCarat.TabIndex = 16;
            this.lblIssueCarat.Text = "Iss Cts";
            this.lblIssueCarat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIssueCarat.ToolTips = "";
            // 
            // cPanel1
            // 
            this.cPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cPanel1.Controls.Add(this.btnShow);
            this.cPanel1.Controls.Add(this.DTPToDate);
            this.cPanel1.Controls.Add(this.DTPFromDate);
            this.cPanel1.Controls.Add(this.lblFromDate);
            this.cPanel1.Controls.Add(this.lblToDate);
            this.cPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cPanel1.Location = new System.Drawing.Point(0, 88);
            this.cPanel1.Name = "cPanel1";
            this.cPanel1.Size = new System.Drawing.Size(1000, 38);
            this.cPanel1.TabIndex = 27;
            this.cPanel1.TabStop = true;
            // 
            // btnShow
            // 
            this.btnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnShow.Appearance.Options.UseFont = true;
            this.btnShow.Appearance.Options.UseForeColor = true;
            this.btnShow.Location = new System.Drawing.Point(394, 5);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(81, 26);
            this.btnShow.TabIndex = 12;
            this.btnShow.Text = "Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
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
            this.DTPToDate.TabIndex = 11;
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
            this.DTPFromDate.TabIndex = 10;
            this.DTPFromDate.ToolTips = "";
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
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 126);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1000, 389);
            this.MainGrid.TabIndex = 28;
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
            this.gridColumn5});
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
            this.gridColumn11.VisibleIndex = 3;
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
            this.gridColumn12.VisibleIndex = 4;
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
            this.gridColumn13.VisibleIndex = 5;
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
            this.gridColumn14.VisibleIndex = 6;
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
            this.gridColumn18.VisibleIndex = 7;
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
            this.gridColumn33.VisibleIndex = 8;
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
            this.gridColumn4.VisibleIndex = 9;
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
            this.gridColumn5.VisibleIndex = 10;
            this.gridColumn5.Width = 135;
            // 
            // FrmRepairingEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 515);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.cPanel1);
            this.Controls.Add(this.PanelHeader);
            this.Name = "FrmRepairingEntry";
            this.Text = "REPAIRING ENTRY";
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.cPanel1.ResumeLayout(false);
            this.cPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel PanelHeader;
        private AxonContLib.cLabel lblSrNo;
        private AxonContLib.cLabel lblKapanName;
        private System.Windows.Forms.RadioButton RbtReturn;
        private System.Windows.Forms.RadioButton RbtIssue;
        private AxonContLib.cLabel cLabel1;
        private DevExpress.XtraEditors.SimpleButton BtnClear;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private AxonContLib.cTextBox txtLossCarat;
        private AxonContLib.cTextBox txtReadyCarat;
        private AxonContLib.cTextBox txtSrNo;
        private AxonContLib.cTextBox txtIssueCarat;
        private AxonContLib.cTextBox txtKapanName;
        private AxonContLib.cLabel lblLossCarat;
        private AxonContLib.cLabel lblReadyCarat;
        private AxonContLib.cLabel lblIssueCarat;
        private AxonContLib.cPanel cPanel1;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel lblFromDate;
        private AxonContLib.cLabel lblToDate;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox TxtPcs;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cTextBox TxtParty;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cComboBox CmbReturnType;
    }
}