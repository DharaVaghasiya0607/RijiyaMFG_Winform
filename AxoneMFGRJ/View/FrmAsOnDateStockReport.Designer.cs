namespace AxoneMFGRJ.Account
{
    partial class FrmAsOnDateStockReport
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.xtraScrollableControl2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.BANDEXTRA = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.PanelClient = new AxonContLib.cPanel();
            this.ChkIsPivot = new AxonContLib.cCheckBox(this.components);
            this.panel1 = new AxonContLib.cPanel();
            this.panel2 = new AxonContLib.cPanel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnShow = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnExcelExport = new System.Windows.Forms.Button();
            this.CmbGroup1 = new AxonContLib.cComboBox(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.CmbGroup2 = new AxonContLib.cComboBox(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.CmbGroup3 = new AxonContLib.cComboBox(this.components);
            this.cLabel7 = new AxonContLib.cLabel(this.components);
            this.ChkCmbProcess = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ChkCmbRough = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ChkCmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.DTPToIssueDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromIssueDate = new AxonContLib.cDateTimePicker(this.components);
            this.labelControl1 = new AxonContLib.cLabel(this.components);
            this.lblReceiveDate = new AxonContLib.cLabel(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xtraScrollableControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.PanelClient.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbRough.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbKapan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraScrollableControl2
            // 
            this.xtraScrollableControl2.Controls.Add(this.groupControl1);
            this.xtraScrollableControl2.Controls.Add(this.PanelClient);
            this.xtraScrollableControl2.Controls.Add(this.label3);
            this.xtraScrollableControl2.Controls.Add(this.label4);
            this.xtraScrollableControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl2.Name = "xtraScrollableControl2";
            this.xtraScrollableControl2.Size = new System.Drawing.Size(1008, 412);
            this.xtraScrollableControl2.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.MainGrid);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 117);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1008, 295);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Stock Report";
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.MainGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGrid.Location = new System.Drawing.Point(2, 21);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1004, 272);
            this.MainGrid.TabIndex = 8;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.BandPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.BandPanel.Options.UseFont = true;
            this.GrdDet.Appearance.BandPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.GrdDet.Appearance.Empty.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDet.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.GrdDet.Appearance.GroupRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
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
            this.GrdDet.AppearancePrint.EvenRow.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.FilterPanel.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.FilterPanel.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.FilterPanel.BorderColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.FilterPanel.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.FilterPanel.Options.UseBorderColor = true;
            this.GrdDet.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.FooterPanel.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.FooterPanel.BorderColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseBorderColor = true;
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupFooter.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.GroupFooter.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.GroupFooter.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.GroupRow.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GrdDet.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.GroupRow.ForeColor = System.Drawing.Color.MediumBlue;
            this.GrdDet.AppearancePrint.GroupRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.GroupRow.Options.UseBorderColor = true;
            this.GrdDet.AppearancePrint.GroupRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupRow.Options.UseForeColor = true;
            this.GrdDet.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.HeaderPanel.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseBorderColor = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseForeColor = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.Preview.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.Row.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.Row.BackColor2 = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.Row.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.Row.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.Row.Options.UseBorderColor = true;
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.BANDEXTRA});
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn1});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.GroupFormat = "{1} {2}";
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.AutoExpandAllGroups = true;
            this.GrdDet.OptionsPrint.AutoResetPrintDocument = false;
            this.GrdDet.OptionsPrint.AutoWidth = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.GrdDet_CustomSummaryCalculate);
            this.GrdDet.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDet_CustomColumnDisplayText);
            // 
            // BANDEXTRA
            // 
            this.BANDEXTRA.AppearanceHeader.Options.UseTextOptions = true;
            this.BANDEXTRA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BANDEXTRA.Caption = "General";
            this.BANDEXTRA.Name = "BANDEXTRA";
            this.BANDEXTRA.VisibleIndex = 0;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "bandedGridColumn1";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            // 
            // PanelClient
            // 
            this.PanelClient.Controls.Add(this.ChkIsPivot);
            this.PanelClient.Controls.Add(this.panel1);
            this.PanelClient.Controls.Add(this.ChkCmbProcess);
            this.PanelClient.Controls.Add(this.ChkCmbRough);
            this.PanelClient.Controls.Add(this.ChkCmbKapan);
            this.PanelClient.Controls.Add(this.cLabel9);
            this.PanelClient.Controls.Add(this.cLabel3);
            this.PanelClient.Controls.Add(this.cLabel4);
            this.PanelClient.Controls.Add(this.DTPToIssueDate);
            this.PanelClient.Controls.Add(this.DTPFromIssueDate);
            this.PanelClient.Controls.Add(this.labelControl1);
            this.PanelClient.Controls.Add(this.lblReceiveDate);
            this.PanelClient.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelClient.Location = new System.Drawing.Point(0, 0);
            this.PanelClient.Name = "PanelClient";
            this.PanelClient.Size = new System.Drawing.Size(1008, 117);
            this.PanelClient.TabIndex = 0;
            // 
            // ChkIsPivot
            // 
            this.ChkIsPivot.AutoSize = true;
            this.ChkIsPivot.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ChkIsPivot.Location = new System.Drawing.Point(785, 40);
            this.ChkIsPivot.Name = "ChkIsPivot";
            this.ChkIsPivot.Size = new System.Drawing.Size(129, 20);
            this.ChkIsPivot.TabIndex = 63;
            this.ChkIsPivot.Text = "Ungroup Report";
            this.ChkIsPivot.ToolTips = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.CmbGroup1);
            this.panel1.Controls.Add(this.cLabel5);
            this.panel1.Controls.Add(this.CmbGroup2);
            this.panel1.Controls.Add(this.cLabel6);
            this.panel1.Controls.Add(this.CmbGroup3);
            this.panel1.Controls.Add(this.cLabel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 49);
            this.panel1.TabIndex = 64;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnExit);
            this.panel2.Controls.Add(this.BtnShow);
            this.panel2.Controls.Add(this.BtnPrint);
            this.panel2.Controls.Add(this.BtnAdd);
            this.panel2.Controls.Add(this.BtnExcelExport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(622, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(386, 49);
            this.panel2.TabIndex = 0;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(54)))), ((int)(((byte)(16)))));
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Location = new System.Drawing.Point(306, 9);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(73, 34);
            this.BtnExit.TabIndex = 43;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.BtnShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnShow.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.ForeColor = System.Drawing.Color.White;
            this.BtnShow.Location = new System.Drawing.Point(6, 9);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(73, 34);
            this.BtnShow.TabIndex = 10;
            this.BtnShow.Text = "&Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(156, 9);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(73, 34);
            this.BtnPrint.TabIndex = 42;
            this.BtnPrint.TabStop = false;
            this.BtnPrint.Text = "&GPrint";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.HotPink;
            this.BtnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.ForeColor = System.Drawing.Color.White;
            this.BtnAdd.Location = new System.Drawing.Point(81, 9);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(73, 34);
            this.BtnAdd.TabIndex = 62;
            this.BtnAdd.TabStop = false;
            this.BtnAdd.Text = "&Clear";
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnExcelExport
            // 
            this.BtnExcelExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.BtnExcelExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExcelExport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExcelExport.ForeColor = System.Drawing.Color.White;
            this.BtnExcelExport.Location = new System.Drawing.Point(231, 9);
            this.BtnExcelExport.Name = "BtnExcelExport";
            this.BtnExcelExport.Size = new System.Drawing.Size(73, 34);
            this.BtnExcelExport.TabIndex = 41;
            this.BtnExcelExport.TabStop = false;
            this.BtnExcelExport.Text = "&Excel";
            this.BtnExcelExport.UseVisualStyleBackColor = false;
            this.BtnExcelExport.Click += new System.EventHandler(this.BtnExcelExport_Click);
            // 
            // CmbGroup1
            // 
            this.CmbGroup1.AllowTabKeyOnEnter = false;
            this.CmbGroup1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGroup1.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.CmbGroup1.ForeColor = System.Drawing.Color.Black;
            this.CmbGroup1.FormattingEnabled = true;
            this.CmbGroup1.Items.AddRange(new object[] {
            "NONE",
            "KAPAN",
            "PACKETNO",
            "JOBWORK PARTY",
            "PROCESS",
            "ISSUE DATE",
            "RETURN DATE",
            "SHAPE",
            "CHARNI",
            "PURITY",
            "ROUGH CTS",
            "ROUGH PARTY",
            "ROUGH INVOICE"});
            this.CmbGroup1.Location = new System.Drawing.Point(32, 14);
            this.CmbGroup1.Name = "CmbGroup1";
            this.CmbGroup1.Size = new System.Drawing.Size(155, 24);
            this.CmbGroup1.TabIndex = 7;
            this.CmbGroup1.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(4, 18);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(25, 16);
            this.cLabel5.TabIndex = 56;
            this.cLabel5.Text = "G1";
            this.cLabel5.ToolTips = "";
            // 
            // CmbGroup2
            // 
            this.CmbGroup2.AllowTabKeyOnEnter = false;
            this.CmbGroup2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGroup2.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.CmbGroup2.ForeColor = System.Drawing.Color.Black;
            this.CmbGroup2.FormattingEnabled = true;
            this.CmbGroup2.Items.AddRange(new object[] {
            "NONE",
            "KAPAN",
            "PACKETNO",
            "JOBWORK PARTY",
            "PROCESS",
            "ISSUE DATE",
            "RETURN DATE",
            "SHAPE ",
            "CHARNI",
            "PURITY",
            "ROUGH CTS",
            "ROUGH PARTY",
            "ROUGH INVOICE"});
            this.CmbGroup2.Location = new System.Drawing.Point(220, 14);
            this.CmbGroup2.Name = "CmbGroup2";
            this.CmbGroup2.Size = new System.Drawing.Size(155, 24);
            this.CmbGroup2.TabIndex = 8;
            this.CmbGroup2.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(192, 18);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(25, 16);
            this.cLabel6.TabIndex = 57;
            this.cLabel6.Text = "G2";
            this.cLabel6.ToolTips = "";
            // 
            // CmbGroup3
            // 
            this.CmbGroup3.AllowTabKeyOnEnter = false;
            this.CmbGroup3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGroup3.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.CmbGroup3.ForeColor = System.Drawing.Color.Black;
            this.CmbGroup3.FormattingEnabled = true;
            this.CmbGroup3.Items.AddRange(new object[] {
            "NONE",
            "KAPAN",
            "PACKETNO",
            "JOBWORK PARTY",
            "PROCESS",
            "ISSUE DATE",
            "RETURN DATE",
            "SHAPE ",
            "CHARNI",
            "PURITY",
            "ROUGH CTS",
            "ROUGH PARTY",
            "ROUGH INVOICE"});
            this.CmbGroup3.Location = new System.Drawing.Point(408, 14);
            this.CmbGroup3.Name = "CmbGroup3";
            this.CmbGroup3.Size = new System.Drawing.Size(155, 24);
            this.CmbGroup3.TabIndex = 9;
            this.CmbGroup3.ToolTips = "";
            // 
            // cLabel7
            // 
            this.cLabel7.AutoSize = true;
            this.cLabel7.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.cLabel7.ForeColor = System.Drawing.Color.Black;
            this.cLabel7.Location = new System.Drawing.Point(381, 18);
            this.cLabel7.Name = "cLabel7";
            this.cLabel7.Size = new System.Drawing.Size(25, 16);
            this.cLabel7.TabIndex = 58;
            this.cLabel7.Text = "G3";
            this.cLabel7.ToolTips = "";
            // 
            // ChkCmbProcess
            // 
            this.ChkCmbProcess.Location = new System.Drawing.Point(555, 39);
            this.ChkCmbProcess.Name = "ChkCmbProcess";
            this.ChkCmbProcess.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ChkCmbProcess.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbProcess.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbProcess.Size = new System.Drawing.Size(224, 22);
            this.ChkCmbProcess.TabIndex = 5;
            // 
            // ChkCmbRough
            // 
            this.ChkCmbRough.Location = new System.Drawing.Point(101, 39);
            this.ChkCmbRough.Name = "ChkCmbRough";
            this.ChkCmbRough.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ChkCmbRough.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbRough.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbRough.Size = new System.Drawing.Size(359, 22);
            this.ChkCmbRough.TabIndex = 4;
            // 
            // ChkCmbKapan
            // 
            this.ChkCmbKapan.Location = new System.Drawing.Point(555, 11);
            this.ChkCmbKapan.Name = "ChkCmbKapan";
            this.ChkCmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ChkCmbKapan.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbKapan.Size = new System.Drawing.Size(359, 22);
            this.ChkCmbKapan.TabIndex = 4;
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(11, 42);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(89, 16);
            this.cLabel9.TabIndex = 50;
            this.cLabel9.Text = "Rough Name";
            this.cLabel9.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(466, 42);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(64, 16);
            this.cLabel3.TabIndex = 52;
            this.cLabel3.Text = "Process ";
            this.cLabel3.ToolTips = "";
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(466, 14);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(89, 16);
            this.cLabel4.TabIndex = 50;
            this.cLabel4.Text = "Kapan Name";
            this.cLabel4.ToolTips = "";
            // 
            // DTPToIssueDate
            // 
            this.DTPToIssueDate.AllowTabKeyOnEnter = false;
            this.DTPToIssueDate.CustomFormat = "";
            this.DTPToIssueDate.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.DTPToIssueDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToIssueDate.Location = new System.Drawing.Point(319, 9);
            this.DTPToIssueDate.Name = "DTPToIssueDate";
            this.DTPToIssueDate.ShowCheckBox = true;
            this.DTPToIssueDate.Size = new System.Drawing.Size(141, 23);
            this.DTPToIssueDate.TabIndex = 1;
            this.DTPToIssueDate.ToolTips = "";
            this.DTPToIssueDate.Value = new System.DateTime(2018, 3, 19, 0, 0, 0, 0);
            // 
            // DTPFromIssueDate
            // 
            this.DTPFromIssueDate.AllowTabKeyOnEnter = false;
            this.DTPFromIssueDate.CustomFormat = "";
            this.DTPFromIssueDate.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.DTPFromIssueDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromIssueDate.Location = new System.Drawing.Point(101, 9);
            this.DTPFromIssueDate.Name = "DTPFromIssueDate";
            this.DTPFromIssueDate.ShowCheckBox = true;
            this.DTPFromIssueDate.Size = new System.Drawing.Size(140, 23);
            this.DTPFromIssueDate.TabIndex = 0;
            this.DTPFromIssueDate.ToolTips = "";
            this.DTPFromIssueDate.Value = new System.DateTime(2018, 3, 19, 0, 0, 0, 0);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.labelControl1.ForeColor = System.Drawing.Color.Navy;
            this.labelControl1.Location = new System.Drawing.Point(251, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "To Issue";
            this.labelControl1.ToolTips = "";
            // 
            // lblReceiveDate
            // 
            this.lblReceiveDate.AutoSize = true;
            this.lblReceiveDate.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblReceiveDate.ForeColor = System.Drawing.Color.Black;
            this.lblReceiveDate.Location = new System.Drawing.Point(11, 12);
            this.lblReceiveDate.Name = "lblReceiveDate";
            this.lblReceiveDate.Size = new System.Drawing.Size(80, 16);
            this.lblReceiveDate.TabIndex = 0;
            this.lblReceiveDate.Text = "From Issue";
            this.lblReceiveDate.ToolTips = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(-12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(-12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "*";
            // 
            // FrmAsOnDateStockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.xtraScrollableControl2);
            this.Name = "FrmAsOnDateStockReport";
            this.Text = "STOCK REPORT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLedgerReport_FormClosing);
            this.xtraScrollableControl2.ResumeLayout(false);
            this.xtraScrollableControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.PanelClient.ResumeLayout(false);
            this.PanelClient.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbRough.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbKapan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private AxonContLib.cPanel PanelClient;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cLabel labelControl1;
        private AxonContLib.cLabel lblReceiveDate;
        private AxonContLib.cDateTimePicker DTPToIssueDate;
        private AxonContLib.cDateTimePicker DTPFromIssueDate;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.Button BtnExcelExport;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cComboBox CmbGroup1;
        private AxonContLib.cLabel cLabel7;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cComboBox CmbGroup2;
        private AxonContLib.cComboBox CmbGroup3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbKapan;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbProcess;
        private System.Windows.Forms.Button BtnAdd;
        public DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private AxonContLib.cCheckBox ChkIsPivot;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDEXTRA;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbRough;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cPanel panel2;

    }
}