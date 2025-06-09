using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
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
    public partial class FrmKapanEstimentEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanEsstiment ObjTrn = new BOTRN_KapanEsstiment();
        BOFormPer ObjPer = new BOFormPer();
        DataTable DTab = new DataTable();


        #region Property Settings

        public FrmKapanEstimentEntry()
        {
            InitializeComponent();
        }


        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            string pStrPassword = ObjPer.PASSWORD;

            FrmPassword FrmPassword = new FrmPassword();
            if (FrmPassword.ShowForm(pStrPassword) == System.Windows.Forms.DialogResult.No)
            {
                this.Close();
                return;
            }


            txtKapanName.Focus();

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
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion


        #region Validation

        private bool ValSave()
        {
              return false;
        }

       
        #endregion

        public void Clear()
        {
            DTab.Rows.Clear();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                if (Global.Confirm("Are You Sure To Save This Entry ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                TrnKapanEsstimentProperty Property = new TrnKapanEsstimentProperty();

                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);

                DTab.TableName = "Table";
                string StrXmlForKapanEsstiment = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    StrXmlForKapanEsstiment = sw.ToString();
                }

                Property.XMLFORKAPANESSTIMENT = StrXmlForKapanEsstiment;

                Property = ObjTrn.Save(Property);

                this.Cursor = Cursors.Default;

                Global.Message(Property.ReturnMessageDesc);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    //BtnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                Global.Message(ex.Message);
            }
        }

        
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnBack_Click(null, null);
            //}
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
            Global.ExcelExport(txtKapanName.Text + "List", GrdDet);
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
                       // Property = ObjTrn.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DTab.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTab.AcceptChanges();
                            GetData();
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

        

        
        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            try
            {
                GrdDet.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        #region Button Event

        private void BtnShow_Click(object sender, EventArgs e)
        {
            GetData();
        }

        #endregion

        #region Other Oprretion
        public void GetData()
        {

            DTab = ObjTrn.Fill(Val.ToString(txtKapanName.Text), Val.ToInt64(txtKapanName.Tag));

            GrdDet.Bands["BandKapanName"].Caption = Val.ToString(txtKapanName.Text);
            GrdDet.Bands["BandRoughDate"].Caption = DTab.Rows[0]["ROUGHDATE"].ToString();
            GrdDet.Bands["BandRoughName"].Caption = DTab.Rows[0]["ROUGHTYPE"].ToString();

            int IntCount = 1;
            foreach (DataRow DRow in DTab.Rows)
            {
                DRow["SRNO"] = IntCount;
                IntCount = IntCount + 1;
            }
            DTab.AcceptChanges();

            MainGrid.DataSource = DTab;
            GrdDet.RefreshData();
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

                    FrmSearch.mColumnsToHide = "KAPAN_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
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

       
        #endregion

        #region Grid Event

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                if (e.Column.FieldName == "ROUGHPCS")
                {
                    DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
                    DTab.Rows[e.RowHandle]["ROUGHSIZE"] = Math.Round(Val.Val(DRow["ROUGHCARAT"]) / Val.Val(DRow["ROUGHPCS"]), 2);
                    DTab.AcceptChanges();
                }
                if (e.Column.FieldName == "ROUGHESTRATE")
                {
                    DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
                    DTab.Rows[e.RowHandle]["ROUGHESTAMOUNT"] = Math.Round(Val.Val(DRow["ROUGHCARAT"]) * Val.Val(DRow["ROUGHESTRATE"]), 2);
                    DTab.AcceptChanges();
                }
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }


        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

        }

        #endregion



    }
}
