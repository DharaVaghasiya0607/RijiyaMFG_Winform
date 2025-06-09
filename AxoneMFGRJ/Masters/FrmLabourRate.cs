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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmLabourRate : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_LabourRate ObjTrnLabourRate = new BOTRN_LabourRate();
        
        DataTable DtabLabour = new DataTable();

        #region Property Settings

        public FrmLabourRate()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
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
            ObjFormEvent.ObjToDisposeList.Add(ObjTrnLabourRate);
            ObjFormEvent.ObjToDisposeList.Add(Val);            
        }

        #endregion

        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabLabour.AsEnumerable()
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
            if (Val.ToString(txtProcess.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Please Select Process");
                txtProcess.Focus();
                return true;
            }
            int IntCol = 0, IntRow = 0;
            foreach (DataRow dr in DtabLabour.Rows)
            {

                if ((Val.ToString(dr["LABOURTYPE"]).Trim().Equals(string.Empty)) && (Val.Val(dr["LABOURRATE"]) == 0))
                    continue;

                
                if (Val.ToString(dr["LABOURTYPE"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Labour Type");
                    IntCol = 1;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }



                if (Val.Val(dr["LABOURRATE"])==0)
                {
                    Global.Message("Please Enter Labour Rate");
                    IntCol = 2;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }


                if (Val.Val(dr["LABOURRATE"])<= 0)
                {
                    Global.Message("Please Enter Proper Labour Rate");
                    IntCol = 2;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
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

        #region Enter Event

        private void ControlEnterForGujarati_Enter(object sender, EventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.GUJARATI);
        }
        private void ControlEnterForEnglish_Enter(object sender, EventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
        }


        #endregion

        public void Clear()
        {
            DtabLabour.Rows.Clear();
            DtabLabour.Rows.Add(DtabLabour.NewRow());

            txtProcess.Text = string.Empty;
            txtProcess.Tag = string.Empty;
            txtProcess.Focus();
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


                foreach (DataRow Dr in DtabLabour.Rows)
                {
                    TrnLabourRateProperty Property = new TrnLabourRateProperty();

                    //Property.ITEMGROUP_ID = Val.ToInt64(txtParaType.Text);
                    //Property.PARATYPE = Val.ToString(CmbParameterType.SelectedItem);

                    if (Val.ToString(Dr["LABOURTYPE"]).Trim().Equals(string.Empty) || Val.Val(Dr["LABOURRATE"])==0)
                        continue;

                    Property.PROCESS_ID = Val.ToInt32(Dr["PROCESS_ID"]);
                    Property.CHARNI_ID = Val.ToInt32(Dr["CHARNI_ID"]);
                    Property.LABOURTYPE = Val.ToString(Dr["LABOURTYPE"]);

                    Property.LABOURRATE = Val.Val(Dr["LABOURRATE"]);
                    Property = ObjTrnLabourRate.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {

                    BtnSearch_Click(null, null);
                    //BtnAdd_Click(null, null);

                    //if (GrdDet.RowCount > 1)
                    //{
                    //    GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    //}
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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    BtnBack_Click(null, null);
            }
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
        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataRow DR = GrdDet.GetFocusedDataRow();
            //    FetchValue(DR);
            //    DR = null;
            //}
        }


        public void FetchValue(DataRow DR)
        {
            //txtParaType.Text = Val.ToString(DR["ITEMGROUP_ID"]);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Labour Master List", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["PARACODE"]).Equals(string.Empty) && !Val.ToString(dr["PARANAME"]).Equals(string.Empty) && GrdDet.IsLastRow)
                    {
                        DtabLabour.Rows.Add(DtabLabour.NewRow());
                        DtabLabour.AcceptChanges();

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

        private void repTxtParaName_Validating(object sender, CancelEventArgs e)
        {
            DataRow Dr = GrdDet.GetFocusedDataRow();
            if (CheckDuplicate("PARANAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "Name"))
                e.Cancel = true;
            return;

        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        ParameterMasterProperty Property = new ParameterMasterProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.PARA_ID = Val.ToInt32(Drow["PARA_ID"]);
                        //Property = ObjTrnLabourRate.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabLabour.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabLabour.AcceptChanges();
                        
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

        private void repTxtParaCode_Validating(object sender, CancelEventArgs e)
        {
            DataRow Dr = GrdDet.GetFocusedDataRow();
            if (CheckDuplicate("PARACODE", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "Code"))
                e.Cancel = true;
            return;

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
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);

                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                        BtnSearch_Click(null, null);
                    }
                    else
                    {
                        txtProcess.Text = Val.ToString(DBNull.Value);
                        txtProcess.Tag = Val.ToString(DBNull.Value);
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

        private void BtnAddProcess_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("PROCESS");
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (Val.ToString(txtProcess.Tag).Trim().Equals(string.Empty))
            {
                Global.Message("Please Select Process");
                txtProcess.Focus();
                return;
            }

            TrnLabourRateProperty ObjProperty = new TrnLabourRateProperty();
            ObjProperty.PROCESS_ID = Val.ToInt(txtProcess.Tag);
            DtabLabour = ObjTrnLabourRate.GetData(ObjProperty.PROCESS_ID);

            if (DtabLabour.Rows.Count > 0)
            {
                MainGrid.DataSource = DtabLabour;
                MainGrid.RefreshDataSource();
                GrdDet.FocusedColumn = GrdDet.VisibleColumns[1];
                GrdDet.FocusedRowHandle = 0;
                GrdDet.ShowEditor();
                GrdDet.Focus();
            }
        }

        private void txtProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

    }
}
