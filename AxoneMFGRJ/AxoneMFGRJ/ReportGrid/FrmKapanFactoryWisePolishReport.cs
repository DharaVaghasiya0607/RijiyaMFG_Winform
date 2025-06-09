using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.Transaction;
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
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using BusLib.Master;
using AxoneMFGRJ.Transaction;
using BusLib.ReportGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Reflection;
using DevExpress.Data;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;
using BusLib.Rapaport;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using BusLib.View;
using OfficeOpenXml;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmKapanFactoryWisePolishReport : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjView = new BOTRN_RunninPossition();
        Stimulsoft.Report.StiReport report = new Stimulsoft.Report.StiReport();

        DataTable DtabSizeWisePol = new DataTable();
        DataTable DtabFactoryWise = new DataTable();
        string StrFromDate = null;
        string StrToDate = null;
        string StrKapan = "";
        string ReportType = "";
        string StrCurrEmployee = "";
        string StrManager = "";

        #region Property Settings

        public FrmKapanFactoryWisePolishReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            CmbKapan.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.DisplayMember = "KAPANNAME";
            CmbKapan.ValueMember = "KAPANNAME";
            CmbKapan.Focus();
            CmbKapan.SelectedIndex = -1;
            RbtnSummery_CheckedChanged(null, null);

            //RdbGrdType.SelectedIndex = 0;
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjView);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                //string StrFromDate = "";
                //string StrToDate = "";
                
                if (Val.ToBoolean(DTPFromDate.Checked) == true && Val.ToBoolean(DTPToDate.Checked) == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToString("yyyy-MM-dd"));
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    StrFromDate = null;
                    StrToDate = null;
                }

                if (RbtSizeWisePolishReport.Checked || (RbtFactoryWise.Checked && RbtnDetails.Checked))
                {
                    StrKapan = Val.ToString(CmbKapan.Text);
                }
                else
                {
                    StrKapan = "'" + Val.ToString(CmbKapan.Text) + "'";
                }


                StrCurrEmployee = Val.ToString(txtCurrEmp.Text).Trim().Equals(string.Empty) ? "" : Val.Trim(txtCurrEmp.Tag);
                StrManager = Val.ToString(TxtManager.Text).Trim().Equals(string.Empty) ? "" : Val.Trim(TxtManager.Tag);

                if (RbtnSummery.Checked == true)
                {
                    ReportType = Val.ToString(RbtnSummery.Text);
                }
                else if (RbtnDetails.Checked == true)
                {
                    ReportType = Val.ToString(RbtnDetails.Text);
                }
                // Global.Message(ReportType);
                report = new Stimulsoft.Report.StiReport();
                ObjFormEvent.ObjToDisposeList.Add(report);
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                PanelProgress.Visible = true;
                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                }
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

            /*
             *  try
        {

            string StrFromDate = "";
            string StrToDate = "";
            if (Val.ToBoolean(DTPFromDate.Checked) == true && Val.ToBoolean(DTPToDate.Checked) == true)
            {
                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
            }
            else
            {
                StrFromDate = null;
                StrToDate = null;
            }

            Stimulsoft.Report.StiReport report = new Stimulsoft.Report.StiReport();
            report.Load(Application.StartupPath + "\\RPT\\" + "FactoryWisePolishReport" + ".mrt");
            report.Compile();
            report.RequestParameters = false;
            foreach (Stimulsoft.Report.Dictionary.StiSqlDatabase item in report.CompiledReport.Dictionary.Databases)
            {
                item.ConnectionString = BusLib.Configuration.BOConfiguration.ConnectionString;
            }
            report["KAPANNAME"] = "'" + Val.ToString(CmbKapan.Text) + "'";
            report["FROMDATE"] = StrFromDate;
            report["TODATE"] = StrToDate;

            StiSqlDatabase sql = new StiSqlDatabase("Connection", BusLib.Configuration.BOConfiguration.ConnectionString);
            sql.Alias = "Connection";
            report.CompiledReport.Dictionary.Databases.Clear();
            report.CompiledReport.Dictionary.Databases.Add(sql);
            //report.CompiledReport.DataSources("Rpt_DonationReceipt").Parameters("@KAPANNAME").ParameterValue = Val.ToString(CmbKapan.Text);
                

            report.PreviewMode = StiPreviewMode.StandardAndDotMatrix;
            report.Render(false);
            report.Show();

        }
        catch (Exception ex)
        {
            this.Cursor = Cursors.Default;
            Global.Message(ex.Message);
            return;
        }
            */
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDirectPDFExport_Click(object sender, EventArgs e)
        {

            /*
             * try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "pdf";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "KapanDetailReport";
                svDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    string StrKapan = CmbKapan.SelectedValue.ToString();
                    string StrOpe = "";
                    string StrGrdType = "";   //#P : 01-02-2020

                    string StrFromDate = null;
                    string StrToDate = null;

                    if (DTPFromDate.Checked == true)
                    {
                        StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    }
                    if (DTPToDate.Checked == true)
                    {
                        StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                    }

                    this.Cursor = Cursors.WaitCursor;
                    DataSet DS = ObjView.KapanDetailReport(StrKapan, StrFromDate, StrToDate, Val.Trim(txtMultiMainManager.Tag).ToString());

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ExportKapanDetailPDF("DetailKapanAnalysis", DS, Filepath);

                    System.Diagnostics.Process.Start(Filepath, "cmd");

                    this.Cursor = Cursors.Default;

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
             * */
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                if (RbtSizeWisePolishReport.Checked)
                {
                    DtabSizeWisePol = ObjView.SizeWisePolishReport(StrKapan, StrFromDate, StrToDate);
                }
                else if (RbtnDetails.Checked && RbtFactoryWise.Checked)
                {
                    DtabFactoryWise = ObjView.FactoryWisePolishReport(StrKapan, StrFromDate, StrToDate, ReportType, StrCurrEmployee,StrManager);
                }
                else
                {
                    if (RbtFactoryWise.Checked)
                    {
                        report.Load(Application.StartupPath + "\\RPT\\" + "FactoryWisePolishReport" + ".mrt");
                    }
                    else
                    {
                        report.Load(Application.StartupPath + "\\RPT\\" + "FinalPolishWise4PReport" + ".mrt");
                    }

                    report.Compile();
                    report.RequestParameters = false;
                    
                    foreach (Stimulsoft.Report.Dictionary.StiSqlDatabase item in report.CompiledReport.Dictionary.Databases)
                    {
                        item.ConnectionString = BusLib.Configuration.BOConfiguration.ConnectionString;                        
                    }
                    report["KAPANNAME"] = StrKapan;
                    report["FROMDATE"] = StrFromDate;
                    report["TODATE"] = StrToDate ;
                    report["CURREMPLOYEE_ID"] = StrCurrEmployee;
                    report["MANAGER"] = StrManager;

                    StiSqlDatabase sql = new StiSqlDatabase("Connection", BusLib.Configuration.BOConfiguration.ConnectionString);
                    sql.Alias = "Connection";
                    report.CompiledReport.Dictionary.Databases.Clear();
                    report.CompiledReport.Dictionary.Databases.Add(sql);
                    report.PreviewMode = StiPreviewMode.StandardAndDotMatrix;
                    report.Render(false);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelProgress.Visible = false;
                    
                if (RbtSizeWisePolishReport.Checked)
                {
                    PanelProgress.Visible = false;
                    if (DtabSizeWisePol.Rows.Count <= 0)
                    {
                        Global.Message("No Data Found");
                        return;
                    }

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("SizeWisePolishReport", DtabSizeWisePol);
                }
                else if (RbtnDetails.Checked)
                {
                    GrdPolishReport.DataSource = DtabFactoryWise;
                }
                else
                {
                   

                    //report.Show();
                    StimulsoftViewer.RepShowForm frm = new StimulsoftViewer.RepShowForm();
                    ObjFormEvent.ObjToDisposeList.Add(frm);
                    frm.No_of_Copy = 1;
                    frm.repViewer.Report = report;
                    frm.MdiParent = Global.gMainRef;
                    frm.Show();
                    
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void FrmKapanFactoryWisePolishReport_Load(object sender, EventArgs e)
        {

        }

        private void RbtFactoryWise_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtFactoryWise.Checked == true)
            {
                RbtnSummery.Visible = true;
                RbtnDetails.Visible = true;
                GrdPolishReport.Visible = false;
                lblCurrEmp.Visible = true;
                txtCurrEmp.Visible = true;
            }
            else if (RbtFinalPolish4PWise.Checked == true)
            {
                RbtnDetails.Visible = false;
                RbtnSummery.Visible = false;
                GrdPolishReport.Visible = false;
                RbtnDetails.Checked = false;
                RbtnSummery.Checked = false;
                lblCurrEmp.Visible = false;
                txtCurrEmp.Visible = false;
            }
            else if (RbtSizeWisePolishReport.Checked == true)
            {
                RbtnDetails.Visible = false;
                RbtnSummery.Visible = false;
                GrdPolishReport.Visible = false;
                RbtnDetails.Checked = false;
                RbtnSummery.Checked = false;
                lblCurrEmp.Visible = false;
                txtCurrEmp.Visible = false;
            }
            txtCurrEmp.Text = string.Empty;
            txtCurrEmp.Tag = string.Empty;
        }

        private void cLabel1_Click(object sender, EventArgs e)
        {

        }

        private void RbtnSummery_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtnSummery.Checked)
            {
                GrdPolishReport.Visible = false;
            }
            else if (RbtnDetails.Checked)
            {
                GrdPolishReport.Visible = true;
            }
        }


        private void lblExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog SvDialog = new SaveFileDialog();
                SvDialog.DefaultExt = ".xlsx";
                SvDialog.Title = "Export to Excel";
                SvDialog.FileName = "FactoryWisePolishDetail";
                SvDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((SvDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = GrdPolishReport,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(SvDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [FactoryWisePolishDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(SvDialog.FileName, "CMD");
                    }
                }
                SvDialog.Dispose();
                SvDialog = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("FactoryWisePolishDetail :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void txtCurrEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEECODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtCurrEmp.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtCurrEmp.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void TxtManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEECODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        TxtManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        TxtManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
