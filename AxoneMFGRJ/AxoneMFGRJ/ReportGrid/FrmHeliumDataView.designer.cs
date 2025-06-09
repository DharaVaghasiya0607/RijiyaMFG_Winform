namespace AxoneMFGRJ.Masters
{
    partial class FrmHeliumDataView
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.PanelHeader = new AxonContLib.cPanel();
            this.cLabel13 = new AxonContLib.cLabel(this.components);
            this.BtnKapanLiveStockExcelExport = new DevExpress.XtraEditors.SimpleButton();
            this.txtKapan = new AxonContLib.cTextBox(this.components);
            this.txtEmployee = new AxonContLib.cTextBox(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.RbtALL = new AxonContLib.cRadioButton(this.components);
            this.DTPToDate = new AxonContLib.cDateTimePicker(this.components);
            this.RbtLast = new AxonContLib.cRadioButton(this.components);
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.txtTag = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.txtToPacketNo = new AxonContLib.cTextBox(this.components);
            this.cLabel7 = new AxonContLib.cLabel(this.components);
            this.txtFromPacketNo = new AxonContLib.cTextBox(this.components);
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.BandGeneral = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelHeader.Controls.Add(this.cLabel13);
            this.PanelHeader.Controls.Add(this.BtnKapanLiveStockExcelExport);
            this.PanelHeader.Controls.Add(this.txtKapan);
            this.PanelHeader.Controls.Add(this.txtEmployee);
            this.PanelHeader.Controls.Add(this.cLabel9);
            this.PanelHeader.Controls.Add(this.DTPFromDate);
            this.PanelHeader.Controls.Add(this.RbtALL);
            this.PanelHeader.Controls.Add(this.DTPToDate);
            this.PanelHeader.Controls.Add(this.RbtLast);
            this.PanelHeader.Controls.Add(this.BtnSearch);
            this.PanelHeader.Controls.Add(this.cLabel1);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.cLabel3);
            this.PanelHeader.Controls.Add(this.txtTag);
            this.PanelHeader.Controls.Add(this.cLabel2);
            this.PanelHeader.Controls.Add(this.txtToPacketNo);
            this.PanelHeader.Controls.Add(this.cLabel7);
            this.PanelHeader.Controls.Add(this.txtFromPacketNo);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1063, 79);
            this.PanelHeader.TabIndex = 0;
            // 
            // cLabel13
            // 
            this.cLabel13.AutoSize = true;
            this.cLabel13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel13.ForeColor = System.Drawing.Color.Black;
            this.cLabel13.Location = new System.Drawing.Point(7, 48);
            this.cLabel13.Name = "cLabel13";
            this.cLabel13.Size = new System.Drawing.Size(35, 13);
            this.cLabel13.TabIndex = 5;
            this.cLabel13.Text = "Emp";
            this.cLabel13.ToolTips = "";
            // 
            // BtnKapanLiveStockExcelExport
            // 
            this.BtnKapanLiveStockExcelExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnKapanLiveStockExcelExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnKapanLiveStockExcelExport.Appearance.Options.UseFont = true;
            this.BtnKapanLiveStockExcelExport.Appearance.Options.UseForeColor = true;
            this.BtnKapanLiveStockExcelExport.Location = new System.Drawing.Point(862, 38);
            this.BtnKapanLiveStockExcelExport.Name = "BtnKapanLiveStockExcelExport";
            this.BtnKapanLiveStockExcelExport.Size = new System.Drawing.Size(99, 32);
            this.BtnKapanLiveStockExcelExport.TabIndex = 153;
            this.BtnKapanLiveStockExcelExport.TabStop = false;
            this.BtnKapanLiveStockExcelExport.Text = "Excel Export";
            this.BtnKapanLiveStockExcelExport.Click += new System.EventHandler(this.BtnKapanLiveStockExcelExport_Click);
            // 
            // txtKapan
            // 
            this.txtKapan.ActivationColor = true;
            this.txtKapan.AllowTabKeyOnEnter = false;
            this.txtKapan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKapan.ComplusoryMsg = null;
            this.txtKapan.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtKapan.Format = "";
            this.txtKapan.IsComplusory = false;
            this.txtKapan.Location = new System.Drawing.Point(375, 43);
            this.txtKapan.Name = "txtKapan";
            this.txtKapan.RequiredChars = "";
            this.txtKapan.SelectAllTextOnFocus = true;
            this.txtKapan.ShowToolTipOnFocus = false;
            this.txtKapan.Size = new System.Drawing.Size(104, 22);
            this.txtKapan.TabIndex = 8;
            this.txtKapan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKapan.ToolTips = "";
            this.txtKapan.WaterMarkText = null;
            // 
            // txtEmployee
            // 
            this.txtEmployee.ActivationColor = true;
            this.txtEmployee.AllowTabKeyOnEnter = false;
            this.txtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployee.ComplusoryMsg = null;
            this.txtEmployee.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtEmployee.Format = "";
            this.txtEmployee.IsComplusory = false;
            this.txtEmployee.Location = new System.Drawing.Point(54, 43);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.RequiredChars = "";
            this.txtEmployee.SelectAllTextOnFocus = true;
            this.txtEmployee.ShowToolTipOnFocus = false;
            this.txtEmployee.Size = new System.Drawing.Size(263, 22);
            this.txtEmployee.TabIndex = 6;
            this.txtEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmployee.ToolTips = "";
            this.txtEmployee.WaterMarkText = null;
            this.txtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployee_KeyPress);
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(7, 16);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(37, 13);
            this.cLabel9.TabIndex = 0;
            this.cLabel9.Text = "Date";
            this.cLabel9.ToolTips = "";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFromDate.Location = new System.Drawing.Point(54, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.ShowCheckBox = true;
            this.DTPFromDate.Size = new System.Drawing.Size(128, 24);
            this.DTPFromDate.TabIndex = 1;
            this.DTPFromDate.ToolTips = "";
            // 
            // RbtALL
            // 
            this.RbtALL.AutoSize = true;
            this.RbtALL.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtALL.ForeColor = System.Drawing.Color.Black;
            this.RbtALL.Location = new System.Drawing.Point(388, 14);
            this.RbtALL.Name = "RbtALL";
            this.RbtALL.Size = new System.Drawing.Size(48, 17);
            this.RbtALL.TabIndex = 4;
            this.RbtALL.Text = "ALL";
            this.RbtALL.ToolTips = "";
            this.RbtALL.UseVisualStyleBackColor = true;
            // 
            // DTPToDate
            // 
            this.DTPToDate.AllowTabKeyOnEnter = false;
            this.DTPToDate.Checked = false;
            this.DTPToDate.CustomFormat = "dd/MM/yyyy";
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPToDate.ForeColor = System.Drawing.Color.Black;
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPToDate.Location = new System.Drawing.Point(188, 10);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.ShowCheckBox = true;
            this.DTPToDate.Size = new System.Drawing.Size(128, 24);
            this.DTPToDate.TabIndex = 2;
            this.DTPToDate.ToolTips = "";
            // 
            // RbtLast
            // 
            this.RbtLast.AutoSize = true;
            this.RbtLast.Checked = true;
            this.RbtLast.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RbtLast.ForeColor = System.Drawing.Color.Black;
            this.RbtLast.Location = new System.Drawing.Point(325, 14);
            this.RbtLast.Name = "RbtLast";
            this.RbtLast.Size = new System.Drawing.Size(57, 17);
            this.RbtLast.TabIndex = 3;
            this.RbtLast.TabStop = true;
            this.RbtLast.Text = "LAST";
            this.RbtLast.ToolTips = "";
            this.RbtLast.UseVisualStyleBackColor = true;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.Location = new System.Drawing.Point(781, 38);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 32);
            this.BtnSearch.TabIndex = 15;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(322, 48);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(47, 13);
            this.cLabel1.TabIndex = 7;
            this.cLabel1.Text = "Kapan";
            this.cLabel1.ToolTips = "";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Location = new System.Drawing.Point(967, 38);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(55, 32);
            this.BtnExit.TabIndex = 153;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(485, 48);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(28, 13);
            this.cLabel3.TabIndex = 9;
            this.cLabel3.Text = "Pkt";
            this.cLabel3.ToolTips = "";
            // 
            // txtTag
            // 
            this.txtTag.ActivationColor = true;
            this.txtTag.AllowTabKeyOnEnter = false;
            this.txtTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTag.ComplusoryMsg = null;
            this.txtTag.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTag.Format = "";
            this.txtTag.IsComplusory = false;
            this.txtTag.Location = new System.Drawing.Point(713, 43);
            this.txtTag.Name = "txtTag";
            this.txtTag.RequiredChars = "";
            this.txtTag.SelectAllTextOnFocus = true;
            this.txtTag.ShowToolTipOnFocus = false;
            this.txtTag.Size = new System.Drawing.Size(45, 22);
            this.txtTag.TabIndex = 14;
            this.txtTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTag.ToolTips = "";
            this.txtTag.WaterMarkText = null;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(587, 48);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(23, 13);
            this.cLabel2.TabIndex = 11;
            this.cLabel2.Text = "To";
            this.cLabel2.ToolTips = "";
            // 
            // txtToPacketNo
            // 
            this.txtToPacketNo.ActivationColor = true;
            this.txtToPacketNo.AllowTabKeyOnEnter = false;
            this.txtToPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToPacketNo.ComplusoryMsg = null;
            this.txtToPacketNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPacketNo.Format = "######";
            this.txtToPacketNo.IsComplusory = false;
            this.txtToPacketNo.Location = new System.Drawing.Point(612, 43);
            this.txtToPacketNo.Name = "txtToPacketNo";
            this.txtToPacketNo.RequiredChars = "0123456789.";
            this.txtToPacketNo.SelectAllTextOnFocus = true;
            this.txtToPacketNo.ShowToolTipOnFocus = false;
            this.txtToPacketNo.Size = new System.Drawing.Size(63, 22);
            this.txtToPacketNo.TabIndex = 12;
            this.txtToPacketNo.Text = "0";
            this.txtToPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtToPacketNo.ToolTips = "";
            this.txtToPacketNo.WaterMarkText = null;
            // 
            // cLabel7
            // 
            this.cLabel7.AutoSize = true;
            this.cLabel7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel7.ForeColor = System.Drawing.Color.Black;
            this.cLabel7.Location = new System.Drawing.Point(681, 48);
            this.cLabel7.Name = "cLabel7";
            this.cLabel7.Size = new System.Drawing.Size(31, 13);
            this.cLabel7.TabIndex = 13;
            this.cLabel7.Text = "Tag";
            this.cLabel7.ToolTips = "";
            // 
            // txtFromPacketNo
            // 
            this.txtFromPacketNo.ActivationColor = true;
            this.txtFromPacketNo.AllowTabKeyOnEnter = false;
            this.txtFromPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromPacketNo.ComplusoryMsg = null;
            this.txtFromPacketNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromPacketNo.Format = "######";
            this.txtFromPacketNo.IsComplusory = false;
            this.txtFromPacketNo.Location = new System.Drawing.Point(518, 43);
            this.txtFromPacketNo.Name = "txtFromPacketNo";
            this.txtFromPacketNo.RequiredChars = "0123456789.";
            this.txtFromPacketNo.SelectAllTextOnFocus = true;
            this.txtFromPacketNo.ShowToolTipOnFocus = false;
            this.txtFromPacketNo.Size = new System.Drawing.Size(63, 22);
            this.txtFromPacketNo.TabIndex = 10;
            this.txtFromPacketNo.Text = "0";
            this.txtFromPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFromPacketNo.ToolTips = "";
            this.txtFromPacketNo.WaterMarkText = null;
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject3.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject3.Options.UseFont = true;
            serializableAppearanceObject3.Options.UseForeColor = true;
            serializableAppearanceObject3.Options.UseTextOptions = true;
            serializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 79);
            this.MainGrid.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1063, 394);
            this.MainGrid.TabIndex = 19;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            this.MainGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.MainGrid_Paint);
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.BandPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.BandPanel.Options.UseFont = true;
            this.GrdDet.Appearance.BandPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.BandPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.BandPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.BandPanelRowHeight = 25;
            this.GrdDet.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.BandGeneral});
            this.GrdDet.ColumnPanelRowHeight = 40;
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDet.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GrdDet_RowStyle);
            this.GrdDet.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDet_CustomColumnDisplayText);
            // 
            // BandGeneral
            // 
            this.BandGeneral.Caption = "General";
            this.BandGeneral.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.BandGeneral.Name = "BandGeneral";
            this.BandGeneral.VisibleIndex = 0;
            this.BandGeneral.Width = 71;
            // 
            // FrmHeliumDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 473);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.PanelHeader);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmHeliumDataView";
            this.Text = "HELIUM DATAVIEW";
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel PanelHeader;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnKapanLiveStockExcelExport;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GrdDet;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BandGeneral;
        private AxonContLib.cDateTimePicker DTPToDate;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel9;
        private AxonContLib.cTextBox txtEmployee;
        private AxonContLib.cLabel cLabel13;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cLabel cLabel7;
        private AxonContLib.cTextBox txtFromPacketNo;
        private AxonContLib.cTextBox txtToPacketNo;
        private AxonContLib.cTextBox txtTag;
        private AxonContLib.cRadioButton RbtALL;
        private AxonContLib.cRadioButton RbtLast;
        private AxonContLib.cTextBox txtKapan;
    }
}