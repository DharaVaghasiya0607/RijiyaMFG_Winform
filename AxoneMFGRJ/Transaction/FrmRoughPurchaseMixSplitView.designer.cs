namespace AxoneMFGRJ.Transaction
{
    partial class FrmRoughPurchaseMixSplitView
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
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.compeletToolStripUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4 = new AxonContLib.cPanel();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.DTPToInvoiceDate = new AxonContLib.cDateTimePicker(this.components);
            this.labelControl2 = new AxonContLib.cLabel(this.components);
            this.DTPFromInvoiceDate = new AxonContLib.cDateTimePicker(this.components);
            this.labelControl8 = new AxonContLib.cLabel(this.components);
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAutoFit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new AxonContLib.cPanel();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.MainGridDetail = new DevExpress.XtraGrid.GridControl();
            this.GrdDetDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MainGrid.Location = new System.Drawing.Point(0, 0);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(723, 521);
            this.MainGrid.TabIndex = 2;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compeletToolStripUpdate});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 26);
            // 
            // compeletToolStripUpdate
            // 
            this.compeletToolStripUpdate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compeletToolStripUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.compeletToolStripUpdate.Name = "compeletToolStripUpdate";
            this.compeletToolStripUpdate.Size = new System.Drawing.Size(155, 22);
            this.compeletToolStripUpdate.Text = "Complete";
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.ColumnPanelRowHeight = 50;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.GrdDet.DetailHeight = 431;
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 30;
            this.GrdDet.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GrdDet_RowClick);
            this.GrdDet.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GrdDet_FocusedRowChanged);
            this.GrdDet.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDet_CustomColumnDisplayText);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Trn ID";
            this.gridColumn1.FieldName = "TRN_ID";
            this.gridColumn1.MinWidth = 25;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 124;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Operation";
            this.gridColumn2.FieldName = "OPERATION";
            this.gridColumn2.MinWidth = 25;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 156;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Credit Carat";
            this.gridColumn3.FieldName = "CREDITCARAT";
            this.gridColumn3.MinWidth = 25;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 106;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Debit Carat";
            this.gridColumn4.FieldName = "DEBITCARAT";
            this.gridColumn4.MinWidth = 25;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 125;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Entry Date";
            this.gridColumn5.FieldName = "ENTRYDATE";
            this.gridColumn5.MinWidth = 25;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 152;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.BtnDelete);
            this.panel4.Controls.Add(this.DTPToInvoiceDate);
            this.panel4.Controls.Add(this.labelControl2);
            this.panel4.Controls.Add(this.DTPFromInvoiceDate);
            this.panel4.Controls.Add(this.labelControl8);
            this.panel4.Controls.Add(this.BtnPrint);
            this.panel4.Controls.Add(this.BtnSearch);
            this.panel4.Controls.Add(this.BtnAutoFit);
            this.panel4.Controls.Add(this.BtnExit);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1418, 61);
            this.panel4.TabIndex = 0;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.Location = new System.Drawing.Point(523, 10);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(114, 42);
            this.BtnDelete.TabIndex = 158;
            this.BtnDelete.TabStop = false;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // DTPToInvoiceDate
            // 
            this.DTPToInvoiceDate.AllowTabKeyOnEnter = true;
            this.DTPToInvoiceDate.Checked = false;
            this.DTPToInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToInvoiceDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPToInvoiceDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToInvoiceDate.Location = new System.Drawing.Point(299, 18);
            this.DTPToInvoiceDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DTPToInvoiceDate.Name = "DTPToInvoiceDate";
            this.DTPToInvoiceDate.Size = new System.Drawing.Size(127, 26);
            this.DTPToInvoiceDate.TabIndex = 166;
            this.DTPToInvoiceDate.ToolTips = "";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(272, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 18);
            this.labelControl2.TabIndex = 165;
            this.labelControl2.Text = "To";
            this.labelControl2.ToolTips = "";
            // 
            // DTPFromInvoiceDate
            // 
            this.DTPFromInvoiceDate.AllowTabKeyOnEnter = true;
            this.DTPFromInvoiceDate.Checked = false;
            this.DTPFromInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromInvoiceDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromInvoiceDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromInvoiceDate.Location = new System.Drawing.Point(143, 18);
            this.DTPFromInvoiceDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DTPFromInvoiceDate.Name = "DTPFromInvoiceDate";
            this.DTPFromInvoiceDate.Size = new System.Drawing.Size(127, 26);
            this.DTPFromInvoiceDate.TabIndex = 164;
            this.DTPFromInvoiceDate.ToolTips = "";
            // 
            // labelControl8
            // 
            this.labelControl8.AutoSize = true;
            this.labelControl8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(8, 22);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(133, 18);
            this.labelControl8.TabIndex = 163;
            this.labelControl8.Text = "Transaction Date";
            this.labelControl8.ToolTips = "";
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.Appearance.Options.UseForeColor = true;
            this.BtnPrint.Image = global::AxoneMFGRJ.Properties.Resources.btnexcelexport;
            this.BtnPrint.Location = new System.Drawing.Point(734, 10);
            this.BtnPrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(93, 43);
            this.BtnPrint.TabIndex = 152;
            this.BtnPrint.TabStop = false;
            this.BtnPrint.Text = "Export";
            this.BtnPrint.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Image = global::AxoneMFGRJ.Properties.Resources.btnsearch;
            this.BtnSearch.Location = new System.Drawing.Point(428, 10);
            this.BtnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(93, 43);
            this.BtnSearch.TabIndex = 149;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnAutoFit
            // 
            this.BtnAutoFit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAutoFit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAutoFit.Appearance.Options.UseFont = true;
            this.BtnAutoFit.Appearance.Options.UseForeColor = true;
            this.BtnAutoFit.Image = global::AxoneMFGRJ.Properties.Resources.btnbestfit;
            this.BtnAutoFit.Location = new System.Drawing.Point(639, 10);
            this.BtnAutoFit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnAutoFit.Name = "BtnAutoFit";
            this.BtnAutoFit.Size = new System.Drawing.Size(93, 43);
            this.BtnAutoFit.TabIndex = 150;
            this.BtnAutoFit.TabStop = false;
            this.BtnAutoFit.Text = "Best Fit";
            this.BtnAutoFit.Click += new System.EventHandler(this.BtnAutoFit_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Image = global::AxoneMFGRJ.Properties.Resources.btnexit;
            this.BtnExit.Location = new System.Drawing.Point(829, 10);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(93, 43);
            this.BtnExit.TabIndex = 151;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainerControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 61);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1418, 521);
            this.panel2.TabIndex = 19;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.MainGrid);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.MainGridDetail);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1418, 521);
            this.splitContainerControl1.SplitterPosition = 723;
            this.splitContainerControl1.TabIndex = 3;
            // 
            // MainGridDetail
            // 
            this.MainGridDetail.ContextMenuStrip = this.contextMenuStrip1;
            this.MainGridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridDetail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MainGridDetail.Location = new System.Drawing.Point(0, 0);
            this.MainGridDetail.MainView = this.GrdDetDetail;
            this.MainGridDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MainGridDetail.Name = "MainGridDetail";
            this.MainGridDetail.Size = new System.Drawing.Size(683, 521);
            this.MainGridDetail.TabIndex = 3;
            this.MainGridDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetDetail});
            // 
            // GrdDetDetail
            // 
            this.GrdDetDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetDetail.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDetDetail.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDetDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetDetail.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDetDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDetDetail.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDetDetail.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDetDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetDetail.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDetDetail.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetDetail.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetDetail.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDetDetail.Appearance.Row.Options.UseFont = true;
            this.GrdDetDetail.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetDetail.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetDetail.ColumnPanelRowHeight = 50;
            this.GrdDetDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn22,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn23,
            this.gridColumn24});
            this.GrdDetDetail.DetailHeight = 431;
            this.GrdDetDetail.GridControl = this.MainGridDetail;
            this.GrdDetDetail.Name = "GrdDetDetail";
            this.GrdDetDetail.OptionsBehavior.Editable = false;
            this.GrdDetDetail.OptionsCustomization.AllowSort = false;
            this.GrdDetDetail.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetDetail.OptionsPrint.ExpandAllGroups = false;
            this.GrdDetDetail.OptionsView.ColumnAutoWidth = false;
            this.GrdDetDetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetDetail.OptionsView.ShowAutoFilterRow = true;
            this.GrdDetDetail.OptionsView.ShowFooter = true;
            this.GrdDetDetail.OptionsView.ShowGroupPanel = false;
            this.GrdDetDetail.RowHeight = 30;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Trn ID";
            this.gridColumn6.FieldName = "TRN_ID";
            this.gridColumn6.MinWidth = 25;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 148;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Operation";
            this.gridColumn7.FieldName = "OPERATION";
            this.gridColumn7.MinWidth = 25;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 156;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Lot ID";
            this.gridColumn8.FieldName = "LOT_ID";
            this.gridColumn8.MinWidth = 25;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 106;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "Lot No";
            this.gridColumn9.FieldName = "LOTNO";
            this.gridColumn9.MinWidth = 25;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            this.gridColumn9.Width = 208;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Ref Lot ID";
            this.gridColumn10.FieldName = "REFLOT_ID";
            this.gridColumn10.MinWidth = 25;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Width = 203;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Ref Lot No";
            this.gridColumn11.FieldName = "REFLOTNO";
            this.gridColumn11.MinWidth = 25;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            this.gridColumn11.Width = 94;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Sr.";
            this.gridColumn12.FieldName = "SRNO";
            this.gridColumn12.MinWidth = 25;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 94;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Credit Carat";
            this.gridColumn13.FieldName = "CREDITCARAT";
            this.gridColumn13.MinWidth = 25;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 7;
            this.gridColumn13.Width = 94;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Credit Rate";
            this.gridColumn14.FieldName = "CREDITRATE";
            this.gridColumn14.MinWidth = 25;
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 8;
            this.gridColumn14.Width = 94;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Credit Amount";
            this.gridColumn22.FieldName = "CREDITAMOUNT";
            this.gridColumn22.MinWidth = 25;
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 16;
            this.gridColumn22.Width = 94;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Debit Carat";
            this.gridColumn15.FieldName = "DEBITCARAT";
            this.gridColumn15.MinWidth = 25;
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 9;
            this.gridColumn15.Width = 94;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Debit Rate";
            this.gridColumn16.FieldName = "DEBITRATE";
            this.gridColumn16.MinWidth = 25;
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 10;
            this.gridColumn16.Width = 94;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Debit Amount";
            this.gridColumn17.FieldName = "DEBITAMOUNT";
            this.gridColumn17.MinWidth = 25;
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 11;
            this.gridColumn17.Width = 94;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Total Trf Carat";
            this.gridColumn18.FieldName = "TOTALTRFCARAT";
            this.gridColumn18.MinWidth = 25;
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 12;
            this.gridColumn18.Width = 94;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Total Trf Rate";
            this.gridColumn19.FieldName = "TOTALTRFRATE";
            this.gridColumn19.MinWidth = 25;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 13;
            this.gridColumn19.Width = 94;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Total Trf Amount";
            this.gridColumn20.FieldName = "TOTALTRFAMOUNT";
            this.gridColumn20.MinWidth = 25;
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 14;
            this.gridColumn20.Width = 94;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Entry Date";
            this.gridColumn21.FieldName = "ENTRYDATE";
            this.gridColumn21.MinWidth = 25;
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 15;
            this.gridColumn21.Width = 94;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Sys. Inv. No";
            this.gridColumn23.FieldName = "SYSTEMINVOICENO";
            this.gridColumn23.MinWidth = 25;
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 2;
            this.gridColumn23.Width = 152;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "Manual Inv. No";
            this.gridColumn24.FieldName = "MANUALINVOICENO";
            this.gridColumn24.MinWidth = 25;
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 3;
            this.gridColumn24.Width = 136;
            // 
            // FrmRoughPurchaseMixSplitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1418, 582);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmRoughPurchaseMixSplitView";
            this.Text = "MIX SPLIT HISTORY VIEW";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPurchaseLiveStock_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel panel4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private DevExpress.XtraEditors.SimpleButton BtnAutoFit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private System.Windows.Forms.ToolStripMenuItem compeletToolStripUpdate;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private AxonContLib.cDateTimePicker DTPToInvoiceDate;
        private AxonContLib.cLabel labelControl2;
        private AxonContLib.cDateTimePicker DTPFromInvoiceDate;
        private AxonContLib.cLabel labelControl8;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl MainGridDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;


    }
}