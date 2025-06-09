using AxoneMFGRJ.Masters;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmJobWorkPurchaseMasterDetail : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchaseMasterDetail ObjRough = new BOTRN_RoughPurchaseMasterDetail();
        BOFormPer ObjPer = new BOFormPer();
        string FormType = "JOBWORK";
        DataTable DTabDetail = new DataTable();
        
        #region Property Settings

        public FrmJobWorkPurchaseMasterDetail()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            this.Show();

            DataTable DTabParty = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERPURCHASE);
            ChkCmbSupplier.Properties.DataSource = DTabParty;
            ChkCmbSupplier.Properties.ValueMember = "LEDGER_ID";
            ChkCmbSupplier.Properties.DisplayMember = "LEDGERNAME";

            DTPFromInvoiceDate.Value = DateTime.Now.AddMonths(-1);
            DTPToInvoiceDate.Value = DateTime.Now;
            FetchInvoiceData(-1);
        }

        public void ShowForm(Int64 pIntInvoiceID)
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            this.Show();

            DataTable DTabParty = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERPURCHASE);
            ChkCmbSupplier.Properties.DataSource = DTabParty;
            ChkCmbSupplier.Properties.ValueMember = "LEDGER_ID";
            ChkCmbSupplier.Properties.DisplayMember = "LEDGERNAME";

            DTPFromInvoiceDate.Value = DateTime.Now.AddMonths(-1);
            DTPToInvoiceDate.Value = DateTime.Now;
            FetchInvoiceData(pIntInvoiceID);
            BtnSave.Enabled = false;
            BtnDelete.Enabled = false;
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjRough);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            if (txtSupplier.Text.Trim().Length == 0)
            {
                Global.Message("Supplier Name Is Required");
                txtSupplier.Focus();
                return false;
            }

            else if (DTPReceiveDate.Value < DTPPartyInvoiceDate.Value)
            {
                Global.Message("Invoice Receive Date Must be greter than Party Invoice");
                txtPartyInvoiceNo.Focus();
                return false;
            }

            if (txtGrossAmountUSD.Text.Trim().Length == 0)
            {
                Global.Message("Gross Amount Is Required");
                GrdDet.Focus();
                return false;
            }

            for (int IntI = 0; IntI < DTabDetail.Rows.Count; IntI++)
            {
                DataRow DRow = DTabDetail.Rows[IntI];
                if (Val.Val(DRow["CARAT"]) != 0 && Val.Val(DRow["GROSSAMOUNT"]) == 0)
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Gross Amount Is Required At this Row");
                    return false;    
                }
                if (Val.Val(DRow["CARAT"]) != 0 && Val.ToString(DRow["LOTNO"]).Trim() == "")
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Lot No Is Required At this Row");
                    return false;
                }
                if (Val.Val(DRow["CARAT"]) != 0 && Val.ToString(DRow["ROUGHNAME"]).Trim() == "")
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Rough Name Is Required At this Row");
                    return false;
                }
            }
            return true;
        }

        #endregion

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            DTabDetail.Rows.Clear();

            txtInvoiceID.Text = string.Empty;
            txtInvoiceID.Tag = string.Empty;

            txtSystemInvoiceNo.Text = string.Empty;
            txtSystemInvoiceNo.Tag = string.Empty;
            txtManualInvoiceNo.Text = string.Empty;
            txtSupplier.Tag = string.Empty;
            txtSupplier.Text = string.Empty;
            txtPartyInvoiceNo.Text = string.Empty;
            txtExcRate.Text = string.Empty;
            txtOtherCompany.Text = string.Empty;
            txtPaymentTermsDays.Text = string.Empty;
            DTPDueDate.Checked = false;
            txtYourRemark.Text = string.Empty;
            txtInvoiceRemark.Text = string.Empty;
            DTPReceiveDate.Value = DateTime.Now;
            DTPPartyInvoiceDate.Value = DateTime.Now;

            txtExtraIntPer.Text = string.Empty;
            txtTermsPer.Text = string.Empty;
            txtTermsAmountINR.Text = string.Empty;
            txtTermsAmountUSD.Text = string.Empty;
            txtPaymentTermsDays.Text = string.Empty;
            txtExpPer1.Text = string.Empty;
            txtExpAmountUSD1.Text = string.Empty;
            txtExpAmountINR1.Text = string.Empty;

            txtExpPer2.Text = string.Empty;
            txtExpAmountUSD2.Text = string.Empty;
            txtExpAmountINR2.Text = string.Empty;

            txtExpPer3.Text = string.Empty;
            txtExpAmountUSD3.Text = string.Empty;
            txtExpAmountINR3.Text = string.Empty;

            txtExpPer4.Text = string.Empty;
            txtExpAmountUSD4.Text = string.Empty;
            txtExpAmountINR4.Text = string.Empty;

            txtAddLessPer.Text = string.Empty;
            txtAddLessAmountUSD.Text = string.Empty;
            txtAddLessAmountINR.Text = string.Empty;

            txtNetAmountUSD.Text = string.Empty;
            txtNetAmountINR.Text = string.Empty;

            txtBrokrageAmountINR.Text = string.Empty;
            txtBrokrageAmountUSD.Text = string.Empty;

            CmbRoughType.SelectedIndex = 0;
            CmbPurchaseType.SelectedIndex = 0;
            CmbCurrencyType.SelectedIndex = 0;
            CmbPaymentType.SelectedIndex = 0;

            DTPReceiveDate.Focus();
            DTPReceiveDate_Validated(null, null);
            CalculateSummary();

            lblMode.Text = "Add Mode";

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (ValSave() == false)
                {
                    return;
                }
                if (Global.Confirm("Are you Sure Your Want To Save This Entry ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                if (lblMode.Text == "Add Mode")
                {
                    while (ObjRough.ISExistsSystemInvoiceNo(Val.ToString(txtSystemInvoiceNo.Text),Val.ToString(CmbRoughType.Text),FormType) == true)
                    {
                        if (Global.Confirm("[" + txtSystemInvoiceNo.Text + "] No Is Already Generated\n\nDo You Want To Generate New ?") == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        } 
                        DTPReceiveDate_Validated(null, null);
                    }
                }

                foreach (DataRow DRow in DTabDetail.Rows)
                {
                    if (Val.Trim( DRow["BROKERNAME"]) == "")
                    {
                        DRow["BROKER_ID"] = 0;
                        DRow["BROKRAGEPER"] = 0;
                        DRow["BROKRAGEAMOUNT"] = 0;
                    }
                }
                DTabDetail.AcceptChanges();

                TrnRoughPurchaseProperty Property = new TrnRoughPurchaseProperty();
                Property.INVOICE_ID = Val.ToInt64(txtInvoiceID.Text);
                Property.RECEIVEDATE = Val.SqlDate(DTPReceiveDate.Value.ToShortDateString());
                Property.INVOICEYEAR = Val.ToInt16(DTPReceiveDate.Value.Year);
                Property.INVOICEMONTH = Val.ToInt16(DTPReceiveDate.Value.Month);
                Property.INVOICENO = Val.ToInt16(txtSystemInvoiceNo.Text);

                Property.SYSTEMINVOICENO = txtSystemInvoiceNo.Text;
                Property.MANUALINVOICENO = txtManualInvoiceNo.Text;
                Property.PARTYINVOICENO = txtPartyInvoiceNo.Text;
                Property.PARTYINVOICEDATE = Val.SqlDate(DTPPartyInvoiceDate.Value.ToShortDateString());
                Property.PARTY_ID = Val.ToInt64(txtSupplier.Tag);
                Property.ROUGHTYPE = Val.ToString(CmbRoughType.Text);
                Property.TERMSDAYS = Val.ToInt(txtPaymentTermsDays.Text);
                Property.PAYMENTDATE = Val.SqlDate(DTPDueDate.Value.ToShortDateString());
                Property.OTHERCOMPANY = txtOtherCompany.Text;
                Property.EXCRATE=Val.Val(txtExcRate.Text);
                Property.PURCHASETYPE = Val.ToString(CmbPurchaseType.SelectedItem);
                Property.CURRENCYTYPE = Val.ToString(CmbCurrencyType.SelectedItem);
                Property.PAYMENTTYPE = Val.ToString(CmbPaymentType.SelectedItem);
                Property.GROSSAMOUNT = Val.Val(txtGrossAmountUSD.Text);
                Property.TERMSPER = Val.Val(txtTermsPer.Text);
                Property.TERMSAMOUNT = Val.Val(txtTermsAmountUSD.Text);
                Property.EXPPER1 = Val.Val(txtExpPer1.Text);
                Property.EXPAMOUNT1 = Val.Val(txtExpAmountUSD1.Text);
                Property.EXPPER2 = Val.Val(txtExpPer2.Text);
                Property.EXPAMOUNT2 = Val.Val(txtExpAmountUSD2.Text);
                Property.EXPPER3 = Val.Val(txtExpPer3.Text);
                Property.EXPAMOUNT3 = Val.Val(txtExpAmountUSD3.Text);
                Property.EXPPER4 = Val.Val(txtExpPer4.Text);
                Property.EXPAMOUNT4 = Val.Val(txtExpAmountUSD4.Text);
                Property.ADDLESSPER = Val.Val(txtAddLessPer.Text);
                Property.ADDLESSAMOUNT= Val.Val(txtAddLessAmountUSD.Text);
                Property.NETAMOUNT = Val.Val(txtNetAmountUSD.Text);
                Property.EXTRAINTERESTPER = Val.Val(txtExtraIntPer.Text);
                Property.INVOICEREMARK = txtInvoiceRemark.Text;
                Property.YOURREMARK = txtYourRemark.Text;
                Property.FORMTYPE = "JOBWORK";
                
                DTabDetail.TableName = "Table1";
                string RoughDetailXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabDetail.WriteXml(sw);
                    RoughDetailXML = sw.ToString();
                }

                Property.XMLFORROUGHDETAIL = RoughDetailXML;

                Property = ObjRough.Save(Property, RoughDetailXML);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    txtInvoiceID.Text = Property.ReturnValue;
                    BtnAdd_Click(null, null);
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    BtnSave.Focus();
                }
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

        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnBack_Click(null, null);
            //}
        }

        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataRow DR = GrdDet.GetFocusedDataRow();
            //    FetchValue(DR);
            //    DR = null;
            //}
        }


        public void FetchValue(DataRow DR)
        {
            lblMode.Text = "Edit Mode";
            txtInvoiceID.Text = Val.ToString(DR["INVOICE_ID"]);

            DTPReceiveDate.Value = DateTime.Parse(Val.ToString(DR["RECEIVEDATE"]));

            txtSystemInvoiceNo.Text = Val.ToString(DR["SYSTEMINVOICENO"]);
            txtSystemInvoiceNo.Tag = Val.ToString(DR["INVOICENO"]);

            txtManualInvoiceNo.Text = Val.ToString(DR["MANUALINVOICENO"]);
            txtPartyInvoiceNo.Text = Val.ToString(DR["PARTYINVOICENO"]);
            DTPPartyInvoiceDate.Value = DateTime.Parse(Val.ToString(DR["PARTYINVOICEDATE"]));
            txtSupplier.Text = Val.ToString(DR["PARTYNAME"]);
            txtSupplier.Tag = Val.ToInt64(DR["PARTY_ID"]);
            txtPaymentTermsDays.Text = Val.ToString(DR["TERMSDAYS"]);
            if (Val.IsDate(Val.ToString(DR["PAYMENTDATE"])))
            {
                DTPDueDate.Value = DateTime.Parse(Val.ToString(DR["PAYMENTDATE"]));
            }
            txtOtherCompany.Text = Val.ToString(DR["OTHERCOMPANY"]);
            txtExcRate.Text = Val.ToString(DR["EXCRATE"]);
            
            CmbPurchaseType.SelectedItem = Val.ToString(DR["PURCHASETYPE"]);
            CmbCurrencyType.SelectedItem = Val.ToString(DR["CURRENCYTYPE"]);
            CmbPaymentType.SelectedItem = Val.ToString(DR["PAYMENTTYPE"]);

            txtYourRemark.Text = Val.ToString(DR["INVOICEREMARK"]);
            txtInvoiceRemark.Text = Val.ToString(DR["YOURREMARK"]);

            txtTermsPer.Text = Val.ToString(DR["TERMSPER"]);
            txtExpPer1.Text = Val.ToString(DR["EXPPER1"]);
            txtExpPer2.Text = Val.ToString(DR["EXPPER2"]);
            txtExpPer3.Text = Val.ToString(DR["EXPPER3"]);
            txtExpPer4.Text = Val.ToString(DR["EXPPER4"]);
            txtAddLessPer.Text = Val.ToString(DR["ADDLESSPER"]);
            txtExcRate.Text = Val.ToString(DR["EXCRATE"]);
            txtExtraIntPer.Text = Val.ToString(DR["EXTRAINTERESTPER"]);
            CalculateSummary();
            DTPReceiveDate.Focus();

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("List", GrdDet);
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        TrnRoughPurchaseProperty Property = new TrnRoughPurchaseProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.LOT_ID = Val.ToInt64(Drow["LOT_ID"]);
                        Property.INVOICE_ID =0;
                        
                        Property = ObjRough.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message(Property.ReturnMessageDesc);
                            DTabDetail.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTabDetail.AcceptChanges();
                        }
                        else
                        {
                            Global.MessageError(Property.ReturnMessageDesc);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void txtSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_JOBWORKLEDGER);

                    FrmSearch.mColumnsToHide = "LEDGER_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtSupplier.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtSupplier.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
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

        private void repTxtArticle_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "ARTICLENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "ARTICLENAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_ARTICLE);
                    //FrmSearch.ColumnsToHide = "REASON_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("ARTICLENAME", Val.ToString(FrmSearch.mDRow["ARTICLENAME"]));
                    }
                    
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtRoughMines_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "MINESNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "MINESNAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MINES);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("MINESNAME", Val.ToString(FrmSearch.mDRow["MINESNAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtRoughName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "ROUGHNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "ROUGHNAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_ROUGHNAME);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("ROUGHNAME", Val.ToString(FrmSearch.mDRow["ROUGHNAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtMSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "MSIZENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "MSIZENAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MSIZE);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("MSIZENAME", Val.ToString(FrmSearch.mDRow["MSIZENAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
                switch (e.Column.FieldName.ToUpper())
                {
                    case "PCS":
                    case "CARAT":
                    case "RATE":
                    case "BROKRAGEPER":

                        double DouSizeAvg = 0;
                        double DouGrossAmt = 0;

                        double DouCarat = 0;
                        double DouBrokerAmount = 0;

                        if (Val.Val(DRow["CARAT"]) != 0)
                        {
                            DouCarat = Val.Val(DRow["CARAT"]);

                            DouSizeAvg = Val.Val(DRow["PCS"]) != 0 ? Math.Round(DouCarat / Val.Val(DRow["PCS"]), 2) : 0;
                            DouGrossAmt = Math.Round(DouCarat * Val.Val(DRow["RATE"]), 2);
                            DouBrokerAmount = Math.Round((DouGrossAmt * Val.Val(DRow["BROKRAGEPER"])) / 100, 2);

                            DRow["GROSSAMOUNT"] = DouGrossAmt;
                            DRow["AVGSIZE"] = DouSizeAvg;
                            DRow["BROKRAGEAMOUNT"] = DouBrokerAmount;
                            DRow["GROSSBROKAMOUNT"] = DouGrossAmt + DouBrokerAmount;

                        }
                        CalculateSummary();
                        break;
                    case "ROUGHNAME":
                        if (lblMode.Text == "Add Mode")
                        {
                            GrdDet.SetRowCellValue(e.RowHandle, "LOTDESCRIPTION", Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "ROUGHNAME")));  
                        }
                        break;
                }
                GrdDet.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
       
        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;
            double DouGrossAmount = 0;
            double DouBrokrageAmount = 0;
           
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + Val.ToInt(DRow["PCS"]);
                DouCarat = DouCarat + Val.Val(DRow["CARAT"]);
                DouGrossAmount = DouGrossAmount + Val.Val(DRow["GROSSAMOUNT"]);
                DouBrokrageAmount = DouBrokrageAmount + Val.Val(DRow["BROKRAGEAMOUNT"]);
            }

            txtGrossAmountUSD.Text = DouGrossAmount.ToString();

            txtTermsAmountUSD.Text = Math.Round(DouGrossAmount * Val.Val(txtTermsPer.Text) / 100,2).ToString();
            txtExpAmountUSD1.Text = Math.Round(DouGrossAmount * Val.Val(txtExpPer1.Text) / 100, 2).ToString();
            txtExpAmountUSD2.Text = Math.Round(DouGrossAmount * Val.Val(txtExpPer2.Text) / 100, 2).ToString();
            txtExpAmountUSD3.Text = Math.Round(DouGrossAmount * Val.Val(txtExpPer3.Text) / 100, 2).ToString();
            txtExpAmountUSD4.Text = Math.Round(DouGrossAmount * Val.Val(txtExpPer4.Text) / 100, 2).ToString();
            txtAddLessAmountUSD.Text = Math.Round(DouGrossAmount * Val.Val(txtAddLessPer.Text) / 100, 2).ToString();

            txtNetAmountUSD.Text = Math.Round(DouGrossAmount + 
                Val.Val(txtTermsAmountUSD.Text) + 
                Val.Val(txtExpAmountUSD1.Text) + 
                Val.Val(txtExpAmountUSD2.Text) + 
                Val.Val(txtExpAmountUSD3.Text) + 
                Val.Val(txtExpAmountUSD4.Text) + 
                Val.Val(txtAddLessAmountUSD.Text) ,2
                ).ToString();

            txtBrokrageAmountUSD.Text = DouBrokrageAmount.ToString();

            txtGrossAmountINR.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtGrossAmountUSD.Text), 2).ToString(); ;
            txtTermsAmountINR.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtTermsAmountUSD.Text), 2).ToString();
            txtExpAmountINR1.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtExpAmountUSD1.Text), 2).ToString();
            txtExpAmountINR2.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtExpAmountUSD2.Text), 2).ToString();
            txtExpAmountINR3.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtExpAmountUSD3.Text), 2).ToString();
            txtExpAmountINR4.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtExpAmountUSD4.Text), 2).ToString();
            txtAddLessAmountINR.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtAddLessAmountUSD.Text), 2).ToString();
            txtNetAmountINR.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtNetAmountUSD.Text), 2).ToString();
            txtBrokrageAmountINR.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtBrokrageAmountUSD.Text), 2).ToString();

        }

        private void ChkDTPReceiveDate_ValueChanged(object sender, EventArgs e)
        {
            DTPDueDate.Checked = true;
            txtDueDays_Validated(null, null);
        }

        private void txtDueDays_Validated(object sender, EventArgs e)
        {
            if (Val.ISDate(DTPPartyInvoiceDate.Text) == true)
            {
                DTPDueDate.Value = DateTime.Parse(DTPPartyInvoiceDate.Value.ToShortDateString()).AddDays(Val.ToInt(txtPaymentTermsDays.Text));
            }
        }

        private void DTPDueDate_Validated(object sender, EventArgs e)
        {
            txtPaymentTermsDays.Text = Val.DateDiff(Microsoft.VisualBasic.DateInterval.Day, DTPPartyInvoiceDate.Value.ToShortDateString(), DTPDueDate.Value.ToShortDateString()).ToString();
        }

        private void txtExcRate_Validated(object sender, EventArgs e)
        {
            CalculateSummary();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFromInvoiceDate = null;
                string StrToInvoiceDate = null;
                string StrFromReceiveDate = null;
                string StrToReceiveDate = null;
                string StrPartyID = "";
                
                if (DTPFromInvoiceDate.Checked == true && DTPToInvoiceDate.Checked == true)
                {
                    StrFromInvoiceDate = Val.SqlDate(DTPFromInvoiceDate.Value.ToShortDateString());
                    StrToInvoiceDate = Val.SqlDate(DTPToInvoiceDate.Value.ToShortDateString());
                }
                if (DTPFromInvoiceDate.Checked == true && DTPToInvoiceDate.Checked == true)
                {
                    StrFromReceiveDate = Val.SqlDate(DTPFromReceiveDate.Value.ToShortDateString());
                    StrToReceiveDate = Val.SqlDate(DTPToReceiveDate.Value.ToShortDateString());
                }
                StrPartyID = Val.Trim(ChkCmbSupplier.Properties.GetCheckedItems().ToString());

                this.Cursor = Cursors.WaitCursor;
                GrdDetList.BeginUpdate();
                DataTable DTabData = ObjRough.GetDataForRoughPurchase("JOBWORK",StrFromInvoiceDate, StrToInvoiceDate, StrFromReceiveDate, StrToReceiveDate, StrPartyID);
                MainGridList.DataSource = DTabData;
                GrdDetList.RefreshData();
                GrdDetList.EndUpdate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }


        public void FetchInvoiceData(Int64 InvoiceID)
        {
            this.Cursor = Cursors.WaitCursor;
            DataSet DS = ObjRough.GetInvoiceDetail("JOBWORK",InvoiceID);

            if (DS.Tables.Count != 0)
            {
                DTabDetail = DS.Tables[1];

                GrdDet.BeginUpdate();
                MainGrid.DataSource = DTabDetail;
                DTPReceiveDate.Focus();


                DataRow DRow = DTabDetail.NewRow();
                DRow["SRNO"] = GrdDet.RowCount + 1;
                DTabDetail.Rows.Add(DRow);
                GrdDet.BestFitColumns();
                GrdDet.EndUpdate();
            }
            if (DS.Tables[0].Rows.Count != 0)
            {
                DataRow DrDetail = DS.Tables[0].Rows[0];
                FetchValue(DrDetail);
            }
            else
            {
                BtnAdd_Click(null, null);
            }
            this.Cursor = Cursors.Default;
        }

        private void GrdDetList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Clicks == 2)
            {
                FetchInvoiceData(Val.ToInt64(GrdDetList.GetFocusedRowCellValue("INVOICE_ID")));
                xtraTabControl1.SelectedTabPageIndex = 0;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtInvoiceID.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please First Select Record That You Want To Delete");
                    return;
                }

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;
                TrnRoughPurchaseProperty Property = new TrnRoughPurchaseProperty();
                Property.INVOICE_ID = Val.ToInt64(txtInvoiceID.Text);
                Property.LOT_ID = 0;
                Property = ObjRough.Delete(Property);
               
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    BtnAdd_Click(null, null);
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    txtInvoiceRemark.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void DTPReceiveDate_Validated(object sender, EventArgs e)
        {
            DataRow DRow = ObjRough.FindNewInvoiceNo(Val.SqlDate(DTPReceiveDate.Value.ToShortDateString()), Val.ToString(CmbRoughType.SelectedItem), "JOBWORK");
            if (DRow == null)
            {
                return;
            }
            else
            {
                txtSystemInvoiceNo.Tag = Val.ToString(DRow["InvoiceNo"]);
                txtSystemInvoiceNo.Text = Val.ToString(DRow["SystemInvoiceNo"]);
            }
        }

        private void txtTermsPer_TextChanged(object sender, EventArgs e)
        {
            CalculateSummary();
        }

        private void txtBroker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "BROKERCODE,BROKERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERBROKER);
                    FrmSearch.mISPostBack = false;
                    FrmSearch.mISPostBackColumn = "BROKERNAME";
                    FrmSearch.mColumnsToHide = "BROKER_ID,BROKERCODE,BILLINGADDRESS,BILLINGSTATE,BILLINGSTATECODE,SHIPPINGADDRESS,SHIPPINGSTATE,SHIPPINGSTATECODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("BROKERNAME", Val.ToString(FrmSearch.mDRow["BROKERNAME"]));
                        GrdDet.SetFocusedRowCellValue("BROKER_ID", Val.ToString(FrmSearch.mDRow["BROKER_ID"]));
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

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Val.Val(GrdDet.GetFocusedRowCellValue("CARAT")) != 0 &&
                    Val.Val(GrdDet.GetFocusedRowCellValue("GROSSAMOUNT")) != 0 &&
                    GrdDet.IsLastRow == true
                    )
                {
                    DataRow DRowPre = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                    DataRow DRow = DTabDetail.NewRow();
                    DRow["SRNO"] = GrdDet.RowCount + 1;
                   
                    if (GrdDet.RowCount > 0)
                    {
                        
                        DRow["ARTICLENAME"] = DRowPre["ARTICLENAME"];
                        DRow["ROUGHNAME"] = DRowPre["ROUGHNAME"];
                        DRow["MINESNAME"] = DRowPre["MINESNAME"];
                        DRow["MSIZENAME"] = DRowPre["MSIZENAME"];
                        DRow["LOTDESCRIPTION"] = DRowPre["LOTDESCRIPTION"];
                        DRow["BROKER_ID"] = DRowPre["BROKER_ID"];
                        DRow["BROKERNAME"] = DRowPre["BROKERNAME"];
                        DRow["BROKRAGEPER"] = DRowPre["BROKRAGEPER"];

                    }
                    DTabDetail.Rows.Add(DRow);
                    GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    GrdDet.FocusedColumn = GrdDet.Columns["LOTNO"];
                }
                else
                {
                    txtInvoiceRemark.Focus();
                }
            }
            
        }


        private void CmbPaymentType_Validated(object sender, EventArgs e)
        {
            DataRow DRow = DTabDetail.NewRow();
            DRow["SRNO"] = GrdDet.RowCount + 1;
            DTabDetail.Rows.Add(DRow);

            GrdDet.Focus();
            GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
            GrdDet.FocusedColumn = GrdDet.Columns["LOTNO"];
        }


    }
}
