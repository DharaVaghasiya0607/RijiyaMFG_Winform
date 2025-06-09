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
    public partial class FrmAgingProcessSetting : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_AgingProcessSetting ObjMast = new BOMST_AgingProcessSetting();
        DataTable DtabPara = new DataTable();

        #region Property Settings

        public FrmAgingProcessSetting()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            DtabPara.Columns.Add("AGINGPROCESS_ID", typeof(System.Guid));
            DtabPara.Columns.Add("SIZENAME", typeof(System.String));
            DtabPara.Columns.Add("FROMCARAT", typeof(System.Double));
            DtabPara.Columns.Add("TOCARAT", typeof(System.Double));
            DtabPara.Columns.Add("AGINGMINUTE", typeof(System.Double));
            DtabPara.Columns.Add("ISACTIVE", typeof(System.Boolean));
            DtabPara.Columns.Add("REMARK", typeof(System.String));

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
            txtProcessName.Tag = string.Empty;
            txtProcessName.Text = string.Empty;
            DtabPara.Rows.Clear();
            txtProcessName.Focus();

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
                    AgeProcessSettingProperty Property = new AgeProcessSettingProperty();

                    Property.PROCESS_ID = Val.ToInt32(txtProcessName.Tag);

                    if (Val.Val(Dr["FROMCARAT"]) == 0 || Val.Val(Dr["TOCARAT"]) == 0 || Val.Val(Dr["AGINGMINUTE"]) == 0)
                        continue;

                    Property.AGINGPROCESS_ID = Val.ToString(Dr["AGINGPROCESS_ID"]).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["AGINGPROCESS_ID"]));
                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);
                    Property.AGINGMINUTE = Val.Val(Dr["AGINGMINUTE"]);
                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
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
            DtabPara = ObjMast.Fill(Val.ToInt32(txtProcessName.Tag));
            DataRow DR = DtabPara.NewRow();
            DtabPara.Rows.Add(DR);
            MainGrid.DataSource = DtabPara;
            MainGrid.Refresh();
            GrdDet.FocusedRowHandle = DR.Table.Rows.IndexOf(DR);
            GrdDet.FocusedColumn = GrdDet.Columns[0];
            GrdDet.Focus();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport(txtProcessName.Text + "List", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (txtProcessName.Text != "" && Val.Val(dr["FROMCARAT"]) != 0 && Val.Val(dr["TOCARAT"]) != 0 && GrdDet.IsLastRow)
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

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        AgeProcessSettingProperty Property = new AgeProcessSettingProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        if (Val.ToString(Drow["AGINGPROCESS_ID"]).Trim().Equals(string.Empty))
                        {
                            Property.AGINGPROCESS_ID = Val.ToString(Drow["AGINGPROCESS_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(Drow["AGINGPROCESS_ID"]));
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
                            Property.AGINGPROCESS_ID = Guid.Parse(Val.ToString(Drow["AGINGPROCESS_ID"]));
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

        private void CmbParameterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            Fill();
        }

        private void txtProcessName_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtProcessName.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcessName.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
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

        private void reptxtFromCarat_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                GrdDet.PostEditor();
                DataRow Dr = GrdDet.GetFocusedDataRow();
                if (GrdDet.FocusedRowHandle < 0 || Val.Val(GrdDet.EditingValue) == 0)
                    return;

                if (Val.Val(Dr["TOCARAT"]) > 0)
                    if (Val.Val(GrdDet.EditingValue) > Val.Val(Dr["TOCARAT"]))
                    {
                        Global.Message("From Amount Must Be Less Than To Amount.!");
                        e.Cancel = true;
                        return;
                    }


                var dValue = from row in DtabPara.AsEnumerable()
                             where Val.Val(row["FROMCARAT"]) <= Val.Val(GrdDet.EditingValue) && Val.Val(row["TOCARAT"]) >= Val.Val(GrdDet.EditingValue) && row.Table.Rows.IndexOf(row) != GrdDet.FocusedRowHandle
                             select row;

                if (dValue.Any())
                {
                    Global.Message("This Value Already Exist Between Some From Carat and To Carat Please Check.!");
                    e.Cancel = true;
                    return;
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void reptxtToCarat_Validating(object sender, CancelEventArgs e)
        {
            DataRow Dr = GrdDet.GetFocusedDataRow();

            if (Val.ToDecimal(Dr["FROMCARAT"]) > Val.ToDecimal(GrdDet.EditingValue))
            {
                Global.Message("To Amount must be Greter Than From Amount");
                e.Cancel = true;
                return;
            }

            var dValue = from row in DtabPara.AsEnumerable()
                         where Val.Val(row["FROMCARAT"]) <= Val.Val(GrdDet.EditingValue) && Val.Val(row["TOCARAT"]) >= Val.Val(GrdDet.EditingValue) && row.Table.Rows.IndexOf(row) != GrdDet.FocusedRowHandle
                         select row;

            if (dValue.Any())
            {
                Global.Message("This Value Already Exist Between Some From Carat and To Carat Please Check.!");
                e.Cancel = true;
                return;
            }
        }

    }
}
