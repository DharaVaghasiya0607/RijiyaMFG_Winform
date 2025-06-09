namespace AxoneMFGRJ.Transaction
{
    partial class FrmSingleGoodsReturnLiveStock
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
            this.btnShowComplete = new DevExpress.XtraEditors.SimpleButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.progressPanelQCImport = new DevExpress.XtraWaitForm.ProgressPanel();
            this.BtnQCImportData = new DevExpress.XtraEditors.SimpleButton();
            this.TxtEmployee = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new AxonContLib.cFlowLayoutPanel(this.components);
            this.BtnAckPending = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.BtnComplete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnReject = new DevExpress.XtraEditors.SimpleButton();
            this.PanelProgress1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.RbtFullStock = new AxonContLib.cRadioButton(this.components);
            this.RbtMYStock = new AxonContLib.cRadioButton(this.components);
            this.RbtOtherStock = new System.Windows.Forms.RadioButton();
            this.ChkWithExtraStock = new AxonContLib.cCheckBox(this.components);
            this.ChkGrpJangedNo = new AxonContLib.cCheckBox(this.components);
            this.RbtDeptStock = new AxonContLib.cRadioButton(this.components);
            this.RbtPktSerialNo = new AxonContLib.cRadioButton(this.components);
            this.RbtnDPTJangedNo = new AxonContLib.cRadioButton(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.flowLayoutPanel2 = new AxonContLib.cFlowLayoutPanel(this.components);
            this.PanelBarcode = new AxonContLib.cPanel(this.components);
            this.cLabel7 = new AxonContLib.cLabel(this.components);
            this.txtBarcode = new AxonContLib.cTextBox(this.components);
            this.PanelPktSerialNo = new AxonContLib.cPanel(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.txtSrNoKapanName = new AxonContLib.cTextBox(this.components);
            this.cLabel10 = new AxonContLib.cLabel(this.components);
            this.txtSrNoSerialNo = new AxonContLib.cTextBox(this.components);
            this.PanelPacketNo = new AxonContLib.cPanel(this.components);
            this.cLabel18 = new AxonContLib.cLabel(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.txtKapanName = new AxonContLib.cTextBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtTag = new AxonContLib.cTextBox(this.components);
            this.txtPacketNo = new AxonContLib.cTextBox(this.components);
            this.PanelJangedNo = new AxonContLib.cPanel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.txtJangedNo = new AxonContLib.cTextBox(this.components);
            this.PnlDPTJangedno = new AxonContLib.cPanel(this.components);
            this.cLabel14 = new AxonContLib.cLabel(this.components);
            this.TxtJangedDate = new AxonContLib.cTextBox(this.components);
            this.cLabel13 = new AxonContLib.cLabel(this.components);
            this.TxtDPTJangedNo = new AxonContLib.cTextBox(this.components);
            this.RbtJangedNo = new AxonContLib.cRadioButton(this.components);
            this.lblRoughMak = new AxonContLib.cLabel(this.components);
            this.RbtBarcode = new AxonContLib.cRadioButton(this.components);
            this.RbtPacketNo = new AxonContLib.cRadioButton(this.components);
            this.BtnBestFit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnKapanLiveStockExcelExport = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new AxonContLib.cPanel(this.components);
            this.panel3 = new AxonContLib.cPanel(this.components);
            this.BtnGroupJanged = new DevExpress.XtraEditors.SimpleButton();
            this.lblDefaultLayout = new AxonContLib.cLabel(this.components);
            this.lblSaveLayout = new AxonContLib.cLabel(this.components);
            this.lblPendingsGoods = new AxonContLib.cLabel(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.lblConfiredGoods = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.txtSelectedCarat = new AxonContLib.cTextBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.txtSelectedPcs = new AxonContLib.cTextBox(this.components);
            this.txtTotalCarat = new AxonContLib.cTextBox(this.components);
            this.txtTotalPcs = new AxonContLib.cTextBox(this.components);
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn144 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn52 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn55 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn58 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn56 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn59 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn60 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn61 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn62 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn63 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn64 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn65 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn66 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn77 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn83 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn84 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn85 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn90 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn91 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn92 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn93 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn94 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn95 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn96 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn97 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn98 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn99 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn100 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn101 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn102 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn103 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn104 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn105 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn106 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn67 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn68 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn69 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn70 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn71 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn72 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn73 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn74 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn75 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn76 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn78 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn79 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn80 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn81 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn82 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn86 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn87 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn88 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn89 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn107 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn108 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn109 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn110 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn111 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn112 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn113 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn114 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn115 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn116 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn117 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn118 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn119 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChkDownload = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn120 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkComplete = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn121 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkReject = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn122 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkCancel = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn123 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn124 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn125 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn126 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn127 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn128 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn129 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn130 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn131 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn132 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn133 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn134 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn135 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn136 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn137 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn138 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn139 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn140 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn141 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn142 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn143 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.PanelProgress = new System.Windows.Forms.GroupBox();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.GrpReject = new DevExpress.XtraEditors.GroupControl();
            this.txtRemark = new AxonContLib.cTextBox(this.components);
            this.cLabel11 = new AxonContLib.cLabel(this.components);
            this.txtReason = new AxonContLib.cTextBox(this.components);
            this.cLabel12 = new AxonContLib.cLabel(this.components);
            this.BtnRejectReason = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRejectClose = new DevExpress.XtraEditors.SimpleButton();
            this.BkgQCImport = new System.ComponentModel.BackgroundWorker();
            this.PanelHeader.SuspendLayout();
            this.panel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.PanelBarcode.SuspendLayout();
            this.PanelPktSerialNo.SuspendLayout();
            this.PanelPacketNo.SuspendLayout();
            this.PanelJangedNo.SuspendLayout();
            this.PnlDPTJangedno.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkComplete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkReject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.PanelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrpReject)).BeginInit();
            this.GrpReject.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.Controls.Add(this.btnShowComplete);
            this.PanelHeader.Controls.Add(this.panel4);
            this.PanelHeader.Controls.Add(this.flowLayoutPanel1);
            this.PanelHeader.Controls.Add(this.RbtPktSerialNo);
            this.PanelHeader.Controls.Add(this.RbtnDPTJangedNo);
            this.PanelHeader.Controls.Add(this.groupControl1);
            this.PanelHeader.Controls.Add(this.RbtJangedNo);
            this.PanelHeader.Controls.Add(this.lblRoughMak);
            this.PanelHeader.Controls.Add(this.RbtBarcode);
            this.PanelHeader.Controls.Add(this.RbtPacketNo);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1370, 158);
            this.PanelHeader.TabIndex = 0;
            // 
            // btnShowComplete
            // 
            this.btnShowComplete.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnShowComplete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnShowComplete.Appearance.Options.UseFont = true;
            this.btnShowComplete.Appearance.Options.UseForeColor = true;
            this.btnShowComplete.Location = new System.Drawing.Point(622, 24);
            this.btnShowComplete.Name = "btnShowComplete";
            this.btnShowComplete.Size = new System.Drawing.Size(130, 59);
            this.btnShowComplete.TabIndex = 209;
            this.btnShowComplete.Text = "&Show Complete";
            this.btnShowComplete.Click += new System.EventHandler(this.btnShowComplete_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel4.Controls.Add(this.progressPanelQCImport);
            this.panel4.Controls.Add(this.BtnQCImportData);
            this.panel4.Controls.Add(this.TxtEmployee);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(187, 95);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(539, 43);
            this.panel4.TabIndex = 208;
            // 
            // progressPanelQCImport
            // 
            this.progressPanelQCImport.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanelQCImport.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.progressPanelQCImport.Appearance.Options.UseBackColor = true;
            this.progressPanelQCImport.Appearance.Options.UseFont = true;
            this.progressPanelQCImport.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 12F);
            this.progressPanelQCImport.AppearanceCaption.Options.UseFont = true;
            this.progressPanelQCImport.AppearanceDescription.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.progressPanelQCImport.AppearanceDescription.Options.UseFont = true;
            this.progressPanelQCImport.BarAnimationElementThickness = 2;
            this.progressPanelQCImport.Location = new System.Drawing.Point(425, 9);
            this.progressPanelQCImport.LookAndFeel.SkinName = "Office 2007 Black";
            this.progressPanelQCImport.LookAndFeel.UseDefaultLookAndFeel = false;
            this.progressPanelQCImport.Name = "progressPanelQCImport";
            this.progressPanelQCImport.ShowDescription = false;
            this.progressPanelQCImport.Size = new System.Drawing.Size(30, 26);
            this.progressPanelQCImport.TabIndex = 211;
            this.progressPanelQCImport.Text = "progressPanel2";
            this.progressPanelQCImport.Visible = false;
            // 
            // BtnQCImportData
            // 
            this.BtnQCImportData.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnQCImportData.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnQCImportData.Appearance.Options.UseFont = true;
            this.BtnQCImportData.Appearance.Options.UseForeColor = true;
            this.BtnQCImportData.Location = new System.Drawing.Point(300, 10);
            this.BtnQCImportData.Name = "BtnQCImportData";
            this.BtnQCImportData.Size = new System.Drawing.Size(119, 25);
            this.BtnQCImportData.TabIndex = 210;
            this.BtnQCImportData.TabStop = false;
            this.BtnQCImportData.Text = "&Import Data";
            this.BtnQCImportData.Click += new System.EventHandler(this.BtnQCImportData_Click);
            // 
            // TxtEmployee
            // 
            this.TxtEmployee.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmployee.Location = new System.Drawing.Point(120, 11);
            this.TxtEmployee.Name = "TxtEmployee";
            this.TxtEmployee.Size = new System.Drawing.Size(177, 23);
            this.TxtEmployee.TabIndex = 209;
            this.TxtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtEmployee_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 208;
            this.label1.Text = "Stock Of Emp";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.BtnAckPending);
            this.flowLayoutPanel1.Controls.Add(this.BtnSearch);
            this.flowLayoutPanel1.Controls.Add(this.BtnDownload);
            this.flowLayoutPanel1.Controls.Add(this.BtnComplete);
            this.flowLayoutPanel1.Controls.Add(this.BtnCancel);
            this.flowLayoutPanel1.Controls.Add(this.BtnReject);
            this.flowLayoutPanel1.Controls.Add(this.PanelProgress1);
            this.flowLayoutPanel1.Controls.Add(this.panel5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(799, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(571, 158);
            this.flowLayoutPanel1.TabIndex = 207;
            // 
            // BtnAckPending
            // 
            this.BtnAckPending.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAckPending.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAckPending.Appearance.Options.UseFont = true;
            this.BtnAckPending.Appearance.Options.UseForeColor = true;
            this.BtnAckPending.Location = new System.Drawing.Point(3, 3);
            this.BtnAckPending.Name = "BtnAckPending";
            this.BtnAckPending.Size = new System.Drawing.Size(85, 36);
            this.BtnAckPending.TabIndex = 150;
            this.BtnAckPending.TabStop = false;
            this.BtnAckPending.Text = "&Ack.(0)";
            this.BtnAckPending.Click += new System.EventHandler(this.BtnAckPending_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Location = new System.Drawing.Point(94, 3);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(65, 36);
            this.BtnSearch.TabIndex = 150;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "&Load All";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnDownload
            // 
            this.BtnDownload.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDownload.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDownload.Appearance.Options.UseFont = true;
            this.BtnDownload.Appearance.Options.UseForeColor = true;
            this.BtnDownload.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnDownload.Location = new System.Drawing.Point(165, 3);
            this.BtnDownload.Name = "BtnDownload";
            this.BtnDownload.Size = new System.Drawing.Size(81, 34);
            this.BtnDownload.TabIndex = 204;
            this.BtnDownload.TabStop = false;
            this.BtnDownload.Text = "Download";
            this.BtnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // BtnComplete
            // 
            this.BtnComplete.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnComplete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnComplete.Appearance.Options.UseFont = true;
            this.BtnComplete.Appearance.Options.UseForeColor = true;
            this.BtnComplete.Location = new System.Drawing.Point(252, 3);
            this.BtnComplete.Name = "BtnComplete";
            this.BtnComplete.Size = new System.Drawing.Size(81, 36);
            this.BtnComplete.TabIndex = 205;
            this.BtnComplete.Text = "&Complete";
            this.BtnComplete.Click += new System.EventHandler(this.BtnComplete_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.Location = new System.Drawing.Point(339, 3);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(81, 34);
            this.BtnCancel.TabIndex = 206;
            this.BtnCancel.TabStop = false;
            this.BtnCancel.Text = "Cance&l";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnReject
            // 
            this.BtnReject.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnReject.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnReject.Appearance.Options.UseFont = true;
            this.BtnReject.Appearance.Options.UseForeColor = true;
            this.BtnReject.Location = new System.Drawing.Point(426, 3);
            this.BtnReject.Name = "BtnReject";
            this.BtnReject.Size = new System.Drawing.Size(81, 34);
            this.BtnReject.TabIndex = 207;
            this.BtnReject.TabStop = false;
            this.BtnReject.Text = "&Reject";
            this.BtnReject.Click += new System.EventHandler(this.BtnReject_Click);
            // 
            // PanelProgress1
            // 
            this.PanelProgress1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.PanelProgress1.Appearance.Options.UseBackColor = true;
            this.PanelProgress1.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 12F);
            this.PanelProgress1.AppearanceCaption.Options.UseFont = true;
            this.PanelProgress1.AppearanceDescription.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.PanelProgress1.AppearanceDescription.Options.UseFont = true;
            this.PanelProgress1.BarAnimationElementThickness = 2;
            this.PanelProgress1.Location = new System.Drawing.Point(513, 3);
            this.PanelProgress1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.PanelProgress1.Name = "PanelProgress1";
            this.PanelProgress1.ShowCaption = false;
            this.PanelProgress1.ShowDescription = false;
            this.PanelProgress1.Size = new System.Drawing.Size(44, 41);
            this.PanelProgress1.TabIndex = 208;
            this.PanelProgress1.Text = "progressPanel1";
            this.PanelProgress1.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.RbtFullStock);
            this.panel5.Controls.Add(this.RbtMYStock);
            this.panel5.Controls.Add(this.RbtOtherStock);
            this.panel5.Controls.Add(this.ChkWithExtraStock);
            this.panel5.Controls.Add(this.ChkGrpJangedNo);
            this.panel5.Controls.Add(this.RbtDeptStock);
            this.panel5.Location = new System.Drawing.Point(3, 50);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(554, 80);
            this.panel5.TabIndex = 208;
            // 
            // RbtFullStock
            // 
            this.RbtFullStock.AllowTabKeyOnEnter = false;
            this.RbtFullStock.AutoSize = true;
            this.RbtFullStock.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtFullStock.ForeColor = System.Drawing.Color.Black;
            this.RbtFullStock.Location = new System.Drawing.Point(3, 9);
            this.RbtFullStock.Name = "RbtFullStock";
            this.RbtFullStock.Size = new System.Drawing.Size(84, 17);
            this.RbtFullStock.TabIndex = 158;
            this.RbtFullStock.Tag = "FULLSTOCK";
            this.RbtFullStock.Text = "FULL STK";
            this.RbtFullStock.ToolTips = "Display All Over Company Stock";
            this.RbtFullStock.UseVisualStyleBackColor = true;
            this.RbtFullStock.CheckedChanged += new System.EventHandler(this.RbtFullStock_CheckedChanged);
            // 
            // RbtMYStock
            // 
            this.RbtMYStock.AllowTabKeyOnEnter = false;
            this.RbtMYStock.AutoSize = true;
            this.RbtMYStock.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtMYStock.ForeColor = System.Drawing.Color.Black;
            this.RbtMYStock.Location = new System.Drawing.Point(119, 9);
            this.RbtMYStock.Name = "RbtMYStock";
            this.RbtMYStock.Size = new System.Drawing.Size(71, 17);
            this.RbtMYStock.TabIndex = 158;
            this.RbtMYStock.Tag = "MYSTOCK";
            this.RbtMYStock.Text = "MY STK";
            this.RbtMYStock.ToolTips = "Display Only Login User Stock";
            this.RbtMYStock.UseVisualStyleBackColor = true;
            this.RbtMYStock.CheckedChanged += new System.EventHandler(this.RbtFullStock_CheckedChanged);
            // 
            // RbtOtherStock
            // 
            this.RbtOtherStock.AutoSize = true;
            this.RbtOtherStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RbtOtherStock.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtOtherStock.ForeColor = System.Drawing.Color.Black;
            this.RbtOtherStock.Location = new System.Drawing.Point(318, 9);
            this.RbtOtherStock.Name = "RbtOtherStock";
            this.RbtOtherStock.Size = new System.Drawing.Size(79, 17);
            this.RbtOtherStock.TabIndex = 157;
            this.RbtOtherStock.Tag = "OTHERSTOCK";
            this.RbtOtherStock.Text = "OTH STK";
            this.RbtOtherStock.UseVisualStyleBackColor = true;
            this.RbtOtherStock.CheckedChanged += new System.EventHandler(this.RbtFullStock_CheckedChanged);
            // 
            // ChkWithExtraStock
            // 
            this.ChkWithExtraStock.AllowTabKeyOnEnter = false;
            this.ChkWithExtraStock.AutoSize = true;
            this.ChkWithExtraStock.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkWithExtraStock.Location = new System.Drawing.Point(85, 45);
            this.ChkWithExtraStock.Name = "ChkWithExtraStock";
            this.ChkWithExtraStock.Size = new System.Drawing.Size(105, 17);
            this.ChkWithExtraStock.TabIndex = 184;
            this.ChkWithExtraStock.TabStop = false;
            this.ChkWithExtraStock.Text = "With Ext Stk";
            this.ChkWithExtraStock.ToolTips = "If Apply Any Difference";
            this.ChkWithExtraStock.UseVisualStyleBackColor = true;
            this.ChkWithExtraStock.Visible = false;
            // 
            // ChkGrpJangedNo
            // 
            this.ChkGrpJangedNo.AllowTabKeyOnEnter = false;
            this.ChkGrpJangedNo.AutoSize = true;
            this.ChkGrpJangedNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkGrpJangedNo.Location = new System.Drawing.Point(3, 45);
            this.ChkGrpJangedNo.Name = "ChkGrpJangedNo";
            this.ChkGrpJangedNo.Size = new System.Drawing.Size(76, 17);
            this.ChkGrpJangedNo.TabIndex = 206;
            this.ChkGrpJangedNo.TabStop = false;
            this.ChkGrpJangedNo.Text = "Grp JNo";
            this.ChkGrpJangedNo.ToolTips = "If Apply Any Difference";
            this.ChkGrpJangedNo.UseVisualStyleBackColor = true;
            this.ChkGrpJangedNo.Visible = false;
            // 
            // RbtDeptStock
            // 
            this.RbtDeptStock.AllowTabKeyOnEnter = false;
            this.RbtDeptStock.AutoSize = true;
            this.RbtDeptStock.Checked = true;
            this.RbtDeptStock.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtDeptStock.ForeColor = System.Drawing.Color.Black;
            this.RbtDeptStock.Location = new System.Drawing.Point(209, 9);
            this.RbtDeptStock.Name = "RbtDeptStock";
            this.RbtDeptStock.Size = new System.Drawing.Size(86, 17);
            this.RbtDeptStock.TabIndex = 158;
            this.RbtDeptStock.TabStop = true;
            this.RbtDeptStock.Tag = "DEPTSTOCK";
            this.RbtDeptStock.Text = "DEPT STK";
            this.RbtDeptStock.ToolTips = "Display Department (MY Stock + Other Stock)";
            this.RbtDeptStock.UseVisualStyleBackColor = true;
            this.RbtDeptStock.CheckedChanged += new System.EventHandler(this.RbtFullStock_CheckedChanged);
            // 
            // RbtPktSerialNo
            // 
            this.RbtPktSerialNo.AllowTabKeyOnEnter = false;
            this.RbtPktSerialNo.AutoSize = true;
            this.RbtPktSerialNo.BackColor = System.Drawing.Color.Transparent;
            this.RbtPktSerialNo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtPktSerialNo.ForeColor = System.Drawing.Color.Black;
            this.RbtPktSerialNo.Location = new System.Drawing.Point(62, 33);
            this.RbtPktSerialNo.Name = "RbtPktSerialNo";
            this.RbtPktSerialNo.Size = new System.Drawing.Size(59, 17);
            this.RbtPktSerialNo.TabIndex = 159;
            this.RbtPktSerialNo.Tag = "SRNO";
            this.RbtPktSerialNo.Text = "SRNO";
            this.RbtPktSerialNo.ToolTips = "Display All Over Company Stock";
            this.RbtPktSerialNo.UseVisualStyleBackColor = false;
            this.RbtPktSerialNo.CheckedChanged += new System.EventHandler(this.RbtBarcode_CheckedChanged);
            // 
            // RbtnDPTJangedNo
            // 
            this.RbtnDPTJangedNo.AllowTabKeyOnEnter = false;
            this.RbtnDPTJangedNo.AutoSize = true;
            this.RbtnDPTJangedNo.BackColor = System.Drawing.Color.Transparent;
            this.RbtnDPTJangedNo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtnDPTJangedNo.ForeColor = System.Drawing.Color.Black;
            this.RbtnDPTJangedNo.Location = new System.Drawing.Point(62, 59);
            this.RbtnDPTJangedNo.Name = "RbtnDPTJangedNo";
            this.RbtnDPTJangedNo.Size = new System.Drawing.Size(118, 17);
            this.RbtnDPTJangedNo.TabIndex = 161;
            this.RbtnDPTJangedNo.Tag = "DPTJANGEDNO";
            this.RbtnDPTJangedNo.Text = "DPTJANGEDNO";
            this.RbtnDPTJangedNo.ToolTips = "Display All Over Company Stock";
            this.RbtnDPTJangedNo.UseVisualStyleBackColor = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.flowLayoutPanel2);
            this.groupControl1.Location = new System.Drawing.Point(187, 3);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(431, 80);
            this.groupControl1.TabIndex = 207;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.PanelBarcode);
            this.flowLayoutPanel2.Controls.Add(this.PanelPktSerialNo);
            this.flowLayoutPanel2.Controls.Add(this.PanelPacketNo);
            this.flowLayoutPanel2.Controls.Add(this.PanelJangedNo);
            this.flowLayoutPanel2.Controls.Add(this.PnlDPTJangedno);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(2, 20);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(427, 58);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // PanelBarcode
            // 
            this.PanelBarcode.Controls.Add(this.cLabel7);
            this.PanelBarcode.Controls.Add(this.txtBarcode);
            this.PanelBarcode.Location = new System.Drawing.Point(3, 2);
            this.PanelBarcode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PanelBarcode.Name = "PanelBarcode";
            this.PanelBarcode.Size = new System.Drawing.Size(215, 50);
            this.PanelBarcode.TabIndex = 206;
            // 
            // cLabel7
            // 
            this.cLabel7.AutoSize = true;
            this.cLabel7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel7.ForeColor = System.Drawing.Color.Black;
            this.cLabel7.Location = new System.Drawing.Point(6, 9);
            this.cLabel7.Name = "cLabel7";
            this.cLabel7.Size = new System.Drawing.Size(60, 13);
            this.cLabel7.TabIndex = 1;
            this.cLabel7.Text = "Barcode";
            this.cLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel7.ToolTips = "";
            // 
            // txtBarcode
            // 
            this.txtBarcode.ActivationColor = true;
            this.txtBarcode.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtBarcode.AllowTabKeyOnEnter = false;
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarcode.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Format = "";
            this.txtBarcode.IsComplusory = false;
            this.txtBarcode.Location = new System.Drawing.Point(6, 25);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.SelectAllTextOnFocus = true;
            this.txtBarcode.Size = new System.Drawing.Size(192, 23);
            this.txtBarcode.TabIndex = 2;
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcode.ToolTips = "";
            this.txtBarcode.WaterMarkText = null;
            this.txtBarcode.Validated += new System.EventHandler(this.txtBarcode_Validated);
            // 
            // PanelPktSerialNo
            // 
            this.PanelPktSerialNo.Controls.Add(this.cLabel9);
            this.PanelPktSerialNo.Controls.Add(this.txtSrNoKapanName);
            this.PanelPktSerialNo.Controls.Add(this.cLabel10);
            this.PanelPktSerialNo.Controls.Add(this.txtSrNoSerialNo);
            this.PanelPktSerialNo.Location = new System.Drawing.Point(224, 2);
            this.PanelPktSerialNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PanelPktSerialNo.Name = "PanelPktSerialNo";
            this.PanelPktSerialNo.Size = new System.Drawing.Size(198, 50);
            this.PanelPktSerialNo.TabIndex = 207;
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(3, 7);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(47, 13);
            this.cLabel9.TabIndex = 5;
            this.cLabel9.Text = "Kapan";
            this.cLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel9.ToolTips = "";
            // 
            // txtSrNoKapanName
            // 
            this.txtSrNoKapanName.ActivationColor = true;
            this.txtSrNoKapanName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSrNoKapanName.AllowTabKeyOnEnter = false;
            this.txtSrNoKapanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSrNoKapanName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrNoKapanName.Format = "";
            this.txtSrNoKapanName.IsComplusory = false;
            this.txtSrNoKapanName.Location = new System.Drawing.Point(3, 24);
            this.txtSrNoKapanName.Name = "txtSrNoKapanName";
            this.txtSrNoKapanName.SelectAllTextOnFocus = true;
            this.txtSrNoKapanName.Size = new System.Drawing.Size(92, 23);
            this.txtSrNoKapanName.TabIndex = 6;
            this.txtSrNoKapanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSrNoKapanName.ToolTips = "";
            this.txtSrNoKapanName.WaterMarkText = null;
            // 
            // cLabel10
            // 
            this.cLabel10.AutoSize = true;
            this.cLabel10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel10.ForeColor = System.Drawing.Color.Black;
            this.cLabel10.Location = new System.Drawing.Point(94, 8);
            this.cLabel10.Name = "cLabel10";
            this.cLabel10.Size = new System.Drawing.Size(38, 13);
            this.cLabel10.TabIndex = 7;
            this.cLabel10.Text = "SrNo";
            this.cLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel10.ToolTips = "";
            // 
            // txtSrNoSerialNo
            // 
            this.txtSrNoSerialNo.ActivationColor = true;
            this.txtSrNoSerialNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSrNoSerialNo.AllowTabKeyOnEnter = false;
            this.txtSrNoSerialNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSrNoSerialNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrNoSerialNo.Format = "#####0";
            this.txtSrNoSerialNo.IsComplusory = false;
            this.txtSrNoSerialNo.Location = new System.Drawing.Point(96, 24);
            this.txtSrNoSerialNo.Name = "txtSrNoSerialNo";
            this.txtSrNoSerialNo.SelectAllTextOnFocus = true;
            this.txtSrNoSerialNo.Size = new System.Drawing.Size(92, 23);
            this.txtSrNoSerialNo.TabIndex = 8;
            this.txtSrNoSerialNo.Text = "0";
            this.txtSrNoSerialNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSrNoSerialNo.ToolTips = "";
            this.txtSrNoSerialNo.WaterMarkText = null;
            this.txtSrNoSerialNo.Validated += new System.EventHandler(this.txtSrNoSerialNo_Validated);
            // 
            // PanelPacketNo
            // 
            this.PanelPacketNo.Controls.Add(this.cLabel18);
            this.PanelPacketNo.Controls.Add(this.cLabel8);
            this.PanelPacketNo.Controls.Add(this.txtKapanName);
            this.PanelPacketNo.Controls.Add(this.cLabel1);
            this.PanelPacketNo.Controls.Add(this.txtTag);
            this.PanelPacketNo.Controls.Add(this.txtPacketNo);
            this.PanelPacketNo.Location = new System.Drawing.Point(3, 56);
            this.PanelPacketNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PanelPacketNo.Name = "PanelPacketNo";
            this.PanelPacketNo.Size = new System.Drawing.Size(220, 50);
            this.PanelPacketNo.TabIndex = 206;
            // 
            // cLabel18
            // 
            this.cLabel18.AutoSize = true;
            this.cLabel18.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel18.ForeColor = System.Drawing.Color.Black;
            this.cLabel18.Location = new System.Drawing.Point(3, 6);
            this.cLabel18.Name = "cLabel18";
            this.cLabel18.Size = new System.Drawing.Size(47, 13);
            this.cLabel18.TabIndex = 1;
            this.cLabel18.Text = "Kapan";
            this.cLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel18.ToolTips = "";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(167, 6);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(31, 13);
            this.cLabel8.TabIndex = 5;
            this.cLabel8.Text = "Tag";
            this.cLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel8.ToolTips = "";
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
            this.txtKapanName.Location = new System.Drawing.Point(3, 23);
            this.txtKapanName.Name = "txtKapanName";
            this.txtKapanName.SelectAllTextOnFocus = true;
            this.txtKapanName.Size = new System.Drawing.Size(84, 23);
            this.txtKapanName.TabIndex = 2;
            this.txtKapanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKapanName.ToolTips = "";
            this.txtKapanName.WaterMarkText = null;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(88, 6);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(28, 13);
            this.cLabel1.TabIndex = 3;
            this.cLabel1.Text = "Pkt";
            this.cLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel1.ToolTips = "";
            // 
            // txtTag
            // 
            this.txtTag.ActivationColor = true;
            this.txtTag.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtTag.AllowTabKeyOnEnter = false;
            this.txtTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTag.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTag.Format = "";
            this.txtTag.IsComplusory = false;
            this.txtTag.Location = new System.Drawing.Point(167, 23);
            this.txtTag.Name = "txtTag";
            this.txtTag.SelectAllTextOnFocus = true;
            this.txtTag.Size = new System.Drawing.Size(48, 23);
            this.txtTag.TabIndex = 6;
            this.txtTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTag.ToolTips = "";
            this.txtTag.WaterMarkText = null;
            this.txtTag.Validated += new System.EventHandler(this.txtTag_Validated);
            // 
            // txtPacketNo
            // 
            this.txtPacketNo.ActivationColor = true;
            this.txtPacketNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPacketNo.AllowTabKeyOnEnter = false;
            this.txtPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPacketNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacketNo.Format = "#####0";
            this.txtPacketNo.IsComplusory = false;
            this.txtPacketNo.Location = new System.Drawing.Point(88, 23);
            this.txtPacketNo.Name = "txtPacketNo";
            this.txtPacketNo.SelectAllTextOnFocus = true;
            this.txtPacketNo.Size = new System.Drawing.Size(78, 23);
            this.txtPacketNo.TabIndex = 4;
            this.txtPacketNo.Text = "0";
            this.txtPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPacketNo.ToolTips = "";
            this.txtPacketNo.WaterMarkText = null;
            // 
            // PanelJangedNo
            // 
            this.PanelJangedNo.Controls.Add(this.cLabel2);
            this.PanelJangedNo.Controls.Add(this.txtJangedNo);
            this.PanelJangedNo.Location = new System.Drawing.Point(229, 56);
            this.PanelJangedNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PanelJangedNo.Name = "PanelJangedNo";
            this.PanelJangedNo.Size = new System.Drawing.Size(147, 50);
            this.PanelJangedNo.TabIndex = 206;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(3, 7);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(70, 13);
            this.cLabel2.TabIndex = 204;
            this.cLabel2.Text = "JangedNo";
            this.cLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel2.ToolTips = "";
            // 
            // txtJangedNo
            // 
            this.txtJangedNo.ActivationColor = true;
            this.txtJangedNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtJangedNo.AllowTabKeyOnEnter = false;
            this.txtJangedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJangedNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJangedNo.Format = "";
            this.txtJangedNo.IsComplusory = false;
            this.txtJangedNo.Location = new System.Drawing.Point(3, 24);
            this.txtJangedNo.Name = "txtJangedNo";
            this.txtJangedNo.SelectAllTextOnFocus = true;
            this.txtJangedNo.Size = new System.Drawing.Size(139, 23);
            this.txtJangedNo.TabIndex = 205;
            this.txtJangedNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtJangedNo.ToolTips = "";
            this.txtJangedNo.WaterMarkText = null;
            this.txtJangedNo.Validated += new System.EventHandler(this.txtJangedNo_Validated);
            // 
            // PnlDPTJangedno
            // 
            this.PnlDPTJangedno.Controls.Add(this.cLabel14);
            this.PnlDPTJangedno.Controls.Add(this.TxtJangedDate);
            this.PnlDPTJangedno.Controls.Add(this.cLabel13);
            this.PnlDPTJangedno.Controls.Add(this.TxtDPTJangedNo);
            this.PnlDPTJangedno.Location = new System.Drawing.Point(3, 110);
            this.PnlDPTJangedno.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PnlDPTJangedno.Name = "PnlDPTJangedno";
            this.PnlDPTJangedno.Size = new System.Drawing.Size(310, 50);
            this.PnlDPTJangedno.TabIndex = 209;
            // 
            // cLabel14
            // 
            this.cLabel14.AutoSize = true;
            this.cLabel14.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel14.ForeColor = System.Drawing.Color.Black;
            this.cLabel14.Location = new System.Drawing.Point(147, 7);
            this.cLabel14.Name = "cLabel14";
            this.cLabel14.Size = new System.Drawing.Size(87, 13);
            this.cLabel14.TabIndex = 206;
            this.cLabel14.Text = "Janged Date";
            this.cLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel14.ToolTips = "";
            // 
            // TxtJangedDate
            // 
            this.TxtJangedDate.ActivationColor = true;
            this.TxtJangedDate.ActivationColorCode = System.Drawing.Color.Empty;
            this.TxtJangedDate.AllowTabKeyOnEnter = false;
            this.TxtJangedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtJangedDate.Enabled = false;
            this.TxtJangedDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtJangedDate.Format = "";
            this.TxtJangedDate.IsComplusory = false;
            this.TxtJangedDate.Location = new System.Drawing.Point(147, 24);
            this.TxtJangedDate.Name = "TxtJangedDate";
            this.TxtJangedDate.SelectAllTextOnFocus = true;
            this.TxtJangedDate.Size = new System.Drawing.Size(139, 23);
            this.TxtJangedDate.TabIndex = 207;
            this.TxtJangedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtJangedDate.ToolTips = "";
            this.TxtJangedDate.WaterMarkText = null;
            // 
            // cLabel13
            // 
            this.cLabel13.AutoSize = true;
            this.cLabel13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel13.ForeColor = System.Drawing.Color.Black;
            this.cLabel13.Location = new System.Drawing.Point(3, 7);
            this.cLabel13.Name = "cLabel13";
            this.cLabel13.Size = new System.Drawing.Size(95, 13);
            this.cLabel13.TabIndex = 204;
            this.cLabel13.Text = "DPTJangedNo";
            this.cLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel13.ToolTips = "";
            // 
            // TxtDPTJangedNo
            // 
            this.TxtDPTJangedNo.ActivationColor = true;
            this.TxtDPTJangedNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.TxtDPTJangedNo.AllowTabKeyOnEnter = false;
            this.TxtDPTJangedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDPTJangedNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDPTJangedNo.Format = "";
            this.TxtDPTJangedNo.IsComplusory = false;
            this.TxtDPTJangedNo.Location = new System.Drawing.Point(3, 24);
            this.TxtDPTJangedNo.Name = "TxtDPTJangedNo";
            this.TxtDPTJangedNo.SelectAllTextOnFocus = true;
            this.TxtDPTJangedNo.Size = new System.Drawing.Size(139, 23);
            this.TxtDPTJangedNo.TabIndex = 205;
            this.TxtDPTJangedNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtDPTJangedNo.ToolTips = "";
            this.TxtDPTJangedNo.WaterMarkText = null;
            this.TxtDPTJangedNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDPTJangedNo_KeyPress);
            this.TxtDPTJangedNo.Validated += new System.EventHandler(this.TxtDPTJangedNo_Validated);
            // 
            // RbtJangedNo
            // 
            this.RbtJangedNo.AllowTabKeyOnEnter = false;
            this.RbtJangedNo.AutoSize = true;
            this.RbtJangedNo.BackColor = System.Drawing.Color.Transparent;
            this.RbtJangedNo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtJangedNo.ForeColor = System.Drawing.Color.Black;
            this.RbtJangedNo.Location = new System.Drawing.Point(62, 108);
            this.RbtJangedNo.Name = "RbtJangedNo";
            this.RbtJangedNo.Size = new System.Drawing.Size(97, 17);
            this.RbtJangedNo.TabIndex = 158;
            this.RbtJangedNo.Tag = "JANGED NO";
            this.RbtJangedNo.Text = "JANGED NO";
            this.RbtJangedNo.ToolTips = "Display All Over Company Stock";
            this.RbtJangedNo.UseVisualStyleBackColor = false;
            this.RbtJangedNo.CheckedChanged += new System.EventHandler(this.RbtBarcode_CheckedChanged);
            // 
            // lblRoughMak
            // 
            this.lblRoughMak.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRoughMak.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRoughMak.Font = new System.Drawing.Font("Cambria", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoughMak.ForeColor = System.Drawing.Color.Navy;
            this.lblRoughMak.Location = new System.Drawing.Point(0, 0);
            this.lblRoughMak.Name = "lblRoughMak";
            this.lblRoughMak.Size = new System.Drawing.Size(56, 158);
            this.lblRoughMak.TabIndex = 152;
            this.lblRoughMak.Text = "ROUGH";
            this.lblRoughMak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRoughMak.ToolTips = "";
            // 
            // RbtBarcode
            // 
            this.RbtBarcode.AllowTabKeyOnEnter = false;
            this.RbtBarcode.AutoSize = true;
            this.RbtBarcode.BackColor = System.Drawing.Color.Transparent;
            this.RbtBarcode.Checked = true;
            this.RbtBarcode.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtBarcode.ForeColor = System.Drawing.Color.Black;
            this.RbtBarcode.Location = new System.Drawing.Point(62, 8);
            this.RbtBarcode.Name = "RbtBarcode";
            this.RbtBarcode.Size = new System.Drawing.Size(84, 17);
            this.RbtBarcode.TabIndex = 158;
            this.RbtBarcode.TabStop = true;
            this.RbtBarcode.Tag = "BARCODE";
            this.RbtBarcode.Text = "BARCODE";
            this.RbtBarcode.ToolTips = "Display All Over Company Stock";
            this.RbtBarcode.UseVisualStyleBackColor = false;
            this.RbtBarcode.CheckedChanged += new System.EventHandler(this.RbtBarcode_CheckedChanged);
            // 
            // RbtPacketNo
            // 
            this.RbtPacketNo.AllowTabKeyOnEnter = false;
            this.RbtPacketNo.AutoSize = true;
            this.RbtPacketNo.BackColor = System.Drawing.Color.Transparent;
            this.RbtPacketNo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtPacketNo.ForeColor = System.Drawing.Color.Black;
            this.RbtPacketNo.Location = new System.Drawing.Point(62, 83);
            this.RbtPacketNo.Name = "RbtPacketNo";
            this.RbtPacketNo.Size = new System.Drawing.Size(92, 17);
            this.RbtPacketNo.TabIndex = 158;
            this.RbtPacketNo.Tag = "PACKETNO";
            this.RbtPacketNo.Text = "PACKETNO";
            this.RbtPacketNo.ToolTips = "Display All Over Company Stock";
            this.RbtPacketNo.UseVisualStyleBackColor = false;
            this.RbtPacketNo.CheckedChanged += new System.EventHandler(this.RbtBarcode_CheckedChanged);
            // 
            // BtnBestFit
            // 
            this.BtnBestFit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBestFit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBestFit.Appearance.Options.UseFont = true;
            this.BtnBestFit.Appearance.Options.UseForeColor = true;
            this.BtnBestFit.Location = new System.Drawing.Point(223, 9);
            this.BtnBestFit.Name = "BtnBestFit";
            this.BtnBestFit.Size = new System.Drawing.Size(75, 26);
            this.BtnBestFit.TabIndex = 154;
            this.BtnBestFit.TabStop = false;
            this.BtnBestFit.Text = "Best Fit";
            this.BtnBestFit.Click += new System.EventHandler(this.BtnBestFit_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Location = new System.Drawing.Point(301, 9);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 26);
            this.BtnExit.TabIndex = 153;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnKapanLiveStockExcelExport
            // 
            this.BtnKapanLiveStockExcelExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnKapanLiveStockExcelExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnKapanLiveStockExcelExport.Appearance.Options.UseFont = true;
            this.BtnKapanLiveStockExcelExport.Appearance.Options.UseForeColor = true;
            this.BtnKapanLiveStockExcelExport.Location = new System.Drawing.Point(145, 9);
            this.BtnKapanLiveStockExcelExport.Name = "BtnKapanLiveStockExcelExport";
            this.BtnKapanLiveStockExcelExport.Size = new System.Drawing.Size(75, 26);
            this.BtnKapanLiveStockExcelExport.TabIndex = 153;
            this.BtnKapanLiveStockExcelExport.TabStop = false;
            this.BtnKapanLiveStockExcelExport.Text = "Excel";
            this.BtnKapanLiveStockExcelExport.Click += new System.EventHandler(this.BtnKapanLiveStockExcelExport_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.lblPendingsGoods);
            this.panel2.Controls.Add(this.cLabel5);
            this.panel2.Controls.Add(this.lblConfiredGoods);
            this.panel2.Controls.Add(this.cLabel3);
            this.panel2.Controls.Add(this.cLabel6);
            this.panel2.Controls.Add(this.txtSelectedCarat);
            this.panel2.Controls.Add(this.cLabel4);
            this.panel2.Controls.Add(this.txtSelectedPcs);
            this.panel2.Controls.Add(this.txtTotalCarat);
            this.panel2.Controls.Add(this.txtTotalPcs);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 483);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1370, 38);
            this.panel2.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnGroupJanged);
            this.panel3.Controls.Add(this.BtnExit);
            this.panel3.Controls.Add(this.BtnBestFit);
            this.panel3.Controls.Add(this.BtnKapanLiveStockExcelExport);
            this.panel3.Controls.Add(this.lblDefaultLayout);
            this.panel3.Controls.Add(this.lblSaveLayout);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(677, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(693, 38);
            this.panel3.TabIndex = 155;
            // 
            // BtnGroupJanged
            // 
            this.BtnGroupJanged.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnGroupJanged.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnGroupJanged.Appearance.Options.UseFont = true;
            this.BtnGroupJanged.Appearance.Options.UseForeColor = true;
            this.BtnGroupJanged.Location = new System.Drawing.Point(10, 9);
            this.BtnGroupJanged.Name = "BtnGroupJanged";
            this.BtnGroupJanged.Size = new System.Drawing.Size(130, 26);
            this.BtnGroupJanged.TabIndex = 156;
            this.BtnGroupJanged.TabStop = false;
            this.BtnGroupJanged.Text = "Group Janged";
            this.BtnGroupJanged.Click += new System.EventHandler(this.BtnGroupJanged_Click);
            // 
            // lblDefaultLayout
            // 
            this.lblDefaultLayout.AutoSize = true;
            this.lblDefaultLayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDefaultLayout.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultLayout.ForeColor = System.Drawing.Color.Navy;
            this.lblDefaultLayout.Location = new System.Drawing.Point(593, 16);
            this.lblDefaultLayout.Name = "lblDefaultLayout";
            this.lblDefaultLayout.Size = new System.Drawing.Size(97, 13);
            this.lblDefaultLayout.TabIndex = 150;
            this.lblDefaultLayout.Text = "Delete Layout";
            this.lblDefaultLayout.ToolTips = "";
            this.lblDefaultLayout.Click += new System.EventHandler(this.lblDefaultLayout_Click);
            // 
            // lblSaveLayout
            // 
            this.lblSaveLayout.AutoSize = true;
            this.lblSaveLayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSaveLayout.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveLayout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSaveLayout.Location = new System.Drawing.Point(500, 16);
            this.lblSaveLayout.Name = "lblSaveLayout";
            this.lblSaveLayout.Size = new System.Drawing.Size(87, 13);
            this.lblSaveLayout.TabIndex = 150;
            this.lblSaveLayout.Text = "Save Layout";
            this.lblSaveLayout.ToolTips = "";
            this.lblSaveLayout.Click += new System.EventHandler(this.lblSaveLayout_Click);
            // 
            // lblPendingsGoods
            // 
            this.lblPendingsGoods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(240)))));
            this.lblPendingsGoods.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingsGoods.ForeColor = System.Drawing.Color.Navy;
            this.lblPendingsGoods.Location = new System.Drawing.Point(489, 10);
            this.lblPendingsGoods.Name = "lblPendingsGoods";
            this.lblPendingsGoods.Size = new System.Drawing.Size(20, 20);
            this.lblPendingsGoods.TabIndex = 150;
            this.lblPendingsGoods.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Navy;
            this.cLabel5.Location = new System.Drawing.Point(513, 13);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(66, 13);
            this.cLabel5.TabIndex = 150;
            this.cLabel5.Text = "Pendings";
            this.cLabel5.ToolTips = "";
            // 
            // lblConfiredGoods
            // 
            this.lblConfiredGoods.BackColor = System.Drawing.Color.White;
            this.lblConfiredGoods.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfiredGoods.ForeColor = System.Drawing.Color.Navy;
            this.lblConfiredGoods.Location = new System.Drawing.Point(381, 10);
            this.lblConfiredGoods.Name = "lblConfiredGoods";
            this.lblConfiredGoods.Size = new System.Drawing.Size(20, 20);
            this.lblConfiredGoods.TabIndex = 150;
            this.lblConfiredGoods.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Navy;
            this.cLabel3.Location = new System.Drawing.Point(407, 13);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(74, 13);
            this.cLabel3.TabIndex = 150;
            this.cLabel3.Text = "Confirmed";
            this.cLabel3.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Navy;
            this.cLabel6.Location = new System.Drawing.Point(200, 13);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(27, 13);
            this.cLabel6.TabIndex = 150;
            this.cLabel6.Text = "Sel";
            this.cLabel6.ToolTips = "";
            // 
            // txtSelectedCarat
            // 
            this.txtSelectedCarat.ActivationColor = false;
            this.txtSelectedCarat.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSelectedCarat.AllowTabKeyOnEnter = false;
            this.txtSelectedCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtSelectedCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSelectedCarat.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.txtSelectedCarat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtSelectedCarat.Format = "#####0.000";
            this.txtSelectedCarat.IsComplusory = false;
            this.txtSelectedCarat.Location = new System.Drawing.Point(290, 10);
            this.txtSelectedCarat.Name = "txtSelectedCarat";
            this.txtSelectedCarat.ReadOnly = true;
            this.txtSelectedCarat.SelectAllTextOnFocus = true;
            this.txtSelectedCarat.Size = new System.Drawing.Size(84, 20);
            this.txtSelectedCarat.TabIndex = 149;
            this.txtSelectedCarat.TabStop = false;
            this.txtSelectedCarat.Text = "0";
            this.txtSelectedCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSelectedCarat.ToolTips = "";
            this.txtSelectedCarat.WaterMarkText = null;
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Navy;
            this.cLabel4.Location = new System.Drawing.Point(7, 13);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(40, 13);
            this.cLabel4.TabIndex = 150;
            this.cLabel4.Text = "Total";
            this.cLabel4.ToolTips = "";
            // 
            // txtSelectedPcs
            // 
            this.txtSelectedPcs.ActivationColor = false;
            this.txtSelectedPcs.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSelectedPcs.AllowTabKeyOnEnter = false;
            this.txtSelectedPcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtSelectedPcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSelectedPcs.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.txtSelectedPcs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtSelectedPcs.Format = "#####0";
            this.txtSelectedPcs.IsComplusory = false;
            this.txtSelectedPcs.Location = new System.Drawing.Point(227, 10);
            this.txtSelectedPcs.Name = "txtSelectedPcs";
            this.txtSelectedPcs.ReadOnly = true;
            this.txtSelectedPcs.SelectAllTextOnFocus = true;
            this.txtSelectedPcs.Size = new System.Drawing.Size(63, 20);
            this.txtSelectedPcs.TabIndex = 149;
            this.txtSelectedPcs.TabStop = false;
            this.txtSelectedPcs.Text = "0";
            this.txtSelectedPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSelectedPcs.ToolTips = "";
            this.txtSelectedPcs.WaterMarkText = null;
            // 
            // txtTotalCarat
            // 
            this.txtTotalCarat.ActivationColor = false;
            this.txtTotalCarat.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtTotalCarat.AllowTabKeyOnEnter = false;
            this.txtTotalCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalCarat.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.txtTotalCarat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalCarat.Format = "#####0.000";
            this.txtTotalCarat.IsComplusory = false;
            this.txtTotalCarat.Location = new System.Drawing.Point(112, 10);
            this.txtTotalCarat.Name = "txtTotalCarat";
            this.txtTotalCarat.ReadOnly = true;
            this.txtTotalCarat.SelectAllTextOnFocus = true;
            this.txtTotalCarat.Size = new System.Drawing.Size(84, 20);
            this.txtTotalCarat.TabIndex = 149;
            this.txtTotalCarat.TabStop = false;
            this.txtTotalCarat.Text = "0";
            this.txtTotalCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalCarat.ToolTips = "";
            this.txtTotalCarat.WaterMarkText = null;
            // 
            // txtTotalPcs
            // 
            this.txtTotalPcs.ActivationColor = false;
            this.txtTotalPcs.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtTotalPcs.AllowTabKeyOnEnter = false;
            this.txtTotalPcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalPcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalPcs.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.txtTotalPcs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalPcs.Format = "#####0";
            this.txtTotalPcs.IsComplusory = false;
            this.txtTotalPcs.Location = new System.Drawing.Point(49, 10);
            this.txtTotalPcs.Name = "txtTotalPcs";
            this.txtTotalPcs.ReadOnly = true;
            this.txtTotalPcs.SelectAllTextOnFocus = true;
            this.txtTotalPcs.Size = new System.Drawing.Size(63, 20);
            this.txtTotalPcs.TabIndex = 149;
            this.txtTotalPcs.TabStop = false;
            this.txtTotalPcs.Text = "0";
            this.txtTotalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalPcs.ToolTips = "";
            this.txtTotalPcs.WaterMarkText = null;
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
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
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.Row.Options.UseTextOptions = true;
            this.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn144,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn25,
            this.gridColumn7,
            this.gridColumn26,
            this.gridColumn27,
            this.gridColumn28,
            this.gridColumn29,
            this.gridColumn30,
            this.gridColumn31,
            this.gridColumn32,
            this.gridColumn33,
            this.gridColumn34,
            this.gridColumn35,
            this.gridColumn48,
            this.gridColumn49,
            this.gridColumn50,
            this.gridColumn51,
            this.gridColumn52,
            this.gridColumn53,
            this.gridColumn54,
            this.gridColumn55,
            this.gridColumn57,
            this.gridColumn58,
            this.gridColumn56,
            this.gridColumn59,
            this.gridColumn60,
            this.gridColumn61,
            this.gridColumn62,
            this.gridColumn63,
            this.gridColumn64,
            this.gridColumn65,
            this.gridColumn66,
            this.gridColumn36,
            this.gridColumn37,
            this.gridColumn38,
            this.gridColumn40,
            this.gridColumn41,
            this.gridColumn42,
            this.gridColumn43,
            this.gridColumn44,
            this.gridColumn45,
            this.gridColumn46,
            this.gridColumn47,
            this.gridColumn77,
            this.gridColumn83,
            this.gridColumn84,
            this.gridColumn85,
            this.gridColumn90,
            this.gridColumn91,
            this.gridColumn92,
            this.gridColumn93,
            this.gridColumn94,
            this.gridColumn95,
            this.gridColumn96,
            this.gridColumn97,
            this.gridColumn98,
            this.gridColumn99,
            this.gridColumn100,
            this.gridColumn101,
            this.gridColumn102,
            this.gridColumn103,
            this.gridColumn104,
            this.gridColumn105,
            this.gridColumn106,
            this.gridColumn39,
            this.gridColumn67,
            this.gridColumn68,
            this.gridColumn69,
            this.gridColumn70,
            this.gridColumn71,
            this.gridColumn72,
            this.gridColumn73,
            this.gridColumn74,
            this.gridColumn75,
            this.gridColumn76,
            this.gridColumn78,
            this.gridColumn79,
            this.gridColumn80,
            this.gridColumn81,
            this.gridColumn82,
            this.gridColumn86,
            this.gridColumn87,
            this.gridColumn88,
            this.gridColumn89,
            this.gridColumn107,
            this.gridColumn108,
            this.gridColumn109,
            this.gridColumn110,
            this.gridColumn111,
            this.gridColumn112,
            this.gridColumn113,
            this.gridColumn114,
            this.gridColumn115,
            this.gridColumn116,
            this.gridColumn117,
            this.gridColumn118,
            this.gridColumn119,
            this.gridColumn120,
            this.gridColumn121,
            this.gridColumn122,
            this.gridColumn123,
            this.gridColumn124,
            this.gridColumn125,
            this.gridColumn126,
            this.gridColumn127,
            this.gridColumn128,
            this.gridColumn129,
            this.gridColumn130,
            this.gridColumn131,
            this.gridColumn132,
            this.gridColumn133,
            this.gridColumn134,
            this.gridColumn135,
            this.gridColumn136,
            this.gridColumn137,
            this.gridColumn138,
            this.gridColumn139,
            this.gridColumn140,
            this.gridColumn14,
            this.gridColumn141,
            this.gridColumn142,
            this.gridColumn143});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GrdDet_RowStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = "Packet ID";
            this.gridColumn1.FieldName = "PACKET_ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Width = 250;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "Kapan ID";
            this.gridColumn2.FieldName = "KAPAN_ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Width = 250;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.Caption = "Lot";
            this.gridColumn3.FieldName = "KAPANNAME";
            this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 71;
            // 
            // gridColumn144
            // 
            this.gridColumn144.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn144.AppearanceCell.ForeColor = System.Drawing.Color.Navy;
            this.gridColumn144.AppearanceCell.Options.UseFont = true;
            this.gridColumn144.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn144.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn144.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn144.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn144.AppearanceHeader.Options.UseFont = true;
            this.gridColumn144.Caption = "Stn Status";
            this.gridColumn144.FieldName = "PACKETSTATUS";
            this.gridColumn144.Name = "gridColumn144";
            this.gridColumn144.OptionsColumn.AllowEdit = false;
            this.gridColumn144.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn144.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn144.Visible = true;
            this.gridColumn144.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.Caption = "Ownership";
            this.gridColumn4.FieldName = "MAINMANAGERCODE";
            this.gridColumn4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Width = 105;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.Caption = "Manager ID";
            this.gridColumn5.FieldName = "MAINMANAGER_ID";
            this.gridColumn5.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Width = 83;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.Caption = "PktNo";
            this.gridColumn6.FieldName = "PACKETNO";
            this.gridColumn6.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceCell.ForeColor = System.Drawing.Color.Purple;
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.Caption = "Tag";
            this.gridColumn8.FieldName = "TAG";
            this.gridColumn8.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 40;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.Caption = "Barcode";
            this.gridColumn9.FieldName = "BARCODE";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.Caption = "RFIDTAG";
            this.gridColumn10.FieldName = "RFIDTAG";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn11.AppearanceCell.Options.UseFont = true;
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.Caption = "LotPcs";
            this.gridColumn11.FieldName = "LOTPCS";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn11.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn11.Width = 54;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn12.AppearanceCell.Options.UseFont = true;
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn12.AppearanceHeader.Options.UseFont = true;
            this.gridColumn12.Caption = "LotCts";
            this.gridColumn12.FieldName = "LOTCARAT";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
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
            this.gridColumn13.Caption = "BalPcs";
            this.gridColumn13.FieldName = "BALANCEPCS";
            this.gridColumn13.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn13.Width = 61;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn15.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn15.AppearanceHeader.Options.UseFont = true;
            this.gridColumn15.Caption = "SecPcs";
            this.gridColumn15.FieldName = "SECONDPCS";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn15.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn15.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn15.Width = 58;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn16.AppearanceCell.Options.UseFont = true;
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn16.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn16.AppearanceHeader.Options.UseFont = true;
            this.gridColumn16.Caption = "SecCts";
            this.gridColumn16.FieldName = "SECONDCARAT";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn16.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn16.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn16.Width = 78;
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn17.AppearanceCell.Options.UseFont = true;
            this.gridColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn17.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn17.AppearanceHeader.Options.UseFont = true;
            this.gridColumn17.Caption = "LostPcs";
            this.gridColumn17.FieldName = "LOSTPCS";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn17.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn17.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn17.Width = 59;
            // 
            // gridColumn18
            // 
            this.gridColumn18.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn18.AppearanceCell.Options.UseFont = true;
            this.gridColumn18.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn18.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn18.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn18.AppearanceHeader.Options.UseFont = true;
            this.gridColumn18.Caption = "LostCts";
            this.gridColumn18.FieldName = "LOSTCARAT";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn18.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn18.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn19.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gridColumn19.AppearanceCell.Options.UseFont = true;
            this.gridColumn19.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn19.AppearanceHeader.Options.UseFont = true;
            this.gridColumn19.Caption = "Wei(-)";
            this.gridColumn19.FieldName = "LOSSCARAT";
            this.gridColumn19.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn19.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn19.Width = 59;
            // 
            // gridColumn20
            // 
            this.gridColumn20.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn20.AppearanceCell.Options.UseFont = true;
            this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn20.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn20.AppearanceHeader.Options.UseFont = true;
            this.gridColumn20.Caption = "Mix(-/+)";
            this.gridColumn20.FieldName = "MIXINGLESSPLUS";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn20.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // gridColumn21
            // 
            this.gridColumn21.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn21.AppearanceCell.Options.UseFont = true;
            this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn21.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn21.AppearanceHeader.Options.UseFont = true;
            this.gridColumn21.Caption = "Mak Date";
            this.gridColumn21.FieldName = "MAKDATE";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn21.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn21.Width = 121;
            // 
            // gridColumn22
            // 
            this.gridColumn22.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn22.AppearanceCell.Options.UseFont = true;
            this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn22.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn22.AppearanceHeader.Options.UseFont = true;
            this.gridColumn22.Caption = "Pol Date";
            this.gridColumn22.FieldName = "POLDATE";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn22.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn22.Width = 107;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn23.AppearanceCell.Options.UseFont = true;
            this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn23.AppearanceHeader.Options.UseFont = true;
            this.gridColumn23.Caption = "FROMEMPLOYEE_ID";
            this.gridColumn23.FieldName = "FROMEMPLOYEE_ID";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn23.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn24
            // 
            this.gridColumn24.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn24.AppearanceCell.Options.UseFont = true;
            this.gridColumn24.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn24.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn24.AppearanceHeader.Options.UseFont = true;
            this.gridColumn24.Caption = "From Emp";
            this.gridColumn24.FieldName = "FROMEMPLOYEECODE";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.OptionsColumn.AllowEdit = false;
            this.gridColumn24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn24.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 11;
            this.gridColumn24.Width = 88;
            // 
            // gridColumn25
            // 
            this.gridColumn25.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn25.AppearanceCell.Options.UseFont = true;
            this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn25.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn25.AppearanceHeader.Options.UseFont = true;
            this.gridColumn25.Caption = "TOEMPLOYEE_ID";
            this.gridColumn25.FieldName = "TOEMPLOYEE_ID";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.OptionsColumn.AllowEdit = false;
            this.gridColumn25.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn25.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.Caption = "To Emp";
            this.gridColumn7.FieldName = "TOEMPLOYEECODE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 12;
            this.gridColumn7.Width = 78;
            // 
            // gridColumn26
            // 
            this.gridColumn26.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn26.AppearanceCell.Options.UseFont = true;
            this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn26.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn26.AppearanceHeader.Options.UseFont = true;
            this.gridColumn26.Caption = "FROMDEPARTMENT_ID";
            this.gridColumn26.FieldName = "FROMDEPARTMENT_ID";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.OptionsColumn.AllowEdit = false;
            this.gridColumn26.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn26.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn27
            // 
            this.gridColumn27.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn27.AppearanceCell.Options.UseFont = true;
            this.gridColumn27.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn27.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn27.AppearanceHeader.Options.UseFont = true;
            this.gridColumn27.Caption = "From Dept";
            this.gridColumn27.FieldName = "FROMDEPARTMENTNAME";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.OptionsColumn.AllowEdit = false;
            this.gridColumn27.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn27.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn27.Visible = true;
            this.gridColumn27.VisibleIndex = 15;
            this.gridColumn27.Width = 120;
            // 
            // gridColumn28
            // 
            this.gridColumn28.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn28.AppearanceCell.Options.UseFont = true;
            this.gridColumn28.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn28.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn28.AppearanceHeader.Options.UseFont = true;
            this.gridColumn28.Caption = "TODEPARTMENT_ID";
            this.gridColumn28.FieldName = "TODEPARTMENT_ID";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.OptionsColumn.AllowEdit = false;
            this.gridColumn28.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn28.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn29
            // 
            this.gridColumn29.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn29.AppearanceCell.Options.UseFont = true;
            this.gridColumn29.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn29.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn29.AppearanceHeader.Options.UseFont = true;
            this.gridColumn29.Caption = "To Dept";
            this.gridColumn29.FieldName = "TODEPARTMENTNAME";
            this.gridColumn29.Name = "gridColumn29";
            this.gridColumn29.OptionsColumn.AllowEdit = false;
            this.gridColumn29.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn29.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn29.Visible = true;
            this.gridColumn29.VisibleIndex = 16;
            this.gridColumn29.Width = 104;
            // 
            // gridColumn30
            // 
            this.gridColumn30.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn30.AppearanceCell.Options.UseFont = true;
            this.gridColumn30.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn30.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn30.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn30.AppearanceHeader.Options.UseFont = true;
            this.gridColumn30.Caption = "FROMPROCESS_ID";
            this.gridColumn30.FieldName = "FROMPROCESS_ID";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.OptionsColumn.AllowEdit = false;
            this.gridColumn30.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn30.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn31
            // 
            this.gridColumn31.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn31.AppearanceCell.Options.UseFont = true;
            this.gridColumn31.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn31.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn31.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn31.AppearanceHeader.Options.UseFont = true;
            this.gridColumn31.Caption = "From Proc";
            this.gridColumn31.FieldName = "FROMPROCESSNAME";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.OptionsColumn.AllowEdit = false;
            this.gridColumn31.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn31.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 13;
            this.gridColumn31.Width = 125;
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
            this.gridColumn32.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn32.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn33
            // 
            this.gridColumn33.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn33.AppearanceCell.Options.UseFont = true;
            this.gridColumn33.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn33.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn33.AppearanceHeader.Options.UseFont = true;
            this.gridColumn33.Caption = "To Proc";
            this.gridColumn33.FieldName = "TOPROCESSNAME";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.OptionsColumn.AllowEdit = false;
            this.gridColumn33.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn33.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 14;
            this.gridColumn33.Width = 142;
            // 
            // gridColumn34
            // 
            this.gridColumn34.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn34.AppearanceCell.Options.UseFont = true;
            this.gridColumn34.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn34.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn34.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn34.AppearanceHeader.Options.UseFont = true;
            this.gridColumn34.Caption = "NEXTPROCESS_ID";
            this.gridColumn34.FieldName = "NEXTPROCESS_ID";
            this.gridColumn34.Name = "gridColumn34";
            this.gridColumn34.OptionsColumn.AllowEdit = false;
            this.gridColumn34.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn34.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn35
            // 
            this.gridColumn35.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn35.AppearanceCell.Options.UseFont = true;
            this.gridColumn35.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn35.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn35.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn35.AppearanceHeader.Options.UseFont = true;
            this.gridColumn35.Caption = "Required Process";
            this.gridColumn35.FieldName = "NEXTPROCESSNAME";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.OptionsColumn.AllowEdit = false;
            this.gridColumn35.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn35.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn35.Width = 135;
            // 
            // gridColumn48
            // 
            this.gridColumn48.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn48.AppearanceCell.Options.UseFont = true;
            this.gridColumn48.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn48.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn48.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn48.AppearanceHeader.Options.UseFont = true;
            this.gridColumn48.Caption = "Traf Date";
            this.gridColumn48.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt";
            this.gridColumn48.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn48.FieldName = "TRANSDATE";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.OptionsColumn.AllowEdit = false;
            this.gridColumn48.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn48.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 19;
            this.gridColumn48.Width = 115;
            // 
            // gridColumn49
            // 
            this.gridColumn49.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn49.AppearanceCell.Options.UseFont = true;
            this.gridColumn49.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn49.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn49.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn49.AppearanceHeader.Options.UseFont = true;
            this.gridColumn49.Caption = "Traf IP";
            this.gridColumn49.FieldName = "TRANSIP";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.OptionsColumn.AllowEdit = false;
            this.gridColumn49.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn49.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 20;
            this.gridColumn49.Width = 138;
            // 
            // gridColumn50
            // 
            this.gridColumn50.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn50.AppearanceCell.Options.UseFont = true;
            this.gridColumn50.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn50.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn50.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn50.AppearanceHeader.Options.UseFont = true;
            this.gridColumn50.Caption = "Traf By";
            this.gridColumn50.FieldName = "TRANSBYCODE";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.OptionsColumn.AllowEdit = false;
            this.gridColumn50.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn50.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn50.Visible = true;
            this.gridColumn50.VisibleIndex = 21;
            this.gridColumn50.Width = 165;
            // 
            // gridColumn51
            // 
            this.gridColumn51.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn51.AppearanceCell.Options.UseFont = true;
            this.gridColumn51.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn51.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn51.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn51.AppearanceHeader.Options.UseFont = true;
            this.gridColumn51.Caption = "Conf Date";
            this.gridColumn51.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt";
            this.gridColumn51.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn51.FieldName = "CONFDATE";
            this.gridColumn51.Name = "gridColumn51";
            this.gridColumn51.OptionsColumn.AllowEdit = false;
            this.gridColumn51.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn51.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn51.Visible = true;
            this.gridColumn51.VisibleIndex = 22;
            this.gridColumn51.Width = 117;
            // 
            // gridColumn52
            // 
            this.gridColumn52.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn52.AppearanceCell.Options.UseFont = true;
            this.gridColumn52.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn52.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn52.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn52.AppearanceHeader.Options.UseFont = true;
            this.gridColumn52.Caption = "Conf IP";
            this.gridColumn52.FieldName = "CONFIP";
            this.gridColumn52.Name = "gridColumn52";
            this.gridColumn52.OptionsColumn.AllowEdit = false;
            this.gridColumn52.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn52.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn52.Visible = true;
            this.gridColumn52.VisibleIndex = 23;
            this.gridColumn52.Width = 148;
            // 
            // gridColumn53
            // 
            this.gridColumn53.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn53.AppearanceCell.Options.UseFont = true;
            this.gridColumn53.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn53.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn53.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn53.AppearanceHeader.Options.UseFont = true;
            this.gridColumn53.Caption = "Conf By";
            this.gridColumn53.FieldName = "CONFBYCODE";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.OptionsColumn.AllowEdit = false;
            this.gridColumn53.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn53.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 24;
            this.gridColumn53.Width = 163;
            // 
            // gridColumn54
            // 
            this.gridColumn54.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn54.AppearanceCell.Options.UseFont = true;
            this.gridColumn54.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn54.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn54.Caption = "Janged No";
            this.gridColumn54.FieldName = "JANGEDNO";
            this.gridColumn54.Name = "gridColumn54";
            this.gridColumn54.OptionsColumn.AllowEdit = false;
            this.gridColumn54.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn54.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn54.Visible = true;
            this.gridColumn54.VisibleIndex = 8;
            this.gridColumn54.Width = 113;
            // 
            // gridColumn55
            // 
            this.gridColumn55.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn55.AppearanceCell.Options.UseFont = true;
            this.gridColumn55.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn55.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn55.Caption = "Packet Type";
            this.gridColumn55.FieldName = "PACKETTYPE";
            this.gridColumn55.Name = "gridColumn55";
            this.gridColumn55.OptionsColumn.AllowEdit = false;
            this.gridColumn55.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn55.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn55.Width = 112;
            // 
            // gridColumn57
            // 
            this.gridColumn57.Caption = "Main Packet ID";
            this.gridColumn57.FieldName = "MAINPACKET_ID";
            this.gridColumn57.Name = "gridColumn57";
            this.gridColumn57.OptionsColumn.AllowEdit = false;
            this.gridColumn57.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn57.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn57.Width = 250;
            // 
            // gridColumn58
            // 
            this.gridColumn58.Caption = "Parent Packet ID";
            this.gridColumn58.FieldName = "PARENTPACKET_ID";
            this.gridColumn58.Name = "gridColumn58";
            this.gridColumn58.OptionsColumn.AllowEdit = false;
            this.gridColumn58.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn58.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn58.Width = 250;
            // 
            // gridColumn56
            // 
            this.gridColumn56.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn56.AppearanceCell.Options.UseFont = true;
            this.gridColumn56.Caption = "Trn ID";
            this.gridColumn56.FieldName = "TRN_ID";
            this.gridColumn56.Name = "gridColumn56";
            this.gridColumn56.OptionsColumn.AllowEdit = false;
            this.gridColumn56.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn56.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn56.Width = 250;
            // 
            // gridColumn59
            // 
            this.gridColumn59.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn59.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gridColumn59.AppearanceCell.Options.UseFont = true;
            this.gridColumn59.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn59.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn59.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn59.Caption = "Tag";
            this.gridColumn59.FieldName = "PACKETTAG";
            this.gridColumn59.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn59.Name = "gridColumn59";
            this.gridColumn59.OptionsColumn.AllowEdit = false;
            this.gridColumn59.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn59.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn59.Width = 60;
            // 
            // gridColumn60
            // 
            this.gridColumn60.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn60.AppearanceCell.ForeColor = System.Drawing.Color.Purple;
            this.gridColumn60.AppearanceCell.Options.UseFont = true;
            this.gridColumn60.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn60.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn60.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn60.Caption = "MTag";
            this.gridColumn60.FieldName = "MAINPACKETTAG";
            this.gridColumn60.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn60.Name = "gridColumn60";
            this.gridColumn60.OptionsColumn.AllowEdit = false;
            this.gridColumn60.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn60.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn60.Width = 60;
            // 
            // gridColumn61
            // 
            this.gridColumn61.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn61.AppearanceCell.ForeColor = System.Drawing.Color.Teal;
            this.gridColumn61.AppearanceCell.Options.UseFont = true;
            this.gridColumn61.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn61.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn61.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn61.Caption = "PTag";
            this.gridColumn61.FieldName = "PARENTPACKETTAG";
            this.gridColumn61.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn61.Name = "gridColumn61";
            this.gridColumn61.OptionsColumn.AllowEdit = false;
            this.gridColumn61.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn61.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn61.Width = 60;
            // 
            // gridColumn62
            // 
            this.gridColumn62.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn62.AppearanceCell.Options.UseFont = true;
            this.gridColumn62.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn62.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn62.Caption = "Ext Pcs";
            this.gridColumn62.FieldName = "EXTRAPCS";
            this.gridColumn62.Name = "gridColumn62";
            this.gridColumn62.OptionsColumn.AllowEdit = false;
            this.gridColumn62.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn62.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn62.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // gridColumn63
            // 
            this.gridColumn63.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn63.AppearanceCell.Options.UseFont = true;
            this.gridColumn63.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn63.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn63.Caption = "Ext Crt";
            this.gridColumn63.FieldName = "EXTRACARAT";
            this.gridColumn63.Name = "gridColumn63";
            this.gridColumn63.OptionsColumn.AllowEdit = false;
            this.gridColumn63.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn63.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn63.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // gridColumn64
            // 
            this.gridColumn64.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn64.AppearanceCell.Options.UseFont = true;
            this.gridColumn64.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn64.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn64.Caption = "Rej Pcs";
            this.gridColumn64.FieldName = "REJECTIONPCS";
            this.gridColumn64.Name = "gridColumn64";
            this.gridColumn64.OptionsColumn.AllowEdit = false;
            this.gridColumn64.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn64.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn64.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // gridColumn65
            // 
            this.gridColumn65.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn65.AppearanceCell.Options.UseFont = true;
            this.gridColumn65.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn65.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn65.Caption = "Rej Cts";
            this.gridColumn65.FieldName = "REJECTIONCARAT";
            this.gridColumn65.Name = "gridColumn65";
            this.gridColumn65.OptionsColumn.AllowEdit = false;
            this.gridColumn65.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn65.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn65.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // gridColumn66
            // 
            this.gridColumn66.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn66.AppearanceCell.Options.UseFont = true;
            this.gridColumn66.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn66.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn66.Caption = "Entry Type";
            this.gridColumn66.FieldName = "CURR_ENTRYTYPE";
            this.gridColumn66.Name = "gridColumn66";
            this.gridColumn66.OptionsColumn.AllowEdit = false;
            this.gridColumn66.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn66.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn66.Width = 127;
            // 
            // gridColumn36
            // 
            this.gridColumn36.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn36.AppearanceCell.Options.UseFont = true;
            this.gridColumn36.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn36.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn36.Caption = "Shp";
            this.gridColumn36.FieldName = "SHAPECODE";
            this.gridColumn36.Name = "gridColumn36";
            this.gridColumn36.OptionsColumn.AllowEdit = false;
            this.gridColumn36.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn36.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn37
            // 
            this.gridColumn37.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn37.AppearanceCell.Options.UseFont = true;
            this.gridColumn37.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn37.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn37.Caption = "Cla";
            this.gridColumn37.FieldName = "CLARITYCODE";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.OptionsColumn.AllowEdit = false;
            this.gridColumn37.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn37.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn38
            // 
            this.gridColumn38.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn38.AppearanceCell.Options.UseFont = true;
            this.gridColumn38.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn38.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn38.Caption = "Col";
            this.gridColumn38.FieldName = "COLORCODE";
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.OptionsColumn.AllowEdit = false;
            this.gridColumn38.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn38.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn40
            // 
            this.gridColumn40.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn40.AppearanceCell.Options.UseFont = true;
            this.gridColumn40.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn40.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn40.Caption = "Cut";
            this.gridColumn40.FieldName = "CUTCODE";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.OptionsColumn.AllowEdit = false;
            this.gridColumn40.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn40.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn41
            // 
            this.gridColumn41.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn41.AppearanceCell.Options.UseFont = true;
            this.gridColumn41.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn41.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn41.Caption = "Pol";
            this.gridColumn41.FieldName = "POLCODE";
            this.gridColumn41.Name = "gridColumn41";
            this.gridColumn41.OptionsColumn.AllowEdit = false;
            this.gridColumn41.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn41.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn42
            // 
            this.gridColumn42.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn42.AppearanceCell.Options.UseFont = true;
            this.gridColumn42.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn42.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn42.Caption = "Sym";
            this.gridColumn42.FieldName = "SYMCODE";
            this.gridColumn42.Name = "gridColumn42";
            this.gridColumn42.OptionsColumn.AllowEdit = false;
            this.gridColumn42.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn42.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn43
            // 
            this.gridColumn43.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn43.AppearanceCell.Options.UseFont = true;
            this.gridColumn43.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn43.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn43.Caption = "FL";
            this.gridColumn43.FieldName = "FLCODE";
            this.gridColumn43.Name = "gridColumn43";
            this.gridColumn43.OptionsColumn.AllowEdit = false;
            this.gridColumn43.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn43.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn44
            // 
            this.gridColumn44.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn44.AppearanceCell.Options.UseFont = true;
            this.gridColumn44.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn44.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn44.Caption = "Milky";
            this.gridColumn44.FieldName = "MILKYCODE";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.OptionsColumn.AllowEdit = false;
            this.gridColumn44.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn44.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn45
            // 
            this.gridColumn45.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn45.AppearanceCell.Options.UseFont = true;
            this.gridColumn45.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn45.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn45.Caption = "LBLC";
            this.gridColumn45.FieldName = "LBLCCODE";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.OptionsColumn.AllowEdit = false;
            this.gridColumn45.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn45.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn46
            // 
            this.gridColumn46.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn46.AppearanceCell.Options.UseFont = true;
            this.gridColumn46.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn46.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn46.Caption = "Natts";
            this.gridColumn46.FieldName = "NATTSCODE";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.OptionsColumn.AllowEdit = false;
            this.gridColumn46.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn46.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn47
            // 
            this.gridColumn47.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn47.AppearanceCell.Options.UseFont = true;
            this.gridColumn47.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn47.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn47.Caption = "Tension";
            this.gridColumn47.FieldName = "TENSIONCODE";
            this.gridColumn47.Name = "gridColumn47";
            this.gridColumn47.OptionsColumn.AllowEdit = false;
            this.gridColumn47.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn47.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn77
            // 
            this.gridColumn77.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn77.AppearanceCell.Options.UseFont = true;
            this.gridColumn77.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn77.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn77.Caption = "Prd Cts";
            this.gridColumn77.FieldName = "EXPCARAT";
            this.gridColumn77.Name = "gridColumn77";
            this.gridColumn77.OptionsColumn.AllowEdit = false;
            this.gridColumn77.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn77.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn83
            // 
            this.gridColumn83.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn83.AppearanceCell.Options.UseFont = true;
            this.gridColumn83.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn83.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn83.Caption = "GCts";
            this.gridColumn83.FieldName = "GCARAT";
            this.gridColumn83.Name = "gridColumn83";
            this.gridColumn83.OptionsColumn.AllowEdit = false;
            this.gridColumn83.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn83.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn84
            // 
            this.gridColumn84.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.gridColumn84.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn84.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn84.AppearanceCell.Options.UseFont = true;
            this.gridColumn84.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn84.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn84.Caption = "Status";
            this.gridColumn84.FieldName = "STATUS";
            this.gridColumn84.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn84.Name = "gridColumn84";
            this.gridColumn84.OptionsColumn.AllowEdit = false;
            this.gridColumn84.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn84.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn84.Width = 46;
            // 
            // gridColumn85
            // 
            this.gridColumn85.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn85.AppearanceCell.Options.UseFont = true;
            this.gridColumn85.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn85.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn85.Caption = "Main Marker";
            this.gridColumn85.FieldName = "MARKERCODE";
            this.gridColumn85.Name = "gridColumn85";
            this.gridColumn85.OptionsColumn.AllowEdit = false;
            this.gridColumn85.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn85.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn85.Width = 94;
            // 
            // gridColumn90
            // 
            this.gridColumn90.Caption = "FROMEMPLOYEENAME";
            this.gridColumn90.FieldName = "FROMEMPLOYEENAME";
            this.gridColumn90.Name = "gridColumn90";
            this.gridColumn90.OptionsColumn.AllowEdit = false;
            this.gridColumn90.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn91
            // 
            this.gridColumn91.Caption = "TOEMPLOYEENAME";
            this.gridColumn91.FieldName = "TOEMPLOYEENAME";
            this.gridColumn91.Name = "gridColumn91";
            this.gridColumn91.OptionsColumn.AllowEdit = false;
            this.gridColumn91.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn92
            // 
            this.gridColumn92.Caption = "TRANSBYNAME";
            this.gridColumn92.FieldName = "TRANSBYNAME";
            this.gridColumn92.Name = "gridColumn92";
            this.gridColumn92.OptionsColumn.AllowEdit = false;
            this.gridColumn92.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn93
            // 
            this.gridColumn93.Caption = "CONFBYNAME";
            this.gridColumn93.FieldName = "CONFBYNAME";
            this.gridColumn93.Name = "gridColumn93";
            this.gridColumn93.OptionsColumn.AllowEdit = false;
            this.gridColumn93.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn94
            // 
            this.gridColumn94.Caption = "MARKERNAME";
            this.gridColumn94.FieldName = "MARKERNAME";
            this.gridColumn94.Name = "gridColumn94";
            this.gridColumn94.OptionsColumn.AllowEdit = false;
            this.gridColumn94.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn95
            // 
            this.gridColumn95.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn95.AppearanceCell.Options.UseFont = true;
            this.gridColumn95.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn95.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn95.Caption = "Last Issue";
            this.gridColumn95.FieldName = "ISSUECODE";
            this.gridColumn95.Name = "gridColumn95";
            this.gridColumn95.OptionsColumn.AllowEdit = false;
            this.gridColumn95.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn96
            // 
            this.gridColumn96.Caption = "ISSUENAME";
            this.gridColumn96.FieldName = "ISSUENAME";
            this.gridColumn96.Name = "gridColumn96";
            this.gridColumn96.OptionsColumn.AllowEdit = false;
            this.gridColumn96.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn97
            // 
            this.gridColumn97.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn97.AppearanceCell.Options.UseFont = true;
            this.gridColumn97.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn97.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn97.Caption = "Last2Last";
            this.gridColumn97.FieldName = "PREVISSUECODE";
            this.gridColumn97.Name = "gridColumn97";
            this.gridColumn97.OptionsColumn.AllowEdit = false;
            this.gridColumn97.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn98
            // 
            this.gridColumn98.Caption = "PREVISSUENAME";
            this.gridColumn98.FieldName = "PREVISSUENAME";
            this.gridColumn98.Name = "gridColumn98";
            this.gridColumn98.OptionsColumn.AllowEdit = false;
            this.gridColumn98.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn99
            // 
            this.gridColumn99.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridColumn99.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn99.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn99.AppearanceCell.Options.UseFont = true;
            this.gridColumn99.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn99.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn99.Caption = "Age";
            this.gridColumn99.DisplayFormat.FormatString = "{0:N0}";
            this.gridColumn99.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn99.FieldName = "STOCKDAYS";
            this.gridColumn99.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn99.Name = "gridColumn99";
            this.gridColumn99.OptionsColumn.AllowEdit = false;
            this.gridColumn99.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn99.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "STOCKDAYS", "{0:N0}")});
            this.gridColumn99.Visible = true;
            this.gridColumn99.VisibleIndex = 39;
            this.gridColumn99.Width = 48;
            // 
            // gridColumn100
            // 
            this.gridColumn100.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn100.AppearanceCell.Options.UseFont = true;
            this.gridColumn100.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn100.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn100.Caption = "Work. Code";
            this.gridColumn100.FieldName = "WORKERCODE";
            this.gridColumn100.Name = "gridColumn100";
            this.gridColumn100.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn101
            // 
            this.gridColumn101.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn101.AppearanceCell.Options.UseFont = true;
            this.gridColumn101.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn101.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn101.Caption = "Work.Name";
            this.gridColumn101.FieldName = "WORKERNAME";
            this.gridColumn101.Name = "gridColumn101";
            this.gridColumn101.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn102
            // 
            this.gridColumn102.Caption = "POLISHFINALEMPLOYEE_ID";
            this.gridColumn102.FieldName = "POLISHFINALEMPLOYEE_ID";
            this.gridColumn102.Name = "gridColumn102";
            this.gridColumn102.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn103
            // 
            this.gridColumn103.Caption = "POLISHFINALEMPLOYEENAME";
            this.gridColumn103.FieldName = "POLISHFINALEMPLOYEENAME";
            this.gridColumn103.Name = "gridColumn103";
            this.gridColumn103.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn104
            // 
            this.gridColumn104.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn104.AppearanceCell.ForeColor = System.Drawing.Color.DarkGreen;
            this.gridColumn104.AppearanceCell.Options.UseFont = true;
            this.gridColumn104.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn104.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn104.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn104.Caption = "Pol Fin EmpCode";
            this.gridColumn104.FieldName = "POLISHFINALEMPLOYEECODE";
            this.gridColumn104.Name = "gridColumn104";
            this.gridColumn104.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn105
            // 
            this.gridColumn105.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn105.AppearanceCell.ForeColor = System.Drawing.Color.DarkGreen;
            this.gridColumn105.AppearanceCell.Options.UseFont = true;
            this.gridColumn105.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn105.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn105.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn105.Caption = "Pol Fin Carat";
            this.gridColumn105.FieldName = "POLISHFINALREADYCARAT";
            this.gridColumn105.Name = "gridColumn105";
            this.gridColumn105.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn106
            // 
            this.gridColumn106.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn106.AppearanceCell.ForeColor = System.Drawing.Color.DarkGreen;
            this.gridColumn106.AppearanceCell.Options.UseFont = true;
            this.gridColumn106.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn106.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn106.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn106.Caption = "Pol Fin Date";
            this.gridColumn106.FieldName = "POLISHFINALENTRYDATE";
            this.gridColumn106.Name = "gridColumn106";
            this.gridColumn106.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn39
            // 
            this.gridColumn39.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn39.AppearanceCell.Options.UseFont = true;
            this.gridColumn39.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn39.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn39.Caption = "From Manager ID";
            this.gridColumn39.FieldName = "FROMMANAGER_ID";
            this.gridColumn39.Name = "gridColumn39";
            this.gridColumn39.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn67
            // 
            this.gridColumn67.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn67.AppearanceCell.Options.UseFont = true;
            this.gridColumn67.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn67.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn67.Caption = "From Manager";
            this.gridColumn67.FieldName = "FROMMANAGERCODE";
            this.gridColumn67.Name = "gridColumn67";
            this.gridColumn67.OptionsColumn.AllowEdit = false;
            this.gridColumn67.Visible = true;
            this.gridColumn67.VisibleIndex = 17;
            this.gridColumn67.Width = 102;
            // 
            // gridColumn68
            // 
            this.gridColumn68.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn68.AppearanceCell.Options.UseFont = true;
            this.gridColumn68.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn68.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn68.Caption = "From Manager Name";
            this.gridColumn68.FieldName = "FROMMANAGERNAME";
            this.gridColumn68.Name = "gridColumn68";
            this.gridColumn68.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn69
            // 
            this.gridColumn69.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn69.AppearanceCell.Options.UseFont = true;
            this.gridColumn69.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn69.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn69.Caption = "To Manager ID";
            this.gridColumn69.FieldName = "TOMANAGER_ID";
            this.gridColumn69.Name = "gridColumn69";
            this.gridColumn69.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn70
            // 
            this.gridColumn70.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn70.AppearanceCell.Options.UseFont = true;
            this.gridColumn70.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn70.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn70.Caption = "To Manager";
            this.gridColumn70.FieldName = "TOMANAGERCODE";
            this.gridColumn70.Name = "gridColumn70";
            this.gridColumn70.OptionsColumn.AllowEdit = false;
            this.gridColumn70.Visible = true;
            this.gridColumn70.VisibleIndex = 18;
            this.gridColumn70.Width = 83;
            // 
            // gridColumn71
            // 
            this.gridColumn71.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn71.AppearanceCell.Options.UseFont = true;
            this.gridColumn71.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn71.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn71.Caption = "To Manager Name";
            this.gridColumn71.FieldName = "TOMANAGERNAME";
            this.gridColumn71.Name = "gridColumn71";
            this.gridColumn71.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn72
            // 
            this.gridColumn72.Caption = "GIASHAPECODE";
            this.gridColumn72.FieldName = "GIASHAPECODE";
            this.gridColumn72.Name = "gridColumn72";
            this.gridColumn72.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn73
            // 
            this.gridColumn73.Caption = "GIACLARITYCODE";
            this.gridColumn73.FieldName = "GIACLARITYCODE";
            this.gridColumn73.Name = "gridColumn73";
            this.gridColumn73.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn74
            // 
            this.gridColumn74.Caption = "GIACOLORCODE";
            this.gridColumn74.FieldName = "GIACOLORCODE";
            this.gridColumn74.Name = "gridColumn74";
            this.gridColumn74.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn75
            // 
            this.gridColumn75.Caption = "GIACUTCODE";
            this.gridColumn75.FieldName = "GIACUTCODE";
            this.gridColumn75.Name = "gridColumn75";
            this.gridColumn75.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn76
            // 
            this.gridColumn76.Caption = "GIAPOLCODE";
            this.gridColumn76.FieldName = "GIAPOLCODE";
            this.gridColumn76.Name = "gridColumn76";
            this.gridColumn76.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn78
            // 
            this.gridColumn78.Caption = "GIASYMCODE";
            this.gridColumn78.FieldName = "GIASYMCODE";
            this.gridColumn78.Name = "gridColumn78";
            this.gridColumn78.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn79
            // 
            this.gridColumn79.Caption = "GIAFLCODE";
            this.gridColumn79.FieldName = "GIAFLCODE";
            this.gridColumn79.Name = "gridColumn79";
            this.gridColumn79.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn80
            // 
            this.gridColumn80.Caption = "GIAREPORTNO";
            this.gridColumn80.FieldName = "GIAREPORTNO";
            this.gridColumn80.Name = "gridColumn80";
            this.gridColumn80.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn81
            // 
            this.gridColumn81.Caption = "GIACONTROLNO";
            this.gridColumn81.FieldName = "GIACONTROLNO";
            this.gridColumn81.Name = "gridColumn81";
            this.gridColumn81.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn82
            // 
            this.gridColumn82.Caption = "GIAAMOUNT";
            this.gridColumn82.FieldName = "GIAAMOUNT";
            this.gridColumn82.Name = "gridColumn82";
            this.gridColumn82.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn86
            // 
            this.gridColumn86.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn86.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn86.Caption = "Exp Cts";
            this.gridColumn86.FieldName = "EXPWEIGHT";
            this.gridColumn86.Name = "gridColumn86";
            this.gridColumn86.OptionsColumn.AllowEdit = false;
            this.gridColumn86.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn87
            // 
            this.gridColumn87.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn87.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn87.Caption = "Grp JangedNo";
            this.gridColumn87.FieldName = "GRPJANGEDNO";
            this.gridColumn87.Name = "gridColumn87";
            this.gridColumn87.OptionsColumn.AllowEdit = false;
            this.gridColumn87.Width = 88;
            // 
            // gridColumn88
            // 
            this.gridColumn88.Caption = "EntryType";
            this.gridColumn88.FieldName = "ENTRYTYPE";
            this.gridColumn88.Name = "gridColumn88";
            this.gridColumn88.OptionsColumn.AllowEdit = false;
            this.gridColumn88.Visible = true;
            this.gridColumn88.VisibleIndex = 7;
            this.gridColumn88.Width = 95;
            // 
            // gridColumn89
            // 
            this.gridColumn89.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn89.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn89.Caption = "Shape";
            this.gridColumn89.FieldName = "SHAPENAME";
            this.gridColumn89.Name = "gridColumn89";
            this.gridColumn89.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn107
            // 
            this.gridColumn107.Caption = "Manager Name";
            this.gridColumn107.FieldName = "MAINMANAGERNAME";
            this.gridColumn107.MinWidth = 21;
            this.gridColumn107.Name = "gridColumn107";
            this.gridColumn107.OptionsColumn.AllowEdit = false;
            this.gridColumn107.Width = 81;
            // 
            // gridColumn108
            // 
            this.gridColumn108.Caption = "TempMarker_ID";
            this.gridColumn108.FieldName = "TEMPMARKER_ID";
            this.gridColumn108.Name = "gridColumn108";
            this.gridColumn108.OptionsColumn.AllowEdit = false;
            this.gridColumn108.Width = 64;
            // 
            // gridColumn109
            // 
            this.gridColumn109.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn109.AppearanceCell.Options.UseFont = true;
            this.gridColumn109.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn109.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn109.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn109.AppearanceHeader.Options.UseFont = true;
            this.gridColumn109.Caption = "Group";
            this.gridColumn109.FieldName = "PACKETGROUPNAME";
            this.gridColumn109.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn109.Name = "gridColumn109";
            this.gridColumn109.OptionsColumn.AllowEdit = false;
            this.gridColumn109.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn109.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn110
            // 
            this.gridColumn110.Caption = "Group_ID";
            this.gridColumn110.FieldName = "PACKETGROUP_ID";
            this.gridColumn110.Name = "gridColumn110";
            this.gridColumn110.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn111
            // 
            this.gridColumn111.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn111.AppearanceCell.Options.UseFont = true;
            this.gridColumn111.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn111.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn111.Caption = "PktSrno";
            this.gridColumn111.FieldName = "PKTSERIALNO";
            this.gridColumn111.Name = "gridColumn111";
            this.gridColumn111.OptionsColumn.AllowEdit = false;
            this.gridColumn111.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn111.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn111.Visible = true;
            this.gridColumn111.VisibleIndex = 5;
            this.gridColumn111.Width = 65;
            // 
            // gridColumn112
            // 
            this.gridColumn112.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn112.AppearanceCell.Options.UseFont = true;
            this.gridColumn112.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn112.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn112.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn112.AppearanceHeader.Options.UseFont = true;
            this.gridColumn112.Caption = "Grade";
            this.gridColumn112.FieldName = "PACKETGRADENAME";
            this.gridColumn112.Name = "gridColumn112";
            this.gridColumn112.OptionsColumn.AllowEdit = false;
            this.gridColumn112.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn112.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn113
            // 
            this.gridColumn113.Caption = "Party_Id";
            this.gridColumn113.FieldName = "PARTY_ID";
            this.gridColumn113.Name = "gridColumn113";
            this.gridColumn113.OptionsColumn.AllowEdit = false;
            this.gridColumn113.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn114
            // 
            this.gridColumn114.Caption = "PartyCode";
            this.gridColumn114.FieldName = "PARTYCODE";
            this.gridColumn114.Name = "gridColumn114";
            this.gridColumn114.OptionsColumn.AllowEdit = false;
            this.gridColumn114.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn114.Visible = true;
            this.gridColumn114.VisibleIndex = 25;
            // 
            // gridColumn115
            // 
            this.gridColumn115.Caption = "PartyName";
            this.gridColumn115.FieldName = "PARTYNAME";
            this.gridColumn115.Name = "gridColumn115";
            this.gridColumn115.OptionsColumn.AllowEdit = false;
            this.gridColumn115.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn116
            // 
            this.gridColumn116.Caption = "InwardNo";
            this.gridColumn116.FieldName = "INWARDNO";
            this.gridColumn116.Name = "gridColumn116";
            this.gridColumn116.OptionsColumn.AllowEdit = false;
            this.gridColumn116.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn116.Visible = true;
            this.gridColumn116.VisibleIndex = 26;
            // 
            // gridColumn117
            // 
            this.gridColumn117.Caption = "InwardDate";
            this.gridColumn117.FieldName = "INWARDDATE";
            this.gridColumn117.Name = "gridColumn117";
            this.gridColumn117.OptionsColumn.AllowEdit = false;
            this.gridColumn117.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn117.Visible = true;
            this.gridColumn117.VisibleIndex = 27;
            // 
            // gridColumn118
            // 
            this.gridColumn118.Caption = "TotalAge";
            this.gridColumn118.FieldName = "TOTALAGE";
            this.gridColumn118.Name = "gridColumn118";
            this.gridColumn118.OptionsColumn.AllowEdit = false;
            this.gridColumn118.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn118.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max)});
            this.gridColumn118.Visible = true;
            this.gridColumn118.VisibleIndex = 35;
            // 
            // gridColumn119
            // 
            this.gridColumn119.Caption = "Download";
            this.gridColumn119.ColumnEdit = this.ChkDownload;
            this.gridColumn119.FieldName = "DOWNLOAD";
            this.gridColumn119.Name = "gridColumn119";
            this.gridColumn119.OptionsColumn.AllowEdit = false;
            this.gridColumn119.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn119.Visible = true;
            this.gridColumn119.VisibleIndex = 31;
            // 
            // ChkDownload
            // 
            this.ChkDownload.AutoHeight = false;
            this.ChkDownload.Caption = "Check";
            this.ChkDownload.Name = "ChkDownload";
            this.ChkDownload.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.ChkDownload.PictureChecked = global::AxoneMFGRJ.Properties.Resources.Checked;
            this.ChkDownload.PictureUnchecked = global::AxoneMFGRJ.Properties.Resources.Unchecked;
            // 
            // gridColumn120
            // 
            this.gridColumn120.Caption = "Complete";
            this.gridColumn120.ColumnEdit = this.repChkComplete;
            this.gridColumn120.FieldName = "COMPLETE";
            this.gridColumn120.Name = "gridColumn120";
            this.gridColumn120.OptionsColumn.AllowEdit = false;
            this.gridColumn120.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn120.Visible = true;
            this.gridColumn120.VisibleIndex = 32;
            // 
            // repChkComplete
            // 
            this.repChkComplete.AutoHeight = false;
            this.repChkComplete.Caption = "Check";
            this.repChkComplete.Name = "repChkComplete";
            this.repChkComplete.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkComplete.PictureChecked = global::AxoneMFGRJ.Properties.Resources.Checked;
            this.repChkComplete.PictureUnchecked = global::AxoneMFGRJ.Properties.Resources.Unchecked;
            // 
            // gridColumn121
            // 
            this.gridColumn121.Caption = "Reject";
            this.gridColumn121.ColumnEdit = this.repChkReject;
            this.gridColumn121.FieldName = "REJECT";
            this.gridColumn121.Name = "gridColumn121";
            this.gridColumn121.OptionsColumn.AllowEdit = false;
            this.gridColumn121.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn121.Visible = true;
            this.gridColumn121.VisibleIndex = 34;
            // 
            // repChkReject
            // 
            this.repChkReject.AutoHeight = false;
            this.repChkReject.Caption = "Check";
            this.repChkReject.Name = "repChkReject";
            this.repChkReject.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkReject.PictureChecked = global::AxoneMFGRJ.Properties.Resources.Checked;
            this.repChkReject.PictureUnchecked = global::AxoneMFGRJ.Properties.Resources.Checked;
            // 
            // gridColumn122
            // 
            this.gridColumn122.Caption = "Cancel";
            this.gridColumn122.ColumnEdit = this.repChkCancel;
            this.gridColumn122.FieldName = "CANCEL";
            this.gridColumn122.Name = "gridColumn122";
            this.gridColumn122.OptionsColumn.AllowEdit = false;
            this.gridColumn122.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn122.Visible = true;
            this.gridColumn122.VisibleIndex = 33;
            // 
            // repChkCancel
            // 
            this.repChkCancel.AutoHeight = false;
            this.repChkCancel.Caption = "Check";
            this.repChkCancel.Name = "repChkCancel";
            this.repChkCancel.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkCancel.PictureChecked = global::AxoneMFGRJ.Properties.Resources.Checked;
            this.repChkCancel.PictureUnchecked = global::AxoneMFGRJ.Properties.Resources.Unchecked;
            // 
            // gridColumn123
            // 
            this.gridColumn123.Caption = "UPLOADEXT";
            this.gridColumn123.FieldName = "UPLOADEXT";
            this.gridColumn123.Name = "gridColumn123";
            this.gridColumn123.OptionsColumn.AllowEdit = false;
            this.gridColumn123.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn124
            // 
            this.gridColumn124.Caption = "DOWNLOADEXT";
            this.gridColumn124.FieldName = "DOWNLOADEXT";
            this.gridColumn124.Name = "gridColumn124";
            this.gridColumn124.OptionsColumn.AllowEdit = false;
            this.gridColumn124.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn125
            // 
            this.gridColumn125.Caption = "UPLOADSERVERPATH";
            this.gridColumn125.FieldName = "UPLOADSERVERPATH";
            this.gridColumn125.Name = "gridColumn125";
            this.gridColumn125.OptionsColumn.AllowEdit = false;
            this.gridColumn125.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn126
            // 
            this.gridColumn126.Caption = "UPLOADFILEPATH";
            this.gridColumn126.FieldName = "UPLOADFILEPATH";
            this.gridColumn126.Name = "gridColumn126";
            this.gridColumn126.OptionsColumn.AllowEdit = false;
            this.gridColumn126.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn127
            // 
            this.gridColumn127.Caption = "UPLOADSERVERUSERNAME";
            this.gridColumn127.FieldName = "UPLOADSERVERUSERNAME";
            this.gridColumn127.Name = "gridColumn127";
            this.gridColumn127.OptionsColumn.AllowEdit = false;
            this.gridColumn127.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn128
            // 
            this.gridColumn128.Caption = "UPLOADSERVERPASSWORD";
            this.gridColumn128.FieldName = "UPLOADSERVERPASSWORD";
            this.gridColumn128.Name = "gridColumn128";
            this.gridColumn128.OptionsColumn.AllowEdit = false;
            this.gridColumn128.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn129
            // 
            this.gridColumn129.Caption = "DOWNLOADSERVERPATH";
            this.gridColumn129.FieldName = "DOWNLOADSERVERPATH";
            this.gridColumn129.Name = "gridColumn129";
            this.gridColumn129.OptionsColumn.AllowEdit = false;
            this.gridColumn129.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn130
            // 
            this.gridColumn130.Caption = "DOWNLOADFILEPATH";
            this.gridColumn130.FieldName = "DOWNLOADFILEPATH";
            this.gridColumn130.Name = "gridColumn130";
            this.gridColumn130.OptionsColumn.AllowEdit = false;
            this.gridColumn130.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn131
            // 
            this.gridColumn131.Caption = "DOWNLOADSERVERUSERNAME";
            this.gridColumn131.FieldName = "DOWNLOADSERVERUSERNAME";
            this.gridColumn131.Name = "gridColumn131";
            this.gridColumn131.OptionsColumn.AllowEdit = false;
            this.gridColumn131.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn132
            // 
            this.gridColumn132.Caption = "DOWNLOADSERVERPASSWORD";
            this.gridColumn132.FieldName = "DOWNLOADSERVERPASSWORD";
            this.gridColumn132.Name = "gridColumn132";
            this.gridColumn132.OptionsColumn.AllowEdit = false;
            this.gridColumn132.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn133
            // 
            this.gridColumn133.Caption = "REJECT_ID";
            this.gridColumn133.FieldName = "REJECT_ID";
            this.gridColumn133.Name = "gridColumn133";
            this.gridColumn133.OptionsColumn.AllowEdit = false;
            this.gridColumn133.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn134
            // 
            this.gridColumn134.Caption = "Reason";
            this.gridColumn134.FieldName = "REASON";
            this.gridColumn134.Name = "gridColumn134";
            this.gridColumn134.OptionsColumn.AllowEdit = false;
            this.gridColumn134.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn134.Visible = true;
            this.gridColumn134.VisibleIndex = 29;
            this.gridColumn134.Width = 141;
            // 
            // gridColumn135
            // 
            this.gridColumn135.Caption = "Reject Reason";
            this.gridColumn135.FieldName = "REJECTREASON";
            this.gridColumn135.Name = "gridColumn135";
            this.gridColumn135.OptionsColumn.AllowEdit = false;
            this.gridColumn135.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn135.Visible = true;
            this.gridColumn135.VisibleIndex = 30;
            this.gridColumn135.Width = 182;
            // 
            // gridColumn136
            // 
            this.gridColumn136.Caption = "MACHINE_ID";
            this.gridColumn136.FieldName = "MACHINE_ID";
            this.gridColumn136.Name = "gridColumn136";
            this.gridColumn136.OptionsColumn.AllowEdit = false;
            this.gridColumn136.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn137
            // 
            this.gridColumn137.Caption = "Machine";
            this.gridColumn137.FieldName = "MACHINENAME";
            this.gridColumn137.Name = "gridColumn137";
            this.gridColumn137.OptionsColumn.AllowEdit = false;
            this.gridColumn137.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn137.Visible = true;
            this.gridColumn137.VisibleIndex = 10;
            // 
            // gridColumn138
            // 
            this.gridColumn138.Caption = "Cts";
            this.gridColumn138.FieldName = "CARAT";
            this.gridColumn138.Name = "gridColumn138";
            this.gridColumn138.OptionsColumn.AllowEdit = false;
            this.gridColumn138.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn138.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn138.Visible = true;
            this.gridColumn138.VisibleIndex = 6;
            this.gridColumn138.Width = 57;
            // 
            // gridColumn139
            // 
            this.gridColumn139.Caption = "ReturnType";
            this.gridColumn139.FieldName = "RETURNTYPE";
            this.gridColumn139.Name = "gridColumn139";
            this.gridColumn139.OptionsColumn.AllowEdit = false;
            this.gridColumn139.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn139.Visible = true;
            this.gridColumn139.VisibleIndex = 28;
            // 
            // gridColumn140
            // 
            this.gridColumn140.Caption = "ClientRefNo";
            this.gridColumn140.FieldName = "CLIENTREFNO";
            this.gridColumn140.Name = "gridColumn140";
            this.gridColumn140.OptionsColumn.AllowEdit = false;
            this.gridColumn140.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn140.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn140.Width = 81;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "DEPTJangedNo";
            this.gridColumn14.FieldName = "DEPTJANGEDNO";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 9;
            this.gridColumn14.Width = 105;
            // 
            // gridColumn141
            // 
            this.gridColumn141.Caption = "Local Upl Path";
            this.gridColumn141.FieldName = "LOCALOUTPUTPATH";
            this.gridColumn141.Name = "gridColumn141";
            this.gridColumn141.OptionsColumn.AllowEdit = false;
            this.gridColumn141.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn141.Visible = true;
            this.gridColumn141.VisibleIndex = 36;
            this.gridColumn141.Width = 97;
            // 
            // gridColumn142
            // 
            this.gridColumn142.Caption = "Local Upl UserName";
            this.gridColumn142.FieldName = "LOCALOUTPUTPATHUSERNAME";
            this.gridColumn142.Name = "gridColumn142";
            this.gridColumn142.OptionsColumn.AllowEdit = false;
            this.gridColumn142.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn142.Visible = true;
            this.gridColumn142.VisibleIndex = 37;
            this.gridColumn142.Width = 130;
            // 
            // gridColumn143
            // 
            this.gridColumn143.Caption = "Local Upl Pwd";
            this.gridColumn143.FieldName = "LOCALOUTPUTPATHPASSWORD";
            this.gridColumn143.Name = "gridColumn143";
            this.gridColumn143.OptionsColumn.AllowEdit = false;
            this.gridColumn143.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn143.Visible = true;
            this.gridColumn143.VisibleIndex = 38;
            this.gridColumn143.Width = 105;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 158);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ChkDownload,
            this.repChkCancel,
            this.repChkReject,
            this.repChkComplete});
            this.MainGrid.Size = new System.Drawing.Size(1370, 325);
            this.MainGrid.TabIndex = 19;
            this.MainGrid.ToolTipController = this.toolTipController1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            this.MainGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainGrid_KeyUp);
            // 
            // PanelProgress
            // 
            this.PanelProgress.BackColor = System.Drawing.Color.White;
            this.PanelProgress.Controls.Add(this.lblMessage2);
            this.PanelProgress.Controls.Add(this.progressPanel1);
            this.PanelProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PanelProgress.Location = new System.Drawing.Point(492, 187);
            this.PanelProgress.Name = "PanelProgress";
            this.PanelProgress.Size = new System.Drawing.Size(384, 107);
            this.PanelProgress.TabIndex = 21;
            this.PanelProgress.TabStop = false;
            this.PanelProgress.Visible = false;
            // 
            // lblMessage2
            // 
            this.lblMessage2.BackColor = System.Drawing.Color.Black;
            this.lblMessage2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage2.ForeColor = System.Drawing.Color.White;
            this.lblMessage2.Location = new System.Drawing.Point(1, 61);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(384, 46);
            this.lblMessage2.TabIndex = 153;
            this.lblMessage2.Text = "Message";
            this.lblMessage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressPanel1
            // 
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.Appearance.Options.UseFont = true;
            this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 12F);
            this.progressPanel1.AppearanceCaption.Options.UseFont = true;
            this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.progressPanel1.AppearanceDescription.Options.UseFont = true;
            this.progressPanel1.BarAnimationElementThickness = 2;
            this.progressPanel1.Location = new System.Drawing.Point(109, 14);
            this.progressPanel1.LookAndFeel.SkinName = "Office 2007 Blue";
            this.progressPanel1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.ShowDescription = false;
            this.progressPanel1.Size = new System.Drawing.Size(151, 38);
            this.progressPanel1.TabIndex = 152;
            this.progressPanel1.Text = "progressPanel1";
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // GrpReject
            // 
            this.GrpReject.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.GrpReject.AppearanceCaption.Options.UseFont = true;
            this.GrpReject.Controls.Add(this.txtRemark);
            this.GrpReject.Controls.Add(this.cLabel11);
            this.GrpReject.Controls.Add(this.txtReason);
            this.GrpReject.Controls.Add(this.cLabel12);
            this.GrpReject.Controls.Add(this.BtnRejectReason);
            this.GrpReject.Controls.Add(this.BtnRejectClose);
            this.GrpReject.Location = new System.Drawing.Point(503, 300);
            this.GrpReject.Name = "GrpReject";
            this.GrpReject.Size = new System.Drawing.Size(351, 172);
            this.GrpReject.TabIndex = 184;
            this.GrpReject.Text = "Please Select Reason";
            this.GrpReject.Visible = false;
            // 
            // txtRemark
            // 
            this.txtRemark.ActivationColor = true;
            this.txtRemark.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtRemark.AllowTabKeyOnEnter = false;
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Format = "";
            this.txtRemark.IsComplusory = false;
            this.txtRemark.Location = new System.Drawing.Point(5, 113);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.SelectAllTextOnFocus = true;
            this.txtRemark.Size = new System.Drawing.Size(339, 23);
            this.txtRemark.TabIndex = 155;
            this.txtRemark.ToolTips = "";
            this.txtRemark.WaterMarkText = null;
            // 
            // cLabel11
            // 
            this.cLabel11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel11.ForeColor = System.Drawing.Color.Black;
            this.cLabel11.Location = new System.Drawing.Point(5, 86);
            this.cLabel11.Name = "cLabel11";
            this.cLabel11.Size = new System.Drawing.Size(184, 24);
            this.cLabel11.TabIndex = 154;
            this.cLabel11.Text = "Remark";
            this.cLabel11.ToolTips = "";
            // 
            // txtReason
            // 
            this.txtReason.ActivationColor = true;
            this.txtReason.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtReason.AllowTabKeyOnEnter = false;
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Format = "";
            this.txtReason.IsComplusory = false;
            this.txtReason.Location = new System.Drawing.Point(5, 60);
            this.txtReason.Name = "txtReason";
            this.txtReason.SelectAllTextOnFocus = true;
            this.txtReason.Size = new System.Drawing.Size(339, 23);
            this.txtReason.TabIndex = 153;
            this.txtReason.ToolTips = "";
            this.txtReason.WaterMarkText = null;
            this.txtReason.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReason_KeyPress);
            // 
            // cLabel12
            // 
            this.cLabel12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel12.ForeColor = System.Drawing.Color.Black;
            this.cLabel12.Location = new System.Drawing.Point(5, 33);
            this.cLabel12.Name = "cLabel12";
            this.cLabel12.Size = new System.Drawing.Size(184, 24);
            this.cLabel12.TabIndex = 13;
            this.cLabel12.Text = "Reason";
            this.cLabel12.ToolTips = "";
            // 
            // BtnRejectReason
            // 
            this.BtnRejectReason.Appearance.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRejectReason.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnRejectReason.Appearance.Options.UseFont = true;
            this.BtnRejectReason.Appearance.Options.UseForeColor = true;
            this.BtnRejectReason.Location = new System.Drawing.Point(120, 140);
            this.BtnRejectReason.Name = "BtnRejectReason";
            this.BtnRejectReason.Size = new System.Drawing.Size(103, 25);
            this.BtnRejectReason.TabIndex = 1;
            this.BtnRejectReason.Text = "Ok";
            this.BtnRejectReason.Click += new System.EventHandler(this.BtnRejectReason_Click);
            // 
            // BtnRejectClose
            // 
            this.BtnRejectClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.BtnRejectClose.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.BtnRejectClose.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnRejectClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnRejectClose.Appearance.Options.UseBackColor = true;
            this.BtnRejectClose.Appearance.Options.UseFont = true;
            this.BtnRejectClose.Appearance.Options.UseForeColor = true;
            this.BtnRejectClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BtnRejectClose.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.BtnRejectClose.Location = new System.Drawing.Point(310, 0);
            this.BtnRejectClose.Name = "BtnRejectClose";
            this.BtnRejectClose.Size = new System.Drawing.Size(38, 34);
            this.BtnRejectClose.TabIndex = 12;
            // 
            // BkgQCImport
            // 
            this.BkgQCImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BkgQCImport_DoWork);
            this.BkgQCImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BkgQCImport_RunWorkerCompleted);
            // 
            // FrmSingleGoodsReturnLiveStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 521);
            this.Controls.Add(this.GrpReject);
            this.Controls.Add(this.PanelProgress);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PanelHeader);
            this.Name = "FrmSingleGoodsReturnLiveStock";
            this.Text = "SINGLE GOODS RETURN";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmKapanDashboard_KeyDown);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.PanelBarcode.ResumeLayout(false);
            this.PanelBarcode.PerformLayout();
            this.PanelPktSerialNo.ResumeLayout(false);
            this.PanelPktSerialNo.PerformLayout();
            this.PanelPacketNo.ResumeLayout(false);
            this.PanelPacketNo.PerformLayout();
            this.PanelJangedNo.ResumeLayout(false);
            this.PanelJangedNo.PerformLayout();
            this.PnlDPTJangedno.ResumeLayout(false);
            this.PnlDPTJangedno.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkComplete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkReject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.PanelProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrpReject)).EndInit();
            this.GrpReject.ResumeLayout(false);
            this.GrpReject.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel PanelHeader;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cTextBox txtTotalPcs;
        private AxonContLib.cTextBox txtTotalCarat;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cTextBox txtSelectedCarat;
        private AxonContLib.cTextBox txtSelectedPcs;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel18;
        private AxonContLib.cTextBox txtTag;
        private AxonContLib.cTextBox txtPacketNo;
        private AxonContLib.cTextBox txtKapanName;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnKapanLiveStockExcelExport;
        private AxonContLib.cLabel lblConfiredGoods;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel lblPendingsGoods;
        private AxonContLib.cLabel cLabel5;
        private DevExpress.XtraEditors.SimpleButton BtnBestFit;
        private AxonContLib.cLabel lblRoughMak;
        private System.Windows.Forms.RadioButton RbtOtherStock;
        private AxonContLib.cRadioButton RbtFullStock;
        private AxonContLib.cRadioButton RbtMYStock;
        private AxonContLib.cRadioButton RbtDeptStock;
        private AxonContLib.cPanel panel3;
        private DevExpress.XtraEditors.SimpleButton BtnAckPending;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private AxonContLib.cLabel lblSaveLayout;
        private AxonContLib.cLabel lblDefaultLayout;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtJangedNo;
        private DevExpress.XtraEditors.SimpleButton BtnGroupJanged;
        private AxonContLib.cFlowLayoutPanel flowLayoutPanel1;
        private AxonContLib.cLabel cLabel7;
        private AxonContLib.cTextBox txtBarcode;
        private AxonContLib.cRadioButton RbtJangedNo;
        private AxonContLib.cRadioButton RbtPacketNo;
        private AxonContLib.cRadioButton RbtBarcode;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cPanel PanelJangedNo;
        private AxonContLib.cPanel PanelPacketNo;
        private AxonContLib.cPanel PanelBarcode;
        private AxonContLib.cFlowLayoutPanel flowLayoutPanel2;
        private AxonContLib.cRadioButton RbtPktSerialNo;
        private AxonContLib.cPanel PanelPktSerialNo;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cTextBox txtSrNoKapanName;
        private AxonContLib.cLabel cLabel10;
        private AxonContLib.cTextBox txtSrNoSerialNo;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn52;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn55;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn58;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn56;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn59;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn60;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn61;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn62;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn63;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn64;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn65;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn66;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn42;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn43;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn47;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn77;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn83;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn84;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn85;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn90;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn91;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn92;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn93;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn94;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn95;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn96;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn97;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn98;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn99;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn100;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn101;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn102;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn103;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn104;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn105;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn106;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn67;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn68;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn69;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn70;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn71;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn72;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn73;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn74;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn75;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn76;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn78;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn79;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn80;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn81;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn82;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn86;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn87;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn88;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn89;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn107;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn108;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn109;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn110;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn111;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn112;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn113;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn114;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn115;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn116;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn117;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn118;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn119;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn120;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn121;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn122;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn123;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn124;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn125;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn126;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn127;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn128;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn129;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn130;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn131;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn132;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn133;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn134;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn135;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn136;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn137;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn138;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn139;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn140;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraEditors.SimpleButton BtnDownload;
        private DevExpress.XtraEditors.SimpleButton BtnComplete;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnReject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtEmployee;
        private System.Windows.Forms.Panel panel4;
        private AxonContLib.cCheckBox ChkGrpJangedNo;
        private AxonContLib.cCheckBox ChkWithExtraStock;
        private System.Windows.Forms.GroupBox PanelProgress;
        private System.Windows.Forms.Label lblMessage2;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        
        private DevExpress.XtraEditors.GroupControl GrpReject;
        private AxonContLib.cTextBox txtRemark;
        private AxonContLib.cLabel cLabel11;
        private AxonContLib.cTextBox txtReason;
        private AxonContLib.cLabel cLabel12;
        private DevExpress.XtraEditors.SimpleButton BtnRejectReason;
        private DevExpress.XtraEditors.SimpleButton BtnRejectClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ChkDownload;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkComplete;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkReject;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkCancel;
        private DevExpress.XtraWaitForm.ProgressPanel PanelProgress1;
        private AxonContLib.cPanel PnlDPTJangedno;
        private AxonContLib.cLabel cLabel13;
        private AxonContLib.cTextBox TxtDPTJangedNo;
        private AxonContLib.cLabel cLabel14;
        private AxonContLib.cTextBox TxtJangedDate;
        private AxonContLib.cRadioButton RbtnDPTJangedNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn141;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn142;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn143;
        private DevExpress.XtraEditors.SimpleButton BtnQCImportData;
        private System.ComponentModel.BackgroundWorker BkgQCImport;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanelQCImport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn144;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraEditors.SimpleButton btnShowComplete;
    }
}