namespace Administrator
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
            this.lbl_Login = new System.Windows.Forms.Label();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.txtBx_Login = new System.Windows.Forms.TextBox();
            this.txtBx_Password = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Validate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Login
            // 
            this.lbl_Login.AutoSize = true;
            this.lbl_Login.Location = new System.Drawing.Point(41, 43);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(36, 13);
            this.lbl_Login.TabIndex = 0;
            this.lbl_Login.Text = "Login:";
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Location = new System.Drawing.Point(2, 79);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(75, 13);
            this.lbl_Password.TabIndex = 1;
            this.lbl_Password.Text = "Mot de passe:";
            // 
            // txtBx_Login
            // 
            this.txtBx_Login.Location = new System.Drawing.Point(83, 40);
            this.txtBx_Login.Name = "txtBx_Login";
            this.txtBx_Login.Size = new System.Drawing.Size(162, 20);
            this.txtBx_Login.TabIndex = 0;
            // 
            // txtBx_Password
            // 
            this.txtBx_Password.Location = new System.Drawing.Point(83, 76);
            this.txtBx_Password.Name = "txtBx_Password";
            this.txtBx_Password.PasswordChar = '*';
            this.txtBx_Password.Size = new System.Drawing.Size(162, 20);
            this.txtBx_Password.TabIndex = 1;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(170, 114);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Annuler";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Validate
            // 
            this.btn_Validate.Location = new System.Drawing.Point(83, 114);
            this.btn_Validate.Name = "btn_Validate";
            this.btn_Validate.Size = new System.Drawing.Size(75, 23);
            this.btn_Validate.TabIndex = 2;
            this.btn_Validate.Text = "Valider";
            this.btn_Validate.UseVisualStyleBackColor = true;
            this.btn_Validate.Click += new System.EventHandler(this.btn_Validate_Click);
            // 
            // frm_Login
            // 
            this.AcceptButton = this.btn_Validate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(284, 164);
            this.Controls.Add(this.btn_Validate);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.txtBx_Password);
            this.Controls.Add(this.txtBx_Login);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.lbl_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Login;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.TextBox txtBx_Login;
        private System.Windows.Forms.TextBox txtBx_Password;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Validate;
    }
}