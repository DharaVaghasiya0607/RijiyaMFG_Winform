using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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

namespace AxoneMFGRJ.Utility
{
    public partial class FrmPacketDollarUnloock : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_LabourProcess ObjLProcess = new BOTRN_LabourProcess();
        BOMST_KapanProcessSetting ObjMastSetting = new BOMST_KapanProcessSetting();
        BOTRN_PenaltyIncentive ObjPenalty = new BOTRN_PenaltyIncentive();
        DataTable DtabDollar = new DataTable();

        #region Property 

        public FrmPacketDollarUnloock()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
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
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        #region : Grid Event

        private void GrdDet_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    DataRow DR = GrdDet.GetDataRow(e.RowHandle);
                    txtKapanName.Text = Val.ToString(DR["KAPANNAME"]);
                    txtPacketNo.Text = Val.ToString(DR["PACKETNO"]);
                    txtPacketNo.Tag = Val.ToString(DR["PACKET_ID"]);
                    txtTag.Text = Val.ToString(DR["TAG"]);
                    txtExpDollar.Text = Val.ToString(DR["AMOUNT"]);
                    txtExpDollarRevice.Text = Val.ToString(DR["EXPDOLLARREVICE"]);
                    txtRemark.Text = Val.ToString(DR["REMARK"]);
                   
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        #endregion

        
        #region : Other 

        private void txtExpDollar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtExpDollar.Text.Length == 0)
            {
                if (e.KeyChar == '.')
                {
                    e.Handled = true;
                }
            }
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && txtExpDollar.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtISDollarRevice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        public void Fill()
        {
            DtabDollar = ObjLProcess.DallarFill(Val.ToString(txtKapanName.Text), Val.ToInt(txtPacketNo.Text), Val.ToString(txtTag.Text), Val.Val(txtExpDollarRevice.Text), Val.ToBoolean(txtExpDollar.Text));
            MainGrid.DataSource = DtabDollar;
            MainGrid.Refresh();
            GrdDet.BestFitColumns();
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
                    FrmSearch.mDTab = ObjPenalty.FindKapan();
                    FrmSearch.mColumnsToHide = "KAPAN_ID,MANAGER_ID,MANAGERNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);

                    }
                    else
                    {
                        txtKapanName.Text = Val.ToString(DBNull.Value);
                        txtKapanName.Tag = Val.ToString(DBNull.Value);
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


        private void txtTag_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    Fill();

            //    if (DtabDollar.Rows.Count > 0)
            //    {
            //        txtExpDollar.Text = Val.ToString(DtabDollar.Rows[0]["AMOUNT"]);
            //        txtPacketNo.Tag = Val.ToInt64(DtabDollar.Rows[0]["PACKET_ID"]);
            //    }

            //}
            //catch (Exception Ex)
            //{
            //    Global.Message(Ex.Message.ToString());
            //}

        }

        private void txtPacketNo_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        txtTag.Focus();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Global.Message("IN CATCH");
            //    Global.Message(Ex.Message.ToString());
            //}
        
}

        #endregion

        #region : Button Event

        private void txtTag_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Fill();

                if (DtabDollar.Rows.Count > 0)
                {
                    txtExpDollar.Text = Val.ToString(DtabDollar.Rows[0]["AMOUNT"]);
                    txtPacketNo.Tag = Val.ToInt64(DtabDollar.Rows[0]["PACKET_ID"]);
                }

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }


        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure You Want To Update Amount For This Packet...??") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                TrnSinglePacketCreationProperty Property = new TrnSinglePacketCreationProperty();
                Property.PACKET_ID = Val.ToInt64(txtPacketNo.Tag);
                Property.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);
                Property.KAPANNAME = txtKapanName.Text;
                Property.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                Property.TAG = txtTag.Text;
                Property.EXPDOLLARREVICE = Val.Val(txtExpDollarRevice.Text);
                Property.EXPDOLLAR = Val.Val(txtExpDollarRevice.Text);
                Property = ObjLProcess.DollarUpdate(Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    Fill();
                    //BtnClear_Click(null, null);
                    txtKapanName.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtExpDollar.Text = string.Empty;
                txtExpDollarRevice.Text = string.Empty;
                txtRemark.Text = string.Empty;
                DtabDollar.Rows.Clear();

            }
            catch(Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        #endregion

        
        
    }
}
