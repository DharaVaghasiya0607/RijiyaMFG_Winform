namespace AxoneMFGRJ.Rapaport
{
    partial class FrmTensionSakhatLabourPer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTensionSakhatLabourPer));
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedAmountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtLabourPer = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4 = new AxonContLib.cPanel(this.components);
            this.panel5 = new AxonContLib.cPanel(this.components);
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.PnlDollarLabourType = new AxonContLib.cPanel(this.components);
            this.CmbLabourType = new AxonContLib.cComboBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.panel2 = new AxonContLib.cPanel(this.components);
            this.txtYear = new AxonContLib.cTextBox(this.components);
            this.txtMonth = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.BtnLeft = new DevExpress.XtraEditors.SimpleButton();
            this.PnlCopyPaste = new AxonContLib.cPanel(this.components);
            this.CmbCopyToMonth = new AxonContLib.cComboBox(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.txtCopyToYear = new AxonContLib.cTextBox(this.components);
            this.BtnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtLabourPer)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.PnlDollarLabourType.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PnlCopyPaste.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 44);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTxtLabourPer});
            this.MainGrid.Size = new System.Drawing.Size(1082, 379);
            this.MainGrid.TabIndex = 1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedAmountToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(214, 26);
            // 
            // deleteSelectedAmountToolStripMenuItem
            // 
            this.deleteSelectedAmountToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.deleteSelectedAmountToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.deleteSelectedAmountToolStripMenuItem.Name = "deleteSelectedAmountToolStripMenuItem";
            this.deleteSelectedAmountToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.deleteSelectedAmountToolStripMenuItem.Text = "Delete Selected Item";
            this.deleteSelectedAmountToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedAmountToolStripMenuItem_Click);
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn5});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Tension Id";
            this.gridColumn1.FieldName = "PARA_ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Tension Name";
            this.gridColumn3.FieldName = "PARANAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 122;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "LabourPer";
            this.gridColumn4.ColumnEdit = this.repTxtLabourPer;
            this.gridColumn4.FieldName = "LABOURPER";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 105;
            // 
            // repTxtLabourPer
            // 
            this.repTxtLabourPer.AutoHeight = false;
            this.repTxtLabourPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repTxtLabourPer.Mask.EditMask = "f2";
            this.repTxtLabourPer.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repTxtLabourPer.Name = "repTxtLabourPer";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "ID";
            this.gridColumn2.FieldName = "LABOUR_ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Sr. No.";
            this.gridColumn5.FieldName = "LABOUR_SRNO";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 85;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.PnlDollarLabourType);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 44);
            this.panel4.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.BtnShow);
            this.panel5.Controls.Add(this.cLabel3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(523, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(559, 44);
            this.panel5.TabIndex = 149;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.Appearance.Options.UseForeColor = true;
            this.BtnShow.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnShow.ImageOptions.SvgImage")));
            this.BtnShow.Location = new System.Drawing.Point(3, 7);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(103, 31);
            this.BtnShow.TabIndex = 11;
            this.BtnShow.Text = "Show";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(471, 24);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(252, 14);
            this.cLabel3.TabIndex = 145;
            this.cLabel3.Text = "Month Wise Pcs Production Entry Module";
            this.cLabel3.ToolTips = "";
            this.cLabel3.Visible = false;
            // 
            // PnlDollarLabourType
            // 
            this.PnlDollarLabourType.Controls.Add(this.CmbLabourType);
            this.PnlDollarLabourType.Controls.Add(this.cLabel4);
            this.PnlDollarLabourType.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlDollarLabourType.Location = new System.Drawing.Point(235, 0);
            this.PnlDollarLabourType.Name = "PnlDollarLabourType";
            this.PnlDollarLabourType.Size = new System.Drawing.Size(288, 44);
            this.PnlDollarLabourType.TabIndex = 148;
            // 
            // CmbLabourType
            // 
            this.CmbLabourType.AllowTabKeyOnEnter = true;
            this.CmbLabourType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLabourType.Font = new System.Drawing.Font("Verdana", 11.25F);
            this.CmbLabourType.ForeColor = System.Drawing.Color.Black;
            this.CmbLabourType.FormattingEnabled = true;
            this.CmbLabourType.Items.AddRange(new object[] {
            "Tension",
            "Hardness"});
            this.CmbLabourType.Location = new System.Drawing.Point(44, 9);
            this.CmbLabourType.MaxDropDownItems = 12;
            this.CmbLabourType.Name = "CmbLabourType";
            this.CmbLabourType.Size = new System.Drawing.Size(237, 26);
            this.CmbLabourType.TabIndex = 5;
            this.CmbLabourType.ToolTips = "";
            this.CmbLabourType.SelectedIndexChanged += new System.EventHandler(this.CmbLabourType_SelectedIndexChanged);
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(7, 15);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(36, 14);
            this.cLabel4.TabIndex = 147;
            this.cLabel4.Text = "Type";
            this.cLabel4.ToolTips = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtYear);
            this.panel2.Controls.Add(this.txtMonth);
            this.panel2.Controls.Add(this.cLabel2);
            this.panel2.Controls.Add(this.cLabel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(235, 44);
            this.panel2.TabIndex = 3;
            // 
            // txtYear
            // 
            this.txtYear.ActivationColor = true;
            this.txtYear.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtYear.AllowTabKeyOnEnter = true;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtYear.Format = "######";
            this.txtYear.IsComplusory = false;
            this.txtYear.Location = new System.Drawing.Point(43, 11);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.SelectAllTextOnFocus = true;
            this.txtYear.Size = new System.Drawing.Size(86, 23);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.ToolTips = "";
            this.txtYear.WaterMarkText = null;
            // 
            // txtMonth
            // 
            this.txtMonth.ActivationColor = true;
            this.txtMonth.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtMonth.AllowTabKeyOnEnter = true;
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtMonth.Format = "######";
            this.txtMonth.IsComplusory = false;
            this.txtMonth.Location = new System.Drawing.Point(185, 11);
            this.txtMonth.MaxLength = 4;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.SelectAllTextOnFocus = true;
            this.txtMonth.Size = new System.Drawing.Size(46, 23);
            this.txtMonth.TabIndex = 3;
            this.txtMonth.Text = "0";
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.ToolTips = "";
            this.txtMonth.WaterMarkText = null;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(6, 15);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(34, 14);
            this.cLabel2.TabIndex = 0;
            this.cLabel2.Text = "Year";
            this.cLabel2.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(136, 15);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(46, 14);
            this.cLabel1.TabIndex = 2;
            this.cLabel1.Text = "Month";
            this.cLabel1.ToolTips = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.BtnLeft);
            this.panel1.Controls.Add(this.PnlCopyPaste);
            this.panel1.Controls.Add(this.BtnExport);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnAdd);
            this.panel1.Controls.Add(this.BtnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 50);
            this.panel1.TabIndex = 2;
            // 
            // BtnLeft
            // 
            this.BtnLeft.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnLeft.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnLeft.Appearance.Options.UseFont = true;
            this.BtnLeft.Appearance.Options.UseForeColor = true;
            this.BtnLeft.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.BtnLeft.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnLeft.ImageOptions.Image = global::AxoneMFGRJ.Properties.Resources.A2;
            this.BtnLeft.Location = new System.Drawing.Point(520, 0);
            this.BtnLeft.Name = "BtnLeft";
            this.BtnLeft.Size = new System.Drawing.Size(39, 50);
            this.BtnLeft.TabIndex = 35;
            this.BtnLeft.TabStop = false;
            this.BtnLeft.Click += new System.EventHandler(this.BtnLeft_Click);
            // 
            // PnlCopyPaste
            // 
            this.PnlCopyPaste.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PnlCopyPaste.Controls.Add(this.CmbCopyToMonth);
            this.PnlCopyPaste.Controls.Add(this.cLabel5);
            this.PnlCopyPaste.Controls.Add(this.cLabel6);
            this.PnlCopyPaste.Controls.Add(this.txtCopyToYear);
            this.PnlCopyPaste.Controls.Add(this.BtnCopy);
            this.PnlCopyPaste.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlCopyPaste.Location = new System.Drawing.Point(559, 0);
            this.PnlCopyPaste.Name = "PnlCopyPaste";
            this.PnlCopyPaste.Size = new System.Drawing.Size(523, 50);
            this.PnlCopyPaste.TabIndex = 34;
            // 
            // CmbCopyToMonth
            // 
            this.CmbCopyToMonth.AllowTabKeyOnEnter = true;
            this.CmbCopyToMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCopyToMonth.Font = new System.Drawing.Font("Verdana", 11.25F);
            this.CmbCopyToMonth.ForeColor = System.Drawing.Color.Black;
            this.CmbCopyToMonth.FormattingEnabled = true;
            this.CmbCopyToMonth.Items.AddRange(new object[] {
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"});
            this.CmbCopyToMonth.Location = new System.Drawing.Point(310, 12);
            this.CmbCopyToMonth.MaxDropDownItems = 12;
            this.CmbCopyToMonth.Name = "CmbCopyToMonth";
            this.CmbCopyToMonth.Size = new System.Drawing.Size(70, 26);
            this.CmbCopyToMonth.TabIndex = 2;
            this.CmbCopyToMonth.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(205, 18);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(104, 14);
            this.cLabel5.TabIndex = 9;
            this.cLabel5.Text = "Copy To Month";
            this.cLabel5.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(6, 18);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(96, 14);
            this.cLabel6.TabIndex = 7;
            this.cLabel6.Text = "Copy To Year";
            this.cLabel6.ToolTips = "";
            // 
            // txtCopyToYear
            // 
            this.txtCopyToYear.ActivationColor = true;
            this.txtCopyToYear.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtCopyToYear.AllowTabKeyOnEnter = true;
            this.txtCopyToYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCopyToYear.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtCopyToYear.Format = "######";
            this.txtCopyToYear.IsComplusory = false;
            this.txtCopyToYear.Location = new System.Drawing.Point(104, 14);
            this.txtCopyToYear.MaxLength = 4;
            this.txtCopyToYear.Name = "txtCopyToYear";
            this.txtCopyToYear.SelectAllTextOnFocus = true;
            this.txtCopyToYear.Size = new System.Drawing.Size(90, 23);
            this.txtCopyToYear.TabIndex = 1;
            this.txtCopyToYear.Text = "0";
            this.txtCopyToYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCopyToYear.ToolTips = "";
            this.txtCopyToYear.WaterMarkText = null;
            // 
            // BtnCopy
            // 
            this.BtnCopy.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnCopy.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCopy.Appearance.Options.UseFont = true;
            this.BtnCopy.Appearance.Options.UseForeColor = true;
            this.BtnCopy.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnCopy.ImageOptions.SvgImage")));
            this.BtnCopy.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnCopy.Location = new System.Drawing.Point(391, 8);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(103, 32);
            this.BtnCopy.TabIndex = 3;
            this.BtnCopy.Text = "Copy To";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExport.ImageOptions.SvgImage")));
            this.BtnExport.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExport.Location = new System.Drawing.Point(225, 8);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(103, 35);
            this.BtnExport.TabIndex = 33;
            this.BtnExport.TabStop = false;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSave.ImageOptions.SvgImage")));
            this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSave.Location = new System.Drawing.Point(9, 8);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 35);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAdd.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Appearance.Options.UseFont = true;
            this.BtnAdd.Appearance.Options.UseForeColor = true;
            this.BtnAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnAdd.ImageOptions.SvgImage")));
            this.BtnAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnAdd.Location = new System.Drawing.Point(117, 8);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(103, 35);
            this.BtnAdd.TabIndex = 31;
            this.BtnAdd.TabStop = false;
            this.BtnAdd.Text = "&Clear";
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBack.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Appearance.Options.UseForeColor = true;
            this.BtnBack.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnBack.ImageOptions.SvgImage")));
            this.BtnBack.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnBack.Location = new System.Drawing.Point(333, 8);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 32;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // FrmTensionSakhatLabourPer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmTensionSakhatLabourPer";
            this.Text = "TENSION/SAKHAT LABOUR PER";
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtLabourPer)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.PnlDollarLabourType.ResumeLayout(false);
            this.PnlDollarLabourType.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.PnlCopyPaste.ResumeLayout(false);
            this.PnlCopyPaste.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private AxonContLib.cPanel panel4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedAmountToolStripMenuItem;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtMonth;
        private AxonContLib.cTextBox txtYear;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cPanel PnlCopyPaste;
        private AxonContLib.cComboBox CmbCopyToMonth;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cTextBox txtCopyToYear;
        private DevExpress.XtraEditors.SimpleButton BtnCopy;
        private DevExpress.XtraEditors.SimpleButton BtnLeft;
        private AxonContLib.cComboBox CmbLabourType;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cPanel panel2;
        private AxonContLib.cPanel PnlDollarLabourType;
        private AxonContLib.cPanel panel5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtLabourPer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;


    }
}