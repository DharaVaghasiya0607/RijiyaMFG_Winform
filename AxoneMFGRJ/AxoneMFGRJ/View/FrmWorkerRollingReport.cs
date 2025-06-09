using AxoneMFGRJ.Report;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.View
{
    public partial class FrmWorkerRollingReport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DtabRolling = new DataTable();

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        string mStrReportTitle = "";

        public FrmWorkerRollingReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);

            this.Show();
        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                MainGrd.DataSource = null;
                GrdDet.Columns.Clear();

                string StrOpe = "";

                if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                    txtEmployee.Tag = string.Empty;


                if (RdbBeforeMak.Checked == true) StrOpe = "BEFOREMAK";
                else if (RdbAfterMak.Checked == true) StrOpe = "AFTERMAK";

                DtabRolling = Obj.GetWorkerRollingReport(StrOpe, Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Text));

                //GrdDet.BeginUpdate();

                if (DtabRolling.Rows.Count <= 0)
                {
                    Global.Message("No Data Found");
                    return;
                }

                if (DtabRolling.Columns.Contains("PROCESSNAME"))
                    DtabRolling.Columns.Remove("PROCESSNAME");
                if (DtabRolling.Columns.Contains("TOTALPCSPROCESSWISE"))
                    DtabRolling.Columns.Remove("TOTALPCSPROCESSWISE");

                

                MainGrd.DataSource = DtabRolling;
                GrdDet.RefreshData();

                for(int i = 0 ; i < GrdDet.Columns.Count ; i++)
                {
                    GrdDet.Columns[i].Caption = Val.ProperText(Val.ToString(GrdDet.Columns[i].FieldName));
                    GrdDet.Columns[i].Width = 90;
                }
                GrdDet.OptionsView.AllowHtmlDrawHeaders = true;
                GrdDet.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                GrdDet.ColumnPanelRowHeight = 40;
                
                //GrdDet.Appearance.Row.TextOptions.HAlignment = HorzAlignment.Center;
                //GrdDet.Appearance.Row.Font = new Font("Verdana", 8);
                //GrdDet.Appearance.HeaderPanel.Font = new Font("Verdana", 9, FontStyle.Bold);

                GrdDet.Appearance.Row.TextOptions.HAlignment = HorzAlignment.Center;

                GrdDet.Columns["EMPLOYEE_ID"].Visible = false;

                GrdDet.Columns["EMPLOYEECODE"].Caption = "Emp";

                //GrdDet.Columns["TOTALPCSWITHOW"].Caption = "With OW";
                //GrdDet.Columns["TOTALPCSWITHOUTOW"].Caption = "Without OW";

                //GrdDet.BestFitColumns();

                //GrdDet.EndUpdate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }

        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString(mStrReportTitle, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }


        private void GrdDetDept_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

        }

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtEmployee.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void lblPacketPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Labour Detail (" + Str + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGrd;
                link.Landscape = true;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void lblExportSummary_Click(object sender, EventArgs e)
        {

        }

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Summary (" + Str + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGrd;
                link.Landscape = true;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "0/0.000")
                {
                    e.DisplayText = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnExcel_Click(object sender, EventArgs e)
        {


            GrdDet.OptionsPrint.UsePrintStyles = false;
            string Str = txtEmployee.Text.Trim().Length == 0 ? "All Workers" : txtEmployee.Text;

            string Str2 = "";
            if (RdbBeforeMak.Checked == true) Str2 = "Before Makable";
            else if (RdbAfterMak.Checked == true) Str2 = "After  Makable";

            mStrReportTitle = Str2 + " Worker Rolling Report (" + Str + ")";
            
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "WorkerRollingReport.xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    GrdDet.Appearance.Row.Font = new Font("Verdana", 8);
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrd,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };


                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);


                    DevExpress.XtraPrinting.XlsxExportOptions  a = new DevExpress.XtraPrinting.XlsxExportOptions();
                    //a.Suppress256ColumnsWarning = true;
                    //a.Suppress65536RowsWarning = true;
                    a.TextExportMode = TextExportMode.Text;

                    //link.ExportToXlsx(svDialog.FileName);
                    link.ExportToXlsx(svDialog.FileName,a);

                    if (Global.Confirm("Do You Want To Open [WorkerRollingReport.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                GrdDet.Appearance.Row.Font = new Font("Verdana", 7);
                svDialog.Dispose();
                svDialog = null;
                GrdDet.OptionsPrint.UsePrintStyles = true;
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }


        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Workers" : txtEmployee.Text;


                string Str2 = "";
                if (RdbBeforeMak.Checked == true) Str2 = "Before Makable";
                else if (RdbAfterMak.Checked == true) Str2 = "After  Makable";

                mStrReportTitle = Str2 + " Worker Rolling Report (" + Str + ")";

                link.Component = MainGrd;
                link.Landscape = true;

                link.Margins.Left = 10;
                link.Margins.Right = 10;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


        private void GrdDet_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0 || e.Column.FieldName == "EMPLOYEECODE" || e.Column.FieldName == "DEPT")
                {
                    return;
                }


                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string StrOpe = "", StrProcessName = "";
                    if (RdbBeforeMak.Checked == true) StrOpe = "BEFOREMAK";
                    else if (RdbAfterMak.Checked == true) StrOpe = "AFTERMAK";

                    bool IsWithOw = true;

                    if (e.Column.FieldName == "TOTALPCSWITHOUTOW")
                        IsWithOw = false;

                    if (e.Column.FieldName != "TOTALPCSWITHOUTOW" && e.Column.FieldName != "TOTALPCSWITHOW" && e.Column.FieldName != "EMPLOYEECODE")
                        StrProcessName = e.Column.FieldName;


                    if(Val.ToString(e.Column.FieldName).ToUpper().Contains("TOTAL"))
                    {
                        StrProcessName = string.Empty;
                    }

                    DataRow Dr = GrdDet.GetFocusedDataRow();

                    //if (Val.ToString(Dr["EMPLOYEECODE"]).Trim().ToUpper().Equals("TOTAL"))
                    //    return;

                    DataTable DtabRollingDetail = Obj.GetWorkerRollingReport(StrOpe, Val.ToInt64(Dr["EMPLOYEE_ID"]), Val.SqlDate(DTPFromDate.Text), "DETAIL", IsWithOw, StrProcessName);
                    if (DtabRollingDetail.Rows.Count > 0)
                    {
                        string StrReportTitle = " Worker [" + Val.ToString(Dr["EMPLOYEECODE"]) + "] Current Rolling Report";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = "";
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = DtabRollingDetail;
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                string strEmpCode = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "EMPLOYEECODE"));

                if (strEmpCode == "TOTAL")
                {
                    e.Appearance.Font = new Font("Verdana", 7f, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                }
                if (e.Column.FieldName == "EMPLOYEECODE")
                {
                    //e.Appearance.BackColor = Color.FromArgb(192, 210, 255);
                    //e.Appearance.BackColor2 = Color.FromArgb(192, 210, 255);

                    //e.Appearance.BackColor = Color.FromArgb(221, 235, 247);
                    //e.Appearance.BackColor2 = Color.FromArgb(221, 235, 247);


                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void FrmMarkerRollingReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    BtnRefresh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


    }
}
