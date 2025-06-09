using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.Transaction;
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
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using BusLib.Master;
using AxoneMFGRJ.Transaction;
using BusLib.ReportGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Reflection;
using DevExpress.Data;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;
using BusLib.Rapaport;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmArtistPredictionList : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        DataTable DTabArtistPrediction = new DataTable();

        double DouTotal = 0;

        #region Property Settings

        public FrmArtistPredictionList()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            DataTable DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);
            DTabPrdType.DefaultView.Sort = "SEQUENCENo";
            DTabPrdType = DTabPrdType.DefaultView.ToTable();

            CmbPrdType.Properties.DataSource = DTabPrdType;
            CmbPrdType.Properties.DisplayMember = "PRDTYPENAME";
            CmbPrdType.Properties.ValueMember = "PRDTYPE_ID";

            //CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            //CmbKapan.Properties.DisplayMember = "KAPANNAME";
            //CmbKapan.Properties.ValueMember = "KAPANNAME";
            //CmbKapan.Focus();

            DTabArtistPrediction = ObjView.GetDataForArtistPrediction("NONE", 0, "", 0, "","","");


            MainGrd.DataSource = DTabArtistPrediction;
            MainGrd.Refresh();

            txtKapanName.Focus();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjView);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GrdDet.BeginUpdate();
                string StrKapan = Val.Trim(txtKapanName.Text);

                string StrPrdType = Val.Trim(CmbPrdType.Properties.GetCheckedItems());
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

                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

                this.Cursor = Cursors.WaitCursor;
                DTabArtistPrediction = ObjView.GetDataForArtistPrediction(StrKapan, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt64(txtEmployee.Tag), StrPrdType, StrFromDate, StrToDate);

                MainGrd.DataSource = DTabArtistPrediction;
                MainGrd.Refresh();

                GrdDet.EndUpdate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                return;
            }


        }


        #region Background Worker

        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] 
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }

        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "ArtistPredictionList";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrd ,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 200, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [Prediction.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }


        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("Artist Prediction List", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Packet :- " + Val.ToString(txtKapanName.Text) + "-" + txtPacketNo.Text +"-" + txtTag.Text, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (txtEmployee.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void BtnEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "EMPLOYEECODE";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                if (FrmSearch.mDRow != null)
                {
                    txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                    txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                }

                FrmSearch.Hide();
                FrmSearch.Dispose();
                FrmSearch = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow DRow = GrdDet.GetFocusedDataRow();

            string StrKapanName = Val.ToString(DRow["KAPANNAME"]);
            Int32 IntPacketNo = Val.ToInt32(DRow["PACKETNO"]);
            string StrMTag = Val.ToString(DRow["MTAG"]);
            string StrPrdType = Val.ToString(DRow["PRDTYPENAME"]);
            Int32 IntPlanNo = Val.ToInt32(DRow["PLANNO"]);

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                GrdDet.SetRowCellValue(IntI, "ISFINAL", false);
            }

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DD = GrdDet.GetDataRow(IntI);
                if (
                    DD != null &&
                    Val.ToString(DD["KAPANNAME"]) == Val.ToString(DRow["KAPANNAME"]) &&
                    Val.ToString(DD["PACKETNO"]) == Val.ToString(DRow["PACKETNO"]) &&
                    Val.ToString(DD["MTAG"]) == Val.ToString(DRow["MTAG"]) &&
                    Val.ToString(DD["PRDTYPENAME"]) == Val.ToString(DRow["PRDTYPENAME"]) &&
                    Val.ToString(DD["PLANNO"]) == Val.ToString(DRow["PLANNO"])
                    )
                {
                    GrdDet.SetRowCellValue(IntI, "ISFINAL", true);
                }
            }


        }

        private void GrdDetRowDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            bool BoolFinal = Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "ISFINAL"));
            bool BoolTFlag = Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "TFLAG"));
            bool BoolDFlag = Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "DFLAG"));
            bool BoolMaxFlag = Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "MAXAMTFLAG"));

            if (BoolMaxFlag == true)
            {
                e.Appearance.BackColor = lblSMFFlag.BackColor;
            }

            if (BoolTFlag == true)
            {
                e.Appearance.BackColor = lblTFlag.BackColor;
            }
            else if (BoolDFlag == true)
            {
                e.Appearance.BackColor = lblDFlag.BackColor;
            }
            else if (BoolFinal == true)
            {
                e.Appearance.BackColor = lblISFinal.BackColor;
            }


        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGrd) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGrd.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "EMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "EMPLOYEENAME")));
                    return;
                }

            }
            finally
            {
                e.Info = info;
            }
        }

        private void GrdDetRowDetail_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e) //Add : Pinali : 06-06-2019 For Merge value calculate Total Summary
        {
            try
            {

                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotal = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    double P1 = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "SUMAMOUNT"));
                    double P2 = Val.Val(GrdDet.GetRowCellValue(e.RowHandle - 1, "SUMAMOUNT"));

                    if (P1 != P2)
                    {
                        DouTotal = DouTotal + Val.Val(P1);
                    }

                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("SUMAMOUNT") == 0)
                    {
                        e.TotalValue = DouTotal;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtPacketNo.Text) == 0)
                {
                    txtKapanName.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    txtKapanName.Focus();
                    return;
                }

                if (Val.ISNumeric(txtTag.Text) == true)
                {
                    Char c = (Char)(64 + Val.ToInt(txtTag.Text));
                    txtTag.Text = c.ToString();
                }

                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                        && txtPacketNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
                        && txtTag.Text.Trim() == Val.ToString(DRow["TAG"]).Trim()
                        )
                    {
                        ISFind = true;
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtKapanName.Focus();
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataTable DtPacketWise = ObjView.GetDataForArtistPredictionPacketWise(Val.ToString(txtKapanName.Text), Val.ToInt(txtPacketNo.Text), txtTag.Text);
                    if (DtPacketWise == null || DtPacketWise.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;

                        Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        return;
                    }
                    else
                    {
                        for (int Icount = 0; Icount < DtPacketWise.Rows.Count; Icount++)
                        {
                            //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                            IEnumerable<DataRow> rowsNew = DTabArtistPrediction.Rows.Cast<DataRow>();
                            if (rowsNew.Where(s => Val.ToString(s["ID"]) == Val.ToString(DtPacketWise.Rows[Icount]["ID"])).Count() > 0)
                            {
                                this.Cursor = Cursors.Default;
                                Global.Message("This Packets Details Already Exists");
                                txtKapanName.Text = string.Empty;
                                txtPacketNo.Text = string.Empty;
                                txtTag.Text = string.Empty;
                                txtKapanName.Focus();
                                return;
                            }
                           //07-06-2019

                            DataRow DRNew = DTabArtistPrediction.NewRow();
                            foreach (DataColumn DCol in DTabArtistPrediction.Columns)
                            {
                                DRNew[DCol.ColumnName] = DtPacketWise.Rows[Icount][DCol.ColumnName];
                            }
                            DTabArtistPrediction.Rows.Add(DRNew);
                        }
                        
                    }
                    DtPacketWise = null;
                }
                GrdDet.RefreshData();

                GrdDet.BestFitMaxRowCount = 500;
                GrdDet.BestFitColumns();
                MainGrd.Refresh();

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;

                txtKapanName.Focus();

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
