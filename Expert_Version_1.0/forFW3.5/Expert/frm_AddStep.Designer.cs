namespace MARS_Expert
{
    partial class frm_AddStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AddStep));
            this.lbl_ProcedureName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBx_Id = new System.Windows.Forms.TextBox();
            this.txtBx_Title = new System.Windows.Forms.TextBox();
            this.txtBx_Description = new System.Windows.Forms.TextBox();
            this.btn_AddObject = new System.Windows.Forms.Button();
            this.btn_RemoveObject = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lstVw_3dObjects = new System.Windows.Forms.ListView();
            this.clmnHdr_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Rotation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Scale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lbl_ProcedureName
            // 
            this.lbl_ProcedureName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_ProcedureName.Font = new System.Drawing.Font("Segoe UI Symbol", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ProcedureName.Location = new System.Drawing.Point(12, 9);
            this.lbl_ProcedureName.Name = "lbl_ProcedureName";
            this.lbl_ProcedureName.Size = new System.Drawing.Size(466, 52);
            this.lbl_ProcedureName.TabIndex = 0;
            this.lbl_ProcedureName.Text = "Parent Procedure";
            this.lbl_ProcedureName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Id: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Libelle:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Description: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Objets 3d:";
            // 
            // txtBx_Id
            // 
            this.txtBx_Id.Enabled = false;
            this.txtBx_Id.Location = new System.Drawing.Point(42, 71);
            this.txtBx_Id.Name = "txtBx_Id";
            this.txtBx_Id.ReadOnly = true;
            this.txtBx_Id.Size = new System.Drawing.Size(100, 20);
            this.txtBx_Id.TabIndex = 5;
            // 
            // txtBx_Title
            // 
            this.txtBx_Title.Location = new System.Drawing.Point(222, 71);
            this.txtBx_Title.Name = "txtBx_Title";
            this.txtBx_Title.Size = new System.Drawing.Size(256, 20);
            this.txtBx_Title.TabIndex = 6;
            // 
            // txtBx_Description
            // 
            this.txtBx_Description.Location = new System.Drawing.Point(15, 119);
            this.txtBx_Description.Multiline = true;
            this.txtBx_Description.Name = "txtBx_Description";
            this.txtBx_Description.Size = new System.Drawing.Size(461, 73);
            this.txtBx_Description.TabIndex = 7;
            // 
            // btn_AddObject
            // 
            this.btn_AddObject.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddObject.Image")));
            this.btn_AddObject.Location = new System.Drawing.Point(15, 357);
            this.btn_AddObject.Name = "btn_AddObject";
            this.btn_AddObject.Size = new System.Drawing.Size(35, 23);
            this.btn_AddObject.TabIndex = 9;
            this.btn_AddObject.UseVisualStyleBackColor = true;
            this.btn_AddObject.Click += new System.EventHandler(this.btn_AddObject_Click);
            // 
            // btn_RemoveObject
            // 
            this.btn_RemoveObject.Image = ((System.Drawing.Image)(resources.GetObject("btn_RemoveObject.Image")));
            this.btn_RemoveObject.Location = new System.Drawing.Point(56, 357);
            this.btn_RemoveObject.Name = "btn_RemoveObject";
            this.btn_RemoveObject.Size = new System.Drawing.Size(35, 23);
            this.btn_RemoveObject.TabIndex = 11;
            this.btn_RemoveObject.UseVisualStyleBackColor = true;
            this.btn_RemoveObject.Click += new System.EventHandler(this.btn_RemoveObject_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(399, 357);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 12;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(318, 357);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 13;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
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
            this.lstVw_3dObjects.FullRowSelect = true;
            this.lstVw_3dObjects.GridLines = true;
            this.lstVw_3dObjects.Location = new System.Drawing.Point(15, 219);
            this.lstVw_3dObjects.MultiSelect = false;
            this.lstVw_3dObjects.Name = "lstVw_3dObjects";
            this.lstVw_3dObjects.Size = new System.Drawing.Size(461, 123);
            this.lstVw_3dObjects.TabIndex = 14;
            this.lstVw_3dObjects.UseCompatibleStateImageBehavior = false;
            this.lstVw_3dObjects.View = System.Windows.Forms.View.Details;
            // 
            // clmnHdr_Id
            // 
            this.clmnHdr_Id.Text = "Id";
            // 
            // clmnHdr_Number
            // 
            this.clmnHdr_Number.Text = "N°";
            // 
            // clmnHdr_Name
            // 
            this.clmnHdr_Name.Text = "Libelle";
            this.clmnHdr_Name.Width = 100;
            // 
            // clmnHdr_Type
            // 
            this.clmnHdr_Type.Text = "Type";
            this.clmnHdr_Type.Width = 100;
            // 
            // clmnHdr_Position
            // 
            this.clmnHdr_Position.Text = "Position(x,y,z)";
            this.clmnHdr_Position.Width = 100;
            // 
            // clmnHdr_Rotation
            // 
            this.clmnHdr_Rotation.Text = "Rotation(x,y,z,angle)";
            this.clmnHdr_Rotation.Width = 100;
            // 
            // clmnHdr_Scale
            // 
            this.clmnHdr_Scale.Text = "Echelle(x,y,z)";
            this.clmnHdr_Scale.Width = 100;
            // 
            // frm_AddStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 396);
            this.Controls.Add(this.lstVw_3dObjects);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_RemoveObject);
            this.Controls.Add(this.btn_AddObject);
            this.Controls.Add(this.txtBx_Description);
            this.Controls.Add(this.txtBx_Title);
            this.Controls.Add(this.txtBx_Id);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_ProcedureName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frm_AddStep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frm_AddStep";
            this.Load += new System.EventHandler(this.frm_AddStep_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_ProcedureName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBx_Id;
        private System.Windows.Forms.TextBox txtBx_Title;
        private System.Windows.Forms.TextBox txtBx_Description;
        private System.Windows.Forms.Button btn_AddObject;
        private System.Windows.Forms.Button btn_RemoveObject;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.ListView lstVw_3dObjects;
        private System.Windows.Forms.ColumnHeader clmnHdr_Id;
        private System.Windows.Forms.ColumnHeader clmnHdr_Number;
        private System.Windows.Forms.ColumnHeader clmnHdr_Name;
        private System.Windows.Forms.ColumnHeader clmnHdr_Type;
        private System.Windows.Forms.ColumnHeader clmnHdr_Position;
        private System.Windows.Forms.ColumnHeader clmnHdr_Rotation;
        private System.Windows.Forms.ColumnHeader clmnHdr_Scale;
    }
}