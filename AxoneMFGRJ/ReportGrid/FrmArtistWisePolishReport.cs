using BusLib.Configuration;
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

using System.Reflection;
using DevExpress.Data;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;
using BusLib.Rapaport;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using BusLib.View;
using OfficeOpenXml;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;
using System.Globalization;
using DevExpress.XtraPrinting;
using DevExpress.Data.PivotGrid;
using System.IO;
using System.Collections;

namespace AxoneMFGRJ.ReportGrid
{
	public partial class FrmArtistWisePolishReport : Form
	{
		AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
		DataTable DtabDet = new DataTable();
		DataTable DtabDetail = new DataTable();

		BOTRN_ArtistWisePolishReport ObjArt = new BOTRN_ArtistWisePolishReport();

		public FrmArtistWisePolishReport()
		{
			InitializeComponent();
		}

		public void ShowForm()
		{
			this.Show();
			AttachFormDefaultEvent();

			int St = DateTime.Now.Day;
			St = St - 1;

			DTPFromDate.Value = DateTime.Now.AddDays(-St);
			DTPToDate.Value = DateTime.Now;

			DataTable DtabEmp = ObjArt.FindEmployee();
			DataTable DtabManager = ObjArt.FindManager();

			DataView view = new DataView(DtabEmp);
			DataTable distinctValues = view.ToTable(true, "EMPCODE");

			DataView view2 = new DataView(DtabManager);
			DataTable managerValues = view2.ToTable(true, "MANAGERCODE");

			CmbEmployee.Properties.DataSource = distinctValues;
			CmbEmployee.Properties.DisplayMember = "EMPCODE";
			CmbEmployee.Properties.ValueMember = "EMPCODE";

			CmbManager.Properties.DataSource = managerValues;
			CmbManager.Properties.DisplayMember = "MANAGERCODE";
			CmbManager.Properties.ValueMember = "MANAGERCODE";
			//DTPFromDate.Focus();
		}

		private void AttachFormDefaultEvent()
		{
			Val.FormGeneralSetting(this);
			ObjFormEvent.mForm = this;
		//	ObjFormEvent.FormKeyDown = true;
			ObjFormEvent.FormKeyPress = true;
			ObjFormEvent.FormClosing = true;
			ObjFormEvent.ObjToDisposeList.Add(ObjArt);
			ObjFormEvent.ObjToDisposeList.Add(Val);
			ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
		}

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			string StrEmpCode = Val.Trim(CmbEmployee.Properties.GetCheckedItems());
			string StrManagerCode = Val.Trim(CmbManager.Properties.GetCheckedItems());

			DtabDet = ObjArt.GetDataOfArtistWiseReport(Val.SqlDate(DTPFromDate.Text), Val.SqlDate(DTPToDate.Text), StrEmpCode, StrManagerCode);

			MainGrd.DataSource = DtabDet;
			MainGrd.RefreshData();

			

			this.Cursor = Cursors.Default;

		}

		private void MainGrd_CustomDrawFieldValue(object sender, DevExpress.XtraPivotGrid.PivotCustomDrawFieldValueEventArgs e)
		{
			DevExpress.XtraPivotGrid.PivotGridControl pPivotGrid = (DevExpress.XtraPivotGrid.PivotGridControl)sender;
			PropertyInfo pi = typeof(PivotCustomDrawFieldValueEventArgs).GetProperty("FieldCellViewInfo", (BindingFlags.NonPublic | BindingFlags.Instance));
			DevExpress.XtraPivotGrid.ViewInfo.PivotFieldsAreaCellViewInfo viewInfo = ((DevExpress.XtraPivotGrid.ViewInfo.PivotFieldsAreaCellViewInfo)(pi.GetValue(e, null)));
			if (
				(
					(
						(viewInfo.Item.Area == PivotArea.RowArea)
						&&
							(
								(viewInfo.MinLastLevelIndex <= pPivotGrid.Cells.FocusedCell.Y)
								&&
								(viewInfo.MaxLastLevelIndex >= pPivotGrid.Cells.FocusedCell.Y)
							)
					)
					||
					(
						(viewInfo.Item.Area == PivotArea.ColumnArea)
						&&
							(
								(viewInfo.MinLastLevelIndex <= pPivotGrid.Cells.FocusedCell.X)
								&&
								(viewInfo.MaxLastLevelIndex >= pPivotGrid.Cells.FocusedCell.X)
							)
					)
				  )
				)
			{ }
			else
			{
				e.Appearance.ForeColor = Color.Black;
			}
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			DTPFromDate.Value = DateTime.Now.AddMonths(-1);
			DTPToDate.Value = DateTime.Now;

			CmbEmployee.Properties.Items.Clear();
			CmbEmployee.Text = "";

			CmbManager.Properties.Items.Clear();
			CmbManager.Text = "";
			
			DtabDet.Clear();

			DtabDetail.Clear();

			DataTable DtabEmp = ObjArt.FindEmployee();
			DataTable DtabManager = ObjArt.FindManager();

			DataView view = new DataView(DtabEmp);
			DataTable distinctValues = view.ToTable(true, "EMPCODE");

			DataView view2 = new DataView(DtabManager);
			DataTable managerValues = view2.ToTable(true, "MANAGERCODE");

			CmbEmployee.Properties.DataSource = distinctValues;
			CmbEmployee.Properties.DisplayMember = "EMPCODE";
			CmbEmployee.Properties.ValueMember = "EMPCODE";

			CmbManager.Properties.DataSource = managerValues;
			CmbManager.Properties.DisplayMember = "MANAGERCODE";
			CmbManager.Properties.ValueMember = "MANAGERCODE";

			DTPFromDate.Focus();
		}

