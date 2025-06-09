namespace AxoneMFGRJ.View
{
    partial class FrmShapeWisePrdReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShapeWisePrdReport));
            this.BtnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.RbtMkblPrdDetail = new AxonContLib.cRadioButton(this.components);
            this.RbtFinalPrdDetail = new AxonContLib.cRadioButton(this.components);
            this.PanelProgress = new DevExpress.XtraWaitForm.ProgressPanel();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.txtMainManager = new AxonContLib.cTextBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBestFit = new DevExpress.XtraEditors.SimpleButton();
            this.GrpPacketSearch = new DevExpress.XtraEditors.GroupControl();
            this.MainGridPacketDetail = new DevExpress.XtraGrid.GridControl();
            this.GrdDetPacketDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.lblDeptPrint = new System.Windows.Forms.Label();
            this.lblDeptExport = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.MainGrd = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtEmployee = new AxonContLib.cTextBox(this.components);
            this.lblPrintSummary = new System.Windows.Forms.Label();
            this.lblExportSummary = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrpPacketSearch)).BeginInit();
            this.GrpPacketSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridPacketDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetPacketDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.Appearance.Options.UseFont = true;
            this.BtnRefresh.Appearance.Options.UseTextOptions = true;
            this.BtnRefresh.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefresh.Location = new System.Drawing.Point(756, 15);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(102, 30);
            this.BtnRefresh.TabIndex = 11;
            this.BtnRefresh.Text = "Refresh (F5)";
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RbtMkblPrdDetail);
            this.panel1.Controls.Add(this.RbtFinalPrdDetail);
            this.panel1.Controls.Add(this.PanelProgress);
            this.panel1.Controls.Add(this.cLabel3);
            this.panel1.Controls.Add(this.txtMainManager);
            this.panel1.Controls.Add(this.cLabel1);
            this.panel1.Controls.Add(this.cLabel2);
            this.panel1.Controls.Add(this.CmbKapan);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnBestFit);
            this.panel1.Controls.Add(this.BtnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1150, 62);
            this.panel1.TabIndex = 0;
            // 
            // RbtMkblPrdDetail
            // 
            this.RbtMkblPrdDetail.AllowTabKeyOnEnter = false;
            this.RbtMkblPrdDetail.AutoSize = true;
            this.RbtMkblPrdDetail.BackColor = System.Drawing.Color.Transparent;
            this.RbtMkblPrdDetail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.RbtMkblPrdDetail.ForeColor = System.Drawing.Color.Black;
            this.RbtMkblPrdDetail.Location = new System.Drawing.Point(495, 11);
            this.RbtMkblPrdDetail.Name = "RbtMkblPrdDetail";
            this.RbtMkblPrdDetail.Size = new System.Drawing.Size(99, 18);
            this.RbtMkblPrdDetail.TabIndex = 226;
            this.RbtMkblPrdDetail.Tag = "2";
            this.RbtMkblPrdDetail.Text = "Mkbl Detail";
            this.RbtMkblPrdDetail.ToolTips = "Display All Over Company Stock";
            this.RbtMkblPrdDetail.UseVisualStyleBackColor = false;
            // 
            // RbtFinalPrdDetail
            // 
            this.RbtFinalPrdDetail.AllowTabKeyOnEnter = false;
            this.RbtFinalPrdDetail.AutoSize = true;
            this.RbtFinalPrdDetail.BackColor = System.Drawing.Color.Transparent;
            this.RbtFinalPrdDetail.Checked = true;
            this.RbtFinalPrdDetail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.RbtFinalPrdDetail.ForeColor = System.Drawing.Color.Black;
            this.RbtFinalPrdDetail.Location = new System.Drawing.Point(405, 11);
            this.RbtFinalPrdDetail.Name = "RbtFinalPrdDetail";
            this.RbtFinalPrdDetail.Size = new System.Drawing.Size(84, 18);
            this.RbtFinalPrdDetail.TabIndex = 225;
            this.RbtFinalPrdDetail.TabStop = true;
            this.RbtFinalPrdDetail.Tag = "2";
            this.RbtFinalPrdDetail.Text = "Final Prd";
            this.RbtFinalPrdDetail.ToolTips = "Display All Over Company Stock";
            this.RbtFinalPrdDetail.UseVisualStyleBackColor = false;
            // 
            // PanelProgress
            // 
            this.PanelProgress.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.PanelProgress.Appearance.Options.UseBackColor = true;
            this.PanelProgress.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 12F);
            this.PanelProgress.AppearanceCaption.Options.UseFont = true;
            this.PanelProgress.AppearanceDescription.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.PanelProgress.AppearanceDescription.Options.UseFont = true;
            this.PanelProgress.Location = new System.Drawing.Point(714, 10);
            this.PanelProgress.LookAndFeel.UseDefaultLookAndFeel = false;
            this.PanelProgress.Name = "PanelProgress";
            this.PanelProgress.ShowCaption = false;
            this.PanelProgress.ShowDescription = false;
            this.PanelProgress.Size = new System.Drawing.Size(36, 41);
            this.PanelProgress.TabIndex = 205;
            this.PanelProgress.Text = "Please Wait";
            this.PanelProgress.Visible = false;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(10, 38);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(73, 13);
            this.cLabel3.TabIndex = 17;
            this.cLabel3.Text = "Main Mngr";
            this.cLabel3.ToolTips = "";
            // 
            // txtMainManager
            // 
            this.txtMainManager.ActivationColor = false;
            this.txtMainManager.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtMainManager.AllowTabKeyOnEnter = false;
            this.txtMainManager.BackColor = System.Drawing.SystemColors.Info;
            this.txtMainManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMainManager.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtMainManager.Format = "";
            this.txtMainManager.IsComplusory = false;
            this.txtMainManager.Location = new System.Drawing.Point(85, 33);
            this.txtMainManager.Name = "txtMainManager";
            this.txtMainManager.SelectAllTextOnFocus = true;
            this.txtMainManager.Size = new System.Drawing.Size(310, 22);
            this.txtMainManager.TabIndex = 16;
            this.txtMainManager.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMainManager.ToolTips = "";
            this.txtMainManager.WaterMarkText = null;
            this.txtMainManager.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMarkerCode_KeyPress);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(999, 10);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(71, 13);
            this.cLabel1.TabIndex = 4;
            this.cLabel1.Text = "Employee";
            this.cLabel1.ToolTips = "";
            this.cLabel1.Visible = false;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(10, 13);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(47, 13);
            this.cLabel2.TabIndex = 0;
            this.cLabel2.Text = "Kapan";
            this.cLabel2.ToolTips = "";
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(85, 8);
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
            this.CmbKapan.Size = new System.Drawing.Size(310, 22);
            this.CmbKapan.TabIndex = 1;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseTextOptions = true;
            this.BtnExit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(937, 15);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(56, 30);
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
            this.BtnBestFit.Location = new System.Drawing.Point(862, 15);
            this.BtnBestFit.Name = "BtnBestFit";
            this.BtnBestFit.Size = new System.Drawing.Size(71, 30);
            this.BtnBestFit.TabIndex = 14;
            this.BtnBestFit.TabStop = false;
            this.BtnBestFit.Text = "Best Fit";
            this.BtnBestFit.Click += new System.EventHandler(this.BtnBestFit_Click);
            // 
            // GrpPacketSearch
            // 
            this.GrpPacketSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrpPacketSearch.Appearance.Options.UseFont = true;
            this.GrpPacketSearch.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrpPacketSearch.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GrpPacketSearch.AppearanceCaption.Options.UseFont = true;
            this.GrpPacketSearch.AppearanceCaption.Options.UseForeColor = true;
            this.GrpPacketSearch.AppearanceCaption.Options.UseTextOptions = true;
            this.GrpPacketSearch.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrpPacketSearch.Controls.Add(this.MainGridPacketDetail);
            this.GrpPacketSearch.Controls.Add(this.lblDeptPrint);
            this.GrpPacketSearch.Controls.Add(this.lblDeptExport);
            this.GrpPacketSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpPacketSearch.Location = new System.Drawing.Point(0, 0);
            this.GrpPacketSearch.LookAndFeel.SkinName = "Whiteprint";
            this.GrpPacketSearch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GrpPacketSearch.Name = "GrpPacketSearch";
            this.GrpPacketSearch.Size = new System.Drawing.Size(737, 598);
            this.GrpPacketSearch.TabIndex = 180;
            this.GrpPacketSearch.Text = "Shape Wise Detail ";
            // 
            // MainGridPacketDetail
            // 
            this.MainGridPacketDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridPacketDetail.Location = new System.Drawing.Point(2, 22);
            this.MainGridPacketDetail.MainView = this.GrdDetPacketDetail;
            this.MainGridPacketDetail.Name = "MainGridPacketDetail";
            this.MainGridPacketDetail.Size = new System.Drawing.Size(733, 574);
            this.MainGridPacketDetail.TabIndex = 177;
            this.MainGridPacketDetail.ToolTipController = this.toolTipController1;
            this.MainGridPacketDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetPacketDetail});
            // 
            // GrdDetPacketDetail
            // 
            this.GrdDetPacketDetail.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetPacketDetail.Appearance.FixedLine.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetPacketDetail.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetPacketDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetPacketDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetPacketDetail.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.GrdDetPacketDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDetPacketDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetPacketDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetPacketDetail.Appearance.HorzLine.BackColor = System.Drawing.Color.Black;
            this.GrdDetPacketDetail.Appearance.HorzLine.BackColor2 = System.Drawing.Color.Black;
            this.GrdDetPacketDetail.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetPacketDetail.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetPacketDetail.Appearance.Row.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetPacketDetail.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDetPacketDetail.Appearance.VertLine.BackColor = System.Drawing.Color.Black;
            this.GrdDetPacketDetail.Appearance.VertLine.BackColor2 = System.Drawing.Color.Black;
            this.GrdDetPacketDetail.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetPacketDetail.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetPacketDetail.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPacketDetail.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDetPacketDetail.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPacketDetail.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.Lines.BackColor = System.Drawing.Color.DarkGray;
            this.GrdDetPacketDetail.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDetPacketDetail.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetPacketDetail.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPacketDetail.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDetPacketDetail.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDetPacketDetail.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPacketDetail.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDetPacketDetail.ColumnPanelRowHeight = 25;
            this.GrdDetPacketDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn5,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14});
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
            this.GrdDetPacketDetail.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.GrdDetPacketDetail.GridControl = this.MainGridPacketDetail;
            this.GrdDetPacketDetail.Name = "GrdDetPacketDetail";
            this.GrdDetPacketDetail.OptionsBehavior.Editable = false;
            this.GrdDetPacketDetail.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetPacketDetail.OptionsMenu.EnableColumnMenu = false;
            this.GrdDetPacketDetail.OptionsMenu.EnableFooterMenu = false;
            this.GrdDetPacketDetail.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDetPacketDetail.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDetPacketDetail.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDetPacketDetail.OptionsSelection.EnableAppearanceHideSelection = false;
            this.GrdDetPacketDetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDetPacketDetail.OptionsView.ColumnAutoWidth = false;
            this.GrdDetPacketDetail.OptionsView.ShowAutoFilterRow = true;
            this.GrdDetPacketDetail.OptionsView.ShowFooter = true;
            this.GrdDetPacketDetail.OptionsView.ShowGroupPanel = false;
            this.GrdDetPacketDetail.RowHeight = 23;
            this.GrdDetPacketDetail.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = "Kapan";
            this.gridColumn1.FieldName = "KAPANNAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 88;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.Caption = "Shp";
            this.gridColumn4.FieldName = "SHP";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 55;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.Caption = "PktNo";
            this.gridColumn7.FieldName = "PACKETNO";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 74;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.Caption = "Tag";
            this.gridColumn5.FieldName = "TAG";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 55;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.Caption = "Carat";
            this.gridColumn8.FieldName = "EXPCRT";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 71;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.Caption = "Amount";
            this.gridColumn9.FieldName = "AMOUNT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 12;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "PrdType";
            this.gridColumn2.FieldName = "PRDTYPE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.Caption = "Col";
            this.gridColumn3.FieldName = "COLOR";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            this.gridColumn3.Width = 55;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.Caption = "Clr";
            this.gridColumn6.FieldName = "CLARITY";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 55;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.Caption = "Cut";
            this.gridColumn10.FieldName = "CUT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 8;
            this.gridColumn10.Width = 55;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn11.AppearanceCell.Options.UseFont = true;
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.Caption = "Pol";
            this.gridColumn11.FieldName = "POL";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 9;
            this.gridColumn11.Width = 55;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn12.AppearanceCell.Options.UseFont = true;
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn12.AppearanceHeader.Options.UseFont = true;
            this.gridColumn12.Caption = "Sym";
            this.gridColumn12.FieldName = "SYM";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 10;
            this.gridColumn12.Width = 55;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.gridColumn13.AppearanceCell.Options.UseFont = true;
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn13.AppearanceHeader.Options.UseFont = true;
            this.gridColumn13.Caption = "Fl";
            this.gridColumn13.FieldName = "FL";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 11;
            this.gridColumn13.Width = 55;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn14.AppearanceCell.Options.UseFont = true;
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn14.AppearanceHeader.Options.UseFont = true;
            this.gridColumn14.Caption = "Packet";
            this.gridColumn14.FieldName = "PACKETCATEGORY";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 13;
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // lblDeptPrint
            // 
            this.lblDeptPrint.AutoSize = true;
            this.lblDeptPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeptPrint.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblDeptPrint.ForeColor = System.Drawing.Color.Blue;
            this.lblDeptPrint.Location = new System.Drawing.Point(74, 3);
            this.lblDeptPrint.Name = "lblDeptPrint";
            this.lblDeptPrint.Size = new System.Drawing.Size(38, 13);
            this.lblDeptPrint.TabIndex = 178;
            this.lblDeptPrint.Text = "Print";
            this.lblDeptPrint.Click += new System.EventHandler(this.lblDeptPrint_Click);
            // 
            // lblDeptExport
            // 
            this.lblDeptExport.AutoSize = true;
            this.lblDeptExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeptExport.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblDeptExport.ForeColor = System.Drawing.Color.Blue;
            this.lblDeptExport.Location = new System.Drawing.Point(12, 3);
            this.lblDeptExport.Name = "lblDeptExport";
            this.lblDeptExport.Size = new System.Drawing.Size(50, 13);
            this.lblDeptExport.TabIndex = 178;
            this.lblDeptExport.Text = "Export";
            this.lblDeptExport.Click += new System.EventHandler(this.lblDeptExport_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 62);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.GrpPacketSearch);
            this.splitContainer1.Size = new System.Drawing.Size(1150, 598);
            this.splitContainer1.SplitterDistance = 409;
            this.splitContainer1.TabIndex = 182;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.MainGrd);
            this.groupControl1.Controls.Add(this.txtEmployee);
            this.groupControl1.Controls.Add(this.lblPrintSummary);
            this.groupControl1.Controls.Add(this.lblExportSummary);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(409, 598);
            this.groupControl1.TabIndex = 181;
            this.groupControl1.Text = "Shape Wise Summary";
            // 
            // MainGrd
            // 
            this.MainGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrd.Location = new System.Drawing.Point(2, 22);
            this.MainGrd.MainView = this.GrdDet;
            this.MainGrd.Name = "MainGrd";
            this.MainGrd.Size = new System.Drawing.Size(405, 574);
            this.MainGrd.TabIndex = 179;
            this.MainGrd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet,
            this.gridView1});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.FixedLine.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
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
            this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.BackColor2 = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 8F);
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
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.DarkGray;
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Options.UseTextOptions = true;
            this.GrdDet.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.bandedGridColumn13,
            this.bandedGridColumn1,
            this.bandedGridColumn2,
            this.bandedGridColumn3,
            this.gridColumn17});
            this.GrdDet.FooterPanelHeight = 25;
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
            this.GrdDet.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition3,
            styleFormatCondition4});
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
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
            this.GrdDet.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.GrdDet_CellMerge);
            this.GrdDet.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GrdDet_RowStyle);
            // 
            // bandedGridColumn13
            // 
            this.bandedGridColumn13.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bandedGridColumn13.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn13.Caption = "Shape";
            this.bandedGridColumn13.FieldName = "SHP";
            this.bandedGridColumn13.Name = "bandedGridColumn13";
            this.bandedGridColumn13.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.bandedGridColumn13.Visible = true;
            this.bandedGridColumn13.VisibleIndex = 0;
            this.bandedGridColumn13.Width = 70;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "Pcs";
            this.bandedGridColumn1.FieldName = "PCS";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.VisibleIndex = 1;
            this.bandedGridColumn1.Width = 77;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "ExpCrt";
            this.bandedGridColumn2.FieldName = "ExpCrt";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.VisibleIndex = 2;
            this.bandedGridColumn2.Width = 93;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "Amount";
            this.bandedGridColumn3.FieldName = "Amount";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.bandedGridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.VisibleIndex = 3;
            this.bandedGridColumn3.Width = 102;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Shape_ID";
            this.gridColumn17.FieldName = "Shape_ID";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.MainGrd;
            this.gridView1.Name = "gridView1";
            // 
            // txtEmployee
            // 
            this.txtEmployee.ActivationColor = true;
            this.txtEmployee.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtEmployee.AllowTabKeyOnEnter = false;
            this.txtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployee.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployee.Format = "";
            this.txtEmployee.IsComplusory = false;
            this.txtEmployee.Location = new System.Drawing.Point(191, 131);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.SelectAllTextOnFocus = true;
            this.txtEmployee.Size = new System.Drawing.Size(166, 23);
            this.txtEmployee.TabIndex = 3;
            this.txtEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmployee.ToolTips = "";
            this.txtEmployee.Visible = false;
            this.txtEmployee.WaterMarkText = null;
            this.txtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployee_KeyPress);
            // 
            // lblPrintSummary
            // 
            this.lblPrintSummary.AutoSize = true;
            this.lblPrintSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPrintSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblPrintSummary.ForeColor = System.Drawing.Color.Blue;
            this.lblPrintSummary.Location = new System.Drawing.Point(68, 3);
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
            this.lblExportSummary.Location = new System.Drawing.Point(6, 3);
            this.lblExportSummary.Name = "lblExportSummary";
            this.lblExportSummary.Size = new System.Drawing.Size(50, 13);
            this.lblExportSummary.TabIndex = 178;
            this.lblExportSummary.Text = "Export";
            this.lblExportSummary.Click += new System.EventHandler(this.lblExportSummary_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // FrmShapeWisePrdReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1150, 660);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmShapeWisePrdReport";
            this.Text = "SHAPE WISE PRD REPORT";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMarkerRollingReport_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrpPacketSearch)).EndInit();
            this.GrpPacketSearch.ResumeLayout(false);
            this.GrpPacketSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridPacketDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetPacketDetail)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnRefresh;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnBestFit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.GroupControl GrpPacketSearch;
        private DevExpress.XtraGrid.GridControl MainGridPacketDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetPacketDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.Windows.Forms.Label lblDeptPrint;
        private System.Windows.Forms.Label lblDeptExport;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cLabel cLabel1;
        private DevExpress.XtraGrid.GridControl MainGrd;
        private AxonContLib.cTextBox txtEmployee;
        private System.Windows.Forms.Label lblPrintSummary;
        private System.Windows.Forms.Label lblExportSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraWaitForm.ProgressPanel PanelProgress;
        private AxonContLib.cTextBox txtMainManager;
        private AxonContLib.cLabel cLabel3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private AxonContLib.cRadioButton RbtMkblPrdDetail;
        private AxonContLib.cRadioButton RbtFinalPrdDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
    }
}