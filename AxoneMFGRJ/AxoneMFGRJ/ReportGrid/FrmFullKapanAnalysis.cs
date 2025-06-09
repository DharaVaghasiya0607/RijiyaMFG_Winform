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
    public partial class FrmFullKapanAnalysis : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjView = new BOTRN_RunninPossition();



        #region Property Settings

        public FrmFullKapanAnalysis()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

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

            RdbGrdType.SelectedIndex = 0;
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
                string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());
                string mStrPacketCategory = Val.Trim(chkCmbPacketCat.Properties.GetCheckedItems());
                string mStrPacketGroup = Val.Trim(ChkCmbPacketGroup.Properties.GetCheckedItems());

                string StrOpe = "";
                string StrGrdType = "";   //#P : 01-02-2020


                if (RbtAll.Checked == true)
                {
                    StrOpe = "ALL";
                }
                else if (RbtPktCreated.Checked == true)
                {
                    StrOpe = "CREATED";
                }
                else if (RbtPktNotCreated.Checked == true)
                {
                    StrOpe = "NOTCREATED";
                }


                if (RdbGrdType.SelectedIndex == 0)
                {
                    StrGrdType = "ALL";
                }
                else if (RdbGrdType.SelectedIndex == 1)
                {
                    StrGrdType = "GRD";
                }
                else if (RdbGrdType.SelectedIndex == 2)
                {
                    StrGrdType = "BY";
                }
                else if (RdbGrdType.SelectedIndex == 3)
                {
                    StrGrdType = "LAB";
                }

                string StrFromDate = null;
                string StrToDate = null;
                string StrMainManager_ID = "";

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                StrMainManager_ID = Val.ToString(txtMultiMainManager.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtMultiMainManager.Tag);

                this.Cursor = Cursors.WaitCursor;
                DataSet DS = ObjView.FullKapanAnalysis(StrKapan, StrOpe, StrFromDate, StrToDate, Val.ToBoolean(ChkWithPCN.Checked), StrGrdType, StrMainManager_ID, mStrPacketCategory, mStrPacketGroup);


                if (RbtFullKapanAnalysis.Checked == true)
                {
                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("FullKapanAnalysis", DS);
                }
                else if (RbtKapanEstimate.Checked == true)
                {
                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("KapanEstimate", DS);
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
                svDialog.FileName = "KapanAnalysisReport";
                svDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());
                    string mStrPacketCategory = Val.Trim(chkCmbPacketCat.Properties.GetCheckedItems());
                    string mStrPacketGroup = Val.Trim(ChkCmbPacketGroup.Properties.GetCheckedItems());

                    string StrOpe = "";
                    string StrGrdType = "";   //#P : 01-02-2020

                    if (RbtAll.Checked == true)
                    {
                        StrOpe = "ALL";
                    }
                    else if (RbtPktCreated.Checked == true)
                    {
                        StrOpe = "CREATED";
                    }
                    else if (RbtPktNotCreated.Checked == true)
                    {
                        StrOpe = "NOTCREATED";
                    }



                    if (RdbGrdType.SelectedIndex == 0)
                    {
                        StrGrdType = "ALL";
                    }
                    else if (RdbGrdType.SelectedIndex == 1)
                    {
                        StrGrdType = "GRD";
                    }
                    else if (RdbGrdType.SelectedIndex == 2)
                    {
                        StrGrdType = "BY";
                    }
                    else if (RdbGrdType.SelectedIndex == 3)
                    {
                        StrGrdType = "LAB";
                    }

                    string StrFromDate = null;
                    string StrToDate = null;
                    string StrMainManager_ID = "";

                    if (DTPFromDate.Checked == true)
                    {
                        StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    }
                    if (DTPToDate.Checked == true)
                    {
                        StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                    }
                    StrMainManager_ID = Val.ToString(txtMultiMainManager.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtMultiMainManager.Tag);

                    this.Cursor = Cursors.WaitCursor;
                    DataSet DS = ObjView.FullKapanAnalysis(StrKapan, StrOpe, StrFromDate, StrToDate, Val.ToBoolean(ChkWithPCN.Checked), StrGrdType, StrMainManager_ID, mStrPacketCategory
                        , mStrPacketGroup);
                    
                    if (RbtFullKapanAnalysis.Checked == true)
                    {
                        Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                        FrmReportViewer.MdiParent = Global.gMainRef;
                        FrmReportViewer.ShowForm("FullKapanAnalysis", DS);
                    }
                    else if (RbtKapanEstimate.Checked == true)
                    {
                        Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                        FrmReportViewer.MdiParent = Global.gMainRef;
                        FrmReportViewer.ShowForm("KapanEstimate", DS);
                    }


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
