using BusLib;
using BusLib.Configuration;
using BusLib.Transaction;
using DevExpress.Utils;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Config = BusLib.Configuration.BOConfiguration;


namespace AxoneMFGRJ.ReportGrid
{
	public partial class FrmArtistWisePendingReport : Form
	{
		public FrmArtistWisePendingReport()
		{
			InitializeComponent();
		}

		AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

		DataTable DtabDet = new DataTable();
		DataTable DtabDetail = new DataTable();
		BOTRN_ArtistWisePendingReport Obj = new BOTRN_ArtistWisePendingReport();

		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		string StrShape_ID = "";
		string StrColor_ID = "";
		string StrClarity_ID = "";
		string StrSize_ID = "";
		Int32 StrDepartment_ID = 0;

		public void ShowForm()
		{
			this.Show();
			AttachFormDefaultEvent();

			string StrDepartment = Config.gEmployeeProperty.DEPARTMENTNAME;
			Int64 StrDeptID = Config.gEmployeeProperty.DEPARTMENT_ID;

			FillControl();
			txtEmpCode.Enabled = false;

			TxtDepartment.Tag = StrDeptID;
			TxtDepartment.Text = StrDepartment;

			if (StrDepartment == "ADMIN" || StrDepartment == "MFG")
			{
				TxtDepartment.ReadOnly = false;
			}
			else
			{
				TxtDepartment.ReadOnly = true;
				TxtDepartment.Enabled = false;
			}

		}

		private void AttachFormDefaultEvent()
		{
			Val.FormGeneralSetting(this);
			ObjFormEvent.mForm = this;
			ObjFormEvent.FormKeyDown = true;
			ObjFormEvent.FormKeyPress = true;
			ObjFormEvent.FormClosing = true;
			ObjFormEvent.ObjToDisposeList.Add(Obj);
			ObjFormEvent.ObjToDisposeList.Add(Val);
			ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
		}

