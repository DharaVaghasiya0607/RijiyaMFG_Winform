using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.Transaction;
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
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using BusLib.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using BusLib.Rapaport;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmFileTransfer : DevExpress.XtraEditors.XtraForm
    {
        [DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        BOFindRap ObjRap = new BOFindRap();
        BOTRN_FileTransfer ObjFile = new BOTRN_FileTransfer();

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        Color mSelectedColor = Color.FromArgb(192, 0, 0);
        Color mDeSelectColor = Color.Black;
        Color mSelectedBackColor = Color.FromArgb(255, 224, 192);
        Color mDSelectedBackColor = Color.WhiteSmoke;

        string mStrFileTransferUsername = "";
        string mStrFileTransferPassword = "";

        #region Property Settings

        public FrmFileTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSettingForPopup(this);

            AttachFormDefaultEvent();
            txtKapanName.Focus();

            DataTable DTabProcess = new BOComboFill().FillCmb(BOComboFill.TABLE.FILE_TRANSFER_TYPE);

            mStrFileTransferUsername = new BOMST_FormPermission().GetFileTransferUsername();
            mStrFileTransferPassword = new BOMST_FormPermission().GetFileTransferPassword();

            PanelFileProcess.Controls.Clear();
            int IntI = 0;

            string StrDeptGrp = Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTGROUP);

            foreach (DataRow DRow in DTabProcess.Rows)
            {

                if (StrDeptGrp == "MFG" && Val.ToString(DRow["PARANAME"]) != "FINAL MK")
                    continue;

                AxonContLib.cRadioButton ValueList = new AxonContLib.cRadioButton();
                ValueList.Text = DRow["PARANAME"].ToString();
                ValueList.Tag = DRow["PARA_ID"].ToString();
                ValueList.AccessibleDescription = Val.ToString(DRow["REMARK"]);
                ValueList.ToolTips = Val.ToString(DRow["PARACODE"]);
                ValueList.AutoSize = true;
                ValueList.Click += new EventHandler(cRadioShapeButton_Click);
                ValueList.Font = PanelFileProcess.Font;
                ValueList.Cursor = Cursors.Hand;
                if (IntI == 0)
                {
                    ValueList.Checked = true;
                    ValueList.ForeColor = mSelectedColor;
                    ValueList.BackColor = mSelectedBackColor;
                }
                else
                {
                    ValueList.Checked = false;
                    ValueList.ForeColor = mDeSelectColor;
                    ValueList.BackColor = mDSelectedBackColor;
                }
                PanelFileProcess.Controls.Add(ValueList);
                IntI++;

            }

            this.Show();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjFile);
            ObjFormEvent.ObjToDisposeList.Add(ObjRap);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        private void cRadioShapeButton_Click(object sender, EventArgs e)
        {

            AxonContLib.cRadioButton rd = (AxonContLib.cRadioButton)sender;

            foreach (AxonContLib.cRadioButton Cont in PanelFileProcess.Controls)
            {
                Cont.ForeColor = mDeSelectColor;
                Cont.BackColor = mDSelectedBackColor;
            }

            AxonContLib.cRadioButton rbSelected = PanelFileProcess.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
            rbSelected.ForeColor = mSelectedColor;
            rbSelected.BackColor = mSelectedBackColor;

        }


        #endregion

        //private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        if (Global.OnKeyPressToOpenPopup(e))
        //        {
        //            FrmSearch FrmSearch = new FrmSearch();
        //            FrmSearch.SearchField = "KAPANNAME";
        //            FrmSearch.SearchText = e.KeyChar.ToString();
        //            this.Cursor = Cursors.WaitCursor;
        //            FrmSearch.DTab = ObjRap.GetKapan();

        //            FrmSearch.ColumnsToHide = "";
        //            this.Cursor = Cursors.Default;
        //            FrmSearch.ShowDialog();
        //            e.Handled = true;
        //            if (FrmSearch.DRow != null)
        //            {
        //                txtKapanName.Text = Val.ToString(FrmSearch.DRow["KAPANNAME"]);
        //            }

        //            FrmSearch.Hide();
        //            FrmSearch.Dispose();
        //            FrmSearch = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.MessageError(ex.Message);
        //    }
        //}

        //private void txtPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        if (Global.OnKeyPressToOpenPopup(e))
        //        {
        //            FrmSearch FrmSearch = new FrmSearch();
        //            FrmSearch.SearchField = "PACKETNO";
        //            FrmSearch.SearchText = e.KeyChar.ToString();
        //            this.Cursor = Cursors.WaitCursor;
        //            FrmSearch.DTab = ObjRap.GetPacketNo(txtKapanName.Text);

        //            FrmSearch.ColumnsToHide = "";
        //            this.Cursor = Cursors.Default;
        //            FrmSearch.ShowDialog();
        //            e.Handled = true;
        //            if (FrmSearch.DRow != null)
        //            {
        //                txtPacketNo.Text = Val.ToString(FrmSearch.DRow["PACKETNO"]);
        //            }

        //            FrmSearch.Hide();
        //            FrmSearch.Dispose();
        //            FrmSearch = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.MessageError(ex.Message);
        //    }
        //}

        //private void txtTag_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        if (Global.OnKeyPressToOpenPopup(e))
        //        {
        //            FrmSearch FrmSearch = new FrmSearch();
        //            FrmSearch.SearchField = "TAG";
        //            FrmSearch.SearchText = e.KeyChar.ToString();
        //            this.Cursor = Cursors.WaitCursor;
        //            FrmSearch.DTab = ObjRap.GetTag(txtKapanName.Text, Val.ToInt(txtPacketNo.Text));

        //            FrmSearch.ColumnsToHide = "KAPAN_ID,PACKET_ID,EMPLOYEE_ID,KAPANNAME,PACKETNO,LOTPCS,BALANCEPCS";
        //            this.Cursor = Cursors.Default;
        //            FrmSearch.ShowDialog();
        //            e.Handled = true;
        //            if (FrmSearch.DRow != null)
        //            {
        //                txtTag.Text = Val.ToString(FrmSearch.DRow["TAG"]);
        //                txtTag.Tag = Val.ToString(FrmSearch.DRow["PACKET_ID"]);
        //                txtKapanName.Tag = Val.ToString(FrmSearch.DRow["KAPAN_ID"]);

        //                if (txtEmployee.Enabled == true)
        //                {
        //                    txtEmployee.Tag = Val.ToString(FrmSearch.DRow["EMPLOYEE_ID"]);
        //                    txtEmployee.Text = Val.ToString(FrmSearch.DRow["EMPLOYEECODE"]);
        //                }
        //            }

        //            FrmSearch.Hide();
        //            FrmSearch.Dispose();
        //            FrmSearch = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.MessageError(ex.Message);
        //    }
        //}

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (txtEmployee.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "EMPLOYEECODE";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearchPopupBox.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearchPopupBox.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Tag = Val.ToString(FrmSearchPopupBox.mDRow["EMPLOYEE_ID"]);
                    }

                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog1 = new OpenFileDialog
            //{
            //    InitialDirectory = @"D:\",
            //    Title = "Browse Stone Files",

            //    CheckFileExists = true,
            //    CheckPathExists = true,

            //    DefaultExt = "",
            //    Filter = "All files (*.*)|*.*",
            //    FilterIndex = 2,
            //    RestoreDirectory = true,

            //    ReadOnlyChecked = true,
            //    ShowReadOnly = true
            //};

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(openFileDialog1.FileName);

                AxonContLib.cRadioButton rbSelected = PanelFileProcess.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);

                string StrFileName = rbSelected.ToolTips + "_" + txtKapanName.Text + "_" + txtPacketNo.Text + "_" + txtTag.Text + "_" + txtEmployee.Text + f.Extension;

                BONetworkShare.DisconnectFromShare(rbSelected.AccessibleDescription + "\\" + StrFileName, true); //Disconnect in case we are currently connected with our credentials;
                BONetworkShare.ConnectToShare(rbSelected.AccessibleDescription + "\\" + StrFileName, mStrFileTransferUsername, mStrFileTransferPassword); //Connect with the new credentials

                try
                {
                    //byte[] fileNameByte = Encoding.ASCII.GetBytes(f.FullName);
                    //byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                    //fileNameLen.CopyTo(fileNameByte, 0);
                    //TcpClient clientSocket = new TcpClient(remoteHostIP, remoteHostPort);
                    //NetworkStream networkStream = clientSocket.GetStream();
                    //networkStream.Write(fileNameLen, 0, fileNameLen.GetLength(0)); 
                    //networkStream.Close(); 

                    File.Copy(openFileDialog1.FileName, rbSelected.AccessibleDescription + "\\" + StrFileName, true);
                    BONetworkShare.DisconnectFromShare(rbSelected.AccessibleDescription + "\\" + StrFileName, false); //Disconnect from the server.

                }

                catch (Exception ex)
                {
                    Global.MessageError(ex.Message);
                }
                Global.Message("File Transfer Successfully."); //Add Milan (30-03-2021)
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                AxonContLib.cRadioButton rbSelected = PanelFileProcess.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);


                string StrSharingFolderNme = rbSelected.AccessibleDescription;
                string StrFileName = rbSelected.ToolTips + "_" + txtKapanName.Text + "_" + txtPacketNo.Text + "_" + txtTag.Text + "_" + txtEmployee.Text + ".cap";

                BONetworkShare.DisconnectFromShare(rbSelected.AccessibleDescription + "\\" + StrFileName, true); //Disconnect in case we are currently connected with our credentials;
                BONetworkShare.ConnectToShare(rbSelected.AccessibleDescription + "\\" + StrFileName, mStrFileTransferUsername, mStrFileTransferPassword); //Connect with the new credentials

                if (File.Exists(Application.StartupPath + "\\StoneFileDownload\\" + StrFileName))
                {
                    File.Delete(Application.StartupPath + "\\StoneFileDownload\\" + StrFileName);
                }

                File.Copy(StrSharingFolderNme + "\\" + StrFileName, Application.StartupPath + "\\StoneFileDownload\\" + StrFileName, true);

                BONetworkShare.DisconnectFromShare(rbSelected.AccessibleDescription + "\\" + StrFileName, false); //Disconnect from the server.

                Global.Message("File Successfully Downloaded");

                System.Diagnostics.Process.Start(Application.StartupPath + "\\StoneFileDownload\\" + StrFileName, "CMD");

                //File.Delete(Application.StartupPath + "\\StoneDownload\\" + StrFileName);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
            */

            try
            {
                AxonContLib.cRadioButton rbSelected = PanelFileProcess.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);


                string StrSharingFolderNme = rbSelected.AccessibleDescription;
                string StrFileName = rbSelected.ToolTips + "_" + txtKapanName.Text + "_" + txtPacketNo.Text + "_" + txtTag.Text + "_" + txtEmployee.Text + ".cap";

                DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(StrSharingFolderNme);

                string StrPktNo = rbSelected.ToolTips + "_" + txtKapanName.Text + "_" + txtPacketNo.Text + "_";

                if (Val.ToInt32(rbSelected.Tag) == 540 || Val.ToInt32(rbSelected.Tag) == 541) //540:Final & 541:Checker
                {
                    StrPktNo = rbSelected.ToolTips + "_" + txtKapanName.Text + "_" + txtPacketNo.Text + "_";
                }
                else
                {
                    if (txtTag.Text.Trim().Equals(string.Empty))
                    {
                        Global.Message("Tag Is Required");
                        txtTag.Focus();
                        return;
                    }

                    StrPktNo = rbSelected.ToolTips + "_" + txtKapanName.Text + "_" + txtPacketNo.Text + "_" + txtTag.Text + "_";
                }



                string StrDestinationPath = "";
                string StrSourceFilePath = "";

                string StrTag = "";
                string StrEmp = "";

                StrSourceFilePath = StrSharingFolderNme + "\\" + StrPktNo;

                //FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + StrPktNo + "*.*");
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + StrPktNo + "*");
                using (new SharingLockUnlock(StrSharingFolderNme, mStrFileTransferUsername, mStrFileTransferPassword))
                {
                    if (filesInDir.Length > 0)
                    {
                        StrDestinationPath = Val.ToString(filesInDir[0].FullName);
                        string StrReplace = (StrDestinationPath.Replace(StrSourceFilePath, "").Replace(".cap", ""));
                        StrTag = StrReplace.Substring(0,StrReplace.LastIndexOf("_"));
                        StrEmp = StrReplace.Substring(StrReplace.LastIndexOf("_") + 1, (StrReplace.Replace(StrTag,"")).Replace("_","").Length);
                        //string Str = StrDestinationPath.Substring(Val.ToString(StrSourceFilePath).Length, Val.ToString(StrReplace).Length);
                    }
                }

                txtTag.Text = StrTag;
                txtEmployee.Text = StrEmp;

                File.Copy(StrDestinationPath, Application.StartupPath + "\\StoneFileDownload\\" + StrFileName, true);
                Global.Message("File Successfully Downloaded");
                System.Diagnostics.Process.Start(Application.StartupPath + "\\StoneFileDownload\\" + StrFileName, "CMD");

                //File.Delete(Application.StartupPath + "\\StoneDownload\\" + StrFileName);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            if (txtKapanName.Text.Trim().Length == 0)
            {
                return;
            }
            if (Val.ToInt(txtPacketNo.Text) == 0)
            {
                txtKapanName.Focus();
                return;
            }
            if (txtTag.Text.Trim().Length == 0)
            {
                txtKapanName.Focus();
                return;
            }

            if (Val.ISNumeric(txtTag.Text) == true)
            {
                Char c = (Char)(64 + Val.ToInt(txtTag.Text));
                txtTag.Text = c.ToString();
            }

            this.Cursor = Cursors.WaitCursor;

            bool ISFind = false;
            if (ISFind == false)
            {
                DataTable Dtab = ObjRap.GetPacketList(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), Val.ToString(txtTag.Text));

                if (Dtab == null || Dtab.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;

                    Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
                    txtKapanName.Text = string.Empty;
                    txtPacketNo.Text = string.Empty;
                    txtTag.Text = string.Empty;

                    txtKapanName.Focus();
                    return;
                }
                else
                {
                    txtTag.Tag = Val.ToString(Dtab.Rows[0]["PACKET_ID"]);
                    txtKapanName.Tag = Val.ToString(Dtab.Rows[0]["KAPAN_ID"]);

                    //if (txtEmployee.Enabled == true)
                    //{
                    //    txtEmployee.Tag = Val.ToString(Dtab.Rows[0]["EMPLOYEE_ID"]);
                    //    txtEmployee.Text = Val.ToString(Dtab.Rows[0]["EMPLOYEECODE"]);
                    //}
                }
                this.Cursor = Cursors.Default;


            }
        }
    }
}
