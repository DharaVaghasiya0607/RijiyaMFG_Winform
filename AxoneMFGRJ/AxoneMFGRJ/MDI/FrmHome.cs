using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.Data;
using DevExpress.XtraCharts;
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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmHome : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughMIS ObjRoughMIS = new BOTRN_RoughMIS();

        #region Property Settings

        public FrmHome()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();


            //DataTable DTabInvice = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_IMPORT);

            //CmbInvoiceCompare.Properties.DataSource = DTabInvice;
            //CmbInvoiceCompare.Properties.DisplayMember = "INVOICENAME";
            //CmbInvoiceCompare.Properties.ValueMember = "INVOICE_ID";

            //DataTable DTabKapan = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPAN);

            //CmbKapanCompare.Properties.DataSource = DTabKapan;
            //CmbKapanCompare.Properties.DisplayMember = "KAPANNAME";
            //CmbKapanCompare.Properties.ValueMember = "KAPAN_ID";
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);            
        }

        #endregion

        private void BtnPacketPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKapan.Text.Length == 0)
                {
                    Global.Message("Kapan Name Is Required");
                    txtKapan.Focus();
                    return;
                }
                if (txtProcess.Text.Length == 0)
                {
                    Global.Message("Process Name Is Required");
                    txtProcess.Focus();
                    return;
                }
                if (Global.Confirm("Are You Print The Lotting Print ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string StrRough = Val.Trim(txtKapan.Tag);

                DataTable DTabPrint = new BOTRN_PacketCreate().GetLottingPrintData(StrRough, Val.ToInt(txtProcess.Tag));

                Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();

                FrmReportViewer.ShowForm("FinalPacketPrint", DTabPrint);
                this.Cursor = Cursors.Default;
            }
            catch (System.Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }

        private void txtProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);

                    FrmSearch.mColumnsToHide = "PROCESS_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                    }
                    else
                    {
                        txtProcess.Text = Val.ToString(DBNull.Value);
                        txtProcess.Tag = Val.ToString(DBNull.Value);
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

        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANMIX);

                    FrmSearch.mColumnsToHide = "KAPAN_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapan.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
                    }
                    else
                    {
                        txtKapan.Text = Val.ToString(DBNull.Value);
                        txtKapan.Tag = Val.ToString(DBNull.Value);
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

        private void BtnShowAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtKapan.Text.Length == 0)
                //{
                //    Global.Message("Kapan Name Is Required");
                //    txtKapan.Focus();
                //    return;
                //}
                //if (txtProcess.Text.Length == 0)
                //{
                //    Global.Message("Process Name Is Required");
                //    txtProcess.Focus();
                //    return;
                //}


                this.Cursor = Cursors.WaitCursor;

                string StrRough = Val.Trim(txtKapan.Tag);
                string StrInvoiceID = Val.Trim(txtInvoice.Tag);
                string StrProcess = Val.Trim(txtProcess.Tag);

                DataSet DS = ObjRoughMIS.GetRoughMISAnalysis(StrInvoiceID, StrRough, StrProcess);

                MainGrd.DataSource = DS.Tables[0];
                MainGrd.RefreshDataSource();
                GrdDet.BestFitColumns();

                MainGridParty.DataSource = DS.Tables[1];
                MainGridParty.RefreshDataSource();

                GrdDetParty.Columns["PROCESS"].Group();
                GrdDetParty.Columns["PROCESS"].Visible = false;
                GrdDetParty.ExpandAllGroups();

                GrdDetParty.GroupSummary.Add(SummaryItemType.Sum, "ISSUEPCS", GrdDetParty.Columns["ISSUEPCS"], "{0:N0}");
                GrdDetParty.GroupSummary.Add(SummaryItemType.Sum, "ISSUECARAT", GrdDetParty.Columns["ISSUECARAT"], "{0:N2}");
                GrdDetParty.GroupSummary.Add(SummaryItemType.Sum, "READYPCS", GrdDetParty.Columns["READYPCS"], "{0:N0}");
                GrdDetParty.GroupSummary.Add(SummaryItemType.Sum, "READYCARAT", GrdDetParty.Columns["READYCARAT"], "{0:N2}");
                GrdDetParty.GroupSummary.Add(SummaryItemType.Sum, "OSPCS", GrdDetParty.Columns["OSPCS"], "{0:N0}");
                GrdDetParty.GroupSummary.Add(SummaryItemType.Sum, "OSCARAT", GrdDetParty.Columns["OSCARAT"], "{0:N2}");
                GrdDetParty.GroupSummary.Add(SummaryItemType.Custom, "READYPER", GrdDetParty.Columns["READYPER"], "{0:N2}");
                GrdDetParty.GroupSummary.Add(SummaryItemType.Sum, "LABOUR", GrdDetParty.Columns["LABOUR"], "{0:N2}");

                // GrdDetParty.BestFitColumns();
                this.Cursor = Cursors.Default;
            }
            catch (System.Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }


        }

        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            string StrParticulars = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PARAMETER")).ToUpper();
            string StrAvgSize = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "AVGSIZE")).ToUpper();


            if (
                   StrParticulars == "LABOUR" ||
                   StrParticulars == "TOTAL LABOUR" ||
                    StrParticulars.Contains("PADTAR")
               )
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.FloralWhite;
                e.Appearance.BackColor2 = Color.FloralWhite;
            }

            else if (StrAvgSize == "NET PADTAR 84%"
                || StrAvgSize.Contains("MINUS FROM LABOUR")
                || StrAvgSize.Contains("14 %")
                || StrAvgSize.Contains("2 %")
                )
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.FloralWhite;
                e.Appearance.BackColor2 = Color.FloralWhite;
                e.Appearance.ForeColor = Color.FromArgb(192, 0, 0);
            }

            else if (StrAvgSize == "TOTAL ASSORT AMOUNT"
                     || StrAvgSize == "DIFF WITH TOTAL LABOUR"
                     || StrAvgSize == "DIVIDE BY LOTTING PCS"
                      )
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.FloralWhite;
                e.Appearance.BackColor2 = Color.FloralWhite;
            }

            else if (
                    StrParticulars == "SHAPE"
                    || StrParticulars == "PURITY"
                    || StrParticulars == "CHARNI"
                )
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.FromArgb(160, 160, 225);
                e.Appearance.BackColor2 = Color.FromArgb(160, 160, 225);
            }
            else if (
                StrParticulars.Contains("SUMMARY")
                || StrParticulars.Contains("DETAIL")
                 || StrParticulars.Contains("OWNERSHIPS")
                )
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.LightGray;
                e.Appearance.BackColor2 = Color.LightGray;
            }

            else if (StrParticulars.Contains("SHAPE TOTAL")
                || StrParticulars.Contains("PURITY TOTAL")
                || StrParticulars.Contains("CHARNI TOTAL")
                )
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void MNKapanAnalysisPrint_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

            PrinterSettingsUsing pst = new PrinterSettingsUsing();

            PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

            //Lesson2 link = new Lesson2(PrintSystem);
            PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

            GrdDet.OptionsPrint.AutoWidth = true;
            GrdDet.OptionsPrint.UsePrintStyles = true;

            GrdDet.AppearancePrint.Row.Font = new Font("Verdana", 8);
            link.Component = MainGrd;
            link.Landscape = false;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

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

        private void MNKapanAnalysisExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "KapanPartyAnalysis";
                svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;
                    GrdDetParty.ExportToXlsx(Filepath);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("Kapan Analysis", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;



            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Kapan :- " + txtKapan.Text, System.Drawing.Color.Navy, new RectangleF(0, 40, e.Graph.ClientPageSize.Width, 60), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("Verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("Verdana", 8, FontStyle.Bold);
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
            BrickPageNo.Font = new Font("Verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void BtnKapanLiveStockAutoFit_Click(object sender, EventArgs e)
        {
            GrdDetParty.BestFitColumns();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            GrdDetParty.Bands["BandLabour"].Visible = true;
        }

        private void txtInvoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "INVOICENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_IMPORT);

                    FrmSearch.mColumnsToHide = "INVOICE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtInvoice.Text = Val.ToString(FrmSearch.mDRow["INVOICENAME"]);
                        txtInvoice.Tag = Val.ToString(FrmSearch.mDRow["INVOICE_ID"]);
                    }
                    else
                    {
                        txtInvoice.Text = Val.ToString(DBNull.Value);
                        txtInvoice.Tag = Val.ToString(DBNull.Value);
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


    }
}
