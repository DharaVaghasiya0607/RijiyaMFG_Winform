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
    public partial class FrmHeliumPrintLimitSetting : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOHEL_PrintMaxLimitMaster ObjMast = new BOHEL_PrintMaxLimitMaster();
        DataTable DtabPrintLimit = new DataTable();

        #region Property Settings

        public FrmHeliumPrintLimitSetting()
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
            CmbShapeType.SelectedIndex = 0;
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
            DtabPrintLimit.Rows.Clear();
            DtabPrintLimit.Rows.Add(DtabPrintLimit.NewRow());

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

                foreach (DataRow Dr in DtabPrintLimit.GetChanges().Rows)
                {
                    HelPrintMaxLimitProperty Property = new HelPrintMaxLimitProperty();
                    Property.SHAPETYPE = Val.ToString(CmbShapeType.SelectedItem);

                    if (Val.ToInt32(Dr["MAXPRINTLIMIT"]) == 0 || Val.Val(Dr["FROMCARAT"]) == 0 || Val.Val(Dr["TOCARAT"]) == 0)
                        continue;

                    Property.MAXLIMIT_ID = Val.ToString(Dr["MAXLIMIT_ID"]).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["MAXLIMIT_ID"]));
                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);
                    Property.MAXPRINTLIMIT = Val.ToInt32(Dr["MAXPRINTLIMIT"]);
                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property = ObjMast.Save(Property);
                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DtabPrintLimit.AcceptChanges();

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
            DtabPrintLimit = ObjMast.Fill(Val.ToString(CmbShapeType.SelectedItem));
            DtabPrintLimit.Rows.Add(DtabPrintLimit.NewRow());
            MainGrid.DataSource = DtabPrintLimit;
            MainGrid.Refresh();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport(CmbShapeType.SelectedItem.ToString() + " Print Limits List", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (Val.ToInt32(dr["MAXPRINTLIMIT"]) != 0 && Val.Val(dr["FROMCARAT"]) != 0 && Val.Val(dr["TOCARAT"]) != 0 && GrdDet.IsLastRow)
                    {
                        DtabPrintLimit.Rows.Add(DtabPrintLimit.NewRow());
                        //DtabPrintLimit.AcceptChanges();

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

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        HelPrintMaxLimitProperty Property = new HelPrintMaxLimitProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.MAXLIMIT_ID = Guid.Parse(Val.ToString(Drow["MAXLIMIT_ID"]));
                        Property = ObjMast.Delete(Property);
                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabPrintLimit.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabPrintLimit.AcceptChanges();
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

        private void CmbParameterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            Fill();
        }
    }
}
