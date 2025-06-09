using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BusLib.Transaction;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmLabReturnPrediction : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();

        public FrmLabReturnPrediction()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            ChkCmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            ChkCmbKapan.Properties.DisplayMember = "KAPANNAME";
            ChkCmbKapan.Properties.ValueMember = "KAPANNAME";
            ChkCmbKapan.Focus();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            //ObjFormEvent.ObjToDisposeList.Add(ObjBrk);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Global.ExcelExport("Lab Prediction",GrdDetList);
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void BtnAutoFit_Click(object sender, EventArgs e)
        {
            try
            {
                GrdDetList.BestFitColumns();
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrKapan = "";
                Int32 FromPacketNo = 0;
                Int32 ToPacketNo = 0;
                string StrFromDate = "";
                string StrToDate = "";


                StrKapan = Val.Trim(ChkCmbKapan.Properties.GetCheckedItems());
                FromPacketNo = Val.ToInt32(txtFromPacketNo.Text);
                ToPacketNo = Val.ToInt32(txtToPacketNo.Text);

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                DataTable DTabLab = ObjKapan.GetLabPredictionData(StrKapan, FromPacketNo, ToPacketNo, txtTag.Text, StrFromDate, StrToDate);

                MainGrdList.DataSource = DTabLab;
                MainGrdList.Refresh();
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

    }
}