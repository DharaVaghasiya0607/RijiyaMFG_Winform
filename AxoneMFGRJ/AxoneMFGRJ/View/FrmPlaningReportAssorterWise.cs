using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Data;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FrmPlaningReportAssorterWise : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();
        DataTable DtabData = new DataTable();

        DataSet DS = new DataSet();
        DataTable DTabSummary = new DataTable();
        DataTable DTabDetail = new DataTable();

        string StrPrdType_ID;
        string mStrOpeViewType = "";
        string StrReportTitle = "";
        string mStrPacketCategory = "";

        System.Diagnostics.Stopwatch watch = null;

        double pDouMainCts = 0;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        string mStrReportTitle = "";
        int mIntProcessID = 0;
        string StrFromDate = "", StrToDate = "";
        string mStrKapan = "";
        string mStrTable = "";
        string mStrMainManage_ID = "";
        string mStrAssorter_ID = "";

        double DouPolCts = 0;
        double DouOrgCts = 0;
        double DouRghCts = 0;

        double DouDetPolCts = 0;
        double DouDetRghCts = 0;

        double DouMkWght = 0;
        double DouMkPcs = 0;
        double DouDMCts = 0;

        double DouKapanPer = 0;
        bool IsNextImage = true;

        string mStrPacketGroup = "";
        //double DouGRate = 0;
        //double DouGAmount = 0;

        //double DouExcRate = 0;
        //double DouMumbaiAmt = 0;

        public FrmPlaningReportAssorterWise()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            xtraTabControl1.SelectedTabPageIndex = 0;

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            // Dhara : 23-03-2023 start

            ChkCmbTable.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_TABLE);
            ChkCmbTable.Properties.DisplayMember = "TABLENAME";
            ChkCmbTable.Properties.ValueMember = "TABLE_ID";

            // Dhara : 23-03-2023 End

            chkCmbPacketCat.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PACKETCATEGORY);
            chkCmbPacketCat.Properties.DisplayMember = "PACKETCATEGORYNAME";
            chkCmbPacketCat.Properties.ValueMember = "PACKETCATEGORY_ID";

            ChkCmbPacketGroup.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PACKETGROUP);
            ChkCmbPacketGroup.Properties.DisplayMember = "PACKETGROUPNAME";
            ChkCmbPacketGroup.Properties.ValueMember = "PACKETGROUP_ID";

            DTPFromDate.Checked = false;
            DTPToDate.Checked = false;            
            CmbViewType.SelectedIndex = 0; // K :10/11/2022
            if (CmbViewType.SelectedIndex == 0) 
            {                
                panelGrid.Visible = false;
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A6;
            }
            
            this.Show();
            RbtFinal_CheckedChanged(null, null);

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
                mStrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());
                mStrTable = Val.Trim(ChkCmbTable.Properties.GetCheckedItems());
                
                mStrPacketCategory = Val.Trim(chkCmbPacketCat.Properties.GetCheckedItems());
                mStrPacketGroup = Val.Trim(ChkCmbPacketGroup.Properties.GetCheckedItems());
                if (mStrKapan.Length == 0)
                {
                    lblMessage.Text = "Kapan Name Is Required";
                    CmbKapan.Focus();
                    return;
                }
                lblMessage.Text = "";
                StrFromDate = "";
                StrToDate = "";

                mStrMainManage_ID = Val.ToString(txtMultiMainManager.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtMultiMainManager.Tag);
                mStrAssorter_ID = Val.ToString(txtAssorter.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtAssorter.Tag);

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = null;
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                else
                {
                    StrToDate = null;
                }


                if (RbtFinal.Checked)
                    StrPrdType_ID = "2";
                else if (RbtMakable.Checked)
                    StrPrdType_ID = "4";

                if (CmbViewType.SelectedIndex == 0) //K : 10/11/2022
                {
                    mStrOpeViewType = "NONE";
                    panelGrid.Visible = false;
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A6;
                }
                else if (CmbViewType.SelectedIndex == 1)
                {
                    mStrOpeViewType = "DEPARTMENT_WISE";
                    panelGrid.Visible = true;
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                    StrReportTitle = "Department Wise Data";
                }
                else if (CmbViewType.SelectedIndex == 2)
                {
                    mStrOpeViewType = "SIZE_WISE";
                    panelGrid.Visible = true;
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                    StrReportTitle = "Size Wise Data";
                }
                else if (CmbViewType.SelectedIndex == 3)
                {
                    mStrOpeViewType = "PURITY_WISE";
                    panelGrid.Visible = true;
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                    StrReportTitle = "Purity Wise Data";
                }
                else if (CmbViewType.SelectedIndex == 4)
                {
                    mStrOpeViewType = "KAPAN_WISE";
                    panelGrid.Visible = true;
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                    StrReportTitle = "Kapan Wise Data";
                }
                else if (CmbViewType.SelectedIndex == 5)
                {
                    mStrOpeViewType = "MARKER_WISE";
                    panelGrid.Visible = true;
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                    StrReportTitle = "Marker Wise Data";
                }
                else if (CmbViewType.SelectedIndex == 6)
                {
                    mStrOpeViewType = "MAINMANAGER_WISE";
                    panelGrid.Visible = true;
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                    StrReportTitle = "Main Manager Wise Data";
                }                
                DTabDetail.Rows.Clear();
                DTabSummary.Rows.Clear();
                GrpViewTypeSearch.Text = StrReportTitle;

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                watch = System.Diagnostics.Stopwatch.StartNew();
                BtnRefresh.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }

        }

        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] 
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }

        private void FrmPolishOKReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnRefresh_Click(null, null);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DS = Obj.PlanningReportAssorterWiseGetData(mStrKapan, mStrTable, StrFromDate, StrToDate, StrPrdType_ID,mStrMainManage_ID, mStrOpeViewType,mStrAssorter_ID, mStrPacketCategory, mStrPacketGroup);

                //DS = Obj.GetProcessWiseReport(Val.Trim(CmbKapan.Properties.GetCheckedItems()), mIntProcessID, Val.ToInt64(txtEmployee.Tag), StrFromDate, StrToDate, ChkIsIssuewise.Checked);
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BtnRefresh.Enabled = true;
                PanelProgress.Visible = false;

                DTabDetail = DS.Tables[0];
                DTabSummary = DS.Tables[1];
                DtabData = DS.Tables[2];

                MainGridFSummary.DataSource = DTabSummary;
                MainGridFSummary.RefreshDataSource();

                MainGridDetail.DataSource = DTabDetail;
                MainGridDetail.RefreshDataSource();

                MaingrdSummary.DataSource = DtabData;
                MaingrdSummary.RefreshDataSource();

                //GrdDetDetail.Columns["ASSORTER"].Group();
                //GrdDetDetail.Columns["ASSORTER"].Visible = true;
                //GrdDetDetail.Columns["ASSORTER"].GroupIndex = 0;

                if (RbtFinal.Checked)
                {
                    GrdDetDetail.Columns["PACKETNO"].Group();
                    GrdDetDetail.Columns["PACKETNO"].Visible = true;
                    GrdDetDetail.Columns["PACKETNO"].GroupIndex = 0;
                    GrdDetDetail.Columns["ASSORTER"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                }
                else
                {
                    GrdDetDetail.Columns["PACKETNO"].Group();
                    GrdDetDetail.Columns["PACKETNO"].Visible = true;
                    GrdDetDetail.Columns["PACKETNO"].GroupIndex = -1;
                    //GrdDetDetail.Columns["ASSORTER"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                }

                //    GrdDetDetail.Columns["PACKETNO"].Visible = true;

                if (GrdDetDetail.GroupSummary.Count == 0)
                {
                    GrdDetDetail.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "PACKETNO", GrdDetDetail.Columns["PACKETNO"], "{0}");
                    GrdDetDetail.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ORIGINALWT", GrdDetDetail.Columns["ORIGINALWT"], "{0:N3}");
                    GrdDetDetail.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "POLISHCTS", GrdDetDetail.Columns["POLISHCTS"], "{0:N3}");
                    GrdDetDetail.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ROUGHWEIGHT", GrdDetDetail.Columns["ROUGHWEIGHT"], "{0:N3}");
                    GrdDetDetail.GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, "ROUGHPOLISHPER", GrdDetDetail.Columns["ROUGHPOLISHPER"], "{0:N3}");
                }

                GrdDetDetail.ExpandAllGroups();


                GrdDetFSummary.BestFitColumns();
                MainGrid.BestFitColumns();

                //GrdSummary.BeginUpdate();
                //GrdJanged.BeginUpdate();
                //GrdDetDetail.BeginUpdate();

                //MainGrdSummary.DataSource = DTabSummary;
                //GrdSummary.RefreshData();

            }
            catch (Exception Ex)
            {
                BtnRefresh.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void GrdDetSummary_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DataRow DR = DTabSummary.Rows[e.RowHandle];
                string AssorterName = Val.ToString(DR["ASSORTERNAME"]).Trim();
                DataTable DTableNew = DTabDetail.Clone();
                DataRow[] rowsToCopy;
                rowsToCopy = DTabDetail.Select("ASSORTER='" + AssorterName + "'");
                foreach (DataRow Row in rowsToCopy)
                {
                    DTableNew.ImportRow(Row);
                }

                MainGridDetail.DataSource = DTableNew;
                MainGridDetail.RefreshDataSource();

                //GrdDetDetail.Columns["ASSORTER"].Group();
                //GrdDetDetail.Columns["ASSORTER"].Visible = false;
                //if (GrdDetDetail.GroupSummary.Count == 0)
                //{
                //    GrdDetDetail.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "PACKETNO", GrdDetDetail.Columns["PACKETNO"], "{0}");
                //    GrdDetDetail.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ORIGINALWT", GrdDetDetail.Columns["ORIGINALWT"], "{0:N3}");
                //    GrdDetDetail.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "POLISHCTS", GrdDetDetail.Columns["POLISHCTS"], "{0:N3}");
                //}
                GrdDetDetail.ExpandAllGroups();
                xtraTabControl1.SelectedTabPageIndex = 1;
            }
        }

        private void lblDeptExport_Click_1(object sender, EventArgs e)
        {

            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                mStrReportTitle = "Planning Report Assoerter Wise (Summary)";
                CommonExcelExportFuction(MainGridFSummary, GrdDetFSummary, "PlanningReportAssorterWise");
            }
            else
            {
                mStrReportTitle = "Planning Report Assoerter Wise (Detail)";
                CommonExcelExportFuction(MainGridDetail, GrdDetDetail, "PlanningReportAssorterWise");
            }
        }

        public void CommonExcelExportFuction(
         DevExpress.XtraGrid.GridControl pMainGrid,
         DevExpress.XtraGrid.Views.BandedGrid.BandedGridView pGrdDet,
          string pStrFileName

         )
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = pStrFileName;
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = pMainGrid,
                        Landscape = false,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [" + pStrFileName + ".xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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


        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            int IntHeight = 0;
            TextBrick BrickCompany = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Black, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickCompany.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickCompany.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickCompany.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickCompany.ForeColor = Color.FromArgb(27, 66, 105);

            IntHeight = IntHeight + 20;
            TextBrick BrickTitle = e.Graph.DrawString(mStrReportTitle, System.Drawing.Color.Navy, new RectangleF(0, IntHeight, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 9, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitle.ForeColor = Color.FromArgb(27, 66, 105);

            IntHeight = IntHeight + 20;
            string StrFilter = "From Date : " + DTPFromDate.Value.ToShortDateString() + " To " + DTPToDate.Value.ToShortDateString() + "\n";
            StrFilter = StrFilter + "Kapan : " + Val.ToString(CmbKapan.Text);
            TextBrick BrickTitlesParam = e.Graph.DrawString(StrFilter, System.Drawing.Color.Navy, new RectangleF(0, IntHeight, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("Verdana", 8, FontStyle.Regular);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            BrickTitlesParam.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, IntHeight, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        private void lblDeptPrint_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                mStrReportTitle = "Planning Report Assorter Wise (Summary)";
                CommonPrintFuction(MainGridFSummary, GrdDetFSummary);
            }
            else
            {
                mStrReportTitle = "Planning Report Assorter Wise (Detail)";
                CommonPrintFuction(MainGridDetail, GrdDetDetail);
            }
        }

        public void CommonPrintFuction(
          DevExpress.XtraGrid.GridControl pMainGrid,
          DevExpress.XtraGrid.Views.BandedGrid.BandedGridView pGrdDet

          )
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = pMainGrid;
                link.Landscape = false;
                GrdDetFSummary.OptionsPrint.AutoWidth = false;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 170;

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

        private void RbtFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtFinal.Checked == true)
            {
                GrdDetFSummary.Bands["BANDFINALPRD"].Visible = true;
                GrdDetFSummary.Bands["BANDMKBLPRD"].Visible = false;
                //LblViewType.Visible = false;
                //CmbViewType.Visible = false;
                //BtnLeft.Visible = false;
                //panelGrid.Visible = false;
                DTabSummary.Rows.Clear();
                DTabDetail.Rows.Clear();
                BtnRefresh.Focus();
            }
            else
            {
                GrdDetFSummary.Bands["BANDFINALPRD"].Visible = false;
                GrdDetFSummary.Bands["BANDMKBLPRD"].Visible = true;
                //LblViewType.Visible = true;
                //CmbViewType.Visible = true;
                //BtnLeft.Visible = true;
                //panelGrid.Visible = true;           
                
                DTabSummary.Rows.Clear();
                DTabDetail.Rows.Clear();
                BtnRefresh.Focus();
            }           
           
        }

        private void GrdDetFSummary_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouPolCts = 0;
                    DouOrgCts = 0;
                    DouRghCts = 0;
                    DouMkWght = 0;
                    DouMkPcs = 0;
                    DouDMCts = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouPolCts = DouPolCts + Val.Val(GrdDetFSummary.GetRowCellValue(e.RowHandle, "POLCARAT"));
                    DouOrgCts = DouOrgCts + Val.Val(GrdDetFSummary.GetRowCellValue(e.RowHandle, "ORIGINALWT"));
                    DouMkWght = DouMkWght + Val.Val(GrdDetFSummary.GetRowCellValue(e.RowHandle, "PRDBALANCECARAT"));
                    DouMkPcs = DouMkPcs + Val.Val(GrdDetFSummary.GetRowCellValue(e.RowHandle, "PCS"));
                    DouDMCts = DouDMCts + Val.Val(GrdDetFSummary.GetRowCellValue(e.RowHandle, "PRDCARAT"));
                    DouRghCts = DouRghCts + Val.Val(GrdDetFSummary.GetRowCellValue(e.RowHandle, "ROUGHWEIGHT"));
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ORIGINALPER") == 0)
                    {
                        if (Val.Val(DouPolCts) > 0)
                            e.TotalValue = Math.Round((Val.Val(DouPolCts) / Val.Val(DouOrgCts) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PRDBALANCEPER") == 0)
                    {
                        if (Val.Val(DouMkWght) > 0)
                            e.TotalValue = Math.Round((Val.Val(DouMkWght) / Val.Val(DouOrgCts) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PRDBALANCESIZE") == 0)
                    {
                        if (Val.Val(DouMkWght) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouMkPcs) / Val.Val(DouMkWght), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PRDPER") == 0)
                    {
                        if (Val.Val(DouMkWght) > 0)
                            e.TotalValue = Math.Round((Val.Val(DouDMCts) / Val.Val(DouMkWght) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PRDKAPANPER") == 0)
                    {
                        if (Val.Val(DouDMCts) > 0)
                            e.TotalValue = Math.Round((Val.Val(DouDMCts) / Val.Val(DouOrgCts) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ROUGHPOLISHPER") == 0)
                    {
                        if (Val.Val(DouRghCts) > 0)
                            e.TotalValue = Math.Round(((Val.Val(DouPolCts) / Val.Val(DouRghCts)) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }
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

        private void BtnLeft_Click(object sender, EventArgs e)//K : 17/11/2022
        {
            if (CmbViewType.SelectedIndex == 0)
            {
                DtabData.Rows.Clear();
                Global.Message("Please Select ViewType.!!");
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A6;
                panelGrid.Visible = false;
                IsNextImage = false;
                return;
            }

            if (IsNextImage)
            {
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A6;
                panelGrid.Visible = false;
                IsNextImage = false;
            }            
            else
            {
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                panelGrid.Visible = true;
                panelGrid.Visible = true;
                IsNextImage = true;
            }            
        }

        private void MainGrid_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)//K : 17/11/2022
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                if (e.Clicks == 2)
                {
                    xtraTabControl1.SelectedTabPageIndex = 1;
                    this.Cursor = Cursors.WaitCursor;
                    this.Cursor = Cursors.WaitCursor;
                    // GrdDetDetail.Columns["CLACODE"].ClearFilter();
                    // GrdDetDetail.Columns["CLACODE"].ClearFilter();
                    GrdDetDetail.Columns["CLACODE"].ClearFilter();
                    GrdDetDetail.Columns["LOTNO"].ClearFilter();
                    GrdDetDetail.Columns["ASSORTER"].ClearFilter();
                    GrdDetDetail.Columns["MAINMANAGER"].ClearFilter();
                    if (CmbViewType.SelectedIndex == 1)
                    {
                        GrdDetDetail.Columns["DEPARTMENTNAME"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("DEPARTMENTNAME='" + Val.ToString(MainGrid.GetFocusedRowCellValue("PARTICULAR")) + "'");
                    }
                    //if (CmbViewType.SelectedIndex == 2)
                    //{
                    //    GrdDetDetail.Columns["EMPLOYEECODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("EMPLOYEECODE='" + Val.ToString(MainGrid.GetFocusedRowCellValue("PARTICULAR")) + "'");
                    //}
                    if (CmbViewType.SelectedIndex == 3)
                    {
                        GrdDetDetail.Columns["CLACODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("CLACODE='" + Val.ToString(MainGrid.GetFocusedRowCellValue("PARTICULAR")) + "'");
                    }
                    if (CmbViewType.SelectedIndex == 4)
                    {
                        GrdDetDetail.Columns["LOTNO"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("LOTNO='" + Val.ToString(MainGrid.GetFocusedRowCellValue("PARTICULAR")) + "'");
                    }
                    if (CmbViewType.SelectedIndex == 5)
                    {
                        GrdDetDetail.Columns["ASSORTER"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("ASSORTER='" + Val.ToString(MainGrid.GetFocusedRowCellValue("PARTICULAR")) + "'");
                    }
                    if (CmbViewType.SelectedIndex == 6)
                    {
                        GrdDetDetail.Columns["MAINMANAGER"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("MAINMANAGER='" + Val.ToString(MainGrid.GetFocusedRowCellValue("PARTICULAR")) + "'");
                    }

                    this.Cursor = Cursors.Default;

                    this.Cursor = Cursors.Default;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void RbtMakable_CheckedChanged(object sender, EventArgs e)
        {
            //if (CmbViewType.SelectedIndex == 0)
            //{
            //    DtabData.Rows.Clear();
            //    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A6;
            //    panelGrid.Visible = false;
            //    IsNextImage = false;
            //}
        }

        private void LblExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "View Type Wise Detail (" + GrpViewTypeSearch.Text + ")";
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = mStrReportTitle;
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MaingrdSummary,
                        Landscape = false,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [" + mStrReportTitle + ".xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void LblPrint_Click(object sender, EventArgs e)
        {
            try
            {
                mStrReportTitle = "View Type Wise Detail (" + GrpViewTypeSearch.Text + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MaingrdSummary;
                link.Landscape = false;
                
                link.PaperKind = PaperKind.A4;
                MainGrid.OptionsPrint.AutoWidth = false;

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

        private void txtAssorter_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID,AUTOCONFIRM";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEECODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtAssorter.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtAssorter.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void GrdDetDetail_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouDetPolCts = 0;
                    DouDetRghCts = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouDetPolCts = DouDetPolCts + Val.Val(GrdDetDetail.GetRowCellValue(e.RowHandle, "POLISHCTS"));
                    DouDetRghCts = DouDetRghCts + Val.Val(GrdDetDetail.GetRowCellValue(e.RowHandle, "ROUGHWEIGHT"));
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ROUGHPOLISHPER") == 0)
                    {
                        if (Val.Val(DouDetRghCts) > 0)
                            e.TotalValue = Math.Round(((Val.Val(DouDetPolCts) / Val.Val(DouDetRghCts)) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
