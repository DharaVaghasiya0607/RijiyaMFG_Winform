
namespace AxoneMFGRJ.Utility
{
    partial class FrmHeliumFileImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainGridView = new DevExpress.XtraGrid.GridControl();
            this.GridDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // MainGridView
            // 
            this.MainGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridView.Location = new System.Drawing.Point(0, 0);
            this.MainGridView.MainView = this.GridDetail;
            this.MainGridView.Name = "MainGridView";
            this.MainGridView.Size = new System.Drawing.Size(800, 427);
            this.MainGridView.TabIndex = 4;
            this.MainGridView.Tag = "";
            this.MainGridView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridDetail});
            // 
            // GridDetail
            // 
            this.GridDetail.GridControl = this.MainGridView;
            this.GridDetail.Name = "GridDetail";
            this.GridDetail.OptionsBehavior.Editable = false;
            this.GridDetail.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 427);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(800, 23);
            this.button1.TabIndex = 3;
            this.button1.Tag = "";
            this.button1.Text = "UPDATE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmHeliumFileImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainGridView);
            this.Controls.Add(this.button1);
            this.Name = "FrmHeliumFileImport";
            this.Tag = "HeliumFileImport";
            this.Text = "HELIUM FILE IMPORT";
            this.Load += new System.EventHandler(this.FrmHeliumFileImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl MainGridView;
        private DevExpress.XtraGrid.Views.Grid.GridView GridDetail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
    }
}