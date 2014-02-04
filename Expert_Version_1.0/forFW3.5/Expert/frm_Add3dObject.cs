using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using NyARToolkitCSUtils.Capture;
using NyARToolkitCSUtils;
using NyARToolkitCSUtils.Direct3d;
using jp.nyatla.nyartoolkit.cs;
using jp.nyatla.nyartoolkit.cs.core;
using jp.nyatla.nyartoolkit.cs.detector;

using MARS_Expert.WebService;
using MARS_Expert.ResourceManager;
using MARS_Expert.Manager;

namespace MARS_Expert
{
    public partial class frm_Add3dObject : Form
    {
        int transformation = 0;

        Etape Step;
        Object3d LastObj;
        int number;
        Control pParetn;
        public frm_Add3dObject(Control paretn,Etape step,Object3d lastObj)
        {
            InitializeComponent();
            this.pParetn = paretn;

            btn_DrawObject.Enabled = false;
            grpbx_Manipulation.Enabled = false;

            this.Step = step;
            this.LastObj = lastObj;
            
            //this._Operation = operation;

            //if (this._Operation == Operation.AddNew)
            {
                //MessageBox.Show("Step.Name: "+this.Step.getName(),"frm_AddP=Object.Constructor");
                if (LastObj != null)
                    number = LastObj.getNb() + 1;

                if (this.Step != null)
                {

                    lbl_StepName.Text = "Etape: "+this.Step.getName();

                    //MessageBox.Show("frm_AddStep.RenderForm.Objects.count: " + RenderForm.Objects.Count.ToString());
                    if (RenderForm.Objects != null && RenderForm.Objects.Count > 0)
                    {
                        foreach (Object3d obj in RenderForm.Objects)
                        {
                            //Fill the list box of the created objects.
                            {
                                lstbx_Objects.Items.Add(obj);
                                //obj.LoadMesh();
                                //lstbx_Objets.Items.Add(file_info);
                                tabControl1.SelectedIndex = 0;
                            }
                        }
                    }
                    //----------------------------------
                }
                else
                    MessageBox.Show("Step null","frm_AddObject.Constructor");
            }
            //else
            {
            }
        }

        List<Image> lstCloseBtnImgs;
        List<Image> lstBtnImgs;
        private void frm_Add3dObject_Load(object sender, EventArgs e)
        {
            #region Load Data
            foreach (ObjForListView obj in Manager.XMLResourceManager.XML3dObject.GetAll3dObjects())
            {
                switch (obj.Type)
                {
                    case "sign":
                        lstbx_Signs.Items.Add(obj);
                        break;
                    case "tool":
                        lstbx_Tools.Items.Add(obj);
                        break;
                    case "material":
                        lstbx_Materials.Items.Add(obj);
                        break;
                    default: break;
                }
            }
            #endregion

            #region Setup position
            int x, y;
            x = pParetn.Parent.Left + pParetn.Location.X + (pParetn.Size.Width - this.Width) / 2;
            y = pParetn.Location.Y;
            this.Location = new Point(x, y);
            #endregion

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

            if (lstbx_Objects.Items.Count > 0)
                btn_RemoveObject.Enabled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(tabControl1.SelectedIndex.ToString());
            if (tabControl1.SelectedIndex == 0)
            {
                btn_DrawObject.Enabled = false;

                if (lstbx_Objects.Items.Count > 0)
                    btn_RemoveObject.Enabled = true;
                ListBox listbox = ((ListBox)tabControl1.SelectedTab.GetChildAtPoint(new Point(0, 0)));

                if (listbox.SelectedItems.Count > 0)
                    grpbx_Manipulation.Enabled = true;
                else
                    grpbx_Manipulation.Enabled = false;
            }
            else
            {
                btn_DrawObject.Enabled = true;
                grpbx_Manipulation.Enabled = false;
            }
        }

