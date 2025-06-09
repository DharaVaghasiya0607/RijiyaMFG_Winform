using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
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
using DevExpress.XtraGrid.Columns;
using AxoneMFGRJ;

namespace AxoneMFGRJ.Attendance
{
    public partial class FrmLeaveEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Attendance ObjMast = new BOMST_Attendance();
        BOTRN_LeaveEntry objLeave = new BOTRN_LeaveEntry();

        DataTable DtabSearch = new DataTable();

      
        double DouWHours = 10;

        #region Property Settings

        public FrmLeaveEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            Fill();
            
            this.Show();
            Clear();

            BtnSearch_Click(null, null);
        }

        public void Fill()
        {
            CmbEmployee.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
            CmbEmployee.Properties.DisplayMember = "EMPLOYEENAME";
            CmbEmployee.Properties.ValueMember = "EMPLOYEE_ID";
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

        #region Validation


        private bool ValSave()
        {

            if (Val.ToString(txtEmployeeCode.Text.Trim()) == "")
            {
                Global.Message("Employee Is Required");
                txtEmployeeCode.Focus();
                return false;
            }
            if (Val.ToString(txtReason.Text.Trim()) == "")
            {
                Global.Message("Reason Is Required");
                txtReason.Focus();
                return false;
            }
            if (Val.ToString(CmbLeaveStatus.Text.Trim()) == "APPROVED")
            {
                Global.Message("You Can't Update This Record.. Because It's Already Approved.");
                txtEmployeeCode.Focus();
                return false;
            }
            if (RbtFullDay.Checked == true && Val.ToInt32(txtTotalDays.Text) <= 0)
            {
                Global.Message("Please Insert Valid Leave From Date");
                DTPLeaveFromDate.Focus();
                return false;
            }

            return true;
        }
        private bool ValDelete()
        {
            if (Val.ToInt64(txtSlipNo.Text.Trim()) == 0)
            {
                Global.Message("SlipNo Is Required");
                txtSlipNo.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region Control Events

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                if (Global.Confirm("Do You Want To Save This Entry") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                string Status = "";

                LeaveEntryProperty Property = new LeaveEntryProperty();

                if (RbtFullDay.Checked)
                {
                    Status = "FULL DAY";
                    Property.LEAVEFROMTIME = "";
                    Property.LEAVETOTIME = "";
                }
                else if (RbtFirstHalf.Checked)
                {
                    Status = "FIRST HALF";
                    Property.LEAVEFROMTIME = "";
                    Property.LEAVETOTIME = "";

                }
                else if (RbtSecondHalf.Checked)
                {
                    Status = "SECOND HALF";
                    Property.LEAVEFROMTIME = "";
                    Property.LEAVETOTIME = "";

                }
                else if (RbtShortLeave.Checked)
                {
                    Status = "SHORT LEAVE";
                    Property.LEAVEFROMTIME = Val.SqlTime(TimeLeaveFrom.Text);
                    Property.LEAVETOTIME = Val.SqlTime(TimeLeaveTo.Text);
                }

                Property.LEAVE_ID = Val.ToString(txtSlipNo.Tag).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(txtSlipNo.Tag));
                Property.SLIPNO = Val.ToInt32(txtSlipNo.Text);
                Property.SLIPDATE = Val.SqlDate(DTPSlipDate.Text);

                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployeeCode.Tag);
                Property.REASON_ID = Val.ToInt32(txtReason.Tag);
                Property.OTHERREASON = Val.ToString(txtOtherReason.Text);

                Property.REMARK = Val.ToString(txtRemark.Text);
                Property.LEAVETYPE = Val.ToString(Status);

                Property.LEAVEFROMDATE = Val.SqlDate(DTPLeaveFromDate.Text);
                Property.LEAVETODATE = Val.SqlDate(DTPLeaveToDate.Text);
                Property.TOTALDAYS = Val.Val(txtTotalDays.Text);

                Property.TOTALHOURS = Val.Val(txtTotalHours.Text.ToString() + '.' + lblTotalMinutes.Text.ToString());
                Property.HH = Val.ToInt32(txtTotalHours.Text);
                Property.MM = Val.ToInt32(lblTotalMinutes.Text);

                Property.ISOFFICEWORK = Val.ToBoolean(ChkISOfficeWork.Checked);
                Property.LEAVESTATUS = CmbLeaveStatus.Text;
                Property.COMMENT = txtComment.Text;

                //Property.STATUSUPDATEDATE = txtStatusUpdateDate.Text;
                //Property.STATUSUPDATEBY = Val.ToInt32(txtStatusUpdateBy.Text);
                //Property.STATUSUPDATEIP = Val.ToString(txtStatusUpdateBy.Tag);

                Property = objLeave.Save(Property);

                string strMsg;

                strMsg = Property.ReturnMessageDesc + "\n Please Note Your SlipNo : " + Property.ReturnValue;
                Global.Message(strMsg);

                Clear();
                BtnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }
        public void Clear()
        {
            txtSlipNo.Text = string.Empty;
            txtSlipNo.Tag = string.Empty;

            txtEmployeeCode.Text = string.Empty;
            txtEmployeeCode.Tag = string.Empty;
            txtEmployeeName.Text = string.Empty;

            txtReason.Text = string.Empty;
            txtOtherReason.Text = string.Empty;
            txtRemark.Text = string.Empty;

            DTPSlipDate.Text = Val.ToString(DateTime.Now);
            ChkISOfficeWork.Checked = false;

            //txtTotalDays.Text = "0";
           

            //DTPLeaveFromDate.Text = DateTime.Now.AddDays(1).ToString();
            //DTPLeaveToDate.Text = DateTime.Now.AddDays(1).ToString();

            DTPLeaveFromDate.Text = DateTime.Now.ToString();
            DTPLeaveToDate.Text = DateTime.Now.ToString();

            BtnSlipPrint.Enabled = false;
            //TimeLeaveFrom.Text = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
            //TimeLeaveTo.Text = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
            TimeLeaveFrom.Text = string.Format("{0:HH:mm:ss tt}", "9:00 AM");
            TimeLeaveTo.Text = string.Format("{0:HH:mm:ss tt}", "7:00 PM");

            RbtFullDay.Checked = true;
            txtTotalHours.Text = "0";
            lblTotalMinutes.Text = "0";
            CmbLeaveStatus.Text = "PENDING";
            CmbLeaveStatus.Enabled = false;

            PicEmpPhoto.Image = null;

            txtComment.Text = string.Empty;
            txtStatusUpdateDate.Text = string.Empty;
            txtStatusUpdateBy.Text = string.Empty;

            DTPSearchFromDate.Text = DateTime.Now.AddMonths(-1).ToString();

            //BtnSearch_Click(null, null);

            txtEmployeeCode.Focus();
            
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        private void txtEmployeeCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEEWITHIMAGE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID,PROFILEIMAGE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployeeCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployeeCode.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        txtEmployeeName.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);

                        byte[] EmpProfile = FrmSearch.mDRow["PROFILEIMAGE"] as byte[] ?? null;
                        if (EmpProfile != null)
                        {
                            using (MemoryStream ms = new MemoryStream(EmpProfile))
                            {
                                PicEmpPhoto.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            PicEmpPhoto.Image = null;
                        }

                    }
                    else
                    {
                        txtEmployeeCode.Text = string.Empty;
                        txtEmployeeCode.Tag = string.Empty;
                        txtEmployeeName.Text = string.Empty;
                        PicEmpPhoto.Image = null;
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

        private void txtEmployeeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEEWITHIMAGE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID,PROFILEIMAGE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployeeCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployeeCode.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        txtEmployeeName.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);

                        byte[] EmpProfile = FrmSearch.mDRow["PROFILEIMAGE"] as byte[] ?? null;
                        if (EmpProfile != null)
                        {
                            using (MemoryStream ms = new MemoryStream(EmpProfile))
                            {
                                PicEmpPhoto.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            PicEmpPhoto.Image = null;
                        }

                    }
                    else
                    {
                        txtEmployeeCode.Text = string.Empty;
                        txtEmployeeCode.Tag = string.Empty;
                        txtEmployeeName.Text = string.Empty;
                        PicEmpPhoto.Image = null;
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
        private void DTPLeaveFromDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (DateTime.Parse(DTPLeaveFromDate.Text) == DateTime.Parse(DTPLeaveToDate.Text))
                    txtTotalDays.Text = "1";

                else if (RbtFirstHalf.Checked || RbtSecondHalf.Checked)
                {
                    DTPLeaveToDate.Text = Val.ToString(DTPLeaveFromDate.Text);
                    txtTotalDays.Text = "0.5";
                }
                else if (RbtShortLeave.Checked)
                {
                    DTPLeaveToDate.Text = Val.ToString(DTPLeaveFromDate.Text);
                   txtTotalDays.Text = "0";
                }
                else
                {
                    TimeSpan s = DateTime.Parse(DTPLeaveToDate.Text).Subtract(DateTime.Parse(DTPLeaveFromDate.Text));
                    //txtTotalDays.Text = s.TotalDays.ToString();
                    txtTotalDays.Text = Val.ToString(Val.ToInt(s.TotalDays) + 1);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void TimeLeaveFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan s = DateTime.Parse(TimeLeaveTo.Text).Subtract(DateTime.Parse(TimeLeaveFrom.Text));

                

                //txtTotalHours.Text = Val.ToString(Val.ToInt(s.Hours) * Val.Val(txtTotalDays.Text));

                txtTotalHours.Text = Val.ToString(Val.ToInt(s.Hours) * (Val.Val(txtTotalDays.Text) == 0 ? 1 : Val.Val(txtTotalDays.Text)));

                lblTotalMinutes.Text = s.Minutes.ToString();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void RbtFullDay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RbtFirstHalf.Checked )
                {
                    txtTotalDays.Text = "0.5";
                    txtTotalHours.Text = Val.ToString(Math.Round(Val.Val(txtTotalDays.Text) * DouWHours));
                    lblTotalMinutes.Text = "0";
                    
                    DTPLeaveToDate.Enabled = false;
                    DTPLeaveToDate.Text = DTPLeaveFromDate.Text;
                    TimeLeaveFrom.Enabled = false;
                    TimeLeaveTo.Enabled = false;
                }
                else if (RbtSecondHalf.Checked)
                {
                    txtTotalDays.Text = "0.5";
                    txtTotalHours.Text = Val.ToString(Math.Round(Val.Val(txtTotalDays.Text) * DouWHours));
                    lblTotalMinutes.Text = "0";

                    DTPLeaveToDate.Enabled = false;
                    DTPLeaveToDate.Text = DTPLeaveFromDate.Text;
                    TimeLeaveFrom.Enabled = false;
                    TimeLeaveTo.Enabled = false;
                }
                else if (RbtShortLeave.Checked)
                {
                    txtTotalDays.Text = "0";
                    
                    txtTotalHours.Text = "0";

                    DTPLeaveFromDate.Enabled = true;
                    DTPLeaveToDate.Enabled = false;
                    DTPLeaveToDate.Text = DTPLeaveFromDate.Text;
                    TimeLeaveFrom.Enabled = true;
                    TimeLeaveTo.Enabled = true;
                }
                else
                {
                    DTPLeaveFromDate.Enabled = true;
                    DTPLeaveToDate.Enabled = true;
                    TimeLeaveFrom.Enabled = false;
                    TimeLeaveTo.Enabled = false;
                    txtTotalDays.Text = "1";

                    txtTotalHours.Text = "0";
                    lblTotalMinutes.Text = "0";
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtReason_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "REASON_ID,REASONNAME,REASONCODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_REASON);
                    FrmSearch.mColumnsToHide = "REASON_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        
                        txtReason.Tag = Val.ToString(FrmSearch.mDRow["REASON_ID"]);
                        txtReason.Text = Val.ToString(FrmSearch.mDRow["REASONNAME"]);
                    }
                    else
                    {
                        txtReason.Tag = string.Empty;
                        txtReason.Text = string.Empty;
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

        private void txtOtherReason_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "OTHERREASON";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "OTHERREASON";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_OTHERREASON);
                    //FrmSearch.ColumnsToHide = "REASON_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtOtherReason.Text = Val.ToString(FrmSearch.mDRow["OTHERREASON"]);
                    }
                    else
                    {
                        txtOtherReason.Text = string.Empty;
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

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                
                if (e.Clicks == 2)
                {
                    DataRow Dr = GrdDet.GetFocusedDataRow();


                    if (Val.ToString(Dr["LEAVETYPE"]).ToUpper() == "SHORT LEAVE")
                    {
                        RbtShortLeave.Checked = true;
                    }
                    else if (Val.ToString(Dr["LEAVETYPE"]).ToUpper() == "FIRST HALF")
                    {
                        RbtFirstHalf.Checked = true;
                    }
                    else if (Val.ToString(Dr["LEAVETYPE"]).ToUpper() == "SECOND HALF")
                    {
                        RbtSecondHalf.Checked = true;
                    }
                    else
                    {
                        RbtFullDay.Checked = true;
                    }
                    txtSlipNo.Tag = Val.ToString(Dr["LEAVE_ID"]);
                    txtSlipNo.Text = Val.ToString(Dr["SLIPNO"]);
                    DTPSlipDate.Text = Val.ToString(Dr["SLIPDATE"]);
                    txtEmployeeCode.Tag = Val.ToString(Dr["EMPLOYEE_ID"]);
                    txtEmployeeCode.Text = Val.ToString(Dr["EMPLOYEECODE"]);
                    txtEmployeeName.Text = Val.ToString(Dr["EMPLOYEENAME"]);

                    txtReason.Tag = Val.ToString(Dr["REASON_ID"]);
                    txtReason.Text = Val.ToString(Dr["REASON"]).ToUpper();
                    txtOtherReason.Text = Val.ToString(Dr["OTHERREASON"]).ToUpper();
                    txtRemark.Text = Val.ToString(Dr["REMARK"]).ToUpper();

                    DTPLeaveFromDate.Text = Val.ToString(Dr["LEAVEFROMDATE"]);
                    DTPLeaveToDate.Text = Val.ToString(Dr["LEAVETODATE"]);
                    txtTotalDays.Text = Val.ToString(Dr["TOTALDAYS"]);

                    TimeLeaveFrom.Text = Val.ToString(Dr["LEAVEFROMTIME"]);
                    TimeLeaveTo.Text = Val.ToString(Dr["LEAVETOTIME"]);
                   // txtTotalHours.Text = Val.ToString(Dr["TOTALHOURS"]);
                    txtTotalHours.Text = Val.ToString(Dr["HH"]);
                    lblTotalMinutes.Text = Val.ToString(Dr["MM"]);

                    ChkISOfficeWork.Checked = Val.ToBoolean(Dr["ISOFFICEWORK"]);

                    CmbLeaveStatus.Text = Val.ToString(Dr["LEAVESTATUS"]).ToUpper();
                    txtComment.Text = Val.ToString(Dr["COMMENT"]);
                    txtStatusUpdateDate.Text = Val.ToString(Dr["STATUSUPDATEDATE"]);
                    txtStatusUpdateBy.Tag = Val.ToString(Dr["STATUSUPDATEBY"]);
                    txtStatusUpdateBy.Text = Val.ToString(Dr["STATUSUPDATEBYCODE"]);

                    string StrCurrenctDate = DateTime.Now.ToString("dd/MM/yyyy");

                    if (Val.ToString(CmbLeaveStatus.Text) == "APPROVED" && StrCurrenctDate == DateTime.Parse(DTPLeaveFromDate.Text).ToString("dd/MM/yyyy"))
                        BtnSlipPrint.Enabled = true;
                    else
                        BtnSlipPrint.Enabled = false;

                    byte[] EmpProfile = Dr["PROFILEIMAGE"] as byte[] ?? null;
                    if (EmpProfile != null)
                    {
                        using (MemoryStream ms = new MemoryStream(EmpProfile))
                        {
                            PicEmpPhoto.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        PicEmpPhoto.Image = null;
                    }

                    txtEmployeeCode.Focus();

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string StrEmployee_Id;
            string StrStatus;

            StrEmployee_Id = Val.Trim(CmbEmployee.Properties.GetCheckedItems());
            StrStatus = Val.Trim(ChkCmbLeaveStatus.Properties.GetCheckedItems());
            DtabSearch = objLeave.Fill(Val.SqlDate(DTPSearchFromDate.Text), Val.SqlDate(DTPSearchToDate.Text), StrEmployee_Id, StrStatus,Guid.Empty);

            //if (DTabSearch.Rows.Count <= 0)
            //{
            //    Global.Message("No Dtat Found");
            //}
            MainGrid.DataSource = DtabSearch;
            MainGrid.RefreshDataSource();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (DtabSearch.Rows.Count <= 0)
            {
                Global.Message("No Data  Found");
                return;
            }
            Global.ExcelExport("Leave Details", GrdDet);
        }

        private void BtnSlipPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DtLeaveDetailPrint = new BOTRN_LeaveEntry().Fill("", "", "", "", Guid.Parse(Val.ToString(txtSlipNo.Tag)));
                //}

                Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();

                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowForm("LeaveSlipPrint", DtLeaveDetailPrint);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }



    }
}
