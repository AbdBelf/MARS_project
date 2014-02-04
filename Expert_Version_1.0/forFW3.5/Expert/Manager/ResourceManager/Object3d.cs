using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Diagnostics;
using MARS_Expert.Manager;
using System.IO;

namespace MARS_Expert.ResourceManager
{
    public class Object3d
    {
        private int iId;
        private int iNb;
        private string sName;
        //The type could be one of "Sign, tool, material"
        private string sType;
        private string fullPath;

        private Vector3 vPosition;
        private Vector3 vRotation;
        private Vector3 vScale;


        private int m_NumSubSets;
        private Material[] m_Materials;
        private Texture[] m_Textures;

        // The mesh that holds all of the data.
        public Mesh m_Mesh;

        private Device m_Device;

        private List<Etape> etapes = new List<Etape>();
        private Etape Step = new Etape();

        public Object3d()
        {
        }

        public Object3d(int id, string name, string type, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.iId = id;
            this.sName = name;
            this.sType = type;
            this.fullPath = Helper.getObjectPath(sType) + name + ".x";
            this.vPosition = position;
            this.vRotation = rotation;
            this.vScale = scale;
        }

        public Object3d(int id, string name,string type, Vector3 position, Vector3 rotation, Vector3 scale,Device device)
        {
            this.iId = id;
            this.sName = name;
            this.sType = type;
            this.fullPath = Helper.getObjectPath(sType) + name + ".x";
            this.vPosition = position;
            this.vRotation = rotation;
            this.vScale = scale;
            this.m_Device = device;
        }

        public int getId()
        {
            return iId;
        }
        public void setId(int iId)
        {
            this.iId = iId;
        }

        public int getNb()
        {
            return iNb;
        }
        public void setNb(int iNb)
        {
            this.iNb = iNb;
        }
        
        public string getName()
        {
            return sName;
        }
        public void setName(string sName)
        {
            this.sName = sName;
        }
        
        public string getObjType()
        {
            return sType;
        }
        public void setType(string sType)
        {
            this.sType = sType;
        }

        
        public string getPath()
        {
            return fullPath;
        }
        public void setPath(string sPath)
        {
            this.fullPath = sPath;
        }
        public void setPath()
        {
            this.fullPath = Helper.getObjectPath(this.sType) + this.sName + ".x";
        }
        
        public Vector3 getPosition()
        {
            return vPosition;
        }
        public void setPosition(Vector3 vPosition)
        {
            this.vPosition = vPosition;
        }
        
        public Vector3 getRotation()
        {
            return vRotation;
        }
        public void setRotation(Vector3 vRotation)
        {
            this.vRotation = vRotation;
        }
        
        public Vector3 getScale()
        {
            return vScale;
        }
        public void setScale(Vector3 vScale)
        {
            this.vScale = vScale;
        }
        
        public Device getDevice()
        {
            return this.m_Device;
        }
        public void setDevice(Device device)
        {
            this.m_Device = device;
        }

        public Etape getStep()
        {
            return this.Step;
        }
        public void setStep(Etape step)
        {
            this.Step = step;
        }
        
        // Load a mesh from a .x file.
        public void LoadMesh()
        {
            // Load the mesh.
            ExtendedMaterial[] exmaterials = null;
            try
            {
                //m_Mesh = Mesh.FromFile(this.sName, MeshFlags.Managed, m_Device, out exmaterials);
                //System.Windows.Forms.MessageBox.Show("FullPath: "+this.fullPath);
                Object3d obj = this;
                //System.Windows.Forms.MessageBox.Show("obj.path: " + obj.getPath() + ". name: " + obj.getName() + ". type: " + obj.getObjType(), "Object3d.LoadMesh");
                //System.Windows.Forms.MessageBox.Show("name: " + obj.getName(), "Object3d.LoadMesh");
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException();
                }
                m_Mesh = Mesh.FromFile(this.fullPath, MeshFlags.Managed, m_Device, out exmaterials);
                //m_Mesh = Mesh.FromFile(this.sName, MeshFlags.Managed, m_Device, out exmaterials);
            }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString());
        }

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
                            m_Textures[i] = TextureLoader.FromFile(m_Device, Path.GetDirectoryName(fullPath) + texture_file);
                        }
                        catch
                        {
                            Debug.WriteLine("*********************");
                            Debug.WriteLine("Error loading texture " + texture_file);
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Texture " + i + ": " + "<null>");
                }

                m_Materials[i] = exmaterials[i].Material3D;
                m_Materials[i].Ambient = m_Materials[i].Diffuse;
            }

            // Save the number of subsets.
            m_NumSubSets = m_Materials.Length;
        }

        public void Draw()
        {
            //if (m_Materials != null)
            //    System.Windows.Forms.MessageBox.Show("texture not null");
            // Draw the mesh's subsets.
            for (int i = 0; i < m_NumSubSets; i++)
            {
                m_Device.Material = m_Materials[i];
                m_Device.SetTexture(0, m_Textures[i]);
                m_Mesh.DrawSubset(i);
            }
        }

        public void addEtape(Etape etape)
        {
            etapes.Add(etape);
        }

        public void removeEtape(int index)
        {
            etapes.RemoveAt(index);
        }

        public void removeEtape(Etape etape)
        {
            etapes.Remove(etape);
        }

        public int etapesCount()
        {
            return etapes.Count;
        }

        public Etape getEtape(int index)
        {
            return etapes[index];
        }

        public void setParentStep(Etape etape)
        {
            this.Step = etape;
        }

        public Etape getParentStep()
        {
            return this.Step;
        }

        public static Object3d GetByID(int id)
        {
            Object3d ret = null;
            try
            {
                ret = Manager.XMLResourceManager.XML3dObject.GetById(id);
            }
            catch (System.IO.FileNotFoundException) { return null; }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }

        public override string ToString()
        {
            return this.sName;
        }
    }
}
