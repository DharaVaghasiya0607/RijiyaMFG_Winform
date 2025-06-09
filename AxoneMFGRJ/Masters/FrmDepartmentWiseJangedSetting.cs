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
    public partial class FrmDepartmentWiseJangedSetting : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        BOMST_DepartmentWiseJangedSetting ObjMast = new BOMST_DepartmentWiseJangedSetting();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        DataTable DTabDept = new DataTable();
        #region Property Setting
        public FrmDepartmentWiseJangedSetting()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            Fill();

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

        #endregion

        private void Fill()
        {
            DTabDept = ObjMast.Fill();
            MainGrid.DataSource = DTabDept;
            DTabDept.Rows.Add(DTabDept.NewRow());
            MainGrid.Refresh();
        }

        private bool ValSave()
        {
            int IntCol = 0, IntRow = 0;
            foreach (DataRow dr in DTabDept.Rows)
            {
                //For Update Validation
                if (Val.ToString(dr["STARTFROMDEPARTMENTNAME"]).Trim().Equals(string.Empty) && !Val.ToString(dr["STARTFROMDEPARTMENT_ID"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Enter Start Department");
                    IntCol = 0;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                //end as


                if (Val.ToString(dr["STARTFROMDEPARTMENTNAME"]).Trim().Equals(string.Empty))
                {
                    if (DTabDept.Rows.Count == 1)
                    {
                        Global.Message("Please Entrt Start From Departmentname name");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }

                if (Val.ToString(dr["ENDFROMDEPARTMENTNAME"]).Trim().Equals(string.Empty))
                {
                    if (DTabDept.Rows.Count == 1)
                    {
                        Global.Message("Please Entrt End From Departmentname name");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }

                if (Val.ToString(dr["STARTTODEPARTMENTNAME"]).Trim().Equals(string.Empty))
                {
                    if (DTabDept.Rows.Count == 1)
                    {
                        Global.Message("Please Start To Departmentname name");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }

                if (Val.ToString(dr["ENDTODEPARTMENTNAME"]).Trim().Equals(string.Empty))
                {
                    if (DTabDept.Rows.Count == 1)
                    {
                        Global.Message("Please To End To Departmentname name");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }


                if (Val.ToString(dr["STARTFROMPROCESSNAME"]).Trim().Equals(string.Empty))
                {
                    if (DTabDept.Rows.Count == 1)
                    {
                        Global.Message("Please From Start Process name");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }


                if (Val.ToString(dr["STARTTOPROCESSNAME"]).Trim().Equals(string.Empty))
                {
                    if (DTabDept.Rows.Count == 1)
                    {
                        Global.Message("Please start To Process name");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }


                if (Val.ToString(dr["ENDFROMPROCESSNAME"]).Trim().Equals(string.Empty))
                {
                    if (DTabDept.Rows.Count == 1)
                    {
                        Global.Message("Please End From Process name");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }


                if (Val.ToString(dr["ENDTOPROCESSNAME"]).Trim().Equals(string.Empty))
                {
                    if (DTabDept.Rows.Count == 1)
                    {
                        Global.Message("Please End To Process name");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }


            }
            if (IntRow > 0)
            {
                GrdDet.FocusedRowHandle = IntRow;
                GrdDet.FocusedColumn = GrdDet.VisibleColumns[IntCol];
                GrdDet.Focus();
                return true;
            }
            return false;
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

                if (Global.Confirm("Are You Sure You Want To Save This Entry ??") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;


                foreach (DataRow Dr in DTabDept.Rows)
                {
                    DepartmentWiseJangedSettingProperty Property = new DepartmentWiseJangedSettingProperty();

                    if ((Val.ToString(Dr["STARTFROMDEPARTMENTNAME"]).Trim().Equals(string.Empty)) && (Val.ToString(Dr["STARTTODEPARTMENTNAME"]).Trim().Equals(string.Empty)) && (Val.ToString(Dr["STARTFROMPROCESSNAME"]).Trim().Equals(string.Empty)) && (Val.ToString(Dr["STARTTOPROCESSNAME"]).Trim().Equals(string.Empty)) &&
                        (Val.ToString(Dr["ENDFROMDEPARTMENTNAME"]).Trim().Equals(string.Empty)) && (Val.ToString(Dr["ENDTODEPARTMENTNAME"]).Trim().Equals(string.Empty)) && (Val.ToString(Dr["ENDFROMPROCESSNAME"]).Trim().Equals(string.Empty)) && (Val.ToString(Dr["ENDTOPROCESSNAME"]).Trim().Equals(string.Empty)))
                        continue;

                    Property.ID = Val.ToInt32(Dr["ID"]);
                    Property.STARTFROMPROCESS_ID = Val.ToInt32(Dr["STARTFROMPROCESS_ID"]);
                    Property.STARTTOPROCESS_ID = Val.ToInt32(Dr["STARTTOPROCESS_ID"]);
                    Property.STARTFROMDEPARTMENT_ID = Val.ToInt32(Dr["STARTFROMDEPARTMENT_ID"]);
                    Property.STARTTODEPARTMENT_ID = Val.ToInt32(Dr["STARTTODEPARTMENT_ID"]);
                    Property.STARTENTRYTYPE = Val.ToString(Dr["STARTENTRYTYPE"]);

                    Property.ENDFROMPROCESS_ID = Val.ToInt32(Dr["ENDFROMPROCESS_ID"]);
                    Property.ENDTOPROCESS_ID = Val.ToInt32(Dr["ENDTOPROCESS_ID"]);
                    Property.ENDFROMDEPARTMENT_ID = Val.ToInt32(Dr["ENDFROMDEPARTMENT_ID"]);
                    Property.ENDTODEPARTMENT_ID = Val.ToInt32(Dr["ENDTODEPARTMENT_ID"]);
                    Property.ENDENTRYTYPE = Val.ToString(Dr["ENDENTRYTYPE"]);

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
                        GrdDet.SetFocusedRowCellValue("STARTFROMPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                        GrdDet.SetFocusedRowCellValue("STARTFROMPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
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
                        GrdDet.SetFocusedRowCellValue("STARTTOPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                        GrdDet.SetFocusedRowCellValue("STARTTOPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));

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
            Clear();
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["ENDTOPROCESSNAME"]).Equals(string.Empty) && GrdDet.IsLastRow)
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
                        DepartmentWiseJangedSettingProperty Property = new DepartmentWiseJangedSettingProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.ID = Val.ToInt32(Drow["ID"]);
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

        
       
        private void RepFromDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (Global.OnKeyPressEveToPopup(e))
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
                        GrdDet.SetFocusedRowCellValue("STARTFROMDEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdDet.SetFocusedRowCellValue("STARTFROMDEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                }
            }
            catch { }
        }

        private void RepTxtToDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
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
                        GrdDet.SetFocusedRowCellValue("STARTTODEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdDet.SetFocusedRowCellValue("STARTTODEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));

                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                }
            }
            catch { }
        }

        private void RepTxtEndToProcess_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("ENDTOPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                        GrdDet.SetFocusedRowCellValue("ENDTOPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));

                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                }
            }
            catch { }
        }

        private void ReptxtEndFromProcess_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("ENDFROMPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                        GrdDet.SetFocusedRowCellValue("ENDFROMPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));

                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                }
            }
            catch { }
        }

        private void ReptxtEndToDept_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
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
                        GrdDet.SetFocusedRowCellValue("ENDTODEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdDet.SetFocusedRowCellValue("ENDTODEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));

                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                }
            }
            catch { }
        }

        private void RepTxtEndFromDept_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
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
                        GrdDet.SetFocusedRowCellValue("ENDFROMDEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdDet.SetFocusedRowCellValue("ENDFROMDEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));

                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                }
            }
            catch { }
        }
    }
}

