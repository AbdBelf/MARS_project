using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using MARS_Expert.ResourceManager;

namespace MARS_Expert.Manager.XMLResourceManager
{
    class XMLFailure
    {
        /// <summary>
        /// Creates the file "panne.xml" and inserts the first panne to it.
        /// </summary>
        /// <param name="expert">The object holding the data.</param>
        /// <returns></returns>
        private static bool firstAdd(Panne failure)
        {
            try
            {
                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration
                xw.WriteStartDocument();

                // Write the root node
                xw.WriteStartElement("Pannes");//<Pannes>

                // Write the expert and the expert elements
                xw.WriteStartElement("panne");//<panne>
                xw.WriteStartAttribute("id");
                xw.WriteString("0");
                xw.WriteEndAttribute();
                //----------------
                xw.WriteStartElement("libelle");//<libelle>
                xw.WriteString(failure.getName());
                xw.WriteEndElement();//</libelle>
                //-----------------
                xw.WriteStartElement("description");//<description>
                xw.WriteString(failure.getDescription());
                xw.WriteEndElement();//</description>
                //-----------------
                xw.WriteStartElement("type_panne");//<type_panne>
                xw.WriteString(failure.getTypePanne().getId().ToString());
                xw.WriteEndElement();//</type_panne>
                //-----------------
                xw.WriteEndElement();//</panne>

                xw.WriteStartElement("MaxID");//<MaxID>
                xw.WriteString("0");
                xw.WriteEndElement();//</MaxID>

                // Close the document
                xw.WriteEndDocument();//</Pannes>

                // Flush the write
                xw.Flush();

                Byte[] buffer = new Byte[ms.Length];
                buffer = ms.ToArray();
                string xmlOutput = System.Text.Encoding.UTF8.GetString(buffer);

                //File.WriteAllText((Stream)Helper.service.LoadFile("panne.xml", xmlOutput);
                Helper.service.CreateXmlFile("panne.xml", xmlOutput);
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString()); 
            }
            return true;
        }

