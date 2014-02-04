﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using jp.nyatla.nyartoolkit.cs.core;
using jp.nyatla.nyartoolkit.cs.markersystem;
using NyARToolkitCSUtils;
using NyARToolkitCSUtils.Direct3d;
using NyARToolkitCSUtils.Capture;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MarkerPlane
{
    /// <summary>
    /// このサンプルプログラムは、マウスカーソルの延長線上にあるマーカ平面に立方体を描画します。
    /// </summary>
    class Sketch : D3dSketch
    {
        private const int SCREEN_WIDTH = 640;
        private const int SCREEN_HEIGHT = 480;
        private const String AR_CODE_FILE = "../../../../../data/patt.hiro";

        private NyARD3dMarkerSystem _ms;
        private NyARDirectShowCamera _ss;
        private NyARD3dRender _rs;
        private int mid;
        public override void setup(CaptureDevice i_cap)
        {
            Device d3d = this.size(SCREEN_WIDTH, SCREEN_HEIGHT);
            i_cap.PrepareCapture(SCREEN_WIDTH, SCREEN_HEIGHT, 30.0f);
            INyARMarkerSystemConfig cf = new NyARMarkerSystemConfig(SCREEN_WIDTH, SCREEN_HEIGHT);
            d3d.RenderState.ZBufferEnable = true;
            d3d.RenderState.Lighting = false;
            d3d.RenderState.CullMode = Cull.CounterClockwise;
            this._ms = new NyARD3dMarkerSystem(cf);
            this._ss = new NyARDirectShowCamera(i_cap);
            this._rs = new NyARD3dRender(d3d, this._ms);
            this.mid = this._ms.addARMarker(AR_CODE_FILE, 16, 25, 80);

            //set View mmatrix
            this._rs.loadARViewMatrix(d3d);
            //set Viewport matrix
            this._rs.loadARViewPort(d3d);
            //setD3dProjectionMatrix
            this._rs.loadARProjectionMatrix(d3d);
            this._ss.start();
        }

        public override void loop(Device i_d3d)
        {
            lock (this._ss)
            {
                this._ms.update(this._ss);
                this._rs.drawBackground(i_d3d, this._ss.getSourceImage());
                i_d3d.BeginScene();
                i_d3d.Clear(ClearFlags.ZBuffer, Color.DarkBlue, 1.0f, 0);
                if (this._ms.isExistMarker(this.mid))
                {
                    //get marker plane pos from Mouse X,Y
                    Point p=this.form.PointToClient(Cursor.Position);
                    Vector3 mp = new Vector3();
                    this._ms.getMarkerPlanePos(this.mid, p.X,p.Y,ref mp);
                    mp.Z = 20.0f;
                    //立方体の平面状の位置を計算
                    Matrix transform_mat2 = Matrix.Translation(mp);
                    //変換行列を掛ける
                    transform_mat2 *= this._ms.getD3dMarkerMatrix(this.mid);
                    // 計算したマトリックスで座標変換
                    i_d3d.SetTransform(TransformType.World, transform_mat2);
                    // レンダリング（描画）
                    this._rs.colorCube(i_d3d, 40);
                }
                i_d3d.EndScene();
            }
            i_d3d.Present();
        }
        public override void cleanup()
        {
            this._rs.Dispose();
        }
        static void Main(string[] args)
        {
            new Sketch().run();

        }
    }
}
