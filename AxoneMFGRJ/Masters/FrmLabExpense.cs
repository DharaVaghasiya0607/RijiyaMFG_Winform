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
    public partial class FrmLabExpense : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_ParameterDiscount ObjTrn = new BOTRN_ParameterDiscount();

        BOMST_LabExpense ObjMast = new BOMST_LabExpense();
        DataTable DtabExpense = new DataTable();

        #region Property Settings

        public FrmLabExpense()
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

            FillRapaportDate();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
           // ObjFormEvent.FormKeyDown = true;
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
            DtabExpense.Rows.Clear();
            DtabExpense.Rows.Add(DtabExpense.NewRow());

            //CmbRapaportRapDate.SelectedItem = null;

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
            CmbRapaportRapDate.SelectedItem = null;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "Please Insert Record then Save";
                string ReturnMessageType = "";
                if (CmbRapaportRapDate.SelectedItem == null)
                {
                    Global.Message("Pelese Select Rap Date");
                    return;
                }
                //if (count <= 1)
                //{
                //    return;
                //}
                foreach (DataRow Dr in DtabExpense.GetChanges().Rows)
                {
                    LabExpenseMasterProperty Property = new LabExpenseMasterProperty();

                    if (Val.ToString(Dr["SIZENAME"]).Trim().Equals(string.Empty) || Val.Val(Dr["FROMCARAT"]) == 0 || Val.Val(Dr["TOCARAT"]) == 0)
                        continue;

                    Property.LABEXPENSE_ID = Val.ToInt32(Dr["LABEXPENSE_ID"]);
                    Property.RAPDATE = Val.ToString(CmbRapaportRapDate.SelectedItem);
                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);
                    Property.SIZENAME = Val.ToString(Dr["SIZENAME"]);
                    Property.RATE = Val.Val(Dr["RATE"]);

                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);

                    Property = ObjMast.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DtabExpense.AcceptChanges();
                
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
        public void FillRapaportDate()
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable DTabParameter = ObjTrn.GetOriginalRapData("RAPDATE", "", "", 0, 0);
            DTabParameter.DefaultView.Sort = "RAPDATE DESC";
            DTabParameter = DTabParameter.DefaultView.ToTable();

            CmbRapaportRapDate.Items.Clear();

            foreach (DataRow DRow in DTabParameter.Rows)
            {
                CmbRapaportRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            DTabParameter.Dispose();
            DTabParameter = null;
            this.Cursor = Cursors.Default;
        }


        public void Fill()
        {

            DtabExpense = ObjMast.Fill(Val.ToString(CmbRapaportRapDate.SelectedItem));
            DtabExpense.Rows.Add(DtabExpense.NewRow());
            MainGrid.DataSource = DtabExpense;
            MainGrid.Refresh();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void BtnExport_Click(object sender, EventArgs e)
        {
          Global.ExcelExport("LabExpenseList", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (Val.ToString(dr["SIZENAME"]) != "" && Val.Val(dr["FROMCARAT"]) != 0 && Val.Val(dr["TOCARAT"]) != 0  && GrdDet.IsLastRow)
                    {
                        DtabExpense.Rows.Add(DtabExpense.NewRow());

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

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        LabExpenseMasterProperty Property = new LabExpenseMasterProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.LABEXPENSE_ID = Val.ToInt32(Drow["LABEXPENSE_ID"]);
                        Property = ObjMast.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabExpense.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabExpense.AcceptChanges();
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
        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName.ToUpper())
            {
                case "FROMCARAT":
                case "TOCARAT":
                    string FromCarat = Val.Format(GrdDet.GetFocusedRowCellValue("FROMCARAT"),"###0.000");
                    string ToCarat = Val.Format(GrdDet.GetFocusedRowCellValue("TOCARAT"), "###0.000");
                    GrdDet.SetFocusedRowCellValue("SIZENAME",FromCarat + " To " + ToCarat);
                    break;
                default:
                    break;
            }
        }

        private void BtnRapaportGetData_Click(object sender, EventArgs e)
        {
            Fill();
        }

        private void CmbRapaportRapDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            BtnRapaportGetData.Focus();
        }

    }
}
