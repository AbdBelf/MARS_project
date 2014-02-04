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
//using MARS_Expert.webService1;
using MARS_Expert.ResourceManager;
using MARS_Expert.Manager;
using System.Net;

using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.DirectX.AudioVideoPlayback;

namespace MARS_Expert
{
    public partial class RenderForm : Form
    {
        public Stack Children { get; set; }
        string login = "";
        public Procedure CreatedProcedure { get; set; }
        public static int NoNotification = 0;
        public static int NotificationExists = 1;
        public static int NotificationApproved = 2;
        public static int NotificationCanceled = 3;

        //private const int SCREEN_WIDTH = 320;
        //private const int SCREEN_HEIGHT = 240;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;

        //The list of pattern files
        private String[] AR_CODE_FILES;

        private const String AR_CAMERA_FILE = "data/camera_para.dat";

        private NyARDoubleMatrix44 __OnBuffer_nyar_transmat = new NyARDoubleMatrix44();

        private int _nb_marker_detected;
        private Matrix _trans_mat;


        private NyARDetectMarker _ar;


        private NyARToolkitCSUtils.Direct3d.NyARD3dSurface _surface;
        private NyARBitmapRaster _raster;

        // The Direct3D device.
        public static Device m_Device;

        //The pattern list
        NyARCode[] ar_code;

        //The list of detected pattern
        int[] ar_code_index;

        //The list that contains the width of the markers
        double[] marker_width;

        //The best confidence chosen from all existing markers
        double bestConfidence = 0;

        //The index of the marker that has the best confidence
        int bestMarker = 0;

        //the 3d Object to be drown
        Object3d Objct3d;

        //THe list of 3d objects used in the scene
        public static List<Object3d> Objects = new List<Object3d>();

        #region "D3D Setup Code"

        bool Pause = true;

        //Define what transformation is applied on an object 3d.
        //0 for translation
        //1 for rotation
        //2 for scaling
        int transformation = 0;

        public RenderForm(string login)
        {
            //MessageBox.Show("login: " + login, "RenderForm.Constructor");
            InitializeComponent();

            this.login = login;
            Manager.ExpertManager.Expert exprt = Manager.XMLManager.XMLExpert.SearchById(this.login);

            string str = exprt.getFirstName() + " " + exprt.getLastName();
            this.lbl_Text.Text = "Expert: " + str;
            //Mettre le label au centre
            this.lbl_Text.Location = new Point((958 - 29 - lbl_Text.Width) / 2, lbl_Text.Top);
            Objects = new List<Object3d>();

            tmr_CheckNotification.Enabled = true;

            this.SCREEN_WIDTH = pic3d.Width;
            this.SCREEN_HEIGHT = pic3d.Height;

            Children = new Stack();
        }

        private Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public static List<Image> lstMarkerImageList = new List<Image>();
        public static int CurrentImage = 0;
        public void LoadMarkerImages(String[] paths)
        {
            lstMarkerImageList.Clear();
            foreach (string path in paths)
            {

                lstMarkerImageList.Add(Image.FromFile(path));
            }
            //ImageList.Add(Image.FromFile(@"D:\Users\zetrix\Desktop\Markers\Top.jpg"));
            //ImageList.Add(Image.FromFile(@"D:\Users\zetrix\Desktop\Markers\Right.jpg"));
            ////ImageList.Add(Image.FromFile(@"D:\Users\zetrix\Desktop\Markers\Foot.jpg"));
            //ImageList.Add(Image.FromFile(@"D:\Users\zetrix\Desktop\Markers\Foot1.jpg"));
            //ImageList.Add(Image.FromFile(@"D:\Users\zetrix\Desktop\Markers\Left.jpg"));
            ////ImageList.Add(Image.FromFile(@"D:\Users\zetrix\Desktop\Markers\Head.jpg"));
            //ImageList.Add(Image.FromFile(@"D:\Users\zetrix\Desktop\Markers\Head1.jpg"));
        }

        private void btn_NextImage_Click(object sender, EventArgs e)
        {
            CurrentImage = (CurrentImage + 1) % lstMarkerImageList.Count;
        }

        private void btn_PreviousImage_Click(object sender, EventArgs e)
        {
            if (CurrentImage == 0)
                CurrentImage = lstMarkerImageList.Count - 1;
            else
                CurrentImage = (CurrentImage - 1);
        }

