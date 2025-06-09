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

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmLabourPcs : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_LabourPcs ObjTrn = new BOTRN_LabourPcs();
        DataTable DtabPara = new DataTable();


        #region Property Settings

        public FrmLabourPcs()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DtabPara.Columns.Add("ID", typeof(System.Guid));
            DtabPara.Columns.Add("YY", typeof(System.Int32));
            DtabPara.Columns.Add("MM", typeof(System.Int32));
            DtabPara.Columns.Add("FROMPCS", typeof(System.Int32));
            DtabPara.Columns.Add("TOPCS", typeof(System.Int32));
            DtabPara.Columns.Add("PER", typeof(System.Double));

            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();

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
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            DtabPara.Rows.Clear();
            DtabPara.Rows.Add(DtabPara.NewRow());            
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.Val(txtYear.Text) == 0)
                {
                    Global.Message("Year Is Required");
                    txtYear.Focus();
                    return;
                }
                if (Val.Val(txtMonth.Text) == 0)
                {
                    Global.Message("Month Is Required");
                    txtMonth.Focus();
                    return;
                }

                if (Val.Val(txtMonth.Text) > 12 || Val.Val(txtMonth.Text) <= 0)
                {
                    Global.Message("Your Month IS Invalid, Must Be Between 0 To 12");
                    txtMonth.Focus();
                    return;
                }

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";


                foreach (DataRow Dr in DtabPara.GetChanges().Rows)
                {
                    TrnLabourPcsProperty Property = new TrnLabourPcsProperty();

                    if (Val.Val(Dr["FROMPCS"]) == 0 || Val.Val(Dr["TOPCS"]) == 0)
                        continue;

                    if (Val.ToString(Dr["ID"]) == "")
                    {
                        Dr["ID"] = Guid.NewGuid();
                    }
                    Property.ID = Guid.Parse(Val.ToString(Dr["ID"]));
                    Property.YY = Val.ToInt(txtYear.Text);
                    Property.MM = Val.ToInt(txtMonth.Text);
                    Property.FROMPCS = Val.ToInt(Dr["FROMPCS"]);
                    Property.TOPCS = Val.ToInt(Dr["TOPCS"]);
                    Property.PER = Val.Val(Dr["PER"]);
                    
                    Property = ObjTrn.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DtabPara.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    
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
            if (Val.Val(txtYear.Text) == 0)
            {
                Global.Message("Year Is Required");
                txtYear.Focus();
                return;
            }
            if (Val.Val(txtMonth.Text) == 0)
            {
                Global.Message("Month Is Required");
                txtMonth.Focus();
                return;
            }

            if (Val.Val(txtMonth.Text) > 12 || Val.Val(txtMonth.Text) <= 0)
            {
                Global.Message("Your Month IS Invalid, Must Be Between 0 To 12");
                txtMonth.Focus();
                return;
            }
            DtabPara = ObjTrn.Fill(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text));
            DataRow DrNew = DtabPara.NewRow();
            DtabPara.Rows.Add(DrNew);
            MainGrid.DataSource = DtabPara;
            MainGrid.Refresh();

            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.FocusedRowHandle = DrNew.Table.Rows.IndexOf(DrNew);
            GrdDet.Focus();
            GrdDet.ShowEditor();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PcsLabourList", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDet.GetFocusedDataRow();
                    GrdDet.PostEditor();
                    if (Val.Val(dr["FROMPCS"]) != 0 && Val.Val(dr["TOPCS"]) != 0 && GrdDet.IsLastRow) //&& Val.Val(dr["PER"]) != 0 
                    {
                        DtabPara.Rows.Add(DtabPara.NewRow());                        
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
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        if (Val.ToString(Drow["ID"]) == "")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabPara.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabPara.AcceptChanges();
                        }
                        else
                        {
                            TrnLabourPcsProperty Property = new TrnLabourPcsProperty();
                            Property.ID = Guid.Parse(Val.ToString(Drow["ID"]));
                            Property = ObjTrn.Delete(Property);

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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            Fill();
        }

    }
}
