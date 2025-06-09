using BusLib.Master;
using BusLib.Configuration;
using BusLib.TableName;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.Data;
using AxoneMFGRJ.Utility;
using BusLib;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmClarityAssortment : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanInward ObjKapan = new BOTRN_KapanInward();
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabDetail = new DataTable();

        double DouCarat = 0;
        double DouAmount = 0;
        double DouManualRate = 0;

        public FrmClarityAssortment()
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

            DtpFromDate.Value = DateTime.Now.AddMonths(-1);
            DtpToDate.Value = DateTime.Now;
            BtnSearch_Click(null, null);

            LookUpShape.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
            LookUpShape.Properties.ValueMember = "SHAPE_ID";
            LookUpShape.Properties.DisplayMember = "SHAPENAME";

            LookUpSize.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.PARCEL_MIXSIZE);
            LookUpSize.Properties.ValueMember = "MIXSIZE_ID";
            LookUpSize.Properties.DisplayMember = "MIXSIZENAME";

            CmbYear.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_YEAR);
            CmbYear.Properties.DisplayMember = "YEARNAME";
            CmbYear.Properties.ValueMember = "YEAR_ID";

            this.Show();

        }
        #region Validation


        private bool ValSave()
        {

            if (Val.Val(txtDiff.Text) < 0)
            {
                Global.Message("Error In Balance Mismatch [" + txtDiff.Text + "]..");
                return false;
            }
            if (txtKapan.Text.Length == 0)
            {
                Global.Message("Kapan Name Is Required..");
                return false;
            }
            if (LookUpShape.Text.Length == 0)
            {
                Global.Message("Shape Name Is Required..");
                return false;
            }
            if (txtDepartment.Text.Length == 0)
            {
                Global.Message("Department Name Is Required..");
                txtDepartment.Focus();
                return false;
            }
            if (Val.Val(txtInwardCarat.Text) == 0)
            {
                Global.Message("Inward Carat Is Required..");
                return false;
            }


            return true;
        }


        #endregion

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

                Guid gUser_ID = Val.ToString(txtUser_ID.Text).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(txtUser_ID.Tag));

                DataTable DTabSummury = ObjKapan.ClarityAssortmentGetKapanData(//txtSearchInwardNo.Text,
                                txtSearchKapanName.Text,
                                Val.SqlDate(DtpFromDate.Value.ToShortDateString()),
                                Val.SqlDate(DtpToDate.Value.ToShortDateString()),
                                StrStatus,
                                  Val.ToString(CmbPriceType.SelectedItem),// add by urvisha :09-05-2024
                                "",
                                gUser_ID, Val.Trim(CmbYear.Properties.GetCheckedItems())
                                );
                MainGrdSummry.DataSource = DTabSummury;
                MainGrdSummry.Refresh();

                GrdDetSummry.Columns["GroupColumn"].Group();
                GrdDetSummry.Columns["GroupColumn"].Visible = false;

                if (GrdDetSummry.GroupSummary.Count == 0)
                {
                    GrdDetSummry.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "KAPANNAME", GrdDetSummry.Columns["KAPANNAME"], "{0:N0}");
                    GrdDetSummry.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "INWARDCARAT", GrdDetSummry.Columns["INWARDCARAT"], "{0:N3}");
                    GrdDetSummry.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "PENDINGCARAT", GrdDetSummry.Columns["PENDINGCARAT"], "{0:N3}");
                    GrdDetSummry.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ASSORTCARAT", GrdDetSummry.Columns["ASSORTCARAT"], "{0:N3}");
                }
                GrdDetSummry.ExpandAllGroups();
              
                this.Cursor = Cursors.Default;
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
                DTabDetail.TableName = "Table";
                string StrXMLValues = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabDetail.WriteXml(sw);
                    StrXMLValues = sw.ToString();
                }
                Property.KAPANNAME = Val.ToString(txtKapan.Text);
                Property.SIZEASSORT_ID = Val.ToGuid(Val.ToString(txtKapan.Tag));
                Property.SHAPE_ID = Val.ToInt(LookUpShape.EditValue);
                Property.MIXSIZE_ID = Val.ToInt32(LookUpSize.EditValue);
                Property.DEPARTMENT_ID = Val.ToInt(txtDepartment.Tag);
                Property.StrInwardXml = StrXMLValues;

                Property = ObjKapan.ClarityAssortmentSave(Property);

                this.Cursor = Cursors.Default;

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    DTabDetail.Rows.Clear();
                    txtKapan.Text = string.Empty;
                    txtKapan.Tag = string.Empty;
                    LookUpShape.Text = string.Empty;
                    LookUpShape.Tag = string.Empty;
                    txtBalanceCarat.Text = string.Empty;
                    txtInwardCarat.Text = string.Empty;
                    txtDepartment.Text = string.Empty;
                    BtnSearch_Click(null, null);
                    txtDepartment.Focus();
                }
                else
                {
                    Global.Message(Property.ReturnMessageDesc);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                Global.Message(ex.Message);
            }
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
                if (FrmPassword.DialogResult == DialogResult.Yes)
                {
                    Property.KAPANNAME = Val.ToString(txtKapan.Text);//add by gunjan:16/03/2023
                    Property.SHAPE_ID = Val.ToInt(LookUpShape.EditValue);//add by gunjan:16/03/2023
                    Property.MIXSIZE_ID = Val.ToInt32(LookUpSize.EditValue);//add by gunjan:16/03/2023
                    Property.DEPARTMENT_ID = Val.ToInt(txtDepartment.Tag);

                    Property = ObjKapan.ClarityAssortmentDelete(Property);
                    Global.Message(Property.ReturnMessageDesc);

                }
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    DTabDetail.Rows.Clear();

                    txtKapan.Text = string.Empty;
                    txtKapan.Tag = string.Empty;

                    LookUpShape.Text = string.Empty;
                    LookUpShape.Tag = string.Empty;

                    txtBalanceCarat.Text = string.Empty;
                    txtInwardCarat.Text = string.Empty;

                    BtnSearch_Click(null, null);
                }
                else
                {
                    Global.Message("Record is Not Deleted!!");
                }

            }
            catch (System.Exception ex)
            {
                Global.MessageToster(ex.Message);
            }
            Property = null;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("ClarityAssortment", GrdDetSize);
        }

        public void Calculate()
        {
            try
            {
                double DouCarat = 0;
                double DouLessPlus = 0;

                if (DTabDetail != null && DTabDetail.Rows.Count != 0)
                {

                    double DouTotalCarat = Val.Val(DTabDetail.Compute("SUM(CARAT)", ""));

                    double DouPer = 0;

                    for (int IntI = 0; IntI < GrdDetSize.RowCount; IntI++)
                    {
                        DataRow DRow = GrdDetSize.GetDataRow(IntI);
                        DouCarat = DouCarat + Val.Val(DRow["CARAT"]);
                        DouLessPlus = DouLessPlus + Val.Val(DRow["LESSPLUSCARAT"]);


                        if (DouTotalCarat != 0)
                        {
                            DouPer = Math.Round((Val.Val(DRow["CARAT"]) / DouTotalCarat) * 100, 2);
                        }
                        //DRow["AMOUNT"] = Math.Round(Val.Val(DRow["PRICEPERCARAT"]) * Val.Val(DRow["CARAT"]), 2);
                        DRow["PER"] = DouPer;
                    }

                    if (Val.ToInt32(txtShape.Tag) != 5135) // Fancy nu clarity wise assortment nae thae
                    {
                        txtDiff.Text = Val.ToString(Math.Round(Val.Val(txtInwardCarat.Text) - (DouCarat + DouLessPlus), 3));
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void GrdDetSummry_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            string StrStatus = Val.ToString(GrdDetSummry.GetRowCellValue(e.RowHandle, "STATUS"));
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

        private void GrdDetSummry_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            FetchRecord(e.RowHandle);
        }

        private void GrdDetSize_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void GrdDetSize_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            Calculate();
            if (e.Column.FieldName == "CARAT")
            {
                if (Val.Val(txtDiff.Text) < 0)
                {
                    Global.Message("Not Enough Balance For Assortment, Carat Difference Is [" + txtDiff.Text + "]");
                    DTabDetail.Rows[e.RowHandle]["CARAT"] = 0;
                    DTabDetail.AcceptChanges();

                }
            }
        }

        public void FetchRecord(int IntRowhandle)
        {
            try
            {
                if (IntRowhandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                string SizeAssortID = Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "SIZEASSORT_ID"));
                txtKapan.Text = Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "KAPANNAME"));
                txtKapan.Tag = Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "SIZEASSORT_ID"));

                LookUpShape.EditValue = Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "SHAPE_ID"));
                LookUpSize.EditValue = Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "MIXSIZE_ID"));

                txtBalanceCarat.Text = Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "PENDINGCARAT"));
                txtInwardCarat.Text = Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "INWARDCARAT"));

                lblTitle.Text = "Clarity Wise Assortment Of [ " + Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "SIZEASSORTNO")) + " / " + Val.ToString(GrdDetSummry.GetRowCellValue(IntRowhandle, "SIZENAME")) + " ]";

                txtDepartment.Focus();
                LoadData();
                txtDepartment.Text = "ADMIN-DN";
                txtDepartment.Tag = 436;
                LoadDataForSize();

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }

        private void GrdDetSummry_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FetchRecord(e.FocusedRowHandle);
        }

        private void GrdDetSize_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (GrdDetSize.FocusedRowHandle < 0)
                {
                    return;
                }

                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    GrdDetSize.FocusedRowHandle = GrdDetSize.FocusedRowHandle + 1;
                    GrdDetSize.FocusedColumn = GrdDetSize.Columns["CARAT"];
                    GrdDetSize.Focus();
                }
            }
            catch (Exception EX)
            {

            }
        }

        private void GrdDetSize_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouAmount = 0;
                    DouManualRate = 0;

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GrdDetSize.GetRowCellValue(e.RowHandle, "CARAT"));
                    DouAmount = DouAmount + (Val.Val(GrdDetSize.GetRowCellValue(e.RowHandle, "CARAT")) * Val.Val(GrdDetSize.GetRowCellValue(e.RowHandle, "PRICEPERCARAT")));
                    DouManualRate = DouManualRate + (Val.Val(GrdDetSize.GetRowCellValue(e.RowHandle, "CARAT")) * Val.Val(GrdDetSize.GetRowCellValue(e.RowHandle, "MANUALRATE")));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PRICEPERCARAT") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val((DouAmount + DouManualRate)) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        public void LoadData()
        {
            Guid SizeAssortID = Val.ToGuid(GrdDetSummry.GetFocusedRowCellValue("SIZEASSORT_ID"));

            if (Val.Trim(txtDepartment.Text).Length == 0)
            {
                DTabDetail.Rows.Clear();
            }

            else
            {
                DTabDetail = ObjKapan.ClarityAssortmentGetSizeData("", SizeAssortID, Val.ToInt(txtDepartment.Tag), "", 0, Val.Trim(CmbYear.Properties.GetCheckedItems()));
            }

            MainGridSize.DataSource = DTabDetail;
            MainGridSize.Refresh();
            Calculate();
        }

        public void LoadDataForSize()
        {
            Guid SizeAssortID = Val.ToGuid(GrdDetSummry.GetFocusedRowCellValue("SIZEASSORT_ID"));
            string KapanName = Val.ToString(GrdDetSummry.GetFocusedRowCellValue("KAPANNAME"));
            if (Val.Trim(txtDepartment.Text).Length == 0)
            {
                DTabDetail.Rows.Clear();
            }

            else
            {
                DTabDetail = ObjKapan.ClarityAssortmentGetSizeData(KapanName, SizeAssortID, Val.ToInt(txtDepartment.Tag), Val.ToString(LookUpSize.EditValue), Val.ToInt32(LookUpShape.EditValue), Val.Trim(CmbYear.Properties.GetCheckedItems()));

            }
            MainGridSize.DataSource = DTabDetail;
            MainGridSize.Refresh();
            if (DTabDetail.Rows.Count > 0)
            {
                lblTitle.Text = "Clarity Wise Assortment Of [ " + Val.ToString(DTabDetail.Rows[0]["KAPANNAME"].ToString()) + " / " + Val.ToString(LookUpSize.Text) + " ]";
                txtInwardCarat.Text = DTabDetail.Rows[0]["SIZEASSORTMENTCARAT"].ToString();
                txtBalanceCarat.Text = DTabDetail.Rows[0]["PENDINGCARAT"].ToString();
            }
            else
            {
                lblTitle.Text = "Clarity Wise Assortment";
                txtBalanceCarat.Text = string.Empty;
                txtInwardCarat.Text = string.Empty;
                txtInwardNo.Text = string.Empty;
            }
            Calculate();
        }
    
        private void GrdDetSize_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (DTabDetail.Rows.Count > 0)
                {
                    Int32 pIntMixClarity_ID = Val.ToInt32(GrdDetSize.GetFocusedRowCellValue("MIXCLARITY_ID"));
                    Int32 pIntMixSizeSeqNo = Val.ToInt32(GrdDetSize.GetFocusedRowCellValue("MIXSIZESEQNO"));

                    Int32 pIntMixSize90Up = Val.ToInt32(GrdDetSize.GetFocusedRowCellValue("MIXSIZE90UP_SEQNO"));

                    if (pIntMixSizeSeqNo >= pIntMixSize90Up || pIntMixClarity_ID == 1727 || pIntMixClarity_ID == 1728) //1728=GIA 1727=Mix Clarity) 
                    {
                        GrdDetSize.Columns["PRICEPERCARAT"].OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        GrdDetSize.Columns["PRICEPERCARAT"].OptionsColumn.AllowEdit = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void txtSearchKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
         try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.PARCEL_KAPAN);
                    //FrmSearch.ColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtSearchKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        LoadDataForSize();
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

        private void txtSearchKapanName_Validating(object sender, CancelEventArgs e)
        {
           LoadDataForSize();
        }

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTCODE,DEPARTMENTNAME";
                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENTMIX);
                    // FrmSearch.ColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                        LoadDataForSize();
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

        private void txtUser_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTCODE,DEPARTMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENTMIX);
                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                        //LoadData();
                        LoadDataForSize();
                        
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

        private void LookUpShape_EditValueChanged(object sender, EventArgs e)
        {
         LoadDataForSize();
        }

        private void LookUpSize_EditValueChanged(object sender, EventArgs e)
        {  
            LoadDataForSize();
        }

        private void LookUpShape_Validating(object sender, CancelEventArgs e)
        {
            LoadDataForSize();
        }

        private void MainGridSize_Click(object sender, EventArgs e)
        {

        }
        
    }
}