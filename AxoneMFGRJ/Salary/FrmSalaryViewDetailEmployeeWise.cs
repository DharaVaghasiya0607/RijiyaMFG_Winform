using BusLib.Configuration;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Data;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Salary
{
    public partial class FrmSalaryViewDetailEmployeeWise : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable DTabPacketWiseStock = new DataTable();
        public string mStrOpe = "";

        double DouTotalPlanVarDollar = 0;
        double DouTotalPlanVarRupees = 0;
        double DouTotalDFPlusDollar = 0;
        double DouTotalDFMinusDollar = 0;
        double DouTotalDFPlusRupees = 0;
        double DouTotalDFMinusRupees = 0;
        double DouTotalPcs = 0;

        double DouTotalDPlusDollar_BeforePer = 0;
        double DouTotalDMinusDollar_BeforePer = 0;
        double DouTotalDFPlusDollar_BeforePer = 0;
        double DouTotalDFMinusDollar_BeforePer = 0;


        public FrmSalaryViewDetailEmployeeWise()
        {
            InitializeComponent();
        }

        public void ShowForm(string StrReportTitle)
        {
            GrpPacketSearch.Text = StrReportTitle;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            GrdDet.BeginUpdate();
            MainGrid.DataSource = DTabPacketWiseStock;

            GrdDet.RefreshData();
            GrdDet.BestFitColumns();
            GrdDet.EndUpdate();


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
            TextBrick BrickTitlesParam = e.Graph.DrawString("Date :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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
                svDialog.FileName = "SalaryDetail";
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

                    if (Global.Confirm("Do You Want To Open [PacketWiseStock.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void MainGrid_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == info.BandsInfo.BandCount - 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
        }

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalPlanVarDollar = 0;
                    DouTotalPlanVarRupees = 0;

                    DouTotalDFPlusDollar = 0;
                    DouTotalDFMinusDollar = 0;
                    DouTotalDFPlusRupees = 0;
                    DouTotalDFMinusRupees = 0;
                    DouTotalPcs = 0;

                    DouTotalDPlusDollar_BeforePer = 0;
                    DouTotalDMinusDollar_BeforePer = 0;
                    DouTotalDFPlusDollar_BeforePer = 0;
                    DouTotalDFMinusDollar_BeforePer = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    string P1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "KAPANPACKET"));
                    string P2 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle - 1, "KAPANPACKET"));
                    if (P1 != P2)
                    {
                        DouTotalPlanVarDollar = DouTotalPlanVarDollar + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "PLANVARIATIONDOLLAR"));
                        DouTotalPlanVarRupees = DouTotalPlanVarRupees + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "PLANVARIATIONRUPEES"));

                        DouTotalDPlusDollar_BeforePer = DouTotalDPlusDollar_BeforePer + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BEFOREPER_PLUSDOLLAR"));
                        DouTotalDMinusDollar_BeforePer = DouTotalDMinusDollar_BeforePer + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BEFOREPER_MINUSDOLLAR"));
                        DouTotalDFPlusDollar_BeforePer = DouTotalDFPlusDollar_BeforePer + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BEFOREPER_DFPLUSDOLLAR"));
                        DouTotalDFMinusDollar_BeforePer = DouTotalDFMinusDollar_BeforePer + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BEFOREPER_DFMINUSDOLLAR"));

                        DouTotalDFPlusDollar = DouTotalDFPlusDollar + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "DFPLUSDOLLAR"));
                        DouTotalDFMinusDollar = DouTotalDFMinusDollar + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "DFMINUSDOLLAR"));
                        DouTotalDFPlusRupees = DouTotalDFPlusRupees + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "DFPLUSRUPEES"));
                        DouTotalDFMinusRupees = DouTotalDFMinusRupees + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "DFMINUSRUPEES"));
                        DouTotalPcs = DouTotalPcs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "PCS"));   //#P : 21-01-2020
                    }

                    //string P3 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "PACKETTAG"));
                    //string P4 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle - 1, "PACKETTAG"));
                    //if (P3 != P4)
                    //{
                    //    DouTotalPcs = DouTotalPcs + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PCS"));   //#P : 21-01-2020
                    //}
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PLANVARIATIONDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalPlanVarDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PLANVARIATIONRUPEES") == 0)
                    {
                        e.TotalValue = DouTotalPlanVarRupees;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFPLUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFPlusDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFMINUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFMinusDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFPLUSRUPEES") == 0)
                    {
                        e.TotalValue = DouTotalDFPlusRupees;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFMINUSRUPEES") == 0)
                    {
                        e.TotalValue = DouTotalDFMinusRupees;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFTYPE") == 0)
                    {
                        e.TotalValue = DouTotalPcs;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_PLUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDPlusDollar_BeforePer;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_MINUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDMinusDollar_BeforePer;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_DFPLUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFPlusDollar_BeforePer;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_DFMINUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFMinusDollar_BeforePer;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrdDet_CellMerge(object sender, CellMergeEventArgs e)
        {
            try
            {
                string MergeOnStr = "DFTYPE,PLANVARIATIONDOLLAR,PLANVARIATIONRUPEES,PLANVARIATIONPER,BEFOREPER_DFPLUSDOLLAR,BEFOREPER_DFMINUSDOLLAR,DFPLUSPER,DFMINUSPER,DFPLUSRUPEES,DFMINUSRUPEES,BEFOREPER_PLUSDOLLAR,BEFOREPER_MINUSDOLLAR";
                string MergeOn = "KAPANPACKET";

                if (MergeOnStr.Contains(e.Column.FieldName))
                {
                    string val1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle1, GrdDet.Columns[MergeOn]));
                    string val2 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle2, GrdDet.Columns[MergeOn]));
                    if (val1 == val2)
                        e.Merge = true;
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }
    }
}
