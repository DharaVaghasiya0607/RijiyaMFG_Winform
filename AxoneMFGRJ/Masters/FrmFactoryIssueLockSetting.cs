using BusLib;
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
    public partial class FrmFactoryIssueLockSetting : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_FactoryIssueLockSetting ObjMast = new BOMST_FactoryIssueLockSetting();
        DataTable DTabFactoryIssueLockSetting = new DataTable();

        #region Property Settings

        public FrmFactoryIssueLockSetting()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            //ObjPer.GetFormPermission(Val.ToString(this.Tag));
            //BtnSave.Enabled = ObjPer.ISINSERT;
            //DeleteSelectedToolStripMenuItem.Enabled = ObjPer.ISDELETE;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DTabFactoryIssueLockSetting.Columns.Add("EMPLOYEE_ID", typeof(System.Int64));
            DTabFactoryIssueLockSetting.Columns.Add("SHAPE_ID", typeof(System.Int32));
            DTabFactoryIssueLockSetting.Columns.Add("SHAPE", typeof(System.String));
            DTabFactoryIssueLockSetting.Columns.Add("FROMCARAT", typeof(System.Double));
            DTabFactoryIssueLockSetting.Columns.Add("TOCARAT", typeof(System.Double));
            DTabFactoryIssueLockSetting.Columns.Add("COLOUR", typeof(System.String));
            DTabFactoryIssueLockSetting.Columns.Add("CLARITY", typeof(System.String));

            DataTable DTabColour = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);
            repChkCmbColour.DataSource = DTabColour;
            repChkCmbColour.DisplayMember = "COLORNAME";
            repChkCmbColour.ValueMember = "COLOR_ID";

            DataTable DTabClarity = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);
            repChkCmbClarity.DataSource = DTabClarity;
            repChkCmbClarity.DisplayMember = "CLARITYNAME";
            repChkCmbClarity.ValueMember = "CLARITY_ID";

            this.Show();
            Fill();
            //Clear();
        }
        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = false;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }
        #endregion

        public void Clear()
        {
            DTabFactoryIssueLockSetting.Rows.Clear();
            Fill();
        }
        public void Fill()
        {
            DTabFactoryIssueLockSetting = ObjMast.Fill();
            DataRow DR = DTabFactoryIssueLockSetting.NewRow();
            DTabFactoryIssueLockSetting.Rows.Add(DR);
            MainGrid.DataSource = DTabFactoryIssueLockSetting;
            MainGrid.Refresh();
            GrdDet.FocusedRowHandle = DR.Table.Rows.IndexOf(DR);
            GrdDet.FocusedColumn = GrdDet.Columns[0];
            GrdDet.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                foreach (DataRow Dr in DTabFactoryIssueLockSetting.Rows)
                {
                    FactoryIssueLockSettingProperty Property = new FactoryIssueLockSettingProperty();

                    if (Val.ToString(Dr["SHAPE_ID"]).Trim().Equals(string.Empty))
                        continue;

                    Property.PROCESS_ID = Val.ToInt32(Dr["PROCESS_ID"]);
                    Property.DEPARTMENT_ID = Val.ToInt32(Dr["DEPARTMENT_ID"]);
                    Property.EMPLOYEE_ID = Val.ToInt64(Dr["EMPLOYEE_ID"]);
                    Property.SHAPE = Val.ToString(Dr["SHAPE"]);
                    Property.SHAPE_ID = Val.ToInt32(Dr["SHAPE_ID"]);
                    Property.FROMCARAT = Val.ToDouble(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.ToDouble(Dr["TOCARAT"]);                                                                                                                                               
                    Property.COLOUR = Val.ToString(Dr["COLOUR"]);
                    Property.CLARITY = Val.ToString(Dr["CLARITY"]);

                    Property = ObjMast.Save(Property);
                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DTabFactoryIssueLockSetting.AcceptChanges();
                BtnSave.Focus();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Clear();

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        FactoryIssueLockSettingProperty Property = new FactoryIssueLockSettingProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.EMPLOYEE_ID = Int64.Parse(Val.ToString(Drow["EMPLOYEE_ID"]));

                        Property = ObjMast.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DTabFactoryIssueLockSetting.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTabFactoryIssueLockSetting.AcceptChanges();
                            Clear();
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
        private void BtnExport_Click(object sender, EventArgs e)
        {
            
        }
        private void repTxtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "PARA_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDet.SetFocusedRowCellValue("SHAPE", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
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

        private void repChkCmbClarity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["SHAPE"]).Equals(string.Empty) && !Val.ToString(dr["FROMCARAT"]).Equals(string.Empty) && GrdDet.IsLastRow)
                    {
                        DTabFactoryIssueLockSetting.Rows.Add(DTabFactoryIssueLockSetting.NewRow());
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

        private void ReptxtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTCODE,DEPARTMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("DEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdDet.SetFocusedRowCellValue("DEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("DEPARTMENT_ID", 0);
                        GrdDet.SetFocusedRowCellValue("DEPARTMENTNAME", string.Empty);
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

        private void ReptxtProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("PROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                        GrdDet.SetFocusedRowCellValue("PROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("PROCESS_ID", 0);
                        GrdDet.SetFocusedRowCellValue("PROCESSNAME", string.Empty);
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

        }
    }
}
