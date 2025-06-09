using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Collections;
using BusLib.Configuration;
using BusLib.Account;
using BusLib;
using BusLib.TableName;
using AxoneMFGRJ.Masters;

namespace AxoneMFGRJ.Account
{
    public partial class FrmPaymentEntry : DevExpress.XtraEditors.XtraForm
    {
        BOComboFill combo = new BOComboFill();
        BOLedgerTransaction objLedger = new BOLedgerTransaction();
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
      
        string GuidRecord_Id = string.Empty;

        public FORMTYPE mFormType = FORMTYPE.P;

        public enum FORMTYPE
        {
            P = 0,
            R = 1,            
            CO = 2
            //CP = 0,
            //BP = 1,
            //CR = 2,
            //BR = 3,
            //CO = 4
        }

        #region Constructor

        public FrmPaymentEntry()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType = FORMTYPE.P)
        {
           
            mFormType = pFormType;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            //GetData();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            BtnDelete.Enabled = ObjPer.ISDELETE;

            DTPVoucherDate.Value = DateTime.Now;

            DTPSFromDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            DTPSToDate.Text = DateTime.Now.ToShortDateString();

            txtFinYear.Text = Global.GetFinancialYear(DTPVoucherDate.Text);

          
            if (pFormType == FORMTYPE.P)
            {
                CmbVoucherType.SelectedIndex = 0; 
              
            }
            else if (pFormType == FORMTYPE.R)
            {
                CmbVoucherType.SelectedIndex = 1; 
              
            }
            else if (pFormType == FORMTYPE.CO)
            {
                CmbVoucherType.SelectedIndex = 2;
            }
            //else if (pFormType == FORMTYPE.CR)
            //{
            //    CmbVoucherType.SelectedIndex = 2; 
            //}
            //else if (pFormType == FORMTYPE.BR)
            //{
            //    CmbVoucherType.SelectedIndex = 3;
            //}
            
            this.Text = CmbVoucherType.SelectedItem.ToString().ToUpper();
            BtnNew_Click(null, null);

            string[] StrTrnType = System.Configuration.ConfigurationManager.AppSettings["TrnType"].ToString().Split(',');
            CmbTrnType.Items.Clear();
            foreach (string Str in StrTrnType)
            {
                CmbTrnType.Items.Add(Str);
                CmbChkTrnType.Properties.Items.Add(Str);
            }
            CmbTrnType.SelectedIndex = 0;

	    }

