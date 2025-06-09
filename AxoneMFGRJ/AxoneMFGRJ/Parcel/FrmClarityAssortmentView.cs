using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using BusLib.Master;
using BusLib.Configuration;
using BusLib.TableName;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using OfficeOpenXml;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using BusLib;
using System.Collections;
using BusLib.Transaction;
using System.IO;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmClarityAssortmentView : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanInward ObjKapan = new BOTRN_KapanInward();
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        DataTable DTabAssort = new DataTable();
        DataTable DtabEverage = new DataTable();
        double DouRate = 0;
        double DouAmount = 0;
        double DouCarat = 0;

        public FrmClarityAssortmentView()
        {
            InitializeComponent();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = false;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjKapan);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            // ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            txtKapan.Focus();
            this.Show();

            

        }

        public void Calculate()
        {
            try
            {
                double DouCarat = 0;

                if (DtabEverage != null && DtabEverage.Rows.Count != 0)
                {
                    double DouTotalCarat = Val.Val(DtabEverage.Compute("SUM(CARAT)", ""));

                    double DouProposal = 0;

                    for (int IntI = 0; IntI < GridDetail.RowCount; IntI++)
                    {
                        DataRow DRow = GridDetail.GetDataRow(IntI);
                        DouCarat = DouCarat + Val.Val(DRow["CARAT"]);

                        if (DouTotalCarat != 0)
                        {
                            DouProposal = Math.Round((Val.Val(DRow["CARAT"]) / DouTotalCarat) * 100, 2);
                        }

                        GridDetail.SetRowCellValue(IntI, "PROPOSAL", DouProposal);
                       
                    }
                    DtabEverage.AcceptChanges();
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
                
                this.Cursor = Cursors.WaitCursor;

                DataSet DS = ObjKapan.GetClarityAssortmentData(Val.ToString(txtKapan.Text), Val.ToString(txtPriceDate.Text));

                DTabAssort = DS.Tables[0];
                DtabEverage = DS.Tables[1];

                pivotGrid.DataSource = DTabAssort;
                MainGridView.DataSource = DtabEverage;
                GridDetail.BestFitColumns();

                Calculate();
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Global.ExcelExport("Kapan Analysis", pivotGrid);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.PARCEL_KAPAN);

                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        //txtKapan.Tag = Val.ToString(FrmSearch.DRow["EMPLOYEE_ID"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                    BtnSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GridCerti_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
        }

        private void pivotGrid_CustomCellValue(object sender, DevExpress.XtraPivotGrid.PivotCellValueEventArgs e)//Gunjan:19/04/2023
        {
            try
            {
                double TCarat = 0, TAmount = 0;
                if (object.ReferenceEquals(e.DataField, PivoteColRate))
                {
                    TCarat = Val.ToDouble(e.GetCellValue(PivoteColCarat));
                    TAmount = Val.ToDouble(e.GetCellValue(PivoteColAmt));
                    e.Value = TAmount == 0 ? 0 : Math.Round(TAmount / TCarat, 2);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void txtPriceDate_KeyPress(object sender, KeyPressEventArgs e)//Gunjan:19/04/2023
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PRICEDATE,REMARK";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRICEHEAD);
                    FrmSearch.mColumnsToHide = "PRICE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPriceDate.Tag = Val.ToString(FrmSearch.mDRow["PRICE_ID"]);
                        txtPriceDate.Text = Val.ToString(FrmSearch.mDRow["PRICEDATE"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void pivotGrid_CustomFieldSort(object sender, DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs e)//Gunjan:20/04/2023
        {
            try
            {

                if (e.Field.FieldName == "MIXCLARITYNAME")
                {
                    if (e.Value1 == null || e.Value2 == null) return;
                    e.Handled = true;
                    int s1 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "CLARITYSEQNO"));
                    int s2 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "CLARITYSEQNO"));
                    e.Result = Comparer.Default.Compare(s1, s2);
                    e.Handled = true;
                }

                if (e.Field.FieldName == "SIZENAME")
                {
                    if (e.Value1 == null || e.Value2 == null) return;
                    e.Handled = true;
                    int s1 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "SEQUENCENO"));
                    int s2 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "SEQUENCENO"));
                    e.Result = Comparer.Default.Compare(s1, s2);
                    e.Handled = true;
                }

                if (e.Field.FieldName == "SHAPENAME")
                {
                    if (e.Value1 == null || e.Value2 == null) return;
                    e.Handled = true;
                    int s1 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "SHAPESEQNO"));
                    int s2 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "SHAPESEQNO"));
                    e.Result = Comparer.Default.Compare(s1, s2);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void pivotGrid_Click(object sender, EventArgs e)
        {

        }

        private void GridDetail_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouAmount = 0;

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GridDetail.GetRowCellValue(e.RowHandle, "CARAT"));
                    DouAmount = DouAmount + (Val.Val(GridDetail.GetRowCellValue(e.RowHandle, "AMOUNT")));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouAmount) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                  

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        

    }
}