        /// <summary>
        /// inserts a panne to "panne.xml".
        /// </summary>
        /// <param name="expert">The object holding the data</param>
        /// <returns></returns>
        private static bool insert(Panne failure)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                Int32 ID; /* Variable utilisée pour savoir quel est l'ID qu'il faut affecter au nouveau
                       * noeud créé */
                string rslt = Helper.service.LoadFile("panne.xml").ToString();

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
                ExpXPath = "//panne[@id='" + ID.ToString() + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext();
                    /* On crée le noeud principal (panne). */
                    Nodes.Current.InsertElementAfter("", "panne", "", "");
                    /* On se place sur le noeud ainsi créé. */
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    ID++; /* On incrémente ID pour que sa valeur soit identique à celle se
                       * trouvant dans le noeud MaxID. */
                    /* Encodage des données */
                    Nodes.Current.CreateAttribute("", "id", "", ID.ToString());
                    Nodes.Current.AppendChildElement("", "libelle", "", failure.getName());
                    Nodes.Current.AppendChildElement("", "description", "", failure.getDescription());
                    Nodes.Current.AppendChildElement("", "type_panne", "", failure.getTypePanne().getId().ToString());
                    //XmlDoc.Save((Stream)Helper.service.LoadFile("panne.xml");
                    Helper.service.SaveXmlFile("panne.xml", XmlDoc);
                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.FileNotFoundException x) { }catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString()); 
            }
            return true;
        }

        public static bool Add(Panne failure)
        {
            //If the file exists we insert a new element.
            if (Helper.service.FileExists("panne.xml"))
            {
                return insert(failure);
            }
            else //Otherwise we create the file and insert the first elemet.
            {
                return firstAdd(failure);
            }
        }

        private static int ChildrenCount(string xmlFile, string NodeExpPath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            string rslt = Helper.service.LoadFile(xmlFile).ToString();

            StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            sw.Write(rslt);
            sw.Close();


            XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;

            Navigator = XmlDoc.CreateNavigator();
            /* On lance la recherche */
            Nodes = Navigator.Select(Navigator.Compile(NodeExpPath));
            Nodes.MoveNext();
            int childCount = 0;
            if (Nodes.Current.HasChildren)
            {
                Nodes.Current.MoveToFirstChild();
                childCount++;
                while (Nodes.MoveNext())
                {
                    childCount++;
                }
            }
            return childCount;
        }


        /// <summary>
        /// Gets the Id of the next failure to insert.
        /// </summary>
        /// <returns>The returned id or 0 if no element exists.</returns>
        public static int getNextId()
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            Int32 ID; /* Variable utilisée pour savoir quel est l'ID qu'il faut affecter au nouveau
                       * noeud créé */
            string rslt = Helper.service.LoadFile("panne.xml").ToString();

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

        internal static bool Exists(int id)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            string rslt = Helper.service.LoadFile("panne.xml").ToString();

            StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            sw.Write(rslt);
            sw.Close();


            XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            Navigator = XmlDoc.CreateNavigator();
            /* Recherche du noeud MaxID pour déterminer quelle sera l'ID du nouveau
             * procedure. */
            string ExpXPath = "//panne[@id='" + id.ToString() + "']";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            Nodes.MoveNext();
            /* On place l'ID le plus élevé du document dans la variable ID */
            if (Nodes.Count > 0)
                return true;
            else
                return false;
        }

        public static Panne GetById(int id)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Panne panne = new Panne();
            try
            {
                string rslt = Helper.service.LoadFile("panne.xml").ToString();

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

                string ExpXPath = "//panne[@id='" + id + "']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Etape */
                    panne.setId(id);
                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Libelle" */
                    panne.setName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Description"
                    panne.setDescription(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();// On se déplace sur le noeud suivant "type_panne"
                    panne.setTypePanne(XMLFailureType.GetById(Convert.ToInt32(Nodes.Current.Value)));
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    panne = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "etape" */
            return panne;
        }

        /// <summary>
        /// Modifies the Dailure Data.
        /// </summary>
        /// <param name="login">The id of the target Failure object.</param>
        /// <param name="admin">The object that holds the new data.</param>
        /// <returns></returns>
        public static bool Update(int id, Panne failure)
        {
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                string rslt = Helper.service.LoadFile("panne.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//panne[@id='" + id.ToString() + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstAttribute();
                    Nodes.Current.SetValue(failure.getId().ToString());
                    Nodes.Current.MoveToParent();

                    Nodes.Current.MoveToFirstChild();
                    //System.Windows.Forms.MessageBox.Show(Nodes.Current.Name.ToString() + " | " + Nodes.Current.Value.ToString());
                    Nodes.Current.SetValue(failure.getName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(failure.getDescription());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(failure.getTypePanne().getId().ToString());
                    //XmlDoc.Save((Stream)Helper.service.LoadFile("panne.xml");
                    Helper.service.SaveXmlFile("panne.xml", XmlDoc);
                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.FileNotFoundException x) { }catch (Exception x)
            { System.Windows.Forms.MessageBox.Show(x.ToString()); }
            return true;
        }

        /// <summary>
        /// Returns the list of failures.
        /// </summary>
        /// <returns></returns>
        public static List<Panne> GetAllFailures()
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Panne> pannes = new List<Panne>();
            Panne panne = new Panne();
            try
            {
                string rslt = Helper.service.LoadFile("panne.xml").ToString();

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

                string ExpXPath = "//panne";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                //System.Windows.Forms.MessageBox.Show("Node.count. "+Nodes.Count,"XMLFailureType.GetAllFailureTypes");
                if (Nodes.Count != 0)
                {
                    // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Etape */

                    int tillCount = 0;
                    while (tillCount < Nodes.Count)
                    {
                        Nodes.MoveNext();
                        panne = new Panne();
                        panne.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", "")));
                        //System.Windows.Forms.MessageBox.Show("Attrib. " + Nodes.Current.GetAttribute("id", ""), "XMLFailureType.GetAllFailureTypes");
                        Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Libelle" */
                        //System.Windows.Forms.MessageBox.Show("Current: " + Nodes.Current.Name + "    Current.Value " + Nodes.Current.Value, "XMLFailureType.GetAllFailureTypes");
                        panne.setName(Nodes.Current.Value);
                        //System.Windows.Forms.MessageBox.Show("libelle. " + Nodes.Current.Value, "XMLFailureType.GetAllFailureTypes");
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Description"
                        panne.setDescription(Nodes.Current.Value);
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "FailureType"
                        panne.setTypePanne(TypePanne.GetByID(Convert.ToInt32(Nodes.Current.Value)));
                        pannes.Add(panne);
                        tillCount++;
                        Nodes.Current.MoveToParent();
                    }
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    panne = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "etape" */
            return pannes;
        }

        /// <summary>
        /// For a specified type returns the list of failures.
        /// </summary>
        /// <returns></returns>
        public static List<Panne> GetAllFailuresByType(int typeId)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Panne> pannes = new List<Panne>();
            Panne panne = new Panne();
            try
            {
                string rslt = Helper.service.LoadFile("panne.xml").ToString();

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

                string ExpXPath = "//panne[type_panne='"+typeId+"']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                //System.Windows.Forms.MessageBox.Show("Node.count. "+Nodes.Count,"XMLFailureType.GetAllFailureTypes");
                if (Nodes.Count != 0)
                {
                    // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Etape */

                    int tillCount = 0;
                    while (tillCount < Nodes.Count)
                    {
                        Nodes.MoveNext();
                        panne = new Panne();
                        panne.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", "")));
                        //System.Windows.Forms.MessageBox.Show("Attrib. " + Nodes.Current.GetAttribute("id", ""), "XMLFailureType.GetAllFailureTypes");
                        Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Libelle" */
                        //System.Windows.Forms.MessageBox.Show("Current: " + Nodes.Current.Name + "    Current.Value " + Nodes.Current.Value, "XMLFailureType.GetAllFailureTypes");
                        panne.setName(Nodes.Current.Value);
                        //System.Windows.Forms.MessageBox.Show("libelle. " + Nodes.Current.Value, "XMLFailureType.GetAllFailureTypes");
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Description"
                        panne.setDescription(Nodes.Current.Value);
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "FailureType"
                        panne.setTypePanne(TypePanne.GetByID(Convert.ToInt32(Nodes.Current.Value)));
                        pannes.Add(panne);
                        tillCount++;
                        Nodes.Current.MoveToParent();
                    }
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    panne = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "etape" */
            return pannes;
        }
    }
}
