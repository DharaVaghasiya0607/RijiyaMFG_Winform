using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusLib.TableName;
using BusLib.Configuration;
using AxoneMFGRJ.MDI;
using BusLib.Master;
using System.Deployment.Application;
using System.Configuration;
using System.IO;
using System.Xml;
namespace AxoneMFGRJ.Utility
{
    public partial class FrmChangePassword : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        #region Constructor

        public FrmChangePassword()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            try
            {
                lblCompanyName.Text = System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString();
                txtOldPassWord.Text = System.Configuration.ConfigurationManager.AppSettings["USERNAME"].ToString();
                txtnewPassWord.Text = System.Configuration.ConfigurationManager.AppSettings["PASSWORD"].ToString();

            }
            catch (Exception EX)
            {

            }


            this.ShowDialog();
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


        #endregion


        #region Events

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtOldPassWord.Text.Length == 0)
                {
                    Global.Message("Old PassWord Is Required");
                    txtOldPassWord.Focus();
                    return;
                }
                if (txtnewPassWord.Text.Length == 0)
                {
                    Global.Message("New PassWord Is Required");
                    txtnewPassWord.Focus();
                    return;
                }

                BOMST_Ledger Objchange = new BOMST_Ledger();

                string Str = Objchange.ChangePassWord(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, txtOldPassWord.Text, txtnewPassWord.Text);
                this.Cursor = Cursors.Default;
                Global.Message(Str);
                if (Str.Contains("SUCCESS"))
                {
                    foreach (System.Windows.Forms.Form frm in Global.gMainRef.MdiChildren)
                    {
                        frm.Dispose();
                        frm.Close();
                    }

                    Application.Restart();
                }
                else
                {
                    txtOldPassWord.Focus();
                }

                
            }

            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());


        #endregion
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
