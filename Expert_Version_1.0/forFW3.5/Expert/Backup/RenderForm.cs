using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;

namespace Cd3dLoadXFile
{
    public partial class RenderForm : Form
    {
        public RenderForm()
        {
            InitializeComponent();

            radSolid.CheckedChanged += new System.EventHandler(FillMode_CheckedChanged);
            radWireframe.CheckedChanged += new System.EventHandler(FillMode_CheckedChanged);
            radPoints.CheckedChanged += new System.EventHandler(FillMode_CheckedChanged);
        }

        // The Direct3D device.
        private Device m_Device;

        #region "D3D Setup Code"

        private int m_NumSubSets;
        private Material[] m_Materials;
        private Texture[] m_Textures;

        // The mesh that holds all of the data.
        Mesh m_Mesh;

        // The location of mesh data files.
        private string m_FilePath;

        // The distance from the origin.
        private float m_Range = 20;

        // Initialize the graphics device. Return True if successful.
        public bool InitializeGraphics()
        {
            PresentParameters parms = new PresentParameters();
            parms.Windowed = true;
            parms.SwapEffect = SwapEffect.Discard;
            parms.EnableAutoDepthStencil = true;            // Depth stencil on.
            parms.AutoDepthStencilFormat = DepthFormat.D16;

            // Best: Hardware device and hardware vertex processing.
            try
            {
                m_Device = new Device(0, DeviceType.Hardware, pic3d,
                    CreateFlags.HardwareVertexProcessing, parms);
                Debug.WriteLine("Hardware, HardwareVertexProcessing");
            }
            catch { }

            // Good: Hardware device and software vertex processing.
            if (m_Device == null)
            {
                try
                {
                    m_Device = new Device(0, DeviceType.Hardware, pic3d,
                        CreateFlags.SoftwareVertexProcessing, parms);
                    Debug.WriteLine("Hardware, SoftwareVertexProcessing");
                }
                catch { }
            }

            // Adequate?: Software device and software vertex processing.
            if (m_Device == null)
            {
                try
                {
                    m_Device = new Device(0, DeviceType.Reference, pic3d,
                        CreateFlags.SoftwareVertexProcessing, parms);
                    Debug.WriteLine("Reference, SoftwareVertexProcessing");
                }
                catch (Exception ex)
                {
                    // If we still can't make a device, give up.
                    MessageBox.Show("Error initializing Direct3D\n\n" + ex.Message,
                        "Direct3D Error", MessageBoxButtons.OK);
                    return false;
                }
            }

            // Turn on D3D lighting.
            m_Device.RenderState.Lighting = true;

            // Turn on the Z-buffer.
            m_Device.RenderState.ZBufferEnable = true;

            // Cull triangles that are oriented counter clockwise.
            m_Device.RenderState.CullMode = Cull.CounterClockwise;

            // Make points bigger so they're easy to see.
            m_Device.RenderState.PointSize = 4;

            // Start in solid mode.
            m_Device.RenderState.FillMode = FillMode.Solid;

            // Make the lights.
            SetupLights();

            // We succeeded.
            return true;
        }

        // Load a mesh from a .x file.
        public void LoadMesh(string file_path, string file_name)
        {
            // Load the mesh.
            if (!file_path.EndsWith("\\")) file_path += "\\";
            ExtendedMaterial[] exmaterials = null;
            m_Mesh = Mesh.FromFile(file_name, MeshFlags.Managed, m_Device, out exmaterials);

            // Load the textures and materials.
            m_Textures = new Texture[exmaterials.Length];
            m_Materials = new Material[exmaterials.Length];
            for (int i = 0; i < exmaterials.Length; i++)
            {
                string texture_file = exmaterials[i].TextureFilename;
                if (texture_file != null)
                {
                    Debug.WriteLine("Texture " + i + ": " + texture_file);
                    if (texture_file.Length > 0)
                    {
                        try
                        {
                            m_Textures[i] = TextureLoader.FromFile(m_Device, file_path + texture_file);
                        }
                        catch
                        {
                            Debug.WriteLine("*********************");
                            Debug.WriteLine("Error loading texture " + texture_file);
                        }
                    }
                } else {
                    Debug.WriteLine("Texture " + i + ": " + "<null>");
                }

                m_Materials[i] = exmaterials[i].Material3D;
                m_Materials[i].Ambient = m_Materials[i].Diffuse;
            }

            // Save the number of subsets.
            m_NumSubSets = m_Materials.Length;
        }

