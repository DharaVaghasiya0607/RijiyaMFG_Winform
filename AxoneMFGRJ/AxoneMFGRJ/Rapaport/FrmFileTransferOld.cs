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

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmFileTransferOld : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        BOTRN_FileTransfer ObjFile = new BOTRN_FileTransfer();

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        
        Color mSelectedColor = Color.FromArgb(192, 0, 0);
        Color mDeSelectColor = Color.Black;
        Color mSelectedBackColor = Color.FromArgb(255, 224, 192);
        Color mDSelectedBackColor = Color.WhiteSmoke;

        DataTable DTabPacket = new DataTable();
        
        #region Property Settings

        public FrmFileTransferOld()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            txtKapanName.Focus();

            DataTable DTabProcess = new BOComboFill().FillCmb(BOComboFill.TABLE.FILE_TRANSFER_TYPE);

            PanelFileProcess.Controls.Clear();
            int IntI = 0;
            foreach (DataRow DRow in DTabProcess.Rows)
            {
                AxonContLib.cRadioButton ValueList = new AxonContLib.cRadioButton();
                ValueList.Text = DRow["PARANAME"].ToString();
                ValueList.Tag = DRow["PARA_ID"].ToString();
                ValueList.AccessibleDescription = Val.ToString(DRow["REMARK"]);
                ValueList.ToolTips = Val.ToString(DRow["REMARK"]);
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
                    ValueList.Checked = false ;
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

            BtnShow_Click(null, null);
        }


        #endregion

        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjRap.GetKapan();
                  
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void txtPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjRap.GetPacketNo(txtKapanName.Text);
                  
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void txtTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "TAG";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjRap.GetTag(txtKapanName.Text, Val.ToInt(txtPacketNo.Text));
                    
                    FrmSearch.mColumnsToHide = "KAPAN_ID,PACKET_ID,EMPLOYEE_ID,KAPANNAME,PACKETNO,LOTPCS,BALANCEPCS";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtTag.Text = Val.ToString(FrmSearch.mDRow["TAG"]);
                        txtTag.Tag = Val.ToString(FrmSearch.mDRow["PACKET_ID"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);

                        if (txtEmployee.Enabled == true)
                        {
                            txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                            txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        }
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

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
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (txtKapanName.Text.Length == 0)
            {
                Global.MessageError("Kapan Name Is Required");
                txtKapanName.Focus();
                return;
            }
           
            DataRow DRPkt = ObjRap.GetPacketDataRow(txtKapanName.Text, Val.ToInt32(txtPacketNo.Text), txtTag.Text);

            if (DRPkt != null)
            {
                txtTag.Tag = Val.ToString(DRPkt["PACKET_ID"]);
            }
            else
            {
                txtTag.Tag = Guid.NewGuid().ToString();
            }
            
            this.Cursor = Cursors.WaitCursor;
            AxonContLib.cRadioButton rbShp = PanelFileProcess.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
            
            TrnFileTransferProperty Property = new TrnFileTransferProperty();
            Property.PROCESS_ID = Val.ToInt32(rbShp.Tag);
            Property.PROCESSNAME = Val.ToString(rbShp.Text);
            Property.KAPANNAME = txtKapanName.Text;
            Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
            Property.TAG = txtTag.Text;
            Property.PACKET_ID = Guid.Parse(txtTag.Tag.ToString());
            Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
            Property.EMPLOYEECODE = txtEmployee.Text;

            DTabPacket = ObjFile.Fill(Property, ChkDisplayAll.Checked);

            MainGrid.DataSource = DTabPacket;
            MainGrid.Refresh();
            GrdDet.BestFitColumns();

            GrdDet.Columns["ISUPLOAD"].Visible = !ChkDisplayAll.Checked;
            Property = null;

            this.Cursor = Cursors.Default;
        }

        private void GrdDet_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks != 2)
            {
                return;
            }

            DataRow DRow = GrdDet.GetDataRow(e.RowHandle);

            if (e.Column.FieldName.ToUpper() == "ISUPLOAD")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog  
                {  
                    InitialDirectory = @"D:\",  
                    Title = "Browse Stone Files",  
  
                    CheckFileExists = true,  
                    CheckPathExists = true,  
  
                    DefaultExt = "",  
                    Filter = "All files (*.*)|*.*",  
                    FilterIndex = 2,  
                    RestoreDirectory = true,  
  
                    ReadOnlyChecked = true,  
                    ShowReadOnly = true  
                };  
  
                if (openFileDialog1.ShowDialog() == DialogResult.OK)  
                {
                    FileInfo f = new FileInfo(openFileDialog1.FileName);

                    AxonContLib.cRadioButton rbSelected = PanelFileProcess.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);

                    TrnFileTransferProperty Property = new TrnFileTransferProperty();
                    Property.ID = Val.ToString(DRow["ID"]);
                    Property.PROCESS_ID = Val.ToInt32(DRow["PROCESS_ID"]);
                    Property.PROCESSNAME = Val.ToString(DRow["PROCESSNAME"]);
                    Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"]);
                    Property.PACKETNO = Val.ToInt32(DRow["PACKETNO"]);
                    Property.TAG = Val.ToString(DRow["TAG"]);
                    Property.PACKET_ID = Guid.Parse(Val.ToString(DRow["PACKET_ID"]));
                    Property.EMPLOYEE_ID = Val.ToInt64(DRow["EMPLOYEE_ID"]);
                    Property.EMPLOYEECODE = Val.ToString(DRow["EMPLOYEECODE"]);
                    Property.FILENAME = Property.KAPANNAME + "_" + Property.PACKETNO.ToString() + "_" + Property.TAG + "_" + Property.EMPLOYEECODE + f.Extension;

                    try
                    {

                        //byte[] fileNameByte = Encoding.ASCII.GetBytes(f.FullName);
                        //byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                        //fileNameLen.CopyTo(fileNameByte, 0);
                        //TcpClient clientSocket = new TcpClient(remoteHostIP, remoteHostPort);
                        //NetworkStream networkStream = clientSocket.GetStream();
                        //networkStream.Write(fileNameLen, 0, fileNameLen.GetLength(0)); 
                        //networkStream.Close(); 


                        File.Copy(openFileDialog1.FileName, rbSelected.AccessibleDescription + "\\" + Property.FILENAME, true);

                        Property = ObjFile.Save(Property);
                        Global.Message(Property.ReturnMessageDesc);
                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            GrdDet.SetRowCellValue(e.RowHandle, "ID", Property.ReturnValue);
                            GrdDet.SetRowCellValue(e.RowHandle, "ISVIEW", "View");
                            GrdDet.SetRowCellValue(e.RowHandle, "FILEPATH", rbSelected.AccessibleDescription + "\\" + Property.FILENAME);
                            GrdDet.SetRowCellValue(e.RowHandle, "FILENAME", Property.FILENAME);
                            if (Property.ReturnMessageDesc.Contains("INSERTED"))
                            {
                                GrdDet.SetRowCellValue(e.RowHandle, "ENTRYDATE", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                                GrdDet.SetRowCellValue(e.RowHandle, "ENTRYBYCODE", BOConfiguration.gEmployeeProperty.LEDGERCODE);
                                GrdDet.SetRowCellValue(e.RowHandle, "ENTRYIP", BOConfiguration.ComputerIP);
                            }
                            else
                            {
                                GrdDet.SetRowCellValue(e.RowHandle, "UPDATEDATE", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                                GrdDet.SetRowCellValue(e.RowHandle, "UPDATEBYCODE", BOConfiguration.gEmployeeProperty.LEDGERCODE);
                                GrdDet.SetRowCellValue(e.RowHandle, "UPDATEIP", BOConfiguration.ComputerIP);
                            }
                            GrdDet.BestFitColumns();
                        }
                        Property = null;
                        GrdDet.RefreshData();
                    }
                    catch (Exception ex)
                    {
                        Global.MessageError(ex.Message);
                    }
                }  
            }
            else if (e.Column.FieldName.ToUpper() == "ISVIEW" && e.CellValue.ToString() == "View")
            {
                AxonContLib.cRadioButton rbSelected = PanelFileProcess.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                string FileName = rbSelected.AccessibleDescription + "\\" + Val.ToString(DRow["FILENAME"]);

                System.Diagnostics.Process.Start(FileName, "CMD");

            }
            else if (e.Column.FieldName.ToUpper() == "ISDELETE")
            {
                if (Global.Confirm("Are You Sure To Delete ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                TrnFileTransferProperty Property = new TrnFileTransferProperty();
                Property.ID = Val.ToString(DRow["ID"]);
                if (Property.ID != "")
                {
                    Property = ObjFile.Delete(Property);
                    Global.Message(Property.ReturnMessageDesc);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDet.DeleteRow(e.RowHandle);
                    }
                }
                
                Property = null;
                GrdDet.RefreshData();
            }
        }

        private void ChkDisplayAll_CheckedChanged(object sender, EventArgs e)
        {
            BtnShow_Click(null, null);
        }
      
    }
}
