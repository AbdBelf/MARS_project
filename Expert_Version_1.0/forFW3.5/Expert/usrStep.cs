using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARS_Expert.ResourceManager;
using MARS_Expert.Manager;
using System.IO;
using Microsoft.DirectX;

namespace MARS_Expert
{
    public partial class usrStep : UserControl
    {
        public RenderForm pParent { set; get; }
        public usrProcedure Brother { set; get; }
        Etape Step;
        Procedure Procedure;
        Operation _Operation;
        Etape prevStep;

        public usrStep(RenderForm parent,usrProcedure brother,Etape prevStep, Etape step,Procedure procedure,Operation operation)
        {
            InitializeComponent();

            pParent = parent;
            Brother = brother;
            
            this.Step = step;
            this.Procedure = procedure;
            this._Operation = operation;
            
            
            this.prevStep = prevStep;
        }
       
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (Step.getObjectList() != null && Step.getObjectList().Count > 0)
            {
                //
                //Crete the Step's elements.
                //
                Step.setName(txtBx_Title.Text);
                Step.setDescription(txtBx_Description.Text);
                Step.setObjectList(RenderForm.Objects);


                if (_Operation == Operation.AddNew)
                {
                    //Add the created step to the steps list of the procedure.
                    
                    this.Brother.procedure.addEtape(Step);
                    
                    //Fill the list view with the step's data.
                    ListViewItem item = new ListViewItem(new string[] { Step.getId().ToString(), Step.getName(), Step.getDescription() });
                    this.Brother.lstVew_Steps.Items.Add(item);
                }
                else
                {
                    //Get the selected item (step) from the list.
                    int selected = Brother.lstVew_Steps.SelectedIndices[0];
                    ListViewItem item = Brother.lstVew_Steps.Items[selected];

                    //Add the created step to the steps list of the procedure.

                    //Remove the old version of step.
                    this.Brother.procedure.steps.RemoveAt(selected);

                    //Insert the new version at the same place.
                    this.Brother.procedure.steps.Insert(selected, Step);

                    //Fill the list view with the step's data.
                    item = new ListViewItem(new string[] { Step.getId().ToString(), Step.getName(), Step.getDescription() });

                    //Remove the old version of step.
                    Brother.lstVew_Steps.Items.RemoveAt(selected);
                    //Insert the new version at the same place.
                    Brother.lstVew_Steps.Items.Insert(selected, item);
                }

                Dispose();
            }
            else
            {
                MessageBox.Show(this, "L'étape doit avoir au minimum un objet.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btn_AddObject_Click(object sender, EventArgs e)
        {
            frm_Add3dObject frm = null;
            //Fill up the step info cause they will be used by the Object3d Form.
            this.Step.setName(txtBx_Title.Text);
            this.Step.setDescription(txtBx_Description.Text);
            
            //If the step has already some 3d models we pass the number of the last one so the new model will take its place after it.
            if (this.Step.getObjectList() != null && this.Step.getObjectList().Count > 0)
                frm = new frm_Add3dObject(this, this.Step, this.Step.getObjectList()[this.Step.getObjectList().Count - 1]);
            else
                frm = new frm_Add3dObject(this, this.Step, null);
            
            //MessageBox.Show("Step.Name: "+Step.getName(),"frm_AddStep.btn_AddObject");
            
            frm.ShowDialog();



            if (frm.Tag != null)
            {
                lstVw_3dObjects.Items.Clear();
                RenderForm.Objects.Clear();
                this.Step.setObjectList(new List<Object3d>());
                RenderForm.Objects = (List<Object3d>)frm.Tag;

                //MessageBox.Show("Render.Objects.count: " + RenderForm.Objects.Count, "frm_AddStep.btn_AddObject");
                foreach (Object3d obj in RenderForm.Objects)
                {
                    //MessageBox.Show("frm_AddStep.btn_AddObject.Obj.name:\n" + obj.getName().ToString());
                    //MessageBox.Show("REnder.Objects.count: " + RenderForm.Objects.Count, "frm_AddStep.btn_AddObject");
                    //this.Step.addObject3d(obj);
                    //Fill the list view with the RenderForm.Objects data.
                    ListViewItem item = new ListViewItem(new string[] { obj.getId().ToString(),obj.getNb().ToString(), Path.GetFileNameWithoutExtension(obj.getName()), obj.getObjType(), 
                    VectorToString(obj.getPosition(),false), VectorToString(obj.getRotation(),true), VectorToString(obj.getScale(),false) });
                    lstVw_3dObjects.Items.Add(item);

                    this.Step.addObject3d(obj);

                }
            }
            //else
            //    MessageBox.Show("frm_AddStep.btn_AddObject: " + "Tag is null");
        }

        private void btn_RemoveObject_Click(object sender, EventArgs e)
        {
            if (lstVw_3dObjects.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show(this, "Etes-vous sur de supprimer cet objet?", "Confirmez la suppression", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    int selected = lstVw_3dObjects.SelectedIndices[0];
                    //MessageBox.Show("This.step.ObjectCount: " + Step.getObjectList().Count, "frm_AddStep.btn_RemoveObject");
                    //Remove the selected model.
                    RenderForm.Objects.RemoveAt(selected);

                    //MessageBox.Show("This.step.ObjectCount: " + Step.getObjectList().Count,"frm_AddStep.btn_RemoveObject");
                    //Remove the selected model.
                    this.Step.removeObjects3d(selected);

                    //Remove the selected model.
                    lstVw_3dObjects.Items.RemoveAt(selected);
                }
            }
            else
                MessageBox.Show(this, "Sélectionner un objet SVP.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        List<Image> lstBtnImgs;
        List<Image> lstAddModelsBtnImgs;
        List<Image> lstRemoveModelBtnImgs;
        private void frm_AddStep_Load(object sender, EventArgs e)
        {
            #region Load the data
            lstVw_3dObjects.Width++;
            //Set the name of the procedure to whom this Step belongs.
            if (this.Procedure != null)
                lbl_ProcedureName.Text = "Procedure: " + this.Procedure.getName();
            else
                MessageBox.Show("Procedure Null", "frm_AddStep.Constructor");

            if (this._Operation == Operation.AddNew)
            {
                this.Step = new Etape();

                //Since the automatic configuration of the Id depends on the etape.xml file
                //and the creation of multiple steps doesn't use the etape.xml file for each time
                //so we set our new Id depending on the previous step's ID which makes our new Id = ID+1.
                if (prevStep != null)
                    this.Step.setId(prevStep.getId() + 1);

                //Reinitialise the list of 3d RenderForm.Objects
                RenderForm.Objects = new List<Object3d>();
            }
            else//If this form is used to update a step, we load its data to the text fields first.
            {
                if (Step != null)
                {
                    txtBx_Title.Text = Step.getName();
                    txtBx_Description.Text = Step.getDescription();
                    RenderForm.Objects = Step.getObjectList();

                    //----------------------------------
                    if (this.Step.getObjectList() != null)
                    {
                        RenderForm.Objects = new List<Object3d>();
                        foreach (Object3d obj in this.Step.getObjectList())
                            RenderForm.Objects.Add(obj);
                    }

                    //MessageBox.Show("Step.Objects.count: " + Step.getObjectList().Count.ToString(),"frm_AddStep.FormLoad");
                    if (RenderForm.Objects != null && RenderForm.Objects.Count > 0)
                    {
                        foreach (Object3d obj in RenderForm.Objects)
                        {
                            //MessageBox.Show("obj.path: " + obj.getPath() + ". name: " + obj.getName() + ". type: " + obj.getObjType(), "frm_AddStep.RenderForm");
                            obj.setDevice(RenderForm.m_Device);
                            try
                            {
                                obj.LoadMesh();
                            }
                            catch (FileNotFoundException)
                            {
                                string str = "Le modèle " + obj.getName() + " n'existe pas dans le chemain " + obj.getPath();
                                MessageBox.Show(this, str, "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //Fill the list view with the RenderForm.Objects data.
                            //System.Windows.Forms.MessageBox.Show("obj.Id: " + obj.getId() + "\nPosX: " + obj.getPosition().X + "\nPosY: " + obj.getPosition().Y +
                            //    "\nPosZ: " + obj.getPosition().Z,"frm_AddStep.FromLoad");

                            ListViewItem item = new ListViewItem(new string[] { obj.getId().ToString(),obj.getNb().ToString(),Path.GetFileNameWithoutExtension(obj.getName()),
                                obj.getObjType(), VectorToString(obj.getPosition(),false), VectorToString(obj.getRotation(),true), VectorToString(obj.getScale(),false) });
                            lstVw_3dObjects.Items.Add(item);
                        }
                    }

                }
            }
            txtBx_Id.Text = this.Step.getId().ToString();
            #endregion

            #region Load buttons design
            //
            //buttons video button
            //
            lstBtnImgs = new List<Image>();
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton.png"));
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton-hover.png"));
            lstBtnImgs.Add(Image.FromFile("images\\SmallButton-click.png"));

            #endregion

            //
            //Add Step button
            //
            lstAddModelsBtnImgs = new List<Image>();
            lstAddModelsBtnImgs.Add(Image.FromFile("images\\UpdateButton.png"));
            lstAddModelsBtnImgs.Add(Image.FromFile("images\\UpdateButton-hover.png"));
            lstAddModelsBtnImgs.Add(Image.FromFile("images\\UpdateButton-click.png"));
            //
            //Remove Step button
            //
            lstRemoveModelBtnImgs = new List<Image>();
            lstRemoveModelBtnImgs.Add(Image.FromFile("images\\RemoveButton.png"));
            lstRemoveModelBtnImgs.Add(Image.FromFile("images\\RemoveButton-hover.png"));
            lstRemoveModelBtnImgs.Add(Image.FromFile("images\\RemoveButton-click.png"));
        }

        public string VectorToString(Vector3 vector, bool rotation)
        {
            string ret = "";

            Vector3 vect = (Vector3)vector;
            if (rotation)
            {
                vect.X = Helper.RadiansToDegrees(vect.X);
                vect.Y = Helper.RadiansToDegrees(vect.Y);
                vect.Z = Helper.RadiansToDegrees(vect.Z);
            }

            ret += "(" + Helper.GetFourDigitsNumber(vect.X.ToString()) + "," + Helper.GetFourDigitsNumber(vect.Y.ToString()) + "," +
                Helper.GetFourDigitsNumber(vect.Z.ToString()) + ")";
            //MessageBox.Show(ret);
            return ret;
        }

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

        #region AddModel button design and behaviour

        private void btn_AddModel_MouseEnter(object sender, EventArgs e)
        {
            btn_AddObject.Image = lstAddModelsBtnImgs[1];
        }

        private void btn_AddModel_MouseLeave(object sender, EventArgs e)
        {
            btn_AddObject.Image = lstAddModelsBtnImgs[0];
        }

        private void btn_AddModel_MouseDown(object sender, MouseEventArgs e)
        {
            btn_AddObject.Image = lstAddModelsBtnImgs[2];
        }

        private void btn_AddModel_MouseUp(object sender, MouseEventArgs e)
        {
            btn_AddObject.Image = lstAddModelsBtnImgs[0];
        }

        #endregion

        #region RemoveModel button design and behaviour

        private void btn_RemoveModel_MouseEnter(object sender, EventArgs e)
        {
            btn_RemoveObject.Image = lstRemoveModelBtnImgs[1];
        }

        private void btn_RemoveModel_MouseLeave(object sender, EventArgs e)
        {
            btn_RemoveObject.Image = lstRemoveModelBtnImgs[0];
        }

        private void btn_RemoveModel_MouseDown(object sender, MouseEventArgs e)
        {
            btn_RemoveObject.Image = lstRemoveModelBtnImgs[2];
        }

        private void btn_RemoveModel_MouseUp(object sender, MouseEventArgs e)
        {
            btn_RemoveObject.Image = lstRemoveModelBtnImgs[0];
        }

        #endregion

        private void lstVw_3dObjects_DoubleClick(object sender, EventArgs e)
        {
            frm_Add3dObject frm = null;
            //Fill up the step info cause they will be used by the Object3d Form.
            this.Step.setName(txtBx_Title.Text);
            this.Step.setDescription(txtBx_Description.Text);

            //If the step has already some 3d models we pass the number of the last one so the new model will take its place after it.
            if (this.Step.getObjectList() != null && this.Step.getObjectList().Count > 0)
                frm = new frm_Add3dObject(this, this.Step, this.Step.getObjectList()[this.Step.getObjectList().Count - 1]);
            else
                frm = new frm_Add3dObject(this, this.Step, null);

            frm.ShowDialog();

            if (frm.Tag != null)
            {
                lstVw_3dObjects.Items.Clear();
                RenderForm.Objects.Clear();
                this.Step.setObjectList(new List<Object3d>());
                RenderForm.Objects = (List<Object3d>)frm.Tag;

                //MessageBox.Show("Render.Objects.count: " + RenderForm.Objects.Count, "frm_AddStep.btn_AddObject");
                foreach (Object3d obj in RenderForm.Objects)
                {
                    //Fill the list view with the RenderForm.Objects data.
                    ListViewItem item = new ListViewItem(new string[] { obj.getId().ToString(),obj.getNb().ToString(), Path.GetFileNameWithoutExtension(obj.getName()), obj.getObjType(), 
                    VectorToString(obj.getPosition(),false), VectorToString(obj.getRotation(),true), VectorToString(obj.getScale(),false) });
                    lstVw_3dObjects.Items.Add(item);

                    this.Step.addObject3d(obj);

                }
            }
        }
    }
}
