using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARS_Expert.ResourceManager;
using System.Xml.XPath;
using System.IO;
namespace MARS_Expert.Manager.XMLResourceManager
{
    class XML3dObject
    {
        /// <summary>
        /// Search for an obj data using its id.
        /// </summary>
        /// <param name="id">The id of the targeted obj.</param>
        /// <returns></returns>
        public static Object3d GetById(int id)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Object3d obj = new Object3d();
            try
            {
                string rslt = Helper.service.LoadFile("objet3d.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'obj à  partir du login */
                string ExpXPath = "//objet[@id='" + id + "']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Object3d */
                    obj.setId(id);
                    obj.setName(Path.GetFileNameWithoutExtension(Nodes.Current.GetAttribute("nom", "")));
                    obj.setType(Nodes.Current.GetAttribute("type", ""));
                    //System.Windows.Forms.MessageBox.Show("obj.type: " + Nodes.Current.GetAttribute("type", ""),"XML3dObject.GetById");
                }
                /* Si aucun obj n'a été trouvé */
                else
                {
                    obj = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return obj;
        }

        public static Object3d GetByNumber(int stepId, int nb)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Object3d obj = new Object3d();
            try
            {
                string rslt = Helper.service.LoadFile("etape.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'obj à  partir du login */
                string ExpXPath = "//objet[@nb='" + nb + "']";
                ExpXPath = "//etape[@id='" + stepId + "']//objet[@nb='" + nb + "']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                //System.Windows.Forms.MessageBox.Show("Nodes.Count: " + Nodes.Count, "XMLObject3d.GetByNumber");
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Object3d */
                    //System.Windows.Forms.MessageBox.Show("Current: "+Nodes.Current.Name,"XMLObject3d.GetByNumber");
                    //System.Windows.Forms.MessageBox.Show("Current.id: " + Nodes.Current.GetAttribute("id", ""), "XMLObject3d.GetByNumber");
                    obj = GetById(Convert.ToInt32(Nodes.Current.GetAttribute("id", "")));
                    obj.setPath();
                    obj.setNb(nb);
                }
                /* Si aucun obj n'a été trouvé */
                else
                {
                    obj = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return obj;
        }

        /// <summary>
        /// Returns the list of 3d objects of a specified step.
        /// </summary>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public static List<Object3d> GetStepObjects(int stepID)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Object3d> objects = new List<Object3d>();
            try
            {
                string rslt = Helper.service.LoadFile("etape.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On affecte false à  la
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();

                string ExpXPath = "//etape[@id='" + stepID.ToString() + "']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Etape */
                    Nodes.Current.MoveToFirstChild();//Move to libelle
                    Nodes.Current.MoveToNext();//Move to description
                    Nodes.Current.MoveToNext();//Move to objets
                    if (Nodes.Current.HasChildren && Nodes.Current.Name == "objets")
                    {
                        Nodes.Current.MoveToFirstChild();//Move to first objet);
                        //System.Windows.Forms.MessageBox.Show("XML3dObject.Before while Loop\n"+"Current: " + Nodes.Current.Name + "\nNb: " + Nodes.Current.GetAttribute("nb", ""));
                        Object3d obj = GetByNumber(stepID, Convert.ToInt32(Nodes.Current.GetAttribute("nb", "")));

                        //Get the transformations of of the first 3d object.
                        getObjectTransformation(Nodes, obj);

                        //System.Windows.Forms.MessageBox.Show("obj.Id: " + obj.getId() + "\nPosX: " + obj.getPosition().X
                        //        + "\nPosY: " + obj.getPosition().Y + "\nPosZ: " + obj.getPosition().Z, "XMLObject3D.GetStepObjects");

                        //System.Windows.Forms.MessageBox.Show("XML3dObject.After first call(getObjectTrans): "+obj.getPosition().X);
                        //Addthe first object to the list of objects.
                        objects.Add(obj);

                        while (Nodes.Current.MoveToNext(XPathNodeType.Element))
                        {
                            //Nodes.Current.MoveToNext();//Move to next objet
                            //System.Windows.Forms.MessageBox.Show(Nodes.Current.GetAttribute("nb", ""));
                            obj = GetByNumber(stepID, Convert.ToInt32(Nodes.Current.GetAttribute("nb", "")));
                            //Get the transformations of of the rest of the 3d objects.
                            getObjectTransformation(Nodes, obj);

                            //System.Windows.Forms.MessageBox.Show("obj.Id: " + obj.getId() + "\nPosX: " + obj.getPosition().X
                            //    + "\nPosY: " + obj.getPosition().Y + "\nPosZ: " + obj.getPosition().Z, "XMLObject3D.GetStepObjects");

                            objects.Add(obj);
                        }
                    }
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    objects = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            //System.Windows.Forms.MessageBox.Show("Objects.Count: " + objects.Count, "XML3dObjectGetStepObjects");
            /* Renvoi de toutes les données dans une instance de la classe "etape" */
            return objects;
        }

        
        private static void getObjectTransformation(XPathNodeIterator Nodes, Object3d obj)
        {
            Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to first position
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.current:  " + Nodes.Current.Name);
            float posx = (float)Convert.ToDouble(Nodes.Current.GetAttribute("x", "")) * XMLStep.translateFactor;
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.current.getattrib:  " + Nodes.Current.GetAttribute("x", "")+"  "+posx);
            float posy = (float)Convert.ToDouble(Nodes.Current.GetAttribute("y", "")) * XMLStep.translateFactor;
            float posz = (float)Convert.ToDouble(Nodes.Current.GetAttribute("z", "")) * XMLStep.translateFactor * XMLStep.zFactor;
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.posx: " + posx);

            Helper.TransformAxis(ref posx, ref posy, ref posz, Transformation.Translation, false);
            obj.setPosition(new Microsoft.DirectX.Vector3(posx, posy, posz));
            

            Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to first rotation
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.current:  " + Nodes.Current.Name);
            float rotx = (float)Convert.ToDouble(Nodes.Current.GetAttribute("x", ""));
            float roty = (float)Convert.ToDouble(Nodes.Current.GetAttribute("y", ""));
            float rotz = (float)Convert.ToDouble(Nodes.Current.GetAttribute("z", ""));
            float angle = (float)Convert.ToDouble(Nodes.Current.GetAttribute("angle", ""));
            angle = Constante.stableAngle;
            //System.Windows.Forms.MessageBox.Show("Angle: "+angle);
            //System.Windows.Forms.MessageBox.Show("Befor: Y: " + roty + " Z: " + rotz);
            Helper.TransformAxis(ref rotx, ref roty, ref rotz, Transformation.Rotation, false);
            //System.Windows.Forms.MessageBox.Show("After: Y: " + roty + " Z: " + rotz);
            
            
            obj.setRotation(new Microsoft.DirectX.Vector3(Helper.DegreesToRadians(rotx * angle), Helper.DegreesToRadians(roty * angle), Helper.DegreesToRadians(rotz * angle)));

            Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to first scale
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.current:  " + Nodes.Current.Name);
            float sclx = (float)Convert.ToDouble(Nodes.Current.GetAttribute("x", "")) / XMLStep.scaleFactor;
            float scly = (float)Convert.ToDouble(Nodes.Current.GetAttribute("y", "")) / XMLStep.scaleFactor;
            float sclz = (float)Convert.ToDouble(Nodes.Current.GetAttribute("z", "")) / XMLStep.scaleFactor;
            obj.setScale(new Microsoft.DirectX.Vector3(sclx, scly, sclz));
            //Move back to the parent so the invoker of this methode can continue from where it started.
            Nodes.Current.MoveToParent();
        }

        private static void getObjectTransformation_(XPathNodeIterator Nodes, Object3d obj)
        {
            Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to first position
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.current:  " + Nodes.Current.Name);
            float posx = (float)Convert.ToDouble(Nodes.Current.GetAttribute("x", "")) * XMLStep.translateFactor;
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.current.getattrib:  " + Nodes.Current.GetAttribute("x", "")+"  "+posx);
            float posz = (float)Convert.ToDouble(Nodes.Current.GetAttribute("y", "")) * XMLStep.translateFactor;
            float posy = (float)Convert.ToDouble(Nodes.Current.GetAttribute("z", "")) * XMLStep.translateFactor * XMLStep.zFactor;
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.posx: " + posx);
            obj.setPosition(new Microsoft.DirectX.Vector3(posx, posy, posz));
            
            Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to first rotation
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.current:  " + Nodes.Current.Name);
            float rotx = (float)Convert.ToDouble(Nodes.Current.GetAttribute("x", "")) * (-1);
            float rotz = (float)Convert.ToDouble(Nodes.Current.GetAttribute("y", "")) * (-1);
            float roty = (float)Convert.ToDouble(Nodes.Current.GetAttribute("z", ""));

            System.Windows.Forms.MessageBox.Show("Befor: Y: " + roty + " Z: " + rotz);

            float angle = (float)Convert.ToDouble(Nodes.Current.GetAttribute("angle", ""));



            obj.setRotation(new Microsoft.DirectX.Vector3(Helper.DegreesToRadians(rotx * angle), Helper.DegreesToRadians(roty * angle),
            Helper.DegreesToRadians(rotz * angle)));

            Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to first scale
            //System.Windows.Forms.MessageBox.Show("XMLObject3D.getObjectTransformation.current:  " + Nodes.Current.Name);
            float sclx = (float)Convert.ToDouble(Nodes.Current.GetAttribute("x", "")) / XMLStep.scaleFactor;
            float scly = (float)Convert.ToDouble(Nodes.Current.GetAttribute("y", "")) / XMLStep.scaleFactor;
            float sclz = (float)Convert.ToDouble(Nodes.Current.GetAttribute("z", "")) / XMLStep.scaleFactor;
            obj.setScale(new Microsoft.DirectX.Vector3(sclx, scly, sclz));
            //Move back to the parent so the invoker of this methode can continue from where it started.
            Nodes.Current.MoveToParent();
        }


        /// <summary>
        /// Returns the list of 3d objects.
        /// </summary>
        /// <returns></returns>
        //public static List<string[]> GetAll3dObjects()
        public static List<ObjForListView> GetAll3dObjects()
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            //List<string[]> objects = new List<string[]>();
            List<ObjForListView> objects = new List<ObjForListView>();
            try
            {
                string rslt = Helper.service.LoadFile("objet3d.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On affecte false à  la
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();

                string ExpXPath = "//objet";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                //System.Windows.Forms.MessageBox.Show("Node.count. "+Nodes.Count,"XMLObject3d.GetAllObjects");
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Etape */

                    int tillCount = 0;
                    while (tillCount < Nodes.Count)
                    {
                        //data contains:
                        //0. Id
                        //1. Name
                        //2. Type

                        //string[] data = new string[3];
                        ObjForListView data = new ObjForListView(Nodes.Current.GetAttribute("id", ""), Path.GetFileNameWithoutExtension(Nodes.Current.GetAttribute("nom", "")),
                            Nodes.Current.GetAttribute("type", ""));

                        //data[0] = Nodes.Current.GetAttribute("id", "");

                        //System.Windows.Forms.MessageBox.Show("Attrib. " + Nodes.Current.GetAttribute("id", ""), "XMLObject3d.GetAllObjects");

                        //data[1] = Nodes.Current.GetAttribute("nom", "");
                        //data[2] = Nodes.Current.GetAttribute("type", "");

                        objects.Add(data);
                        Nodes.Current.MoveToNext();
                        tillCount++;
                    }

                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    objects = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "etape" */
            //System.Windows.Forms.MessageBox.Show("ret.count " + objects.Count, "XMLObject3d.GetAllObjects");
            return objects;
        }
    }
}
