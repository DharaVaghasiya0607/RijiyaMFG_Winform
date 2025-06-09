namespace AxoneMFGRJ.Masters
{
    partial class FrmMixSize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMixSize));
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtFromCarat = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtToCarat = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepChkActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RpTxtDeptName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCmbSizeWiseDept = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repIsAdditionalAssortment = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.RepTxtRemk = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedAmountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new AxonContLib.cPanel(this.components);
            this.CmbRoughType = new AxonContLib.cComboBox(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtFromCarat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtToCarat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepChkActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpTxtDeptName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbSizeWiseDept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repIsAdditionalAssortment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepTxtRemk)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.BtnExport);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnClear);
            this.panel1.Controls.Add(this.BtnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 357);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(853, 50);
            this.panel1.TabIndex = 193;
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.ImageOptions.Image = global::AxoneMFGRJ.Properties.Resources.btnexcelexport;
            this.BtnExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExport.ImageOptions.SvgImage")));
            this.BtnExport.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExport.Location = new System.Drawing.Point(201, 6);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(90, 35);
            this.BtnExport.TabIndex = 37;
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
            this.BtnSave.ImageOptions.Image = global::AxoneMFGRJ.Properties.Resources.btnsave;
            this.BtnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSave.ImageOptions.SvgImage")));
            this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSave.Location = new System.Drawing.Point(9, 6);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(90, 35);
            this.BtnSave.TabIndex = 34;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseForeColor = true;
            this.BtnClear.ImageOptions.Image = global::AxoneMFGRJ.Properties.Resources.btnclear;
            this.BtnClear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnClear.ImageOptions.SvgImage")));
            this.BtnClear.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnClear.Location = new System.Drawing.Point(105, 6);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(90, 35);
            this.BtnClear.TabIndex = 35;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "&Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBack.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Appearance.Options.UseForeColor = true;
            this.BtnBack.ImageOptions.Image = global::AxoneMFGRJ.Properties.Resources.btnexit;
            this.BtnBack.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnBack.ImageOptions.SvgImage")));
            this.BtnBack.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnBack.Location = new System.Drawing.Point(297, 6);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(90, 35);
            this.BtnBack.TabIndex = 36;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 50);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepTxtRemk,
            this.reptxtFromCarat,
            this.reptxtToCarat,
            this.reptxtName,
            this.RpTxtDeptName,
            this.RepChkActive,
            this.repCmbSizeWiseDept,
            this.repTxtRemark,
            this.repIsAdditionalAssortment});
            this.MainGrid.Size = new System.Drawing.Size(853, 307);
            this.MainGrid.TabIndex = 194;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.Row.Options.UseTextOptions = true;
            this.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 13F);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn12,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Sr No";
            this.gridColumn1.FieldName = "SEQUENCENO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Size_ID";
            this.gridColumn2.FieldName = "SIZE_ID";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "From Carat";
            this.gridColumn3.ColumnEdit = this.reptxtFromCarat;
            this.gridColumn3.FieldName = "FROMCARAT";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // reptxtFromCarat
            // 
            this.reptxtFromCarat.AutoHeight = false;
            this.reptxtFromCarat.Name = "reptxtFromCarat";
            this.reptxtFromCarat.Validating += new System.ComponentModel.CancelEventHandler(this.reptxtFromCarat_Validating);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "To Carat";
            this.gridColumn4.ColumnEdit = this.reptxtToCarat;
            this.gridColumn4.FieldName = "TOCARAT";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // reptxtToCarat
            // 
            this.reptxtToCarat.AutoHeight = false;
            this.reptxtToCarat.Name = "reptxtToCarat";
            this.reptxtToCarat.Validating += new System.ComponentModel.CancelEventHandler(this.reptxtToCarat_Validating);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Name";
            this.gridColumn5.ColumnEdit = this.reptxtName;
            this.gridColumn5.FieldName = "SIZENAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 172;
            // 
            // reptxtName
            // 
            this.reptxtName.AutoHeight = false;
            this.reptxtName.Name = "reptxtName";
            this.reptxtName.Validating += new System.ComponentModel.CancelEventHandler(this.reptxtName_Validating);
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "FinalReportGroup";
            this.gridColumn12.FieldName = "FINALREPORTGROUP";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 2;
            this.gridColumn12.Width = 123;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Active";
            this.gridColumn6.ColumnEdit = this.RepChkActive;
            this.gridColumn6.FieldName = "ISACTIVE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 121;
            // 
            // RepChkActive
            // 
            this.RepChkActive.AutoHeight = false;
            this.RepChkActive.Caption = "Check";
            this.RepChkActive.Name = "RepChkActive";
            this.RepChkActive.PictureChecked = global::AxoneMFGRJ.Properties.Resources.Checked;
            this.RepChkActive.PictureUnchecked = global::AxoneMFGRJ.Properties.Resources.Unchecked;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Remark";
            this.gridColumn7.ColumnEdit = this.repTxtRemark;
            this.gridColumn7.FieldName = "REMARK";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 239;
            // 
            // repTxtRemark
            // 
            this.repTxtRemark.AutoHeight = false;
            this.repTxtRemark.Name = "repTxtRemark";
            this.repTxtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repTxtRemark_KeyDown);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Department_ID";
            this.gridColumn8.FieldName = "DEPARTMENT_ID";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Dept Name";
            this.gridColumn9.ColumnEdit = this.RpTxtDeptName;
            this.gridColumn9.FieldName = "DEPARTMENT_NAME";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            this.gridColumn9.Width = 167;
            // 
            // RpTxtDeptName
            // 
            this.RpTxtDeptName.AutoHeight = false;
            this.RpTxtDeptName.Name = "RpTxtDeptName";
            this.RpTxtDeptName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RpTxtDeptName_KeyDown);
            this.RpTxtDeptName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RpTxtDeptName_KeyPress);
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Size Wise Department";
            this.gridColumn10.ColumnEdit = this.repCmbSizeWiseDept;
            this.gridColumn10.FieldName = "SIZEWISEDEPARTMENT_ID";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 7;
            this.gridColumn10.Width = 250;
            // 
            // repCmbSizeWiseDept
            // 
            this.repCmbSizeWiseDept.AutoHeight = false;
            this.repCmbSizeWiseDept.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbSizeWiseDept.Name = "repCmbSizeWiseDept";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "IsAdditionalAssortment";
            this.gridColumn11.ColumnEdit = this.repIsAdditionalAssortment;
            this.gridColumn11.FieldName = "ISADDITIONALASSORTMENT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 164;
            // 
            // repIsAdditionalAssortment
            // 
            this.repIsAdditionalAssortment.AutoHeight = false;
            this.repIsAdditionalAssortment.Caption = "Check";
            this.repIsAdditionalAssortment.Name = "repIsAdditionalAssortment";
            // 
            // RepTxtRemk
            // 
            this.RepTxtRemk.AutoHeight = false;
            this.RepTxtRemk.Name = "RepTxtRemk";
            this.RepTxtRemk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RepTxtRemk_KeyDown);
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
            // panel4
            // 
            this.panel4.Controls.Add(this.CmbRoughType);
            this.panel4.Controls.Add(this.cLabel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(853, 50);
            this.panel4.TabIndex = 195;
            // 
            // CmbRoughType
            // 
            this.CmbRoughType.AllowTabKeyOnEnter = true;
            this.CmbRoughType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbRoughType.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbRoughType.ForeColor = System.Drawing.Color.Black;
            this.CmbRoughType.FormattingEnabled = true;
            this.CmbRoughType.Items.AddRange(new object[] {
            "NATURAL",
            "HPHT",
            "CVD"});
            this.CmbRoughType.Location = new System.Drawing.Point(69, 13);
            this.CmbRoughType.Name = "CmbRoughType";
            this.CmbRoughType.Size = new System.Drawing.Size(394, 26);
            this.CmbRoughType.TabIndex = 28;
            this.CmbRoughType.ToolTips = "";
            this.CmbRoughType.SelectedIndexChanged += new System.EventHandler(this.CmbRoughType_SelectedIndexChanged);
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(20, 16);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(43, 18);
            this.cLabel8.TabIndex = 0;
            this.cLabel8.Text = "Type";
            this.cLabel8.ToolTips = "";
            // 
            // FrmMixSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 407);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmMixSize";
            this.Text = "MIX SIZE MASTER";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtFromCarat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtToCarat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepChkActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpTxtDeptName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbSizeWiseDept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repIsAdditionalAssortment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepTxtRemk)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnClear;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
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
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RepTxtRemk;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtFromCarat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtToCarat;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RpTxtDeptName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit RepChkActive;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repCmbSizeWiseDept;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedAmountToolStripMenuItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRemark;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repIsAdditionalAssortment;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private AxonContLib.cPanel panel4;
        private AxonContLib.cComboBox CmbRoughType;
        private AxonContLib.cLabel cLabel8;
    }
}