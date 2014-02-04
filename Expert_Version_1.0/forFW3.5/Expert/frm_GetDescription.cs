using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARS_Expert.Manager;

namespace MARS_Expert
{
    public partial class frm_GetDescription : Form
    {
        public frm_GetDescription(string str, string oldDesc)
        {
            InitializeComponent();
            this.Text = str;
            label1.Text = "Saisir la description de " + str;
            txtBx_Description.Text = oldDesc;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Tag = txtBx_Description.Text;
            Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        List<Image> lstCloseBtnImgs;
        List<Image> lstBtnImgs;
        private void frm_GetDescription_Load(object sender, EventArgs e)
        {
            lstCloseBtnImgs = new List<Image>();
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close.png"));
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close-Mouse hover.png"));
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close-Mouse click.png"));

            #region Load buttons design
            //
            //buttons button
            //
            lstBtnImgs = new List<Image>();
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton.png"));
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton-hover.png"));
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton-click.png"));

            #endregion
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
