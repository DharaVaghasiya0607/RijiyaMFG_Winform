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
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using BarcodeLib.Barcode;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmCrapsIssueReturn : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_CrapsIssueReturn ObjTrn = new BOTRN_CrapsIssueReturn();
        DataTable DTabPacket = new DataTable();
        DataSet DSTab = new DataSet();
        DataTable DTabTransaction = new DataTable();
        DataTable DTabRejectionCts = new DataTable();
        string mStrParentFormType = "", TRN_IDEDIT = "";
        bool mBoolAutoConfirm = false;
        double DouReadyCts1 = 0;
        double DouReadyPcs1 = 0;
        double DouLossCts1 = 0;
        string StrFromDate = null;
        string StrToDate = null;

        public enum FORMTYPE
        {
            TRANSFER = 1,
            STAFFISSUE = 2,
            STAFFRETURN = 3
        }

        public FORMTYPE mFormType { get; set; }

        #region Property Settings

        public FrmCrapsIssueReturn()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            //mStrParentFormType = ParentFormType;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DTPFromDate.Value = DateTime.Now.AddMonths(-1);
            DTPToDate.Value = DateTime.Now;

            //mFormType = pFormType;
            
            //MainGrid.DataSource = DTabPacket;
            //GrdDet.RefreshData();
            RbtIssue_CheckedChanged(null, null);
            btnShow_Click(null, null);
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
        
        public bool ValSave()
        {
            if (txtKapanName.Text.Trim().Length == 0)
            {
                Global.MessageError("Kapan Name Is Required");
                txtKapanName.Focus();
                return false;
            }
            if (RbtIssue.Checked == true)
            {
                if (txtKapanName.Text.Trim().Length > 0 && Val.Val(txtIssueCarat.Text) <= 0)
                {
                    Global.MessageError("Issue Carat Is Required");
                    txtIssueCarat.Focus();
                    return false;
                }
            }
            if (RbtReturn.Checked == true)
            {
                if (txtKapanName.Text.Trim().Length > 0 && Val.Val(txtReadyCarat.Text) <= 0)
                {
                    Global.MessageError("Ready Carat Is Required");
                    txtReadyCarat.Focus();
                    return false;
                }
            }

            return true;
        }

        public void Fill()
        {
            DSTab = ObjTrn.GetTransactionViewData("", 0, 0, StrFromDate, StrToDate);
            DTabTransaction = DSTab.Tables[0];
            DTabRejectionCts = DSTab.Tables[1];
            if (DTabTransaction.Rows.Count <= 0)
            {
                //Global.Message("No Data Found..");
                return;
            }
            MainGrid.DataSource = DTabTransaction;
            MainGrid.Refresh();
            GrdDet.BestFitMaxRowCount = 500;
            GrdDet.BestFitColumns();
        }

        public void CLEARDATA()
        {
            RbtIssue.Checked = true;
            txtKapanName.Tag = string.Empty;
            txtKapanName.Text = string.Empty;
            txtIssueCarat.Text = string.Empty;
            txtReadyCarat.Text = string.Empty;
            txtLossCarat.Text = string.Empty;
            txtSrNo.Text = string.Empty;
            TRN_IDEDIT = "";
            lblBalanceCtsValue.Text = "0.00";
            lblBalanceCtsValue.Tag = "0";
            txtKapanName.Focus();
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }    
                if (Global.Confirm("Are You Sure You Want To Save ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                //txtJangedNo.Text = string.Empty;
                TrnCrapsIssueReturnProperty Property = new TrnCrapsIssueReturnProperty();
                if (RbtIssue.Checked == true)
                    Property.TRN_ID = 0;
                else
                    Property.TRN_ID = Val.ToInt64(TRN_IDEDIT);
                Property.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToInt(txtSrNo.Text);
                Property.ISSUEPCS = Val.ToInt32(1);
                Property.ISSUECARAT = Val.Val(txtIssueCarat.Text);
                Property.READYPCS = Val.ToInt32(1);
                Property.READYCARAT = Val.Val(txtReadyCarat.Text);
                if (Val.Val(txtLossCarat.Text) == 0)
                    Property.LOSTPCS = Val.ToInt32(0);
                else
                    Property.LOSTPCS = Val.ToInt32(1);
                Property.LOSTCARAT = Val.Val(txtLossCarat.Text);
                DateTime date = DateTime.Now;
                Property.TRANSDATE = date.ToString();

                Property = ObjTrn.CrapsSave(Property);

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    GrdDet.RefreshData();
                    BtnClear_Click(null, null);
                }
                this.Cursor = Cursors.Default;
                Property = null;
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }
        
        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.RowHandle < 0)
            //{
            //    return;
            //}
            //DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
            //switch (e.Column.FieldName.ToUpper())
            //{
            //    case "READYCARAT":
            //        double DouReady = Val.Val(DRow["READYCARAT"]);
            //        double DouIssue = Val.Val(DRow["ISSUECARAT"]);
            //        double DouLossCarat = Math.Round(DouIssue - DouReady, 3);
            //        DouLossCts1 = DouLossCarat;
            //        if (DouLossCarat < 0)
            //        {
            //            Global.MessageError("Loss Carat Is Less Then Zero");
            //            GrdDet.SetRowCellValue(e.RowHandle, "READYCARAT", DouIssue);
            //            GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", 0);
            //        }
            //        else
            //        {
            //            GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", DouLossCarat);
            //        }
            //        break;
            //    case "LOSTPCS":
            //        double DouLostPcs = Val.Val(DRow["LOSTPCS"]);
            //        double DouReadyCts = Val.Val(DRow["READYCARAT"]);
            //        double DouReadyPcs = Val.Val(DRow["READYPCS"]);
            //        double DouLossCts = Val.Val(DRow["LOSSCARAT"]);
            //        double DouIssuePcs = Val.Val(DRow["ISSUEPCS"]);
            //        if (DouLostPcs > DouIssuePcs)
            //        {
            //            Global.MessageError("Issue Pcs Is Greater Then Lost Pcs");
            //            GrdDet.SetRowCellValue(e.RowHandle, "READYPCS", DouReadyPcs);
            //            GrdDet.SetRowCellValue(e.RowHandle, "READYCARAT", DouReadyCts);
            //            GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", DouLossCts);
            //            GrdDet.SetRowCellValue(e.RowHandle, "LOSTCARAT", 0);
            //        }
            //        else if (DouLostPcs == 0)
            //        {
            //            GrdDet.SetRowCellValue(e.RowHandle, "READYPCS", DouReadyPcs1);
            //            GrdDet.SetRowCellValue(e.RowHandle, "READYCARAT", DouReadyCts1);
            //            GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", DouLossCts1);
            //            GrdDet.SetRowCellValue(e.RowHandle, "LOSTCARAT", 0);
            //        }
            //        else
            //        {
            //            GrdDet.SetRowCellValue(e.RowHandle, "READYPCS", 0);
            //            GrdDet.SetRowCellValue(e.RowHandle, "READYCARAT", 0);
            //            GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", 0);
            //            GrdDet.SetRowCellValue(e.RowHandle, "LOSTCARAT", DouReadyCts);
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }

        private void RbtIssue_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtIssue.Checked == true)
            {
                lblKapanName.Visible = true;
                txtKapanName.Visible = true;
                lblIssueCarat.Visible = true;
                txtIssueCarat.Visible = true;
                lblSrNo.Visible = true;
                txtSrNo.Visible = true;
                txtSrNo.Enabled = false;
                lblReadyCarat.Visible = true;
                txtReadyCarat.Visible = true;
                txtReadyCarat.Enabled = false;
                lblLossCarat.Visible = true;
                txtLossCarat.Visible = true;
                txtLossCarat.Enabled = false;
                txtIssueCarat.Enabled = true;
                lblBalanceCarat.Visible = true;
                lblBalanceCtsValue.Visible = true;
                txtKapanName.Focus();
            }
            else
            {
                lblKapanName.Visible = true;
                txtKapanName.Visible = true;
                lblIssueCarat.Visible = true;
                txtIssueCarat.Visible = true;
                lblSrNo.Visible = true;
                txtSrNo.Visible = true;
                txtSrNo.Enabled = true;
                lblReadyCarat.Visible = true;
                txtReadyCarat.Visible = true;
                txtReadyCarat.Enabled = true;
                lblLossCarat.Visible = true;
                txtLossCarat.Visible = true;
                txtIssueCarat.Enabled = false;
                txtLossCarat.Enabled = false;
                lblBalanceCarat.Visible = false;
                lblBalanceCtsValue.Visible = false;
                txtKapanName.Focus();
            }
        }

        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.Transaction.BOTRN_SinglePacketCreate().FindKapan();

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            CLEARDATA();
        }

        private void txtKapanName_Validated(object sender, EventArgs e)
        {
            if (RbtIssue.Checked == true)
            {
                lblBalanceCtsValue.Text = string.Empty;
                lblBalanceCtsValue.Tag = string.Empty;

                StrFromDate = null;
                StrToDate = null;
                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                DSTab = ObjTrn.GetTransactionViewData(txtKapanName.Text, Val.ToInt64(txtKapanName.Tag), 0, "", "");
                DTabRejectionCts = DSTab.Tables[1];
                if (DTabRejectionCts.Rows.Count <= 0)
                {
                    Global.Message("No Data Found..");
                    txtKapanName.Focus();
                    return;
                }
                if (DTabRejectionCts.Rows.Count > 0)
                {
                    lblBalanceCtsValue.Text = Val.ToString(DTabRejectionCts.Rows[0]["REJECTIONCARAT"]);
                    lblBalanceCtsValue.Tag = Val.ToString(DTabRejectionCts.Rows[0]["KAPAN_ID"]);
                    txtIssueCarat.Focus();
                }
            }
            if (RbtReturn.Checked == true)
            {
                txtSrNo.Focus();
            }

        }

        private void txtSrNo_Validated(object sender, EventArgs e)
        {
            if (RbtReturn.Checked == true)
            {
                if (txtSrNo.Text.Trim().Length != 0)
                {
                    StrFromDate = null;
                    StrToDate = null;
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                    DSTab = ObjTrn.GetTransactionViewData(txtKapanName.Text, Val.ToInt64(txtKapanName.Tag), Val.ToInt32(txtSrNo.Text), StrFromDate, StrToDate);
                    DTabPacket = DSTab.Tables[0];
                    DTabRejectionCts = DSTab.Tables[1];
                    if (DTabPacket.Rows.Count <= 0)
                    {
                        Global.Message("No Data Found..");
                        txtKapanName.Focus();
                        return;
                    }
                    if (DTabPacket.Rows.Count > 0)
                    {
                        txtIssueCarat.Text = Val.ToString(DTabPacket.Rows[0]["ISSUECARAT"]);
                        txtReadyCarat.Text = Val.ToString(DTabPacket.Rows[0]["ISSUECARAT"]);
                        TRN_IDEDIT = Val.ToString(DTabPacket.Rows[0]["TRN_ID"]);
                        txtReadyCarat.Focus();
                    }
                }
            }
        }

        private void txtReadyCarat_Validated(object sender, EventArgs e)
        {
            if (RbtReturn.Checked == true)
            {
                double IssCrt = 0.000;
                double ReadyCrt = 0.000;
                double LossCrt = 0.000;
                IssCrt = Val.ToDouble(txtIssueCarat.Text);
                ReadyCrt = Val.ToDouble(txtReadyCarat.Text);
                if (IssCrt < ReadyCrt)
                {
                    Global.MessageError("Ready Carat Is Greater Then Issue Carat");
                    txtReadyCarat.Focus();
                    return;
                }
                LossCrt = IssCrt - ReadyCrt;
                txtLossCarat.Text = LossCrt.ToString();
                BtnSave.Focus();
            }
        }

        private void txtIssueCarat_Validated(object sender, EventArgs e)
        {
            if (RbtIssue.Checked == true)
            {
                double IssCrt = 0.000;
                double blncCrt = 0.000;
                IssCrt = Val.ToDouble(txtIssueCarat.Text);
                blncCrt = Val.ToDouble(lblBalanceCtsValue.Text);
                if (blncCrt < IssCrt)
                {
                    Global.MessageError("Issue Carat Is Greater Then Rejection Balance Carat");
                    txtIssueCarat.Focus();
                    return;
                }
                BtnSave.Focus();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            StrFromDate = null;
            StrToDate = null;
            StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
            StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
            Fill();
            txtKapanName.Focus();
        }

    }
}
