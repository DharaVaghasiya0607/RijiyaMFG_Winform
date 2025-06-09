using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.TableName;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;
using DevExpress.XtraPivotGrid;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmAttendanceRegister : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Attendance ObjMast = new BOMST_Attendance();

        #region Property Settings

        public FrmAttendanceRegister()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            RbtWD_CheckedChanged(null, null);

            CmbMonth.Focus();

            CmbMonth.SelectedItem = DateTime.Now.ToString("MMM");
            txtYear.Text = DateTime.Now.Year.ToString();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    this.Cursor = Cursors.WaitCursor;

            //    string StrYearMonth = txtYear.Text + ((CmbMonth.SelectedIndex + 1) < 10 ? "0" + (CmbMonth.SelectedIndex + 1).ToString() : (CmbMonth.SelectedIndex + 1).ToString());

            //    DataTable DTab = ObjMast.GetAttendanceRegisterPrint(Val.ToInt(StrYearMonth));

            //    if (DTab.Rows.Count == 0)
            //    {
            //        Global.Message("No Record Found");
            //        this.Cursor = Cursors.Default;

            //        return;
            //    }

            //    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
            //    FrmReportViewer.MdiParent = Global.gMainRef;
            //    FrmReportViewer.ShowForm("AttendanceSalesRegister", DTab);
            //    this.Cursor = Cursors.Default;

            //}
            //catch (Exception ex)
            //{
            //    this.Cursor = Cursors.Default;

            //    Global.Message(ex.Message);

            //}

            DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

            PrinterSettingsUsing pst = new PrinterSettingsUsing();

            PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);
            PivotGridData.OptionsPrint.UsePrintAppearance = true;
            //Lesson2 link = new Lesson2(PrintSystem);
            PrintableComponentLink link = new PrintableComponentLink(PrintSystem);
            link.Component = PivotGridData;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link.Margins.Left = 40;
            link.Margins.Right = 40;
            link.Margins.Bottom = 40;
            link.Margins.Top = 120;

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);

            link.CreateDocument();

            link.ShowPreview();
            link.PrintDlg();
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("Attendance Register Of ( " + CmbMonth.SelectedItem.ToString() + "-" + txtYear.Text + " )", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            string Str = txtDepartment.Text.Trim().Length == 0 ? "All Department" : txtDepartment.Text;
            TextBrick BrickTitlesParam = e.Graph.DrawString("Department  : " + Str, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;
        }

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Black, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("Verdana", 8, FontStyle.Bold);
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtYear.Text.Length != 4)
            {
                Global.Message("Please Enter Proper Year i.e. 2017,2018 etc");
                txtYear.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            string StrYearMonth = txtYear.Text + ((CmbMonth.SelectedIndex + 1) < 10 ? "0" + (CmbMonth.SelectedIndex + 1).ToString() : (CmbMonth.SelectedIndex + 1).ToString());

            if (txtDepartment.Text.Length == 0)
            {
                txtDepartment.Tag = "";
                Global.MessageError("Department Required");
                return;
            }

            RbtWD_CheckedChanged(null, null);

            if (RbtAll.Checked == true) 
            {
                PivotGridData.Fields["WHOURS"].Visible = true;
                PivotGridData.Fields["AP"].Visible = true;
                PivotGridData.Fields["WDAYS"].Visible = true;
            }



            DataTable DTab = ObjMast.GetAttendanceRegister(Val.ToInt(StrYearMonth), Val.ToInt(txtDepartment.Tag));
            PivotGridData.DataSource = DTab;
            PivotGridData.Refresh();

            PivotGridData.Fields["EMPLOYEENAME"].Visible = ChkEmp.Checked;
            PivotGridData.Fields["DEPARTMENTNAME"].Visible = ChkDept.Checked;
            PivotGridData.Fields["DESIGNATIONNAME"].Visible = ChkDesig.Checked;
            PivotGridData.BestFit();
            PivotGridData.Fields["WHOURS"].Width = 25;
            this.Cursor = Cursors.Default;

        }

        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "AttendanceRegister";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = PivotGridData,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [" + svDialog.FileName + ".xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void PivotGridData_CustomCellEdit(object sender, DevExpress.XtraPivotGrid.PivotCustomCellEditEventArgs e)
        {

        }

        private void PivotGridData_CustomDrawCell(object sender, DevExpress.XtraPivotGrid.PivotCustomDrawCellEventArgs e)
        {
            try
            {
                if (e.DataField.FieldName == "WHOURS")
                {
                    if (Val.ToString(e.DisplayText) == "0.00" || Val.ToString(e.DisplayText) == "0.0" || Val.ToString(e.DisplayText) == "0")
                    {
                        e.Appearance.ForeColor = Color.White;
                    }

                }

            }
            catch (Exception Ex)
            {

            }
        }

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTCODE,DEPARTMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);

                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                    }
                    else
                    {
                        txtDepartment.Text = Val.ToString(DBNull.Value);
                        txtDepartment.Tag = Val.ToString(DBNull.Value);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void PivotGridData_FocusedCellChanged(object sender, EventArgs e)
        {

        }

        private void PivotGridData_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    PivotCellEventArgs CurrentCell = PivotGridData.Cells.GetFocusedCellInfo();

            //    if (CurrentCell.ColumnValueType == PivotGridValueType.GrandTotal || CurrentCell.RowValueType == PivotGridValueType.GrandTotal)
            //    {
            //        return;
            //    }

            //    string StrEmployeeID = Val.ToString(CurrentCell.GetFieldValue(EMPLOYEEID));
            //    double DouWhours = Val.Val(CurrentCell.GetFieldValue(WHOURS));
            //    int IntDay = Val.ToInt(CurrentCell.GetFieldValue(DAY));
            //    int IntMonth = CmbMonth.SelectedIndex + 1;

            //    string AtdyyyyMMdd = txtYear.Text + (IntMonth < 10 ? "0" + IntMonth.ToString() : IntMonth.ToString()) + (IntDay < 10 ? "0" + IntDay.ToString() : IntDay.ToString());



            //}
            //catch
            //{

            //}
        }

        private void RbtWD_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtWH.Checked == true)
            {
                PivotGridData.Fields["WHOURS"].Visible = true;
                PivotGridData.Fields["AP"].Visible = false;
                PivotGridData.Fields["WDAYS"].Visible = false;
            }

            else if (RbtAP.Checked == true)
            {
                PivotGridData.Fields["WHOURS"].Visible = false;
                PivotGridData.Fields["AP"].Visible = true;
                PivotGridData.Fields["WDAYS"].Visible = false;
            }
            else if (RbtWD.Checked == true)
            {
                PivotGridData.Fields["WHOURS"].Visible = false;
                PivotGridData.Fields["AP"].Visible = false;
                PivotGridData.Fields["WDAYS"].Visible = true;
            }
            else if (RbtAll.Checked == true)
            {
                PivotGridData.Fields["WHOURS"].Visible = true;
                PivotGridData.Fields["AP"].Visible = true;
                PivotGridData.Fields["WDAYS"].Visible = true;
            }
        }
    }
}
