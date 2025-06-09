using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;
using System.Collections;
using BusLib.Master;


namespace AxoneMFGRJ
{
    public partial class FrmDepartmentIssueReturnLock : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable DTab;

        public string ColumnsToHide = "";

        public bool AllowFirstColumnHide = false;

        public string IsTransactionLock = "";



         AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        public DataRow DRow { get; set; }

        public FrmDepartmentIssueReturnLock()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            AttachFormDefaultEvent();
            Val.FormGeneralSetting(this);         
            this.Show();

            IsTransactionLock  = new BOMST_FormPermission().GetDepartmentIssueReturnLockUnlockkValue();

            if (IsTransactionLock == "YES")
            {
                lblDeptTransactionLockStatus.Text = "Locked";
                lblDeptTransactionLockStatus.ForeColor = Color.DarkRed;
                BtnDeptIssueReturnLock.Enabled = false;
                BtnDeptIssueReturnUnLock.Enabled = true;
            }
            else
            {
                lblDeptTransactionLockStatus.Text = "UnLocked";
                lblDeptTransactionLockStatus.ForeColor = Color.DarkGreen;
                BtnDeptIssueReturnLock.Enabled = true;
                BtnDeptIssueReturnUnLock.Enabled = false;
            }

        }
        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.FormKeyPress = false;

            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);

        }
      
        private void FrmSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                this.Close();
            }            
        }


        private void BtnDeptIssueReturnUnLock_Click(object sender, EventArgs e)
        {
            int IntRes = new BOMST_FormPermission().SaveDepartmentIssueReturnLockUnlock("NO");
            if (IntRes != -1)
            {
                Global.Message("Department Issue/Return Transaction Unlock Successfully.");
                lblDeptTransactionLockStatus.Text = "UnLocked";
                lblDeptTransactionLockStatus.ForeColor = Color.DarkGreen;
                BtnDeptIssueReturnUnLock.Enabled = false;
                BtnDeptIssueReturnLock.Enabled = true;
            }
        }

        private void BtnDeptIssueReturnLock_Click(object sender, EventArgs e)
        {
            int IntRes = new BOMST_FormPermission().SaveDepartmentIssueReturnLockUnlock("YES");
            if (IntRes != -1)
            {
                Global.Message("Department Issue/Return Transaction Lock Successfully.");
                lblDeptTransactionLockStatus.Text = "Locked";
                lblDeptTransactionLockStatus.ForeColor = Color.DarkRed;
                BtnDeptIssueReturnLock.Enabled = false;
                BtnDeptIssueReturnUnLock.Enabled = true;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {

        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {

        }

       
    }
}