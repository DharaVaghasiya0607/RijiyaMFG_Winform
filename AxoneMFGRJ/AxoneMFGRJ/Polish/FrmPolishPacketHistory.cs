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


    }
}
