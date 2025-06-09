namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmMISAnalysisMakPolGrd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMISAnalysisMakPolGrd));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.ChkCmbSize = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.ChkCmbClarity = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.ChkCmbColor = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.ChkCmbShape = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.ChklCmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.BANDPARTICULARS = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BANDMAKABLE = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BANDPOLISH = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgPolPcsPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgPolCaratPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn13 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgPolAmountPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.BANDBY = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgByPcsPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgByCaratPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn16 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgByAmountPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.BANDLAB = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn17 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgLabPcsPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn18 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgLabCaratPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn19 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgLabAmountPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.panel2 = new AxonContLib.cPanel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbClarity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbShape.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChklCmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgPolPcsPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgPolCaratPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgPolAmountPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgByPcsPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgByCaratPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgByAmountPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgLabPcsPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgLabCaratPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgLabAmountPer)).BeginInit();
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
            this.BtnSearch.Location = new System.Drawing.Point(690, 28);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(87, 32);
            this.BtnSearch.TabIndex = 5;
            this.BtnSearch.Text = "Refresh";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject9.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject9.Options.UseFont = true;
            serializableAppearanceObject9.Options.UseForeColor = true;
            serializableAppearanceObject9.Options.UseTextOptions = true;
            serializableAppearanceObject9.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject10.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject10.Options.UseFont = true;
            serializableAppearanceObject10.Options.UseForeColor = true;
            serializableAppearanceObject10.Options.UseTextOptions = true;
            serializableAppearanceObject10.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject11.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject11.Options.UseFont = true;
            serializableAppearanceObject11.Options.UseForeColor = true;
            serializableAppearanceObject11.Options.UseTextOptions = true;
            serializableAppearanceObject11.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject12.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject12.Options.UseFont = true;
            serializableAppearanceObject12.Options.UseForeColor = true;
            serializableAppearanceObject12.Options.UseTextOptions = true;
            serializableAppearanceObject12.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPToDate.Location = new System.Drawing.Point(216, 9);
            this.DTPToDate.Name = "DTPToDate";
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
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(84, 9);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(129, 24);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(5, 14);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(76, 14);
            this.cLabel9.TabIndex = 158;
            this.cLabel9.Text = "From Date";
            this.cLabel9.ToolTips = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ChkCmbSize);
            this.panel1.Controls.Add(this.cLabel5);
            this.panel1.Controls.Add(this.ChkCmbClarity);
            this.panel1.Controls.Add(this.cLabel4);
            this.panel1.Controls.Add(this.ChkCmbColor);
            this.panel1.Controls.Add(this.cLabel3);
            this.panel1.Controls.Add(this.ChkCmbShape);
            this.panel1.Controls.Add(this.cLabel1);
            this.panel1.Controls.Add(this.ChklCmbKapan);
            this.panel1.Controls.Add(this.cLabel2);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnExport);
            this.panel1.Controls.Add(this.progressPanel1);
            this.panel1.Controls.Add(this.DTPToDate);
            this.panel1.Controls.Add(this.DTPFromDate);
            this.panel1.Controls.Add(this.cLabel9);
            this.panel1.Controls.Add(this.BtnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 67);
            this.panel1.TabIndex = 161;
            // 
            // ChkCmbSize
            // 
            this.ChkCmbSize.Location = new System.Drawing.Point(536, 36);
            this.ChkCmbSize.Name = "ChkCmbSize";
            this.ChkCmbSize.Properties.AllowMultiSelect = true;
            this.ChkCmbSize.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.ChkCmbSize.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbSize.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkCmbSize.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ChkCmbSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbSize.Properties.DropDownRows = 20;
            this.ChkCmbSize.Properties.IncrementalSearch = true;
            this.ChkCmbSize.Size = new System.Drawing.Size(144, 22);
            this.ChkCmbSize.TabIndex = 176;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(495, 40);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(35, 14);
            this.cLabel5.TabIndex = 175;
            this.cLabel5.Text = "Size";
            this.cLabel5.ToolTips = "";
            // 
            // ChkCmbClarity
            // 
            this.ChkCmbClarity.Location = new System.Drawing.Point(405, 36);
            this.ChkCmbClarity.Name = "ChkCmbClarity";
            this.ChkCmbClarity.Properties.AllowMultiSelect = true;
            this.ChkCmbClarity.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.ChkCmbClarity.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbClarity.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkCmbClarity.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ChkCmbClarity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbClarity.Properties.DropDownRows = 20;
            this.ChkCmbClarity.Properties.IncrementalSearch = true;
            this.ChkCmbClarity.Size = new System.Drawing.Size(75, 22);
            this.ChkCmbClarity.TabIndex = 174;
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(356, 40);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(51, 14);
            this.cLabel4.TabIndex = 173;
            this.cLabel4.Text = "Clarity";
            this.cLabel4.ToolTips = "";
            // 
            // ChkCmbColor
            // 
            this.ChkCmbColor.Location = new System.Drawing.Point(273, 36);
            this.ChkCmbColor.Name = "ChkCmbColor";
            this.ChkCmbColor.Properties.AllowMultiSelect = true;
            this.ChkCmbColor.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.ChkCmbColor.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbColor.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkCmbColor.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ChkCmbColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbColor.Properties.DropDownRows = 20;
            this.ChkCmbColor.Properties.IncrementalSearch = true;
            this.ChkCmbColor.Size = new System.Drawing.Size(72, 22);
            this.ChkCmbColor.TabIndex = 172;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(227, 40);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(42, 14);
            this.cLabel3.TabIndex = 171;
            this.cLabel3.Text = "Color";
            this.cLabel3.ToolTips = "";
            // 
            // ChkCmbShape
            // 
            this.ChkCmbShape.Location = new System.Drawing.Point(84, 36);
            this.ChkCmbShape.Name = "ChkCmbShape";
            this.ChkCmbShape.Properties.AllowMultiSelect = true;
            this.ChkCmbShape.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.ChkCmbShape.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbShape.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkCmbShape.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ChkCmbShape.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbShape.Properties.DropDownRows = 20;
            this.ChkCmbShape.Properties.IncrementalSearch = true;
            this.ChkCmbShape.Size = new System.Drawing.Size(129, 22);
            this.ChkCmbShape.TabIndex = 170;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(30, 40);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(48, 14);
            this.cLabel1.TabIndex = 169;
            this.cLabel1.Text = "Shape";
            this.cLabel1.ToolTips = "";
            // 
            // ChklCmbKapan
            // 
            this.ChklCmbKapan.Location = new System.Drawing.Point(405, 9);
            this.ChklCmbKapan.Name = "ChklCmbKapan";
            this.ChklCmbKapan.Properties.AllowMultiSelect = true;
            this.ChklCmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.ChklCmbKapan.Properties.Appearance.Options.UseFont = true;
            this.ChklCmbKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChklCmbKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ChklCmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChklCmbKapan.Properties.DropDownRows = 20;
            this.ChklCmbKapan.Properties.IncrementalSearch = true;
            this.ChklCmbKapan.Size = new System.Drawing.Size(275, 22);
            this.ChklCmbKapan.TabIndex = 168;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(356, 14);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(48, 14);
            this.cLabel2.TabIndex = 167;
            this.cLabel2.Text = "Kapan";
            this.cLabel2.ToolTips = "";
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
            this.BtnExit.Location = new System.Drawing.Point(870, 28);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(87, 32);
            this.BtnExit.TabIndex = 166;
            this.BtnExit.Text = "Exit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExport.ImageOptions.SvgImage")));
            this.BtnExport.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExport.Location = new System.Drawing.Point(780, 28);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(87, 32);
            this.BtnExport.TabIndex = 165;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // progressPanel1
            // 
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanel1.AppearanceCaption.Options.UseFont = true;
            this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel1.AppearanceDescription.Options.UseFont = true;
            this.progressPanel1.BarAnimationElementThickness = 2;
            this.progressPanel1.Caption = "";
            this.progressPanel1.Location = new System.Drawing.Point(963, 32);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.ShowCaption = false;
            this.progressPanel1.ShowDescription = false;
            this.progressPanel1.Size = new System.Drawing.Size(33, 27);
            this.progressPanel1.TabIndex = 164;
            this.progressPanel1.Visible = false;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            gridLevelNode1.RelationName = "Level1";
            this.MainGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGrid.Location = new System.Drawing.Point(0, 77);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repPrgPolPcsPer,
            this.repPrgPolCaratPer,
            this.repPrgPolAmountPer,
            this.repPrgByPcsPer,
            this.repPrgByCaratPer,
            this.repPrgByAmountPer,
            this.repPrgLabPcsPer,
            this.repPrgLabCaratPer,
            this.repPrgLabAmountPer});
            this.MainGrid.Size = new System.Drawing.Size(1184, 684);
            this.MainGrid.TabIndex = 162;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
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
            this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.BANDPARTICULARS,
            this.BANDMAKABLE,
            this.BANDPOLISH,
            this.BANDBY,
            this.BANDLAB});
            this.GrdDet.ColumnPanelRowHeight = 20;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn1,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.bandedGridColumn2,
            this.bandedGridColumn11,
            this.bandedGridColumn3,
            this.bandedGridColumn12,
            this.bandedGridColumn4,
            this.bandedGridColumn13,
            this.bandedGridColumn5,
            this.bandedGridColumn14,
            this.bandedGridColumn6,
            this.bandedGridColumn15,
            this.bandedGridColumn7,
            this.bandedGridColumn16,
            this.bandedGridColumn8,
            this.bandedGridColumn17,
            this.bandedGridColumn9,
            this.bandedGridColumn18,
            this.bandedGridColumn10,
            this.bandedGridColumn19});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.AllowPartialRedrawOnScrolling = false;
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDet.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsPrint.PrintBandHeader = false;
            this.GrdDet.OptionsPrint.PrintFooter = false;
            this.GrdDet.OptionsPrint.PrintHeader = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowBands = false;
            this.GrdDet.OptionsView.ShowColumnHeaders = false;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.GrdDet.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.GrdDet_CellMerge);
            this.GrdDet.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.GrdDet_CustomDrawCell);
            this.GrdDet.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GrdDet_RowCellStyle);
            this.GrdDet.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDet_CustomColumnDisplayText);
            // 
            // BANDPARTICULARS
            // 
            this.BANDPARTICULARS.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.BANDPARTICULARS.AppearanceHeader.Options.UseFont = true;
            this.BANDPARTICULARS.AppearanceHeader.Options.UseTextOptions = true;
            this.BANDPARTICULARS.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BANDPARTICULARS.Caption = "...";
            this.BANDPARTICULARS.Columns.Add(this.bandedGridColumn1);
            this.BANDPARTICULARS.Columns.Add(this.gridColumn1);
            this.BANDPARTICULARS.Columns.Add(this.gridColumn2);
            this.BANDPARTICULARS.Name = "BANDPARTICULARS";
            this.BANDPARTICULARS.VisibleIndex = 0;
            this.BANDPARTICULARS.Width = 100;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "FIRSTDATE";
            this.bandedGridColumn1.FieldName = "FIRSTDATE";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ENDDATE";
            this.gridColumn1.FieldName = "ENDDATE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Particulars";
            this.gridColumn2.FieldName = "PARAMNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn2.Visible = true;
            this.gridColumn2.Width = 100;
            // 
            // BANDMAKABLE
            // 
            this.BANDMAKABLE.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.BANDMAKABLE.AppearanceHeader.Options.UseFont = true;
            this.BANDMAKABLE.AppearanceHeader.Options.UseTextOptions = true;
            this.BANDMAKABLE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BANDMAKABLE.Caption = "Makable";
            this.BANDMAKABLE.Columns.Add(this.gridColumn3);
            this.BANDMAKABLE.Columns.Add(this.gridColumn4);
            this.BANDMAKABLE.Columns.Add(this.gridColumn5);
            this.BANDMAKABLE.Name = "BANDMAKABLE";
            this.BANDMAKABLE.VisibleIndex = 1;
            this.BANDMAKABLE.Width = 220;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Pcs";
            this.gridColumn3.FieldName = "MAKPCS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PCS", "{0:N0}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.Width = 50;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Cts";
            this.gridColumn4.FieldName = "MAKCARAT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CARAT", "{0:N2}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Amt";
            this.gridColumn5.FieldName = "MAKAMOUNT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "{0:N2}")});
            this.gridColumn5.Visible = true;
            this.gridColumn5.Width = 100;
            // 
            // BANDPOLISH
            // 
            this.BANDPOLISH.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.BANDPOLISH.AppearanceHeader.Options.UseFont = true;
            this.BANDPOLISH.AppearanceHeader.Options.UseTextOptions = true;
            this.BANDPOLISH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BANDPOLISH.Caption = "Polish";
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn2);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn11);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn3);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn12);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn4);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn13);
            this.BANDPOLISH.Name = "BANDPOLISH";
            this.BANDPOLISH.VisibleIndex = 2;
            this.BANDPOLISH.Width = 580;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn2.Caption = "Pcs";
            this.bandedGridColumn2.FieldName = "POLPCS";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 50;
            // 
            // bandedGridColumn11
            // 
            this.bandedGridColumn11.Caption = "%";
            this.bandedGridColumn11.ColumnEdit = this.repPrgPolPcsPer;
            this.bandedGridColumn11.FieldName = "POLPCSPER";
            this.bandedGridColumn11.Name = "bandedGridColumn11";
            this.bandedGridColumn11.Visible = true;
            this.bandedGridColumn11.Width = 120;
            // 
            // repPrgPolPcsPer
            // 
            this.repPrgPolPcsPer.AppearanceReadOnly.ForeColor = System.Drawing.Color.Red;
            this.repPrgPolPcsPer.AppearanceReadOnly.ForeColor2 = System.Drawing.Color.Red;
            this.repPrgPolPcsPer.EndColor = System.Drawing.Color.Black;
            this.repPrgPolPcsPer.LookAndFeel.UseDefaultLookAndFeel = false;
            this.repPrgPolPcsPer.Name = "repPrgPolPcsPer";
            this.repPrgPolPcsPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgPolPcsPer.ShowTitle = true;
            this.repPrgPolPcsPer.StartColor = System.Drawing.Color.Black;
            this.repPrgPolPcsPer.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.repPrgPolPcsPer_CustomDisplayText);
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn3.Caption = "Cts";
            this.bandedGridColumn3.FieldName = "POLCARAT";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.Width = 70;
            // 
            // bandedGridColumn12
            // 
            this.bandedGridColumn12.Caption = "%";
            this.bandedGridColumn12.ColumnEdit = this.repPrgPolCaratPer;
            this.bandedGridColumn12.FieldName = "POLCARATPER";
            this.bandedGridColumn12.Name = "bandedGridColumn12";
            this.bandedGridColumn12.Visible = true;
            this.bandedGridColumn12.Width = 120;
            // 
            // repPrgPolCaratPer
            // 
            this.repPrgPolCaratPer.EndColor = System.Drawing.Color.Black;
            this.repPrgPolCaratPer.Name = "repPrgPolCaratPer";
            this.repPrgPolCaratPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgPolCaratPer.ShowTitle = true;
            this.repPrgPolCaratPer.StartColor = System.Drawing.Color.Black;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn4.Caption = "Amt";
            this.bandedGridColumn4.FieldName = "POLAMOUNT";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 100;
            // 
            // bandedGridColumn13
            // 
            this.bandedGridColumn13.Caption = "%";
            this.bandedGridColumn13.ColumnEdit = this.repPrgPolAmountPer;
            this.bandedGridColumn13.FieldName = "POLAMOUNTPER";
            this.bandedGridColumn13.Name = "bandedGridColumn13";
            this.bandedGridColumn13.Visible = true;
            this.bandedGridColumn13.Width = 120;
            // 
            // repPrgPolAmountPer
            // 
            this.repPrgPolAmountPer.EndColor = System.Drawing.Color.Black;
            this.repPrgPolAmountPer.Name = "repPrgPolAmountPer";
            this.repPrgPolAmountPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgPolAmountPer.ShowTitle = true;
            this.repPrgPolAmountPer.StartColor = System.Drawing.Color.Black;
            // 
            // BANDBY
            // 
            this.BANDBY.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.BANDBY.AppearanceHeader.Options.UseFont = true;
            this.BANDBY.AppearanceHeader.Options.UseTextOptions = true;
            this.BANDBY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BANDBY.Caption = "BY";
            this.BANDBY.Columns.Add(this.bandedGridColumn5);
            this.BANDBY.Columns.Add(this.bandedGridColumn14);
            this.BANDBY.Columns.Add(this.bandedGridColumn6);
            this.BANDBY.Columns.Add(this.bandedGridColumn15);
            this.BANDBY.Columns.Add(this.bandedGridColumn7);
            this.BANDBY.Columns.Add(this.bandedGridColumn16);
            this.BANDBY.Name = "BANDBY";
            this.BANDBY.VisibleIndex = 3;
            this.BANDBY.Width = 580;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn5.Caption = "Pcs";
            this.bandedGridColumn5.FieldName = "BYPCS";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.Width = 50;
            // 
            // bandedGridColumn14
            // 
            this.bandedGridColumn14.Caption = "%";
            this.bandedGridColumn14.ColumnEdit = this.repPrgByPcsPer;
            this.bandedGridColumn14.FieldName = "BYPCSPER";
            this.bandedGridColumn14.Name = "bandedGridColumn14";
            this.bandedGridColumn14.Visible = true;
            this.bandedGridColumn14.Width = 120;
            // 
            // repPrgByPcsPer
            // 
            this.repPrgByPcsPer.EndColor = System.Drawing.Color.Black;
            this.repPrgByPcsPer.Name = "repPrgByPcsPer";
            this.repPrgByPcsPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgByPcsPer.ShowTitle = true;
            this.repPrgByPcsPer.StartColor = System.Drawing.Color.Black;
            // 
            // bandedGridColumn6
            // 
            this.bandedGridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn6.Caption = "Cts";
            this.bandedGridColumn6.FieldName = "BYCARAT";
            this.bandedGridColumn6.Name = "bandedGridColumn6";
            this.bandedGridColumn6.Visible = true;
            this.bandedGridColumn6.Width = 70;
            // 
            // bandedGridColumn15
            // 
            this.bandedGridColumn15.Caption = "%";
            this.bandedGridColumn15.ColumnEdit = this.repPrgByCaratPer;
            this.bandedGridColumn15.FieldName = "BYCARATPER";
            this.bandedGridColumn15.Name = "bandedGridColumn15";
            this.bandedGridColumn15.Visible = true;
            this.bandedGridColumn15.Width = 120;
            // 
            // repPrgByCaratPer
            // 
            this.repPrgByCaratPer.EndColor = System.Drawing.Color.Black;
            this.repPrgByCaratPer.Name = "repPrgByCaratPer";
            this.repPrgByCaratPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgByCaratPer.ShowTitle = true;
            this.repPrgByCaratPer.StartColor = System.Drawing.Color.Black;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn7.Caption = "Amt";
            this.bandedGridColumn7.FieldName = "BYAMOUNT";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.Visible = true;
            this.bandedGridColumn7.Width = 100;
            // 
            // bandedGridColumn16
            // 
            this.bandedGridColumn16.Caption = "%";
            this.bandedGridColumn16.ColumnEdit = this.repPrgByAmountPer;
            this.bandedGridColumn16.FieldName = "BYAMOUNTPER";
            this.bandedGridColumn16.Name = "bandedGridColumn16";
            this.bandedGridColumn16.Visible = true;
            this.bandedGridColumn16.Width = 120;
            // 
            // repPrgByAmountPer
            // 
            this.repPrgByAmountPer.EndColor = System.Drawing.Color.Black;
            this.repPrgByAmountPer.Name = "repPrgByAmountPer";
            this.repPrgByAmountPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgByAmountPer.ShowTitle = true;
            this.repPrgByAmountPer.StartColor = System.Drawing.Color.Black;
            // 
            // BANDLAB
            // 
            this.BANDLAB.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.BANDLAB.AppearanceHeader.Options.UseFont = true;
            this.BANDLAB.AppearanceHeader.Options.UseTextOptions = true;
            this.BANDLAB.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BANDLAB.Caption = "Lab";
            this.BANDLAB.Columns.Add(this.bandedGridColumn8);
            this.BANDLAB.Columns.Add(this.bandedGridColumn17);
            this.BANDLAB.Columns.Add(this.bandedGridColumn9);
            this.BANDLAB.Columns.Add(this.bandedGridColumn18);
            this.BANDLAB.Columns.Add(this.bandedGridColumn10);
            this.BANDLAB.Columns.Add(this.bandedGridColumn19);
            this.BANDLAB.Name = "BANDLAB";
            this.BANDLAB.VisibleIndex = 4;
            this.BANDLAB.Width = 580;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn8.Caption = "Pcs";
            this.bandedGridColumn8.FieldName = "LABPCS";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.Width = 50;
            // 
            // bandedGridColumn17
            // 
            this.bandedGridColumn17.Caption = "%";
            this.bandedGridColumn17.ColumnEdit = this.repPrgLabPcsPer;
            this.bandedGridColumn17.FieldName = "LABPCSPER";
            this.bandedGridColumn17.Name = "bandedGridColumn17";
            this.bandedGridColumn17.Visible = true;
            this.bandedGridColumn17.Width = 120;
            // 
            // repPrgLabPcsPer
            // 
            this.repPrgLabPcsPer.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.repPrgLabPcsPer.EndColor = System.Drawing.Color.Black;
            this.repPrgLabPcsPer.Name = "repPrgLabPcsPer";
            this.repPrgLabPcsPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgLabPcsPer.ShowTitle = true;
            this.repPrgLabPcsPer.StartColor = System.Drawing.Color.Black;
            // 
            // bandedGridColumn9
            // 
            this.bandedGridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn9.Caption = "Cts";
            this.bandedGridColumn9.FieldName = "LABCARAT";
            this.bandedGridColumn9.Name = "bandedGridColumn9";
            this.bandedGridColumn9.Visible = true;
            this.bandedGridColumn9.Width = 70;
            // 
            // bandedGridColumn18
            // 
            this.bandedGridColumn18.Caption = "%";
            this.bandedGridColumn18.ColumnEdit = this.repPrgLabCaratPer;
            this.bandedGridColumn18.FieldName = "LABCARATPER";
            this.bandedGridColumn18.Name = "bandedGridColumn18";
            this.bandedGridColumn18.Visible = true;
            this.bandedGridColumn18.Width = 120;
            // 
            // repPrgLabCaratPer
            // 
            this.repPrgLabCaratPer.EndColor = System.Drawing.Color.Black;
            this.repPrgLabCaratPer.Name = "repPrgLabCaratPer";
            this.repPrgLabCaratPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgLabCaratPer.ShowTitle = true;
            this.repPrgLabCaratPer.StartColor = System.Drawing.Color.Black;
            // 
            // bandedGridColumn10
            // 
            this.bandedGridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn10.Caption = "Amt";
            this.bandedGridColumn10.FieldName = "LABAMOUNT";
            this.bandedGridColumn10.Name = "bandedGridColumn10";
            this.bandedGridColumn10.Visible = true;
            this.bandedGridColumn10.Width = 100;
            // 
            // bandedGridColumn19
            // 
            this.bandedGridColumn19.Caption = "%";
            this.bandedGridColumn19.ColumnEdit = this.repPrgLabAmountPer;
            this.bandedGridColumn19.FieldName = "LABAMOUNTPER";
            this.bandedGridColumn19.Name = "bandedGridColumn19";
            this.bandedGridColumn19.Visible = true;
            this.bandedGridColumn19.Width = 120;
            // 
            // repPrgLabAmountPer
            // 
            this.repPrgLabAmountPer.EndColor = System.Drawing.Color.Black;
            this.repPrgLabAmountPer.Name = "repPrgLabAmountPer";
            this.repPrgLabAmountPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgLabAmountPer.ShowTitle = true;
            this.repPrgLabAmountPer.StartColor = System.Drawing.Color.Black;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 10);
            this.panel2.TabIndex = 163;
            // 
            // FrmMISAnalysisMakPolGrd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmMISAnalysisMakPolGrd";
            this.Text = "MIS ANALYSIS MAK/POL/GRD";
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbClarity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbShape.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChklCmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgPolPcsPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgPolCaratPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgPolAmountPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgByPcsPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgByCaratPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgByAmountPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgLabPcsPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgLabCaratPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPrgLabAmountPer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgPolPcsPer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgPolCaratPer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgPolAmountPer;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDPARTICULARS;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDMAKABLE;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDPOLISH;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDBY;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn15;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn16;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDLAB;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn17;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn18;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn19;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgByPcsPer;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgByCaratPer;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgByAmountPer;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgLabPcsPer;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgLabCaratPer;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgLabAmountPer;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChklCmbKapan;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbShape;
        private AxonContLib.cLabel cLabel1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbColor;
        private AxonContLib.cLabel cLabel3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbClarity;
        private AxonContLib.cLabel cLabel4;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbSize;
        private AxonContLib.cLabel cLabel5;


    }
}