using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
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
    public partial class FrmProcessWiseLossPer : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_ProcessWiseLossPer ObjMast = new BOMST_ProcessWiseLossPer();
        DataTable DtabLoss = new DataTable();

        FormType mFormType = FormType.DEPTLOCK;

        public enum FormType
        {
            DEPTLOCK,
            EMPLOCK,
            KAPAN
        }

        #region Property Settings
        public FrmProcessWiseLossPer()
        {
            InitializeComponent();
        }
        public void ShowForm(FormType pFormType)
        {
            mFormType = pFormType;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            if (mFormType == FormType.DEPTLOCK)
            {
                this.Text = "DEPARTMENT WISE LOCK";
                GrdDet.Columns["EMPLOYEENAME"].Visible = false;
            }
            else if (mFormType == FormType.KAPAN)
            {
                this.Text = "KAPAN WISE LOCK";
                GrdDet.Columns["EMPLOYEENAME"].Visible = false;
            }
            else
            {
                this.Text = "EMPLOYEE WISE LOCK";
            }

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            DataTable DTabDepartment = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
            CmbDepartment.DataSource = DTabDepartment;
            CmbDepartment.ValueMember = "DEPARTMENT_ID";
            CmbDepartment.DisplayMember = "DEPARTMENTNAME";

            BtnAdd_Click(null, null);
            Fill();
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
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion
       
        public void Fill()
        {
            DtabLoss = ObjMast.Fill(0,Val.ToInt32(CmbDepartment.SelectedValue),mFormType.ToString());
            DtabLoss.Rows.Add(DtabLoss.NewRow());
            MainGrid.DataSource = DtabLoss;
            MainGrid.Refresh();

        }
        public void Clear()
        {
            DtabLoss.Rows.Clear();
            DtabLoss.Rows.Add(DtabLoss.NewRow());
        }

        private void MainGrid_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                foreach (DataRow Dr in DtabLoss.Rows)
                {
                    ProcessMasterProperty Property = new ProcessMasterProperty();

                    if (Val.Val(Dr["FROMCARAT"]) + Val.Val(Dr["TOCARAT"]) == 0)
                        continue;

                    Property.PER_ID = Val.ToInt32(Dr["PER_ID"]);

                    if (mFormType == FormType.DEPTLOCK)
                    {
                        if (Val.ToString(Dr["DEPARTMENTNAME"]).Trim().Length == 0)
                            continue;
                        if (Val.ToString(Dr["PROCESSNAME"]).Trim().Length == 0)
                            continue;
                        Property.DEPARTMENT_ID = Val.ToInt(Dr["DEPARTMENT_ID"]);
                        Property.PROCESS_ID = Val.ToInt(Dr["PROCESS_ID"]); 
                        Property.EMPLOYEE_ID = 0;
                    }
                    else if (mFormType == FormType.EMPLOCK)
                    {
                        if (Val.ToString(Dr["EMPLOYEENAME"]).Trim().Length == 0)
                            continue;
                        if (Val.ToString(Dr["DEPARTMENTNAME"]).Trim().Length == 0)
                            continue;
                        if (Val.ToString(Dr["PROCESSNAME"]).Trim().Length == 0)
                            continue;
                        Property.DEPARTMENT_ID = Val.ToInt(Dr["DEPARTMENT_ID"]);
                        Property.PROCESS_ID = Val.ToInt(Dr["PROCESS_ID"]); 
                        Property.EMPLOYEE_ID = Val.ToInt64(Dr["EMPLOYEE_ID"]);
                    }
                    Property.RoughType = Val.ToString(Dr["ROUGHTYPE"]);
                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);
                    Property.NoOfIssue = Val.ToInt(Dr["NOOFISSUE"]);
                    Property.AGEINGHOURS = Val.Val(Dr["AGEINGHOURS"]);
                    Property.PER = Val.Val(Dr["PER"]);
                    Property.ISIssueReturnLock = Val.ToBoolean(Dr["ISISSUERETURNLOCK"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property.KAPAN_ID = Val.ToInt64(Dr["KAPANID"]);

                    Property = ObjMast.Save(Property, mFormType.ToString());

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Dr["PER_ID"] = Property.ReturnValue;
                    Property = null;
                }
                DtabLoss.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    //BtnAdd_Click(null, null);

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                    //txtItemGroupCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
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
                    if (!Val.ToString(dr["FROMCARAT"]).Equals(string.Empty) && !Val.ToString(dr["TOCARAT"]).Equals(string.Empty) && GrdDet.IsLastRow)
                    {
                        DtabLoss.Rows.Add(DtabLoss.NewRow());
                        //DtabPara.AcceptChanges();

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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PROCESS LOSS", GrdDet);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        ProcessMasterProperty Property = new ProcessMasterProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.PER_ID = Val.ToInt32(Drow["PER_ID"]);
                        Property = ObjMast.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabLoss.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabLoss.AcceptChanges();
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


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void txtProcess_Validating(object sender, CancelEventArgs e)
        {
            //DataRow Dr = GrdDet.GetFocusedDataRow();
            //if (CheckDuplicate("PROCESSCODE", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "PROCESSNAME"))
              //  e.Cancel = true;
            //return;
        }

        private void txtPrc_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtRepEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEEDISPLAY);
                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("EMPLOYEENAME", Val.ToString(FrmSearch.mDRow["LEDGERNAME"]));
                        GrdDet.SetFocusedRowCellValue("EMPLOYEE_ID", Val.ToString(FrmSearch.mDRow["LEDGER_ID"]));

                        GrdDet.SetFocusedRowCellValue("DEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));
                        GrdDet.SetFocusedRowCellValue("DEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
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

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_DEPARTMENT);
                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("DEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));
                        GrdDet.SetFocusedRowCellValue("DEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
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

        private void txtProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("PROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        GrdDet.SetFocusedRowCellValue("PROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            Fill();
        }

        private void ReptxtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME,KAPAN_ID";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOTRN_SinglePacketCreate().FindKapan();
                    FrmSearch.mColumnsToHide = "KAPAN_ID, MANAGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("KAPANNAME", Val.ToString(FrmSearch.mDRow["KAPANNAME"]));
                        GrdDet.SetFocusedRowCellValue("KAPANID", Val.ToString(FrmSearch.mDRow["KAPAN_ID"]));
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
    }
}
