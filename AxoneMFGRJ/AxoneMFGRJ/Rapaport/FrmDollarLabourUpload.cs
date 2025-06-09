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

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmDollarLabourUpload : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMT_DollarLabourUpload ObjLProcess = new BOMT_DollarLabourUpload();
        DataTable DTabLabProcess = new DataTable();

        bool IsNextImage = true;

        #region Property Settings

        public FrmDollarLabourUpload()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            CmbSize.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DOLLARLABOURSIZE);
            CmbSize.ValueMember = "SIZE_ID";
            CmbSize.DisplayMember = "SIZENAME";

            CmbShape.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
            CmbShape.ValueMember = "SHAPE_ID";
            CmbShape.DisplayMember = "SHAPENAME";

            BtnAdd_Click(null, null);

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

            else if (CmbShape.Text.Trim() == string.Empty)
            {
                Global.Message("SHAPE Is Required");
                CmbShape.Focus();
                return true;
            }

            else if (CmbSize.Text.Trim() == string.Empty)
            {
                Global.Message("SIZE Is Required");
                CmbSize.Focus();
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
                   

                   
                }

               
            }
            if (IntRow >= 0)
            {
                GridData.FocusedRowHandle = IntRow;
                GridData.FocusedColumn = GridData.VisibleColumns[IntCol];
                GridData.Focus();
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
            CmbShape.SelectedIndex = 0;
            CmbSize.SelectedIndex = 0;
            TxtSym.Tag = string.Empty;
            TxtCut.Tag = string.Empty;
            TxtPol.Tag = string.Empty;
                       
            CmbMonth.Focus();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
                    if ((Val.ToString(Dr["CUTNAME"]).Trim().Equals(string.Empty) || Val.ToString(Dr["POLNAME"]).Trim().Equals(string.Empty) || Val.ToString(Dr["SYMNAME"]).Trim().Equals(string.Empty) || Val.Val(Dr["RATE"]) <= 0))
                        continue;

                    DollarLabourUploadProperty Property = new DollarLabourUploadProperty();

                    Property.LABOUR_ID = Val.ToInt(Dr["LABOUR_ID"]);
                    Property.YYYY = Val.ToInt(txtYear.Text);
                    Property.MM = Val.ToInt(CmbMonth.SelectedIndex + 1);

                    Property.SIZE_ID = Val.ToInt32(CmbSize.SelectedValue);

                    Property.SHAPE_ID = Val.ToInt(CmbShape.SelectedValue);
                    Property.BONUSPER = Val.Val(Dr["BONUSPER"]);
                    Property.RATE = Val.Val(Dr["RATE"]);
                    Property.LABOURTYPE = Val.ToString(Dr["LABOURTYPE"]);



                    Property.POL_ID = Val.ToInt(Dr["POL_ID"]); 
                    Property.SYM_ID = Val.ToInt(Dr["SYM_ID"]);
                    Property.CUT_ID = Val.ToInt(Dr["CUT_ID"]);

                    Property = ObjLProcess.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DTabLabProcess.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    if (GridData.RowCount > 1)
                    {
                        GridData.FocusedRowHandle = GridData.RowCount - 1;
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

        private void TxtCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GridData.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CUTCODE,CUTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CUT);
                    FrmSearch.mColumnsToHide = "CUT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GridData.SetFocusedRowCellValue("CUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        GridData.SetFocusedRowCellValue("CUT_ID", Val.ToInt32(FrmSearch.mDRow["CUT_ID"]));                        
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

        private void TxtPol_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GridData.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {

                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "POLCODE,POLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_POL);
                    FrmSearch.mColumnsToHide = "POL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GridData.SetFocusedRowCellValue("POLNAME", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                        GridData.SetFocusedRowCellValue("POL_ID", Val.ToInt32(FrmSearch.mDRow["POL_ID"]));

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

        private void btnshow_Click(object sender, EventArgs e)
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
            
                else if (CmbShape.Text.Trim() == string.Empty)
                {
                    Global.Message("Shape Is Required");
                    CmbShape.Focus();
                    return;
                }
                else if (CmbSize.Text.Trim() == string.Empty)
                {
                    Global.Message("SIZE Is Required");
                    SIZE.Focus();
                    return;
                }

                Fill();

                GridData.FocusedColumn = GridData.VisibleColumns[0];
                GridData.FocusedRowHandle = 0;
                GridData.ShowEditor();
                GridData.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        public void Fill()
        {
            DollarLabourUploadProperty DollarProcessProperty = new DollarLabourUploadProperty();
            DollarProcessProperty.YYYY = Val.ToInt(txtYear.Text);
            DollarProcessProperty.MM = Val.ToInt(CmbMonth.SelectedIndex + 1);
            DollarProcessProperty.SHAPE_ID= Val.ToInt(CmbShape.SelectedValue);
            DollarProcessProperty.SIZE_ID = Val.ToInt(CmbSize.SelectedValue);
            DTabLabProcess = ObjLProcess.Fill(DollarProcessProperty);

            DataRow DrProcess = DTabLabProcess.NewRow();
            DTabLabProcess.Rows.Add(DrProcess);
            MainGrid.DataSource = DTabLabProcess;
            MainGrid.Refresh();

        }

        private void TxtSym_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GridData.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {

                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SYMNAME,SYMNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SYM);
                    FrmSearch.mColumnsToHide = "SYM_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GridData.SetFocusedRowCellValue("SYMNAME", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                        GridData.SetFocusedRowCellValue("SYM_ID", Val.ToInt32(FrmSearch.mDRow["SYM_ID"]));

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

        private void GridData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Clicks == 2)
            {
                DataRow DR = GridData.GetDataRow(e.RowHandle);
                FetchValue(DR);
                DR = null;
            }
        }
        public void FetchValue(DataRow DR)
        {
            //txtParaType.Text = Val.ToString(DR["ITEMGROUP_ID"]);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            string StrFileName = Val.ToString(CmbSize.Text) + "_" + Val.ToString(CmbMonth.Text) + Val.ToString(txtYear.Text) + "_" + Val.ToString(CmbShape.Text);

            Global.ExcelExport(StrFileName + "List", GridData);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteSelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridData.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        DollarLabourUploadProperty Property = new DollarLabourUploadProperty();

                        DataRow Drow = GridData.GetDataRow(GridData.FocusedRowHandle);

                        Property.LABOUR_ID = Val.ToInt(Drow["LABOUR_ID"]);
                        Property = ObjLProcess.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DTabLabProcess.Rows.RemoveAt(GridData.FocusedRowHandle);
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

        private void TxtBonusPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GridData.GetFocusedDataRow();
                    if (Val.ToString(dr["CUTNAME"]) != "" && GridData.IsLastRow)
                    {
                        DataRow DrProcess = DTabLabProcess.NewRow();
                        DTabLabProcess.Rows.Add(DrProcess);

                    }
                    else if (GridData.IsLastRow)
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

        private void RepCmbLabourType_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
    
}
