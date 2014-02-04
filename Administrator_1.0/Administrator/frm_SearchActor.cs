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
    public partial class frm_SearchActor : Form
    {
        ListView lstVw_ActorsList;
        bool includeDeleted;
        string[] item;

        public frm_SearchActor(string actor, ListView listView)
        {
            InitializeComponent();

            lbl_Actor.Text = actor;
            this.lstVw_ActorsList = listView;
        }

        public void SearchAndLoad()
        {
            lstVw_ActorsList.Items.Clear();

            //E for Expert
            if (lbl_Actor.Text[0] == 'E')
            {
                #region Fill the experts data.
                List<Manager.Expert> experts = new List<Manager.Expert>();
                if (txtBx_Search.Text == "")
                {
                    experts = XML_Manager.XMLExpert.GetAll(false);
                }
                else
                    if (rdbtn_ID.Checked)
                    {
                        experts.Add(XML_Manager.XMLExpert.SearchById(Convert.ToInt32(txtBx_Search.Text)));
                    }
                    else
                        if (rdbtn_Login.Checked)
                        {
                            experts.Add(XML_Manager.XMLExpert.SearchById(txtBx_Search.Text));
                        }
                        else
                            if (rdbtn_Name.Checked)
                            {
                                experts = XML_Manager.XMLExpert.SearchByName(txtBx_Search.Text);
                            }

                foreach (Manager.Expert expert in experts)
                {
                    item = new string[] { expert.getId().ToString(), expert.getLogin(), expert.getPassword(), expert.getFirstName(),
                       expert.getLastName(),expert.getEmail(),expert.getPhoneNumber(),expert.getAddress(),
                       expert.getRole(),expert.getSpecialty(),expert.getStatus().ToString()};
                    lstVw_ActorsList.Items.Add(new ListViewItem(item));
                }
                #endregion
            }
            else//the technician
            {
                #region Fill the technicians data.
                List<Manager.Technician> technicians = new List<Manager.Technician>();

                if (txtBx_Search.Text == "")
                {
                    technicians = XML_Manager.XMLTechnician.GetAll(false);
                }
                else
                    if (rdbtn_ID.Checked)
                    {
                        technicians.Add(XML_Manager.XMLTechnician.SearchById(Convert.ToInt32(txtBx_Search.Text)));
                    }
                    else
                        if (rdbtn_Login.Checked)
                        {
                            technicians.Add(XML_Manager.XMLTechnician.SearchById(txtBx_Search.Text));
                        }
                        else
                            if (rdbtn_Name.Checked)
                            {
                                technicians = XML_Manager.XMLTechnician.SearchByName(txtBx_Search.Text);
                            }

                foreach (Manager.Technician tech in technicians)
                {
                    item = new string[] { tech.getId().ToString(), tech.getLogin(), tech.getPassword(), tech.getFirstName(),
                       tech.getLastName(),tech.getEmail(),tech.getPhoneNumber(),tech.getAddress(),
                       tech.getRole(),tech.getSpecialty(),tech.getStatus().ToString()};
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
            for (int i = 0; i < lstVw_ActorsList.Items.Count; i++)
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

        private void btn_Search_Click(object sender, EventArgs e)
        {
            SearchAndLoad();
        }

        private void txtBx_Search_TextChanged(object sender, EventArgs e)
        {
            SearchAndLoad();
        }

        private void txtBx_Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rdbtn_ID.Checked && (e.KeyChar <'0' || e.KeyChar>'9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
