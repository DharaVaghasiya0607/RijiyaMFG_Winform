namespace AxoneMFGRJ.Dashboard
{
    partial class FrmPlanningDashboard
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView1 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView2 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView3 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
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
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.BANDPARTICULARS = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BANDMAKABLE = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BANDPOLISH = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgPolPcsPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgPolCaratPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn13 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgPolAmountPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.BANDBY = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgByPcsPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn16 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn17 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgByCaratPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn18 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn19 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgByAmountPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.BANDLAB = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn20 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn21 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgLabPcsPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn22 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn23 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgLabCaratPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.bandedGridColumn24 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn25 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repPrgLabAmountPer = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
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
            gridLevelNode2.RelationName = "Level1";
            this.MainGridMakable.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
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
            this.panel1.Controls.Add(this.MainGrid);
            this.panel1.Controls.Add(this.splitContainerControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(19, 81);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1351, 612);
            this.panel1.TabIndex = 12;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            gridLevelNode1.RelationName = "Level1";
            this.MainGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGrid.Location = new System.Drawing.Point(0, 667);
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
            this.MainGrid.Size = new System.Drawing.Size(1334, 833);
            this.MainGrid.TabIndex = 163;
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
            this.bandedGridColumn2,
            this.bandedGridColumn3,
            this.bandedGridColumn4,
            this.bandedGridColumn5,
            this.bandedGridColumn6,
            this.bandedGridColumn7,
            this.bandedGridColumn8,
            this.bandedGridColumn11,
            this.bandedGridColumn9,
            this.bandedGridColumn12,
            this.bandedGridColumn10,
            this.bandedGridColumn13,
            this.bandedGridColumn14,
            this.bandedGridColumn15,
            this.bandedGridColumn16,
            this.bandedGridColumn17,
            this.bandedGridColumn18,
            this.bandedGridColumn19,
            this.bandedGridColumn20,
            this.bandedGridColumn21,
            this.bandedGridColumn22,
            this.bandedGridColumn23,
            this.bandedGridColumn24,
            this.bandedGridColumn25});
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
            this.BANDPARTICULARS.Columns.Add(this.bandedGridColumn2);
            this.BANDPARTICULARS.Columns.Add(this.bandedGridColumn3);
            this.BANDPARTICULARS.Columns.Add(this.bandedGridColumn4);
            this.BANDPARTICULARS.Name = "BANDPARTICULARS";
            this.BANDPARTICULARS.VisibleIndex = 0;
            this.BANDPARTICULARS.Width = 100;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "FIRSTDATE";
            this.bandedGridColumn2.FieldName = "FIRSTDATE";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "ENDDATE";
            this.bandedGridColumn3.FieldName = "ENDDATE";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn4.Caption = "Particulars";
            this.bandedGridColumn4.FieldName = "PARAMNAME";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 100;
            // 
            // BANDMAKABLE
            // 
            this.BANDMAKABLE.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.BANDMAKABLE.AppearanceHeader.Options.UseFont = true;
            this.BANDMAKABLE.AppearanceHeader.Options.UseTextOptions = true;
            this.BANDMAKABLE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BANDMAKABLE.Caption = "Makable";
            this.BANDMAKABLE.Columns.Add(this.bandedGridColumn5);
            this.BANDMAKABLE.Columns.Add(this.bandedGridColumn6);
            this.BANDMAKABLE.Columns.Add(this.bandedGridColumn7);
            this.BANDMAKABLE.Name = "BANDMAKABLE";
            this.BANDMAKABLE.VisibleIndex = 1;
            this.BANDMAKABLE.Width = 220;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn5.Caption = "Pcs";
            this.bandedGridColumn5.FieldName = "MAKPCS";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PCS", "{0:N0}")});
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.Width = 50;
            // 
            // bandedGridColumn6
            // 
            this.bandedGridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn6.Caption = "Cts";
            this.bandedGridColumn6.FieldName = "MAKCARAT";
            this.bandedGridColumn6.Name = "bandedGridColumn6";
            this.bandedGridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CARAT", "{0:N2}")});
            this.bandedGridColumn6.Visible = true;
            this.bandedGridColumn6.Width = 70;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn7.Caption = "Amt";
            this.bandedGridColumn7.FieldName = "MAKAMOUNT";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "{0:N2}")});
            this.bandedGridColumn7.Visible = true;
            this.bandedGridColumn7.Width = 100;
            // 
            // BANDPOLISH
            // 
            this.BANDPOLISH.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.BANDPOLISH.AppearanceHeader.Options.UseFont = true;
            this.BANDPOLISH.AppearanceHeader.Options.UseTextOptions = true;
            this.BANDPOLISH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BANDPOLISH.Caption = "Polish";
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn8);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn11);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn9);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn12);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn10);
            this.BANDPOLISH.Columns.Add(this.bandedGridColumn13);
            this.BANDPOLISH.Name = "BANDPOLISH";
            this.BANDPOLISH.VisibleIndex = 2;
            this.BANDPOLISH.Width = 580;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn8.Caption = "Pcs";
            this.bandedGridColumn8.FieldName = "POLPCS";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.Width = 50;
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
            // 
            // bandedGridColumn9
            // 
            this.bandedGridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn9.Caption = "Cts";
            this.bandedGridColumn9.FieldName = "POLCARAT";
            this.bandedGridColumn9.Name = "bandedGridColumn9";
            this.bandedGridColumn9.Visible = true;
            this.bandedGridColumn9.Width = 70;
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
            // bandedGridColumn10
            // 
            this.bandedGridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn10.Caption = "Amt";
            this.bandedGridColumn10.FieldName = "POLAMOUNT";
            this.bandedGridColumn10.Name = "bandedGridColumn10";
            this.bandedGridColumn10.Visible = true;
            this.bandedGridColumn10.Width = 100;
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
            this.BANDBY.Columns.Add(this.bandedGridColumn14);
            this.BANDBY.Columns.Add(this.bandedGridColumn15);
            this.BANDBY.Columns.Add(this.bandedGridColumn16);
            this.BANDBY.Columns.Add(this.bandedGridColumn17);
            this.BANDBY.Columns.Add(this.bandedGridColumn18);
            this.BANDBY.Columns.Add(this.bandedGridColumn19);
            this.BANDBY.Name = "BANDBY";
            this.BANDBY.VisibleIndex = 3;
            this.BANDBY.Width = 120;
            // 
            // bandedGridColumn14
            // 
            this.bandedGridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn14.Caption = "Pcs";
            this.bandedGridColumn14.FieldName = "BYPCS";
            this.bandedGridColumn14.Name = "bandedGridColumn14";
            this.bandedGridColumn14.Width = 50;
            // 
            // bandedGridColumn15
            // 
            this.bandedGridColumn15.Caption = "%";
            this.bandedGridColumn15.ColumnEdit = this.repPrgByPcsPer;
            this.bandedGridColumn15.FieldName = "BYPCSPER";
            this.bandedGridColumn15.Name = "bandedGridColumn15";
            this.bandedGridColumn15.Width = 120;
            // 
            // repPrgByPcsPer
            // 
            this.repPrgByPcsPer.EndColor = System.Drawing.Color.Black;
            this.repPrgByPcsPer.Name = "repPrgByPcsPer";
            this.repPrgByPcsPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgByPcsPer.ShowTitle = true;
            this.repPrgByPcsPer.StartColor = System.Drawing.Color.Black;
            // 
            // bandedGridColumn16
            // 
            this.bandedGridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn16.Caption = "Cts";
            this.bandedGridColumn16.FieldName = "BYCARAT";
            this.bandedGridColumn16.Name = "bandedGridColumn16";
            this.bandedGridColumn16.Width = 70;
            // 
            // bandedGridColumn17
            // 
            this.bandedGridColumn17.Caption = "%";
            this.bandedGridColumn17.ColumnEdit = this.repPrgByCaratPer;
            this.bandedGridColumn17.FieldName = "BYCARATPER";
            this.bandedGridColumn17.Name = "bandedGridColumn17";
            this.bandedGridColumn17.Width = 120;
            // 
            // repPrgByCaratPer
            // 
            this.repPrgByCaratPer.EndColor = System.Drawing.Color.Black;
            this.repPrgByCaratPer.Name = "repPrgByCaratPer";
            this.repPrgByCaratPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgByCaratPer.ShowTitle = true;
            this.repPrgByCaratPer.StartColor = System.Drawing.Color.Black;
            // 
            // bandedGridColumn18
            // 
            this.bandedGridColumn18.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn18.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn18.Caption = "Amt";
            this.bandedGridColumn18.FieldName = "BYAMOUNT";
            this.bandedGridColumn18.Name = "bandedGridColumn18";
            this.bandedGridColumn18.Width = 100;
            // 
            // bandedGridColumn19
            // 
            this.bandedGridColumn19.Caption = "%";
            this.bandedGridColumn19.ColumnEdit = this.repPrgByAmountPer;
            this.bandedGridColumn19.FieldName = "BYAMOUNTPER";
            this.bandedGridColumn19.Name = "bandedGridColumn19";
            this.bandedGridColumn19.Width = 120;
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
            this.BANDLAB.Columns.Add(this.bandedGridColumn20);
            this.BANDLAB.Columns.Add(this.bandedGridColumn21);
            this.BANDLAB.Columns.Add(this.bandedGridColumn22);
            this.BANDLAB.Columns.Add(this.bandedGridColumn23);
            this.BANDLAB.Columns.Add(this.bandedGridColumn24);
            this.BANDLAB.Columns.Add(this.bandedGridColumn25);
            this.BANDLAB.Name = "BANDLAB";
            this.BANDLAB.VisibleIndex = 4;
            this.BANDLAB.Width = 120;
            // 
            // bandedGridColumn20
            // 
            this.bandedGridColumn20.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn20.Caption = "Pcs";
            this.bandedGridColumn20.FieldName = "LABPCS";
            this.bandedGridColumn20.Name = "bandedGridColumn20";
            this.bandedGridColumn20.Width = 50;
            // 
            // bandedGridColumn21
            // 
            this.bandedGridColumn21.Caption = "%";
            this.bandedGridColumn21.ColumnEdit = this.repPrgLabPcsPer;
            this.bandedGridColumn21.FieldName = "LABPCSPER";
            this.bandedGridColumn21.Name = "bandedGridColumn21";
            this.bandedGridColumn21.Width = 120;
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
            // bandedGridColumn22
            // 
            this.bandedGridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn22.Caption = "Cts";
            this.bandedGridColumn22.FieldName = "LABCARAT";
            this.bandedGridColumn22.Name = "bandedGridColumn22";
            this.bandedGridColumn22.Width = 70;
            // 
            // bandedGridColumn23
            // 
            this.bandedGridColumn23.Caption = "%";
            this.bandedGridColumn23.ColumnEdit = this.repPrgLabCaratPer;
            this.bandedGridColumn23.FieldName = "LABCARATPER";
            this.bandedGridColumn23.Name = "bandedGridColumn23";
            this.bandedGridColumn23.Width = 120;
            // 
            // repPrgLabCaratPer
            // 
            this.repPrgLabCaratPer.EndColor = System.Drawing.Color.Black;
            this.repPrgLabCaratPer.Name = "repPrgLabCaratPer";
            this.repPrgLabCaratPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgLabCaratPer.ShowTitle = true;
            this.repPrgLabCaratPer.StartColor = System.Drawing.Color.Black;
            // 
            // bandedGridColumn24
            // 
            this.bandedGridColumn24.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn24.Caption = "Amt";
            this.bandedGridColumn24.FieldName = "LABAMOUNT";
            this.bandedGridColumn24.Name = "bandedGridColumn24";
            this.bandedGridColumn24.Width = 100;
            // 
            // bandedGridColumn25
            // 
            this.bandedGridColumn25.Caption = "%";
            this.bandedGridColumn25.ColumnEdit = this.repPrgLabAmountPer;
            this.bandedGridColumn25.FieldName = "LABAMOUNTPER";
            this.bandedGridColumn25.Name = "bandedGridColumn25";
            this.bandedGridColumn25.Width = 120;
            // 
            // repPrgLabAmountPer
            // 
            this.repPrgLabAmountPer.EndColor = System.Drawing.Color.Black;
            this.repPrgLabAmountPer.Name = "repPrgLabAmountPer";
            this.repPrgLabAmountPer.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repPrgLabAmountPer.ShowTitle = true;
            this.repPrgLabAmountPer.StartColor = System.Drawing.Color.Black;
            // 
            // FrmPlanningDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 693);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.hideContainerLeft);
            this.Controls.Add(this.panel4);
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmPlanningDashboard";
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
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDPARTICULARS;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDMAKABLE;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDPOLISH;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgPolPcsPer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgPolCaratPer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgPolAmountPer;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDBY;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn15;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgByPcsPer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn16;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn17;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgByCaratPer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn18;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn19;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgByAmountPer;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BANDLAB;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn20;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn21;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgLabPcsPer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn22;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn23;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgLabCaratPer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn24;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn25;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repPrgLabAmountPer;


    }
}