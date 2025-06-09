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
    public partial class FrmPacketHistoryView : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelectionForKapan;
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();

        DataTable DtabPacket = new DataTable();
        DataTable DTabMainPacketLiveStock = new DataTable();
        DataTable DTabSubPacketLiveStock = new DataTable();
        BOFormPer ObjPer = new BOFormPer();

        System.Diagnostics.Stopwatch watch = null;

        string mStrPassward = "";

        #region Property Settings

        public FrmPacketHistoryView()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName)
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DTabSubPacketLiveStock = ObjKapan.GetDataForPacketHistory(pStrKapanName);
            DTabSubPacketLiveStock.DefaultView.Sort = "ENTRYDATE DESC";
            MainGrid.DataSource = DTabSubPacketLiveStock;
            

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
    }
}
