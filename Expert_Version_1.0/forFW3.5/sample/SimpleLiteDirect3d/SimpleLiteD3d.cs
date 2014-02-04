/* 
 * Capture Test NyARToolkitCSサンプルプログラム
 * --------------------------------------------------------------------------------
 * The MIT License
 * Copyright (c) 2008 nyatla
 * airmail(at)ebony.plala.or.jp
 * http://nyatla.jp/nyartoolkit/
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 */
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

using SimpleLiteDirect3d.marsService;

namespace SimpleLiteDirect3d
{

    public partial class SimpleLiteD3d : IDisposable, CaptureListener
    {
        Service1 service = new Service1();
        private const int SCREEN_WIDTH = 640;
        private const int SCREEN_HEIGHT = 480;

        //private const int SCREEN_WIDTH = 320;
        //private const int SCREEN_HEIGHT = 240;
        private const String AR_CODE_FILE = "../../../../../data/patt.hiro";
        private const String AR_CAMERA_FILE = "../../../../../data/camera_para.dat";

        private CaptureDevice  _cap;
        //NyAR
        private NyARSingleDetectMarker _ar;
        //private DsRgbRaster _raster;
        private NyARBitmapRaster _raster;
        
        private NyARD3dSurface _surface;
        
        private Device _device = null;
        private ColorCube _cube;

        private NyARDoubleMatrix44 __OnBuffer_nyar_transmat = new NyARDoubleMatrix44();
        private bool _is_marker_enable;
        private Matrix _trans_mat;



        public Image byteToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }


        
        public void OnBuffer(CaptureDevice i_sender, double i_sample_time, IntPtr i_buffer, int i_buffer_len)
        {
            int w = i_sender.video_width;
            int h = i_sender.video_height;
            int s = w * (i_sender.video_bit_count / 8);
            NyARDoubleMatrix44 nyar_transmat = this.__OnBuffer_nyar_transmat;
            
        
            lock (this)
            {
                
                //this._raster.setBuffer(i_buffer,i_buffer_len, i_sender.video_vertical_flip);
                
                #region My Code
                try
                {
                    byte[] bimg = service.getb();
                    //if(bimg != null)
                    {
                        Image img = byteToImage(bimg);
                        if (img != null)
                        {
                            //frm.textBox1.Text = img.ToString();
                            this._raster = new NyARBitmapRaster((Bitmap)img);
                        }
                    }
                    //else
                }
                catch (Exception x)
                {
                    //MessageBox.Show(x.ToString());
                }
                #endregion
                
                
                
                bool is_marker_enable = this._ar.detectMarkerLite(this._raster, 110);
                if (is_marker_enable)
                {
                
                    this._ar.getTransmationMatrix(nyar_transmat);
                    NyARD3dUtil.toD3dCameraView(nyar_transmat, 1f, ref this._trans_mat);
                }
                this._is_marker_enable = is_marker_enable;
                
                this._surface.setRaster(this._raster);
            }
            return;
        }
        
        public void StartCap()
        {
            this._cap.StartCapture();
            return;
        }
        
        public void StopCap()
        {
            this._cap.StopCapture();
            return;
        }


        
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

