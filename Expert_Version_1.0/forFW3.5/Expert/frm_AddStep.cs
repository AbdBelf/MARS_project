using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARS_Expert.ResourceManager;
using Microsoft.DirectX;
using System.IO;
using MARS_Expert.Manager;

namespace MARS_Expert
{
    public partial class frm_AddStep : Form
    {
        //List<Object3d> RenderForm.Objects = new List<Object3d>();
        Etape Step;
        Procedure Procedure;
        Operation _Operation;
        Etape prevStep;
        public frm_AddStep(Etape prevStep, Etape step,Procedure procedure,Operation operation)
        {
            InitializeComponent();

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
                
                //Use the Tag object to transfere data between forms.
                this.Tag = Step;
                Close();
            }
            else
            {
                MessageBox.Show(this, "L'étape doit avoir au minimum un objet.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    VectorToString(obj.getPosition()), VectorToString(obj.getRotation()), VectorToString(obj.getScale()) });
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

        private void frm_AddStep_Load(object sender, EventArgs e)
        {
            lstVw_3dObjects.Width++;
            lstVw_3dObjects.Width--;

            //Set the name of the procedure to whom this Step belongs.
            if (this.Procedure != null)
                lbl_ProcedureName.Text = this.Procedure.getName();
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
                    //RenderForm.Objects = Step.getObjectList();

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
                            obj.LoadMesh();
                            //Fill the list view with the RenderForm.Objects data.
                            //System.Windows.Forms.MessageBox.Show("obj.Id: " + obj.getId() + "\nPosX: " + obj.getPosition().X + "\nPosY: " + obj.getPosition().Y +
                            //    "\nPosZ: " + obj.getPosition().Z,"frm_AddStep.FromLoad");

                            ListViewItem item = new ListViewItem(new string[] { obj.getId().ToString(),obj.getNb().ToString(),Path.GetFileNameWithoutExtension(obj.getName()),
                                obj.getObjType(), VectorToString(obj.getPosition()), VectorToString(obj.getRotation()), VectorToString(obj.getScale()) });
                            lstVw_3dObjects.Items.Add(item);
                        }
                    }

                }
            }
            txtBx_Id.Text = this.Step.getId().ToString();
        }

        public string VectorToString(Object vector)
        {
            string ret = "";
            if (vector.GetType() == typeof(Vector3))
            {
                Vector3 vect = (Vector3)vector;
                ret += "(" + vect.X + "," + vect.Y + "," + vect.Z +")";
            }
            else
                if (vector.GetType() == typeof(Vector3))
                {
                    Vector3 vect = (Vector3)vector;
                    ret += "(" + vect.X + "," + vect.Y + "," + vect.Z + ")";
                }
            //MessageBox.Show(ret);
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This.step.ObjectCount: " + Step.getObjectList().Count, "frm_AddStep_Button1");
        }

     
    }
}
