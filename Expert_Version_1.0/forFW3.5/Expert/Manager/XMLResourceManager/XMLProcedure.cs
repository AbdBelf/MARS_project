using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using MARS_Expert.ResourceManager;

namespace MARS_Expert.Manager.XMLResourceManager
{
    class XMLProcedure
    {
        /// <summary>
        /// Creates the file "procedure.xml" and inserts the first procedure to it.
        /// </summary>
        /// <param name="expert">The object holding the data.</param>
        /// <returns></returns>
        private static bool firstAdd(Procedure procedure)
        {
            try
            {
                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration
                xw.WriteStartDocument();

                // Write the root node
                xw.WriteStartElement("Procedures");

                // Write the expert and the expert elements
                xw.WriteStartElement("procedure");

                xw.WriteStartAttribute("id");
                xw.WriteString("0");
                xw.WriteEndAttribute();
                //----------------
                xw.WriteStartElement("libelle");
                xw.WriteString(procedure.getName());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("description");
                xw.WriteString(procedure.getDescription());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("panne");
                xw.WriteString(procedure.getPanne().getId().ToString());
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

                //File.WriteAllText((Stream)Helper.service.LoadFile("procedure.xml", xmlOutput);
                Helper.service.CreateXmlFile("procedure.xml", xmlOutput);

            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                //System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return true;
        }

        /// <summary>
        /// inserts a procedure to "procedure.xml".
        /// </summary>
        /// <param name="expert">The object holding the data</param>
        /// <returns></returns>
        private static bool insert(Procedure procedure)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                Int32 ID; /* Variable utilisée pour savoir quel est l'ID qu'il faut affecter au nouveau
                       * noeud créé */

                string rslt = Helper.service.LoadFile("procedure.xml").ToString();

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
                //ExpXPath = "//procedure[@id='" + ID.ToString() + "']";
                //Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    //Nodes.MoveNext();
                    Nodes.Current.MoveToPrevious();
                    /* On crée le noeud principal (procedure). */
                    Nodes.Current.InsertElementAfter("", "procedure", "", "");
                    /* On se place sur le noeud ainsi créé. */
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    ID++; /* On incrémente ID pour que sa valeur soit identique à celle se
                       * trouvant dans le noeud MaxID. */
                    /* Encodage des données */
                    Nodes.Current.CreateAttribute("", "id", "", ID.ToString());
                    Nodes.Current.AppendChildElement("", "libelle", "", procedure.getName());
                    Nodes.Current.AppendChildElement("", "description", "", procedure.getDescription());
                    Nodes.Current.AppendChildElement("", "panne", "", procedure.getPanne().getId().ToString());

                    //XmlDoc.Save((Stream)Helper.service.LoadFile("procedure.xml");
                    Helper.service.SaveXmlFile("procedure.xml", XmlDoc);
                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                //System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return true;
        }

        public static bool Add(Procedure procedure)
        {
            //If the file exists we insert a new element.
            if (Helper.service.FileExists("procedure.xml"))
            {
                //System.Windows.Forms.MessageBox.Show("Insert","XMLProcedure.Add");
                return insert(procedure);
            }
            else //Otherwise we create the file and insert the first elemet.
            {
                //System.Windows.Forms.MessageBox.Show("FirstAdd", "XMLProcedure.Add");
                return firstAdd(procedure);
            }
        }

        /// <summary>
        /// Modifies the Procedure Data.
        /// </summary>
        /// <param name="login">The id of the target Procedure object.</param>
        /// <param name="admin">The object that holds the new data.</param>
        /// <returns></returns>
        public static bool Update(int id, Procedure procedure)
        {
            //System.Windows.Forms.MessageBox.Show("Proc.Id: "+procedure.getId()+"  Proc.Name: "+procedure.getName(),"XMLProcedure.Modify");
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                string rslt = Helper.service.LoadFile("procedure.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");

                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//procedure[@id='" + id.ToString() + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstAttribute();
                    Nodes.Current.SetValue(procedure.getId().ToString());
                    Nodes.Current.MoveToParent();

                    Nodes.Current.MoveToFirstChild();
                    //System.Windows.Forms.MessageBox.Show(Nodes.Current.Name.ToString() + " | " + Nodes.Current.Value.ToString());
                    Nodes.Current.SetValue(procedure.getName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(procedure.getDescription());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(procedure.getPanne().getId().ToString());
                    //System.Windows.Forms.MessageBox.Show(Nodes.Current.Name + " | " + Nodes.Current.Value);
                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    //XmlDoc.Save((Stream)Helper.service.LoadFile("procedure.xml");
                    Helper.service.SaveXmlFile("procedure.xml", XmlDoc);
                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                //System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return true;
        }


        /// <summary>
        /// Gets the Id of the next procedure to insert.
        /// </summary>
        /// <returns>The returned id or 0 if no element exists.</returns>
        public static int getNextId()
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            Int32 ID; /* Variable utilisée pour savoir quel est l'ID qu'il faut affecter au nouveau
                       * noeud créé */
            string rslt = Helper.service.LoadFile("procedure.xml").ToString();

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
            string rslt = Helper.service.LoadFile("procedure.xml").ToString();

            StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
            sw.Write(rslt);
            sw.Close();


            XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");

            Navigator = XmlDoc.CreateNavigator();
            /* Recherche du noeud MaxID pour déterminer quelle sera l'ID du nouveau
             * procedure. */
            string ExpXPath = "//procedure[@id='" + id.ToString() + "']";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            Nodes.MoveNext();
            if (Nodes.Count > 0)
                return true;
            else
                return false;
        }

