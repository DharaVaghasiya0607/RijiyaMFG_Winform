using BusLib.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Utility
{
	public partial class FrmExpGoalPopUp : Form
	{
		AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
		AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Ledger ObjMast = new BOMST_Ledger();

		public FrmExpGoalPopUp()
		{
			InitializeComponent();
		}

		public void ShowForm()
		{
			Val.FormGeneralSetting(this);
			AttachFormDefaultEvent();
			this.Show();

			DTPDate.Value = DateTime.Now;
		}

		public void AttachFormDefaultEvent()
		{
			ObjFormEvent.mForm = this;
			ObjFormEvent.FormKeyDown = true;
			ObjFormEvent.FormKeyPress = true;
			ObjFormEvent.FormResize = true;
			ObjFormEvent.FormClosing = true;
			ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
			//ObjFormEvent.ObjToDisposeList.Add(ObjView);
			ObjFormEvent.ObjToDisposeList.Add(Val);

		}

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string StrDate = ""; int IntGoal = 0;

                if (Global.Confirm("Are You Sure To Save Exp.Goal..?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    StrDate = Val.SqlDate(DTPDate.Text);
                    IntGoal = Val.ToInt32(txtGoal.Text);
                    string StrRes = ObjMast.SetExpGoal(StrDate, IntGoal);
                    if (StrRes != "")
                    {
                        Global.Message(StrRes);
                        txtGoal.Text = string.Empty;
                        DTPDate.Value = DateTime.Now;
                        this.Close();
                    }
                    else
                    {
                        Global.Message("Opps...Something Want Wrong ....!");
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
            
        }
	}
}
