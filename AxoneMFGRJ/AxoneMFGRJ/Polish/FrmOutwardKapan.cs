using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmOutwardKapan : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_ParcelKapanOutward ObjOutward = new BOTRN_ParcelKapanOutward();
        BOTRN_KapanInward ObjInward = new BOTRN_KapanInward();
        DataTable DtabOutward = new DataTable();

        public FrmOutwardKapan()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            this.Show();

        }
        public void Clear()
        {
            txtKapanName.Text = string.Empty;
            txtInwardCarat.Text = string.Empty;
            txtAssortCarat.Text = string.Empty;            
            DTPOutwardDate.Text = Val.ToString(DateTime.Now);
            txtRemark.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtRateRs.Text = string.Empty;
            txtKapanName.Focus();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKapanName.Text.Trim().Length == 0)
                {
                    Global.MessageError("Kapan Name Is Required");
                    txtKapanName.Focus();
                    return;
                }
                if (txtInwardCarat.Text.Trim().Length == 0)
                {
                    Global.MessageError("Inward Carat Is Required");
                    txtInwardCarat.Focus();
                    return;
                }
                if (txtAssortCarat.Text.Trim().Length == 0)
                {
                    Global.MessageError("Assort Carat Is Required");
                    txtAssortCarat.Focus();
                    return;
                }
                if (DTPOutwardDate.Text.Trim().Length == 0)
                {
                    Global.MessageError("Outward Date Is Required");
                    DTPOutwardDate.Focus();
                    return;
                }

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";
                KapanOutwardProperty Property = new KapanOutwardProperty();
                
                    Property.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);
                    Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                    Property.INWARDCARAT = Val.ToDecimal(txtInwardCarat.Text);
                    Property.ASSORTCARAT = Val.ToDecimal(txtAssortCarat.Text);
                    Property.OUTWARDDATE = Val.SqlDate(DTPOutwardDate.Text);
                    Property.OUTWARD_ID = Val.ToInt64(DTPOutwardDate.Tag);
                    Property.REMARK = Val.ToString(txtRemark.Text);
                    Property.RATE = Val.ToDecimal(txtRate.Text);
                    Property.RATERS = Val.ToDecimal(txtRateRs.Text);

                    Property = ObjOutward.SaveKapanOutward(Property);

                


                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    //FillPanulty();
                    Clear();
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    txtKapanName.Focus();
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME,KAPAN_ID";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.PARCEL_KAPANOUTWARD);
                    this.Cursor = Cursors.Default;
                    FrmSearch.mColumnsToHide = "KAPAN_ID";
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
                        txtInwardCarat.Text = Val.ToString(FrmSearch.mDRow["CARAT"]);
                        txtAssortCarat.Text = Val.ToString(FrmSearch.mDRow["ASSORTEDCARAT"]);
                        //BtnSearch_Click(null, null);
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFromDate = null;
                string StrDate = null;
                string StrKapanName = "";
                string pStrOpe = "";


                if (RbtInward.Checked == true)
                {
                    pStrOpe = "INWARD";
                }
                else
                {
                    pStrOpe = "OUTWARD";
                }

                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                StrDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                

                this.Cursor = Cursors.WaitCursor;
                if (pStrOpe == "INWARD")
                {
                    DataTable DTabData = ObjInward.GetDataForOutward(Val.ToString(txtKapanName.Text), StrFromDate, StrDate);
                    DataRow Dr = DTabData.NewRow();
                    MainGrid.DataSource = DTabData;

                    GrdKapanOutward.Columns["OUTWARDDATE"].Visible = false;
                    GrdKapanOutward.Columns["RATE"].Visible = false;
                    GrdKapanOutward.Columns["RATERS"].Visible = false;
                    GrdKapanOutward.Columns["REMARK"].Visible = false;

                    GrdKapanOutward.Columns["INWARDNO"].Visible = true;
                    GrdKapanOutward.Columns["CNT"].Visible = true;
                    GrdKapanOutward.Columns["INWARDDATE"].Visible = true;
                    GrdKapanOutward.Columns["AMOUNT"].Visible = true;
                    GrdKapanOutward.Columns["CARAT"].Visible = true;
                    GrdKapanOutward.Columns["PENDINGCARAT"].Visible = true;
                    GrdKapanOutward.Columns["ASSORTEDCARAT"].Visible = true;
                    GrdKapanOutward.Columns["STATUS"].Visible = true;
                    GrdKapanOutward.Columns["ENTRYDATE"].Visible = true;
                    GrdKapanOutward.Columns["ENTRYIP"].Visible = true;
                    GrdKapanOutward.Columns["ENTRYBY"].Visible = true;
                    GrdKapanOutward.Columns["KAPAN_ID"].Visible = true;
                    GrdKapanOutward.Columns["REPLOSSREMARK"].Visible = true;
                    GrdKapanOutward.Columns["REPAIRINGLOSS"].Visible = true;
                    GrdKapanOutward.Columns["EXCRATE"].Visible = true;

                    MainGrid.Refresh();
                }
                else
                {
                    DataTable DTabData = ObjOutward.FillOutward(StrFromDate, StrDate, StrKapanName);
                    MainGrid.DataSource = DTabData;

                    GrdKapanOutward.Columns["OUTWARDDATE"].Visible = true;
                    GrdKapanOutward.Columns["RATE"].Visible = true;
                    GrdKapanOutward.Columns["RATERS"].Visible = true;
                    GrdKapanOutward.Columns["REMARK"].Visible = true;

                    GrdKapanOutward.Columns["INWARDNO"].Visible = false;
                    GrdKapanOutward.Columns["CNT"].Visible = false;
                    GrdKapanOutward.Columns["INWARDDATE"].Visible = false;
                    GrdKapanOutward.Columns["AMOUNT"].Visible = false;
                    GrdKapanOutward.Columns["CARAT"].Visible = false;
                    GrdKapanOutward.Columns["PENDINGCARAT"].Visible = false;
                    GrdKapanOutward.Columns["ASSORTEDCARAT"].Visible = false;
                    GrdKapanOutward.Columns["STATUS"].Visible = false;
                    GrdKapanOutward.Columns["ENTRYDATE"].Visible = false;
                    GrdKapanOutward.Columns["ENTRYIP"].Visible = false;
                    GrdKapanOutward.Columns["ENTRYBY"].Visible = false;
                    GrdKapanOutward.Columns["KAPAN_ID"].Visible = false;
                    GrdKapanOutward.Columns["REPLOSSREMARK"].Visible = false;
                    GrdKapanOutward.Columns["REPAIRINGLOSS"].Visible = false;
                    GrdKapanOutward.Columns["EXCRATE"].Visible = false;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Kapan OutWard List", GrdKapanOutward);
        }

        private void GrdKapanOutward_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                if ((e.Clicks == 2) && (RbtOutward.Checked == true))
                {
                    DataRow DR = GrdKapanOutward.GetDataRow(e.RowHandle);
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
            if (RbtOutward.Checked == true)
            {
                txtKapanName.Text = Val.ToString(DR["KAPANNAME"]);
                txtKapanName.Tag = Val.ToInt64(DR["KAPAN_ID"]);
                txtInwardCarat.Text = Val.ToString(DR["INWARDCARAT"]);
                txtAssortCarat.Text = Val.ToString(DR["ASSORTCARAT"]);
                DTPOutwardDate.Text = Val.ToString(DR["OUTWARDDATE"]);
                DTPOutwardDate.Tag = Val.ToString(DR["OUTWARD_ID"]);
                txtRemark.Text = Val.ToString(DR["REMARK"]);
                txtRateRs.Text = Val.ToString(DR["RATERS"]);
                txtRate.Text = Val.ToString(DR["RATE"]);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (GrdKapanOutward.RowCount != 0)
            {
                SaveFileDialog Dialog = new SaveFileDialog();
                Dialog.Filter = ".xls|.xlsx";
                if (Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    GrdKapanOutward.ExportToXlsx(Dialog.FileName);

                    if (Global.Confirm("Do You Want To Open The File ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Dialog.FileName, "CMD");
                    }
                }

            }

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtKapanName.Text = string.Empty;
            txtKapanName.Tag = string.Empty;
            txtInwardCarat.Text = string.Empty;
            txtAssortCarat.Text = string.Empty;
            DTPOutwardDate.Value = DateTime.Now;
            DTPOutwardDate.Tag = string.Empty;
            txtRemark.Text = string.Empty;
            txtRateRs.Text = string.Empty;
            txtRate.Text = string.Empty; 
        }
    }
}
