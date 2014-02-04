using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARS_Expert.Manager;
using MARS_Expert.ResourceManager;
using MARS_Expert.Manager.XMLResourceManager;

namespace MARS_Expert
{
    /// <summary>
    /// Used to select an existing Failure or Failure type from the data base.
    /// </summary>
    public partial class frm_ListOf : Form
    {
        Subject Sbjct;
        TypePanne Typepanne;
        public frm_ListOf(Subject sbjct)
        {
            InitializeComponent();
            this.Sbjct=sbjct;
        }

        public frm_ListOf(Subject sbjct, TypePanne type)
        {
            InitializeComponent();
            this.Sbjct = sbjct;
            this.Typepanne = type;
        }

        List<Image> lstCloseBtnImgs;
        List<Image> lstBtnImgs;
        private void frm_ListOf_Load(object sender, EventArgs e)
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


            if (Sbjct == Subject.FailureTypeSbjct)
            {
                foreach (TypePanne type in XMLFailureType.GetAllFailureTypes())
                {
                    lstBx.Items.Add(type);
                }
            }
            else
                if (Sbjct == Subject.FailureSbjct)
                {
                    foreach (Panne panne in XMLFailure.GetAllFailuresByType(this.Typepanne.getId()))
                        lstBx.Items.Add(panne);
                }
                else
                    if (Sbjct == Subject.ProcedureSbjct)
                    {
                        foreach (Procedure proc in XMLProcedure.GetAllProcedures())
                            lstBx.Items.Add(proc);
                    }

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (lstBx.SelectedIndices.Count > 0)
            {
                //Send back the selected data.
                this.Tag = lstBx.SelectedItem;
                Close();
            }
            else
            {
                DialogResult dr = MessageBox.Show(this, "Vous n'avez pas sélectionné un element! Voulez vous continuer quand même?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
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

        private void lstBx_DoubleClick(object sender, EventArgs e)
        {
            if (lstBx.SelectedIndices.Count > 0)
            {
                //Send back the selected data.
                this.Tag = lstBx.SelectedItem;
                Close();
            }
            else
            {
                DialogResult dr = MessageBox.Show(this, "Vous n'avez pas sélectionné un element! Voulez vous continuer quand même?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Close();
                }
            }
        }
    }
}
