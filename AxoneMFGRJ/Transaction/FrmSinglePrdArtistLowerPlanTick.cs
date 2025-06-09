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
using AxoneMFGRJ.Report;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSinglePrdArtistLowerPlanTick : DevExpress.XtraEditors.XtraForm
    {

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();

        DataTable DTabPacket = new DataTable();
        DataTable DTabArtistTickData = new DataTable();
        bool IsDownImage = true;

        string mStrParentFormType = "";

        #region Property Settings

        public FrmSinglePrdArtistLowerPlanTick()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DTabArtistTickData = ObjTrn.GetDataForArtistLowerPlanTick("PACKETINFO","", 0, "");
            MainGrid.DataSource = DTabArtistTickData;
            MainGrid.Refresh();

            CalculateSummary();

            txtKapanName.Focus();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            IsDownImage = false;
            BtnUpDown_Click(null, null);

            this.Show();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();

        }

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }

        }
        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {

                if (DTabArtistTickData.Rows.Count <= 0)
                {
                    Global.Message("No Data Found For Update..");
                    return;
                }

                if (Global.Confirm("Are You Sure to Tick Lowest Artist Plan..? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DTabArtistTickData.TableName = "Table1";
                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

                string StrArtistDetailXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabArtistTickData.WriteXml(sw);
                    StrArtistDetailXML = sw.ToString();
                }
                Property = ObjTrn.UpdateArtistLowestPlanTick(StrArtistDetailXML, Property);

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    this.Cursor = Cursors.Default;
                    //Global.Message("Lowest Artist Plan Locked Successfully.");
                    BtnClear_Click(null, null);
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    //Global.Message(Property.ReturnMessageDesc);
                    //Global.Message("Something Goes Wrong..Pls Check..");
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }
       
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRequiredProcess_Validated(object sender, EventArgs e)
        {
            BtnSave.Focus();
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
                        Global.Message("This Packet Is Already Selected.");
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtKapanName.Focus();
                        CalculateSummary();
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataTable DTabSelectedData = ObjTrn.GetDataForArtistLowerPlanTick("PACKETINFO", txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text);

                    if (DTabSelectedData.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtKapanName.Focus();
                        return;
                    }

                    DataRow DRow = DTabSelectedData.Rows[0];

                    if (DRow == null || Val.ToString(DRow["RETURNTYPE"]) != "SUCCESS")
                    {
                        this.Cursor = Cursors.Default;
                        //Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
                        Global.MessageError(Val.ToString(DRow["RETURNDESC"]));
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtKapanName.Focus();
                        return;
                    }
                    else
                    {
                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabArtistTickData.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtKapanName.Text = string.Empty;
                            txtPacketNo.Text = string.Empty;
                            txtTag.Text = string.Empty;
                            txtKapanName.Focus();
                            return;
                        }
                        // 07-06-2019

                        DataRow DRNew = DTabArtistTickData.NewRow();
                        foreach (DataColumn DCol in DTabArtistTickData.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }

                        DTabArtistTickData.Rows.Add(DRNew);
                    }
                    DRow = null;
                }
                DTabArtistTickData.AcceptChanges();
                GrdDet.RefreshData();
                //GrdDet.BestFitMaxRowCount = 500;
                //GrdDet.BestFitColumns();
                MainGrid.Refresh();

                CalculateSummary();

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtKapanName.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtFinalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjTrn.GetDataForArtistLowerPlanTick("EMPINFO",Val.ToString(DRow["KAPANNAME"]), Val.ToInt(DRow["PACKETNO"]), Val.ToString(DRow["TAG"]));
                    FrmSearch.mColumnsToHide = "KAPANNAME,PACKETNO,TAG,KAPAN_ID,PACKET_ID,EMPLOYEE_ID,PRDTABLE_ID,PRD_ID,EMPLOYEENAME,PRDTYPE_ID,RETURNTYPE,RETURNDESC";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("PRD_ID", Val.ToString(FrmSearch.mDRow["PRD_ID"]));
                        GrdDet.SetFocusedRowCellValue("PRDTABLE_ID", Val.ToString(FrmSearch.mDRow["PRDTABLE_ID"]));
                        GrdDet.SetFocusedRowCellValue("EMPLOYEECODE", Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]));
                        GrdDet.SetFocusedRowCellValue("EMPLOYEE_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));

                        GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
                        GrdDet.SetFocusedRowCellValue("COLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        GrdDet.SetFocusedRowCellValue("CLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        GrdDet.SetFocusedRowCellValue("CUTNAME", Val.ToString(FrmSearch.mDRow["CUTNAME"]));
                        GrdDet.SetFocusedRowCellValue("POLNAME", Val.ToString(FrmSearch.mDRow["POLNAME"]));
                        GrdDet.SetFocusedRowCellValue("SYMNAME", Val.ToString(FrmSearch.mDRow["SYMNAME"]));
                        GrdDet.SetFocusedRowCellValue("FLNAME", Val.ToString(FrmSearch.mDRow["FLNAME"]));
                        GrdDet.SetFocusedRowCellValue("ARTISTAMOUNT", Val.Val(FrmSearch.mDRow["ARTISTAMOUNT"]));
                        GrdDet.SetFocusedRowCellValue("DIFFAMOUNT", Val.Val(FrmSearch.mDRow["DIFFAMOUNT"]));
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

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
            switch (e.Column.FieldName.ToUpper())
            {
                case "READYCARAT":
                    double DouReady = Val.Val(DRow["READYCARAT"]);
                    double DouIssue = Val.Val(DRow["ISSUECARAT"]);
                    double DouLossCarat = Math.Round(DouIssue - DouReady, 3);
                    GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", DouLossCarat);

                    //txtKapanName.Focus();
                    break;
                default:
                    break;
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGrid) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGrid.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMEMPLOYEENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DHAREMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "DHAREMPLOYEENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MAXIEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MAXIEMPLOYEENAME")));
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
            txtKapanName.Text = string.Empty;
            txtKapanName.Tag = string.Empty;
            txtPacketNo.Text = string.Empty;
            txtTag.Text = string.Empty;
            CmbKapan.SetEditValue(0);
            DTabArtistTickData.Rows.Clear();

            txtKapanName.Focus();
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                   DTabArtistTickData.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                   DTabArtistTickData.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnUpDown_Click(object sender, EventArgs e)
        {
            if (IsDownImage)
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A3;
                PanelHeader.Visible = false;
                MainGrid.Visible = false;
                PnlFilterPanel.Visible = true;
                MainGrdList.Visible = true;
                MainGrdList.Dock = DockStyle.Fill;
                PnlUpDown.Dock = DockStyle.Top;
                IsDownImage = false;
            }
            else
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A4;
                IsDownImage = true;
                PnlFilterPanel.Visible = false;
                MainGrdList.Visible = false;
                MainGrid.Dock = DockStyle.Fill;
                PnlUpDown.Dock = DockStyle.Bottom;
                PanelHeader.Visible = true;
                MainGrid.Visible = true;
                MainGrid.BringToFront();
                
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable DTabSearchData = ObjTrn.GetDataForArtistLowerPlanTick("SAVETICKINFO", Val.Trim(CmbKapan.Properties.GetCheckedItems()), 0, "");
                if (DTabSearchData.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.Message("No Data Found");
                }

                MainGrdList.DataSource = DTabSearchData;
                GrdDet.RefreshData();
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