            try{
                return new Device(0, DeviceType.Hardware, i_window.Handle, fl_base|CreateFlags.HardwareVertexProcessing, pp);
            }catch (Exception ex1){
                Debug.WriteLine(ex1.ToString());
                try{
                    return new Device(0, DeviceType.Hardware, i_window.Handle, fl_base | CreateFlags.SoftwareVertexProcessing, pp);
                }catch (Exception ex2){
                    
                    Debug.WriteLine(ex2.ToString());
                    try{
                        return new Device(0, DeviceType.Reference, i_window.Handle, fl_base | CreateFlags.SoftwareVertexProcessing, pp);
                    }catch (Exception ex3){
                        throw ex3;
                    }
                }
            }
        }

        public bool InitializeApplication(Form1 topLevelForm,CaptureDevice i_cap_device)
        {
            topLevelForm.ClientSize=new Size(SCREEN_WIDTH,SCREEN_HEIGHT);
        
            i_cap_device.SetCaptureListener(this);
            i_cap_device.PrepareCapture(SCREEN_WIDTH, SCREEN_HEIGHT, 30);
            this._cap = i_cap_device;
            
            
            
            //this._raster = new DsRgbRaster(i_cap_device.video_width, i_cap_device.video_height,NyARBufferType.BYTE1D_B8G8R8X8_32);

            #region my code
            try
                {
                    byte[] bimg = service.getb();
                    //if(bimg != null)
                    {
                        Image img = byteToImage(bimg);
                        if (img != null)
                        {
                            //frm.textBox1.Text = img.ToString();
                            this._raster = new NyARBitmapRaster((Bitmap)img);
                        }
                    }
                    //else
                }
                catch (Exception x)
                {
                    //MessageBox.Show(x.ToString());
                }
            #endregion

            
            NyARParam ap = NyARParam.createFromARParamFile(new StreamReader(AR_CAMERA_FILE));
            ap.changeScreenSize(SCREEN_WIDTH, SCREEN_HEIGHT);

            
            NyARCode code = NyARCode.createFromARPattFile(new StreamReader(AR_CODE_FILE),16, 16);

            
            this._ar = NyARSingleDetectMarker.createInstance(ap, code, 80.0, NyARSingleDetectMarker.PF_NYARTOOLKIT);
            
            
            this._ar.setContinueMode(true);

            
            this._device = PrepareD3dDevice(topLevelForm);
            this._device.RenderState.ZBufferEnable = true;
            this._device.RenderState.Lighting = false;


            
            Matrix tmp = new Matrix();
            NyARD3dUtil.toCameraFrustumRH(ap.getPerspectiveProjectionMatrix(),ap.getScreenSize(),1, 10, 10000,ref tmp);
            this._device.Transform.Projection = tmp;

            
            this._device.Transform.View = Matrix.LookAtLH(
                new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 0.0f));
            Viewport vp = new Viewport();
            vp.X = 0;
            vp.Y = 0;
            vp.Height = ap.getScreenSize().h;
            vp.Width = ap.getScreenSize().w;
            vp.MaxZ = 1.0f;
            
            this._device.Viewport = vp;

            
            this._cube = new ColorCube(this._device, 40);


            
            this._surface = new NyARD3dSurface(this._device, SCREEN_WIDTH, SCREEN_HEIGHT);

            this._is_marker_enable = false;
            return true;
        }


        

        public void MainLoop()
        {
            
            lock (this)
            {
                
                Surface dest_surface = this._device.GetBackBuffer(0, 0, BackBufferType.Mono);
                Rectangle src_dest_rect = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);
                this._device.StretchRectangle((Surface)this._surface, src_dest_rect, dest_surface, src_dest_rect, TextureFilter.None);

                
                this._device.BeginScene();
                this._device.Clear(ClearFlags.ZBuffer, Color.DarkBlue, 1.0f, 0);


                
                if (this._is_marker_enable && this._ar.getConfidence()>0.4)
                {


                
                    Matrix transform_mat2 = Matrix.Translation(0,0,20.0f);

                
                    transform_mat2 *= this._trans_mat;
                
                    this._device.SetTransform(TransformType.World, transform_mat2);

                
                    this._cube.draw(this._device);
                }

                
                this._device.EndScene();

                
                this._device.Present();
            }
            return;
        }

        
        public void Dispose()
        {
            lock (this)
            {

        
                if (this._cube != null)
                {
                    this._cube.Dispose();
                }
                if (this._surface != null)
                {
                    this._surface.Dispose();
                }
        
                if (this._device != null)
                {
                    this._device.Dispose();
                }
            }
        }
    }
}
