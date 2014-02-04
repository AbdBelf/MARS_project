using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARS_Expert.ResourceManager;
using MARS_Expert.Manager;
namespace MARS_Expert
{
    public partial class frm_AddProcedure : Form
    {
        TypePanne failureType;
        Panne Failure;
        Procedure procedure;
        
        Operation _Operation;
        //The description of the Failure(panne) and the Failure type(type de panne) respectivly.
        string FailureDesc;
        string FailureTypeDesc;

        List<TextBox> Fields = new List<TextBox>();
        List<Label> FieldLabels = new List<Label>();

        Color emptyFieldColor = Color.Orange;
        Color invalidFieldColor = Color.Red;

        //Indicated if the user has deleted any steps.
        bool StepsRemoved = false;

        public frm_AddProcedure(TypePanne type, Panne failure, Procedure procedure, Operation operation)
        {
            InitializeComponent();

            //Initialize the attributes.
            failureType = type;
            Failure = failure;
            this.procedure = procedure;
            this.StepsRemoved = false;
            this._Operation = operation;

            //Use the GUI to add a new procedure.
            if (this._Operation == Operation.AddNew)
            {
                if (this.procedure == null)
                {
                    this.procedure = new Procedure();
                    //Create the list of steps for the current procedure.
                    this.procedure.steps = new List<Etape>();
                }
            }
            //Use the GUI to update a procedure.
            else
            {
                if (type != null)
                {
                    //txtBx_FailureType.ReadOnly = true;
                    //txtBx_FailureType.Enabled = false;
                    txtBx_FailureType.Text = failureType.getName();
                    FailureTypeDesc = type.getDescription();
                }
                else
                {
                    MessageBox.Show("Type est null", "frm_AddProcedure.Constructor");
                    throw new ArgumentNullException();
                }
                if (failure != null)
                {
                    //txtBx_Failure.ReadOnly = true;
                    //txtBx_Failure.Enabled = false;
                    txtBx_Failure.Text = failure.getName();
                    FailureDesc = failure.getDescription();
                }
                else
                {
                    MessageBox.Show("Panne est null", "frm_AddProcedure.Constructor");
                    throw new ArgumentNullException();
                }

                if (this.procedure != null)
                {
                    txtBx_Title.Text = this.procedure.getName();
                    txtBx_Description.Text = this.procedure.getDescription();


                    if (this.procedure.steps != null && this.procedure.steps.Count > 0)
                    {
                        foreach (Etape step in this.procedure.getEtapes())
                        {
                            //Fill the list view with the step's data.
                            ListViewItem item = new ListViewItem(new string[] { step.getId().ToString(), step.getName(), step.getDescription() });
                            lstVew_Steps.Items.Add(item);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Procedure est null", "frm_AddProcedure.Constructor");
                    throw new ArgumentNullException();
                }
            }
            //Set the value of the Id textBox.
            txtBx_Id.Text = this.procedure.getId().ToString();

            #region Load the form text fields to Fields list.
            Fields.Add(txtBx_Title);
            if (failureType == null)
                Fields.Add(txtBx_FailureType);
            if (Failure == null)
                Fields.Add(txtBx_Failure);
            #endregion

            #region Load the form labels to FieldLabels list.
            FieldLabels.Add(lbl_Title);
            if (failureType == null)
                FieldLabels.Add(lbl_FailureType);
            if (Failure == null)
                FieldLabels.Add(lbl_Failure);
            #endregion
        }

        private void frm_AddProcedure_Load(object sender, EventArgs e)
        {
        }

        private void btn_AddStep_Click(object sender, EventArgs e)
        {
            this.procedure.setName(txtBx_Title.Text);
            this.procedure.setDescription(txtBx_Description.Text);
            
            //steps[steps.Count - 1] is the previous step of the one which is about to be created.
            //The last step is previous of the one to create now.
            Etape prevStep;
            if (this.procedure.steps != null && this.procedure.steps.Count > 0)
                prevStep = this.procedure.steps[this.procedure.steps.Count - 1];
            else
                prevStep = null;

            frm_AddStep frm = new frm_AddStep(prevStep, null, this.procedure,Operation.AddNew);
            frm.ShowDialog();
            //Add the created step to the steps list of the procedure.
            if (frm.Tag != null)
            {
                Etape step = (Etape)frm.Tag;
                this.procedure.addEtape(step);
                //Fill the list view with the step's data.
                ListViewItem item = new ListViewItem(new string[] { step.getId().ToString(), step.getName(), step.getDescription() });
                lstVew_Steps.Items.Add(item);
            }
        }

        private void btn_ModifySetp_Click(object sender, EventArgs e)
        {
            if (lstVew_Steps.SelectedIndices.Count > 0)
            {
                //Set the procedure data if its not already set.
                this.procedure.setName(txtBx_Title.Text);
                this.procedure.setDescription(txtBx_Description.Text);

                //Get the selected item (step) from the list.
                int selected = lstVew_Steps.SelectedIndices[0];
                ListViewItem item = lstVew_Steps.Items[selected];
                
                //Etape step = new Etape(Convert.ToInt32(item.SubItems[0].Text), item.SubItems[1].Text, item.SubItems[2].Text, this.procedure);
                Etape step = null;
                
                //Gets the id of the selected Step.
                int id = Convert.ToInt32(item.SubItems[0].Text);
                //MessageBox.Show("SelectedStepId: " + id, "frm_AddProcedure.btn_ModifyStep");
                //In the list of the procedure steps we look for the step with the previous Id.
                foreach (Etape stp in this.procedure.steps)
                {
                    if (stp.getId() == id)
                    {
                        step = stp;
                        break;
                    }
                }

                //We send the slected step to the update form.
                frm_AddStep frm = new frm_AddStep(null, step, this.procedure,Operation.Update);
                frm.ShowDialog();

                //Add the created step to the steps list of the procedure.
                if (frm.Tag != null)
                {
                    step = (Etape)frm.Tag;
                    //Remove the old version of step.
                    this.procedure.steps.RemoveAt(selected);

                    //Insert the new version at the same place.
                    this.procedure.steps.Insert(selected, step);

                    //Fill the list view with the step's data.
                    item = new ListViewItem(new string[] { step.getId().ToString(), step.getName(), step.getDescription() });

                    //Remove the old version of step.
                    lstVew_Steps.Items.RemoveAt(selected);
                    //Insert the new version at the same place.
                    lstVew_Steps.Items.Insert(selected, item);
                }

            }
            else
                MessageBox.Show(this, "Sélectionner une étape SVP.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_RemoveStep_Click(object sender, EventArgs e)
        {
            if (lstVew_Steps.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show(this, "Etes-vous sur de supprimer cette étape?", "Confirmez la suppression", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    int selected = lstVew_Steps.SelectedIndices[0];

                    //Remove the selected step.
                    this.procedure.steps.RemoveAt(selected);

                    //Remove the selected step.
                    lstVew_Steps.Items.RemoveAt(selected);

                    //A step has been deleted.
                    StepsRemoved = true;
                }
            }
            else
                MessageBox.Show(this, "Sélectionner une étape SVP.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btn_Up_Click(object sender, EventArgs e)
        {
            if (lstVew_Steps.SelectedIndices.Count > 0)
            {
                int selected = lstVew_Steps.SelectedIndices[0];
                if (selected > 0)
                {
                    //Save the upper step to move down later.
                    ListViewItem item = (ListViewItem)lstVew_Steps.Items[selected - 1].Clone();

                    //Keep the order of Ids the same.
                    item.Text = lstVew_Steps.Items[selected].Text;
                    lstVew_Steps.Items[selected].Text = lstVew_Steps.Items[selected - 1].Text;

                    //Remove the item we saved
                    lstVew_Steps.Items.RemoveAt(selected - 1);

                    //Move the step up.
                    lstVew_Steps.Items.Insert(selected, item);


                    //Save the upper step to move down later.
                    //Make a copy of the upper element.
                    Etape etp = new Etape();
                    etp.setId(this.procedure.steps[selected - 1].getId());
                    etp.setName(this.procedure.steps[selected - 1].getName());
                    etp.setDescription(this.procedure.steps[selected - 1].getDescription());
                    etp.setprocedure(this.procedure.steps[selected - 1].getprocedure());
                    foreach (Object3d obj in this.procedure.steps[selected - 1].getObjectList()) 
                    {
                        etp.addObject3d(obj);
                    }

                    //Keep the order of Ids the same.
                    etp.setId(this.procedure.steps[selected].getId());
                    this.procedure.steps[selected].setId(this.procedure.steps[selected - 1].getId());

                    //Remove the item we saved
                    this.procedure.steps.RemoveAt(selected - 1);

                    //Move the step up.
                    this.procedure.steps.Insert(selected, etp);

                    //Use this variable so that the app delete the old steps and reinsert them in the specified order.
                    StepsRemoved = true;
                    //MessageBox.Show("selected-1: " + this.procedure.steps[selected - 1].getId() + "  selected: " + this.procedure.steps[selected].getId());
                }
            }
            else
                MessageBox.Show(this, "Sélectionner une étape SVP.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_Down_Click(object sender, EventArgs e)
        {
            if (lstVew_Steps.SelectedIndices.Count > 0)
            {
                int selected = lstVew_Steps.SelectedIndices[0];
                if (selected < lstVew_Steps.Items.Count-1)
                {
                    //Save the lower step to move up later.
                    ListViewItem item = (ListViewItem)lstVew_Steps.Items[selected + 1].Clone();

                    //Keep the order of Ids the same.
                    item.Text = lstVew_Steps.Items[selected].Text;
                    lstVew_Steps.Items[selected].Text = lstVew_Steps.Items[selected + 1].Text;

                    //Remove the item we saved
                    lstVew_Steps.Items.RemoveAt(selected + 1);

                    //Move the step down.
                    lstVew_Steps.Items.Insert(selected, item);


                    //Save the lower step to move down later.
                    //Make a copy of the upper element.
                    Etape etp = new Etape();
                    etp.setId(this.procedure.steps[selected + 1].getId());
                    etp.setName(this.procedure.steps[selected + 1].getName());
                    etp.setDescription(this.procedure.steps[selected + 1].getDescription());
                    etp.setprocedure(this.procedure.steps[selected + 1].getprocedure());
                    foreach (Object3d obj in this.procedure.steps[selected + 1].getObjectList())
                    {
                        etp.addObject3d(obj);
                    }

                    //Keep the order of Ids the same.
                    etp.setId(this.procedure.steps[selected].getId());
                    this.procedure.steps[selected].setId(this.procedure.steps[selected + 1].getId());

                    //Remove the item we saved
                    this.procedure.steps.RemoveAt(selected + 1);

                    //Move the step up.
                    this.procedure.steps.Insert(selected, etp);

                    //Use this variable so that the app delete the old steps and reinsert them in the specified order.
                    StepsRemoved = true;
                    //MessageBox.Show("selected-1: " + this.procedure.steps[selected - 1].getId() + "  selected: " + this.procedure.steps[selected].getId());
                }
            }
            else
                MessageBox.Show(this, "Sélectionner une étape SVP.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_Save_DataBase_Click(object sender, EventArgs e)
        {
            //try
            {
                //
                //Remember that this button could be used to add new elements to the DB as it could be used to update them.
                //Put in mind to treat this issue.Abdelalim
                //

                //Check for emty fields.
                if (Check_Fields())
                {
                    if (this.procedure.steps.Count > 0)
                    {
                        //Set the procedure final values
                        this.procedure.setName(txtBx_Title.Text);
                        this.procedure.setDescription(txtBx_Description.Text);

                        //Set the failuretype final values
                        if (this.failureType == null)
                            this.failureType = new TypePanne();
                        this.failureType.setName(txtBx_FailureType.Text);
                        this.failureType.setDescription(FailureTypeDesc);

                        //Set the failure final values
                        if (this.Failure == null)
                            this.Failure = new Panne();
                        this.Failure.setName(txtBx_Failure.Text);
                        this.Failure.setDescription(FailureDesc);
                        this.failureType.addPanne(Failure);

                        //Add new proecedure.
                        if (this._Operation == Operation.AddNew)
                        {
                            //If no existing Failure type is chosen create new one.
                            if (this.failureType == null)
                            {
                                //Get the Id of the new failure type (type de panne).
                                this.failureType = new TypePanne();
                                this.failureType.setName(txtBx_FailureType.Text);
                                this.failureType.setDescription(FailureTypeDesc);
                            }
                            //If no existing Failure is chosen create new one.
                            if (this.Failure == null)
                            {
                                this.Failure = new Panne();
                                this.Failure.setName(txtBx_Failure.Text);
                                this.Failure.setDescription(FailureDesc);

                                //Add _Failure to the list of failures of the FailureType object.
                                this.failureType.addPanne(this.Failure);
                            }

                            //Addthe procedure to the Failure's list of procedures.
                            this.Failure.addProcedure(procedure);

                            //Save the failure type (le type de panne).
                            if (!failureType.Exists())//If a new type is created we save it to the data base.
                            {
                                failureType.Add();
                                Failure.Add();// Save the failure to the DB since it belongs to a newly created FailureType.
                            }
                            else//If the FailureType exists we check for the the failure(panne).
                            //Save the failure (la panne).
                            {
                                failureType.Update(failureType.getId());
                                if (!Failure.Exists())//If a new type is created we save it to the data base.
                                {
                                    Failure.Add();
                                }
                                else
                                    Failure.Update(Failure.getId());
                            }
                            //
                            //Saving the procedure.
                            //
                            procedure.AddToDB();
                            //MessageBox.Show("Proc.Steps.count: " + this.procedure.getEtapes().Count, "frm_AddProcedure.btn_Save");
                            //Save the steps.
                            foreach (Etape step in this.procedure.getEtapes())
                            {
                                //Associates each step with the current procedure.
                                //procedure.addEtape(step);
                                step.setprocedure(this.procedure);
                                //MessageBox.Show("Etape.Proc: " + step.getprocedure().getId(), "frm_AddProcedure.btn_Save");
                                //Save the step and its objects to the file etape.xml.
                                step.Add();
                            }
                        }
                        else//Update an existing procedure.
                        {
                            //Addthe procedure to the Failure's list of procedures.
                            this.Failure.addProcedure(procedure);
                            //MessageBox.Show("Falure.name: " + Failure.getId(), "frm_AddPrecedure.btn_Save");
                            //
                            //Saving the procedure.
                            //
                            procedure.Update(this.procedure.getId());

                            //Save the steps.
                            if (StepsRemoved)
                            {
                                //Remove all the steps of the current procedure from the data base.
                                //MessageBox.Show("Proc.steps.count: " + this.procedure.getEtapes().Count, "frm_AddProcedure.btn_Save");
                                Etape.RemoveByProcedure(this.procedure.getId());
                            }

                            //In case we allow the user to update the failure and its type from this GUI.
                            //Update the FailureType.
                            failureType.Update(failureType.getId());
                            //Update the failure (la panne).
                            Failure.Update(Failure.getId());

                            //MessageBox.Show("Steps.Count: "+this.procedure.getEtapes().Count, "frm_AddProcedure.btn_Save");
                            foreach (Etape step in this.procedure.steps)
                            {
                                //Associates each step with the current procedure.
                                step.setprocedure(this.procedure);
                                //If the current step exists in the DB means we're about to update it.
                                if (step.Exists())
                                {
                                    step.Update(step.getId());
                                }
                                else
                                {
                                    //Save the step and its objects to the file etape.xml.
                                    step.Add();
                                    //MessageBox.Show("Step "+step.getName()+" added","frm_AddProcedure.btn_Save");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "La procedure doit avoir au minimum une étape.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Remplir les champs importants d'abord.", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show(this, "Opération terminée avec succès.", "Message d'information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Tag = this.procedure;
                this.Close();
            }
            //catch (Exception x) { MessageBox.Show(x.ToString()); }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Write the description of the failure type (type de panne).
        private void txtBx_FailureType_Validated(object sender, EventArgs e)
        {
            if (FailureTypeDesc == null || FailureTypeDesc == "")
            {
                frm_GetDescription frm = new frm_GetDescription("Failure Type", "");
                frm.ShowDialog();
                if (frm.Tag != null)
                {
                    FailureTypeDesc = frm.Tag.ToString();
                }
            }
        }

        //Write the description of the failure (panne).
        private void txtBx_Failure_Validated(object sender, EventArgs e)
        {
            if (FailureDesc == "" || FailureDesc == null)
            {
                frm_GetDescription frm = new frm_GetDescription("Failure", "");
                frm.ShowDialog();
                if (frm.Tag != null)
                {
                    FailureDesc = frm.Tag.ToString();
                }
            }
        }

        //Checks for the description when the text is validated and no Description is inserted.
        private void descriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Label _sender = (Label)ctxtMnuStrp_GetDescription.SourceControl;
            if (_sender.Name == "lbl_FailureType")//Get the description of the failure Type.
            {
                frm_GetDescription frm = new frm_GetDescription("Type de panne", FailureTypeDesc);
                frm.ShowDialog();
                if (frm.Tag != null)
                {
                    FailureTypeDesc = frm.Tag.ToString();
                }
            }
            else
            {
                frm_GetDescription frm = new frm_GetDescription("Panne", FailureDesc);
                frm.ShowDialog();
                if (frm.Tag != null)
                {
                    FailureDesc = frm.Tag.ToString();
                }
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
        /// Sete the labels colors to Black.
        /// </summary>
        public void InitializeLabelColors()
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                FieldLabels[i].ForeColor = Color.Black;
            }
        }

        private void ctxtMnuStrp_GetDescription_Layout(object sender, LayoutEventArgs e)
        {
            if (txtBx_FailureType.Enabled == false)
                ctxtMnuStrp_GetDescription.Enabled = false;
        }

        private void lstVew_Steps_MouseClick(object sender, MouseEventArgs e)
        {
            //Get the selected item (step) from the list.
            int selected = lstVew_Steps.SelectedIndices[0];
            ListViewItem item = lstVew_Steps.Items[selected];
            //Etape step = new Etape(Convert.ToInt32(item.SubItems[0].Text), item.SubItems[1].Text, item.SubItems[2].Text, this.procedure);
            Etape step = null;
            //Gets the id of the selected Step.
            int id = Convert.ToInt32(item.SubItems[0].Text);
            
            //In the list of the procedure steps we look for the step with the previous Id.
            foreach (Etape stp in this.procedure.steps)
            {
                if (stp.getId() == id)
                {
                    step = stp;
                    break;
                }
            }
            lstVew_3dObjects.Items.Clear();
            foreach (Object3d obj in step.getObjectList())
            {
                lstVew_3dObjects.Items.Add(obj.getName());
            }
        }

        private void lnkLbl_ExistantFailureType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //get the list of types of failure
            frm_ListOf frm = new frm_ListOf(Subject.FailureTypeSbjct);
            frm.ShowDialog();

            if (frm.Tag != null)
            {
                this.failureType = (TypePanne)frm.Tag;

                txtBx_FailureType.Text = this.failureType.getName();
                FailureTypeDesc = this.failureType.getDescription();
                try
                {
                    this.Failure.setTypePanne(this.failureType);
                }
                catch (Exception) { }
            }
        }

        private void lnkLbl_ExistantFailure_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.failureType != null)
            {
                //get the list of failures
                frm_ListOf frm = new frm_ListOf(Subject.FailureSbjct,this.failureType);
                frm.ShowDialog();

                if (frm.Tag != null)
                {
                    this.Failure = (Panne)frm.Tag;

                    txtBx_Failure.Text = this.Failure.getName();
                    FailureDesc = this.Failure.getDescription();
                    this.procedure.setPanne(this.Failure);
                    try
                    {
                        this.Failure.setTypePanne(this.failureType);
                    }
                    catch (Exception) { }
                }
            }
            else
                MessageBox.Show(this, "Sélectionnez un type de panne d'abord", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frm_AddProcedure_Move(object sender, EventArgs e)
        {
            this.Text = Cursor.Position.ToString();
        }
    }
}
