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
    public partial class FrmTransactionLock : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable DTab;

        public string ColumnsToHide = "";

        public bool AllowFirstColumnHide = false;

        public string IsTransactionLock = "";



         AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        public DataRow DRow { get; set; }

        public FrmTransactionLock()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            try
            {

                AttachFormDefaultEvent();
                Val.FormGeneralSetting(this);
                this.Show();

                IsTransactionLock = new BOMST_FormPermission().GetTransactionLockUnlockValue();

                if (IsTransactionLock == "YES")
                {
                    lblTransactionLockStatus.Text = "Locked";
                    lblTransactionLockStatus.ForeColor = Color.DarkRed;
                    BtnTransactionLock.Enabled = false;
                    BtnTransactionUnlock.Enabled = true;
                }
                else
                {
                    lblTransactionLockStatus.Text = "UnLocked";
                    lblTransactionLockStatus.ForeColor = Color.DarkGreen;
                    BtnTransactionLock.Enabled = true;
                    BtnTransactionUnlock.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
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


        private void BtnTransactionUnLock_Click(object sender, EventArgs e)
        {
            int IntRes = new BOMST_FormPermission().SaveTransactionLockUnlock("NO");
            if (IntRes != -1)
            {
                Global.Message("Transaction Unlock Successfully.");
                lblTransactionLockStatus.Text = "UnLocked";
                lblTransactionLockStatus.ForeColor = Color.DarkGreen;
                BtnTransactionUnlock.Enabled = false;
                BtnTransactionLock.Enabled = true;
            }
        }

        private void BtnTransactionLock_Click(object sender, EventArgs e)
        {
            int IntRes = new BOMST_FormPermission().SaveTransactionLockUnlock("YES");
            if (IntRes != -1)
            {
                Global.Message("Transaction Lock Successfully.");
                lblTransactionLockStatus.Text = "Locked";
                lblTransactionLockStatus.ForeColor = Color.DarkRed;
                BtnTransactionLock.Enabled = false;
                BtnTransactionUnlock.Enabled = true;
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