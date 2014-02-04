using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrator
{
    public partial class frm_Login : Form
    {
        Color emptyFieldColor = Color.Orange;
        Color invalidFieldColor = Color.Red;

        public frm_Login()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Tag = false;
            Close();
        }

        private void btn_Validate_Click(object sender, EventArgs e)
        {
            InitializeLabelColors();
            Actor act = new Actor();
            act.setLogin(txtBx_Login.Text);
            act.setPassword(txtBx_Password.Text);

            bool ret = true;
            if (txtBx_Login.Text == "")
            {
                lbl_Login.ForeColor = emptyFieldColor;
                ret = false;
            }
            
            if (txtBx_Password.Text == "")
            {
                lbl_Password.ForeColor = emptyFieldColor;
                ret = false;
            }

            if (!ret)
            {
                MessageBox.Show(this, "Veuillez remplir tous les champs", "Message d'erreur", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                return;
            }

            ret = true;

            if (!act.HasValidLogin())
            {
                lbl_Login.ForeColor=invalidFieldColor;
                ret = false;
            }

            if (!act.HasValidPassword())
            {
                lbl_Password.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!ret)
            {
                MessageBox.Show(this, "Le login ou bien le mot de passe est invalide", "Message d'erreur", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                return;
            }

            if (CorrectLogin())
            {
                this.Tag = true;
                //this.ShowInTaskbar = false;
                //this.Hide();
                //(new AdministratorApp()).ShowDialog();
                Close();
            }
            else
                MessageBox.Show(this, "Le login ou bien le mot de passe est erroné", "Message d'erreur", MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
        }

        public bool CorrectLogin()
        {
            return XML_Manager.XMLAdministrator.Login(txtBx_Login.Text, txtBx_Password.Text);
        }

        /// <summary>
        /// Sete the labels colors to Black.
        /// </summary>
        public void InitializeLabelColors()
        {
            lbl_Login.ForeColor = Color.Black;
            lbl_Password.ForeColor = Color.Black;
        }
    }
}