        #endregion // D3D Setup Code

        #region "D3D Drawing Code"
        // Draw.
        public void Render()
        {
            // Clear the back buffer.
            m_Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1, 0);

            // Make a scene.
            m_Device.BeginScene();

            // Draw stuff here...
            // Setup the world, view, and projection matrices.
            SetupMatrices();

            // Draw the mesh's subsets.
            for (int i = 0; i < m_NumSubSets; i++)
            {
                m_Device.Material = m_Materials[i];
                m_Device.SetTexture(0, m_Textures[i]);

                m_Mesh.DrawSubset(i);
            }

            // End the scene and display.
            m_Device.EndScene();
            m_Device.Present();
        }

        // Setup the world, view, and projection matrices.
        private void SetupMatrices()
        {
            // World Matrix:
            const int TICKS_PER_REV = 10000;
            double angle = Environment.TickCount * (2 * Math.PI) / TICKS_PER_REV;
            m_Device.Transform.World = Matrix.RotationY((float)angle);

            // View Matrix:
            Vector3 camera_position  = new Vector3(0, 10, -20);
            camera_position.Normalize();
            camera_position.Multiply(m_Range);

            m_Device.Transform.View = Matrix.LookAtLH(
                camera_position,
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0));

            // Projection Matrix:
            // Perspective transformation defined by:
            //       Field of view           Pi / 4
            //       Aspect ratio            1
            //       Near clipping plane     Z = 1
            //       Far clipping plane      Z = 100
            m_Device.Transform.Projection =
                Matrix.PerspectiveFovLH((float)(Math.PI / 4), 1, 1, 100);
        }

        // Make the lights.
        private void SetupLights()
        {
            m_Device.Lights[0].Type = LightType.Directional;
            m_Device.Lights[0].Diffuse = Color.White;
            m_Device.Lights[0].Ambient = Color.White;
            m_Device.Lights[0].Direction = new Vector3(0, -1, 0);
            m_Device.Lights[0].Enabled = true;

            m_Device.Lights[1].Type = LightType.Directional;
            m_Device.Lights[1].Diffuse = Color.White;
            m_Device.Lights[1].Ambient = Color.White;
            m_Device.Lights[1].Direction = new Vector3(1, -10, 1);
            m_Device.Lights[1].Enabled = true;

            // Add some ambient light.
            m_Device.RenderState.Ambient = Color.White;
        }

        #endregion // D3D Drawing Code

        // Remember the selected fill mode.
        private void FillMode_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Device == null) return;

            if (radSolid.Checked)
                m_Device.RenderState.FillMode = FillMode.Solid;
            else if (radWireframe.Checked)
                m_Device.RenderState.FillMode = FillMode.WireFrame;
            else
                m_Device.RenderState.FillMode = FillMode.Point;
        }

        // Zoom in or out.
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            m_Range /= 1.5f;
            this.Text = "d3dLoadXFile " + m_Range;
            SetupMatrices();
        }
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            m_Range *= 1.5f;
            this.Text = "d3dLoadXFile " + m_Range;
            SetupMatrices();
        }

        // Load the selected mesh.
        private void cboFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo file_info = (FileInfo)cboFile.SelectedItem;

            if (file_info == null)
            {
                m_Mesh = null;
            } else {
                LoadMesh(file_info.DirectoryName, file_info.FullName);
            }
        }

        // List available .x files.
        private void RenderForm_Load(object sender, EventArgs e)
        {
            m_FilePath = Path.GetFullPath(Path.Combine(Application.StartupPath, "..\\..\\Meshes")) + "\\";
            DirectoryInfo fso = new DirectoryInfo(m_FilePath);
            foreach (FileInfo file_info in fso.GetFiles("*.x"))
            {
                cboFile.Items.Add(file_info);
            }

            // Select the first file.
            cboFile.SelectedIndex = 0;
        }
    }
}