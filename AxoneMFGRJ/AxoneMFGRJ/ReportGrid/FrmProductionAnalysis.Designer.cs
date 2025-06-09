namespace AxoneMFGRJ.Masters
{
    partial class FrmProductionAnalysis
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView7 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView8 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView9 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView10 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView11 = new DevExpress.XtraCharts.AreaSeriesView();
            DevExpress.XtraCharts.AreaSeriesView areaSeriesView12 = new DevExpress.XtraCharts.AreaSeriesView();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.panel1 = new AxonContLib.cPanel();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.panel2 = new AxonContLib.cPanel();
            this.RbtYearly = new AxonContLib.cRadioButton(this.components);
            this.RbtDaily = new AxonContLib.cRadioButton(this.components);
            this.RbtQuater = new AxonContLib.cRadioButton(this.components);
            this.RbtWeekly = new AxonContLib.cRadioButton(this.components);
            this.RbtMonthly = new AxonContLib.cRadioButton(this.components);
            this.MainGridMakable = new DevExpress.XtraGrid.GridControl();
            this.GrdDetMakable = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblMakable = new AxonContLib.cLabel(this.components);
            this.panel4 = new AxonContLib.cPanel();
            this.ChartControlAreaMakable = new DevExpress.XtraCharts.ChartControl();
            this.panel5 = new AxonContLib.cPanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panel3 = new AxonContLib.cPanel();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.MainGridPolish = new DevExpress.XtraGrid.GridControl();
            this.GrdDetPolish = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel6 = new AxonContLib.cPanel();
            this.ChartControlAreaPolish = new DevExpress.XtraCharts.ChartControl();
            this.panel7 = new AxonContLib.cPanel();
            this.lblPolish = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridMakable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetMakable)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControlAreaMakable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView9)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridPolish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetPolish)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControlAreaPolish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView12)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSearch.Location = new System.Drawing.Point(365, 11);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(87, 32);
            this.BtnSearch.TabIndex = 0;
            this.BtnSearch.Text = "Refresh";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject2.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject2.Options.UseFont = true;
            serializableAppearanceObject2.Options.UseForeColor = true;
            serializableAppearanceObject2.Options.UseTextOptions = true;
            serializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
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
            this.DTPToDate.Location = new System.Drawing.Point(226, 14);
            this.DTPToDate.Name = "DTPToDate";
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
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(91, 14);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(129, 24);
            this.DTPFromDate.TabIndex = 160;
            this.DTPFromDate.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(12, 20);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(75, 13);
            this.cLabel9.TabIndex = 158;
            this.cLabel9.Text = "From Date";
            this.cLabel9.ToolTips = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressPanel1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.DTPToDate);
            this.panel1.Controls.Add(this.DTPFromDate);
            this.panel1.Controls.Add(this.cLabel9);
            this.panel1.Controls.Add(this.BtnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 47);
            this.panel1.TabIndex = 161;
            // 
            // progressPanel1
            // 
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanel1.AppearanceCaption.Options.UseFont = true;
            this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel1.AppearanceDescription.Options.UseFont = true;
            this.progressPanel1.Caption = "";
            this.progressPanel1.Location = new System.Drawing.Point(458, 14);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.ShowCaption = false;
            this.progressPanel1.ShowDescription = false;
            this.progressPanel1.Size = new System.Drawing.Size(33, 27);
            this.progressPanel1.TabIndex = 164;
            this.progressPanel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RbtYearly);
            this.panel2.Controls.Add(this.RbtDaily);
            this.panel2.Controls.Add(this.RbtQuater);
            this.panel2.Controls.Add(this.RbtWeekly);
            this.panel2.Controls.Add(this.RbtMonthly);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(772, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(412, 47);
            this.panel2.TabIndex = 163;
            // 
            // RbtYearly
            // 
            this.RbtYearly.AutoSize = true;
            this.RbtYearly.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtYearly.ForeColor = System.Drawing.Color.Black;
            this.RbtYearly.Location = new System.Drawing.Point(18, 16);
            this.RbtYearly.Name = "RbtYearly";
            this.RbtYearly.Size = new System.Drawing.Size(67, 17);
            this.RbtYearly.TabIndex = 162;
            this.RbtYearly.Text = "Yearly";
            this.RbtYearly.ToolTips = "";
            this.RbtYearly.UseVisualStyleBackColor = true;
            this.RbtYearly.CheckedChanged += new System.EventHandler(this.RbtYearly_CheckedChanged);
            // 
            // RbtDaily
            // 
            this.RbtDaily.AutoSize = true;
            this.RbtDaily.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtDaily.ForeColor = System.Drawing.Color.Black;
            this.RbtDaily.Location = new System.Drawing.Point(346, 16);
            this.RbtDaily.Name = "RbtDaily";
            this.RbtDaily.Size = new System.Drawing.Size(58, 17);
            this.RbtDaily.TabIndex = 162;
            this.RbtDaily.Text = "Daily";
            this.RbtDaily.ToolTips = "";
            this.RbtDaily.UseVisualStyleBackColor = true;
            this.RbtDaily.CheckedChanged += new System.EventHandler(this.RbtYearly_CheckedChanged);
            // 
            // RbtQuater
            // 
            this.RbtQuater.AutoSize = true;
            this.RbtQuater.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtQuater.ForeColor = System.Drawing.Color.Black;
            this.RbtQuater.Location = new System.Drawing.Point(93, 16);
            this.RbtQuater.Name = "RbtQuater";
            this.RbtQuater.Size = new System.Drawing.Size(81, 17);
            this.RbtQuater.TabIndex = 162;
            this.RbtQuater.Text = "Quaterly";
            this.RbtQuater.ToolTips = "";
            this.RbtQuater.UseVisualStyleBackColor = true;
            this.RbtQuater.CheckedChanged += new System.EventHandler(this.RbtYearly_CheckedChanged);
            // 
            // RbtWeekly
            // 
            this.RbtWeekly.AutoSize = true;
            this.RbtWeekly.Checked = true;
            this.RbtWeekly.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtWeekly.ForeColor = System.Drawing.Color.Black;
            this.RbtWeekly.Location = new System.Drawing.Point(265, 16);
            this.RbtWeekly.Name = "RbtWeekly";
            this.RbtWeekly.Size = new System.Drawing.Size(73, 17);
            this.RbtWeekly.TabIndex = 162;
            this.RbtWeekly.TabStop = true;
            this.RbtWeekly.Text = "Weekly";
            this.RbtWeekly.ToolTips = "";
            this.RbtWeekly.UseVisualStyleBackColor = true;
            this.RbtWeekly.CheckedChanged += new System.EventHandler(this.RbtYearly_CheckedChanged);
            // 
            // RbtMonthly
            // 
            this.RbtMonthly.AutoSize = true;
            this.RbtMonthly.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtMonthly.ForeColor = System.Drawing.Color.Black;
            this.RbtMonthly.Location = new System.Drawing.Point(182, 16);
            this.RbtMonthly.Name = "RbtMonthly";
            this.RbtMonthly.Size = new System.Drawing.Size(76, 17);
            this.RbtMonthly.TabIndex = 162;
            this.RbtMonthly.Text = "Monthly";
            this.RbtMonthly.ToolTips = "";
            this.RbtMonthly.UseVisualStyleBackColor = true;
            this.RbtMonthly.CheckedChanged += new System.EventHandler(this.RbtYearly_CheckedChanged);
            // 
            // MainGridMakable
            // 
            this.MainGridMakable.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode3.RelationName = "Level1";
            this.MainGridMakable.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode3});
            this.MainGridMakable.Location = new System.Drawing.Point(0, 0);
            this.MainGridMakable.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MainGridMakable.MainView = this.GrdDetMakable;
            this.MainGridMakable.Name = "MainGridMakable";
            this.MainGridMakable.Size = new System.Drawing.Size(692, 321);
            this.MainGridMakable.TabIndex = 162;
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
            this.gridColumn3,
            this.gridColumn7,
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn5,
            this.gridColumn9,
            this.gridColumn6});
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
            this.GrdDetMakable.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDetMakable_RowCellClick);
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
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(200)))));
            this.gridColumn3.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn3.Caption = "Pcs";
            this.gridColumn3.FieldName = "PCS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PCS", "{0:N0}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 49;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(200)))));
            this.gridColumn7.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn7.Caption = "%";
            this.gridColumn7.FieldName = "PCSPER";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 60;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridColumn4.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.Caption = "Cts";
            this.gridColumn4.FieldName = "CARAT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CARAT", "{0:N2}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 62;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridColumn8.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn8.Caption = "%";
            this.gridColumn8.FieldName = "CARATPER";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 52;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.BackColor = System.Drawing.Color.MistyRose;
            this.gridColumn5.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn5.Caption = "Amt";
            this.gridColumn5.FieldName = "AMOUNT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "{0:N2}")});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 87;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.BackColor = System.Drawing.Color.MistyRose;
            this.gridColumn9.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn9.Caption = "%";
            this.gridColumn9.FieldName = "AMOUNTPER";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            this.gridColumn9.Width = 53;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn6.Caption = "Emp";
            this.gridColumn6.FieldName = "EMPLOYEE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "EMPLOYEE", "{0:N2}")});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 46;
            // 
            // lblMakable
            // 
            this.lblMakable.AutoSize = true;
            this.lblMakable.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMakable.ForeColor = System.Drawing.Color.DimGray;
            this.lblMakable.Location = new System.Drawing.Point(6, 10);
            this.lblMakable.Name = "lblMakable";
            this.lblMakable.Size = new System.Drawing.Size(160, 19);
            this.lblMakable.TabIndex = 158;
            this.lblMakable.Text = "Makable Production";
            this.lblMakable.ToolTips = "";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.ChartControlAreaMakable);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(466, 321);
            this.panel4.TabIndex = 164;
            // 
            // ChartControlAreaMakable
            // 
            this.ChartControlAreaMakable.AppearanceNameSerializable = "The Trees";
            xyDiagram3.AxisX.NumericScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Automatic;
            xyDiagram3.AxisX.ScaleBreakOptions.Style = DevExpress.XtraCharts.ScaleBreakStyle.Straight;
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram3.AxisX.WholeRange.SideMarginsValue = 0.01D;
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            this.ChartControlAreaMakable.Diagram = xyDiagram3;
            this.ChartControlAreaMakable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartControlAreaMakable.Location = new System.Drawing.Point(0, 0);
            this.ChartControlAreaMakable.Name = "ChartControlAreaMakable";
            series5.Name = "Series 1";
            areaSeriesView7.Transparency = ((byte)(0));
            series5.View = areaSeriesView7;
            series6.Name = "Series 2";
            areaSeriesView8.Transparency = ((byte)(0));
            series6.View = areaSeriesView8;
            this.ChartControlAreaMakable.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
            this.ChartControlAreaMakable.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            areaSeriesView9.Transparency = ((byte)(0));
            this.ChartControlAreaMakable.SeriesTemplate.View = areaSeriesView9;
            this.ChartControlAreaMakable.Size = new System.Drawing.Size(466, 321);
            this.ChartControlAreaMakable.TabIndex = 49;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblMakable);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(2, 21);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1163, 39);
            this.panel5.TabIndex = 165;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.splitContainerControl1);
            this.groupControl1.Controls.Add(this.panel5);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1167, 383);
            this.groupControl1.TabIndex = 167;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.AutoScrollMinSize = new System.Drawing.Size(0, 1300);
            this.panel3.Controls.Add(this.groupControl2);
            this.panel3.Controls.Add(this.groupControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1184, 694);
            this.panel3.TabIndex = 165;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 60);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.MainGridMakable);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panel4);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1163, 321);
            this.splitContainerControl1.SplitterPosition = 692;
            this.splitContainerControl1.TabIndex = 166;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.splitContainerControl2);
            this.groupControl2.Controls.Add(this.panel7);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 383);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1167, 383);
            this.groupControl2.TabIndex = 168;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(2, 60);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.MainGridPolish);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.panel6);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1163, 321);
            this.splitContainerControl2.SplitterPosition = 692;
            this.splitContainerControl2.TabIndex = 166;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // MainGridPolish
            // 
            this.MainGridPolish.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.MainGridPolish.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGridPolish.Location = new System.Drawing.Point(0, 0);
            this.MainGridPolish.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MainGridPolish.MainView = this.GrdDetPolish;
            this.MainGridPolish.Name = "MainGridPolish";
            this.MainGridPolish.Size = new System.Drawing.Size(692, 321);
            this.MainGridPolish.TabIndex = 162;
            this.MainGridPolish.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetPolish});
            // 
            // GrdDetPolish
            // 
            this.GrdDetPolish.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetPolish.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetPolish.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetPolish.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetPolish.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDetPolish.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetPolish.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetPolish.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetPolish.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDetPolish.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDetPolish.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetPolish.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetPolish.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetPolish.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetPolish.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetPolish.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetPolish.Appearance.Row.Options.UseFont = true;
            this.GrdDetPolish.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetPolish.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetPolish.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetPolish.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPolish.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDetPolish.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDetPolish.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetPolish.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDetPolish.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDetPolish.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDetPolish.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetPolish.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDetPolish.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.GrdDetPolish.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDetPolish.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetPolish.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPolish.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDetPolish.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDetPolish.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDetPolish.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDetPolish.ColumnPanelRowHeight = 25;
            this.GrdDetPolish.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19});
            this.GrdDetPolish.GridControl = this.MainGridPolish;
            this.GrdDetPolish.Name = "GrdDetPolish";
            this.GrdDetPolish.OptionsBehavior.Editable = false;
            this.GrdDetPolish.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetPolish.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDetPolish.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDetPolish.OptionsPrint.ExpandAllGroups = false;
            this.GrdDetPolish.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDetPolish.OptionsSelection.MultiSelect = true;
            this.GrdDetPolish.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDetPolish.OptionsView.ColumnAutoWidth = false;
            this.GrdDetPolish.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetPolish.OptionsView.ShowFooter = true;
            this.GrdDetPolish.OptionsView.ShowGroupPanel = false;
            this.GrdDetPolish.RowHeight = 23;
            this.GrdDetPolish.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDetPolish_RowCellClick);
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "FIRSTDATE";
            this.gridColumn10.FieldName = "FIRSTDATE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "ENDDATE";
            this.gridColumn11.FieldName = "ENDDATE";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Particulars";
            this.gridColumn12.FieldName = "PARTICULAR";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            this.gridColumn12.Width = 224;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(200)))));
            this.gridColumn13.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn13.Caption = "Pcs";
            this.gridColumn13.FieldName = "PCS";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PCS", "{0:N0}")});
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            this.gridColumn13.Width = 49;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(200)))));
            this.gridColumn14.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn14.Caption = "%";
            this.gridColumn14.FieldName = "PCSPER";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            this.gridColumn14.Width = 60;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridColumn15.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn15.Caption = "Cts";
            this.gridColumn15.FieldName = "CARAT";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn15.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CARAT", "{0:N2}")});
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 3;
            this.gridColumn15.Width = 62;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridColumn16.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn16.Caption = "%";
            this.gridColumn16.FieldName = "CARATPER";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 4;
            this.gridColumn16.Width = 52;
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceCell.BackColor = System.Drawing.Color.MistyRose;
            this.gridColumn17.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn17.Caption = "Amt";
            this.gridColumn17.FieldName = "AMOUNT";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn17.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "{0:N2}")});
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 5;
            this.gridColumn17.Width = 87;
            // 
            // gridColumn18
            // 
            this.gridColumn18.AppearanceCell.BackColor = System.Drawing.Color.MistyRose;
            this.gridColumn18.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn18.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn18.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn18.Caption = "%";
            this.gridColumn18.FieldName = "AMOUNTPER";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 6;
            this.gridColumn18.Width = 53;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn19.Caption = "Emp";
            this.gridColumn19.FieldName = "EMPLOYEE";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "EMPLOYEE", "{0:N2}")});
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 7;
            this.gridColumn19.Width = 46;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.ChartControlAreaPolish);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(466, 321);
            this.panel6.TabIndex = 164;
            // 
            // ChartControlAreaPolish
            // 
            this.ChartControlAreaPolish.AppearanceNameSerializable = "The Trees";
            xyDiagram4.AxisX.NumericScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Automatic;
            xyDiagram4.AxisX.ScaleBreakOptions.Style = DevExpress.XtraCharts.ScaleBreakStyle.Straight;
            xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram4.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram4.AxisX.WholeRange.SideMarginsValue = 0.01D;
            xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
            this.ChartControlAreaPolish.Diagram = xyDiagram4;
            this.ChartControlAreaPolish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartControlAreaPolish.Location = new System.Drawing.Point(0, 0);
            this.ChartControlAreaPolish.Name = "ChartControlAreaPolish";
            series7.Name = "Series 1";
            areaSeriesView10.Transparency = ((byte)(0));
            series7.View = areaSeriesView10;
            series8.Name = "Series 2";
            areaSeriesView11.Transparency = ((byte)(0));
            series8.View = areaSeriesView11;
            this.ChartControlAreaPolish.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7,
        series8};
            this.ChartControlAreaPolish.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            areaSeriesView12.Transparency = ((byte)(0));
            this.ChartControlAreaPolish.SeriesTemplate.View = areaSeriesView12;
            this.ChartControlAreaPolish.Size = new System.Drawing.Size(466, 321);
            this.ChartControlAreaPolish.TabIndex = 49;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lblPolish);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(2, 21);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1163, 39);
            this.panel7.TabIndex = 165;
            // 
            // lblPolish
            // 
            this.lblPolish.AutoSize = true;
            this.lblPolish.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPolish.ForeColor = System.Drawing.Color.DimGray;
            this.lblPolish.Location = new System.Drawing.Point(6, 10);
            this.lblPolish.Name = "lblPolish";
            this.lblPolish.Size = new System.Drawing.Size(143, 19);
            this.lblPolish.TabIndex = 158;
            this.lblPolish.Text = "Polish Production";
            this.lblPolish.ToolTips = "";
            // 
            // FrmProductionAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 741);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmProductionAnalysis";
            this.Text = "PRODUCTION DASHBOARD";
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridMakable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetMakable)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControlAreaMakable)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridPolish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetPolish)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(areaSeriesView12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControlAreaPolish)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cRadioButton RbtQuater;
        private AxonContLib.cRadioButton RbtYearly;
        private AxonContLib.cRadioButton RbtMonthly;
        private AxonContLib.cRadioButton RbtWeekly;
        private AxonContLib.cRadioButton RbtDaily;
        private DevExpress.XtraGrid.GridControl MainGridMakable;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetMakable;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private AxonContLib.cLabel lblMakable;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
        private AxonContLib.cPanel panel4;
        private DevExpress.XtraCharts.ChartControl ChartControlAreaMakable;
        private AxonContLib.cPanel panel5;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cPanel panel3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraGrid.GridControl MainGridPolish;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetPolish;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private AxonContLib.cPanel panel6;
        private DevExpress.XtraCharts.ChartControl ChartControlAreaPolish;
        private AxonContLib.cPanel panel7;
        private AxonContLib.cLabel lblPolish;


    }
}