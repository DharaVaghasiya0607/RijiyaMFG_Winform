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
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using AxoneMFGRJ.Report;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleRejectionTransfer : DevExpress.XtraEditors.XtraForm
    {
        BOTRN_Rejection ObjRejection = new BOTRN_Rejection();

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();

        DataTable DTabPacket = new DataTable();

        #region Property Settings

        public FrmSingleRejectionTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm(DataTable pDTabStock)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            //#P : 05-10-2020
            string PCNPacketRMkblRateListXML;
            pDTabStock.TableName = "PCNPacketList";
            using (StringWriter sw = new StringWriter())
            {
                pDTabStock.WriteXml(sw);
                PCNPacketRMkblRateListXML = sw.ToString();
            }
            DataTable DtabRMkblRate = ObjRejection.GetRejectionPacketRMkblRate(PCNPacketRMkblRateListXML);
            //End : #P : 05-10-2020

            DTPTransferDate.Value = DateTime.Now;


            DTabPacket = new DataTable();
            DTabPacket.Columns.Add(new DataColumn("PACKET_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("KAPANCATEGORY", typeof(string)));
           
            DTabPacket.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TAG", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("RFIDTAG", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("ISSUEPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));

            DTabPacket.Columns.Add(new DataColumn("ROUGHMAKABLERATE", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("ROUGHMAKABLEAMOUNT", typeof(double)));

            DTabPacket.Columns.Add(new DataColumn("RATE", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("AMOUNT", typeof(double)));

            DTabPacket.Columns.Add(new DataColumn("OLDTRN_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("REMARK", typeof(string)));

            foreach (DataRow DR in pDTabStock.Rows)
            {
                DataRow DRNew = DTabPacket.NewRow();

                DRNew["OLDTRN_ID"] = DR["TRN_ID"];
                DRNew["PACKET_ID"] = DR["PACKET_ID"];
                DRNew["KAPAN_ID"] = DR["KAPAN_ID"];
                DRNew["KAPANNAME"] = DR["KAPANNAME"];
                DRNew["KAPANCATEGORY"] = DR["KAPANCATEGORY"];
              
                DRNew["PACKETNO"] = DR["PACKETNO"];
                DRNew["TAG"] = DR["TAG"];
                DRNew["BARCODE"] = DR["BARCODE"];
                DRNew["RFIDTAG"] = DR["RFIDTAG"];
                DRNew["ISSUEPCS"] = DR["BALANCEPCS"];
                DRNew["ISSUECARAT"] = DR["BALANCECARAT"];


                DRNew["RATE"] = 0.00;
                DRNew["AMOUNT"] = 0.00;
                //DRNew["RATE"] = DR["ROUGHMAKABLERATE"];
                //DRNew["AMOUNT"] = DR["ROUGHMAKABLEAMOUNT"];

                //DRNew["ROUGHMAKABLERATE"] = DR["ROUGHMAKABLERATE"];
                //DRNew["ROUGHMAKABLEAMOUNT"] = DR["ROUGHMAKABLEAMOUNT"];

                DRNew["REMARK"] = "Rejection Transer";


                var Result = from row in DtabRMkblRate.AsEnumerable()
                             where Val.ToString(row["KAPANNAME"]).ToUpper() == Val.ToString(Val.ToString(DR["KAPANNAME"])).ToUpper()
                                   && Val.ToInt32(row["PACKETNO"]) == Val.ToInt32(Val.ToString(DR["PACKETNO"]))
                                   && Val.ToString(row["TAG"]).ToUpper() == Val.ToString(Val.ToString(DR["TAG"])).ToUpper()
                             select row;

                if (Result.Count() > 0)
                {
                    DRNew["ROUGHMAKABLERATE"] = Val.Val(Result.FirstOrDefault()["ROUGHMAKABLERATE"]);
                    DRNew["ROUGHMAKABLEAMOUNT"] = Val.Val(Result.FirstOrDefault()["ROUGHMAKABLEAMOUNT"]);
                }
                DTabPacket.Rows.Add(DRNew);
            }


            MainGrid.DataSource = DTabPacket;
            GrdDet.RefreshData();

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
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
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
                DouCarat = DouCarat + Val.Val(DRow["ISSUECARAT"]);
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

        private void txtRejection_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "REJECTIONCODE,REJECTIONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_REJECTION);

                    FrmSearch.mColumnsToHide = "REJECTION_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        //#P : 13-10-2020
                        if (Val.ToString(FrmSearch.mDRow["REJECTIONNAME"]).Trim().ToUpper().Contains("PCN"))
                        {
                            DataTable DTabnlPCNPktList = new DataTable();
                            DataRow[] DrError = DTabPacket.Select("KAPANCATEGORY = 'PCN'");
                            if (DrError.Length != 0)
                            {
                                DTabnlPCNPktList = DrError.CopyToDataTable();
                            }
                            if (DTabnlPCNPktList.Rows.Count > 0)
                            {
                                DataView dv = new DataView(DTabnlPCNPktList);
                                DataTable DTabErrorPkts = dv.ToTable(true, "KAPANNAME", "PACKETNO", "TAG");

                                txtRejection.Text = string.Empty;
                                txtRejection.Tag = string.Empty;

                                FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                                // FrmPopupGrid.DTab = DtData;                   
                                FrmPopupGrid.CountedColumn = "PACKETNO";
                                FrmPopupGrid.ColumnsToHide = "";
                                FrmPopupGrid.MainGrid.DataSource = DTabErrorPkts;
                                FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                                FrmPopupGrid.Text = "List Of Packets Which Is Already In PCN...";
                                FrmPopupGrid.ISPostBack = true;
                                FrmPopupGrid.LblTitle.Text = "Packets Are Already In PCN Rejection";
                                this.Cursor = Cursors.Default;

                                FrmPopupGrid.Width = 1000;
                                FrmPopupGrid.GrdDet.OptionsBehavior.Editable = false;
                                //FrmPopupGrid.Size = this.Size;

                                FrmPopupGrid.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                                FrmPopupGrid.GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
                                FrmPopupGrid.GrdDet.Columns["PACKETNO"].Caption = "Packet No";
                                FrmPopupGrid.GrdDet.Columns["TAG"].Caption = "Tag";
                                FrmPopupGrid.GrdDet.Columns["TAG"].Width = 50;
                                FrmPopupGrid.GrdDet.Columns["PACKETNO"].Width = 100;
                                FrmPopupGrid.ShowDialog();
                                FrmPopupGrid.Hide();
                                FrmPopupGrid.Dispose();
                                FrmPopupGrid = null;
                                return;
                            }
                        }
                        //End : #P : 13-10-2020

                        txtRejection.Text = Val.ToString(FrmSearch.mDRow["REJECTIONNAME"]);
                        txtRejection.Tag = Val.ToString(FrmSearch.mDRow["REJECTION_ID"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public bool ValSave()
        {
            if (txtRejection.Text.Trim().Length == 0)
            {
                Global.Message("Rejection Name Is Required");
                txtRejection.Focus();
                return false;
            }
            //if (Val.Val(txtRate.Text) == 0 && txtRate.Visible == true)
            //{
            //    Global.Message("Rate Is Required");
            //    txtRate.Focus();
            //    return false;
            //}
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
                //Add : Pinali : 22-10-2019 : Check That.... Packet Contains Rough Makable Entry(Only When Transfer To PCN Rejection)
                //if (Val.ToInt32(txtRejection.Tag) == 527) //REJECTION -> 527-PCN
                if (Val.ToString(txtRejection.Text).ToUpper().Contains("PCN")) //REJECTION -> 527-PCN
                {
                    DataTable DtabPCNPacketList = DTabPacket.Copy();
                    DtabPCNPacketList.TableName = "PCNPacketList";

                    string PCNPacketListXML;
                    using (StringWriter sw = new StringWriter())
                    {
                        DtabPCNPacketList.WriteXml(sw);
                        PCNPacketListXML = sw.ToString();
                    }

                    DataTable DtabVal = ObjRejection.CheckValidationForPCNRejection(PCNPacketListXML);
                    if (DtabVal.Rows.Count > 0)
                    {
                        FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                        // FrmPopupGrid.DTab = DtData;                   
                        FrmPopupGrid.CountedColumn = "PACKETNO";
                        FrmPopupGrid.ColumnsToHide = "KAPAN_ID,PACKET_ID";
                        FrmPopupGrid.MainGrid.DataSource = DtabVal;
                        FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                        FrmPopupGrid.Text = "List Of PCN Packets Which 'Rough Makable' Is Not Exists.";
                        FrmPopupGrid.ISPostBack = true;
                        FrmPopupGrid.LblTitle.Text = "Rough Makable Is Not Exists";
                        this.Cursor = Cursors.Default;

                        FrmPopupGrid.Width = 1000;
                        FrmPopupGrid.GrdDet.OptionsBehavior.Editable = false;
                        //FrmPopupGrid.Size = this.Size;

                        FrmPopupGrid.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        FrmPopupGrid.GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
                        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Caption = "Packet No";
                        FrmPopupGrid.GrdDet.Columns["TAG"].Caption = "Tag";

                        FrmPopupGrid.GrdDet.Columns["ISSUEPCS"].Caption = "Issue Pcs";
                        FrmPopupGrid.GrdDet.Columns["ISSUECARAT"].Caption = "Issue Cts";
                        FrmPopupGrid.GrdDet.Columns["RATE"].Caption = "Rate";
                        FrmPopupGrid.GrdDet.Columns["AMOUNT"].Caption = "Amount";

                        FrmPopupGrid.GrdDet.Columns["TAG"].Width = 50;
                        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Width = 100;
                        //FrmPopupGrid.GrdDet.Columns["REMARK"].Width = 150;
                        FrmPopupGrid.ShowDialog();
                        FrmPopupGrid.Hide();
                        FrmPopupGrid.Dispose();
                        FrmPopupGrid = null;
                        return;
                    }
                }
                //End : Pinali : 22-10-2019

                if (Global.Confirm("Are You Sure You Want To Transfer To Rejection ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                foreach (DataRow Dr in DTabPacket.Rows)
                {
                    //if (Val.Val(Dr["RATE"]) == 0)
                    //{
                    //    Global.Message("RATE IS ZERO IN SOME ROW, PLEASE CHECK");
                    //    return;
                    //}

                    TRN_RejectionProperty Property = new TRN_RejectionProperty();

                    Property.REJECTIONTRN_ID = 0;
                    Property.REJECTIONDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                    Property.REJECTIONFROM = "PACKET";

                    Property.REJECTION_ID = Val.ToInt32(txtRejection.Tag);
                    Property.LOT_ID = 0;
                    Property.PARTYNAME = "";
                    Property.KAPAN_ID = Val.ToInt64(Dr["KAPAN_ID"]);
                    Property.KAPANNAME = Val.ToString(Dr["KAPANNAME"]);
                    Property.PACKET_ID = Val.ToInt64(Dr["PACKET_ID"]);
                    Property.PACKETNO = Val.ToInt(Dr["PACKETNO"]);
                    Property.TAG = Val.ToString(Dr["TAG"]);

                    Property.PCS = Val.ToInt32(Dr["ISSUEPCS"]);
                    Property.CARAT = Val.Val(Dr["ISSUECARAT"]);
                    Property.RATE = Val.Val(Dr["RATE"]);
                    //Property.AMOUNT = Math.Round(Property.CARAT * Property.RATE, 2);
                    Property.AMOUNT = Val.ToString(txtRejection.Text).ToUpper().Contains("PCN") ? Val.Val(Dr["AMOUNT"]) : Math.Round(Property.CARAT * Property.RATE, 2);

                    Property.REMARK = Val.ToString(Dr["REMARK"]);

                    Property = ObjRejection.Save(Property);
                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;
                    Property = null;

                }

                this.Cursor = Cursors.Default;
                Global.Message(ReturnMessageDesc + txtRejection.Text);
                DTabPacket.Clear();
                this.Close();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }


        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRequiredProcess_Validated(object sender, EventArgs e)
        {
            BtnSave.Focus();
        }

        private void BtnApplyAll_Click(object sender, EventArgs e)
        {
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                double Carat = Val.Val(GrdDet.GetRowCellValue(IntI, "ISSUECARAT"));
                double Rate = Val.Val(txtRate.Text);
                double Amount = Math.Round(Carat * Rate, 3);

                GrdDet.SetRowCellValue(IntI, "RATE", Rate);
                GrdDet.SetRowCellValue(IntI, "AMOUNT", Amount);
            }
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.ToUpper() == "RATE")
            {
                double Carat = Val.Val(GrdDet.GetFocusedRowCellValue("ISSUECARAT"));
                double Rate = Val.Val(GrdDet.GetFocusedRowCellValue("RATE"));
                double Amount = Math.Round(Carat * Rate, 3);

                GrdDet.SetFocusedRowCellValue("AMOUNT", Amount);
            }
        }

        private void txtRejection_Validating(object sender, CancelEventArgs e) // Add : Pinali : 23-10-2019
        {
            try
            {
                if (!Val.ToString(txtRejection.Text).ToUpper().Contains("PCN"))
                {
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        GrdDet.SetRowCellValue(IntI, "RATE", 0);
                        GrdDet.SetRowCellValue(IntI, "AMOUNT", 0);
                    }

                    lblRate.Visible = true;
                    txtRate.Visible = true;
                    BtnApplyAll.Visible = true;
                }
                else
                {
                    double RoughMakRate = 0;
                    double RoughMakAmount = 0;
                    for (int IntI = 0; IntI < DTabPacket.Rows.Count; IntI++)   //Coz When Using Grid Then it's Call GridCellValueChanging event so i.e Datatable Used 
                    {
                        RoughMakRate = Val.Val(DTabPacket.Rows[IntI]["ROUGHMAKABLERATE"]);
                        RoughMakAmount = Val.Val(DTabPacket.Rows[IntI]["ROUGHMAKABLEAMOUNT"]);

                        DTabPacket.Rows[IntI]["RATE"] = Val.Val(RoughMakRate);
                        DTabPacket.Rows[IntI]["AMOUNT"] = Val.Val(RoughMakAmount);
                    }
                    DTabPacket.AcceptChanges();

                    lblRate.Visible = false;
                    txtRate.Visible = false;
                    BtnApplyAll.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

    }
}
