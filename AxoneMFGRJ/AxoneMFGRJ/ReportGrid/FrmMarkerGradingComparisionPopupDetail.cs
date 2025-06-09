using BusLib.Configuration;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmMarkerGradingComparisionPopupDetail : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable DTabPacketWiseStock = new DataTable();
        public string mStrOpe = "";

        public FrmMarkerGradingComparisionPopupDetail()
        {
            InitializeComponent();
        }

        public void ShowForm(string StrReportTitle, string StrClick)
        {
            GrpPacketSearch.Text = StrReportTitle;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            if (StrClick == "COLOR")
            {
                GrdDet.Columns["COLORNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["COLORNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["COLORNAME"].AppearanceCell.Font.Size, FontStyle.Bold);
                GrdDet.Columns["GCOLORNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["GCOLORNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["GCOLORNAME"].AppearanceCell.Font.Size, FontStyle.Bold);
               
                GrdDet.Columns["COLORNAME"].AppearanceCell.BackColor = Color.FromArgb( 255, 255, 192);
                GrdDet.Columns["GCOLORNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
            }
            if (StrClick == "CLARITY")
            {
                GrdDet.Columns["CLARITYNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["CLARITYNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["CLARITYNAME"].AppearanceCell.Font.Size, FontStyle.Bold);
                GrdDet.Columns["GCLARITYNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["GCLARITYNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["GCLARITYNAME"].AppearanceCell.Font.Size, FontStyle.Bold);

                GrdDet.Columns["CLARITYNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
                GrdDet.Columns["GCLARITYNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
            }
            if (StrClick == "CUT")
            {
                GrdDet.Columns["CUTNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["CUTNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["CUTNAME"].AppearanceCell.Font.Size, FontStyle.Bold);
                GrdDet.Columns["GCUTNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["GCUTNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["GCUTNAME"].AppearanceCell.Font.Size, FontStyle.Bold);

                GrdDet.Columns["CUTNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
                GrdDet.Columns["GCUTNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
            }
            if (StrClick == "POL")
            {
                GrdDet.Columns["POLNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["POLNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["POLNAME"].AppearanceCell.Font.Size, FontStyle.Bold);
                GrdDet.Columns["GPOLNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["GPOLNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["GPOLNAME"].AppearanceCell.Font.Size, FontStyle.Bold);

                GrdDet.Columns["POLNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
                GrdDet.Columns["GPOLNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
            }
            if (StrClick == "SYM")
            {
                GrdDet.Columns["SYMNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["SYMNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["SYMNAME"].AppearanceCell.Font.Size, FontStyle.Bold);
                GrdDet.Columns["GSYMNAME"].AppearanceCell.Font = new Font(GrdDet.Columns["GSYMNAME"].AppearanceCell.Font.FontFamily, GrdDet.Columns["GSYMNAME"].AppearanceCell.Font.Size, FontStyle.Bold);

                GrdDet.Columns["SYMNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
                GrdDet.Columns["GSYMNAME"].AppearanceCell.BackColor = Color.FromArgb(255, 255, 192);
            }
            
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
                svDialog.FileName = "PacketWiseGrading";
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

       

        

    }
}