        private void lstbx_Objets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbx_Objects.SelectedItems.Count > 0)
                grpbx_Manipulation.Enabled = true;
            else
                grpbx_Manipulation.Enabled = false;
        }

        private void rdbtn_Position_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Position.Checked)
            {
                transformation = 0;
            }
            else
                if (rdbtn_Rotation.Checked)
                {
                    transformation = 1;
                }
            if (rdbtn_Scale.Checked)
            {
                transformation = 2;
            }
            lstbx_Objects_Click(this, e);
        }

        private void btn_DrawObject_Click(object sender, EventArgs e)
        {
            ListBox listbox = ((ListBox)tabControl1.SelectedTab.GetChildAtPoint(new Point(0, 0)));

            ObjForListView model = (ObjForListView)(listbox.SelectedItem);

            if (model != null)
            {
                Vector3 scale = new Vector3(1f,1f,1f);
                switch (model.Name)
                {
                    case "droid":
                        scale = new Vector3(0.085f, 0.085f, 0.085f);
                        break;
                    case "miku":
                        scale = new Vector3(0.35f, 0.35f, 0.35f);
                        break;
                    case "tournevis":
                        scale = new Vector3(7.7f, 7.7f, 7.7f);
                        break;
                    case "flecheVerte":
                        scale = new Vector3(0.88f, 0.88f, 0.88f);
                        break;
                    case "box":
                        scale = new Vector3(20f, 20f, 20f);
                        break;
                }
                Object3d obj3d = new Object3d(Convert.ToInt32(model.Id), model.Name, model.Type,
                    new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0, 0, 0), scale, RenderForm.m_Device);
                obj3d.setNb(this.number);

                try
                {
                    obj3d.LoadMesh();
                }
                catch (FileNotFoundException)
                {
                    string str = "Le modèle " + obj3d.getName() + " n'existe pas dans le chemain " + obj3d.getPath();
                    MessageBox.Show(this, str, "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                RenderForm.Objects.Add(obj3d);
                lstbx_Objects.Items.Add(model);
                tabControl1.SelectedIndex = 0;
                if (lstbx_Objects.Items.Count > 0)
                {
                    lstbx_Objects.SelectedIndex = lstbx_Objects.Items.Count - 1;
                    lstbx_Objects_Click(null, EventArgs.Empty);
                }
                btn_RemoveObject.Enabled = true;
            }
        }

        private void btn_RemoveObject_Click(object sender, EventArgs e)
        {
            Control control = tabControl1.SelectedTab.GetChildAtPoint(new Point(0, 0));
            if (tabControl1.SelectedIndex == 0)
            {
                ListBox lstbx = (ListBox)control;
                int selectedobj = (lstbx).SelectedIndex;
                
                if (selectedobj == -1)
                    return;

                lstbx.Items.RemoveAt(selectedobj);
                RenderForm.Objects.RemoveAt(selectedobj);
            }
            if (lstbx_Objects.Items.Count == 0)
                btn_RemoveObject.Enabled = false;
            else
            {
                lstbx_Objects.SelectedIndex = lstbx_Objects.Items.Count - 1;
                lstbx_Objects_Click(this, e);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //Use the Tag object to transfere data between forms.
            //foreach (Object3d obj in RenderForm.Objects)
            //    MessageBox.Show(obj.getId().ToString());
            
            //Make a copy of the objects insted of sending the old list.
            List<Object3d> tempObj = new List<Object3d>();
            foreach(Object3d obj in RenderForm.Objects)
                tempObj.Add(obj);
            
            //MessageBox.Show("objects.count: " + RenderForm.Objects.Count);
            if (RenderForm.Objects.Count == 0)
                this.Tag = null;
            else
                this.Tag = tempObj;
            Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstbx_Objects_Click(object sender, EventArgs e)
        {
            if (lstbx_Objects.SelectedIndices.Count > 0)
            {
                int selected = lstbx_Objects.SelectedIndex;
                Object3d obj = RenderForm.Objects[selected];
                switch (transformation)
                {
                    //translation
                    case 0:
                        txtBx_X.Text = Helper.GetFourDigitsNumber(obj.getPosition().X.ToString()).ToString();
                        txtBx_Y.Text = Helper.GetFourDigitsNumber(obj.getPosition().Y.ToString()).ToString();
                        txtBx_Z.Text = Helper.GetFourDigitsNumber(obj.getPosition().Z.ToString()).ToString();

                        break;
                    //rotation
                    case 1:
                        
                        txtBx_X.Text = Helper.GetFourDigitsNumber(Helper.RadiansToDegrees(obj.getRotation().X).ToString()).ToString();
                        txtBx_Y.Text = Helper.GetFourDigitsNumber(Helper.RadiansToDegrees(obj.getRotation().Y).ToString()).ToString();
                        txtBx_Z.Text = Helper.GetFourDigitsNumber(Helper.RadiansToDegrees(obj.getRotation().Z).ToString()).ToString();

                        break;
                    //scaling
                    case 2:
                        txtBx_X.Text = Helper.GetFourDigitsNumber((obj.getScale().X).ToString()).ToString();
                        txtBx_Y.Text = Helper.GetFourDigitsNumber((obj.getScale().Y).ToString()).ToString();
                        txtBx_Z.Text = Helper.GetFourDigitsNumber((obj.getScale().Z).ToString()).ToString();

                        break;
                    default: break;
                }
            }
        }

        private void txtBx_X_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || (e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))))
                {
                    if (e.KeyChar == '-' && ((TextBox)sender).Text.Length + 1 <= 1)
                    {
                        e.Handled = false;
                    }
                    else
                        e.Handled = true;
                }
            
            if (e.KeyChar == (char)Keys.Enter && ((TextBox)sender).Text == "")
                    ((TextBox)sender).Text = "0";

            if (e.KeyChar == (char)Keys.Enter && ((TextBox)sender).Text != "")
            {
                if (((TextBox)sender).Name == "txtBx_X")
                {
                    float X = float.Parse(txtBx_X.Text);
                    ModelSetX(X);
                }
                else
                    if (((TextBox)sender).Name == "txtBx_Y")
                    {
                        float Y = float.Parse(txtBx_Y.Text);
                        ModelSetY(Y);
                    }
                    else
                    {
                        float Z = float.Parse(txtBx_Z.Text);
                        ModelSetZ(Z);
                    }

                //lstbx_Objects_Click(this, e);
            }
        }

        float step = 5.0f;
        private void btn_UpX_Click(object sender, EventArgs e)
        {
            float X = float.Parse(txtBx_X.Text);
            X += step;
            txtBx_X.Text = X.ToString();

            ModelSetX(X);
            lstbx_Objects_Click(this, e);
        }

        private void btn_DownX_Click(object sender, EventArgs e)
        {
            float X = float.Parse(txtBx_X.Text);
            X -= step;
            txtBx_X.Text = X.ToString();

            ModelSetX(X);
            lstbx_Objects_Click(this, e);
        }

        private void btn_UpY_Click(object sender, EventArgs e)
        {
            float Y = float.Parse(txtBx_Y.Text);
            Y += step;
            txtBx_Y.Text = Y.ToString();

            ModelSetY(Y);
            lstbx_Objects_Click(this, e);
        }

        private void btn_DownY_Click(object sender, EventArgs e)
        {
            float Y = float.Parse(txtBx_Y.Text);
            Y -= step;
            txtBx_Y.Text = Y.ToString();

            ModelSetY(Y);
            lstbx_Objects_Click(this, e);
        }

        private void btn_UpZ_Click(object sender, EventArgs e)
        {
            float Z = float.Parse(txtBx_Z.Text);
            Z += step;
            txtBx_Z.Text = Z.ToString();

            ModelSetZ(Z);
            lstbx_Objects_Click(this, e);
        }

        private void btn_DownZ_Click(object sender, EventArgs e)
        {
            float Z = float.Parse(txtBx_Z.Text);
            Z -= step;
            txtBx_Z.Text = Z.ToString();

            ModelSetZ(Z);
            lstbx_Objects_Click(this, e);
        }

        public void ModelSetX(float X)
        {
            Object3d obj = RenderForm.Objects[lstbx_Objects.SelectedIndex];
            switch (transformation)
            {
                //translation
                case 0:
                    Vector3 newVect = new Vector3(X, RenderForm.Objects[lstbx_Objects.SelectedIndex].getPosition().Y,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getPosition().Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setPosition(newVect);
                    break;
                //rotation
                case 1:

                    newVect = new Vector3(X, RenderForm.Objects[lstbx_Objects.SelectedIndex].getRotation().Y,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getRotation().Z);

                    Vector3 v = new Vector3(Helper.DegreesToRadians(newVect.X), newVect.Y, newVect.Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setRotation(v);
                    break;
                //scaling
                case 2:
                    if (X < 0)
                    {
                        X = 0;
                        txtBx_X.Text = "0";
                    }
                    newVect = new Vector3(X, RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Y,
                      RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    //------------------------ Y -----------------------------------------------
                    newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().X, X,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    //------------------------ Z -----------------------------------------------
                    newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().X,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Y, X);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    lstbx_Objects_Click(this, EventArgs.Empty);
                    break;
                default: break;
            }
        }

        public void ModelSetY(float Y)
        {
            Object3d obj = RenderForm.Objects[lstbx_Objects.SelectedIndex];
            switch (transformation)
            {
                //translation
                case 0:
                    Vector3 newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getPosition().X, Y,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getPosition().Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setPosition(newVect);
                    break;
                //rotation
                case 1:
                    newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getRotation().X, Y,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getRotation().Z);

                    Vector3 v = new Vector3(newVect.X, Helper.DegreesToRadians(newVect.Y), newVect.Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setRotation(v);
                    break;
                //scaling
                case 2:
                    if (Y < 0)
                    {
                        Y = 0;
                        txtBx_Y.Text = "0";
                    }
                     newVect = new Vector3(Y, RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Y,
                      RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    //------------------------ Y -----------------------------------------------
                    newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().X, Y,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    //------------------------ Z -----------------------------------------------
                    newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().X,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Y, Y);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    lstbx_Objects_Click(this, EventArgs.Empty);
                    break;
                default: break;
            }
        }

        public void ModelSetZ(float Z)
        {
            Object3d obj = RenderForm.Objects[lstbx_Objects.SelectedIndex];
            switch (transformation)
            {
                //translation
                case 0:
                    Vector3 newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getPosition().X,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getPosition().Y, Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setPosition(newVect);
                    break;
                //rotation
                case 1:
                    newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getRotation().X, RenderForm.Objects[lstbx_Objects.SelectedIndex].getRotation().Y,
                        Z);

                    Vector3 v = new Vector3(newVect.X, newVect.Y, Helper.DegreesToRadians(newVect.Z));
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setRotation(v);
                    break;
                //scaling
                case 2:
                    if (Z < 0)
                    {
                        Z = 0;
                        txtBx_Z.Text = "0";
                    }
                    newVect = new Vector3(Z, RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Y,
                      RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    //------------------------ Y -----------------------------------------------
                    newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().X, Z,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    //------------------------ Z -----------------------------------------------
                    newVect = new Vector3(RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().X,
                        RenderForm.Objects[lstbx_Objects.SelectedIndex].getScale().Y, Z);
                    RenderForm.Objects[lstbx_Objects.SelectedIndex].setScale(newVect);
                    lstbx_Objects_Click(this, EventArgs.Empty);
                    break;
                default: break;
            }
        }

        private void btn_NextImage_Click(object sender, EventArgs e)
        {
            RenderForm.CurrentImage = (RenderForm.CurrentImage + 1) % RenderForm.lstMarkerImageList.Count;
        }

        private void btn_PreviousImage_Click(object sender, EventArgs e)
        {
            if (RenderForm.CurrentImage == 0)
                RenderForm.CurrentImage = RenderForm.lstMarkerImageList.Count - 1;
            else
                RenderForm.CurrentImage = (RenderForm. CurrentImage - 1);
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

        #region Draw model design and behaviour

        private void btn_DrawObject_MouseEnter(object sender, EventArgs e)
        {
            btn_DrawObject.Image = lstBtnImgs[1];
        }

        private void btn_DrawObject_MouseLeave(object sender, EventArgs e)
        {
            btn_DrawObject.Image = lstBtnImgs[0];
        }

        private void btn_DrawObject_MouseDown(object sender, MouseEventArgs e)
        {
            btn_DrawObject.Image = lstBtnImgs[2];
        }

        private void btn_DrawObject_MouseUp(object sender, MouseEventArgs e)
        {
            btn_DrawObject.Image = lstBtnImgs[0];
        }

        #endregion

        #region Remove model design and behaviour

        private void btn_RemoveObject_MouseEnter(object sender, EventArgs e)
        {
            btn_DrawObject.Image = lstBtnImgs[1];
        }

        private void btn_RemoveObject_MouseLeave(object sender, EventArgs e)
        {
            btn_RemoveObject.Image = lstBtnImgs[0];
        }

        private void btn_RemoveObject_MouseDown(object sender, MouseEventArgs e)
        {
            btn_RemoveObject.Image = lstBtnImgs[2];
        }

        private void btn_RemoveObject_MouseUp(object sender, MouseEventArgs e)
        {
            btn_RemoveObject.Image = lstBtnImgs[0];
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || (e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))))
            {
                e.Handled = true;
            }
            else
                if(e.KeyChar ==(char)Keys.Enter)
                {
                    if (txtBx_Increment.Text != "")
                    step = float.Parse(txtBx_Increment.Text);
                }
        }

        private void txtBx_Increment_Validated(object sender, EventArgs e)
        {
            if (txtBx_Increment.Text != "")
                step = float.Parse(txtBx_Increment.Text);
        }
    }
}
