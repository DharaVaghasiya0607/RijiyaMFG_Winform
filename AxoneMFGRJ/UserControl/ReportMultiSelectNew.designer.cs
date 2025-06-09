namespace AxoneMFGRJ.UserControl
{
    partial class ReportMultiSelectNew
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ListTo = new AxonContLib.cListView(this.components);
            this.ListFrom = new AxonContLib.cListView(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(220)))));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ListTo);
            this.groupBox1.Controls.Add(this.ListFrom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 324);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group Panel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(173, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Group By Columns";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(8, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Note : Double Click On Panel To Add & Remove";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "UnGroup Columns";
            // 
            // ListTo
            // 
            this.ListTo.AllowTabKeyOnEnter = false;
            this.ListTo.BackColor = System.Drawing.Color.White;
            this.ListTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListTo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListTo.ForeColor = System.Drawing.Color.Black;
            this.ListTo.GridLines = true;
            this.ListTo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListTo.Location = new System.Drawing.Point(176, 42);
            this.ListTo.Name = "ListTo";
            this.ListTo.Size = new System.Drawing.Size(165, 257);
            this.ListTo.TabIndex = 0;
            this.ListTo.ToolTips = "";
            this.ListTo.UseCompatibleStateImageBehavior = false;
            this.ListTo.View = System.Windows.Forms.View.Details;
            this.ListTo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListTo_MouseDoubleClick);
            // 
            // ListFrom
            // 
            this.ListFrom.AllowTabKeyOnEnter = false;
            this.ListFrom.BackColor = System.Drawing.Color.White;
            this.ListFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListFrom.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListFrom.ForeColor = System.Drawing.Color.Black;
            this.ListFrom.GridLines = true;
            this.ListFrom.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListFrom.Location = new System.Drawing.Point(7, 42);
            this.ListFrom.Name = "ListFrom";
            this.ListFrom.Size = new System.Drawing.Size(165, 257);
            this.ListFrom.TabIndex = 1;
            this.ListFrom.ToolTips = "";
            this.ListFrom.UseCompatibleStateImageBehavior = false;
            this.ListFrom.View = System.Windows.Forms.View.Details;
            this.ListFrom.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListFrom_MouseDoubleClick);
            // 
            // ReportMultiSelectNew
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportMultiSelectNew";
            this.Size = new System.Drawing.Size(349, 324);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cListView ListFrom;
        private AxonContLib.cListView ListTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