        private void AttachFormDefaultEvent()
        {
            this.KeyPreview = true;
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = false;
            ObjFormEvent.FormResize = false;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(objLedger);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        #region Validation


        private bool ValSave()
        {

            if (Val.ISDate(DTPVoucherDate.Text) == false)
            {
                Global.Message("Receive Date Is Required");
                DTPVoucherDate.Focus();
                return false;
            }
            if (Val.ToString(txtVoucherNo.Text.Trim()) == "")
            {
                Global.Message("Voucher No Is Required");
                txtVoucherNo.Focus();
                return false;
            }
            if (Val.ToString(txtAccount.Text.Trim()) == "")
            {
                Global.Message("Source Account Is Required");
                txtAccount.Focus();
                return false;
            }

            if (Val.ToString(txtPartyAccount.Text.Trim()) == "")
            {
                Global.Message("Party Account Is Required");
                txtAccount.Focus();
                return false;
            }
            if (Val.ToString(txtAmount.Text.Trim()) == "")
            {
                Global.Message("Amount Is Required");
                txtAmount.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region KeyPress Events

        private void txtSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    string Str = Val.Left(Val.ToString(CmbVoucherType.SelectedItem), 2);
                    if (Str == "PM")//|| Str == "CR")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERCASH);
                    }
                    else if (Str == "RP" )//|| Str == "BR")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERBANK);
                    }
                    else if (Str == "CO")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERBANKCASH);
                    }
                    
                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtAccount.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtAccount.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    lblBalance.Text = Global.FindLedgerClosingStr(Val.ToInt64(txtAccount.Tag));

                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        #endregion

        #region Control Events

        private void BtnNew_Click(object sender, EventArgs e)
        {
            this.txtTrnID.Text = "";
            this.txtTrnID.Tag = "";
            this.txtAccount.Text = "";
            this.txtAccount.Tag = "";
            lblBalance.Text = "( Balance )";
            lblPartyBalance.Text = "( Balance )";

            this.txtVoucherNo.Text = "";
            this.txtVoucherStr.Text = "";

            CmbTrnType.SelectedIndex = 0;
            txtPartyAccount.Text = "";
            txtPartyAccount.Tag = "";
            txtAmount.Text = "0";
            txtRefDocNo.Text = "";
            txtRemark.Text = "";
            txtRemarkGujarati.Text = "";

            DTPVoucherDate.Focus();

            string Str = Val.Left(CmbVoucherType.SelectedItem.ToString(), 2);

            txtVoucherNo.Text = objLedger.FindVoucherNo(txtFinYear.Text, Str).ToString();

            txtVoucherStr.Text = txtFinYear.Text + "/" + Str + "/" + txtVoucherNo.Text;

            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }                
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
                    string StrCheck = Val.Left(CmbVoucherType.SelectedItem.ToString(), 2);

                    while (objLedger.ISExistsVoucherNo(Val.ToInt64(txtVoucherNo.Text), StrCheck))
                    {
                        Global.Message("THIS VOUCHER NUMBER IS ALREADY GENERATED  SO SYSTEM GENERATES NEW BILL NO ? ");
                        txtVoucherNo.Text = objLedger.FindVoucherNo(txtFinYear.Text, StrCheck).ToString();
                        txtVoucherStr.Text = txtFinYear.Text + "/" + StrCheck + "/" + txtVoucherNo.Text;
                    }
                }

                if (mFormType == FORMTYPE.P) //|| mFormType == FORMTYPE.BP)
                {
                    Property.EntryType = "PAYMENT";

                    //Property.CreditAmount = 0  ;
                    //Property.DebitAmount = Val.Val(txtAmount.Text);
                }
                else if (mFormType == FORMTYPE.R) //|| mFormType == FORMTYPE.BR)
                {
                    Property.EntryType = "RECEIPT";
                   // Property.CreditAmount = Val.Val(txtAmount.Text);
                    //Property.DebitAmount = 0;
                }
                else if (mFormType == FORMTYPE.CO)
                {
                    Property.EntryType = "CONTRA";
                   // Property.CreditAmount = 0;
                   // Property.DebitAmount = Val.Val(txtAmount.Text);
                }

                Property.BookType = Val.Left(CmbVoucherType.SelectedItem.ToString(), 2);
                Property.BookTypeFull = CmbVoucherType.SelectedItem.ToString();
                Property.FinYear = txtFinYear.Text;
                Property.VoucherDate = Val.SqlDate(DTPVoucherDate.Text);
                Property.VoucherNo = Val.ToInt32(txtVoucherNo.Text);
                Property.VoucherStr = txtVoucherStr.Text;
                Property.Ledger_ID = Val.ToInt64(txtAccount.Tag);
                Property.LedgerName = txtAccount.Text;
                Property.RefLedger_ID = Val.ToInt64(txtPartyAccount.Tag);
                Property.RefLedgerName = txtPartyAccount.Text;

                Property.TrnType = Val.ToString(CmbTrnType.SelectedItem);

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

                if (mFormType == FORMTYPE.P) //|| mFormType == FORMTYPE.BP)
                {
                    Property.EntryType = "PAYMENT";
                    //Property.CreditAmount = Val.Val(txtAmount.Text);
                    //Property.DebitAmount = 0;
                }
                else if (mFormType == FORMTYPE.R) //|| mFormType == FORMTYPE.BR)
                {
                    Property.EntryType = "RECEIPT";
                    //Property.CreditAmount = 0;
                    //Property.DebitAmount = Val.Val(txtAmount.Text);
                                    }
                else if (mFormType == FORMTYPE.CO)
                {
                    Property.EntryType = "CONTRA";
                   // Property.CreditAmount =Val.Val(txtAmount.Text);
                   // Property.DebitAmount = 0;
                }

                Property.BookType = Val.Left(CmbVoucherType.SelectedItem.ToString(), 2);
                Property.BookTypeFull = CmbVoucherType.SelectedItem.ToString();
                Property.FinYear = txtFinYear.Text;
                Property.VoucherNo = Val.ToInt32(txtVoucherNo.Text);
                Property.VoucherDate = Val.SqlDate(DTPVoucherDate.Text);

                Property.VoucherStr = txtVoucherStr.Text;
                Property.Ledger_ID = Val.ToInt64(txtPartyAccount.Tag);
                Property.LedgerName = txtPartyAccount.Text;
                Property.RefLedger_ID = Val.ToInt64(txtAccount.Tag);
                Property.RefLedgerName = txtAccount.Text;
                Property.TrnType = Val.ToString(CmbTrnType.SelectedItem);

               // Property.RefDoc = txtRefDocNo.Text;
                Property.ChequeNo = "";
                Property.Note = txtRemark.Text;
               // Property.NoteGujarati = txtRemarkGujarati.Text;

                Property.ENTRYDATE = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

                AL.Add(Property);

                string StrResu = objLedger.Save(AL, Val.ToInt64(txtTrnID.Text));

                this.Cursor = Cursors.Default;

                Global.Message(StrResu);

                if (!StrResu.Contains ("FAIL"))
                {
                    this.txtTrnID.Text = "";
                    this.txtTrnID.Tag = "";

                    this.txtVoucherNo.Text = "";
                    this.txtVoucherStr.Text = "";
                    lblBalance.Text = "( Balance )";
                    lblPartyBalance.Text = "( Balance )";

                    CmbTrnType.SelectedIndex = 0;
                    txtPartyAccount.Text = "";
                    txtPartyAccount.Tag = "";
                    txtAmount.Text = "0";
                    txtRefDocNo.Text = "";
                    txtRemark.Text = "";
                    txtRemarkGujarati.Text = "";
                    lblBalance.Text = "";
                    lblPartyBalance.Text = "";

                    DTPVoucherDate.Focus();

                    string Str = Val.Left(CmbVoucherType.SelectedItem.ToString(), 2);

                    txtVoucherNo.Text = objLedger.FindVoucherNo(txtFinYear.Text, Str).ToString();

                    txtVoucherStr.Text = txtFinYear.Text + "/" + Str + "/" + txtVoucherNo.Text;

                    Global.SelectLanguage(Global.LANGUAGE.ENGLISH);

                    lblBalance.Text = Global.FindLedgerClosingStr(Val.ToInt64(txtAccount.Tag));

                    BtnShow_Click(null, null);

                    if (GrdDetSummary.RowCount > 1)
                    {
                        GrdDetSummary.FocusedRowHandle = GrdDetSummary.RowCount - 1;
                    }
                }
               
            }
            catch (System.Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (ValSave() == false)
                {
                    return;
                }

                if (Global.Confirm("Are You Sure To Delete This Voucher") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                //if (objLedger.Delete(Val.ToInt64(txtTrnID.Text)) != -1)
                //{
                //    Global.Message("SUCCESSFULLY DELETED");
                //}
                BtnNew_Click(null, null);
                BtnShow_Click(null, null);
       
            }
            catch (System.Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

       
        private void CmbVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTrnID.Text.Length == 0)
            {
                txtAccount.Text = "";
                txtAccount.Tag = "";

                string Str = Val.Left(CmbVoucherType.SelectedItem.ToString(), 2);
                switch (Str)
                {
                    case "P":
                        mFormType = FORMTYPE.P;
                        lblType.Text = "જાવક";
                        lblAccount.Text = "Cash A/C";
                        break;
                    case "R":
                        mFormType = FORMTYPE.R;
                        lblType.Text = "આવક";
                        lblAccount.Text = "Bank A/C";
                        break;
                    //case "BP":
                    //    mFormType = FORMTYPE.BP;
                    //    lblType.Text = "જાવક";
                    //    lblAccount.Text = "Bank A/C";
                    //    break;
                    //case "BR":
                    //    mFormType = FORMTYPE.BR;
                    //    lblType.Text = "આવક";
                    //    lblAccount.Text = "Bank A/C";
                    //    break;
                    case "CO":
                        mFormType = FORMTYPE.CO;
                        lblType.Text = "કોન્ટ્રા";
                        lblAccount.Text = "Account";
                        break;
                    default:
                        break;
                }

                txtVoucherNo.Text = objLedger.FindVoucherNo(txtFinYear.Text, Str).ToString();

                txtVoucherStr.Text = txtFinYear.Text + "/" + Str + "/" + txtVoucherNo.Text;

            }
          
        }

        #region Enter Event

        private void ControlEnterForGujarati_Enter(object sender, EventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.GUJARATI);
        }
        private void ControlEnterForEnglish_Enter(object sender, EventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
        }


        #endregion

        private void GrdDetSummary_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Clicks == 2)
            {
                DataRow DRow = GrdDetSummary.GetDataRow(e.RowHandle);

                txtFinYear.Text = Val.ToString(DRow["FINYEAR"]);

                DTPVoucherDate.Text = Val.ToDate(Val.ToString(DRow["VOUCHERDATE"]), AxonDataLib.BOConversion.DateFormat.DDMMYYYY);

                CmbVoucherType.SelectedItem = Val.ToString(DRow["BOOKTYPEFULL"]);
                txtVoucherNo.Text = Val.ToString(DRow["VOUCHERNO"]);
                txtVoucherStr.Text = Val.ToString(DRow["VOUCHERSTR"]);
                txtTrnID.Text = Val.ToString(DRow["TRN_ID"]);
                txtAccount.Tag = Val.ToString(DRow["LEDGER_ID"]);
                txtAccount.Text = Val.ToString(DRow["LEDGERNAME"]);
                

                DataTable DTabTrn = objLedger.GetDetail(Val.ToInt64(txtTrnID.Text));

                CmbTrnType.SelectedIndex =0;
                txtPartyAccount.Text = "";
                txtPartyAccount.Tag = "";
                txtAmount.Text =  "0";
                txtRefDocNo.Text = "";
                txtRemark.Text = "";

                if (DTabTrn.Rows.Count != 0)
                {
                    CmbTrnType.SelectedItem = Val.ToString(DTabTrn.Rows[0]["TRNTYPE"]);
                    txtPartyAccount.Text = Val.ToString(DTabTrn.Rows[0]["REFLEDGERNAME"]);
                    txtPartyAccount.Tag = Val.ToString(DTabTrn.Rows[0]["REFLEDGER_ID"]);
                    txtAmount.Text = Val.ToString(DTabTrn.Rows[0]["FINAL_AMOUNT"]);
                    txtRefDocNo.Text = Val.ToString(DTabTrn.Rows[0]["REFDOC"]);
                    txtRemark.Text = Val.ToString(DTabTrn.Rows[0]["NOTE"]);
                    txtRemarkGujarati.Text = Val.ToString(DTabTrn.Rows[0]["NOTEGUJARATI"]);
                }
                lblBalance.Text = Global.FindLedgerClosingStr(Val.ToInt64(txtAccount.Tag));
                lblPartyBalance.Text = Global.FindLedgerClosingStr(Val.ToInt64(txtPartyAccount.Tag));

                CmbTrnType.Focus();

                //xtraTabControl1.SelectedTabPageIndex = 0;
            }
        }

        private void txtRefLedgerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {

                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    string Str = Val.Left(Val.ToString(CmbVoucherType.SelectedItem), 2);

                    if (Str == "CP" || Str == "CR" || Str == "BR" || Str == "BR")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERNONCASH);
                    }
                    else if (Str == "CO")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERBANKCASH);
                    }
                    else
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERALL);
                    }
                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {

                        txtPartyAccount.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtPartyAccount.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        lblPartyBalance.Text = Global.FindLedgerClosingStr(Val.ToInt64(txtPartyAccount.Tag));
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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            DataTable DTabSummary = objLedger.GetSummary(Val.SqlDate(DTPSFromDate.Text), Val.SqlDate(DTPSToDate.Text), CmbVoucherType.SelectedItem.ToString(),Val.Trim(CmbChkTrnType.Properties.GetCheckedItems()));
            MainGridSummary.DataSource = DTabSummary;
            MainGridSummary.Refresh();
        }

        private void BtnAddCountry_Click(object sender, EventArgs e)
        {
            FrmLedger FrmLedger = new FrmLedger();
            FrmLedger.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmLedger);
            FrmLedger.ShowForm("LEDGER");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmLedger FrmLedger = new FrmLedger();
            FrmLedger.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmLedger);
            FrmLedger.ShowForm("LEDGER");
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            lblInWords.Text = Global.NumbersToWords(Val.ToInt32(txtAmount.Text));
        }

        private void FrmPaymentEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            GrdDetSummary.BestFitColumns();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (GrdDetSummary.RowCount != 0)
            {
                SaveFileDialog Dialog = new SaveFileDialog();
                Dialog.Filter = ".xls|.xlsx";
                if (Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    GrdDetSummary.ExportToXlsx(Dialog.FileName);

                    if (Global.Confirm("Do You Want To Open The File ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Dialog.FileName, "CMD");
                    }
                }

            }
        }

    }
}