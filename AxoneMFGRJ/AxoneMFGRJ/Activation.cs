using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace AxoneMFGRJ
{
    public partial class Activation : Form
    {

        
        int IntActive = 0;
        string mStrPassword = "";
        Boolean mBoolKeyAllow = false;
        
        public Activation()
        {
            InitializeComponent();
            txtActivation3.Text = System.Environment.MachineName.ToString().ToUpper();
            txtRegistration3.Text = System.Environment.MachineName.ToString().ToUpper();
            IntActive = 15;
            ChkTrial.Text = "Select Me For Trial Up To " + IntActive.ToString() + " Days";
        }

        private string _RegisterKey;

        public string RegisterKey
        {
            get { return _RegisterKey; }
            set { _RegisterKey = value; }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkTrial.Checked == true)
                {
                    Microsoft.Win32.RegistryKey EncryptedKey;

                    try
                    {
                        Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(@"Software\" + RegisterKey);
                    }
                    catch { }

                    EncryptedKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\" + RegisterKey);
                    EncryptedKey.Close();

                    EncryptedKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\" + RegisterKey, true);

                    string StartDate = DateTime.Now.ToString("dd-MMM-yyyy");
                    string EndDate = DateTime.Now.AddDays(15).ToString("dd-MMM-yyyy");
                   
                    EncryptedKey.SetValue("Type", "TRIAL");
                    EncryptedKey.SetValue("FDATE", StartDate);
                    EncryptedKey.SetValue("TDATE", EndDate);
                    
                    EncryptedKey.SetValue("COUNTER", "1");

                    EncryptedKey.Close();

                    Global.Message("Trial Version Activation For " + IntActive.ToString() + " Days Done Successfully");
                    Global.Message("Re Open the Application And Enjoy !!!!!");
                    Application.Exit();
                }
                else
                {
                    
                    string StrActivation = txtActivation1.Text + txtActivation3.Text;
                    string StrRegistration = txtRegistration1.Text + txtRegistration3.Text;

                    if (StrActivation.ToUpper() != "RKISHAN@JAYHO" + System.Environment.MachineName.ToString().ToUpper())
                    {
                        Global.Message("Invalid Activation Key");
                        txtActivation1.Focus();
                        return;
                    }

                    if (StrRegistration.ToUpper() != "RKISHAN@JAYHO" + System.Environment.MachineName.ToString().ToUpper())
                    {
                        Global.Message("Invalid Registration Key");
                        txtRegistration1.Focus();
                        return;
                    }

                    Microsoft.Win32.RegistryKey EncryptedKey;

                    try
                    {
                        Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(@"Software\" + RegisterKey);
                    }
                    catch { }

                    EncryptedKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\" + RegisterKey);
                    EncryptedKey.Close();

                    EncryptedKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\" + RegisterKey, true);

                    String StrNumAct = "";
                    foreach (char c in StrActivation.ToCharArray())
                    {
                        StrNumAct = StrNumAct + ((int)c).ToString();
                    }

                    String StrNumReg = "";
                    foreach (char c in StrRegistration.ToCharArray())
                    {
                        StrNumReg = StrNumReg + ((int)c).ToString();
                    }
                    EncryptedKey.SetValue("Type", "OK");
                    EncryptedKey.SetValue("Activation", StrNumAct);
                    EncryptedKey.SetValue("Registration", StrNumReg);
                    EncryptedKey.Close();

                    Global.Message("Software Activation Done Successfully");
                    Global.Message("Re Open the Application And Enjoy !!!!!");
                    Application.Exit();

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        public int DateDiff(string pStrFDate, string pStrTDate)
        {
            System.DateTime FDate, TDate;
            try
            {
                FDate = DateTime.Parse(pStrFDate);
                TDate = DateTime.Parse(pStrTDate);
                return Convert.ToInt32(DateAndTime.DateDiff(DateInterval.Day, FDate, TDate, FirstDayOfWeek.Monday, FirstWeekOfYear.System));
            }
            catch
            {
                return 0;
            }
        }

        public Boolean CheckActivation(string RegisterKey)
        {
            try
            {
                Microsoft.Win32.RegistryKey EncryptedKey;
                EncryptedKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\" + RegisterKey, true);

                String StrType = EncryptedKey.GetValue("Type").ToString();

                if (StrType.ToString().ToUpper().Equals("TRIAL"))
                {
                    DateTime StrFDate = DateTime.Parse(EncryptedKey.GetValue("FDATE").ToString());
                    DateTime StrTDate = DateTime.Parse(EncryptedKey.GetValue("TDATE").ToString());
                    String StrCounter = EncryptedKey.GetValue("COUNTER").ToString();

                    string TodayDate = DateTime.Now.ToString("dd-MMM-yyyy");

                    int IntCounter = Convert.ToInt32(StrCounter);
                    
                    if (IntCounter > 15)
                    {
                        Global.Message("Sorry....Your Trial Licence Is Expired");
                        Global.Message("Contact To Administrator");
                        return false;
                    }
                    else
                    {
                        TimeSpan ss = StrTDate.Subtract(DateTime.Now);

                        if (StrTDate.ToString("dd-MMM-yyyy") != TodayDate)
                        {

                            IntCounter++;

                            EncryptedKey.SetValue("Type", "TRIAL");
                            EncryptedKey.SetValue("FDATE", StrFDate.ToString("dd-MMM-yyyy"));
                            EncryptedKey.SetValue("TDATE", TodayDate);

                            EncryptedKey.SetValue("COUNTER", IntCounter);

                            EncryptedKey.Close();

                        }
                        if (15- IntCounter <= 5)
                        {
                            Global.Message("You have " + Convert.ToString(15 - IntCounter) + " Days Remaning For Activation");
                        }
                        return true;
                    }
                }
                else
                {
                    String StrActRetrieve = EncryptedKey.GetValue("Activation").ToString();
                    String StrRegRetrieve = EncryptedKey.GetValue("Registration").ToString();

                    String StrAct = "RKISHAN@JAYHO" + System.Environment.MachineName.ToString().ToUpper();
                    String StrNumAct = "";
                    foreach (char c in StrAct.ToCharArray())
                    {
                        StrNumAct = StrNumAct + ((int)c).ToString();
                    }

                    String StrReg = "RKISHAN@JAYHO" + System.Environment.MachineName.ToString().ToUpper();
                    String StrNumReg = "";

                    EncryptedKey.Close();

                    foreach (char c in StrReg.ToCharArray())
                    {
                        StrNumReg = StrNumReg + ((int)c).ToString();
                    }

                    if ((StrActRetrieve == StrNumAct && StrNumAct.Length != 0) && (StrRegRetrieve == StrNumReg && StrNumReg.Length != 0))
                    {
                        return true;
                    }
                    else
                    {                        
                        OpenActivationForm(RegisterKey);
                        return false;
                    }
                }

            }
            catch (Exception)
            {                
                OpenActivationForm(RegisterKey);
                return false;
            }
            
        }

        public void OpenActivationForm(string RegisterKey)
        {
            Global.Message("Sorry !!!! Software Activation Is Required");
            this.ShowDialog();
        }

        public void DeleteActivation(string RegisterKey)
        {
            try
            {

                try
                {
                    Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(@"Software\" + RegisterKey);
                }
                catch { }

                Global.Message("Key Deleted Successfully");
                Application.Exit();
            }
            catch
            {
                Global.Message("Key Not Found");
            }
        }


        private void Activation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)(27))
            {
                this.Close();
                this.Hide();
                this.Dispose();                
                Application.Exit();
            }
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void Activation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }            
        }

        private void Activation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text.ToUpper() == txtPass.Tag.ToString().ToUpper())
            {
                BtnDelete.Enabled = true;
                //BtnSubmit.Enabled = true;
                //ChkTrial.Enabled = true;
                //txtActivation1.Enabled = true;
                //txtActivation2.Enabled = true;
                //txtActivation3.Enabled = true;
                //txtRegistration1.Enabled = true;
                //txtRegistration2.Enabled = true;
                //txtRegistration3.Enabled = true;
            }
            else
            {
                BtnDelete.Enabled = false;
                //BtnSubmit.Enabled = false;
                //ChkTrial.Enabled = false;
                //txtActivation1.Enabled = false;
                //txtActivation2.Enabled = false;
                //txtActivation3.Enabled = false;
                //txtRegistration1.Enabled = false;
                //txtRegistration2.Enabled = false;
                //txtRegistration3.Enabled = false;
            }

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteActivation(RegisterKey);
        }

    }
}
