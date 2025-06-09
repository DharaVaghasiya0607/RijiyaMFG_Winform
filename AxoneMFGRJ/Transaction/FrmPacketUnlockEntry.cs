using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmPacketUnlockEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PacketUnlockSetting ObjTrnUnlock = new BOTRN_PacketUnlockSetting();
        BOFormPer ObjPer = new BOFormPer();
        DataTable DtabUnlock = new DataTable();


        #region Property Settings

        public FrmPacketUnlockEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            BtnDelete.Enabled = ObjPer.ISDELETE;
            Clear();
            Fill();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrnUnlock);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion


        #region Validation

        private bool ValSave()
        {
            if (Val.ToString(txtKapanName.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Kapan Is Required.");
                txtKapanName.Focus();
                return true;
            }
            else if (Val.ToString(CmbSettingType.SelectedItem).Trim().Equals(string.Empty))
            {
                Global.Message("Type Is Required.");
                CmbSettingType.Focus();
                return true;
            }
            else if (Val.ToString(txtPacketNo.Text).Trim().Equals(string.Empty))
            {
                Global.Message("PacketNo Is Required.");
                txtPacketNo.Focus();
                return true;
            }
            else if (Val.ToString(txtTag.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Tag Is Required.");
                txtPacketNo.Focus();
                return true;
            }
            else if (Val.ToString(txtRemark.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Remark Is Required.");
                txtRemark.Focus();
                return true;
            }
            return false;
        }

        private bool ValDelete()
        {
            //if (txtItemGroupCode.Text.Trim().Length == 0)
            //{
            //    Global.Message("Group Code Is Required");
            //    txtItemGroupCode.Focus();
            //    return false;
            //}

            return true;
        }

        #endregion

        public void Clear()
        {
            try
            {
                txtUnlock_ID.Text = string.Empty;
                DTPEntryDate.Text = Val.ToString(DateTime.Now);
                CmbSettingType.SelectedIndex = 0;

                txtKapanName.Tag = string.Empty;
                txtKapanName.Text = string.Empty;

                txtPacketNo.Tag = string.Empty;
                txtPacketNo.Text = string.Empty;

                txtTag.Text = string.Empty;

                txtEmployee.Tag = string.Empty;
                txtEmployee.Text = string.Empty;

                txtManager.Tag = string.Empty;
                txtManager.Text = string.Empty;

                txtDepartment.Tag = string.Empty;
                txtDepartment.Text = string.Empty;

                txtProcess.Tag = string.Empty;
                txtProcess.Text = string.Empty;

                txtRemark.Text = string.Empty;

                txtKapanName.Enabled = true;
                txtPacketNo.Enabled = true;
                txtTag.Enabled = true;
                CmbSettingType.Enabled = true;

                txtKapanName.Focus();

                DtpFromDate.Text = Val.ToString(DateTime.Now);
                DtpToDate.Text = Val.ToString(DateTime.Now);
                CmbKapan.SetEditValue(-1);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave())
                {
                    return;
                }

                TrnPacketUnlockSettingProperty Property = new TrnPacketUnlockSettingProperty();

                Property.UNLOCK_ID = Val.ToString(txtUnlock_ID.Text).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(txtUnlock_ID.Text));
                Property.UNLOCKDATE = Val.SqlDate(DTPEntryDate.Text);
                Property.UNLOCKSETTINGTYPE = Val.ToString(CmbSettingType.SelectedItem);

                Property.KAPAN_ID = Guid.Parse(Val.ToString(txtKapanName.Tag));
                Property.PACKET_ID = Guid.Parse(Val.ToString(txtPacketNo.Tag));

                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                Property.TAG = Val.ToString(txtTag.Text);

                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);
                Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
                Property.PROCESS_ID = Val.ToInt32(txtProcess.Tag);

                Property.TOTALHOURS = Val.Val(txtTotalHours.Text);

                if(lblMode.Text.ToUpper() == "ADD MODE")
                    Property.FROMDATETIME = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                else
                    Property.FROMDATETIME = Val.ToString(DTPUnlockFromDateTime.Value);

                Property.REMARK = Val.ToString(txtRemark.Text);

                Property = ObjTrnUnlock.Save(Property);

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    Clear();
                }
                else
                {
                    DTPEntryDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            this.Cursor = Cursors.WaitCursor;

            string StrKapanName = Val.Trim(CmbKapan.Properties.GetCheckedItems());

            DtabUnlock = ObjTrnUnlock.Fill(Val.SqlDate(DtpFromDate.Text), Val.SqlDate(DtpToDate.Text), StrKapanName);

            if (DtabUnlock.Rows.Count > 0)
            {
                MainGrid.DataSource = DtabUnlock;
                MainGrid.Refresh();
            }
            else
            {
                MainGrid.DataSource = null;
                DtabUnlock.Rows.Clear();
            }

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            this.Cursor = Cursors.Default;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                if (e.Clicks == 2)
                {
                    DataRow DR = GrdDet.GetDataRow(e.RowHandle);
                    FetchValue(DR);
                    DR = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataRow DR = GrdDet.GetFocusedDataRow();
            //    FetchValue(DR);
            //    DR = null;
            //}
        }


        public void FetchValue(DataRow DR)
        {
            txtUnlock_ID.Text = Val.ToString(DR["UNLOCK_ID"]);
            DTPEntryDate.Text = Val.ToString(DR["UNLOCKDATE"]);
            CmbSettingType.SelectedItem = Val.ToString(DR["UNLOCKSETTINGTYPE"]);
            txtKapanName.Tag = Val.ToString(DR["KAPAN_ID"]);
            txtKapanName.Text = Val.ToString(DR["KAPANNAME"]);
            txtPacketNo.Tag = Val.ToString(DR["PACKET_ID"]);
            txtPacketNo.Text = Val.ToString(DR["PACKETNO"]);
            txtTag.Text = Val.ToString(DR["TAG"]);

            txtTotalHours.Text = Val.ToString(DR["TOTALHOURS"]);
            DTPUnlockFromDateTime.Text = Val.ToString(DR["FROMDATE"]);

            txtEmployee.Tag = Val.ToString(DR["EMPLOYEE_ID"]);
            txtEmployee.Text = Val.ToString(DR["EMPLOYEECODE"]);
            txtManager.Tag = Val.ToString(DR["MANAGER_ID"]);
            txtManager.Text = Val.ToString(DR["MANAGERCODE"]);
            txtDepartment.Tag = Val.ToString(DR["DEPARTMENT_ID"]);
            txtDepartment.Text = Val.ToString(DR["DEPARTMENTNAME"]);
            txtProcess.Tag = Val.ToString(DR["PROCESS_ID"]);
            txtProcess.Text = Val.ToString(DR["PROCESSNAME"]);

            txtRemark.Text = Val.ToString(DR["REMARK"]);

            txtKapanName.Enabled = false;
            txtPacketNo.Enabled = false;
            txtTag.Enabled = false;
            CmbSettingType.Enabled = false;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Kapan Process Setting List", GrdDet);
        }

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
                    FrmSearch.mDTab = new BOTRN_SinglePacketCreate().FindKapan();
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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                Fill();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


        private void FrmKapanProcessSetting_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    Fill();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {
                //if (txtKapanName.Text.Trim().Length == 0)
                //{
                //    Global.MessageError("Kapan Is Required");
                //    txtKapanName.Focus();
                //    return;
                //} 
                //else if (txtPacketNo.Text.Trim().Length == 0)
                //{
                //    Global.MessageError("Packet No Is Required");
                //    txtPacketNo.Focus();
                //    return;
                //}
                //else if (txtTag.Text.Trim().Length == 0)
                //{
                //    Global.MessageError("Tag Is Required");
                //    txtTag.Focus();
                //    return;
                //}

                txtEmployee.Text = string.Empty;
                txtEmployee.Tag = string.Empty;
                txtManager.Text = string.Empty;
                txtManager.Tag = string.Empty;
                txtDepartment.Text = string.Empty;
                txtDepartment.Tag = string.Empty;
                txtProcess.Text = string.Empty;
                txtProcess.Tag = string.Empty;


                string Str = txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text;

                //Add : Pinali : 10-08-2019
                //DataRow DRow = ObjTrn.GetFinalEmployeeIssPacketInfo(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt(txtProcessTo.Tag), txtProcessTo.Text);

                DataRow DRow = ObjTrnUnlock.GetScanPacketDetail(Val.ToString(txtKapanName.Text), Val.ToInt(txtPacketNo.Text), Val.ToString(txtTag.Text));

                if (DRow == null)
                {
                    Global.MessageError("Oops. " + Str + "  Packet Is Not In Stock. Please Check In Running Stock");
                    txtKapanName.Text = "";
                    txtPacketNo.Text = "";
                    txtTag.Text = "";
                    txtKapanName.Focus();
                    return;
                }

                txtKapanName.Tag = Val.ToString(DRow["KAPAN_ID"]);
                txtPacketNo.Tag = Val.ToString(DRow["PACKET_ID"]);

                txtEmployee.Tag = Val.ToString(DRow["TOEMPLOYEE_ID"]);
                txtEmployee.Text = Val.ToString(DRow["TOEMPLOYEECODE"]);

                txtManager.Tag = Val.ToString(DRow["TOMANAGER_ID"]);
                txtManager.Text = Val.ToString(DRow["TOMANAGERCODE"]);

                txtDepartment.Tag = Val.ToString(DRow["TODEPARTMENT_ID"]);
                txtDepartment.Text = Val.ToString(DRow["TODEPARTMENTNAME"]);

                txtProcess.Tag = Val.ToString(DRow["TOPROCESS_ID"]);
                txtProcess.Text = Val.ToString(DRow["TOPROCESSNAME"]);



            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPEntryDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                //DTPEntryDate.MaxDate = DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy"));
                //DTPEntryDate.MaxDate = DateTime.Parse(Val.ToString(DateTime.Now.AddDays(0).AddTicks(-1)));
                DTPEntryDate.MaxDate = DateTime.Parse(Val.ToString(DateTime.Now.Date.AddSeconds(86400 - 1)));
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(Val.ToString(txtUnlock_ID.Text).Trim().Equals(string.Empty))
                    Global.Message("Please Select Record From The List That You Want To Delete");

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;

                this.Cursor = Cursors.WaitCursor;

                TrnPacketUnlockSettingProperty Property = new TrnPacketUnlockSettingProperty();
                Property.UNLOCK_ID = Guid.Parse(Val.ToString(txtUnlock_ID.Text));
                Property = ObjTrnUnlock.Delete(Property);
                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnAdd_Click(null, null);
                    Fill();
                }
                else
                {
                    txtKapanName.Focus();
                }
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }
        }

    }
}
