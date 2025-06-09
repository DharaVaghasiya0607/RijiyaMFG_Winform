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

namespace AxoneMFGRJ.Attendance
{
    public partial class FrmLeaveApproval : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Attendance ObjMast = new BOMST_Attendance();

        DataTable DtabAtd = new DataTable();

        Guid gMainLeave_ID;

        double DouWHours = 0;

        #region Property Settings

        public FrmLeaveApproval()
        {
            InitializeComponent();
        }

        public void ShowForm(string StrLeave_ID)
        {

            //Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            //ObjPer.GetFormPermission(Val.ToString(this.Tag));
            //BtnApproved.Enabled = ObjPer.ISINSERT;
 
            gMainLeave_ID = Val.ToString(StrLeave_ID).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(StrLeave_ID);

            DataTable DtApprove = new BOTRN_LeaveEntry().Fill("", "", "", "", gMainLeave_ID);

            if (DtApprove.Rows.Count > 0)
            { 
                DataRow DR = DtApprove.Rows[0];
                txtSlipNo.Text = Val.ToString(DR["SLIPNO"]);
                DTPSlipDate.Text = Val.ToString(DR["SLIPDATE"]);

                txtEmployeeCode.Tag = Val.ToString(DR["EMPLOYEE_ID"]);
                txtEmployeeCode.Text = Val.ToString(DR["EMPLOYEECODE"]);
                txtEmployeeName.Text = Val.ToString(DR["EMPLOYEENAME"]);
                txtReason.Text = Val.ToString(DR["REASON"]);
                txtOtherReason.Text = Val.ToString(DR["OTHERREASON"]);
                txtRemark.Text = Val.ToString(DR["REMARK"]);

                if (Val.ToString(DR["LEAVETYPE"]) == "FULL DAY")
                    txtLeaveDate.Text = "From :" + Convert.ToDateTime(Val.ToString(DR["LEAVEFROMDATE"])).ToString("dd/MM/yyyy").ToString() + " TO : " + Convert.ToDateTime(Val.ToString(DR["LEAVETODATE"])).ToString("dd/MM/yyyy").ToString() + " (" + Val.ToString(DR["LEAVETYPE"]) + ")";
                else
                    txtLeaveDate.Text = "On : " + Convert.ToDateTime(Val.ToString(DR["LEAVEFROMDATE"])).ToString("dd/MM/yyyy").ToString() + " (" + Val.ToString(DR["LEAVETYPE"]) + ")";

                txtLeaveDays.Text = "Total Days : " + Val.ToString(DR["TOTALDAYS"]) + " Total Hours : " + Val.ToString(DR["TOTALHOURS"]);
                ChkISOfficeWork.Checked = Val.ToBoolean(DR["ISOFFICEWORK"]);

                lblEntryBy.Text = Val.ToString(DR["ENTRYBYCODE"]);
                lblEntryDate.Text = Val.ToString(DR["ENTRYDATE"]);
                lblEntryIP.Text = Val.ToString(DR["ENTRYIP"]);

                byte[] EmpProfile = DR["PROFILEIMAGE"] as byte[] ?? null;
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

                txtComment.Focus();
            }


            this.ShowDialog();

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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnApproved_Click(object sender, EventArgs e)
        {
            try
            {

                //add : Bhagyashree : 25-07-2019
                Int64 EMP_ID;

                EMP_ID = Val.ToInt64(txtEmployeeCode.Tag);

                int I = new BOTRN_LeaveEntry().CheckValidationForLiveApproval(EMP_ID);

                if (I == -1)
                {
                    Global.MessageError("You Can't Approve Because Packets Are Already Exits In '" + txtEmployeeCode.Text + "' Please Check.");
                    return;
                }
                //End : Bhagyashree : 25-07-2019

                LeaveEntryProperty Property = new LeaveEntryProperty();

                Property.COMMENT = txtComment.Text;
                Property.LEAVESTATUS = "APPROVED";
                Property.STATUSUPDATEDATE = Val.ToString(DateTime.Now);
                Property.LEAVE_ID = gMainLeave_ID;

                int IntRes = new BOTRN_LeaveEntry().UpdatePendingLeaveStatus(Property);

                if (IntRes >= 1)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtComment.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Comment Is Required");
                    txtComment.Focus();
                    return;
                }

                LeaveEntryProperty Property = new LeaveEntryProperty();

                Property.COMMENT = txtComment.Text;
                Property.LEAVESTATUS = "CANCELED";
                Property.STATUSUPDATEDATE = Val.ToString(DateTime.Now);
                Property.LEAVE_ID = gMainLeave_ID;

                int IntRes = new BOTRN_LeaveEntry().UpdatePendingLeaveStatus(Property);

                if (IntRes >= 1)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }



    }
}