		private void BtnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void MainGrd_CellDoubleClick(object sender, PivotCellEventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;

				PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
				string StrEmpCode = Val.ToString(ds.GetValue(0, "EMPCODE"));
				string StrEntDate = Val.ToString(ds.GetValue(0, "DATE"));

				string StrDate = "";

				if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
				{
					StrDate = null;
				}
				else
				{
					StrDate = Val.SqlDate(Val.ToString(ds.GetValue(0, "DATE")));
				}

				DtabDetail = ObjArt.GetDetailOfArtistWiseReport(StrEmpCode, StrDate, Val.SqlDate(DTPFromDate.Text), Val.SqlDate(DTPToDate.Text));

				MainGrdDetail.DataSource = DtabDetail;
				MainGrdDetail.RefreshDataSource();
				GrdDetail.BestFitColumns();

				this.Cursor = Cursors.Default;

			}
			catch (Exception EX)
			{
				Global.Message(EX.Message);
			}
		}

		#region Excel-Export And Print

		private void LblExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog svDialog = new SaveFileDialog();
			svDialog.DefaultExt = "xlsx";
			svDialog.Title = "Export to Excel";
			svDialog.FileName = "Artist Wise Polish Report Summry";
			svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
			if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
			{
				string Filepath = svDialog.FileName;
				MainGrd.ExportToXlsx(Filepath);

				if (Global.Confirm("Do You Want To Open [" + "Artist Wise Polish Report Summry" + "] ?") == System.Windows.Forms.DialogResult.Yes)
				{
					System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
				}

			}

			//Global.ExcelExport("Artist Wise Polish Report", MainGrd);
		}

		private void LblPrint_Click(object sender, EventArgs e)
		{
			try
			{
				DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

				PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

				//string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

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

		private void LblExportDetail_Click(object sender, EventArgs e)
		{
			DataTable DTABEXCEL = DtabDetail;

			object misValue = System.Reflection.Missing.Value;
			SaveFileDialog svDialog = new SaveFileDialog();
			svDialog.DefaultExt = "xlsx";
			svDialog.Title = "Export to Excel";
			svDialog.FileName = "Artist Wise Polish Report Detail.xlsx";
			svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";



			if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
			{
				string StrFilePath = svDialog.FileName;

				if (File.Exists(StrFilePath))
				{
					File.Delete(StrFilePath);
				}

				FileInfo workBook = new FileInfo(StrFilePath);
				Color BackColor = Color.LightGray;
				Color FontColor = Color.Black;
				string FontName = "Verdana";
				float FontSize = 8;

				int StartRow = 0;
				int StartColumn = 0;
				int EndRow = 0;
				int EndColumn = 0;

				using (ExcelPackage xlPackage = new ExcelPackage(workBook))
				{
					ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Result_" + DateTime.Now.ToString("ddMMyyyy"));

					StartRow = 1;
					EndRow = StartRow + DTABEXCEL.Rows.Count;
					StartColumn = 1;
					EndColumn = DTABEXCEL.Columns.Count;


					worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].LoadFromDataTable(DTABEXCEL, true);
					worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
					worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
					worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


					//worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
					//worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
					//worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
					//worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
					worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = FontName;
					worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = FontSize;

					worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
					worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
					worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
					worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

					worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
					worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.PatternColor.SetColor(BackColor);
					worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.BackgroundColor.SetColor(BackColor);
					worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Font.Color.SetColor(FontColor);


					worksheet.Cells["A:Z"].AutoFitColumns();
					xlPackage.Save();

					if (Global.Confirm("Do You Want To Open [Artist Wise Polish Report Detail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
					{
						System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
					}
				}
				svDialog.Dispose();
				svDialog = null;
			}
		}

		private void LblDetailPrint_Click(object sender, EventArgs e)
		{
			try
			{

				DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

				PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

				//string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

				link.Component = MainGrdDetail;
				link.Landscape = true;

				link.Margins.Left = 10;
				link.Margins.Right = 10;
				link.Margins.Bottom = 40;
				link.Margins.Top = 130;

				link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary2);
				link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterAreaSummary2);
				link.CreateDocument();
				link.ShowPreview();
				link.PrintDlg();


			}
			catch (Exception ex)
			{
				Global.Message(ex.Message.ToString());
			}
		}
		public void Link_CreateMarginalHeaderAreaSummary2(object sender, CreateAreaEventArgs e)
		{
			// ' For Report Title

			TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
			BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
			BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
			BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

			// ' For Group 
			TextBrick BrickTitleseller = e.Graph.DrawString("Employee Wise Split Report Detail", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

		public void Link_CreateMarginalFooterAreaSummary2(object sender, CreateAreaEventArgs e)
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

		public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
		{
			// ' For Report Title

			TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
			BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
			BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
			BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

			// ' For Group 
			TextBrick BrickTitleseller = e.Graph.DrawString("Employee Wise Split Report", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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
		#endregion

		#region Enter Event
		private void FrmArtistWisePolishReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
			{
				BtnSearch.PerformClick();
			}
		}

		private void DTPFromDate_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void DTPToDate_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void CmbEmployee_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{TAB}");
			}
		}
		#endregion

		private void MainGrd_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
		{

			if (e.Field.FieldName == "EMPCODE")
            {
                if (e.Value1 == null || e.Value2 == null) return;
                e.Handled = true;
				int s1 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "FEmpInt"));
				int s2 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "FEmpInt"));
                e.Result = Comparer.Default.Compare(s1, s2);
                e.Handled = true;
            }
		}
	}
}
