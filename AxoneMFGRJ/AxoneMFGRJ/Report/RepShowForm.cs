using System;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace AxoneMFGRJ.StimulsoftViewer
{
    public partial class RepShowForm : DevExpress.XtraEditors.XtraForm
    {      
        public int No_of_Copy { get; set; }
        
        public RepShowForm()
        {
            InitializeComponent();
        }

        private void RepShowForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.Control && e.KeyCode == Keys.P)
            {
                this.repViewer.InvokeClickPrintButton();
            }
        }

        private void repViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void repViewer_ClickPrintButton(object sender, EventArgs e)
        {
            PrinterSettings printersettings = new PrinterSettings();
            printersettings.Copies = 1;
            printersettings.Collate = false;
            this.repViewer.Report.Print(true, printersettings);
        }
        
        private void repViewer_Close(object sender, EventArgs e)
        {
            this.Close();
        }
     
    }
}