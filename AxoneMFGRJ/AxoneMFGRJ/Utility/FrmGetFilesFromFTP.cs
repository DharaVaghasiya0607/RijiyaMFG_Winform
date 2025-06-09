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
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Security.Principal;
using AxoneMFGRJ.Utility;


namespace AxoneMFGRJ
{
    public partial class FrmGetFilesFromFTP : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable DTab;

        public string ColumnsToHide = "";

        public bool AllowFirstColumnHide = false;

        public string StrInoutText = "";

         AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();

        public DataRow DRow { get; set; }

        public FrmGetFilesFromFTP()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
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


        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //File.Copy(@"D:\Testing\Page.xlsx", @"\\219.91.168.146\Page.xlsx", true);

                //TcpListener filelistener = new TcpListener(System.Net.IPAddress.Parse("219.91.168.146"), 8085);
                //filelistener.Start();
                //TcpClient client = filelistener.AcceptTcpClient();
                //Global.Message("Client connection accepted from :" + client.Client.RemoteEndPoint + ".");
                //byte[] buffer = new byte[1500];
                //int bytesread = 1;

                //StreamWriter writer = new StreamWriter("J:\\Test\\GIAGrading.rar");

                //while (bytesread > 0)
                //{
                //    bytesread = client.GetStream().Read(buffer, 0, buffer.Length);
                //    if (bytesread == 0)
                //        break;
                //    writer.BaseStream.Write(buffer, 0, buffer.Length);
                //    Global.Message((bytesread + " Received. "));
                //}
                //writer.Close();
                //--------------------------------------------------------------------------------------------
                //IntPtr admin_token = default(IntPtr);
                //WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
                //WindowsIdentity wid_admin = null;
                //WindowsImpersonationContext wic = null;

                //if (LogonUser("LocalUsername", "LocalDomain", "LocalPass", 9, 0, ref admin_token) != 0)
                //{
                //    wid_admin = new WindowsIdentity(admin_token);
                //    wic = wid_admin.Impersonate();
                //    System.IO.File.Copy("C:\\test\\copy.txt", "\\\\Server2\\test\\copy.txt", true);
                //    Console.WriteLine("Copy succeeded");
                //}
                //else
                //{
                //    Console.WriteLine("Copy Failed");
                //}

                //----------------------------------------------------------------------------------------------------

                var fileName = "Page.xlsx";
                string local = Path.Combine(@"D:\Testing", fileName);
                string  remote = Path.Combine(@"\\219.91.168.146\Test\", fileName);

                	
                //string Username = "administrator";
                //string Password = "admin@789";

                //NetworkCredential theNetworkCredential = new NetworkCredential(Username, Password,"");
                //CredentialCache theNetcache = new CredentialCache();
                //theNetcache.Add("",3389,"",theNetworkCredential);
                ////theNetcache.Add(@"\\computer", theNetworkCredential, "Basic", theNetworkCredential);
                ////then do whatever, such as getting a list of folders:
                //string[] theFolders = System.IO.Directory.GetDirectories("@\\computer\share");

 

                //WebClient request = new WebClient();
                //request.Credentials = new NetworkCredential(@"administrator", "admin@789");

                if (File.Exists(local))
                {
                    File.Delete(local);

                    File.Copy(remote, local, true);
                }
                else
                {
                    File.Copy(remote, local, true);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        //public string GetIP()
        //{
        //    string name = Dns.GetHostName();
        //    IPHostEntry entry = Dns.GetHostEntry(name);
        //    IPAddress[] addr = entry.AddressList;
        //    if (addr[1].ToString().Split('.').Length == 4)
        //    {
        //        return addr[1].ToString();
        //    }
        //    return addr[2].ToString();
        //}

    }
}