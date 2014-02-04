namespace MARS_Expert
{
    partial class frm_AddProcedure
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AddProcedure));
            this.grpBx_AddProcedure = new System.Windows.Forms.GroupBox();
            this.lnkLbl_ExistantFailure = new System.Windows.Forms.LinkLabel();
            this.lnkLbl_ExistantFailureType = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.grpBx_Steps = new System.Windows.Forms.GroupBox();
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.btn_RemoveStep = new System.Windows.Forms.Button();
            this.btn_ModifySetp = new System.Windows.Forms.Button();
            this.btn_AddStep = new System.Windows.Forms.Button();
            this.btn_Save2DataBase = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lstVew_3dObjects = new System.Windows.Forms.ListView();
            this.clmnHdr_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstVew_Steps = new System.Windows.Forms.ListView();
            this.clmnHdr_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHdr_Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtBx_Description = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBx_Failure = new System.Windows.Forms.TextBox();
            this.txtBx_FailureType = new System.Windows.Forms.TextBox();
            this.lbl_Failure = new System.Windows.Forms.Label();
            this.ctxtMnuStrp_GetDescription = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.descriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtBx_Title = new System.Windows.Forms.TextBox();
            this.txtBx_Id = new System.Windows.Forms.TextBox();
            this.lbl_FailureType = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grpBx_AddProcedure.SuspendLayout();
            this.grpBx_Steps.SuspendLayout();
            this.ctxtMnuStrp_GetDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBx_AddProcedure
            // 
            this.grpBx_AddProcedure.Controls.Add(this.lnkLbl_ExistantFailure);
            this.grpBx_AddProcedure.Controls.Add(this.lnkLbl_ExistantFailureType);
            this.grpBx_AddProcedure.Controls.Add(this.label1);
            this.grpBx_AddProcedure.Controls.Add(this.grpBx_Steps);
            this.grpBx_AddProcedure.Controls.Add(this.txtBx_Description);
            this.grpBx_AddProcedure.Controls.Add(this.label8);
            this.grpBx_AddProcedure.Controls.Add(this.txtBx_Failure);
            this.grpBx_AddProcedure.Controls.Add(this.txtBx_FailureType);
            this.grpBx_AddProcedure.Controls.Add(this.lbl_Failure);
            this.grpBx_AddProcedure.Controls.Add(this.txtBx_Title);
            this.grpBx_AddProcedure.Controls.Add(this.txtBx_Id);
            this.grpBx_AddProcedure.Controls.Add(this.lbl_FailureType);
            this.grpBx_AddProcedure.Controls.Add(this.lbl_Title);
            this.grpBx_AddProcedure.Controls.Add(this.label7);
            this.grpBx_AddProcedure.Location = new System.Drawing.Point(5, 0);
            this.grpBx_AddProcedure.Name = "grpBx_AddProcedure";
            this.grpBx_AddProcedure.Size = new System.Drawing.Size(487, 452);
            this.grpBx_AddProcedure.TabIndex = 30;
            this.grpBx_AddProcedure.TabStop = false;
            this.grpBx_AddProcedure.Text = "Ajouter une procedure de maintenance";
            // 
            // lnkLbl_ExistantFailure
            // 
            this.lnkLbl_ExistantFailure.AutoSize = true;
            this.lnkLbl_ExistantFailure.Location = new System.Drawing.Point(298, 93);
            this.lnkLbl_ExistantFailure.Name = "lnkLbl_ExistantFailure";
            this.lnkLbl_ExistantFailure.Size = new System.Drawing.Size(85, 13);
            this.lnkLbl_ExistantFailure.TabIndex = 30;
            this.lnkLbl_ExistantFailure.TabStop = true;
            this.lnkLbl_ExistantFailure.Text = "Panne existante";
            this.lnkLbl_ExistantFailure.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl_ExistantFailure_LinkClicked);
            // 
            // lnkLbl_ExistantFailureType
            // 
            this.lnkLbl_ExistantFailureType.AutoSize = true;
            this.lnkLbl_ExistantFailureType.Location = new System.Drawing.Point(89, 93);
            this.lnkLbl_ExistantFailureType.Name = "lnkLbl_ExistantFailureType";
            this.lnkLbl_ExistantFailureType.Size = new System.Drawing.Size(121, 13);
            this.lnkLbl_ExistantFailureType.TabIndex = 29;
            this.lnkLbl_ExistantFailureType.TabStop = true;
            this.lnkLbl_ExistantFailureType.Text = "Type de panne existant";
            this.lnkLbl_ExistantFailureType.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl_ExistantFailureType_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(11, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "* Des champs obligatoires";
            // 
            // grpBx_Steps
            // 
            this.grpBx_Steps.Controls.Add(this.btn_Down);
            this.grpBx_Steps.Controls.Add(this.btn_Up);
            this.grpBx_Steps.Controls.Add(this.btn_RemoveStep);
            this.grpBx_Steps.Controls.Add(this.btn_ModifySetp);
            this.grpBx_Steps.Controls.Add(this.btn_AddStep);
            this.grpBx_Steps.Controls.Add(this.btn_Save2DataBase);
            this.grpBx_Steps.Controls.Add(this.btn_Cancel);
            this.grpBx_Steps.Controls.Add(this.lstVew_3dObjects);
            this.grpBx_Steps.Controls.Add(this.lstVew_Steps);
            this.grpBx_Steps.Location = new System.Drawing.Point(14, 218);
            this.grpBx_Steps.Name = "grpBx_Steps";
            this.grpBx_Steps.Size = new System.Drawing.Size(459, 211);
            this.grpBx_Steps.TabIndex = 25;
            this.grpBx_Steps.TabStop = false;
            this.grpBx_Steps.Text = "L\'ensemeble des étapes";
            // 
            // btn_Down
            // 
            this.btn_Down.Image = ((System.Drawing.Image)(resources.GetObject("btn_Down.Image")));
            this.btn_Down.Location = new System.Drawing.Point(177, 182);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(35, 23);
            this.btn_Down.TabIndex = 20;
            this.btn_Down.UseVisualStyleBackColor = true;
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // btn_Up
            // 
            this.btn_Up.Image = ((System.Drawing.Image)(resources.GetObject("btn_Up.Image")));
            this.btn_Up.Location = new System.Drawing.Point(136, 182);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(35, 23);
            this.btn_Up.TabIndex = 19;
            this.btn_Up.UseVisualStyleBackColor = true;
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // btn_RemoveStep
            // 
            this.btn_RemoveStep.Image = ((System.Drawing.Image)(resources.GetObject("btn_RemoveStep.Image")));
            this.btn_RemoveStep.Location = new System.Drawing.Point(95, 182);
            this.btn_RemoveStep.Name = "btn_RemoveStep";
            this.btn_RemoveStep.Size = new System.Drawing.Size(35, 23);
            this.btn_RemoveStep.TabIndex = 18;
            this.btn_RemoveStep.UseVisualStyleBackColor = true;
            this.btn_RemoveStep.Click += new System.EventHandler(this.btn_RemoveStep_Click);
            // 
            // btn_ModifySetp
            // 
            this.btn_ModifySetp.Image = ((System.Drawing.Image)(resources.GetObject("btn_ModifySetp.Image")));
            this.btn_ModifySetp.Location = new System.Drawing.Point(54, 182);
            this.btn_ModifySetp.Name = "btn_ModifySetp";
            this.btn_ModifySetp.Size = new System.Drawing.Size(35, 23);
            this.btn_ModifySetp.TabIndex = 17;
            this.btn_ModifySetp.UseVisualStyleBackColor = true;
            this.btn_ModifySetp.Click += new System.EventHandler(this.btn_ModifySetp_Click);
            // 
            // btn_AddStep
            // 
            this.btn_AddStep.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddStep.Image")));
            this.btn_AddStep.Location = new System.Drawing.Point(13, 182);
            this.btn_AddStep.Name = "btn_AddStep";
            this.btn_AddStep.Size = new System.Drawing.Size(35, 23);
            this.btn_AddStep.TabIndex = 16;
            this.btn_AddStep.UseVisualStyleBackColor = true;
            this.btn_AddStep.Click += new System.EventHandler(this.btn_AddStep_Click);
            // 
            // btn_Save2DataBase
            // 
            this.btn_Save2DataBase.Location = new System.Drawing.Point(292, 182);
            this.btn_Save2DataBase.Name = "btn_Save2DataBase";
            this.btn_Save2DataBase.Size = new System.Drawing.Size(74, 23);
            this.btn_Save2DataBase.TabIndex = 15;
            this.btn_Save2DataBase.Text = "Sauvegarder";
            this.btn_Save2DataBase.UseVisualStyleBackColor = true;
            this.btn_Save2DataBase.Click += new System.EventHandler(this.btn_Save_DataBase_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(372, 182);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 14;
            this.btn_Cancel.Text = "Annuler";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lstVew_3dObjects
            // 
            this.lstVew_3dObjects.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lstVew_3dObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnHdr_Name});
            this.lstVew_3dObjects.Location = new System.Drawing.Point(326, 15);
            this.lstVew_3dObjects.Name = "lstVew_3dObjects";
            this.lstVew_3dObjects.Size = new System.Drawing.Size(121, 162);
            this.lstVew_3dObjects.TabIndex = 1;
            this.lstVew_3dObjects.UseCompatibleStateImageBehavior = false;
            this.lstVew_3dObjects.View = System.Windows.Forms.View.Details;
            // 
            // clmnHdr_Name
            // 
            this.clmnHdr_Name.Text = "Objets 3d";
            this.clmnHdr_Name.Width = 113;
            // 
            // lstVew_Steps
            // 
            this.lstVew_Steps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnHdr_Id,
            this.clmnHdr_Title});
            this.lstVew_Steps.FullRowSelect = true;
            this.lstVew_Steps.GridLines = true;
            this.lstVew_Steps.Location = new System.Drawing.Point(13, 15);
            this.lstVew_Steps.MultiSelect = false;
            this.lstVew_Steps.Name = "lstVew_Steps";
            this.lstVew_Steps.Size = new System.Drawing.Size(314, 162);
            this.lstVew_Steps.TabIndex = 0;
            this.lstVew_Steps.UseCompatibleStateImageBehavior = false;
            this.lstVew_Steps.View = System.Windows.Forms.View.Details;
            this.lstVew_Steps.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstVew_Steps_MouseClick);
            // 
            // clmnHdr_Id
            // 
            this.clmnHdr_Id.Text = "Id";
            // 
            // clmnHdr_Title
            // 
            this.clmnHdr_Title.Text = "Libelle";
            this.clmnHdr_Title.Width = 250;
            // 
            // txtBx_Description
            // 
            this.txtBx_Description.Location = new System.Drawing.Point(12, 133);
            this.txtBx_Description.Multiline = true;
            this.txtBx_Description.Name = "txtBx_Description";
            this.txtBx_Description.Size = new System.Drawing.Size(461, 73);
            this.txtBx_Description.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Description: ";
            // 
            // txtBx_Failure
            // 
            this.txtBx_Failure.Location = new System.Drawing.Point(301, 70);
            this.txtBx_Failure.Name = "txtBx_Failure";
            this.txtBx_Failure.Size = new System.Drawing.Size(172, 20);
            this.txtBx_Failure.TabIndex = 23;
            this.txtBx_Failure.Validated += new System.EventHandler(this.txtBx_Failure_Validated);
            // 
            // txtBx_FailureType
            // 
            this.txtBx_FailureType.Location = new System.Drawing.Point(92, 70);
            this.txtBx_FailureType.Name = "txtBx_FailureType";
            this.txtBx_FailureType.Size = new System.Drawing.Size(150, 20);
            this.txtBx_FailureType.TabIndex = 22;
            this.txtBx_FailureType.Validated += new System.EventHandler(this.txtBx_FailureType_Validated);
            // 
            // lbl_Failure
            // 
            this.lbl_Failure.AutoSize = true;
            this.lbl_Failure.ContextMenuStrip = this.ctxtMnuStrp_GetDescription;
            this.lbl_Failure.Location = new System.Drawing.Point(254, 74);
            this.lbl_Failure.Name = "lbl_Failure";
            this.lbl_Failure.Size = new System.Drawing.Size(47, 13);
            this.lbl_Failure.TabIndex = 18;
            this.lbl_Failure.Text = "Panne:*";
            // 
            // ctxtMnuStrp_GetDescription
            // 
            this.ctxtMnuStrp_GetDescription.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descriptionToolStripMenuItem});
            this.ctxtMnuStrp_GetDescription.Name = "ctxtMnuStrp_GetDescription";
            this.ctxtMnuStrp_GetDescription.Size = new System.Drawing.Size(153, 48);
            this.ctxtMnuStrp_GetDescription.Layout += new System.Windows.Forms.LayoutEventHandler(this.ctxtMnuStrp_GetDescription_Layout);
            // 
            // descriptionToolStripMenuItem
            // 
            this.descriptionToolStripMenuItem.Name = "descriptionToolStripMenuItem";
            this.descriptionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.descriptionToolStripMenuItem.Text = "Description";
            this.descriptionToolStripMenuItem.Click += new System.EventHandler(this.descriptionToolStripMenuItem_Click);
            // 
            // txtBx_Title
            // 
            this.txtBx_Title.Location = new System.Drawing.Point(219, 30);
            this.txtBx_Title.Name = "txtBx_Title";
            this.txtBx_Title.Size = new System.Drawing.Size(256, 20);
            this.txtBx_Title.TabIndex = 21;
            // 
            // txtBx_Id
            // 
            this.txtBx_Id.Enabled = false;
            this.txtBx_Id.Location = new System.Drawing.Point(39, 30);
            this.txtBx_Id.Name = "txtBx_Id";
            this.txtBx_Id.ReadOnly = true;
            this.txtBx_Id.Size = new System.Drawing.Size(100, 20);
            this.txtBx_Id.TabIndex = 20;
            // 
            // lbl_FailureType
            // 
            this.lbl_FailureType.AutoSize = true;
            this.lbl_FailureType.ContextMenuStrip = this.ctxtMnuStrp_GetDescription;
            this.lbl_FailureType.Location = new System.Drawing.Point(5, 74);
            this.lbl_FailureType.Name = "lbl_FailureType";
            this.lbl_FailureType.Size = new System.Drawing.Size(89, 13);
            this.lbl_FailureType.TabIndex = 17;
            this.lbl_FailureType.Text = "Type de panne:*";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Location = new System.Drawing.Point(163, 34);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(46, 13);
            this.lbl_Title.TabIndex = 16;
            this.lbl_Title.Text = "Libelle:*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Id: ";
            // 
            // frm_AddProcedure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 459);
            this.Controls.Add(this.grpBx_AddProcedure);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(770, 113);
            this.Name = "frm_AddProcedure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm_AddProcedure";
            this.Load += new System.EventHandler(this.frm_AddProcedure_Load);
            this.Move += new System.EventHandler(this.frm_AddProcedure_Move);
            this.grpBx_AddProcedure.ResumeLayout(false);
            this.grpBx_AddProcedure.PerformLayout();
            this.grpBx_Steps.ResumeLayout(false);
            this.ctxtMnuStrp_GetDescription.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBx_AddProcedure;
        private System.Windows.Forms.GroupBox grpBx_Steps;
        private System.Windows.Forms.Button btn_Down;
        private System.Windows.Forms.Button btn_Up;
        private System.Windows.Forms.Button btn_RemoveStep;
        private System.Windows.Forms.Button btn_ModifySetp;
        private System.Windows.Forms.Button btn_AddStep;
        private System.Windows.Forms.Button btn_Save2DataBase;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ListView lstVew_3dObjects;
        private System.Windows.Forms.ColumnHeader clmnHdr_Name;
        private System.Windows.Forms.ListView lstVew_Steps;
        private System.Windows.Forms.ColumnHeader clmnHdr_Id;
        private System.Windows.Forms.ColumnHeader clmnHdr_Title;
        private System.Windows.Forms.TextBox txtBx_Description;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBx_Failure;
        private System.Windows.Forms.TextBox txtBx_FailureType;
        private System.Windows.Forms.Label lbl_Failure;
        private System.Windows.Forms.TextBox txtBx_Title;
        private System.Windows.Forms.TextBox txtBx_Id;
        private System.Windows.Forms.Label lbl_FailureType;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip ctxtMnuStrp_GetDescription;
        private System.Windows.Forms.ToolStripMenuItem descriptionToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkLbl_ExistantFailure;
        private System.Windows.Forms.LinkLabel lnkLbl_ExistantFailureType;
    }
}