namespace AxoneMFGRJ.Utility
{
    partial class FrmKapanTransfer
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
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtSourceServer = new AxonContLib.cTextBox(this.components);
            this.txtSourceDBName = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.txtSourceUsername = new AxonContLib.cTextBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.txtSourcePassword = new AxonContLib.cTextBox(this.components);
            this.GrpSource = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.txtDestinationServer = new AxonContLib.cTextBox(this.components);
            this.cLabel7 = new AxonContLib.cLabel(this.components);
            this.txtDestinationDBName = new AxonContLib.cTextBox(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.txtDestinationUsername = new AxonContLib.cTextBox(this.components);
            this.txtDestinationPassword = new AxonContLib.cTextBox(this.components);
            this.bgWorkerMaster = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.LblSalesDetail = new AxonContLib.cLabel(this.components);
            this.xtraTabActivity = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.MainGrdTransaction = new DevExpress.XtraGrid.GridControl();
            this.GrdDetTransaction = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.progressPanelTransactionDelete = new DevExpress.XtraWaitForm.ProgressPanel();
            this.lblDeleteTransaction = new AxonContLib.cLabel(this.components);
            this.BtnDeleteTransaction = new AxonContLib.cButton(this.components);
            this.BtnTransferTransaction = new AxonContLib.cButton(this.components);
            this.progressPanelTransaction = new DevExpress.XtraWaitForm.ProgressPanel();
            this.lblMessageTransaction = new AxonContLib.cLabel(this.components);
            this.cLabel11 = new AxonContLib.cLabel(this.components);
            this.bgWorkerTransaction = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerPricing = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerTransactionDelete = new System.ComponentModel.BackgroundWorker();
            this.GrpSource.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabActivity)).BeginInit();
            this.xtraTabActivity.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetTransaction)).BeginInit();
            this.SuspendLayout();
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(17, 22);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(55, 13);
            this.cLabel1.TabIndex = 184;
            this.cLabel1.Text = "SERVER";
            this.cLabel1.ToolTips = "";
            // 
            // txtSourceServer
            // 
            this.txtSourceServer.ActivationColor = false;
            this.txtSourceServer.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSourceServer.AllowTabKeyOnEnter = false;
            this.txtSourceServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourceServer.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSourceServer.Format = "";
            this.txtSourceServer.IsComplusory = false;
            this.txtSourceServer.Location = new System.Drawing.Point(103, 17);
            this.txtSourceServer.Name = "txtSourceServer";
            this.txtSourceServer.SelectAllTextOnFocus = true;
            this.txtSourceServer.Size = new System.Drawing.Size(235, 22);
            this.txtSourceServer.TabIndex = 185;
            this.txtSourceServer.Text = "150.107.188.120,1433";
            this.txtSourceServer.ToolTips = "";
            this.txtSourceServer.WaterMarkText = null;
            // 
            // txtSourceDBName
            // 
            this.txtSourceDBName.ActivationColor = false;
            this.txtSourceDBName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSourceDBName.AllowTabKeyOnEnter = false;
            this.txtSourceDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourceDBName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSourceDBName.Format = "";
            this.txtSourceDBName.IsComplusory = false;
            this.txtSourceDBName.Location = new System.Drawing.Point(103, 44);
            this.txtSourceDBName.Name = "txtSourceDBName";
            this.txtSourceDBName.SelectAllTextOnFocus = true;
            this.txtSourceDBName.Size = new System.Drawing.Size(235, 22);
            this.txtSourceDBName.TabIndex = 185;
            this.txtSourceDBName.Text = "AxoneDiamMFGSingleMix";
            this.txtSourceDBName.ToolTips = "";
            this.txtSourceDBName.WaterMarkText = null;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(17, 49);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(64, 13);
            this.cLabel2.TabIndex = 184;
            this.cLabel2.Text = "DB NAME";
            this.cLabel2.ToolTips = "";
            // 
            // txtSourceUsername
            // 
            this.txtSourceUsername.ActivationColor = false;
            this.txtSourceUsername.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSourceUsername.AllowTabKeyOnEnter = false;
            this.txtSourceUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourceUsername.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSourceUsername.Format = "";
            this.txtSourceUsername.IsComplusory = false;
            this.txtSourceUsername.Location = new System.Drawing.Point(103, 72);
            this.txtSourceUsername.Name = "txtSourceUsername";
            this.txtSourceUsername.SelectAllTextOnFocus = true;
            this.txtSourceUsername.Size = new System.Drawing.Size(235, 22);
            this.txtSourceUsername.TabIndex = 185;
            this.txtSourceUsername.Text = "SA";
            this.txtSourceUsername.ToolTips = "";
            this.txtSourceUsername.WaterMarkText = null;
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(17, 77);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(76, 13);
            this.cLabel4.TabIndex = 184;
            this.cLabel4.Text = "USERNAME";
            this.cLabel4.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(17, 103);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(78, 13);
            this.cLabel5.TabIndex = 184;
            this.cLabel5.Text = "PASSWORD";
            this.cLabel5.ToolTips = "";
            // 
            // txtSourcePassword
            // 
            this.txtSourcePassword.ActivationColor = false;
            this.txtSourcePassword.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSourcePassword.AllowTabKeyOnEnter = false;
            this.txtSourcePassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourcePassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSourcePassword.Format = "";
            this.txtSourcePassword.IsComplusory = false;
            this.txtSourcePassword.Location = new System.Drawing.Point(103, 98);
            this.txtSourcePassword.Name = "txtSourcePassword";
            this.txtSourcePassword.PasswordChar = '*';
            this.txtSourcePassword.SelectAllTextOnFocus = true;
            this.txtSourcePassword.Size = new System.Drawing.Size(235, 22);
            this.txtSourcePassword.TabIndex = 185;
            this.txtSourcePassword.Text = "SRD@9039824700038";
            this.txtSourcePassword.ToolTips = "";
            this.txtSourcePassword.WaterMarkText = null;
            // 
            // GrpSource
            // 
            this.GrpSource.Controls.Add(this.cLabel1);
            this.GrpSource.Controls.Add(this.cLabel5);
            this.GrpSource.Controls.Add(this.txtSourceServer);
            this.GrpSource.Controls.Add(this.cLabel4);
            this.GrpSource.Controls.Add(this.txtSourceDBName);
            this.GrpSource.Controls.Add(this.cLabel2);
            this.GrpSource.Controls.Add(this.txtSourceUsername);
            this.GrpSource.Controls.Add(this.txtSourcePassword);
            this.GrpSource.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpSource.Location = new System.Drawing.Point(23, 59);
            this.GrpSource.Name = "GrpSource";
            this.GrpSource.Size = new System.Drawing.Size(346, 128);
            this.GrpSource.TabIndex = 186;
            this.GrpSource.TabStop = false;
            this.GrpSource.Text = "SOURCE";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cLabel3);
            this.groupBox2.Controls.Add(this.cLabel6);
            this.groupBox2.Controls.Add(this.txtDestinationServer);
            this.groupBox2.Controls.Add(this.cLabel7);
            this.groupBox2.Controls.Add(this.txtDestinationDBName);
            this.groupBox2.Controls.Add(this.cLabel8);
            this.groupBox2.Controls.Add(this.txtDestinationUsername);
            this.groupBox2.Controls.Add(this.txtDestinationPassword);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(374, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 128);
            this.groupBox2.TabIndex = 186;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DESTINATION";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(17, 22);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(55, 13);
            this.cLabel3.TabIndex = 184;
            this.cLabel3.Text = "SERVER";
            this.cLabel3.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(17, 103);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(78, 13);
            this.cLabel6.TabIndex = 184;
            this.cLabel6.Text = "PASSWORD";
            this.cLabel6.ToolTips = "";
            // 
            // txtDestinationServer
            // 
            this.txtDestinationServer.ActivationColor = false;
            this.txtDestinationServer.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtDestinationServer.AllowTabKeyOnEnter = false;
            this.txtDestinationServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestinationServer.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinationServer.Format = "";
            this.txtDestinationServer.IsComplusory = false;
            this.txtDestinationServer.Location = new System.Drawing.Point(103, 17);
            this.txtDestinationServer.Name = "txtDestinationServer";
            this.txtDestinationServer.SelectAllTextOnFocus = true;
            this.txtDestinationServer.Size = new System.Drawing.Size(235, 22);
            this.txtDestinationServer.TabIndex = 185;
            this.txtDestinationServer.Text = ".";
            this.txtDestinationServer.ToolTips = "";
            this.txtDestinationServer.WaterMarkText = null;
            // 
            // cLabel7
            // 
            this.cLabel7.AutoSize = true;
            this.cLabel7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel7.ForeColor = System.Drawing.Color.Black;
            this.cLabel7.Location = new System.Drawing.Point(17, 77);
            this.cLabel7.Name = "cLabel7";
            this.cLabel7.Size = new System.Drawing.Size(76, 13);
            this.cLabel7.TabIndex = 184;
            this.cLabel7.Text = "USERNAME";
            this.cLabel7.ToolTips = "";
            // 
            // txtDestinationDBName
            // 
            this.txtDestinationDBName.ActivationColor = false;
            this.txtDestinationDBName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtDestinationDBName.AllowTabKeyOnEnter = false;
            this.txtDestinationDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestinationDBName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinationDBName.Format = "";
            this.txtDestinationDBName.IsComplusory = false;
            this.txtDestinationDBName.Location = new System.Drawing.Point(103, 44);
            this.txtDestinationDBName.Name = "txtDestinationDBName";
            this.txtDestinationDBName.SelectAllTextOnFocus = true;
            this.txtDestinationDBName.Size = new System.Drawing.Size(235, 22);
            this.txtDestinationDBName.TabIndex = 185;
            this.txtDestinationDBName.Text = "SNKTender";
            this.txtDestinationDBName.ToolTips = "";
            this.txtDestinationDBName.WaterMarkText = null;
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(17, 49);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(64, 13);
            this.cLabel8.TabIndex = 184;
            this.cLabel8.Text = "DB NAME";
            this.cLabel8.ToolTips = "";
            // 
            // txtDestinationUsername
            // 
            this.txtDestinationUsername.ActivationColor = false;
            this.txtDestinationUsername.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtDestinationUsername.AllowTabKeyOnEnter = false;
            this.txtDestinationUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestinationUsername.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinationUsername.Format = "";
            this.txtDestinationUsername.IsComplusory = false;
            this.txtDestinationUsername.Location = new System.Drawing.Point(103, 72);
            this.txtDestinationUsername.Name = "txtDestinationUsername";
            this.txtDestinationUsername.SelectAllTextOnFocus = true;
            this.txtDestinationUsername.Size = new System.Drawing.Size(235, 22);
            this.txtDestinationUsername.TabIndex = 185;
            this.txtDestinationUsername.Text = "SA";
            this.txtDestinationUsername.ToolTips = "";
            this.txtDestinationUsername.WaterMarkText = null;
            // 
            // txtDestinationPassword
            // 
            this.txtDestinationPassword.ActivationColor = false;
            this.txtDestinationPassword.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtDestinationPassword.AllowTabKeyOnEnter = false;
            this.txtDestinationPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestinationPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinationPassword.Format = "";
            this.txtDestinationPassword.IsComplusory = false;
            this.txtDestinationPassword.Location = new System.Drawing.Point(103, 98);
            this.txtDestinationPassword.Name = "txtDestinationPassword";
            this.txtDestinationPassword.PasswordChar = '*';
            this.txtDestinationPassword.SelectAllTextOnFocus = true;
            this.txtDestinationPassword.Size = new System.Drawing.Size(235, 22);
            this.txtDestinationPassword.TabIndex = 185;
            this.txtDestinationPassword.Text = "1234";
            this.txtDestinationPassword.ToolTips = "";
            this.txtDestinationPassword.WaterMarkText = null;
            // 
            
            // panel1
            // 
            this.panel1.Controls.Add(this.CmbKapan);
            this.panel1.Controls.Add(this.cLabel9);
            this.panel1.Controls.Add(this.panelControl3);
            this.panel1.Controls.Add(this.GrpSource);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 191);
            this.panel1.TabIndex = 190;
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(105, 30);
            this.CmbKapan.Name = "CmbKapan";
            this.CmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbKapan.Properties.Appearance.Options.UseFont = true;
            this.CmbKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbKapan.Properties.DropDownRows = 20;
            this.CmbKapan.Properties.IncrementalSearch = true;
            this.CmbKapan.Size = new System.Drawing.Size(589, 24);
            this.CmbKapan.TabIndex = 190;
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(39, 33);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(53, 14);
            this.cLabel9.TabIndex = 189;
            this.cLabel9.Text = "KAPAN";
            this.cLabel9.ToolTips = "";
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.MistyRose;
            this.panelControl3.Appearance.BackColor2 = System.Drawing.Color.PaleTurquoise;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.Controls.Add(this.LblSalesDetail);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.panelControl3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(971, 27);
            this.panelControl3.TabIndex = 246;
            // 
            // LblSalesDetail
            // 
            this.LblSalesDetail.BackColor = System.Drawing.Color.Transparent;
            this.LblSalesDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblSalesDetail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSalesDetail.ForeColor = System.Drawing.Color.MidnightBlue;
            this.LblSalesDetail.Location = new System.Drawing.Point(3, 3);
            this.LblSalesDetail.Name = "LblSalesDetail";
            this.LblSalesDetail.Size = new System.Drawing.Size(965, 21);
            this.LblSalesDetail.TabIndex = 241;
            this.LblSalesDetail.Text = "  --: Kapan Transfer :--";
            this.LblSalesDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblSalesDetail.ToolTips = "";
            // 
            // xtraTabActivity
            // 
            this.xtraTabActivity.AppearancePage.Header.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.xtraTabActivity.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabActivity.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.xtraTabActivity.AppearancePage.HeaderActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xtraTabActivity.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabActivity.AppearancePage.HeaderActive.Options.UseForeColor = true;
            this.xtraTabActivity.Cursor = System.Windows.Forms.Cursors.Default;
            this.xtraTabActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabActivity.Location = new System.Drawing.Point(0, 191);
            this.xtraTabActivity.LookAndFeel.SkinName = "VS2010";
            this.xtraTabActivity.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabActivity.Name = "xtraTabActivity";
            this.xtraTabActivity.SelectedTabPage = this.xtraTabPage2;
            this.xtraTabActivity.Size = new System.Drawing.Size(971, 362);
            this.xtraTabActivity.TabIndex = 191;
            this.xtraTabActivity.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage2});
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Appearance.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.xtraTabPage2.Appearance.Header.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.xtraTabPage2.Appearance.Header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.xtraTabPage2.Appearance.Header.Options.UseBackColor = true;
            this.xtraTabPage2.Appearance.Header.Options.UseFont = true;
            this.xtraTabPage2.Appearance.Header.Options.UseTextOptions = true;
            this.xtraTabPage2.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xtraTabPage2.Controls.Add(this.splitContainer2);
            this.xtraTabPage2.Controls.Add(this.cLabel11);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(969, 328);
            this.xtraTabPage2.TabPageWidth = 120;
            this.xtraTabPage2.Text = "TRANSACTION";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 20);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.MainGrdTransaction);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.progressPanelTransactionDelete);
            this.splitContainer2.Panel2.Controls.Add(this.lblDeleteTransaction);
            this.splitContainer2.Panel2.Controls.Add(this.BtnDeleteTransaction);
            this.splitContainer2.Panel2.Controls.Add(this.BtnTransferTransaction);
            this.splitContainer2.Panel2.Controls.Add(this.progressPanelTransaction);
            this.splitContainer2.Panel2.Controls.Add(this.lblMessageTransaction);
            this.splitContainer2.Size = new System.Drawing.Size(969, 308);
            this.splitContainer2.SplitterDistance = 722;
            this.splitContainer2.SplitterWidth = 8;
            this.splitContainer2.TabIndex = 191;
            this.splitContainer2.TabStop = false;
            // 
            // MainGrdTransaction
            // 
            this.MainGrdTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrdTransaction.Location = new System.Drawing.Point(0, 0);
            this.MainGrdTransaction.MainView = this.GrdDetTransaction;
            this.MainGrdTransaction.Name = "MainGrdTransaction";
            this.MainGrdTransaction.Size = new System.Drawing.Size(720, 306);
            this.MainGrdTransaction.TabIndex = 189;
            this.MainGrdTransaction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetTransaction});
            // 
            // GrdDetTransaction
            // 
            this.GrdDetTransaction.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetTransaction.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetTransaction.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetTransaction.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDetTransaction.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetTransaction.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.GrdDetTransaction.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetTransaction.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDetTransaction.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetTransaction.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetTransaction.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetTransaction.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetTransaction.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetTransaction.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDetTransaction.Appearance.Row.Options.UseFont = true;
            this.GrdDetTransaction.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetTransaction.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetTransaction.ColumnPanelRowHeight = 25;
            this.GrdDetTransaction.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.GrdDetTransaction.GridControl = this.MainGrdTransaction;
            this.GrdDetTransaction.Name = "GrdDetTransaction";
            this.GrdDetTransaction.OptionsBehavior.Editable = false;
            this.GrdDetTransaction.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetTransaction.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDetTransaction.OptionsPrint.ExpandAllGroups = false;
            this.GrdDetTransaction.OptionsSelection.MultiSelect = true;
            this.GrdDetTransaction.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDetTransaction.OptionsView.ColumnAutoWidth = false;
            this.GrdDetTransaction.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetTransaction.OptionsView.ShowAutoFilterRow = true;
            this.GrdDetTransaction.OptionsView.ShowFooter = true;
            this.GrdDetTransaction.OptionsView.ShowGroupPanel = false;
            this.GrdDetTransaction.RowHeight = 25;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "TableName";
            this.gridColumn2.FieldName = "TABLENAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Width = 125;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Status";
            this.gridColumn3.FieldName = "STATUS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 400;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Display";
            this.gridColumn4.FieldName = "TABLENAME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 300;
            // 
            // progressPanelTransactionDelete
            // 
            this.progressPanelTransactionDelete.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanelTransactionDelete.Appearance.Options.UseBackColor = true;
            this.progressPanelTransactionDelete.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanelTransactionDelete.AppearanceCaption.Options.UseFont = true;
            this.progressPanelTransactionDelete.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanelTransactionDelete.AppearanceDescription.Options.UseFont = true;
            this.progressPanelTransactionDelete.Location = new System.Drawing.Point(7, 253);
            this.progressPanelTransactionDelete.Name = "progressPanelTransactionDelete";
            this.progressPanelTransactionDelete.Size = new System.Drawing.Size(181, 45);
            this.progressPanelTransactionDelete.TabIndex = 193;
            this.progressPanelTransactionDelete.Text = "progressPanel1";
            this.progressPanelTransactionDelete.Visible = false;
            // 
            // lblDeleteTransaction
            // 
            this.lblDeleteTransaction.AutoSize = true;
            this.lblDeleteTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeleteTransaction.ForeColor = System.Drawing.Color.Maroon;
            this.lblDeleteTransaction.Location = new System.Drawing.Point(9, 235);
            this.lblDeleteTransaction.Name = "lblDeleteTransaction";
            this.lblDeleteTransaction.Size = new System.Drawing.Size(63, 13);
            this.lblDeleteTransaction.TabIndex = 192;
            this.lblDeleteTransaction.Text = "Message";
            this.lblDeleteTransaction.ToolTips = "";
            // 
            // BtnDeleteTransaction
            // 
            this.BtnDeleteTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteTransaction.ForeColor = System.Drawing.Color.Navy;
            this.BtnDeleteTransaction.Location = new System.Drawing.Point(7, 157);
            this.BtnDeleteTransaction.Name = "BtnDeleteTransaction";
            this.BtnDeleteTransaction.Size = new System.Drawing.Size(155, 75);
            this.BtnDeleteTransaction.TabIndex = 191;
            this.BtnDeleteTransaction.Text = "DELETE";
            this.BtnDeleteTransaction.ToolTips = "";
            this.BtnDeleteTransaction.UseVisualStyleBackColor = true;
            this.BtnDeleteTransaction.Click += new System.EventHandler(this.BtnDeleteTransaction_Click);
            // 
            // BtnTransferTransaction
            // 
            this.BtnTransferTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTransferTransaction.ForeColor = System.Drawing.Color.Navy;
            this.BtnTransferTransaction.Location = new System.Drawing.Point(7, 10);
            this.BtnTransferTransaction.Name = "BtnTransferTransaction";
            this.BtnTransferTransaction.Size = new System.Drawing.Size(155, 75);
            this.BtnTransferTransaction.TabIndex = 3;
            this.BtnTransferTransaction.Text = "TRANSFER";
            this.BtnTransferTransaction.ToolTips = "";
            this.BtnTransferTransaction.UseVisualStyleBackColor = true;
            this.BtnTransferTransaction.Click += new System.EventHandler(this.BtnTransferTransaction_Click);
            // 
            // progressPanelTransaction
            // 
            this.progressPanelTransaction.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanelTransaction.Appearance.Options.UseBackColor = true;
            this.progressPanelTransaction.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanelTransaction.AppearanceCaption.Options.UseFont = true;
            this.progressPanelTransaction.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanelTransaction.AppearanceDescription.Options.UseFont = true;
            this.progressPanelTransaction.Location = new System.Drawing.Point(12, 104);
            this.progressPanelTransaction.Name = "progressPanelTransaction";
            this.progressPanelTransaction.Size = new System.Drawing.Size(181, 45);
            this.progressPanelTransaction.TabIndex = 188;
            this.progressPanelTransaction.Text = "progressPanel1";
            this.progressPanelTransaction.Visible = false;
            // 
            // lblMessageTransaction
            // 
            this.lblMessageTransaction.AutoSize = true;
            this.lblMessageTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageTransaction.ForeColor = System.Drawing.Color.Maroon;
            this.lblMessageTransaction.Location = new System.Drawing.Point(10, 88);
            this.lblMessageTransaction.Name = "lblMessageTransaction";
            this.lblMessageTransaction.Size = new System.Drawing.Size(63, 13);
            this.lblMessageTransaction.TabIndex = 190;
            this.lblMessageTransaction.Text = "Message";
            this.lblMessageTransaction.ToolTips = "";
            // 
            // cLabel11
            // 
            this.cLabel11.BackColor = System.Drawing.Color.Transparent;
            this.cLabel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.cLabel11.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cLabel11.ForeColor = System.Drawing.Color.MidnightBlue;
            this.cLabel11.Location = new System.Drawing.Point(0, 0);
            this.cLabel11.Name = "cLabel11";
            this.cLabel11.Size = new System.Drawing.Size(969, 20);
            this.cLabel11.TabIndex = 243;
            this.cLabel11.Text = "    Transaction Data Transfer :--";
            this.cLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cLabel11.ToolTips = "";
            // 
            // bgWorkerTransaction
            // 
            this.bgWorkerTransaction.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerTransaction_DoWork);
            this.bgWorkerTransaction.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerTransaction_RunWorkerCompleted);
            // 
            // bgWorkerPricing
            // 
            // 
            // bgWorkerTransactionDelete
            // 
            this.bgWorkerTransactionDelete.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerTransactionDelete_DoWork);
            this.bgWorkerTransactionDelete.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerTransactionDelete_RunWorkerCompleted);
            // 
            // FrmKapanTransfer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(971, 553);
            this.Controls.Add(this.xtraTabActivity);
            this.Controls.Add(this.panel1);
            this.Name = "FrmKapanTransfer";
            this.Text = "KAPAN TRANSFER";
            this.GrpSource.ResumeLayout(false);
            this.GrpSource.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabActivity)).EndInit();
            this.xtraTabActivity.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetTransaction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtSourceServer;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtSourcePassword;
        private AxonContLib.cTextBox txtSourceUsername;
        private AxonContLib.cTextBox txtSourceDBName;
        private System.Windows.Forms.GroupBox GrpSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cTextBox txtDestinationServer;
        private AxonContLib.cLabel cLabel7;
        private AxonContLib.cTextBox txtDestinationDBName;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cTextBox txtDestinationUsername;
        private AxonContLib.cTextBox txtDestinationPassword;
        private System.ComponentModel.BackgroundWorker bgWorkerMaster;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private AxonContLib.cLabel cLabel9;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private AxonContLib.cLabel LblSalesDetail;
        private DevExpress.XtraTab.XtraTabControl xtraTabActivity;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanelTransaction;
        private AxonContLib.cButton BtnTransferTransaction;
        private DevExpress.XtraGrid.GridControl MainGrdTransaction;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetTransaction;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.ComponentModel.BackgroundWorker bgWorkerTransaction;
        private System.ComponentModel.BackgroundWorker bgWorkerPricing;
        private AxonContLib.cLabel lblMessageTransaction;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private AxonContLib.cLabel cLabel11;
        private AxonContLib.cButton BtnDeleteTransaction;
        private AxonContLib.cLabel lblDeleteTransaction;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanelTransactionDelete;
        private System.ComponentModel.BackgroundWorker bgWorkerTransactionDelete;
    }
}