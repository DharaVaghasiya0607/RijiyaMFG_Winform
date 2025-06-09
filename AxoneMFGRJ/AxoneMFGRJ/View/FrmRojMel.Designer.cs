namespace AxoneMFGRJ.Account
{
    partial class FrmRojMel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRojMel));
            this.xtraScrollableControl2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.ChkCmbParty = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl3 = new AxonContLib.cLabel(this.components);
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.PanelClient = new AxonContLib.cPanel();
            this.BtnForward = new System.Windows.Forms.Button();
            this.BtnBackWord = new System.Windows.Forms.Button();
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.labelControl1 = new AxonContLib.cLabel(this.components);
            this.lblReceiveDate = new AxonContLib.cLabel(this.components);
            this.panel1 = new AxonContLib.cPanel();
            this.BtnExcelExport = new System.Windows.Forms.Button();
            this.BtnShow = new System.Windows.Forms.Button();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnGPrint = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xtraScrollableControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.PanelClient.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraScrollableControl2
            // 
            this.xtraScrollableControl2.Controls.Add(this.groupControl1);
            this.xtraScrollableControl2.Controls.Add(this.PanelClient);
            this.xtraScrollableControl2.Controls.Add(this.label3);
            this.xtraScrollableControl2.Controls.Add(this.label4);
            this.xtraScrollableControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl2.Name = "xtraScrollableControl2";
            this.xtraScrollableControl2.Size = new System.Drawing.Size(1008, 412);
            this.xtraScrollableControl2.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.ChkCmbParty);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.MainGrid);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 49);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1008, 363);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Roj Mel";
            // 
            // ChkCmbParty
            // 
            this.ChkCmbParty.Location = new System.Drawing.Point(233, 159);
            this.ChkCmbParty.Name = "ChkCmbParty";
            this.ChkCmbParty.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 13F);
            this.ChkCmbParty.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbParty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbParty.Properties.IncrementalSearch = true;
            this.ChkCmbParty.Size = new System.Drawing.Size(453, 28);
            this.ChkCmbParty.TabIndex = 9;
            this.ChkCmbParty.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Font = new System.Drawing.Font("Cambria", 13F);
            this.labelControl3.ForeColor = System.Drawing.Color.Navy;
            this.labelControl3.Location = new System.Drawing.Point(198, 162);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(29, 21);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "A/C";
            this.labelControl3.ToolTips = "";
            this.labelControl3.Visible = false;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(2, 22);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1004, 339);
            this.MainGrid.TabIndex = 0;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.BandPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.BandPanel.Options.UseFont = true;
            this.GrdDet.Appearance.BandPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.BandPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.BandPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Cambria", 11F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand3,
            this.gridBand2});
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsCustomization.AllowFilter = false;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GrdDet_RowCellStyle);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "Income (આવક)";
            this.gridBand1.Columns.Add(this.gridColumn1);
            this.gridBand1.Columns.Add(this.gridColumn2);
            this.gridBand1.Columns.Add(this.gridColumn3);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 530;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "INCOMELEDGER_ID";
            this.gridColumn1.FieldName = "INCOMELEDGER_ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Account";
            this.gridColumn2.FieldName = "INCOMELEDGERNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.Width = 350;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn3.Caption = "Amount";
            this.gridColumn3.FieldName = "INCOMEAMOUNT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.Width = 180;
            // 
            // gridBand3
            // 
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 1;
            this.gridBand3.Width = 10;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "Expense (જાવક)";
            this.gridBand2.Columns.Add(this.gridColumn4);
            this.gridBand2.Columns.Add(this.gridColumn5);
            this.gridBand2.Columns.Add(this.gridColumn6);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 2;
            this.gridBand2.Width = 530;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "EXPENSELEDGER_ID";
            this.gridColumn4.FieldName = "EXPENSELEDGER_ID";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Account";
            this.gridColumn5.FieldName = "EXPENSELEDGERNAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.Width = 350;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn6.Caption = "Amount";
            this.gridColumn6.FieldName = "EXPENSEAMOUNT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.Width = 180;
            // 
            // PanelClient
            // 
            this.PanelClient.Controls.Add(this.BtnForward);
            this.PanelClient.Controls.Add(this.BtnBackWord);
            this.PanelClient.Controls.Add(this.DTPToDate);
            this.PanelClient.Controls.Add(this.DTPFromDate);
            this.PanelClient.Controls.Add(this.labelControl1);
            this.PanelClient.Controls.Add(this.lblReceiveDate);
            this.PanelClient.Controls.Add(this.panel1);
            this.PanelClient.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelClient.Location = new System.Drawing.Point(0, 0);
            this.PanelClient.Name = "PanelClient";
            this.PanelClient.Size = new System.Drawing.Size(1008, 49);
            this.PanelClient.TabIndex = 0;
            // 
            // BtnForward
            // 
            this.BtnForward.BackColor = System.Drawing.Color.Teal;
            this.BtnForward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnForward.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnForward.ForeColor = System.Drawing.Color.White;
            this.BtnForward.Location = new System.Drawing.Point(392, 6);
            this.BtnForward.Name = "BtnForward";
            this.BtnForward.Size = new System.Drawing.Size(39, 35);
            this.BtnForward.TabIndex = 33;
            this.BtnForward.TabStop = false;
            this.BtnForward.Text = ">";
            this.BtnForward.UseVisualStyleBackColor = false;
            this.BtnForward.Click += new System.EventHandler(this.BtnForward_Click);
            // 
            // BtnBackWord
            // 
            this.BtnBackWord.BackColor = System.Drawing.Color.Teal;
            this.BtnBackWord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBackWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBackWord.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackWord.ForeColor = System.Drawing.Color.White;
            this.BtnBackWord.Location = new System.Drawing.Point(351, 6);
            this.BtnBackWord.Name = "BtnBackWord";
            this.BtnBackWord.Size = new System.Drawing.Size(39, 35);
            this.BtnBackWord.TabIndex = 33;
            this.BtnBackWord.TabStop = false;
            this.BtnBackWord.Text = "<";
            this.BtnBackWord.UseVisualStyleBackColor = false;
            this.BtnBackWord.Click += new System.EventHandler(this.BtnBackWord_Click);
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(215, 10);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(130, 23);
            this.DTPToDate.TabIndex = 3;
            this.DTPToDate.ToolTips = "";
            this.DTPToDate.Value = new System.DateTime(2016, 6, 4, 19, 23, 5, 0);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(57, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(130, 23);
            this.DTPFromDate.TabIndex = 1;
            this.DTPFromDate.ToolTips = "";
            this.DTPFromDate.Value = new System.DateTime(2016, 6, 4, 19, 23, 5, 0);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.ForeColor = System.Drawing.Color.Navy;
            this.labelControl1.Location = new System.Drawing.Point(190, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(25, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "To";
            this.labelControl1.ToolTips = "";
            // 
            // lblReceiveDate
            // 
            this.lblReceiveDate.AutoSize = true;
            this.lblReceiveDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiveDate.ForeColor = System.Drawing.Color.Navy;
            this.lblReceiveDate.Location = new System.Drawing.Point(12, 13);
            this.lblReceiveDate.Name = "lblReceiveDate";
            this.lblReceiveDate.Size = new System.Drawing.Size(40, 16);
            this.lblReceiveDate.TabIndex = 0;
            this.lblReceiveDate.Text = "From";
            this.lblReceiveDate.ToolTips = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnExcelExport);
            this.panel1.Controls.Add(this.BtnShow);
            this.panel1.Controls.Add(this.BtnBack);
            this.panel1.Controls.Add(this.BtnGPrint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(499, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 49);
            this.panel1.TabIndex = 4;
            // 
            // BtnExcelExport
            // 
            this.BtnExcelExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.BtnExcelExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExcelExport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExcelExport.ForeColor = System.Drawing.Color.White;
            this.BtnExcelExport.Location = new System.Drawing.Point(350, 7);
            this.BtnExcelExport.Name = "BtnExcelExport";
            this.BtnExcelExport.Size = new System.Drawing.Size(73, 35);
            this.BtnExcelExport.TabIndex = 37;
            this.BtnExcelExport.TabStop = false;
            this.BtnExcelExport.Text = "&Excel";
            this.BtnExcelExport.UseVisualStyleBackColor = false;
            this.BtnExcelExport.Click += new System.EventHandler(this.BtnExcelExport_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.BtnShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnShow.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.ForeColor = System.Drawing.Color.White;
            this.BtnShow.Location = new System.Drawing.Point(197, 7);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(73, 35);
            this.BtnShow.TabIndex = 36;
            this.BtnShow.Text = "&Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(54)))), ((int)(((byte)(16)))));
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.Color.White;
            this.BtnBack.Location = new System.Drawing.Point(427, 7);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(73, 35);
            this.BtnBack.TabIndex = 39;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnGPrint
            // 
            this.BtnGPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.BtnGPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGPrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGPrint.ForeColor = System.Drawing.Color.White;
            this.BtnGPrint.Location = new System.Drawing.Point(274, 7);
            this.BtnGPrint.Name = "BtnGPrint";
            this.BtnGPrint.Size = new System.Drawing.Size(73, 35);
            this.BtnGPrint.TabIndex = 38;
            this.BtnGPrint.TabStop = false;
            this.BtnGPrint.Text = "&GPrint";
            this.BtnGPrint.UseVisualStyleBackColor = false;
            this.BtnGPrint.Click += new System.EventHandler(this.BtnGPrint_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(-12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(-12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "*";
            // 
            // FrmRojMel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.xtraScrollableControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRojMel";
            this.Text = "ROJ MEL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRojMel_FormClosing);
            this.xtraScrollableControl2.ResumeLayout(false);
            this.xtraScrollableControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.PanelClient.ResumeLayout(false);
            this.PanelClient.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private AxonContLib.cPanel PanelClient;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn6;
        private AxonContLib.cLabel labelControl1;
        private AxonContLib.cLabel lblReceiveDate;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbParty;
        private AxonContLib.cLabel labelControl3;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cDateTimePicker DTPToDate;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Button BtnGPrint;
        private System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.Button BtnExcelExport;
        private System.Windows.Forms.Button BtnBackWord;
        private System.Windows.Forms.Button BtnForward;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;

    }
}