        public bool isRunning = true;
        public void getImageFromService()
        {
            try
            {
                byte[] bimg = Helper.service.getImage(this.login);
                Image img = null;
                if (!chkBx_LocalImage.Checked)
                {
                    img = byteToImage(bimg);
                }
                else
                {
                    //img = Image.FromFile(@"D:\Users\zetrix\Desktop\Marker.jpg");
                    img = lstMarkerImageList[CurrentImage];
                }

                Image resizedImg = resizeImage(img, new Size(SCREEN_WIDTH, SCREEN_HEIGHT));

                if (img != null)
                {
                    //lock (this)
                    //this._raster = new NyARBitmapRaster((Bitmap)img);
                    this._raster = new NyARBitmapRaster((Bitmap)resizedImg);
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Problème de connection", "RenderForm.getImageFromService");
                KillRenderThread();
            }
            catch (Exception x)
            {
                //If the app is closed we dont show the message.
                if (x.GetType() == typeof(System.Threading.ThreadAbortException))
                    return;

                if (this.isRunning)
                    MessageBox.Show("La video n'est pas disponible maintenant, essayer plus tard.", "RenderForm.getImageFromService");
                //MessageBox.Show(x.ToString()); 
                KillRenderThread();
                try
                {
                    ChangeVideoStateLabel("Démarrer");
                }
                catch (Exception) { }
            }
        }

        public void ChangeVideoStateLabel(string label)
        {
            if (InvokeRequired)
                Invoke(new changeVideoStatLabel(ChangeVideoStateLabel),label);
            else
            {
                btn_StartVideo.Text = label;
            }
        }
        delegate void changeVideoStatLabel(string label);

        public void KillRenderThread()
        {
            try
            {
                Pause = true;
                RenderThread.Abort();
            }
            catch (Exception) { }
            finally
            {
                RenderThread = null;
            }
        }

        // Initialize the graphics device. Return True if successful.
        public bool InitializeGraphics()
        {

            //Prepare the pictureBox to render objects on it
            m_Device = PrepareD3dDevice(pic3d);

            // Turn on D3D lighting.
            m_Device.RenderState.Lighting = true;

            // Turn on the Z-buffer.
            m_Device.RenderState.ZBufferEnable = true;

            // Cull triangles that are oriented counter clockwise.
            m_Device.RenderState.CullMode = Cull.Clockwise;// CounterClockwise;

            // Make points bigger so they're easy to see.
            m_Device.RenderState.PointSize = 4;

            // Start in solid mode.
            m_Device.RenderState.FillMode = FillMode.Solid;

            // Make the lights.
            SetupLights();

            this._surface = new NyARD3dSurface(m_Device, SCREEN_WIDTH, SCREEN_HEIGHT);

            // We succeeded.

            return true;
        }

        #endregion // D3D Setup Code

        private Device PrepareD3dDevice(Control i_window)
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Flip;
            pp.BackBufferFormat = Format.X8R8G8B8;
            pp.BackBufferCount = 1;
            pp.EnableAutoDepthStencil = true;
            pp.AutoDepthStencilFormat = DepthFormat.D16;
            CreateFlags fl_base = CreateFlags.FpuPreserve;

            try
            {
                return new Device(0, DeviceType.Hardware, i_window.Handle, fl_base | CreateFlags.HardwareVertexProcessing, pp);
            }
            catch (Exception ex1)
            {
                Debug.WriteLine(ex1.ToString());
                try
                {
                    return new Device(0, DeviceType.Hardware, i_window.Handle, fl_base | CreateFlags.SoftwareVertexProcessing, pp);
                }
                catch (Exception ex2)
                {

                    Debug.WriteLine(ex2.ToString());
                    try
                    {
                        return new Device(0, DeviceType.Reference, i_window.Handle, fl_base | CreateFlags.SoftwareVertexProcessing, pp);
                    }
                    catch (Exception ex3)
                    {
                        //MessageBox.Show(ex3.ToString(), "RenderForm.Prepare3dDevice");
                        throw ex3;
                    }
                }
            }
        }

