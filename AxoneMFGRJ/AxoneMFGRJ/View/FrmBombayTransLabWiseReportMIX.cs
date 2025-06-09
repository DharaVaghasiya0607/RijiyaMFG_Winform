using AxoneMFGRJ.Report;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.Data;
using BusLib.Rapaport;
using OfficeOpenXml;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;
using System.Globalization;
using DevExpress.Data.PivotGrid;


namespace AxoneMFGRJ.View
{
    public partial class FrmBombayTransLabWiseReportMIX : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabSummury = new DataTable();
		DataTable DtabDetail = new DataTable();

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        public FrmBombayTransLabWiseReportMIX()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

			DateTime DT = new DateTime(2020,11,19);
			DtpFromDate.Value = (DT);

			//DtpFromDate.Value = DateTime.Now.AddMonths(-1);
			DtpToDate.Value = DateTime.Now;
			GrdDet.BestFitColumns();
			GrdDetDetail.BestFitColumns();
        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }
        
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DTabSummury = Obj.GetDataForBombayTransferLabMix(Val.SqlDate(DtpFromDate.Value.ToShortDateString()), Val.SqlDate(DtpToDate.Value.ToShortDateString()));
                if (DTabSummury.Rows.Count <= 0)
                {
                    Global.Message("No Data Found");
					this.Cursor = Cursors.Default;
					return;
                }

                MainGrd.DataSource = DTabSummury;
                GrdDet.RefreshData();

				GrdDetDetail.BestFitColumns();
				GrdDet.BestFitColumns();
                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }

        }

        public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
        {

			// ' For Report Title

			TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
			BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
			BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
			BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

			// ' For Group 
			TextBrick BrickTitleseller = e.Graph.DrawString("BOMBAY TRANSFER WISE REPORT(MIX)", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

        public void Link_CreateMarginalFooterAreaSummary(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
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

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
			GrdDetDetail.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDet_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
			{
                if (e.Clicks == 2)
                {
					string StrDate = "";
					string StrToDate = "";

					DtabDetail.Rows.Clear();

					this.Cursor = Cursors.WaitCursor;

					DataRow DR = GrdDet.GetFocusedDataRow();
					var Index = GrdDet.FocusedRowHandle;

					var cellValue = GrdDet.GetRowCellValue(Index, "TRANSDATE");

					if (Val.ToString(cellValue) == Val.ToString("TOTAL"))
					{
						StrDate = Val.SqlDate(DtpFromDate.Value.ToShortDateString());
						StrToDate = Val.SqlDate(DtpToDate.Value.ToShortDateString());
					}
					else
					{
						 StrDate = Val.SqlDate(Val.ToString(DR["TRANSDATE"]));
						 StrToDate = Val.SqlDate(Val.ToString(DR["TRANSDATE"]));
					}


					DtabDetail = Obj.GetDataForBombayTransferLab_DetailMix(StrDate, StrToDate);

					MainGrdDetail.DataSource = DtabDetail;
					MainGrdDetail.RefreshDataSource();
					GrdDetDetail.BestFitColumns();
					GrdDet.BestFitColumns();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FrmMarkerRollingReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    BtnRefresh.PerformClick();
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
                if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "0 (0.000)")
                {
                    e.DisplayText = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
			try
			{
				SaveFileDialog svDialog = new SaveFileDialog();
				svDialog.DefaultExt = ".xlsx";
				svDialog.Title = "Export to Excel";
				svDialog.FileName = "Bombay Transfer Lab Wise Detail Report (GIA)";
				svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
				if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
				{
					PrintableComponentLinkBase link = new PrintableComponentLinkBase()
					{
						PrintingSystemBase = new PrintingSystemBase(),
						Component = MainGrdDetail,
						Landscape = true,
						PaperKind = PaperKind.A4,
						Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
					};

					link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);

					link.ExportToXls(svDialog.FileName);

					if (Global.Confirm("Do You Want To Open [Bombay Transfer Lab Wise Detail Report(GIA).xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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
			
			
			//DataTable DTABEXCEL = DtabDetail;

			//object misValue = System.Reflection.Missing.Value;
			//SaveFileDialog svDialog = new SaveFileDialog();
			//svDialog.DefaultExt = "xlsx";
			//svDialog.Title = "Export to Excel";
			//svDialog.FileName = "Bombay Trans Lab Report Detail(MIX).xlsx";
			//svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";



			//if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
			//{
			//	string StrFilePath = svDialog.FileName;

			//	if (File.Exists(StrFilePath))
			//	{
			//		File.Delete(StrFilePath);
			//	}

			//	FileInfo workBook = new FileInfo(StrFilePath);
			//	Color BackColor = Color.LightGray;
			//	Color FontColor = Color.Black;
			//	string FontName = "Verdana";
			//	float FontSize = 9;

			//	int StartRow = 0;
			//	int StartColumn = 0;
			//	int EndRow = 0;
			//	int EndColumn = 0;

			//	using (ExcelPackage xlPackage = new ExcelPackage(workBook))
			//	{
			//		ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Result_" + DateTime.Now.ToString("ddMMyyyy"));

			//		StartRow = 1;
			//		EndRow = StartRow + DTABEXCEL.Rows.Count;
			//		StartColumn = 1;
			//		EndColumn = DTABEXCEL.Columns.Count;


			//		worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].LoadFromDataTable(DTABEXCEL, true);
			//		worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
			//		worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
			//		worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


			//		//worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
			//		//worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
			//		//worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
			//		//worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
			//		worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = FontName;
			//		worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = FontSize;

			//		worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
			//		worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
			//		worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
			//		worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

			//		worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
			//		worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.PatternColor.SetColor(BackColor);
			//		worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.BackgroundColor.SetColor(BackColor);
			//		worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Font.Color.SetColor(FontColor);

			//		worksheet.Cells["A1:P1"].Style.Font.Bold = true;

			//		worksheet.Cells["A:Z"].AutoFitColumns();
			//		xlPackage.Save();

			//		if (Global.Confirm("Do You Want To Open [Prediction.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
			//		{
			//			System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
			//		}
			//	}
			//	svDialog.Dispose();
			//	svDialog = null;
			//}
        }

        private void lblDeptPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGrdDetail;
                link.Landscape = true;


                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

				link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary1);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }
		public void Link_CreateMarginalHeaderAreaSummary1(object sender, CreateAreaEventArgs e)
		{
			// ' For Report Title

			TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
			BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
			BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
			BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

			// ' For Group 
			TextBrick BrickTitleseller = e.Graph.DrawString("BOMBAY TRANSFER WISE REPORT DETAIL(MIX)", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void lblExportSummary_Click(object sender, EventArgs e)
        {

            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "Bombay Trans Wise Report(MIX).xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    GrdDet.Appearance.Row.Font = new Font("Verdana", 8.55f);
					GrdDet.Appearance.HeaderPanel.Font = new Font("Verdana", 9.5f);
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrd,
                        Landscape = false,
                        PaperKind = PaperKind.A4,
                       // Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);

                    link.ExportToXlsx(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [Bombay Trans Wise Report(MIX).xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                GrdDet.Appearance.Row.Font = new Font("Verdana", 8);
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }

        }

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

              //  string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

                link.Component = MainGrd;
                link.Landscape = true;

                link.Margins.Left = 10;
                link.Margins.Right = 10;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterAreaSummary);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        private void MainGrd_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                string StrTotal = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "TRANSDATE"));

                if (StrTotal.ToUpper() == "TOTAL")
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

		private void GrdDet_RowStyle(object sender, RowStyleEventArgs e)
		{
			//try
			//{
			//	string StrTotal = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "TRANSDATE"));

			//	if (StrTotal.ToUpper() == "TOTAL")
			//	{
			//		e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
			//		e.Appearance.ForeColor = Color.DarkBlue;
			//		e.Appearance.BackColor = Color.LightGray;
			//		e.Appearance.BackColor2 = Color.LightGray;
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Global.Message(ex.Message.ToString());
			//}
		}




    }
}
