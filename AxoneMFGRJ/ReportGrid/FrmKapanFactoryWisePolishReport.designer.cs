namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmKapanFactoryWisePolishReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKapanFactoryWisePolishReport));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.BtnDirectPDFExport = new DevExpress.XtraEditors.SimpleButton();
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.CmbKapan = new AxonContLib.cComboBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.PanelProgress = new DevExpress.XtraWaitForm.ProgressPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.RbtFactoryWise = new AxonContLib.cRadioButton(this.components);
            this.RbtFinalPolish4PWise = new AxonContLib.cRadioButton(this.components);
            this.RbtSizeWisePolishReport = new AxonContLib.cRadioButton(this.components);
            this.GrdPolishReport = new DevExpress.XtraGrid.GridControl();
            this.GrdData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblExport = new System.Windows.Forms.Label();
            this.RbtnDetails = new AxonContLib.cRadioButton(this.components);
            this.RbtnSummery = new AxonContLib.cRadioButton(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.TxtManager = new AxonContLib.cTextBox(this.components);
            this.lblCurrEmp = new AxonContLib.cLabel(this.components);
            this.txtCurrEmp = new AxonContLib.cTextBox(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdPolishReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdData)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.BtnSearch.Location = new System.Drawing.Point(87, 133);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(139, 32);
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
            this.BtnExit.Location = new System.Drawing.Point(378, 133);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 32);
            this.BtnExit.TabIndex = 153;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
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
            this.cLabel2.Location = new System.Drawing.Point(57, 108);
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
            this.BtnDirectPDFExport.Location = new System.Drawing.Point(226, 133);
            this.BtnDirectPDFExport.Name = "BtnDirectPDFExport";
            this.BtnDirectPDFExport.Size = new System.Drawing.Size(152, 32);
            this.BtnDirectPDFExport.TabIndex = 0;
            this.BtnDirectPDFExport.Text = "Direct .Pdf Export";
            this.BtnDirectPDFExport.Click += new System.EventHandler(this.BtnDirectPDFExport_Click);
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(299, 75);
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
            this.DTPFromDate.Location = new System.Drawing.Point(105, 75);
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
            this.cLabel8.Location = new System.Drawing.Point(240, 81);
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
            this.cLabel9.Location = new System.Drawing.Point(29, 81);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(75, 13);
            this.cLabel9.TabIndex = 158;
            this.cLabel9.Text = "From Date";
            this.cLabel9.ToolTips = "";
            // 
            // CmbKapan
            // 
            this.CmbKapan.AllowTabKeyOnEnter = true;
            this.CmbKapan.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbKapan.ForeColor = System.Drawing.Color.Black;
            this.CmbKapan.FormattingEnabled = true;
            this.CmbKapan.Items.AddRange(new object[] {
            "BLACK",
            "CANADAMARK",
            "CHARNI",
            "CLARITY",
            "COLOR",
            "COLORSHADE",
            "CUT",
            "CUT_POLISH_SYM_CC_DISCOUNT",
            "DEPARTMENT",
            "DESIGNATION",
            "EYECLEAN",
            "FLUORESCENCE",
            "FLUVISIBILITY",
            "GRAIN",
            "HEARTANDARROW",
            "LAB",
            "LBLC",
            "LUSTER",
            "MACHINECOLORTYPE",
            "MILKY",
            "NATTS",
            "NATURAL",
            "OPEN",
            "OTHERUNIT",
            "PAVALION",
            "PLANNINGTYPE",
            "POLISH",
            "PROCESS",
            "REPORTPATHPREFIX",
            "REPORTTYPE",
            "ROUGHTYPE",
            "SHAPE",
            "STATUS",
            "STOCKTYPE",
            "SUBLOT",
            "SYMMETRY",
            "TENSION",
            "SAKHAT",
            "WHITE",
            "REJECTION",
            "SOURCE",
            "ARTICLE",
            "MSIZE",
            "FILE_TRANSFER_TYPE",
            "CUT-POL-SYM-COMBINATION",
            "REASON",
            "DOCUMENTTYPE",
            "BREAKING_TYPE",
            "SALARY_TYPE",
            "LEAVETYPE",
            "RAPMISTAKEPRDTYPE",
            "PACKETGROUP"});
            this.CmbKapan.Location = new System.Drawing.Point(105, 102);
            this.CmbKapan.Name = "CmbKapan";
            this.CmbKapan.Size = new System.Drawing.Size(323, 24);
            this.CmbKapan.TabIndex = 161;
            this.CmbKapan.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold);
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(35, 9);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(300, 23);
            this.cLabel1.TabIndex = 162;
            this.cLabel1.Text = "Factory Wise Polish Report";
            this.cLabel1.ToolTips = "";
            this.cLabel1.Visible = false;
            this.cLabel1.Click += new System.EventHandler(this.cLabel1_Click);
            // 
            // PanelProgress
            // 
            this.PanelProgress.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.PanelProgress.Appearance.Options.UseBackColor = true;
            this.PanelProgress.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 12F);
            this.PanelProgress.AppearanceCaption.Options.UseFont = true;
            this.PanelProgress.AppearanceDescription.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.PanelProgress.AppearanceDescription.Options.UseFont = true;
            this.PanelProgress.BarAnimationElementThickness = 2;
            this.PanelProgress.Location = new System.Drawing.Point(462, 130);
            this.PanelProgress.LookAndFeel.UseDefaultLookAndFeel = false;
            this.PanelProgress.Name = "PanelProgress";
            this.PanelProgress.ShowCaption = false;
            this.PanelProgress.ShowDescription = false;
            this.PanelProgress.Size = new System.Drawing.Size(37, 37);
            this.PanelProgress.TabIndex = 206;
            this.PanelProgress.Text = "progressPanel1";
            this.PanelProgress.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // RbtFactoryWise
            // 
            this.RbtFactoryWise.AllowTabKeyOnEnter = false;
            this.RbtFactoryWise.AutoSize = true;
            this.RbtFactoryWise.BackColor = System.Drawing.Color.Transparent;
            this.RbtFactoryWise.Checked = true;
            this.RbtFactoryWise.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtFactoryWise.ForeColor = System.Drawing.Color.Black;
            this.RbtFactoryWise.Location = new System.Drawing.Point(106, 403);
            this.RbtFactoryWise.Name = "RbtFactoryWise";
            this.RbtFactoryWise.Size = new System.Drawing.Size(261, 22);
            this.RbtFactoryWise.TabIndex = 224;
            this.RbtFactoryWise.TabStop = true;
            this.RbtFactoryWise.Tag = "2";
            this.RbtFactoryWise.Text = "Factory Wise Polish Report";
            this.RbtFactoryWise.ToolTips = "Display All Over Company Stock";
            this.RbtFactoryWise.UseVisualStyleBackColor = false;
            this.RbtFactoryWise.Visible = false;
            this.RbtFactoryWise.CheckedChanged += new System.EventHandler(this.RbtFactoryWise_CheckedChanged);
            // 
            // RbtFinalPolish4PWise
            // 
            this.RbtFinalPolish4PWise.AllowTabKeyOnEnter = false;
            this.RbtFinalPolish4PWise.AutoSize = true;
            this.RbtFinalPolish4PWise.BackColor = System.Drawing.Color.Transparent;
            this.RbtFinalPolish4PWise.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.RbtFinalPolish4PWise.ForeColor = System.Drawing.Color.Black;
            this.RbtFinalPolish4PWise.Location = new System.Drawing.Point(32, 47);
            this.RbtFinalPolish4PWise.Name = "RbtFinalPolish4PWise";
            this.RbtFinalPolish4PWise.Size = new System.Drawing.Size(258, 22);
            this.RbtFinalPolish4PWise.TabIndex = 225;
            this.RbtFinalPolish4PWise.Tag = "4";
            this.RbtFinalPolish4PWise.Text = "Final PolishWise 4P Report";
            this.RbtFinalPolish4PWise.ToolTips = "";
            this.RbtFinalPolish4PWise.UseVisualStyleBackColor = false;
            this.RbtFinalPolish4PWise.CheckedChanged += new System.EventHandler(this.RbtFactoryWise_CheckedChanged);
            // 
            // RbtSizeWisePolishReport
            // 
            this.RbtSizeWisePolishReport.AllowTabKeyOnEnter = false;
            this.RbtSizeWisePolishReport.AutoSize = true;
            this.RbtSizeWisePolishReport.BackColor = System.Drawing.Color.Transparent;
            this.RbtSizeWisePolishReport.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSizeWisePolishReport.ForeColor = System.Drawing.Color.Black;
            this.RbtSizeWisePolishReport.Location = new System.Drawing.Point(300, 47);
            this.RbtSizeWisePolishReport.Name = "RbtSizeWisePolishReport";
            this.RbtSizeWisePolishReport.Size = new System.Drawing.Size(231, 22);
            this.RbtSizeWisePolishReport.TabIndex = 226;
            this.RbtSizeWisePolishReport.Tag = "2";
            this.RbtSizeWisePolishReport.Text = "Size Wise Polish Report";
            this.RbtSizeWisePolishReport.ToolTips = "";
            this.RbtSizeWisePolishReport.UseVisualStyleBackColor = false;
            this.RbtSizeWisePolishReport.CheckedChanged += new System.EventHandler(this.RbtFactoryWise_CheckedChanged);
            // 
            // GrdPolishReport
            // 
            this.GrdPolishReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdPolishReport.Location = new System.Drawing.Point(0, 200);
            this.GrdPolishReport.MainView = this.GrdData;
            this.GrdPolishReport.Name = "GrdPolishReport";
            this.GrdPolishReport.Size = new System.Drawing.Size(927, 394);
            this.GrdPolishReport.TabIndex = 0;
            this.GrdPolishReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdData});
            // 
            // GrdData
            // 
            this.GrdData.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.GrdData.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.GrdData.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdData.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdData.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
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
            this.GrdData.ColumnPanelRowHeight = 30;
            this.GrdData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn13,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn14});
            this.GrdData.GridControl = this.GrdPolishReport;
            this.GrdData.GroupCount = 1;
            this.GrdData.Name = "GrdData";
            this.GrdData.OptionsBehavior.AutoExpandAllGroups = true;
            this.GrdData.OptionsFilter.AllowFilterEditor = false;
            this.GrdData.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdData.OptionsPrint.ExpandAllGroups = false;
            this.GrdData.OptionsView.ColumnAutoWidth = false;
            this.GrdData.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdData.OptionsView.ShowAutoFilterRow = true;
            this.GrdData.OptionsView.ShowFooter = true;
            this.GrdData.OptionsView.ShowGroupPanel = false;
            this.GrdData.RowHeight = 25;
            this.GrdData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "TransDate";
            this.gridColumn13.FieldName = "TRANSDATE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Status";
            this.gridColumn1.FieldName = "STATUS";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Kapan";
            this.gridColumn2.FieldName = "KAPANNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "PktNo.";
            this.gridColumn3.FieldName = "PACKETNO";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 55;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tag";
            this.gridColumn4.FieldName = "TAG";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 58;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Sr.No.";
            this.gridColumn5.FieldName = "PKTSERIALNO";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Group";
            this.gridColumn6.FieldName = "MANAGER";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 141;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Party";
            this.gridColumn7.FieldName = "PARTYNAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 151;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Shape";
            this.gridColumn8.FieldName = "SHAPENAME";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Mak Crt";
            this.gridColumn9.FieldName = "MAKBALANCECARAT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Isu Crt";
            this.gridColumn10.FieldName = "ISSUECARAT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Ret Crt";
            this.gridColumn11.FieldName = "READYCARAT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn11.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "DM Crt";
            this.gridColumn12.FieldName = "DEMANDCARAT";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 11;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "CurrEmp";
            this.gridColumn14.FieldName = "CURREMPLOYEECODE";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblExport);
            this.panel2.Controls.Add(this.RbtnDetails);
            this.panel2.Controls.Add(this.RbtnSummery);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 170);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(927, 30);
            this.panel2.TabIndex = 228;
            // 
            // lblExport
            // 
            this.lblExport.AutoSize = true;
            this.lblExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExport.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblExport.ForeColor = System.Drawing.Color.Blue;
            this.lblExport.Location = new System.Drawing.Point(0, 17);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(50, 13);
            this.lblExport.TabIndex = 226;
            this.lblExport.Text = "Export";
            this.lblExport.Click += new System.EventHandler(this.lblExport_Click);
            // 
            // RbtnDetails
            // 
            this.RbtnDetails.AllowTabKeyOnEnter = false;
            this.RbtnDetails.AutoSize = true;
            this.RbtnDetails.BackColor = System.Drawing.Color.Transparent;
            this.RbtnDetails.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.RbtnDetails.ForeColor = System.Drawing.Color.Black;
            this.RbtnDetails.Location = new System.Drawing.Point(183, 5);
            this.RbtnDetails.Name = "RbtnDetails";
            this.RbtnDetails.Size = new System.Drawing.Size(69, 21);
            this.RbtnDetails.TabIndex = 225;
            this.RbtnDetails.Tag = "2";
            this.RbtnDetails.Text = "Detail";
            this.RbtnDetails.ToolTips = "";
            this.RbtnDetails.UseVisualStyleBackColor = false;
            this.RbtnDetails.CheckedChanged += new System.EventHandler(this.RbtnSummery_CheckedChanged);
            // 
            // RbtnSummery
            // 
            this.RbtnSummery.AllowTabKeyOnEnter = false;
            this.RbtnSummery.AutoSize = true;
            this.RbtnSummery.BackColor = System.Drawing.Color.Transparent;
            this.RbtnSummery.Checked = true;
            this.RbtnSummery.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.RbtnSummery.ForeColor = System.Drawing.Color.Black;
            this.RbtnSummery.Location = new System.Drawing.Point(78, 5);
            this.RbtnSummery.Name = "RbtnSummery";
            this.RbtnSummery.Size = new System.Drawing.Size(99, 21);
            this.RbtnSummery.TabIndex = 225;
            this.RbtnSummery.TabStop = true;
            this.RbtnSummery.Tag = "2";
            this.RbtnSummery.Text = "Summary";
            this.RbtnSummery.ToolTips = "";
            this.RbtnSummery.UseVisualStyleBackColor = false;
            this.RbtnSummery.CheckedChanged += new System.EventHandler(this.RbtnSummery_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cLabel3);
            this.panel3.Controls.Add(this.TxtManager);
            this.panel3.Controls.Add(this.PanelProgress);
            this.panel3.Controls.Add(this.lblCurrEmp);
            this.panel3.Controls.Add(this.txtCurrEmp);
            this.panel3.Controls.Add(this.cLabel1);
            this.panel3.Controls.Add(this.DTPFromDate);
            this.panel3.Controls.Add(this.DTPToDate);
            this.panel3.Controls.Add(this.BtnDirectPDFExport);
            this.panel3.Controls.Add(this.BtnExit);
            this.panel3.Controls.Add(this.cLabel9);
            this.panel3.Controls.Add(this.cLabel8);
            this.panel3.Controls.Add(this.RbtSizeWisePolishReport);
            this.panel3.Controls.Add(this.RbtFinalPolish4PWise);
            this.panel3.Controls.Add(this.cLabel2);
            this.panel3.Controls.Add(this.BtnSearch);
            this.panel3.Controls.Add(this.CmbKapan);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(927, 170);
            this.panel3.TabIndex = 229;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(439, 108);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(63, 13);
            this.cLabel3.TabIndex = 230;
            this.cLabel3.Text = "Manager";
            this.cLabel3.ToolTips = "";
            // 
            // TxtManager
            // 
            this.TxtManager.ActivationColor = false;
            this.TxtManager.ActivationColorCode = System.Drawing.Color.Empty;
            this.TxtManager.AllowTabKeyOnEnter = false;
            this.TxtManager.BackColor = System.Drawing.Color.White;
            this.TxtManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtManager.Font = new System.Drawing.Font("Verdana", 10F);
            this.TxtManager.Format = "";
            this.TxtManager.IsComplusory = false;
            this.TxtManager.Location = new System.Drawing.Point(512, 103);
            this.TxtManager.Name = "TxtManager";
            this.TxtManager.SelectAllTextOnFocus = true;
            this.TxtManager.Size = new System.Drawing.Size(323, 24);
            this.TxtManager.TabIndex = 229;
            this.TxtManager.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtManager.ToolTips = "";
            this.TxtManager.WaterMarkText = null;
            this.TxtManager.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtManager_KeyPress);
            // 
            // lblCurrEmp
            // 
            this.lblCurrEmp.AutoSize = true;
            this.lblCurrEmp.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrEmp.ForeColor = System.Drawing.Color.Black;
            this.lblCurrEmp.Location = new System.Drawing.Point(439, 81);
            this.lblCurrEmp.Name = "lblCurrEmp";
            this.lblCurrEmp.Size = new System.Drawing.Size(67, 13);
            this.lblCurrEmp.TabIndex = 228;
            this.lblCurrEmp.Text = "Curr Emp";
            this.lblCurrEmp.ToolTips = "";
            // 
            // txtCurrEmp
            // 
            this.txtCurrEmp.ActivationColor = false;
            this.txtCurrEmp.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtCurrEmp.AllowTabKeyOnEnter = false;
            this.txtCurrEmp.BackColor = System.Drawing.SystemColors.Info;
            this.txtCurrEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrEmp.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtCurrEmp.Format = "";
            this.txtCurrEmp.IsComplusory = false;
            this.txtCurrEmp.Location = new System.Drawing.Point(512, 75);
            this.txtCurrEmp.Name = "txtCurrEmp";
            this.txtCurrEmp.SelectAllTextOnFocus = true;
            this.txtCurrEmp.Size = new System.Drawing.Size(323, 24);
            this.txtCurrEmp.TabIndex = 227;
            this.txtCurrEmp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCurrEmp.ToolTips = "";
            this.txtCurrEmp.WaterMarkText = null;
            this.txtCurrEmp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrEmp_KeyPress);
            // 
            // FrmKapanFactoryWisePolishReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 594);
            this.Controls.Add(this.GrdPolishReport);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.RbtFactoryWise);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmKapanFactoryWisePolishReport";
            this.Text = "POLISH REPORT";
            this.Load += new System.EventHandler(this.FrmKapanFactoryWisePolishReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdPolishReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraEditors.SimpleButton BtnDirectPDFExport;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cComboBox CmbKapan;
        private AxonContLib.cLabel cLabel1;
        private DevExpress.XtraWaitForm.ProgressPanel PanelProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private AxonContLib.cRadioButton RbtFactoryWise;
        private AxonContLib.cRadioButton RbtFinalPolish4PWise;
        private AxonContLib.cRadioButton RbtSizeWisePolishReport;
        private DevExpress.XtraGrid.GridControl GrdPolishReport;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private System.Windows.Forms.Panel panel2;
        private AxonContLib.cRadioButton RbtnSummery;
        private AxonContLib.cRadioButton RbtnDetails;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblExport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private AxonContLib.cLabel lblCurrEmp;
        private AxonContLib.cTextBox txtCurrEmp;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cTextBox TxtManager;
    }
}