        Vector3 camera_position = new Vector3(0, 0, -10.0f);
        public void AR_Initialization(Control topLevelForm)
        {
            //Camera parameters
            NyARParam ap = NyARParam.createFromARParamFile(new StreamReader(AR_CAMERA_FILE));
            ap.changeScreenSize(SCREEN_WIDTH, SCREEN_HEIGHT);

            //List of patterns
            ar_code = new NyARCode[Constante.PATT_MAX];

            //Pattern files
            AR_CODE_FILES = new String[Constante.PATT_MAX];

            //Pattern index list
            ar_code_index = new int[Constante.PATT_MAX];

            AR_CODE_FILES[0] = "data/patt_U.dat";
            AR_CODE_FILES[1] = "data/patt_S.dat";
            AR_CODE_FILES[2] = "data/patt_T.dat";
            AR_CODE_FILES[3] = "data/patt_H.dat";
            AR_CODE_FILES[4] = "data/patt_B.dat";


            for (int count = 0; count < Constante.PATT_MAX; count++)
                ar_code[count] = NyARCode.createFromARPattFile(new StreamReader(AR_CODE_FILES[count]), 16, 16);

            marker_width = new double[Constante.PATT_MAX];
            for (int count = 0; count < Constante.PATT_MAX; count++)
                marker_width[count] = 80.0f;


            this._ar = new NyARDetectMarker(ap, ar_code, marker_width, Constante.PATT_MAX);

            this._ar.setContinueMode(true);

            Matrix tmp = new Matrix();
            NyARD3dUtil.toCameraFrustumRH(ap, 1, 2000, ref tmp);
            m_Device.Transform.Projection = tmp;

            // View Matrix:
            Vector3 camera_position = new Vector3(0, 0, -500);
            camera_position.Normalize();
            //camera_position.Multiply(m_Range);

            m_Device.Transform.View = Matrix.LookAtLH(
                camera_position, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            //m_Device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, SCREEN_WIDTH / SCREEN_HEIGHT, 1, 2000);
            Viewport vp = new Viewport();
            vp.X = 0;
            vp.Y = 0;
            //vp.Height = ap.getScreenSize().h;
            //vp.Width = ap.getScreenSize().w;
            vp.Height = SCREEN_HEIGHT;
            vp.Width = SCREEN_WIDTH;
            vp.MaxZ = 1.0f;

            m_Device.Viewport = vp;

            this._surface = new NyARD3dSurface(m_Device, SCREEN_WIDTH, SCREEN_HEIGHT);

            NyARDoubleMatrix44 nyar_transmat = this.__OnBuffer_nyar_transmat;

            int nb_marker_detected = 0;
            nb_marker_detected = this._ar.detectMarkerLite(this._raster, Constante.binarisation);

            if (nb_marker_detected > 0)
            {
                bestConfidence = 0;
                bestMarker = 0;

                //if the number of detected markers is bigger than the max number available we set it back to MAX number
                if (nb_marker_detected > Constante.MARK_MAX)
                    nb_marker_detected = Constante.MARK_MAX;

                //get the best confidence from the detected markers
                for (int count = 0; count < nb_marker_detected; count++)
                {
                    ar_code_index[count] = this._ar.getARCodeIndex(count);
                    if (this._ar.getConfidence(count) > bestConfidence)
                    {
                        bestConfidence = this._ar.getConfidence(count);
                        bestMarker = count;
                    }
                    //textBox1.Text += "bestConfidence: " + bestConfidence + "  bestMarker: " + bestMarker+"\n";
                }

                //textBox1.Text += "finally:\nbestConfidence: " + bestConfidence + "  bestMarker: " + bestMarker+"\n\n";

                try
                {
                    //MessageBox.Show("bestMarker: " + bestMarker, "RenderForm.AR_Initialization");
                    this._ar.getTransmationMatrix(bestMarker, nyar_transmat);
                }
                catch (Exception x)
                { //MessageBox.Show(x.ToString(), "RenderForm.AR_Initialize"); 
                }
                NyARD3dUtil.toD3dCameraView(nyar_transmat, 1f, ref this._trans_mat);
            }

            this._nb_marker_detected = nb_marker_detected;

            {
                try
                {
                    this._surface.setRaster(this._raster);
                }
                catch (Exception x)
                {
                    //MessageBox.Show(x.ToString(), "RenderForm.AR_Initialization");
                }
            }
        }

        #region "D3D Drawing Code"
        // Draw.
        public void Render()
        {
            if (!Pause)
            {
                //get the image from web service
                getImageFromService();
            }

            //setup world matrices and get the transformation matrix depending on the detected marker
            try
            {
                AR_Initialization(pic3d);
            }
            catch (Exception x)
            {
                //MessageBox.Show(x.ToString(), "RenderForm.Render");
            }

            //lock (this)
            {
                // Clear the back buffer.
                m_Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, (Color)(new ColorConverter()).ConvertFromString("#a0a0a0"), 1, 0);

                //Overlay the camera view and the 3d object view
                Surface dest_surface = m_Device.GetBackBuffer(0, 0, BackBufferType.Mono);
                Rectangle src_dest_rect = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);
                try
                {
                    m_Device.StretchRectangle((Surface)this._surface, src_dest_rect, dest_surface, src_dest_rect, TextureFilter.Linear);
                }
                catch (Exception x)
                {
                    //MessageBox.Show(x.ToString(), "RenderForm.Render");
                }


                // Make a scene.
                m_Device.BeginScene();

                //textBox1.Text += _nb_marker_detected.ToString();
                if (this._nb_marker_detected > 0 && this._ar.getConfidence(0) > 0.6)
                {

                    Matrix translation = new Matrix();

                    //changement de repere
                    //The best marker is U
                    if (ar_code_index[bestMarker] == 0)
                    {
                        float x = 14.0f * Constante.translateFactor;
                        float y = 0.0f * Constante.translateFactor;
                        float z = 0.0f * Constante.translateFactor;
                        translation = Matrix.Translation(x, -z, y);
                        translation = Matrix.Translation(0, 0, 0);
                    }
                    //The best marker is S
                    else
                        if (ar_code_index[bestMarker] == 1)
                        {
                            float x = 4.75f * Constante.translateFactor;
                            float y = -5.0f * Constante.translateFactor;
                            float z = 0.0f * Constante.translateFactor;
                            translation = Matrix.Translation(x, -z, y);
                        }
                        else//The best marker is T
                            if (ar_code_index[bestMarker] == 2)
                            {
                                translation = Matrix.Translation(0.0f, 0.0f, 0.0f);
                            }
                            /*
                             * gl.glTranslatef(-7.25f, 4.25f, 0);
                                gl.glTranslatef(-9.0f, 0, 2.5f);
                             */
                            //The best marker is H
                            else
                                if (ar_code_index[bestMarker] == 3)
                                {
                                    float x = -7.25f * Constante.translateFactor;
                                    float y = 4.25f * Constante.translateFactor;
                                    float z = 0.0f * Constante.translateFactor;
                                    translation = Matrix.Translation(x, -z, y);
                                }
                                //The best marker is B
                                else if (ar_code_index[bestMarker] == 4)
                                {
                                    float x = -9.0f * Constante.translateFactor;
                                    float y = 0.0f * Constante.translateFactor;
                                    float z = 2.5f * Constante.translateFactor;
                                    translation = Matrix.Translation(x, -z, y);
                                }

                    //m_Device.Transform.World = Matrix.Scaling(41.6f, 41.6f, 41.6f) * this._trans_mat;

                    //DrawGrid();
                    //Draw all the objects
                    if (Objects != null && Objects.Count > 0)
                        for (int count = 0; count < Objects.Count; count++)
                        {
                            Matrix trans = Matrix.Translation(Objects[count].getPosition().X, Objects[count].getPosition().Y,
                                Objects[count].getPosition().Z);

                            Matrix rotX = Matrix.RotationX(Objects[count].getRotation().X);
                            Matrix rotY = Matrix.RotationY(Objects[count].getRotation().Y);
                            Matrix rotZ = Matrix.RotationZ(Objects[count].getRotation().Z);
                            Matrix scl = Matrix.Scaling(Objects[count].getScale().X, Objects[count].getScale().Y, Objects[count].getScale().Z);

                            Matrix m = Matrix.Translation(Objects[count].getPosition().X, 0, 0);

                            m_Device.Transform.World = scl * rotX * rotY * rotZ * trans * translation * this._trans_mat;
                            //m_Device.Transform.World = /*Matrix.Scaling(41.6f, 41.6f, 41.6f) * */scl * rotX * rotY * rotZ * trans /* *Matrix.RotationX((float)Math.PI / 2) */* this._trans_mat;
                            //m_Device.Transform.World = scl * rotX * rotY * rotZ * this._trans_mat * trans;

                            Objects[count].Draw();
                        }
                }

                // End the scene and display.
                m_Device.EndScene();
                m_Device.Present();
            }
            Thread.Sleep(500);
        }

