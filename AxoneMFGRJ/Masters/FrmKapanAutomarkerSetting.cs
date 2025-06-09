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
    public partial class FrmKapanAutomarkerSetting : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        DataTable DtabKapan = new DataTable();
        BOMST_KapanProcessSetting ObjMast = new BOMST_KapanProcessSetting();

        #region Property Settings

        public FrmKapanAutomarkerSetting()
        {
  
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            //BtnAdd_Click(null, null);
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


        private void Fill()
        {
            DtabKapan = ObjMast.KapanFill();
            MainGrid.DataSource = DtabKapan;
            DtabKapan.Rows.Add(DtabKapan.NewRow());
            MainGrid.Refresh();
        }
        public void Clear()
        {
            DtabKapan.Rows.Clear();
            DtabKapan.Rows.Add(DtabKapan.NewRow());

        }

        private void repTxtMarcerName_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERNAME,LEDGERCODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEEKAPAN);
                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("MARKERCODE", Val.ToString(FrmSearch.mDRow["LEDGERCODE"]));
                        GrdDet.SetFocusedRowCellValue("MARKER_ID", Val.ToString(FrmSearch.mDRow["LEDGER_ID"]));

                       
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

        private void repKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANMIX);

                    FrmSearch.mColumnsToHide = "KAPAN_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("KAPAN_ID", Val.ToString(FrmSearch.mDRow["KAPAN_ID"]));
                        GrdDet.SetFocusedRowCellValue("KAPANNAME", Val.ToString(FrmSearch.mDRow["KAPANNAME"]));
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";


                foreach (DataRow Dr in DtabKapan.GetChanges().Rows)
                {
                    KapanAutomarkerSettingProprerty Property = new KapanAutomarkerSettingProprerty();

                

                    if (Val.ToString(Dr["KAPANNAME"]).Trim().Equals(string.Empty))
                        continue;
                    Property.ID = Val.ToInt64(Dr["ID"]);
                    Property.KAPAN_ID = Val.ToInt64(Dr["KAPAN_ID"]);
                    
                    Property.KAPANNAME = Val.ToString(Dr["KAPANNAME"]);
                    Property.FROMSIZE = Val.ToInt(Dr["FROMSIZE"]);
                    Property.TOSIZE = Val.ToInt(Dr["TOSIZE"]);
                    Property.DEPARTMENT_ID = Val.ToInt64(Dr["DEPARTMENT_ID"]);
                    Property.MARKER_ID = Val.ToInt64(Dr["MARKER_ID"]);
                     Property = ObjMast.MarkerSave (Property);

                     ReturnMessageDesc = Property.ReturnMessageDesc;
                     ReturnMessageType = Property.ReturnMessageType;
                    
                    Property = null;
                }
                DtabKapan.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                   // BtnAdd_Click(null, null);

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

        private void repDepartmentName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        KapanAutomarkerSettingProprerty Property = new KapanAutomarkerSettingProprerty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.ID = Val.ToInt32(Drow["ID"]);
                        Property = ObjMast.kAPANDelete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabKapan.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabKapan.AcceptChanges();
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

        private void repTxtMarcerName_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (Val.ToString(dr["MARKERCODE"]) != "" && GrdDet.IsLastRow)
                    {
                        DtabKapan.Rows.Add(DtabKapan.NewRow());
                        //DtabItem.AcceptChanges();

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

        private void MainGrid_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Kapan List", GrdDet);
        }

        private void ReptxtToSize_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["KAPANNAME"]).Equals(string.Empty) && !Val.ToString(dr["MARKERCODE"]).Equals(string.Empty) && !Val.ToString(dr["FROMSIZE"]).Equals(string.Empty) && GrdDet.IsLastRow)
                    {
                        DtabKapan.Rows.Add(DtabKapan.NewRow());
                        DtabKapan.AcceptChanges();
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
    }
}
