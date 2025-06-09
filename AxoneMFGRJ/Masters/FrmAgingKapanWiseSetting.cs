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
    public partial class FrmAgingKapanWiseSetting : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_AgingKapanWiseSetting ObjMast = new BOMST_AgingKapanWiseSetting();
        DataTable DtabPara = new DataTable();
        bool IsNextImage = true;

        #region Property Settings

        public FrmAgingKapanWiseSetting()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            DataTable DTabKapan = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANSINGLE);
            cmbKapan.Properties.DataSource = DTabKapan;
            cmbKapan.Properties.DisplayMember = "KAPANNAME";
            cmbKapan.Properties.ValueMember = "KAPANNAME";

            Fill();

            BtnAdd_Click(null, null);
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

        public void Clear()
        {
            txtKapanName.Tag = string.Empty;
            txtKapanName.Text = string.Empty;
            DtabPara.Rows.Clear();
            txtKapanName.Focus();

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";


                foreach (DataRow Dr in DtabPara.Rows)
                {
                    AgeKapanWiseSettingProperty Property = new AgeKapanWiseSettingProperty();

                    Property.KAPANNAME = Val.ToString(txtKapanName.Text);

                    //#P : 24-08-2020 : Coz Client Value nakhine 0 thi Update karse
                    //if (Val.Val(Dr["AGINGMINUTE"]) == 0 && Val.Val(Dr["CARAT"]) == 0 && Val.Val(Dr["AMOUNT"]) == 0)   
                    //    continue;

                    Property.AGINGSETTING_ID = Val.ToString(Dr["AGINGSETTING_ID"]).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["AGINGSETTING_ID"]));
                    Property.PROCESS_ID = Val.ToInt32(Dr["PROCESS_ID"]);
                    Property.AGINGMINUTE = Val.Val(Dr["AGINGMINUTE"]);
                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property.CARAT = Val.Val(Dr["CARAT"]);
                    Property.AMOUNT = Val.Val(Dr["AMOUNT"]);
                    Property = ObjMast.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DtabPara.AcceptChanges();
                BtnSave.Focus();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            DtabPara = ObjMast.Fill(Val.ToString(txtKapanName.Text));
            MainGrid.DataSource = DtabPara;
            MainGrid.Refresh();
            GrdDet.Focus();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport(txtKapanName.Text + "List", GrdDet);
        }

        private void CmbParameterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            Fill();
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
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANSINGLE);

                   // FrmSearch.ColumnsToHide = "KAPAN_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        //txtKapanName.Tag = Val.ToString(FrmSearch.DRow["KAPAN_ID"]);
                        Fill();
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

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (txtKapanName.Text != "" && Val.Val(dr["AGINGMINUTE"]) != 0 && GrdDet.IsLastRow)
                    {
                        DtabPara.Rows.Add(DtabPara.NewRow());
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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            int IntRes = 0;

            string StrKapan = Val.Trim(cmbKapan.Properties.GetCheckedItems());

            string[] Kapan = StrKapan.Split(',');

            for (int i = 0; i < Kapan.Length; i++)
            {
                IntRes = ObjMast.CopyPasteKapanwiseSetting(Val.ToString(txtKapanName.Text), Kapan[i]);
                this.Cursor = Cursors.Default;
            }

            if (IntRes != -1)
            {
                Global.Message("Data Copied Successfully");
                return;
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
                cmbKapan.Focus();
            }
        }

        private void deleteSelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        AgeKapanWiseSettingProperty Property = new AgeKapanWiseSettingProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        if (Val.ToString(Drow["AGINGSETTING_ID"]).Trim().Equals(string.Empty))
                        {
                            Property.AGINGSETTING_ID = Val.ToString(Drow["AGINGSETTING_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(Drow["AGINGSETTING_ID"]));
                            Property = ObjMast.Delete(Property);

                            if (Property.ReturnMessageType == "SUCCESS")
                            {
                                DtabPara.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                                DtabPara.AcceptChanges();
                                Fill();
                            }
                            else
                            {
                                Global.Message("ERROR IN DELETE ENTRY");
                            }
                        }
                        else
                        {
                            Property.AGINGSETTING_ID = Guid.Parse(Val.ToString(Drow["AGINGSETTING_ID"]));
                            Property = ObjMast.Delete(Property);

                            if (Property.ReturnMessageType == "SUCCESS")
                            {
                                Global.Message("ENTRY DELETED SUCCESSFULLY");
                                DtabPara.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                                DtabPara.AcceptChanges();
                                Fill();
                            }
                            else
                            {
                                Global.Message("ERROR IN DELETE ENTRY");
                            }

                        }

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
