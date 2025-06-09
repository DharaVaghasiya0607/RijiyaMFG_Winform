namespace AxoneMFGRJ
{
    partial class FrmSearchBoxForRapCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchBoxForRapCalc));
            this.txtSeach = new System.Windows.Forms.TextBox();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.panel1 = new AxonContLib.cPanel();
            this.txtPassForEditBack = new AxonContLib.cTextBox(this.components);
            this.lblDeptExport = new System.Windows.Forms.Label();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.panel2 = new AxonContLib.cPanel();
            this.panel3 = new AxonContLib.cPanel();
            this.panel4 = new AxonContLib.cPanel();
            this.lblMessage = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSeach
            // 
            this.txtSeach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSeach.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSeach.Font = new System.Drawing.Font("Cambria", 13F);
            this.txtSeach.Location = new System.Drawing.Point(10, 26);
            this.txtSeach.Name = "txtSeach";
            this.txtSeach.Size = new System.Drawing.Size(732, 28);
            this.txtSeach.TabIndex = 1;
            this.txtSeach.TextChanged += new System.EventHandler(this.txtSeach_TextChanged);
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
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseForeColor = true;
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
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 9F);
            this.GrdDet.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDet.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
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
            this.GrdDet.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GrdDet_FocusedRowChanged);
            this.GrdDet.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDet_CellValueChanged);
            this.GrdDet.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDet_CellValueChanging);
            this.GrdDet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdDet_KeyDown);
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(10, 62);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(732, 427);
            this.MainGrid.TabIndex = 3;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            this.MainGrid.Click += new System.EventHandler(this.MainGrid_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPassForEditBack);
            this.panel1.Controls.Add(this.lblDeptExport);
            this.panel1.Controls.Add(this.cLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 26);
            this.panel1.TabIndex = 0;
            // 
            // txtPassForEditBack
            // 
            this.txtPassForEditBack.ActivationColor = false;
            this.txtPassForEditBack.AllowTabKeyOnEnter = false;
            this.txtPassForEditBack.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPassForEditBack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassForEditBack.ComplusoryMsg = null;
            this.txtPassForEditBack.Format = "";
            this.txtPassForEditBack.IsComplusory = false;
            this.txtPassForEditBack.Location = new System.Drawing.Point(146, 6);
            this.txtPassForEditBack.Name = "txtPassForEditBack";
            this.txtPassForEditBack.PasswordChar = '*';
            this.txtPassForEditBack.RequiredChars = "";
            this.txtPassForEditBack.SelectAllTextOnFocus = true;
            this.txtPassForEditBack.ShowToolTipOnFocus = false;
            this.txtPassForEditBack.Size = new System.Drawing.Size(45, 14);
            this.txtPassForEditBack.TabIndex = 157;
            this.txtPassForEditBack.TabStop = false;
            this.txtPassForEditBack.Tag = "AXONE";
            this.txtPassForEditBack.ToolTips = "";
            this.txtPassForEditBack.WaterMarkText = null;
            this.txtPassForEditBack.TextChanged += new System.EventHandler(this.txtPassForEditBack_TextChanged);
            // 
            // lblDeptExport
            // 
            this.lblDeptExport.AutoSize = true;
            this.lblDeptExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeptExport.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblDeptExport.ForeColor = System.Drawing.Color.Blue;
            this.lblDeptExport.Location = new System.Drawing.Point(692, 7);
            this.lblDeptExport.Name = "lblDeptExport";
            this.lblDeptExport.Size = new System.Drawing.Size(50, 13);
            this.lblDeptExport.TabIndex = 179;
            this.lblDeptExport.Text = "Export";
            this.lblDeptExport.Click += new System.EventHandler(this.lblDeptExport_Click);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Navy;
            this.cLabel1.Location = new System.Drawing.Point(12, 9);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(131, 13);
            this.cLabel1.TabIndex = 0;
            this.cLabel1.Text = "Type to search / filter";
            this.cLabel1.ToolTips = "";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(732, 8);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 463);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(742, 26);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 463);
            this.panel4.TabIndex = 5;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Maroon;
            this.lblMessage.Location = new System.Drawing.Point(42, 469);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(63, 13);
            this.lblMessage.TabIndex = 6;
            this.lblMessage.Text = "Message";
            this.lblMessage.ToolTips = "";
            // 
            // FrmSearchBoxForRapCalc
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 489);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtSeach);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmSearchBoxForRapCalc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLEASE SELECT ANY OF ONE RECORD";
            this.Load += new System.EventHandler(this.FrmSearch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSearch_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSeach;
        public DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cPanel panel2;
        private AxonContLib.cPanel panel3;
        private AxonContLib.cPanel panel4;
        private AxonContLib.cLabel cLabel1;
        private System.Windows.Forms.Label lblDeptExport;
        public DevExpress.XtraGrid.GridControl MainGrid;
        private AxonContLib.cTextBox txtPassForEditBack;
        private AxonContLib.cLabel lblMessage;

      

    }
}