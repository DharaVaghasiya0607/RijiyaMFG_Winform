namespace AxoneMFGRJ.Masters
{
    partial class FrmAttendanceRegister
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
            this.panel4 = new AxonContLib.cPanel();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtDepartment = new AxonContLib.cTextBox(this.components);
            this.panel1 = new AxonContLib.cPanel();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.BtnExcelExport = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnBack = new System.Windows.Forms.Button();
            this.ChkDept = new AxonContLib.cCheckBox(this.components);
            this.ChkEmp = new AxonContLib.cCheckBox(this.components);
            this.ChkDesig = new AxonContLib.cCheckBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.lblParty = new AxonContLib.cLabel(this.components);
            this.CmbMonth = new AxonContLib.cComboBox(this.components);
            this.txtYear = new AxonContLib.cTextBox(this.components);
            this.panel2 = new AxonContLib.cPanel();
            this.PivotGridData = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.DEPT = new DevExpress.XtraPivotGrid.PivotGridField();
            this.DESIG = new DevExpress.XtraPivotGrid.PivotGridField();
            this.EMPCODE = new DevExpress.XtraPivotGrid.PivotGridField();
            this.EMPNAME = new DevExpress.XtraPivotGrid.PivotGridField();
            this.DAY = new DevExpress.XtraPivotGrid.PivotGridField();
            this.WH = new DevExpress.XtraPivotGrid.PivotGridField();
            this.EMPID = new DevExpress.XtraPivotGrid.PivotGridField();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PivotGridData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cLabel1);
            this.panel4.Controls.Add(this.txtDepartment);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.cLabel2);
            this.panel4.Controls.Add(this.lblParty);
            this.panel4.Controls.Add(this.CmbMonth);
            this.panel4.Controls.Add(this.txtYear);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 73);
            this.panel4.TabIndex = 0;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(203, 8);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(82, 14);
            this.cLabel1.TabIndex = 150;
            this.cLabel1.Text = "Department";
            this.cLabel1.ToolTips = "";
            // 
            // txtDepartment
            // 
            this.txtDepartment.ActivationColor = true;
            this.txtDepartment.AllowTabKeyOnEnter = false;
            this.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDepartment.ComplusoryMsg = null;
            this.txtDepartment.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment.Format = "";
            this.txtDepartment.IsComplusory = false;
            this.txtDepartment.Location = new System.Drawing.Point(203, 26);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.RequiredChars = "";
            this.txtDepartment.SelectAllTextOnFocus = true;
            this.txtDepartment.ShowToolTipOnFocus = false;
            this.txtDepartment.Size = new System.Drawing.Size(314, 26);
            this.txtDepartment.TabIndex = 149;
            this.txtDepartment.ToolTips = "";
            this.txtDepartment.WaterMarkText = null;
            this.txtDepartment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDepartment_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnSearch);
            this.panel1.Controls.Add(this.BtnExcelExport);
            this.panel1.Controls.Add(this.BtnExport);
            this.panel1.Controls.Add(this.BtnBack);
            this.panel1.Controls.Add(this.ChkDept);
            this.panel1.Controls.Add(this.ChkEmp);
            this.panel1.Controls.Add(this.ChkDesig);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(621, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 73);
            this.panel1.TabIndex = 140;
            // 
            // BtnSearch
            // 
            this.BtnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Location = new System.Drawing.Point(11, 9);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(103, 35);
            this.BtnSearch.TabIndex = 5;
            this.BtnSearch.Text = "Sho&w F5";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnExcelExport
            // 
            this.BtnExcelExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.BtnExcelExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExcelExport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExcelExport.ForeColor = System.Drawing.Color.White;
            this.BtnExcelExport.Location = new System.Drawing.Point(227, 9);
            this.BtnExcelExport.Name = "BtnExcelExport";
            this.BtnExcelExport.Size = new System.Drawing.Size(103, 35);
            this.BtnExcelExport.TabIndex = 32;
            this.BtnExcelExport.TabStop = false;
            this.BtnExcelExport.Text = "Export";
            this.BtnExcelExport.UseVisualStyleBackColor = false;
            this.BtnExcelExport.Click += new System.EventHandler(this.BtnExcelExport_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.Teal;
            this.BtnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExport.ForeColor = System.Drawing.Color.White;
            this.BtnExport.Location = new System.Drawing.Point(118, 9);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(103, 35);
            this.BtnExport.TabIndex = 32;
            this.BtnExport.TabStop = false;
            this.BtnExport.Text = "Print ";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(54)))), ((int)(((byte)(16)))));
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.Color.White;
            this.BtnBack.Location = new System.Drawing.Point(336, 9);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 30;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // ChkDept
            // 
            this.ChkDept.AutoSize = true;
            this.ChkDept.Checked = true;
            this.ChkDept.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkDept.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDept.Location = new System.Drawing.Point(11, 50);
            this.ChkDept.Name = "ChkDept";
            this.ChkDept.Size = new System.Drawing.Size(56, 17);
            this.ChkDept.TabIndex = 2;
            this.ChkDept.Text = "Dept";
            this.ChkDept.ToolTips = "";
            this.ChkDept.UseVisualStyleBackColor = true;
            // 
            // ChkEmp
            // 
            this.ChkEmp.AutoSize = true;
            this.ChkEmp.Checked = true;
            this.ChkEmp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkEmp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkEmp.Location = new System.Drawing.Point(135, 50);
            this.ChkEmp.Name = "ChkEmp";
            this.ChkEmp.Size = new System.Drawing.Size(54, 17);
            this.ChkEmp.TabIndex = 3;
            this.ChkEmp.Text = "Emp";
            this.ChkEmp.ToolTips = "";
            this.ChkEmp.UseVisualStyleBackColor = true;
            // 
            // ChkDesig
            // 
            this.ChkDesig.AutoSize = true;
            this.ChkDesig.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDesig.Location = new System.Drawing.Point(70, 50);
            this.ChkDesig.Name = "ChkDesig";
            this.ChkDesig.Size = new System.Drawing.Size(62, 17);
            this.ChkDesig.TabIndex = 3;
            this.ChkDesig.Text = "Desig";
            this.ChkDesig.ToolTips = "";
            this.ChkDesig.UseVisualStyleBackColor = true;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(110, 8);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(35, 14);
            this.cLabel2.TabIndex = 139;
            this.cLabel2.Text = "Year";
            this.cLabel2.ToolTips = "";
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParty.ForeColor = System.Drawing.Color.Black;
            this.lblParty.Location = new System.Drawing.Point(8, 8);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(46, 14);
            this.lblParty.TabIndex = 139;
            this.lblParty.Text = "Month";
            this.lblParty.ToolTips = "";
            // 
            // CmbMonth
            // 
            this.CmbMonth.AllowTabKeyOnEnter = false;
            this.CmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMonth.Font = new System.Drawing.Font("Verdana", 11.25F);
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
            this.CmbMonth.Location = new System.Drawing.Point(8, 26);
            this.CmbMonth.MaxDropDownItems = 12;
            this.CmbMonth.Name = "CmbMonth";
            this.CmbMonth.Size = new System.Drawing.Size(94, 26);
            this.CmbMonth.TabIndex = 0;
            this.CmbMonth.ToolTips = "";
            // 
            // txtYear
            // 
            this.txtYear.ActivationColor = true;
            this.txtYear.AllowTabKeyOnEnter = false;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.ComplusoryMsg = null;
            this.txtYear.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Format = "######";
            this.txtYear.IsComplusory = false;
            this.txtYear.Location = new System.Drawing.Point(110, 26);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.RequiredChars = "0123456789.";
            this.txtYear.SelectAllTextOnFocus = true;
            this.txtYear.ShowToolTipOnFocus = false;
            this.txtYear.Size = new System.Drawing.Size(86, 26);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.ToolTips = "";
            this.txtYear.WaterMarkText = null;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.PivotGridData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1082, 400);
            this.panel2.TabIndex = 19;
            // 
            // PivotGridData
            // 
            this.PivotGridData.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.PivotGridData.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.Appearance.Cell.Options.UseBackColor = true;
            this.PivotGridData.Appearance.Cell.Options.UseFont = true;
            this.PivotGridData.Appearance.CustomTotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.PivotGridData.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.Appearance.CustomTotalCell.Options.UseBackColor = true;
            this.PivotGridData.Appearance.CustomTotalCell.Options.UseFont = true;
            this.PivotGridData.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.PivotGridData.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.Appearance.FieldValueGrandTotal.Options.UseBackColor = true;
            this.PivotGridData.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.PivotGridData.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.PivotGridData.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.Appearance.FieldValueTotal.Options.UseBackColor = true;
            this.PivotGridData.Appearance.FieldValueTotal.Options.UseFont = true;
            this.PivotGridData.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.PivotGridData.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.Appearance.GrandTotalCell.Options.UseBackColor = true;
            this.PivotGridData.Appearance.GrandTotalCell.Options.UseFont = true;
            this.PivotGridData.Appearance.Lines.BackColor = System.Drawing.Color.Gray;
            this.PivotGridData.Appearance.Lines.Options.UseBackColor = true;
            this.PivotGridData.Appearance.SelectedCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.PivotGridData.Appearance.SelectedCell.Options.UseFont = true;
            this.PivotGridData.Appearance.SelectedCell.Options.UseTextOptions = true;
            this.PivotGridData.Appearance.SelectedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PivotGridData.Appearance.TotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.PivotGridData.Appearance.TotalCell.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.Appearance.TotalCell.Options.UseBackColor = true;
            this.PivotGridData.Appearance.TotalCell.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.Cell.Font = new System.Drawing.Font("Verdana", 7F);
            this.PivotGridData.AppearancePrint.Cell.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.CustomTotalCell.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.AppearancePrint.CustomTotalCell.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.FieldHeader.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.AppearancePrint.FieldHeader.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.FieldValue.Font = new System.Drawing.Font("Verdana", 7F);
            this.PivotGridData.AppearancePrint.FieldValue.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.FieldValueGrandTotal.Font = new System.Drawing.Font("Verdana", 7F);
            this.PivotGridData.AppearancePrint.FieldValueGrandTotal.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.FieldValueTotal.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.AppearancePrint.FieldValueTotal.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.GrandTotalCell.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.AppearancePrint.GrandTotalCell.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.HeaderGroupLine.Font = new System.Drawing.Font("Verdana", 7F);
            this.PivotGridData.AppearancePrint.HeaderGroupLine.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.Lines.Font = new System.Drawing.Font("Verdana", 7F);
            this.PivotGridData.AppearancePrint.Lines.Options.UseFont = true;
            this.PivotGridData.AppearancePrint.TotalCell.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGridData.AppearancePrint.TotalCell.Options.UseFont = true;
            this.PivotGridData.Cursor = System.Windows.Forms.Cursors.Default;
            this.PivotGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PivotGridData.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.DEPT,
            this.DESIG,
            this.EMPCODE,
            this.EMPNAME,
            this.DAY,
            this.WH,
            this.EMPID});
            this.PivotGridData.Location = new System.Drawing.Point(0, 0);
            this.PivotGridData.Name = "PivotGridData";
            this.PivotGridData.OptionsPrint.UsePrintAppearance = true;
            this.PivotGridData.OptionsView.ShowColumnHeaders = false;
            this.PivotGridData.OptionsView.ShowDataHeaders = false;
            this.PivotGridData.OptionsView.ShowFilterHeaders = false;
            this.PivotGridData.Size = new System.Drawing.Size(1082, 400);
            this.PivotGridData.TabIndex = 33;
            this.PivotGridData.FocusedCellChanged += new System.EventHandler(this.PivotGridData_FocusedCellChanged);
            this.PivotGridData.CustomDrawCell += new DevExpress.XtraPivotGrid.PivotCustomDrawCellEventHandler(this.PivotGridData_CustomDrawCell);
            this.PivotGridData.CustomCellEdit += new System.EventHandler<DevExpress.XtraPivotGrid.PivotCustomCellEditEventArgs>(this.PivotGridData_CustomCellEdit);
            this.PivotGridData.Click += new System.EventHandler(this.PivotGridData_Click);
            // 
            // DEPT
            // 
            this.DEPT.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.DEPT.Appearance.Cell.Options.UseFont = true;
            this.DEPT.Appearance.CellGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DEPT.Appearance.CellGrandTotal.Options.UseFont = true;
            this.DEPT.Appearance.CellTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DEPT.Appearance.CellTotal.Options.UseFont = true;
            this.DEPT.Appearance.Header.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.DEPT.Appearance.Header.Options.UseFont = true;
            this.DEPT.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F);
            this.DEPT.Appearance.Value.Options.UseFont = true;
            this.DEPT.Appearance.ValueGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DEPT.Appearance.ValueGrandTotal.Options.UseFont = true;
            this.DEPT.Appearance.ValueTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DEPT.Appearance.ValueTotal.Options.UseFont = true;
            this.DEPT.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.DEPT.AreaIndex = 0;
            this.DEPT.Caption = "Dept";
            this.DEPT.FieldName = "DEPARTMENTNAME";
            this.DEPT.Name = "DEPT";
            // 
            // DESIG
            // 
            this.DESIG.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.DESIG.Appearance.Cell.Options.UseFont = true;
            this.DESIG.Appearance.CellGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DESIG.Appearance.CellGrandTotal.Options.UseFont = true;
            this.DESIG.Appearance.CellTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DESIG.Appearance.CellTotal.Options.UseFont = true;
            this.DESIG.Appearance.Header.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.DESIG.Appearance.Header.Options.UseFont = true;
            this.DESIG.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F);
            this.DESIG.Appearance.Value.Options.UseFont = true;
            this.DESIG.Appearance.ValueGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DESIG.Appearance.ValueGrandTotal.Options.UseFont = true;
            this.DESIG.Appearance.ValueTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DESIG.Appearance.ValueTotal.Options.UseFont = true;
            this.DESIG.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.DESIG.AreaIndex = 1;
            this.DESIG.Caption = "Desig";
            this.DESIG.FieldName = "DESIGNATIONNAME";
            this.DESIG.Name = "DESIG";
            this.DESIG.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            // 
            // EMPCODE
            // 
            this.EMPCODE.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.EMPCODE.Appearance.Cell.Options.UseFont = true;
            this.EMPCODE.Appearance.CellGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EMPCODE.Appearance.CellGrandTotal.Options.UseFont = true;
            this.EMPCODE.Appearance.CellTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EMPCODE.Appearance.CellTotal.Options.UseFont = true;
            this.EMPCODE.Appearance.Header.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.EMPCODE.Appearance.Header.Options.UseFont = true;
            this.EMPCODE.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F);
            this.EMPCODE.Appearance.Value.Options.UseFont = true;
            this.EMPCODE.Appearance.ValueGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EMPCODE.Appearance.ValueGrandTotal.Options.UseFont = true;
            this.EMPCODE.Appearance.ValueTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EMPCODE.Appearance.ValueTotal.Options.UseFont = true;
            this.EMPCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.EMPCODE.AreaIndex = 2;
            this.EMPCODE.Caption = "Code";
            this.EMPCODE.FieldName = "EMPLOYEECODE";
            this.EMPCODE.Name = "EMPCODE";
            this.EMPCODE.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            // 
            // EMPNAME
            // 
            this.EMPNAME.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.EMPNAME.Appearance.Cell.Options.UseFont = true;
            this.EMPNAME.Appearance.CellGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EMPNAME.Appearance.CellGrandTotal.Options.UseFont = true;
            this.EMPNAME.Appearance.CellTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EMPNAME.Appearance.CellTotal.Options.UseFont = true;
            this.EMPNAME.Appearance.Header.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.EMPNAME.Appearance.Header.Options.UseFont = true;
            this.EMPNAME.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F);
            this.EMPNAME.Appearance.Value.Options.UseFont = true;
            this.EMPNAME.Appearance.ValueGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EMPNAME.Appearance.ValueGrandTotal.Options.UseFont = true;
            this.EMPNAME.Appearance.ValueTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EMPNAME.Appearance.ValueTotal.Options.UseFont = true;
            this.EMPNAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.EMPNAME.AreaIndex = 3;
            this.EMPNAME.Caption = "Employee";
            this.EMPNAME.FieldName = "EMPLOYEENAME";
            this.EMPNAME.Name = "EMPNAME";
            this.EMPNAME.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.EMPNAME.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.EMPNAME.Width = 155;
            // 
            // DAY
            // 
            this.DAY.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.DAY.Appearance.Cell.Options.UseFont = true;
            this.DAY.Appearance.Cell.Options.UseTextOptions = true;
            this.DAY.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DAY.Appearance.CellGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DAY.Appearance.CellGrandTotal.Options.UseFont = true;
            this.DAY.Appearance.CellTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DAY.Appearance.CellTotal.Options.UseFont = true;
            this.DAY.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DAY.Appearance.Value.Options.UseFont = true;
            this.DAY.Appearance.Value.Options.UseTextOptions = true;
            this.DAY.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DAY.Appearance.ValueGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DAY.Appearance.ValueGrandTotal.Options.UseFont = true;
            this.DAY.Appearance.ValueTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.DAY.Appearance.ValueTotal.Options.UseFont = true;
            this.DAY.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.DAY.AreaIndex = 0;
            this.DAY.Caption = "Day";
            this.DAY.FieldName = "DD";
            this.DAY.Name = "DAY";
            this.DAY.Width = 70;
            // 
            // WH
            // 
            this.WH.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.WH.Appearance.Cell.Options.UseFont = true;
            this.WH.Appearance.CellGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.WH.Appearance.CellGrandTotal.Options.UseFont = true;
            this.WH.Appearance.CellGrandTotal.Options.UseTextOptions = true;
            this.WH.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.WH.Appearance.CellTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.WH.Appearance.CellTotal.Options.UseFont = true;
            this.WH.Appearance.CellTotal.Options.UseTextOptions = true;
            this.WH.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.WH.Appearance.Header.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.WH.Appearance.Header.Options.UseFont = true;
            this.WH.Appearance.Header.Options.UseTextOptions = true;
            this.WH.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.WH.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F);
            this.WH.Appearance.Value.Options.UseFont = true;
            this.WH.Appearance.Value.Options.UseTextOptions = true;
            this.WH.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.WH.Appearance.ValueGrandTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.WH.Appearance.ValueGrandTotal.Options.UseFont = true;
            this.WH.Appearance.ValueGrandTotal.Options.UseTextOptions = true;
            this.WH.Appearance.ValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.WH.Appearance.ValueTotal.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.WH.Appearance.ValueTotal.Options.UseFont = true;
            this.WH.Appearance.ValueTotal.Options.UseTextOptions = true;
            this.WH.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.WH.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.WH.AreaIndex = 0;
            this.WH.Caption = "WH";
            this.WH.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.WH.FieldName = "WHOURS";
            this.WH.Name = "WH";
            this.WH.Width = 35;
            // 
            // EMPID
            // 
            this.EMPID.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.EMPID.AreaIndex = 1;
            this.EMPID.Caption = "Employee ID";
            this.EMPID.FieldName = "EMPLOYEE_ID";
            this.EMPID.Name = "EMPID";
            // 
            // FrmAttendanceRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Name = "FrmAttendanceRegister";
            this.Text = "DAILY ATTENDANCE ENTRY";
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PivotGridData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel panel4;
        private AxonContLib.cPanel panel2;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnBack;
        private AxonContLib.cTextBox txtYear;
        private AxonContLib.cComboBox CmbMonth;
        private AxonContLib.cLabel lblParty;
        private AxonContLib.cPanel panel1;
        private System.Windows.Forms.Button BtnExcelExport;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtDepartment;
        private AxonContLib.cCheckBox ChkEmp;
        private AxonContLib.cCheckBox ChkDesig;
        private AxonContLib.cCheckBox ChkDept;
        private DevExpress.XtraPivotGrid.PivotGridControl PivotGridData;
        private DevExpress.XtraPivotGrid.PivotGridField DEPT;
        private DevExpress.XtraPivotGrid.PivotGridField DESIG;
        private DevExpress.XtraPivotGrid.PivotGridField EMPCODE;
        private DevExpress.XtraPivotGrid.PivotGridField EMPNAME;
        private DevExpress.XtraPivotGrid.PivotGridField DAY;
        private DevExpress.XtraPivotGrid.PivotGridField WHOURS;
        private AxonContLib.cLabel cLabel2;
        private DevExpress.XtraPivotGrid.PivotGridField EMPLOYEEID;
        private DevExpress.XtraPivotGrid.PivotGridField WH;
        private DevExpress.XtraPivotGrid.PivotGridField EMPID;


    }
}