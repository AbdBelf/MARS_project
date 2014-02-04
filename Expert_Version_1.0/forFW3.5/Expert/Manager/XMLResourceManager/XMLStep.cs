using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using MARS_Expert.ResourceManager;
using System.IO;
using Microsoft.DirectX;

namespace MARS_Expert.Manager.XMLResourceManager
{
    class XMLStep
    {
        public static int zFactor = Constante.zFactor;
        public static float translateFactor = Constante.translateFactor;
        public static float scaleFactor = Constante.scaleFactor;
        public static float angle = Constante.angle;



        /// <summary>
        /// Gets the Id of the next etape to insert.
        /// </summary>
        /// <returns>The returned id or 0 if no element exists.</returns>
        public static int getNextId()
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            Int32 ID; /* Variable utilisée pour savoir quel est l'ID qu'il faut affecter au nouveau
                       * noeud créé */
            string rslt = Helper.service.LoadFile("etape.xml").ToString();

            StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            sw.Write(rslt);
            sw.Close();


            XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            Navigator = XmlDoc.CreateNavigator();
            /* Recherche du noeud MaxID pour déterminer quelle sera l'ID du nouveau
             * procedure. */
            string ExpXPath = "//MaxID";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            Nodes.MoveNext();
            /* On place l'ID le plus élevé du document dans la variable ID */
            if (Nodes.Count > 0)
                ID = Nodes.Current.ValueAsInt + 1;
            else
                ID = 0;
            return ID;
        }

        /// <summary>
        /// Checks the existance of an Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static bool Exists(int id)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            string rslt = Helper.service.LoadFile("etape.xml").ToString();

            StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            sw.Write(rslt);
            sw.Close();


            XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            Navigator = XmlDoc.CreateNavigator();
            /* Recherche du noeud MaxID pour déterminer quelle sera l'ID du nouveau
             * procedure. */
            string ExpXPath = "//etape[@id='" + id.ToString() + "']";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            Nodes.MoveNext();
            if (Nodes.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Creates the file "etape.xml" and inserts the first step to it.
        /// </summary>
        /// <param name="expert">The object holding the data.</param>
        /// <returns></returns>
        private static bool FirstAdd(Etape etape)
        {
            try
            {
                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration
                xw.WriteStartDocument();

                // Write the root node
                xw.WriteStartElement("Etapes");

                xw.WriteStartElement("etape");

                xw.WriteStartAttribute("id");
                xw.WriteString("0");
                xw.WriteEndAttribute();
                //----------------
                xw.WriteStartElement("libelle");
                xw.WriteString(etape.getName());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("description");
                xw.WriteString(etape.getDescription());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("objets");
                //System.Windows.Forms.MessageBox.Show("Step.Objects.Count: " + etape.getObjectList().Count, "XMLStep.FirstAdd");
                for (int i = 0; i < etape.getObjectList().Count; i++)
                {
                    Object3d obj = etape.getObjectList()[i];

                    xw.WriteStartElement("objet");//<objet>
                    xw.WriteStartAttribute("id");
                    xw.WriteString(obj.getId().ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartAttribute("nb");
                    xw.WriteString(i.ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartElement("position");//<position>
                    //Transform axis
                    float X, Y, Z;
                    X=obj.getPosition().X;
                    Y=obj.getPosition().Y;
                    Z=obj.getPosition().Z;

                    Helper.TransformAxis(ref X, ref Y, ref Z, Transformation.Translation, true);
                    xw.WriteStartAttribute("x");
                    xw.WriteString((X / translateFactor).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartAttribute("y");
                    xw.WriteString((Y / translateFactor).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartAttribute("z");
                    xw.WriteString((zFactor * Z / translateFactor).ToString());
                    xw.WriteEndAttribute();
                    xw.WriteEndElement();//</position>

                    xw.WriteStartElement("rotation");//<rotation>
                    //Transform axis
                    X = obj.getRotation().X;
                    Y = obj.getRotation().Y;
                    Z = obj.getRotation().Z;
                    Helper.TransformAxis(ref X, ref Y, ref Z, Transformation.Rotation, true);

                    float angleRad = Helper.DegreesToRadians(Constante.stableAngle);
                    Vector3 v = Vector3.Multiply(obj.getRotation(), 1 / angleRad);
                    Vector3 N = Helper.Normalize(v);
                    //System.Windows.Forms.MessageBox.Show("obj/angle: " + v.ToString() + "\nNorm: " + v1.ToString());

                    angleRad = angleRad / (N.X / v.X);
                    //angle = Helper.RadiansToDegrees(angleRad);
                    angle = Constante.stableAngle;


                    xw.WriteStartAttribute("angle");
                    if (obj.getRotation().X + obj.getRotation().Y + obj.getRotation().Z == 0)
                        xw.WriteString((0).ToString());
                    else
                        xw.WriteString((angle).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartAttribute("x");
                    xw.WriteString((Helper.RadiansToDegrees(X) / angle).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartAttribute("y");
                    xw.WriteString((Helper.RadiansToDegrees(Y) / angle).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartAttribute("z");
                    xw.WriteString((Helper.RadiansToDegrees(Z) / angle).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteEndElement();//</rotation>

                    xw.WriteStartElement("scale");//<scale>

                    xw.WriteStartAttribute("x");
                    xw.WriteString((obj.getScale().X * scaleFactor).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartAttribute("y");
                    xw.WriteString((obj.getScale().Y * scaleFactor).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteStartAttribute("z");
                    xw.WriteString((obj.getScale().Z * scaleFactor).ToString());
                    xw.WriteEndAttribute();

                    xw.WriteEndElement();//</scale>

                    xw.WriteEndElement();//</objet>
                }
                xw.WriteEndElement();

                xw.WriteStartElement("procedure");
                xw.WriteString(etape.getprocedure().getId().ToString());
                xw.WriteEndElement();
                //-----------------

                xw.WriteEndElement();

                xw.WriteStartElement("MaxID");
                xw.WriteString("0");
                xw.WriteEndElement();

                // Close the document
                xw.WriteEndDocument();

                // Flush the write
                xw.Flush();

                Byte[] buffer = new Byte[ms.Length];
                buffer = ms.ToArray();
                string xmlOutput = System.Text.Encoding.UTF8.GetString(buffer);

                //File.WriteAllText((Stream)Helper.service.LoadFile("etape.xml", xmlOutput);
                Helper.service.CreateXmlFile("etape.xml", xmlOutput);
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x) { System.Windows.Forms.MessageBox.Show(x.ToString()); }
            return true;
        }

        /// <summary>
        /// inserts a step to "etape.xml".
        /// </summary>
        /// <param name="expert">The object holding the data</param>
        /// <returns></returns>
        private static bool Insert(Etape etape)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                Int32 ID; /* Variable utilisée pour savoir quel est l'ID qu'il faut affecter au nouveau
                       * noeud créé */
                string rslt = Helper.service.LoadFile("etape.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                Navigator = XmlDoc.CreateNavigator();
                /* Recherche du noeud MaxID pour déterminer quelle sera l'ID du nouveau
                 * procedure. */
                string ExpXPath = "//MaxID";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                Nodes.MoveNext();
                /* On place l'ID le plus élevé du document dans la variable ID */
                ID = Nodes.Current.ValueAsInt;
                /* On incrémente la valeur du noeud MaxID car une fois notre nouveau noeud 
                 * créé, l'ID le plus élevé du document sera aussi incrémenté */
                Nodes.Current.SetValue((ID + 1).ToString());
                /* On se place sur le noeud ayant l'ID le plus élevé */
                //ExpXPath = "//etape[@id='" + ID.ToString() + "']";
                //Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    //Nodes.MoveNext();
                    /* On crée le noeud principal (etape). */
                    Nodes.Current.MoveToPrevious();
                    //System.Windows.Forms.MessageBox.Show("Current.Previous: " + Nodes.Current.Name,"XMLStep.Insert");
                    Nodes.Current.InsertElementAfter("", "etape", "", "");
                    /* On se place sur le noeud ainsi créé. */
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    ID++; /* On incrémente ID pour que sa valeur soit identique à celle se
                       * trouvant dans le noeud MaxID. */
                    /* Encodage des données */
                    Nodes.Current.CreateAttribute("", "id", "", ID.ToString());
                    Nodes.Current.AppendChildElement("", "libelle", "", etape.getName());
                    Nodes.Current.AppendChildElement("", "description", "", etape.getDescription());

                    int count = 0;
                    //System.Windows.Forms.MessageBox.Show("3d obj "+etape.getObjectList().Count.ToString());
                    Nodes.Current.AppendChildElement("", "objets", "", "");//Create node objets.
                    Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to libelle.
                    Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to description.
                    Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to objets.
                    //System.Windows.Forms.MessageBox.Show("Before Loop:" + Nodes.Current.Name);
                    foreach (Object3d obj in etape.getObjectList())
                    {
                        //System.Windows.Forms.MessageBox.Show("count:" + count);

                        Nodes.Current.AppendChildElement("", "objet", "", "");//Create node objet.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to objet.

                        //Move pass all the created object elements.
                        for (int i = 0; i < count + 1; i++)
                        {
                            //Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to objet.
                            Nodes.Current.MoveToNext();
                        }
                        //System.Windows.Forms.MessageBox.Show("for Loop:"+Nodes.Current.Name);
                        Nodes.Current.CreateAttribute("", "id", "", obj.getId().ToString());
                        //Nodes.Current.CreateAttribute("", "nb", "", obj.getNb().ToString());
                        Nodes.Current.CreateAttribute("", "nb", "", count.ToString());
                        count++;
                        Nodes.Current.AppendChildElement("", "position", "", "");//Create node position.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node position.
                        //Transform axis
                        float X, Y, Z;
                        X = obj.getPosition().X;
                        Y = obj.getPosition().Y;
                        Z = obj.getPosition().Z;

                        Helper.TransformAxis(ref X, ref Y, ref Z, Transformation.Translation, true);

                        Nodes.Current.CreateAttribute("", "x", "", (X / translateFactor).ToString());
                        Nodes.Current.CreateAttribute("", "y", "", (Y / translateFactor).ToString());
                        Nodes.Current.CreateAttribute("", "z", "", (zFactor * Z / translateFactor).ToString());
                        Nodes.Current.MoveToParent();//Move back to objet.

                        Nodes.Current.AppendChildElement("", "rotation", "", "");//Create node rotation.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node position.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node rotation.

                        float angleRad = Helper.DegreesToRadians(Constante.stableAngle);
                        Vector3 v = Vector3.Multiply(obj.getRotation(), 1 / angleRad);
                        Vector3 N = Helper.Normalize(v);
                        //System.Windows.Forms.MessageBox.Show("obj/angle: " + v.ToString() + "\nNorm: " + v1.ToString());

                        angleRad = angleRad / (N.X / v.X);
                        //angle = Helper.RadiansToDegrees(angleRad);
                        angle = Constante.stableAngle;

                        if (obj.getRotation().X + obj.getRotation().Y + obj.getRotation().Z == 0)
                            Nodes.Current.CreateAttribute("", "angle", "", (0).ToString());
                        else
                            Nodes.Current.CreateAttribute("", "angle", "", (angle).ToString());

                        //System.Windows.Forms.MessageBox.Show("Befor: X: " + obj.getRotation().X);

                        X = obj.getRotation().X;
                        Y = obj.getRotation().Y;
                        Z = obj.getRotation().Z;

                        Helper.TransformAxis(ref X, ref Y, ref Z, Transformation.Rotation, true);
                        //System.Windows.Forms.MessageBox.Show("After: X: " + obj.getRotation().X);

                        Nodes.Current.CreateAttribute("", "x", "", (Helper.RadiansToDegrees(X) / angle).ToString());
                        Nodes.Current.CreateAttribute("", "y", "", (Helper.RadiansToDegrees(Y) / angle).ToString());
                        Nodes.Current.CreateAttribute("", "z", "", (Helper.RadiansToDegrees(Z) / angle).ToString());
                        Nodes.Current.MoveToParent();//Move back to objet.

                        Nodes.Current.AppendChildElement("", "scale", "", "");//Create node scale.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node position.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node rotation.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node scale.
                        Nodes.Current.CreateAttribute("", "x", "", (obj.getScale().X * scaleFactor).ToString());
                        Nodes.Current.CreateAttribute("", "y", "", (obj.getScale().Y * scaleFactor).ToString());
                        Nodes.Current.CreateAttribute("", "z", "", (obj.getScale().Z * scaleFactor).ToString());

                        Nodes.Current.MoveToParent();//Move back to objet.
                        //System.Windows.Forms.MessageBox.Show("End "+Nodes.Current.Name);
                        Nodes.Current.MoveToParent();//Move back to objets.
                        //System.Windows.Forms.MessageBox.Show("End "+Nodes.Current.Name);

                        //System.Windows.Forms.MessageBox.Show(Nodes.Current.Name);
                        //return false;
                    }
                    Nodes.Current.MoveToParent();//Move back to etape.
                    //System.Windows.Forms.MessageBox.Show(Nodes.Current.Name);
                    Nodes.Current.AppendChildElement("", "procedure", "", etape.getprocedure().getId().ToString());
                    //return false;

                    //XmlDoc.Save((Stream)Helper.service.LoadFile("etape.xml");
                    //System.Windows.Forms.MessageBox.Show("Saving:"+ Helper.service.SaveXmlFile("etape.xml", XmlDoc).ToString(),"XMLStep.Insert");
                    Helper.service.SaveXmlFile("etape.xml", XmlDoc);

                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x) { System.Windows.Forms.MessageBox.Show(x.ToString()); }
            return true;
        }

        public static bool Add(Etape etape)
        {
            //If the file exists we insert a new element.
            //The number root children must be at least and element and the MaxID element = 2.
            if (Helper.service.FileExists("etape.xml"))
            {
                //System.Windows.Forms.MessageBox.Show("Insert");
                return Insert(etape);
            }
            else //Otherwise we create the file and insert the first elemet.
            {
                //System.Windows.Forms.MessageBox.Show("FirstAdd");
                return FirstAdd(etape);
            }
        }

        /// <summary>
        /// Modifies the etape Data.
        /// </summary>
        /// <param name="login">The id of the target Etape object.</param>
        /// <param name="admin">The object that holds the new data.</param>
        /// <returns></returns>
        public static bool Update(int id, Etape etape)
        {
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                string rslt = Helper.service.LoadFile("etape.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//etape[@id='" + id.ToString() + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstAttribute();
                    Nodes.Current.SetValue(id.ToString());
                    Nodes.Current.MoveToParent();

                    Nodes.Current.MoveToFirstChild();
                    //System.Windows.Forms.MessageBox.Show(Nodes.Current.Name.ToString() + " | " + Nodes.Current.Value.ToString());
                    Nodes.Current.SetValue(etape.getName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(etape.getDescription());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    //System.Windows.Forms.MessageBox.Show("Update: "+Nodes.Current.Name);
                    {
                        //Delete the set of objects.
                        Nodes.Current.DeleteSelf();
                        //Recreate the objects.
                        //System.Windows.Forms.MessageBox.Show("XMLStep.Update.Objects.Count: " + etape.getObjectList().Count.ToString());
                        if (etape.getObjectList().Count > 0)
                            InsertObjects(id, etape.getObjectList(), Nodes);
                    }
                    Nodes.Current.MoveToFirstChild();
                    Nodes.Current.MoveToNext();//Move to Libelle.
                    Nodes.Current.MoveToNext();//Move to Description.
                    //System.Windows.Forms.MessageBox.Show("Update: " + Nodes.Current.Name);
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(etape.getprocedure().getId().ToString());
                    //System.Windows.Forms.MessageBox.Show("Update: " + Nodes.Current.Name);
                    //return false;

                    //XmlDoc.Save((Stream)Helper.service.LoadFile("etape.xml");
                    Helper.service.SaveXmlFile("etape.xml", XmlDoc);
                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            { System.Windows.Forms.MessageBox.Show(x.ToString()); }
            return true;
        }

        /// <summary>
        /// inserts a step to "etape.xml".
        /// </summary>
        /// <param name="expert">The object holding the data</param>
        /// <returns></returns>
        private static bool InsertObjects(int id, List<Object3d> objects, XPathNodeIterator Nodes)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                string rslt = Helper.service.LoadFile("etape.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                Navigator = XmlDoc.CreateNavigator();
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext();
                    int count = 0;

                    Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to libelle.
                    Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to description.
                    Nodes.Current.InsertElementAfter("", "objets", "", "");//Create node objets.
                    Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to objets.

                    //System.Windows.Forms.MessageBox.Show("Before Loop:" + Nodes.Current.Name);
                    foreach (Object3d obj in objects)
                    {
                        //System.Windows.Forms.MessageBox.Show("obj: " + obj.getName());

                        Nodes.Current.AppendChildElement("", "objet", "", "");//Create node objet.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to objet.
                        //System.Windows.Forms.MessageBox.Show("Loop:" + Nodes.Current.Name);
                        //Move pass all the created object elements.
                        for (int i = 0; i < count + 1; i++)
                        {
                            //Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to objet.
                            Nodes.Current.MoveToNext();
                        }
                        //System.Windows.Forms.MessageBox.Show("for Loop:"+Nodes.Current.Name);
                        Nodes.Current.CreateAttribute("", "id", "", obj.getId().ToString());
                        Nodes.Current.CreateAttribute("", "nb", "", count.ToString());
                        count++;
                        Nodes.Current.AppendChildElement("", "position", "", "");//Create node position.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node position.
                        //Transform axis
                        float X, Y, Z;
                        X = obj.getPosition().X;
                        Y = obj.getPosition().Y;
                        Z = obj.getPosition().Z;

                        Helper.TransformAxis(ref X, ref Y, ref Z, Transformation.Translation, true);
                        //System.Windows.Forms.MessageBox.Show("Z: " + Z);

                        Nodes.Current.CreateAttribute("", "x", "", (X / translateFactor).ToString());
                        Nodes.Current.CreateAttribute("", "y", "", (Y / translateFactor).ToString());
                        Nodes.Current.CreateAttribute("", "z", "", (zFactor * Z / translateFactor).ToString());
                        
                        Nodes.Current.MoveToParent();//Move back to objet.

                        Nodes.Current.AppendChildElement("", "rotation", "", "");//Create node rotation.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node position.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node rotation.
                        //System.Windows.Forms.MessageBox.Show("" + (obj.getRotation().X + obj.getRotation().Y + obj.getRotation().Z), "XMLStep.InsertObjects");
                        float angleRad = Helper.DegreesToRadians(Constante.stableAngle);
                        Vector3 v = Vector3.Multiply(obj.getRotation(), 1 / angleRad);
                        Vector3 N = Helper.Normalize(v);
                        //System.Windows.Forms.MessageBox.Show("obj/angle: " + v.ToString() + "\nNorm: " + v1.ToString());

                        angleRad = angleRad / (N.X / v.X);
                        //angle = Helper.RadiansToDegrees(angleRad);
                        angle = Constante.stableAngle;
                        //System.Windows.Forms.MessageBox.Show("" + (angle1), "XMLStep.InsertObjects");
                        
                        if (obj.getRotation().X + obj.getRotation().Y + obj.getRotation().Z == 0)
                            Nodes.Current.CreateAttribute("", "angle", "", (0).ToString());
                        else
                            Nodes.Current.CreateAttribute("", "angle", "", (angle).ToString());
                            //Nodes.Current.CreateAttribute("", "angle", "", (Helper.RadiansToDegrees(angle1)).ToString());
                        
                        //System.Windows.Forms.MessageBox.Show("angle.X: " + Helper.RadiansToDegrees(obj.getRotation().X) +
                        //"\nangle.Y: " + Helper.RadiansToDegrees(obj.getRotation().Y) +
                        //"\nangle.Z: " + Helper.RadiansToDegrees(obj.getRotation().Z));

                        //System.Windows.Forms.MessageBox.Show("Befor: Y: " + obj.getRotation().Y + " Z: " + obj.getRotation().Z);
                        X = obj.getRotation().X;
                        Y = obj.getRotation().Y;
                        Z = obj.getRotation().Z;
                        Helper.TransformAxis(ref X, ref Y, ref Z, Transformation.Rotation, true);
                        //System.Windows.Forms.MessageBox.Show("After: Y: " + obj.getRotation().Y + " Z: " + obj.getRotation().Z);

                        Nodes.Current.CreateAttribute("", "x", "", (Helper.RadiansToDegrees(X) / Constante.stableAngle).ToString());
                        Nodes.Current.CreateAttribute("", "y", "", (Helper.RadiansToDegrees(Y) / Constante.stableAngle).ToString());
                        Nodes.Current.CreateAttribute("", "z", "", (Helper.RadiansToDegrees(Z) / Constante.stableAngle).ToString());
                        
                        //Nodes.Current.CreateAttribute("", "x", "", v1.X.ToString());
                        //Nodes.Current.CreateAttribute("", "y", "", v1.Y.ToString());
                        //Nodes.Current.CreateAttribute("", "z", "", v1.Z.ToString());

                        Nodes.Current.MoveToParent();//Move back to objet.

                        Nodes.Current.AppendChildElement("", "scale", "", "");//Create node scale.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node position.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node rotation.
                        Nodes.Current.MoveToFollowing(XPathNodeType.Element);//Move to node scale.
                        Nodes.Current.CreateAttribute("", "x", "", (obj.getScale().X * scaleFactor).ToString());
                        Nodes.Current.CreateAttribute("", "y", "", (obj.getScale().Y * scaleFactor).ToString());
                        Nodes.Current.CreateAttribute("", "z", "", (obj.getScale().Z * scaleFactor).ToString());

                        Nodes.Current.MoveToParent();//Move back to objet.
                        //System.Windows.Forms.MessageBox.Show("End "+Nodes.Current.Name);
                        Nodes.Current.MoveToParent();//Move back to objets.
                        //System.Windows.Forms.MessageBox.Show("End "+Nodes.Current.Name);

                        //System.Windows.Forms.MessageBox.Show(Nodes.Current.Name);
                        //return false;
                    }

                    Nodes.Current.MoveToParent();//Move back to etape.
                    //return false;
                    //XmlDoc.Save((Stream)Helper.service.LoadFile("etape.xml");
                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x) { System.Windows.Forms.MessageBox.Show(x.ToString()); }
            return true;
        }

        /// <summary>
        /// Get the list of steps for a specified procedure.
        /// </summary>
        /// <param name="Id">The id of the procedure</param>
        /// <returns></returns>
        public static List<Etape> GetByProcedure(int Id)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Etape> steps = new List<Etape>();
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
                /* On crée ici l'expression XPath de recherche d'expert à  partir de l'id */

                //To eleminate the case sensetive in XPath we use the methode translate().
                string ExpXPath = "//etape[procedure='" + Id + "']";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    while (Nodes.MoveNext()) // NOTE: Necéssaire pour se placer sur le noeud recherché
                    {
                        Etape step = new Etape();
                        /* Encodage des données dans la classe Expert */
                        step.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", ""))); /* Pas besoin de chercher cette donnée vu que c'est notre 
                                   * critère de recherche, on peut donc directement
                                   * l'encoder. */
                        Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Libelle" */
                        step.setName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Description"
                        step.setDescription(Nodes.Current.Value);
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Objets"
                        //
                        //Get all the objects of the current step.
                        //
                        foreach (Object3d obj in XML3dObject.GetStepObjects(step.getId()))
                        {
                            step.addObject3d(obj);
                            //System.Windows.Forms.MessageBox.Show("obj.Id: " + obj.getId() + "\nPosX: " + obj.getPosition().X
                            //    + "\nPosY: " + obj.getPosition().Y + "\nPosZ: " + obj.getPosition().Z, "XMLStep.GetByProcedure");
                        }
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Procedure"
                        //step.setprocedure(Convert.ToInt32(Nodes.Current.Value));
                        steps.Add(step);
                    }
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
                return null;
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return steps;
        }

        /// <summary>
        /// Gets the step by Id.
        /// </summary>
        /// <param name="id">The id of the targeted Step.</param>
        /// <returns></returns>
        public static Etape GetById(int id)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Etape step = new Etape();
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

                string ExpXPath = "//etape[@id='" + id + "']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Etape */
                    step.setId(id);
                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Libelle" */
                    step.setName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Description"
                    step.setDescription(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    //
                    //Get all the objects
                    //
                    step.setObjectList(XML3dObject.GetStepObjects(id));
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Procedure"
                    step.setprocedure(XMLProcedure.GetById(Convert.ToInt32(Nodes.Current.Value)));
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    step = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "etape" */
            return step;
        }

        /// <summary>
        /// Remove the list of steps for a specified procedure.
        /// </summary>
        /// <param name="Id">The id of the procedure</param>
        /// <returns></returns>
        public static bool RemoveByProcedure(int Id)
        {
            bool ret = true;
            //try
            {
                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("etape.xml");
                XmlDocument XmlDoc = new XmlDocument();
                string rslt = Helper.service.LoadFile("etape.xml").ToString();
                //System.Windows.Forms.MessageBox.Show(rslt,"XMLStep.RemoveByProcedure");
                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;

                /* On crée un navigateur */
                //Navigator = XPathDocu.CreateNavigator();
                Navigator = XmlDoc.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'expert à  partir de l'id */
                string ExpXPath = "//etape[procedure='" + Id + "']";
                //System.Windows.Forms.MessageBox.Show("proc.Id: "+Id);
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    while (Nodes.MoveNext()) // NOTE: Necéssaire pour se placer sur le noeud recherché
                    {
                        //System.Windows.Forms.MessageBox.Show("XMLStep.RemoveByProc.Current to del: " + Nodes.Current.Name + "  " + Nodes.Current.GetAttribute("id", ""));
                        Nodes.Current.DeleteSelf();
                        //System.Windows.Forms.MessageBox.Show("XMLStep.RemoveByProc.Current: " + Nodes.Current.Name);

                        /* On lance la recherche */
                        Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                    }

                    /* On lance la recherche */
                    //Nodes = Navigator.Select(Navigator.Compile(ExpXPath));

                    //Nodes.MoveNext();
                    Nodes = Navigator.Select(Navigator.Compile("//Etapes"));
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstChild();
                    //System.Windows.Forms.MessageBox.Show("XMLStep.RemoveByProc.Current..: " + Nodes.Current.Name+"  HasChildren: "+Nodes.Current.HasChildren);
                    Nodes.Current.MoveToNext();
                    //System.Windows.Forms.MessageBox.Show("XMLStep.RemoveByProc.Current..: " + Nodes.Current.Name);
                    int childCount = ChildrenCount(/*"etape.xml"*/XmlDoc, "//Etapes");

                    if (childCount <= 1)
                    {
                        //System.Windows.Forms.MessageBox.Show("ChildCount<=1 "+childCount+" Current: " + Nodes.Current.Name, "XMLStep.RemoveByProc");
                        Helper.service.DeleteFile("etape.xml");
                        return true;
                    }
                }
                //return false;

                //XmlDoc.Save((Stream)Helper.service.LoadFile("etape.xml");
                Helper.service.SaveXmlFile("etape.xml", XmlDoc);
            }
            //catch (Exception x)
            {
                //System.Windows.Forms.MessageBox.Show(x.ToString());
                //return false;
            }
            return ret;
        }

        private static int ChildrenCount(/*string xmlFile,*/ XmlDocument XmlDoc, string NodeExpPath)
        {
            //XmlDocument XmlDoc = new XmlDocument();
            //string rslt = Helper.service.LoadFile(xmlFile).ToString();
            //System.Windows.Forms.MessageBox.Show(rslt,"XMLStep.ChildrenCount");
            //StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            //sw.Write(rslt);
            //sw.Close();


            //XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");

            XPathNavigator Navigator;
            XPathNodeIterator Nodes;

            Navigator = XmlDoc.CreateNavigator();
            /* On lance la recherche */
            Nodes = Navigator.Select(Navigator.Compile(NodeExpPath));
            Nodes.MoveNext();
            //System.Windows.Forms.MessageBox.Show("XMLStep.ChildrenCount: " + Nodes.Current.Name);
            int childCount = 0;
            if (Nodes.Current.HasChildren)
            {
                Nodes.Current.MoveToFirstChild();
                childCount++;
                while (Nodes.Current.MoveToNext())
                {
                    childCount++;
                }
            }
            return childCount;
        }
    }
}