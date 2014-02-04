namespace MARS_Expert
{
    partial class frm_ListOf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ListOf));
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lstBx = new System.Windows.Forms.ListBox();
            this.pnl_Top = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_CloseForm = new System.Windows.Forms.Button();
            this.pnl_Left = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_Right = new System.Windows.Forms.Panel();
            this.pnl_Bottom = new System.Windows.Forms.Panel();
            this.pnl_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_Left.SuspendLayout();
            this.SuspendLayout();
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
            this.btn_OK.Location = new System.Drawing.Point(147, 36);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
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
            this.btn_Cancel.Location = new System.Drawing.Point(147, 65);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Annuler";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            this.btn_Cancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btn_Cancel.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btn_Cancel.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_Cancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // lstBx
            // 
            this.lstBx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lstBx.FormattingEnabled = true;
            this.lstBx.Location = new System.Drawing.Point(12, 36);
            this.lstBx.Name = "lstBx";
            this.lstBx.Size = new System.Drawing.Size(129, 199);
            this.lstBx.TabIndex = 2;
            this.lstBx.DoubleClick += new System.EventHandler(this.lstBx_DoubleClick);
            // 
            // pnl_Top
            // 
            this.pnl_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Top.BackgroundImage")));
            this.pnl_Top.Controls.Add(this.pictureBox1);
            this.pnl_Top.Controls.Add(this.btn_CloseForm);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(3, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Size = new System.Drawing.Size(228, 30);
            this.pnl_Top.TabIndex = 61;
            this.pnl_Top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_Top_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 26);
            this.pictureBox1.TabIndex = 61;
            this.pictureBox1.TabStop = false;
            // 
            // btn_CloseForm
            // 
            this.btn_CloseForm.BackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatAppearance.BorderSize = 0;
            this.btn_CloseForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CloseForm.Image = ((System.Drawing.Image)(resources.GetObject("btn_CloseForm.Image")));
            this.btn_CloseForm.Location = new System.Drawing.Point(196, -1);
            this.btn_CloseForm.Name = "btn_CloseForm";
            this.btn_CloseForm.Size = new System.Drawing.Size(29, 34);
            this.btn_CloseForm.TabIndex = 48;
            this.btn_CloseForm.UseVisualStyleBackColor = false;
            this.btn_CloseForm.Click += new System.EventHandler(this.btn_CloseForm_Click);
            this.btn_CloseForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_CloseForm_MouseDown);
            this.btn_CloseForm.MouseEnter += new System.EventHandler(this.btn_CloseForm_MouseEnter);
            this.btn_CloseForm.MouseLeave += new System.EventHandler(this.btn_CloseForm_MouseLeave);
            this.btn_CloseForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_CloseForm_MouseUp);
            // 
            // pnl_Left
            // 
            this.pnl_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnl_Left.Controls.Add(this.panel1);
            this.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Left.Location = new System.Drawing.Point(0, 0);
            this.pnl_Left.Name = "pnl_Left";
            this.pnl_Left.Size = new System.Drawing.Size(3, 241);
            this.pnl_Left.TabIndex = 62;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 241);
            this.panel1.TabIndex = 57;
            // 
            // pnl_Right
            // 
            this.pnl_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnl_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_Right.Location = new System.Drawing.Point(231, 0);
            this.pnl_Right.Name = "pnl_Right";
            this.pnl_Right.Size = new System.Drawing.Size(3, 241);
            this.pnl_Right.TabIndex = 64;
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Bottom.Location = new System.Drawing.Point(0, 241);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Size = new System.Drawing.Size(234, 3);
            this.pnl_Bottom.TabIndex = 63;
            // 
            // frm_ListOf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 244);
            this.Controls.Add(this.pnl_Top);
            this.Controls.Add(this.pnl_Left);
            this.Controls.Add(this.pnl_Right);
            this.Controls.Add(this.pnl_Bottom);
            this.Controls.Add(this.lstBx);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_ListOf";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ListOf";
            this.Load += new System.EventHandler(this.frm_ListOf_Load);
            this.pnl_Top.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_Left.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ListBox lstBx;
        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.Button btn_CloseForm;
        private System.Windows.Forms.Panel pnl_Left;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_Right;
        private System.Windows.Forms.Panel pnl_Bottom;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}