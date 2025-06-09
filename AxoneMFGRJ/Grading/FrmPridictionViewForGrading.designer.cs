namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmPridictionViewForGrading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPridictionViewForGrading));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.PanelHeader = new AxonContLib.cPanel(this.components);
            this.panel3 = new AxonContLib.cPanel(this.components);
            this.PanleProgress = new DevExpress.XtraWaitForm.ProgressPanel();
            this.txtStoneNo = new AxonContLib.cTextBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.txtYear = new AxonContLib.cTextBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel7 = new AxonContLib.cLabel(this.components);
            this.txtFromPacketNo = new AxonContLib.cTextBox(this.components);
            this.txtToPacketNo = new AxonContLib.cTextBox(this.components);
            this.txtTag = new AxonContLib.cTextBox(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.CmbPrdType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel14 = new AxonContLib.cLabel(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.RbtPktNotCreated = new AxonContLib.cRadioButton(this.components);
            this.RbtPktCreated = new AxonContLib.cRadioButton(this.components);
            this.RbtAll = new AxonContLib.cRadioButton(this.components);
            this.cLabel13 = new AxonContLib.cLabel(this.components);
            this.txtEmployee = new AxonContLib.cTextBox(this.components);
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.BandGeneral = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.lblMessage = new AxonContLib.cLabel(this.components);
            this.panel2 = new AxonContLib.cPanel(this.components);
            this.lblDefaultLayout = new AxonContLib.cLabel(this.components);
            this.BtnKapanLiveStockExcelExport = new DevExpress.XtraEditors.SimpleButton();
            this.lblSaveLayout = new AxonContLib.cLabel(this.components);
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PanelHeader.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbPrdType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelHeader.Controls.Add(this.panel3);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1264, 83);
            this.PanelHeader.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.PanleProgress);
            this.panel3.Controls.Add(this.txtStoneNo);
            this.panel3.Controls.Add(this.cLabel4);
            this.panel3.Controls.Add(this.txtYear);
            this.panel3.Controls.Add(this.cLabel1);
            this.panel3.Controls.Add(this.cLabel3);
            this.panel3.Controls.Add(this.BtnExit);
            this.panel3.Controls.Add(this.cLabel2);
            this.panel3.Controls.Add(this.cLabel6);
            this.panel3.Controls.Add(this.BtnSearch);
            this.panel3.Controls.Add(this.cLabel7);
            this.panel3.Controls.Add(this.txtFromPacketNo);
            this.panel3.Controls.Add(this.txtToPacketNo);
            this.panel3.Controls.Add(this.txtTag);
            this.panel3.Controls.Add(this.DTPToDate);
            this.panel3.Controls.Add(this.CmbPrdType);
            this.panel3.Controls.Add(this.DTPFromDate);
            this.panel3.Controls.Add(this.CmbKapan);
            this.panel3.Controls.Add(this.cLabel14);
            this.panel3.Controls.Add(this.cLabel8);
            this.panel3.Controls.Add(this.cLabel9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1263, 81);
            this.panel3.TabIndex = 0;
            // 
            // PanleProgress
            // 
            this.PanleProgress.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.PanleProgress.Appearance.Options.UseBackColor = true;
            this.PanleProgress.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PanleProgress.AppearanceCaption.Options.UseFont = true;
            this.PanleProgress.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.PanleProgress.AppearanceDescription.Options.UseFont = true;
            this.PanleProgress.BarAnimationElementThickness = 2;
            this.PanleProgress.Location = new System.Drawing.Point(1045, 6);
            this.PanleProgress.Name = "PanleProgress";
            this.PanleProgress.Size = new System.Drawing.Size(171, 35);
            this.PanleProgress.TabIndex = 157;
            this.PanleProgress.Text = "progressPanel1";
            this.PanleProgress.Visible = false;
            // 
            // txtStoneNo
            // 
            this.txtStoneNo.ActivationColor = false;
            this.txtStoneNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtStoneNo.AllowTabKeyOnEnter = false;
            this.txtStoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStoneNo.Format = "";
            this.txtStoneNo.IsComplusory = false;
            this.txtStoneNo.Location = new System.Drawing.Point(628, 36);
            this.txtStoneNo.Multiline = true;
            this.txtStoneNo.Name = "txtStoneNo";
            this.txtStoneNo.SelectAllTextOnFocus = true;
            this.txtStoneNo.Size = new System.Drawing.Size(249, 40);
            this.txtStoneNo.TabIndex = 156;
            this.txtStoneNo.ToolTips = "";
            this.txtStoneNo.WaterMarkText = null;
            this.txtStoneNo.TextChanged += new System.EventHandler(this.txtStoneNo_TextChanged);
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(550, 43);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(65, 13);
            this.cLabel4.TabIndex = 155;
            this.cLabel4.Text = "Stone No";
            this.cLabel4.ToolTips = "";
            // 
            // txtYear
            // 
            this.txtYear.ActivationColor = true;
            this.txtYear.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtYear.AllowTabKeyOnEnter = false;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtYear.Format = "######";
            this.txtYear.IsComplusory = false;
            this.txtYear.Location = new System.Drawing.Point(429, 37);
            this.txtYear.MaxLength = 6;
            this.txtYear.Name = "txtYear";
            this.txtYear.SelectAllTextOnFocus = true;
            this.txtYear.Size = new System.Drawing.Size(115, 24);
            this.txtYear.TabIndex = 13;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.ToolTips = "";
            this.txtYear.WaterMarkText = null;
            this.txtYear.Validated += new System.EventHandler(this.txtYear_Validated);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(3, 13);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(47, 13);
            this.cLabel1.TabIndex = 0;
            this.cLabel1.Text = "Kapan";
            this.cLabel1.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(310, 13);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(28, 13);
            this.cLabel3.TabIndex = 2;
            this.cLabel3.Text = "Pkt";
            this.cLabel3.ToolTips = "";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExit.ImageOptions.SvgImage")));
            this.BtnExit.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExit.Location = new System.Drawing.Point(966, 6);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 32);
            this.BtnExit.TabIndex = 153;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(412, 13);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(23, 13);
            this.cLabel2.TabIndex = 4;
            this.cLabel2.Text = "To";
            this.cLabel2.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(3, 43);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(40, 13);
            this.cLabel6.TabIndex = 10;
            this.cLabel6.Text = "Prdic";
            this.cLabel6.ToolTips = "";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSearch.ImageOptions.SvgImage")));
            this.BtnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSearch.Location = new System.Drawing.Point(883, 6);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(81, 32);
            this.BtnSearch.TabIndex = 20;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // cLabel7
            // 
            this.cLabel7.AutoSize = true;
            this.cLabel7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel7.ForeColor = System.Drawing.Color.Black;
            this.cLabel7.Location = new System.Drawing.Point(506, 13);
            this.cLabel7.Name = "cLabel7";
            this.cLabel7.Size = new System.Drawing.Size(31, 13);
            this.cLabel7.TabIndex = 6;
            this.cLabel7.Text = "Tag";
            this.cLabel7.ToolTips = "";
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
            this.txtFromPacketNo.Location = new System.Drawing.Point(343, 8);
            this.txtFromPacketNo.Name = "txtFromPacketNo";
            this.txtFromPacketNo.SelectAllTextOnFocus = true;
            this.txtFromPacketNo.Size = new System.Drawing.Size(63, 22);
            this.txtFromPacketNo.TabIndex = 3;
            this.txtFromPacketNo.Text = "0";
            this.txtFromPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFromPacketNo.ToolTips = "";
            this.txtFromPacketNo.WaterMarkText = null;
            // 
            // txtToPacketNo
            // 
            this.txtToPacketNo.ActivationColor = true;
            this.txtToPacketNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtToPacketNo.AllowTabKeyOnEnter = false;
            this.txtToPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToPacketNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPacketNo.Format = "######";
            this.txtToPacketNo.IsComplusory = false;
            this.txtToPacketNo.Location = new System.Drawing.Point(437, 8);
            this.txtToPacketNo.Name = "txtToPacketNo";
            this.txtToPacketNo.SelectAllTextOnFocus = true;
            this.txtToPacketNo.Size = new System.Drawing.Size(63, 22);
            this.txtToPacketNo.TabIndex = 5;
            this.txtToPacketNo.Text = "0";
            this.txtToPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtToPacketNo.ToolTips = "";
            this.txtToPacketNo.WaterMarkText = null;
            // 
            // txtTag
            // 
            this.txtTag.ActivationColor = true;
            this.txtTag.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtTag.AllowTabKeyOnEnter = false;
            this.txtTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTag.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTag.Format = "";
            this.txtTag.IsComplusory = false;
            this.txtTag.Location = new System.Drawing.Point(538, 8);
            this.txtTag.Name = "txtTag";
            this.txtTag.SelectAllTextOnFocus = true;
            this.txtTag.Size = new System.Drawing.Size(45, 22);
            this.txtTag.TabIndex = 7;
            this.txtTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTag.ToolTips = "";
            this.txtTag.WaterMarkText = null;
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(754, 6);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.ShowCheckBox = true;
            this.DTPToDate.Size = new System.Drawing.Size(123, 24);
            this.DTPToDate.TabIndex = 16;
            this.DTPToDate.ToolTips = "";
            // 
            // CmbPrdType
            // 
            this.CmbPrdType.Location = new System.Drawing.Point(52, 38);
            this.CmbPrdType.Name = "CmbPrdType";
            this.CmbPrdType.Properties.AllowMultiSelect = true;
            this.CmbPrdType.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbPrdType.Properties.Appearance.Options.UseFont = true;
            this.CmbPrdType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbPrdType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbPrdType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbPrdType.Properties.DropDownRows = 20;
            this.CmbPrdType.Properties.IncrementalSearch = true;
            this.CmbPrdType.Size = new System.Drawing.Size(292, 20);
            this.CmbPrdType.TabIndex = 11;
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(628, 6);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.ShowCheckBox = true;
            this.DTPFromDate.Size = new System.Drawing.Size(122, 24);
            this.DTPFromDate.TabIndex = 15;
            this.DTPFromDate.ToolTips = "";
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(52, 8);
            this.CmbKapan.Name = "CmbKapan";
            this.CmbKapan.Properties.AllowMultiSelect = true;
            this.CmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.Appearance.Options.UseFont = true;
            this.CmbKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbKapan.Properties.DropDownRows = 20;
            this.CmbKapan.Properties.IncrementalSearch = true;
            this.CmbKapan.Size = new System.Drawing.Size(255, 20);
            this.CmbKapan.TabIndex = 1;
            // 
            // cLabel14
            // 
            this.cLabel14.AutoSize = true;
            this.cLabel14.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel14.ForeColor = System.Drawing.Color.Black;
            this.cLabel14.Location = new System.Drawing.Point(475, 64);
            this.cLabel14.Name = "cLabel14";
            this.cLabel14.Size = new System.Drawing.Size(69, 12);
            this.cLabel14.TabIndex = 12;
            this.cLabel14.Text = "i.e. 201904";
            this.cLabel14.ToolTips = "";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(350, 43);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(80, 13);
            this.cLabel8.TabIndex = 12;
            this.cLabel8.Text = "Year Month";
            this.cLabel8.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(587, 12);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(37, 13);
            this.cLabel9.TabIndex = 14;
            this.cLabel9.Text = "Date";
            this.cLabel9.ToolTips = "";
            // 
            // RbtPktNotCreated
            // 
            this.RbtPktNotCreated.AllowTabKeyOnEnter = false;
            this.RbtPktNotCreated.AutoSize = true;
            this.RbtPktNotCreated.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtPktNotCreated.ForeColor = System.Drawing.Color.Black;
            this.RbtPktNotCreated.Location = new System.Drawing.Point(753, 231);
            this.RbtPktNotCreated.Name = "RbtPktNotCreated";
            this.RbtPktNotCreated.Size = new System.Drawing.Size(102, 17);
            this.RbtPktNotCreated.TabIndex = 156;
            this.RbtPktNotCreated.Text = "Not Created";
            this.RbtPktNotCreated.ToolTips = "";
            this.RbtPktNotCreated.UseVisualStyleBackColor = true;
            this.RbtPktNotCreated.Visible = false;
            // 
            // RbtPktCreated
            // 
            this.RbtPktCreated.AllowTabKeyOnEnter = false;
            this.RbtPktCreated.AutoSize = true;
            this.RbtPktCreated.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtPktCreated.ForeColor = System.Drawing.Color.Black;
            this.RbtPktCreated.Location = new System.Drawing.Point(648, 231);
            this.RbtPktCreated.Name = "RbtPktCreated";
            this.RbtPktCreated.Size = new System.Drawing.Size(101, 17);
            this.RbtPktCreated.TabIndex = 155;
            this.RbtPktCreated.Text = "Pkt Created";
            this.RbtPktCreated.ToolTips = "";
            this.RbtPktCreated.UseVisualStyleBackColor = true;
            this.RbtPktCreated.Visible = false;
            // 
            // RbtAll
            // 
            this.RbtAll.AllowTabKeyOnEnter = false;
            this.RbtAll.AutoSize = true;
            this.RbtAll.Checked = true;
            this.RbtAll.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtAll.ForeColor = System.Drawing.Color.Black;
            this.RbtAll.Location = new System.Drawing.Point(600, 231);
            this.RbtAll.Name = "RbtAll";
            this.RbtAll.Size = new System.Drawing.Size(42, 17);
            this.RbtAll.TabIndex = 154;
            this.RbtAll.TabStop = true;
            this.RbtAll.Text = "All";
            this.RbtAll.ToolTips = "";
            this.RbtAll.UseVisualStyleBackColor = true;
            this.RbtAll.Visible = false;
            // 
            // cLabel13
            // 
            this.cLabel13.AutoSize = true;
            this.cLabel13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel13.ForeColor = System.Drawing.Color.Black;
            this.cLabel13.Location = new System.Drawing.Point(167, 236);
            this.cLabel13.Name = "cLabel13";
            this.cLabel13.Size = new System.Drawing.Size(35, 13);
            this.cLabel13.TabIndex = 16;
            this.cLabel13.Text = "Emp";
            this.cLabel13.ToolTips = "";
            this.cLabel13.Visible = false;
            // 
            // txtEmployee
            // 
            this.txtEmployee.ActivationColor = true;
            this.txtEmployee.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtEmployee.AllowTabKeyOnEnter = false;
            this.txtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployee.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtEmployee.Format = "";
            this.txtEmployee.IsComplusory = false;
            this.txtEmployee.Location = new System.Drawing.Point(216, 231);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.SelectAllTextOnFocus = true;
            this.txtEmployee.Size = new System.Drawing.Size(332, 22);
            this.txtEmployee.TabIndex = 17;
            this.txtEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmployee.ToolTips = "";
            this.txtEmployee.Visible = false;
            this.txtEmployee.WaterMarkText = null;
            this.txtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployee_KeyPress);
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
            serializableAppearanceObject2.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject2.Options.UseFont = true;
            serializableAppearanceObject2.Options.UseForeColor = true;
            serializableAppearanceObject2.Options.UseTextOptions = true;
            serializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject3.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject3.Options.UseFont = true;
            serializableAppearanceObject3.Options.UseForeColor = true;
            serializableAppearanceObject3.Options.UseTextOptions = true;
            serializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject4.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject4.Options.UseFont = true;
            serializableAppearanceObject4.Options.UseForeColor = true;
            serializableAppearanceObject4.Options.UseTextOptions = true;
            serializableAppearanceObject4.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 83);
            this.MainGrid.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1264, 347);
            this.MainGrid.TabIndex = 19;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            this.MainGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.MainGrid_Paint);
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.BandPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.BandPanel.Options.UseFont = true;
            this.GrdDet.Appearance.BandPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.BandPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.BandPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.BandPanelRowHeight = 25;
            this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.BandGeneral});
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDet.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.GrdDet_CustomDrawCell);
            this.GrdDet.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GrdDet_RowCellStyle);
            this.GrdDet.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.GrdDet_CustomSummaryCalculate);
            this.GrdDet.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDet_CustomColumnDisplayText);
            // 
            // BandGeneral
            // 
            this.BandGeneral.Caption = "General";
            this.BandGeneral.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.BandGeneral.Name = "BandGeneral";
            this.BandGeneral.VisibleIndex = 0;
            this.BandGeneral.Width = 71;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMessage.Location = new System.Drawing.Point(12, 15);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(63, 13);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message";
            this.lblMessage.ToolTips = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblDefaultLayout);
            this.panel2.Controls.Add(this.BtnKapanLiveStockExcelExport);
            this.panel2.Controls.Add(this.lblSaveLayout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(931, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(333, 43);
            this.panel2.TabIndex = 154;
            // 
            // lblDefaultLayout
            // 
            this.lblDefaultLayout.AutoSize = true;
            this.lblDefaultLayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDefaultLayout.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultLayout.ForeColor = System.Drawing.Color.Navy;
            this.lblDefaultLayout.Location = new System.Drawing.Point(102, 15);
            this.lblDefaultLayout.Name = "lblDefaultLayout";
            this.lblDefaultLayout.Size = new System.Drawing.Size(97, 13);
            this.lblDefaultLayout.TabIndex = 155;
            this.lblDefaultLayout.Text = "Delete Layout";
            this.lblDefaultLayout.ToolTips = "";
            this.lblDefaultLayout.Click += new System.EventHandler(this.lblDefaultLayout_Click);
            // 
            // BtnKapanLiveStockExcelExport
            // 
            this.BtnKapanLiveStockExcelExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnKapanLiveStockExcelExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnKapanLiveStockExcelExport.Appearance.Options.UseFont = true;
            this.BtnKapanLiveStockExcelExport.Appearance.Options.UseForeColor = true;
            this.BtnKapanLiveStockExcelExport.Location = new System.Drawing.Point(214, 5);
            this.BtnKapanLiveStockExcelExport.Name = "BtnKapanLiveStockExcelExport";
            this.BtnKapanLiveStockExcelExport.Size = new System.Drawing.Size(111, 32);
            this.BtnKapanLiveStockExcelExport.TabIndex = 153;
            this.BtnKapanLiveStockExcelExport.TabStop = false;
            this.BtnKapanLiveStockExcelExport.Text = "Excel Export";
            this.BtnKapanLiveStockExcelExport.Click += new System.EventHandler(this.BtnKapanLiveStockExcelExport_Click);
            // 
            // lblSaveLayout
            // 
            this.lblSaveLayout.AutoSize = true;
            this.lblSaveLayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSaveLayout.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveLayout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSaveLayout.Location = new System.Drawing.Point(9, 15);
            this.lblSaveLayout.Name = "lblSaveLayout";
            this.lblSaveLayout.Size = new System.Drawing.Size(87, 13);
            this.lblSaveLayout.TabIndex = 156;
            this.lblSaveLayout.Text = "Save Layout";
            this.lblSaveLayout.ToolTips = "";
            this.lblSaveLayout.Click += new System.EventHandler(this.lblSaveLayout_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 430);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 43);
            this.panel1.TabIndex = 22;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted_1);
            // 
            // FrmPridictionViewForGrading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 473);
            this.Controls.Add(this.RbtPktNotCreated);
            this.Controls.Add(this.cLabel13);
            this.Controls.Add(this.RbtPktCreated);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.RbtAll);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PanelHeader);
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmPridictionViewForGrading";
            this.Text = "PRIDICTION VIEW FOR GRADING";
            this.PanelHeader.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbPrdType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxonContLib.cPanel PanelHeader;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private AxonContLib.cTextBox txtToPacketNo;
        private AxonContLib.cTextBox txtFromPacketNo;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cLabel cLabel3;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private AxonContLib.cTextBox txtTag;
        private AxonContLib.cLabel cLabel7;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BandGeneral;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbPrdType;
        private AxonContLib.cLabel cLabel6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cPanel panel3;
        private AxonContLib.cLabel cLabel13;
        private AxonContLib.cTextBox txtEmployee;
        private AxonContLib.cTextBox txtYear;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel cLabel14;
        private AxonContLib.cRadioButton RbtPktNotCreated;
        private AxonContLib.cRadioButton RbtPktCreated;
        private AxonContLib.cRadioButton RbtAll;
        private AxonContLib.cLabel lblMessage;
        private AxonContLib.cPanel panel2;
        private AxonContLib.cLabel lblDefaultLayout;
        private DevExpress.XtraEditors.SimpleButton BtnKapanLiveStockExcelExport;
        private AxonContLib.cLabel lblSaveLayout;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cTextBox txtStoneNo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraWaitForm.ProgressPanel PanleProgress;


    }
}