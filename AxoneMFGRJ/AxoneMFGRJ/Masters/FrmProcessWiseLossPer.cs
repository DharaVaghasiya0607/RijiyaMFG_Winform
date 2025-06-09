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
    public partial class FrmProcessWiseLossPer : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_ProcessWiseLossPer ObjMast = new BOMST_ProcessWiseLossPer();
        DataTable DtabLoss = new DataTable();

        #region Property Settings
        public FrmProcessWiseLossPer()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

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
        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabLoss.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(txtProcess.Tag + " " + StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        public void Fill()
        {
            DtabLoss = ObjMast.Fill(Val.ToInt32(txtPrc.Tag));
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
                if(ValSave() == false)
                {
                    return;
                }
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                foreach (DataRow Dr in DtabLoss.Rows)
                {
                    ProcessMasterProperty Property = new ProcessMasterProperty();

                    //if (Val.ToString(Dr["PROCESS_ID"]).Trim().Equals(string.Empty))
                    //    continue;
                    Property.PROCESS_ID = Val.ToInt32(txtPrc.Tag);
                    Property.PER_ID = Val.ToInt32(Dr["PER_ID"]); 
                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);
                    Property.PER = Val.ToString(Dr["PER"]);
                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property = ObjMast.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

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

        private bool ValSave()
        {
            for (int i = 0; i < DtabLoss.Rows.Count; i++)
            {
                DataRow dr = DtabLoss.Rows[i];
                if ((Val.Val(dr["FROMCARAT"]) == 0))
                {
                    Global.Message("From Carat Is Required..");
                    return false;
                }
                else if ((Val.Val(dr["TOCARAT"]) == 0))
                {
                    Global.Message("To Carat Is Required..");
                    return false;
                }
                else if(Val.ToString(dr["PER"]) == "")
                {
                    Global.Message("Per Is Required..");
                    return false;
                }
            }
            
            return true;
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
                    GrdDet.PostEditor();
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if ((GrdDet.IsLastRow && Val.ToString(dr["REMARK"]) != ""))
                    {
                        DtabLoss.Rows.Add(DtabLoss.NewRow());
                        //DtabItem.AcceptChanges();

                    }
                    else if ((Val.ToString(dr["FROMCARAT"]) == ""))
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
                Fill();
                //DataRow Dr = GrdDet.GetFocusedDataRow();
                //if (CheckDuplicate("PROCESSNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "PROCESSNAME"))
                //return;
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
                        //GrdDet.SetFocusedRowCellValue("PROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        //GrdDet.SetFocusedRowCellValue("PROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                        txtPrc.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtPrc.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                Fill();
                DataRow Dr = GrdDet.GetFocusedDataRow();
                if (CheckDuplicate("PROCESSNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "PROCESSNAME"))
                    return;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
    }
}
