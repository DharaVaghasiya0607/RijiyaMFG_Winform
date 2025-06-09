using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmEmployeeRights : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_FormPermission ObjMast = new BOMST_FormPermission();

        DataTable DTabForm = new DataTable();
        DataTable DTabDisplay = new DataTable();
        DataTable DTabTransfer = new DataTable();
        DataTable DTabProcess = new DataTable();
        DataTable DTabReport = new DataTable();


        #region Property Settings

        public FrmEmployeeRights()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            txtEmployee.Focus();

            DataTable DTabPrdType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRDTYPE);
            CmbPrdType.Properties.DataSource = DTabPrdType;
            CmbPrdType.Properties.DisplayMember = "PRDTYPENAME";
            CmbPrdType.Properties.ValueMember = "PRDTYPE_ID";

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        BtnShow_Click(null, null);
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


        private void BtnShow_Click(object sender, EventArgs e)
        {
            DTabForm.Rows.Clear();
            DTabDisplay.Rows.Clear();
            DTabTransfer.Rows.Clear();
            DTabProcess.Rows.Clear();
            DTabReport.Rows.Clear();
            Fill(Val.ToInt64(txtEmployee.Tag));
        }

        public void Fill(Int64 pIntEmployeeID)
        {
            this.Cursor = Cursors.WaitCursor;
            DataSet DS = ObjMast.Fill(pIntEmployeeID);

            DTabForm = DS.Tables[0];
            DTabDisplay = DS.Tables[1];
            DTabTransfer = DS.Tables[2];
            DTabProcess = DS.Tables[3];
            DTabReport = DS.Tables[4];

            GrdDetForm.BeginUpdate();
            GrdDetDisplay.BeginUpdate();
            GrdDetTransfer.BeginUpdate();
            GrdProcess.BeginUpdate();
            GrdDetReport.BeginUpdate();

            MainGridForm.DataSource = DTabForm;
            GrdDetForm.RefreshData();

            MainGridDisplay.DataSource = DTabDisplay;
            GrdDetDisplay.RefreshData();

            MainGridTransfer.DataSource = DTabTransfer;
            GrdDetTransfer.RefreshData();

            MainGridReport.DataSource = DTabReport;
            GrdDetReport.RefreshData();





           // DTabProcess.Rows.Add(DTabProcess.NewRow());

         
            if (DTabProcess.Rows.Count > 0)
            {
                int MaxSrNo = 0;
                MaxSrNo = (int)DTabProcess.Compute("Max(SRNO)", "");
                DataRow Dr = DTabProcess.NewRow();
                Dr["SRNO"] = MaxSrNo + 1;
                DTabProcess.Rows.Add(Dr);
                DTabProcess.AcceptChanges();

            }
            else
            {
                DataRow Dr = DTabProcess.NewRow();
                Dr["SRNO"] = 1;
                DTabProcess.Rows.Add(Dr);
                DTabProcess.AcceptChanges();
            }
            MainGrdProcess.DataSource = DTabProcess;
            GrdProcess.RefreshData();


            GrdDetForm.EndUpdate();
            GrdDetDisplay.EndUpdate();
            GrdDetTransfer.EndUpdate();
            GrdProcess.EndUpdate();
            GrdDetReport.EndUpdate();

            EmployeeActionRightsProperty Property = ObjMast.EmployeeActionRightsGetDataByPK(pIntEmployeeID);
            CmbPrdType.SetEditValue(Property.PRDTYPE_ID);
            ChkFullStock.Checked = Property.ISFULLSTOCK;
            ChkDeptStock.Checked = Property.ISDEPTSTOCK;
            ChkMyStock.Checked = Property.ISMYSTOCK;
            ChkOtherStock.Checked = Property.ISOTHERSTOCK;
            ChkTransfer.Checked = Property.DEPTTRANSFER;
            ChkIssue.Checked = Property.EMPISSUE;
            ChkReturn.Checked = Property.EMPRETURN;
            ChkSplit.Checked = Property.RETURNWITHSPLIT;
            ChkRejectionTransfer.Checked = Property.REJECTIONTRANSFER;
            // DHARA : 21-04-22
            ChkIsMainFullStock.Checked = Property.ISMAINFULLSTOCK;
            ChkIsMainDeptStock.Checked = Property.ISMAINDEPTSTOCK;
            ChkIsMainMyStock.Checked = Property.ISMAINMYSTOCK;
            ChkIsMainOtherStock.Checked = Property.ISMAINOTHERSTOCK;

            ChkIsSubFullStock.Checked = Property.ISSUBFULLSTOCK;
            ChkIsSubDeptStock.Checked = Property.ISSUBDEPTSTOCK;
            ChkIsSubMyStock.Checked = Property.ISSUBMYSTOCK;
            ChkIsSubOtherStock.Checked = Property.ISSUBOTHERSTOCK;

            ChkIsSubTransfer.Checked = Property.ISSUBDEPTTRANSFER;
            ChkIsSubIssue.Checked = Property.ISSUBEMPISSUE;
            ChkIsSubReturn.Checked = Property.ISSUBEMPRETURN;
            ChkIsSubSplit.Checked = Property.ISSUBRETURNWITHSPLIT;
            ChkIsSubRejection.Checked = Property.ISSUBREJECTIONTRANSFER;
            // DHARA : 21-04-22
            txtIPAddress.Text = Property.IPADDRESS;
            ChkAllowAllIP.Checked = Property.ALLOWALLIP;
            txtPassForDispDisc.Text = Property.RAPPASSFORDISPDISC;
            ChkAllowToChangeEmp.Checked = Property.RAPCHANGEEMPLOYEE;
            ChkAllowToOpenAllPkts.Checked = Property.RAPCHANGEPACKETS;
            ChkAllowForUpdtePrediction.Checked = Property.RAPUPDATEPREDICTION;
            txtMaxPacketNo.Text = Property.MAXPACKETSTOCK.ToString();
            txtUserName.Text = Property.USERNAME;
            txtPassword.Text = Property.PASSWORD;
            txtExtraPermissionPer.Text = Val.ToString(Property.EXTRAMINPER);
            ChkIsExtraMin.Checked = Property.ISALLOWEXTRAMIN;
            ChkISConfirmGrader.Checked = Property.ISCONFIRMGRADER;
            ChkGroupJangadNo.Checked = Property.ISGROUPJANGADNO;
            CmbBPrintType.Text = Val.ToString(Property.BPRINTTYPE);
            txtServerPath.Text = Val.ToString(Property.UPLOADSERVERPATH);
            txtServerUsername.Text = Val.ToString(Property.UPLOADSERVERUSERNAME);
            txtServerPassword.Text = Val.ToString(Property.UPLOADSERVERPASSWORD);
            TxtQCMainpath.Text = Val.ToString(Property.QCMAINSERVERPATH);
            TxtQCMainUser.Text = Val.ToString(Property.QCMAINSERVERUSERNAME);
            TxtQCMainPass.Text = Val.ToString(Property.QCMAINSERVERPASSWORD);

            txtQCUserServerPath.Text = Val.ToString(Property.QCUSERWISESERVERPATH);
            txtQCUserwiseUser.Text = Val.ToString(Property.QCUSERWISEUSERNAME);
            txtQCUserPassward.Text = Val.ToString(Property.QCUSERWISEPASSWARD);

            ChkIsDollarLock.Checked = Val.ToBoolean(Property.ISDOLLARLOCK);

            Property = null;

            this.Cursor = Cursors.Default;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployee.Text.Trim().Length == 0)
                {
                    Global.Message("Employee Name Is Required");
                    txtEmployee.Focus();
                    return;
                }

                DTabForm.AcceptChanges();
                DTabDisplay.AcceptChanges();
                DTabTransfer.AcceptChanges();
                DTabProcess.AcceptChanges();
                DTabReport.AcceptChanges();


                //DataTable Dt = ObjMast.CheckUsername(Val.ToInt64(txtEmployee.Tag), txtUserName.Text);
                //if (Dt.Rows.Count > 0)
                //{
                //    Global.Message("UserName Already Exists");
                //    return;
                //}

                this.Cursor = Cursors.WaitCursor;

                EmployeeActionRightsProperty Property = new EmployeeActionRightsProperty();
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                Property.PRDTYPE_ID = Val.Trim(CmbPrdType.Properties.GetCheckedItems());
                Property.ISFULLSTOCK = ChkFullStock.Checked;
                Property.ISDEPTSTOCK = ChkDeptStock.Checked;
                Property.ISMYSTOCK = ChkMyStock.Checked;
                Property.ISOTHERSTOCK = ChkOtherStock.Checked;
                Property.DEPTTRANSFER = ChkTransfer.Checked;
                Property.EMPISSUE = ChkIssue.Checked;
                Property.EMPRETURN = ChkReturn.Checked;
                Property.REJECTIONTRANSFER = ChkRejectionTransfer.Checked;
                Property.RETURNWITHSPLIT = ChkSplit.Checked;
                // Dhara : 21-04-2022 For Mix Live Stock User Rights
                Property.ISMAINFULLSTOCK = ChkIsMainFullStock.Checked;
                Property.ISMAINDEPTSTOCK = ChkIsMainDeptStock.Checked;
                Property.ISMAINMYSTOCK = ChkIsMainMyStock.Checked;
                Property.ISMAINOTHERSTOCK = ChkIsMainOtherStock.Checked;

                Property.ISSUBFULLSTOCK = ChkIsSubFullStock.Checked;
                Property.ISSUBDEPTSTOCK = ChkIsSubDeptStock.Checked;
                Property.ISSUBMYSTOCK = ChkIsSubMyStock.Checked;
                Property.ISSUBOTHERSTOCK = ChkIsSubOtherStock.Checked;

                Property.ISSUBDEPTTRANSFER = ChkIsSubTransfer.Checked;
                Property.ISSUBEMPISSUE = ChkIsSubIssue.Checked;
                Property.ISSUBEMPRETURN = ChkIsSubReturn.Checked;
                Property.ISSUBREJECTIONTRANSFER = ChkIsSubRejection.Checked;
                Property.ISSUBRETURNWITHSPLIT = ChkIsSubSplit.Checked;
                // Dhara : 21-04-2022
                Property.IPADDRESS = txtIPAddress.Text;
                Property.ALLOWALLIP = ChkAllowAllIP.Checked;
                Property.RAPPASSFORDISPDISC = txtPassForDispDisc.Text;
                Property.RAPCHANGEEMPLOYEE = ChkAllowToChangeEmp.Checked;
                Property.RAPCHANGEPACKETS = ChkAllowToOpenAllPkts.Checked;
                Property.RAPUPDATEPREDICTION = ChkAllowForUpdtePrediction.Checked;
                Property.MAXPACKETSTOCK = Val.ToInt(txtMaxPacketNo.Text);
                Property.USERNAME = txtUserName.Text;
                Property.PASSWORD = txtPassword.Text;
                Property.ISALLOWEXTRAMIN = ChkIsExtraMin.Checked;
                Property.EXTRAMINPER = Val.Val(txtExtraPermissionPer.Text);

                Property.ISCONFIRMGRADER = ChkISConfirmGrader.Checked;
                Property.ISGROUPJANGADNO = ChkGroupJangadNo.Checked;

                Property.BPRINTTYPE = CmbBPrintType.Text;
                Property.UPLOADSERVERPATH = txtServerPath.Text;
                Property.UPLOADSERVERUSERNAME = txtServerUsername.Text;
                Property.UPLOADSERVERPASSWORD = txtServerPassword.Text;

                Property.QCMAINSERVERPATH = TxtQCMainpath.Text;
                Property.QCMAINSERVERUSERNAME = TxtQCMainUser.Text;
                Property.QCMAINSERVERPASSWORD = TxtQCMainPass.Text;

                Property.QCUSERWISESERVERPATH = txtQCUserServerPath.Text;
                Property.QCUSERWISEUSERNAME = txtQCUserwiseUser.Text;
                Property.QCUSERWISEPASSWARD = txtQCUserPassward.Text;
                 
                // #Dhara : 14-04-2023
                Property.ISDOLLARLOCK = ChkIsDollarLock.Checked;
                //END #Dhara : 14-04-2023

               int IntRes = ObjMast.Save(Val.ToInt64(txtEmployee.Tag), DTabForm, DTabDisplay, DTabTransfer, DTabProcess,DTabReport, Property);

                Property = null;
                BtnSave.Focus();
                this.Cursor = Cursors.Default;

                if (IntRes != -1)
                {
                    Global.Message("SUCCESSFULLY SAVED RIGHTS");
                    Fill(Val.ToInt64(txtEmployee.Tag));
                }
                else
                {
                    Global.MessageError("OOPS SOMETHING GOES WRONG");
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

        private void txtCopyFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtCopyFrom.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtCopyFrom.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);

                        DTabForm.Rows.Clear();
                        DTabDisplay.Rows.Clear();
                        DTabTransfer.Rows.Clear();
                        DTabProcess.Rows.Clear();
                        DTabReport.Rows.Clear();

                        //#P : 12-12-2020 : Coz From User Select kare To ToEmp Vala nu Username And Pwd As It Revum Joiye
                        string StrToEmpUserName = "";
                        string StrToEmpPassword = "";
                        StrToEmpUserName = Val.ToString(txtUserName.Text);
                        StrToEmpPassword = Val.ToString(txtPassword.Text);

                        Fill(Val.ToInt64(txtCopyFrom.Tag));

                        //txtUserName.Text = string.Empty;
                        //txtPassword.Text = string.Empty;
                        txtUserName.Text = StrToEmpUserName;
                        txtPassword.Text = StrToEmpPassword;

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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            DTabForm.Rows.Clear();
            DTabDisplay.Rows.Clear();
            DTabTransfer.Rows.Clear();
            DTabProcess.Rows.Clear();
            DTabReport.Rows.Clear();
            txtEmployee.Text = "";
            txtEmployee.Tag = "";

            CmbPrdType.SetEditValue(-1);
            ChkFullStock.Checked = false;
            ChkDeptStock.Checked = false;
            ChkMyStock.Checked = false;
            ChkOtherStock.Checked = false;
            ChkTransfer.Checked = false;
            ChkIssue.Checked = false;
            ChkReturn.Checked = false;
            ChkSplit.Checked = false;
            txtIPAddress.Text = "";
            ChkAllowAllIP.Checked = false;
            ChkRejectionTransfer.Checked = false;
            txtPassForDispDisc.Text = "";
            ChkAllowToChangeEmp.Checked = false;
            ChkAllowToOpenAllPkts.Checked = false;
            ChkAllowForUpdtePrediction.Checked = false;
            txtMaxPacketNo.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;

            txtCopyFrom.Text = "";
            txtCopyFrom.Tag = "";
            txtEmployee.Focus();

            txtServerPath.Text = string.Empty;
            txtServerUsername.Text = string.Empty;
            txtServerPassword.Text = string.Empty;
            TxtQCMainpath.Text = string.Empty;
            TxtQCMainUser.Text = string.Empty;
            TxtQCMainPass.Text = string.Empty;

            txtQCUserServerPath.Text = string.Empty;
            txtQCUserwiseUser.Text = string.Empty;
            txtQCUserPassward.Text = string.Empty;

            ChkIsDollarLock.Checked = false;

            CmbBPrintType.SelectedIndex = 0;

        }

        public bool CheckDuplicate(DataTable Dt, string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            Dt.AcceptChanges();

            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in Dt.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        private void repTxtProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdProcess.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdProcess.PostEditor();
                        DataRow Dr = GrdProcess.GetFocusedDataRow();

                        GrdProcess.SetFocusedRowCellValue("PROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        GrdProcess.SetFocusedRowCellValue("PROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));

                        if (CheckDuplicate(DTabProcess, "PROCESSNAME", Val.ToString(GrdProcess.EditingValue), GrdProcess.FocusedRowHandle, "'" + Val.ToString(GrdProcess.EditingValue) + "'  PROCESS"))
                        {
                            GrdProcess.SetFocusedRowCellValue("PROCESSNAME", string.Empty);
                            GrdProcess.SetFocusedRowCellValue("PROCESS_ID", 0);
                            return;
                        }
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



        private void repDeleteProcessSetting_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GrdProcess.GetFocusedDataRow();
                if (dr != null)
                {
                    if (!Val.ToString(dr["PROCESSSETTING_ID"]).Trim().Equals(string.Empty))
                    {
                        int res = new BOMST_Ledger().DeleteLedgerDetailInfo("PROCESSSETTING", Val.ToString(dr["PROCESSSETTING_ID"]));
                        if (res > 0)
                        {
                            dr.Delete();
                            DTabProcess.AcceptChanges();
                        }
                    }
                    else
                    {
                        dr.Delete();
                        DTabProcess.AcceptChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtFromAmount_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                GrdProcess.PostEditor();
                DataRow Dr = GrdProcess.GetFocusedDataRow();
                if (GrdProcess.FocusedRowHandle < 0 || Val.Val(GrdProcess.EditingValue) == 0)
                    return;

                if (Val.Val(Dr["TOAMOUNT"]) > 0)
                    if (Val.Val(GrdProcess.EditingValue) > Val.Val(Dr["TOAMOUNT"]))
                    {
                        Global.Message("From Amount Must Be Less Than To Amount.!");
                        e.Cancel = true;
                        return;
                    }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtToAmount_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                GrdProcess.PostEditor();
                DataRow Dr = GrdProcess.GetFocusedDataRow();

                if (GrdProcess.FocusedRowHandle < 0 || Val.Val(GrdProcess.EditingValue) == 0)
                    return;

                if (Val.Val(Dr["FROMAMOUNT"]) > 0)
                    if (Val.Val(Dr["FROMAMOUNT"]) > Val.Val(GrdProcess.EditingValue))
                    {
                        Global.Message("To Amount Must Be Greater Than From Amount.!");
                        e.Cancel = true;
                        return;
                    }
                if (!Val.ToString(Dr["PROCESSNAME"]).Equals(string.Empty) && Val.Val(Dr["FROMAMOUNT"]) > 0 && Val.Val(GrdProcess.EditingValue) > 0 && GrdProcess.IsLastRow)
                {
                    int MaxSrNo = 0;
                    MaxSrNo = (int)DTabProcess.Compute("Max(SRNO)", "");
                    DataRow DRE = DTabProcess.NewRow();
                    DRE["SRNO"] = MaxSrNo + 1;
                    DTabProcess.Rows.Add(DRE);
                }
                else if (GrdProcess.IsLastRow)
                {
                    BtnSave.Focus();
                    //e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

      

    }
}

