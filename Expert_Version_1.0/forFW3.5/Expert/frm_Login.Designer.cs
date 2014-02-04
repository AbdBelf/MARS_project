namespace MARS_Expert
{
    partial class frm_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Login));
            this.lbl_Login = new System.Windows.Forms.Label();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.pnl_Top = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_MinimizeForm = new System.Windows.Forms.Button();
            this.btn_CloseForm = new System.Windows.Forms.Button();
            this.pnl_Left = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_Bottom = new System.Windows.Forms.Panel();
            this.pnl_Right = new System.Windows.Forms.Panel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txtBx_Login = new System.Windows.Forms.TextBox();
            this.pnl_Id = new System.Windows.Forms.Panel();
            this.txtBx_Password = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_Left.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Login
            // 
            this.lbl_Login.AutoSize = true;
            this.lbl_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lbl_Login.Location = new System.Drawing.Point(68, 43);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(36, 13);
            this.lbl_Login.TabIndex = 0;
            this.lbl_Login.Text = "Login:";
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lbl_Password.Location = new System.Drawing.Point(29, 79);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(75, 13);
            this.lbl_Password.TabIndex = 1;
            this.lbl_Password.Text = "Mot de passe:";
            // 
            // pnl_Top
            // 
            this.pnl_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Top.BackgroundImage")));
            this.pnl_Top.Controls.Add(this.label2);
            this.pnl_Top.Controls.Add(this.pictureBox1);
            this.pnl_Top.Controls.Add(this.btn_MinimizeForm);
            this.pnl_Top.Controls.Add(this.btn_CloseForm);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Size = new System.Drawing.Size(300, 30);
            this.pnl_Top.TabIndex = 56;
            this.pnl_Top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_Top_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.label2.Location = new System.Drawing.Point(89, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 63;
            this.label2.Text = "L\'authentification";
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
            // btn_MinimizeForm
            // 
            this.btn_MinimizeForm.BackColor = System.Drawing.Color.Transparent;
            this.btn_MinimizeForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_MinimizeForm.BackgroundImage")));
            this.btn_MinimizeForm.FlatAppearance.BorderSize = 0;
            this.btn_MinimizeForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_MinimizeForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_MinimizeForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MinimizeForm.Location = new System.Drawing.Point(239, 2);
            this.btn_MinimizeForm.Name = "btn_MinimizeForm";
            this.btn_MinimizeForm.Size = new System.Drawing.Size(27, 27);
            this.btn_MinimizeForm.TabIndex = 49;
            this.btn_MinimizeForm.UseVisualStyleBackColor = false;
            this.btn_MinimizeForm.Click += new System.EventHandler(this.btn_MinimizeForm_Click);
            this.btn_MinimizeForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MinimizeForm_MouseDown);
            this.btn_MinimizeForm.MouseEnter += new System.EventHandler(this.btn_MinimizeForm_MouseEnter);
            this.btn_MinimizeForm.MouseLeave += new System.EventHandler(this.btn_MinimizeForm_MouseLeave);
            this.btn_MinimizeForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MinimizeForm_MouseUp);
            // 
            // btn_CloseForm
            // 
            this.btn_CloseForm.BackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatAppearance.BorderSize = 0;
            this.btn_CloseForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CloseForm.Image = ((System.Drawing.Image)(resources.GetObject("btn_CloseForm.Image")));
            this.btn_CloseForm.Location = new System.Drawing.Point(267, -2);
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
            this.pnl_Left.Location = new System.Drawing.Point(0, 30);
            this.pnl_Left.Name = "pnl_Left";
            this.pnl_Left.Size = new System.Drawing.Size(3, 131);
            this.pnl_Left.TabIndex = 57;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 131);
            this.panel1.TabIndex = 57;
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Bottom.Location = new System.Drawing.Point(3, 158);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Size = new System.Drawing.Size(297, 3);
            this.pnl_Bottom.TabIndex = 59;
            // 
            // pnl_Right
            // 
            this.pnl_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnl_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_Right.Location = new System.Drawing.Point(297, 30);
            this.pnl_Right.Name = "pnl_Right";
            this.pnl_Right.Size = new System.Drawing.Size(3, 128);
            this.pnl_Right.TabIndex = 60;
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
            this.btn_OK.Location = new System.Drawing.Point(122, 121);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_Validate_Click);
            this.btn_OK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btn_OK.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btn_OK.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_OK.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.Location = new System.Drawing.Point(203, 121);
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
            // txtBx_Login
            // 
            this.txtBx_Login.BackColor = System.Drawing.Color.White;
            this.txtBx_Login.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBx_Login.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic);
            this.txtBx_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.txtBx_Login.Location = new System.Drawing.Point(127, 49);
            this.txtBx_Login.Name = "txtBx_Login";
            this.txtBx_Login.Size = new System.Drawing.Size(139, 16);
            this.txtBx_Login.TabIndex = 65;
            this.txtBx_Login.Text = "abdel";
            // 
            // pnl_Id
            // 
            this.pnl_Id.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Id.BackgroundImage")));
            this.pnl_Id.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Id.Enabled = false;
            this.pnl_Id.Location = new System.Drawing.Point(122, 43);
            this.pnl_Id.Name = "pnl_Id";
            this.pnl_Id.Size = new System.Drawing.Size(150, 29);
            this.pnl_Id.TabIndex = 66;
            // 
            // txtBx_Password
            // 
            this.txtBx_Password.BackColor = System.Drawing.Color.White;
            this.txtBx_Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBx_Password.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic);
            this.txtBx_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.txtBx_Password.Location = new System.Drawing.Point(127, 85);
            this.txtBx_Password.Name = "txtBx_Password";
            this.txtBx_Password.PasswordChar = '*';
            this.txtBx_Password.Size = new System.Drawing.Size(139, 16);
            this.txtBx_Password.TabIndex = 67;
            this.txtBx_Password.Text = "000000";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(122, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 29);
            this.panel2.TabIndex = 68;
            // 
            // frm_Login
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(300, 161);
            this.Controls.Add(this.txtBx_Password);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtBx_Login);
            this.Controls.Add(this.pnl_Id);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.pnl_Right);
            this.Controls.Add(this.pnl_Bottom);
            this.Controls.Add(this.pnl_Left);
            this.Controls.Add(this.pnl_Top);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.lbl_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Login";
            this.Load += new System.EventHandler(this.frm_Login_Load);
            this.pnl_Top.ResumeLayout(false);
            this.pnl_Top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_Left.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Login;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.Button btn_MinimizeForm;
        private System.Windows.Forms.Button btn_CloseForm;
        private System.Windows.Forms.Panel pnl_Left;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_Bottom;
        private System.Windows.Forms.Panel pnl_Right;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txtBx_Login;
        private System.Windows.Forms.Panel pnl_Id;
        private System.Windows.Forms.TextBox txtBx_Password;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
    }
}