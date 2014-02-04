namespace Administrator
{
    partial class frm_SearchActor
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
            this.pnl_TitleHolder = new System.Windows.Forms.Panel();
            this.lbl_Actor = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbtn_Name = new System.Windows.Forms.RadioButton();
            this.btn_Search = new System.Windows.Forms.Button();
            this.rdbtn_Login = new System.Windows.Forms.RadioButton();
            this.txtBx_Search = new System.Windows.Forms.TextBox();
            this.rdbtn_ID = new System.Windows.Forms.RadioButton();
            this.pnl_TitleHolder.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_TitleHolder
            // 
            this.pnl_TitleHolder.Controls.Add(this.lbl_Actor);
            this.pnl_TitleHolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_TitleHolder.Location = new System.Drawing.Point(0, 0);
            this.pnl_TitleHolder.Name = "pnl_TitleHolder";
            this.pnl_TitleHolder.Size = new System.Drawing.Size(315, 45);
            this.pnl_TitleHolder.TabIndex = 10;
            // 
            // lbl_Actor
            // 
            this.lbl_Actor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Actor.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Actor.Location = new System.Drawing.Point(0, 0);
            this.lbl_Actor.Name = "lbl_Actor";
            this.lbl_Actor.Size = new System.Drawing.Size(315, 45);
            this.lbl_Actor.TabIndex = 1;
            this.lbl_Actor.Text = "Expert";
            this.lbl_Actor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbtn_Name);
            this.groupBox1.Controls.Add(this.btn_Search);
            this.groupBox1.Controls.Add(this.rdbtn_Login);
            this.groupBox1.Controls.Add(this.txtBx_Search);
            this.groupBox1.Controls.Add(this.rdbtn_ID);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 81);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chercher par";
            // 
            // rdbtn_Name
            // 
            this.rdbtn_Name.AutoSize = true;
            this.rdbtn_Name.Location = new System.Drawing.Point(114, 21);
            this.rdbtn_Name.Name = "rdbtn_Name";
            this.rdbtn_Name.Size = new System.Drawing.Size(46, 17);
            this.rdbtn_Name.TabIndex = 5;
            this.rdbtn_Name.Text = "Nom";
            this.rdbtn_Name.UseVisualStyleBackColor = true;
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(235, 41);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 7;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // rdbtn_Login
            // 
            this.rdbtn_Login.AutoSize = true;
            this.rdbtn_Login.Location = new System.Drawing.Point(47, 21);
            this.rdbtn_Login.Name = "rdbtn_Login";
            this.rdbtn_Login.Size = new System.Drawing.Size(50, 17);
            this.rdbtn_Login.TabIndex = 4;
            this.rdbtn_Login.Text = "Login";
            this.rdbtn_Login.UseVisualStyleBackColor = true;
            // 
            // txtBx_Search
            // 
            this.txtBx_Search.Location = new System.Drawing.Point(4, 44);
            this.txtBx_Search.Name = "txtBx_Search";
            this.txtBx_Search.Size = new System.Drawing.Size(225, 20);
            this.txtBx_Search.TabIndex = 0;
            this.txtBx_Search.TextChanged += new System.EventHandler(this.txtBx_Search_TextChanged);
            this.txtBx_Search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBx_Search_KeyPress);
            // 
            // rdbtn_ID
            // 
            this.rdbtn_ID.AutoSize = true;
            this.rdbtn_ID.Checked = true;
            this.rdbtn_ID.Location = new System.Drawing.Point(6, 21);
            this.rdbtn_ID.Name = "rdbtn_ID";
            this.rdbtn_ID.Size = new System.Drawing.Size(35, 17);
            this.rdbtn_ID.TabIndex = 3;
            this.rdbtn_ID.TabStop = true;
            this.rdbtn_ID.Text = "Id";
            this.rdbtn_ID.UseVisualStyleBackColor = true;
            // 
            // frm_SearchActor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 126);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnl_TitleHolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_SearchActor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "La recherche";
            this.pnl_TitleHolder.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_TitleHolder;
        private System.Windows.Forms.Label lbl_Actor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbtn_Name;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.RadioButton rdbtn_Login;
        private System.Windows.Forms.TextBox txtBx_Search;
        private System.Windows.Forms.RadioButton rdbtn_ID;
    }
}