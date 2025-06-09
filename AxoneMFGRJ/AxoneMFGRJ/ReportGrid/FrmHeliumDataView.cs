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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmHeliumDataView : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        DataTable DTabPredictionView = new DataTable();
        DataTable DTabPrdType = new DataTable();

     

        #region Property Settings

        public FrmHeliumDataView()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            

            DTabPredictionView.Columns.Add(new DataColumn("HEL_ID", typeof(Guid)));
            DTabPredictionView.Columns.Add(new DataColumn("PACKET_ID", typeof(Guid)));
            DTabPredictionView.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("PACKETTAG", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));
            DTabPredictionView.Columns.Add(new DataColumn("CODE", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("NAME", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("DEPT", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("PROCESS", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("ENTRYDATE", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("ENTRYCOMPNAME", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("ENTRYIP", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("CNT", typeof(Int32)));
            DTabPredictionView.Columns.Add(new DataColumn("SFLAG", typeof(bool)));
            
            DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.HEL_COLUMN);
            DTabPrdType.DefaultView.Sort = "ID";
            DTabPrdType = DTabPrdType.DefaultView.ToTable();
            
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                DTabPredictionView.Columns.Add(new DataColumn(Val.ToString(DRow["COLNAME"]), typeof(string))); 
            }

            GrdDet.BeginUpdate();

            MainGrid.DataSource = DTabPredictionView;
            MainGrid.Refresh();
            GrdDet.PopulateColumns();

            GrdDet.Bands.Clear();

            var gridBand = new GridBand();
            gridBand.Name = "BandGeneral";
            gridBand.Caption = "General";
            gridBand.Tag = "General";
            gridBand.RowCount = 1;
            gridBand.VisibleIndex = 0;
            GrdDet.Bands.Add(gridBand);

            GrdDet.Columns["HEL_ID"].OwnerBand = gridBand;
            GrdDet.Columns["PACKET_ID"].OwnerBand = gridBand;
            GrdDet.Columns["KAPANNAME"].OwnerBand = gridBand;
            GrdDet.Columns["PACKETTAG"].OwnerBand = gridBand;
            GrdDet.Columns["ISSUECARAT"].OwnerBand = gridBand;
            GrdDet.Columns["CODE"].OwnerBand = gridBand;
            GrdDet.Columns["NAME"].OwnerBand = gridBand;
            GrdDet.Columns["DEPT"].OwnerBand = gridBand;
            GrdDet.Columns["PROCESS"].OwnerBand = gridBand;
            GrdDet.Columns["ENTRYDATE"].OwnerBand = gridBand;
            GrdDet.Columns["ENTRYCOMPNAME"].OwnerBand = gridBand;
            GrdDet.Columns["ENTRYIP"].OwnerBand = gridBand;
            GrdDet.Columns["CNT"].OwnerBand = gridBand;
            GrdDet.Columns["SFLAG"].OwnerBand = gridBand;


            GrdDet.Columns["HEL_ID"].Caption  = "HelID";
            GrdDet.Columns["PACKET_ID"].Caption = "PktID";
            GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
            GrdDet.Columns["PACKETTAG"].Caption = "Pkt";
            GrdDet.Columns["ISSUECARAT"].Caption = "Cts";
            GrdDet.Columns["CODE"].Caption = "Code";
            GrdDet.Columns["NAME"].Caption = "Name";
            GrdDet.Columns["DEPT"].Caption = "Dept";
            GrdDet.Columns["PROCESS"].Caption = "Proc";
            GrdDet.Columns["ENTRYDATE"].Caption = "Entry Date";
            GrdDet.Columns["ENTRYCOMPNAME"].Caption = "Comp Name";
            GrdDet.Columns["ENTRYIP"].Caption = "Comp IP";
            GrdDet.Columns["CNT"].Caption = "Cnt";
            GrdDet.Columns["SFLAG"].Caption = "SFlg";

            GrdDet.Columns["HEL_ID"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["PACKET_ID"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["KAPANNAME"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["PACKETTAG"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["ISSUECARAT"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["CODE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["NAME"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["DEPT"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["PROCESS"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["ENTRYDATE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["ENTRYCOMPNAME"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["ENTRYIP"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["CNT"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            GrdDet.Columns["ENTRYDATE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            GrdDet.Columns["ENTRYDATE"].DisplayFormat.FormatString = "dd/MM/yy hh:mm:ss tt";

            GrdDet.Columns["ISSUECARAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            GrdDet.Columns["ENTRYDATE"].DisplayFormat.FormatString = "{0:N2}";

            GrdDet.Columns["ISSUECARAT"].Summary.Add(SummaryItemType.Sum, "ISSUECARAT", "{0:N2}");
            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "ISSUECARAT", GrdDet.Columns["ISSUECARAT"], "{0:N2}");

            GrdDet.Columns["HEL_ID"].Visible = false;
            GrdDet.Columns["PACKET_ID"].Visible = false;
            GrdDet.Columns["SFLAG"].Visible = false;

            var gridBandOther = new GridBand();
            gridBandOther.Name = "BandHelium";
            gridBandOther.Caption = "Helium";
            gridBandOther.Tag = "Helium";
            gridBandOther.RowCount = 1;
            gridBandOther.VisibleIndex = 1;
            GrdDet.Bands.Add(gridBandOther);

            try
            {
                foreach (DataRow DRow in DTabPrdType.Rows)
                {
                    string Col = Val.ToString(DRow["COLNAME"]);

                    GrdDet.Columns[Col].OwnerBand = gridBandOther;
                    GrdDet.Columns[Col].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col].Caption = Val.ProperText(Col).Replace("_", " ");
                }

                for (int i = 0; i < GrdDet.Columns.Count; i++)
                {
                    GrdDet.Columns[i].OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                }

                GrdDet.EndUpdate();
               
            }
            catch (Exception EX)
            {
                GrdDet.EndUpdate();
                Global.MessageError(EX.Message);
            }
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
                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

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

                string StrType = string.Empty;

                if (RbtLast.Checked == true)
                {
                    StrType = "LAST";
                }

                if (RbtALL.Checked == true)
                {
                    StrType = "ALL";
                }

                DataTable DTab = ObjView.HeliumGetDataView(StrType,
                    txtKapan.Text,
                    Val.ToInt(txtFromPacketNo.Text),
                    Val.ToInt(txtToPacketNo.Text),
                    txtTag.Text,
                    Val.ToInt64(txtEmployee.Tag),
                    StrFromDate,
                    StrToDate);

                GrdDet.BeginUpdate();

                DTabPredictionView.Rows.Clear();
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataRow DRNew = DTabPredictionView.NewRow();

                    DRNew["HEL_ID"] = Val.ToString(DRow["HEL_ID"]);
                    DRNew["PACKET_ID"] = Val.ToString(DRow["PACKET_ID"]);
                    DRNew["KAPANNAME"] = Val.ToString(DRow["KAPANNAME"]);
                    DRNew["PACKETTAG"] = Val.ToString(DRow["PACKETTAG"]);
                    DRNew["ISSUECARAT"] = Val.ToString(DRow["ISSUECARAT"]);
                    DRNew["CODE"] = Val.ToString(DRow["CODE"]);
                    DRNew["NAME"] = Val.ToString(DRow["NAME"]);
                    DRNew["DEPT"] = Val.ToString(DRow["DEPT"]);
                    DRNew["PROCESS"] = Val.ToString(DRow["PROCESS"]);
                    DRNew["ENTRYDATE"] = Val.ToString(DRow["ENTRYDATE"]);
                    DRNew["ENTRYCOMPNAME"] = Val.ToString(DRow["ENTRYCOMPNAME"]);
                    DRNew["ENTRYIP"] = Val.ToString(DRow["ENTRYIP"]);
                    DRNew["CNT"] = Val.ToString(DRow["CNT"]);
                    DRNew["SFLAG"] = Val.ToString(DRow["SFLAG"]);

                    foreach (DataRow DRowPrd in DTabPrdType.Rows)
                    {
                        string Col = Val.ToString(DRowPrd["COLNAME"]);
                        DRNew[Col] = Val.ToString(DRow[Col]);
                    }
                    DTabPredictionView.Rows.Add(DRNew);
                }

                GrdDet.RefreshData();
                GrdDet.BestFitColumns();

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
                svDialog.FileName = "HeliumData";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    //if (tabControl1.SelectedIndex == 0)
                    {
                        PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                        {
                            PrintingSystemBase = new PrintingSystemBase(),
                            Component = MainGrid,
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
            TextBrick BrickTitleseller = e.Graph.DrawString("Helium Data View", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Date From :- " + Val.Trim(DTPFromDate.Value.ToShortDateString()) + " To " + Val.Trim(DTPToDate.Value.ToShortDateString()), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
        }

        private void BtnExpandAll_Click(object sender, EventArgs e)
        {
            GrdDet.ExpandAllGroups();
        }

        private void BtnCollepsAll_Click(object sender, EventArgs e)
        {
            GrdDet.CollapseAllGroups();
        }

        private void MainGrid_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == info.BandsInfo.BandCount - 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
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

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void GrdDet_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (RbtALL.Checked == true)
            {
                if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "SFLAG")) == true)
                {
                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.LightGray;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.White;
                }    
            }
            
        }

    }
}
