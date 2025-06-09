namespace AxoneMFGRJ.Utility
{
    partial class FrmClientTicket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientTicket));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DtpTicketDate = new AxonContLib.cDateTimePicker(this.components);
            this.txtTicketNo = new AxonContLib.cTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGeneratedBy = new AxonContLib.cTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTicketPriority = new AxonContLib.cComboBox(this.components);
            this.cmbTIcketStatus = new AxonContLib.cComboBox(this.components);
            this.txtTicketDetail = new AxonContLib.cTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.label12 = new System.Windows.Forms.Label();
            this.CmbSearchStatus = new AxonContLib.cComboBox(this.components);
            this.DtpSearchToDate = new AxonContLib.cDateTimePicker(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.DtpSearchFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.txtSearchTicketNo = new AxonContLib.cTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(925, 474);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Size = new System.Drawing.Size(925, 474);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.txtGeneratedBy);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.cmbTicketPriority);
            this.panel4.Controls.Add(this.cmbTIcketStatus);
            this.panel4.Controls.Add(this.txtTicketDetail);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(925, 188);
            this.panel4.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.BtnDelete);
            this.panel3.Controls.Add(this.BtnSave);
            this.panel3.Controls.Add(this.BtnClear);
            this.panel3.Controls.Add(this.BtnExit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 147);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(925, 41);
            this.panel3.TabIndex = 23;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDelete.ImageOptions.SvgImage")));
            this.BtnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnDelete.Location = new System.Drawing.Point(201, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(91, 35);
            this.BtnDelete.TabIndex = 7;
            this.BtnDelete.TabStop = false;
            this.BtnDelete.Text = "&Delete";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSave.ImageOptions.SvgImage")));
            this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSave.Location = new System.Drawing.Point(10, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(91, 35);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseForeColor = true;
            this.BtnClear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnClear.ImageOptions.SvgImage")));
            this.BtnClear.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnClear.Location = new System.Drawing.Point(105, 4);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(91, 35);
            this.BtnClear.TabIndex = 6;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "&Clear";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExit.ImageOptions.SvgImage")));
            this.BtnExit.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExit.Location = new System.Drawing.Point(297, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(91, 35);
            this.BtnExit.TabIndex = 8;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "E&xit";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DtpTicketDate);
            this.panel2.Controls.Add(this.txtTicketNo);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(925, 36);
            this.panel2.TabIndex = 22;
            // 
            // DtpTicketDate
            // 
            this.DtpTicketDate.AllowTabKeyOnEnter = true;
            this.DtpTicketDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpTicketDate.ForeColor = System.Drawing.Color.Black;
            this.DtpTicketDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTicketDate.Location = new System.Drawing.Point(317, 9);
            this.DtpTicketDate.Name = "DtpTicketDate";
            this.DtpTicketDate.ShowUpDown = true;
            this.DtpTicketDate.Size = new System.Drawing.Size(106, 21);
            this.DtpTicketDate.TabIndex = 6;
            this.DtpTicketDate.ToolTips = "";
            // 
            // txtTicketNo
            // 
            this.txtTicketNo.ActivationColor = false;
            this.txtTicketNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtTicketNo.AllowTabKeyOnEnter = true;
            this.txtTicketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTicketNo.Format = "";
            this.txtTicketNo.IsComplusory = false;
            this.txtTicketNo.Location = new System.Drawing.Point(94, 9);
            this.txtTicketNo.Name = "txtTicketNo";
            this.txtTicketNo.SelectAllTextOnFocus = true;
            this.txtTicketNo.Size = new System.Drawing.Size(162, 21);
            this.txtTicketNo.TabIndex = 3;
            this.txtTicketNo.ToolTips = "";
            this.txtTicketNo.WaterMarkText = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label2.Location = new System.Drawing.Point(263, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TicketNo";
            // 
            // txtGeneratedBy
            // 
            this.txtGeneratedBy.ActivationColor = false;
            this.txtGeneratedBy.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtGeneratedBy.AllowTabKeyOnEnter = true;
            this.txtGeneratedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGeneratedBy.Format = "";
            this.txtGeneratedBy.IsComplusory = false;
            this.txtGeneratedBy.Location = new System.Drawing.Point(93, 44);
            this.txtGeneratedBy.Name = "txtGeneratedBy";
            this.txtGeneratedBy.SelectAllTextOnFocus = true;
            this.txtGeneratedBy.Size = new System.Drawing.Size(330, 21);
            this.txtGeneratedBy.TabIndex = 15;
            this.txtGeneratedBy.ToolTips = "";
            this.txtGeneratedBy.WaterMarkText = null;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label6.Location = new System.Drawing.Point(5, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Generated By";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label5.Location = new System.Drawing.Point(641, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Priority";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label4.Location = new System.Drawing.Point(429, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Status";
            // 
            // cmbTicketPriority
            // 
            this.cmbTicketPriority.AllowTabKeyOnEnter = true;
            this.cmbTicketPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTicketPriority.Font = new System.Drawing.Font("Verdana", 10F);
            this.cmbTicketPriority.ForeColor = System.Drawing.Color.Black;
            this.cmbTicketPriority.FormattingEnabled = true;
            this.cmbTicketPriority.Items.AddRange(new object[] {
            "H",
            "M",
            "L"});
            this.cmbTicketPriority.Location = new System.Drawing.Point(695, 44);
            this.cmbTicketPriority.Name = "cmbTicketPriority";
            this.cmbTicketPriority.Size = new System.Drawing.Size(106, 24);
            this.cmbTicketPriority.TabIndex = 18;
            this.cmbTicketPriority.ToolTips = "";
            // 
            // cmbTIcketStatus
            // 
            this.cmbTIcketStatus.AllowTabKeyOnEnter = true;
            this.cmbTIcketStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTIcketStatus.Font = new System.Drawing.Font("Verdana", 10F);
            this.cmbTIcketStatus.ForeColor = System.Drawing.Color.Black;
            this.cmbTIcketStatus.FormattingEnabled = true;
            this.cmbTIcketStatus.Items.AddRange(new object[] {
            "Pending",
            "Need To Test",
            "Devloped",
            "Done"});
            this.cmbTIcketStatus.Location = new System.Drawing.Point(474, 44);
            this.cmbTIcketStatus.Name = "cmbTIcketStatus";
            this.cmbTIcketStatus.Size = new System.Drawing.Size(163, 24);
            this.cmbTIcketStatus.TabIndex = 17;
            this.cmbTIcketStatus.ToolTips = "";
            // 
            // txtTicketDetail
            // 
            this.txtTicketDetail.ActivationColor = false;
            this.txtTicketDetail.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtTicketDetail.AllowTabKeyOnEnter = true;
            this.txtTicketDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTicketDetail.Format = "";
            this.txtTicketDetail.IsComplusory = false;
            this.txtTicketDetail.Location = new System.Drawing.Point(94, 72);
            this.txtTicketDetail.Multiline = true;
            this.txtTicketDetail.Name = "txtTicketDetail";
            this.txtTicketDetail.SelectAllTextOnFocus = true;
            this.txtTicketDetail.Size = new System.Drawing.Size(543, 72);
            this.txtTicketDetail.TabIndex = 16;
            this.txtTicketDetail.ToolTips = "";
            this.txtTicketDetail.WaterMarkText = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label3.Location = new System.Drawing.Point(5, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Ticket Detail";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.MainGrid);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(925, 282);
            this.panel6.TabIndex = 0;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 38);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(925, 244);
            this.MainGrid.TabIndex = 26;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
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
            this.gridColumn6,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsCustomization.AllowFilter = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "ID";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Width = 174;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Ticket";
            this.gridColumn1.FieldName = "TICKETNOSTR";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 79;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Ticket Generated By";
            this.gridColumn6.FieldName = "TICKETGENERATEDBY";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 144;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Ticket Date";
            this.gridColumn2.FieldName = "TICKETDATE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 126;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Detail";
            this.gridColumn3.FieldName = "TASKDETAIL";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 272;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Priority";
            this.gridColumn4.FieldName = "PRIORITY";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Status";
            this.gridColumn5.FieldName = "TICKETSTATUS";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel7.Controls.Add(this.BtnShow);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.CmbSearchStatus);
            this.panel7.Controls.Add(this.DtpSearchToDate);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.DtpSearchFromDate);
            this.panel7.Controls.Add(this.txtSearchTicketNo);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(925, 38);
            this.panel7.TabIndex = 25;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.Appearance.Options.UseForeColor = true;
            this.BtnShow.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnShow.ImageOptions.SvgImage")));
            this.BtnShow.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnShow.Location = new System.Drawing.Point(731, 1);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(125, 31);
            this.BtnShow.TabIndex = 9;
            this.BtnShow.Text = "Show";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label12.Location = new System.Drawing.Point(517, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Status";
            // 
            // CmbSearchStatus
            // 
            this.CmbSearchStatus.AllowTabKeyOnEnter = true;
            this.CmbSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSearchStatus.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbSearchStatus.ForeColor = System.Drawing.Color.Black;
            this.CmbSearchStatus.FormattingEnabled = true;
            this.CmbSearchStatus.Items.AddRange(new object[] {
            "Pending",
            "Need To Test",
            "Devloped",
            "Done"});
            this.CmbSearchStatus.Location = new System.Drawing.Point(562, 6);
            this.CmbSearchStatus.Name = "CmbSearchStatus";
            this.CmbSearchStatus.Size = new System.Drawing.Size(163, 24);
            this.CmbSearchStatus.TabIndex = 20;
            this.CmbSearchStatus.ToolTips = "";
            // 
            // DtpSearchToDate
            // 
            this.DtpSearchToDate.AllowTabKeyOnEnter = true;
            this.DtpSearchToDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpSearchToDate.ForeColor = System.Drawing.Color.Black;
            this.DtpSearchToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpSearchToDate.Location = new System.Drawing.Point(409, 9);
            this.DtpSearchToDate.Name = "DtpSearchToDate";
            this.DtpSearchToDate.ShowUpDown = true;
            this.DtpSearchToDate.Size = new System.Drawing.Size(106, 21);
            this.DtpSearchToDate.TabIndex = 8;
            this.DtpSearchToDate.ToolTips = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label11.Location = new System.Drawing.Point(387, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "To";
            // 
            // DtpSearchFromDate
            // 
            this.DtpSearchFromDate.AllowTabKeyOnEnter = true;
            this.DtpSearchFromDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpSearchFromDate.ForeColor = System.Drawing.Color.Black;
            this.DtpSearchFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpSearchFromDate.Location = new System.Drawing.Point(273, 9);
            this.DtpSearchFromDate.Name = "DtpSearchFromDate";
            this.DtpSearchFromDate.ShowUpDown = true;
            this.DtpSearchFromDate.Size = new System.Drawing.Size(106, 21);
            this.DtpSearchFromDate.TabIndex = 6;
            this.DtpSearchFromDate.ToolTips = "";
            // 
            // txtSearchTicketNo
            // 
            this.txtSearchTicketNo.ActivationColor = false;
            this.txtSearchTicketNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSearchTicketNo.AllowTabKeyOnEnter = true;
            this.txtSearchTicketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchTicketNo.Format = "";
            this.txtSearchTicketNo.IsComplusory = false;
            this.txtSearchTicketNo.Location = new System.Drawing.Point(69, 9);
            this.txtSearchTicketNo.Name = "txtSearchTicketNo";
            this.txtSearchTicketNo.SelectAllTextOnFocus = true;
            this.txtSearchTicketNo.Size = new System.Drawing.Size(162, 21);
            this.txtSearchTicketNo.TabIndex = 3;
            this.txtSearchTicketNo.ToolTips = "";
            this.txtSearchTicketNo.WaterMarkText = null;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label9.Location = new System.Drawing.Point(232, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "From";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label10.Location = new System.Drawing.Point(10, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "TicketNo";
            // 
            // FrmClientTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 474);
            this.Controls.Add(this.panel1);
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmClientTicket";
            this.Text = "CLIENT TICKET";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel4;
        private AxonContLib.cTextBox txtGeneratedBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private AxonContLib.cComboBox cmbTicketPriority;
        private AxonContLib.cComboBox cmbTIcketStatus;
        private AxonContLib.cTextBox txtTicketDetail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnClear;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private System.Windows.Forms.Panel panel2;
        private AxonContLib.cDateTimePicker DtpTicketDate;
        private AxonContLib.cTextBox txtTicketNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label12;
        private AxonContLib.cComboBox CmbSearchStatus;
        private AxonContLib.cDateTimePicker DtpSearchToDate;
        private System.Windows.Forms.Label label11;
        private AxonContLib.cDateTimePicker DtpSearchFromDate;
        private AxonContLib.cTextBox txtSearchTicketNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}