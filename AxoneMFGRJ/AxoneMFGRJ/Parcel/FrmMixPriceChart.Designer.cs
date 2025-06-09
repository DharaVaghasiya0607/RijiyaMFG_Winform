namespace AxoneMFGRJ.Parcel
{
    partial class FrmMixPriceChart
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
            this.txtFileName = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.lblSampleExcelFile = new AxonContLib.cLabel(this.components);
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.BtnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new AxonContLib.cLabel(this.components);
            this.label2 = new AxonContLib.cLabel(this.components);
            this.label3 = new AxonContLib.cLabel(this.components);
            this.txtDepartment = new AxonContLib.cTextBox(this.components);
            this.txtPriceID = new AxonContLib.cTextBox(this.components);
            this.txtShape = new AxonContLib.cTextBox(this.components);
            this.lblMessage = new AxonContLib.cLabel(this.components);
            this.MainGrdStock = new DevExpress.XtraGrid.GridControl();
            this.GrdDetStock = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.RpsTxtbxShape = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.RpsTxtMixClrty = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.RpstxtFrmCarat = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.RpstxtToCarat = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.RpstxtPrice = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpsTxtbxShape)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpsTxtMixClrty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpstxtFrmCarat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpstxtToCarat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpstxtPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.ActivationColor = true;
            this.txtFileName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtFileName.AllowTabKeyOnEnter = false;
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtFileName.Format = "";
            this.txtFileName.IsComplusory = false;
            this.txtFileName.Location = new System.Drawing.Point(40, 30);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.SelectAllTextOnFocus = true;
            this.txtFileName.Size = new System.Drawing.Size(181, 22);
            this.txtFileName.TabIndex = 179;
            this.txtFileName.ToolTips = "";
            this.txtFileName.WaterMarkText = null;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F);
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(7, 34);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(28, 14);
            this.cLabel2.TabIndex = 180;
            this.cLabel2.Text = "File";
            this.cLabel2.ToolTips = "";
            // 
            // lblSampleExcelFile
            // 
            this.lblSampleExcelFile.AutoSize = true;
            this.lblSampleExcelFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSampleExcelFile.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblSampleExcelFile.ForeColor = System.Drawing.Color.Green;
            this.lblSampleExcelFile.Location = new System.Drawing.Point(20, 63);
            this.lblSampleExcelFile.Name = "lblSampleExcelFile";
            this.lblSampleExcelFile.Size = new System.Drawing.Size(239, 13);
            this.lblSampleExcelFile.TabIndex = 181;
            this.lblSampleExcelFile.Text = "Click Here To Download File Format";
            this.lblSampleExcelFile.ToolTips = "";
            this.lblSampleExcelFile.Click += new System.EventHandler(this.lblSampleExcelFile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnClear);
            this.panel1.Controls.Add(this.BtnExport);
            this.panel1.Controls.Add(this.BtnShow);
            this.panel1.Controls.Add(this.groupControl1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtDepartment);
            this.panel1.Controls.Add(this.txtPriceID);
            this.panel1.Controls.Add(this.txtShape);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1073, 83);
            this.panel1.TabIndex = 178;
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseForeColor = true;
            this.BtnClear.Image = global::AxoneMFGRJ.Properties.Resources.btnclear;
            this.BtnClear.Location = new System.Drawing.Point(605, 25);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(90, 35);
            this.BtnClear.TabIndex = 180;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "&Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Image = global::AxoneMFGRJ.Properties.Resources.btnexcelexport;
            this.BtnExport.Location = new System.Drawing.Point(509, 25);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(90, 35);
            this.BtnExport.TabIndex = 181;
            this.BtnExport.TabStop = false;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.Appearance.Options.UseForeColor = true;
            this.BtnShow.Image = global::AxoneMFGRJ.Properties.Resources.btnsearch;
            this.BtnShow.Location = new System.Drawing.Point(400, 25);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(103, 35);
            this.BtnShow.TabIndex = 3;
            this.BtnShow.Text = "Show";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.BtnBrowse);
            this.groupControl1.Controls.Add(this.lblSampleExcelFile);
            this.groupControl1.Controls.Add(this.txtFileName);
            this.groupControl1.Controls.Add(this.BtnUpload);
            this.groupControl1.Controls.Add(this.cLabel2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl1.Location = new System.Drawing.Point(726, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(347, 83);
            this.groupControl1.TabIndex = 209;
            this.groupControl1.Text = "Bulk Upload";
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBrowse.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBrowse.Appearance.Options.UseFont = true;
            this.BtnBrowse.Appearance.Options.UseForeColor = true;
            this.BtnBrowse.Location = new System.Drawing.Point(224, 29);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(25, 24);
            this.BtnBrowse.TabIndex = 182;
            this.BtnBrowse.Text = "...";
            this.BtnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // BtnUpload
            // 
            this.BtnUpload.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnUpload.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnUpload.Appearance.Options.UseFont = true;
            this.BtnUpload.Appearance.Options.UseForeColor = true;
            this.BtnUpload.Image = global::AxoneMFGRJ.Properties.Resources.btnsave;
            this.BtnUpload.Location = new System.Drawing.Point(255, 25);
            this.BtnUpload.Name = "BtnUpload";
            this.BtnUpload.Size = new System.Drawing.Size(87, 35);
            this.BtnUpload.TabIndex = 179;
            this.BtnUpload.Text = "Upload";
            this.BtnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Price Head";
            this.label1.ToolTips = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(240, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Department";
            this.label2.ToolTips = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(136, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Shape";
            this.label3.ToolTips = "";
            // 
            // txtDepartment
            // 
            this.txtDepartment.AccessibleDescription = "SHAPE";
            this.txtDepartment.ActivationColor = true;
            this.txtDepartment.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtDepartment.AllowTabKeyOnEnter = true;
            this.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDepartment.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtDepartment.Format = "";
            this.txtDepartment.IsComplusory = false;
            this.txtDepartment.Location = new System.Drawing.Point(240, 38);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.SelectAllTextOnFocus = true;
            this.txtDepartment.Size = new System.Drawing.Size(143, 22);
            this.txtDepartment.TabIndex = 2;
            this.txtDepartment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDepartment.ToolTips = "";
            this.txtDepartment.WaterMarkText = null;
            this.txtDepartment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDepartment_KeyPress);
            // 
            // txtPriceID
            // 
            this.txtPriceID.AccessibleDescription = "";
            this.txtPriceID.ActivationColor = true;
            this.txtPriceID.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPriceID.AllowTabKeyOnEnter = true;
            this.txtPriceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPriceID.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtPriceID.Format = "";
            this.txtPriceID.IsComplusory = false;
            this.txtPriceID.Location = new System.Drawing.Point(8, 38);
            this.txtPriceID.Name = "txtPriceID";
            this.txtPriceID.SelectAllTextOnFocus = true;
            this.txtPriceID.Size = new System.Drawing.Size(124, 22);
            this.txtPriceID.TabIndex = 0;
            this.txtPriceID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPriceID.ToolTips = "";
            this.txtPriceID.WaterMarkText = null;
            this.txtPriceID.TextChanged += new System.EventHandler(this.txtPriceID_TextChanged);
            this.txtPriceID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPriceID_KeyPress);
            // 
            // txtShape
            // 
            this.txtShape.AccessibleDescription = "SHAPE";
            this.txtShape.ActivationColor = true;
            this.txtShape.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtShape.AllowTabKeyOnEnter = true;
            this.txtShape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShape.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtShape.Format = "";
            this.txtShape.IsComplusory = false;
            this.txtShape.Location = new System.Drawing.Point(136, 38);
            this.txtShape.Name = "txtShape";
            this.txtShape.SelectAllTextOnFocus = true;
            this.txtShape.Size = new System.Drawing.Size(99, 22);
            this.txtShape.TabIndex = 1;
            this.txtShape.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShape.ToolTips = "";
            this.txtShape.WaterMarkText = null;
            this.txtShape.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShape_KeyPress);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMessage.Location = new System.Drawing.Point(388, 63);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(63, 13);
            this.lblMessage.TabIndex = 171;
            this.lblMessage.Text = "Message";
            this.lblMessage.ToolTips = "";
            // 
            // MainGrdStock
            // 
            this.MainGrdStock.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.MainGrdStock.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.MainGrdStock.Location = new System.Drawing.Point(0, 83);
            this.MainGrdStock.MainView = this.GrdDetStock;
            this.MainGrdStock.Name = "MainGrdStock";
            this.MainGrdStock.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RpsTxtbxShape,
            this.RpsTxtMixClrty,
            this.RpstxtFrmCarat,
            this.RpstxtToCarat,
            this.RpstxtPrice});
            this.MainGrdStock.Size = new System.Drawing.Size(1073, 364);
            this.MainGrdStock.TabIndex = 179;
            this.MainGrdStock.TabStop = false;
            this.MainGrdStock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetStock});
            // 
            // GrdDetStock
            // 
            this.GrdDetStock.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdDetStock.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdDetStock.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetStock.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetStock.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetStock.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetStock.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDetStock.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDetStock.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetStock.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetStock.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetStock.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDetStock.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetStock.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetStock.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8.5F);
            this.GrdDetStock.Appearance.Row.Options.UseFont = true;
            this.GrdDetStock.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetStock.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetStock.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetStock.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDetStock.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetStock.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDetStock.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetStock.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDetStock.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetStock.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDetStock.AppearancePrint.Row.BackColor = System.Drawing.Color.Transparent;
            this.GrdDetStock.AppearancePrint.Row.Options.UseBackColor = true;
            this.GrdDetStock.ColumnPanelRowHeight = 40;
            this.GrdDetStock.GridControl = this.MainGrdStock;
            this.GrdDetStock.Name = "GrdDetStock";
            this.GrdDetStock.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.GrdDetStock.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetStock.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDetStock.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDetStock.OptionsPrint.ExpandAllGroups = false;
            this.GrdDetStock.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDetStock.OptionsView.ColumnAutoWidth = false;
            this.GrdDetStock.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetStock.OptionsView.ShowAutoFilterRow = true;
            this.GrdDetStock.OptionsView.ShowFooter = true;
            this.GrdDetStock.OptionsView.ShowGroupPanel = false;
            this.GrdDetStock.RowHeight = 25;
            this.GrdDetStock.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDetStock_CellValueChanged);
            this.GrdDetStock.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDetStock_CustomColumnDisplayText);
            // 
            // RpsTxtbxShape
            // 
            this.RpsTxtbxShape.AutoHeight = false;
            this.RpsTxtbxShape.Name = "RpsTxtbxShape";
            // 
            // RpsTxtMixClrty
            // 
            this.RpsTxtMixClrty.AutoHeight = false;
            this.RpsTxtMixClrty.Name = "RpsTxtMixClrty";
            // 
            // RpstxtFrmCarat
            // 
            this.RpstxtFrmCarat.AutoHeight = false;
            this.RpstxtFrmCarat.Name = "RpstxtFrmCarat";
            // 
            // RpstxtToCarat
            // 
            this.RpstxtToCarat.AutoHeight = false;
            this.RpstxtToCarat.Name = "RpstxtToCarat";
            // 
            // RpstxtPrice
            // 
            this.RpstxtPrice.AutoHeight = false;
            this.RpstxtPrice.Name = "RpstxtPrice";
            // 
            // FrmMixPriceChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 447);
            this.Controls.Add(this.MainGrdStock);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMixPriceChart";
            this.Text = "MIX ASSORTMENT PRICE";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrdStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpsTxtbxShape)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpsTxtMixClrty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpstxtFrmCarat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpstxtToCarat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpstxtPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cTextBox txtFileName;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cLabel lblSampleExcelFile;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cLabel label1;
        private AxonContLib.cLabel label2;
        private AxonContLib.cLabel label3;
        private AxonContLib.cTextBox txtDepartment;
        private AxonContLib.cTextBox txtPriceID;
        private AxonContLib.cTextBox txtShape;
        private AxonContLib.cLabel lblMessage;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnClear;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnUpload;
        private DevExpress.XtraGrid.GridControl MainGrdStock;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetStock;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RpsTxtbxShape;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RpsTxtMixClrty;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RpstxtFrmCarat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RpstxtToCarat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RpstxtPrice;
        private DevExpress.XtraEditors.SimpleButton BtnBrowse;

    }
}