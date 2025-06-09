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

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmKapanDetailReport : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjView = new BOTRN_RunninPossition();

        #region Property Settings

        public FrmKapanDetailReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            //CmbKapan.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            //CmbKapan.DisplayMember = "KAPANNAME";
            //CmbKapan.ValueMember = "KAPANNAME";
            //CmbKapan.Focus();
            //CmbKapan.SelectedIndex = -1;

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            chkCmbPacketCat.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PACKETCATEGORY);
            chkCmbPacketCat.Properties.DisplayMember = "PACKETCATEGORYNAME";
            chkCmbPacketCat.Properties.ValueMember = "PACKETCATEGORY_ID";

            ChkCmbPacketGroup.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PACKETGROUP);
            ChkCmbPacketGroup.Properties.DisplayMember = "PACKETGROUPNAME";
            ChkCmbPacketGroup.Properties.ValueMember = "PACKETGROUP_ID";

            CmbKapan.Focus();


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
                string StrOpe ; 
                string StrGrdType = "";   //#P : 01-02-2020
                string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems()); //CmbKapan.SelectedValue.ToString();
                string mStrPacketCategory = Val.Trim(chkCmbPacketCat.Properties.GetCheckedItems());
                string mStrPacketGroup = Val.Trim(ChkCmbPacketGroup.Properties.GetCheckedItems());

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

                if (txtMultiMainManager.Text.ToString().Trim().Equals(string.Empty))
                {
                    txtMultiMainManager.Tag = string.Empty;
                }


                if (RbtKapanDetailReport.Checked)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataSet DS = ObjView.KapanDetailReport(StrKapan, StrFromDate, StrToDate,Val.Trim(txtMultiMainManager.Tag).ToString(), mStrPacketCategory, mStrPacketGroup);

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowKapanDetailForm("DetailKapanAnalysis", DS);
                }
                else if (RbtKapanFinalSummaryReport.Checked)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataSet DS = ObjView.KapanFinalSummaryReport(StrKapan, StrFromDate, StrToDate, Val.Trim(txtMultiMainManager.Tag).ToString());

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowKapanFinalSummaryForm("KapanFinalSummary", DS);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                return;
            }


        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDirectPDFExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "pdf";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "KapanDetailReport";
                svDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());  //CmbKapan.SelectedValue.ToString();
                    string mStrPacketCategory = Val.Trim(chkCmbPacketCat.Properties.GetCheckedItems());
                    string mStrPacketGroup = Val.Trim(ChkCmbPacketGroup.Properties.GetCheckedItems());

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
                    DataSet DS = ObjView.KapanDetailReport(StrKapan, StrFromDate, StrToDate, Val.Trim(txtMultiMainManager.Tag).ToString(), mStrPacketCategory, mStrPacketGroup);

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
        }

        private void txtMultiMainManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_MAINMANAGER);
                    FrmSearch.mStrColumnsToHide = "LEDGER_ID,AUTOCONFIRM";
                    FrmSearch.ValueMemeter = "LEDGER_ID";
                    FrmSearch.DisplayMemeter = "LEDGERCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtMultiMainManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtMultiMainManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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
