using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
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
    public partial class 
        FrmHolidayListMaster : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_HolidayDetail ObjMast = new BOMST_HolidayDetail();
        DataTable DtabHoliday = new DataTable();

        #region Property Settings

        public FrmHolidayListMaster()
        {
            InitializeComponent();
        }

      
        public void ShowForm()
        {
           
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
           
            BtnAdd_Click(null, null);
          
            Fill();
            this.Show();

            Dtpholiday.Focus();
        }

        private bool ValSave()
        {
            if (txtLeaveType.Text.Trim().Length == 0)
            {
                Global.Message("LeaveType Is Required");
                txtLeaveType.Focus();
                return false;
            }

            if (txtWdays.Text.Trim().Length == 0)
            {
                Global.Message("WDay Is Required");
                txtWdays.Focus();
                return false;
            }

            if (txtRemark.Text.Trim().Length == 0)
            {
                Global.Message("Remark Is Required");
                txtRemark.Focus();
                return false;
            }

            return true;
        }
        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        public void Clear()
        {
            txtWdays.Text = string.Empty;
            txtLeaveType.Text = string.Empty;
            txtLeaveType.Tag = string.Empty;
            txtRemark.Text = string.Empty;
            Dtpholiday.Value = DateTime.Now;
            Dtpholiday.Focus();
            txtHoliday.Text = string.Empty;
            txtHoliday.Tag = string.Empty;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }
        public void Fill()
        {
            DtabHoliday = ObjMast.Fill();
            MainGrid.DataSource = DtabHoliday;
            MainGrid.Refresh();

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Holiday List", GrdDet);
        }

      
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                HolidayDetailProperty Property = new HolidayDetailProperty();

                Property.HOLIDAY_ID = Val.ToInt32(txtHoliday.Tag);
                Property.HOLIDAYDATE = Val.SqlDate(Dtpholiday.Value.ToString());
                Property.LEAVETYPE_ID = Val.ToInt32(txtLeaveType.Tag);
                Property.LEAVETYPE = Val.ToString(txtLeaveType.Text);
                Property.WDAYS = Val.ToDouble(txtWdays.Text);
                Property.REMARK = Val.ToString(txtRemark.Text);

                Property = ObjMast.Save(Property);

                ReturnMessageDesc = Property.ReturnMessageDesc;
                ReturnMessageType = Property.ReturnMessageType;

                Property = null;

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    Clear();

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                    Dtpholiday.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        
        private void txtLeaveType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEAVETYEPE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEAVETYPE);

                    FrmSearch.mColumnsToHide = "LEAVETYPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtLeaveType.Text = Val.ToString(FrmSearch.mDRow["LEAVETYEPE"]);
                        txtLeaveType.Tag = Val.ToString(FrmSearch.mDRow["LEAVETYPE_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            HolidayDetailProperty Property = new HolidayDetailProperty();
            try
            {

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;


                Property.HOLIDAY_ID = Val.ToInt32(txtHoliday.Tag);
                Property = ObjMast.Delete(Property);
                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnAdd_Click(null, null);
                    Fill();
                }
                else
                {
                    Dtpholiday.Focus();
                }

            }
            catch (System.Exception ex)
            {
                Global.MessageToster(ex.Message);
            }
            Property = null;
        }

        public void FetchValue(DataRow DR)
        {
            txtHoliday.Tag = Val.ToInt32(DR["HOLIDAY_ID"]);
            txtHoliday.Text = Val.ToString(DR["HOLIDAY_ID"]);
            txtWdays.Text = Val.ToString(DR["WDAYS"]);
            txtLeaveType.Tag = Val.ToInt32(DR["LEAVETYPE_ID"]);
            txtLeaveType.Text = Val.ToString(DR["LEAVETYPE"]);
            Dtpholiday.Text = Val.ToString(DR["HOLIDAYDATE"]);
            txtRemark.Text = Val.ToString(DR["REMARK"]);
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

        private void BtnAdd_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
