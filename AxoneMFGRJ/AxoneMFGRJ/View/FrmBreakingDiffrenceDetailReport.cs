using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using System.IO;
using BusLib.Rapaport;
using AxoneMFGRJ.Utility;
using AxonDataLib;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using System.Linq;
using System.Collections;
using AxoneMFGRJ.Masters;
using BusLib.View;

namespace AxoneMFGRJ.View
{
    public partial class FrmBreakingDiffrenceDetailReport : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabPrediction = new DataTable();

        #region Constructor

        public FrmBreakingDiffrenceDetailReport()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName, int pIntPacketNo, int pIntBreakingType_ID, string pStrBreakingType,string pStrRapDate)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Text = pStrKapanName + "-" + pIntPacketNo.ToString() + " (" + pStrBreakingType + ")";

            txtKapanName.Text = pStrKapanName;
            txtPacketNo.Text = pIntPacketNo.ToString();

            txtBreakingType.Text = pStrBreakingType;
            txtBreakingType.Tag = pIntBreakingType_ID;

            lblRapDate.Text = Val.ToString(pStrRapDate);

            DTabPrediction = Obj.GetBreakingDiffReportDetail(pStrKapanName, pIntPacketNo, pIntBreakingType_ID);

            //DTabPrediction.DefaultView.Sort = "SRNO";

            GrdDet.BeginUpdate();

            

            MainGrid.DataSource = DTabPrediction;
            GrdDet.RefreshData();
            //GrdDet.BestFitColumns();
            GrdDet.ExpandAllGroups();
            GrdDet.EndUpdate();

            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "AMOUNT", GrdDet.Columns["AMOUNT"], "{0:N0}");

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
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(Obj);
            ObjFormEvent.ObjToDisposeList.Add(DTabPrediction);

        }

        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDet_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int IntISPCN = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "ISPCN"));
            if (IntISPCN == 1)
            {
                e.Appearance.BackColor = lblISPCN.BackColor;
            }
        }

        private void GrdDet_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            try
            {
                string MergeOnStr = "KAPANNAME,PACKETNO,BRK_ENTRYTYPE,REPORTTYPE";
                string MergeOn = "TOTALAMOUNT";

                if (MergeOnStr.Contains(e.Column.FieldName))
                {
                    string val1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle1, GrdDet.Columns[MergeOn]));
                    string val2 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle2, GrdDet.Columns[MergeOn]));
                    if (val1 == val2)
                        e.Merge = true;
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
    }

}