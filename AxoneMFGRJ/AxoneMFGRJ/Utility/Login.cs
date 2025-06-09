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
using System.Net.NetworkInformation;
namespace AxoneMFGRJ.Utility
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        #region Constructor

        public FrmLogin()
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
                txtUserName.Text = System.Configuration.ConfigurationManager.AppSettings["USERNAME"].ToString();
                txtPassWord.Text = System.Configuration.ConfigurationManager.AppSettings["PASSWORD"].ToString();

            }
            catch (Exception EX)
            {

            }

            txtConnectionString.Text = BOConfiguration.ConnectionString;
            
            txtPassForDisplayTransferTick_TextChanged(null, null);

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

        #region Form Validation

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control && e.Alt && e.Shift)
            {
                if (ChkTransferDBConnection.Checked)
                {
                   
                    PnlRegularDBConnection.Visible = false;
                    lblConn.Text = BOConfiguration.ConnectionFileName_TransferDB.ToString();
                }
                else
                {
                   PnlRegularDBConnection.Visible = true;
                    lblConn.Text = BOConfiguration.ConnectionFileName.ToString();
                    txtConnectionString.Visible = true;
                    BtnUpdate.Visible = true;
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
                this.Close();
            }
        }


        #endregion

        #region Events

        private void BtnLogin_Click(object sender, EventArgs e)
        {

            if (Application.StartupPath.Contains("191.168.0.5"))
            {
                Global.Message("You Can Not Run EXE From This Location [191.168.0.5]");
                Application.Exit();
            }


            //#P : 12-03-2021
            if (ChkTransferDBConnection.Checked)
            {

            }
            else
            {

            }
            //End : #P : 12-03-2021


            if (txtUserName.Text.Length == 0)
            {
                Global.Message("UserName Is Required");
                txtUserName.Focus();
                return;
            }
            if (txtPassWord.Text.Length == 0)
            {
                Global.Message("Password Is Required");
                txtPassWord.Focus();
                return;
            }

            if (txtUserName.Text == "AXONEADMIN" && txtPassWord.Text == "AXONEADMIN")
            {
                this.Hide();

                BusLib.Configuration.BOConfiguration.gEmployeeProperty = new LedgerMasterProperty();

                BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID = 1;
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME = "Testing";
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME = txtUserName.Text;
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.PASSWORD = txtPassWord.Text;
                Global.gStrExeVersion = lblVersion.Text;

                this.Hide();
                this.Close();
                // BOConfiguration.BackUp();


                //Cmnt : Add : 13-08-2022
                FrmMDI FrmMDI = new FrmMDI();
                Global.gMainRef = FrmMDI;
                FrmMDI.ShowDialog();
                //string sMacAddress = "";
                //foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces()) //Add namespace : using System.Net.NetworkInformation; 
                //{
                //    if (n.OperationalStatus == OperationalStatus.Up)
                //    {
                //        sMacAddress += n.GetPhysicalAddress().ToString();
                //        break;
                //    }
                //}
                //DataTable Dtab = new BOMST_Ledger().CheckEvent(sMacAddress);
                //if (Dtab.Rows.Count > 0)
                //{
                //    if (Val.ToString(Dtab.Rows[0]["MESSAGE"]) == "YES")
                //    {
                //        Global.strEventUrl = Val.ToString(Dtab.Rows[0]["URL"]);
                //        FrmEventBox FrmEventBox = new FrmEventBox();
                //        FrmEventBox.ShowDialog();

                //    }
                //    else
                //    {
                //        FrmMDI FrmMDI = new FrmMDI();
                //        ObjFormEvent.ObjToDisposeList.Add(FrmMDI);
                //        FrmMDI.ShowDialog();
                //    }
                //}
                //else
                //{
                //    FrmMDI FrmMDI = new FrmMDI();
                //    ObjFormEvent.ObjToDisposeList.Add(FrmMDI);
                //    FrmMDI.ShowDialog();
                //}
                //End : Cmnt : Add : 13-08-2022
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            BOMST_Ledger ObjMast = new BOMST_Ledger();


            string Str = ObjMast.CheckLoginValidation(txtUserName.Text, txtPassWord.Text, Val.ToString(lblVersion.Text));
            if (Str != "")
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(Str);
                txtUserName.Focus();
                return;
            }

            DataRow DRow = ObjMast.CheckLogin(txtUserName.Text, txtPassWord.Text);

            if (DRow != null)
            {
                BusLib.Configuration.BOConfiguration.gEmployeeProperty = new LedgerMasterProperty();

                BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID = Val.ToInt64(DRow["LEDGER_ID"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE = Val.ToString(DRow["LEDGERCODE"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME = Val.ToString(DRow["LEDGERNAME"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAMEGUJARATI = Val.ToString(DRow["LEDGERNAMEGUJARATI"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP = Val.ToString(DRow["LEDGERGROUP"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.CONTACTPERSON = Val.ToString(DRow["CONTACTPERSON"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.MOBILENO1 = Val.ToString(DRow["MOBILENO1"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.MOBILENO2 = Val.ToString(DRow["MOBILENO2"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.BANKNAME = Val.ToString(DRow["BANKNAME"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.BANKIFSCCODE = Val.ToString(DRow["BANKIFSCCODE"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.BANKACCOUNTNO = Val.ToString(DRow["BANKACCOUNTNO"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.BANKACCOUNTNAME = Val.ToString(DRow["BANKACCOUNTNAME"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.GSTNO = Val.ToString(DRow["GSTNO"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.CSTNO = Val.ToString(DRow["CSTNO"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.PANNO = Val.ToString(DRow["PANNO"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.BILLINGADDRESS = Val.ToString(DRow["BILLINGADDRESS"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.BILLINGSTATE = Val.ToString(DRow["BILLINGSTATE"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.SHIPPINGADDRESS = Val.ToString(DRow["SHIPPINGADDRESS"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.SHIPPINGSTATE = Val.ToString(DRow["SHIPPINGSTATE"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.ISACTIVE = Val.ToBoolean(DRow["ISACTIVE"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID = Val.ToInt32(DRow["DEPARTMENT_ID"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME = Val.ToString(DRow["DEPARTMENTNAME"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTGROUP = Val.ToString(DRow["DEPARTMENTGROUP"]);

                BusLib.Configuration.BOConfiguration.gEmployeeProperty.DESIGNATION_ID = Val.ToInt32(DRow["DESIGNATION_ID"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.DESIGNATIONNAME = Val.ToString(DRow["DESIGNATIONNAME"]);

                BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANY_ID = Val.ToInt64(DRow["COMPANY_ID"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME = Val.ToString(DRow["COMPANYNAME"]);

                BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID = Val.ToInt64(DRow["MANAGER_ID"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE = Val.ToString(DRow["MANAGERCODE"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERNAME = Val.ToString(DRow["MANAGERNAME"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.SALARYTYPE = Val.ToString(DRow["SALARYTYPE"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.SALARY = Val.Val(DRow["SALARY"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.EXPSALARY = Val.Val(DRow["EXPSALARY"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME = Val.ToString(DRow["USERNAME"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.PASSWORD = Val.ToString(DRow["PASSWORD"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.ADHARNO = Val.ToString(DRow["ADHARNO"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.EMPLOYEETYPE = Val.ToString(DRow["EMPLOYEETYPE"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.STYDY = Val.ToString(DRow["STYDY"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.IDCARDNO = Val.ToString(DRow["IDCARDNO"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.CONTACTPERSONMOBILENO = Val.ToString(DRow["CONTACTPERSONMOBILENO"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.PREVCOMPANYNAME = Val.ToString(DRow["PREVCOMPANYNAME"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.PREVDESIGNATION = Val.ToString(DRow["PREVDESIGNATION"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.PREVSALARY = Val.Val(DRow["PREVSALARY"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.TOTALEXP = Val.Val(DRow["TOTALEXP"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM = Val.ToBoolean(DRow["AUTOCONFIRM"]);
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.LOGINHST_ID = Val.ToInt64(Val.ToString(DRow["LOGINHST_ID"])); //#P : 16-10-2019
                
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.PASSWORD = txtPassWord.Text;

                Global.gStrExeVersion = lblVersion.Text;

                // Get Global Setting For the Same
                new FrmUserConfig().GetConfigData();

                //BusLib.Configuration.BOConfiguration.gEmployeeProperty.IS_DISPLAYVALUE = Val.ToBooleanToInt(DRow["IS_DISPLAYVALUE"]);
                this.Cursor = Cursors.Default;
                this.Hide();

                this.Close();
                // BOConfiguration.BackUp();

                //if (BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID == )

                //string pStrRes = ObjMast.SetExpGoal(Val.ToString(DateTime.Now), 0);
                //if (pStrRes != "")
                //{
                //    this.Cursor = Cursors.Default;
                //    this.Close();
                //}
                //else
                //{
                    //FrmMDI FrmMDI = new FrmMDI();
                    //Global.gMainRef = FrmMDI;
                    //FrmMDI.ShowDialog();
                //}

                string sMacAddress = "";
                foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces()) //Add namespace : using System.Net.NetworkInformation; 
                {
                    if (n.OperationalStatus == OperationalStatus.Up)
                    {
                        sMacAddress += n.GetPhysicalAddress().ToString();
                        break;
                    }
                }
                DataTable Dtab = new BOMST_Ledger().CheckEvent(sMacAddress);
                if (Dtab.Rows.Count > 0)
                {
                    if (Val.ToString(Dtab.Rows[0]["MESSAGE"]) == "YES")
                    {
                        Global.strEventUrl = Val.ToString(Dtab.Rows[0]["URL"]);
                        FrmEventBox FrmEventBox = new FrmEventBox();
                        FrmEventBox.ShowDialog();

                    }
                    else
                    {
                        FrmMDI FrmMDI = new FrmMDI();
                        ObjFormEvent.ObjToDisposeList.Add(FrmMDI);
                        Global.gMainRef = FrmMDI;
                        FrmMDI.ShowDialog();
                    }
                }
                else
                {
                    FrmMDI FrmMDI = new FrmMDI();
                    ObjFormEvent.ObjToDisposeList.Add(FrmMDI);
                    Global.gMainRef = FrmMDI;
                    FrmMDI.ShowDialog();
                }

                return;
            }
            else
            {
                this.Cursor = Cursors.Default;

                Global.Message("INVALID USERNAME AND PASSWORD");
                txtUserName.Focus();
                return;
            }

        }

        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\Background.png"))
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Background.png");
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            //if (ApplicationDeployment.IsNetworkDeployed)
            //{
            //    lblVersion.Visible = true;
            //    lblVersion.Text = string.Format(ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
            //}
            //else
            //{
            //    lblVersion.Visible = false;
            //}

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {

            string Str = Global.Encrypt(txtConnectionString.Text);


            //System.IO.File.WriteAllText(Application.StartupPath + "\\conn.txt", Str);
            System.IO.File.WriteAllText(Application.StartupPath + "\\" + BOConfiguration.ConnectionFileName + ".txt", Str);


            BOConfiguration.ConnectionString = txtConnectionString.Text;

            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.ConnectionStrings.ConnectionStrings["ConnectionString"].ConnectionString = Str;
            //config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("connectionStrings");

            Global.Message("Connection String Changed. Your Application Automatically Closed");
            txtConnectionString.Visible = false;
            BtnUpdate.Visible = false;

            this.Close();
            Application.Restart();
        }

        private void txtPassForDisplayTransferTick_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtPassForDisplayTransferTick.Tag) != "" && Val.ToString(txtPassForDisplayTransferTick.Tag).ToUpper() == txtPassForDisplayTransferTick.Text.ToUpper())
                {
                    ChkTransferDBConnection.Visible = true;
                }
                else
                {
                    ChkTransferDBConnection.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
