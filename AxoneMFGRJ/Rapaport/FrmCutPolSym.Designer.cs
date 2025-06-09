namespace AxoneMFGRJ.Rapaport
{
    partial class FrmCutPolSym
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCutPolSym));
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedAmountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkIsActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repTxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtParaName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtParaCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtSeqNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.CmbLabourType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repCmbProcessGroup = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.RepCmbPrdType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.panel4 = new AxonContLib.cPanel(this.components);
            this.CmbParameterType = new AxonContLib.cComboBox(this.components);
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSeqNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbLabourType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbProcessGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbPrdType)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 50);
            this.MainGrid.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkIsActive,
            this.repTxtRemark,
            this.repTxtParaName,
            this.repTxtParaCode,
            this.repTxtSeqNo,
            this.CmbLabourType,
            this.repCmbProcessGroup,
            this.RepCmbPrdType});
            this.MainGrid.Size = new System.Drawing.Size(1082, 373);
            this.MainGrid.TabIndex = 0;
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
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
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
            this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
            this.GrdDet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdDet_KeyDown);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "ID";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Width = 150;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Cut ID";
            this.gridColumn1.FieldName = "CUT_ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 150;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Cut";
            this.gridColumn2.FieldName = "CUTCODE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Pol ID";
            this.gridColumn3.FieldName = "POL_ID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 150;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Pol";
            this.gridColumn4.FieldName = "POLCODE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 150;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Sym ID";
            this.gridColumn5.FieldName = "SYM_ID";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Width = 150;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Sym";
            this.gridColumn6.FieldName = "SYMNAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 150;
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
            this.repTxtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repTxtRemark_KeyDown);
            // 
            // repTxtParaName
            // 
            this.repTxtParaName.AutoHeight = false;
            this.repTxtParaName.MaxLength = 100;
            this.repTxtParaName.Name = "repTxtParaName";
            this.repTxtParaName.Validating += new System.ComponentModel.CancelEventHandler(this.repTxtParaName_Validating);
            // 
            // repTxtParaCode
            // 
            this.repTxtParaCode.AutoHeight = false;
            this.repTxtParaCode.MaxLength = 15;
            this.repTxtParaCode.Name = "repTxtParaCode";
            this.repTxtParaCode.Validating += new System.ComponentModel.CancelEventHandler(this.repTxtParaCode_Validating);
            // 
            // repTxtSeqNo
            // 
            this.repTxtSeqNo.AutoHeight = false;
            this.repTxtSeqNo.Mask.EditMask = "f0";
            this.repTxtSeqNo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repTxtSeqNo.Name = "repTxtSeqNo";
            // 
            // CmbLabourType
            // 
            this.CmbLabourType.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbLabourType.Appearance.Options.UseFont = true;
            this.CmbLabourType.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbLabourType.AppearanceDropDown.Options.UseFont = true;
            this.CmbLabourType.AppearanceFocused.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbLabourType.AppearanceFocused.Options.UseFont = true;
            this.CmbLabourType.AutoHeight = false;
            this.CmbLabourType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbLabourType.Items.AddRange(new object[] {
            "",
            "PCS",
            "CARAT"});
            this.CmbLabourType.Name = "CmbLabourType";
            this.CmbLabourType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repCmbProcessGroup
            // 
            this.repCmbProcessGroup.AutoHeight = false;
            this.repCmbProcessGroup.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbProcessGroup.Items.AddRange(new object[] {
            "CLV",
            "MFG",
            "COMMON",
            "BOMBAY",
            "OTHER"});
            this.repCmbProcessGroup.Name = "repCmbProcessGroup";
            this.repCmbProcessGroup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // RepCmbPrdType
            // 
            this.RepCmbPrdType.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbPrdType.Appearance.Options.UseFont = true;
            this.RepCmbPrdType.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbPrdType.AppearanceDropDown.Options.UseFont = true;
            this.RepCmbPrdType.AppearanceFocused.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbPrdType.AppearanceFocused.Options.UseFont = true;
            this.RepCmbPrdType.AutoHeight = false;
            this.RepCmbPrdType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepCmbPrdType.DropDownRows = 20;
            this.RepCmbPrdType.Name = "RepCmbPrdType";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(12, 14);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(43, 18);
            this.cLabel8.TabIndex = 0;
            this.cLabel8.Text = "Type";
            this.cLabel8.ToolTips = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.CmbParameterType);
            this.panel4.Controls.Add(this.cLabel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 50);
            this.panel4.TabIndex = 0;
            // 
            // CmbParameterType
            // 
            this.CmbParameterType.AllowTabKeyOnEnter = true;
            this.CmbParameterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbParameterType.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbParameterType.ForeColor = System.Drawing.Color.Black;
            this.CmbParameterType.FormattingEnabled = true;
            this.CmbParameterType.Items.AddRange(new object[] {
            "BLACK",
            "CANADAMARK",
            "CHARNI",
            "CLARITY",
            "COLOR",
            "COLORSHADE",
            "CUT",
            "CUT_POLISH_SYM_CC_DISCOUNT",
            "DEPARTMENT",
            "DESIGNATION",
            "EYECLEAN",
            "FLUORESCENCE",
            "FLUVISIBILITY",
            "GRAIN",
            "HEARTANDARROW",
            "LAB",
            "LBLC",
            "LUSTER",
            "MACHINECOLORTYPE",
            "MILKY",
            "NATTS",
            "NATURAL",
            "OPEN",
            "OTHERUNIT",
            "PAVALION",
            "PLANNINGTYPE",
            "POLISH",
            "PROCESS",
            "REPORTPATHPREFIX",
            "REPORTTYPE",
            "ROUGHTYPE",
            "SHAPE",
            "STATUS",
            "STOCKTYPE",
            "SUBLOT",
            "SYMMETRY",
            "TENSION",
            "WHITE",
            "REJECTION",
            "SOURCE",
            "ARTICLE",
            "MSIZE",
            "FILE_TRANSFER_TYPE",
            "CUT-POL-SYM-COMBINATION"});
            this.CmbParameterType.Location = new System.Drawing.Point(61, 11);
            this.CmbParameterType.Name = "CmbParameterType";
            this.CmbParameterType.Size = new System.Drawing.Size(394, 26);
            this.CmbParameterType.TabIndex = 28;
            this.CmbParameterType.ToolTips = "";
            this.CmbParameterType.SelectedIndexChanged += new System.EventHandler(this.CmbParameterType_SelectedIndexChanged);
            this.CmbParameterType.Validated += new System.EventHandler(this.CmbParameterType_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.BtnExport);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnAdd);
            this.panel1.Controls.Add(this.BtnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 50);
            this.panel1.TabIndex = 31;
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
            this.BtnSave.TabIndex = 30;
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
            // FrmCutPolSym
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
            this.Name = "FrmCutPolSym";
            this.Text = "CUT POL SYM";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLedger_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSeqNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbLabourType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbProcessGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbPrdType)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cPanel panel4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkIsActive;
        private AxonContLib.cComboBox CmbParameterType;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtParaCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtParaName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedAmountToolStripMenuItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtSeqNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox CmbLabourType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repCmbProcessGroup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit RepCmbPrdType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;


    }
}