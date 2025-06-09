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
    public partial class FrmMakablePacketCreate : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();
        string mStrKapanCategory = "ORIGINAL";
        BOFormPer ObjPer = new BOFormPer();

        string pStrOpeRej = "";
        Int64 pIntFromEmp_ID = 0;
        Int64 pIntToManager_ID = 0;
        Int32 pIntToDepartment_ID = 0;

        EmployeeActionRightsProperty EmployeeRightsProperty = new EmployeeActionRightsProperty();

        public enum FORMTYPE
        {
            TransferToParcel = 1
        }

        public FORMTYPE mFormType { get; set; }


        string StrXmlForRejection = "";

        #region Property Settings

        public FrmMakablePacketCreate()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName, Int64 pIntKapan_ID, string pStrMainPacketNo, Int64 pIntMainPacket_ID, string pStrMarkerCode, Int64 pStrMarker_ID, double pDouBalanceCarat, string pStrEmpCode, Int64 pINtEmp_ID)
        {
            Val.FormGeneralSetting(this);

            AttachFormDefaultEvent();
            DTPEntryDate.Value = DateTime.Now;

            txtKapan.Text = pStrKapanName;
            txtKapan.Tag = pIntKapan_ID;

            txtMainPktNo.Text = pStrMainPacketNo;
            txtMainPktNo.Tag = pIntMainPacket_ID;

            txtBalanceCarat.Text = Val.ToString(pDouBalanceCarat);
            pIntFromEmp_ID = pINtEmp_ID;

            this.Show();
            BtnContinue_Click(null, null);


            if (lblMode.Text == "Add Mode")
            {
                BtnAddNewCode.Enabled = true;
            }
            else
            {
                BtnAddNewCode.Enabled = false;
            }
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

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);

        }

        #endregion

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnExit_Click(null, null);
            //}
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
            PanelKapan.Enabled = false;
            GetXPktBalance();
            GenerateMaxPacketNoKapanWise();

            txtPcs.Focus();
        }

        public void GetXPktBalance()
        {
            try
            {
                DataRow DR = ObjPacket.GetMakPktBalancePcsCarat(Val.ToInt64(txtKapan.Tag), Val.ToInt64(txtMainPktNo.Tag));
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

        public void GetXPktNo()
        {
            try
            {
                DataTable DTab = ObjPacket.FindMakPacketNo(Val.ToInt64(txtKapan.Tag));
                if (DTab.Rows.Count == 0)
                {
                    txtPacketNo.Text = string.Empty;
                    txtPacketNo.Tag = string.Empty;
                }
                else
                {
                    txtMainPktNo.Tag = Val.ToString(DTab.Rows[0]["PACKET_ID"]);
                    txtMainPktNo.Text = Val.ToString(DTab.Rows[0]["PACKETNO"]);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);

            }
        }


        public void GenerateMaxPacketNoKapanWise()
        {
            txtPacketNo.Text = ObjPacket.FindNewMixPacketNo(Val.ToInt64(txtKapan.Tag)).ToString();
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

            if (Val.Val(txtPacketNo.Text) == 0)
            {
                Global.Message("Packet Number Not Generated... Please Check In Program");
                txtPacketNo.Focus();
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

            //if (txtMarker.Text.Length == 0)
            //{
            //    Global.Message("Please Enter Marker ");
            //    txtMarker.Focus();
            //    return false;
            //}
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

                Property.PACKET_ID = 0;
                Property.KAPAN_ID = Val.ToInt64(txtKapan.Tag);
                Property.KAPANNAME = txtKapan.Text;
                Property.MAINPACKET_ID = Val.ToInt64(txtMainPktNo.Tag);

                Property.KAPANCATEGORY = mStrKapanCategory;

                Property.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                Property.TAG = txtTag.Text;

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

                Property.ENTRYDATE = DTPEntryDate.Value.ToString();

                Property.AMOUNT = Val.Val(txtExpDollar.Text);
                Property.REMARK = txtRemark.Text;

                Property.JANGEDNO = Val.ToInt64(lblJangedNo.Text);

                Property.TOEMPLOYEE_ID = Val.ToInt64(txtMarker.Tag);
                Property.TODEPARTMENT_ID = pIntToDepartment_ID;
                Property.TOMANAGER_ID = pIntToManager_ID;

                Property.FROMEMPLOYEE_ID = pIntFromEmp_ID;

                Property.MARKER_ID = Val.ToInt64(txtMarker.Tag);
                Property.MARKERCODE = Val.ToString(txtMarker.Text);

                Property.EXPPER = Val.Val(txtExpPer.Text);
                Property.EXPWEIGHT = Val.Val(txtExpWeight.Text);

                Property.PACKETTYPE = "ORIGINAL";

                Property = ObjPacket.CreateMakeblePacket(Property, lblMode.Text);

                lblMessage.Text = Property.ReturnMessageDesc;


                if (Property.ReturnMessageType == "SUCCESS")
                {
                    //if (ChkPrintBarcode.Checked == true)
                    //{
                    //    txtBarcodeNo.Text = Property.RETURNBARCODENO;
                    //    Global.BarcodePrint(txtKapan.Text, txtPacketNo.Text, txtTag.Text, DTPEntryDate.Value.ToString("dd-MM-yyyy"), txtCarat.Text, txtMarker.Text, "", txtBarcodeNo.Text, txtPcs.Text);
                    //}

                    lblJangedNo.Text = Val.ToString(Property.ReturnValueJangedNo);

                    txtPacketNo.Text = string.Empty;
                    txtPacketNo.Tag = string.Empty;
                    txtBarcodeNo.Text = string.Empty;

                    TrnSinglePacketCreationProperty MaxPacketProperty = new TrnSinglePacketCreationProperty();
                    MaxPacketProperty.KAPAN_ID = Val.ToInt64(txtKapan.Tag);
                    MaxPacketProperty = ObjPacket.FindNewPacketNoWithKapan(MaxPacketProperty);
                    if (MaxPacketProperty.ReturnMessageType == "FAIL")
                    {
                        txtPacketNo.Text = "0";
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    GenerateMaxPacketNoKapanWise();
                    GetXPktBalance();
                    MaxPacketProperty = null;
                }

                ChkPacketEdit.Checked = false;

                lblMode.Text = "Add Mode";
                txtPcs.Text = "1";
                txtCarat.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtExpDollar.Text = string.Empty;
                txtExpPer.Text = string.Empty;
                txtExpWeight.Text = string.Empty;
                txtBarcodeNo.Text = string.Empty;

                txtExpDollar.Text = "";
                txtPcs.Focus();
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjPacket.FindKapan();

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapan.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
                        GetXPktNo();
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

        private void txtSubLot_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SUBLOT";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjPacket.FindKapanSubLot(txtKapan.Text);

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtSubLot.Text = Val.ToString(FrmSearch.mDRow["SUBLOT"]);
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

        private void txtSubLot1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SUBLOT1";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjPacket.FindKapanSubLot1(txtKapan.Text, txtSubLot.Text);

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtSubLot1.Text = Val.ToString(FrmSearch.mDRow["SUBLOT1"]);
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORCODE,COLORNAME";
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYCODE,CLARITYNAME";
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORSHADECODE,COLORSHADENAME";
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "FLCODE,FLNAME";
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LBLCCODE,LBLCNAME";
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "NATTSCODE,NATTSNAME";
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "MILKYCODE,MILKYNAME";
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
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "TENSIONCODE,TENSIONNAME";
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
        #endregion

        private void ChkPacketEdit_CheckedChanged(object sender, EventArgs e)
        {
            txtPacketNo.ReadOnly = !ChkPacketEdit.Checked;
            lblMode.Text = "Edit Mode";
        }

        private void txtPacketNo_Validated(object sender, EventArgs e)
        {
            if (ChkPacketEdit.Checked == true)
            {
                lblMode.Text = "Add Mode";
                //if (lblMode.Text == "Add Mode")
                //{
                //    ChkBarcodeUpdate.Enabled = true;
                //}
                //txtBarcodeNo.Text = Val.ToString(ObjPacket.FindMaxBarcodeNo());

                txtBarcodeNo.Text = string.Empty;

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

                DataRow DRow = ObjPacket.GetSinglePacketDetail(txtKapan.Text, txtSubLot.Text, txtSubLot1.Text, Val.ToInt(txtPacketNo.Text));
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

                    txtBarcodeNo.Text = Val.ToString(DRow["BARCODENO"]);
                    txtCarat.Focus();
                    BtnSave.Enabled = false;
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
            txtKapan.Tag = string.Empty;

            txtSubLot.Text = "NONE";
            txtSubLot.Text = "NONE";

            txtSubLot1.Text = "NONE";
            txtSubLot1.Text = "NONE";

            txtPacketNo.Text = string.Empty;
            txtPacketNo.Tag = string.Empty;

            txtBalanceCarat.Text = "0.00";

            ChkPacketEdit.Checked = false;

            txtMarker.Tag = string.Empty;
            txtMarker.Text = string.Empty;

            txtMainPktNo.Text = string.Empty;
            txtMainPktNo.Tag = string.Empty;

            txtKapan.Focus();

            txtBarcodeNo.Text = string.Empty;
            txtExpWeight.Text = string.Empty;
            txtExpPer.Text = string.Empty;

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

        private void txtMarker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO);
                    FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TrnSinglePacketCreationProperty Property = new TrnSinglePacketCreationProperty();

                        txtMarker.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
                        txtMarker.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);

                        pIntToManager_ID = Val.ToInt64(FrmSearch.mDRow["MANAGER_ID"]);
                        pIntToDepartment_ID = Val.ToInt32(FrmSearch.mDRow["DEPARTMENT_ID"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void cTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void txtCarat_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtExpWeight_Validated(object sender, EventArgs e)
        {
            try
            {
                double pDouCarat = Val.Val(txtCarat.Text);
                double pDouExpCarat = Val.Val(txtExpWeight.Text);
                double pDouExpPer = Math.Round(((pDouExpCarat / pDouCarat)) * 100, 3);
                txtExpPer.Text = Val.ToString(pDouExpPer);
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void txtCarat_Validated(object sender, EventArgs e)
        {
            try
            {

                double pDouCarat = Val.Val(txtCarat.Text);
                double pDouExpPer = Val.ToInt32(txtExpPer.Text);
                double pDouExpWeight = Math.Round(((pDouCarat * pDouExpPer) / 100), 3);
                txtExpWeight.Text = Val.ToString(pDouExpWeight);

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void txtExpPer_Validated(object sender, EventArgs e)
        {
            try
            {
                double pDouCarat = Val.Val(txtCarat.Text);
                double pDouExpPer = Val.Val(txtExpPer.Text);
                double pDouExpWeight = Math.Round(((pDouCarat * pDouExpPer) / 100), 3);
                txtExpWeight.Text = Val.ToString(pDouExpWeight);

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

    }
}
