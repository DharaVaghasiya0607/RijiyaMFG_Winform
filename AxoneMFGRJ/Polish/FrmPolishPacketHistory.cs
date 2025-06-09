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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using BusLib.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using AxoneMFGRJ.Utility;
using AxoneMFGRJ;
using AxoneMFGRJ.Parcel;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmPolishPacketHistory : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PolishOkPacketUpdate ObjMast = new BOTRN_PolishOkPacketUpdate();
        BOTRN_SinglePolishOKTransfer ObjTrn = new BOTRN_SinglePolishOKTransfer();

        DataTable DtabPacketHistory = new DataTable();
        BODevGridSelection ObjGridSelection;

        string mStrKapanName = "";

        #region Property Settings

        public FrmPolishPacketHistory()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName)
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            mStrKapanName = pStrKapanName;

            DtabPacketHistory = ObjMast.GetDataForPolishOkTransferPacketHistory(mStrKapanName);
            DtabPacketHistory.DefaultView.Sort = "ENTRYDATE DESC";
            MainGrid.DataSource = DtabPacketHistory;


            this.Show();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        #region Other Event

        private void RepBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                GrdDet.PostEditor();
                if (Global.Confirm("Do You Want To Delete This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                PolishIssueReturnProperty Property = new PolishIssueReturnProperty();

                Int64 pIntPacketNo = Val.ToInt64(GrdDet.GetFocusedRowCellValue("PACKET_ID"));
                Int64 pIntPolishPacketNo = Val.ToInt64(GrdDet.GetFocusedRowCellValue("POLISHPACKET_ID"));
                Property.PCS = Val.ToInt32(GrdDet.GetFocusedRowCellValue("PCS"));
                Property.CARAT = Val.Val(GrdDet.GetFocusedRowCellValue("CARAT"));

                Property = ObjTrn.DeleteFromPolishOkTransfer(Property, pIntPacketNo, pIntPolishPacketNo);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);

                    DtabPacketHistory = ObjMast.GetDataForPolishOkTransferPacketHistory(mStrKapanName);
                    DtabPacketHistory.DefaultView.Sort = "ENTRYDATE DESC";
                    MainGrid.DataSource = DtabPacketHistory;
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }


        #endregion

        public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("POLISH PACKET DETAIL", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }


        private void lblExportDetail_Click(object sender, EventArgs e)
        {
          try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "KapanWiseRollingReport.xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    GrdDet.Appearance.Row.Font = new Font("Verdana", 8.25f);
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrid,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);

                    link.ExportToXlsx(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [KapanWiseRollingReport.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                GrdDet.Appearance.Row.Font = new Font("Verdana", 8);
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }
    }
}
