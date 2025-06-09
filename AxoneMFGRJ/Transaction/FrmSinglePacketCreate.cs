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
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSinglePacketCreate : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();
        BOTRN_LabourProcessMakable ObjLabour = new BOTRN_LabourProcessMakable();

        string mStrKapanCategory = "ORIGINAL";
        BOFormPer ObjPer = new BOFormPer();
        BODevGridSelection ObjGridSelection;
        DataTable DtabGridData = new DataTable();

        string pStrRoughType = "";

        EmployeeActionRightsProperty EmployeeRightsProperty = new EmployeeActionRightsProperty();

        #region Property Settings

        public FrmSinglePacketCreate()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName, string pStrManager, Int64 pIntManagerID, Int64 pIntKapan_ID, string StrKapanCategory)
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
            //#K : 08/11/2022
            DtabGridData.Columns.Add("KAPANNAME", typeof(string));
            DtabGridData.Columns.Add("PACKETNO", typeof(Int32));
            DtabGridData.Columns.Add("TAG", typeof(string));
            DtabGridData.Columns.Add("MANAGERNAME", typeof(string));
            DtabGridData.Columns.Add("CARAT", typeof(double));
            DtabGridData.Columns.Add("REMARK", typeof(string));
            DtabGridData.Columns.Add("PACKET_ID", typeof(Int64));
            DtabGridData.Columns.Add("KAPAN_ID", typeof(Int32));

            if (MainGrid.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDet;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            //#k : end
           
            BtnContinue_Click(null, null);
            btnUpdate.Visible = false;
            GrdDet.Columns["CARAT"].OptionsColumn.AllowEdit = false;
            //#k : end
            this.Show();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            DTPEntryDate.Text = DateTime.Now.ToShortDateString();

            txtKapan.Focus();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            txtPassForUpdatePacket.Tag = Val.ToString(ObjPer.PASSWORD);

            //#K : 08/11/2022
            DtabGridData.Columns.Add("KAPANNAME", typeof(string));
            DtabGridData.Columns.Add("PACKETNO", typeof(Int32));
            DtabGridData.Columns.Add("TAG", typeof(string));
            DtabGridData.Columns.Add("MANAGERNAME", typeof(string));
            DtabGridData.Columns.Add("CARAT", typeof(double));
            DtabGridData.Columns.Add("REMARK", typeof(string));
            DtabGridData.Columns.Add("PACKET_ID", typeof(Int64));
            DtabGridData.Columns.Add("KAPAN_ID", typeof(Int32));

            if (MainGrid.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDet;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            btnUpdate.Visible = false;
            GrdDet.Columns["CARAT"].OptionsColumn.AllowEdit = false;
            //#k : end
            this.Show();
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
            GetRoughBalance();
            PanelKapan.Enabled = false;
            GenerateMaxPacketNoKapanWise();
            txtPcs.Focus();
            if (mStrKapanCategory == "REPAIRING" || mStrKapanCategory == "PCN")
                PnlPrevious.Visible = true;
            else
                PnlPrevious.Visible = false;

        }
        public void GenerateMaxPacketNoKapanWise()
        {
            //if (txtKapan.Text == "REP")
            if (mStrKapanCategory == "REPAIRING" || mStrKapanCategory == "PCN")
                txtPacketNo.Text = ObjPacket.FindNewPacketNo(txtKapan.Text).ToString();
            else
            {

                TrnSinglePacketCreationProperty Property = new TrnSinglePacketCreationProperty();
                Property.KAPAN_ID = Val.ToInt64(txtKapan.Tag);
                Property = ObjPacket.FindNewPacketNoWithKapan(Property);

                if (Property.ReturnMessageType == "FAIL")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    txtPacketNo.Text = "0";
                    return;
                }
                txtPacketNo.Text = Val.ToString(Property.ReturnValueMaxPacketNo);
                Property = null;
            }
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

            if (txtTag.Text.Trim() == string.Empty)
            {
                Global.Message("Tag Required");
                return false;
            }

            if (txtPacketCategory.Text.Trim() == string.Empty)
            {
                Global.Message("Packet Category Required");
                txtPacketCategory.Focus();
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


            if (mStrKapanCategory == "REPAIRING" && Val.ToString(txtRemark.Text).Trim().Equals(string.Empty))
            {
                if (Val.ToString(txtPrevKapan.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Previous Kapan Is Required");
                    txtPrevKapan.Focus();
                    return false;
                }
                if (Val.ToString(txtPrevPacketNo.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Previous Packet No Is Required");
                    txtPrevPacketNo.Focus();
                    return false;
                }
                if (Val.ToString(txtPrevPacketTag.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Previous Packet Tag Is Required");
                    txtPrevPacketTag.Focus();
                    return false;
                }
            }
            else if (mStrKapanCategory == "REPAIRING" && Val.ToString(txtPrevKapan.Text).Trim().Equals(string.Empty))
            {
                if (Val.ToString(txtRemark.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Remark Is Required");
                    txtRemark.Focus();
                    return false;
                }
            }
            if (txtMainManager.Text.Trim() == string.Empty) //#P : 13-05-2022
            {
                Global.Message("Main Manager IS Required");
                txtMainManager.Focus();
                return false;
            }

            //if (Val.ToString(txtKapan.Text).Trim().Equals("REP"))
            //{
            //    if (Val.ToString(txtPrevKapan.Text).Trim().Equals(string.Empty))
            //    {
            //        Global.Message("Previous Kapan Is Required");
            //        txtPrevKapan.Focus();
            //        return false;
            //    }
            //    if (Val.ToString(txtPrevPacketNo.Text).Trim().Equals(string.Empty))
            //    {
            //        Global.Message("Previous Packet No Is Required");
            //        txtPrevPacketNo.Focus();
            //        return false;
            //    }
            //    if (Val.ToString(txtPrevPacketTag.Text).Trim().Equals(string.Empty))
            //    {
            //        Global.Message("Previous Packet Tag Is Required");
            //        txtPrevPacketTag.Focus();
            //        return false;
            //    }
            //}

            if (mStrKapanCategory == "PCN")
            {
                if (Val.ToString(txtPrevKapan.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Previous Kapan Is Required");
                    txtPrevKapan.Focus();
                    return false;
                }
                if (Val.ToString(txtPrevPacketNo.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Previous Packet No Is Required");
                    txtPrevPacketNo.Focus();
                    return false;
                }
                if (Val.ToString(txtPrevPacketTag.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Previous Packet Tag Is Required");
                    txtPrevPacketTag.Focus();
                    return false;
                }
            }


            //if (Math.Round(Val.Val(txtCarat.Text), 3) > Math.Round(Val.Val(txtBalanceCarat.Text), 3))
            //{
            //    Global.Message("Your Issue Carat Is Greater Than Balance Carat");
            //    txtCarat.Focus();
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

                while (lblMode.Text == "Add Mode" && ObjPacket.CheckPacketNoExists(txtKapan.Text, Val.ToInt32(txtPacketNo.Text), txtTag.Text))
                {
                    if (Global.Confirm("This Packet No. Is Already Created \n\n Are You Want To Create New One ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        //txtPacketNo.Text = ObjPacket.FindNewPacketNo(txtKapan.Text).ToString();
                        GenerateMaxPacketNoKapanWise();
                    }
                    else
                    {
                        return;
                    }
                }

                //#P : 13-10-2020 : PCN ma ek Var Pkt Ref tarike je pkt lidhu hoy te same pkt biji var as a Reference tarike Use na thay tenu validation...
                if (mStrKapanCategory == "PCN")
                {
                    TrnSinglePacketCreationProperty PropertyPrev = new TrnSinglePacketCreationProperty();
                    PropertyPrev.KAPANNAME = Val.ToString(txtKapan.Text);
                    PropertyPrev.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                    PropertyPrev.TAG = Val.ToString(txtTag.Text);

                    PropertyPrev.PREVKAPANNAME = Val.ToString(txtPrevKapan.Text);
                    PropertyPrev.PREVPACKETNO = Val.ToInt32(txtPrevPacketNo.Text);
                    PropertyPrev.PREVPACKETTAG = Val.ToString(txtPrevPacketTag.Text);
                    PropertyPrev = ObjPacket.CheckValidationForPrevPacketDetail(PropertyPrev);

                    if (PropertyPrev.ReturnMessageType == "FAIL")
                    {
                        Global.MessageError(PropertyPrev.ReturnMessageDesc);
                        return;
                    }
                }
                //End : #P : 13-10-2020


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
                //Property.MAINMANAGER_ID = Val.ToInt64(txtManager.Tag); //Change : #P : 13-05-2022
                Property.MAINMANAGER_ID = Val.ToInt64(txtMainManager.Tag);
                
                Property.KAPANCATEGORY = mStrKapanCategory;

                Property.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                Property.TAG = txtTag.Text;

                Property.PKTSERIALNO = Val.ToInt(txtPktSerialNo.Text);

                Property.PACKETTYPE = "ORIGINAL";

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

                // For Repairing kapan : 30-04-2019
                Property.PREVKAPANNAME = Val.ToString(txtPrevKapan.Text);
                Property.PREVPACKETNO = Val.ToInt32(txtPrevPacketNo.Text);
                Property.PREVPACKETTAG = Val.ToString(txtPrevPacketTag.Text);
                Property.PREVPACKET_ID = Val.ToInt64(txtPrevPacketNo.Text.Trim()) == 0 ? 0 : Val.ToInt64(Val.ToString(txtPrevPacketNo.Tag));
                Property.PREVPACKETCARAT = Val.Val(txtPrevPacketCarat.Text);
                //End : 30-04-2019

                Property.MARKER_ID = Val.ToInt64(txtMarker.Tag);
                Property.MARKERCODE = Val.ToString(txtMarker.Text);

                Property.PACKETGROUP_ID = Val.ToInt32(txtGroup.Tag);
                Property.PACKETGRADE_ID = Val.ToInt32(TxtGrade.Tag); //krina
                Property.PACKETCATEGORY_ID = Val.ToInt32(txtPacketCategory.Tag);
                Property.PLANNINGGRADE_ID = Val.ToInt32(txtPlannigGrade.Tag); // Dhara : 16-12-2024 : As per Discussion with Sohangbhai
                Property = ObjPacket.CreatePacket(Property, lblMode.Text);

                lblMessage.Text = Property.ReturnMessageDesc;
                txtPacketNo.Tag = Property.ReturnValue;

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    if (ChkPrintBarcode.Checked == true)
                    {
                        if (mStrKapanCategory == "PCN")
                        {
                            Global.BarcodePrintForPCN(txtKapan.Text, txtPacketNo.Text, txtTag.Text, DTPEntryDate.Value.ToString("dd-MM-yyyy"), txtCarat.Text, txtMarker.Text, Val.ToString(txtPrevKapan.Text), txtPrevPacketNo.Text, txtPrevPacketTag.Text);
                        }
                        else
                        {
                            Global.BarcodePrint(txtKapan.Text, txtPacketNo.Text, txtTag.Text, DTPEntryDate.Value.ToString("dd-MM-yyyy"), txtCarat.Text, BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE);
                        }
                    }

                    lblJangedNo.Text = Val.ToString(Property.ReturnValueJangedNo);
                    //#K : 08/11/2022
                    DataRow Ds = DtabGridData.NewRow();
                    Ds["KAPANNAME"] = Val.ToString(txtKapan.Text);
                    Ds["PACKETNO"] = Val.ToInt32(txtPacketNo.Text);
                    Ds["TAG"] = Val.ToString(txtTag.Text);
                    Ds["MANAGERNAME"] = Val.ToString(txtManager.Text);
                    Ds["CARAT"] = Val.Val(txtCarat.Text);
                    Ds["REMARK"] = Val.ToString(txtRemark.Text);
                    Ds["PACKET_ID"] = Val.ToInt64(txtPacketNo.Tag);
                    Ds["KAPAN_ID"] = Val.ToInt32(txtKapan.Tag);

                    DtabGridData.Rows.Add(Ds);

                    MainGrid.DataSource = DtabGridData;
                    MainGrid.Refresh();
                    GrdDet.BestFitColumns();
                    //#k : end

                    txtPacketNo.Text = string.Empty;
                    txtPacketNo.Tag = string.Empty;

                    GetRoughBalance();                    

                    if (mStrKapanCategory == "REPAIRING" || mStrKapanCategory == "PCN")
                        txtPacketNo.Text = ObjPacket.FindNewPacketNo(txtKapan.Text).ToString();
                    else
                    {
                        //txtPacketNo.Text = ObjPacket.FindNewPacketNo(txtKapan.Text).ToString();
                        TrnSinglePacketCreationProperty MaxPacketProperty = new TrnSinglePacketCreationProperty();
                        MaxPacketProperty.KAPAN_ID = Val.ToInt64(txtKapan.Tag);
                        MaxPacketProperty = ObjPacket.FindNewPacketNoWithKapan(MaxPacketProperty);
                        if (MaxPacketProperty.ReturnMessageType == "FAIL")
                        {
                            txtPacketNo.Text = "0";
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        txtPacketNo.Text = Val.ToString(MaxPacketProperty.ReturnValueMaxPacketNo);
                        MaxPacketProperty = null;
                    }
                    
                    ChkPacketEdit.Checked = false;

                    lblMode.Text = "Add Mode";
                    txtPcs.Text = "1";
                    txtCarat.Text = string.Empty;
                    txtRemark.Text = string.Empty;
                    txtExpDollar.Text = string.Empty;
                    txtPktSerialNo.Text = string.Empty;
                    txtExpDollar.Text = "";
                    txtCarat.Focus();                   
                    this.Cursor = Cursors.Default;
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
                    FrmSearch.mSearchField = "KAPANNAME";
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

                        if (txtKapan.Text.Trim().Equals("REP"))
                            PnlPrevious.Visible = true;
                        else
                            PnlPrevious.Visible = false;

                        TrnSinglePacketCreationProperty Property = new TrnSinglePacketCreationProperty();
                        Property.KAPAN_ID = Val.ToInt64(txtKapan.Tag);
                        Property = ObjPacket.FindRoughType(Property);
                        pStrRoughType = Val.ToString(Property.RETURNROUGHTYPE);
                        //Property = null;
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
                if (Global.OnKeyPressToOpenPopup(e))
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
                if (Global.OnKeyPressToOpenPopup(e))
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
                if (Global.OnKeyPressToOpenPopup(e))
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
                if (Global.OnKeyPressToOpenPopup(e))
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
                if (Global.OnKeyPressToOpenPopup(e))
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
                if (Global.OnKeyPressToOpenPopup(e))
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
                if (Global.OnKeyPressToOpenPopup(e))
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
        private void txtGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETGROUPCODE,PACKETGROUPNAME";
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

                txtPrevKapan.Text = string.Empty;
                txtPrevPacketNo.Text = string.Empty;
                txtPrevPacketTag.Text = string.Empty;
                txtPrevPacketTag.Text = string.Empty;
                txtPrevPacketCarat.Text = string.Empty;

                txtPktSerialNo.Text = string.Empty;

                txtMarker.Tag = string.Empty;
                txtMarker.Text = string.Empty;

                TxtGrade.Text = string.Empty;
                TxtGrade.Tag = string.Empty;

                txtGroup.Text = string.Empty;
                txtGroup.Tag = string.Empty;

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
                    txtPrevKapan.Text = Val.ToString(DRow["PREVKAPANNAME"]);
                    txtPrevPacketNo.Tag = Val.ToString(DRow["PREVPACKET_ID"]);
                    txtPrevPacketNo.Text = Val.ToString(DRow["PREVPACKETNO"]);
                    txtPrevPacketTag.Text = Val.ToString(DRow["PREVPACKETTAG"]);
                    txtPrevPacketCarat.Text = Val.ToString(DRow["PREVPACKETCARAT"]);

                    txtPktSerialNo.Text = Val.ToString(DRow["PKTSERIALNO"]);

                    txtMainManager.Text = Val.ToString(DRow["MAINMANAGERNAME"]);
                    txtMainManager.Tag = Val.ToString(DRow["MAINMANAGER_ID"]);

                    TxtGrade.Text = Val.ToString(DRow["PACKETGRADENAME"]);
                    TxtGrade.Tag = Val.ToString(DRow["PACKETGRADE_ID"]);

                    txtGroup.Text = Val.ToString(DRow["PACKETGROUPNAME"]);
                    txtGroup.Tag = Val.ToString(DRow["PACKETGROUP_ID"]);
                    txtCarat.Focus();
                    if (Val.ToString(txtPassForUpdatePacket.Tag) != "" && Val.ToString(txtPassForUpdatePacket.Tag).ToUpper() == txtPassForUpdatePacket.Text.ToUpper() && lblMode.Text.ToUpper() == "EDIT MODE")
                    {
                        BtnSave.Enabled = true;
                    }
                    else
                    {
                        BtnSave.Enabled = false;
                    }

                    MainGrid.Visible = false; //In Edit Mode Hide Pkt Detail
                }
                else
                {
                    BtnSave.Enabled = true; // Add : #P : 19-02-2020
                    MainGrid.Visible = true; //In Add Mode Visible Pkt Detail
                }

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
            txtManager.Text = string.Empty;
            txtManager.Tag = string.Empty;

            txtMainManager.Text = string.Empty;
            txtMainManager.Tag = string.Empty;

            txtPacketNo.Text = string.Empty;
            txtPacketNo.Tag = string.Empty;

            PnlPrevious.Visible = false;

            txtBalanceCarat.Text = "0.00";

            ChkPacketEdit.Checked = false;

            txtPrevKapan.Text = string.Empty;
            txtPrevKapan.Tag = string.Empty;
            txtPrevPacketNo.Text = string.Empty;
            txtPrevPacketNo.Tag = string.Empty;
            txtPrevPacketTag.Text = string.Empty;
            txtPrevPacketCarat.Text = string.Empty;

            txtMarker.Tag = string.Empty;
            txtMarker.Text = string.Empty;

            txtGroup.Text = string.Empty;
            txtGroup.Tag = string.Empty;
            TxtGrade.Text = string.Empty;

            txtKapan.Focus();
            txtPktSerialNo.Text = string.Empty;
            txtPassForUpdatePacket.Text = string.Empty;
            ObjGridSelection.ClearSelection();

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

        private void txtPrevPacketTag_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                
                DataTable DtabRefPacketDetail = new DataTable();
                if (mStrKapanCategory == "PCN")
                    DtabRefPacketDetail = new BOFindRap().GetRejectionPCNPacketList(txtPrevKapan.Text, Val.ToInt32(txtPrevPacketNo.Text),Val.ToString(txtPrevPacketTag.Text));
                else
                    DtabRefPacketDetail = new BOFindRap().GetPacketList(txtPrevKapan.Text, Val.ToInt32(txtPrevPacketNo.Text),Val.ToString(txtPrevPacketTag.Text));

                if (DtabRefPacketDetail.Rows.Count <= 0)
                {
                    Global.MessageError("Packet Not Exist.");
                    txtPrevKapan.Text = string.Empty;
                    txtPrevKapan.Tag = string.Empty;
                    txtPrevPacketNo.Text = string.Empty;
                    txtPrevPacketNo.Tag = string.Empty;
                    txtPrevPacketTag.Text = string.Empty;
                    txtPrevKapan.Focus();
                    txtPrevPacketCarat.Text = string.Empty;
                    if (mStrKapanCategory == "PCN")
                    {
                        txtMarker.Tag = string.Empty;
                        txtMarker.Text = string.Empty;
                    }
                    return;
                }

                txtPrevPacketTag.Text = Val.ToString(DtabRefPacketDetail.Rows[0]["TAG"]);
                txtPrevPacketNo.Tag = Val.ToString(DtabRefPacketDetail.Rows[0]["PACKET_ID"]);
                txtPrevKapan.Tag = Val.ToString(DtabRefPacketDetail.Rows[0]["KAPAN_ID"]);
                txtPrevPacketCarat.Text = Val.ToString(DtabRefPacketDetail.Rows[0]["BALANCECARAT"]);
                if (mStrKapanCategory == "PCN")
                {
                    txtMarker.Tag = Val.ToString(DtabRefPacketDetail.Rows[0]["EMPLOYEE_ID"]);
                    txtMarker.Text = Val.ToString(DtabRefPacketDetail.Rows[0]["EMPLOYEECODE"]);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
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

            // ADD : #K : 08/11/2022
            if(Val.ToString(txtPassForUpdatePacket.Tag) != "" && Val.ToString(txtPassForUpdatePacket.Tag).ToUpper() == txtPassForUpdatePacket.Text.ToUpper() && lblMode.Text.ToUpper() == "ADD MODE")
            { 
                btnUpdate.Visible = true;
                GrdDet.Columns["CARAT"].OptionsColumn.AllowEdit = true;
            }
            else
            {
                btnUpdate.Visible = false;
                GrdDet.Columns["CARAT"].OptionsColumn.AllowEdit = false;
            }
            //#K
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

        private void txtMainManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BOComboFill.TABLE.MST_MAINMANAGER);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtMainManager.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtMainManager.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
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

        private void TxtGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (TxtGrade.Enabled == false)
                {
                    return;
                }

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETGRADECODE,PACKETGRADENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PACKETGRADE);
                    FrmSearch.mColumnsToHide = "PACKETGRADE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtGrade.Text = Val.ToString(FrmSearch.mDRow["PACKETGRADENAME"]);
                        TxtGrade.Tag = Val.ToString(FrmSearch.mDRow["PACKETGRADE_ID"]);
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

        private void btnUpdate_Click(object sender, EventArgs e)//ADD : #K : 08/11/2022
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";
                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);
                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    this.Cursor = Cursors.Default;
                    return;                   
                }
                if (Global.Confirm("Are You Sure to Want to Update ?") == System.Windows.Forms.DialogResult.No)
                {
                    this.Cursor = Cursors.Default;
                    return;                    
                }

                foreach (DataRow dr in DTab.Rows)
                {
                    TrnSinglePacketCreationProperty Property = new TrnSinglePacketCreationProperty();

                    Property.PACKET_ID = Val.ToInt64(dr["PACKET_ID"]);
                    Property.KAPAN_ID = Val.ToInt32(dr["KAPAN_ID"]);
                    Property.LOTCARAT = Val.ToDouble(dr["CARAT"]);
                    Property.KAPANNAME = Val.ToString(dr["KAPANNAME"]);
                    Property.PACKETNO = Val.ToInt32(dr["PACKETNO"]);
                    Property.TAG = Val.ToString(dr["TAG"]);
                    Property = ObjPacket.PacketCaratUpdate(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;
                    if(ReturnMessageType == "FAIL")
                    {
                        Global.MessageError(ReturnMessageDesc);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    Property = null;
                }
                DTab.AcceptChanges();

                Global.Message(ReturnMessageDesc);
                
                this.Cursor = Cursors.Default;
                if (ReturnMessageType == "SUCCESS")
                {

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
            }
            catch(Exception ex)
            {
                Global.Message(ex.Message);
                this.Cursor = Cursors.Default;
            }
        }

        private void txtPacketCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (TxtGrade.Enabled == false)
                {
                    return;
                }

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETCATEGORYCODE,PACKETCATEGORYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PACKETCATEGORY);
                    FrmSearch.mColumnsToHide = "PACKETGRADE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPacketCategory.Text = Val.ToString(FrmSearch.mDRow["PACKETCATEGORYNAME"]);
                        txtPacketCategory.Tag = Val.ToString(FrmSearch.mDRow["PACKETCATEGORY_ID"]);
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

        // Add by Dhara For Plannig Grade : 12-12-2024

        private void txtPlannigGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (TxtGrade.Enabled == false)
                {
                    return;
                }

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PLANNINGCODE,PLANNINGNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjLabour.GetPlanningGrade(pStrRoughType);
                    FrmSearch.mColumnsToHide = "PLANNINGGRADE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPlannigGrade.Text = Val.ToString(FrmSearch.mDRow["PLANNINGNAME"]);
                        txtPlannigGrade.Tag = Val.ToString(FrmSearch.mDRow["PLANNINGGRADE_ID"]);
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
    }
}
