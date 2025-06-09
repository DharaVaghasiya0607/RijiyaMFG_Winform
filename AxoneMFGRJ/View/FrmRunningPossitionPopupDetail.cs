using BusLib.Configuration;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Utils;
using DevExpress.XtraCharts;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace AxoneMFGRJ.View
{
    public partial class FrmRunningPossitionPopupDetail : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        public DataTable DTabPacketWiseStock = new DataTable();
        public string mStrOpe = "";

        string PStrPktDepartmentName = "";
        string pStrPktManagerName = "";
        string pStrPktStock = "";
        string pStrPktReportType = "";
        String mStrFromType = "";
        string pStrPktStockCategory = "";
        string pStrPktStockType = "";
        string pStrPktStrKapan = "";
        Int32 pIntPktNo = 0;
        string pStrPktTag = "";
        bool pBoolPktExtraStock = false;
        string pStrPktProcess = "";
        string pStrPktSubProcess = "";
        string pIntPktManager_ID = "";
        string pStrOPE = "";
        string pStrPktEmnployee = "";
        string pStrPktProcessName = "";
        string pStrPktMainManager = "";

        public FrmRunningPossitionPopupDetail()
        {
            InitializeComponent();
        }

        public void ShowForm(String StrReportTitle, string pStrFromType, string pStrStockCategory, string pStrStockType, string pStrKapan, Int32 pIntPkt, string pStrTag, bool pBoolWithExtraStock, string pStrProcess, string mMainManager_ID, string pStrSubProcess, string pStrPktOPE, String PStrDepartmentName, String pStrManagerName, string pStrEmployeeName, string pStrProcessName, string pStrMainManager, string pPktStock)
        {
            GrpPacketSearch.Text = StrReportTitle;
            PStrPktDepartmentName = PStrDepartmentName;
            pStrPktManagerName = pStrPktManagerName;
            pStrPktStock = pPktStock;// Consider StockType From Stock
            pStrPktReportType = pStrPktReportType;
            mStrFromType = pStrFromType;
            pStrPktStockCategory = pStrStockCategory;
            pStrPktStockType = pStrStockType;
            pStrPktStrKapan = pStrKapan;
            pIntPktNo = pIntPkt;
            pStrPktTag = pStrTag;
            pBoolPktExtraStock = pBoolWithExtraStock;
            pStrPktProcess = pStrProcess;
            pStrPktSubProcess = pStrSubProcess;
            pIntPktManager_ID = mMainManager_ID;
            pStrOPE = pStrPktOPE;
            pStrPktManagerName = pStrManagerName;
            pStrPktEmnployee = pStrEmployeeName;
            pStrPktProcessName = pStrProcessName; // Consider From Stock On Double Click
            pStrPktMainManager = pStrMainManager;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            BtnRefresh_Click(null, null);

            this.Show();
        }


        public void ShowForm(string StrReportTitle)
        {
            GrpPacketSearch.Text = StrReportTitle;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            BtnRefresh_Click(null, null);

            this.Show();
        }



        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(DTabPacketWiseStock);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString(GrpPacketSearch.Text, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("[ " + mStrOpe + " ] STOCK Of AsOnDate :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        public void Link_CreateMarginalEmpHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString(GrpEmpPacketSearch.Text, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("[ " + mStrOpe + " ] STOCK Of AsOnDate :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void lblPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGrid;
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

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "PacketDepartmentWiseStock";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrid,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [PacketDepartmentWiseStock.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGrid) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGrid.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);

                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMEMPLOYEENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOEMPLOYEENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMMANAGERNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOEMPMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOMANAGERNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "ISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "ISSUENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "PREVISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "PREVISSUENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MARKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MARKERNAME")));
                    return;
                }

            }
            finally
            {
                e.Info = info;
            }
        }

        private void lblEmpPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGridEmp;
                link.Landscape = true;


                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalEmpHeaderArea);
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

        private void lblEmpExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "PacketEmployeeWiseStock";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGridEmp,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalEmpHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [PacketEmployeeWiseStock.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void toolTipController2_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGridEmp) return;
            ToolTipControlInfo infoEmp = null;
            try
            {

                GridView viewEmp = MainGridEmp.GetViewAt(e.ControlMousePosition) as GridView;
                if (viewEmp == null) return;
                GridHitInfo hiEmp = viewEmp.CalcHitInfo(e.ControlMousePosition);
                if (hiEmp.HitTest == GridHitTest.RowCell && hiEmp.Column.FieldName == "TOEMPLOYEECODE")
                {
                    infoEmp = new ToolTipControlInfo(hiEmp.RowHandle.ToString() + hiEmp.Column.FieldName, Val.ToString(viewEmp.GetRowCellValue(hiEmp.RowHandle, "TOEMPLOYEENAME")));
                    return;
                }
            }
            finally
            {
                e.Info = infoEmp;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {

            DataSet Ds = Obj.RunningPosstionDataPacketDetailWise(mStrFromType, pStrPktStockCategory, pStrPktStockType, pStrPktStrKapan, pIntPktNo, pStrPktTag, pBoolPktExtraStock, pStrPktProcess, pIntPktManager_ID, pStrPktSubProcess, pStrOPE, PStrPktDepartmentName, pStrPktManagerName, pStrPktEmnployee, pStrPktProcessName, pStrPktMainManager, pStrPktStock);

            GrdDet.BeginUpdate();
            DTabPacketWiseStock = Ds.Tables[0];
            MainGrid.DataSource = DTabPacketWiseStock;
            GrdDet.RefreshData();
            GrdDet.BestFitColumns();
            GrdDet.EndUpdate();

            var newDt = DTabPacketWiseStock.AsEnumerable()
                .OrderBy(r => r.Field<string>("TOEMPLOYEECODE"))
                .GroupBy(r => new
                {
                    TOEMLOYEECODE = r.Field<string>("TOEMPLOYEECODE"),
                    TOEMPLOYEENAME = r.Field<string>("TOEMPLOYEENAME"),
                })
                .Select(g =>
                {
                    var row = DTabPacketWiseStock.NewRow();
                    row["TOEMPLOYEECODE"] = g.Key.TOEMLOYEECODE;
                    row["TOEMPLOYEENAME"] = g.Key.TOEMPLOYEENAME;
                    row["TOTALPCS"] = g.Sum(r => r.Field<int>("TOTALPCS"));
                    row["LOTCARAT"] = g.Sum(r => r.Field<decimal>("LOTCARAT"));
                    row["TOTALCARAT"] = g.Sum(r => r.Field<decimal>("TOTALCARAT"));
                    return row;
                }).CopyToDataTable();

            GrdDetEmp.BeginUpdate();
            MainGridEmp.DataSource = newDt;
            GrdDetEmp.RefreshData();
            GrdDetEmp.BestFitColumns();
            GrdDetEmp.EndUpdate();

            GrdJanged.BeginUpdate();
            MainGrdJanged.DataSource = Ds.Tables[1];
            GrdJanged.RefreshData();
            GrdJanged.BestFitColumns();
            GrdJanged.EndUpdate();
        }

        private void GrdJanged_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {

                if (e.RowHandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                DataRow DRow = GrdJanged.GetDataRow(e.RowHandle);
                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "KAPANNAME"))
                {

                    DataRow [] UDrow = DTabPacketWiseStock.Select("KAPANNAME = '" + Val.ToString(DRow["KAPANNAME"]) + "'");
                    DataTable DTab = UDrow.CopyToDataTable();

                    //DataRow[] UDRow = DTabPacketWiseStock.Select("KAPANNAME = '" + Val.ToString(DRow["KAPANNAME"]) + "'");
                    //DataTable DTab = UDRow.CopyToDataTable();

                    MainGrid.DataSource = DTab;
                    GrdDet.RefreshData();
                    
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }
        }
    }
}
