using BusLib.Configuration;
using BusLib.ReportGrid;
using BusLib.Transaction;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmMfgPrdComp : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_GradingAnalysis Obj = new BOTRN_GradingAnalysis();
        DataSet DS = new DataSet();

        string StrFromDate = "";
        string StrToDate = "";
        string StrType = "";
        string StrLab = "";
        string StrMonthly = "";
        string StrGroup = "";
        string StrKapan = "";

        public FrmMfgPrdComp()
        {
            InitializeComponent();
        }

        string mStrReportTitle = "";

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
            CmbKapan.Focus();

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;

            CmbType.SelectedIndex = 0;

        }
        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(Obj);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void FrmGridingAnalysis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnRefresh.PerformClick();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DS = Obj.GetDataForMFGPrdComparision(StrFromDate, StrToDate, StrType, StrKapan);
            }
            catch (Exception ex)
            {
                progressPanel1.Visible = false;
                BtnRefresh.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                progressPanel1.Visible = true;

                DataTable DTabActData = DS.Tables[0];
                DTabActData.Rows.Clear();
                DataTable DTabPrdData = DS.Tables[1];

                for (int i = 0; i < DTabPrdData.Rows.Count; i++)
                {

                    string StrKapanName = DTabPrdData.Rows[i]["KAPANNAME"].ToString();
                    string StrPacketNo = DTabPrdData.Rows[i]["PACKETNO"].ToString();
                    string StrTag = DTabPrdData.Rows[i]["TAG"].ToString();
                    string StrPrdType = DTabPrdData.Rows[i]["PRDTYPE_ID"].ToString();

                    string StrQry = "KAPANNAME = '" + StrKapanName + "' and PACKETNO = '" + StrPacketNo + "' AND TAG = '" + StrTag + "' ";
                    DataRow[] DRow = DTabActData.Select(StrQry);
                    if (DRow != null && DRow.Length == 1)
                    {
                        int SelectedIndex = 0;
                        if (DRow.Length > 0)
                            SelectedIndex = DTabActData.Rows.IndexOf(DRow[0]);

                        if (StrPrdType == "7")
                        {

                            DTabActData.Rows[SelectedIndex]["KAPANNAME"] = DTabPrdData.Rows[i]["KAPANNAME"];
                            DTabActData.Rows[SelectedIndex]["PACKETNO"] = DTabPrdData.Rows[i]["PACKETNO"];
                            DTabActData.Rows[SelectedIndex]["TAG"] = DTabPrdData.Rows[i]["TAG"];
                            DTabActData.Rows[SelectedIndex]["TAGPOLISH"] = DTabPrdData.Rows[i]["TAG"];
                            DTabActData.Rows[SelectedIndex]["EMPCODEPOLISH"] = DTabPrdData.Rows[i]["EMPCODE"];
                            DTabActData.Rows[SelectedIndex]["SHPCODEPOLISH"] = DTabPrdData.Rows[i]["SHPCODE"];
                            DTabActData.Rows[SelectedIndex]["COLCODEPOLISH"] = DTabPrdData.Rows[i]["COLCODE"];
                            DTabActData.Rows[SelectedIndex]["CLACODEPOLISH"] = DTabPrdData.Rows[i]["CLACODE"];
                            DTabActData.Rows[SelectedIndex]["CUTCODEPOLISH"] = DTabPrdData.Rows[i]["CUTCODE"];
                            DTabActData.Rows[SelectedIndex]["POLCODEPOLISH"] = DTabPrdData.Rows[i]["POLCODE"];
                            DTabActData.Rows[SelectedIndex]["SYMCODEPOLISH"] = DTabPrdData.Rows[i]["SYMCODE"];
                            DTabActData.Rows[SelectedIndex]["FLCODEPOLISH"] = DTabPrdData.Rows[i]["FLCODE"];
                            DTabActData.Rows[SelectedIndex]["CARATPOLISH"] = DTabPrdData.Rows[i]["CARAT"];
                            DTabActData.Rows[SelectedIndex]["RAPAPORTPOLISH"] = DTabPrdData.Rows[i]["RAPAPORT"];
                            DTabActData.Rows[SelectedIndex]["DISCOUNTPOLISH"] = DTabPrdData.Rows[i]["DISCOUNT"];
                            DTabActData.Rows[SelectedIndex]["PRICEPERCARATPOLISH"] = DTabPrdData.Rows[i]["PRICEPERCARAT"];
                            DTabActData.Rows[SelectedIndex]["AMOUNTPOLISH"] = DTabPrdData.Rows[i]["AMOUNT"];
                        }
                        else if (StrPrdType == "8")
                        {
                            DTabActData.Rows[SelectedIndex]["KAPANNAME"] = DTabPrdData.Rows[i]["KAPANNAME"];
                            DTabActData.Rows[SelectedIndex]["PACKETNO"] = DTabPrdData.Rows[i]["PACKETNO"];
                            DTabActData.Rows[SelectedIndex]["TAG"] = DTabPrdData.Rows[i]["TAG"];
                            DTabActData.Rows[SelectedIndex]["TAGGRAD"] = DTabPrdData.Rows[i]["TAG"];
                            DTabActData.Rows[SelectedIndex]["EMPCODEGRAD"] = DTabPrdData.Rows[i]["EMPCODE"];
                            DTabActData.Rows[SelectedIndex]["SHPCODEGRAD"] = DTabPrdData.Rows[i]["SHPCODE"];
                            DTabActData.Rows[SelectedIndex]["COLCODEGRAD"] = DTabPrdData.Rows[i]["COLCODE"];
                            DTabActData.Rows[SelectedIndex]["CLACODEGRAD"] = DTabPrdData.Rows[i]["CLACODE"];
                            DTabActData.Rows[SelectedIndex]["CUTCODEGRAD"] = DTabPrdData.Rows[i]["CUTCODE"];
                            DTabActData.Rows[SelectedIndex]["POLCODEGRAD"] = DTabPrdData.Rows[i]["POLCODE"];
                            DTabActData.Rows[SelectedIndex]["SYMCODEGRAD"] = DTabPrdData.Rows[i]["SYMCODE"];
                            DTabActData.Rows[SelectedIndex]["FLCODEGRAD"] = DTabPrdData.Rows[i]["FLCODE"];
                            DTabActData.Rows[SelectedIndex]["CARATGRAD"] = DTabPrdData.Rows[i]["CARAT"];
                            DTabActData.Rows[SelectedIndex]["RAPAPORTGRAD"] = DTabPrdData.Rows[i]["RAPAPORT"];
                            DTabActData.Rows[SelectedIndex]["DISCOUNTGRAD"] = DTabPrdData.Rows[i]["DISCOUNT"];
                            DTabActData.Rows[SelectedIndex]["PRICEPERCARATGRAD"] = DTabPrdData.Rows[i]["PRICEPERCARAT"];
                            DTabActData.Rows[SelectedIndex]["AMOUNTGRAD"] = DTabPrdData.Rows[i]["AMOUNT"];
                        }
                        else if (StrPrdType == "9")
                        {
                            DTabActData.Rows[SelectedIndex]["KAPANNAME"] = DTabPrdData.Rows[i]["KAPANNAME"];
                            DTabActData.Rows[SelectedIndex]["PACKETNO"] = DTabPrdData.Rows[i]["PACKETNO"];
                            DTabActData.Rows[SelectedIndex]["TAG"] = DTabPrdData.Rows[i]["TAG"];
                            DTabActData.Rows[SelectedIndex]["TAGMGRAD"] = DTabPrdData.Rows[i]["TAG"];
                            DTabActData.Rows[SelectedIndex]["EMPCODEMGRAD"] = DTabPrdData.Rows[i]["EMPCODE"];
                            DTabActData.Rows[SelectedIndex]["SHPCODEMGRAD"] = DTabPrdData.Rows[i]["SHPCODE"];
                            DTabActData.Rows[SelectedIndex]["COLCODEMGRAD"] = DTabPrdData.Rows[i]["COLCODE"];
                            DTabActData.Rows[SelectedIndex]["CLACODEMGRAD"] = DTabPrdData.Rows[i]["CLACODE"];
                            DTabActData.Rows[SelectedIndex]["CUTCODEMGRAD"] = DTabPrdData.Rows[i]["CUTCODE"];
                            DTabActData.Rows[SelectedIndex]["POLCODEMGRAD"] = DTabPrdData.Rows[i]["POLCODE"];
                            DTabActData.Rows[SelectedIndex]["SYMCODEMGRAD"] = DTabPrdData.Rows[i]["SYMCODE"];
                            DTabActData.Rows[SelectedIndex]["FLCODEMGRAD"] = DTabPrdData.Rows[i]["FLCODE"];
                            DTabActData.Rows[SelectedIndex]["CARATMGRAD"] = DTabPrdData.Rows[i]["CARAT"];
                            DTabActData.Rows[SelectedIndex]["RAPAPORTMGRAD"] = DTabPrdData.Rows[i]["RAPAPORT"];
                            DTabActData.Rows[SelectedIndex]["DISCOUNTMGRAD"] = DTabPrdData.Rows[i]["DISCOUNT"];
                            DTabActData.Rows[SelectedIndex]["PRICEPERCARATMGRAD"] = DTabPrdData.Rows[i]["PRICEPERCARAT"];
                            DTabActData.Rows[SelectedIndex]["AMOUNTMGRAD"] = DTabPrdData.Rows[i]["AMOUNT"];
                        }
                        else if (StrPrdType == "11")
                        {
                            DTabActData.Rows[SelectedIndex]["KAPANNAME"] = DTabPrdData.Rows[i]["KAPANNAME"];
                            DTabActData.Rows[SelectedIndex]["PACKETNO"] = DTabPrdData.Rows[i]["PACKETNO"];
                            DTabActData.Rows[SelectedIndex]["TAG"] = DTabPrdData.Rows[i]["TAG"];
                            DTabActData.Rows[SelectedIndex]["TAGMLABGRAD"] = DTabPrdData.Rows[i]["TAG"];
                            DTabActData.Rows[SelectedIndex]["EMPCODELABGRAD"] = DTabPrdData.Rows[i]["EMPCODE"];
                            DTabActData.Rows[SelectedIndex]["SHPCODELABGRAD"] = DTabPrdData.Rows[i]["SHPCODE"];
                            DTabActData.Rows[SelectedIndex]["COLCODELABGRAD"] = DTabPrdData.Rows[i]["COLCODE"];
                            DTabActData.Rows[SelectedIndex]["CLACODELABGRAD"] = DTabPrdData.Rows[i]["CLACODE"];
                            DTabActData.Rows[SelectedIndex]["CUTCODELABGRAD"] = DTabPrdData.Rows[i]["CUTCODE"];
                            DTabActData.Rows[SelectedIndex]["POLCODELABGRAD"] = DTabPrdData.Rows[i]["POLCODE"];
                            DTabActData.Rows[SelectedIndex]["SYMCODELABGRAD"] = DTabPrdData.Rows[i]["SYMCODE"];
                            DTabActData.Rows[SelectedIndex]["FLCODELABGRAD"] = DTabPrdData.Rows[i]["FLCODE"];
                            DTabActData.Rows[SelectedIndex]["CARATLABGRAD"] = DTabPrdData.Rows[i]["CARAT"];
                            DTabActData.Rows[SelectedIndex]["RAPAPORTLABGRAD"] = DTabPrdData.Rows[i]["RAPAPORT"];
                            DTabActData.Rows[SelectedIndex]["DISCOUNTLABGRAD"] = DTabPrdData.Rows[i]["DISCOUNT"];
                            DTabActData.Rows[SelectedIndex]["PRICEPERCARATLABGRAD"] = DTabPrdData.Rows[i]["PRICEPERCARAT"];
                            DTabActData.Rows[SelectedIndex]["AMOUNTLABGRAD"] = DTabPrdData.Rows[i]["AMOUNT"];
                        }
                    }
                    else
                    {
                        DataRow DRNew = DTabActData.NewRow();
                        if (StrPrdType == "7")
                        {

                            DRNew["KAPANNAME"] = DTabPrdData.Rows[i]["KAPANNAME"];
                            DRNew["PACKETNO"] = DTabPrdData.Rows[i]["PACKETNO"];
                            DRNew["TAG"] = DTabPrdData.Rows[i]["TAG"];
                            DRNew["TAGPOLISH"] = DTabPrdData.Rows[i]["TAG"];
                            DRNew["EMPCODEPOLISH"] = DTabPrdData.Rows[i]["EMPCODE"];
                            DRNew["SHPCODEPOLISH"] = DTabPrdData.Rows[i]["SHPCODE"];
                            DRNew["COLCODEPOLISH"] = DTabPrdData.Rows[i]["COLCODE"];
                            DRNew["CLACODEPOLISH"] = DTabPrdData.Rows[i]["CLACODE"];
                            DRNew["CUTCODEPOLISH"] = DTabPrdData.Rows[i]["CUTCODE"];
                            DRNew["POLCODEPOLISH"] = DTabPrdData.Rows[i]["POLCODE"];
                            DRNew["SYMCODEPOLISH"] = DTabPrdData.Rows[i]["SYMCODE"];
                            DRNew["FLCODEPOLISH"] = DTabPrdData.Rows[i]["FLCODE"];
                            DRNew["CARATPOLISH"] = DTabPrdData.Rows[i]["CARAT"];
                            DRNew["RAPAPORTPOLISH"] = DTabPrdData.Rows[i]["RAPAPORT"];
                            DRNew["DISCOUNTPOLISH"] = DTabPrdData.Rows[i]["DISCOUNT"];
                            DRNew["PRICEPERCARATPOLISH"] = DTabPrdData.Rows[i]["PRICEPERCARAT"];
                            DRNew["AMOUNTPOLISH"] = DTabPrdData.Rows[i]["AMOUNT"];
                        }
                        else if (StrPrdType == "8")
                        {
                            DRNew["KAPANNAME"] = DTabPrdData.Rows[i]["KAPANNAME"];
                            DRNew["PACKETNO"] = DTabPrdData.Rows[i]["PACKETNO"];
                            DRNew["TAG"] = DTabPrdData.Rows[i]["TAG"];
                            DRNew["TAGGRAD"] = DTabPrdData.Rows[i]["TAG"];
                            DRNew["EMPCODEGRAD"] = DTabPrdData.Rows[i]["EMPCODE"];
                            DRNew["SHPCODEGRAD"] = DTabPrdData.Rows[i]["SHPCODE"];
                            DRNew["COLCODEGRAD"] = DTabPrdData.Rows[i]["COLCODE"];
                            DRNew["CLACODEGRAD"] = DTabPrdData.Rows[i]["CLACODE"];
                            DRNew["CUTCODEGRAD"] = DTabPrdData.Rows[i]["CUTCODE"];
                            DRNew["POLCODEGRAD"] = DTabPrdData.Rows[i]["POLCODE"];
                            DRNew["SYMCODEGRAD"] = DTabPrdData.Rows[i]["SYMCODE"];
                            DRNew["FLCODEGRAD"] = DTabPrdData.Rows[i]["FLCODE"];
                            DRNew["CARATGRAD"] = DTabPrdData.Rows[i]["CARAT"];
                            DRNew["RAPAPORTGRAD"] = DTabPrdData.Rows[i]["RAPAPORT"];
                            DRNew["DISCOUNTGRAD"] = DTabPrdData.Rows[i]["DISCOUNT"];
                            DRNew["PRICEPERCARATGRAD"] = DTabPrdData.Rows[i]["PRICEPERCARAT"];
                            DRNew["AMOUNTGRAD"] = DTabPrdData.Rows[i]["AMOUNT"];
                        }
                        else if (StrPrdType == "9")
                        {
                            DRNew["KAPANNAME"] = DTabPrdData.Rows[i]["KAPANNAME"];
                            DRNew["PACKETNO"] = DTabPrdData.Rows[i]["PACKETNO"];
                            DRNew["TAG"] = DTabPrdData.Rows[i]["TAG"];
                            DRNew["TAGMGRAD"] = DTabPrdData.Rows[i]["TAG"];
                            DRNew["EMPCODEMGRAD"] = DTabPrdData.Rows[i]["EMPCODE"];
                            DRNew["SHPCODEMGRAD"] = DTabPrdData.Rows[i]["SHPCODE"];
                            DRNew["COLCODEMGRAD"] = DTabPrdData.Rows[i]["COLCODE"];
                            DRNew["CLACODEMGRAD"] = DTabPrdData.Rows[i]["CLACODE"];
                            DRNew["CUTCODEMGRAD"] = DTabPrdData.Rows[i]["CUTCODE"];
                            DRNew["POLCODEMGRAD"] = DTabPrdData.Rows[i]["POLCODE"];
                            DRNew["SYMCODEMGRAD"] = DTabPrdData.Rows[i]["SYMCODE"];
                            DRNew["FLCODEMGRAD"] = DTabPrdData.Rows[i]["FLCODE"];
                            DRNew["CARATMGRAD"] = DTabPrdData.Rows[i]["CARAT"];
                            DRNew["RAPAPORTMGRAD"] = DTabPrdData.Rows[i]["RAPAPORT"];
                            DRNew["DISCOUNTMGRAD"] = DTabPrdData.Rows[i]["DISCOUNT"];
                            DRNew["PRICEPERCARATMGRAD"] = DTabPrdData.Rows[i]["PRICEPERCARAT"];
                            DRNew["AMOUNTMGRAD"] = DTabPrdData.Rows[i]["AMOUNT"];
                        }
                        else if (StrPrdType == "11")
                        {
                            DRNew["KAPANNAME"] = DTabPrdData.Rows[i]["KAPANNAME"];
                            DRNew["PACKETNO"] = DTabPrdData.Rows[i]["PACKETNO"];
                            DRNew["TAG"] = DTabPrdData.Rows[i]["TAG"];
                            DRNew["TAGMLABGRAD"] = DTabPrdData.Rows[i]["TAG"];
                            DRNew["EMPCODELABGRAD"] = DTabPrdData.Rows[i]["EMPCODE"];
                            DRNew["SHPCODELABGRAD"] = DTabPrdData.Rows[i]["SHPCODE"];
                            DRNew["COLCODELABGRAD"] = DTabPrdData.Rows[i]["COLCODE"];
                            DRNew["CLACODELABGRAD"] = DTabPrdData.Rows[i]["CLACODE"];
                            DRNew["CUTCODELABGRAD"] = DTabPrdData.Rows[i]["CUTCODE"];
                            DRNew["POLCODELABGRAD"] = DTabPrdData.Rows[i]["POLCODE"];
                            DRNew["SYMCODELABGRAD"] = DTabPrdData.Rows[i]["SYMCODE"];
                            DRNew["FLCODELABGRAD"] = DTabPrdData.Rows[i]["FLCODE"];
                            DRNew["CARATLABGRAD"] = DTabPrdData.Rows[i]["CARAT"];
                            DRNew["RAPAPORTLABGRAD"] = DTabPrdData.Rows[i]["RAPAPORT"];
                            DRNew["DISCOUNTLABGRAD"] = DTabPrdData.Rows[i]["DISCOUNT"];
                            DRNew["PRICEPERCARATLABGRAD"] = DTabPrdData.Rows[i]["PRICEPERCARAT"];
                            DRNew["AMOUNTLABGRAD"] = DTabPrdData.Rows[i]["AMOUNT"];
                        }
                        DTabActData.Rows.Add(DRNew);
                    }

                }

                MainGrid.DataSource = DTabActData;
                GrdDet.RefreshData();

                progressPanel1.Visible = false;
                BtnRefresh.Enabled = true;

            }
            catch (Exception ex)
            {
                progressPanel1.Visible = false;
                BtnRefresh.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }


        #region EXEL-EXPORT AND PRINT FUNCTIONS

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            //// ' For Report Title

            //TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            //BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            //// ' For Group 
            //TextBrick BrickTitleseller = e.Graph.DrawString(mStrReportTitle, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            //BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitleseller.ForeColor = Color.Black;

            //string StrFilter = "FromDate : " + DTPFromDate.Text + " To " + DTPToDate.Text;

            ////// ' For Filter 
            //TextBrick BrickTitlesParam = e.Graph.DrawString(StrFilter, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            //BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitlesParam.ForeColor = Color.Black;


            //int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            //TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            //BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            //BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitledate.ForeColor = Color.Black;

        }

        public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
        {
            //string StrName = "Griding Analysis ["+xtraTabControl1.SelectedTabPage.Text.Trim()+"]";;

            //TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            //BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            //// ' For Group 
            //TextBrick BrickTitleseller = e.Graph.DrawString(StrName, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            //BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitleseller.ForeColor = Color.Black;

            //int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            //TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            //BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            //BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitledate.ForeColor = Color.Black;

        }

        public void Link_CreateMarginalFooterAreaSummary(object sender, CreateAreaEventArgs e)
        {
            //int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            //PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            //BrickPageNo.LineAlignment = BrickAlignment.Far;
            //BrickPageNo.Alignment = BrickAlignment.Far;
            //// BrickPageNo.AutoWidth = true;
            //BrickPageNo.Font = new Font("verdana", 8, FontStyle.Bold); ;
            //BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            //BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //SaveFileDialog svDialog = new SaveFileDialog();
                //svDialog.DefaultExt = ".xlsx";
                //svDialog.Title = "Export to Excel";
                //svDialog.FileName = "GridingAnalysis("+xtraTabControl1.SelectedTabPage.Text.Trim()+").xlsx";
                //svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                //if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                //{
                //    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                //    {
                //        PrintingSystemBase = new PrintingSystemBase(),
                //        Component = MainGrdCut,
                //        Landscape = true,
                //        PaperKind = PaperKind.A4,
                //        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                //    };

                //    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                //    link.ExportToXlsx(svDialog.FileName);

                //    if (Global.Confirm("Do You Want To Open [Griding Analysis(" + xtraTabControl1.SelectedTabPage.Text.Trim() + ").xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                //    {
                //        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                //    }
                //}
                //svDialog.Dispose();
                //svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

            //    PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

            //    //string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

            //    link.Component = MainGrdCut;
            //    link.Landscape = true;

            //    link.Margins.Left = 10;
            //    link.Margins.Right = 10;
            //    link.Margins.Bottom = 40;
            //    link.Margins.Top = 130;

            //    link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);
            //    link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterAreaSummary);
            //    link.CreateDocument();
            //    link.ShowPreview();
            //    link.PrintDlg();


            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message.ToString());
            //}
        }
        #endregion


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            progressPanel1.Visible = true;

            StrFromDate = Val.SqlDate(DTPFromDate.Text);
            StrToDate = Val.SqlDate(DTPToDate.Text);

            if (Val.ToString(CmbType.Text) == "POLISH CHECKER PREDICTION")
                StrType = "7";
            if (Val.ToString(CmbType.Text) == "GRADING")
                StrType = "8";
            if (Val.ToString(CmbType.Text) == "MUMBAI GRADING")
                StrType = "9";
            if (Val.ToString(CmbType.Text) == "LAB GRD")
                StrType = "11";

            StrKapan = CmbKapan.Text;

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;

            CmbKapan.Properties.Items.Clear();
            CmbKapan.Text = "";

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
            CmbKapan.Focus();

            CmbType.SelectedIndex = 0;
            MainGrid.DataSource = null;
            GrdDet.RefreshData();

        }

        private void ChkPcs_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkGrading.Checked == false)
                gridBand2.Visible = false;
            else
                gridBand2.Visible = true;
        }

        private void ChkMumbaiGrading_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkMumbaiGrading.Checked == false)
                gridBand3.Visible = false;
            else
                gridBand3.Visible = true;
        }

        private void ChkLabGrading_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkLabGrading.Checked == false)
                gridBand4.Visible = false;
            else
                gridBand4.Visible = true;
        }

        private void MainGrid_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
        }


    }
}
