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
using DevExpress.Data;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmRejectionTransfer : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
      
        BOTRN_Rejection ObjRejection = new BOTRN_Rejection();

        BOTRN_PurchaseLiveStock ObjTrnLiveStock = new BOTRN_PurchaseLiveStock();        
        DataTable DtabRejection = new DataTable();
        string mStrRejectionFrom = string.Empty;

        double TotKapanCarat = 0;
        double DouCarat = 0;  // K : 06/12/2022
        double DouRejectionAmount = 0; 
        double DouRejectionCarat = 0; 

        #region Property Settings

        public FrmRejectionTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm(string pLotOrKapanID, string pStrPartyOrRoughName,string pStrPartyInvoiceNoOrKapanCarat,string pStrRejectionFrom)
        {
            AttachFormDefaultEvent();

            Val.FormGeneralSetting(this);

            mStrRejectionFrom = pStrRejectionFrom;
            if (pStrRejectionFrom == "INVOICE")
            {

                lblLotID.Text = Val.ToString(pLotOrKapanID);
                lblLotID.Tag = pLotOrKapanID;

                lblPartyName.Text = pStrPartyInvoiceNoOrKapanCarat + " : " + pStrPartyOrRoughName;
                lblInvocieNo.Text = pStrPartyInvoiceNoOrKapanCarat;

                DataRow DR = ObjRejection.GetOrgAndBalCaratOfLot(Val.ToInt64(lblLotID.Text));

                if (DR == null)
                {
                    lblBalanceCarat.Text = "";
                    lblOrgCarat.Text = "";
                 
                }
                else
                {
                    lblBalanceCarat.Text = Val.ToString(DR["BALANCECARAT"]);
                    lblOrgCarat.Text = Val.ToString(DR["CARAT"]);
   
                }
            }
            else if (pStrRejectionFrom == "KAPAN")
            {
               
                LabelParty.Text = "Kapan";
                LabelInvoiceCarat.Text = "Kapan Carat";
                LabelInvoice.Text = "Kapan";
                
                lblLotID.Text = Val.ToString(pLotOrKapanID);
                lblLotID.Tag = pLotOrKapanID;

                lblPartyName.Text = pStrPartyOrRoughName;
                lblInvocieNo.Text = pStrPartyInvoiceNoOrKapanCarat;

                DataRow DR = ObjRejection.GetOrgAndBalCaratOfKapan(Val.ToInt64(lblLotID.Text));

                if (DR == null)
                {
                    lblBalanceCarat.Text = "";
                    lblOrgCarat.Text = "";
                  
                }
                else
                {
                    lblBalanceCarat.Text = Val.ToString(DR["BALANCECARAT"]);
                    lblOrgCarat.Text = Val.ToString(DR["KAPANCARAT"]);
         
                }
            }

            DtabRejection.Columns.Add(new DataColumn("ENTRYDATE", typeof(DateTime)));
            DtabRejection.Columns.Add(new DataColumn("REJECTION_ID", typeof(int)));
            DtabRejection.Columns.Add(new DataColumn("REJECTIONNAME", typeof(string)));
            DtabRejection.Columns.Add(new DataColumn("PCS", typeof(int)));
            DtabRejection.Columns.Add(new DataColumn("CARAT", typeof(double)));
            DtabRejection.Columns.Add(new DataColumn("RATE", typeof(double)));
            DtabRejection.Columns.Add(new DataColumn("AMOUNT", typeof(double)));
            DtabRejection.Columns.Add(new DataColumn("REMARK", typeof(string)));
            
            DtabRejection.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy"));
            // DtabKapan.Rows.Add(DtabKapan.NewRow());

            MainGrid.DataSource = DtabRejection;

            GrdDet.FocusedRowHandle = 0;
            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.Focus();

            MainGrid.RefreshDataSource();
            this.Show();

        }

        public void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            //ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);            

        }

        #endregion


        public void Clear()
        {
            DtabRejection.Rows.Clear();
            DtabRejection.Rows.Add(DtabRejection.NewRow());

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.Val(lblBalanceCarat.Text) == 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.MessageToster("NO BALANCE IS THERE");
                    return;
                }

                if (Global.Confirm("Are You Sure You Want To Transfer Rejection ? ")== System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;


                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                foreach (DataRow Dr in DtabRejection.Rows)
                {
                    TRN_RejectionProperty Property = new TRN_RejectionProperty();
                    if (Val.ToString(Dr["REJECTIONNAME"]).Trim().Equals(string.Empty) || Val.ToString(Dr["ENTRYDATE"]).Trim().Equals(string.Empty) || Val.Val(Dr["CARAT"]) <= 0)
                        continue;

                    Property.REJECTIONTRN_ID = 0;
                    Property.REJECTIONDATE = Val.ToString(Val.SqlDate(Val.ToString(Dr["ENTRYDATE"])));
                    Property.REJECTIONFROM = mStrRejectionFrom;

                    Property.REJECTION_ID = Val.ToInt32(Dr["REJECTION_ID"]);

                    if (Property.REJECTIONFROM == "INVOICE")
                    {
                        Property.LOT_ID = Val.ToInt64(lblLotID.Text);
                        Property.PARTYNAME = lblPartyName.Text;

                        Property.KAPAN_ID = 0;
                        Property.KAPANNAME = "";
                        Property.PACKET_ID = 0;
                        Property.PACKETNO = 0;
                        Property.TAG = "";
                    }
                    else if (Property.REJECTIONFROM == "KAPAN")
                    {
                        Property.LOT_ID = 0;
                        Property.PARTYNAME = "";
                        Property.KAPAN_ID = Val.ToInt64(lblLotID.Text);
                        Property.KAPANNAME = lblPartyName.Text;
                        Property.PACKET_ID = 0;
                        Property.PACKETNO = 0;
                        Property.TAG = "";
                    }
                   
                    Property.PCS = Val.ToInt32(Dr["PCS"]);
                    Property.CARAT = Val.Val(Dr["CARAT"]);
                    Property.RATE = Val.Val(Dr["RATE"]);
                    Property.AMOUNT = Math.Round(Property.CARAT * Property.RATE, 2);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                                     
                    Property = ObjRejection.Save(Property);
                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;
                    Property = null;
                }

                if (ReturnMessageType == "SUCCESS")
                {
                    Global.Message(ReturnMessageDesc);
                    this.Close();
                }
                else
                {
                    Global.MessageError(ReturnMessageDesc);
                }
               
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
            }

            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.ToUpper() == "RATE" || e.Column.FieldName.ToUpper() == "CARAT")
                {
                    double DouCarat = Val.Val(GrdDet.GetFocusedRowCellValue("CARAT"));
                    double DouRate = Val.Val(GrdDet.GetFocusedRowCellValue("RATE"));

                    GrdDet.SetFocusedRowCellValue("AMOUNT", Math.Round(DouCarat * DouRate, 0));
                }



            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
        }

        public void Calculatesummary()
        {
            TotKapanCarat = 0;
            DataRow dr = GrdDet.GetFocusedDataRow();
            foreach (DataRow Drow in DtabRejection.Rows)
            {
                TotKapanCarat = TotKapanCarat + Val.Val(Drow["CARAT"]);
            }
        }

        private void reptxtCarat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {


                //GrdDet.PostEditor();
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDet.PostEditor();
                    Calculatesummary();
                    DataRow dr = GrdDet.GetFocusedDataRow();
                        if (Val.Val(TotKapanCarat) != 0)
                        {
                            if (TotKapanCarat > Val.Val(lblBalanceCarat.Text))
                            {
                                Global.Message("Kapan Carat Is Greate Than Balance Carat.");
                                GrdDet.SetFocusedRowCellValue("KAPANCARAT", 0);
                                GrdDet.FocusedRowHandle = dr.Table.Rows.IndexOf(dr);
                                GrdDet.FocusedColumn = GrdDet.VisibleColumns[6];
                                GrdDet.Focus();
                                e.Handled = true;
                                //e.Cancel = true;

                            }

                        }
                       
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }


        }

        private void FrmKapanCreation_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnBack_Click(null, null);
            //}
        }

        private void reptxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["REJECTIONNAME"]).Equals(string.Empty) && Val.Val(dr["CARAT"]) > 0.00 && GrdDet.IsLastRow)
                    {
                        DtabRejection.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                        DtabRejection.Rows.Add(DtabRejection.NewRow());
                        DtabRejection.AcceptChanges();

                    }
                    else if (GrdDet.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void reptxtKapan_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("REJECTION_ID", Val.ToString(FrmSearch.mDRow["REJECTION_ID"]));
                        GrdDet.SetFocusedRowCellValue("REJECTIONNAME", Val.ToString(FrmSearch.mDRow["REJECTIONNAME"]));
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

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try // K : 06/12/2022
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouRejectionAmount = 0;
                    DouRejectionCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "CARAT"));

                    DouRejectionAmount = DouRejectionAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "AMOUNT"));
                    DouRejectionCarat = DouRejectionAmount / DouCarat;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouRejectionAmount) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }
    }
}
