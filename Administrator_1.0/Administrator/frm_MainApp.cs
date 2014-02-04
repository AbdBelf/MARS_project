using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.XPath;


namespace Administrator
{
    public partial class AdministratorApp : Form
    {
        //string login;
        //string password;
        //string firstName;
        //string lastName;
        //string email;
        //string phoneNumber;
        //string address;
        //string role;//Expert, Technician or Administrator
        //string specialty;//Domaine de travail
        //string status;

        string[] item;

        public AdministratorApp()
        {
            InitializeComponent();
            loadListView();
        }

        public void loadListView()
        {
            lstVw_ActorsList.Items.Clear();

            //If the Expert radiobutton is checked we load the experts information into the list view
            if(rdbtn_Expert.Checked)
            {
                #region Fill the experts data.                
                List<Manager.Expert> experts = XML_Manager.XMLExpert.GetAll(chkBx_ShowDeactivated.Checked);
                foreach (Manager.Expert expert in experts)
                {
                    item = new string[] { expert.getId().ToString(), expert.getLogin(), expert.getPassword(), expert.getFirstName(),
                       expert.getLastName(),expert.getEmail(),expert.getPhoneNumber(),expert.getAddress(),
                       expert.getRole(),expert.getSpecialty(),expert.getStatus().ToString()};
                    lstVw_ActorsList.Items.Add(new ListViewItem(item));
                }
                #endregion
            }
            else//If the Technician radiobutton is checked we load the thecnicians information into the list view
            {
                #region Fill the technicians data.
                List<Manager.Technician> technicians = XML_Manager.XMLTechnician.GetAll(chkBx_ShowDeactivated.Checked);
                //MessageBox.Show(technicians.Count.ToString());
                foreach (Manager.Technician technician in technicians)
                {
                    item = new string[] {technician.getId().ToString(), technician.getLogin(), technician.getPassword(), technician.getFirstName(),
                       technician.getLastName(),technician.getEmail(),technician.getPhoneNumber(),technician.getAddress(),
                       technician.getRole(),technician.getSpecialty(),technician.getStatus().ToString()};
                    lstVw_ActorsList.Items.Add(new ListViewItem(item));
                }
                #endregion
            }

            //Clolorer la liste
            ColorListView();
        }

        public void ColorListView()
        {
            ColorConverter c = new ColorConverter();

            //lstVw_ActorsList.BackColor = Color.Ivory;
            //lstVw_ActorsList.BackColor = Color.Lavender;
            lstVw_ActorsList.ForeColor = (Color)c.ConvertFromString("#333333");
            for(int i=0;i<lstVw_ActorsList.Items.Count;i++)
            {
                //if (i % 2 == 0)
                //{
                //    lstVw_ActorsList.Items[i].BackColor = (Color)c.ConvertFromString("#dedeff");
                //}
                //else
                //{
                //    lstVw_ActorsList.Items[i].BackColor = (Color)c.ConvertFromString("#bfbff7");
                //}
                if (lstVw_ActorsList.Items[i].SubItems[10].Text == "Deactivated")
                {
                    //Use of gray color.
                    lstVw_ActorsList.Items[i].BackColor = (Color)c.ConvertFromString("#dadada");
                }
            }
        }

        private void rdbtn_Expert_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Expert.Checked)
                pnl_UpperContentHelper.BackColor = rdbtn_Expert.BackColor;

