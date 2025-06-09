namespace AxoneMFGRJ.View
{
    partial class FrmRoughStockReport
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition3 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition4 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.CmbClvEmpName = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.CmbRoughType = new AxonContLib.cComboBox(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.RbtOnHand = new AxonContLib.cRadioButton(this.components);
            this.RbtComplete = new AxonContLib.cRadioButton(this.components);
            this.RbtCurrent = new AxonContLib.cRadioButton(this.components);
            this.RbtAll = new AxonContLib.cRadioButton(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBestFit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblPrintSummary = new System.Windows.Forms.Label();
            this.lblExportSummary = new System.Windows.Forms.Label();
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GrdSummary = new DevExpress.XtraEditors.GroupControl();
            this.MainGrd = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridDetail = new DevExpress.XtraEditors.GroupControl();
            this.LblPrintDetail = new System.Windows.Forms.Label();
            this.LblExportDetail = new System.Windows.Forms.Label();
            this.MainGrdDetail = new DevExpress.XtraGrid.GridControl();
            this.GrdDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbClvEmpName.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdSummary)).BeginInit();
            this.GrdSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetail)).BeginInit();
            this.GridDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CmbClvEmpName);
            this.panel1.Controls.Add(this.cLabel3);
            this.panel1.Controls.Add(this.cLabel5);
            this.panel1.Controls.Add(this.CmbRoughType);
            this.panel1.Controls.Add(this.DTPFromDate);
            this.panel1.Controls.Add(this.DTPToDate);
            this.panel1.Controls.Add(this.cLabel9);
            this.panel1.Controls.Add(this.cLabel8);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cLabel1);
            this.panel1.Controls.Add(this.cLabel2);
            this.panel1.Controls.Add(this.CmbKapan);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnBestFit);
            this.panel1.Controls.Add(this.BtnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 72);
            this.panel1.TabIndex = 1;
            // 
            // CmbClvEmpName
            // 
            this.CmbClvEmpName.Location = new System.Drawing.Point(300, 10);
            this.CmbClvEmpName.Name = "CmbClvEmpName";
            this.CmbClvEmpName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbClvEmpName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.CmbClvEmpName.Size = new System.Drawing.Size(220, 20);
            this.CmbClvEmpName.TabIndex = 217;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(216, 14);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(68, 13);
            this.cLabel3.TabIndex = 216;
            this.cLabel3.Text = "Clv Name";
            this.cLabel3.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(216, 44);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(83, 13);
            this.cLabel5.TabIndex = 214;
            this.cLabel5.Text = "Rough Type";
            this.cLabel5.ToolTips = "";
            // 
            // CmbRoughType
            // 
            this.CmbRoughType.AllowTabKeyOnEnter = true;
            this.CmbRoughType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbRoughType.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbRoughType.ForeColor = System.Drawing.Color.Black;
            this.CmbRoughType.FormattingEnabled = true;
            this.CmbRoughType.Items.AddRange(new object[] {
            "ALL",
            "NATURAL",
            "CVD",
            "HPHT"});
            this.CmbRoughType.Location = new System.Drawing.Point(300, 38);
            this.CmbRoughType.Name = "CmbRoughType";
            this.CmbRoughType.Size = new System.Drawing.Size(220, 24);
            this.CmbRoughType.TabIndex = 213;
            this.CmbRoughType.ToolTips = "";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(84, 8);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.ShowCheckBox = true;
            this.DTPFromDate.Size = new System.Drawing.Size(129, 24);
            this.DTPFromDate.TabIndex = 211;
            this.DTPFromDate.ToolTips = "";
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(84, 38);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.ShowCheckBox = true;
            this.DTPToDate.Size = new System.Drawing.Size(129, 24);
            this.DTPToDate.TabIndex = 210;
            this.DTPToDate.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(8, 14);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(75, 13);
            this.cLabel9.TabIndex = 209;
            this.cLabel9.Text = "From Date";
            this.cLabel9.ToolTips = "";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(30, 44);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(53, 13);
            this.cLabel8.TabIndex = 208;
            this.cLabel8.Text = "ToDate";
            this.cLabel8.ToolTips = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RbtOnHand);
            this.panel2.Controls.Add(this.RbtComplete);
            this.panel2.Controls.Add(this.RbtCurrent);
            this.panel2.Controls.Add(this.RbtAll);
            this.panel2.Location = new System.Drawing.Point(610, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 23);
            this.panel2.TabIndex = 17;
            // 
            // RbtOnHand
            // 
            this.RbtOnHand.AllowTabKeyOnEnter = false;
            this.RbtOnHand.AutoSize = true;
            this.RbtOnHand.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtOnHand.ForeColor = System.Drawing.Color.Black;
            this.RbtOnHand.Location = new System.Drawing.Point(241, 3);
            this.RbtOnHand.Name = "RbtOnHand";
            this.RbtOnHand.Size = new System.Drawing.Size(82, 18);
            this.RbtOnHand.TabIndex = 12;
            this.RbtOnHand.Tag = "ONHAND";
            this.RbtOnHand.Text = "On Hand";
            this.RbtOnHand.ToolTips = "";
            this.RbtOnHand.UseVisualStyleBackColor = true;
            // 
            // RbtComplete
            // 
            this.RbtComplete.AllowTabKeyOnEnter = false;
            this.RbtComplete.AutoSize = true;
            this.RbtComplete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtComplete.ForeColor = System.Drawing.Color.Black;
            this.RbtComplete.Location = new System.Drawing.Point(145, 3);
            this.RbtComplete.Name = "RbtComplete";
            this.RbtComplete.Size = new System.Drawing.Size(87, 18);
            this.RbtComplete.TabIndex = 11;
            this.RbtComplete.Tag = "COMPLETE";
            this.RbtComplete.Text = "Complete";
            this.RbtComplete.ToolTips = "";
            this.RbtComplete.UseVisualStyleBackColor = true;
            // 
            // RbtCurrent
            // 
            this.RbtCurrent.AllowTabKeyOnEnter = false;
            this.RbtCurrent.AutoSize = true;
            this.RbtCurrent.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtCurrent.ForeColor = System.Drawing.Color.Black;
            this.RbtCurrent.Location = new System.Drawing.Point(59, 3);
            this.RbtCurrent.Name = "RbtCurrent";
            this.RbtCurrent.Size = new System.Drawing.Size(75, 18);
            this.RbtCurrent.TabIndex = 10;
            this.RbtCurrent.Tag = "CURRENT";
            this.RbtCurrent.Text = "Current";
            this.RbtCurrent.ToolTips = "";
            this.RbtCurrent.UseVisualStyleBackColor = true;
            // 
            // RbtAll
            // 
            this.RbtAll.AllowTabKeyOnEnter = false;
            this.RbtAll.AutoSize = true;
            this.RbtAll.Checked = true;
            this.RbtAll.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtAll.ForeColor = System.Drawing.Color.Black;
            this.RbtAll.Location = new System.Drawing.Point(4, 3);
            this.RbtAll.Name = "RbtAll";
            this.RbtAll.Size = new System.Drawing.Size(42, 18);
            this.RbtAll.TabIndex = 9;
            this.RbtAll.TabStop = true;
            this.RbtAll.Tag = "ALL";
            this.RbtAll.Text = "All";
            this.RbtAll.ToolTips = "";
            this.RbtAll.UseVisualStyleBackColor = true;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(539, 14);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(69, 13);
            this.cLabel1.TabIndex = 16;
            this.cLabel1.Text = "ViewType";
            this.cLabel1.ToolTips = "";
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(806, 47);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(47, 13);
            this.cLabel2.TabIndex = 0;
            this.cLabel2.Text = "Kapan";
            this.cLabel2.ToolTips = "";
            this.cLabel2.Visible = false;
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(856, 42);
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
            this.CmbKapan.Size = new System.Drawing.Size(85, 22);
            this.CmbKapan.TabIndex = 1;
            this.CmbKapan.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseTextOptions = true;
            this.BtnExit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(724, 35);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(57, 31);
            this.BtnExit.TabIndex = 15;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "Exit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnBestFit
            // 
            this.BtnBestFit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBestFit.Appearance.Options.UseFont = true;
            this.BtnBestFit.Appearance.Options.UseTextOptions = true;
            this.BtnBestFit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnBestFit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBestFit.Location = new System.Drawing.Point(648, 35);
            this.BtnBestFit.Name = "BtnBestFit";
            this.BtnBestFit.Size = new System.Drawing.Size(72, 31);
            this.BtnBestFit.TabIndex = 14;
            this.BtnBestFit.TabStop = false;
            this.BtnBestFit.Text = "Best Fit";
            this.BtnBestFit.Click += new System.EventHandler(this.BtnBestFit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.Appearance.Options.UseFont = true;
            this.BtnRefresh.Appearance.Options.UseTextOptions = true;
            this.BtnRefresh.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefresh.Location = new System.Drawing.Point(541, 35);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(103, 31);
            this.BtnRefresh.TabIndex = 11;
            this.BtnRefresh.Text = "Refresh (F5)";
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // gridView3
            // 
            this.gridView3.Name = "gridView3";
            // 
            // gridView4
            // 
            this.gridView4.Name = "gridView4";
            // 
            // gridView5
            // 
            this.gridView5.Name = "gridView5";
            // 
            // gridView6
            // 
            this.gridView6.Name = "gridView6";
            // 
            // lblPrintSummary
            // 
            this.lblPrintSummary.AutoSize = true;
            this.lblPrintSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPrintSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblPrintSummary.ForeColor = System.Drawing.Color.Blue;
            this.lblPrintSummary.Location = new System.Drawing.Point(64, 3);
            this.lblPrintSummary.Name = "lblPrintSummary";
            this.lblPrintSummary.Size = new System.Drawing.Size(38, 13);
            this.lblPrintSummary.TabIndex = 178;
            this.lblPrintSummary.Text = "Print";
            this.lblPrintSummary.Click += new System.EventHandler(this.lblPrintSummary_Click);
            // 
            // lblExportSummary
            // 
            this.lblExportSummary.AutoSize = true;
            this.lblExportSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExportSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblExportSummary.ForeColor = System.Drawing.Color.Blue;
            this.lblExportSummary.Location = new System.Drawing.Point(8, 3);
            this.lblExportSummary.Name = "lblExportSummary";
            this.lblExportSummary.Size = new System.Drawing.Size(50, 13);
            this.lblExportSummary.TabIndex = 178;
            this.lblExportSummary.Text = "Export";
            this.lblExportSummary.Click += new System.EventHandler(this.lblExportSummary_Click);
            // 
            // gridView7
            // 
            this.gridView7.Name = "gridView7";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 72);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GrdSummary);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.GridDetail);
            this.splitContainer1.Size = new System.Drawing.Size(953, 656);
            this.splitContainer1.SplitterDistance = 355;
            this.splitContainer1.TabIndex = 2;
            // 
            // GrdSummary
            // 
            this.GrdSummary.Controls.Add(this.lblPrintSummary);
            this.GrdSummary.Controls.Add(this.lblExportSummary);
            this.GrdSummary.Controls.Add(this.MainGrd);
            this.GrdSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdSummary.Location = new System.Drawing.Point(0, 0);
            this.GrdSummary.Name = "GrdSummary";
            this.GrdSummary.Size = new System.Drawing.Size(355, 656);
            this.GrdSummary.TabIndex = 0;
            // 
            // MainGrd
            // 
            this.MainGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrd.Location = new System.Drawing.Point(2, 21);
            this.MainGrd.MainView = this.GrdDet;
            this.MainGrd.Name = "MainGrd";
            this.MainGrd.Size = new System.Drawing.Size(351, 633);
            this.MainGrd.TabIndex = 183;
            this.MainGrd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.FixedLine.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GrdDet.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.BackColor2 = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.Row.Options.UseTextOptions = true;
            this.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.BackColor2 = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Options.UseTextOptions = true;
            this.GrdDet.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn37,
            this.gridColumn38});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            styleFormatCondition1.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Black;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.Appearance.Options.UseFont = true;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[SEL]=1";
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            styleFormatCondition2.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.Appearance.Options.UseFont = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition2.Expression = "([CONF_DATE]=\'\' OR [CONF_DATE] IS NULL)\r\nAND [SEL] = \'True\'";
            this.GrdDet.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.GrdDet.GridControl = this.MainGrd;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsCustomization.AllowQuickHideColumns = false;
            this.GrdDet.OptionsCustomization.AllowRowSizing = true;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsFilter.AllowMRUFilterList = false;
            this.GrdDet.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
            this.GrdDet.OptionsMenu.EnableColumnMenu = false;
            this.GrdDet.OptionsMenu.EnableFooterMenu = false;
            this.GrdDet.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDet.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsSelection.EnableAppearanceHideSelection = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "Particular";
            this.gridColumn37.FieldName = "DATATYPE";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn37.Visible = true;
            this.gridColumn37.VisibleIndex = 0;
            this.gridColumn37.Width = 157;
            // 
            // gridColumn38
            // 
            this.gridColumn38.Caption = "Carat";
            this.gridColumn38.FieldName = "CARAT";
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn38.Visible = true;
            this.gridColumn38.VisibleIndex = 1;
            this.gridColumn38.Width = 96;
            // 
            // GridDetail
            // 
            this.GridDetail.Controls.Add(this.LblPrintDetail);
            this.GridDetail.Controls.Add(this.LblExportDetail);
            this.GridDetail.Controls.Add(this.MainGrdDetail);
            this.GridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDetail.Location = new System.Drawing.Point(0, 0);
            this.GridDetail.Name = "GridDetail";
            this.GridDetail.Size = new System.Drawing.Size(594, 656);
            this.GridDetail.TabIndex = 0;
            // 
            // LblPrintDetail
            // 
            this.LblPrintDetail.AutoSize = true;
            this.LblPrintDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LblPrintDetail.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.LblPrintDetail.ForeColor = System.Drawing.Color.Blue;
            this.LblPrintDetail.Location = new System.Drawing.Point(64, 3);
            this.LblPrintDetail.Name = "LblPrintDetail";
            this.LblPrintDetail.Size = new System.Drawing.Size(38, 13);
            this.LblPrintDetail.TabIndex = 184;
            this.LblPrintDetail.Text = "Print";
            this.LblPrintDetail.Click += new System.EventHandler(this.LblPrintDetail_Click);
            // 
            // LblExportDetail
            // 
            this.LblExportDetail.AutoSize = true;
            this.LblExportDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LblExportDetail.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.LblExportDetail.ForeColor = System.Drawing.Color.Blue;
            this.LblExportDetail.Location = new System.Drawing.Point(8, 3);
            this.LblExportDetail.Name = "LblExportDetail";
            this.LblExportDetail.Size = new System.Drawing.Size(50, 13);
            this.LblExportDetail.TabIndex = 185;
            this.LblExportDetail.Text = "Export";
            this.LblExportDetail.Click += new System.EventHandler(this.LblExportDetail_Click);
            // 
            // MainGrdDetail
            // 
            this.MainGrdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrdDetail.Location = new System.Drawing.Point(2, 21);
            this.MainGrdDetail.MainView = this.GrdDetail;
            this.MainGrdDetail.Name = "MainGrdDetail";
            this.MainGrdDetail.Size = new System.Drawing.Size(590, 633);
            this.MainGrdDetail.TabIndex = 184;
            this.MainGrdDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetail});
            // 
            // GrdDetail
            // 
            this.GrdDetail.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetail.Appearance.FixedLine.Options.UseFont = true;
            this.GrdDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetail.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetail.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetail.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GrdDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetail.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDetail.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GrdDetail.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetail.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetail.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDetail.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDetail.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetail.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDetail.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetail.Appearance.HorzLine.BackColor2 = System.Drawing.Color.Gray;
            this.GrdDetail.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetail.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetail.Appearance.Row.Options.UseFont = true;
            this.GrdDetail.Appearance.Row.Options.UseTextOptions = true;
            this.GrdDetail.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDetail.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetail.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetail.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetail.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDetail.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdDetail.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDetail.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdDetail.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetail.Appearance.VertLine.BackColor2 = System.Drawing.Color.Gray;
            this.GrdDetail.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetail.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetail.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetail.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDetail.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDetail.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetail.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDetail.AppearancePrint.Lines.BackColor = System.Drawing.Color.DarkGray;
            this.GrdDetail.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDetail.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetail.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetail.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDetail.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetail.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDetail.AppearancePrint.Row.Options.UseTextOptions = true;
            this.GrdDetail.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetail.ColumnPanelRowHeight = 35;
            this.GrdDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
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
            this.gridColumn13,
            this.gridColumn14});
            styleFormatCondition3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            styleFormatCondition3.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
            styleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.Black;
            styleFormatCondition3.Appearance.Options.UseBackColor = true;
            styleFormatCondition3.Appearance.Options.UseFont = true;
            styleFormatCondition3.Appearance.Options.UseForeColor = true;
            styleFormatCondition3.ApplyToRow = true;
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition3.Expression = "[SEL]=1";
            styleFormatCondition4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            styleFormatCondition4.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
            styleFormatCondition4.Appearance.Options.UseBackColor = true;
            styleFormatCondition4.Appearance.Options.UseFont = true;
            styleFormatCondition4.ApplyToRow = true;
            styleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition4.Expression = "([CONF_DATE]=\'\' OR [CONF_DATE] IS NULL)\r\nAND [SEL] = \'True\'";
            this.GrdDetail.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition3,
            styleFormatCondition4});
            this.GrdDetail.GridControl = this.MainGrdDetail;
            this.GrdDetail.Name = "GrdDetail";
            this.GrdDetail.OptionsBehavior.Editable = false;
            this.GrdDetail.OptionsSelection.MultiSelect = true;
            this.GrdDetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDetail.OptionsView.ColumnAutoWidth = false;
            this.GrdDetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetail.OptionsView.ShowAutoFilterRow = true;
            this.GrdDetail.OptionsView.ShowFooter = true;
            this.GrdDetail.OptionsView.ShowGroupPanel = false;
            this.GrdDetail.RowHeight = 23;
            this.GrdDetail.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.GrdDetail.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.GrdDetail_CustomSummaryCalculate);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Particular";
            this.gridColumn1.FieldName = "DATATYPE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Width = 158;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Carat";
            this.gridColumn2.FieldName = "CARAT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn2.Width = 118;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Lot_ID";
            this.gridColumn3.FieldName = "LOT_ID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Lot No";
            this.gridColumn4.FieldName = "LOTNO";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Clv Lot No";
            this.gridColumn5.FieldName = "CLVLOTNO";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Cleaver";
            this.gridColumn6.FieldName = "CLEAVERCODE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Cleaver Name";
            this.gridColumn7.FieldName = "CLEAVERNAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Lot Carat";
            this.gridColumn8.FieldName = "LOTCARAT";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Rate $";
            this.gridColumn9.FieldName = "RATE";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom)});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Amount";
            this.gridColumn10.FieldName = "AMOUNT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 7;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Rough No";
            this.gridColumn11.FieldName = "ROUGHNO";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Outdate";
            this.gridColumn12.FieldName = "REJECTIONDATE";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Kapan Date";
            this.gridColumn13.DisplayFormat.FormatString = "d";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn13.FieldName = "KAPANCREATEDATE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            this.gridColumn13.Width = 101;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Remark";
            this.gridColumn14.FieldName = "KAPANREMARK";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 8;
            // 
            // FrmRoughStockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 728);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmRoughStockReport";
            this.Text = "ROUGH STOCK REPORT";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbClvEmpName.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdSummary)).EndInit();
            this.GrdSummary.ResumeLayout(false);
            this.GrdSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetail)).EndInit();
            this.GridDetail.ResumeLayout(false);
            this.GridDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel panel1;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnBestFit;
        private DevExpress.XtraEditors.SimpleButton BtnRefresh;
        private AxonContLib.cLabel cLabel1;
        private System.Windows.Forms.Panel panel2;
        private AxonContLib.cRadioButton RbtCurrent;
        private AxonContLib.cRadioButton RbtAll;
        private AxonContLib.cRadioButton RbtComplete;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cComboBox CmbRoughType;
        private System.Windows.Forms.Label lblExportSummary;
        private System.Windows.Forms.Label lblPrintSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.GroupControl GrdSummary;
        private DevExpress.XtraGrid.GridControl MainGrd;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraEditors.GroupControl GridDetail;
        private DevExpress.XtraGrid.GridControl MainGrdDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Label LblPrintDetail;
        private System.Windows.Forms.Label LblExportDetail;
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
        private AxonContLib.cRadioButton RbtOnHand;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbClvEmpName;
        private AxonContLib.cLabel cLabel3;
    }
}