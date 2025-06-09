namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmFullKapanAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFullKapanAnalysis));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.BtnDirectPDFExport = new DevExpress.XtraEditors.SimpleButton();
            this.RbtPktNotCreated = new AxonContLib.cRadioButton(this.components);
            this.RbtPktCreated = new AxonContLib.cRadioButton(this.components);
            this.RbtAll = new AxonContLib.cRadioButton(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.ChkWithPCN = new AxonContLib.cCheckBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.RdbGrdType = new DevExpress.XtraEditors.RadioGroup();
            this.txtMultiMainManager = new AxonContLib.cTextBox(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.RbtFullKapanAnalysis = new System.Windows.Forms.RadioButton();
            this.RbtKapanEstimate = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.ChkCmbPacketGroup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.chkCmbPacketCat = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RdbGrdType.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbPacketGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCmbPacketCat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSearch.ImageOptions.SvgImage")));
            this.BtnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSearch.Location = new System.Drawing.Point(434, 44);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(142, 32);
            this.BtnSearch.TabIndex = 0;
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
            this.BtnExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExit.ImageOptions.SvgImage")));
            this.BtnExit.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExit.Location = new System.Drawing.Point(735, 44);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 32);
            this.BtnExit.TabIndex = 153;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // CmbKapan
            // 
            this.CmbKapan.EditValue = "";
            this.CmbKapan.Location = new System.Drawing.Point(28, 49);
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
            this.CmbKapan.Size = new System.Drawing.Size(400, 20);
            this.CmbKapan.TabIndex = 1;
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
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(25, 31);
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
            this.BtnDirectPDFExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDirectPDFExport.ImageOptions.SvgImage")));
            this.BtnDirectPDFExport.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnDirectPDFExport.Location = new System.Drawing.Point(577, 44);
            this.BtnDirectPDFExport.Name = "BtnDirectPDFExport";
            this.BtnDirectPDFExport.Size = new System.Drawing.Size(152, 32);
            this.BtnDirectPDFExport.TabIndex = 0;
            this.BtnDirectPDFExport.Text = "Direct .Pdf Export";
            this.BtnDirectPDFExport.Click += new System.EventHandler(this.BtnDirectPDFExport_Click);
            // 
            // RbtPktNotCreated
            // 
            this.RbtPktNotCreated.AllowTabKeyOnEnter = false;
            this.RbtPktNotCreated.AutoSize = true;
            this.RbtPktNotCreated.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtPktNotCreated.ForeColor = System.Drawing.Color.Black;
            this.RbtPktNotCreated.Location = new System.Drawing.Point(181, 123);
            this.RbtPktNotCreated.Name = "RbtPktNotCreated";
            this.RbtPktNotCreated.Size = new System.Drawing.Size(102, 17);
            this.RbtPktNotCreated.TabIndex = 154;
            this.RbtPktNotCreated.Text = "Not Created";
            this.RbtPktNotCreated.ToolTips = "";
            this.RbtPktNotCreated.UseVisualStyleBackColor = true;
            // 
            // RbtPktCreated
            // 
            this.RbtPktCreated.AllowTabKeyOnEnter = false;
            this.RbtPktCreated.AutoSize = true;
            this.RbtPktCreated.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtPktCreated.ForeColor = System.Drawing.Color.Black;
            this.RbtPktCreated.Location = new System.Drawing.Point(76, 123);
            this.RbtPktCreated.Name = "RbtPktCreated";
            this.RbtPktCreated.Size = new System.Drawing.Size(101, 17);
            this.RbtPktCreated.TabIndex = 155;
            this.RbtPktCreated.Text = "Pkt Created";
            this.RbtPktCreated.ToolTips = "";
            this.RbtPktCreated.UseVisualStyleBackColor = true;
            // 
            // RbtAll
            // 
            this.RbtAll.AllowTabKeyOnEnter = false;
            this.RbtAll.AutoSize = true;
            this.RbtAll.Checked = true;
            this.RbtAll.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtAll.ForeColor = System.Drawing.Color.Black;
            this.RbtAll.Location = new System.Drawing.Point(28, 123);
            this.RbtAll.Name = "RbtAll";
            this.RbtAll.Size = new System.Drawing.Size(42, 17);
            this.RbtAll.TabIndex = 156;
            this.RbtAll.TabStop = true;
            this.RbtAll.Text = "All";
            this.RbtAll.ToolTips = "";
            this.RbtAll.UseVisualStyleBackColor = true;
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(298, 84);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.ShowCheckBox = true;
            this.DTPToDate.Size = new System.Drawing.Size(129, 24);
            this.DTPToDate.TabIndex = 159;
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
            this.DTPFromDate.Location = new System.Drawing.Point(104, 84);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.ShowCheckBox = true;
            this.DTPFromDate.Size = new System.Drawing.Size(129, 24);
            this.DTPFromDate.TabIndex = 160;
            this.DTPFromDate.ToolTips = "";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(239, 90);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(53, 13);
            this.cLabel8.TabIndex = 157;
            this.cLabel8.Text = "ToDate";
            this.cLabel8.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(25, 90);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(75, 13);
            this.cLabel9.TabIndex = 158;
            this.cLabel9.Text = "From Date";
            this.cLabel9.ToolTips = "";
            // 
            // ChkWithPCN
            // 
            this.ChkWithPCN.AllowTabKeyOnEnter = false;
            this.ChkWithPCN.AutoSize = true;
            this.ChkWithPCN.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkWithPCN.Location = new System.Drawing.Point(298, 124);
            this.ChkWithPCN.Name = "ChkWithPCN";
            this.ChkWithPCN.Size = new System.Drawing.Size(124, 17);
            this.ChkWithPCN.TabIndex = 186;
            this.ChkWithPCN.TabStop = false;
            this.ChkWithPCN.Text = "With PCN Stock";
            this.ChkWithPCN.ToolTips = "If Apply Any Difference";
            this.ChkWithPCN.UseVisualStyleBackColor = true;
            this.ChkWithPCN.Visible = false;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(25, 147);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(118, 13);
            this.cLabel1.TabIndex = 190;
            this.cLabel1.Text = "Select Grd Type :";
            this.cLabel1.ToolTips = "";
            // 
            // RdbGrdType
            // 
            this.RdbGrdType.Location = new System.Drawing.Point(144, 144);
            this.RdbGrdType.Name = "RdbGrdType";
            this.RdbGrdType.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RdbGrdType.Properties.Appearance.Options.UseFont = true;
            this.RdbGrdType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("All", "All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Grd", "Grd"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("By", "By"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Lab", "Lab")});
            this.RdbGrdType.Size = new System.Drawing.Size(249, 22);
            this.RdbGrdType.TabIndex = 192;
            // 
            // txtMultiMainManager
            // 
            this.txtMultiMainManager.ActivationColor = true;
            this.txtMultiMainManager.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtMultiMainManager.AllowTabKeyOnEnter = false;
            this.txtMultiMainManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMultiMainManager.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtMultiMainManager.Format = "";
            this.txtMultiMainManager.IsComplusory = false;
            this.txtMultiMainManager.Location = new System.Drawing.Point(144, 170);
            this.txtMultiMainManager.Name = "txtMultiMainManager";
            this.txtMultiMainManager.SelectAllTextOnFocus = true;
            this.txtMultiMainManager.Size = new System.Drawing.Size(209, 22);
            this.txtMultiMainManager.TabIndex = 228;
            this.txtMultiMainManager.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMultiMainManager.ToolTips = "";
            this.txtMultiMainManager.WaterMarkText = null;
            this.txtMultiMainManager.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMultiMainManager_KeyPress);
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(38, 175);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(105, 13);
            this.cLabel3.TabIndex = 227;
            this.cLabel3.Text = "Main Manager :";
            this.cLabel3.ToolTips = "";
            // 
            // RbtFullKapanAnalysis
            // 
            this.RbtFullKapanAnalysis.AutoSize = true;
            this.RbtFullKapanAnalysis.Checked = true;
            this.RbtFullKapanAnalysis.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtFullKapanAnalysis.Location = new System.Drawing.Point(3, 5);
            this.RbtFullKapanAnalysis.Name = "RbtFullKapanAnalysis";
            this.RbtFullKapanAnalysis.Size = new System.Drawing.Size(152, 17);
            this.RbtFullKapanAnalysis.TabIndex = 229;
            this.RbtFullKapanAnalysis.TabStop = true;
            this.RbtFullKapanAnalysis.Text = "Full Kapan Analysis";
            this.RbtFullKapanAnalysis.UseVisualStyleBackColor = true;
            // 
            // RbtKapanEstimate
            // 
            this.RbtKapanEstimate.AutoSize = true;
            this.RbtKapanEstimate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtKapanEstimate.Location = new System.Drawing.Point(161, 5);
            this.RbtKapanEstimate.Name = "RbtKapanEstimate";
            this.RbtKapanEstimate.Size = new System.Drawing.Size(126, 17);
            this.RbtKapanEstimate.TabIndex = 229;
            this.RbtKapanEstimate.Text = "Kapan Estimate";
            this.RbtKapanEstimate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RbtFullKapanAnalysis);
            this.panel1.Controls.Add(this.RbtKapanEstimate);
            this.panel1.Location = new System.Drawing.Point(28, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 25);
            this.panel1.TabIndex = 230;
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(41, 228);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(102, 13);
            this.cLabel6.TabIndex = 233;
            this.cLabel6.Text = "Packet Group :";
            this.cLabel6.ToolTips = "";
            // 
            // ChkCmbPacketGroup
            // 
            this.ChkCmbPacketGroup.Location = new System.Drawing.Point(144, 223);
            this.ChkCmbPacketGroup.Name = "ChkCmbPacketGroup";
            this.ChkCmbPacketGroup.Properties.AllowMultiSelect = true;
            this.ChkCmbPacketGroup.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkCmbPacketGroup.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbPacketGroup.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkCmbPacketGroup.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ChkCmbPacketGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbPacketGroup.Properties.DropDownRows = 20;
            this.ChkCmbPacketGroup.Properties.IncrementalSearch = true;
            this.ChkCmbPacketGroup.Size = new System.Drawing.Size(210, 20);
            this.ChkCmbPacketGroup.TabIndex = 234;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(21, 202);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(122, 13);
            this.cLabel5.TabIndex = 231;
            this.cLabel5.Text = "Packet Category :";
            this.cLabel5.ToolTips = "";
            // 
            // chkCmbPacketCat
            // 
            this.chkCmbPacketCat.Location = new System.Drawing.Point(144, 197);
            this.chkCmbPacketCat.Name = "chkCmbPacketCat";
            this.chkCmbPacketCat.Properties.AllowMultiSelect = true;
            this.chkCmbPacketCat.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.chkCmbPacketCat.Properties.Appearance.Options.UseFont = true;
            this.chkCmbPacketCat.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.chkCmbPacketCat.Properties.AppearanceDropDown.Options.UseFont = true;
            this.chkCmbPacketCat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkCmbPacketCat.Properties.DropDownRows = 20;
            this.chkCmbPacketCat.Properties.IncrementalSearch = true;
            this.chkCmbPacketCat.Size = new System.Drawing.Size(210, 20);
            this.chkCmbPacketCat.TabIndex = 232;
            // 
            // FrmFullKapanAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 247);
            this.Controls.Add(this.cLabel6);
            this.Controls.Add(this.ChkCmbPacketGroup);
            this.Controls.Add(this.cLabel5);
            this.Controls.Add(this.chkCmbPacketCat);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtMultiMainManager);
            this.Controls.Add(this.cLabel3);
            this.Controls.Add(this.RdbGrdType);
            this.Controls.Add(this.cLabel1);
            this.Controls.Add(this.ChkWithPCN);
            this.Controls.Add(this.DTPToDate);
            this.Controls.Add(this.DTPFromDate);
            this.Controls.Add(this.cLabel8);
            this.Controls.Add(this.cLabel9);
            this.Controls.Add(this.RbtPktNotCreated);
            this.Controls.Add(this.RbtPktCreated);
            this.Controls.Add(this.RbtAll);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.cLabel2);
            this.Controls.Add(this.BtnDirectPDFExport);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.CmbKapan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmFullKapanAnalysis";
            this.Text = "FULL KAPAN ANALYSIS";
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RdbGrdType.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbPacketGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCmbPacketCat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraEditors.SimpleButton BtnDirectPDFExport;
        private AxonContLib.cRadioButton RbtPktNotCreated;
        private AxonContLib.cRadioButton RbtPktCreated;
        private AxonContLib.cRadioButton RbtAll;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cCheckBox ChkWithPCN;
        private AxonContLib.cLabel cLabel1;
        private DevExpress.XtraEditors.RadioGroup RdbGrdType;
        private AxonContLib.cTextBox txtMultiMainManager;
        private AxonContLib.cLabel cLabel3;
        private System.Windows.Forms.RadioButton RbtFullKapanAnalysis;
        private System.Windows.Forms.RadioButton RbtKapanEstimate;
        private System.Windows.Forms.Panel panel1;
        private AxonContLib.cLabel cLabel6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbPacketGroup;
        private AxonContLib.cLabel cLabel5;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkCmbPacketCat;


    }
}