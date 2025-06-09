namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmKapanDetailReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKapanDetailReport));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.BtnDirectPDFExport = new DevExpress.XtraEditors.SimpleButton();
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.RbtKapanDetailReport = new AxonContLib.cRadioButton(this.components);
            this.RbtKapanFinalSummaryReport = new AxonContLib.cRadioButton(this.components);
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtMultiMainManager = new AxonContLib.cTextBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.ChkCmbPacketGroup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.chkCmbPacketCat = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbPacketGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCmbPacketCat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSearch.ImageOptions.SvgImage")));
            this.BtnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSearch.Location = new System.Drawing.Point(430, 65);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(142, 32);
            this.BtnSearch.TabIndex = 0;
            this.BtnSearch.Text = "Generate Report";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExit.ImageOptions.SvgImage")));
            this.BtnExit.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExit.Location = new System.Drawing.Point(728, 65);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 32);
            this.BtnExit.TabIndex = 153;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseForeColor = true;
            serializableAppearanceObject1.Options.UseTextOptions = true;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject2.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject2.Options.UseFont = true;
            serializableAppearanceObject2.Options.UseForeColor = true;
            serializableAppearanceObject2.Options.UseTextOptions = true;
            serializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject3.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject3.Options.UseFont = true;
            serializableAppearanceObject3.Options.UseForeColor = true;
            serializableAppearanceObject3.Options.UseTextOptions = true;
            serializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject4.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject4.Options.UseFont = true;
            serializableAppearanceObject4.Options.UseForeColor = true;
            serializableAppearanceObject4.Options.UseTextOptions = true;
            serializableAppearanceObject4.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(21, 49);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(47, 13);
            this.cLabel2.TabIndex = 0;
            this.cLabel2.Text = "Kapan";
            this.cLabel2.ToolTips = "";
            // 
            // BtnDirectPDFExport
            // 
            this.BtnDirectPDFExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDirectPDFExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDirectPDFExport.Appearance.Options.UseFont = true;
            this.BtnDirectPDFExport.Appearance.Options.UseForeColor = true;
            this.BtnDirectPDFExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDirectPDFExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDirectPDFExport.ImageOptions.SvgImage")));
            this.BtnDirectPDFExport.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnDirectPDFExport.Location = new System.Drawing.Point(574, 65);
            this.BtnDirectPDFExport.Name = "BtnDirectPDFExport";
            this.BtnDirectPDFExport.Size = new System.Drawing.Size(152, 32);
            this.BtnDirectPDFExport.TabIndex = 0;
            this.BtnDirectPDFExport.Text = "Direct .Pdf Export";
            this.BtnDirectPDFExport.Click += new System.EventHandler(this.BtnDirectPDFExport_Click);
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(295, 130);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.ShowCheckBox = true;
            this.DTPToDate.Size = new System.Drawing.Size(129, 24);
            this.DTPToDate.TabIndex = 159;
            this.DTPToDate.ToolTips = "";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(101, 130);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.ShowCheckBox = true;
            this.DTPFromDate.Size = new System.Drawing.Size(129, 24);
            this.DTPFromDate.TabIndex = 160;
            this.DTPFromDate.ToolTips = "";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.Black;
            this.cLabel8.Location = new System.Drawing.Point(236, 136);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(53, 13);
            this.cLabel8.TabIndex = 157;
            this.cLabel8.Text = "ToDate";
            this.cLabel8.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(22, 136);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(75, 13);
            this.cLabel9.TabIndex = 158;
            this.cLabel9.Text = "From Date";
            this.cLabel9.ToolTips = "";
            // 
            // RbtKapanDetailReport
            // 
            this.RbtKapanDetailReport.AllowTabKeyOnEnter = false;
            this.RbtKapanDetailReport.AutoSize = true;
            this.RbtKapanDetailReport.Checked = true;
            this.RbtKapanDetailReport.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.RbtKapanDetailReport.ForeColor = System.Drawing.Color.Black;
            this.RbtKapanDetailReport.Location = new System.Drawing.Point(24, 16);
            this.RbtKapanDetailReport.Name = "RbtKapanDetailReport";
            this.RbtKapanDetailReport.Size = new System.Drawing.Size(181, 21);
            this.RbtKapanDetailReport.TabIndex = 162;
            this.RbtKapanDetailReport.TabStop = true;
            this.RbtKapanDetailReport.Tag = "DEPTSTOCK";
            this.RbtKapanDetailReport.Text = "Kapan Detail Report";
            this.RbtKapanDetailReport.ToolTips = "Display Department (MY Stock + Other Stock)";
            this.RbtKapanDetailReport.UseVisualStyleBackColor = true;
            // 
            // RbtKapanFinalSummaryReport
            // 
            this.RbtKapanFinalSummaryReport.AllowTabKeyOnEnter = false;
            this.RbtKapanFinalSummaryReport.AutoSize = true;
            this.RbtKapanFinalSummaryReport.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.RbtKapanFinalSummaryReport.ForeColor = System.Drawing.Color.Black;
            this.RbtKapanFinalSummaryReport.Location = new System.Drawing.Point(227, 16);
            this.RbtKapanFinalSummaryReport.Name = "RbtKapanFinalSummaryReport";
            this.RbtKapanFinalSummaryReport.Size = new System.Drawing.Size(252, 21);
            this.RbtKapanFinalSummaryReport.TabIndex = 163;
            this.RbtKapanFinalSummaryReport.Tag = "DEPTSTOCK";
            this.RbtKapanFinalSummaryReport.Text = "Kapan Final Summary Report";
            this.RbtKapanFinalSummaryReport.ToolTips = "Display Department (MY Stock + Other Stock)";
            this.RbtKapanFinalSummaryReport.UseVisualStyleBackColor = true;
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(25, 68);
            this.CmbKapan.Name = "CmbKapan";
            this.CmbKapan.Properties.AllowMultiSelect = true;
            this.CmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbKapan.Properties.Appearance.Options.UseFont = true;
            this.CmbKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbKapan.Properties.DropDownRows = 20;
            this.CmbKapan.Properties.IncrementalSearch = true;
            this.CmbKapan.Size = new System.Drawing.Size(399, 22);
            this.CmbKapan.TabIndex = 164;
            // 
            // txtMultiMainManager
            // 
            this.txtMultiMainManager.ActivationColor = true;
            this.txtMultiMainManager.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtMultiMainManager.AllowTabKeyOnEnter = false;
            this.txtMultiMainManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMultiMainManager.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMultiMainManager.Format = "";
            this.txtMultiMainManager.IsComplusory = false;
            this.txtMultiMainManager.Location = new System.Drawing.Point(126, 100);
            this.txtMultiMainManager.Name = "txtMultiMainManager";
            this.txtMultiMainManager.SelectAllTextOnFocus = true;
            this.txtMultiMainManager.Size = new System.Drawing.Size(298, 23);
            this.txtMultiMainManager.TabIndex = 166;
            this.txtMultiMainManager.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMultiMainManager.ToolTips = "";
            this.txtMultiMainManager.WaterMarkText = null;
            this.txtMultiMainManager.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMultiMainManager_KeyPress);
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(23, 104);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(97, 13);
            this.cLabel4.TabIndex = 165;
            this.cLabel4.Text = "Main Manager";
            this.cLabel4.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(445, 135);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(102, 13);
            this.cLabel6.TabIndex = 237;
            this.cLabel6.Text = "Packet Group :";
            this.cLabel6.ToolTips = "";
            // 
            // ChkCmbPacketGroup
            // 
            this.ChkCmbPacketGroup.Location = new System.Drawing.Point(548, 130);
            this.ChkCmbPacketGroup.Name = "ChkCmbPacketGroup";
            this.ChkCmbPacketGroup.Properties.AllowMultiSelect = true;
            this.ChkCmbPacketGroup.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkCmbPacketGroup.Properties.Appearance.Options.UseFont = true;
            this.ChkCmbPacketGroup.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkCmbPacketGroup.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ChkCmbPacketGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChkCmbPacketGroup.Properties.DropDownRows = 20;
            this.ChkCmbPacketGroup.Properties.IncrementalSearch = true;
            this.ChkCmbPacketGroup.Size = new System.Drawing.Size(210, 20);
            this.ChkCmbPacketGroup.TabIndex = 238;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(425, 105);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(122, 13);
            this.cLabel5.TabIndex = 235;
            this.cLabel5.Text = "Packet Category :";
            this.cLabel5.ToolTips = "";
            // 
            // chkCmbPacketCat
            // 
            this.chkCmbPacketCat.Location = new System.Drawing.Point(548, 100);
            this.chkCmbPacketCat.Name = "chkCmbPacketCat";
            this.chkCmbPacketCat.Properties.AllowMultiSelect = true;
            this.chkCmbPacketCat.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9F);
            this.chkCmbPacketCat.Properties.Appearance.Options.UseFont = true;
            this.chkCmbPacketCat.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.chkCmbPacketCat.Properties.AppearanceDropDown.Options.UseFont = true;
            this.chkCmbPacketCat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkCmbPacketCat.Properties.DropDownRows = 20;
            this.chkCmbPacketCat.Properties.IncrementalSearch = true;
            this.chkCmbPacketCat.Size = new System.Drawing.Size(210, 20);
            this.chkCmbPacketCat.TabIndex = 236;
            // 
            // FrmKapanDetailReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 195);
            this.Controls.Add(this.cLabel6);
            this.Controls.Add(this.ChkCmbPacketGroup);
            this.Controls.Add(this.cLabel5);
            this.Controls.Add(this.chkCmbPacketCat);
            this.Controls.Add(this.txtMultiMainManager);
            this.Controls.Add(this.cLabel4);
            this.Controls.Add(this.CmbKapan);
            this.Controls.Add(this.RbtKapanFinalSummaryReport);
            this.Controls.Add(this.RbtKapanDetailReport);
            this.Controls.Add(this.DTPToDate);
            this.Controls.Add(this.DTPFromDate);
            this.Controls.Add(this.cLabel8);
            this.Controls.Add(this.cLabel9);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.cLabel2);
            this.Controls.Add(this.BtnDirectPDFExport);
            this.Controls.Add(this.BtnSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmKapanDetailReport";
            this.Text = "FULL KAPAN DETAIL REPORT";
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCmbPacketGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCmbPacketCat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraEditors.SimpleButton BtnDirectPDFExport;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cRadioButton RbtKapanDetailReport;
        private AxonContLib.cRadioButton RbtKapanFinalSummaryReport;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private AxonContLib.cTextBox txtMultiMainManager;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cLabel cLabel6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ChkCmbPacketGroup;
        private AxonContLib.cLabel cLabel5;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkCmbPacketCat;


    }
}