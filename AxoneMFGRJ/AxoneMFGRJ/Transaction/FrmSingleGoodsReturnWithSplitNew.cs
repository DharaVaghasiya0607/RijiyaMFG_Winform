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
using Microsoft.VisualBasic;
using BusLib.Master;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleGoodsReturnWithSplitNew : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        DataTable DTabPrd = new DataTable();
        bool mBoolAutoConfirm = false;
        Int64 ValJangedNoTran = 0; //06-06-2022


        DataTable DTabNewPacket = new DataTable();
        DataRow DRowMainPacket = null;
        string StrBPrintType = "";

        #region Property Settings

        public FrmSingleGoodsReturnWithSplitNew()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName = "", string pStrPacketNo = "", string pStrTag = "")
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            RbtPacketNo.Checked = true;
            txtKapanName.Text = pStrKapanName;
            txtPacketNo.Text = pStrPacketNo;
            txtTag.Text = pStrTag;

            DTPTransferDate.Value = DateTime.Now;

            DTabNewPacket = new DataTable();
            DTabNewPacket.Columns.Add(new DataColumn("TAG", typeof(string)));
            DTabNewPacket.Columns.Add(new DataColumn("REASON_ID", typeof(Int32)));
            DTabNewPacket.Columns.Add(new DataColumn("REASONNAME", typeof(string)));
            DTabNewPacket.Columns.Add(new DataColumn("ISSUEPCS", typeof(Int32)));
            DTabNewPacket.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));
            DTabNewPacket.Columns.Add(new DataColumn("PACKETTYPE", typeof(string)));
            DTabNewPacket.Columns.Add(new DataColumn("REMARK", typeof(string)));
            DTabNewPacket.Columns.Add(new DataColumn("EXPWEIGHT", typeof(double)));
            DTabNewPacket.Columns.Add(new DataColumn("PRDCARAT", typeof(double)));
            DTabNewPacket.Columns.Add(new DataColumn("SHAPENAME", typeof(string)));
            DTabNewPacket.Columns.Add(new DataColumn("SHAPE_ID", typeof(Int32)));
            DTabNewPacket.Columns.Add(new DataColumn("PKTSERIALNO", typeof(string)));
            DTabNewPacket.Columns.Add(new DataColumn("BARCODE", typeof(string)));

            MainGrid.DataSource = DTabNewPacket;
            GrdDet.RefreshData();

            txtTag_Validated(null, null);

            this.Show();
            //txtTransferTo.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
            //txtTransferTo.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
            //mBoolAutoConfirm = BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM;

            txtDepartment.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME;
            txtDepartment.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;

            //#P : 19-10-2019
            //txtManager.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID;
            //txtManager.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE;
            ////End : #P : 19-10-2019

            txtType.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;
            txtType.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;

            EmployeeActionRightsProperty PropertyEmployeeActionRights = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            StrBPrintType = PropertyEmployeeActionRights.BPRINTTYPE;
            if (StrBPrintType == "TSC")
                RbtTSC.Checked = true;
            else if (StrBPrintType.ToUpper() == "CITIZEN")
                RbtCitizen.Checked = true;
            else if (StrBPrintType.ToUpper() == "TSCGALAXY")
                RbtTscGalaxy.Checked = true;
            else if (StrBPrintType == "")
            {
                RbtTSC.Checked = true;
                StrBPrintType = "TSC";
            }

            ChkJumpISSToTRN_CheckedChanged(null, null);
            txtProcessTo.Focus();
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

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }

        }

        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
        }

        private void txtTransferTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
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
                        txtTransferTo.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
                        txtTransferTo.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        txtType.Text = Val.ToString(FrmSearch.mDRow["LEDGERGROUP"]);
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                        mBoolAutoConfirm = Val.ToBoolean(FrmSearch.mDRow["AUTOCONFIRM"]);

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
                Global.MessageError(ex.Message);
            }
        }

        private void txtToProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcessTo.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcessTo.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        txtRequiredProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtRequiredProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);


                        foreach (DataRow DRow in DTabNewPacket.Rows)
                        {
                            DRow["TOPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                            DRow["TOPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);

                            DRow["NEXTPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                            DRow["NEXTPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        }

                        DTabNewPacket.AcceptChanges();
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

        private void txtRequiredProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRequiredProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtRequiredProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        foreach (DataRow DRow in DTabNewPacket.Rows)
                        {
                            DRow["NEXTPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                            DRow["NEXTPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        }

                        DTabNewPacket.AcceptChanges();
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

        public bool ValSave()
        {
            if (txtTransferTo.Text.Trim().Length == 0)
            {
                Global.MessageError("Transfer To Name Is Required");
                txtTransferTo.Focus();
                return false;
            }
            if (txtDepartment.Text.Trim().Length == 0)
            {
                Global.MessageError("Department Name Is Required");
                txtDepartment.Focus();
                return false;
            }
            if (txtType.Text.Trim().Length == 0)
            {
                Global.MessageError("Type Is Required");
                txtType.Focus();
                return false;
            }
            if (txtProcessTo.Text.Trim().Length == 0)
            {
                Global.MessageError("For Process Field Is Required");
                txtProcessTo.Focus();
                return false;
            }
            if (txtRequiredProcess.Text.Trim().Length == 0)
            {
                Global.MessageError("Required Process Field Is Required");
                txtRequiredProcess.Focus();
                return false;
            }

            if ((txtKapanName.Text.Trim().Length == 0) && (RbtPacketNo.Checked == true))
            {
                Global.MessageError("Kapan Name Is Required");
                txtKapanName.Focus();
                return false;
            }

            if (Val.Val(txtPacketNo.Text) == 0 && (RbtPacketNo.Checked == true))
            {
                Global.MessageError("Packet No Is Required");
                txtPacketNo.Focus();
                return false;
            }

            if (txtTag.Text.Trim().Length == 0 && (RbtPacketNo.Checked == true))
            {
                Global.MessageError("Tag Field Is Required");
                txtTag.Focus();
                return false;
            }

            if ((txtSrNoKapanName.Text.Trim().Length == 0) && (RbtPktSerialNo.Checked == true))
            {
                Global.MessageError("Kapan Name Is Required");
                txtSrNoKapanName.Focus();
                return false;
            }

            if ((txtSrNoSerialNo.Text.Trim().Length == 0) && (RbtPktSerialNo.Checked == true))
            {
                Global.MessageError("Srno Is Required");
                txtSrNoSerialNo.Focus();
                return false;
            }

            if (Val.Val(txtIssuePcs.Text) == 0)
            {
                Global.MessageError("Issue Pcs Is Required");
                txtIssuePcs.Focus();
                return false;
            }
            if (Val.Val(txtIssueCarat.Text) == 0)
            {
                Global.MessageError("Issue Carat Is Required");
                txtIssueCarat.Focus();
                return false;
            }

            if (Val.Val(txtReadyPcs.Text) == 0)
            {
                Global.MessageError("Ready Pcs Is Required");
                txtReadyPcs.Focus();
                return false;
            }
            if (Val.Val(txtReadyCarat.Text) == 0)
            {
                Global.MessageError("Ready Carat Is Required");
                txtReadyCarat.Focus();
                return false;
            }

            if (DRowMainPacket == null)
            {
                Global.MessageError("Packet Data IS NULL Pls Re Enter ");
                txtKapanName.Focus();
                return false;
            }

            if (Val.Val(txtReadyCarat.Text) < Val.Val(txtExpWeight.Text))
            {
                Global.Message("You Can't Add ExpWeight More Then Ready Weight...!");
                txtExpWeight.Focus();
                return false;
            }


            //#P : 03-10-2022
            if (Val.ToInt(txtSecondPcs.Text) > Val.Val(DTabNewPacket.Rows.Count))
            {
                Global.MessageError("Second Pcs And the List of Rows Must be Same..Pls Check And Delete Rows From The Second Detail Listing.");
                txtSecondPcs.Focus();
                return false;
            }
            if (Val.Val(txtDiffCarat.Text) != 0)
            {
                Global.MessageError("Oops.. Second And Extra Carat Total MisMatch With TotalCarat Please Check..");
                txtTotalCarat.Focus();
                return false;
            }
            if (ChkJumpISSToTRN.Checked && txtProcessToReturn.Text.Trim().Equals(string.Empty))
            {
                Global.MessageError("Return Process IS Required While Jump.");
                txtProcessToReturn.Focus();
                return false;
            }
            //End : #P : 03-10-2022

            //if (Val.Val(txtReadyCarat.Text) == 0)
            //{
            //    Global.Message("Ready Carat is Zero Please Check...!");
            //    txtReadyCarat.Focus();
            //    return false;
            //}


            double DouCarat = Val.Val(txtIssueCarat.Text);
            double DouReadyCarat = Val.Val(txtReadyCarat.Text);
            double DouLostCarat = Val.Val(txtLostCarat.Text);
            double DouSecondCarat = Val.Val(txtTotalSecondCarat.Text);
            double DouExtraCarat = Val.Val(txtTotalExtraCarat.Text);
            double DouRejectionCarat = Val.Val(txtRejectionCarat.Text);

            double DouLossCarat = Val.Val(txtLossCarat.Text);
            double DouMixingLessPlus = Val.Val(txtMixingLessPlus.Text);

            double DouDiff = Math.Round(Val.Val(DouCarat - DouReadyCarat - DouLostCarat - DouLossCarat - DouMixingLessPlus - DouSecondCarat - DouExtraCarat - DouRejectionCarat), 3);

            if (DouLossCarat < 0)
            {
                Global.MessageError("Loss Carat Is Less Then Zero At Row [" + DouDiff.ToString() + "] ");
                txtRequiredProcess.Focus();
                return false;
            }

            if (DouDiff != 0)
            {
                Global.MessageError("Carat Mismatched At Row [" + DouDiff.ToString() + "] ");
                txtRequiredProcess.Focus();
                return false;
            }


            return true;
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                //#P : 06-06-2022
                if (ChkJumpISSToTRN.Checked && Val.ToInt64(DRowMainPacket["FROMEMPLOYEE_ID"]) == Val.ToInt64(txtTransferTo.Tag))
                {
                    Global.MessageError("Jump Employee Is Wrong Please Check...");
                    return;
                }
                //End : 06-06-2022

                if (DRowMainPacket["ENTRYTYPE"].ToString() != "EMPISS")
                {
                    Global.MessageError("Your Selected Packet Status Is Not 'ISSUE' So You Can't Split This Packet Pls Check...");
                    return;
                }

                if (Global.Confirm("Are You Sure You Want To Split This Packet ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                Int64 JangedNo = 0;

                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                Property.TRN_ID = 0;

                Property.OLDTRN_ID = Val.ToInt64(DRowMainPacket["TRN_ID"].ToString());
                Property.KAPAN_ID = Val.ToInt64(DRowMainPacket["KAPAN_ID"].ToString());

                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                Property.TAG = Val.ToString(txtTag.Text);

                Property.PACKET_ID = Val.ToInt64(DRowMainPacket["PACKET_ID"].ToString());
                Property.JANGEDNO = JangedNo;
                Property.ENTRYSRNO = 1;
                //Property.ENTRYTYPE = RbtStaffReturn.Tag.ToString();
                Property.ENTRYTYPE = ChkJumpISSToTRN.Checked ? "TRANSFER" : RbtStaffReturn.Tag.ToString();

                Property.FROMDEPARTMENT_ID = Val.ToInt32(DRowMainPacket["TODEPARTMENT_ID"]);
                Property.TODEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);

                Property.FROMEMPLOYEE_ID = Val.ToInt64(DRowMainPacket["TOEMPLOYEE_ID"]);
                Property.TOEMPLOYEE_ID = Val.ToInt64(txtTransferTo.Tag);

                Property.FROMMANAGER_ID = Val.ToInt64(DRowMainPacket["TOMANAGER_ID"]);
                Property.TOMANAGER_ID = Val.ToInt64(txtManager.Tag);

                Property.FROMPROCESS_ID = Val.ToInt32(DRowMainPacket["TOPROCESS_ID"]);
                Property.TOPROCESS_ID = Val.ToInt32(txtProcessTo.Tag);
                Property.NEXTPROCESS_ID = Val.ToInt32(txtRequiredProcess.Tag);

                Property.JUMPRETURNPROCESS_ID = Val.ToInt32(txtProcessToReturn.Tag);

                Property.ISSUEPCS = Val.ToInt32(txtIssuePcs.Text);
                Property.ISSUECARAT = Val.Val(txtIssueCarat.Text);

                Property.RETURNTYPE = "DONE";

                Property.READYPCS = Val.ToInt32(txtReadyPcs.Text);
                Property.READYCARAT = Val.Val(txtReadyCarat.Text);

                Property.SECONDPCS = Val.ToInt32(txtTotalSecondPcs.Text);
                Property.SECONDCARAT = Val.Val(txtTotalSecondCarat.Text);

                Property.EXTRAPCS = Val.ToInt32(txtTotalExtraPcs.Text);
                Property.EXTRACARAT = Val.Val(txtTotalExtraCarat.Text);

                Property.REJECTIONPCS = Val.ToInt32(txtRejectionPcs.Text);
                Property.REJECTIONCARAT = Val.Val(txtRejectionCarat.Text);

                Property.RRPCS = 0;
                Property.RRCARAT = 0;

                Property.LOSTPCS = Val.ToInt32(txtLostPcs.Text);
                Property.LOSTCARAT = Val.Val(txtLostCarat.Text);
                Property.LOSSCARAT = Val.Val(txtLossCarat.Text);
                Property.MIXINGLESSPLUS = Val.Val(txtMixingLessPlus.Text);

                Property.TRANSDATE = DTPTransferDate.Value.ToString();
                Property.TRANSTYPE = RbtStaffReturn.Tag.ToString();
                Property.REMARK = "Packet Going To Be Split";
                Property.AUTOCONFIRM = mBoolAutoConfirm;
                Property.EXPWEIGHT = Val.Val(txtExpWeight.Text);

                Property.JUMPISSTOTRN = ChkJumpISSToTRN.Checked ? Val.ToString(ChkJumpISSToTRN.Tag) : ""; //#P : 06-06-2022

                Property.PRDSHAPE_ID = Val.ToInt32(txtPrdShp.Tag);
                Property.PRDCTS = Val.Val(txtPrdCts.Text);

                Property.ISENTRYFROMSPLITMODULE = true;

                Property = ObjTrn.TransferGoods(Property);
                if (Property.ReturnMessageType == "FAIL")
                {
                    this.Cursor = Cursors.Default;
                    Global.MessageError(Property.ReturnMessageDesc);
                }
                else
                {
                    //JangedNo = Property.JANGEDNO; #P : 06-06-2022
                    JangedNo = ChkJumpISSToTRN.Checked ? Property.JANGEDNORet : Property.JANGEDNO; //JANGEDNORet : Transfer no JangedNo aavse : 06-06-2022
                    ValJangedNoTran = Property.JANGEDNO;

                    TrnSinglePacketCreationProperty Cls = new TrnSinglePacketCreationProperty();

                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        DataRow DRow = GrdDet.GetDataRow(IntI);

                        if (Val.ToInt32(DRow["ISSUEPCS"]) == 0 || Val.Val(DRow["ISSUECARAT"]) == 0)
                        {
                            continue;
                        }

                        Cls = new TrnSinglePacketCreationProperty();
                        Cls.PACKET_ID = Val.ToInt64(Val.ToString(DRowMainPacket["PACKET_ID"]));
                        Cls.MAINPACKET_ID = Val.ToInt64(Val.ToString(DRowMainPacket["MAINPACKET_ID"]));
                        Cls.KAPAN_ID = Val.ToInt64(DRowMainPacket["KAPAN_ID"]);

                        Cls.KAPANNAME = Val.ToString(DRowMainPacket["KAPANNAME"]);
                        Cls.MAINMANAGER_ID = Val.ToInt64(DRowMainPacket["MAINMANAGER_ID"]);

                        Cls.PACKETGROUP_ID = Val.ToInt32(DRowMainPacket["PACKETGROUP_ID"]);
                        Cls.PACKETGRADE_ID = Val.ToInt32(DRowMainPacket["PACKETGRADE_ID"]);
                        Cls.PACKETCATEGORY_ID = Val.ToInt32(DRowMainPacket["PACKETCATEGORY_ID"]);

                        Cls.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                        Cls.TAG = txtTag.Text;
                        //Cls.JANGEDNO = Property.JANGEDNO;
                        Cls.JANGEDNO = JangedNo;
                        Cls.JANGEDNOTRAN = ValJangedNoTran;

                        Cls.LOTPCS = Val.ToInt32(DRow["ISSUEPCS"]);
                        Cls.LOTCARAT = Val.Val(DRow["ISSUECARAT"]);

                        //  Cls.REASON_ID = Val.ToInt32(DRow["REASON_ID"]);

                        Cls.ENTRYDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());

                        Cls.FROMEMPLOYEE_ID = Val.ToInt64(DRowMainPacket["TOEMPLOYEE_ID"]);
                        Cls.TOEMPLOYEE_ID = Val.ToInt64(txtTransferTo.Tag);

                        Cls.FROMMANAGER_ID = Val.ToInt64(DRowMainPacket["TOMANAGER_ID"]);
                        Cls.TOMANAGER_ID = Val.ToInt64(txtManager.Tag);

                        Cls.FROMDEPARTMENT_ID = Val.ToInt32(DRowMainPacket["TODEPARTMENT_ID"]);
                        Cls.TODEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);

                        Cls.FROMPROCESS_ID = Val.ToInt32(DRowMainPacket["TOPROCESS_ID"]);
                        Cls.TOPROCESS_ID = Val.ToInt32(txtProcessTo.Tag);
                        Cls.NEXTPROCESS_ID = Val.ToInt32(txtRequiredProcess.Tag);

                        Cls.JUMPRETURNPROCESS_ID = Val.ToInt32(txtProcessToReturn.Tag);

                        Cls.CLARITY_ID = Val.ToInt32(DRowMainPacket["CLARITY_ID"]);
                        Cls.COLOR_ID = Val.ToInt32(DRowMainPacket["COLOR_ID"]);
                        Cls.COLORSHADE_ID = Val.ToInt32(DRowMainPacket["COLORSHADE_ID"]);
                        Cls.FL_ID = Val.ToInt32(DRowMainPacket["FL_ID"]);

                        Cls.LBLC_ID = Val.ToInt32(DRowMainPacket["LBLC_ID"]);
                        Cls.NATTS_ID = Val.ToInt32(DRowMainPacket["NATTS_ID"]);
                        Cls.MILKY_ID = Val.ToInt32(DRowMainPacket["MILKY_ID"]);
                        Cls.TENSION_ID = Val.ToInt32(DRowMainPacket["TENSION_ID"]);

                        Cls.MARKER_ID = Val.ToInt64(DRowMainPacket["MARKER_ID"]);
                        Cls.MARKERCODE = Val.ToString(DRowMainPacket["MARKERCODE"]);

                        Cls.AMOUNT = Val.Val(DRowMainPacket["RATE"]) * Cls.LOTCARAT;

                        Cls.REMARK = Val.ToString(DRow["REMARK"]);
                        Cls.PACKETTYPE = Val.ToString(DRow["PACKETTYPE"]);

                        Cls.EXPWEIGHT = Val.Val(DRow["EXPWEIGHT"]);

                        Cls.PRDSHAPE_ID = Val.ToInt32(DRow["SHAPE_ID"]);
                        Cls.PRDCTS = Val.Val(DRow["PRDCARAT"]);

                        if (Val.ToString(DRow["TAG"]) == "")
                        {
                            Cls.ISNEWTAG = true;
                        }
                        else
                        {
                            Cls.ISNEWTAG = false;
                        }
                        Cls.PRDTAG = Val.ToString(DRow["TAG"]);

                        //Cls.SPLITTRN_ID = Val.ToInt64(Property.ReturnValue);
                        Cls.SPLITTRN_ID = ChkJumpISSToTRN.Checked ? Val.ToInt64(Property.ReturnValueEmpRet_TRNID) : Val.ToInt64(Property.ReturnValue); //While Jump

                        Cls.AUTOCONFIRM = mBoolAutoConfirm;
                        Cls.JUMPISSTOTRN = ChkJumpISSToTRN.Checked ? Val.ToString(ChkJumpISSToTRN.Tag) : ""; //#P : 06-06-2022

                        Cls.RATE = Val.Val(DRowMainPacket["RATE"]); //hinal : 21-06-2022
                        Cls.ISREJECTION = Val.ToBoolean(chkRejectionTransfer.Checked);

                        Cls.PACKETGRADE_ID = Val.ToInt(txtPacketGrade.Tag);
                        Cls.PACKETGROUP_ID = Val.ToInt(txtPacketGroup.Tag);

                        Cls = ObjTrn.SplitPacketSave(Cls);

                        if (Cls.ReturnMessageType == "SUCCESS")
                        {
                            if (Val.ToString(DRow["TAG"]) == "")
                            {
                                GrdDet.SetRowCellValue(IntI, "TAG", Cls.ReturnValueTag);
                                GrdDet.SetRowCellValue(IntI, "PKTSERIALNO", Cls.ReturnValuePktSerialNo);
                                GrdDet.SetRowCellValue(IntI, "BARCODE", Cls.ReturnValueBarcode);
                            }

                            //if (ChkPrintBarcode.Checked == true)
                            //{
                            //    Global.BarcodePrint(Cls.KAPANNAME, Cls.PACKETNO.ToString(), Cls.ReturnValueTag, DTPTransferDate.Value.ToString("dd-MM-yy"), Cls.LOTCARAT.ToString(), Cls.MARKERCODE);
                            //}
                        }
                        else
                        {
                            GrdDet.SetRowCellValue(IntI, "ISSUEPCS", 0);
                        }
                        Cls = null;
                    }

                    //#P : 03-10-2022 : Extra Packets Save Functionality

                    if (Val.Val(txtExtraCarat.Text) > 0)
                    {
                        Cls = new TrnSinglePacketCreationProperty();
                        Cls.PACKET_ID = Val.ToInt64(Val.ToString(DRowMainPacket["PACKET_ID"]));
                        Cls.MAINPACKET_ID = Val.ToInt64(Val.ToString(DRowMainPacket["MAINPACKET_ID"]));
                        Cls.KAPAN_ID = Val.ToInt64(DRowMainPacket["KAPAN_ID"]);

                        Cls.KAPANNAME = Val.ToString(DRowMainPacket["KAPANNAME"]);
                        Cls.MAINMANAGER_ID = Val.ToInt64(DRowMainPacket["MAINMANAGER_ID"]);

                        Cls.PACKETGROUP_ID = Val.ToInt32(DRowMainPacket["PACKETGROUP_ID"]);
                        Cls.PACKETGRADE_ID = Val.ToInt32(DRowMainPacket["PACKETGRADE_ID"]);
                        Cls.PACKETCATEGORY_ID = Val.ToInt32(DRowMainPacket["PACKETCATEGORY_ID"]);

                        Cls.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                        Cls.TAG = txtTag.Text;
                        //Cls.JANGEDNO = Property.JANGEDNO;
                        Cls.JANGEDNO = JangedNo;
                        Cls.JANGEDNOTRAN = ValJangedNoTran;

                        Cls.LOTPCS = 1;
                        Cls.LOTCARAT = Val.Val(txtExtraCarat.Text);

                        //  Cls.REASON_ID = Val.ToInt32(DRow["REASON_ID"]);

                        Cls.ENTRYDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());

                        Cls.FROMEMPLOYEE_ID = Val.ToInt64(DRowMainPacket["TOEMPLOYEE_ID"]);
                        Cls.TOEMPLOYEE_ID = Val.ToInt64(txtTransferTo.Tag);

                        Cls.FROMMANAGER_ID = Val.ToInt64(DRowMainPacket["TOMANAGER_ID"]);
                        Cls.TOMANAGER_ID = Val.ToInt64(txtManager.Tag);

                        Cls.FROMDEPARTMENT_ID = Val.ToInt32(DRowMainPacket["TODEPARTMENT_ID"]);
                        Cls.TODEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);

                        Cls.FROMPROCESS_ID = Val.ToInt32(DRowMainPacket["TOPROCESS_ID"]);
                        Cls.TOPROCESS_ID = Val.ToInt32(txtProcessTo.Tag);
                        Cls.NEXTPROCESS_ID = Val.ToInt32(txtRequiredProcess.Tag);

                        Cls.JUMPRETURNPROCESS_ID = Val.ToInt32(txtProcessToReturn.Tag);

                        Cls.CLARITY_ID = Val.ToInt32(DRowMainPacket["CLARITY_ID"]);
                        Cls.COLOR_ID = Val.ToInt32(DRowMainPacket["COLOR_ID"]);
                        Cls.COLORSHADE_ID = Val.ToInt32(DRowMainPacket["COLORSHADE_ID"]);
                        Cls.FL_ID = Val.ToInt32(DRowMainPacket["FL_ID"]);

                        Cls.LBLC_ID = Val.ToInt32(DRowMainPacket["LBLC_ID"]);
                        Cls.NATTS_ID = Val.ToInt32(DRowMainPacket["NATTS_ID"]);
                        Cls.MILKY_ID = Val.ToInt32(DRowMainPacket["MILKY_ID"]);
                        Cls.TENSION_ID = Val.ToInt32(DRowMainPacket["TENSION_ID"]);

                        Cls.MARKER_ID = Val.ToInt64(DRowMainPacket["MARKER_ID"]);
                        Cls.MARKERCODE = Val.ToString(DRowMainPacket["MARKERCODE"]);

                        Cls.AMOUNT = Val.Val(DRowMainPacket["RATE"]) * Cls.LOTCARAT;

                        Cls.REMARK = "";
                        Cls.PACKETTYPE = "EXTRA";

                        Cls.EXPWEIGHT = 0.00;

                        Cls.PRDSHAPE_ID = 0;
                        Cls.PRDCTS = 0;

                        Cls.ISNEWTAG = true;
                        Cls.PRDTAG = "";

                        //Cls.SPLITTRN_ID = Val.ToInt64(Property.ReturnValue);
                        Cls.SPLITTRN_ID = ChkJumpISSToTRN.Checked ? Val.ToInt64(Property.ReturnValueEmpRet_TRNID) : Val.ToInt64(Property.ReturnValue); //While Jump

                        Cls.AUTOCONFIRM = mBoolAutoConfirm;
                        Cls.JUMPISSTOTRN = ChkJumpISSToTRN.Checked ? Val.ToString(ChkJumpISSToTRN.Tag) : ""; //#P : 06-06-2022

                        Cls.RATE = Val.Val(DRowMainPacket["RATE"]); //hinal : 21-06-2022
                        Cls.ISREJECTION = Val.ToBoolean(chkRejectionTransfer.Checked);

                        Cls.PACKETGRADE_ID = Val.ToInt(txtPacketGrade.Tag);
                        Cls.PACKETGROUP_ID = Val.ToInt(txtPacketGroup.Tag);

                        Cls = ObjTrn.SplitPacketSave(Cls);
                        if (Cls.ReturnMessageType != "SUCCESS")
                        {
                            Global.MessageError(Cls.ReturnMessageDesc + " [On Extra Packets Save Functionality]");
                            return;
                        }
                    }
                    //End : #P : 03-10-2022 : Extra Packets Save Functionality

                    #region Barcode Print Code If Checked Print Barcode

                    if (ChkPrintBarcode.Checked == true)
                    {
                        #region Comment code
                        /* : Cmnt : Temporary
                    string fileLoc = Application.StartupPath + "\\PrintBarcodeData.txt";
                    if (System.IO.File.Exists(fileLoc) == true)
                    {
                        System.IO.File.Delete(fileLoc);
                    }

                    System.IO.File.Create(fileLoc).Dispose();

                    string StrKapanName = "";
                    int pIntPktNo = 0;
                    string pStrTag = "";
                    string Date = "";
                    string Carat = "";
                    string MarkerCode = "";

                    using (var sw = new StreamWriter(fileLoc, true))
                    {
                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRow = GrdDet.GetDataRow(IntI);

                            if (Val.ToInt32(DRow["ISSUEPCS"]) == 0 || Val.Val(DRow["ISSUECARAT"]) == 0)
                            {
                                continue;
                            }

                            StrKapanName = Val.ToString(txtKapanName.Text);
                            pIntPktNo = Val.ToInt32(txtPacketNo.Text);
                            pStrTag = Val.ToString(DRow["TAG"]);
                            Date = DTPTransferDate.Value.ToString("dd-MM-yy");
                            Carat = Val.ToString(DRow["ISSUECARAT"]);
                            MarkerCode = Val.ToString(DRowMainPacket["MARKERCODE"]);

                            string StrBarcode = StrKapanName + Environment.NewLine + pIntPktNo + Environment.NewLine + pStrTag;
                            string StrPrint = StrKapanName + "-" + pIntPktNo + "-" + pStrTag;
                            string OM = "OM";
                            sw.WriteLine("I8,A");
                            sw.WriteLine("ZN");
                            sw.WriteLine("q400");
                            sw.WriteLine("O");
                            sw.WriteLine("JF");
                            sw.WriteLine("KIZZQ0");
                            sw.WriteLine("KI9+0.0");
                            sw.WriteLine("ZT");
                            sw.WriteLine("Q120,25");
                            sw.WriteLine("Arglabel 200 31");
                            sw.WriteLine("exit");
                            sw.WriteLine("KI80");
                            sw.WriteLine("N");
                            //sw.WriteLine("B351,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                            sw.WriteLine("B325,95,2,1,1,2,51,N,\"" + StrBarcode + "\"");
                            sw.WriteLine("A325,120,2,3,1,1,N,\"" + StrPrint + "\"");
                            sw.WriteLine("A140,140,2,1,1,1,N,\"" + Date + "\"");
                            sw.WriteLine("A325,28,2,3,1,1,N,\"" + MarkerCode + "\"");
                            sw.WriteLine("A240,28,2,3,1,1,N,\"" + Carat + "\"");
                            sw.WriteLine("P1");
                            sw.WriteLine("");
                        }
                        sw.Close();
                    }
                    //sw.Dispose();
                    //sw = null;
                    if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                    {
                        Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                    }
                    */
                        #endregion

                        /*
                        //#P : 16-05-2022 : Multiple Barcode in one Print
                        DataTable DTabPrint = (DataTable)MainGrid.DataSource;

                        if (RbtTSC.Checked == true)
                        {
                            Global.BarcodePrintTSC(DTabPrint);
                        }
                        else if (RbtCitizen.Checked == true)
                        {
                            Global.BarcodePrintCitizen(DTabPrint);
                        }
                        else if (RbtTscGalaxy.Checked == true)
                        {
                            //Global.BarcodePrintTSCGalaxy(Val.ToString(DRow["KAPANNAME"]),
                            //    Val.ToString(DRow["PACKETNO"]),
                            //    Val.ToString(DRow["TAG"]),
                            //    Val.ToString(DRow["LOTCARAT"]),
                            //    Val.ToString(DRow["KAPANMANAGERCODE"]),
                            //    Val.ToString(DRow["PACKETGROUPCODE"]),
                            //    Val.ToString(DRow["COLORSHADECODE"]),
                            //    Val.ToString(DRow["BARCODE"]));
                        }
                         * End : #p : 16-05-2022 : WorkPending 
                        */
                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRows = GrdDet.GetDataRow(IntI);

                            if (Val.ToInt32(DRows["ISSUEPCS"]) == 0 || Val.Val(DRows["ISSUECARAT"]) == 0)
                            {
                                continue;
                            }

                            string StrGroup = Val.ToString(txtPacketGrade.Text) + "/" + Val.ToString(txtPacketGroup.Text);
                            string StrParentTag = "(" + Val.ToString(txtTag.Text) + ")";

                            DataTable DTabPacketLiveStock = ObjKapan.GetPacketDataForBarcodePrint(txtKapanName.Text, "", "", Val.ToInt(txtPacketNo.Text), Val.ToInt(txtPacketNo.Text), Val.ToString(txtTag.Text));

                            if (RbtTSC.Checked == true)
                            {
                                Global.BarcodePrintTSC(Val.ToString(txtKapanName.Text),
                                    Val.ToString(txtPacketNo.Text),
                                    Val.ToString(DRows["TAG"]),
                                    Val.ToString(DTPTransferDate.Value.ToString("dd-MM-yy")),
                                    Val.ToString(DRows["ISSUECARAT"]),
                                    Val.ToString(Val.ToString(DTabPacketLiveStock.Rows[0]["MARKERCODE"])),
                                    Val.ToString(DRows["BARCODE"]),
                                    StrGroup,
                                    StrParentTag
                                    );

                            }
                            else if (RbtCitizen.Checked == true)
                            {
                                Global.BarcodePrintCitizen(Val.ToString(txtKapanName.Text),
                                        Val.ToString(txtPacketNo.Text),
                                        Val.ToString(DRows["TAG"]),
                                        Val.ToString(DTPTransferDate.Value.ToString("dd-MM-yy")),
                                        Val.ToString(DRows["ISSUECARAT"]),
                                        Val.ToString(Val.ToString(DTabPacketLiveStock.Rows[0]["MARKERCODE"])),
                                        Val.ToString(DRows["BARCODE"]),
                                        Val.ToString(DRows["PKTSERIALNO"]),
                                        Val.ToString(txtTag.Text)
                                        );

                            }
                            else if (RbtTscGalaxy.Checked == true)
                            {
                                //Global.BarcodePrintTSCGalaxy(Val.ToString(DRow["KAPANNAME"]),
                                //    Val.ToString(DRow["PACKETNO"]),
                                //    Val.ToString(DRow["TAG"]),
                                //    Val.ToString(DRow["LOTCARAT"]),
                                //    Val.ToString(DRow["KAPANMANAGERCODE"]),
                                //    Val.ToString(DRow["PACKETGROUPCODE"]),
                                //    Val.ToString(DRow["COLORSHADECODE"]),
                                //    Val.ToString(DRow["BARCODE"]));
                            }
                        }
                    }

                    #endregion

                    txtIssuePcs.Text = "1";
                    txtIssueCarat.Text = string.Empty;

                    txtReadyPcs.Text = "1";
                    txtReadyCarat.Text = string.Empty;

                    txtTotalSecondPcs.Text = string.Empty;
                    txtTotalSecondCarat.Text = string.Empty;

                    txtLostPcs.Text = string.Empty;
                    txtLostCarat.Text = string.Empty;
                    txtLossCarat.Text = string.Empty;
                    txtMixingLessPlus.Text = string.Empty;

                    txtTotalSecondPcs.Text = string.Empty;
                    txtTotalSecondCarat.Text = string.Empty;
                    txtTotalExtraPcs.Text = string.Empty;
                    txtTotalExtraCarat.Text = string.Empty;
                    txtRejectionPcs.Text = string.Empty;
                    txtRejectionCarat.Text = string.Empty;

                    txtTotalCarat.Text = string.Empty;
                    txtExtraCarat.Text = string.Empty;
                    txtSecondPcs.Text = string.Empty;
                    txtDiffCarat.Text = string.Empty;

                    txtTag.Tag = string.Empty;
                    txtKapanName.Text = string.Empty;
                    txtPacketNo.Text = string.Empty;
                    txtTag.Text = string.Empty;
                    txtExpWeight.Text = string.Empty;
                    txtPrdShp.Text = string.Empty;
                    txtPrdShp.Tag = string.Empty;
                    txtPrdCts.Text = string.Empty;

                    txtBarcode.Text = string.Empty;
                    txtSrNoKapanName.Text = string.Empty;
                    txtSrNoSerialNo.Text = string.Empty;
                    txtJangedNo.Text = string.Empty;

                    //chkRejectionTransfer.Checked = false;

                    DTabNewPacket.Rows.Clear();
                    DTabPrd.Rows.Clear();

                    this.Cursor = Cursors.Default;

                    Global.Message("Your Packet Successfully Splited : " + txtTransferTo.Text + "\n\nYour Slip Number : " + JangedNo);
                    PanelHeader.Enabled = true;
                    txtKapanName.Focus();

                }

                Property = null;
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }


        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                   /*
                if (GrdDet.FocusedRowHandle >= 0 && Val.Val(GrdDet.GetFocusedRowCellValue("ISSUECARAT")) != 0 && GrdDet.IsLastRow)
                {
                 
                    DataRow DRNew = DTabNewPacket.NewRow();
                    DRNew["ISSUEPCS"] = 1;
                    DRNew["PACKETTYPE"] = "SECOND";
                    DTabNewPacket.Rows.Add(DRNew);
                     
                }
                else
                {
                    BtnSave.Focus();
                }
                    * * */
                if (GrdDet.FocusedRowHandle >= 0 && GrdDet.IsLastRow)
                {
                    BtnSave.Focus();
                }
            }
            GrdDet_CellValueChanging(null, null);
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            if (txtKapanName.Text.Trim().Length == 0)
            {
                //Global.Message("Kapan Name Is Required");
                txtKapanName.Focus();
                return;
            }
            if (txtTag.Text.Trim().Length == 0)
            {
                //Global.Message("Tag Is Required");
                txtTag.Focus();
                return;
            }
            if (Val.ToInt32(txtPacketNo.Text) == 0)
            {
                //Global.Message("Packet No Is Required");
                txtPacketNo.Focus();
                return;
            }

            txtIssuePcs.Text = "1";
            txtIssueCarat.Text = string.Empty;

            txtReadyPcs.Text = "1";
            txtReadyCarat.Text = string.Empty;

            txtTotalSecondPcs.Text = string.Empty;
            txtTotalSecondCarat.Text = string.Empty;

            txtLostPcs.Text = string.Empty;
            txtLostCarat.Text = string.Empty;
            txtLossCarat.Text = string.Empty;
            txtMixingLessPlus.Text = string.Empty;
            txtTag.Tag = string.Empty;

            DRowMainPacket = ObjTrn.GetPacketInfo(txtKapanName.Text, Val.ToInt32(txtPacketNo.Text), Val.ToString(txtTag.Text), Val.ToString(txtBarcode.Text), Val.ToInt32(txtSrNoSerialNo.Text), Val.ToInt64(txtJangedNo.Text));
            if (DRowMainPacket == null)
            {
                Global.MessageError("This Packet Is Not In Your Stock");
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtKapanName.Focus();
                return;
            }

            if (Val.ToString(DRowMainPacket["ENTRYTYPE"]) != "EMPISS") //#P : 18-10-2022
            {
                Global.MessageError("This Packet Is Not In 'ISSUE' status So You Can't Split");
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtKapanName.Focus();
                return;
            }

            txtIssuePcs.Text = Val.ToString(DRowMainPacket["BALANCEPCS"]);
            txtIssueCarat.Text = Val.ToString(DRowMainPacket["BALANCECARAT"]);
            txtTag.Tag = Val.ToString(DRowMainPacket["TAGSRNO"]);

            txtPacketGrade.Tag = Val.ToString(DRowMainPacket["PACKETGRADE_ID"]);
            txtPacketGrade.Text = Val.ToString(DRowMainPacket["PACKETGRADECODE"]);
            txtPacketGroup.Tag = Val.ToString(DRowMainPacket["PACKETGROUP_ID"]);
            txtPacketGroup.Text = Val.ToString(DRowMainPacket["PACKETGROUPCODE"]);

            if (ChkJumpISSToTRN.Checked) //#p : 07-06-2022 // && txtTransferTo.Text.Trim().Equals(string.Empty)(Coz DiffPkts ma DiffMarker hase.. )
            {
                txtTransferTo.Text = Val.ToString(DRowMainPacket["MARKERCODE"]);
                txtTransferTo.Tag = Val.ToString(DRowMainPacket["MARKER_ID"]);
                txtType.Text = Val.ToString(DRowMainPacket["MARKERLEDGERGROUP"]);
                txtDepartment.Text = Val.ToString(DRowMainPacket["MARKERDEPARTMENTNAME"]);
                txtDepartment.Tag = Val.ToString(DRowMainPacket["MARKERDEPARTMENT_ID"]);
                mBoolAutoConfirm = Val.ToBoolean(DRowMainPacket["MARKERAUTOCONFIRM"]);
                txtManager.Text = Val.ToString(DRowMainPacket["MARKERMANAGERNAME"]);
                txtManager.Tag = Val.ToString(DRowMainPacket["MARKERMANAGER_ID"]);
            }

            BtnContinue.Focus();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            PanelHeader.Enabled = false;

            /* #P : 03-10-2022 : Cmnt And Add : New Row Add as Per Second Pcs
            DataRow DRNew = DTabNewPacket.NewRow();
            DRNew["ISSUEPCS"] = 1;
            DRNew["PACKETTYPE"] = "SECOND";
            DTabNewPacket.Rows.Add(DRNew);
             * */

            //End : #P : 03-10-2022

            ////txtReadyCarat.Focus();
            ////DTabPrd.Rows.Clear();
            ////DataTable DtabDetail = ObjTrn.GetDataForSplitWithPrd(Val.ToString(txtKapanName.Text), Val.ToInt32(txtPacketNo.Text), Val.ToString(txtTag.Text));

            ////DataRow[] dr = DtabDetail.Select("TAG <> '" + Val.ToString(txtTag.Text) + "'");
            ////if (dr.Length > 0)
            ////{
            ////    DTabPrd = dr.CopyToDataTable();
            ////}

            ////DataRow[] drMainTag = DtabDetail.Select("TAG = '" + Val.ToString(txtTag.Text) + "'");
            ////if (drMainTag.Length > 0)
            ////{
            ////    txtPrdCts.Text = Val.ToString(drMainTag[0]["PRDCARAT"]);
            ////    txtPrdShp.Text = Val.ToString(drMainTag[0]["SHAPENAME"]);
            ////    txtPrdShp.Tag = Val.ToString(drMainTag[0]["SHAPE_ID"]);
            ////}

            ////if (DTabPrd.Rows.Count == 0)
            ////{
            ////    DTabPrd.Rows.Clear();
            ////    if (Val.ToInt32(txtProcessTo.Tag) == 2728)
            ////    {

            ////    }
            ////    else
            ////    {
            ////        return;
            ////    }
            ////Global.Message("You Have Not Enogh Data For Split");
            ////BtnContinue.Enabled = true;
            ////txtIssuePcs.Text = "1";
            ////txtIssueCarat.Text = string.Empty;

            ////txtReadyPcs.Text = "1";
            ////txtReadyCarat.Text = string.Empty;

            ////txtSecondPcs.Text = string.Empty;
            ////txtSecondCarat.Text = string.Empty;

            ////txtLostPcs.Text = string.Empty;
            ////txtLostCarat.Text = string.Empty;
            ////txtLossCarat.Text = string.Empty;
            ////txtMixingLessPlus.Text = string.Empty;

            ////txtSecondPcs.Text = string.Empty;
            ////txtSecondCarat.Text = string.Empty;
            ////txtExtraPcs.Text = string.Empty;
            ////txtExtraCarat.Text = string.Empty;
            ////txtRejectionPcs.Text = string.Empty;
            ////txtRejectionCarat.Text = string.Empty;

            ////txtTag.Tag = string.Empty;
            ////txtKapanName.Text = string.Empty;
            ////txtPacketNo.Text = string.Empty;
            ////txtTag.Text = string.Empty;
            ////txtExpWeight.Text = string.Empty;
            ////else
            ////{
            ////    DTabNewPacket = DTabPrd.Copy();
            ////    MainGrid.DataSource = DTabNewPacket;
            ////    GrdDet.RefreshData();
            ////}
            ////if (DTabPrd.Rows.Count > 0)
            ////{
            ////    DTabNewPacket = DTabPrd.Copy();
            ////    MainGrid.DataSource = DTabNewPacket;
            ////    GrdDet.RefreshData();
            ////}
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e == null || e.RowHandle < 0)
                {
                    return;
                }

                int IntSecondPcs = 0, IntExtraPcs = 0, IntRejectionPcs = 0;
                double DouSecondCarat = 0, DouExtraCarat = 0, DouRejectionCarat = 0;

                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    if (Val.Val(GrdDet.GetRowCellValue(IntI, "ISSUEPCS")) == 0 || Val.Val(GrdDet.GetRowCellValue(IntI, "ISSUECARAT")) == 0)
                    {
                        continue;
                    }

                    if (Val.ToString(GrdDet.GetRowCellValue(IntI, "PACKETTYPE")) == "SECOND")
                    {
                        IntSecondPcs = IntSecondPcs + 1;
                        DouSecondCarat = DouSecondCarat + Val.Val(GrdDet.GetRowCellValue(IntI, "ISSUECARAT"));

                    }
                    else if (Val.ToString(GrdDet.GetRowCellValue(IntI, "PACKETTYPE")) == "EXTRA")
                    {
                        IntExtraPcs = IntExtraPcs + 1;
                        DouExtraCarat = DouExtraCarat + Val.Val(GrdDet.GetRowCellValue(IntI, "ISSUECARAT"));

                    }
                    else if (Val.ToString(GrdDet.GetRowCellValue(IntI, "PACKETTYPE")) == "REJECTION")
                    {

                        IntRejectionPcs = IntRejectionPcs + 1;
                        DouRejectionCarat = DouRejectionCarat + Val.Val(GrdDet.GetRowCellValue(IntI, "ISSUECARAT"));

                    }
                }

                txtTotalSecondPcs.Text = IntSecondPcs.ToString();
                txtTotalSecondCarat.Text = DouSecondCarat.ToString();

                //txtTotalExtraPcs.Text = IntExtraPcs.ToString();
                //txtTotalExtraCarat.Text = DouExtraCarat.ToString();

                txtRejectionPcs.Text = IntRejectionPcs.ToString();
                txtRejectionCarat.Text = DouRejectionCarat.ToString();

                DataRow DRow = GrdDet.GetDataRow(e.RowHandle);

                if (e.Column.FieldName == "EXPWEIGHT")
                {
                    double pDouIssueCarat = 0;
                    double PDouExpWeight = 0;

                    pDouIssueCarat = Val.Val(GrdDet.GetFocusedRowCellValue("ISSUECARAT"));
                    PDouExpWeight = Val.Val(GrdDet.GetFocusedRowCellValue("EXPWEIGHT"));

                    if (pDouIssueCarat < PDouExpWeight)
                    {
                        Global.MessageError("You Can't Enter ExpCarat More Then Issue Carat...!");
                        GrdDet.SetRowCellValue(e.RowHandle, "EXPWEIGHT", 0);
                    }
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
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
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANSINGLE);
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void txtReason_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "REASONCODE,REASONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_REASON);
                    FrmSearch.mColumnsToHide = "REASON_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("REASON_ID", Val.ToString(FrmSearch.mDRow["REASON_ID"]));
                        GrdDet.SetFocusedRowCellValue("REASONNAME", Val.ToString(FrmSearch.mDRow["REASONNAME"]));
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

        private void txtReadyCarat_TextChanged(object sender, EventArgs e)
        {
            CalculationDiff();
        }

        public void CalculationDiff()
        {
            txtLossCarat.Text = Math.Round(Val.Val(txtIssueCarat.Text) - (Val.Val(txtReadyCarat.Text) + Val.Val(txtTotalExtraCarat.Text) + Val.Val(txtTotalSecondCarat.Text) + Val.Val(txtRejectionCarat.Text)), 3).ToString();
            txtDiffCarat.Text = (Val.Val(txtTotalCarat.Text) - Val.Val(txtReadyCarat.Text) - Val.Val(txtExtraCarat.Text) - Val.Val(txtTotalSecondCarat.Text)).ToString();
        }


        private void txtReadyCarat_Validated(object sender, EventArgs e)
        {
            //if (GrdDet.RowCount >= 1)
            //{
            //    if (Global.Confirm("Do You Want To Clear Sub Packet Details ?") == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        DTabNewPacket.Rows.Clear();
            //    }
            //}

            //DataRow DRNew = DTabNewPacket.NewRow();
            //DRNew["ISSUEPCS"] = 1;
            //DRNew["PACKETTYPE"] = "SECOND";

            //DTabNewPacket.Rows.Add(DRNew);

            //GrdDet.Focus();
            //GrdDet.FocusedRowHandle = 0;
            //GrdDet.FocusedColumn = GrdDet.Columns["PACKETTYPE"];
            //GrdDet.PostEditor();
            //GrdDet_CellValueChanging(GrdDet, null);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle < 0)
            {
                return;
            }
            DataRow DRow = GrdDet.GetFocusedDataRow();
            if (Val.Val(DRow["ISSUECARAT"]) != 0)
            {
                Global.Message("You Can't Delete This Entry");
                return;
            }

            if (Global.Confirm("Do You Want To Delete this entry ?") == System.Windows.Forms.DialogResult.Yes)
            {
                GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
                DTabNewPacket.AcceptChanges();
            }

        }

        private void txtCarat_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            //{
            //    GrdDet.PostEditor();
            //    if (GrdDet.FocusedRowHandle >= 0 && Val.Val(GrdDet.GetFocusedRowCellValue("EXPWEIGHT")) != 0 && GrdDet.IsLastRow)
            //    {
            //        DataRow DRNew = DTabNewPacket.NewRow();
            //        DRNew["ISSUEPCS"] = 1;
            //        DRNew["PACKETTYPE"] = "SECOND";
            //        if (Val.ToInt32(txtProcessTo.Tag) == 2728)
            //        {
            //            DTabNewPacket.Rows.Add(DRNew);
            //        }
            //    }
            //    else
            //    {
            //        BtnSave.Focus();
            //        e.Handled = true;
            //    }
            //    GrdDet.Focus();
            //    GrdDet.FocusedRowHandle = GrdDet.FocusedRowHandle + 1;
            //    GrdDet.FocusedColumn = GrdDet.Columns["PACKETTYPE"];
            //}
            // GrdDet_CellValueChanging(null, null);
        }

        private void txtExpWeight_Validating(object sender, CancelEventArgs e)
        {
            //if (GrdDet.RowCount >= 1)
            //{
            //    if (Global.Confirm("Do You Want To Clear Sub Packet Details ?") == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        DTabNewPacket.Rows.Clear();
            //    }
            //}

            DataRow DRNew = DTabNewPacket.NewRow();
            DRNew["ISSUEPCS"] = 1;
            DRNew["PACKETTYPE"] = "SECOND";
            GrdDet.Focus();
            GrdDet.FocusedRowHandle = 0;
            GrdDet.FocusedColumn = GrdDet.Columns["PACKETTYPE"];
            GrdDet.PostEditor();
            GrdDet_CellValueChanging(GrdDet, null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIssuePcs.Text = "1";
            txtIssueCarat.Text = string.Empty;

            txtReadyPcs.Text = "1";
            txtReadyCarat.Text = string.Empty;

            txtTotalSecondPcs.Text = string.Empty;
            txtTotalSecondCarat.Text = string.Empty;

            txtLostPcs.Text = string.Empty;
            txtLostCarat.Text = string.Empty;
            txtLossCarat.Text = string.Empty;
            txtMixingLessPlus.Text = string.Empty;

            txtTotalSecondPcs.Text = string.Empty;
            txtTotalSecondCarat.Text = string.Empty;
            txtTotalExtraPcs.Text = string.Empty;
            txtTotalExtraCarat.Text = string.Empty;
            txtRejectionPcs.Text = string.Empty;
            txtRejectionCarat.Text = string.Empty;


            txtTotalCarat.Text = string.Empty;
            txtExtraCarat.Text = string.Empty;
            txtSecondPcs.Text = string.Empty;
            txtDiffCarat.Text = string.Empty;

            txtTag.Tag = string.Empty;
            txtKapanName.Text = string.Empty;
            txtPacketNo.Text = string.Empty;
            txtTag.Text = string.Empty;
            txtExpWeight.Text = string.Empty;
            txtPrdShp.Text = string.Empty;
            txtPrdShp.Tag = string.Empty;
            txtPrdCts.Text = string.Empty;
            DTabNewPacket.Rows.Clear();

            txtBarcode.Text = string.Empty;
            txtSrNoKapanName.Text = string.Empty;
            txtSrNoSerialNo.Text = string.Empty;

            chkRejectionTransfer.Checked = false;

            if (RbtBarcode.Checked)
                txtBarcode.Focus();
            else if (RbtPacketNo.Checked)
                txtKapanName.Focus();
            else if (RbtPktSerialNo.Checked)
                txtSrNoKapanName.Focus();

            PanelHeader.Enabled = true;
        }

        private void RbtBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtBarcode.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Focus();
            }
            else if (RbtJangedNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtJangedNo.Focus();
            }
            else if (RbtPacketNo.Checked)
            {
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtKapanName.Focus();
            }
            else if (RbtPktSerialNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }
            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelJangedNo.Visible = RbtJangedNo.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;

            btnClear_Click(null, null);
        }

        private void txtBarcode_Validated(object sender, EventArgs e)
        {
            if (txtBarcode.Text.Trim().Length == 0)
            {
                return;
            }
            txtIssuePcs.Text = "1";
            txtIssueCarat.Text = string.Empty;

            txtReadyPcs.Text = "1";
            txtReadyCarat.Text = string.Empty;

            txtTotalSecondPcs.Text = string.Empty;
            txtTotalSecondCarat.Text = string.Empty;

            txtLostPcs.Text = string.Empty;
            txtLostCarat.Text = string.Empty;
            txtLossCarat.Text = string.Empty;
            txtMixingLessPlus.Text = string.Empty;
            txtTag.Tag = string.Empty;

            DRowMainPacket = ObjTrn.GetPacketInfo("", 0, "", Val.ToString(txtBarcode.Text), 0, 0);
            if (DRowMainPacket == null)
            {
                Global.MessageError("This Packet Is Not In Your Stock");
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
                return;
            }

            txtIssuePcs.Text = Val.ToString(DRowMainPacket["BALANCEPCS"]);
            txtIssueCarat.Text = Val.ToString(DRowMainPacket["BALANCECARAT"]);
            txtTag.Tag = Val.ToString(DRowMainPacket["TAGSRNO"]);
            txtTag.Text = Val.ToString(DRowMainPacket["TAG"]);
            txtKapanName.Text = Val.ToString(DRowMainPacket["KAPANNAME"]);
            txtPacketNo.Text = Val.ToString(DRowMainPacket["PACKETNO"]);

            txtPacketGrade.Tag = Val.ToString(DRowMainPacket["PACKETGRADE_ID"]);
            txtPacketGrade.Text = Val.ToString(DRowMainPacket["PACKETGRADECODE"]);
            txtPacketGroup.Tag = Val.ToString(DRowMainPacket["PACKETGROUP_ID"]);
            txtPacketGroup.Text = Val.ToString(DRowMainPacket["PACKETGROUPCODE"]);


            if (ChkJumpISSToTRN.Checked) //#p : 07-06-2022 // && txtTransferTo.Text.Trim().Equals(string.Empty)(Coz DiffPkts ma DiffMarker hase.. )
            {
                txtTransferTo.Text = Val.ToString(DRowMainPacket["MARKERCODE"]);
                txtTransferTo.Tag = Val.ToString(DRowMainPacket["MARKER_ID"]);
                txtType.Text = Val.ToString(DRowMainPacket["MARKERLEDGERGROUP"]);
                txtDepartment.Text = Val.ToString(DRowMainPacket["MARKERDEPARTMENTNAME"]);
                txtDepartment.Tag = Val.ToString(DRowMainPacket["MARKERDEPARTMENT_ID"]);
                mBoolAutoConfirm = Val.ToBoolean(DRowMainPacket["MARKERAUTOCONFIRM"]);
                txtManager.Text = Val.ToString(DRowMainPacket["MARKERMANAGERNAME"]);
                txtManager.Tag = Val.ToString(DRowMainPacket["MARKERMANAGER_ID"]);
            }

            BtnContinue.Focus();
        }

        private void txtSrNoSerialNo_Validated(object sender, EventArgs e)
        {
            if (txtSrNoKapanName.Text.Trim().Length == 0)
            {
                txtSrNoKapanName.Focus();
                return;
            }

            if (Val.ToInt32(txtSrNoSerialNo.Text) == 0)
            {
                return;
            }

            txtIssuePcs.Text = "1";
            txtIssueCarat.Text = string.Empty;

            txtReadyPcs.Text = "1";
            txtReadyCarat.Text = string.Empty;

            txtTotalSecondPcs.Text = string.Empty;
            txtTotalSecondCarat.Text = string.Empty;

            txtLostPcs.Text = string.Empty;
            txtLostCarat.Text = string.Empty;
            txtLossCarat.Text = string.Empty;
            txtMixingLessPlus.Text = string.Empty;
            txtTag.Tag = string.Empty;

            DRowMainPacket = ObjTrn.GetPacketInfo(txtSrNoKapanName.Text, 0, "", "", Val.ToInt32(txtSrNoSerialNo.Text), 0);
            if (DRowMainPacket == null)
            {
                Global.MessageError("This Packet Is Not In Your Stock");
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                txtSrNoKapanName.Focus();
                return;
            }

            txtIssuePcs.Text = Val.ToString(DRowMainPacket["BALANCEPCS"]);
            txtIssueCarat.Text = Val.ToString(DRowMainPacket["BALANCECARAT"]);
            txtTag.Tag = Val.ToString(DRowMainPacket["TAGSRNO"]);
            txtTag.Text = Val.ToString(DRowMainPacket["TAG"]);
            txtKapanName.Text = Val.ToString(DRowMainPacket["KAPANNAME"]);
            txtPacketNo.Text = Val.ToString(DRowMainPacket["PACKETNO"]);

            txtPacketGrade.Tag = Val.ToString(DRowMainPacket["PACKETGRADE_ID"]);
            txtPacketGrade.Text = Val.ToString(DRowMainPacket["PACKETGRADECODE"]);
            txtPacketGroup.Tag = Val.ToString(DRowMainPacket["PACKETGROUP_ID"]);
            txtPacketGroup.Text = Val.ToString(DRowMainPacket["PACKETGROUPCODE"]);

            if (ChkJumpISSToTRN.Checked) //#p : 07-06-2022 // && txtTransferTo.Text.Trim().Equals(string.Empty)(Coz DiffPkts ma DiffMarker hase.. )
            {
                txtTransferTo.Text = Val.ToString(DRowMainPacket["MARKERCODE"]);
                txtTransferTo.Tag = Val.ToString(DRowMainPacket["MARKER_ID"]);
                txtType.Text = Val.ToString(DRowMainPacket["MARKERLEDGERGROUP"]);
                txtDepartment.Text = Val.ToString(DRowMainPacket["MARKERDEPARTMENTNAME"]);
                txtDepartment.Tag = Val.ToString(DRowMainPacket["MARKERDEPARTMENT_ID"]);
                mBoolAutoConfirm = Val.ToBoolean(DRowMainPacket["MARKERAUTOCONFIRM"]);
                txtManager.Text = Val.ToString(DRowMainPacket["MARKERMANAGERNAME"]);
                txtManager.Tag = Val.ToString(DRowMainPacket["MARKERMANAGER_ID"]);
            }
            BtnContinue.Focus();
        }

        private void txtJangedNo_Validated(object sender, EventArgs e)
        {
            if (txtJangedNo.Text.Trim().Length == 0)
            {
                txtJangedNo.Focus();
                return;
            }
            txtIssuePcs.Text = "1";
            txtIssueCarat.Text = string.Empty;

            txtReadyPcs.Text = "1";
            txtReadyCarat.Text = string.Empty;

            txtTotalSecondPcs.Text = string.Empty;
            txtTotalSecondCarat.Text = string.Empty;

            txtLostPcs.Text = string.Empty;
            txtLostCarat.Text = string.Empty;
            txtLossCarat.Text = string.Empty;
            txtMixingLessPlus.Text = string.Empty;
            txtTag.Tag = string.Empty;

            DRowMainPacket = ObjTrn.GetPacketInfo(txtKapanName.Text, Val.ToInt32(txtPacketNo.Text), Val.ToString(txtTag.Text), Val.ToString(txtBarcode.Text), Val.ToInt32(txtSrNoSerialNo.Text), Val.ToInt64(txtJangedNo.Text));
            if (DRowMainPacket == null)
            {
                Global.MessageError("This Packet Is Not In Your Stock");
                txtJangedNo.Text = string.Empty;
                txtJangedNo.Focus();
                return;
            }

            txtIssuePcs.Text = Val.ToString(DRowMainPacket["BALANCEPCS"]);
            txtIssueCarat.Text = Val.ToString(DRowMainPacket["BALANCECARAT"]);
            txtTag.Tag = Val.ToString(DRowMainPacket["TAGSRNO"]);
            txtTag.Text = Val.ToString(DRowMainPacket["TAG"]);
            txtKapanName.Text = Val.ToString(DRowMainPacket["KAPANNAME"]);
            txtPacketNo.Text = Val.ToString(DRowMainPacket["PACKETNO"]);

            txtPacketGrade.Tag = Val.ToString(DRowMainPacket["PACKETGRADE_ID"]);
            txtPacketGrade.Text = Val.ToString(DRowMainPacket["PACKETGRADECODE"]);
            txtPacketGroup.Tag = Val.ToString(DRowMainPacket["PACKETGROUP_ID"]);
            txtPacketGroup.Text = Val.ToString(DRowMainPacket["PACKETGROUPCODE"]);

            if (ChkJumpISSToTRN.Checked) //#p : 07-06-2022 // && txtTransferTo.Text.Trim().Equals(string.Empty)(Coz DiffPkts ma DiffMarker hase.. )
            {
                txtTransferTo.Text = Val.ToString(DRowMainPacket["MARKERCODE"]);
                txtTransferTo.Tag = Val.ToString(DRowMainPacket["MARKER_ID"]);
                txtType.Text = Val.ToString(DRowMainPacket["MARKERLEDGERGROUP"]);
                txtDepartment.Text = Val.ToString(DRowMainPacket["MARKERDEPARTMENTNAME"]);
                txtDepartment.Tag = Val.ToString(DRowMainPacket["MARKERDEPARTMENT_ID"]);
                mBoolAutoConfirm = Val.ToBoolean(DRowMainPacket["MARKERAUTOCONFIRM"]);
                txtManager.Text = Val.ToString(DRowMainPacket["MARKERMANAGERNAME"]);
                txtManager.Tag = Val.ToString(DRowMainPacket["MARKERMANAGER_ID"]);
            }

            BtnContinue.Focus();
        }

        private void txtSecondPcs_Validated(object sender, EventArgs e)
        {
            try
            {
                int TotalSecondPcs = Val.ToInt(txtSecondPcs.Text);
                int TotalRowsInDataTable = DTabNewPacket.Rows.Count;

                if (TotalRowsInDataTable > TotalSecondPcs)
                {
                    Global.MessageError("Oops..You Have To Delete Rows From Second Detail List.");
                    txtSecondPcs.Text = "0";
                    return;
                }

                for (int i = TotalRowsInDataTable; i < TotalSecondPcs; i++)
                {
                    DataRow DRNew = DTabNewPacket.NewRow();
                    DRNew["ISSUEPCS"] = 1;
                    DRNew["PACKETTYPE"] = "SECOND";
                    DTabNewPacket.Rows.Add(DRNew);
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void txtExtraCarat_TextChanged(object sender, EventArgs e)
        {
            txtTotalExtraPcs.Text = "1";
            txtTotalExtraCarat.Text = txtExtraCarat.Text;
        }

        private void ChkJumpISSToTRN_CheckedChanged(object sender, EventArgs e)
        {
            

            if (ChkJumpISSToTRN.Checked == true)
            {
                txtProcessToReturn.Visible = true;
                lblReturnProcess.Visible = true;
            }
            else
            {
                txtProcessToReturn.Visible = false;
                lblReturnProcess.Visible = false;
            }
        }

        private void txtProcessToReturn_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcessToReturn.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcessToReturn.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        if (txtProcessTo.Text.Trim().Equals(string.Empty))
                        {
                            txtProcessTo.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                            txtProcessTo.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                            txtRequiredProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                            txtRequiredProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                            foreach (DataRow DRow in DTabNewPacket.Rows)
                            {
                                DRow["TOPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                                DRow["TOPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);

                                DRow["NEXTPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                                DRow["NEXTPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                            }
                        }

                        DTabNewPacket.AcceptChanges();
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
    }
}
