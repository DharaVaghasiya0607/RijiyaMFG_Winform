using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using BusLib.Transaction;
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
    public partial class FrmKapanProcessSetting : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_KapanProcessSetting ObjMastSetting = new BOMST_KapanProcessSetting();
        BOFormPer ObjPer = new BOFormPer();
        DataTable DtabSetting = new DataTable();


        #region Property Settings

        public FrmKapanProcessSetting()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
           
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
           
            this.Show();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
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
            ObjFormEvent.ObjToDisposeList.Add(ObjMastSetting);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion


        #region Validation

        private bool ValSave()
        {
            if (Val.ToString(txtKapanName.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Kapan Is Required.");
                txtKapanName.Focus();
                return true;
            }
            else if (Val.ToString(CmbSettingType.SelectedItem).Trim().Equals(string.Empty))
            {
                Global.Message("Type Is Required.");
                CmbSettingType.Focus();
                return true;
            }
            else if (DtabSetting.Rows.Count <= 0)
            {
                Global.Message("No Data Found For Save.");
                txtKapanName.Focus();
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
            DtabSetting.Rows.Clear();
            txtKapanName.Text = string.Empty;
            txtKapanName.Focus();
            CmbSettingType.SelectedIndex = 0;
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

                if (DtabSetting.GetChanges() == null || DtabSetting.GetChanges().Rows.Count < 0)
                {
                    return;
                }

                foreach (DataRow Dr in DtabSetting.GetChanges().Rows)
                {
                    KapanProcessSettingProperty Property = new KapanProcessSettingProperty();
                
                    if (Val.ToString(Dr["PARANAME"]).Trim().Equals(string.Empty))
                        continue;

                    Property.SETTING_ID = Val.ToString(Dr["SETTING_ID"]).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["SETTING_ID"]));
                    Property.SETTINGTYPE = Val.ToString(CmbSettingType.SelectedItem);
                    Property.KAPANNAME = Val.ToString(Dr["KAPANNAME"]);
                    Property.PARA_ID = Val.ToInt32(Dr["PARA_ID"]);
                    Property.PARANAME = Val.ToString(Dr["PARANAME"]);

                    Property.DUEHOURS = Val.Val(Dr["DUEHOURS"]);
                    Property.LOSSPER = Val.Val(Dr["LOSSPER"]);

                    Property = ObjMastSetting.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DtabSetting.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            if (Val.ToString(txtKapanName.Text).Trim().Equals(string.Empty))
            {
                Global.Message("Please Select Kapan First.");
                txtKapanName.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            DtabSetting = ObjMastSetting.Fill(Val.ToString(txtKapanName.Text),Val.ToString(CmbSettingType.SelectedItem));

            if (DtabSetting.Rows.Count <= 0)
            {
                Global.Message("No Data Found.");
                MainGrid.DataSource = null;
                this.Cursor = Cursors.Default;
                return;
            }

            MainGrid.DataSource = DtabSetting;
            MainGrid.Refresh();
            this.Cursor = Cursors.Default;
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
            Global.ExcelExport("Kapan Process Setting List", GrdDet);
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
                        DtabSetting.Rows.Add(DtabSetting.NewRow());
                        //DtabSetting.AcceptChanges();

                    }
                    else if(GrdDet.IsLastRow)
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
                    FrmSearch.mDTab = new BOTRN_SinglePacketCreate().FindKapan();
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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {

                Fill();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnUpdateInAllKapan_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure You Want To Update Process Setting For All Kapan...??") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                KapanProcessSettingProperty Property = new KapanProcessSettingProperty();
                Property.KAPANNAME = string.Empty;
                Property = ObjMastSetting.UpdateProcessSettingForAllKapan("ALL",Property);

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        
        private void FrmKapanProcessSetting_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    Fill();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void CmbSettingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (Val.ToString(CmbSettingType.SelectedItem) == "DEPARTMENT")
                {
                    GrdDet.Columns["LOSSPER"].Visible = false;
                    GrdDet.Columns["PARANAME"].Caption = "Department";
                }
                else
                {
                    GrdDet.Columns["LOSSPER"].Visible = true;
                    GrdDet.Columns["PARANAME"].Caption = "Process";
                }
                DtabSetting.Rows.Clear();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
