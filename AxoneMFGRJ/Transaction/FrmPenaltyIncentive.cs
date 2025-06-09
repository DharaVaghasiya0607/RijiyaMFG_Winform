using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid.Views.Grid;
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

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmPenaltyIncentive : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PenaltyIncentive ObjPenalty = new BOTRN_PenaltyIncentive();
        DataTable DtabPenalty = new DataTable();
        BOFormPer ObjPer = new BOFormPer();

        BODevGridSelection ObjGridSelection;
        bool IsDownImage = false;
        string pStrPassword = "";

        #region Property Settings

        public FrmPenaltyIncentive()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            BtnUpDown_Click(null, null);
            BtnVerified.Enabled = false;
            BtnCalculate.Enabled = false;

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            pStrPassword = ObjPer.PASSWORD;

            this.Show();
            Clear();
        }
        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjPenalty);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            if (txtEmployeeCode.Text.Trim().Equals(string.Empty) && Val.ToString(txtEmployeeName.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Employee Is Required");
                txtEmployeeCode.Focus();
                return true;
            }
            if (txtReason.Text.Trim().Equals(string.Empty))
            {
                Global.Message("Reason Code Is Required");
                txtReason.Focus();
                return true;
            }
            if (txtRemark.Text.Trim().Equals(string.Empty))
            {
                Global.Message("Remark Is Required");
                txtRemark.Focus();
                return true;
            }
            if (Val.Val(txtAmount.Text) == 0)
            {
                Global.Message("Amount Is Required");
                txtAmount.Focus();
                return true;
            }
            return false;
        }

        private bool ValSaveUpload(DataTable pDt)
        {
            if (cmbEntryTypeUpload.Text == "" || cmbEntryTypeUpload.Text == string.Empty)
            {
                Global.MessageError("Type Cant Be Blank");
                cmbEntryTypeUpload.Focus();
                return true;
            
            }

            for (int i = 0; i < pDt.Rows.Count; i++)
            {
                if (Val.ToDecimal( pDt.Rows[i]["AMOUNT"].ToString()) <= 0)
                {
                    Global.MessageError("AMOUNT Cant Be Blank.");
                    return true;
                }
                if (pDt.Rows[i]["REASON"].ToString() == "")
                {
                    Global.MessageError("REASON Cant Be Blank.");
                    return true;
                }
            }
            return false;
        }

        private bool ValDelete()
        {
            //if (txtItemGroupCode.Text.Trim().Length == 0)
            //{
            //    Global.Message("Group Code Is Required");
            //    txtItemGroupCode.Focus();
            //    return false;
            //}

            return true;
        }

        #endregion

        public void Clear()
        {

            DTPEntryDate.Text = Val.ToString(DateTime.Now);
            DTPEntryDate.Tag = string.Empty;

            CmbEntryType.SelectedIndex = 0;
            txtEmployeeCode.Text = string.Empty;
            txtEmployeeCode.Tag = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtKapanName.Text = string.Empty;

            txtPacketNo.Text = string.Empty;
            txtPacketTag.Text = string.Empty;

            txtAmount.Text = "0.00";
            txtReason.Text = string.Empty;
            txtReason.Tag = string.Empty;

            txtRemark.Text = string.Empty;

            DTPFromDate.Text = Val.ToString(DateTime.Now.AddMonths(-1));
            DTPToDate.Text = Val.ToString(DateTime.Now);
            txtSearchEmployee.Text = string.Empty;
            txtSearchEmployee.Tag = string.Empty;

            txtPcs.Text = String.Empty;
         //   txtRate.Text = String.Empty;

            txtDepartment.Text = String.Empty;
            txtDepartment.Tag = String.Empty;

            Fill();

            DTPEntryDate.Focus();

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValSave())
                {
                    return;
                }

                //if (Global.Confirm("Are Your Sure To Save All Records ?") == System.Windows.Forms.DialogResult.No)
                //    return;

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                string StrEntryDate = "";
                TrnPenaltyIncentiveProperty Property = new TrnPenaltyIncentiveProperty();

                Property.PENALTY_ID = Val.ToInt64(DTPEntryDate.Tag);
                Property.PENALTYTYPE = Val.ToString(CmbEntryType.Text);
                Property.PENALTYDATE = Val.SqlDate(DTPEntryDate.Text);
                StrEntryDate = DTPEntryDate.Text;
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployeeCode.Tag);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToString(txtPacketNo.Text);

                Property.PACKETTAG = Val.ToString(txtPacketTag.Text);

                if (Val.ToString(txtReason.Text).Trim().Equals(string.Empty))
                    txtReason.Tag = string.Empty;

                Property.REASON = Val.ToString(txtReason.Text);
                Property.AMOUNT = Val.Val(txtAmount.Text);

                Property.REMARK = Val.ToString(txtRemark.Text);

                Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);

                Property.NOOFPCS = Val.ToInt32(txtPcs.Text);
                Property.CARAT = Val.Val(txtCarat.Text);
                Property.RATE = Val.Val(txtRate.Text);

                Property = ObjPenalty.Save(Property);

                ReturnMessageDesc = Property.ReturnMessageDesc;
                ReturnMessageType = Property.ReturnMessageType;

                DtabPenalty.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Clear();
                    DTPEntryDate.Focus();
                    CmbEntryType.Text = Property.PENALTYTYPE;
                    DTPEntryDate.Text = StrEntryDate;
                }
                Property = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            DtabPenalty = ObjPenalty.Fill(Val.SqlDate(DTPFromDate.Text), Val.SqlDate(DTPToDate.Text), Val.ToInt64(txtSearchEmployee.Tag));
            MainGrid.DataSource = DtabPenalty;
            MainGrid.Refresh();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnBack_Click(null, null);
            //}
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Clicks == 2)
            {
                DataRow DR = GrdDet.GetDataRow(e.RowHandle);
                FetchValue(DR);
                DR = null;
            }

        }

        public void FetchValue(DataRow DR)
        {
            //txtParaType.Text = Val.ToString(DR["ITEMGROUP_ID"]);

            DTPEntryDate.Tag = Val.ToString(DR["ID"]);

            DTPEntryDate.Text = Val.ToString(DR["PENALTYDATE"]);
            CmbEntryType.Text = Val.ToString(DR["PENALTYTYPE"]);
            txtEmployeeCode.Text = Val.ToString(DR["EMPLOYEECODE"]);
            txtEmployeeCode.Tag = Val.ToString(DR["EMPLOYEE_ID"]);
            txtEmployeeName.Text = Val.ToString(DR["EMPLOYEENAME"]);

            txtKapanName.Text = Val.ToString(DR["KAPANNAME"]);
            txtPacketNo.Text = Val.ToString(DR["PACKETNO"]);
            txtPacketTag.Text = Val.ToString(DR["PACKETTAG"]);

            //txtReason.Tag = Val.ToString(DR["REASON_ID"]);
            txtReason.Text = Val.ToString(DR["REASONNAME"]);

            txtAmount.Text = Val.ToString(DR["AMOUNT"]);
            txtRemark.Text = Val.ToString(DR["REMARK"]);

            txtDepartment.Text = Val.ToString(DR["DEPARTMENTNAME"]);
            txtDepartment.Tag = Val.ToString(DR["DEPARTMENT_ID"]);

            txtPcs.Text = Val.ToString(DR["PCS"]);
            txtRate.Text = Val.ToString(DR["RATE"]);
            txtCarat.Text = Val.ToString(DR["CARAT"]);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PenaltyIncentiveList", GrdDet);
        }

        private void txtEmployeeCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (txtEmployeeCode.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjPenalty.GetEmployee(Val.ToInt32(txtDepartment.Tag));
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID,AUTOCONFIRM";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployeeCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployeeCode.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        txtEmployeeName.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
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

        private void txtEmployeeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtEmployeeCode.Enabled == false)
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
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID,AUTOCONFIRM";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployeeCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployeeCode.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        txtEmployeeName.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
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

        //private void txtReason_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        if (txtEmployeeCode.Enabled == false)
        //        {
        //            return;
        //        }
        //        if (Global.OnKeyPressToOpenPopup(e))
        //        {
        //            FrmSearch FrmSearch = new FrmSearch();
        //            FrmSearch.SearchField = "REASONCODE,REASONNAME";
        //            FrmSearch.SearchText = e.KeyChar.ToString();
        //            this.Cursor = Cursors.WaitCursor;
        //            FrmSearch.DTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_REASON);
        //            FrmSearch.ColumnsToHide = "REASON_ID";
        //            this.Cursor = Cursors.Default;
        //            FrmSearch.ShowDialog();
        //            e.Handled = true;
        //            if (FrmSearch.DRow != null)
        //            {
        //                txtReason.Text = Val.ToString(FrmSearch.DRow["REASONNAME"]);
        //                txtReason.Tag = Val.ToString(FrmSearch.DRow["REASON_ID"]);
        //            }
        //            FrmSearch.Hide();
        //            FrmSearch.Dispose();
        //            FrmSearch = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.MessageError(ex.Message);
        //    }
        //}

        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOFindRap().GetKapan();
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void txtPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOFindRap().GetPacketNo(txtKapanName.Text);
                    //if (EmployeeRightsProperty.RAPCHANGEPACKETS == true)
                    //{
                    //    FrmSearch.DTab = ObjRap.GetPacketNo(txtKapanName.Text);
                    //}
                    //else
                    //{
                    //    FrmSearch.DTab = ObjRap.GetEmployeeOSPacketNo(txtKapanName.Text);
                    //}
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
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

        private void txtPacketTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SRNO,TAG";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOFindRap().GetTag(txtKapanName.Text, Val.ToInt(txtPacketNo.Text));
                    FrmSearch.mColumnsToHide = "KAPAN_ID,PACKET_ID,EMPLOYEE_ID,KAPANNAME,PACKETNO,LOTPCS,BALANCEPCS";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPacketTag.Text = Val.ToString(FrmSearch.mDRow["TAG"]);
                        txtPacketNo.Tag = Val.ToString(FrmSearch.mDRow["PACKET_ID"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (Val.ToString(txtSearchEmployee.Text).Trim().Equals(string.Empty))
                txtSearchEmployee.Tag = string.Empty;

            Fill();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (Val.ToString(DTPEntryDate.Tag).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Records From The List That you Want to Delete");
                    return;
                }

                TrnPenaltyIncentiveProperty Property = new TrnPenaltyIncentiveProperty();

                Property.PENALTY_ID = Val.ToInt64(DTPEntryDate.Tag);
                Property = ObjPenalty.DeletePanulty(Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message("ENTRY DELETED SUCCESSFULLY");
                    Clear();
                }
                else
                {
                    Global.Message("ERROR IN DELETE ENTRY");
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtSearchEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtSearchEmployee.Enabled == false)
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
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID,AUTOCONFIRM";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtSearchEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]) + " - " + Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtSearchEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenFileDialog = new OpenFileDialog();
                OpenFileDialog.Filter = "Excel Files (*.xls,*.xlsx)|*.xls;*.xlsx;";
                if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtFileName.Text = OpenFileDialog.FileName;

                    string extension = Path.GetExtension(txtFileName.Text.ToString());
                    string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(txtFileName.Text);
                    destinationPath = destinationPath.Replace(extension, ".xlsx");
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    File.Copy(txtFileName.Text, destinationPath);

                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                }
                OpenFileDialog.Dispose();
                OpenFileDialog = null;
                BtnBrowse.Enabled = false;
                BtnCalculate.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString() + "InValid File Name");
            }
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            BtnBrowse.Enabled = false;
            BtnCalculate.Enabled = false;
            BtnVerified.Enabled = true;
            DataTable DtabExcelData = new DataTable();
            DtabExcelData.Rows.Clear();
            string extension = Path.GetExtension(txtFileName.Text.ToString());
            string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(txtFileName.Text);
            destinationPath = destinationPath.Replace(extension, ".xlsx");
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }
            File.Copy(txtFileName.Text, destinationPath);

            DtabExcelData = GetDataTableFromExcel(destinationPath);

            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }

            GrdDetList.BeginUpdate();
            if (MainGrdList.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDetList;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 1;

                ////GrdDetStock.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
                //GrdDetList.Bands["BANDMAIN"].Fixed = FixedStyle.None;
                //GridBand band = GrdDetStock.Bands.AddBand("..");
                //band.Columns.Add(GrdDetStock.Columns["COLSELECTCHECKBOX"]);
                //band.Fixed = FixedStyle.Left;
                //band.VisibleIndex = 0;
                //GrdDetList.Bands["BANDMAIN"].Fixed = FixedStyle.Left;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }


            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 1;
            }
            GrdDetList.EndUpdate();
            MainGrdList.DataSource = DtabExcelData;
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
                if (ObjGridSelection != null)
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

            return resultTable;
        }

        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    if (Convert.ToString(firstRowCell.Text).Equals(string.Empty))
                        continue;

                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        if (Convert.ToString(cell.Text).Equals(string.Empty))
                            continue;

                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }

        private void BtnVerified_Click(object sender, EventArgs e)
        {
            DataTable DTabVerfied = GetTableOfSelectedRows(GrdDetList, true);
            if (DTabVerfied.Rows.Count == 0)
            {
                Global.MessageError("Please Select Record For Upload");
                return;
            }
           
            DTabVerfied.TableName = "Table1";

            if (ValSaveUpload(DTabVerfied))
                return;
            this.Cursor = Cursors.WaitCursor;
            if (DTabVerfied.Columns.Contains("PENALTYTYPE") == false)
                DTabVerfied.Columns.Add("PENALTYTYPE");

            foreach (DataRow row in DTabVerfied.Rows)
            {
                row["PENALTYDATE"] = Val.ToDate(row["PENALTYDATE"], AxonDataLib.BOConversion.DateFormat.MMDDYYYY);
                row["PENALTYTYPE"] = cmbEntryTypeUpload.Text;
            }
            string StockSyncVerifiedDetailForXml = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                DTabVerfied.WriteXml(sw);
                StockSyncVerifiedDetailForXml = sw.ToString();
            }
            TrnPenaltyIncentiveProperty pPenltyIncProperty = new TrnPenaltyIncentiveProperty();
            pPenltyIncProperty = ObjPenalty.StockUploadSave(StockSyncVerifiedDetailForXml, pPenltyIncProperty);
            if (pPenltyIncProperty.ReturnMessageType == "SUCESS")
            {
                Global.Message(pPenltyIncProperty.ReturnMessageDesc);
                BtnClearUpload_Click(null, null);
            }
            else
            {
                Global.MessageError(pPenltyIncProperty.ReturnMessageDesc);
            }
            this.Cursor = Cursors.Default;
        }

        private void BtnUpDown_Click(object sender, EventArgs e)
        {
            if (IsDownImage)
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A3;
                panel1.Visible = false;
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
                panel1.Visible = true;
                MainGrid.Visible = true;
                MainGrid.BringToFront();

            }
        }

        private void BtnClearUpload_Click(object sender, EventArgs e)
        {
            MainGrdList.DataSource = null;
            MainGrdList.RefreshDataSource();
            BtnVerified.Enabled = false;
            txtFileName.Text = "";
            cmbEntryTypeUpload.SelectedIndex = -1;
            BtnBrowse.Enabled = true;
            BtnCalculate.Enabled = false;
        }

        private void lblSampleExcelFile_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFilePathDestination = "";
                StrFilePathDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\PenaltyFileUploadFormat_" + DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM") + DateTime.Now.Day.ToString() + ".xlsx";
                if (File.Exists(StrFilePathDestination))
                {
                    File.Delete(StrFilePathDestination);
                }
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Format\\PenaltyFileUploadFormat.xlsx", StrFilePathDestination);
                 
                System.Diagnostics.Process.Start(StrFilePathDestination, "CMD");
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtPenaltyPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "POINTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PENLTYPOINT);
                    FrmSearch.mColumnsToHide = "POINT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPenulty.Text = Val.ToString(FrmSearch.mDRow["POINTNAME"]);
                        txtPenulty.Tag = Val.ToString(FrmSearch.mDRow["POINT_ID"]);
                        txtPcs.Text = Val.ToString(FrmSearch.mDRow["NOOFPCS"]);
                        txtRate.Text = Val.ToString(FrmSearch.mDRow["RATE"]);
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

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTNAME, DEPARTMENTCODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
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

        private void txtPassForUpdateDelete_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtPassForUpdateDelete.Tag) != "" && Val.ToString(txtPassForUpdateDelete.Tag).ToUpper() == txtPassForUpdateDelete.Text.ToUpper())
                {
                    BtnSave.Enabled = true;
                    BtnDelete.Enabled = true;
                }
                else
                {
                    BtnSave.Enabled = false;
                    BtnDelete.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = Val.ToString(Val.Val(txtCarat.Text) * Val.Val(txtRate.Text));
            }
            catch (Exception Ex)
            {
                
            }
        }

        private void txtCarat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = Val.ToString(Val.Val(txtCarat.Text) * Val.Val(txtRate.Text));
            }
            catch (Exception Ex)
            {

            }
        }
    }
}
