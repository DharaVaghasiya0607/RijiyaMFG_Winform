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
using BusLib.View;
using OfficeOpenXml;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Repository;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmMISAnalysisMakPolGrd : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_ProductionAnalysis ObjView = new BOTRN_ProductionAnalysis();

        DataTable DtMISAnalysis = new DataTable();
        bool IsExportPrint = false;


        #region Property Settings

        public FrmMISAnalysisMakPolGrd()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            FillControl();
            this.Show();
            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;
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

        public void FillControl()
        {
            ChklCmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            ChklCmbKapan.Properties.DisplayMember = "KAPANNAME";
            ChklCmbKapan.Properties.ValueMember = "KAPANNAME";

            ChkCmbShape.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_SHAPE);
            ChkCmbShape.Properties.DisplayMember = "SHAPENAME";
            ChkCmbShape.Properties.ValueMember = "SHAPE_ID";

            ChkCmbColor.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_COLOR);
            ChkCmbColor.Properties.DisplayMember = "COLORNAME";
            ChkCmbColor.Properties.ValueMember = "COLOR_ID";

            ChkCmbClarity.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_CLARITY);
            ChkCmbClarity.Properties.DisplayMember = "CLARITYNAME";
            ChkCmbClarity.Properties.ValueMember = "CLARITY_ID";

            ChkCmbSize.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_POLISHSIZE);
            ChkCmbSize.Properties.DisplayMember = "SIZENAME";
            ChkCmbSize.Properties.ValueMember = "SIZE_ID";



        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                string StrFromDate = null;
                string StrToDate = null;
                string StrKapan = "";
                string StrShape_ID = "";
                string StrColor_ID = "";
                string StrClarity_ID = "";
                string StrSize_ID = "";

                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                StrKapan = Val.Trim(ChklCmbKapan.Properties.GetCheckedItems());

                StrShape_ID = Val.Trim(ChkCmbShape.Properties.GetCheckedItems());
                StrColor_ID = Val.Trim(ChkCmbColor.Properties.GetCheckedItems());
                StrClarity_ID = Val.Trim(ChkCmbClarity.Properties.GetCheckedItems());
                StrSize_ID = Val.Trim(ChkCmbSize.Properties.GetCheckedItems());

                this.Cursor = Cursors.WaitCursor;
                DtMISAnalysis = ObjView.GetMISAnalysisMakPolGrdData("", StrFromDate, StrToDate, StrKapan, StrShape_ID, StrColor_ID, StrClarity_ID, StrSize_ID);

                if (DtMISAnalysis.Rows.Count > 0)
                {
                    MainGrid.DataSource = DtMISAnalysis;
                    MainGrid.Refresh();
                }
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
        private void GrdDet_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                int IntIsColor = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "ISCOLOR"));

                string StrCol1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PARAMNAME"));

                string StrCol2 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle - 1, "PARAMNAME"));


                // Draw Horizontal line ---------- //
                GridViewInfo vi2 = GrdDet.GetViewInfo() as GridViewInfo;
                for (int j = 0; j < vi2.RowsInfo.Count; j++)
                {
                    GridRowInfo ri = vi2.RowsInfo[j];
                    if (IntIsColor != 1)
                        e.Graphics.DrawLine(Pens.Black, new Point(ri.Bounds.Left, ri.Bounds.Bottom), new Point(ri.Bounds.Right, ri.Bounds.Bottom));
                }

                //if (!Val.ToString(StrCol1).Trim().Equals(string.Empty))
                if (IntIsColor == 0 || (IntIsColor == 3))
                {
                    GridViewInfo vi = GrdDet.GetViewInfo() as GridViewInfo;
                    {
                        Point p1 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right - 1, vi.ViewRects.Rows.Y);
                        Point p2 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right - 1, vi.ViewRects.Rows.Bottom);

                        Point p3 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Y);
                        Point p4 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Bottom);

                        Pen p = new Pen(Color.Black);
                        //e.Graphics.DrawRectangle(Pens.Black, e.Bounds);
                        e.Graphics.DrawLine(p, p1, p2);

                        //if (IntIsColor == 3)
                        //{
                        //    string str = Val.ToString(e.CellValue);
                        //    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 234, 221)), e.Bounds);
                        //}
                    }

                }

                else if (IntIsColor == 2)  //Draw Line For Makable/Polish/By/Lab Headers
                {
                    if (e.Column.FieldName.ToUpper().Contains("AMOUNTPER") || (IntIsColor == 2 && e.Column.FieldName == "PARAMNAME") || e.Column.FieldName == "MAKAMOUNT")
                    {
                        GridViewInfo vi = GrdDet.GetViewInfo() as GridViewInfo;
                        Point p1 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right - 1, vi.ViewRects.Rows.Y);
                        Point p2 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right - 1, vi.ViewRects.Rows.Bottom);

                        Point p3 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Y);
                        Point p4 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Bottom);

                        Pen p = new Pen(Color.Black);
                        e.Graphics.DrawLine(p, p1, p2);

                    }
                }
                else if (IntIsColor == 1)
                {
                    //GrdDet.OptionsView.ShowHorzLines= false;
                    GrdDet.OptionsView.ShowHorizontalLines = DefaultBoolean.False;
                }


                //int IntPolPcs = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "POLPCS"));
                //double DouPolPcsPer = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLPCSPER"));
                //if (e.Column.FieldName == "POLPCSPER" && Val.Val(DouPolPcsPer) != 0)
                //    e.CellValue = e.DisplayText = Val.ToString(IntPolPcs) + "/" + Val.ToString(DouPolPcsPer) + "%";

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                int IntIsColor = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "ISCOLOR"));

                string StrCol1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PARAMNAME"));

                if (IntIsColor == 3)
                {
                    e.Appearance.Font = new Font("Verdana", 8.25f, FontStyle.Bold);
                    e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                    //if(e.Column.FieldName.Contains("PER"))
                    //e.Appearance.BackColor = Color.FromArgb(238, 234, 221);
                }
                else if (IntIsColor == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(238, 234, 221);
                    e.Appearance.Font = new Font("Verdana", 8.65f, FontStyle.Bold | FontStyle.Italic);
                    e.Appearance.ForeColor = Color.Black;
                }
                if (StrCol1.Trim().Equals("TOTAL"))
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                }


                if (IntIsColor == 2)
                {
                    e.Appearance.Font = new Font("Verdana", 8.25f, FontStyle.Bold);
                    if (e.Column.FieldName.ToUpper().Contains("CARATPER"))
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    else e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                }


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                int IntIsColor = 0;

                //int IntIsColor =0; Val.ToInt32(GrdDet.GetRowCellValue(e..RowHandle, "ISCOLOR"));

                if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                {
                    e.DisplayText = String.Empty;
                }
                if (Val.ToString(e.Column.FieldName).ToUpper().Contains("PER"))
                {
                    if (IntIsColor == 2)
                    {
                        if (Val.ToString(e.Column.FieldName).ToUpper().Equals("POLCARATPER"))
                            e.DisplayText = "Polish";
                        else if (Val.ToString(e.Column.FieldName).ToUpper().Equals("BYCARATPER"))
                            e.DisplayText = "By";
                        else if (Val.ToString(e.Column.FieldName).ToUpper().Equals("LABCARATPER"))
                            e.DisplayText = "Lab";
                    }

                    if (IntIsColor == 3)
                    {
                        e.DisplayText = "%";
                    }
                    else if ((IntIsColor == 2 || IntIsColor == 1 || IntIsColor == 4))
                    {
                        if (IntIsColor == 2 && (e.Column.FieldName.ToUpper() == "POLCARATPER" || e.Column.FieldName.ToUpper() == "BYCARATPER" || e.Column.FieldName.ToUpper() == "LABCARATPER"))
                        {
                            //e.DisplayText = "";
                        }
                        else
                            e.DisplayText = "";
                    }
                }
                //int IntPolPcs = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "POLPCS"));
                //double DouPolPcsPer = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLPCSPER"));
                //if (e.Column.FieldName == "POLPCSPER" && Val.Val(DouPolPcsPer) != 0)
                //    e.DisplayText = Val.ToString(IntPolPcs) + "/" + Val.ToString(DouPolPcsPer) + "%";

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_CellMerge(object sender, CellMergeEventArgs e)
        {
            //GridView view = sender as GridView;
            //try
            //{
            //    string str1 = Val.ToString(e.Column);
            //    string str2 = Val.ToString(e.CellValue2);

            //    if (str1 != str2 && IntIsColor == 1)
            //    {

            //    }

            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message.ToString());
            //}
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "MISAnalysisReport.xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrid,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };
                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    DevExpress.XtraPrinting.XlsxExportOptions a = new DevExpress.XtraPrinting.XlsxExportOptions();
                    a.TextExportMode = TextExportMode.Text;

                    //link.ExportToXlsx(svDialog.FileName, a);

                    GrdDet.ExportToXlsx(svDialog.FileName, a);

                    if (Global.Confirm("Do You Want To Open [WorkerRollingReport.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                svDialog.Dispose();
                svDialog = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
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
            TextBrick BrickTitleseller = e.Graph.DrawString("MIS ANALYSIS REPORT", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        private void repPrgPolPcsPer_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                //e.DisplayText = Val.ToString(IntPolPcs) + "-" + Val.ToString(e.Value);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
    }
}
