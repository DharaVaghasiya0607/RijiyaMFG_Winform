using BusLib;
using BusLib.Configuration;
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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using AxoneMFGRJ.Transaction;
using AxoneMFGRJ.Utility;
using BusLib.Rapaport;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSerialNoGenerate : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOFindRap ObjRap = new BOFindRap();
     
        DataTable DtabPacket = new DataTable();
        DataTable  DtabStockDetail = new DataTable();
        String PasteData = "";
        IDataObject PasteclipData = Clipboard.GetDataObject();

        #region Property Settings

        public FrmSerialNoGenerate()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            BtnClear_Click(null, null);

			BtnMumbaiBarcodePrint.Enabled = false;
			BtnBprintMumbaiTSC.Enabled = false;
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
        }

        #endregion


        private void BtnGenerate_Click(object sender, EventArgs e)
        {

            //if (txtStoneCertiMFGMemo.Text.Trim().Length == 0)
            //{
            //    Global.Message("StoneNo / CertiNo / SerialNo / MemoNo Is Required");
            //    txtStoneCertiMFGMemo.Focus();
            //    return;
            //}
            this.Cursor = Cursors.WaitCursor;

            DtabStockDetail = ObjRap.GetSinglePrdSerialNoGenerateGetData("SEARCH",Val.SqlDate(DtpFromDate.Text), Val.SqlDate(DtpToDate.Text), Val.ToString(txtStoneCertiMFGMemo.Text).Trim(),"");
            
            MainGrid.DataSource = DtabStockDetail;
            MainGrid.Refresh();
            GrdDet.BestFitColumns();

            if (MainGrid.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new  BODevGridSelection();
                ObjGridSelection.View = GrdDet;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
           
            this.Cursor = Cursors.Default;
        }

        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect)
        {
            if (view.RowCount <= 0)
            {
                return null;
            }
            ArrayList aryLst = new ArrayList();


            DataTable resultTable = new DataTable();
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

            return resultTable;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }


        private void BtnBarcodePrintCurrEmp_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Barcode Print");
                return;
            }

            if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                //Global.BombayPrintBarcodePrint(DRow);
            }
            //foreach (DataRow DRow in DTab.Rows)
            //{

            //    LiveStockProperty Property = new LiveStockProperty();

            //    Global.BarcodePrint(Val.ToString(DRow["KAPANNAME"]),
            //        Val.ToString(DRow["PACKETNO"]),
            //        Val.ToString(DRow["TAG"]),
            //        Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
            //        Val.ToString(DRow["LOTCARAT"]),
            //        Val.ToString(DRow["EMPLOYEECODE"]));
            //}
        }

        private void txtStoneCertiMFGMemo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                IDataObject clipData = Clipboard.GetDataObject();
                String Data = Convert.ToString(clipData.GetData(System.Windows.Forms.DataFormats.Text));
                String str1 = Data.Replace("\r\n", ",");                   //data.Replace(\n, ",");
                str1 = str1.Trim();
                str1 = str1.TrimEnd();
                str1 = str1.TrimStart();
                str1 = str1.TrimEnd(',');
                str1 = str1.TrimStart(',');
                txtStoneCertiMFGMemo.Text = str1;
            }
            lblTotalCount.Text = "(" + txtStoneCertiMFGMemo.Text.Split(',').Length + ")";
        }

        private void txtStoneCertiMFGMemo_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtStoneCertiMFGMemo.Focus())
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    PasteData = Convert.ToString(PasteclipData.GetData(System.Windows.Forms.DataFormats.Text));
                }
            }
            lblTotalCount.Text = "(" + txtStoneCertiMFGMemo.Text.Split(',').Length + ")";
        }

        private void txtStoneCertiMFGMemo_TextChanged(object sender, EventArgs e)
        {
            if (txtStoneCertiMFGMemo.Text.Length > 0 && Convert.ToString(PasteData) != "")
            {
                txtStoneCertiMFGMemo.SelectAll();
                String str1 = PasteData.Replace("\r\n", ",");                   //data.Replace(\n, ",");
                str1 = str1.Trim();
                str1 = str1.TrimEnd();
                str1 = str1.TrimStart();
                str1 = str1.TrimEnd(',');
                str1 = str1.TrimStart(',');
                txtStoneCertiMFGMemo.Text = str1;
                PasteData = "";
            }

            lblTotalCount.Text = "(" + txtStoneCertiMFGMemo.Text.Split(',').Length + ")";

        }

        private void BtnCertificateBPrint_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Barcode Print");
                return;
            }

            if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                Global.CertificateBarcodePrint(DRow);
            }
            Global.Message("Print Successfully");
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGrid) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGrid.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "BILLPARTYCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "BILLPARTYNAME")));
                    return;
                } 
            }
            finally
            {
                e.Info = info;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtStoneCertiMFGMemo.Text = string.Empty;
            lblTotalCount.Text = "(0)";
            DtpFromDate.Text = DateTime.Now.ToString();
            DtpToDate.Text = DateTime.Now.ToString();
			ChkMumbaiBar.Checked = false;
			DtabStockDetail.Clear();
			MainGrid.RefreshDataSource();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(DtabStockDetail.Rows.Count <= 0)
                    return;

                this.Cursor = Cursors.WaitCursor;
                DtabStockDetail.TableName = "Table1";
                string SRDNoupdateDetailForXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DtabStockDetail.WriteXml(sw);
                    SRDNoupdateDetailForXML = sw.ToString();
                }
                DataTable DtabUpdateDetail = ObjRap.GetSinglePrdSerialNoGenerateGetData("UPDATE", Val.SqlDate(DtpFromDate.Text), Val.SqlDate(DtpToDate.Text), Val.ToString(txtStoneCertiMFGMemo.Text).Trim(), SRDNoupdateDetailForXML);

                if (DtabUpdateDetail.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (Val.ToString(DtabUpdateDetail.Rows[0]["RETURNTYPE"]) != "SUCCESS")
                {
                    Global.Message(Val.ToString(DtabUpdateDetail.Rows[0]["RETURNDESC"]));
                }
                else
                {
                    Global.Message(Val.ToString(DtabUpdateDetail.Rows[0]["RETURNDESC"]));
                    DtabStockDetail = DtabUpdateDetail.Copy();
                    DtabStockDetail.AcceptChanges();
                    MainGrid.DataSource = DtabStockDetail;
                    GrdDet.RefreshData();
                    GrdDet.BestFitColumns();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                Int32 ISAlreadyGenerated = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "ISNEWLYGENERATED"));

                if (ISAlreadyGenerated == 0)
                {
                    e.Appearance.BackColor = Color.LightGray;
                }

            }
            catch(Exception ex)
            {
            }
        }

		private void BtnMumbaiBarcodePrint_Click(object sender, EventArgs e)
		{
			try
			{
				DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

				if (DTab.Rows.Count == 0)
				{
					Global.Message("Please Select at least One Row For Barcode Print");
					return;
				}

				if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
				{
					return;
				}

				foreach (DataRow DRow in DTab.Rows)
				{
					Global.BombayPrintBarcodePrint(DRow);
				}
				Global.Message("Print Successfully");
			}
			catch(Exception ex)
			{
				Global.Message(ex.Message);
			}
		}

		private void BtnBprintMumbaiTSC_Click(object sender, EventArgs e)
		{
			try
			{
				DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

				if (DTab.Rows.Count == 0)
				{
					Global.Message("Please Select at least One Row For Barcode Print");
					return;
				}

				if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
				{
					return;
				}

				foreach (DataRow DRow in DTab.Rows)
				{
					Global.BombayPrintBarcodePrintTSC(DRow);
				}
				Global.Message("Print Successfully");
			}
			catch (Exception EX)
			{
				Global.Message(EX.Message);
			}
		}

		private void ChkMumbaiBar_CheckedChanged(object sender, EventArgs e)
		{
			if (ChkMumbaiBar.Checked == true)
			{
				BtnSearch.Visible = true;
				BtnMumbaiBarcodePrint.Enabled = true;
				BtnBprintMumbaiTSC.Enabled = true;
				BtnGenerate.Enabled = false;
				BtnUpdate.Enabled = false;
				BtnCertificateBPrint.Enabled = false;
				RtbLabFinalPrice.Visible = true;
				RtbLabinclusion.Visible = true;

				DtabStockDetail.Clear();
				MainGrid.RefreshDataSource();
			}
			else if(ChkMumbaiBar.Checked == false)
			{
				BtnSearch.Visible = false;
				BtnMumbaiBarcodePrint.Enabled = false;
				BtnBprintMumbaiTSC.Enabled = false;
				BtnGenerate.Enabled = true;
				BtnUpdate.Enabled = true;
				BtnCertificateBPrint.Enabled = true;

				RtbLabFinalPrice.Visible = false;
				RtbLabinclusion.Visible = false;

				DtabStockDetail.Clear();
				MainGrid.RefreshDataSource();
			}
		}

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;

				string FromDate;
				string ToDate;

				if (DtpFromDate.Checked == true)
				{
					FromDate = Val.SqlDate(DtpFromDate.Value.ToShortDateString());
				}
				else
				{
					FromDate = "";
				}

				if (DtpToDate.Checked == true)
				{
					ToDate = Val.SqlDate(DtpToDate.Value.ToShortDateString());
				}
				else
				{
					ToDate = "";
				}

				string str;

				if (RtbLabinclusion.Checked == true)
				{
					str = "LABINCLUSION";
				}
				else
				{
					str = "LABFINALPRICE";
				}

				DtabStockDetail = ObjRap.GetDataForMumbaiBarcodePrint(str, FromDate, ToDate, Val.ToString(txtStoneCertiMFGMemo.Text).Trim());

				MainGrid.DataSource = DtabStockDetail;
				MainGrid.Refresh();
				GrdDet.BestFitColumns();

				if (MainGrid.RepositoryItems.Count == 0)
				{
                    ObjGridSelection = new BODevGridSelection();
                    ObjGridSelection.View = GrdDet;
                    ObjGridSelection.ClearSelection();
                    ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
				}
				else
				{
                    ObjGridSelection.ClearSelection();
				}
				GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
                if (ObjGridSelection != null)
				{
                    ObjGridSelection.ClearSelection();
                    ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
				}

				this.Cursor = Cursors.Default;
			}
			catch (Exception ex)
			{
				Global.Message(ex.Message);
			}
		}
    }
}
