using BusLib.Account;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AxoneMFGRJ.Account
{
    public partial class FrmBillWiseEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Ledger ObjMast = new BOMST_Ledger();
        BOFormPer ObjPer = new BOFormPer();
        BOLedgerTransaction objLedgerTrn = new BOLedgerTransaction();

        DataTable DtabPaymentDetail = new DataTable();
        DataTable DtabSummary = new DataTable();
        string LedgerType = string.Empty;
        public FORMTYPE mFormType = FORMTYPE.PM;

        double pDouPendingAmtFE = 0;
        double pDouPendingAmt = 0;

        public enum FORMTYPE
        {
            PM = 0,
            RP = 1,
            CO = 2
            //CP = 0,
            //BP = 1,
            //CR = 2,
            //BR = 3,
            //CO = 4
        }
        #region Property Settings

        public FrmBillWiseEntry()
        {
            InitializeComponent();
        }
        public void ShowForm(FORMTYPE pFormType = FORMTYPE.PM)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            DTPEntryDate.Value = DateTime.Now;
            DTPFromDate.Value = DateTime.Now.AddMonths(-1);
            DTPToDate.Value = DateTime.Now;

            DataTable DTabParty = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERALL);

            BtnClear_Click(null, null);
        }

        //kudeep 26082020
        public void ShowForm(string pName, string jandegNoStr, string pPendngAmt)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            DTPEntryDate.Value = DateTime.Now;
            DTPFromDate.Value = DateTime.Now.AddMonths(-1);
            DTPToDate.Value = DateTime.Now;

            BtnClear_Click(null, null);

            DataTable dt = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERCASHPAYMENT);
            DataRow[] dr = dt.Select("LEDGER_ID = '" + pName + "'");
            if (dr.Count() > 0)
            {
                txtPartyAC.Text = Val.ToString(dr[0]["LEDGERNAME"]);
                txtPartyAC.Tag = Val.ToString(dr[0]["LEDGER_ID"]);
                DataTable DTab = objLedgerTrn.FindLedgerClosingAmt(Guid.Parse(Val.ToString(txtInvoiceNo.Tag)), Val.ToString(CmbPaymentType.SelectedItem));
                txtClosingAmt.Text = Global.FindLedgerClosingStr(Val.ToInt64(txtPartyAC.Tag));
                DtabPaymentDetail = objLedgerTrn.GetBillWiseOutstanding(Val.ToInt64(txtInvoiceNo.Tag), Val.ToInt64(txtPartyAC.Tag), Val.Left(Val.ToString(CmbPaymentType.SelectedItem), 2), jandegNoStr);
                if (DtabPaymentDetail.Rows.Count > 0)
                    DtabPaymentDetail.Rows[0]["FPAYMENTAMOUNT"] = pPendngAmt.ToString();
                txtAmountFE.Text = pPendngAmt.ToString();

                MainGrdPayment.DataSource = DtabPaymentDetail;
                MainGrdPayment.Refresh();
            }
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
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        public void CalculateDiff()
        {
            double DouAmount = 0;
            double DouAmountBnkChrges = 0;

            for (int IntI = 0; IntI < GrdDetPayment.RowCount; IntI++)
            {

                DataRow DR = GrdDetPayment.GetDataRow(IntI);
                if (txtCurrency.Text == "USD")
                {
                    DouAmount = DouAmount + Val.Val(DR["PAYMENTAMOUNT"]);
                    //DouAmountBnkChrges = DouAmountBnkChrges + Val.Val(DR["BANKCHARGES"]);
                }
                else
                {
                    DouAmount = DouAmount + Val.Val(DR["FPAYMENTAMOUNT"]);
                    //DouAmountBnkChrges = DouAmountBnkChrges + Val.Val(DR["BANKCHARGESFE"]);
                }

            }

            lblDiff.Text = "Diff : " + Math.Round(Val.Val(txtAmountFE.Text) - (DouAmount), 2).ToString();

        }

        #region Validation

        private bool ValSave()
        {

            if (Val.ISDate(DTPEntryDate.Text) == false)
            {
                Global.Message("Entry Date Is Required");
                DTPEntryDate.Focus();
                return false;
            }
            if (Val.ToString(txtVoucherStr.Text.Trim()) == "")
            {
                Global.Message("Voucher No Is Required");
                txtVoucherStr.Focus();
                return false;
            }
            if (Val.ToString(txtCashBankAC.Text.Trim()) == "")
            {
                Global.Message("Source Account Is Required");
                txtCashBankAC.Focus();
                return false;
            }

            if (Val.ToString(txtPartyAC.Text.Trim()) == "")
            {
                Global.Message("Party Account Is Required");
                txtPartyAC.Focus();
                return false;
            }
            if (Val.ToString(txtAmountFE.Text.Trim()) == "")
            {
                Global.Message("Amount Is Required");
                txtAmountFE.Focus();
                return false;
            }
            double DouAmount = 0;
            for (int IntI = 0; IntI < GrdDetPayment.RowCount; IntI++)
            {
                DataRow DR = GrdDetPayment.GetDataRow(IntI);

                //if (DR["CURRENCY"].ToString() == "USD")
                //{
                //    if (Val.Val(DR["PENDINGAMOUNT"].ToString()) < (Val.Val(DR["PAYMENTAMOUNT"]) + Val.Val(DR["BANKCHARGES"])))
                //    {
                //        Global.Message("Amount Does Not Match With Pending Amount, Please check For Row No : " + (IntI+1).ToString());
                //        return false;
                //    }
                //}
                //else
                //{
                //if (Val.Val(DR["PENDINGAMOUNTFE"].ToString()) < (Val.Val(DR["FPAYMENTAMOUNT"]) + Val.Val(DR["BANKCHARGESFE"])))
                //{
                //    Global.Message("Amount Does Not Match With Pending Amount, Please check For Row No : " + (IntI + 1).ToString());
                //    return false;
                //}

                if (Val.ToString(DR["PAYMENTTYPE"].ToString()) == "" && (Val.Val(DR["PAYMENTAMOUNT"]) != 0 || Val.Val(DR["PAYMENTAMOUNT"]) != 0))
                {
                    Global.Message("Payment Type is Required, Please check For Row No : " + (IntI + 1).ToString());
                    return false;
                }
                //}

                if (txtCurrency.Text == "USD")
                    DouAmount = DouAmount + Val.Val(DR["PAYMENTAMOUNT"]);
                else
                    DouAmount = DouAmount + Val.Val(DR["FPAYMENTAMOUNT"]);
            }

            //if (Math.Round(Val.Val(txtAmountFE.Text), 2) != Math.Round(DouAmount, 2))
            //{
            //    Global.Message("Amount Does Not Match With Bill Amount, Please check ");
            //    txtAmountFE.Focus();
            //    return false;
            //}

            return true;
        }

        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPartyAC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE, LEDGERNAME";
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    string Str = Val.Left(Val.ToString(CmbPaymentType.SelectedItem), 2);

                    FrmSearch.mDTab = objLedgerTrn.FindLedgerParty(LedgerType, Str);

                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    txtPartyAC.Text = "";
                    txtPartyAC.Tag = "";
                    if (FrmSearch.mDRow != null)
                    {
                        txtPartyAC.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtPartyAC.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        //lblPartyBalance.Text = Global.FindLedgerClosingStr(Val.ToInt64(txtPartyAC.Tag));
                        //DataTable DTab = objLedgerTrn.FindLedgerClosingAmt(Val.ToInt64(txtPartyAC.Tag));
                        //txtClosingAmt.Text = Val.ToString(DTab.Rows[0]["AMOUNT"]);
                        //txtClosingAmtFE.Text = Val.ToString(DTab.Rows[0]["FAMOUNT"]);
                    }

                    //DtabPaymentDetail = objLedgerTrn.GetBillWiseOutstanding(Guid.Empty, Val.ToInt64(txtPartyAC.Tag), Val.Left(Val.ToString(CmbPaymentType.SelectedItem), 2));
                    //MainGrdPayment.DataSource = DtabPaymentDetail;
                    //MainGrdPayment.Refresh();
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

        private void txtCashAC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    string Str = Val.Left(Val.ToString(CmbPaymentType.SelectedItem), 2);
                    if (Str == "PM")//|| Str == "CR")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERCASH);
                    }
                    else if (Str == "RP")// || Str == "BR")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERBANK);
                    }
                    else if (Str == "CO")// || Str == "BR")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERBANKCASH);
                    }

                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtCashBankAC.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtCashBankAC.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                    if (Val.ToString(txtInvoiceNo.Tag) == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        DataTable DTab = objLedgerTrn.FindLedgerClosingAmt(Val.ToInt64(txtInvoiceNo.Tag), Val.ToString(CmbPaymentType.SelectedItem));
                        if (DTab.Rows.Count != 0)
                        {
                            lblBalance.Text = Val.ToString(DTab.Rows[0]["AMOUNT"]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            GrdDetPayment.SetFocusedRowCellValue("PAYMENTAMOUNT", Val.Val(txtAmount.Text));
            CalculateDiff();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                if (GrdDetPayment.RowCount != 0)
                {

                    ArrayList AL = new ArrayList();

                    this.Cursor = Cursors.WaitCursor;

                    LedgerTransactionProperty Property = new LedgerTransactionProperty();
                    int IntSrNo = 0;

                    if (Global.Confirm("Are you Sure Your Want To Save This Entry ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    for (int IntI = 0; IntI < GrdDetPayment.RowCount; IntI++)
                    {
                        DataRow DR = GrdDetPayment.GetDataRow(IntI);

                        if (Val.ToString(DR["PAYMENTTYPE"]) == "DOLLAR")
                        {
                            if (Val.Val(DR["PAYMENTAMOUNT"]) == 0)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (Val.Val(DR["FPAYMENTAMOUNT"]) == 0)
                            {
                                continue;
                            }
                        }

                        Property.SrNo = IntSrNo + 1;
                        if (Val.ToString(DR["PAYMENTTYPE"]) == "Dollar")
                        {
                            Property.ExcRateDiff = Val.ToString(txtCurrency.Text) == "USD" ? Math.Round(Math.Round((Val.Val(DR["PAYMENTAMOUNT"]) * Val.Val(DR["EXCRATE"])), 0) - Val.Val(DR["FPAYMENTAMOUNT"]), 2)
                                                   : Math.Round(Math.Round((Val.Val(DR["FPAYMENTAMOUNT"]) / Val.Val(DR["EXCRATE"])), 2) - Val.Val(DR["PAYMENTAMOUNT"]), 2);
                        }
                        AL.Add(Property);
                        IntSrNo++;
                    }
                    Property.Trn_ID = Val.ToInt64(txtTrnID.Text);

                    txtTrnID.Text = Val.ToString(Property.Trn_ID);

                    if (mFormType == FORMTYPE.PM)//|| mFormType == FORMTYPE.BP)
                    {
                        Property.EntryType = "PAYMENT";
                    }
                    else if (mFormType == FORMTYPE.RP) //|| mFormType == FORMTYPE.BR)
                    {
                        Property.EntryType = "RECEIPT";
                    }
                    else if (mFormType == FORMTYPE.CO)
                    {
                        Property.EntryType = "CONTRA";
                    }

                    // Save Master Record : 27-01-2023
                    Property.ENTRYDATE = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

                    Property.ExcRate = Val.Val(txtExcRate.Text);
                    Property.Currency_ID = Val.ToInt32(txtCurrency.Tag);

                    Property.BookType = Val.Left(CmbPaymentType.SelectedItem.ToString(), 2);
                    Property.BookTypeFull = CmbPaymentType.SelectedItem.ToString();
                    Property.FinYear = txtFinYear.Text;

                    Property.VoucherDate = Val.SqlDate(DTPEntryDate.Text);
                    Property.VoucherNo = Val.ToInt32(txtVoucherNo.Text);
                    Property.VoucherStr = txtVoucherStr.Text;

                    Property.Ledger_ID = Val.ToInt64(txtPartyAC.Tag);
                    Property.LedgerName = txtPartyAC.Text;

                    Property.RefLedger_ID = Val.ToInt64(txtCashBankAC.Tag);
                    Property.RefLedgerName = txtCashBankAC.Text;

                    Property.TrnType = Val.ToString(CmbPaymentType.SelectedItem);

                    Property.SubType = Val.ToString(CmbSubType.SelectedItem);
                    Property.RefDocNo = txtRefDocNo.Text;
                    Property.ChequeNo = "";
                    Property.Note = txtRemark.Text;

                    Property.Amount = Val.Val(txtAmount.Text);
                    Property.FAmount = Val.Val(txtAmountFE.Text);

                    Property.PAYMENTTYPE = Val.ToString(cmbPayment.SelectedItem);

                    // Save Detail Record : Dhara : 27-01-2023
                    DtabPaymentDetail.AcceptChanges();
                    DataTable DTabXml = DtabPaymentDetail.Copy();

                    DTabXml.TableName = "Table1";
                    string PaymentDetailXml = string.Empty;
                    using (StringWriter sw = new StringWriter())
                    {
                        DTabXml.WriteXml(sw);
                        PaymentDetailXml = sw.ToString();
                    }
                    Property.XMLPAYMENTDETAIL = PaymentDetailXml;
                    objLedgerTrn.Save(AL, Val.ToInt64(txtTrnID.Text));

                    this.Cursor = Cursors.Default;
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        Property = null;
                    }

                }
                else
                {
                    #region ::Expra Expense Entry::

                    ArrayList AL = new ArrayList();

                    int IntRecCount = 0;

                    this.Cursor = Cursors.WaitCursor;

                    IntRecCount++;

                    LedgerTransactionProperty Property = new LedgerTransactionProperty();

                    Property.Trn_ID = Val.ToInt64(txtTrnID.Text);
                    Property.SrNo = IntRecCount;
                    Property.GSrNo = 1;

                    if (Val.ToInt64(txtTrnID.Text) == 0)
                    {
                        string StrCheck = Val.Left(CmbPaymentType.SelectedItem.ToString(), 2);
                        while (objLedgerTrn.ISExistsVoucherNo(Val.ToInt64(txtVoucherNo.Text), StrCheck))
                        {
                            Global.Message("THIS VOUCHER NUMBER IS ALREADY GENERATED  SO SYSTEM GENERATES NEW BILL NO ? ");
                            txtVoucherNo.Text = objLedgerTrn.FindVoucherNo(txtFinYear.Text, StrCheck).ToString();
                            txtVoucherStr.Text = txtFinYear.Text + "/" + StrCheck + "/" + txtVoucherNo.Text;
                        }
                    }

                    if (mFormType == FORMTYPE.PM) //|| mFormType == FORMTYPE.BP)
                    {
                        Property.EntryType = "PAYMENT";
                        Property.PendingAmountFE = 0;//Debit Amt
                        Property.Amount = Val.Val(txtAmount.Text);
                        Property.FAmount = Val.Val(txtAmountFE.Text);
                    }
                    else if (mFormType == FORMTYPE.RP) //|| mFormType == FORMTYPE.BR)
                    {
                        Property.EntryType = "RECEIPT";
                        Property.PendingAmountFE = 0;//Credit Amt
                        Property.Amount = Val.Val(txtAmount.Text);
                        Property.FAmount = Val.Val(txtAmountFE.Text);
                    }
                    else if (mFormType == FORMTYPE.CO)
                    {
                        Property.EntryType = "CONTRA";
                        Property.CreditAmount = 0;
                        Property.Amount = Val.Val(txtAmount.Text);//Debit Amt
                        Property.FAmount = Val.Val(txtAmountFE.Text);
                    }

                    Property.BookType = Val.Left(CmbPaymentType.SelectedItem.ToString(), 2);
                    Property.BookTypeFull = CmbPaymentType.SelectedItem.ToString();
                    Property.FinYear = txtFinYear.Text;
                    Property.VoucherDate = Val.SqlDate(DTPEntryDate.Text);
                    Property.VoucherNo = Val.ToInt32(txtVoucherNo.Text);
                    Property.VoucherStr = txtVoucherStr.Text;
                    Property.Ledger_ID = Val.ToInt64(txtPartyAC.Tag);
                    Property.LedgerName = txtPartyAC.Text;
                    Property.RefLedger_ID = Val.ToInt64(txtCashBankAC.Tag);
                    Property.RefLedgerName = txtCashBankAC.Text;

                    Property.TrnType = Val.ToString(CmbPaymentType.SelectedItem);

                    //Property.RefDoc = txtRefDocNo.Text;
                    Property.ChequeNo = "";
                    Property.Note = txtRemark.Text;
                    //Property.NoteGujarati = txtRemarkGujarati.Text;

                    Property.ENTRYDATE = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

                    AL.Add(Property);

                    // SECOND ENTRY
                    IntRecCount++;

                    Property = new LedgerTransactionProperty();

                    Property.Trn_ID = Val.ToInt64(txtTrnID.Text);
                    Property.SrNo = IntRecCount;
                    Property.GSrNo = 2;

                    if (mFormType == FORMTYPE.PM) //|| mFormType == FORMTYPE.BP)
                    {
                        Property.EntryType = "PAYMENT";
                        Property.PendingAmountFE = 0;//Debit Amt
                        Property.Amount = Val.Val(txtAmount.Text);//Debit Amt
                        Property.FAmount = Val.Val(txtAmountFE.Text);
                    }
                    else if (mFormType == FORMTYPE.RP) //|| mFormType == FORMTYPE.BR)
                    {
                        Property.EntryType = "RECEIPT";
                        Property.PendingAmountFE = 0;//Credit Amt
                        Property.Amount = Val.Val(txtAmount.Text);//Debit Amt
                        Property.FAmount = Val.Val(txtAmountFE.Text);
                    }
                    else if (mFormType == FORMTYPE.CO)
                    {
                        Property.EntryType = "CONTRA";
                        Property.CreditAmount = 0;
                        Property.Amount = Val.Val(txtAmount.Text);//Debit Amt
                        Property.FAmount = Val.Val(txtAmountFE.Text);
                    }

                    Property.BookType = Val.Left(CmbPaymentType.SelectedItem.ToString(), 2);
                    Property.BookTypeFull = CmbPaymentType.SelectedItem.ToString();
                    Property.FinYear = txtFinYear.Text;
                    Property.VoucherNo = Val.ToInt32(txtVoucherNo.Text);
                    Property.VoucherDate = Val.SqlDate(DTPEntryDate.Text);

                    Property.VoucherStr = txtVoucherStr.Text;

                    Property.Ledger_ID = Val.ToInt64(txtCashBankAC.Tag);
                    Property.LedgerName = txtCashBankAC.Text;
                    Property.RefLedger_ID = Val.ToInt64(txtPartyAC.Tag);
                    Property.RefLedgerName = txtPartyAC.Text;

                    Property.TrnType = Val.ToString(CmbPaymentType.SelectedItem);
                    // Property.RefDoc = txtRefDocNo.Text;
                    Property.ChequeNo = "";
                    Property.Note = txtRemark.Text;
                    // Property.NoteGujarati = txtRemarkGujarati.Text;

                    Property.ENTRYDATE = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

                    AL.Add(Property);

                    objLedgerTrn.Save(AL, Val.ToInt64(txtTrnID.Text));

                    this.Cursor = Cursors.Default;
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                    }
                    #endregion
                }
                BtnClear.PerformClick();
            }
            catch (System.Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.txtTrnID.Text = "";
            this.txtTrnID.Tag = "";
            this.txtCashBankAC.Text = "";
            this.txtCashBankAC.Tag = "";
            this.lblBalance.Text = "( Balance )";
            this.cmbPayment.SelectedIndex = 0;

            this.txtVoucherStr.Text = "";
            this.txtVoucherNo.Text = "";

            LblMode.Text = "Add Mode";

            this.txtCurrency.Text = "INR";
            this.txtCurrency.Tag = "4";

            CmbPaymentType.SelectedIndex = 0;
            txtPartyAC.Text = "";
            txtPartyAC.Tag = "";
            txtAmountFE.Text = "0";
            txtRefDocNo.Text = "";
            txtRemark.Text = "";
            txtExcRate.Text = "";

            txtClosingAmt.Text = String.Empty;
            txtClosingAmtFE.Text = String.Empty;

            DTPEntryDate.Text = DateTime.Now.ToString();
            CmbSubType.SelectedIndex = 0;

            MainGrdPayment.DataSource = null;
            MainGrdPayment.Refresh();

            txtFinYear.Text = Global.GetFinancialYear(DTPEntryDate.Text);

            string Str = Val.Left(CmbPaymentType.SelectedItem.ToString(), 2);

            //txtVoucherStr.Text = objLedgerTrn.FindVoucherNo(txtFinYear.Text, Str).ToString();
            //txtVoucherNo.Text = txtFinYear.Text + "/" + Str + "/" + txtVoucherStr.Text;

            txtVoucherNo.Text = objLedgerTrn.FindVoucherNo(txtFinYear.Text, Str).ToString();
            txtVoucherStr.Text = txtFinYear.Text + "/" + Str + "/" + txtVoucherNo.Text;

            txtAmount.Text = string.Empty;
            txtClosingAmt.Text = string.Empty;
            txtClosingAmtFE.Text = string.Empty;

            txtInvoiceNo.Text = string.Empty;
            txtInvoiceNo.Tag = string.Empty;

            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
            CalculateDiff();

            DTPEntryDate.Focus();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (Val.ToString(txtTrnID.Text).Trim().Equals(string.Empty))
                    return;

                if (Global.Confirm("Are You Sure To Delete This Payment Record ?") == System.Windows.Forms.DialogResult.No)
                    return;

                this.Cursor = Cursors.WaitCursor;

                LedgerTransactionProperty Property = new LedgerTransactionProperty();
                Property.Trn_ID = Val.ToInt64(txtTrnID.Text);

                Property = objLedgerTrn.Delete(Property);

                Global.Message(Property.ReturnMessageDesc);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnClear_Click(null, null);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void CmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCashBankAC.Text = "";
            txtCashBankAC.Tag = "";

            string Str = Val.Left(CmbPaymentType.SelectedItem.ToString(), 2);
            switch (Str)
            {
                case "PM":
                    mFormType = FORMTYPE.PM;
                    lblAccount.Text = "Cash A/C";
                    break;
                case "RP":
                    mFormType = FORMTYPE.RP;
                    lblAccount.Text = "Bank A/C";
                    break;
                //case "BP":
                //    mFormType = FORMTYPE.BP;
                //    lblAccount.Text = "Bank A/C";
                //    txtFinanceAC.Enabled = true;
                //    txtFinanceAmount.Enabled = true;
                //    break;
                //case "BR":
                //    mFormType = FORMTYPE.BR;
                //    lblAccount.Text = "Bank A/C";
                //    txtFinanceAC.Enabled = true;
                //    txtFinanceAmount.Enabled = true;
                //    break;
                case "CO":
                    mFormType = FORMTYPE.CO;
                    lblAccount.Text = "Account";
                    break;
                default:
                    break;
            }
            txtVoucherNo.Text = objLedgerTrn.FindVoucherNo(txtFinYear.Text, Str).ToString();
            txtVoucherStr.Text = txtFinYear.Text + "/" + Str + "/" + txtVoucherNo.Text;
        }

        private void GrdDetPayment_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //try
            //{
            //    switch (e.Column.FieldName.ToString().ToUpper())
            //    {
            //        case "PAYMENTAMOUNT":
            //        case "FPAYMENTAMOUNT":

            //            if (txtCurrency.Text == "USD" && e.Column.FieldName != "FPAYMENTAMOUNT")
            //            {
            //                double fPayAmt = 0;
            //                fPayAmt = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "PAYMENTAMOUNT")) * Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "EXCRATE")); //#P:14-07-2020

            //                GrdDetPayment.SetFocusedRowCellValue("FPAYMENTAMOUNT", fPayAmt);
            //                txtAmountFE.Text = (fPayAmt).ToString();
            //                CalculateDiff();
            //                break;
            //            }
            //            else if (txtCurrency.Text != "USD" && e.Column.FieldName != "PAYMENTAMOUNT")
            //            {
            //                double PayAmt = 0;
            //                PayAmt = Math.Round(Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "FPAYMENTAMOUNT")) / Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "EXCRATE")), 5);
            //                GrdDetPayment.SetFocusedRowCellValue("PAYMENTAMOUNT", PayAmt);
            //                txtAmount.Text = (PayAmt).ToString();
            //                txtAmountFE.Text = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "FPAYMENTAMOUNT")).ToString();
            //                txtExcRate.Text = (Val.Val(txtAmountFE.Text) / Val.Val(txtAmount.Text)).ToString();
            //                CalculateDiff();
            //                break;
            //            }

            //            break;

            //        case "BANKCHARGES":
            //        case "BANKCHARGESFE":
            //            if (txtCurrency.Text == "USD" && e.Column.FieldName != "BANKCHARGESFE")
            //            {
            //                double BankCharges = 0, BankChargesFE = 0, PendingAmt = 0, PendingAmtFE = 0;
            //                BankCharges = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "BANKCHARGES")); //#P:14-07-2020
            //                BankChargesFE = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "BANKCHARGES")) * Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "EXCRATE")); //#P:14-07-2020

            //                PendingAmt = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "PENDINGAMOUNT"));
            //                PendingAmtFE = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "PENDINGAMOUNTFE"));

            //                GrdDetPayment.SetFocusedRowCellValue("BANKCHARGESFE", BankChargesFE);
            //                GrdDetPayment.SetFocusedRowCellValue("FPAYMENTAMOUNT", PendingAmtFE - BankChargesFE);
            //                GrdDetPayment.SetFocusedRowCellValue("PAYMENTAMOUNT", PendingAmt - BankCharges);
            //                CalculateDiff();
            //                break;
            //            }
            //            else if (txtCurrency.Text != "USD" && e.Column.FieldName != "BANKCHARGES")
            //            {
            //                double BankCharges = 0, BankChargesFE = 0, PendingAmt = 0, PendingAmtFE = 0;
            //                BankCharges = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "BANKCHARGESFE")) / Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "EXCRATE"));
            //                BankChargesFE = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "BANKCHARGESFE"));

            //                PendingAmt = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "PENDINGAMOUNT"));
            //                PendingAmtFE = Val.Val(GrdDetPayment.GetRowCellValue(e.RowHandle, "PENDINGAMOUNTFE"));

            //                GrdDetPayment.SetFocusedRowCellValue("BANKCHARGES", BankCharges);
            //                GrdDetPayment.SetFocusedRowCellValue("PAYMENTAMOUNT", PendingAmt - BankCharges);
            //                GrdDetPayment.SetFocusedRowCellValue("FPAYMENTAMOUNT", PendingAmtFE - BankChargesFE);
            //                CalculateDiff();
            //                break;
            //            }

            //            break;

            //        default:
            //            break;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}

        }

        private void txtCurrency_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CURRENCYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CURRENCY);

                    FrmSearch.mColumnsToHide = "CURRENCY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtCurrency.Text = Val.ToString(FrmSearch.mDRow["CURRENCYNAME"]);
                        txtCurrency.Tag = Val.ToString(FrmSearch.mDRow["CURRENCY_ID"]);

                        //if (Val.ToString(txtCurrency.Text) == "USD")
                        //    lblAmount.Text = "Amount ($)";
                        //else
                        //    lblAmount.Text = "Amount (₹)";

                        //txtExcRate.Text = new BOTRN_MemoEntry().GetExchangeRate(Val.ToInt(txtCurrency.Tag), Val.SqlDate(DTPEntryDate.Value.ToShortDateString())).ToString();
                        txtExcRate_Validated(null, null);
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

        private void txtCurrency_Validated(object sender, EventArgs e)
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                //txtExcRate.Text = new BOTRN_MemoEntry().GetExchangeRate(Val.ToInt(txtCurrency.Tag), Val.SqlDate(DTPEntryDate.Value.ToShortDateString())).ToString();
                //txtExcRate_Validated(null, null);
                //this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void txtExcRate_Validated(object sender, EventArgs e)
        {
            //if (Val.Val(txtExcRate.Text) != 0)
            //{
            //    double pDouAmountFE = 0;
            //    pDouAmountFE = Val.Val(txtExcRate.Text) * Val.Val(txtAmount.Text);
            //    txtAmountFE.Text = Val.ToString(pDouAmountFE);
            //    GrdDetPayment.SetFocusedRowCellValue("EXCRATE", Val.Val(txtExcRate.Text));
            //    txtAmountFE_Validating(null, null);
            //}
            //CalculateDiff();
            //GrdDetPayment.RefreshData();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DtabSummary = objLedgerTrn.GetBillWisePaymentGetData("SUMMARY", Val.SqlDate(DTPFromDate.Text), Val.SqlDate(DTPToDate.Text), "", 0);

                if (DtabSummary.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.Message("No Data Found.");
                    MainGrid.DataSource = null;
                    return;
                }
                MainGrid.DataSource = DtabSummary;
                GrdDet.RefreshData();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;

                    DataRow Drow = GrdDet.GetFocusedDataRow();
                    DtabPaymentDetail = objLedgerTrn.GetBillWisePaymentGetData("DETAIL", "", "", Val.ToString(Drow["BOOKTYPE"]), Val.ToInt32(Drow["VOUCHERNO"]));

                    //if (DtabPaymentDetail.Rows.Count <= 0)
                    //{
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}

                    LblMode.Text = "Edit Mode";
                    CmbPaymentType.Text = Val.ToString(Drow["BOOKTYPEFULL"]);
                    txtPartyAC.Text = Val.ToString(Drow["ACCOUNTNAME"]);
                    txtPartyAC.Tag = Val.ToString(Drow["ACCOUNT_ID"]);
                    txtCashBankAC.Text = Val.ToString(Drow["LEDGERNAME"]);
                    txtCashBankAC.Tag = Val.ToString(Drow["LEDGER_ID"]);
                    txtCurrency.Text = Val.ToString(Drow["CURRENCY"]);
                    txtCurrency.Tag = Val.ToString(Drow["CURRENCY_ID"]);
                    txtExcRate.Text = Val.ToString(Drow["EXCRATE"]);
                    CmbSubType.Text = Val.ToString(Drow["SUBTYPE"]);
                    DTPEntryDate.Text = Val.ToString(Drow["VOUCHERDATE"]);

                    txtAmount.Text = Val.ToString(Val.Val(Drow["AMOUNT"]));
                    txtAmountFE.Text = Val.ToString(Val.Val(Drow["FAMOUNT"]));

                    txtRefDocNo.Text = Val.ToString(Drow["REFDOC"]);
                    txtRemark.Text = Val.ToString(Drow["NOTE"]);

                    txtVoucherNo.Text = Val.ToString(Drow["VOUCHERNO"]);
                    txtVoucherStr.Text = Val.ToString(Drow["VOUCHERSTR"]);
                    txtFinYear.Text = Val.ToString(Drow["FINYEAR"]);

                    txtTrnID.Text = Val.ToString(Drow["TRN_ID"]);

                    //if (Val.ToBooleanToInt(Drow["CONVERTTOINR"]) == 0)
                    //    ChkBxConvertToInr.Checked = false;
                    //else
                    //    ChkBxConvertToInr.Checked = true;

                    txtInvoiceNo.Tag = Val.ToInt64(Drow["INVOICE_ID"]);
                    txtInvoiceNo.Text = Val.ToString(Drow["INVOICENO"]);
                    cmbPayment.Text = Val.ToString(Drow["PAYMENTTYPE"]);

                    DataTable DTab = objLedgerTrn.FindLedgerClosingAmt(Val.ToInt64(txtInvoiceNo.Tag), Val.ToString(CmbPaymentType.SelectedItem));
                    if (DTab.Rows.Count != 0)
                    {
                        txtClosingAmt.Text = Val.ToString(DTab.Rows[0]["AMOUNT"]);
                        txtClosingAmtFE.Text = Val.ToString(DTab.Rows[0]["FAMOUNT"]);
                    }

                    MainGrdPayment.DataSource = DtabPaymentDetail;
                    MainGrdPayment.Refresh();

                    CalculateDiff();

                    xtraTabControl1.SelectedTabPageIndex = 0;

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetPayment_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (GrdDetPayment.FocusedRowHandle < 0)
                return;
            try
            {
                DataRow Dr = GrdDetPayment.GetFocusedDataRow();

                if (txtCurrency.Text == "USD")
                {
                    GrdDetPayment.Columns["PAYMENTAMOUNT"].OptionsColumn.AllowEdit = true;
                    GrdDetPayment.Columns["FPAYMENTAMOUNT"].OptionsColumn.AllowEdit = false;

                    GrdDetPayment.Columns["BANKCHARGES"].OptionsColumn.AllowEdit = true;
                    GrdDetPayment.Columns["BANKCHARGESFE"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    GrdDetPayment.Columns["PAYMENTAMOUNT"].OptionsColumn.AllowEdit = false;
                    GrdDetPayment.Columns["FPAYMENTAMOUNT"].OptionsColumn.AllowEdit = true;

                    GrdDetPayment.Columns["BANKCHARGES"].OptionsColumn.AllowEdit = false;
                    GrdDetPayment.Columns["BANKCHARGESFE"].OptionsColumn.AllowEdit = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetPayment_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (GrdDetPayment.FocusedRowHandle < 0)
                return;
            try
            {
                DataRow Dr = GrdDetPayment.GetFocusedDataRow();

                if (txtCurrency.Text == "USD")
                {
                    GrdDetPayment.Columns["PAYMENTAMOUNT"].OptionsColumn.AllowEdit = true;
                    GrdDetPayment.Columns["FPAYMENTAMOUNT"].OptionsColumn.AllowEdit = false;

                    GrdDetPayment.Columns["BANKCHARGES"].OptionsColumn.AllowEdit = true;
                    GrdDetPayment.Columns["BANKCHARGESFE"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    GrdDetPayment.Columns["PAYMENTAMOUNT"].OptionsColumn.AllowEdit = false;
                    GrdDetPayment.Columns["FPAYMENTAMOUNT"].OptionsColumn.AllowEdit = true;

                    GrdDetPayment.Columns["BANKCHARGES"].OptionsColumn.AllowEdit = false;
                    GrdDetPayment.Columns["BANKCHARGESFE"].OptionsColumn.AllowEdit = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtAmount_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {

                if (Val.Val(txtAmount.Text) != 0)
                {
                    if (Val.ToString(cmbPayment.SelectedItem) =="Part Payment")
                    {
                        pDouPendingAmt = (Val.Val(txtClosingAmt.Text) - (Val.Val(txtAmount.Text)));
                        GrdDetPayment.SetFocusedRowCellValue("PENDINGAMOUNT", pDouPendingAmt);
                    }
                    double pDouAmountFE = 0;
                    pDouAmountFE = Val.Val(txtExcRate.Text) * Val.Val(txtAmount.Text);
                    txtAmountFE.Text = Val.ToString(pDouAmountFE);
                    GrdDetPayment.SetFocusedRowCellValue("EXCRATE", Val.Val(txtExcRate.Text));
                    GrdDetPayment.SetFocusedRowCellValue("PAYMENTAMOUNT", Val.Val(txtAmount.Text));
                    txtAmountFE_Validating(null, null);
                }
                else
                {
                    txtExcRate.Text = string.Empty;
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void txtInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "INVOICENO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    string Str = Val.Left(Val.ToString(CmbPaymentType.SelectedItem), 2);
                    FrmSearch.mDTab = objLedgerTrn.FindLedgerInvoice(Val.ToInt64(txtPartyAC.Tag), Str);
                    FrmSearch.mColumnsToHide = "Invoice_ID";
                    if (FrmSearch.mDTab.Rows.Count == 0)
                    {
                        Global.Message(Val.ToString(txtPartyAC.Text) + " Have No Pending Bills");
                        txtInvoiceNo.Text = string.Empty;
                        e.KeyChar = '\0';
                        txtCashBankAC.Focus();
                        txtClosingAmtFE.Text = string.Empty;
                        txtClosingAmt.Text = string.Empty;
                        MainGrdPayment.DataSource = null;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    txtInvoiceNo.Text = string.Empty;
                    txtInvoiceNo.Tag = string.Empty;
                    if (FrmSearch.mDRow != null)
                    {
                        txtInvoiceNo.Text = Val.ToString(FrmSearch.mDRow["INVOICENO"]);
                        txtInvoiceNo.Tag = Val.ToString(FrmSearch.mDRow["INVOICE_ID"]);
                        //lblPartyBalance.Text = Global.FindLedgerClosingStr(Val.ToInt64(txtPartyAC.Tag));
                        DataTable DTab = objLedgerTrn.FindLedgerClosingAmt(Val.ToInt64(txtInvoiceNo.Tag), Str);
                        txtExcRate.Text = Val.ToString(DTab.Rows[0]["EXCRATE"]);
                        txtClosingAmt.Text = Val.ToString(DTab.Rows[0]["AMOUNT"]);
                        txtClosingAmtFE.Text = Val.ToString(DTab.Rows[0]["FAMOUNT"]);
                        //Global.Message(txtClosingAmtFE.Text);
                    }

                    DtabPaymentDetail = objLedgerTrn.GetBillWiseOutstanding(Val.ToInt64(txtInvoiceNo.Tag), Val.ToInt64(txtPartyAC.Tag), Val.Left(Val.ToString(CmbPaymentType.SelectedItem), 2));
                    MainGrdPayment.DataSource = DtabPaymentDetail;
                    MainGrdPayment.Refresh();
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

        private void cmbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrdDetPayment.SetFocusedRowCellValue("PAYMENTTYPE", Val.ToString(cmbPayment.SelectedItem));
            txtExcRate.Focus();
        }

        private void txtAmountFE_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (Val.Val(txtAmountFE.Text) != 0)
            {
                pDouPendingAmtFE = Val.Val(txtClosingAmtFE.Text) - Val.Val(txtAmountFE.Text);
            }

            GrdDetPayment.SetFocusedRowCellValue("FPAYMENTAMOUNT", Val.Val(txtAmountFE.Text));
            GrdDetPayment.SetFocusedRowCellValue("PENDINGAMOUNTFE", pDouPendingAmtFE);

        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void DTPEntryDate_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    cmbPayment.Focus();
            //}
        }

        private void txtRemark_Validated(object sender, EventArgs e)
        {
            BtnSave.Focus();
        }

    }
}