		#region Excel & Print
		private void lblDeptExport_Click(object sender, EventArgs e)
		{

			try
			{
				SaveFileDialog svDialog = new SaveFileDialog();
				svDialog.DefaultExt = ".xlsx";
				svDialog.Title = "Export to Excel";
				svDialog.FileName = "Artist Wise Pending Report Summry.xlsx";
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

					if (Global.Confirm("Do You Want To Open [Artist Wise Pending Report Summry.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

		private void lblDeptPrint_Click(object sender, EventArgs e)
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

		private void lblPacketExport_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog svDialog = new SaveFileDialog();
				svDialog.DefaultExt = ".xlsx";
				svDialog.Title = "Export to Excel";
				svDialog.FileName = "Artist Wise Pending Report Detail.xlsx";
				svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
				if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
				{
					GrdDet.Appearance.Row.Font = new Font("Verdana", 8.55f);
					GrdDet.Appearance.HeaderPanel.Font = new Font("Verdana", 9.5f);
					PrintableComponentLinkBase link = new PrintableComponentLinkBase()
					{
						PrintingSystemBase = new PrintingSystemBase(),
						Component = MainGridDetail,
						Landscape = true,
						PaperKind = PaperKind.A4,
						// Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
					};

					link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaDetail);

					link.ExportToXlsx(svDialog.FileName);

					if (Global.Confirm("Do You Want To Open [Artist Wise Pending Report Detail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

		private void lblPacketPrint_Click(object sender, EventArgs e)
		{
			try
			{

				DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

				PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

				//  string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

				link.Component = MainGridDetail;
				link.Landscape = true;

				link.Margins.Left = 10;
				link.Margins.Right = 10;
				link.Margins.Bottom = 40;
				link.Margins.Top = 130;

				link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaDetail);
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

		public void Link_CreateMarginalHeaderAreaDetail(object sender, CreateAreaEventArgs e)
		{
			// ' For Report Title
			TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
			BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
			BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
			BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

			// ' For Group 
			TextBrick BrickTitleseller = e.Graph.DrawString("ARTIST WISE PENDING REPORT DETAIL", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

		public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
		{
			// ' For Report Title
			TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
			BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
			BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
			BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

			// ' For Group 
			TextBrick BrickTitleseller = e.Graph.DrawString("ARTIST WISE PENDING REPORT SUMMRY", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

		private void FrmArtistWisePendingReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
			{
				BtnRefresh.PerformClick();
			}
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			string StrDepartment = Config.gEmployeeProperty.DEPARTMENTNAME;
			Int64 StrDeptID = Config.gEmployeeProperty.DEPARTMENT_ID;

			txtEmpCode.Text = "";
			txtEmpCode.Tag = "";

			PicEmpPhoto.Image = null;
			PicEmpPhoto.Update();

			PanelProgress.Visible = false;
			BtnRefresh.Enabled = true;

			TxtDepartment.Tag = StrDeptID;
			TxtDepartment.Text = StrDepartment;

			if (StrDepartment == "ADMIN" || StrDepartment == "MFG")
			{
				TxtDepartment.ReadOnly = false;
			}
			else
			{
				TxtDepartment.ReadOnly = true;
			}

			ChkCmbShape.Properties.Items.Clear();
			ChkCmbShape.Text = "";

			ChkCmbColor.Properties.Items.Clear();
			ChkCmbColor.Text = "";

			ChkCmbClarity.Properties.Items.Clear();
			ChkCmbClarity.Text = "";

			ChkCmbSize.Properties.Items.Clear();
			ChkCmbSize.Text = "";

			DtabDet.Clear();
			DtabDetail.Clear();

			FillControl();

			MainGrd.DataSource = DtabDet;
			MainGridDetail.DataSource = DtabDetail;

		}

		public void FillControl()
		{
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

		private void BtnRefresh_Click(object sender, EventArgs e)
		{
			try
			{

				//string StrShape_ID = "";
				//string StrColor_ID = "";
				//string StrClarity_ID = "";
				//string StrSize_ID = "";

				//StrShape_ID = Val.Trim(ChkCmbShape.Properties.GetCheckedItems());
				//StrColor_ID = Val.Trim(ChkCmbColor.Properties.GetCheckedItems());
				//StrClarity_ID = Val.Trim(ChkCmbClarity.Properties.GetCheckedItems());
				//StrSize_ID = Val.Trim(ChkCmbSize.Properties.GetCheckedItems());

				//DataSet DS = Obj.ArtistWiseGetData(Val.ToInt64(TxtDepartment.Tag), StrShape_ID, StrColor_ID, StrClarity_ID, StrSize_ID, Val.ToInt64(TxtDepartment.Tag));

				//DtabDet = DS.Tables[0];
				//DtabDetail = DS.Tables[1];

				//if (DtabDet.Rows.Count == 0 || DtabDetail.Rows.Count == 0)
				//{
				//	Global.Message("Data Not Found..");
				//}

				//MainGrd.DataSource = DtabDet;
				//MainGrd.RefreshDataSource();

				//MainGridDetail.DataSource = DtabDetail;
				//MainGridDetail.RefreshDataSource();

				//GrdDetDetail.Columns["EMPCODE"].ClearFilter();

				//GrdDet.BestFitColumns();
				//GrdDetDetail.BestFitColumns();

				

				StrShape_ID = Val.Trim(ChkCmbShape.Properties.GetCheckedItems());
				StrColor_ID = Val.Trim(ChkCmbColor.Properties.GetCheckedItems());
				StrClarity_ID = Val.Trim(ChkCmbClarity.Properties.GetCheckedItems());
				StrSize_ID = Val.Trim(ChkCmbSize.Properties.GetCheckedItems());
				StrDepartment_ID = Val.ToInt32(TxtDepartment.Tag);

				DtabDet.Clear();
				DtabDetail.Clear();

                //MainGrd.DataSource = DtabDet;
                //MainGrd.RefreshDataSource();

                //MainGridDetail.DataSource = DtabDetail;
                //MainGridDetail.RefreshDataSource();

				PanelProgress.Visible = true;
				BtnRefresh.Enabled = false;

				if(!backgroundWorker1.IsBusy)
				{
					backgroundWorker1.RunWorkerAsync();
				}

			}
			catch (Exception EX)
			{
				PanelProgress.Visible = false;
				BtnRefresh.Enabled = true;
				Global.Message(EX.Message);
			}
		}

		private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			try
			{

				if (e.FocusedRowHandle < 0)
				{
					return;
				}
				this.Cursor = Cursors.WaitCursor;

				this.Cursor = Cursors.WaitCursor;
				GrdDetDetail.Columns["EMPCODE"].ClearFilter();
				GrdDetDetail.Columns["EMPCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("EMPCODE='" + Val.ToString(GrdDet.GetFocusedRowCellValue("EMPCODE")) + "'");

                GrdDetDetail.Columns["TRANSDATE"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

				this.Cursor = Cursors.Default;

				DataRow DR = GrdDet.GetFocusedDataRow();
				txtEmpCode.Text = Val.ToString(DR["EMPCODE"]);

				byte[] OFFICELOGO = GrdDet.GetRowCellValue(e.FocusedRowHandle, "IMG") as byte[] ?? null;
				if (OFFICELOGO != null)
				{
					using (MemoryStream ms = new MemoryStream(OFFICELOGO))
					{
						PicEmpPhoto.Image = Image.FromStream(ms);
					}
				}
				else
				{
					PicEmpPhoto.Image = null;
				}

				this.Cursor = Cursors.Default;

			}
			catch (Exception ex)
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void TxtDepartment_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				string StrDepartment = Config.gEmployeeProperty.DEPARTMENTNAME;

				if (StrDepartment == "ADMIN" || StrDepartment == "MFG")
				{
					if (Global.OnKeyPressToOpenPopup(e))
					{
						FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
						FrmSearch.mSearchField = "DepartmentName";
						FrmSearch.mSearchText = e.KeyChar.ToString();
						this.Cursor = Cursors.WaitCursor;
						FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
						//FrmSearch.ColumnsToHide = "REASON_ID";
						this.Cursor = Cursors.Default;
						FrmSearch.ShowDialog();
						e.Handled = true;
						if (FrmSearch.mDRow != null)
						{
							TxtDepartment.Text = Val.ToString(FrmSearch.mDRow["DepartmentName"]);
							TxtDepartment.Tag = Val.ToString(FrmSearch.mDRow["Department_ID"]);
						}
						else
						{
							TxtDepartment.Text = string.Empty;
						}

						FrmSearch.Hide();
						FrmSearch.Dispose();
						FrmSearch = null;
					}
				}
				else
				{
					TxtDepartment.ReadOnly = true;
					TxtDepartment.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				Global.MessageError(ex.Message);
			}


		}

		private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
		{
			try
			{
				if (e.RowHandle > 0)
				{
					return;
				}
				this.Cursor = Cursors.WaitCursor;

				this.Cursor = Cursors.WaitCursor;
				GrdDetDetail.Columns["EMPCODE"].ClearFilter();
				GrdDetDetail.Columns["EMPCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("EMPCODE='" + Val.ToString(GrdDet.GetFocusedRowCellValue("EMPCODE")) + "'");
				this.Cursor = Cursors.Default;

				this.Cursor = Cursors.Default;

			}
			catch (Exception ex)
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void BtnBestFit_Click(object sender, EventArgs e)
		{
			GrdDetDetail.BestFitColumns();
			GrdDet.BestFitColumns();
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
				if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "EMPCODE")
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

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				
				DataSet DS = Obj.ArtistWiseGetData(StrShape_ID, StrColor_ID, StrClarity_ID, StrSize_ID, StrDepartment_ID);

				DtabDet = DS.Tables[0];
				DtabDetail = DS.Tables[1];
				
			}
			catch(Exception Ex)
			{
				PanelProgress.Visible = false;
				BtnRefresh.Enabled = true;
				Global.Message(Ex.Message);
			}

		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				PanelProgress.Visible = false;
				BtnRefresh.Enabled = true;

				//MainGrd.BeginUpdate();
				//MainGridDetail.BeginUpdate();

				MainGrd.DataSource = DtabDet;
				MainGrd.RefreshDataSource();

				MainGridDetail.DataSource = DtabDetail;
				MainGridDetail.RefreshDataSource();

				GrdDet.BestFitColumns();
				GrdDetDetail.BestFitColumns();

				//GrdDet.EndUpdate();
				//GrdDetDetail.EndUpdate();

				PanelProgress.Visible = false;
				BtnRefresh.Enabled = true;

				GrdDetDetail.Columns["EMPCODE"].ClearFilter();
			}
			catch (Exception ex)
			{
				PanelProgress.Visible = false;
				BtnRefresh.Enabled = true;
				Global.Message(ex.Message);
			}

		}
	}
}
