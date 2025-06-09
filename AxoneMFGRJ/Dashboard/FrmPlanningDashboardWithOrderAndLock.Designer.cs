namespace AxoneMFGRJ.Dashboard
{
    partial class FrmPlanningDashboardWithOrderAndLock
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView1 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView2 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView3 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.BtnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.XtraTabDateControl = new DevExpress.XtraTab.XtraTabControl();
            this.TabToday = new DevExpress.XtraTab.XtraTabPage();
            this.TabYesterday = new DevExpress.XtraTab.XtraTabPage();
            this.TabThisWeek = new DevExpress.XtraTab.XtraTabPage();
            this.TabThisMonth = new DevExpress.XtraTab.XtraTabPage();
            this.TabThisQuater = new DevExpress.XtraTab.XtraTabPage();
            this.TabThis6Month = new DevExpress.XtraTab.XtraTabPage();
            this.TabThisYear = new DevExpress.XtraTab.XtraTabPage();
            this.TabThisCustom = new DevExpress.XtraTab.XtraTabPage();
            this.panel4 = new AxonContLib.cPanel(this.components);
            this.PnlLoding = new DevExpress.XtraWaitForm.ProgressPanel();
            this.lblTime = new AxonContLib.cLabel(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.cPanel2 = new AxonContLib.cPanel(this.components);
            this.rbtRoughStatusAll = new AxonContLib.cRadioButton(this.components);
            this.rbtRoughStatusCVD = new AxonContLib.cRadioButton(this.components);
            this.rbtRoughStatusNatural = new AxonContLib.cRadioButton(this.components);
            this.rbtRoughStatusHPHT = new AxonContLib.cRadioButton(this.components);
            this.cPanel1 = new AxonContLib.cPanel(this.components);
            this.ChkSummAmt = new AxonContLib.cCheckBox(this.components);
            this.ChkSummPcs = new AxonContLib.cCheckBox(this.components);
            this.ChkSummExpPer = new AxonContLib.cCheckBox(this.components);
            this.ChkSummPktCts = new AxonContLib.cCheckBox(this.components);
            this.ChkSummExpCts = new AxonContLib.cCheckBox(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel11 = new AxonContLib.cLabel(this.components);
            this.PvtMakGrid = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.ColMakSizeName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ColMakFromCarat = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ColMakShape = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ColMakShapeSeqNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ColMakPcs = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ColMakPktCts = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ColMakExpCts = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ColMakExpPer = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ColMakAmount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.DockSideMenu = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerLeft = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.MainGridFilter = new DevExpress.XtraGrid.GridControl();
            this.GrdDetFilter = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkFilter = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkIsActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repTxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtRepEmployee = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtDepartment = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtProcess = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.CmbRoughType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.MainGridMakable = new DevExpress.XtraGrid.GridControl();
            this.GrdDetMakable = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cPanel3 = new AxonContLib.cPanel(this.components);
            this.RbtYearly = new AxonContLib.cRadioButton(this.components);
            this.RbtQuaterly = new AxonContLib.cRadioButton(this.components);
            this.RbtDaily = new AxonContLib.cRadioButton(this.components);
            this.RbtWeekly = new AxonContLib.cRadioButton(this.components);
            this.RbtMonthly = new AxonContLib.cRadioButton(this.components);
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.ChartControlAreaMakable = new DevExpress.XtraCharts.ChartControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.BtnExpandMainGrid = new DevExpress.XtraEditors.SimpleButton();
            this.lblPrintSummary = new System.Windows.Forms.Label();
            this.MainGrd = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn52 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn55 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn56 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn58 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn59 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn60 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn61 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn62 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn63 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn64 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblExportSummary = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.XtraTabDateControl)).BeginInit();
            this.XtraTabDateControl.SuspendLayout();
            this.panel4.SuspendLayout();
            this.cPanel2.SuspendLayout();
            this.cPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PvtMakGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DockSideMenu)).BeginInit();
            this.hideContainerLeft.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbRoughType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridMakable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetMakable)).BeginInit();
            this.cPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControlAreaMakable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnLoad
            // 
            this.BtnLoad.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnLoad.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnLoad.Appearance.Options.UseFont = true;
            this.BtnLoad.Appearance.Options.UseForeColor = true;
            this.BtnLoad.Appearance.Options.UseTextOptions = true;
            this.BtnLoad.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.BtnLoad.Location = new System.Drawing.Point(1064, 10);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(100, 60);
            this.BtnLoad.TabIndex = 30;
            this.BtnLoad.Text = "&Load F5";
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // XtraTabDateControl
            // 
            this.XtraTabDateControl.AppearancePage.Header.Font = new System.Drawing.Font("Verdana", 9F);
            this.XtraTabDateControl.AppearancePage.Header.Options.UseFont = true;
            this.XtraTabDateControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.XtraTabDateControl.AppearancePage.HeaderActive.Options.UseFont = true;
            this.XtraTabDateControl.Location = new System.Drawing.Point(9, 10);
            this.XtraTabDateControl.LookAndFeel.SkinName = "Sharp";
            this.XtraTabDateControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.XtraTabDateControl.Name = "XtraTabDateControl";
            this.XtraTabDateControl.SelectedTabPage = this.TabToday;
            this.XtraTabDateControl.Size = new System.Drawing.Size(729, 27);
            this.XtraTabDateControl.TabIndex = 322;
            this.XtraTabDateControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabToday,
            this.TabYesterday,
            this.TabThisWeek,
            this.TabThisMonth,
            this.TabThisQuater,
            this.TabThis6Month,
            this.TabThisYear,
            this.TabThisCustom});
            this.XtraTabDateControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.XtraTabDateControl_SelectedPageChanged);
            // 
            // TabToday
            // 
            this.TabToday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabToday.Name = "TabToday";
            this.TabToday.Size = new System.Drawing.Size(723, 0);
            this.TabToday.Tag = "TODAY";
            this.TabToday.Text = "Today";
            // 
            // TabYesterday
            // 
            this.TabYesterday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabYesterday.Name = "TabYesterday";
            this.TabYesterday.Size = new System.Drawing.Size(723, 0);
            this.TabYesterday.Tag = "YESTERDAY";
            this.TabYesterday.Text = "Yesterday";
            // 
            // TabThisWeek
            // 
            this.TabThisWeek.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabThisWeek.Name = "TabThisWeek";
            this.TabThisWeek.Size = new System.Drawing.Size(723, 0);
            this.TabThisWeek.Tag = "WEEK";
            this.TabThisWeek.Text = "This Week";
            // 
            // TabThisMonth
            // 
            this.TabThisMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabThisMonth.Name = "TabThisMonth";
            this.TabThisMonth.Size = new System.Drawing.Size(723, 0);
            this.TabThisMonth.Tag = "MONTH";
            this.TabThisMonth.Text = "This Month";
            // 
            // TabThisQuater
            // 
            this.TabThisQuater.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabThisQuater.Name = "TabThisQuater";
            this.TabThisQuater.Size = new System.Drawing.Size(723, 0);
            this.TabThisQuater.Tag = "QUATER";
            this.TabThisQuater.Text = "This Quater";
            // 
            // TabThis6Month
            // 
            this.TabThis6Month.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabThis6Month.Name = "TabThis6Month";
            this.TabThis6Month.Size = new System.Drawing.Size(723, 0);
            this.TabThis6Month.Tag = "6MONTH";
            this.TabThis6Month.Text = "This 6 Month";
            // 
            // TabThisYear
            // 
            this.TabThisYear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabThisYear.Name = "TabThisYear";
            this.TabThisYear.Size = new System.Drawing.Size(723, 0);
            this.TabThisYear.Tag = "YEAR";
            this.TabThisYear.Text = "This Year";
            // 
            // TabThisCustom
            // 
            this.TabThisCustom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabThisCustom.Name = "TabThisCustom";
            this.TabThisCustom.Size = new System.Drawing.Size(723, 0);
            this.TabThisCustom.Tag = "CUSTOME";
            this.TabThisCustom.Text = "Custom";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.PnlLoding);
            this.panel4.Controls.Add(this.lblTime);
            this.panel4.Controls.Add(this.DTPToDate);
            this.panel4.Controls.Add(this.cPanel2);
            this.panel4.Controls.Add(this.XtraTabDateControl);
            this.panel4.Controls.Add(this.cPanel1);
            this.panel4.Controls.Add(this.DTPFromDate);
            this.panel4.Controls.Add(this.cLabel11);
            this.panel4.Controls.Add(this.BtnLoad);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1370, 81);
            this.panel4.TabIndex = 0;
            // 
            // PnlLoding
            // 
            this.PnlLoding.Appearance.BackColor = System.Drawing.Color.White;
            this.PnlLoding.Appearance.Options.UseBackColor = true;
            this.PnlLoding.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 12F);
            this.PnlLoding.AppearanceCaption.Options.UseFont = true;
            this.PnlLoding.AppearanceDescription.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.PnlLoding.AppearanceDescription.Options.UseFont = true;
            this.PnlLoding.BarAnimationElementThickness = 2;
            this.PnlLoding.Location = new System.Drawing.Point(1097, 32);
            this.PnlLoding.LookAndFeel.UseDefaultLookAndFeel = false;
            this.PnlLoding.Name = "PnlLoding";
            this.PnlLoding.ShowCaption = false;
            this.PnlLoding.ShowDescription = false;
            this.PnlLoding.Size = new System.Drawing.Size(28, 29);
            this.PnlLoding.TabIndex = 247;
            this.PnlLoding.Text = "progressPanel1";
            this.PnlLoding.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.Purple;
            this.lblTime.Location = new System.Drawing.Point(678, 52);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(71, 14);
            this.lblTime.TabIndex = 246;
            this.lblTime.Text = "00:00:00";
            this.lblTime.ToolTips = "";
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPToDate.Location = new System.Drawing.Point(926, 10);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(124, 25);
            this.DTPToDate.TabIndex = 242;
            this.DTPToDate.ToolTips = "";
            // 
            // cPanel2
            // 
            this.cPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cPanel2.Controls.Add(this.rbtRoughStatusAll);
            this.cPanel2.Controls.Add(this.rbtRoughStatusCVD);
            this.cPanel2.Controls.Add(this.rbtRoughStatusNatural);
            this.cPanel2.Controls.Add(this.rbtRoughStatusHPHT);
            this.cPanel2.Location = new System.Drawing.Point(9, 45);
            this.cPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cPanel2.Name = "cPanel2";
            this.cPanel2.Size = new System.Drawing.Size(265, 26);
            this.cPanel2.TabIndex = 240;
            // 
            // rbtRoughStatusAll
            // 
            this.rbtRoughStatusAll.AllowTabKeyOnEnter = false;
            this.rbtRoughStatusAll.AutoSize = true;
            this.rbtRoughStatusAll.Checked = true;
            this.rbtRoughStatusAll.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtRoughStatusAll.ForeColor = System.Drawing.Color.Black;
            this.rbtRoughStatusAll.Location = new System.Drawing.Point(3, 2);
            this.rbtRoughStatusAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbtRoughStatusAll.Name = "rbtRoughStatusAll";
            this.rbtRoughStatusAll.Size = new System.Drawing.Size(39, 17);
            this.rbtRoughStatusAll.TabIndex = 237;
            this.rbtRoughStatusAll.TabStop = true;
            this.rbtRoughStatusAll.Tag = "";
            this.rbtRoughStatusAll.Text = "All";
            this.rbtRoughStatusAll.ToolTips = "";
            this.rbtRoughStatusAll.UseVisualStyleBackColor = true;
            this.rbtRoughStatusAll.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // rbtRoughStatusCVD
            // 
            this.rbtRoughStatusCVD.AllowTabKeyOnEnter = false;
            this.rbtRoughStatusCVD.AutoSize = true;
            this.rbtRoughStatusCVD.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtRoughStatusCVD.ForeColor = System.Drawing.Color.Black;
            this.rbtRoughStatusCVD.Location = new System.Drawing.Point(57, 2);
            this.rbtRoughStatusCVD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbtRoughStatusCVD.Name = "rbtRoughStatusCVD";
            this.rbtRoughStatusCVD.Size = new System.Drawing.Size(51, 17);
            this.rbtRoughStatusCVD.TabIndex = 239;
            this.rbtRoughStatusCVD.Tag = "";
            this.rbtRoughStatusCVD.Text = "CVD";
            this.rbtRoughStatusCVD.ToolTips = "";
            this.rbtRoughStatusCVD.UseVisualStyleBackColor = true;
            this.rbtRoughStatusCVD.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // rbtRoughStatusNatural
            // 
            this.rbtRoughStatusNatural.AllowTabKeyOnEnter = false;
            this.rbtRoughStatusNatural.AutoSize = true;
            this.rbtRoughStatusNatural.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtRoughStatusNatural.ForeColor = System.Drawing.Color.Black;
            this.rbtRoughStatusNatural.Location = new System.Drawing.Point(181, 2);
            this.rbtRoughStatusNatural.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbtRoughStatusNatural.Name = "rbtRoughStatusNatural";
            this.rbtRoughStatusNatural.Size = new System.Drawing.Size(66, 17);
            this.rbtRoughStatusNatural.TabIndex = 238;
            this.rbtRoughStatusNatural.Tag = "";
            this.rbtRoughStatusNatural.Text = "Natural";
            this.rbtRoughStatusNatural.ToolTips = "";
            this.rbtRoughStatusNatural.UseVisualStyleBackColor = true;
            this.rbtRoughStatusNatural.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // rbtRoughStatusHPHT
            // 
            this.rbtRoughStatusHPHT.AllowTabKeyOnEnter = false;
            this.rbtRoughStatusHPHT.AutoSize = true;
            this.rbtRoughStatusHPHT.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtRoughStatusHPHT.ForeColor = System.Drawing.Color.Black;
            this.rbtRoughStatusHPHT.Location = new System.Drawing.Point(119, 2);
            this.rbtRoughStatusHPHT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbtRoughStatusHPHT.Name = "rbtRoughStatusHPHT";
            this.rbtRoughStatusHPHT.Size = new System.Drawing.Size(55, 17);
            this.rbtRoughStatusHPHT.TabIndex = 239;
            this.rbtRoughStatusHPHT.Tag = "";
            this.rbtRoughStatusHPHT.Text = "HPHT";
            this.rbtRoughStatusHPHT.ToolTips = "";
            this.rbtRoughStatusHPHT.UseVisualStyleBackColor = true;
            this.rbtRoughStatusHPHT.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // cPanel1
            // 
            this.cPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cPanel1.Controls.Add(this.ChkSummAmt);
            this.cPanel1.Controls.Add(this.ChkSummPcs);
            this.cPanel1.Controls.Add(this.ChkSummExpPer);
            this.cPanel1.Controls.Add(this.ChkSummPktCts);
            this.cPanel1.Controls.Add(this.ChkSummExpCts);
            this.cPanel1.Location = new System.Drawing.Point(286, 45);
            this.cPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cPanel1.Name = "cPanel1";
            this.cPanel1.Size = new System.Drawing.Size(356, 26);
            this.cPanel1.TabIndex = 240;
            // 
            // ChkSummAmt
            // 
            this.ChkSummAmt.AllowTabKeyOnEnter = false;
            this.ChkSummAmt.AutoSize = true;
            this.ChkSummAmt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummAmt.Location = new System.Drawing.Point(281, 3);
            this.ChkSummAmt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkSummAmt.Name = "ChkSummAmt";
            this.ChkSummAmt.Size = new System.Drawing.Size(52, 17);
            this.ChkSummAmt.TabIndex = 9;
            this.ChkSummAmt.Text = "Amt";
            this.ChkSummAmt.ToolTips = "";
            this.ChkSummAmt.UseVisualStyleBackColor = true;
            this.ChkSummAmt.CheckedChanged += new System.EventHandler(this.ChkSummAmt_CheckedChanged);
            // 
            // ChkSummPcs
            // 
            this.ChkSummPcs.AllowTabKeyOnEnter = false;
            this.ChkSummPcs.AutoSize = true;
            this.ChkSummPcs.Checked = true;
            this.ChkSummPcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSummPcs.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummPcs.Location = new System.Drawing.Point(3, 3);
            this.ChkSummPcs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkSummPcs.Name = "ChkSummPcs";
            this.ChkSummPcs.Size = new System.Drawing.Size(48, 17);
            this.ChkSummPcs.TabIndex = 9;
            this.ChkSummPcs.Text = "Pcs";
            this.ChkSummPcs.ToolTips = "";
            this.ChkSummPcs.UseVisualStyleBackColor = true;
            this.ChkSummPcs.CheckedChanged += new System.EventHandler(this.ChkSummPcs_CheckedChanged);
            // 
            // ChkSummExpPer
            // 
            this.ChkSummExpPer.AllowTabKeyOnEnter = false;
            this.ChkSummExpPer.AutoSize = true;
            this.ChkSummExpPer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummExpPer.Location = new System.Drawing.Point(208, 3);
            this.ChkSummExpPer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkSummExpPer.Name = "ChkSummExpPer";
            this.ChkSummExpPer.Size = new System.Drawing.Size(68, 17);
            this.ChkSummExpPer.TabIndex = 9;
            this.ChkSummExpPer.Text = "Exp %";
            this.ChkSummExpPer.ToolTips = "";
            this.ChkSummExpPer.UseVisualStyleBackColor = true;
            this.ChkSummExpPer.CheckedChanged += new System.EventHandler(this.ChkSummExpPer_CheckedChanged);
            // 
            // ChkSummPktCts
            // 
            this.ChkSummPktCts.AllowTabKeyOnEnter = false;
            this.ChkSummPktCts.AutoSize = true;
            this.ChkSummPktCts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummPktCts.Location = new System.Drawing.Point(54, 3);
            this.ChkSummPktCts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkSummPktCts.Name = "ChkSummPktCts";
            this.ChkSummPktCts.Size = new System.Drawing.Size(71, 17);
            this.ChkSummPktCts.TabIndex = 9;
            this.ChkSummPktCts.Text = "Pkt Cts";
            this.ChkSummPktCts.ToolTips = "";
            this.ChkSummPktCts.UseVisualStyleBackColor = true;
            this.ChkSummPktCts.CheckedChanged += new System.EventHandler(this.ChkSummPktCts_CheckedChanged);
            // 
            // ChkSummExpCts
            // 
            this.ChkSummExpCts.AllowTabKeyOnEnter = false;
            this.ChkSummExpCts.AutoSize = true;
            this.ChkSummExpCts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummExpCts.Location = new System.Drawing.Point(129, 3);
            this.ChkSummExpCts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkSummExpCts.Name = "ChkSummExpCts";
            this.ChkSummExpCts.Size = new System.Drawing.Size(74, 17);
            this.ChkSummExpCts.TabIndex = 9;
            this.ChkSummExpCts.Text = "Exp Cts";
            this.ChkSummExpCts.ToolTips = "";
            this.ChkSummExpCts.UseVisualStyleBackColor = true;
            this.ChkSummExpCts.CheckedChanged += new System.EventHandler(this.ChkSummExpCts_CheckedChanged);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(796, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(124, 25);
            this.DTPFromDate.TabIndex = 241;
            this.DTPFromDate.ToolTips = "";
            this.DTPFromDate.Value = new System.DateTime(2019, 4, 9, 0, 0, 0, 0);
            // 
            // cLabel11
            // 
            this.cLabel11.AutoSize = true;
            this.cLabel11.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel11.ForeColor = System.Drawing.Color.Black;
            this.cLabel11.Location = new System.Drawing.Point(756, 15);
            this.cLabel11.Name = "cLabel11";
            this.cLabel11.Size = new System.Drawing.Size(34, 13);
            this.cLabel11.TabIndex = 240;
            this.cLabel11.Text = "Date";
            this.cLabel11.ToolTips = "";
            // 
            // PvtMakGrid
            // 
            this.PvtMakGrid.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.PvtMakGrid.Appearance.Cell.Options.UseForeColor = true;
            this.PvtMakGrid.Appearance.ColumnHeaderArea.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.ColumnHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.PvtMakGrid.Appearance.ColumnHeaderArea.Options.UseFont = true;
            this.PvtMakGrid.Appearance.ColumnHeaderArea.Options.UseForeColor = true;
            this.PvtMakGrid.Appearance.ColumnHeaderArea.Options.UseTextOptions = true;
            this.PvtMakGrid.Appearance.ColumnHeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PvtMakGrid.Appearance.CustomTotalCell.ForeColor = System.Drawing.Color.Black;
            this.PvtMakGrid.Appearance.CustomTotalCell.Options.UseForeColor = true;
            this.PvtMakGrid.Appearance.DataHeaderArea.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.DataHeaderArea.Options.UseFont = true;
            this.PvtMakGrid.Appearance.DataHeaderArea.Options.UseTextOptions = true;
            this.PvtMakGrid.Appearance.DataHeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PvtMakGrid.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.FieldHeader.Options.UseFont = true;
            this.PvtMakGrid.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.FieldValue.Options.UseFont = true;
            this.PvtMakGrid.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.PvtMakGrid.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.FieldValueTotal.Options.UseFont = true;
            this.PvtMakGrid.Appearance.FilterHeaderArea.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.FilterHeaderArea.Options.UseFont = true;
            this.PvtMakGrid.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.PvtMakGrid.Appearance.GrandTotalCell.Options.UseFont = true;
            this.PvtMakGrid.Appearance.GrandTotalCell.Options.UseForeColor = true;
            this.PvtMakGrid.Appearance.HeaderArea.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.HeaderArea.Options.UseFont = true;
            this.PvtMakGrid.Appearance.HeaderArea.Options.UseTextOptions = true;
            this.PvtMakGrid.Appearance.HeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PvtMakGrid.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.Appearance.TotalCell.ForeColor = System.Drawing.Color.Black;
            this.PvtMakGrid.Appearance.TotalCell.Options.UseFont = true;
            this.PvtMakGrid.Appearance.TotalCell.Options.UseForeColor = true;
            this.PvtMakGrid.AppearancePrint.CustomTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.AppearancePrint.CustomTotalCell.Options.UseFont = true;
            this.PvtMakGrid.AppearancePrint.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.AppearancePrint.FieldHeader.Options.UseFont = true;
            this.PvtMakGrid.AppearancePrint.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.AppearancePrint.FieldValueGrandTotal.Options.UseFont = true;
            this.PvtMakGrid.AppearancePrint.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.AppearancePrint.FieldValueTotal.Options.UseFont = true;
            this.PvtMakGrid.AppearancePrint.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.AppearancePrint.GrandTotalCell.Options.UseFont = true;
            this.PvtMakGrid.AppearancePrint.HeaderGroupLine.BackColor = System.Drawing.Color.LightGray;
            this.PvtMakGrid.AppearancePrint.HeaderGroupLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.AppearancePrint.HeaderGroupLine.Options.UseBackColor = true;
            this.PvtMakGrid.AppearancePrint.HeaderGroupLine.Options.UseFont = true;
            this.PvtMakGrid.AppearancePrint.TotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PvtMakGrid.AppearancePrint.TotalCell.Options.UseFont = true;
            this.PvtMakGrid.BackColor = System.Drawing.Color.Silver;
            this.PvtMakGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PvtMakGrid.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.ColMakSizeName,
            this.ColMakFromCarat,
            this.ColMakShape,
            this.ColMakShapeSeqNo,
            this.ColMakPcs,
            this.ColMakPktCts,
            this.ColMakExpCts,
            this.ColMakExpPer,
            this.ColMakAmount});
            this.PvtMakGrid.Location = new System.Drawing.Point(2, 20);
            this.PvtMakGrid.Name = "PvtMakGrid";
            this.PvtMakGrid.OptionsCustomization.AllowHideFields = DevExpress.XtraPivotGrid.AllowHideFieldsType.Always;
            this.PvtMakGrid.OptionsDataField.RowHeaderWidth = 116;
            this.PvtMakGrid.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button;
            this.PvtMakGrid.OptionsView.RowTreeOffset = 24;
            this.PvtMakGrid.OptionsView.RowTreeWidth = 116;
            this.PvtMakGrid.OptionsView.ShowColumnTotals = false;
            this.PvtMakGrid.Size = new System.Drawing.Size(718, 371);
            this.PvtMakGrid.TabIndex = 7;
            this.PvtMakGrid.CellClick += new DevExpress.XtraPivotGrid.PivotCellEventHandler(this.PvtMakGrid_CellClick);
            // 
            // ColMakSizeName
            // 
            this.ColMakSizeName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.ColMakSizeName.AreaIndex = 0;
            this.ColMakSizeName.Caption = "Size";
            this.ColMakSizeName.FieldName = "SIZENAME";
            this.ColMakSizeName.Name = "ColMakSizeName";
            // 
            // ColMakFromCarat
            // 
            this.ColMakFromCarat.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.ColMakFromCarat.AreaIndex = 1;
            this.ColMakFromCarat.Caption = "FROMCARAT";
            this.ColMakFromCarat.FieldName = "FROMCARAT";
            this.ColMakFromCarat.Name = "ColMakFromCarat";
            this.ColMakFromCarat.Visible = false;
            // 
            // ColMakShape
            // 
            this.ColMakShape.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.ColMakShape.AreaIndex = 0;
            this.ColMakShape.Caption = "Shape";
            this.ColMakShape.FieldName = "SHAPENAME";
            this.ColMakShape.Name = "ColMakShape";
            // 
            // ColMakShapeSeqNo
            // 
            this.ColMakShapeSeqNo.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.ColMakShapeSeqNo.AreaIndex = 2;
            this.ColMakShapeSeqNo.Caption = "SHAPESEQNO";
            this.ColMakShapeSeqNo.FieldName = "SHAPESEQNO";
            this.ColMakShapeSeqNo.Name = "ColMakShapeSeqNo";
            this.ColMakShapeSeqNo.Visible = false;
            // 
            // ColMakPcs
            // 
            this.ColMakPcs.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.ColMakPcs.AreaIndex = 0;
            this.ColMakPcs.Caption = "Pcs";
            this.ColMakPcs.CellFormat.FormatString = "{0:N0}";
            this.ColMakPcs.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ColMakPcs.FieldName = "PCS";
            this.ColMakPcs.Name = "ColMakPcs";
            this.ColMakPcs.Width = 50;
            // 
            // ColMakPktCts
            // 
            this.ColMakPktCts.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.ColMakPktCts.AreaIndex = 1;
            this.ColMakPktCts.Caption = "Pkt Cts";
            this.ColMakPktCts.CellFormat.FormatString = "{0:N2}";
            this.ColMakPktCts.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ColMakPktCts.FieldName = "PKTCTS";
            this.ColMakPktCts.Name = "ColMakPktCts";
            this.ColMakPktCts.Width = 83;
            // 
            // ColMakExpCts
            // 
            this.ColMakExpCts.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.ColMakExpCts.AreaIndex = 2;
            this.ColMakExpCts.Caption = "Exp Cts";
            this.ColMakExpCts.CellFormat.FormatString = "{0:N2}";
            this.ColMakExpCts.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ColMakExpCts.FieldName = "EXPCTS";
            this.ColMakExpCts.Name = "ColMakExpCts";
            this.ColMakExpCts.Width = 86;
            // 
            // ColMakExpPer
            // 
            this.ColMakExpPer.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.ColMakExpPer.AreaIndex = 3;
            this.ColMakExpPer.Caption = "Exp %";
            this.ColMakExpPer.CellFormat.FormatString = "{0:N2}";
            this.ColMakExpPer.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ColMakExpPer.FieldName = "EXPPER";
            this.ColMakExpPer.Name = "ColMakExpPer";
            // 
            // ColMakAmount
            // 
            this.ColMakAmount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.ColMakAmount.AreaIndex = 4;
            this.ColMakAmount.Caption = "Amt";
            this.ColMakAmount.CellFormat.FormatString = "{0:N2}";
            this.ColMakAmount.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ColMakAmount.FieldName = "AMOUNT";
            this.ColMakAmount.Name = "ColMakAmount";
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.PvtMakGrid);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(722, 393);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Makable Data";
            // 
            // DockSideMenu
            // 
            this.DockSideMenu.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerLeft});
            this.DockSideMenu.Form = this;
            this.DockSideMenu.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // hideContainerLeft
            // 
            this.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.hideContainerLeft.Controls.Add(this.dockPanel1);
            this.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.hideContainerLeft.Location = new System.Drawing.Point(0, 81);
            this.hideContainerLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hideContainerLeft.Name = "hideContainerLeft";
            this.hideContainerLeft.Size = new System.Drawing.Size(19, 612);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("28a0e8d0-a6f7-4b7f-b216-9b8a35a23018");
            this.dockPanel1.Location = new System.Drawing.Point(19, 0);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(360, 200);
            this.dockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.SavedIndex = 0;
            this.dockPanel1.Size = new System.Drawing.Size(360, 693);
            this.dockPanel1.Text = "Advance Filter";
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.MainGridFilter);
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 28);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(411, 820);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // MainGridFilter
            // 
            this.MainGridFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridFilter.Location = new System.Drawing.Point(0, 0);
            this.MainGridFilter.MainView = this.GrdDetFilter;
            this.MainGridFilter.Name = "MainGridFilter";
            this.MainGridFilter.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkIsActive,
            this.repTxtRemark,
            this.txtRepEmployee,
            this.txtDepartment,
            this.txtProcess,
            this.CmbRoughType,
            this.repChkFilter});
            this.MainGridFilter.Size = new System.Drawing.Size(411, 820);
            this.MainGridFilter.TabIndex = 10;
            this.MainGridFilter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetFilter});
            // 
            // GrdDetFilter
            // 
            this.GrdDetFilter.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetFilter.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetFilter.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetFilter.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.GrdDetFilter.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.GrdDetFilter.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdDetFilter.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDetFilter.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetFilter.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDetFilter.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetFilter.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetFilter.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetFilter.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetFilter.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetFilter.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDetFilter.Appearance.Row.Options.UseFont = true;
            this.GrdDetFilter.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetFilter.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetFilter.ColumnPanelRowHeight = 25;
            this.GrdDetFilter.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn7,
            this.gridColumn8});
            this.GrdDetFilter.GridControl = this.MainGridFilter;
            this.GrdDetFilter.Name = "GrdDetFilter";
            this.GrdDetFilter.OptionsCustomization.AllowFilter = false;
            this.GrdDetFilter.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetFilter.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDetFilter.OptionsPrint.ExpandAllGroups = false;
            this.GrdDetFilter.OptionsSelection.MultiSelect = true;
            this.GrdDetFilter.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDetFilter.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetFilter.OptionsView.ShowFooter = true;
            this.GrdDetFilter.OptionsView.ShowGroupPanel = false;
            this.GrdDetFilter.RowHeight = 25;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Filter";
            this.gridColumn3.FieldName = "PARTICULARS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 187;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn7.Caption = "Value";
            this.gridColumn7.ColumnEdit = this.repChkFilter;
            this.gridColumn7.FieldName = "VALUE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 206;
            // 
            // repChkFilter
            // 
            this.repChkFilter.AutoHeight = false;
            this.repChkFilter.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repChkFilter.Name = "repChkFilter";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn8.Caption = "ID";
            this.gridColumn8.FieldName = "ID";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Width = 93;
            // 
            // repChkIsActive
            // 
            this.repChkIsActive.AutoHeight = false;
            this.repChkIsActive.Caption = "Check";
            this.repChkIsActive.Name = "repChkIsActive";
            // 
            // repTxtRemark
            // 
            this.repTxtRemark.AutoHeight = false;
            this.repTxtRemark.Name = "repTxtRemark";
            // 
            // txtRepEmployee
            // 
            this.txtRepEmployee.AutoHeight = false;
            this.txtRepEmployee.Name = "txtRepEmployee";
            // 
            // txtDepartment
            // 
            this.txtDepartment.AutoHeight = false;
            this.txtDepartment.Name = "txtDepartment";
            // 
            // txtProcess
            // 
            this.txtProcess.AutoHeight = false;
            this.txtProcess.Name = "txtProcess";
            // 
            // CmbRoughType
            // 
            this.CmbRoughType.AutoHeight = false;
            this.CmbRoughType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbRoughType.Items.AddRange(new object[] {
            "CVD",
            "HPHT",
            "NATURAL"});
            this.CmbRoughType.Name = "CmbRoughType";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl2.Appearance.Options.UseBackColor = true;
            this.groupControl2.AppearanceCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseBackColor = true;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.MainGridMakable);
            this.groupControl2.Controls.Add(this.cPanel3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(604, 393);
            this.groupControl2.TabIndex = 8;
            this.groupControl2.Text = "Makable Production";
            // 
            // MainGridMakable
            // 
            this.MainGridMakable.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.MainGridMakable.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGridMakable.Location = new System.Drawing.Point(2, 48);
            this.MainGridMakable.MainView = this.GrdDetMakable;
            this.MainGridMakable.Name = "MainGridMakable";
            this.MainGridMakable.Size = new System.Drawing.Size(600, 343);
            this.MainGridMakable.TabIndex = 241;
            this.MainGridMakable.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetMakable});
            // 
            // GrdDetMakable
            // 
            this.GrdDetMakable.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetMakable.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetMakable.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetMakable.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetMakable.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDetMakable.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetMakable.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetMakable.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetMakable.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDetMakable.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDetMakable.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetMakable.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetMakable.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetMakable.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetMakable.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetMakable.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetMakable.Appearance.Row.Options.UseFont = true;
            this.GrdDetMakable.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetMakable.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetMakable.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetMakable.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetMakable.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDetMakable.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDetMakable.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetMakable.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDetMakable.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetMakable.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDetMakable.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetMakable.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDetMakable.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.GrdDetMakable.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDetMakable.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetMakable.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetMakable.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDetMakable.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDetMakable.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetMakable.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDetMakable.ColumnPanelRowHeight = 25;
            this.GrdDetMakable.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.bandedGridColumn1,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.GrdDetMakable.GridControl = this.MainGridMakable;
            this.GrdDetMakable.Name = "GrdDetMakable";
            this.GrdDetMakable.OptionsBehavior.Editable = false;
            this.GrdDetMakable.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetMakable.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDetMakable.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDetMakable.OptionsPrint.ExpandAllGroups = false;
            this.GrdDetMakable.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDetMakable.OptionsSelection.MultiSelect = true;
            this.GrdDetMakable.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDetMakable.OptionsView.ColumnAutoWidth = false;
            this.GrdDetMakable.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetMakable.OptionsView.ShowFooter = true;
            this.GrdDetMakable.OptionsView.ShowGroupPanel = false;
            this.GrdDetMakable.RowHeight = 23;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "FIRSTDATE";
            this.bandedGridColumn1.FieldName = "FIRSTDATE";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ENDDATE";
            this.gridColumn1.FieldName = "ENDDATE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Particulars";
            this.gridColumn2.FieldName = "PARTICULAR";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 224;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(200)))));
            this.gridColumn4.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.Caption = "Pcs";
            this.gridColumn4.FieldName = "PCS";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PCS", "{0:N0}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 49;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(200)))));
            this.gridColumn5.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn5.Caption = "%";
            this.gridColumn5.FieldName = "PCSPER";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 60;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridColumn6.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn6.Caption = "Cts";
            this.gridColumn6.FieldName = "CARAT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CARAT", "{0:N2}")});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 62;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridColumn9.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn9.Caption = "%";
            this.gridColumn9.FieldName = "CARATPER";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            this.gridColumn9.Width = 52;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.BackColor = System.Drawing.Color.MistyRose;
            this.gridColumn10.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn10.Caption = "Amt";
            this.gridColumn10.FieldName = "AMOUNT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "{0:N2}")});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 87;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.BackColor = System.Drawing.Color.MistyRose;
            this.gridColumn11.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn11.Caption = "%";
            this.gridColumn11.FieldName = "AMOUNTPER";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 6;
            this.gridColumn11.Width = 53;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn12.Caption = "Emp";
            this.gridColumn12.FieldName = "EMPLOYEE";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "EMPLOYEE", "{0:N2}")});
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 7;
            this.gridColumn12.Width = 46;
            // 
            // cPanel3
            // 
            this.cPanel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cPanel3.Controls.Add(this.RbtYearly);
            this.cPanel3.Controls.Add(this.RbtQuaterly);
            this.cPanel3.Controls.Add(this.RbtDaily);
            this.cPanel3.Controls.Add(this.RbtWeekly);
            this.cPanel3.Controls.Add(this.RbtMonthly);
            this.cPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.cPanel3.Location = new System.Drawing.Point(2, 22);
            this.cPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cPanel3.Name = "cPanel3";
            this.cPanel3.Size = new System.Drawing.Size(600, 26);
            this.cPanel3.TabIndex = 240;
            // 
            // RbtYearly
            // 
            this.RbtYearly.AllowTabKeyOnEnter = false;
            this.RbtYearly.AutoSize = true;
            this.RbtYearly.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtYearly.ForeColor = System.Drawing.Color.Black;
            this.RbtYearly.Location = new System.Drawing.Point(3, 2);
            this.RbtYearly.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RbtYearly.Name = "RbtYearly";
            this.RbtYearly.Size = new System.Drawing.Size(60, 17);
            this.RbtYearly.TabIndex = 237;
            this.RbtYearly.Tag = "";
            this.RbtYearly.Text = "Yearly";
            this.RbtYearly.ToolTips = "";
            this.RbtYearly.UseVisualStyleBackColor = true;
            this.RbtYearly.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // RbtQuaterly
            // 
            this.RbtQuaterly.AllowTabKeyOnEnter = false;
            this.RbtQuaterly.AutoSize = true;
            this.RbtQuaterly.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtQuaterly.ForeColor = System.Drawing.Color.Black;
            this.RbtQuaterly.Location = new System.Drawing.Point(64, 2);
            this.RbtQuaterly.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RbtQuaterly.Name = "RbtQuaterly";
            this.RbtQuaterly.Size = new System.Drawing.Size(74, 17);
            this.RbtQuaterly.TabIndex = 239;
            this.RbtQuaterly.Tag = "";
            this.RbtQuaterly.Text = "Quaterly";
            this.RbtQuaterly.ToolTips = "";
            this.RbtQuaterly.UseVisualStyleBackColor = true;
            this.RbtQuaterly.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // RbtDaily
            // 
            this.RbtDaily.AllowTabKeyOnEnter = false;
            this.RbtDaily.AutoSize = true;
            this.RbtDaily.Checked = true;
            this.RbtDaily.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtDaily.ForeColor = System.Drawing.Color.Black;
            this.RbtDaily.Location = new System.Drawing.Point(284, 2);
            this.RbtDaily.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RbtDaily.Name = "RbtDaily";
            this.RbtDaily.Size = new System.Drawing.Size(54, 17);
            this.RbtDaily.TabIndex = 238;
            this.RbtDaily.TabStop = true;
            this.RbtDaily.Tag = "";
            this.RbtDaily.Text = "Daily";
            this.RbtDaily.ToolTips = "";
            this.RbtDaily.UseVisualStyleBackColor = true;
            this.RbtDaily.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // RbtWeekly
            // 
            this.RbtWeekly.AllowTabKeyOnEnter = false;
            this.RbtWeekly.AutoSize = true;
            this.RbtWeekly.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtWeekly.ForeColor = System.Drawing.Color.Black;
            this.RbtWeekly.Location = new System.Drawing.Point(215, 2);
            this.RbtWeekly.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RbtWeekly.Name = "RbtWeekly";
            this.RbtWeekly.Size = new System.Drawing.Size(66, 17);
            this.RbtWeekly.TabIndex = 238;
            this.RbtWeekly.Tag = "";
            this.RbtWeekly.Text = "Weekly";
            this.RbtWeekly.ToolTips = "";
            this.RbtWeekly.UseVisualStyleBackColor = true;
            this.RbtWeekly.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // RbtMonthly
            // 
            this.RbtMonthly.AllowTabKeyOnEnter = false;
            this.RbtMonthly.AutoSize = true;
            this.RbtMonthly.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtMonthly.ForeColor = System.Drawing.Color.Black;
            this.RbtMonthly.Location = new System.Drawing.Point(141, 2);
            this.RbtMonthly.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RbtMonthly.Name = "RbtMonthly";
            this.RbtMonthly.Size = new System.Drawing.Size(69, 17);
            this.RbtMonthly.TabIndex = 239;
            this.RbtMonthly.Tag = "";
            this.RbtMonthly.Text = "Monthly";
            this.RbtMonthly.ToolTips = "";
            this.RbtMonthly.UseVisualStyleBackColor = true;
            this.RbtMonthly.CheckedChanged += new System.EventHandler(this.rbtRoughStatusAll_CheckedChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl3.Appearance.Options.UseBackColor = true;
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.ChartControlAreaMakable);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1334, 266);
            this.groupControl3.TabIndex = 8;
            this.groupControl3.Text = "Makable Production Chart";
            // 
            // ChartControlAreaMakable
            // 
            this.ChartControlAreaMakable.AppearanceNameSerializable = "The Trees";
            this.ChartControlAreaMakable.DataBindings = null;
            xyDiagram1.AxisX.NumericScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Automatic;
            xyDiagram1.AxisX.ScaleBreakOptions.Style = DevExpress.XtraCharts.ScaleBreakStyle.Straight;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.SideMarginsValue = 0.01D;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.ChartControlAreaMakable.Diagram = xyDiagram1;
            this.ChartControlAreaMakable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartControlAreaMakable.Legend.Name = "Default Legend";
            this.ChartControlAreaMakable.Location = new System.Drawing.Point(2, 22);
            this.ChartControlAreaMakable.Name = "ChartControlAreaMakable";
            series1.Name = "Series 1";
            areaSeriesView1.Transparency = ((byte)(0));
            series1.View = areaSeriesView1;
            series2.Name = "Series 2";
            areaSeriesView2.Transparency = ((byte)(0));
            series2.View = areaSeriesView2;
            this.ChartControlAreaMakable.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.ChartControlAreaMakable.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            areaSeriesView3.Transparency = ((byte)(0));
            this.ChartControlAreaMakable.SeriesTemplate.View = areaSeriesView3;
            this.ChartControlAreaMakable.Size = new System.Drawing.Size(1330, 242);
            this.ChartControlAreaMakable.TabIndex = 50;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1334, 667);
            this.splitContainerControl1.SplitterPosition = 393;
            this.splitContainerControl1.TabIndex = 10;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1334, 393);
            this.splitContainerControl2.SplitterPosition = 722;
            this.splitContainerControl2.TabIndex = 11;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(0, 1500);
            this.panel1.Controls.Add(this.groupControl4);
            this.panel1.Controls.Add(this.splitContainerControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(19, 81);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1351, 612);
            this.panel1.TabIndex = 12;
            // 
            // groupControl4
            // 
            this.groupControl4.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.groupControl4.AppearanceCaption.ForeColor = System.Drawing.Color.Green;
            this.groupControl4.AppearanceCaption.Options.UseFont = true;
            this.groupControl4.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl4.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl4.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl4.Controls.Add(this.BtnExpandMainGrid);
            this.groupControl4.Controls.Add(this.lblPrintSummary);
            this.groupControl4.Controls.Add(this.MainGrd);
            this.groupControl4.Controls.Add(this.lblExportSummary);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl4.Location = new System.Drawing.Point(0, 667);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(1334, 360);
            this.groupControl4.TabIndex = 182;
            this.groupControl4.Text = "Order Summary";
            // 
            // BtnExpandMainGrid
            // 
            this.BtnExpandMainGrid.Appearance.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.BtnExpandMainGrid.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExpandMainGrid.Appearance.Options.UseFont = true;
            this.BtnExpandMainGrid.Appearance.Options.UseForeColor = true;
            this.BtnExpandMainGrid.Location = new System.Drawing.Point(2, 0);
            this.BtnExpandMainGrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnExpandMainGrid.Name = "BtnExpandMainGrid";
            this.BtnExpandMainGrid.Size = new System.Drawing.Size(27, 18);
            this.BtnExpandMainGrid.TabIndex = 161;
            this.BtnExpandMainGrid.Text = "+";
            // 
            // lblPrintSummary
            // 
            this.lblPrintSummary.AutoSize = true;
            this.lblPrintSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPrintSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblPrintSummary.ForeColor = System.Drawing.Color.Blue;
            this.lblPrintSummary.Location = new System.Drawing.Point(97, 2);
            this.lblPrintSummary.Name = "lblPrintSummary";
            this.lblPrintSummary.Size = new System.Drawing.Size(38, 13);
            this.lblPrintSummary.TabIndex = 178;
            this.lblPrintSummary.Text = "Print";
            // 
            // MainGrd
            // 
            this.MainGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrd.Location = new System.Drawing.Point(2, 20);
            this.MainGrd.MainView = this.GrdDet;
            this.MainGrd.Name = "MainGrd";
            this.MainGrd.Size = new System.Drawing.Size(1330, 338);
            this.MainGrd.TabIndex = 180;
            this.MainGrd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.EvenRow.Options.UseTextOptions = true;
            this.GrdDet.Appearance.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
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
            this.GrdDet.Appearance.FocusedRow.Options.UseTextOptions = true;
            this.GrdDet.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.FocusedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
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
            this.GrdDet.Appearance.OddRow.Options.UseTextOptions = true;
            this.GrdDet.Appearance.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.Row.Options.UseTextOptions = true;
            this.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
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
            this.GrdDet.ColumnPanelRowHeight = 40;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn37,
            this.gridColumn38,
            this.gridColumn39,
            this.gridColumn40,
            this.gridColumn41,
            this.gridColumn42,
            this.gridColumn43,
            this.gridColumn44,
            this.gridColumn45,
            this.gridColumn46,
            this.gridColumn47,
            this.gridColumn48,
            this.gridColumn49,
            this.gridColumn50,
            this.gridColumn51,
            this.gridColumn52,
            this.gridColumn53,
            this.gridColumn54,
            this.gridColumn55,
            this.gridColumn56,
            this.gridColumn57,
            this.gridColumn58,
            this.gridColumn59,
            this.gridColumn60,
            this.gridColumn61,
            this.gridColumn62,
            this.gridColumn63,
            this.gridColumn64});
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
            this.GrdDet.OptionsView.AllowCellMerge = true;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.RowAutoHeight = true;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "ORDER_ID";
            this.gridColumn37.FieldName = "ORDER_ID";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            // 
            // gridColumn38
            // 
            this.gridColumn38.Caption = "Ord Date";
            this.gridColumn38.FieldName = "ORDERDATE";
            this.gridColumn38.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn38.Visible = true;
            this.gridColumn38.VisibleIndex = 0;
            this.gridColumn38.Width = 89;
            // 
            // gridColumn39
            // 
            this.gridColumn39.Caption = "Ord No";
            this.gridColumn39.FieldName = "MANUALORDERNO";
            this.gridColumn39.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn39.Name = "gridColumn39";
            this.gridColumn39.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn39.Visible = true;
            this.gridColumn39.VisibleIndex = 1;
            this.gridColumn39.Width = 103;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "Party";
            this.gridColumn40.FieldName = "PARTYNAME";
            this.gridColumn40.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 2;
            this.gridColumn40.Width = 154;
            // 
            // gridColumn41
            // 
            this.gridColumn41.Caption = "Due Date";
            this.gridColumn41.FieldName = "ORDERDUEDATE";
            this.gridColumn41.Name = "gridColumn41";
            this.gridColumn41.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn41.Visible = true;
            this.gridColumn41.VisibleIndex = 3;
            this.gridColumn41.Width = 109;
            // 
            // gridColumn42
            // 
            this.gridColumn42.Caption = "Pri";
            this.gridColumn42.FieldName = "ORDERPRIORITY";
            this.gridColumn42.Name = "gridColumn42";
            this.gridColumn42.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn42.Width = 81;
            // 
            // gridColumn43
            // 
            this.gridColumn43.Caption = "Seq";
            this.gridColumn43.FieldName = "ORDERSEQNO";
            this.gridColumn43.Name = "gridColumn43";
            this.gridColumn43.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn43.Width = 42;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "ORDERDETAIL_ID";
            this.gridColumn44.FieldName = "ORDERDETAIL_ID";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            // 
            // gridColumn45
            // 
            this.gridColumn45.Caption = "Detail No";
            this.gridColumn45.FieldName = "ORDERDETAILNO";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn45.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn45.Visible = true;
            this.gridColumn45.VisibleIndex = 4;
            this.gridColumn45.Width = 90;
            // 
            // gridColumn46
            // 
            this.gridColumn46.Caption = "Shp";
            this.gridColumn46.FieldName = "SHAPENAME";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn46.Visible = true;
            this.gridColumn46.VisibleIndex = 5;
            // 
            // gridColumn47
            // 
            this.gridColumn47.Caption = "Size";
            this.gridColumn47.FieldName = "FROMCARAT";
            this.gridColumn47.Name = "gridColumn47";
            this.gridColumn47.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn47.Visible = true;
            this.gridColumn47.VisibleIndex = 6;
            this.gridColumn47.Width = 64;
            // 
            // gridColumn48
            // 
            this.gridColumn48.Caption = "TCts";
            this.gridColumn48.FieldName = "TOCARAT";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn48.Width = 66;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "Col";
            this.gridColumn49.FieldName = "FROMCOLORNAME";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 7;
            this.gridColumn49.Width = 50;
            // 
            // gridColumn50
            // 
            this.gridColumn50.Caption = "TCol";
            this.gridColumn50.FieldName = "TOCOLORNAME";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn50.Width = 44;
            // 
            // gridColumn51
            // 
            this.gridColumn51.Caption = "Cla";
            this.gridColumn51.FieldName = "FROMCLARITYNAME";
            this.gridColumn51.Name = "gridColumn51";
            this.gridColumn51.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn51.Visible = true;
            this.gridColumn51.VisibleIndex = 8;
            this.gridColumn51.Width = 67;
            // 
            // gridColumn52
            // 
            this.gridColumn52.Caption = "TCla";
            this.gridColumn52.FieldName = "TOCLARITYNAME";
            this.gridColumn52.Name = "gridColumn52";
            this.gridColumn52.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn52.Width = 59;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "Remark";
            this.gridColumn53.FieldName = "COMMENT";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 9;
            this.gridColumn53.Width = 163;
            // 
            // gridColumn54
            // 
            this.gridColumn54.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn54.AppearanceCell.Options.UseFont = true;
            this.gridColumn54.Caption = "Ord Pcs";
            this.gridColumn54.FieldName = "ORDERPCS";
            this.gridColumn54.Name = "gridColumn54";
            this.gridColumn54.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn54.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn54.Visible = true;
            this.gridColumn54.VisibleIndex = 10;
            // 
            // gridColumn55
            // 
            this.gridColumn55.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridColumn55.AppearanceCell.Font = new System.Drawing.Font("Verdana", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn55.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn55.AppearanceCell.Options.UseFont = true;
            this.gridColumn55.Caption = "Plann Pcs";
            this.gridColumn55.FieldName = "PLANPCS";
            this.gridColumn55.Name = "gridColumn55";
            this.gridColumn55.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn55.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn55.Visible = true;
            this.gridColumn55.VisibleIndex = 11;
            this.gridColumn55.Width = 74;
            // 
            // gridColumn56
            // 
            this.gridColumn56.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridColumn56.AppearanceCell.Font = new System.Drawing.Font("Verdana", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn56.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn56.AppearanceCell.Options.UseFont = true;
            this.gridColumn56.Caption = "Mak Pcs";
            this.gridColumn56.FieldName = "MAKPCS";
            this.gridColumn56.Name = "gridColumn56";
            this.gridColumn56.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn56.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn56.Visible = true;
            this.gridColumn56.VisibleIndex = 12;
            this.gridColumn56.Width = 64;
            // 
            // gridColumn57
            // 
            this.gridColumn57.AppearanceCell.Font = new System.Drawing.Font("Verdana", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn57.AppearanceCell.Options.UseFont = true;
            this.gridColumn57.Caption = "Artist Pcs";
            this.gridColumn57.FieldName = "ARTISTPCS";
            this.gridColumn57.Name = "gridColumn57";
            this.gridColumn57.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn57.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn57.Width = 57;
            // 
            // gridColumn58
            // 
            this.gridColumn58.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridColumn58.AppearanceCell.Font = new System.Drawing.Font("Verdana", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn58.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn58.AppearanceCell.Options.UseFont = true;
            this.gridColumn58.Caption = "Polish Pcs";
            this.gridColumn58.FieldName = "POLISHPCS";
            this.gridColumn58.Name = "gridColumn58";
            this.gridColumn58.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn58.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn58.Visible = true;
            this.gridColumn58.VisibleIndex = 13;
            this.gridColumn58.Width = 71;
            // 
            // gridColumn59
            // 
            this.gridColumn59.AppearanceCell.Font = new System.Drawing.Font("Verdana", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn59.AppearanceCell.Options.UseFont = true;
            this.gridColumn59.Caption = "Booked Pcs";
            this.gridColumn59.FieldName = "BOOKEDPCS";
            this.gridColumn59.Name = "gridColumn59";
            this.gridColumn59.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn59.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn59.Visible = true;
            this.gridColumn59.VisibleIndex = 14;
            // 
            // gridColumn60
            // 
            this.gridColumn60.Caption = "Pen. Pcs";
            this.gridColumn60.FieldName = "PENDINGPCS";
            this.gridColumn60.Name = "gridColumn60";
            this.gridColumn60.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn60.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn60.Visible = true;
            this.gridColumn60.VisibleIndex = 15;
            // 
            // gridColumn61
            // 
            this.gridColumn61.Caption = "Pen %";
            this.gridColumn61.DisplayFormat.FormatString = "{0:N2}";
            this.gridColumn61.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn61.FieldName = "PENDINGPER";
            this.gridColumn61.Name = "gridColumn61";
            this.gridColumn61.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn61.Visible = true;
            this.gridColumn61.VisibleIndex = 16;
            // 
            // gridColumn62
            // 
            this.gridColumn62.Caption = "Booked %";
            this.gridColumn62.DisplayFormat.FormatString = "{0:N2}";
            this.gridColumn62.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn62.FieldName = "BOOKEDPER";
            this.gridColumn62.Name = "gridColumn62";
            this.gridColumn62.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn62.Visible = true;
            this.gridColumn62.VisibleIndex = 17;
            // 
            // gridColumn63
            // 
            this.gridColumn63.Caption = "Over Due";
            this.gridColumn63.FieldName = "OVERDUE";
            this.gridColumn63.Name = "gridColumn63";
            this.gridColumn63.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn63.Visible = true;
            this.gridColumn63.VisibleIndex = 18;
            // 
            // gridColumn64
            // 
            this.gridColumn64.Caption = "Status";
            this.gridColumn64.FieldName = "DETAILSTATUS";
            this.gridColumn64.Name = "gridColumn64";
            this.gridColumn64.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn64.Visible = true;
            this.gridColumn64.VisibleIndex = 19;
            // 
            // lblExportSummary
            // 
            this.lblExportSummary.AutoSize = true;
            this.lblExportSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExportSummary.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblExportSummary.ForeColor = System.Drawing.Color.Blue;
            this.lblExportSummary.Location = new System.Drawing.Point(34, 2);
            this.lblExportSummary.Name = "lblExportSummary";
            this.lblExportSummary.Size = new System.Drawing.Size(50, 13);
            this.lblExportSummary.TabIndex = 178;
            this.lblExportSummary.Text = "Export";
            // 
            // FrmPlanningDashboardWithOrderAndLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 693);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.hideContainerLeft);
            this.Controls.Add(this.panel4);
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmPlanningDashboardWithOrderAndLock";
            this.Text = "PLANNING DASHBOARD";
            ((System.ComponentModel.ISupportInitialize)(this.XtraTabDateControl)).EndInit();
            this.XtraTabDateControl.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.cPanel2.ResumeLayout(false);
            this.cPanel2.PerformLayout();
            this.cPanel1.ResumeLayout(false);
            this.cPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PvtMakGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DockSideMenu)).EndInit();
            this.hideContainerLeft.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbRoughType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridMakable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetMakable)).EndInit();
            this.cPanel3.ResumeLayout(false);
            this.cPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControlAreaMakable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnLoad;
        private DevExpress.XtraTab.XtraTabControl XtraTabDateControl;
        private DevExpress.XtraTab.XtraTabPage TabToday;
        private DevExpress.XtraTab.XtraTabPage TabYesterday;
        private DevExpress.XtraTab.XtraTabPage TabThisWeek;
        private DevExpress.XtraTab.XtraTabPage TabThisMonth;
        private DevExpress.XtraTab.XtraTabPage TabThisQuater;
        private DevExpress.XtraTab.XtraTabPage TabThis6Month;
        private DevExpress.XtraTab.XtraTabPage TabThisYear;
        private DevExpress.XtraTab.XtraTabPage TabThisCustom;
        private AxonContLib.cPanel panel4;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel11;
        public DevExpress.XtraPivotGrid.PivotGridControl PvtMakGrid;
        private AxonContLib.cPanel cPanel1;
        private AxonContLib.cPanel cPanel2;
        private AxonContLib.cRadioButton rbtRoughStatusAll;
        private AxonContLib.cRadioButton rbtRoughStatusCVD;
        private AxonContLib.cRadioButton rbtRoughStatusNatural;
        private AxonContLib.cRadioButton rbtRoughStatusHPHT;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakSizeName;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakFromCarat;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakShape;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakShapeSeqNo;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakPcs;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cCheckBox ChkSummPcs;
        private AxonContLib.cCheckBox ChkSummPktCts;
        private AxonContLib.cCheckBox ChkSummExpCts;
        private AxonContLib.cCheckBox ChkSummExpPer;
        private AxonContLib.cCheckBox ChkSummAmt;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakPktCts;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakExpCts;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakExpPer;
        private DevExpress.XtraPivotGrid.PivotGridField ColMakAmount;
        private DevExpress.XtraBars.Docking.DockManager DockSideMenu;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl MainGridFilter;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetFilter;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRemark;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkIsActive;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtRepEmployee;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtDepartment;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtProcess;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox CmbRoughType;
        private DevExpress.XtraWaitForm.ProgressPanel PnlLoding;
        private AxonContLib.cLabel lblTime;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repChkFilter;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerLeft;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private AxonContLib.cPanel cPanel3;
        private AxonContLib.cRadioButton RbtYearly;
        private AxonContLib.cRadioButton RbtQuaterly;
        private AxonContLib.cRadioButton RbtWeekly;
        private AxonContLib.cRadioButton RbtMonthly;
        private AxonContLib.cRadioButton RbtDaily;
        private DevExpress.XtraGrid.GridControl MainGridMakable;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetMakable;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraCharts.ChartControl ChartControlAreaMakable;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton BtnExpandMainGrid;
        private System.Windows.Forms.Label lblPrintSummary;
        private DevExpress.XtraGrid.GridControl MainGrd;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn42;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn43;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn47;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn52;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn55;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn56;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn58;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn59;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn60;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn61;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn62;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn63;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn64;
        private System.Windows.Forms.Label lblExportSummary;
    }
}