        public static Procedure GetById(int id)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Procedure procedure = new Procedure();
            try
            {
                string rslt = Helper.service.LoadFile("procedure.xml").ToString();

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

                string ExpXPath = "//procedure[@id='" + id + "']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Etape */
                    procedure.setId(id);
                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Libelle" */
                    procedure.setName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Description"
                    procedure.setDescription(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();// On se déplace sur le noeud suivant "type_panne"
                    procedure.setPanne(XMLFailure.GetById(Convert.ToInt32(Nodes.Current.Value)));
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    procedure = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }catch (Exception x)
            {
                //System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "etape" */
            return procedure;
        }

        /// <summary>
        /// Returns the list of the procedures.
        /// </summary>
        /// <returns></returns>
        public static List<Procedure> GetAllProcedures()
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Procedure> procedures = new List<Procedure>();
            Procedure procedure = new Procedure();
            try
            {
                string rslt = Helper.service.LoadFile("procedure.xml").ToString();

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

                string ExpXPath = "//procedure";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                //System.Windows.Forms.MessageBox.Show("Node.count. "+Nodes.Count,"XMLProcedure.GetAllProcedures");
                if (Nodes.Count != 0)
                {
                    // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Etape */

                    int tillCount = 0;
                    while (tillCount < Nodes.Count)
                    {
                        Nodes.MoveNext();
                        procedure = new Procedure();
                        procedure.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", "")));
                        //System.Windows.Forms.MessageBox.Show("Attrib. " + Nodes.Current.GetAttribute("id", ""), "XMLProcedure.GetAllProcedures");
                        Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Libelle" */
                        //System.Windows.Forms.MessageBox.Show("Current: " + Nodes.Current.Name + "    Current.Value " + Nodes.Current.Value, "XMLProcedure.GetAllProcedures");
                        procedure.setName(Nodes.Current.Value);
                        //System.Windows.Forms.MessageBox.Show("libelle. " + Nodes.Current.Value, "XMLProcedure.GetAllProcedures");
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Description"
                        //System.Windows.Forms.MessageBox.Show("Description. " + Nodes.Current.Value, "XMLProcedure.GetAllProcedures");
                        procedure.setDescription(Nodes.Current.Value);
                        //System.Windows.Forms.MessageBox.Show("Type.Description. " +type.getDescription() , "XMLProcedure.GetAllProcedures");
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Failure"
                        procedure.setPanne(Panne.GetByID(Convert.ToInt32(Nodes.Current.Value)));
                        procedures.Add(procedure);
                        tillCount++;
                        Nodes.Current.MoveToParent();
                    }
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    procedure = null;
                }
            }
            catch (System.IO.FileNotFoundException x) { }catch (Exception x)
            {
                //System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            /* Renvoi de toutes les données dans une instance de la classe "etape" */
            return procedures;
        }

        public static bool Delete(int id)
        {
            //System.Windows.Forms.MessageBox.Show("Proc.Id: "+procedure.getId()+"  Proc.Name: "+procedure.getName(),"XMLProcedure.Modify");
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                string rslt = Helper.service.LoadFile("procedure.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");

                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//procedure[@id='" + id.ToString() + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();
                    //System.Windows.Forms.MessageBox.Show(Nodes.Current.GetAttribute("id",""));
                    Nodes.Current.DeleteSelf();

                    //XmlDoc.Save((Stream)Helper.service.LoadFile("procedure.xml");
                    Helper.service.SaveXmlFile("procedure.xml", XmlDoc);
                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.FileNotFoundException x) { }
            catch (Exception x)
            {
                //System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return true;
        }
    }
}
