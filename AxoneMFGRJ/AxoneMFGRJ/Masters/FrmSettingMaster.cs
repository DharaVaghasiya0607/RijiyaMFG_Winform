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

namespace AxoneMFGRJ.Master
{
    public partial class FrmSettingMaster : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Setting ObjMsT = new BOMST_Setting();
        DataTable DtabSetting = new DataTable();

        #region Property Settings

        public FrmSettingMaster()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            BtnAdd_Click(null, null);

            this.Show();
        }



        private bool ValSave()
        {
          
            int IntCol = 0, IntRow = 0;
            foreach (DataRow dr in DtabSetting.Rows)
            {
                //For Update Validation
                if (Val.ToString(dr["SETTINGKEY"]).Trim().Equals(string.Empty) && !Val.ToString(dr["SETTINGVALUE"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Enter SettingKey");
                    IntCol = 0;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                //end as

                if (!Val.ToString(dr["SETTINGKEY"]).Trim().Equals(string.Empty) && Val.ToString(dr["SETTINGVALUE"]).Trim().Equals(string.Empty))
                {
                   
                        Global.Message("Please Enter Setting Value");
                        IntCol = 0;
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

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMsT);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabSetting.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Fill();
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


                foreach (DataRow Dr in DtabSetting.Rows)
                {
                    SettingMasterProperty Property = new SettingMasterProperty();

                    if (Val.ToString(Dr["SETTINGVALUE"]).Trim().Equals(string.Empty) || Val.ToString(Dr["SETTINGKEY"]).Trim().Equals(string.Empty))
                        continue;

                    Property.SETTINGKEY = Val.ToString(Dr["SETTINGKEY"]);
                    Property.SETTINGVALUE = Val.ToString(Dr["SETTINGVALUE"]);

                    Property = ObjMsT.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DtabSetting.AcceptChanges();

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

        public void Fill()
        {
            DtabSetting = ObjMsT.Fill();
            DtabSetting.Rows.Add(DtabSetting.NewRow());
            MainGrid.DataSource = DtabSetting;
            MainGrid.Refresh();

            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.FocusedRowHandle = 0;
            GrdDet.Focus();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Setting List", GrdDet);
        }


        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        if (Val.ToString(Drow["SETTINGKEY"]) == "")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabSetting.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabSetting.AcceptChanges();
                        }
                        else
                        {
                            SettingMasterProperty Property = new SettingMasterProperty();
                            Property.SETTINGKEY = Val.ToString(Drow["SETTINGKEY"]);
                            Property = ObjMsT.Delete(Property);

                            if (Property.ReturnMessageType == "SUCCESS")
                            {
                                Global.Message("ENTRY DELETED SUCCESSFULLY");
                                DtabSetting.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                                DtabSetting.AcceptChanges();
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
        private void BtnShow_Click(object sender, EventArgs e)
        {
            Fill();
        }

        private void RepTxtSettingValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    GrdDet.PostEditor();
                    if (Val.ToString(dr["SETTINGKEY"]) != "" && Val.ToString(dr["SETTINGVALUE"]) != "" && GrdDet.IsLastRow)
                    {
                        DtabSetting.Rows.Add(DtabSetting.NewRow());
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

        private void RepTxtSettingKey_Validating(object sender, CancelEventArgs e)
        {
            DataRow Dr = GrdDet.GetFocusedDataRow();
            if (CheckDuplicate("SETTINGKEY", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "Setting Key"))
                e.Cancel = true;
            return;
        }

    }
}