            //if the selection changed we load the list again wether this radio button is checked or not
            // cause the test is done in the loadList methode
            loadListView();
        }

        private void rdbtn_Technician_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Technician.Checked)
                pnl_UpperContentHelper.BackColor = rdbtn_Technician.BackColor;
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            #region Insert actors
            /*
            Manager.Administrator admin = new Manager.Administrator("Abdelalim", "ZERKANI", "abdel.alim.zerkani@gmail.com", "0791320352", "RUB 3 BABEZZOUAR ALGER", "admin",
                "Acount management", "admin", "1234");

            Manager.Expert expert = new Manager.Expert("Abdelalim", "ZERKANI", "zetrix.zer@gmail.com", "0791320352", "Djbel Djorf Tebessa Alger", "expert",
                "Car engin", "expert1", "1234");

            Manager.Technician thecnician = new Manager.Technician("Abdelalim", "ZERKANI", "zaalim1@gmail.com", "0791320352", "Tebessa Alger", "technician",
                "Car reparing", "tech1", "1234");

            if (!File.Exists("Administrator.xml"))
                XML_Manager.XMLAdministrator.Add(admin);
            
            if (!File.Exists("Experts.xml"))
                XML_Manager.XMLExpert.firstAdd(expert);
            else
                XML_Manager.XMLExpert.insert(expert);

            if (!File.Exists("Technicians.xml"))
                XML_Manager.XMLTechnician.firstAdd(thecnician);
            else
                XML_Manager.XMLTechnician.insert(thecnician);
            MessageBox.Show("Actors added successfuly");*/
            #endregion

            Width++; Width--;
            foreach (Manager.Expert expert in XML_Manager.XMLExpert.GetAll(false))
            { MessageBox.Show(expert.getId().ToString()); }
            
            //MessageBox.Show(XML_Manager.XMLExpert.IdExists("expertj").ToString());
        }

        private void chkBx_ShowDeactivated_CheckedChanged(object sender, EventArgs e)
        {
            loadListView();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (rdbtn_Expert.Checked)
            {
                frm_AddActor addActor = new frm_AddActor("Expert");
                addActor.ShowDialog();
                loadListView();
            }
            else
            {
                frm_AddActor addActor = new frm_AddActor("Technicien");
                addActor.ShowDialog();
                loadListView();
            }
        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            //Manager.Expert expert = new Manager.Expert(selected.SubItems[3].Text,selected.SubItems[4].Text,selected.SubItems[5].Text,selected.SubItems[6].Text,
            //    selected.SubItems[7].Text,selected.SubItems[8].Text,selected.SubItems[9].Text,selected.SubItems[1].Text,selected.SubItems[2].Text);
            if (lstVw_ActorsList.SelectedItems.Count > 0)
            {

                //Get the selected Actor.
                ListViewItem selected = lstVw_ActorsList.SelectedItems[0];
                //Modify an Expert account.
                if (rdbtn_Expert.Checked)
                {
                    Manager.Expert expert = new Manager.Expert(selected.SubItems[3].Text, selected.SubItems[4].Text, selected.SubItems[5].Text, selected.SubItems[6].Text,
                        selected.SubItems[7].Text, selected.SubItems[8].Text, selected.SubItems[9].Text, selected.SubItems[1].Text, selected.SubItems[2].Text);
                    frm_ModifyActor modifActor = new frm_ModifyActor("Expert", expert);
                    modifActor.ShowDialog();
                    loadListView();
                }
                else//Modify a Technician account.
                {
                    Manager.Technician tech = new Manager.Technician(selected.SubItems[3].Text, selected.SubItems[4].Text, selected.SubItems[5].Text, selected.SubItems[6].Text,
                        selected.SubItems[7].Text, selected.SubItems[8].Text, selected.SubItems[9].Text, selected.SubItems[1].Text, selected.SubItems[2].Text);
                    frm_ModifyActor modifActor = new frm_ModifyActor("Technicien", tech);
                    modifActor.ShowDialog();
                    loadListView();
                }
                loadListView();
            }
            else
                MessageBox.Show(this, "Pas d'intervenant sélectionné!", "Message d'erreur", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
        }

        //Delete(Deactivate) an Actor account from the system.
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (lstVw_ActorsList.SelectedItems.Count > 0)
            {
                DialogResult dr= MessageBox.Show(this, "Voulez vous vraiment supprimer cet intervenant?", "Message de confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                    return;
                //Get the selected Actor.
                ListViewItem selected = lstVw_ActorsList.SelectedItems[0];
                //Delete an Expert.
                if (rdbtn_Expert.Checked)
                {
                    Manager.Expert expert = new Manager.Expert();
                    expert.setLogin(selected.SubItems[1].Text);
                    expert.Delete();
                }
                else//Delete a Technician.
                {
                    Manager.Technician tech = new Manager.Technician();
                    tech.setLogin(selected.SubItems[1].Text);
                    tech.Delete();
                }
                loadListView();
            }
            else
                MessageBox.Show(this, "Pas d'intervenant sélectionné!", "Message d'erreur", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (rdbtn_Expert.Checked)
            {
                frm_SearchActor search = new frm_SearchActor("Expert",lstVw_ActorsList);
                search.ShowDialog();
            }
            else//Modify a Technician account.
            {
                frm_SearchActor search = new frm_SearchActor("Technicien",lstVw_ActorsList);
                search.ShowDialog();
            }
        }

        private void gestionDeProfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Login frm =new frm_Login();
            frm.ShowDialog();
            if((bool)frm.Tag)
            {
                Manager.Administrator admin = new Manager.Administrator();
                admin = admin.GetAdmin();
                frm_AdminProfile adminprofile = new frm_AdminProfile(admin);
                adminprofile.ShowDialog();
            }
        }

        private void AdministratorApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void activerCompteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstVw_ActorsList.SelectedItems.Count > 0)
            {
                //Get the selected Actor.
                ListViewItem selected = lstVw_ActorsList.SelectedItems[0];
                //Delete an Expert.
                if (rdbtn_Expert.Checked)
                {
                    Manager.Expert expert = new Manager.Expert();
                    expert.setLogin(selected.SubItems[1].Text);
                    expert.Activate();
                }
                else//Delete a Technician.
                {
                    Manager.Technician tech = new Manager.Technician();
                    tech.setLogin(selected.SubItems[1].Text);
                    tech.Activate();
                }
                loadListView();
            }
            else
                MessageBox.Show(this, "Pas d'intervenant sélectionné!", "Message d'erreur", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            loadListView();
        }

        private void AdministratorApp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.F5)
                loadListView();
        }

    }
}


