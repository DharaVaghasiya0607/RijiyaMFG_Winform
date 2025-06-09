namespace AxoneMFGRJ.Masters
{
    partial class FrmKapanProcessSetting
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
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtParaName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkIsActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repTxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtParaCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repTxtSeqNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repCmbProcessGroup = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.RepCmbPrdType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.RepCmbLockAmtPrdType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.panel4 = new AxonContLib.cPanel();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.CmbSettingType = new AxonContLib.cComboBox(this.components);
            this.BtnUpdateInAllKapan = new DevExpress.XtraEditors.SimpleButton();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.txtKapanName = new AxonContLib.cTextBox(this.components);
            this.panel1 = new AxonContLib.cPanel();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSeqNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbProcessGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbPrdType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbLockAmtPrdType)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 50);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkIsActive,
            this.repTxtRemark,
            this.repTxtParaName,
            this.repTxtParaCode,
            this.repTxtSeqNo,
            this.repCmbProcessGroup,
            this.RepCmbPrdType,
            this.RepCmbLockAmtPrdType});
            this.MainGrid.Size = new System.Drawing.Size(1082, 373);
            this.MainGrid.TabIndex = 0;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
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
            this.gridColumn15,
            this.gridColumn6,
            this.gridColumn13,
            this.gridColumn14});
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
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Setting_ID";
            this.gridColumn1.FieldName = "SETTING_ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "PARA_ID";
            this.gridColumn15.FieldName = "PARA_ID";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Process";
            this.gridColumn6.ColumnEdit = this.repTxtParaName;
            this.gridColumn6.FieldName = "PARANAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 260;
            // 
            // repTxtParaName
            // 
            this.repTxtParaName.AutoHeight = false;
            this.repTxtParaName.MaxLength = 100;
            this.repTxtParaName.Name = "repTxtParaName";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Due Hours";
            this.gridColumn13.FieldName = "DUEHOURS";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            this.gridColumn13.Width = 100;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Loss %";
            this.gridColumn14.FieldName = "LOSSPER";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            this.gridColumn14.Width = 100;
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
            // repTxtParaCode
            // 
            this.repTxtParaCode.AutoHeight = false;
            this.repTxtParaCode.MaxLength = 15;
            this.repTxtParaCode.Name = "repTxtParaCode";
            // 
            // repTxtSeqNo
            // 
            this.repTxtSeqNo.AutoHeight = false;
            this.repTxtSeqNo.Mask.EditMask = "f0";
            this.repTxtSeqNo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repTxtSeqNo.Name = "repTxtSeqNo";
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
            // RepCmbLockAmtPrdType
            // 
            this.RepCmbLockAmtPrdType.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbLockAmtPrdType.Appearance.Options.UseFont = true;
            this.RepCmbLockAmtPrdType.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbLockAmtPrdType.AppearanceDropDown.Options.UseFont = true;
            this.RepCmbLockAmtPrdType.AppearanceFocused.Font = new System.Drawing.Font("Verdana", 9F);
            this.RepCmbLockAmtPrdType.AppearanceFocused.Options.UseFont = true;
            this.RepCmbLockAmtPrdType.AutoHeight = false;
            this.RepCmbLockAmtPrdType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepCmbLockAmtPrdType.DropDownRows = 20;
            this.RepCmbLockAmtPrdType.Name = "RepCmbLockAmtPrdType";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(20, 16);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(54, 18);
            this.cLabel8.TabIndex = 0;
            this.cLabel8.Text = "Kapan";
            this.cLabel8.ToolTips = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cLabel2);
            this.panel4.Controls.Add(this.cLabel1);
            this.panel4.Controls.Add(this.CmbSettingType);
            this.panel4.Controls.Add(this.BtnUpdateInAllKapan);
            this.panel4.Controls.Add(this.BtnShow);
            this.panel4.Controls.Add(this.txtKapanName);
            this.panel4.Controls.Add(this.cLabel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 50);
            this.panel4.TabIndex = 0;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(214, 16);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(44, 18);
            this.cLabel1.TabIndex = 38;
            this.cLabel1.Text = "Type";
            this.cLabel1.ToolTips = "";
            // 
            // CmbSettingType
            // 
            this.CmbSettingType.AllowTabKeyOnEnter = true;
            this.CmbSettingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSettingType.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.CmbSettingType.ForeColor = System.Drawing.Color.Black;
            this.CmbSettingType.FormattingEnabled = true;
            this.CmbSettingType.Items.AddRange(new object[] {
            "PROCESS",
            "DEPARTMENT"});
            this.CmbSettingType.Location = new System.Drawing.Point(259, 13);
            this.CmbSettingType.Name = "CmbSettingType";
            this.CmbSettingType.Size = new System.Drawing.Size(157, 24);
            this.CmbSettingType.TabIndex = 2;
            this.CmbSettingType.ToolTips = "";
            this.CmbSettingType.SelectedIndexChanged += new System.EventHandler(this.CmbSettingType_SelectedIndexChanged);
            // 
            // BtnUpdateInAllKapan
            // 
            this.BtnUpdateInAllKapan.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnUpdateInAllKapan.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnUpdateInAllKapan.Appearance.Options.UseFont = true;
            this.BtnUpdateInAllKapan.Appearance.Options.UseForeColor = true;
            this.BtnUpdateInAllKapan.Location = new System.Drawing.Point(533, 9);
            this.BtnUpdateInAllKapan.Name = "BtnUpdateInAllKapan";
            this.BtnUpdateInAllKapan.Size = new System.Drawing.Size(147, 32);
            this.BtnUpdateInAllKapan.TabIndex = 36;
            this.BtnUpdateInAllKapan.Text = "Update In All Kapan";
            this.BtnUpdateInAllKapan.Click += new System.EventHandler(this.BtnUpdateInAllKapan_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.Appearance.Options.UseForeColor = true;
            this.BtnShow.Location = new System.Drawing.Point(429, 9);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(98, 32);
            this.BtnShow.TabIndex = 35;
            this.BtnShow.Text = "&Show F5";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // txtKapanName
            // 
            this.txtKapanName.ActivationColor = true;
            this.txtKapanName.AllowTabKeyOnEnter = false;
            this.txtKapanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKapanName.ComplusoryMsg = null;
            this.txtKapanName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKapanName.Format = "";
            this.txtKapanName.IsComplusory = false;
            this.txtKapanName.Location = new System.Drawing.Point(78, 14);
            this.txtKapanName.Name = "txtKapanName";
            this.txtKapanName.RequiredChars = "";
            this.txtKapanName.SelectAllTextOnFocus = true;
            this.txtKapanName.ShowToolTipOnFocus = false;
            this.txtKapanName.Size = new System.Drawing.Size(123, 23);
            this.txtKapanName.TabIndex = 1;
            this.txtKapanName.ToolTips = "";
            this.txtKapanName.WaterMarkText = null;
            this.txtKapanName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKapanName_KeyPress);
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
            this.BtnExport.Location = new System.Drawing.Point(224, 6);
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
            this.BtnSave.Location = new System.Drawing.Point(12, 6);
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
            this.BtnAdd.Location = new System.Drawing.Point(118, 6);
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
            this.BtnBack.Location = new System.Drawing.Point(330, 6);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 32;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.cLabel2.ForeColor = System.Drawing.Color.DarkGreen;
            this.cLabel2.Location = new System.Drawing.Point(683, 25);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(140, 12);
            this.cLabel2.TabIndex = 39;
            this.cLabel2.Text = "Note : 1 Day = 12 Hours";
            this.cLabel2.ToolTips = "";
            // 
            // FrmKapanProcessSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Name = "FrmKapanProcessSetting";
            this.Text = "KAPAN PROCESS SETTING";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmKapanProcessSetting_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtSeqNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbProcessGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbPrdType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbLockAmtPrdType)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cPanel panel4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkIsActive;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtParaCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtParaName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtSeqNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repCmbProcessGroup;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit RepCmbPrdType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit RepCmbLockAmtPrdType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private AxonContLib.cTextBox txtKapanName;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnUpdateInAllKapan;
        private AxonContLib.cComboBox CmbSettingType;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel2;


    }
}