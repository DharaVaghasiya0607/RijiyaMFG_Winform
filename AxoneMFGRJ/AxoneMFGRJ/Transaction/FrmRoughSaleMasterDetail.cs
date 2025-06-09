using AxoneMFGRJ.Masters;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.Data;
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
    public partial class FrmRoughSaleMasterDetail : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SaleMasterDetail ObjRough = new BOTRN_SaleMasterDetail();
        BOFormPer ObjPer = new BOFormPer();

        FormType mFormType = new FormType();

        Double pDouCarat = 0;
        Double pDouGrossAmount = 0;


        DataTable DTabDetail = new DataTable();
        public enum FormType
        {
            Lot = 0,
            Rejection = 1
        }
        #region Property Settings

        public FrmRoughSaleMasterDetail()
        {
            InitializeComponent();
        }

        public void ShowForm(DataTable pDab, FormType pFormType)
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

            DTPFromReceiveDate.Value = DateTime.Now.AddMonths(-1);
            DTPToReceiveDate.Value = DateTime.Now;
            FetchInvoiceData(-1);

            int intSr = 0;
            foreach (DataRow DRow in pDab.Rows)
            {
                intSr++;
                DataRow DRNew = DTabDetail.NewRow();

                if (mFormType == FormType.Lot)
                {
                    DRNew["SRNO"] = intSr;
                    DRNew["LOT_ID"] = DRow["LOT_ID"];
                    DRNew["LOTDESCRIPTION"] = DRow["LOTNO"];
                    DRNew["REJECTION_ID"] = 0;
                    DRNew["CARAT"] = DRow["BALANCECARAT"];

                }
                if (mFormType == FormType.Rejection)
                {
                    DRNew["SRNO"] = intSr;
                    DRNew["LOT_ID"] = 0;
                    DRNew["LOTDESCRIPTION"] = DRow["REJECTIONNAME"];
                    DRNew["REJECTION_ID"] = DRow["REJECTION_ID"];
                    DRNew["CARAT"] = DRow["BALANCECARAT"];
                }
                DTabDetail.Rows.Add(DRNew);

                if (lblMode.Text == "Add Mode")
                {
                    lblBalanceCts.Visible = true;
                    lblBalanceCts.Text = Val.ToString(DRow["BALANCECARAT"]);
                }
                else
                {
                    lblBalanceCts.Visible = false;
                }
            }

            GrdDet.BestFitColumns();

            CmbPaymentType.SelectedIndex = 1;
            CmbCurrencyType.SelectedIndex = 1;
            CmbPurchaseType.SelectedIndex = 1;

            mFormType = pFormType;
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

            DTPFromReceiveDate.Value = DateTime.Now.AddMonths(-1);
            DTPToReceiveDate.Value = DateTime.Now;
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
            if (txtCustomer.Text.Trim().Length == 0)
            {
                Global.Message("Party Name Is Required");
                txtCustomer.Focus();
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
                if (Val.Val(DRow["CARAT"]) != 0 && Val.ToString(DRow["LOTDESCRIPTION"]).Trim() == "")
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Lot Description Is Required At this Row");
                    return false;
                }
                if (Val.Val(DRow["CARAT"]) != 0 && Val.Val(DRow["RATE"]) == 0)
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Rate Is Required At this Row");
                    return false;
                }
            }

            //if ((Val.Val(LblTotalCarat.Text) > Val.Val(lblBalanceCts.Text)) && lblMode.Text == "Add Mode")
            //{
            //    Global.Message("You can't Sale more then Balance Carat");
            //    return false;
            //}
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
            txtCustomer.Tag = string.Empty;
            txtCustomer.Text = string.Empty;

            txtExcRate.Text = string.Empty;
            txtOtherCompany.Text = string.Empty;
            txtPaymentTermsDays.Text = string.Empty;
            DTPDueDate.Checked = false;
            txtYourRemark.Text = string.Empty;
            txtInvoiceRemark.Text = string.Empty;
            DTPReceiveDate.Value = DateTime.Now;

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

            CmbPurchaseType.SelectedIndex = 0;
            CmbCurrencyType.SelectedIndex = 0;
            CmbPaymentType.SelectedIndex = 0;

            DTPReceiveDate.Focus();
            DTPReceiveDate_Validated(null, null);
            lblBalanceCts.Text = "0.00";
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
                    while (ObjRough.ISExistsSystemInvoiceNo(Val.ToString(txtSystemInvoiceNo.Text)) == true)
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
                    if (Val.Trim(DRow["BROKERNAME"]) == "")
                    {
                        DRow["BROKER_ID"] = 0;
                        DRow["BROKRAGEPER"] = 0;
                        DRow["BROKRAGEAMOUNT"] = 0;
                    }
                }

                DTabDetail.AcceptChanges();

                TrnSaleProperty Property = new TrnSaleProperty();
                Property.INVOICE_ID = Val.ToInt64(txtInvoiceID.Text);
                Property.INVOICEDATE = Val.SqlDate(DTPReceiveDate.Value.ToShortDateString());
                Property.INVOICEYEAR = Val.ToInt16(DTPReceiveDate.Value.Year);
                Property.INVOICEMONTH = Val.ToInt16(DTPReceiveDate.Value.Month);
                Property.INVOICENO = Val.ToInt16(txtSystemInvoiceNo.Text);

                Property.SYSTEMINVOICENO = txtSystemInvoiceNo.Text;
                Property.MANUALINVOICENO = txtManualInvoiceNo.Text;

                Property.PARTY_ID = Val.ToInt64(txtCustomer.Tag);
                Property.TERMSDAYS = Val.ToInt(txtPaymentTermsDays.Text);
                Property.PAYMENTDATE = Val.SqlDate(DTPDueDate.Value.ToShortDateString());
                Property.OTHERCOMPANY = txtOtherCompany.Text;
                Property.EXCRATE = Val.Val(txtExcRate.Text);
                Property.SALETYPE = Val.ToString(CmbPurchaseType.SelectedItem);
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
                Property.ADDLESSAMOUNT = Val.Val(txtAddLessAmountUSD.Text);
                Property.NETAMOUNT = Val.Val(txtNetAmountUSD.Text);
                Property.EXTRAINTERESTPER = Val.Val(txtExtraIntPer.Text);
                Property.INVOICEREMARK = txtInvoiceRemark.Text;
                Property.YOURREMARK = txtYourRemark.Text;

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

            DTPReceiveDate.Value = DateTime.Parse(Val.ToString(DR["INVOICEDATE"]));

            txtSystemInvoiceNo.Text = Val.ToString(DR["SYSTEMINVOICENO"]);
            txtSystemInvoiceNo.Tag = Val.ToString(DR["INVOICENO"]);

            txtManualInvoiceNo.Text = Val.ToString(DR["MANUALINVOICENO"]);
            txtCustomer.Text = Val.ToString(DR["PARTYNAME"]);
            txtCustomer.Tag = Val.ToInt64(DR["PARTY_ID"]);
            txtPaymentTermsDays.Text = Val.ToString(DR["TERMSDAYS"]);
            if (Val.IsDate(Val.ToString(DR["PAYMENTDATE"])))
            {
                DTPDueDate.Value = DateTime.Parse(Val.ToString(DR["PAYMENTDATE"]));
            }
            txtOtherCompany.Text = Val.ToString(DR["OTHERCOMPANY"]);
            txtExcRate.Text = Val.ToString(DR["EXCRATE"]);

            CmbPurchaseType.SelectedItem = Val.ToString(DR["SALETYPE"]);
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

       

        private void txtCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERSALE);

                    FrmSearch.mColumnsToHide = "LEDGER_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtCustomer.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtCustomer.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
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
                    case "LESSPER":

                        double DouSizeAvg = 0;
                        double DouGrossAmt = 0;

                        double DouCarat = 0;
                        double DouBrokerAmount = 0;

                        double DouNetAmount = 0;

                        if (Val.Val(DRow["CARAT"]) != 0)
                        {
                            DouCarat = Val.Val(DRow["CARAT"]);

                            DouGrossAmt = Math.Round(DouCarat * Val.Val(DRow["RATE"]), 2); 
                            DouBrokerAmount = Math.Round((DouGrossAmt * Val.Val(DRow["BROKRAGEPER"])) / 100, 2);
                            if (Val.Val(DRow["LESSPER"]) > 0)
                            {
                                DouNetAmount = Math.Round((DouGrossAmt - (DouGrossAmt * Val.Val(DRow["LESSPER"])) / 100), 2);
                            }
                            else
                            {
                                DouNetAmount = 0;
                            }

                            DRow["GROSSAMOUNT"] = DouGrossAmt;
                            DRow["BROKRAGEAMOUNT"] = DouBrokerAmount;
                            DRow["GROSSBROKAMOUNT"] = DouGrossAmt + DouBrokerAmount;
                            DRow["NETAMOUNT"] = DouNetAmount;

                        }
                        CalculateSummary();
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
            double DouNetAmount = 0;

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + Val.ToInt(DRow["PCS"]);
                DouCarat = DouCarat + Val.Val(DRow["CARAT"]);
                DouGrossAmount = DouGrossAmount + Val.Val(DRow["GROSSAMOUNT"]);
                DouBrokrageAmount = DouBrokrageAmount + Val.Val(DRow["BROKRAGEAMOUNT"]);
                DouNetAmount = DouNetAmount + Val.Val(DRow["NETAMOUNT"]);
            }

            txtGrossAmountUSD.Text = DouNetAmount.ToString();
            LblTotalCarat.Text = Val.Format(DouCarat, "########0.00");
       

            txtTermsAmountUSD.Text = Math.Round(DouGrossAmount * Val.Val(txtTermsPer.Text) / 100, 2).ToString();
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
                Val.Val(txtAddLessAmountUSD.Text), 2
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
            if (Val.ISDate(DTPReceiveDate.Text) == true)
            {
                DTPDueDate.Value = DateTime.Parse(DTPReceiveDate.Value.ToShortDateString()).AddDays(Val.ToInt(txtPaymentTermsDays.Text));
            }
        }

        private void DTPDueDate_Validated(object sender, EventArgs e)
        {
            txtPaymentTermsDays.Text = Val.DateDiff(Microsoft.VisualBasic.DateInterval.Day, DTPReceiveDate.Value.ToShortDateString(), DTPDueDate.Value.ToShortDateString()).ToString();
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
                string StrPartyID = "";

                if (DTPFromReceiveDate.Checked == true && DTPToReceiveDate.Checked == true)
                {
                    StrFromInvoiceDate = Val.SqlDate(DTPFromReceiveDate.Value.ToShortDateString());
                    StrToInvoiceDate = Val.SqlDate(DTPToReceiveDate.Value.ToShortDateString());
                }

                StrPartyID = Val.Trim(ChkCmbSupplier.Properties.GetCheckedItems().ToString());

                this.Cursor = Cursors.WaitCursor;
                GrdDetList.BeginUpdate();
                DataTable DTabData = ObjRough.GetDataForRoughPurchase(StrFromInvoiceDate, StrToInvoiceDate, StrPartyID);
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
            DataSet DS = ObjRough.GetInvoiceDetail(InvoiceID);

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
                TrnSaleProperty Property = new TrnSaleProperty();
                Property.INVOICE_ID = Val.ToInt64(txtInvoiceID.Text);
                Property.SALE_ID = 0;
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
            txtSystemInvoiceNo.Tag = ObjRough.FindNewInvoiceNo(DTPReceiveDate.Value.Year, DTPReceiveDate.Value.Month);
            txtSystemInvoiceNo.Text = DTPReceiveDate.Value.Year.ToString() + "/" + Global.GetMonthName(DTPReceiveDate.Value.Month) + "/" + Val.ToString(txtSystemInvoiceNo.Tag);
            if (lblMode.Text == "Add Mode")
            {
                txtManualInvoiceNo.Text = txtSystemInvoiceNo.Text;
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
                 double pDouBalanceCts = 0;
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
                        DRow["LOT_ID"] = DRowPre["LOT_ID"];
                        DRow["LOTDESCRIPTION"] = DRowPre["LOTDESCRIPTION"];
                        DRow["BROKER_ID"] = DRowPre["BROKER_ID"];
                        DRow["BROKERNAME"] = DRowPre["BROKERNAME"];
                        DRow["BROKRAGEPER"] = DRowPre["BROKRAGEPER"];
                    }
                    pDouBalanceCts = ((Val.Val(lblBalanceCts.Text) - Val.Val(GrdDet.GetFocusedRowCellValue("CARAT"))));
                    lblBalanceCts.Text = Val.Format(pDouBalanceCts, "########0.00");

                    DTabDetail.Rows.Add(DRow);
                    GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    GrdDet.FocusedColumn = GrdDet.Columns["CARAT"];

                }
                else
                {
                    txtInvoiceRemark.Focus();
                }
            }

        }

        // Dhara : 14-12-2022

      
        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow Drow = GrdDet.GetFocusedDataRow();
                if (Val.ToString(Drow["SALE_ID"]).Trim().Equals(string.Empty))
                {
                    GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
                    MainGrid.Refresh();
                }
                else
                {


                    if (GrdDet.FocusedRowHandle >= 0)
                    {
                        if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                        {
                            TrnSaleProperty Property = new TrnSaleProperty();
                            Property.SALE_ID = Val.ToInt64(Drow["SALE_ID"]);
                            Property.INVOICE_ID = 0;

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
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }

        }

        #region grid event

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    pDouCarat = 0;
                    pDouGrossAmount = 0;
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    pDouCarat = pDouCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "CARAT"));
                    pDouGrossAmount = Math.Round(pDouGrossAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "GROSSAMOUNT")), 2);

                }


                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(pDouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(pDouGrossAmount) / Val.Val(pDouCarat), 2);
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

        #endregion

    }
}
