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

            DtpFromDate.Value = DateTime.Now.AddMonths(-1);
            DtpToDate.Value = DateTime.Now;
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

                DataSet DS = ObjKapan.FillSummaryForOutward(txtSearchInwardNo.Text,
                                txtSearchKapanName.Text,
                                Val.SqlDate(DtpFromDate.Value.ToShortDateString()),
                                Val.SqlDate(DtpToDate.Value.ToShortDateString()),
                                StrStatus);

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
    }
}
