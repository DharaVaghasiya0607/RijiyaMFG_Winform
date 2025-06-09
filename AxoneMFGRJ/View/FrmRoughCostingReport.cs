using BusLib.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.View
{
    public partial class FrmRoughCostingReport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        DataTable DTabPurchase = new DataTable();

        #region Constructor
        public FrmRoughCostingReport()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

        }
        private void AttachFormDefaultEvent()
        {
            this.KeyPreview = true;
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = false;
            ObjFormEvent.FormClosing = true;
         //   ObjFormEvent.ObjToDisposeList.Add(ObjRejection);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }
        #endregion


        private void FrmPurchaseCostingReport_Load(object sender, EventArgs e)
        {

          

        }

        private void MainGridDetail_Click(object sender, EventArgs e)
        {

        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFromDate = null;
                string StrToDate = null;

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                this.Cursor = Cursors.WaitCursor;
              //  GrdDetDetail.BeginUpdate();
                DTabPurchase = ObjMast.GetRoughCostingReport(StrFromDate, StrToDate);
                MainGridDetail.DataSource = DTabPurchase;
                GrdDetDetail.BestFitColumns();

               // MainGridDetail.Refresh();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }

        private void GrdDetDetail_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string MergeOnStr = "PARTYNAME,ENTRYNO,CARAT,RATE,AMOUNT";
            string MergeOn = "PARTYINVOICENO";

            if (MergeOnStr.Contains(e.Column.FieldName))
            {
                string val1 = Val.ToString(GrdDetDetail.GetRowCellValue(e.RowHandle1, GrdDetDetail.Columns[MergeOn]));
                string val2 = Val.ToString(GrdDetDetail.GetRowCellValue(e.RowHandle2, GrdDetDetail.Columns[MergeOn]));
                if (val1 == val2)
                    e.Merge = true;
                e.Handled = true;
            }

        }
    }
}
