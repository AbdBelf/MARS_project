using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Administrator.Manager;

namespace Administrator
{
    public partial class frm_AddActor : Form
    {
        List<TextBox> Fields = new List<TextBox>();
        List<Label> FieldLabels = new List<Label>();

        Color emptyFieldColor = Color.Orange;
        Color invalidFieldColor = Color.Red;

        public frm_AddActor(string actor)
        {
            InitializeComponent();
            lbl_Actor.Text = actor;
            txtBx_Role.Text = actor;

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
        }

        //Add an Actor (Expert/Technician) to the data base.
        private void btn_Validate_Click(object sender, EventArgs e)
        {
            if (Check_Fields())
            {
                //T for Technician
                if (lbl_Actor.Text[0] == 'T')
                {
                    //The instructions to add a technician to the data base.
                    Manager.Technician tech = new Manager.Technician(txtBx_FirstName.Text, txtBx_LastName.Text, txtBx_Email.Text,
                        txtBx_Phone.Text, txtBx_Address.Text, txtBx_Role.Text, txtBx_Specialty.Text, txtBx_Login.Text, txtBx_Password.Text);
                    if (Check_Technician(tech))
                    {
                        if (PasswordDontMatch())
                        {
                            lbl_Password.ForeColor = Color.OrangeRed;
                            lbl_ConfirmPassword.ForeColor = Color.OrangeRed;
                            MessageBox.Show(this, "Les mots de passe ne sont pas identiques.", "Message d'erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        else//Add the Technician.
                        {
                            //If the login(id) doesn't already exist in the DB add this technician.
                            if (!tech.Exists())
                            {
                                if (tech.Add())
                                {
                                    MessageBox.Show(this, "Ajout terminé avec succès.", "Message d'information", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                                this.Close();
                                }
                                else
                                    MessageBox.Show(this, "Ajout terminé avec erreurs.", "Message d'information", MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show(this, "Le login existe deja.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        MessageBox.Show("Il y a des champs invalides dans le formulaires(en rouge).");

                }
                else//E for an Expert
                    if (lbl_Actor.Text[0] == 'E')
                    {
                        //The instructions to add a expert to the data base.
                        Manager.Expert expert = new Manager.Expert(txtBx_FirstName.Text, txtBx_LastName.Text, txtBx_Email.Text,
                            txtBx_Phone.Text, txtBx_Address.Text, txtBx_Role.Text, txtBx_Specialty.Text, txtBx_Login.Text, txtBx_Password.Text);
                        if (Check_Expert(expert))
                        {
                            if (PasswordDontMatch())
                            {
                                lbl_Password.ForeColor = Color.OrangeRed;
                                lbl_ConfirmPassword.ForeColor = Color.OrangeRed;
                                MessageBox.Show(this, "Les mots de passe ne sont pas identiques.", "Message d'erreur",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            else//Add the Expert.
                            {
                                //If the login(id) doesn't already exist in the DB add this expert.
                                if (!expert.Exists())
                                {
                                    expert.Add();
                                    MessageBox.Show(this, "Ajout terminé.", "Message d'information", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                    this.Close();
                                }
                                else
                                    MessageBox.Show(this, "Le login existe deja.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                            MessageBox.Show("Il y a des champs invalides dans le formulaires(en rouge).");
                    }
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

        /// <summary>
        /// Checks for invalid firlds in the form.
        /// </summary>
        /// <param name="tech"></param>
        /// <returns></returns>
        public bool Check_Technician(Actor tech)
        {
            bool ret = true;
            InitializeLabelColors();

            if (!tech.HasValidFirstName())
            {
                lbl_FirstName.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!tech.HasValidLastName())
            {
                lbl_LastName.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!tech.HasValidEmail())
            {
                lbl_Email.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!tech.HasValidPhoneNumber())
            {
                lbl_Phone.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!tech.HasValidAddress())
            {
                lbl_Address.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!tech.HasValidSpecialty())
            {
                lbl_Specialty.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!tech.HasValidLogin())
            {
                lbl_Login.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!tech.HasValidPassword())
            {
                lbl_Password.ForeColor = invalidFieldColor;
                ret = false;
            }
            return ret;
        }

        public bool Check_Expert(Actor expert)
        {
            bool ret = true;
            InitializeLabelColors();

            if (!expert.HasValidFirstName())
            {
                lbl_FirstName.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!expert.HasValidLastName())
            {
                lbl_LastName.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!expert.HasValidEmail())
            {
                lbl_Email.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!expert.HasValidPhoneNumber())
            {
                lbl_Phone.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!expert.HasValidAddress())
            {
                lbl_Address.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!expert.HasValidSpecialty())
            {
                lbl_Specialty.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!expert.HasValidLogin())
            {
                lbl_Login.ForeColor = invalidFieldColor;
                ret = false;
            }
            if (!expert.HasValidPassword())
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
