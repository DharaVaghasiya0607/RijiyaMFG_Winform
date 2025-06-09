namespace AxoneMFGRJ
{
    partial class FrmPopupGrid
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPopupGrid));
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.LblTitle = new DevExpress.XtraEditors.LabelControl();
            this.panel4 = new AxonContLib.cPanel();
            this.lblPacketExport = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 10F);
            this.GrdDet.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GrdDet.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 10F);
            this.GrdDet.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GrdDet.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 10F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 9F);
            this.GrdDet.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.GrdDet.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.GrdDet.OptionsBehavior.FocusLeaveOnTab = true;
            this.GrdDet.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            this.GrdDet.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GrdDet.OptionsFind.FindDelay = 100;
            this.GrdDet.OptionsFind.FindFilterColumns = "";
            this.GrdDet.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.GrdDet.OptionsFind.HighlightFindResults = false;
            this.GrdDet.OptionsFind.SearchInPreview = true;
            this.GrdDet.OptionsFind.ShowCloseButton = false;
            this.GrdDet.OptionsNavigation.AutoFocusNewRow = true;
            this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GrdDet_RowClick);
            this.GrdDet.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDet_CellValueChanging);
            this.GrdDet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdDet_KeyDown);
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.MainGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.MainGrid.Location = new System.Drawing.Point(0, 0);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(784, 396);
            this.MainGrid.TabIndex = 1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            this.MainGrid.Click += new System.EventHandler(this.MainGrid_Click);
            // 
            // LblTitle
            // 
            this.LblTitle.Appearance.BackColor = System.Drawing.Color.Silver;
            this.LblTitle.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.LblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(784, 29);
            this.LblTitle.TabIndex = 43;
            this.LblTitle.Text = "List";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.MainGrid);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(784, 396);
            this.panel4.TabIndex = 44;
            // 
            // lblPacketExport
            // 
            this.lblPacketExport.AutoSize = true;
            this.lblPacketExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPacketExport.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblPacketExport.ForeColor = System.Drawing.Color.Blue;
            this.lblPacketExport.Location = new System.Drawing.Point(-3, 16);
            this.lblPacketExport.Name = "lblPacketExport";
            this.lblPacketExport.Size = new System.Drawing.Size(50, 13);
            this.lblPacketExport.TabIndex = 179;
            this.lblPacketExport.Text = "Export";
            this.lblPacketExport.Click += new System.EventHandler(this.lblPacketExport_Click);
            // 
            // FrmPopupGrid
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 425);
            this.Controls.Add(this.lblPacketExport);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.LblTitle);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmPopupGrid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLEASE SELECT ANY OF ONE RECORD";
            this.Load += new System.EventHandler(this.FrmPopupGrid_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPopupGrid_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        public DevExpress.XtraGrid.GridControl MainGrid;
        private AxonContLib.cPanel panel4;
        private System.Windows.Forms.Label lblPacketExport;
        public DevExpress.XtraEditors.LabelControl LblTitle;

      

    }
}