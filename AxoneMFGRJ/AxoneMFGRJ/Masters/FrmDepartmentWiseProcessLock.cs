using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmDepartmentWiseProcessLock : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        BOMST_DepartmentWiseProcessLock ObjMast = new BOMST_DepartmentWiseProcessLock();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        DataTable DTabDept = new DataTable();
        #region Property Setting
        public FrmDepartmentWiseProcessLock()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DataTable DtabDept = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
            CmbDepartment.DataSource = DtabDept;
            CmbDepartment.DisplayMember = "DEPARTMENTNAME";
            CmbDepartment.ValueMember = "DEPARTMENT_ID";
            CmbDepartment.SelectedIndex = -1;
            CmbDepartment.SelectedIndex = 0;
            this.Show();
        }

        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        private bool ValSave()
        {
            int IsSameEntryCount = 0;

            for (int i = 0; i < GrdDet.RowCount; i++)
            {
                DataRow DR = GrdDet.GetDataRow(i);

                //DataRow DRNew = GrdDet.GetFocusedDataRow(); //For Get focused Row 

                if (Val.ToString(DR["PREVENTRYTYPE"]) != "" && Val.ToString(DR["NEXTENTRYTYPE"]) != "")
                {
                    if (Val.ToString(DR["PREVENTRYTYPE"]) == Val.ToString(DR["NEXTENTRYTYPE"]))
                    {
                        Global.Message("Please select Valid EntryTypes..");
                        return true;
                    }
                }                
            }
            return false;
        }


        #endregion

        private void Fill()
        {
            DTabDept = ObjMast.Fill(Val.ToInt32(CmbDepartment.SelectedValue));
            MainGrid.DataSource = DTabDept;

            //if (DTabDept.Rows.Count > 0)
            //{
            //    DTabDept.Rows.Add(DTabDept.NewRow());
            //}
            DTabDept.Rows.Add(DTabDept.NewRow());
            MainGrid.Refresh();
            GrdDet.BestFitColumns();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ValSave())
                //{
                //    return;
                //}

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                if (Global.Confirm("Are You Sure You Want To Save This Entry ??") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                //var DRows = DTabDept.Rows.Cast<DataRow>().Where(row => Val.ToString(row["PREVENTRYTYPE"]) == "").ToArray();
                //foreach (DataRow dr in DRows)
                //    DTabDept.Rows.Remove(dr);

                foreach (DataRow Dr in DTabDept.Rows)
                {
                    DepartmentWiseProcessLockProperty Property = new DepartmentWiseProcessLockProperty();

                    //if (Val.ToString(Dr["PROCESSLOCK_ID"]) == "" && Val.ToString(Dr["PREVENTRYTYPE"]) == "")
                    //{
                    //    Global.Message("Please Select Previous EntryType...");
                    //    this.Cursor = Cursors.Default;
                    //    return ;
                    //}

                    if (Val.ToString(Dr["NEXTENTRYTYPE"]).Trim().Equals(string.Empty))
                        continue;

                    Property.PROCESSLOCK_ID = Val.ToInt32(Dr["PROCESSLOCK_ID"]);
                    Property.DEPARTMENT_ID = Val.ToInt32(CmbDepartment.SelectedValue);
                    Property.PREVENTRYTYPE = Val.ToString(Dr["PREVENTRYTYPE"]);
                    Property.PREVPROCESS_ID = Val.ToInt32(Dr["PREVPROCESS_ID"]);
                    Property.NEXTENTRYTYPE = Val.ToString(Dr["NEXTENTRYTYPE"]);
                    Property.NEXTPROCESS_ID = Val.ToInt32(Dr["NEXTPROCESS_ID"]);
                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);

                    Property = ObjMast.Save(Property);
                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DTabDept.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    this.Cursor = Cursors.Default;
                    Fill();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void CmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            Fill();
        }

        private void Clear()
        {
            DTabDept.Rows.Clear();
            // DTabDept.Rows.Add(DTabDept.NewRow());
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void repTxtPrevProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID,PROCESSGROUP,PROCESSCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;

                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("PREVPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                        GrdDet.SetFocusedRowCellValue("PREVPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                }
            }
            catch { }
        }

        private void repTxtNextProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID,PROCESSGROUP,PROCESSCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;

                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("NEXTPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                        GrdDet.SetFocusedRowCellValue("NEXTPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                }
            }
            catch { }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            CmbDepartment.SelectedValue = 0;
            Clear();
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDet.PostEditor();
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["PREVENTRYTYPE"]).Equals(string.Empty) && GrdDet.IsLastRow)
                    {
                        DTabDept.Rows.Add(DTabDept.NewRow());
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
        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        DepartmentWiseProcessLockProperty Property = new DepartmentWiseProcessLockProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.PROCESSLOCK_ID = Val.ToInt32(Drow["PROCESSLOCK_ID"]);
                        Property = ObjMast.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DTabDept.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTabDept.AcceptChanges();
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

        public bool CheckDuplicate(string ColPrevEntryType, string ColPrevProcess_ID, string ColNextEntryType, string ColNextProcess_ID, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColPrevEntryType).Trim().Equals(string.Empty)
                || Val.ToString(ColPrevProcess_ID).Trim().Equals(string.Empty)
                || Val.ToString(ColNextEntryType).Trim().Equals(string.Empty)
                || Val.ToString(ColNextProcess_ID).Trim().Equals(string.Empty)
                )
                return false;

            var Result = from row in DTabDept.AsEnumerable()
                         where Val.ToString(row["PREVENTRYTYPE"]).ToUpper() == Val.ToString(ColPrevEntryType).ToUpper()
                               && Val.ToString(row["PREVPROCESS_ID"]).ToUpper() == Val.ToString(ColPrevProcess_ID).ToUpper()
                               && Val.ToString(row["NEXTENTRYTYPE"]).ToUpper() == Val.ToString(ColNextEntryType).ToUpper()
                               && Val.ToString(row["NEXTPROCESS_ID"]).ToUpper() == Val.ToString(ColNextProcess_ID).ToUpper()
                               && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message("This Selection Type Entry Is Already Exists.");
                GrdDet.SetFocusedRowCellValue(GrdDet.FocusedColumn.FieldName, "");
                return true;
            }
            return false;
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GrdDet.PostEditor();
            DataRow Dr = GrdDet.GetFocusedDataRow();

            switch (e.Column.FieldName)
            {
                case "PREVENTRYTYPE":
                case "PREVPROCESSNAME":
                case "NEXTENTRYTYPE":
                case "NEXTPROCESSNAME":
                    CheckDuplicate(Val.ToString(Dr["PREVENTRYTYPE"]), Val.ToString(Dr["PREVPROCESS_ID"]), Val.ToString(Dr["NEXTENTRYTYPE"]), Val.ToString(Dr["NEXTPROCESS_ID"]), GrdDet.FocusedRowHandle,"");
                    break;
            }
        }
    }
}

