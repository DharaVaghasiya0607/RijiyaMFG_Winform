
namespace AxoneMFGRJ.Rapaport
{
    partial class FrmDollarLabourUpload
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtYear = new AxonContLib.cTextBox(this.components);
            this.CmbMonth = new AxonContLib.cComboBox(this.components);
            this.btnshow = new System.Windows.Forms.Button();
            this.CmbSize = new System.Windows.Forms.ComboBox();
            this.CmbShape = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SIZE = new System.Windows.Forms.Label();
            this.SHAPE = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.MainGrid = new GridViewFooter.GridControlFooterOnTop();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GridData = new GridViewFooter.GridViewFooterOnTop();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TxtCut = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TxtPol = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TxtBonusPer = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TxtSym = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepCmbLabourType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtBonusPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtSym)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbLabourType)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtYear);
            this.panel1.Controls.Add(this.CmbMonth);
            this.panel1.Controls.Add(this.btnshow);
            this.panel1.Controls.Add(this.CmbSize);
            this.panel1.Controls.Add(this.CmbShape);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.SIZE);
            this.panel1.Controls.Add(this.SHAPE);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 64);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtYear
            // 
            this.txtYear.ActivationColor = true;
            this.txtYear.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtYear.AllowTabKeyOnEnter = true;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Format = "#######";
            this.txtYear.IsComplusory = false;
            this.txtYear.Location = new System.Drawing.Point(213, 7);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.SelectAllTextOnFocus = true;
            this.txtYear.Size = new System.Drawing.Size(75, 22);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYear.ToolTips = "";
            this.txtYear.WaterMarkText = null;
            // 
            // CmbMonth
            // 
            this.CmbMonth.AllowTabKeyOnEnter = true;
            this.CmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMonth.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbMonth.ForeColor = System.Drawing.Color.Black;
            this.CmbMonth.FormattingEnabled = true;
            this.CmbMonth.Items.AddRange(new object[] {
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
            this.CmbMonth.Location = new System.Drawing.Point(95, 7);
            this.CmbMonth.MaxDropDownItems = 12;
            this.CmbMonth.Name = "CmbMonth";
            this.CmbMonth.Size = new System.Drawing.Size(112, 22);
            this.CmbMonth.TabIndex = 0;
            this.CmbMonth.ToolTips = "";
            // 
            // btnshow
            // 
            this.btnshow.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnshow.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnshow.Location = new System.Drawing.Point(533, 12);
            this.btnshow.Name = "btnshow";
            this.btnshow.Size = new System.Drawing.Size(88, 42);
            this.btnshow.TabIndex = 4;
            this.btnshow.Text = "Show";
            this.btnshow.UseVisualStyleBackColor = false;
            this.btnshow.Click += new System.EventHandler(this.btnshow_Click);
            // 
            // CmbSize
            // 
            this.CmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSize.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbSize.FormattingEnabled = true;
            this.CmbSize.Location = new System.Drawing.Point(348, 8);
            this.CmbSize.Name = "CmbSize";
            this.CmbSize.Size = new System.Drawing.Size(178, 22);
            this.CmbSize.TabIndex = 2;
            // 
            // CmbShape
            // 
            this.CmbShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbShape.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbShape.FormattingEnabled = true;
            this.CmbShape.Location = new System.Drawing.Point(95, 35);
            this.CmbShape.Name = "CmbShape";
            this.CmbShape.Size = new System.Drawing.Size(193, 22);
            this.CmbShape.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "YearMonth";
            // 
            // SIZE
            // 
            this.SIZE.AutoSize = true;
            this.SIZE.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SIZE.Location = new System.Drawing.Point(308, 10);
            this.SIZE.Name = "SIZE";
            this.SIZE.Size = new System.Drawing.Size(35, 16);
            this.SIZE.TabIndex = 0;
            this.SIZE.Text = "Size";
            // 
            // SHAPE
            // 
            this.SHAPE.AutoSize = true;
            this.SHAPE.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SHAPE.Location = new System.Drawing.Point(37, 37);
            this.SHAPE.Name = "SHAPE";
            this.SHAPE.Size = new System.Drawing.Size(49, 16);
            this.SHAPE.TabIndex = 0;
            this.SHAPE.Text = "Shape";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnExport);
            this.panel2.Controls.Add(this.BtnSave);
            this.panel2.Controls.Add(this.BtnAdd);
            this.panel2.Controls.Add(this.BtnBack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 390);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(781, 62);
            this.panel2.TabIndex = 2;
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Location = new System.Drawing.Point(221, 13);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(103, 35);
            this.BtnExport.TabIndex = 7;
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
            this.BtnSave.Location = new System.Drawing.Point(6, 13);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 35);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAdd.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Appearance.Options.UseFont = true;
            this.BtnAdd.Appearance.Options.UseForeColor = true;
            this.BtnAdd.Location = new System.Drawing.Point(113, 13);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(103, 35);
            this.BtnAdd.TabIndex = 6;
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
            this.BtnBack.Location = new System.Drawing.Point(329, 13);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 8;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // MainGrid
            // 
            this.MainGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 64);
            this.MainGrid.MainView = this.GridData;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.TxtCut,
            this.TxtPol,
            this.TxtSym,
            this.TxtBonusPer,
            this.RepCmbLabourType});
            this.MainGrid.Size = new System.Drawing.Size(781, 326);
            this.MainGrid.TabIndex = 3;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridData});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedItemToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(225, 26);
            // 
            // deleteSelectedItemToolStripMenuItem
            // 
            this.deleteSelectedItemToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteSelectedItemToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.deleteSelectedItemToolStripMenuItem.Name = "deleteSelectedItemToolStripMenuItem";
            this.deleteSelectedItemToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.deleteSelectedItemToolStripMenuItem.Text = "DeleteSelected Item";
            this.deleteSelectedItemToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedItemToolStripMenuItem_Click);
            // 
            // GridData
            // 
            this.GridData.Appearance.FilterPanel.Options.UseTextOptions = true;
            this.GridData.Appearance.FilterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridData.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.GridData.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.GridData.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GridData.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GridData.Appearance.FooterPanel.Options.UseFont = true;
            this.GridData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.GridData.Appearance.HeaderPanel.Options.UseFont = true;
            this.GridData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridData.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GridData.Appearance.HorzLine.Options.UseBackColor = true;
            this.GridData.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GridData.Appearance.Row.Options.UseFont = true;
            this.GridData.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GridData.Appearance.VertLine.Options.UseBackColor = true;
            this.GridData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn6,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.GridData.FooterPanelHeight = 25;
            this.GridData.GridControl = this.MainGrid;
            this.GridData.Name = "GridData";
            this.GridData.OptionsFilter.AllowFilterEditor = false;
            this.GridData.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridData.OptionsPrint.ExpandAllGroups = false;
            this.GridData.OptionsView.ColumnAutoWidth = false;
            this.GridData.OptionsView.FooterLocation = GridViewFooter.FooterPosition.Bottom;
            this.GridData.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GridData.OptionsView.ShowAutoFilterRow = true;
            this.GridData.OptionsView.ShowFooter = true;
            this.GridData.OptionsView.ShowGroupPanel = false;
            this.GridData.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GridData_RowCellClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Cut";
            this.gridColumn1.ColumnEdit = this.TxtCut;
            this.gridColumn1.FieldName = "CUTNAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 101;
            // 
            // TxtCut
            // 
            this.TxtCut.AutoHeight = false;
            this.TxtCut.Name = "TxtCut";
            this.TxtCut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCut_KeyPress);
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Cut_ID";
            this.gridColumn6.FieldName = "CUT_ID";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Pol";
            this.gridColumn2.ColumnEdit = this.TxtPol;
            this.gridColumn2.FieldName = "POLNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 94;
            // 
            // TxtPol
            // 
            this.TxtPol.AutoHeight = false;
            this.TxtPol.Name = "TxtPol";
            this.TxtPol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPol_KeyPress);
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Rate";
            this.gridColumn3.FieldName = "RATE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "BonusPer";
            this.gridColumn4.ColumnEdit = this.TxtBonusPer;
            this.gridColumn4.FieldName = "BONUSPER";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 93;
            // 
            // TxtBonusPer
            // 
            this.TxtBonusPer.AutoHeight = false;
            this.TxtBonusPer.Name = "TxtBonusPer";
            this.TxtBonusPer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBonusPer_KeyDown);
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Sym";
            this.gridColumn5.ColumnEdit = this.TxtSym;
            this.gridColumn5.FieldName = "SYMNAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 102;
            // 
            // TxtSym
            // 
            this.TxtSym.AutoHeight = false;
            this.TxtSym.Name = "TxtSym";
            this.TxtSym.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSym_KeyPress);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Pol_ID";
            this.gridColumn7.FieldName = "POL_ID";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Sym_ID";
            this.gridColumn8.FieldName = "SYM_ID";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Shape_ID";
            this.gridColumn9.FieldName = "SHAPE_ID";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Size_ID";
            this.gridColumn10.FieldName = "SIZE_ID";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "Type";
            this.gridColumn11.ColumnEdit = this.RepCmbLabourType;
            this.gridColumn11.FieldName = "LABOURTYPE";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            // 
            // RepCmbLabourType
            // 
            this.RepCmbLabourType.AutoHeight = false;
            this.RepCmbLabourType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepCmbLabourType.Items.AddRange(new object[] {
            "CARAT",
            "PCS"});
            this.RepCmbLabourType.Name = "RepCmbLabourType";
            this.RepCmbLabourType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.RepCmbLabourType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RepCmbLabourType_KeyDown);
            // 
            // FrmDollarLabourUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 452);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmDollarLabourUpload";
            this.Tag = "";
            this.Text = "DOLLAR LABOUR UPLOAD";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtBonusPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtSym)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCmbLabourType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private GridViewFooter.GridControlFooterOnTop MainGrid;
        private GridViewFooter.GridViewFooterOnTop GridData;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label SIZE;
        private System.Windows.Forms.Label SHAPE;
        private System.Windows.Forms.ComboBox CmbSize;
        private System.Windows.Forms.ComboBox CmbShape;
        private System.Windows.Forms.Button btnshow;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private AxonContLib.cComboBox CmbMonth;
        private AxonContLib.cTextBox txtYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit TxtCut;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit TxtPol;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit TxtSym;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedItemToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit TxtBonusPer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox RepCmbLabourType;
    }
}