using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
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

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmLabourProcessMakablePlanning : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_LabourProcessMakable ObjLProcess = new BOTRN_LabourProcessMakable();
        DataTable DTabLabProcess = new DataTable();
        DataTable DTab = new DataTable();
        DataTable DTabMonth = new DataTable();

        bool IsNextImage = true;

        #region Property Settings

        public FrmLabourProcessMakablePlanning()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();


            DTabMonth = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MONTH);
            MainGrdYear.DataSource = DTabMonth;
            //MainGrdYear.Refresh();

            CmbProcess.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
            CmbProcess.ValueMember = "PROCESS_ID";
            CmbProcess.DisplayMember = "PROCESSNAME";

            CmbDepartment.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
            CmbDepartment.ValueMember = "DEPARTMENT_ID";
            CmbDepartment.DisplayMember = "DEPARTMENTNAME";

            CmbCopyToProcess.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
            CmbCopyToProcess.ValueMember = "PROCESS_ID";
            CmbCopyToProcess.DisplayMember = "PROCESSNAME";

            cmbCopyToDept.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
            cmbCopyToDept.ValueMember = "DEPARTMENT_ID";
            cmbCopyToDept.DisplayMember = "DEPARTMENTNAME";

            DTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);//Gunjan : 30/03/2023

            DataRow DRow = DTab.NewRow();
            DRow["PROCESS_ID"] = 0;
            DRow["PROCESSNAME"] = "";
            DTab.Rows.Add(DRow);

            DTab.DefaultView.Sort = "PROCESS_ID";
            DTab = DTab.DefaultView.ToTable();

            cmbSubProcess.DataSource = DTab;
            cmbSubProcess.ValueMember = "PROCESS_ID";
            cmbSubProcess.DisplayMember = "PROCESSNAME";

            BtnAdd_Click(null, null);
            BtnLeft_Click(null, null);
            DataRow DrProcess = DTabLabProcess.NewRow();
            DTabLabProcess.Rows.Add(DrProcess);
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
            ObjFormEvent.ObjToDisposeList.Add(ObjLProcess);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DTabLabProcess.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        #region Validation

        private bool ValSave()
        {
            if (CmbMonth.Text.Trim() == string.Empty)
            {
                Global.Message("Month Is Required");
                CmbMonth.Focus();
                return true;
            }
            else if (txtYear.Text.Trim() == string.Empty)
            {
                Global.Message("Year Is Required");
                txtYear.Focus();
                return true;
            }

           
            else if (CmbProcess.Text.Trim() == string.Empty)
            {
                Global.Message("Process Is Required");
                CmbProcess.Focus();
                return true;
            }

           

            int IntCol = 0, IntRow = -1;
            foreach (DataRow dr in DTabLabProcess.Rows)
            {
                if (DTabLabProcess.Rows.Count == 1)
                {
                    if (Val.Val(dr["RATE"]) <= 0)
                    {
                        Global.Message("Please Enter 'Rate'.");
                        IntCol = 4;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;
                    }
                    else if ((Val.Val(dr["FROMCARAT"]) != 0 && Val.Val(dr["TOCARAT"]) <= 0) || (Val.Val(dr["FROMCARAT"]) <= 0 && Val.Val(dr["TOCARAT"]) != 0))
                    {
                        Global.Message("Please Enter 'From Carat' And 'To Carat' Both.");
                        IntCol = 2;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;
                    }

                    if (Val.Val(dr["FROMCARAT"]) <= 0)
                    {
                        Global.Message("Please Enter 'From Carat'.");
                        IntCol = IntCol + 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;
                    }
                    else if (Val.Val(dr["TOCARAT"]) <= 0)
                    {
                        Global.Message("Please Enter 'To Carat'.");
                        IntCol = IntCol + 1;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;
                    }
                }

                if (Val.Val(dr["RATE"]) != 0 && (Val.Val(dr["FROMCARAT"]) <= 0 || Val.Val(dr["TOCARAT"]) <= 0))
                {
                    Global.Message("Please Enter 'From Carat' And 'To Carat' Both.");
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
                return true;
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
            DTabLabProcess.Rows.Clear();

            txtYear.Text = Val.ToString(DateTime.Now.Year);
            CmbMonth.SelectedIndex = Val.ToInt(DateTime.Now.Month) - 1;
            cmbCopyToMonthOther.SelectedIndex = Val.ToInt(DateTime.Now.Month) - 1;
            CmbProcess.SelectedIndex = 0;
            CmbCopyToProcess.SelectedIndex = 0;
            CmbDepartment.SelectedIndex = 0;
            cmbCopyToDept.SelectedIndex = 0;
            txtManager.Text = string.Empty;
            txtManager.Tag = string.Empty;
            txtCopyToManager.Text = string.Empty;
            txtCopyToManager.Tag = string.Empty;
            txtCopyToYear.Text = Val.ToString(DateTime.Now.Year);
            CmbDepartment.SelectedIndex = 0;
            cmbSubProcess.SelectedIndex = 0;//Gunjan:30/03/2023
            CmbMonth.Focus();
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

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                foreach (DataRow Dr in DTabLabProcess.GetChanges().Rows)
                {
                    if ((Val.Val(Dr["FROMCARAT"]) <= 0 || Val.Val(Dr["TOCARAT"]) <= 0) || Val.Val(Dr["RATE"]) <= 0)
                        continue;

                    TrnLabourProcessProperty Property = new TrnLabourProcessProperty();

                    Property.LABOURPROCESS_ID = Val.ToInt64(Dr["LABOURPROCESS_ID"]);
                    Property.YYYY = Val.ToInt32(txtYear.Text);
                    Property.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);

                    Property.PROCESS_ID = Val.ToInt32(CmbProcess.SelectedValue);

                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);
                    Property.RATE = Val.Val(Dr["RATE"]);

                    Property.LABOURTYPE = Val.ToString(cmbLabourType.SelectedItem);

                    Property.DEPARTMENT_ID = Val.ToInt32(CmbDepartment.SelectedValue);
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);

                    Property.SUBPROCESS_ID = Val.ToInt32(cmbSubProcess.SelectedValue);//Gunjan:30/03/2023

                    Property.PLANNINGGRADE_ID = Val.ToInt32(txtPlanningGrade.Tag);
                    Property.ROUGHTYPE = Val.ToString(CmbRoughType.SelectedItem);

                    Property = ObjLProcess.SaveForMkablePlaning(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DTabLabProcess.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                   //Fill();

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                    CmbMonth.Focus();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            TrnLabourProcessProperty LabProcessProperty = new TrnLabourProcessProperty();
            LabProcessProperty.YYYY = Val.ToInt32(txtYear.Text);
            LabProcessProperty.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);

            LabProcessProperty.PROCESS_ID = Val.ToInt32(CmbProcess.SelectedValue);
            LabProcessProperty.MANAGER_ID = Val.ToInt64(txtManager.Tag);
            LabProcessProperty.MANAGERNAME = Val.ToString(txtManager.Text);
            LabProcessProperty.DEPARTMENT_ID = Val.ToInt32(CmbDepartment.SelectedValue);

            LabProcessProperty.SUBPROCESS_ID = Val.ToInt32(cmbSubProcess.SelectedValue);//Gunjan:30/03/2023
            LabProcessProperty.PLANNINGGRADE_ID = Val.ToInt32(txtPlanningGrade.Tag);//Gunjan:30/03/2023
            LabProcessProperty.LABOURTYPE = Val.ToString(cmbLabourType.SelectedItem);
            LabProcessProperty.ROUGHTYPE = Val.ToString(CmbRoughType.SelectedItem);
            DTabLabProcess = ObjLProcess.FillPlannigGrade(LabProcessProperty);

            DataRow DrProcess = DTabLabProcess.NewRow();
            DTabLabProcess.Rows.Add(DrProcess);
            MainGrid.DataSource = DTabLabProcess;
            MainGrid.Refresh();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
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
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            string StrFileName = Val.ToString(CmbDepartment.Text) + "_" + Val.ToString(CmbMonth.Text) + Val.ToString(txtYear.Text) + "_" + Val.ToString(CmbProcess.Text);

            Global.ExcelExport(StrFileName + "List", GrdDet);
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        TrnLabourProcessProperty Property = new TrnLabourProcessProperty();

                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                        Property.LABOURPROCESS_ID = Val.ToInt64(Drow["LABOURPROCESS_ID"]);
                        Property = ObjLProcess.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DTabLabProcess.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTabLabProcess.AcceptChanges();
                            Fill();
                        }
                        else
                        {
                            Global.Message("ERROR IN DELETE ENTRY");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbMonth.Text.Trim() == string.Empty)
                {
                    Global.Message("Month Is Required");
                    CmbMonth.Focus();
                    return;
                }
                else if (Val.Val(txtYear.Text) == 0)
                {
                    Global.Message("Year Is Required");
                    txtYear.Focus();
                    return;
                }
                else if (CmbProcess.Text.Trim() == string.Empty || Val.ToInt32(CmbProcess.SelectedValue) == 0)
                {
                    Global.Message("Process Is Required");
                    CmbProcess.Focus();
                    return;
                }
                //else if (CmbDepartment.Text.Trim() == string.Empty)
                //{
                //    Global.Message("Shape Is Required");
                //    CmbDepartment.Focus();
                //    return;
                //}
                //else if (txtManager.Text.Trim() == string.Empty)
                //{
                //    Global.Message("Manager Is Required");
                //    txtManager.Focus();
                //    return;
                //}
                else if (txtPlanningGrade.Text.Trim() == string.Empty)
                {
                    Global.Message("Plannig Grade Is Required");
                    txtManager.Focus();
                    return;
                }
                else if (cmbLabourType.Text.Trim() == string.Empty)
                {
                    Global.Message("Labour Is Required");
                    txtManager.Focus();
                    return;
                }

                Fill();

                GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
                GrdDet.FocusedRowHandle = 0;
                GrdDet.ShowEditor();
                GrdDet.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtRate_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        GrdDet.PostEditor();
            //        DataRow dr = GrdDet.GetFocusedDataRow();
            //        if (((Val.Val(dr["FROMCARAT"]) > 0.00 && Val.Val(dr["TOCARAT"]) > 0.00 && Val.Val(dr["RATE"]) > 0.00) || 
            //             (Val.Val(dr["FROMCARAT"]) > 0.00 && Val.Val(dr["TOCARAT"]) > 0.00 && Val.Val(dr["RATE"]) > 0.00)) && GrdDet.IsLastRow)
            //        {
            //            DataRow DrProcess = DTabLabProcess.NewRow();
            //            DTabLabProcess.Rows.Add(DrProcess);
            //        }
            //        else if (GrdDet.IsLastRow)
            //        {
            //            BtnSave.Focus();
            //            e.Handled = true;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
        }

        private void repTxtFromCts_Validating(object sender, CancelEventArgs e)
        {
            GrdDet.PostEditor();
            DataRow Dr = GrdDet.GetFocusedDataRow();
            if (GrdDet.FocusedRowHandle < 0 || Val.Val(GrdDet.EditingValue) == 0)
                return;

            if (Val.Val(Dr["TOCARAT"]) > 0)
                if (Val.Val(GrdDet.EditingValue) > Val.Val(Dr["TOCARAT"]))
                {
                    Global.Message("From Carat Must Be Less Than To Carat.!");
                    e.Cancel = true;
                    return;
                }


            if (CheckDuplicate("FROMCARAT", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "FROM CARAT"))
            {
                e.Cancel = true;
                return;
            }

            var dValue = from row in DTabLabProcess.AsEnumerable()
                         where Val.Val(row["FROMCARAT"]) <= Val.Val(GrdDet.EditingValue) && Val.Val(row["TOCARAT"]) >= Val.Val(GrdDet.EditingValue) && row.Table.Rows.IndexOf(row) != GrdDet.FocusedRowHandle
                         select row;

            if (dValue.Any())
            {
                Global.Message("This Value Already Exist Between Some FromCarat and ToCarat Please Check.!");
                e.Cancel = true;
                return;
            }
        }

        private void repTxtToCts_Validating(object sender, CancelEventArgs e)
        {
            GrdDet.PostEditor();
            DataRow Dr = GrdDet.GetFocusedDataRow();

            if (GrdDet.FocusedRowHandle < 0 || Val.Val(GrdDet.EditingValue) == 0)
                return;

            if (Val.Val(Dr["FROMCARAT"]) > 0)
                if (Val.Val(Dr["FROMCARAT"]) > Val.Val(GrdDet.EditingValue))
                {
                    Global.Message("To CArat Must Be Greater Than From Carat.!");
                    e.Cancel = true;
                    return;
                }

            if (CheckDuplicate("TOCARAT", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "TO CARAT"))
            {
                e.Cancel = true;
            }


            var dValue = from row in DTabLabProcess.AsEnumerable()
                         where Val.Val(row["FROMCARAT"]) <= Val.Val(GrdDet.EditingValue) && Val.Val(row["TOCARAT"]) >= Val.Val(GrdDet.EditingValue) && row.Table.Rows.IndexOf(row) != GrdDet.FocusedRowHandle
                         select row;

            if (dValue.Any())
            {
                Global.Message("This Value Already Exist Between Some FromCarat and ToCarat Please Check.!");
                e.Cancel = true;
                return;
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if (Val.Val(txtYear.Text) == 0)
            {
                Global.Message("Year Is Required");
                txtYear.Focus();
                return;
            }
            if (Val.ToString(CmbMonth.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Month Is Required");
                CmbMonth.Focus();
                return;
            }

            if (Val.Val(txtCopyToYear.Text) == 0)
            {
                Global.Message("Copy To Year Is Required");
                txtCopyToYear.Focus();
                return;
            }
            if (Val.ToString(cmbCopyToMonthOther.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Copy To Month Is Required");
                cmbCopyToMonthOther.Focus();
                return;
            }
          
            if (Val.ToString(cmbCopyToLabour.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Copy To LabourIs Required");
                cmbCopyToLabour.Focus();
                return;
            }

            //if (Val.ToString(txtManager.Text) == "")
            //{
            //    Global.Message("Manager Is Required");
            //    txtManager.Focus();
            //    return;
            //}
            //if (Val.ToString(txtCopyToManager.Text) == "")
            //{
            //    Global.Message("Copy To Manager Is Required");
            //    txtCopyToManager.Focus();
            //    return;
            //}

            //if (Val.ToInt32(CmbCopyToProcess.SelectedValue) == 0)
            //{
            //    Global.Message("Copy To Process Is Required");
            //    CmbCopyToProcess.Focus();
            //    return;
            //}
            //if (Val.ToInt32(CmbProcess.SelectedValue) == 0)
            //{
            //    Global.Message("Process Is Required");
            //    CmbProcess.Focus();
            //    return;
            //}

            //if (Val.Val(CmbDepartment.SelectedValue) == 0)
            //{
            //    Global.Message("Department Is Required");
            //    CmbDepartment.Focus();
            //    return;
            //}
            //if (Val.Val(cmbCopyToDept.SelectedValue) == 0)
            //{
            //    Global.Message("Copy To Department Is Required");
            //    CmbDepartment.Focus();
            //    return;
            //}

            if (Global.Confirm("Are You Sure You Want Transfer Labour Data?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            //int IntRes = ObjLProcess.CopyPasteLabourProcessDataForMakable(Val.ToInt(txtYear.Text), Val.ToInt32(CmbMonth.SelectedIndex + 1), Val.ToInt(txtCopyToYear.Text), Val.ToString(cmbCopyToMonthOther.SelectedIndex + 1).Trim(), Val.ToInt32(CmbProcess.SelectedValue), Val.ToInt32(CmbCopyToProcess.SelectedValue), Val.ToInt32(CmbDepartment.SelectedValue), Val.ToInt32(cmbCopyToDept.SelectedValue), Val.ToInt64(txtManager.Tag), Val.ToInt64(txtCopyToManager.Tag), "", Val.ToInt32(cmbSubProcess.SelectedValue), Val.ToInt32(txtPacketGrade.Tag), Val.ToString(cmbCopyToLabour.SelectedItem));//Val.ToInt32(txtCopyToYear.Text),
            //this.Cursor = Cursors.Default;

            //if (IntRes != -1)
            //{
            //    Global.Message("Data Copied Successfully");
            //    txtCopyToYear.Text = Val.ToString(DateTime.Now.Year);
            //    cmbCopyToMonthOther.SelectedIndex = Val.ToInt(DateTime.Now.Month) - 1;
            //    CmbCopyToProcess.SelectedIndex = 0;
            //    cmbCopyToDept.SelectedIndex = 0;
            //    txtCopyToManager.Text = string.Empty;
            //    txtCopyToManager.Tag = string.Empty;
            //    txtPacketGrade.Text = string.Empty;
            //    txtPacketGrade.Tag = string.Empty;
            //    cmbLabourType.SelectedIndex = 0;
            //    return;
            //}

        }
          private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
           {
            try
            {
                if (txtManager.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE, EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtManager.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtManager.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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
        private void txtCopyToManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtCopyToManager.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE, EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtCopyToManager.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtCopyToManager.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            if (IsNextImage)
            {
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A1;
                PnlCopyPaste.Visible = false;
                IsNextImage = false;
            }
            else
            {
                BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A2;
                PnlCopyPaste.Visible = true;
                IsNextImage = true;
                txtCopyToYear.Focus();
            }
        }

        private void RepCmbLabourType_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDet.PostEditor();
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (((Val.Val(dr["FROMCARAT"]) > 0.00 && Val.Val(dr["TOCARAT"]) > 0.00 && Val.Val(dr["RATE"]) > 0.00) ||
                         (Val.Val(dr["FROMCARAT"]) > 0.00 && Val.Val(dr["TOCARAT"]) > 0.00 && Val.Val(dr["RATE"]) > 0.00)) && GrdDet.IsLastRow)
                    {
                        DataRow DrProcess = DTabLabProcess.NewRow();
                        DTabLabProcess.Rows.Add(DrProcess);
                    }
                    else if (GrdDet.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        

        private void txtCopyToPktGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETGRADENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PACKETGRADE);

                    FrmSearch.mColumnsToHide = "PACKETGRADE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtCopyToPktGrade.Text = Val.ToString(FrmSearch.mDRow["PACKETGRADENAME"]);
                        txtCopyToPktGrade.Tag = Val.ToString(FrmSearch.mDRow["PACKETGRADE_ID"]);
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

        private void txtPlanningGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PLANNINGCODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                   FrmSearch.mDTab = ObjLProcess.GetPlanningGrade(Val.ToString(CmbRoughType.SelectedItem));

                    FrmSearch.mColumnsToHide = "PLANNINGGRADE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPlanningGrade.Text = Val.ToString(FrmSearch.mDRow["PLANNINGNAME"]);
                        txtPlanningGrade.Tag = Val.ToString(FrmSearch.mDRow["PLANNINGGRADE_ID"]);
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

        private void CmbProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (CmbProcess.SelectedItem == "2726") // PLANNING PROCESS
            //{
            //    txtPlanningGrade.Enabled = true;
            //}
            //else
            //{
            //    txtPlanningGrade.Enabled = false;
            //}

        }
    }
}