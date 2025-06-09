namespace AxoneMFGRJ.Polish
{
    partial class FrmIsAdditionalAssortment
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
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnBack = new System.Windows.Forms.Button();
            this.MainGridSize = new DevExpress.XtraGrid.GridControl();
            this.GrdDetAs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtKapan = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ReptxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetAs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtKapan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReptxtRemark)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 186);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 49);
            this.panel1.TabIndex = 2;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(7, 8);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 31);
            this.BtnSave.TabIndex = 50;
            this.BtnSave.TabStop = false;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(54)))), ((int)(((byte)(16)))));
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.Color.White;
            this.BtnBack.Location = new System.Drawing.Point(116, 8);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 31);
            this.BtnBack.TabIndex = 30;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // MainGridSize
            // 
            this.MainGridSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridSize.Location = new System.Drawing.Point(0, 0);
            this.MainGridSize.MainView = this.GrdDetAs;
            this.MainGridSize.Name = "MainGridSize";
            this.MainGridSize.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reptxtKapan,
            this.reptxtRate,
            this.ReptxtRemark});
            this.MainGridSize.Size = new System.Drawing.Size(364, 186);
            this.MainGridSize.TabIndex = 45;
            this.MainGridSize.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetAs});
            // 
            // GrdDetAs
            // 
            this.GrdDetAs.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdDetAs.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(191)))));
            this.GrdDetAs.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetAs.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetAs.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDetAs.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetAs.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDetAs.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDetAs.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetAs.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDetAs.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetAs.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDetAs.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetAs.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDetAs.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetAs.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetAs.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDetAs.Appearance.Row.Options.UseFont = true;
            this.GrdDetAs.Appearance.Row.Options.UseTextOptions = true;
            this.GrdDetAs.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDetAs.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDetAs.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetAs.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 13F);
            this.GrdDetAs.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDetAs.ColumnPanelRowHeight = 25;
            this.GrdDetAs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.GrdDetAs.GridControl = this.MainGridSize;
            this.GrdDetAs.Name = "GrdDetAs";
            this.GrdDetAs.OptionsCustomization.AllowSort = false;
            this.GrdDetAs.OptionsFilter.AllowFilterEditor = false;
            this.GrdDetAs.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDetAs.OptionsPrint.ExpandAllGroups = false;
            this.GrdDetAs.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDetAs.OptionsView.ColumnAutoWidth = false;
            this.GrdDetAs.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetAs.OptionsView.ShowFooter = true;
            this.GrdDetAs.OptionsView.ShowGroupPanel = false;
            this.GrdDetAs.RowHeight = 25;
            this.GrdDetAs.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDetAs_CellValueChanged);
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "SIZEASSORT_ID";
            this.gridColumn6.FieldName = "SIZEASSROT_ID";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "SIZE_ID";
            this.gridColumn20.FieldName = "SIZE_ID";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "kapanName";
            this.gridColumn21.ColumnEdit = this.reptxtKapan;
            this.gridColumn21.FieldName = "KAPANNAME";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn21.Width = 101;
            // 
            // reptxtKapan
            // 
            this.reptxtKapan.AutoHeight = false;
            this.reptxtKapan.Name = "reptxtKapan";
            this.reptxtKapan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.reptxtKapan_KeyPress);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Rate";
            this.gridColumn1.ColumnEdit = this.reptxtRate;
            this.gridColumn1.FieldName = "RATE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            // 
            // reptxtRate
            // 
            this.reptxtRate.AutoHeight = false;
            this.reptxtRate.Name = "reptxtRate";
            this.reptxtRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.reptxtRate_KeyDown);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Size";
            this.gridColumn2.FieldName = "SIZENAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Cts";
            this.gridColumn3.FieldName = "CARAT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "DETAIL_ID";
            this.gridColumn4.FieldName = "DETAIL_ID";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "INWARD_ID";
            this.gridColumn5.FieldName = "INWARD_ID";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "KAPAN_ID";
            this.gridColumn7.FieldName = "KAPAN_ID";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Remark";
            this.gridColumn8.ColumnEdit = this.ReptxtRemark;
            this.gridColumn8.FieldName = "REMARK";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Amt";
            this.gridColumn9.FieldName = "AMOUNT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            // 
            // ReptxtRemark
            // 
            this.ReptxtRemark.AutoHeight = false;
            this.ReptxtRemark.Name = "ReptxtRemark";
            this.ReptxtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReptxtRemark_KeyDown);
            // 
            // FrmIsAdditionalAssortment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 235);
            this.Controls.Add(this.MainGridSize);
            this.Controls.Add(this.panel1);
            this.Name = "FrmIsAdditionalAssortment";
            this.Text = "Fancy Rate Entry";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetAs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtKapan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReptxtRemark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel panel1;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnBack;
        private DevExpress.XtraGrid.GridControl MainGridSize;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetAs;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtKapan;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtRate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ReptxtRemark;
    }
}