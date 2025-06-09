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
    public partial class FrmInputBox : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable DTab;

        public string ColumnsToHide = "";

        public bool AllowFirstColumnHide = false;

        public string StrInoutText = "";

         AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        public DataRow DRow { get; set; }

        public FrmInputBox()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            txtMessage.Text = new BOMST_FormPermission().GetMessage();
            AttachFormDefaultEvent();
            Val.FormGeneralSetting(this);         
            this.Show();             
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

        private void txtSeach_TextChanged(object sender, EventArgs e)
        {
            StrInoutText = txtMessage.Text;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            int IntRes = new BOMST_FormPermission().SaveMessage(txtMessage.Text);
            if (IntRes != -1)
            {
                Global.Message("MESSAGE SAVED");
                Global.gStrSuvichar = txtMessage.Text;
                if (Global.gStrSuvichar.Trim() == "")
                {
                    Global.gStrSuvichar = "!! WELCOM " + Global.gStrCompanyName + " !!";
                }
            }
        }

        private void FrmInputBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            StrInoutText = txtMessage.Text;
        }


    }
}