namespace Cd3dLoadXFile
{
    partial class RenderForm
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
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.cboFile = new System.Windows.Forms.ComboBox();
            this.radPoints = new System.Windows.Forms.RadioButton();
            this.radWireframe = new System.Windows.Forms.RadioButton();
            this.radSolid = new System.Windows.Forms.RadioButton();
            this.pic3d = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic3d)).BeginInit();
            this.SuspendLayout();
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(256, 24);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(24, 23);
            this.btnZoomOut.TabIndex = 17;
            this.btnZoomOut.Text = "-";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(256, 0);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(24, 23);
            this.btnZoomIn.TabIndex = 16;
            this.btnZoomIn.Text = "+";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(5, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(26, 13);
            this.Label1.TabIndex = 19;
            this.Label1.Text = "File:";
            // 
            // cboFile
            // 
            this.cboFile.FormattingEnabled = true;
            this.cboFile.Location = new System.Drawing.Point(37, 0);
            this.cboFile.Name = "cboFile";
            this.cboFile.Size = new System.Drawing.Size(208, 21);
            this.cboFile.TabIndex = 12;
            this.cboFile.SelectedIndexChanged += new System.EventHandler(this.cboFile_SelectedIndexChanged);
            // 
            // radPoints
            // 
            this.radPoints.AutoSize = true;
            this.radPoints.Location = new System.Drawing.Point(192, 32);
            this.radPoints.Name = "radPoints";
            this.radPoints.Size = new System.Drawing.Size(54, 17);
            this.radPoints.TabIndex = 15;
            this.radPoints.Text = "Points";
            this.radPoints.UseVisualStyleBackColor = true;
            // 
            // radWireframe
            // 
            this.radWireframe.AutoSize = true;
            this.radWireframe.Location = new System.Drawing.Point(88, 32);
            this.radWireframe.Name = "radWireframe";
            this.radWireframe.Size = new System.Drawing.Size(73, 17);
            this.radWireframe.TabIndex = 14;
            this.radWireframe.Text = "Wireframe";
            this.radWireframe.UseVisualStyleBackColor = true;
            // 
            // radSolid
            // 
            this.radSolid.AutoSize = true;
            this.radSolid.Checked = true;
            this.radSolid.Location = new System.Drawing.Point(8, 32);
            this.radSolid.Name = "radSolid";
            this.radSolid.Size = new System.Drawing.Size(48, 17);
            this.radSolid.TabIndex = 13;
            this.radSolid.TabStop = true;
            this.radSolid.Text = "Solid";
            this.radSolid.UseVisualStyleBackColor = true;
            // 
            // pic3d
            // 
            this.pic3d.Location = new System.Drawing.Point(0, 56);
            this.pic3d.Name = "pic3d";
            this.pic3d.Size = new System.Drawing.Size(292, 292);
            this.pic3d.TabIndex = 18;
            this.pic3d.TabStop = false;
            // 
            // RenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 348);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cboFile);
            this.Controls.Add(this.radPoints);
            this.Controls.Add(this.radWireframe);
            this.Controls.Add(this.radSolid);
            this.Controls.Add(this.pic3d);
            this.Name = "RenderForm";
            this.Text = "Cd3dLoadXFile";
            this.Load += new System.EventHandler(this.RenderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic3d)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnZoomOut;
        internal System.Windows.Forms.Button btnZoomIn;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cboFile;
        internal System.Windows.Forms.RadioButton radPoints;
        internal System.Windows.Forms.RadioButton radWireframe;
        internal System.Windows.Forms.RadioButton radSolid;
        internal System.Windows.Forms.PictureBox pic3d;

    }
}

