namespace MARS_Expert
{
    partial class RenderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenderForm));
            this.pic3d = new System.Windows.Forms.PictureBox();
            this.ctxtMnuStrp_ClearScene = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.netoyerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Test = new System.Windows.Forms.Button();
            this.tmr_CheckNotification = new System.Windows.Forms.Timer(this.components);
            this.lbl_Notifications = new System.Windows.Forms.Label();
            this.ctxtMnuStrp_lblNotification = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.accepterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refuserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_AddProcedure = new System.Windows.Forms.Button();
            this.btn_StartVideo = new System.Windows.Forms.Button();
            this.btn_UpdateProcedure = new System.Windows.Forms.Button();
            this.btn_SendProcedure = new System.Windows.Forms.Button();
            this.btn_MoveCameraForward = new System.Windows.Forms.Button();
            this.btn_MoveCameraBackward = new System.Windows.Forms.Button();
            this.lbl_CameraLocation = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fermerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearListtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UseLocalImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UseVideoFlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markerUtestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.supprimerProcedureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkBx_LocalImage = new System.Windows.Forms.CheckBox();
            this.btn_NextImage = new System.Windows.Forms.Button();
            this.btn_PreviousImage = new System.Windows.Forms.Button();
            this.ctxtMnuStrp_GetDescription = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.descriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_NotificationHolder = new System.Windows.Forms.Panel();
            this.picBx_Notif = new System.Windows.Forms.PictureBox();
            this.pnl_Top = new System.Windows.Forms.Panel();
            this.btn_CloseForm = new System.Windows.Forms.Button();
            this.btn_MinimizeForm = new System.Windows.Forms.Button();
            this.pnl_MenuHolder = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_Text = new System.Windows.Forms.Label();
            this.pnl_Left = new System.Windows.Forms.Panel();
            this.pnl_Right = new System.Windows.Forms.Panel();
            this.pnl_Bottom = new System.Windows.Forms.Panel();
            this.trkbr_CompressionRate = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trkbr_BinarisationRate = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBx_SendMsg = new System.Windows.Forms.TextBox();
            this.btn_SendMsg = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic3d)).BeginInit();
            this.ctxtMnuStrp_ClearScene.SuspendLayout();
            this.ctxtMnuStrp_lblNotification.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ctxtMnuStrp_GetDescription.SuspendLayout();
            this.pnl_NotificationHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBx_Notif)).BeginInit();
            this.pnl_Top.SuspendLayout();
            this.pnl_MenuHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_CompressionRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_BinarisationRate)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pic3d
            // 
            this.pic3d.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pic3d.ContextMenuStrip = this.ctxtMnuStrp_ClearScene;
            this.pic3d.Location = new System.Drawing.Point(397, 73);
            this.pic3d.Name = "pic3d";
            this.pic3d.Size = new System.Drawing.Size(600, 450);
            this.pic3d.TabIndex = 18;
            this.pic3d.TabStop = false;
            // 
            // ctxtMnuStrp_ClearScene
            // 
            this.ctxtMnuStrp_ClearScene.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netoyerToolStripMenuItem});
            this.ctxtMnuStrp_ClearScene.Name = "ctxtMnuStrp_ClearScene";
            this.ctxtMnuStrp_ClearScene.Size = new System.Drawing.Size(117, 26);
            // 
            // netoyerToolStripMenuItem
            // 
            this.netoyerToolStripMenuItem.Name = "netoyerToolStripMenuItem";
            this.netoyerToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.netoyerToolStripMenuItem.Text = "Netoyer";
            this.netoyerToolStripMenuItem.Click += new System.EventHandler(this.netoyerToolStripMenuItem_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(1100, 429);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(75, 23);
            this.btn_Test.TabIndex = 26;
            this.btn_Test.Text = "Test";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.button1_Click);
            // 
            // tmr_CheckNotification
            // 
            this.tmr_CheckNotification.Interval = 500;
            this.tmr_CheckNotification.Tick += new System.EventHandler(this.tmr_CheckNotification_Tick);
            // 
            // lbl_Notifications
            // 
            this.lbl_Notifications.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Notifications.ContextMenuStrip = this.ctxtMnuStrp_lblNotification;
            this.lbl_Notifications.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Notifications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lbl_Notifications.Location = new System.Drawing.Point(2, -4);
            this.lbl_Notifications.Name = "lbl_Notifications";
            this.lbl_Notifications.Size = new System.Drawing.Size(350, 127);
            this.lbl_Notifications.TabIndex = 27;
            this.lbl_Notifications.Text = "\r\n";
            this.lbl_Notifications.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctxtMnuStrp_lblNotification
            // 
            this.ctxtMnuStrp_lblNotification.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accepterToolStripMenuItem,
            this.refuserToolStripMenuItem});
            this.ctxtMnuStrp_lblNotification.Name = "ctxtMnuStrp_lblNotification";
            this.ctxtMnuStrp_lblNotification.Size = new System.Drawing.Size(122, 48);
            // 
            // accepterToolStripMenuItem
            // 
            this.accepterToolStripMenuItem.Name = "accepterToolStripMenuItem";
            this.accepterToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.accepterToolStripMenuItem.Text = "Accepter";
            this.accepterToolStripMenuItem.Click += new System.EventHandler(this.accepterToolStripMenuItem_Click);
            // 
            // refuserToolStripMenuItem
            // 
            this.refuserToolStripMenuItem.Name = "refuserToolStripMenuItem";
            this.refuserToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.refuserToolStripMenuItem.Text = "Refuser";
            this.refuserToolStripMenuItem.Click += new System.EventHandler(this.refuserToolStripMenuItem_Click);
            // 
            // btn_AddProcedure
            // 
            this.btn_AddProcedure.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AddProcedure.BackgroundImage")));
            this.btn_AddProcedure.FlatAppearance.BorderSize = 0;
            this.btn_AddProcedure.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_AddProcedure.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_AddProcedure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddProcedure.Location = new System.Drawing.Point(261, 73);
            this.btn_AddProcedure.Name = "btn_AddProcedure";
            this.btn_AddProcedure.Size = new System.Drawing.Size(126, 40);
            this.btn_AddProcedure.TabIndex = 38;
            this.toolTip1.SetToolTip(this.btn_AddProcedure, "Ajouter Procedure");
            this.btn_AddProcedure.UseVisualStyleBackColor = true;
            this.btn_AddProcedure.Click += new System.EventHandler(this.btn_AddProcedure_Click);
            this.btn_AddProcedure.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_AddProcedure_MouseDown);
            this.btn_AddProcedure.MouseEnter += new System.EventHandler(this.btn_AddProcedure_MouseEnter);
            this.btn_AddProcedure.MouseLeave += new System.EventHandler(this.btn_AddProcedure_MouseLeave);
            this.btn_AddProcedure.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_AddProcedure_MouseUp);
            // 
            // btn_StartVideo
            // 
            this.btn_StartVideo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_StartVideo.BackgroundImage")));
            this.btn_StartVideo.FlatAppearance.BorderSize = 0;
            this.btn_StartVideo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_StartVideo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_StartVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_StartVideo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StartVideo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btn_StartVideo.Location = new System.Drawing.Point(397, 529);
            this.btn_StartVideo.Name = "btn_StartVideo";
            this.btn_StartVideo.Size = new System.Drawing.Size(113, 35);
            this.btn_StartVideo.TabIndex = 37;
            this.btn_StartVideo.Text = "Démarrer";
            this.btn_StartVideo.UseVisualStyleBackColor = false;
            this.btn_StartVideo.Click += new System.EventHandler(this.btn_StartVideo_Click);
            this.btn_StartVideo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_StartVideo_MouseDown);
            this.btn_StartVideo.MouseEnter += new System.EventHandler(this.btn_StartVideo_MouseEnter);
            this.btn_StartVideo.MouseLeave += new System.EventHandler(this.btn_StartVideo_MouseLeave);
            this.btn_StartVideo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_StartVideo_MouseUp);
            // 
            // btn_UpdateProcedure
            // 
            this.btn_UpdateProcedure.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_UpdateProcedure.BackgroundImage")));
            this.btn_UpdateProcedure.FlatAppearance.BorderSize = 0;
            this.btn_UpdateProcedure.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_UpdateProcedure.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_UpdateProcedure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpdateProcedure.Location = new System.Drawing.Point(137, 73);
            this.btn_UpdateProcedure.Name = "btn_UpdateProcedure";
            this.btn_UpdateProcedure.Size = new System.Drawing.Size(126, 40);
            this.btn_UpdateProcedure.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btn_UpdateProcedure, "Mise à jour Procedure");
            this.btn_UpdateProcedure.UseVisualStyleBackColor = false;
            this.btn_UpdateProcedure.Click += new System.EventHandler(this.btn_UpdateProcedure_Click);
            this.btn_UpdateProcedure.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_UpdateProcedure_MouseDown);
            this.btn_UpdateProcedure.MouseEnter += new System.EventHandler(this.btn_UpdateProcedure_MouseEnter);
            this.btn_UpdateProcedure.MouseLeave += new System.EventHandler(this.btn_UpdateProcedure_MouseLeave);
            this.btn_UpdateProcedure.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_UpdateProcedure_MouseUp);
            // 
            // btn_SendProcedure
            // 
            this.btn_SendProcedure.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SendProcedure.BackgroundImage")));
            this.btn_SendProcedure.FlatAppearance.BorderSize = 0;
            this.btn_SendProcedure.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_SendProcedure.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_SendProcedure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SendProcedure.Location = new System.Drawing.Point(13, 73);
            this.btn_SendProcedure.Name = "btn_SendProcedure";
            this.btn_SendProcedure.Size = new System.Drawing.Size(126, 40);
            this.btn_SendProcedure.TabIndex = 42;
            this.toolTip1.SetToolTip(this.btn_SendProcedure, "Envoyer Procedure");
            this.btn_SendProcedure.UseVisualStyleBackColor = false;
            this.btn_SendProcedure.Click += new System.EventHandler(this.btn_SendProcedure_Click);
            this.btn_SendProcedure.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_SendProcedure_MouseDown);
            this.btn_SendProcedure.MouseEnter += new System.EventHandler(this.btn_SendProcedure_MouseEnter);
            this.btn_SendProcedure.MouseLeave += new System.EventHandler(this.btn_SendProcedure_MouseLeave);
            this.btn_SendProcedure.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_SendProcedure_MouseUp);
            // 
            // btn_MoveCameraForward
            // 
            this.btn_MoveCameraForward.Location = new System.Drawing.Point(1109, 464);
            this.btn_MoveCameraForward.Name = "btn_MoveCameraForward";
            this.btn_MoveCameraForward.Size = new System.Drawing.Size(76, 23);
            this.btn_MoveCameraForward.TabIndex = 43;
            this.btn_MoveCameraForward.Text = "Move Cam +";
            this.btn_MoveCameraForward.UseVisualStyleBackColor = true;
            this.btn_MoveCameraForward.Click += new System.EventHandler(this.btn_MoveCameraForward_Click);
            // 
            // btn_MoveCameraBackward
            // 
            this.btn_MoveCameraBackward.Location = new System.Drawing.Point(1028, 464);
            this.btn_MoveCameraBackward.Name = "btn_MoveCameraBackward";
            this.btn_MoveCameraBackward.Size = new System.Drawing.Size(75, 23);
            this.btn_MoveCameraBackward.TabIndex = 44;
            this.btn_MoveCameraBackward.Text = "Move Cam -";
            this.btn_MoveCameraBackward.UseVisualStyleBackColor = true;
            this.btn_MoveCameraBackward.Click += new System.EventHandler(this.btn_MoveCameraBackward_Click);
            // 
            // lbl_CameraLocation
            // 
            this.lbl_CameraLocation.AutoSize = true;
            this.lbl_CameraLocation.Location = new System.Drawing.Point(1031, 490);
            this.lbl_CameraLocation.Name = "lbl_CameraLocation";
            this.lbl_CameraLocation.Size = new System.Drawing.Size(35, 13);
            this.lbl_CameraLocation.TabIndex = 45;
            this.lbl_CameraLocation.Text = "label1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 24);
            this.menuStrip1.TabIndex = 47;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_Top_MouseDown);
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fermerToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // fermerToolStripMenuItem
            // 
            this.fermerToolStripMenuItem.Name = "fermerToolStripMenuItem";
            this.fermerToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.fermerToolStripMenuItem.Text = "Fermer";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadImagesToolStripMenuItem,
            this.ClearListtoolStripMenuItem,
            this.UseLocalImageToolStripMenuItem,
            this.UseVideoFlowToolStripMenuItem,
            this.markerUtestToolStripMenuItem,
            this.toolStripSeparator1,
            this.supprimerProcedureToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // LoadImagesToolStripMenuItem
            // 
            this.LoadImagesToolStripMenuItem.Name = "LoadImagesToolStripMenuItem";
            this.LoadImagesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.LoadImagesToolStripMenuItem.Text = "Charger Images Locales";
            this.LoadImagesToolStripMenuItem.Click += new System.EventHandler(this.LoadImagesToolStripMenuItem_Click);
            // 
            // ClearListtoolStripMenuItem
            // 
            this.ClearListtoolStripMenuItem.Name = "ClearListtoolStripMenuItem";
            this.ClearListtoolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.ClearListtoolStripMenuItem.Text = "Reinitialiser les images";
            this.ClearListtoolStripMenuItem.Click += new System.EventHandler(this.ClearListtoolStripMenuItem_Click);
            // 
            // UseLocalImageToolStripMenuItem
            // 
            this.UseLocalImageToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.UseLocalImageToolStripMenuItem.Name = "UseLocalImageToolStripMenuItem";
            this.UseLocalImageToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.UseLocalImageToolStripMenuItem.Text = "Utiliser Images Locales";
            this.UseLocalImageToolStripMenuItem.Click += new System.EventHandler(this.hrToolStripMenuItem_Click);
            // 
            // UseVideoFlowToolStripMenuItem
            // 
            this.UseVideoFlowToolStripMenuItem.Checked = true;
            this.UseVideoFlowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseVideoFlowToolStripMenuItem.Name = "UseVideoFlowToolStripMenuItem";
            this.UseVideoFlowToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.UseVideoFlowToolStripMenuItem.Text = "Utiliser le flux de video";
            this.UseVideoFlowToolStripMenuItem.Click += new System.EventHandler(this.utiliserLeFlusDeVideoToolStripMenuItem_Click);
            // 
            // markerUtestToolStripMenuItem
            // 
            this.markerUtestToolStripMenuItem.Name = "markerUtestToolStripMenuItem";
            this.markerUtestToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.markerUtestToolStripMenuItem.Text = "Marker_U (test)";
            this.markerUtestToolStripMenuItem.Click += new System.EventHandler(this.markerUtestToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // supprimerProcedureToolStripMenuItem
            // 
            this.supprimerProcedureToolStripMenuItem.Name = "supprimerProcedureToolStripMenuItem";
            this.supprimerProcedureToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.supprimerProcedureToolStripMenuItem.Text = "Supprimer Procedure";
            this.supprimerProcedureToolStripMenuItem.Click += new System.EventHandler(this.supprimerProcedureToolStripMenuItem_Click);
            // 
            // chkBx_LocalImage
            // 
            this.chkBx_LocalImage.AutoSize = true;
            this.chkBx_LocalImage.Location = new System.Drawing.Point(1045, 531);
            this.chkBx_LocalImage.Name = "chkBx_LocalImage";
            this.chkBx_LocalImage.Size = new System.Drawing.Size(83, 17);
            this.chkBx_LocalImage.TabIndex = 49;
            this.chkBx_LocalImage.Text = "Local Image";
            this.chkBx_LocalImage.UseVisualStyleBackColor = true;
            // 
            // btn_NextImage
            // 
            this.btn_NextImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btn_NextImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NextImage.Image = ((System.Drawing.Image)(resources.GetObject("btn_NextImage.Image")));
            this.btn_NextImage.Location = new System.Drawing.Point(600, 535);
            this.btn_NextImage.Name = "btn_NextImage";
            this.btn_NextImage.Size = new System.Drawing.Size(33, 23);
            this.btn_NextImage.TabIndex = 50;
            this.toolTip1.SetToolTip(this.btn_NextImage, "Image suivante");
            this.btn_NextImage.UseVisualStyleBackColor = true;
            this.btn_NextImage.Visible = false;
            this.btn_NextImage.Click += new System.EventHandler(this.btn_NextImage_Click);
            // 
            // btn_PreviousImage
            // 
            this.btn_PreviousImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btn_PreviousImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PreviousImage.Image = ((System.Drawing.Image)(resources.GetObject("btn_PreviousImage.Image")));
            this.btn_PreviousImage.Location = new System.Drawing.Point(571, 535);
            this.btn_PreviousImage.Name = "btn_PreviousImage";
            this.btn_PreviousImage.Size = new System.Drawing.Size(32, 23);
            this.btn_PreviousImage.TabIndex = 51;
            this.toolTip1.SetToolTip(this.btn_PreviousImage, "Image precedente");
            this.btn_PreviousImage.UseVisualStyleBackColor = true;
            this.btn_PreviousImage.Visible = false;
            this.btn_PreviousImage.Click += new System.EventHandler(this.btn_PreviousImage_Click);
            // 
            // ctxtMnuStrp_GetDescription
            // 
            this.ctxtMnuStrp_GetDescription.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descriptionToolStripMenuItem});
            this.ctxtMnuStrp_GetDescription.Name = "ctxtMnuStrp_GetDescription";
            this.ctxtMnuStrp_GetDescription.Size = new System.Drawing.Size(135, 26);
            // 
            // descriptionToolStripMenuItem
            // 
            this.descriptionToolStripMenuItem.Name = "descriptionToolStripMenuItem";
            this.descriptionToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.descriptionToolStripMenuItem.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label1.Location = new System.Drawing.Point(771, 675);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "La zone de notification";
            // 
            // pnl_NotificationHolder
            // 
            this.pnl_NotificationHolder.BackColor = System.Drawing.Color.Transparent;
            this.pnl_NotificationHolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_NotificationHolder.BackgroundImage")));
            this.pnl_NotificationHolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_NotificationHolder.Controls.Add(this.picBx_Notif);
            this.pnl_NotificationHolder.Controls.Add(this.lbl_Notifications);
            this.pnl_NotificationHolder.Location = new System.Drawing.Point(642, 533);
            this.pnl_NotificationHolder.Name = "pnl_NotificationHolder";
            this.pnl_NotificationHolder.Size = new System.Drawing.Size(355, 141);
            this.pnl_NotificationHolder.TabIndex = 53;
            // 
            // picBx_Notif
            // 
            this.picBx_Notif.BackColor = System.Drawing.Color.Transparent;
            this.picBx_Notif.Enabled = false;
            this.picBx_Notif.Image = ((System.Drawing.Image)(resources.GetObject("picBx_Notif.Image")));
            this.picBx_Notif.Location = new System.Drawing.Point(316, 10);
            this.picBx_Notif.Name = "picBx_Notif";
            this.picBx_Notif.Size = new System.Drawing.Size(32, 32);
            this.picBx_Notif.TabIndex = 61;
            this.picBx_Notif.TabStop = false;
            // 
            // pnl_Top
            // 
            this.pnl_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Top.BackgroundImage")));
            this.pnl_Top.Controls.Add(this.btn_CloseForm);
            this.pnl_Top.Controls.Add(this.btn_MinimizeForm);
            this.pnl_Top.Controls.Add(this.pnl_MenuHolder);
            this.pnl_Top.Controls.Add(this.pictureBox1);
            this.pnl_Top.Controls.Add(this.lbl_Text);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Size = new System.Drawing.Size(1020, 54);
            this.pnl_Top.TabIndex = 54;
            this.pnl_Top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_Top_MouseDown);
            // 
            // btn_CloseForm
            // 
            this.btn_CloseForm.BackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatAppearance.BorderSize = 0;
            this.btn_CloseForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CloseForm.Image = ((System.Drawing.Image)(resources.GetObject("btn_CloseForm.Image")));
            this.btn_CloseForm.Location = new System.Drawing.Point(985, 2);
            this.btn_CloseForm.Name = "btn_CloseForm";
            this.btn_CloseForm.Size = new System.Drawing.Size(27, 27);
            this.btn_CloseForm.TabIndex = 48;
            this.btn_CloseForm.UseVisualStyleBackColor = false;
            this.btn_CloseForm.Click += new System.EventHandler(this.btn_CloseForm_Click);
            this.btn_CloseForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_CloseForm_MouseDown);
            this.btn_CloseForm.MouseEnter += new System.EventHandler(this.btn_CloseForm_MouseEnter);
            this.btn_CloseForm.MouseLeave += new System.EventHandler(this.btn_CloseForm_MouseLeave);
            this.btn_CloseForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_CloseForm_MouseUp);
            // 
            // btn_MinimizeForm
            // 
            this.btn_MinimizeForm.BackColor = System.Drawing.Color.Transparent;
            this.btn_MinimizeForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_MinimizeForm.BackgroundImage")));
            this.btn_MinimizeForm.FlatAppearance.BorderSize = 0;
            this.btn_MinimizeForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_MinimizeForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_MinimizeForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MinimizeForm.Location = new System.Drawing.Point(958, 2);
            this.btn_MinimizeForm.Name = "btn_MinimizeForm";
            this.btn_MinimizeForm.Size = new System.Drawing.Size(27, 27);
            this.btn_MinimizeForm.TabIndex = 49;
            this.btn_MinimizeForm.UseVisualStyleBackColor = false;
            this.btn_MinimizeForm.Click += new System.EventHandler(this.btn_MinimizeForm_Click);
            this.btn_MinimizeForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MinimizeForm_MouseDown);
            this.btn_MinimizeForm.MouseEnter += new System.EventHandler(this.btn_MinimizeForm_MouseEnter);
            this.btn_MinimizeForm.MouseLeave += new System.EventHandler(this.btn_MinimizeForm_MouseLeave);
            this.btn_MinimizeForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MinimizeForm_MouseUp);
            // 
            // pnl_MenuHolder
            // 
            this.pnl_MenuHolder.BackColor = System.Drawing.Color.Transparent;
            this.pnl_MenuHolder.Controls.Add(this.menuStrip1);
            this.pnl_MenuHolder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_MenuHolder.Location = new System.Drawing.Point(0, 30);
            this.pnl_MenuHolder.Name = "pnl_MenuHolder";
            this.pnl_MenuHolder.Size = new System.Drawing.Size(1020, 24);
            this.pnl_MenuHolder.TabIndex = 62;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 26);
            this.pictureBox1.TabIndex = 60;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // lbl_Text
            // 
            this.lbl_Text.AutoSize = true;
            this.lbl_Text.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Text.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lbl_Text.Location = new System.Drawing.Point(446, 7);
            this.lbl_Text.Name = "lbl_Text";
            this.lbl_Text.Size = new System.Drawing.Size(0, 25);
            this.lbl_Text.TabIndex = 50;
            this.lbl_Text.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_Top_MouseDown);
            // 
            // pnl_Left
            // 
            this.pnl_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Left.Location = new System.Drawing.Point(0, 54);
            this.pnl_Left.Name = "pnl_Left";
            this.pnl_Left.Size = new System.Drawing.Size(8, 646);
            this.pnl_Left.TabIndex = 55;
            // 
            // pnl_Right
            // 
            this.pnl_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnl_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_Right.Location = new System.Drawing.Point(1012, 54);
            this.pnl_Right.Name = "pnl_Right";
            this.pnl_Right.Size = new System.Drawing.Size(8, 646);
            this.pnl_Right.TabIndex = 56;
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Bottom.Location = new System.Drawing.Point(8, 692);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Size = new System.Drawing.Size(1004, 8);
            this.pnl_Bottom.TabIndex = 57;
            // 
            // trkbr_CompressionRate
            // 
            this.trkbr_CompressionRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.trkbr_CompressionRate.Location = new System.Drawing.Point(397, 585);
            this.trkbr_CompressionRate.Maximum = 100;
            this.trkbr_CompressionRate.Name = "trkbr_CompressionRate";
            this.trkbr_CompressionRate.Size = new System.Drawing.Size(218, 45);
            this.trkbr_CompressionRate.TabIndex = 58;
            this.trkbr_CompressionRate.Value = 75;
            this.trkbr_CompressionRate.Scroll += new System.EventHandler(this.trkbr_CompressionRate_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(402, 569);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Taux de compression de la video";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(402, 625);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Taux de binarisation d\'image";
            // 
            // trkbr_BinarisationRate
            // 
            this.trkbr_BinarisationRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.trkbr_BinarisationRate.Location = new System.Drawing.Point(397, 641);
            this.trkbr_BinarisationRate.Maximum = 255;
            this.trkbr_BinarisationRate.Name = "trkbr_BinarisationRate";
            this.trkbr_BinarisationRate.Size = new System.Drawing.Size(218, 45);
            this.trkbr_BinarisationRate.TabIndex = 60;
            this.trkbr_BinarisationRate.Value = 100;
            this.trkbr_BinarisationRate.Scroll += new System.EventHandler(this.trkbr_BinarisationRate_Scroll);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.btn_SendMsg);
            this.panel1.Controls.Add(this.txtBx_SendMsg);
            this.panel1.Location = new System.Drawing.Point(13, 573);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 113);
            this.panel1.TabIndex = 62;
            // 
            // txtBx_SendMsg
            // 
            this.txtBx_SendMsg.Location = new System.Drawing.Point(3, 87);
            this.txtBx_SendMsg.Name = "txtBx_SendMsg";
            this.txtBx_SendMsg.Size = new System.Drawing.Size(335, 20);
            this.txtBx_SendMsg.TabIndex = 0;
            this.txtBx_SendMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBx_SendMsg_KeyPress);
            // 
            // btn_SendMsg
            // 
            this.btn_SendMsg.Location = new System.Drawing.Point(343, 85);
            this.btn_SendMsg.Name = "btn_SendMsg";
            this.btn_SendMsg.Size = new System.Drawing.Size(23, 23);
            this.btn_SendMsg.TabIndex = 1;
            this.btn_SendMsg.Text = "S";
            this.btn_SendMsg.UseVisualStyleBackColor = true;
            this.btn_SendMsg.Click += new System.EventHandler(this.btn_SendMsg_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(363, 75);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // RenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1020, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trkbr_BinarisationRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trkbr_CompressionRate);
            this.Controls.Add(this.pnl_Bottom);
            this.Controls.Add(this.pnl_Right);
            this.Controls.Add(this.pnl_Left);
            this.Controls.Add(this.pnl_Top);
            this.Controls.Add(this.pnl_NotificationHolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_PreviousImage);
            this.Controls.Add(this.btn_NextImage);
            this.Controls.Add(this.chkBx_LocalImage);
            this.Controls.Add(this.lbl_CameraLocation);
            this.Controls.Add(this.btn_MoveCameraBackward);
            this.Controls.Add(this.btn_MoveCameraForward);
            this.Controls.Add(this.btn_SendProcedure);
            this.Controls.Add(this.btn_UpdateProcedure);
            this.Controls.Add(this.btn_AddProcedure);
            this.Controls.Add(this.btn_StartVideo);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.pic3d);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RenderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MARS L\'expert";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RenderForm_FormClosing);
            this.Load += new System.EventHandler(this.RenderForm_Load);
            this.SizeChanged += new System.EventHandler(this.RenderForm_SizeChanged);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.RenderForm_ControlAdded);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.RenderForm_ControlRemoved);
            ((System.ComponentModel.ISupportInitialize)(this.pic3d)).EndInit();
            this.ctxtMnuStrp_ClearScene.ResumeLayout(false);
            this.ctxtMnuStrp_lblNotification.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ctxtMnuStrp_GetDescription.ResumeLayout(false);
            this.pnl_NotificationHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBx_Notif)).EndInit();
            this.pnl_Top.ResumeLayout(false);
            this.pnl_Top.PerformLayout();
            this.pnl_MenuHolder.ResumeLayout(false);
            this.pnl_MenuHolder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_CompressionRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_BinarisationRate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pic3d;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Timer tmr_CheckNotification;
        private System.Windows.Forms.Label lbl_Notifications;
        private System.Windows.Forms.ContextMenuStrip ctxtMnuStrp_lblNotification;
        private System.Windows.Forms.ToolStripMenuItem accepterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refuserToolStripMenuItem;
        private System.Windows.Forms.Button btn_AddProcedure;
        private System.Windows.Forms.Button btn_StartVideo;
        private System.Windows.Forms.Button btn_UpdateProcedure;
        private System.Windows.Forms.Button btn_SendProcedure;
        private System.Windows.Forms.Button btn_MoveCameraForward;
        private System.Windows.Forms.Button btn_MoveCameraBackward;
        private System.Windows.Forms.Label lbl_CameraLocation;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkBx_LocalImage;
        private System.Windows.Forms.Button btn_NextImage;
        private System.Windows.Forms.Button btn_PreviousImage;
        private System.Windows.Forms.ContextMenuStrip ctxtMnuStrp_GetDescription;
        private System.Windows.Forms.ToolStripMenuItem descriptionToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem fermerToolStripMenuItem;
        private System.Windows.Forms.Panel pnl_NotificationHolder;
        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.Panel pnl_Left;
        private System.Windows.Forms.Panel pnl_Right;
        private System.Windows.Forms.Panel pnl_Bottom;
        private System.Windows.Forms.Button btn_CloseForm;
        private System.Windows.Forms.Button btn_MinimizeForm;
        private System.Windows.Forms.Label lbl_Text;
        private System.Windows.Forms.TrackBar trkbr_CompressionRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox picBx_Notif;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trkbr_BinarisationRate;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UseLocalImageToolStripMenuItem;
        private System.Windows.Forms.Panel pnl_MenuHolder;
        private System.Windows.Forms.ToolStripMenuItem UseVideoFlowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadImagesToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip ctxtMnuStrp_ClearScene;
        private System.Windows.Forms.ToolStripMenuItem netoyerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markerUtestToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem supprimerProcedureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearListtoolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_SendMsg;
        private System.Windows.Forms.TextBox txtBx_SendMsg;
        private System.Windows.Forms.RichTextBox richTextBox1;

    }
}

