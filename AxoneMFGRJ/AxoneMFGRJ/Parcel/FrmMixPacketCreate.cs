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
using BusLib.Rapaport;
using BusLib.Master;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmMixPacketCreate : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();
        string mStrKapanCategory = "ORIGINAL";
        BOFormPer ObjPer = new BOFormPer();

        Int64 pIntPacket_ID = 0;
        string pStrOpeRej = "";

        EmployeeActionRightsProperty EmployeeRightsProperty = new EmployeeActionRightsProperty();

        public enum FORMTYPE
        {
            TransferToParcel = 1,
            MixPacketCreate = 2
        }

        public FORMTYPE mFormType { get; set; }

        string StrXmlForRejection = "";

        #region Property Settings

        public FrmMixPacketCreate()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName, string pStrManager, Int64 pIntManagerID, Int64 pIntKapan_ID, string StrKapanCategory, double pDouPktCts, Int32 pIntPktPcs)
        {
            Val.FormGeneralSetting(this);

            AttachFormDefaultEvent();
            DTPEntryDate.Text = DateTime.Now.ToShortDateString();

            mStrKapanCategory = StrKapanCategory;

            txtKapan.Text = pStrKapanName;
            txtKapan.Tag = pIntKapan_ID;

            txtManager.Text = pStrManager;
            txtManager.Tag = pIntManagerID;

            GetRoughBalance();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            txtPassForUpdatePacket.Tag = Val.ToString(ObjPer.PASSWORD);

            bool pBoolPktno = ObjPacket.CheckMixPacketNoExists(txtKapan.Text, Val.ToInt32(txtPacketNo.Text));
            {
                if (pBoolPktno == false)
                {
                    if (Global.Confirm("This Packet No. Is Already Created \n\n Are You Want To Merge Packet ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataRow DRow = ObjPacket.GetMixPacket_ID(pStrKapanName, pIntKapan_ID);
                        pIntPacket_ID = Val.ToInt64(DRow["PACKET_ID"]);
                        txtPktCts.Visible = true;
                        lblPktCts.Visible = true;
                        txtPktPcs.Visible = true;
                        lblPktPcs.Visible = true;
                        txtPacketNo.Tag = pIntPacket_ID;
                        txtPktCts.Text = Val.ToString(pDouPktCts);
                        txtPktPcs.Text = Val.ToString(pIntPktPcs);
                        lblMode.Text = "Edit Mode";
                    }
                    else
                    {
                        return;

                    }
                }
                else
                {
                    txtPktCts.Visible = false;
                    lblPktCts.Visible = false;
                    txtPktPcs.Visible = false;
                    lblPktPcs.Visible = false;
                    lblMode.Text = "Add Mode";
                }
            }

            this.Show();
            BtnContinue_Click(null, null);
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            DTPEntryDate.Text = DateTime.Now.ToShortDateString();

            txtKapan.Focus();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            txtPassForUpdatePacket.Tag = Val.ToString(ObjPer.PASSWORD);

            this.Show();
        }

        public void ShowForm(string StrXMLValues, double pDouCarat, FORMTYPE pStrFormType, Int64 pIntKapan_ID, string pStrKapanName, double PDouPktCarat, Int32 pIntPktNo, string pStrMarkerName, Int64 pIntMarker_ID, Int64 PIntPkt_ID)
        {

            Val.FormGeneralSetting(this);

            AttachFormDefaultEvent();
            mFormType = pStrFormType;

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            txtPassForUpdatePacket.Tag = Val.ToString(ObjPer.PASSWORD);

            StrXmlForRejection = StrXMLValues;

            txtRejectCarat.Text = Val.ToString(pDouCarat);
            pDouCarat = pDouCarat + PDouPktCarat;
            txtCarat.Text = Val.ToString(pDouCarat);
            txtPcs.Text = "1";
            txtCarat.Enabled = false;

            txtKapan.Tag = Val.ToString(pIntKapan_ID);
            txtKapan.Text = Val.ToString(pStrKapanName);

            txtBalanceCarat.Text = Val.ToString(PDouPktCarat);
            this.Show();

            pStrOpeRej = "REJECT";
            pIntPacket_ID = PIntPkt_ID;
            if (pIntPacket_ID == 0)
            {
                BtnContinue_Click(null, null);
            }
            else
            {
                lblMode.Text = "Edit Mode";
                PanelKapan.Enabled = false;
                txtKapan.Text = pStrKapanName;
                txtKapan.Tag = pIntKapan_ID;
                txtPacketNo.Text = Val.ToString(pIntPktNo);
                txtPacketNo.Tag = pIntPacket_ID;
                txtMarker.Text = Val.ToString(pStrMarkerName);
                txtMarker.Tag = pIntMarker_ID;
                txtMarker.Enabled = false;
            }

            txtPktCts.Visible = false;
            lblPktCts.Visible = false;
            txtPktPcs.Visible = false;
            lblPktPcs.Visible = false;

        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);

        }

        #endregion


        public void GetRoughBalance()
        {
            try
            {
                string pStrKapanID = ObjPacket.FindKapanID(txtKapan.Text);
                txtKapan.Tag = pStrKapanID;

                mStrKapanCategory = ObjPacket.FindKapanCategory(txtKapan.Text);

                DataRow DR = ObjPacket.GetBalancePcsCarat(Val.ToString(txtKapan.Text), Val.ToInt64(txtKapan.Tag));
                if (DR == null)
                {
                    txtBalanceCarat.Text = "";
                }
                else
                {
                    txtBalanceCarat.Text = Val.ToString(DR["BALANCECARAT"]);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);

            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (txtKapan.Text.Trim() == string.Empty)
            {
                Global.Message("Please Select Kapan First.");
                return;
            }
            GetRoughBalance();
            PanelKapan.Enabled = false;
            txtPcs.Focus();
        }

        public bool ValSave()
        {
            if (PanelKapan.Enabled == true)
            {
                Global.Message("First Press the [Cont.] Button For Further Process");
                BtnContinue.Focus();
                return false;
            }
            if (txtKapan.Text.Trim() == string.Empty)
            {
                Global.Message("Kapan Name Is Required");
                return false;
            }

            if (Val.Val(txtPcs.Text) == 0)
            {
                Global.Message("Packet Pcs Is Required");
                txtPcs.Focus();
                return false;
            }
            if (Val.Val(txtCarat.Text) == 0)
            {
                Global.Message("Packet Carat Is Required");
                txtCarat.Focus();
                return false;
            }
            if (Val.Val(txtCarat.Text) < 0)
            {
                Global.Message("Please Enter Proper Packet Carat.");
                txtCarat.Focus();
                return false;
            }

            return true;
        }


        private void BtnIssue_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                if (txtColor.Text.Trim().Length == 0) txtColor.Tag = string.Empty;
                if (txtClarity.Text.Trim().Length == 0) txtClarity.Tag = string.Empty;
                if (txtColorShade.Text.Trim().Length == 0) txtColorShade.Tag = string.Empty;
                if (txtFL.Text.Trim().Length == 0) txtFL.Tag = string.Empty;
                if (txtLBLC.Text.Trim().Length == 0) txtLBLC.Tag = string.Empty;
                if (txtNatts.Text.Trim().Length == 0) txtNatts.Tag = string.Empty;
                if (txtMilky.Text.Trim().Length == 0) txtMilky.Tag = string.Empty;
                if (txtTension.Text.Trim().Length == 0) txtTension.Tag = string.Empty;

                TrnSinglePacketCreationProperty Property = new TrnSinglePacketCreationProperty();

                if (pIntPacket_ID == 0)
                {
                    Property.PACKET_ID = 0;
                }
                else
                {
                    Property.PACKET_ID = pIntPacket_ID;
                }

                Property.KAPAN_ID = Val.ToInt64(txtKapan.Tag);

                Property.KAPANNAME = txtKapan.Text;
                Property.MAINMANAGER_ID = Val.ToInt64(txtManager.Tag);

                Property.KAPANCATEGORY = mStrKapanCategory;

                Property.PACKETNO = Val.ToInt32(txtPacketNo.Text);

                Property.COLOR_ID = Val.ToInt32(txtColor.Tag);
                Property.CLARITY_ID = Val.ToInt32(txtClarity.Tag);
                Property.COLORSHADE_ID = Val.ToInt32(txtColorShade.Tag);
                Property.FL_ID = Val.ToInt32(txtFL.Tag);

                Property.LBLC_ID = Val.ToInt32(txtLBLC.Tag);
                Property.NATTS_ID = Val.ToInt32(txtNatts.Tag);
                Property.MILKY_ID = Val.ToInt32(txtMilky.Tag);
                Property.TENSION_ID = Val.ToInt32(txtTension.Tag);

                Property.LOTPCS = Val.ToInt32(txtPcs.Text);
                Property.LOTCARAT = Val.Val(txtCarat.Text);

                Property.ENTRYDATE = Val.SqlDate(DTPEntryDate.Value.ToShortDateString());

                Property.AMOUNT = Val.Val(txtExpDollar.Text);
                Property.REMARK = txtRemark.Text;

                Property.JANGEDNO = Val.ToInt64(lblJangedNo.Text);

                Property.MARKER_ID = Val.ToInt64(txtMarker.Tag);
                Property.MARKERCODE = Val.ToString(txtMarker.Text);

                Property.PACKETGROUP_ID = Val.ToInt32(txtGroup.Tag);

                Property.PACKETTYPE = "MIX"; // Dhara : 18-04-2022

                Property.STRXMLFORREJECTION = StrXmlForRejection;
                Property.OPEREJECT = pStrOpeRej;
                Property.REJECTCARAT = Val.Val(txtRejectCarat.Text);

                Property = ObjPacket.CreateMixPacket(Property, lblMode.Text);

                lblMessage.Text = Property.ReturnMessageDesc;

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    if (ChkPrintBarcode.Checked == true)
                    {
                        Global.BarcodePrint(txtKapan.Text, txtPacketNo.Text, "", DTPEntryDate.Value.ToString("dd-MM-yyyy"), txtCarat.Text, BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE);
                    }

                    lblJangedNo.Text = Val.ToString(Property.ReturnValueJangedNo);

                    txtPacketNo.Text = string.Empty;
                    txtPacketNo.Tag = string.Empty;

                    GetRoughBalance();

                    ChkPacketEdit.Checked = false;

                    lblMode.Text = "Add Mode";
                    txtPcs.Text = string.Empty;
                    txtCarat.Text = string.Empty;
                    txtRemark.Text = string.Empty;
                    txtExpDollar.Text = string.Empty;
                    txtPktPcs.Text = string.Empty;
                    txtPktCts.Text = string.Empty;

                    txtExpDollar.Text = "";
                    txtCarat.Focus();

                    this.Close();
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    txtPcs.Focus();
                }

                Property = null;

                this.Cursor = Cursors.Default;


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);

            }
        }

        #region Key Press Events


        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BOComboFill.TABLE.TRN_KAPANSINGLE);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapan.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);

                        txtManager.Text = Val.ToString(FrmSearch.mDRow["MANAGERNAME"]);
                        txtManager.Tag = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);
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

        private void txtColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "COLORCODE,COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);
                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtColor.Text = Val.ToString(FrmSearch.mDRow["COLORNAME"]);
                        txtColor.Tag = Val.ToString(FrmSearch.mDRow["COLOR_ID"]);
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


        private void txtClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "CLARITYCODE,CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);
                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtClarity.Text = Val.ToString(FrmSearch.mDRow["CLARITYNAME"]);
                        txtClarity.Tag = Val.ToString(FrmSearch.mDRow["CLARITY_ID"]);
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

        private void txtColorShade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "COLORSHADECODE,COLORSHADENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLORSHADE);
                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtColorShade.Text = Val.ToString(FrmSearch.mDRow["COLORSHADENAME"]);
                        txtColorShade.Tag = Val.ToString(FrmSearch.mDRow["COLORSHADE_ID"]);
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

        private void txtFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "FLCODE,FLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_FL);
                    FrmSearch.mColumnsToHide = "FL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtFL.Text = Val.ToString(FrmSearch.mDRow["FLNAME"]);
                        txtFL.Tag = Val.ToString(FrmSearch.mDRow["FL_ID"]);
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

        private void txtLBLC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "LBLCCODE,LBLCNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LBLC);
                    FrmSearch.mColumnsToHide = "LBLC_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtLBLC.Text = Val.ToString(FrmSearch.mDRow["LBLCNAME"]);
                        txtLBLC.Tag = Val.ToString(FrmSearch.mDRow["LBLC_ID"]);
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

        private void txtNatts_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "NATTSCODE,NATTSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_NATTS);
                    FrmSearch.mColumnsToHide = "NATTS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtNatts.Text = Val.ToString(FrmSearch.mDRow["NATTSNAME"]);
                        txtNatts.Tag = Val.ToString(FrmSearch.mDRow["NATTS_ID"]);
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

        private void txtMilky_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "MILKYCODE,MILKYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MILKY);
                    FrmSearch.mColumnsToHide = "MILKY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtMilky.Text = Val.ToString(FrmSearch.mDRow["MILKYNAME"]);
                        txtMilky.Tag = Val.ToString(FrmSearch.mDRow["MILKY_ID"]);
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

        private void txtTension_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "TENSIONCODE,TENSIONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_TENSION);
                    FrmSearch.mColumnsToHide = "TENSION_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtTension.Text = Val.ToString(FrmSearch.mDRow["TENSIONNAME"]);
                        txtTension.Tag = Val.ToString(FrmSearch.mDRow["TENSION_ID"]);
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
        private void txtGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "PACKETGROUPCODE,PACKETGROUPNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PACKETGROUP);
                    FrmSearch.mColumnsToHide = "PACKETGROUP_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtGroup.Text = Val.ToString(FrmSearch.mDRow["PACKETGROUPNAME"]);
                        txtGroup.Tag = Val.ToString(FrmSearch.mDRow["PACKETGROUP_ID"]);
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
        #endregion

        private void ChkPacketEdit_CheckedChanged(object sender, EventArgs e)
        {
            txtPacketNo.ReadOnly = !ChkPacketEdit.Checked;
        }

        private void txtPacketNo_Validated(object sender, EventArgs e)
        {
            if (ChkPacketEdit.Checked == true)
            {
                lblMode.Text = "Add Mode";

                txtPcs.Text = "1";
                txtCarat.Text = string.Empty;

                txtColor.Text = string.Empty;
                txtColor.Tag = string.Empty;

                txtClarity.Text = string.Empty;
                txtClarity.Tag = string.Empty;

                txtColorShade.Text = string.Empty;
                txtColorShade.Tag = string.Empty;

                txtFL.Text = string.Empty;
                txtFL.Tag = string.Empty;

                txtMilky.Text = string.Empty;
                txtMilky.Tag = string.Empty;

                txtNatts.Text = string.Empty;
                txtNatts.Tag = string.Empty;

                txtTension.Text = string.Empty;
                txtTension.Tag = string.Empty;

                txtLBLC.Text = string.Empty;
                txtLBLC.Tag = string.Empty;

                txtExpDollar.Text = string.Empty;
                txtExpDollar.Tag = string.Empty;

                txtRemark.Text = "";

                txtMarker.Tag = string.Empty;
                txtMarker.Text = string.Empty;

                DataRow DRow = ObjPacket.GetSinglePacketDetail(txtKapan.Text, Val.ToInt(txtPacketNo.Text));
                if (DRow != null)
                {
                    lblMode.Text = "Edit Mode";

                    txtPcs.Text = "1";
                    txtCarat.Text = Val.ToString(DRow["LOTCARAT"]);

                    txtColor.Text = Val.ToString(DRow["COLORNAME"]);
                    txtColor.Tag = Val.ToString(DRow["COLOR_ID"]);

                    txtClarity.Text = Val.ToString(DRow["CLARITYNAME"]);
                    txtClarity.Tag = Val.ToString(DRow["CLARITY_ID"]);

                    txtColorShade.Text = Val.ToString(DRow["COLORSHADENAME"]);
                    txtColorShade.Tag = Val.ToString(DRow["COLORSHADE_ID"]);

                    txtFL.Text = Val.ToString(DRow["FLNAME"]);
                    txtFL.Tag = Val.ToString(DRow["FL_ID"]);

                    txtMilky.Text = Val.ToString(DRow["MILKYNAME"]);
                    txtMilky.Tag = Val.ToString(DRow["MILKY_ID"]);

                    txtNatts.Text = Val.ToString(DRow["NATTSNAME"]);
                    txtNatts.Tag = Val.ToString(DRow["NATTS_ID"]);

                    txtTension.Text = Val.ToString(DRow["TENSIONNAME"]);
                    txtTension.Tag = Val.ToString(DRow["TENSION_ID"]);

                    txtLBLC.Text = Val.ToString(DRow["LBLCNAME"]);
                    txtLBLC.Tag = Val.ToString(DRow["LBLC_ID"]);

                    txtExpDollar.Text = Val.ToString(DRow["AMOUNT"]);

                    txtRemark.Text = Val.ToString(DRow["REMARK"]);

                    txtMarker.Tag = Val.ToString(DRow["MARKER_ID"]);
                    txtMarker.Text = Val.ToString(DRow["MARKERCODE"]);

                    txtCarat.Focus();
                    if (Val.ToString(txtPassForUpdatePacket.Tag) != "" && Val.ToString(txtPassForUpdatePacket.Tag).ToUpper() == txtPassForUpdatePacket.Text.ToUpper() && lblMode.Text.ToUpper() == "EDIT MODE")
                    {
                        BtnSave.Enabled = true;
                    }
                    else
                    {
                        BtnSave.Enabled = false;
                    }
                }
                else
                    BtnSave.Enabled = true; // Add : #P : 19-02-2020

            }

        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            lblMode.Text = "Add Mode";

            txtPcs.Text = "1";
            txtCarat.Text = string.Empty;

            txtColor.Text = string.Empty;
            txtColor.Tag = string.Empty;

            txtClarity.Text = string.Empty;
            txtClarity.Tag = string.Empty;

            txtColorShade.Text = string.Empty;
            txtColorShade.Tag = string.Empty;

            txtFL.Text = string.Empty;
            txtFL.Tag = string.Empty;

            txtMilky.Text = string.Empty;
            txtMilky.Tag = string.Empty;

            txtNatts.Text = string.Empty;
            txtNatts.Tag = string.Empty;

            txtTension.Text = string.Empty;
            txtTension.Tag = string.Empty;

            txtLBLC.Text = string.Empty;
            txtLBLC.Tag = string.Empty;

            txtExpDollar.Text = string.Empty;
            txtExpDollar.Tag = string.Empty;

            txtRemark.Text = "";
            PanelKapan.Enabled = true;

            txtKapan.Text = string.Empty;
            txtKapan.Text = string.Empty;
            txtManager.Text = string.Empty;
            txtManager.Text = string.Empty;
            txtManager.Text = string.Empty;
            txtManager.Tag = string.Empty;

            txtPacketNo.Text = string.Empty;
            txtPacketNo.Tag = string.Empty;

            txtBalanceCarat.Text = "0.00";

            ChkPacketEdit.Checked = false;

            txtMarker.Tag = string.Empty;
            txtMarker.Text = string.Empty;

            txtGroup.Text = string.Empty;
            txtGroup.Tag = string.Empty;

            txtKapan.Focus();
            txtRejectCarat.Text = string.Empty;
            BtnSave.Enabled = true;
        }

        private void txtPrevKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearch FrmSearch = new FrmSearch();
            //        FrmSearch.SearchField = "KAPANNAME";
            //        FrmSearch.SearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;

            //        if (mStrKapanCategory == "PCN")
            //            FrmSearch.DTab = new BOFindRap().GetRejectionPCNKapan();
            //        else
            //            FrmSearch.DTab = ObjPacket.FindKapan();

            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.DRow != null)
            //        {
            //            txtPrevKapan.Text = Val.ToString(FrmSearch.DRow["KAPANNAME"]);

            //            txtPrevPacketNo.Text = string.Empty;
            //            txtPrevPacketTag.Text = string.Empty;
            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
        }

        private void txtPrevPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearch FrmSearch = new FrmSearch();
            //        FrmSearch.SearchField = "PACKETNO";
            //        FrmSearch.SearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;

            //        if (mStrKapanCategory == "PCN")
            //            FrmSearch.DTab = new BOFindRap().GetRejectionPCNPacketNo(txtPrevKapan.Text);
            //        else
            //            FrmSearch.DTab = new BOFindRap().GetPacketNo(txtPrevKapan.Text);

            //        FrmSearch.ColumnsToHide = "";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.DRow != null)
            //        {
            //            txtPrevPacketNo.Text = Val.ToString(FrmSearch.DRow["PACKETNO"]);

            //            txtPrevPacketTag.Text = string.Empty;
            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.MessageError(ex.Message);
            //}
        }

        private void txtPrevPacketTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearch FrmSearch = new FrmSearch();
            //        FrmSearch.SearchField = "PACKETNO,TAG";
            //        FrmSearch.SearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;

            //        if (mStrKapanCategory == "PCN")
            //            FrmSearch.DTab = new BOFindRap().GetRejectionPCNTag(txtPrevKapan.Text, Val.ToInt32(txtPrevPacketNo.Text));
            //        else
            //            FrmSearch.DTab = new BOFindRap().GetTag(txtPrevKapan.Text, Val.ToInt32(txtPrevPacketNo.Text));

            //        FrmSearch.ColumnsToHide = "KAPAN_ID,PACKET_ID,EMPLOYEE_ID";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.DRow != null)
            //        {
            //            txtPrevPacketTag.Text = Val.ToString(FrmSearch.DRow["TAG"]);
            //            txtPrevPacketNo.Tag = Val.ToString(FrmSearch.DRow["PACKET_ID"]);
            //            txtPrevKapan.Tag = Val.ToString(FrmSearch.DRow["KAPAN_ID"]);

            //            if (mStrKapanCategory == "PCN")
            //            {
            //                txtMarker.Tag = Val.ToString(FrmSearch.DRow["EMPLOYEE_ID"]);
            //                txtMarker.Text = Val.ToString(FrmSearch.DRow["EMPLOYEECODE"]);
            //            }
            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.MessageError(ex.Message);
            //}
        }

        private void txtPassForUpdatePacket_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToString(txtPassForUpdatePacket.Tag) != "" && Val.ToString(txtPassForUpdatePacket.Tag).ToUpper() == txtPassForUpdatePacket.Text.ToUpper() && lblMode.Text.ToUpper() == "EDIT MODE")
            {
                BtnSave.Enabled = true;
            }
            else
            {
                BtnSave.Enabled = false;
            }
        }

        private void txtExpDollar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ChkFocus.Checked == false)
            {
                txtRemark.TabStop = true;
                txtColor.TabStop = true;
                txtClarity.TabStop = true;
                txtColorShade.TabStop = true;
                txtFL.TabStop = true;
                txtLBLC.TabStop = true;
                txtNatts.TabStop = true;
                txtMilky.TabStop = true;
                txtTension.TabStop = true;
            }
            else
            {
                panel1.TabStop = false;
            }
        }
    }
}
