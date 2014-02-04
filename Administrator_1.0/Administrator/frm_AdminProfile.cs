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
    public partial class frm_AdminProfile : Form
    {
        Manager.Administrator _Admin;

        List<TextBox> Fields = new List<TextBox>();
        List<Label> FieldLabels = new List<Label>();

        Color emptyFieldColor = Color.Orange;
        Color invalidFieldColor = Color.Red;

        public frm_AdminProfile(Manager.Administrator admin)
        {
            InitializeComponent();
            _Admin = admin;
            #region Load the form text fields to Fields list.
            Fields.Add(txtBx_LastName);
            Fields.Add(txtBx_FirstName);
            Fields.Add(txtBx_Email);
            Fields.Add(txtBx_Phone);
            Fields.Add(txtBx_Address);
            Fields.Add(txtBx_Role);
            Fields.Add(txtBx_Specialty);
            Fields.Add(txtBx_Login);
            Fields.Add(txtBx_Password);
            Fields.Add(txtBx_ConfirmPassword);
            #endregion

            #region Load the form labels to FieldLabels list.
            FieldLabels.Add(lbl_LastName);
            FieldLabels.Add(lbl_FirstName);
            FieldLabels.Add(lbl_Email);
            FieldLabels.Add(lbl_Phone);
            FieldLabels.Add(lbl_Address);
            FieldLabels.Add(lbl_Role);
            FieldLabels.Add(lbl_Specialty);
            FieldLabels.Add(lbl_Login);
            FieldLabels.Add(lbl_Password);
            FieldLabels.Add(lbl_ConfirmPassword);
            #endregion

            #region Fill the form's text fields
            txtBx_FirstName.Text = _Admin.getFirstName();
            txtBx_LastName.Text = _Admin.getLastName();
            txtBx_Email.Text = _Admin.getEmail();
            txtBx_Phone.Text = _Admin.getPhoneNumber();
            txtBx_Address.Text = _Admin.getAddress();
            txtBx_Role.Text = _Admin.getRole();
            txtBx_Specialty.Text = _Admin.getSpecialty();
            txtBx_Login.Text = _Admin.getLogin();
            txtBx_Password.Text = _Admin.getPassword();
            txtBx_ConfirmPassword.Text = _Admin.getPassword();
            #endregion
        }


        private void btn_Validate_Click(object sender, EventArgs e)
        {
            if (Check_Fields())
            {

                //The instructions to modify admin data.
                Manager.Administrator admin = new Manager.Administrator(txtBx_FirstName.Text, txtBx_LastName.Text, txtBx_Email.Text,
                    txtBx_Phone.Text, txtBx_Address.Text, txtBx_Role.Text, txtBx_Specialty.Text, txtBx_Login.Text, txtBx_Password.Text);
                if (Check_Admin(admin))
                {
                    if (PasswordDontMatch())
                    {
                        lbl_Password.ForeColor = Color.OrangeRed;
                        lbl_ConfirmPassword.ForeColor = Color.OrangeRed;
                        MessageBox.Show(this, "Les mots de passe ne sont pas identiques.", "Message d'erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else//Modify the admin.
                    {
                        if (admin.Modify())
                        {
                            MessageBox.Show(this, "Modification terminée avec succès.", "Message d'information", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                            MessageBox.Show(this, "Modification terminée avec erreurs.", "Message d'information", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("Il y a des champs invalides dans le formulaires(en rouge).");

            }
            else
            {
                MessageBox.Show(this, "Remplir tous les champs d'abord.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sete the labels colors to Black.
        /// </summary>
        public void InitializeLabelColors()
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                FieldLabels[i].ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Checks for empty fields in the form.
        /// </summary>
        /// <returns></returns>
        public bool Check_Fields()
        {
            bool ret = true;
            InitializeLabelColors();

            for (int i = 0; i < Fields.Count; i++)
            {
                if (Fields[i].Text == "")
                {
                    //MessageBox.Show(Fields[i].Name);
                    FieldLabels[i].ForeColor = emptyFieldColor;
                    ret = false;
                }
            }
            return ret;
        }

        private bool Check_Admin(Manager.Administrator admin)
        {
            bool ret = true;
            InitializeLabelColors();

            if (!admin.HasValidFirstName())
            {
                lbl_FirstName.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!admin.HasValidLastName())
            {
                lbl_LastName.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!admin.HasValidEmail())
            {
                lbl_Email.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!admin.HasValidPhoneNumber())
            {
                lbl_Phone.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!admin.HasValidAddress())
            {
                lbl_Address.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!admin.HasValidSpecialty())
            {
                lbl_Specialty.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!admin.HasValidLogin())
            {
                lbl_Login.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!admin.HasValidPassword())
            {
                lbl_Password.ForeColor = invalidFieldColor;
                ret = false;
            }
            return ret;
        }

        public bool PasswordDontMatch()
        {
            return !(txtBx_Password.Text == txtBx_ConfirmPassword.Text);
        }

        /// <summary>
        /// Allows only numbers typing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBx_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
