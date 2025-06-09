using AxoneMFGRJ.Utility;
using BusLib.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections;
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

namespace AxoneMFGRJ.Transaction
{
	public partial class FrmPolishOkPacketUpdate : Form
	{
		AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
		AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
		DataTable DtabData = new DataTable();
		DataTable DTabVerfied = new DataTable();
		BOTRN_PolishOkPacketUpdate Obj = new BOTRN_PolishOkPacketUpdate();
		BODevGridSelection ObjGridSelection;

		public FrmPolishOkPacketUpdate()
		{
			InitializeComponent();
		}

		public void ShowForm()
		{
			Val.FormGeneralSetting(this);
			AttachFormEvents();

			ObjGridSelection = new BODevGridSelection();
			ObjGridSelection.View = GrdDet;
			ObjGridSelection.ClearSelection();
			ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
			ObjGridSelection.CheckMarkColumn.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			DtpDate.Focus();
			this.Show();
		}
		public void AttachFormEvents()
		{
			ObjFormEvent.mForm = this;
			//objBOFormEvents.FormKeyDown = true;
			ObjFormEvent.FormKeyPress = true;
			ObjFormEvent.FormResize = true;
			ObjFormEvent.FormClosing = true;
			ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
			ObjFormEvent.ObjToDisposeList.Add(Val);
		}

		private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect)
		{
			DataTable resultTable = new DataTable();
			try
			{
				if (view.RowCount <= 0)
				{
					return null;
				}
				ArrayList aryLst = new ArrayList();



				DataTable sourceTable = null;
				sourceTable = ((DataView)view.DataSource).Table;

				if (IsSelect)
				{
					aryLst = ObjGridSelection.GetSelectedArrayList();
					resultTable = sourceTable.Clone();
					for (int i = 0; i < aryLst.Count; i++)
					{
						DataRowView oDataRowView = aryLst[i] as DataRowView;
						resultTable.Rows.Add(oDataRowView.Row.ItemArray);
					}
				}

			}
			catch (Exception ex)
			{
				Global.Message(ex.Message);
			}

			return resultTable;
		}

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			DtabData = Obj.Fill(Val.SqlDate(DtpDate.Text));

			MainGrd.DataSource = DtabData;
			MainGrd.RefreshDataSource();

			GrdDet.BestFitColumns();
			this.Cursor = Cursors.Default;
		}

		private void BtnUpdate_Click(object sender, EventArgs e)
		{
			DTabVerfied = GetTableOfSelectedRows(GrdDet, true);

			if (DTabVerfied.Rows.Count <= 0)
			{
				Global.Message("PLEASE SELECT RECORDS THAT YOU WANT TO UPDATE..");
				return;
			}

			this.Cursor = Cursors.WaitCursor;

			string OrderDetailXML = string.Empty;
			DTabVerfied.TableName = "Table";
			using (StringWriter sw = new StringWriter())
			{
				DTabVerfied.WriteXml(sw);
				OrderDetailXML = sw.ToString();
			}

			string IntRes = Obj.UpdateData(OrderDetailXML);
			if (IntRes == "SUCCESS")
			{
				Global.Message("DATA UPDATE SUCCESSFULLY..");
				ObjGridSelection.ClearSelection();
			}
			else
			{
				Global.Message("ERROR IN UPDATE..");
				ObjGridSelection.ClearSelection();
			}

			this.Cursor = Cursors.Default;
			BtnSearch.PerformClick();
		}

		private void DtpDate_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{TAB}");
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
			TextBrick BrickTitleseller = e.Graph.DrawString("POLISH OK PACKET DETAIL", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

		private void BtnExport_Click(object sender, EventArgs e)
		{
			ObjGridSelection.CheckMarkColumn.Visible = false;
			
			try
			{
				SaveFileDialog svDialog = new SaveFileDialog();
				svDialog.DefaultExt = ".xlsx";
				svDialog.Title = "Export to Excel";
				svDialog.FileName = "PolishOkPacketDetail.xlsx";
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

					if (Global.Confirm("Do You Want To Open [Polish Ok Packet Detail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
					{
						System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
					}
				}
				svDialog.Dispose();
				svDialog = null;

				ObjGridSelection.CheckMarkColumn.Visible = true;

			}
			catch (Exception EX)
			{
				Global.Message(EX.Message);
				ObjGridSelection.CheckMarkColumn.Visible = true;
			}
		}
	}
}
