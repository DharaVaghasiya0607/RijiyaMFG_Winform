using BusLib.TableName;
using BusLib.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmRepairingEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_CrapsIssueReturn ObjTrn = new BOTRN_CrapsIssueReturn();
        string TRN_IDEDIT = "";
        DataSet DSTab = new DataSet();
        DataTable DTabTransaction = new DataTable();
        DataTable DTabPacket = new DataTable();
        string StrFromDate = null;
        string StrToDate = null;
        #region Property Setting
        public FrmRepairingEntry()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DTPFromDate.Value = DateTime.Now.AddMonths(-1);
            DTPToDate.Value = DateTime.Now;

            RbtIssue_CheckedChanged(null, null);
            btnShow_Click(null, null);
            this.Show();
        }

        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            //ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }
        #endregion

        private void TxtParty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtParty.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        TxtParty.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
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
        private bool ValSave()
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
            if (TxtParty.Text.Trim().Length == 0)
            {
                Global.MessageError("Party Name Is Required");
                TxtParty.Focus();
                return false;
            }

            return true;
        }
        private void RbtIssue_CheckedChanged(object sender, EventArgs e)
        {
            if(RbtIssue.Checked == true)
            {
                lblKapanName.Visible = true;
                txtKapanName.Visible = true;
                lblIssueCarat.Visible = true;
                txtIssueCarat.Visible = true;
                txtSrNo.Visible = true;
                txtSrNo.Enabled = false;
                txtReadyCarat.Enabled = false;
                txtReadyCarat.Visible = true;
                txtSrNo.Visible = true;
                txtSrNo.Enabled = false;
                lblLossCarat.Visible = true;
                txtLossCarat.Visible = true;
                txtLossCarat.Enabled = false;
                txtIssueCarat.Enabled = true;
                txtKapanName.Focus();
            }
            else
            {
                txtSrNo.Visible = true;
                txtSrNo.Enabled = true;
                lblIssueCarat.Visible = true;
                txtIssueCarat.Visible = true;
                txtIssueCarat.Enabled = false;
                txtLossCarat.Enabled = false; lblReadyCarat.Visible = true;
                txtReadyCarat.Visible = true;
                txtReadyCarat.Enabled = true;
                TxtParty.Enabled = false;
                CmbReturnType.Enabled = false;
                txtKapanName.Focus();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(ValSave() == false)
                {
                    return;
                }
                if (Global.Confirm("Are You Sure You Want To Save ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                TrnCrapsIssueReturnProperty Property = new TrnCrapsIssueReturnProperty();
                if (RbtIssue.Checked == true)
                    Property.TRN_ID = 0;
                else
                    Property.TRN_ID = Val.ToInt64(TRN_IDEDIT);
                Property.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToInt(txtSrNo.Text);
                Property.ISSUEPCS = Val.ToInt32(TxtPcs.Text);
                Property.ISSUECARAT = Val.Val(txtIssueCarat.Text);
                Property.READYPCS = Val.ToInt32(TxtPcs.Text);
                Property.READYCARAT = Val.Val(txtReadyCarat.Text);
                if (Val.Val(txtLossCarat.Text) == 0)
                    Property.LOSTPCS = Val.ToInt32(0);
                else
                    Property.LOSTPCS = Val.ToInt32(TxtPcs.Text);
                Property.LOSTCARAT = Val.Val(txtLossCarat.Text);
                DateTime date = DateTime.Now;
                Property.TRANSDATE = date.ToString();
                Property.PARTY_ID = Val.ToInt32(TxtParty.Tag);
                Property.RETURNTYPE = Val.ToString(CmbReturnType.Text);
                Property = ObjTrn.RepairingEntrySave(Property);

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
            catch(Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void Fill()
        {
            DSTab = ObjTrn.GetRepairingEntryData("", 0, 0,0, StrFromDate, StrToDate);
            DTabTransaction = DSTab.Tables[0];
            if (DTabTransaction.Rows.Count <= 0)
            {               
                return;
            }
            MainGrid.DataSource = DTabTransaction;
            MainGrid.Refresh();
            GrdDet.BestFitMaxRowCount = 500;
            GrdDet.BestFitColumns();
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            RbtIssue.Checked = true;
            txtKapanName.Tag = string.Empty;
            txtKapanName.Text = string.Empty;
            txtIssueCarat.Text = string.Empty;
            txtReadyCarat.Text = string.Empty;
            txtLossCarat.Text = string.Empty;
            txtSrNo.Text = string.Empty;
            TRN_IDEDIT = "";
            TxtPcs.Text = string.Empty;
            TxtParty.Text = string.Empty;
            TxtParty.Tag = string.Empty;
            CmbReturnType.Text = string.Empty;
            TxtParty.Focus();
        }

        private void txtSrNo_Validated(object sender, EventArgs e)
        {
            if(RbtReturn.Checked == true)
            {
                if (txtSrNo.Text.Trim().Length != 0)
                {

                    StrFromDate = null;
                    StrToDate = null;
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                    DSTab = ObjTrn.GetRepairingEntryData(txtKapanName.Text, Val.ToInt64(txtKapanName.Tag), Val.ToInt32(txtSrNo.Text), Val.ToInt32(TxtParty.Tag), StrFromDate, StrToDate);
                    DTabPacket = DSTab.Tables[0];                    
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
                        TxtPcs.Text = Val.ToString(DTabPacket.Rows[0]["ISSUEPCS"]);
                        TxtParty.Text = Val.ToString(DTabPacket.Rows[0]["PARTYNAME"]);
                        TxtParty.Tag = Val.ToInt32(DTabPacket.Rows[0]["PARTY_ID"]);
                        CmbReturnType.Text = Val.ToString(DTabPacket.Rows[0]["RETURNTYPE"]);
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

        private void TxtPcs_Validated(object sender, EventArgs e)
        {
            if (RbtReturn.Checked == true)
            {
                int IssPcs = 0;
                int ReadyPcs = 0;
                IssPcs = Val.ToInt(DTabPacket.Rows[0]["ISSUEPCS"]);
                ReadyPcs = Val.ToInt(TxtPcs.Text);
                if (ReadyPcs > IssPcs)
                {
                    Global.MessageError("Ready Pcs Is Greater Then Issue Pcs");
                    TxtPcs.Focus();
                    return;
                }               
                BtnSave.Focus();
            }
        }
    }
}
