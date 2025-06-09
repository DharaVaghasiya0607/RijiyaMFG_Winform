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
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using AxoneMFGRJ.Report;
using BusLib.Polish;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmPolishGoodsTransfer : DevExpress.XtraEditors.XtraForm
    {

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();        
        BOTRN_PolishTransaction ObjPolish = new BOTRN_PolishTransaction();

        DataTable DTabPacket = new DataTable();

        #region Property Settings

        public FrmPolishGoodsTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm(DataTable pDTabStock)
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            DTPTransferDate.Value = DateTime.Now;

            DTabPacket = new DataTable();
            DTabPacket.Columns.Add(new DataColumn("PACKET_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TRN_ID", typeof(string)));           
           
            DTabPacket.Columns.Add(new DataColumn("ISSUEPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("READYPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("READYCARAT", typeof(double)));           

            DTabPacket.Columns.Add(new DataColumn("LOSSCARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("JANGEDNO", typeof(double)));
           
            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEE_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEENAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("FROMDEPARTMENT_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("FROMDEPARTMENTNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("FROMPROCESS_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("FROMPROCESSNAME", typeof(string))); 
            DTabPacket.Columns.Add(new DataColumn("RETURNTYPE", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("FROMMANAGER_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("FROMMANAGERNAME", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("REMARK", typeof(string)));
           

            foreach (DataRow DR in pDTabStock.Rows)
            {
                DataRow DRNew = DTabPacket.NewRow();
               
                DRNew["PACKET_ID"] = DR["PACKET_ID"];
                DRNew["KAPAN_ID"] = DR["KAPAN_ID"];
                DRNew["TRN_ID"] = DR["TRN_ID"];
                DRNew["KAPANNAME"] = DR["KAPANNAME"];
                DRNew["PACKETNO"] = DR["PACKETNO"];
             
                DRNew["ISSUEPCS"] = DR["BALANCEPCS"];
                DRNew["ISSUECARAT"] = DR["BALANCECARAT"];
                DRNew["READYPCS"] = DR["BALANCEPCS"];
                DRNew["READYCARAT"] = DR["BALANCECARAT"];
                DRNew["JANGEDNO"] = DR["JANGEDNO"];
                DRNew["LOSSCARAT"] = 0.00;
               
                DRNew["FROMEMPLOYEE_ID"] = DR["EMPLOYEE_ID"];
                DRNew["FROMEMPLOYEENAME"] = DR["EMPLOYEENAME"];
                DRNew["FROMMANAGER_ID"] = DR["MANAGER_ID"];
                DRNew["FROMMANAGERNAME"] = DR["MANAGERNAME"];
                DRNew["FROMDEPARTMENT_ID"] = DR["DEPARTMENT_ID"];
                DRNew["FROMDEPARTMENTNAME"] = DR["DEPARTMENT"];
                DRNew["FROMPROCESS_ID"] = DR["PROCESS_ID"];
                DRNew["FROMPROCESSNAME"] = DR["PROCESSNAME"];
                
                DRNew["RETURNTYPE"] = "DONE";
                DRNew["REMARK"] = "";

                DTabPacket.Rows.Add(DRNew);
            }

            MainGrid.DataSource = DTabPacket;
            GrdDet.RefreshData();
            //RbtTransfer_CheckedChanged(null, null);

            CalculateSummary();         
           
               


                GrdDet.Columns["ISSUEPCS"].Caption = "IssPcs";
                GrdDet.Columns["ISSUECARAT"].Caption = "IssCts";
                GrdDet.Columns["READYPCS"].Caption = "RdyPcs";
                GrdDet.Columns["READYCARAT"].Caption = "RdyCts";
               
                GrdDet.Columns["LOSSCARAT"].Visible = true;
      
          

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
            ObjFormEvent.ObjToDisposeList.Add(ObjPolish);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["READYCARAT"]);
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();

        }

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }

        }

        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
        }


        private void BtnKapanLiveStockAutoFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
        }


        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PacketLiveStock", GrdDet);
        }
      
       

        
        public bool ValSave()
        {          

            int Int = 0;

            foreach (DataRow DRow in DTabPacket.Rows)
            {
                Int++;
                int IntPcs = Val.ToInt32(DRow["ISSUEPCS"]);
                double DouCarat = Val.Val(DRow["ISSUECARAT"]);

                int IntReadyPcs = Val.ToInt32(DRow["READYPCS"]);
                double DouReadyCarat = Val.Val(DRow["READYCARAT"]);               

                double DouLossCarat = Val.Val(DRow["LOSSCARAT"]);
              //double DouMixingLessPlus = Val.Val(DRow["MIXINGLESSPLUS"]);


                if (DouReadyCarat > DouCarat)
                {
                    Global.MessageError("Ready Carat Is Greater Than Issue Carat At Row : " + Int.ToString() + " PacketNo : " + DRow["BARCODE"].ToString().Replace("\n", " "));
                    //txtRequiredProcess.Focus();
                    return false;
                }

                if (DouLossCarat < 0)
                {
                    Global.MessageError("Loss Carat Not Less Then Zepro At Row : " + Int.ToString() + " PacketNo : " + DRow["BARCODE"].ToString().Replace("\n", " "));
                    //txtRequiredProcess.Focus();
                    return false;
                }
                
            }

            return true;
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValSave() == false)
                {
                    return;
                }


                if (Global.Confirm("Are You Sure You Want To Transfer ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }


                this.Cursor = Cursors.WaitCursor;

                //string EntryType = "";                

                int IntSrNo = 0;
                txtJangedNo.Text = string.Empty;

                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    IntSrNo++;
                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                    Property.TRN_ID = Val.ToInt64(DRow["TRN_ID"]); ;
                    Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"]);
                    //Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"].ToString());
                    //Property.PACKETNO = Val.ToInt(DRow["PACKETNO"].ToString());
                  
                    Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"]);
                    Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);

                    //Property.FROMDEPARTMENT_ID = Val.ToInt32(DRow["FROMDEPARTMENT_ID"]);
                    //Property.FROMMANAGER_ID = Val.ToInt64(DRow["FROMMANAGER_ID"]); 
                    //Property.FROMEMPLOYEE_ID = Val.ToInt64(DRow["FROMEMPLOYEE_ID"]); 
                    //Property.FROMPROCESS_ID = Val.ToInt32(DRow["FROMPROCESS_ID"]);
                   // Property.ISSUEPCS = Val.ToInt32(DRow["READYPCS"]);
                   // Property.ISSUECARAT = Val.Val(DRow["READYCARAT"]) + Val.Val(DRow["LOSSCARAT"]);
                   //Property.RETURNTYPE = Val.ToString(DRow["RETURNTYPE"]);
                    //if (Property.RETURNTYPE == "DONE")
                    //{
                     Property.READYPCS = Val.ToInt32(DRow["READYPCS"]);
                     Property.READYCARAT = Val.Val(DRow["READYCARAT"]);
                     Property.LOSSCARAT = Val.Val(DRow["LOSSCARAT"]);                     
                    //}
                    //else if (Property.RETURNTYPE == "NOT DONE")
                    //{
                    //    Property.READYPCS = 0;
                    //    Property.READYCARAT = 0.00;  
                       
                    //    Property.LOSSCARAT = 0;
                     
                    //}

                    Property.TRANSDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());                  
                    Property.REMARK = Val.ToString(DRow["REMARK"]);               
                

                    Property = ObjPolish.TransferGoodsPolish(Property);
                    txtJangedNo.Text = Property.JANGEDNO.ToString();
                    
                    if (Property.ReturnMessageType == "FAIL")
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError(Property.ReturnMessageDesc);
                        txtJangedNo.Text = "0";
                        this.Cursor = Cursors.WaitCursor;

                        if (Property.ReturnValue == "-5")
                            break;

                    }
                    else
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        this.Close();
                    }

                    Property = null;
                }
              

                this.Cursor = Cursors.Default;                 
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }


        
        
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            
        }

        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
            switch (e.Column.FieldName.ToUpper())
            {
                case "READYCARAT":
                case "RRCARAT":
                case "EXTRACARAT":

                    double DouReady = Val.Val(DRow["READYCARAT"]);
                    //double DouExtra = Val.Val(DRow["EXTRACARAT"]);
                    //double DouRR = Val.Val(DRow["RRCARAT"]);
                    double DouIssue = Val.Val(DRow["ISSUECARAT"]);
                    double DouLossCarat = Math.Round(DouIssue - DouReady, 3);

                    if (DouLossCarat < 0)
                    {
                        Global.MessageError("Loss Carat Is Less Then Zero");
                        GrdDet.SetRowCellValue(e.RowHandle, "READYCARAT", DouIssue);
                        GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", 0);
                    }
                    else
                    {
                        GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", DouLossCarat);
                    }

                    break;
                default:
                    break;
            }
        }

        private void txtJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (Global.OnKeyPressEveToPopup(e))
                //{
                //    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                //    FrmSearch.mSearchText = "JANGEDNO";
                //    FrmSearch.mSearchText = e.KeyChar.ToString();
                //    this.Cursor = Cursors.WaitCursor;
                //    FrmSearch.mDTab = ObjTrn.PopupJangedForParcelPrint("", 0, Val.SqlDate(DTPTransferDate.Value.ToShortDateString()));
                //    FrmSearch.mColumnsToHide = "";
                //    this.Cursor = Cursors.Default;
                //    FrmSearch.ShowDialog();
                //    e.Handled = true;
                //    if (FrmSearch.mDRow != null)
                //    {
                //        txtJangedNo.Text = Val.ToString(FrmSearch.mDRow["JANGEDNO"]);
                //    }

                //    FrmSearch.Hide();
                //    FrmSearch.Dispose();
                //    FrmSearch = null;
                //}
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
                

    }
}
