using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using BusLib.Transaction;
using AxoneMFGRJ.Utility;
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
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;
using System.Globalization;
using DevExpress.XtraPrinting;
using DevExpress.Data.PivotGrid;
using BusLib.Configuration;

namespace AxoneMFGRJ.ReportGrid
{
	public partial class FrmEmpWiseSplitReport : Form
	{
		AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion(); 
        
        DataTable DtabDet = new DataTable();
		DataTable DtabDetail = new DataTable();
		BOTRN_EmpWiseSplitReport ObjEmp = new BOTRN_EmpWiseSplitReport();
		



		public FrmEmpWiseSplitReport()
		{
			InitializeComponent();

			
			// Adding ItemSource to the Control
		}

		public void ShowForm()
		{
			this.Show();
			AttachFormDefaultEvent();
			
			int St = DateTime.Now.Day;

			St = St - 1;

			DTPFromDate.Value = DateTime.Now.AddDays(- St);
			DTPToDate.Value = DateTime.Now;

			//DTPFromDate.Focus();
		}

		private void AttachFormDefaultEvent()
		{
			Val.FormGeneralSetting(this);
			ObjFormEvent.mForm = this;
			ObjFormEvent.FormKeyDown = true;
			ObjFormEvent.FormKeyPress = true;
			ObjFormEvent.FormClosing = true;
			ObjFormEvent.ObjToDisposeList.Add(ObjEmp);
			ObjFormEvent.ObjToDisposeList.Add(Val);
			ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
		}

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			string StrEmpCode = Val.Trim(CmbEmployee.Properties.GetCheckedItems());

			DtabDet = ObjEmp.GetDataEmpWise(Val.SqlDate(DTPFromDate.Text), Val.SqlDate(DTPToDate.Text), StrEmpCode);	

			MainGrd.DataSource = DtabDet;
			MainGrd.RefreshData();

			DataView view = new DataView(DtabDet);
			DataTable distinctValues = view.ToTable(true, "EMPCODE");

			CmbEmployee.Properties.DataSource = distinctValues;
			CmbEmployee.Properties.DisplayMember = "EMPCODE";
			CmbEmployee.Properties.ValueMember = "EMPCODE";

			this.Cursor = Cursors.Default;


		}

		private void PvtGrdColor_CustomDrawFieldValue_1(object sender, PivotCustomDrawFieldValueEventArgs e)
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
			{
				// e.Appearance.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
				//e.Appearance.ForeColor = System.Drawing.Color.Red;
			}
			else
			{
				e.Appearance.ForeColor = Color.Black;
			}
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
				string StrEntDate = Val.ToString(ds.GetValue(0, "ENTDATE"));

				string StrDate = "";

					if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
					{
						StrDate = null;
					}
					else
					{
						StrDate = Val.SqlDate(Val.ToString(ds.GetValue(0, "ENTDATE")));
					}

				DtabDetail = ObjEmp.GetDetailOfEmp(StrEmpCode, StrDate, Val.SqlDate(DTPFromDate.Text), Val.SqlDate(DTPToDate.Text));

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

		private void MainGrd_Click(object sender, EventArgs e)
		{

		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			DTPFromDate.Value = DateTime.Now.AddMonths(-1);
			DTPToDate.Value = DateTime.Now;

			CmbEmployee.SelectedText = "";
			DtabDet.Clear();

			DtabDetail.Clear();

			DTPFromDate.Focus();
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

		private void lblExportSummary_Click(object sender, EventArgs e)
		{
			Global.ExcelExport("Emp Wise Split Report", MainGrd);
		}

		private void LblExportDetail_Click(object sender, EventArgs e)
		{
			Global.ExcelExport("Emp Wise Split Report Detail", GrdDetail);
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



		private void lblPrintSummary_Click(object sender, EventArgs e)
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

		private void FrmEmpWiseSplitReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
			{
				BtnSearch.PerformClick();
			}
		}
	}
}
