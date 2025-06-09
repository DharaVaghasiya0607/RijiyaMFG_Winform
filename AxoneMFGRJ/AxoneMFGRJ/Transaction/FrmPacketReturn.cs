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
    public partial class FrmPacketReturn : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        //BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_PacketCreate ObjPacket = new BOTRN_PacketCreate();
        DataTable DtabPacketRet = new DataTable();

        public DataTable DTabRefrence = new DataTable();



        #region Property Settings

        public FrmPacketReturn()
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
            DTPEntryDate.Focus();

            CalculateSummary();

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


        public void Clear()
        {
            txtIssueCarat.Text = string.Empty;

        }

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

        private void BtnCreateKapan_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure To Create Kapan ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);


                FrmKapanCreation FrmKapanCreation = new FrmKapanCreation();
                //FrmKapanCreation.ShowForm(Val.ToInt64(DRow["LOT_ID"]), FrmKapanCreation.FORMTYPE.KAPAN);
                //FrmKapanCreation.ShowForm(Guid.Parse(Val.ToString(DRow["LOT_ID"])));

                //  DRow = null;
                // BtnSearch_Click(null, null);
            }
            catch (System.Exception ex)
            {
                Global.Message(ex.Message);

            }
        }

        public bool ValSave()
        {
           
            if (Val.Val(txtIssueCarat.Text) == 0)
            {
                Global.Message("Packet Carat Is Required");
                txtIssueCarat.Focus();
                return false;
            }

            return true;
        }

        public void CalculateSummary()
        {
            double DouTotalCarat = 0;

            double DouReadyCarat = 0;

            foreach (DataRow DRow in DtabPacketRet.Rows)
            {
                if (Val.ToBoolean(DRow["SEL"]) == true)
                {
                    DouTotalCarat = DouTotalCarat + Val.Val(DRow["ISSUECARAT"]);
                    DouReadyCarat = DouReadyCarat + Val.Val(DRow["RETURNCARAT"]);
                }
            }

            txtIssueCarat.Text = DouTotalCarat.ToString();
            txtReturnCarat.Text = DouReadyCarat.ToString();

        }
        private void BtnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

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

                if (Global.Confirm("Are You Sure To Receive All Selected Records ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                Int64 IntJangedNo = 0;
                this.Cursor = Cursors.WaitCursor;

                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);

                    if (Val.ToBoolean(DRow["SEL"]) == false)
                    {
                        continue;
                    }

                    TrnPacketCreationProperty Property = new TrnPacketCreationProperty();


                    Property.PACKET_ID = Guid.Parse(Val.ToString(DRow["PACKET_ID"]));
                    Property.KAPAN_ID = Guid.Parse(Val.ToString(DRow["KAPAN_ID"]));
                    Property.EMPLOYEE_ID = Val.ToInt64(DRow["PARTY_ID"]);
                    Property.PROCESS_ID = Val.ToInt32(DRow["PROCESS_ID"]);
                    Property.SHAPE_ID = Val.ToInt(DRow["SHAPE_ID"]);
                    Property.PACKETNO = Val.ToInt32(DRow["PACKETNO"]);
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

                    Property.RETURNDATE = Val.SqlDate(DTPEntryDate.Text);
                    Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);

                    Property = ObjPacket.SavePacketReturn(Property);

                    lblMessage.Text = Property.ReturnMessageDesc;

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

                Global.Message(lblMessage.Text);

                this.Close();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);

            }
        }

        private void txtLossCarat_Validating(object sender, CancelEventArgs e)
        {

            foreach (DataRow DRow in DtabPacketRet.Rows)
            {
                if (Val.ToBoolean(DRow["SEL"]) == true)
                {
                    double DouTotalLoss = Val.Val(txtLossCarat.Text);
                    double DouTotalCarat = Val.Val(txtIssueCarat.Text);
                    double DouActualCarat = Val.Val(DRow["ISSUECARAT"]);

                    double DouPacketLoss = Math.Round((DouTotalLoss / DouTotalCarat) * DouActualCarat, 4);

                    DRow["LOSSCARAT"] = DouPacketLoss;

                    DRow["RETURNPCS"] = Val.ToInt32(DRow["ISSUEPCS"]);
                    DRow["RETURNCARAT"] = Math.Round(DouActualCarat - DouPacketLoss, 4);

                    DRow["RETURNPER"] = Math.Round((Val.Val(DRow["RETURNCARAT"]) / DouActualCarat) * 100, 4);

                }
                else
                {
                    DRow["LOSSCARAT"] = 0;
                    DRow["RETURNPCS"] = Val.ToInt32(DRow["ISSUEPCS"]);
                    DRow["RETURNCARAT"] = Val.Val(DRow["ISSUECARAT"]);
                    DRow["RETURNPER"] = 100;
                }
            }
            CalculateSummary();
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

        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            CalculateSummary();
        }

        private void txtLossPer_Validated(object sender, EventArgs e)
        {
            txtLossCarat.Text = Val.ToString(Math.Round(Val.Val(txtReturnCarat.Text) * Val.Val(txtLossPer.Text) / 100, 4));
            txtLossCarat_Validating(null, null);
        }

    }
}
