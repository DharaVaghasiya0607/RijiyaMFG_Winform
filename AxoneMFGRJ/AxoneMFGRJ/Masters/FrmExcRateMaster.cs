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
    public partial class FrmExcRateMaster : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_ExcRate ObjMast = new BOTRN_ExcRate();
        DataTable DtabDailyRate = new DataTable();


        #region Property Settings

        public FrmExcRateMaster()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            BtnDelete.Enabled = ObjPer.ISDELETE;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            Clear();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            if (Val.Val(txtExcRate.Text) == 0)
            {
                Global.Message("'Exchange Rate' Is Required.");
                txtExcRate.Focus();
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
            txtExcRate.Text = string.Empty;
            txtExcRate.Tag = string.Empty;

            CmbMonth.SelectedItem = DateTime.Now.AddMonths(-1).ToString("MMM");
            txtYear.Text = DateTime.Now.Year.ToString();

            CmbMonth.Focus();
            Fill();
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

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                ExcRateMasterProperty Property = new ExcRateMasterProperty();

                Property.EXCRATE_ID = Val.ToString(txtExcRate.Tag).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(txtExcRate.Tag));
                Property.YY = Val.ToInt(txtYear.Text);
                Property.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);
                Property.EXCRATE = Val.Val(txtExcRate.Text);

                Property = ObjMast.Save(Property);

                ReturnMessageDesc = Property.ReturnMessageDesc;
                ReturnMessageType = Property.ReturnMessageType;

                Property = null;
                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Clear();
                }
                else
                {
                    //txtItemGroupCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            DtabDailyRate = ObjMast.Fill();
            MainGrid.DataSource = DtabDailyRate;
            MainGrid.Refresh();

        }


        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Exchange Rate List", GrdDet);
        }


        private void BtnExit_Click(object sender, EventArgs e)
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

        public void FetchValue(DataRow DR)
        {
            txtExcRate.Tag = Val.ToString(DR["EXCRATE_ID"]);
            txtExcRate.Text = Val.ToString(DR["EXCRATE"]);

            txtYear.Text = Val.ToString(DR["YY"]);
            CmbMonth.SelectedIndex = Val.ToInt(DR["MM"]) - 1;

        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ExcRateMasterProperty Property = new ExcRateMasterProperty();
            try
            {
                if (Val.ToString(txtExcRate.Tag).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Records From the List That You Want To Delete");
                    return;
                }

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;

                Property.EXCRATE_ID = Guid.Parse(Val.ToString(txtExcRate.Tag));

                Property = ObjMast.Delete(Property);
                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnAdd_Click(null, null);
                    Fill();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
            Property = null;
        }
    }
}