        private void DrawGrid()
        {

        }

        // Setup the world, view, and projection matrices.
        private void SetupMatrices()
        {

            // View Matrix:
            Vector3 camera_position = new Vector3(0, 0, 500.0f);
            camera_position.Normalize();
            //camera_position.Multiply(m_Range);

            m_Device.Transform.View = (Matrix.LookAtLH(
                camera_position,
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0)));

            m_Device.Transform.Projection = (Matrix.PerspectiveFovLH((float)(Math.PI / 4), 640 / 480, 1, 2000));
        }

        // Make the lights.
        private void SetupLights()
        {
            //m_Device.Lights[0].Type = LightType.Directional;
            //m_Device.Lights[0].Diffuse = Color.White;
            //m_Device.Lights[0].Ambient = Color.White;
            //m_Device.Lights[0].Direction = new Vector3(0, -1, 0);
            //m_Device.Lights[0].Enabled = true;

            //m_Device.Lights[1].Type = LightType.Directional;
            //m_Device.Lights[1].Diffuse = Color.White;
            //m_Device.Lights[1].Ambient = Color.White;
            //m_Device.Lights[1].Direction = new Vector3(1, -10, 1);
            //m_Device.Lights[1].Enabled = true;

            //m_Device.RenderState.Ambient = Color.White;

            //// Provides main directional lighting.
            //m_Device.Lights[0].Type = LightType.Directional;
            //m_Device.Lights[0].Position = new Vector3(5, 2, 1);

            //m_Device.Lights[0].Direction = new Vector3(0f, 0f, 0f);
            //m_Device.Lights[0].Diffuse = Color.LightBlue;
            //m_Device.Lights[0].Update();

            //// Provides frontal lighting.
            //m_Device.Lights[1].Type = LightType.Directional;
            //m_Device.Lights[1].Direction = new Vector3(0.0f, -1.0f, -3.0f);
            //m_Device.Lights[1].Diffuse = Color.DarkSlateGray;
            //m_Device.Lights[1].Update();

            //// Turn on the lights.
            //m_Device.Lights[0].Enabled = true;
            //m_Device.Lights[1].Enabled = true;
            //// Turn off the light representing the engine.
            //m_Device.Lights[2].Enabled = false;



            m_Device.RenderState.Lighting = true;

            m_Device.Lights[0].Type = LightType.Directional;
            m_Device.Lights[0].Diffuse = Color.White;
            m_Device.Lights[0].Direction = new Vector3(1, 1, -1);
            m_Device.Lights[0].Update();
            m_Device.Lights[0].Enabled = true;

            m_Device.Lights[1].Type = LightType.Directional;
            m_Device.Lights[1].Diffuse = Color.White;
            m_Device.Lights[1].Direction = new Vector3(-1, -1, -1);
            m_Device.Lights[1].Update();
            m_Device.Lights[1].Enabled = true;
        }

        Vector3 light = new Vector3(5, 2, 1);
        float lt = 5;
        private void button1_Click_1(object sender, EventArgs e)
        {
            lt += 5;
            light = new Vector3(lt, 2, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lt -= 5;
            light = new Vector3(lt, 2, 1);
        }

        #endregion // D3D Drawing Code

        List<Image> lstCloseBtnImgs;
        List<Image> lstMinimizeBtnImgs;
        List<Image> lstAddProcedureBtnImgs;
        List<Image> lstUpdateProcedureBtnImgs;
        List<Image> lstSendProcedureBtnImgs;
        List<Image> lstStartVideoBtnImgs;

        private void RenderForm_Load(object sender, EventArgs e)
        {
            lstCloseBtnImgs = new List<Image>();
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close.png"));
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close-Mouse hover.png"));
            lstCloseBtnImgs.Add(Image.FromFile("images\\Close-Mouse click.png"));

            lstMinimizeBtnImgs = new List<Image>();
            lstMinimizeBtnImgs.Add(Image.FromFile("images\\Minimize.png"));
            lstMinimizeBtnImgs.Add(Image.FromFile("images\\Minimize-Mouse hover.png"));
            lstMinimizeBtnImgs.Add(Image.FromFile("images\\Minimize-Mouse click.png"));
            //
            //Add Procedure button
            //
            lstAddProcedureBtnImgs = new List<Image>();
            lstAddProcedureBtnImgs.Add(Image.FromFile("images\\AddProcedure.png"));
            lstAddProcedureBtnImgs.Add(Image.FromFile("images\\AddProcedure-hover.png"));
            lstAddProcedureBtnImgs.Add(Image.FromFile("images\\AddProcedure-click.png"));
            //
            //Update Procedure button
            //
            lstUpdateProcedureBtnImgs = new List<Image>();
            lstUpdateProcedureBtnImgs.Add(Image.FromFile("images\\UpdateProcedure.png"));
            lstUpdateProcedureBtnImgs.Add(Image.FromFile("images\\UpdateProcedure-hover.png"));
            lstUpdateProcedureBtnImgs.Add(Image.FromFile("images\\UpdateProcedure-click.png"));
            //
            //Send Procedure button
            //
            lstSendProcedureBtnImgs = new List<Image>();
            lstSendProcedureBtnImgs.Add(Image.FromFile("images\\SendProcedure.png"));
            lstSendProcedureBtnImgs.Add(Image.FromFile("images\\SendProcedure-hover.png"));
            lstSendProcedureBtnImgs.Add(Image.FromFile("images\\SendProcedure-click.png"));
            //
            //Start video button
            //
            lstStartVideoBtnImgs = new List<Image>();
            lstStartVideoBtnImgs.Add(Image.FromFile("images\\StartVideo.png"));
            lstStartVideoBtnImgs.Add(Image.FromFile("images\\StartVideo-hover.png"));
            lstStartVideoBtnImgs.Add(Image.FromFile("images\\StartVideo-click.png"));
        }

        public Image byteToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        private void RenderForm_SizeChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usrProcedure usr = new usrProcedure(this, null, null, null, Operation.AddNew);
            usr.Location = new Point(5, 80);
            this.Controls.Add(usr);

            //Object3d obj3d = new Object3d(6, "Arrows2", "sign",
            //    new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f),
            //    m_Device);

            //obj3d.setNb(0);
            //obj3d.LoadMesh();
            //Objects.Add(obj3d);
            this.Text = this.Size.ToString();
            //pic3d.Width--;

            //MessageBox.Show(this._trans_mat.ToString());

            m_Device.Transform.View = Matrix.LookAtLH(
                camera_position, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
        }

        public void LoadRichTxt(string msg, string tech)
        {
            if (InvokeRequired)
                Invoke(new LoadToRichTxt(LoadRichTxt), msg, tech);
            else
            {
                richTextBox1.Text += tech + ":\n";
                //richLegnth = (tech + "\n").Length;
                //richTextBox1.Select(richStart, richLegnth);
                //richTextBox1.SelectionColor = Color.OrangeRed;
                //richTextBox1.SelectionFont = new System.Drawing.Font("Calibri", 9, FontStyle.Bold);
                //richStart += richLegnth;

                richTextBox1.Text += msg + "\n";

                //richLegnth = msg.Length;
                //richTextBox1.Select(richStart, richLegnth + 1);
                //richTextBox1.SelectionColor = Color.Black;
                //richStart += richLegnth;
            }
        }
        delegate void LoadToRichTxt(string msg, string tech);

        public void NotifOn(string path)
        {
            if (InvokeRequired)
                Invoke(new NotifDelegate(NotifOn), path);
            else
            {
                picBx_Notif.Image = Image.FromFile(path);
            }
        }

        public void NotifOff(string path)
        {
            if (InvokeRequired)
                Invoke(new NotifDelegate(NotifOff), path);
            else
            {
                picBx_Notif.Image = Image.FromFile(path);
            }
        }
        delegate void NotifDelegate(string path);

        bool videolunched = false;
        bool NotifSound = false;
        bool NotifSound2 = false;

        private void tmr_CheckNotification_Tick(object sender, EventArgs e)
        {
            try
            {
                //Check the web service for notifications.
                Object[] notifInfo = Helper.service.getNotificationInfo(this.login);
                if (((int)notifInfo[0]) == NotificationExists || ((int)notifInfo[0]) == NotificationApproved)//Notification Exists.
                {
                    lbl_Notifications.Text = "Le technicien " + notifInfo[1].ToString()/* + "  " + notifInfo[2] */+ " a besoin de l'aide:\n";
                    lbl_Notifications.Text += "Type de panne: " + notifInfo[3].ToString() + "\n";
                    lbl_Notifications.Text += "Panne: " + notifInfo[4].ToString() + "\n";
                    lbl_Notifications.Text += "Diagnostique:\n" + notifInfo[5].ToString();
                    
                    NotifOn("images\\notification_warning.png");

                    if (!NotifSound || !NotifSound2)
                    {
                        Audio notify = new Audio("Data\\Notify.mp3");
                        notify.Play();
                        NotifSound = true;
                        NotifSound2 = true;
                    }

                    if ((bool)notifInfo[6] && !videolunched)
                    {
                        Thread.Sleep(2000);
                        btn_StartVideo_Click(null, EventArgs.Empty);
                        videolunched = true;
                    }

                    //if ((bool)notifInfo[6])
                    //    MessageBox.Show("La video est prete.","RenderForm.tmr_CheckNotification");
                    //tmr_CheckNotification.Enabled = false;
                }
                else
                {
                    lbl_Notifications.Text = "";
                    NotifOff("images\\notification_warning_Gray.png");
                    NotifSound2 = false;
                    if (!(bool)notifInfo[6] && videolunched)
                    {
                        videolunched = false;
                        ChangeVideoStateLabel("Démarrer");
                        btn_StartVideo_Click(null, EventArgs.Empty);
                        //NotifOff("images\\notification_warning_Gray.png");
                    }
                }

                #region Check for Messages from the technician
                if (Helper.service.ExisteMsgFromTech(this.login))
                {
                    Audio notify = new Audio("Data\\msg.mp3");
                    notify.Play();
                    string msg = Helper.service.ReceiveMsgFromTech(this.login);
                    //LoadRichTxt(msg,notifInfo[1].ToString());
                    LoadRichTxt(msg, "Abdelalim ZERKANI");
                    
                }

                #endregion
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString(), "RenderForm.tmr_CheckNotification");
            }
        }

        private void RenderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Helper.service.DisconnectExpert(this.login);
                isRunning = false;
                KillRenderThread();
            }
            catch (Exception) {}
        }

        private void accepterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Helper.service.getNotificationState(this.login) == 1)
            {
                Helper.service.ApproveNotification(this.login);
                utiliserLeFlusDeVideoToolStripMenuItem_Click(null, EventArgs.Empty);
            }
        }


        private void refuserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Helper.service.getNotificationState(this.login) == 1)
            {
                Helper.service.CancelNotification(this.login);
                NotifSound2 = false;
            }
        }

        Thread RenderThread;
        private void btn_StartVideo_Click(object sender, EventArgs e)
        {
            if(btn_StartVideo.Text == "Démarrer")
            {
                if (RenderThread == null)
                {
                    RenderThread = new Thread(new ThreadStart(RenderThreadStart));
                    RenderThread.Start();
                }
                btn_StartVideo.Text = "Arrêter";
                Pause = false;
            }
            else
            {
                Pause = true;
                btn_StartVideo.Text = "Démarrer";
                //RenderThread.Abort();
                //RenderThread = null;
            }
        }

        public void RenderThreadStart()
        {
            while (this.Created && this.isRunning)
            {
                Render();
                Application.DoEvents();
            }
        }

        private void btn_AddProcedure_Click(object sender, EventArgs e)
        {
            RenderForm.Objects.Clear();
            //frm_AddProcedure frm = new frm_AddProcedure(null, null, null, Operation.AddNew);
            //frm.ShowDialog();
            //if (frm.Tag != null)
            //    this.CreatedProcedure = (Procedure)frm.Tag;
            usrProcedure proc = new usrProcedure(this, null, null, null, Operation.AddNew);
            proc.Location = new Point(5, 115);
            this.Controls.Add(proc);
        }

        private void btn_UpdateProcedure_Click(object sender, EventArgs e)
        {
            frm_ListOf frm = new frm_ListOf(Subject.ProcedureSbjct);
            frm.ShowDialog();

            if (frm.Tag != null)
            {
                Procedure proc = (Procedure)frm.Tag;

                foreach (Etape etp in Etape.GetByProcedure(proc.getId()))
                {
                    proc.addEtape(etp);
                    //MessageBox.Show("etp.Objects.count: "+etp.getObjectList().Count,"RenderForm.btn_UpdateProcedure");
                }


                //frm_AddProcedure frmProc = new frm_AddProcedure(proc.getPanne().getTypePanne(), proc.getPanne(), proc, Operation.Update);
                //frmProc.ShowDialog();
                usrProcedure usrproc = new usrProcedure(this, proc.getPanne().getTypePanne(), proc.getPanne(), proc, Operation.Update);
                usrproc.Location = new Point(5, 115);
                this.Controls.Add(usrproc);
            }
        }

        private void btn_SendProcedure_Click(object sender, EventArgs e)
        {
            //frm_ListOf frm = new frm_ListOf(Subject.ProcedureSbjct);
            //frm.ShowDialog();

            //if (frm.Tag != null)
            {
                //Procedure proc = (Procedure)frm.Tag;
                //Helper.service.SendProcedure(login, proc.getId());
                if (this.CreatedProcedure != null)
                {
                    Helper.service.SendProcedure(login, this.CreatedProcedure.getId());
                    MessageBox.Show(this, "Procedure envoyée avec succès.", "Message d'information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.CreatedProcedure = null;
                }
                else
                {
                    DialogResult dr = MessageBox.Show(this, "Vous n'avez pas créé des procedures, voullez vous choisir une?.", "Message d'erreur", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dr == DialogResult.Yes)
                    {
                        frm_ListOf frm = new frm_ListOf(Subject.ProcedureSbjct);
                        frm.ShowDialog();
                        if (frm.Tag != null)
                        {
                            Procedure proc = (Procedure)frm.Tag;
                            Helper.service.SendProcedure(login, proc.getId());
                            MessageBox.Show(this, "Procedure envoyée avec succès.", "Message d'information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void btn_MoveCameraForward_Click(object sender, EventArgs e)
        {
            camera_position = new Vector3(0, 0, camera_position.Z + 10);
            lbl_CameraLocation.Text = "Camera Location: X." + camera_position.X + " Y." + camera_position.Y + " Z." + camera_position.Z;
        }

        private void btn_MoveCameraBackward_Click(object sender, EventArgs e)
        {
            camera_position = new Vector3(0, 0, camera_position.Z - 10);
            lbl_CameraLocation.Text = "Camera Location: X." + camera_position.X + " Y." + camera_position.Y + " Z." + camera_position.Z;
        }

        private void RenderForm_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control.GetType() == typeof(usrProcedure))
            {
                btn_AddProcedure.Enabled = false;
                btn_UpdateProcedure.Enabled = false;
                btn_SendProcedure.Enabled = false;
            }

            Children.Push(e.Control);
        }

        private void RenderForm_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control.GetType() == typeof(usrProcedure))
            {
                btn_AddProcedure.Enabled = true;
                btn_UpdateProcedure.Enabled = true;
                btn_SendProcedure.Enabled = true;
            }

            Children.Pop();
            try
            {
                ((Control)Children.Peek()).Show();
            }
            catch (Exception) { }
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

        #region AddProcedure button design and behaviour

        private void btn_AddProcedure_MouseEnter(object sender, EventArgs e)
        {
            btn_AddProcedure.Image = lstAddProcedureBtnImgs[1];
        }

        private void btn_AddProcedure_MouseLeave(object sender, EventArgs e)
        {
            btn_AddProcedure.Image = lstAddProcedureBtnImgs[0];
        }

        private void btn_AddProcedure_MouseDown(object sender, MouseEventArgs e)
        {
            btn_AddProcedure.Image = lstAddProcedureBtnImgs[2];
        }

        private void btn_AddProcedure_MouseUp(object sender, MouseEventArgs e)
        {
            btn_AddProcedure.Image = lstAddProcedureBtnImgs[0];
        }

        #endregion

        #region UpdateProcedure button design and behaviour

        private void btn_UpdateProcedure_MouseEnter(object sender, EventArgs e)
        {
            btn_UpdateProcedure.Image = lstUpdateProcedureBtnImgs[1];
        }

        private void btn_UpdateProcedure_MouseLeave(object sender, EventArgs e)
        {
            btn_UpdateProcedure.Image = lstUpdateProcedureBtnImgs[0];
        }

        private void btn_UpdateProcedure_MouseDown(object sender, MouseEventArgs e)
        {
            btn_UpdateProcedure.Image = lstUpdateProcedureBtnImgs[2];
        }

        private void btn_UpdateProcedure_MouseUp(object sender, MouseEventArgs e)
        {
            btn_UpdateProcedure.Image = lstUpdateProcedureBtnImgs[0];
        }

        #endregion

        #region SendProcedure button design and behaviour

        private void btn_SendProcedure_MouseEnter(object sender, EventArgs e)
        {
            btn_SendProcedure.Image = lstSendProcedureBtnImgs[1];
        }

        private void btn_SendProcedure_MouseLeave(object sender, EventArgs e)
        {
            btn_SendProcedure.Image = lstSendProcedureBtnImgs[0];
        }

        private void btn_SendProcedure_MouseDown(object sender, MouseEventArgs e)
        {
            btn_SendProcedure.Image = lstSendProcedureBtnImgs[2];
        }

        private void btn_SendProcedure_MouseUp(object sender, MouseEventArgs e)
        {
            btn_SendProcedure.Image = lstSendProcedureBtnImgs[0];
        }

        #endregion

        #region SendProcedure button design and behaviour

        private void btn_StartVideo_MouseEnter(object sender, EventArgs e)
        {
            btn_StartVideo.Image = lstStartVideoBtnImgs[1];
        }

        private void btn_StartVideo_MouseLeave(object sender, EventArgs e)
        {
            btn_StartVideo.Image = lstStartVideoBtnImgs[0];
        }

        private void btn_StartVideo_MouseDown(object sender, MouseEventArgs e)
        {
            btn_StartVideo.Image = lstStartVideoBtnImgs[2];
        }

        private void btn_StartVideo_MouseUp(object sender, MouseEventArgs e)
        {
            btn_StartVideo.Image = lstStartVideoBtnImgs[0];
        }

        #endregion

        private void trkbr_CompressionRate_Scroll(object sender, EventArgs e)
        {
            Helper.service.SendCompressionRate(this.login, (float)trkbr_CompressionRate.Value);
        }

        private void pnl_Top_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Helper.ReleaseCapture();
                Helper.SendMessage(Handle, Helper.WM_NCLBUTTONDOWN, Helper.HT_CAPTION, 0);
            }
        }

        private void trkbr_BinarisationRate_Scroll(object sender, EventArgs e)
        {
            Constante.binarisation = trkbr_BinarisationRate.Value;
        }

        private void LoadImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Images locales
            if (RenderThread != null && RenderThread.ThreadState == System.Threading.ThreadState.Running)
            {
                KillRenderThread();
                ChangeVideoStateLabel("Démarrer");
            }

            chkBx_LocalImage.Checked = true;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadMarkerImages(ofd.FileNames);
                hrToolStripMenuItem_Click(null, EventArgs.Empty);
                btn_StartVideo_Click(null, EventArgs.Empty);
            }
        }

        private void hrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Images locales
            if (lstMarkerImageList.Count == 0)
            {
                LoadImagesToolStripMenuItem_Click(null, EventArgs.Empty);
            }
            chkBx_LocalImage.Checked = true;

            UseLocalImageToolStripMenuItem.Checked = true;
            UseVideoFlowToolStripMenuItem.Checked = false;

            btn_NextImage.Visible = true;
            btn_PreviousImage.Visible = true;
        }

        private void utiliserLeFlusDeVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Flux de video
            chkBx_LocalImage.Checked = false;

            UseLocalImageToolStripMenuItem.Checked = false;
            UseVideoFlowToolStripMenuItem.Checked = true;

            btn_NextImage.Visible = false;
            btn_PreviousImage.Visible = false;
        }

        private void netoyerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Objects != null && Children.Count == 0)
                Objects.Clear();
        }

        private void markerUtestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Images locales
            if (RenderThread != null && RenderThread.ThreadState == System.Threading.ThreadState.Running)
            {
                KillRenderThread();
                ChangeVideoStateLabel("Démarrer");
            }

            chkBx_LocalImage.Checked = true;
            lstMarkerImageList.Add(Image.FromFile(@"D:\Users\zetrix\Desktop\Marker_U.jpg"));
            hrToolStripMenuItem_Click(null, EventArgs.Empty);
            btn_StartVideo_Click(null, EventArgs.Empty);

        }

        private void supprimerProcedureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ListOf frm = new frm_ListOf(Subject.ProcedureSbjct);
            frm.ShowDialog();

            if (frm.Tag != null)
            {
                Procedure proc = (Procedure)frm.Tag;

                Manager.XMLResourceManager.XMLProcedure.Delete(proc.getId());
                Manager.XMLResourceManager.XMLStep.RemoveByProcedure(proc.getId());
                MessageBox.Show(this, "Opération terminée avec succès.", "Message d'information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pause = true;
            ChangeVideoStateLabel("Démarrer");
            btn_StartVideo_Click(null,EventArgs.Empty);
            lstMarkerImageList.Clear();
        }

        int richStart = 0;
        //int richLegnth = 0;
        private void btn_SendMsg_Click(object sender, EventArgs e)
        {
            try
            {
                Helper.service.SendMsgToTech(txtBx_SendMsg.Text,this.login);

                richTextBox1.Text += "Moi:\n";
                //richLegnth = "Moi:\n".Length;
                //MessageBox.Show("start: " + richStart + " legnth: " + richLegnth);
                //richTextBox1.Select(richStart, richLegnth);
                //MessageBox.Show("startRch: " + richTextBox1.SelectionStart + " legnthRch: " + richTextBox1.SelectionLength);
                //richTextBox1.SelectionColor = Color.Blue;
                //richTextBox1.SelectionFont = new System.Drawing.Font("Calibri", 9, FontStyle.Bold);
                
                //MessageBox.Show("value: "+richTextBox1.Text.Substring(richStart,richLegnth));
                //richStart += richLegnth;

                //richTextBox1.ForeColor = Color.Black;
                richTextBox1.Text += txtBx_SendMsg.Text+"\n";

                //richLegnth = txtBx_SendMsg.Text.Length;
                //MessageBox.Show("start: " + richStart + "legnth: " + richLegnth);
                //richTextBox1.Select(richStart, richLegnth+1);
                //richTextBox1.SelectionColor = Color.Black;
                //richTextBox1.SelectionFont = new System.Drawing.Font("Calibri", 8, FontStyle.Bold);
                //richStart += richLegnth;

                txtBx_SendMsg.Text = "";
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void txtBx_SendMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btn_SendMsg_Click(null, EventArgs.Empty);
        }
    }
}