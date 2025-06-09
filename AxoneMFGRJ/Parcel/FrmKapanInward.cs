using BusLib.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusLib.Configuration;
using BusLib.TableName;
using System.Data.SqlClient;
using AxoneMFGRJ;
using System.IO;
using System.Text.RegularExpressions;
using AxoneMFGRJ.Utility;
using BusLib.Transaction;
using BusLib;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmKapanInward : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanInward ObjKapan = new BOTRN_KapanInward();
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOTRN_SinglePolishOKTransfer ObjMast = new BOTRN_SinglePolishOKTransfer();

        BOFormPer ObjPer = new BOFormPer();
        DataTable DTabKapanMereg = new DataTable();

        DataTable DTabDetail = new DataTable();

        string StrXMLValues = "";
        bool PriceDate = true;

        #region Property

        public FrmKapanInward()
        {
            InitializeComponent();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = false;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjKapan);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            CmbYear.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_YEAR);
            CmbYear.Properties.DisplayMember = "YEARNAME";
            CmbYear.Properties.ValueMember = "YEAR_ID";

            DtpFromDate.Value = DateTime.Now.AddMonths(-1);
            DtpToDate.Value = DateTime.Now;
            BtnContinue_Click(null, null);
            BtnSearch_Click(null, null);
            PriceDate = false;

            this.Show();

        }

        public void ShowForm(DataTable DTabKapanInward, DataTable DTab)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DtpFromDate.Value = DateTime.Now.AddMonths(-1);
            DtpToDate.Value = DateTime.Now;

            DTabKapanMereg = DTab;

            //////this.Cursor = Cursors.WaitCursor;

            string pStrKapanName = "";
            pStrKapanName = DTabKapanInward.Rows[0]["KAPANNAME"].ToString();

            DTabDetail.Rows.Clear();

            DTabDetail = ObjKapan.FillDetail(Val.ToString(txtInwardNo.Text), Val.SqlDate(DTPInwardDate.Value.ToShortDateString()), pStrKapanName);
            
            double pDouOldCarat = 0;
            Guid pGuidInward_ID = Guid.Empty;
            Int64 pIntInwardNo = 0;
            double pDouPricePerCarat = 0;
            string pStrPriceDate = "";
            Int32 pIntPrice_ID = 0;
            double pDouAmount = 0;

            if (DTabDetail.Rows.Count > 0)
            {
                pDouOldCarat = Val.Val(DTabDetail.Rows[0]["CARAT"]);
                pGuidInward_ID = Guid.Parse(Val.ToString(DTabDetail.Rows[0]["INWARD_ID"]));
                txtInwardNo.Text = Val.ToString(DTabDetail.Rows[0]["INWARDNO"]);
                DTPInwardDate.Text = Val.ToString(DTabDetail.Rows[0]["INWARDDATE"]);
                pDouPricePerCarat = Val.Val(DTabDetail.Rows[0]["COSTPRICEPERCARAT"]);
                pIntPrice_ID = Val.ToInt32(DTabDetail.Rows[0]["PRICE_ID"]);
                pStrPriceDate = Val.ToString(DTabDetail.Rows[0]["PRICEDATE"]);
                pDouAmount = Val.Val(DTabDetail.Rows[0]["COSTAMOUNT"]);
            }
            //int IntPcs = Val.ToInt(DTab.Compute("sum(ORGPCS)",""));
            //double DouCarat = Val.Val(DTab.Compute("sum(ORGCARAT)",""));

            this.Cursor = Cursors.Default;

            foreach (DataRow DRow in DTabKapanInward.Rows)
            {
                DataRow DRNew = DTabDetail.NewRow();

                DRNew["KAPAN_ID"] = DRow["KAPAN_ID"];
                DRNew["KAPANNAME"] = DRow["KAPANNAME"];
                DRNew["CARAT"] = DRow["ORGCARAT"];
                DRNew["PENDINGCARAT"] = DRow["ORGCARAT"];
                DRNew["FINALCARAT"] = DRow["ORGCARAT"];
                DRNew["COSTPRICEPERCARAT"] = pDouPricePerCarat;
                DRNew["PRICE_ID"] = pIntPrice_ID;
                DRNew["PRICEDATE"] = pStrPriceDate;
                DRNew["INWARD_ID"] = pGuidInward_ID;
                DRNew["INWARDNO"] = pIntInwardNo;
                DRNew["COSTAMOUNT"] = pDouAmount;
                DRNew["ENTRYSRNO"] = DTabDetail.Rows.Count + 1;

                DTabDetail.Rows.Add(DRNew);
            }
            DTabDetail.AcceptChanges();
            MainGrid.DataSource = DTabDetail;
            MainGrid.Refresh();
            PriceDate = false;
            this.Show();

        }


        #endregion

        #region Validation

        public void Clears()
        {
            DTabDetail.Rows.Clear();
            txtInwardNo.Text = string.Empty;
        }

        private bool ValSave()
        {
            int IntCol = 0, IntRow = -1;
            foreach (DataRow dr in DTabDetail.Rows)
            {
                if (Val.ToString(dr["KAPANNAME"]).Trim().Equals(string.Empty) && Val.Val(dr["CARAT"]) != 0)
                {
                    Global.Message("Kapan Name Is Required..");
                    IntCol = 0;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                if (PriceDate)
                {

                    if (!Val.ToString(dr["KAPANNAME"]).Trim().Equals(string.Empty) && Val.ToString(dr["PRICEDATE"]).Length == 0)
                    {
                        Global.Message("Price Date Is Required..");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;
                    }
                    if (!Val.ToString(dr["KAPANNAME"]).Trim().Equals(string.Empty) && Val.Val(dr["PRICE_ID"]) == 0)
                    {
                        Global.Message("Price id Date Is Required..");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;
                    }
                    if (Val.Val(dr["CARAT"]) != 0 && Val.ToString(dr["PRICEDATE"]).Length == 0)
                    {
                        Global.Message("Price Date Is Required..");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;
                    }
                    if (Val.Val(dr["CARAT"]) != 0 && Val.Val(dr["PRICE_ID"]) == 0)
                    {
                        Global.Message("Price Date Is Required..");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;
                    }
                }
                if (!Val.ToString(dr["KAPANNAME"]).Trim().Equals(string.Empty) && Val.Val(dr["CARAT"]) == 0)
                {
                    Global.Message("Carat  Is Required..");
                    IntCol = 0;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }

            }
            if (IntRow >= 0)
            {
                GrdDet.FocusedRowHandle = IntRow;
                GrdDet.FocusedColumn = GrdDet.VisibleColumns[IntCol];
                GrdDet.Focus();
                return false;
            }
            return true;
        }


        #endregion

        #region Control Event

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                if (Global.Confirm("Are You Sure To Save This Entry ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                ParcelKapanInwardProperty Property = new ParcelKapanInwardProperty();

                Property.INWARDNO = Val.ToInt64(txtInwardNo.Text);
                Property.INWARDDATE = Val.SqlDate(DTPInwardDate.Value.ToShortDateString());

                DTabDetail.TableName = "Table";
                string StrXMLValuesInsert = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabDetail.WriteXml(sw);
                    StrXMLValuesInsert = sw.ToString();
                }

                Property.StrInwardXml = StrXMLValuesInsert;
                Property.StrXmlValuesForInwardXml = StrXMLValues;

                Property = ObjKapan.Save(Property);

                txtInwardNo.Text = Property.ReturnValue;

                this.Cursor = Cursors.Default;

                Global.Message(Property.ReturnMessageDesc);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    PolishIssueReturnProperty PKapanProperty = new PolishIssueReturnProperty();

                    DTabKapanMereg.TableName = "Table";
                    string StrXMLSelection = string.Empty;
                    using (StringWriter sw = new StringWriter())
                    {
                        if (DTabKapanMereg != null)
                        {
                            DTabKapanMereg.WriteXml(sw);
                            StrXMLSelection = sw.ToString();
                        }
                    }

                    PKapanProperty = ObjMast.UpdateKapanMerge(StrXMLSelection, PKapanProperty);
                    BtnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                Global.Message(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clears();
        }



        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("InwardExport_" + txtInwardNo.Text, GrdDet);
        }


        private void BtnContinue_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DTabDetail = ObjKapan.FillDetail(Val.ToString(txtInwardNo.Text), Val.SqlDate(DTPInwardDate.Value.ToShortDateString()), "");
            DataRow Dr = DTabDetail.NewRow();
            Dr["ENTRYSRNO"] = DTabDetail.Rows.Count + 1;
            DTabDetail.Rows.Add(Dr);
            MainGrid.DataSource = DTabDetail;
            MainGrid.Refresh();

            this.Cursor = Cursors.Default;

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ParcelKapanInwardProperty Property = new ParcelKapanInwardProperty();
            try
            {

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;

                FrmPassword FrmPassword = new FrmPassword();
                FrmPassword.ShowForm(ObjPer.PASSWORD);

                Property.INWARDNO = Val.ToInt64(txtInwardNo.Text);
                Property = ObjKapan.Delete(Property);
                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnClear_Click(null, null);
                }
                else
                {
                    txtInwardNo.Focus();
                }

            }
            catch (System.Exception ex)
            {
                Global.MessageToster(ex.Message);
            }
            Property = null;
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string StrStatus = ",";
                if (ChkPending.Checked == true)
                {
                    StrStatus = StrStatus + "PENDING,";
                }
                if (ChkComplete.Checked == true)
                {
                    StrStatus = StrStatus + "COMPLETE,";
                }
                if (ChkPartial.Checked == true)
                {
                    StrStatus = StrStatus + "PARTIAL,";
                }
                StrStatus = StrStatus + ",";

                DataTable DTabSummury = ObjKapan.FillSummary(txtSearchInwardNo.Text,
                                txtSearchKapanName.Text,
                                Val.SqlDate(DtpFromDate.Value.ToShortDateString()),
                                Val.SqlDate(DtpToDate.Value.ToShortDateString()),
                                StrStatus, Val.ToString(CmbPriceType.SelectedItem), Val.Trim(CmbYear.Properties.GetCheckedItems()));
                MainGrdSummury.DataSource = DTabSummury;
                MainGrdSummury.Refresh();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }

        }

        private void BtnExportSummary_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("InwardSummary", GrdSummury);
        }

        #endregion

        #region KeyPress

        private void ReptxtPriceHeadDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PRICEDATE,REMARK, PRICETYPE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dtTemp = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRICEHEAD);
                    dtTemp.DefaultView.Sort = "PRICEDATE DESC";
                    dtTemp = dtTemp.DefaultView.ToTable();

                    FrmSearch.mDTab = dtTemp;

                    FrmSearch.mColumnsToHide = "PRICE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.PostEditor();
                        DataRow Dr = GrdDet.GetFocusedDataRow();

                        GrdDet.SetFocusedRowCellValue("PRICEDATE", Val.ToString(FrmSearch.mDRow["REMARK"]));
                        GrdDet.SetFocusedRowCellValue("PRICE_ID", Val.ToString(FrmSearch.mDRow["PRICE_ID"]));
                        GrdDet.SetFocusedRowCellValue("PRICETYPE", Val.ToString(FrmSearch.mDRow["PRICETYPE"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void ReptxtShapeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "SHAPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.PostEditor();
                        DataRow Dr = GrdDet.GetFocusedDataRow();

                        GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
                        GrdDet.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void ReptxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow Dro = GrdDet.GetFocusedDataRow();

                if (Val.ToString(Dro["KAPANNAME"]) != "" && Val.Val(Dro["CARAT"]) != 0 && GrdDet.IsLastRow)
                {
                    DataRow DRNew = DTabDetail.NewRow();
                    DRNew["ENTRYSRNO"] = DTabDetail.Rows.Count + 1;
                    DTabDetail.Rows.Add(DRNew);
                }
                else if (GrdDet.IsLastRow)
                {
                    BtnSave.Focus();
                    e.Handled = true;
                }
            }

        }

        #endregion

        #region Grid Detail

        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                if (e.Column.FieldName == "COSTPRICEPERCARAT" || e.Column.FieldName == "CARAT")
                {
                    DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
                    DTabDetail.Rows[e.RowHandle]["COSTAMOUNT"] = Math.Round(Val.Val(DRow["CARAT"]) * Val.Val(DRow["COSTPRICEPERCARAT"]), 2);
                    DTabDetail.Rows[e.RowHandle]["PENDINGCARAT"] = Math.Round(Val.Val(DRow["CARAT"]) - Val.Val(DRow["ASSORTEDCARAT"]), 2);
                    DTabDetail.AcceptChanges();
                }
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void GrdSummury_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Clicks == 2)
            {
                this.Cursor = Cursors.WaitCursor;
                string StrInwardDate = Val.ToString(GrdSummury.GetRowCellValue(e.RowHandle, "INWARDDATE"));
                string StrInwardNo = Val.ToString(GrdSummury.GetRowCellValue(e.RowHandle, "INWARDNO"));

                txtInwardNo.Text = StrInwardNo;
                DTPInwardDate.Value = DateTime.Parse(StrInwardDate);

                DTabDetail = ObjKapan.FillDetail(Val.ToString(txtInwardNo.Text), Val.SqlDate(StrInwardDate), "");
                DataRow Dr = DTabDetail.NewRow();
                Dr["ENTRYSRNO"] = DTabDetail.Rows.Count + 1;
                DTabDetail.Rows.Add(Dr);
                MainGrid.DataSource = DTabDetail;
                MainGrid.Refresh();
                xtraTabControl.SelectedTabPageIndex = 0;
                this.Cursor = Cursors.Default;

            }
        }

        private void GrdSummury_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            string StrStatus = Val.ToString(GrdSummury.GetRowCellValue(e.RowHandle, "STATUS"));
            if (StrStatus == "PENDING")
            {
                e.Appearance.BackColor = lblPending.BackColor;
                e.Appearance.BackColor2 = lblPending.BackColor;
            }
            else if (StrStatus == "PARTIAL")
            {
                e.Appearance.BackColor = lblPartial.BackColor;
                e.Appearance.BackColor2 = lblPartial.BackColor;
            }
            else if (StrStatus == "COMPLETE")
            {
                e.Appearance.BackColor = Color.Transparent;
                e.Appearance.BackColor2 = Color.Transparent;
            }
        }


        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        ParcelKapanInwardProperty Property = new ParcelKapanInwardProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.INWARD_ID = Guid.Parse(Val.ToString(Drow["INWARD_ID"]));
                        Property = ObjKapan.Delete(Property);
                        Global.Message(Property.ReturnMessageDesc);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            BtnClear_Click(null, null);
                            if (DTabDetail.Rows.Count > 0)
                            {
                                DTabDetail.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                                DTabDetail.AcceptChanges();
                            }
                        }
                        else
                        {
                            txtInwardNo.Focus();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        #endregion

        private void reptxtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME,ROUGHNAME,LOTNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANINWARD);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("KAPAN_ID", Val.ToString(FrmSearch.mDRow["KAPAN_ID"]));
                        GrdDet.SetFocusedRowCellValue("KAPANNAME", Val.ToString(FrmSearch.mDRow["KAPANNAME"]));
                        GrdDet.SetFocusedRowCellValue("LOTNO", Val.ToString(FrmSearch.mDRow["LOTNO"]));
                        GrdDet.SetFocusedRowCellValue("ROUGHNAME", Val.ToString(FrmSearch.mDRow["ROUGHNAME"]));
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }


    }
}
