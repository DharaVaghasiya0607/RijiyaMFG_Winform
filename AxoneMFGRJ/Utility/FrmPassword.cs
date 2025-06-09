using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AxoneMFGRJ.Utility
{
    public partial class FrmPassword : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        System.Windows.Forms.DialogResult mDialog;

        string mStrPassword = ""; 

        public FrmPassword()
        {
            InitializeComponent();
        }

        public DialogResult ShowForm(string pFromPassword)
        {
            mStrPassword = pFromPassword;
            txtPassword.Tag = mStrPassword;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.ShowDialog();
            txtPassword.Focus();
            return mDialog;
        }
        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;

            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = false;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            //(Val.Trim(txtPassword.Text) != "" && txtPassword.Tag != "" && txtPassword.Text.ToUpper() == txtPassword.Tag.ToUpper())
            if ((Val.ToString(txtPassword.Tag) != "" && Val.ToString(txtPassword.Tag).ToUpper() == txtPassword.Text.ToUpper())) 
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                mDialog = this.DialogResult;
                this.Close();
            }
            else
            {
                //(Val.Trim(txtPassword.Text) != "" && mStrPassword != "" && txtPassword.Text != mStrPassword)
                if (((Val.ToString(txtPassword.Text) == "") || (Val.ToString(txtPassword.Tag).ToUpper() == txtPassword.Text.ToUpper()))) 
                {
                    Global.Message("Your Password is not Valid");
                }
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                mDialog = this.DialogResult;
                this.Close();
            }
        }

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            mDialog = this.DialogResult;
            this.Close();
        }

        private void FrmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                mDialog = this.DialogResult;
                this.Close();
            }
        }

        private void FrmPassword_Load(object sender, EventArgs e)
        {

        }

    }
}