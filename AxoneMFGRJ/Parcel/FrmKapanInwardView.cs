using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmKapanInwardView : Form
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanInward ObjKapan = new BOTRN_KapanInward();
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        public FrmKapanInwardView()
        {
            InitializeComponent();
        }
        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = false;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjKapan);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            CmbYear.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_YEAR);
            CmbYear.Properties.DisplayMember = "YEARNAME";
            CmbYear.Properties.ValueMember = "YEAR_ID";

            chkOutward_CheckedChanged(null, null);

            DtpFromInwardDate.Value = DateTime.Now.AddMonths(-1);
            DtpToInwardDate.Value = DateTime.Now;
            BtnSearch_Click(null, null);
            this.Show();

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string StrStatus = ",";
                if (ChkPending.Checked == true)
                {
                    StrStatus = StrStatus + "PENDING,";
                }
                if (ChkComplete.Checked == true)
                {
                    StrStatus = StrStatus + "COMPLETE,";
                }
                if (ChkPartial.Checked == true)
                {
                    StrStatus = StrStatus + "PARTIAL,";
                }
                StrStatus = StrStatus + ",";

                string StrFromInwarDate = null;
                string StrToInwarDate = null;
                string StrFromOutwardDate = null;
                string StrToOutwardDate = null;

                if (DtpFromInwardDate.Checked == true && DtpToInwardDate.Checked == true)
                {
                    StrFromInwarDate = Val.SqlDate(DtpFromInwardDate.Value.ToShortDateString());
                    StrToInwarDate = Val.SqlDate(DtpToInwardDate.Value.ToShortDateString());
                }
                if (DtpFromOutwardDate.Checked == true && DtpToOutwardDate.Checked == true)
                {
                    StrFromOutwardDate = Val.SqlDate(DtpFromOutwardDate.Value.ToShortDateString());
                    StrToOutwardDate = Val.SqlDate(DtpToOutwardDate.Value.ToShortDateString());
                }


                DataSet DS = ObjKapan.FillSummaryForOutward(txtSearchInwardNo.Text,
                                txtSearchKapanName.Text,
                                StrFromInwarDate,
                               StrToInwarDate,
                               StrFromOutwardDate,
                               StrToOutwardDate,
                                StrStatus, Val.ToString(CmbPriceType.SelectedItem), Val.Trim(CmbYear.Properties.GetCheckedItems()), chkOutward.Checked);

                DataTable DTabSummury = DS.Tables[0];
                DataTable DTabKapan = DS.Tables[1];

                MainGrdSummury.DataSource = DTabSummury;
                MainGrdSummury.Refresh();

                MainGrdKapan.DataSource = DTabKapan;
                MainGrdKapan.Refresh();

                GrdKapan.Columns["GROUPCOLUMN"].Group();
                GrdKapan.Columns["GROUPCOLUMN"].Visible = false;

                if (GrdKapan.GroupSummary.Count == 0)
                {
                    GrdKapan.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "CLEAVERNAME", GrdKapan.Columns["CLEAVERNAME"], "{0:N0}");
                    GrdKapan.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "LOTCTS", GrdKapan.Columns["LOTCTS"], "{0:N3}");
                    GrdKapan.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ASSORTEDCARAT", GrdKapan.Columns["ASSORTEDCARAT"], "{0:N3}");
                }
                GrdKapan.ExpandAllGroups();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }

        }

        private void BtnExportSummary_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("InwardSummary", GrdSummury);
        }

        private void GrdSummury_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            string StrStatus = Val.ToString(GrdSummury.GetRowCellValue(e.RowHandle, "STATUS"));
            if (StrStatus == "PENDING")
            {
                e.Appearance.BackColor = lblPending.BackColor;
                e.Appearance.BackColor2 = lblPending.BackColor;
            }
            else if (StrStatus == "PARTIAL")
            {
                e.Appearance.BackColor = lblPartial.BackColor;
                e.Appearance.BackColor2 = lblPartial.BackColor;
            }
            else if (StrStatus == "OUTWARD")
            {
                e.Appearance.BackColor = lblOutwardKapan.BackColor;
                e.Appearance.BackColor2 = lblOutwardKapan.BackColor;
            }
            else if (StrStatus == "COMPLETE")
            {
                e.Appearance.BackColor = Color.Transparent;
                e.Appearance.BackColor2 = Color.Transparent;
            }
        }

        private void RepBtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ParcelKapanInwardProperty Property = new ParcelKapanInwardProperty();

                Property.KAPANNAME = Val.ToString(GrdSummury.GetFocusedRowCellValue("KAPANNAME"));
                Property.OUTWARDDATE = Val.ToString(GrdSummury.GetFocusedRowCellValue("OUTWARDDATE"));

                Property = ObjKapan.UpdateOutwardDate(Property);

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    GrdSummury.RefreshData();
                }

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void chkOutward_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkOutward.Checked == true)
                {
                    DtpFromOutwardDate.Enabled = true;
                    DtpToOutwardDate.Enabled = true;
                }
                else
                {
                    DtpFromOutwardDate.Enabled = false;
                    DtpToOutwardDate.Enabled = false;
                }
                
            }
            catch(Exception Ex)
            {

            }
        }
    }
}
