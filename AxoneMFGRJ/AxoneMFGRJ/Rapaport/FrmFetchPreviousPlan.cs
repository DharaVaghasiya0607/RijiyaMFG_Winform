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

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmFetchPreviousPlan : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        
        BOFindRap ObjRap = new BOFindRap();

        DataTable DTabPrediction = new DataTable();
        public DataTable DTabSelected = new DataTable();
        
        #region Constructor

        public FrmFetchPreviousPlan()
        {
            InitializeComponent();
        }

        public void ShowForm(int pIntPrdTypeID,string pStrKapanname,int pIntSrNo , string pStrTag, string pStrPacketID,Int64 IntEmployeeID)
        {
            Val.FormGeneralSettingForPopup(this);
            AttachFormDefaultEvent();

            txtKapanName.Text = pStrKapanname;
            txtPacketNo.Text = pIntSrNo.ToString();
            txtTag.Text = pStrTag;

            DTabPrediction = ObjRap.GetPredictionDataFromPrevious(pIntPrdTypeID, pStrPacketID, IntEmployeeID);

            MainGrid.DataSource = DTabPrediction;
            GrdDet.RefreshData();
            GrdDet.BestFitColumns();

            GrdDet.Columns["PLANNO"].Group();
            GrdDet.ExpandAllGroups();

            if (GrdDet.GroupSummary.Count == 0)
            {
                GrdDet.GroupSummary.Add(SummaryItemType.Count, "TAG", GrdDet.Columns["TAG"], "{0:N0}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "CARAT", GrdDet.Columns["CARAT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "AMOUNT", GrdDet.Columns["AMOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "AMOUNTDISCOUNT", GrdDet.Columns["AMOUNTDISCOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "DISCOUNT", GrdDet.Columns["DISCOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "RAPAPORT", GrdDet.Columns["RAPAPORT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "PRICEPERCARAT", GrdDet.Columns["PRICEPERCARAT"], "{0:N3}");

                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "GCARAT", GrdDet.Columns["GCARAT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "GAMOUNT", GrdDet.Columns["GAMOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "GAMOUNTDISCOUNT", GrdDet.Columns["GAMOUNTDISCOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "GDISCOUNT", GrdDet.Columns["GDISCOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "GRAPAPORT", GrdDet.Columns["GRAPAPORT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "GPRICEPERCARAT", GrdDet.Columns["GPRICEPERCARAT"], "{0:N3}");

                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "DOWNCOLORAMOUNT", GrdDet.Columns["DOWNCOLORAMOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "UPCOLORAMOUNT", GrdDet.Columns["UPCOLORAMOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "DOWNCLARITYAMOUNT", GrdDet.Columns["DOWNCLARITYAMOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "UPCLARITYAMOUNT", GrdDet.Columns["UPCLARITYAMOUNT"], "{0:N3}");
            }
            
            this.ShowDialog();
           
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

            ObjFormEvent.ObjToDisposeList.Add(ObjRap);

            ObjFormEvent.ObjToDisposeList.Add(DTabPrediction);

        }

        #endregion

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                string StrPlanNo = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PLANNO"));
                string StrPrdID = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PRD_ID"));
                string StrEmpCode = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "EMPLOYEECODE"));

                if (Global.Confirm("Are You Sure TO Pick Plan No : " + StrPlanNo + " Of Emp : "+StrEmpCode) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DataRow[] UDrow = DTabPrediction.Select("Prd_ID = '" + StrPrdID + "' And PlanNo = '" + StrPlanNo + "' And EmployeeCode = '" + StrEmpCode + "'");

                if (UDrow != null || UDrow.Length != 0)
                {
                    DTabSelected = new DataTable();
                    DTabSelected = UDrow.CopyToDataTable();
                }
                else
                {
                    DTabSelected = null;
                }

                this.Close();
            }

            
        }

        private void GrdDet_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                GrdDet.MakeRowVisible(e.RowHandle, true);
                return;
            }

            bool BoolFinal = Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "ISFINAL"));
            //if (BoolFinal == false)
            //{
            //    if (PlanNo % 2 == 0)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(230, 230, 230);
            //    }
            //    else
            //    {
            //        e.Appearance.BackColor = Color.Transparent;
            //    }  
            //}
            //else
            if (BoolFinal == true)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 224, 220);
            }

        }


    }

}