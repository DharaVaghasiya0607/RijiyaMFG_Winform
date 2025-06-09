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
    public partial class Frm4PWisePolishReport : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjView = new BOTRN_RunninPossition();
        Stimulsoft.Report.StiReport report = new Stimulsoft.Report.StiReport();
        string StrFromDate = "";
        string StrToDate = "";
        string StrKapan = "";
        #region Property Settings

        public Frm4PWisePolishReport()
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
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = null;
                    StrToDate = null;
                }

                StrKapan = "'" + Val.ToString(CmbKapan.Text) + "'";
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

            try
            {
                //string StrOpe = "";
                //string StrGrdType = "";   //#P : 01-02-2020
                //string StrKapan = CmbKapan.SelectedValue.ToString();


                //string StrFromDate = null;
                //string StrToDate = null;

                //if (DTPFromDate.Checked == true)
                //{
                //    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                //}
                //if (DTPToDate.Checked == true)
                //{
                //    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                //}
                //this.Cursor = Cursors.WaitCursor;
                //DataSet DS = ObjView.KapanDetailReport(StrKapan, StrFromDate, StrToDate);

                //Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                //FrmReportViewer.MdiParent = Global.gMainRef;
                //FrmReportViewer.ShowKapanDetailForm("DetailKapanAnalysis", DS);

                //this.Cursor = Cursors.Default;
                PanelProgress.Visible = true;
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
                report.Load(Application.StartupPath + "\\RPT\\" + "4POKReport" + ".mrt");
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
                PanelProgress.Visible = false;
                report.Show();

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                PanelProgress.Visible = false;
                return;
            }*/
            

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
                //Stimulsoft.Report.StiReport report = new Stimulsoft.Report.StiReport();
                report.Load(Application.StartupPath + "\\RPT\\" + "4PWisePolishReport" + ".mrt");
                report.Compile();
                report.RequestParameters = false;
                foreach (Stimulsoft.Report.Dictionary.StiSqlDatabase item in report.CompiledReport.Dictionary.Databases)
                {
                    item.ConnectionString = BusLib.Configuration.BOConfiguration.ConnectionString;
                }
                report["KAPANNAME"] = StrKapan;
                report["FROMDATE"] = StrFromDate;
                report["TODATE"] = StrToDate;

                StiSqlDatabase sql = new StiSqlDatabase("Connection", BusLib.Configuration.BOConfiguration.ConnectionString);
                sql.Alias = "Connection";
                report.CompiledReport.Dictionary.Databases.Clear();
                report.CompiledReport.Dictionary.Databases.Add(sql);
                //report.CompiledReport.DataSources("Rpt_DonationReceipt").Parameters("@KAPANNAME").ParameterValue = Val.ToString(CmbKapan.Text);
                report.PreviewMode = StiPreviewMode.StandardAndDotMatrix;
                report.Render(false);

                //report.PreviewMode = StiPreviewMode.StandardAndDotMatrix;
                //report.Render(false);
                ////PanelProgress.Visible = false;
                //report.Show();
               
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
                //report.Show();
                StimulsoftViewer.RepShowForm frm = new StimulsoftViewer.RepShowForm();
                ObjFormEvent.ObjToDisposeList.Add(frm);
                frm.No_of_Copy = 1;
                frm.repViewer.Report = report;
                frm.MdiParent = Global.gMainRef;
                frm.Show();
            }
            catch(Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


    }
}
