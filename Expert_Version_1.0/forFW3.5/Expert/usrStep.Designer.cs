namespace MARS_Expert
{
    partial class usrStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrStep));
            this.clmnHdr_Scale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstVw_3dObjects = new System.Windows.Forms.ListView();
            this.clmnHdr_Rotation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_RemoveObject = new System.Windows.Forms.Button();
            this.btn_AddObject = new System.Windows.Forms.Button();
            this.txtBx_Description = new System.Windows.Forms.TextBox();
            this.txtBx_Title = new System.Windows.Forms.TextBox();
            this.txtBx_Id = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_ProcedureName = new System.Windows.Forms.Label();
            this.grpBx_AddProcedure = new System.Windows.Forms.GroupBox();
            this.pnl_Id = new System.Windows.Forms.Panel();
            this.pnl_LibelleHolder = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpBx_AddProcedure.SuspendLayout();
            this.SuspendLayout();
            // 
            // clmnHdr_Scale
            // 
            this.clmnHdr_Scale.Text = "Echelle(x,y,z)";
            this.clmnHdr_Scale.Width = 100;
            // 
            // clmnHdr_Position
            // 
            this.clmnHdr_Position.Text = "Position(x,y,z)";
            this.clmnHdr_Position.Width = 100;
            // 
            // clmnHdr_Type
            // 
            this.clmnHdr_Type.Text = "Type";
            this.clmnHdr_Type.Width = 100;
            // 
            // clmnHdr_Name
            // 
            this.clmnHdr_Name.Text = "Libelle";
            this.clmnHdr_Name.Width = 100;
            // 
            // clmnHdr_Number
            // 
            this.clmnHdr_Number.Text = "N°";
            // 
            // clmnHdr_Id
            // 
            this.clmnHdr_Id.Text = "Id";
            // 
            // lstVw_3dObjects
            // 
            this.lstVw_3dObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnHdr_Id,
            this.clmnHdr_Number,
            this.clmnHdr_Name,
            this.clmnHdr_Type,
            this.clmnHdr_Position,
            this.clmnHdr_Rotation,
            this.clmnHdr_Scale});
            this.lstVw_3dObjects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lstVw_3dObjects.FullRowSelect = true;
            this.lstVw_3dObjects.GridLines = true;
            this.lstVw_3dObjects.Location = new System.Drawing.Point(13, 226);
            this.lstVw_3dObjects.MultiSelect = false;
            this.lstVw_3dObjects.Name = "lstVw_3dObjects";
            this.lstVw_3dObjects.Size = new System.Drawing.Size(362, 123);
            this.lstVw_3dObjects.TabIndex = 27;
            this.lstVw_3dObjects.UseCompatibleStateImageBehavior = false;
            this.lstVw_3dObjects.View = System.Windows.Forms.View.Details;
            this.lstVw_3dObjects.DoubleClick += new System.EventHandler(this.lstVw_3dObjects_DoubleClick);
            // 
            // clmnHdr_Rotation
            // 
            this.clmnHdr_Rotation.Text = "Rotation(x,y,z,angle)";
            this.clmnHdr_Rotation.Width = 100;
            // 
            // btn_OK
            // 
            this.btn_OK.FlatAppearance.BorderSize = 0;
            this.btn_OK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_OK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_OK.Image = ((System.Drawing.Image)(resources.GetObject("btn_OK.Image")));
            this.btn_OK.Location = new System.Drawing.Point(220, 355);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 26;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            this.btn_OK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btn_OK.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btn_OK.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_OK.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.Location = new System.Drawing.Point(301, 355);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 25;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            this.btn_Cancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btn_Cancel.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btn_Cancel.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_Cancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btn_RemoveObject
            // 
            this.btn_RemoveObject.FlatAppearance.BorderSize = 0;
            this.btn_RemoveObject.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_RemoveObject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_RemoveObject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_RemoveObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RemoveObject.Image = ((System.Drawing.Image)(resources.GetObject("btn_RemoveObject.Image")));
            this.btn_RemoveObject.Location = new System.Drawing.Point(45, 355);
            this.btn_RemoveObject.Name = "btn_RemoveObject";
            this.btn_RemoveObject.Size = new System.Drawing.Size(35, 23);
            this.btn_RemoveObject.TabIndex = 24;
            this.btn_RemoveObject.UseVisualStyleBackColor = true;
            this.btn_RemoveObject.Click += new System.EventHandler(this.btn_RemoveObject_Click);
            this.btn_RemoveObject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_RemoveModel_MouseDown);
            this.btn_RemoveObject.MouseEnter += new System.EventHandler(this.btn_RemoveModel_MouseEnter);
            this.btn_RemoveObject.MouseLeave += new System.EventHandler(this.btn_RemoveModel_MouseLeave);
            this.btn_RemoveObject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_RemoveModel_MouseUp);
            // 
            // btn_AddObject
            // 
            this.btn_AddObject.FlatAppearance.BorderSize = 0;
            this.btn_AddObject.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_AddObject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_AddObject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_AddObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddObject.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddObject.Image")));
            this.btn_AddObject.Location = new System.Drawing.Point(13, 355);
            this.btn_AddObject.Name = "btn_AddObject";
            this.btn_AddObject.Size = new System.Drawing.Size(35, 23);
            this.btn_AddObject.TabIndex = 23;
            this.btn_AddObject.UseVisualStyleBackColor = true;
            this.btn_AddObject.Click += new System.EventHandler(this.btn_AddObject_Click);
            this.btn_AddObject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_AddModel_MouseDown);
            this.btn_AddObject.MouseEnter += new System.EventHandler(this.btn_AddModel_MouseEnter);
            this.btn_AddObject.MouseLeave += new System.EventHandler(this.btn_AddModel_MouseLeave);
            this.btn_AddObject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_AddModel_MouseUp);
            // 
            // txtBx_Description
            // 
            this.txtBx_Description.BackColor = System.Drawing.Color.White;
            this.txtBx_Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBx_Description.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtBx_Description.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.txtBx_Description.Location = new System.Drawing.Point(16, 137);
            this.txtBx_Description.Multiline = true;
            this.txtBx_Description.Name = "txtBx_Description";
            this.txtBx_Description.Size = new System.Drawing.Size(349, 62);
            this.txtBx_Description.TabIndex = 22;
            // 
            // txtBx_Title
            // 
            this.txtBx_Title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBx_Title.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtBx_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.txtBx_Title.Location = new System.Drawing.Point(219, 78);
            this.txtBx_Title.Name = "txtBx_Title";
            this.txtBx_Title.Size = new System.Drawing.Size(156, 16);
            this.txtBx_Title.TabIndex = 21;
            // 
            // txtBx_Id
            // 
            this.txtBx_Id.BackColor = System.Drawing.Color.White;
            this.txtBx_Id.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBx_Id.Enabled = false;
            this.txtBx_Id.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic);
            this.txtBx_Id.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.txtBx_Id.Location = new System.Drawing.Point(40, 78);
            this.txtBx_Id.Name = "txtBx_Id";
            this.txtBx_Id.ReadOnly = true;
            this.txtBx_Id.Size = new System.Drawing.Size(100, 16);
            this.txtBx_Id.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Objets 3d:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Description: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Libelle:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Id: ";
            // 
            // lbl_ProcedureName
            // 
            this.lbl_ProcedureName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_ProcedureName.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ProcedureName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbl_ProcedureName.Location = new System.Drawing.Point(10, 16);
            this.lbl_ProcedureName.Name = "lbl_ProcedureName";
            this.lbl_ProcedureName.Size = new System.Drawing.Size(365, 52);
            this.lbl_ProcedureName.TabIndex = 15;
            this.lbl_ProcedureName.Text = "Parent Procedure";
            this.lbl_ProcedureName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBx_AddProcedure
            // 
            this.grpBx_AddProcedure.Controls.Add(this.lbl_ProcedureName);
            this.grpBx_AddProcedure.Controls.Add(this.lstVw_3dObjects);
            this.grpBx_AddProcedure.Controls.Add(this.label1);
            this.grpBx_AddProcedure.Controls.Add(this.btn_OK);
            this.grpBx_AddProcedure.Controls.Add(this.label2);
            this.grpBx_AddProcedure.Controls.Add(this.btn_Cancel);
            this.grpBx_AddProcedure.Controls.Add(this.label3);
            this.grpBx_AddProcedure.Controls.Add(this.label4);
            this.grpBx_AddProcedure.Controls.Add(this.btn_AddObject);
            this.grpBx_AddProcedure.Controls.Add(this.txtBx_Id);
            this.grpBx_AddProcedure.Controls.Add(this.txtBx_Description);
            this.grpBx_AddProcedure.Controls.Add(this.txtBx_Title);
            this.grpBx_AddProcedure.Controls.Add(this.pnl_Id);
            this.grpBx_AddProcedure.Controls.Add(this.pnl_LibelleHolder);
            this.grpBx_AddProcedure.Controls.Add(this.panel1);
            this.grpBx_AddProcedure.Controls.Add(this.btn_RemoveObject);
            this.grpBx_AddProcedure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBx_AddProcedure.Location = new System.Drawing.Point(0, 0);
            this.grpBx_AddProcedure.Name = "grpBx_AddProcedure";
            this.grpBx_AddProcedure.Size = new System.Drawing.Size(378, 388);
            this.grpBx_AddProcedure.TabIndex = 32;
            this.grpBx_AddProcedure.TabStop = false;
            this.grpBx_AddProcedure.Text = "L\'étape de maintenance";
            // 
            // pnl_Id
            // 
            this.pnl_Id.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Id.BackgroundImage")));
            this.pnl_Id.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Id.Enabled = false;
            this.pnl_Id.Location = new System.Drawing.Point(35, 72);
            this.pnl_Id.Name = "pnl_Id";
            this.pnl_Id.Size = new System.Drawing.Size(111, 29);
            this.pnl_Id.TabIndex = 64;
            // 
            // pnl_LibelleHolder
            // 
            this.pnl_LibelleHolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_LibelleHolder.BackgroundImage")));
            this.pnl_LibelleHolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_LibelleHolder.Location = new System.Drawing.Point(212, 71);
            this.pnl_LibelleHolder.Name = "pnl_LibelleHolder";
            this.pnl_LibelleHolder.Size = new System.Drawing.Size(167, 31);
            this.pnl_LibelleHolder.TabIndex = 65;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(4, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 93);
            this.panel1.TabIndex = 66;
            // 
            // usrStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBx_AddProcedure);
            this.Name = "usrStep";
            this.Size = new System.Drawing.Size(380, 400);
            this.Load += new System.EventHandler(this.frm_AddStep_Load);
            this.grpBx_AddProcedure.ResumeLayout(false);
            this.grpBx_AddProcedure.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader clmnHdr_Scale;
        private System.Windows.Forms.ColumnHeader clmnHdr_Position;
        private System.Windows.Forms.ColumnHeader clmnHdr_Type;
        private System.Windows.Forms.ColumnHeader clmnHdr_Name;
        private System.Windows.Forms.ColumnHeader clmnHdr_Number;
        private System.Windows.Forms.ColumnHeader clmnHdr_Id;
        private System.Windows.Forms.ListView lstVw_3dObjects;
        private System.Windows.Forms.ColumnHeader clmnHdr_Rotation;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_RemoveObject;
        private System.Windows.Forms.Button btn_AddObject;
        private System.Windows.Forms.TextBox txtBx_Description;
        private System.Windows.Forms.TextBox txtBx_Title;
        private System.Windows.Forms.TextBox txtBx_Id;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_ProcedureName;
        private System.Windows.Forms.GroupBox grpBx_AddProcedure;
        private System.Windows.Forms.Panel pnl_Id;
        private System.Windows.Forms.Panel pnl_LibelleHolder;
        private System.Windows.Forms.Panel panel1;
    }
}
