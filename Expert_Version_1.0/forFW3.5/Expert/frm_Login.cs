using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARS_Expert.Manager.ExpertManager;
using MARS_Expert.WebService;
using System.Net;
using MARS_Expert.Manager;

namespace MARS_Expert
{
    public partial class frm_Login : Form
    {
        Color emptyFieldColor = Color.Orange;
        Color invalidFieldColor = Color.Red;

        public frm_Login()
        {
            InitializeComponent();
        }

        public string getLogin()
        {
            return txtBx_Login.Text;
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
                Manager.ExpertManager.Expert expert = new Manager.ExpertManager.Expert();
                expert = expert.getExpert(txtBx_Login.Text);
                Manager.Helper.service = new Service1();

            connect:
                try
                {
                    if (!Manager.Helper.service.ConnectExpert(expert.getLogin(), expert.getFirstName(), expert.getLastName(), ""))
                    {
                        MessageBox.Show("Vous n'êtes pas connectés, ressayez!", "frm_Login.btn_Validate");
                        return;
                    }
                }
                catch (WebException)
                {
                    MessageBox.Show("Problème de connection", "frm_Login.btn_Validate");
                    return;
                }
                catch (System.Web.Services.Protocols.SoapException)
                {
                    try
                    {
                        Manager.Helper.service.DisconnectExpert(txtBx_Login.Text);
                        goto connect;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Problème de connection, essayer plus tard.", "frm_Login.btn_Validate");
                        return;
                    }
                }
                    this.Tag = txtBx_Login.Text;
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
            return MARS_Expert.Manager.XMLManager.XMLExpert.Login(txtBx_Login.Text, txtBx_Password.Text);
        }

        /// <summary>
        /// Sete the labels colors to Black.
        /// </summary>
        public void InitializeLabelColors()
        {
            lbl_Login.ForeColor = Color.Black;
            lbl_Password.ForeColor = Color.Black;
        }

        List<Image> lstCloseBtnImgs;
        List<Image> lstMinimizeBtnImgs;
        List<Image> lstBtnImgs;
        private void frm_Login_Load(object sender, EventArgs e)
        {
            lstCloseBtnImgs = new List<Image>();
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close.png"));
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close-Mouse hover.png"));
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close-Mouse click.png"));

            lstMinimizeBtnImgs = new List<Image>();
            lstMinimizeBtnImgs.Add(Image.FromFile("images\\Minimize.png"));
            lstMinimizeBtnImgs.Add(Image.FromFile("images\\Minimize-Mouse hover.png"));
            lstMinimizeBtnImgs.Add(Image.FromFile("images\\Minimize-Mouse click.png"));

            
            #region Load buttons design
            //
            //buttons button
            //
            lstBtnImgs = new List<Image>();
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton.png"));
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton-hover.png"));
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton-click.png"));

            #endregion
            //btn_Validate_Click(this, e);
        }

        #region Exit button design and behaviour

        private void btn_CloseForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_CloseForm_MouseEnter(object sender, EventArgs e)
        {
            btn_CloseForm.Image = lstCloseBtnImgs[1];
        }

        private void btn_CloseForm_MouseLeave(object sender, EventArgs e)
        {
            btn_CloseForm.Image = lstCloseBtnImgs[0];
        }

        private void btn_CloseForm_MouseDown(object sender, MouseEventArgs e)
        {
            btn_CloseForm.Image = lstCloseBtnImgs[2];
        }

        private void btn_CloseForm_MouseUp(object sender, MouseEventArgs e)
        {
            btn_CloseForm.Image = lstCloseBtnImgs[0];
        }

        #endregion

        #region Minimize button design and behaviour

        private void btn_MinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_MinimizeForm_MouseEnter(object sender, EventArgs e)
        {
            btn_MinimizeForm.Image = lstMinimizeBtnImgs[1];
        }

        private void btn_MinimizeForm_MouseLeave(object sender, EventArgs e)
        {
            btn_MinimizeForm.Image = lstMinimizeBtnImgs[0];
        }

        private void btn_MinimizeForm_MouseDown(object sender, MouseEventArgs e)
        {
            btn_MinimizeForm.Image = lstMinimizeBtnImgs[2];
        }

        private void btn_MinimizeForm_MouseUp(object sender, MouseEventArgs e)
        {
            btn_MinimizeForm.Image = lstMinimizeBtnImgs[0];
        }

        #endregion

        #region Buttons design and behaviour

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            if (((Button)sender).Name == "btn_OK")
            {
                btn_OK.Image = lstBtnImgs[1];
            }
            else
                btn_Cancel.Image = lstBtnImgs[1];
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            if (((Button)sender).Name == "btn_OK")
                btn_OK.Image = lstBtnImgs[0];
            else
                btn_Cancel.Image = lstBtnImgs[0];
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (((Button)sender).Name == "btn_OK")
                btn_OK.Image = lstBtnImgs[2];
            else
                btn_Cancel.Image = lstBtnImgs[2];
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (((Button)sender).Name == "btn_OK")
                btn_OK.Image = lstBtnImgs[0];
            else
                btn_Cancel.Image = lstBtnImgs[0];
        }

        #endregion

        private void pnl_Top_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Helper.ReleaseCapture();
                Helper.SendMessage(Handle, Helper.WM_NCLBUTTONDOWN, Helper.HT_CAPTION, 0);
            }
        }
    }
}
