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

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmPacketUpdate : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        //BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_PacketCreate ObjPacket = new BOTRN_PacketCreate();        
        DataTable DtabPacketRet = new DataTable();

        public DataTable DTabRefrence = new DataTable();



        #region Property Settings

        public FrmPacketUpdate()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DtabPacketRet.Columns.Add(new DataColumn("SEL", typeof(bool)));

            DtabPacketRet.Columns.Add(new DataColumn("PACKET_ID", typeof(Guid)));
            DtabPacketRet.Columns.Add(new DataColumn("JANGEDNO", typeof(Int64)));
            DtabPacketRet.Columns.Add(new DataColumn("KAPAN_ID", typeof(Guid)));
            DtabPacketRet.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));

            DtabPacketRet.Columns.Add(new DataColumn("PROCESS_ID", typeof(Int32)));
            DtabPacketRet.Columns.Add(new DataColumn("PROCESSNAME", typeof(string)));

            DtabPacketRet.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));

            DtabPacketRet.Columns.Add(new DataColumn("SHAPE_ID", typeof(int)));
            DtabPacketRet.Columns.Add(new DataColumn("PARTY_ID", typeof(Int64)));
            DtabPacketRet.Columns.Add(new DataColumn("PACKETPARTYNAME", typeof(string)));

            DtabPacketRet.Columns.Add(new DataColumn("SHAPE", typeof(string)));
            DtabPacketRet.Columns.Add(new DataColumn("PURITY", typeof(string)));
            DtabPacketRet.Columns.Add(new DataColumn("CHARNI", typeof(string)));

            DtabPacketRet.Columns.Add(new DataColumn("ISSUEPCS", typeof(int)));
            DtabPacketRet.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));

            DtabPacketRet.Columns.Add(new DataColumn("RETURNPCS", typeof(int)));
            DtabPacketRet.Columns.Add(new DataColumn("RETURNCARAT", typeof(double)));
            DtabPacketRet.Columns.Add(new DataColumn("RETURNPER", typeof(double)));

            DtabPacketRet.Columns.Add(new DataColumn("EXPPER", typeof(double)));
            DtabPacketRet.Columns.Add(new DataColumn("CANCELPCS", typeof(int)));
            DtabPacketRet.Columns.Add(new DataColumn("CANCELCARAT", typeof(double)));
            DtabPacketRet.Columns.Add(new DataColumn("KACHAPCS", typeof(int)));
            DtabPacketRet.Columns.Add(new DataColumn("KACHACARAT", typeof(double)));

            DtabPacketRet.Columns.Add(new DataColumn("LOSSCARAT", typeof(double)));
            DtabPacketRet.Columns.Add(new DataColumn("DIFFCARAT", typeof(double)));



            foreach (DataRow DR in DTabRefrence.Rows)
            {
                DataRow DRNew = DtabPacketRet.NewRow();
                DRNew["SEL"] = true;

                DRNew["PACKET_ID"] = DR["PACKET_ID"];
                DRNew["JANGEDNO"] = DR["JANGEDNO"];
                DRNew["KAPAN_ID"] = DR["KAPAN_ID"];
                DRNew["KAPANNAME"] = DR["KAPANNAME"];

                DRNew["PROCESS_ID"] = DR["PROCESS_ID"];
                DRNew["SHAPE_ID"] = DR["SHAPE_ID"];
                DRNew["EXPPER"] = DR["EXPPER"];

                DRNew["SHAPE"] = DR["SHAPENAME"];
                DRNew["CHARNI"] = DR["CHARNINAME"];
                DRNew["PURITY"] = DR["PURITYNAME"];

                DRNew["PROCESSNAME"] = DR["PROCESSNAME"];

                DRNew["PACKETNO"] = DR["PACKETNO"];

                DRNew["PARTY_ID"] = DR["PARTY_ID"];
                DRNew["PACKETPARTYNAME"] = DR["PACKETPARTYNAME"];
                DRNew["ISSUEPCS"] = DR["ISSUEPCS"];
                DRNew["ISSUECARAT"] = DR["ISSUECARAT"];

                DRNew["RETURNPCS"] = DR["ISSUEPCS"];
                DRNew["RETURNCARAT"] = DR["ISSUECARAT"];
                DRNew["RETURNPER"] = 100;

                DRNew["LOSSCARAT"] = 0;

                DtabPacketRet.Rows.Add(DRNew);
            }

            MainGrd.DataSource = DtabPacketRet;
            MainGrd.RefreshDataSource();
            
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
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);
            ObjFormEvent.ObjToDisposeList.Add(Val);            
        }

        #endregion

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnExit_Click(null, null);
            //}
        }



        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            try
            {
              

                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);

                    if (Val.ToBoolean(DRow["SEL"]) == false)
                    {
                        continue;
                    }

                    double DouIssueCarat = Val.Val(DRow["ISSUECARAT"]);
                    double DouReadyCarat = Val.Val(DRow["RETURNCARAT"]);
                    double DouLossCarat = Val.Val(DRow["LOSSCARAT"]);
                    double DouKachaCarat = Val.Val(DRow["KACHACARAT"]);
                    double DouCancelCarat = Val.Val(DRow["CANCELCARAT"]);

                    if (DouReadyCarat > DouIssueCarat)
                    {
                        Global.Message("Row : " + (IntI + 1).ToString() + " Return Carat Is Greater Than Issue Carat");
                        return;
                    }

                    if (Math.Round(DouIssueCarat, 4) != Math.Round(DouReadyCarat + DouLossCarat + DouKachaCarat + DouCancelCarat, 4))
                    {
                        Global.Message("Row : " + (IntI + 1).ToString() + " Issue Carat Is Not Match With Return + Cancel + Loss + Kacha");
                        return;
                    }
                }

                if (Global.Confirm("Are You Sure To Update All Selected Records ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                Int64 IntJangedNo = 0;
                this.Cursor = Cursors.WaitCursor;

                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);

                    TrnPacketCreationProperty Property = new TrnPacketCreationProperty();

                    Property.PACKET_ID = Guid.Parse(Val.ToString(DRow["PACKET_ID"]));
                    Property.KAPAN_ID = Guid.Parse(Val.ToString(DRow["KAPAN_ID"]));
                    Property.EMPLOYEE_ID = Val.ToInt64(DRow["PARTY_ID"]);
                    Property.PROCESS_ID = Val.ToInt32(DRow["PROCESS_ID"]);
                    Property.SHAPE_ID = Val.ToInt(DRow["SHAPE_ID"]);
                    Property.CHARNI_ID = Val.ToInt(DRow["CHARNI_ID"]);
                    Property.PURITY_ID = Val.ToInt(DRow["PURITY_ID"]);

                    Property.PACKETNO = Val.ToInt32(DRow["PACKETNO"]);

                    Property.ISSUEDATE = Val.SqlDate(Val.ToString(DRow["ISSUEDATE"]));
                   
                    Property.ISSUEPCS = Val.ToInt32(DRow["ISSUEPCS"]);
                    Property.ISSUECARAT = Val.Val(DRow["ISSUECARAT"]);

                    Property.RETURNPCS = Val.ToInt32(DRow["RETURNPCS"]);
                    Property.RETURNCARAT = Val.Val(DRow["RETURNCARAT"]);
                    Property.RETURNPER = Val.Val(DRow["RETURNPER"]);

                    Property.CANCELPCS = Val.ToInt32(DRow["CANCELPCS"]);
                    Property.CANCELCARAT = Val.Val(DRow["CANCELCARAT"]);

                    Property.KACHAPCS = Val.ToInt32(DRow["KACHAPCS"]);
                    Property.KACHACARAT = Val.Val(DRow["KACHACARAT"]);

                    Property.LOSSCARAT = Val.Val(DRow["LOSSCARAT"]);

                    Property.LABOURTYPE = Val.ToString(DRow["LABOURTYPE"]);
                    Property.LABOURRATE = Val.Val(DRow["LABOURRATE"]);

                    if (Val.ISDate(DRow["RETURNDATE"]) == true)
                    {
                        Property.RETURNDATE = Val.SqlDate(Val.ToString(DRow["RETURNDATE"]));
                    }
                    Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);

                    Property = ObjPacket.UpdatePacket(Property);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        IntJangedNo = Property.JANGEDNO;
                    }
                    else
                    {
                        Global.Message(Property.ReturnMessageDesc);
                    }

                    Property = null;
                }
                this.Cursor = Cursors.Default;

                Global.Message("Successfully Update Packet");

                this.Close();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);

            }
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            DataRow DRow = GrdDet.GetDataRow(e.RowHandle);

            switch (e.Column.FieldName.ToUpper())
            {
                case "RETURNPER":
                    double DouPer = Val.Val(DRow["RETURNPER"]);
                    double DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    double DouReadyCarat = Math.Round((DouIssCarat * DouPer) / 100, 4);
                    double DouLossCarat = Math.Round(DouIssCarat - DouReadyCarat, 4);

                    if (DouReadyCarat > DouIssCarat)
                    {
                        Global.Message("Return Carat Is Greater Than Issue");
                        DRow["RETURNCARAT"] = 0;
                        DRow["LOSSCARAT"] = 0;
                    }
                    else
                    {
                        DRow["RETURNCARAT"] = DouReadyCarat;
                        DRow["LOSSCARAT"] = DouLossCarat;
                    }
                    
                    break;
                case "RETURNCARAT":
                    DouReadyCarat = Val.Val(DRow["RETURNCARAT"]);
                    DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    DouPer = Math.Round((DouReadyCarat / DouIssCarat) * 100, 4);
                    DouLossCarat = Math.Round(DouIssCarat - DouReadyCarat, 4);

                    if (DouReadyCarat > DouIssCarat)
                    {
                        Global.Message("Return Carat Is Greater Than Issue");
                        DRow["RETURNPER"] = 0;
                        DRow["LOSSCARAT"] = 0;
                    }
                    else
                    {
                        DRow["RETURNPER"] = DouPer;
                        DRow["LOSSCARAT"] = DouLossCarat;
                    }
                    break;
                case "LOSSCARAT":
                    DouLossCarat = Val.Val(DRow["LOSSCARAT"]);
                    DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    DouReadyCarat = Math.Round(DouIssCarat - DouLossCarat, 4);
                    DouPer = Math.Round((DouReadyCarat / DouIssCarat) * 100, 4);
                    
                    if (DouReadyCarat > DouIssCarat)
                    {
                        Global.Message("Return Carat Is Greater Than Issue");
                        DRow["RETURNPER"] = 0;
                        DRow["RETURNCARAT"] = 0;
                    }
                    else
                    {
                        DRow["RETURNPER"] = DouPer;
                        DRow["RETURNCARAT"] = DouReadyCarat;
                    }
                    //GrdDet.SetRowCellValue(e.RowHandle, "READYPER", DouPer);

                    break;
                case "KACHAPCS":
                    DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    int IntIssPcs = Val.ToInt(DRow["ISSUEPCS"]);
                    int IntKachaPcs = Val.ToInt(DRow["KACHAPCS"]);

                    double KachaCarat = DouPer = Math.Round((DouIssCarat / IntIssPcs) * IntKachaPcs, 4);

                    DRow["KACHACARAT"] = KachaCarat;
                    
                    break;
                case "CANCELPCS":
                    DouIssCarat = Val.Val(DRow["ISSUECARAT"]);
                    IntIssPcs = Val.ToInt(DRow["ISSUEPCS"]);
                   int IntCancelPcs = Val.ToInt(DRow["CANCELPCS"]);

                   double CancelCarat = DouPer = Math.Round((DouIssCarat / IntIssPcs) * IntCancelPcs, 4);

                   DRow["CANCELCARAT"] = CancelCarat;

                    break;
                default:
                    break;
            }

            double DouDiff = Math.Round(Val.Val(DRow["ISSUECARAT"]) - (Val.Val(DRow["RETURNCARAT"]) + Val.Val(DRow["LOSSCARAT"]) + Val.Val(DRow["KACHACARAT"]) + Val.Val(DRow["CANCELCARAT"])), 4);
            DRow["DIFFCARAT"] = DouDiff;

            DtabPacketRet.AcceptChanges();
        }

        private void txtShape_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCharni_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPurity_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

    }
}
