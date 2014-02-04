using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace Administrator.XML_Manager
{
    class XMLAdministrator : XMLActor
    {
        /// <summary>
        /// Adds an Adminstrator to the Data Base (Administrator.xml).
        /// </summary>
        /// <param name="admin">Data to insert.</param>
        /// <returns></returns>
        public static bool Add(Manager.Administrator admin)
        {
            try
            {
                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration
                xw.WriteStartDocument();

                // Write the root node
                xw.WriteStartElement("Administrator");

                // Write the administrator and the administrator elements
                xw.WriteStartElement("Admin");

                xw.WriteStartAttribute("login");
                xw.WriteString(admin.getLogin());
                xw.WriteEndAttribute();

                xw.WriteStartAttribute("password");
                xw.WriteString(admin.getPassword());
                xw.WriteEndAttribute();
                //----------------
                xw.WriteStartElement("firstName");
                xw.WriteString(admin.getFirstName());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("lastName");
                xw.WriteString(admin.getLastName());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("email");
                xw.WriteString(admin.getEmail());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("phoneNumber");
                xw.WriteString(admin.getPhoneNumber());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("address");
                xw.WriteString(admin.getAddress());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("role");
                xw.WriteString(admin.getRole());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("specialty");
                xw.WriteString(admin.getSpecialty());
                xw.WriteEndElement();
                //-----------------


                xw.WriteEndElement();

                // Close the document
                xw.WriteEndDocument();

                // Flush the write
                xw.Flush();

                Byte[] buffer = new Byte[ms.Length];
                buffer = ms.ToArray();
                string xmlOutput = System.Text.Encoding.UTF8.GetString(buffer);

                //File.WriteAllText(path + "Administrator.xml", xmlOutput);
                Program.service.CreateXmlFile("Administrator.xml", xmlOutput);
            }
            catch (Exception x) { return false; }
            return true;
        }

        /// <summary>
        /// The object that holds the new data.
        /// </summary>
        /// <param name="admin">The object that holds the data.</param>
        /// <returns></returns>
        public static bool Modify(Manager.Administrator admin)
        {
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                XmlDoc.Load(path+"Administrator.xml");
                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//Admin";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();

                    Nodes.Current.MoveToFirstAttribute();
                    //Nodes.Current.MoveToNextAttribute();
                    //System.Windows.Forms.MessageBox.Show("Current: " + Nodes.Current.Name + " value: " + admin.getLogin());
                    Nodes.Current.SetValue(admin.getLogin());

                    Nodes.Current.MoveToNextAttribute();
                    Nodes.Current.SetValue(admin.getPassword());
                    Nodes.Current.MoveToParent();

                    Nodes.Current.MoveToFirstChild();
                    Nodes.Current.SetValue(admin.getFirstName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(admin.getLastName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(admin.getEmail());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(admin.getPhoneNumber());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(admin.getAddress());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(admin.getRole());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(admin.getSpecialty());
                
                    //XmlDoc.Save(path+"Administrator.xml");
                    Program.service.SaveXmlFile("Administrator.xml", XmlDoc);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception x)
            { return false; }
            return true;
        }

        /// <summary>
        /// Returns the administrator data.
        /// </summary>
        /// <returns></returns>
        public static Manager.Administrator GetAdmin()
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Manager.Administrator admin = new Manager.Administrator();
            try
            {
                XPathDocument XPathDocu = new XPathDocument(path + "Administrator.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On affecte false à  la variable NoMatches afin de vérifier par la suite
              * si la recherche a été fructueuse*/
                admin.setnoMatch(false);

                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'admin*/
                string ExpXPath = "//Admin";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                //System.Windows.Forms.MessageBox.Show(Nodes.Count.ToString(),"XMLAdmin");
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché

                    admin.setLogin(Nodes.Current.GetAttribute("login", ""));
                    admin.setPassword(Nodes.Current.GetAttribute("password", ""));

                    //System.Windows.Forms.MessageBox.Show(admin.getPassword(),"XMLadmin");

                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                    admin.setFirstName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                    admin.setLastName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    admin.setEmail(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    admin.setPhoneNumber(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    admin.setAddress(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    admin.setRole(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    admin.setSpecialty(Nodes.Current.Value);

                }
                else
                    admin.setnoMatch(true);
            }
            catch (Exception x)
            {
                admin.setnoMatch(true);
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return admin;
        }

        /// <summary>
        /// Checks the administrator login.
        /// </summary>
        /// <returns></returns>
        public static bool Login(string login, string password)
        {
            try
            {
                XPathDocument XPathDocu = new XPathDocument(path + "Administrator.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;

                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'admin*/
                string ExpXPath = "//Admin[@login='" + login + "' and @password='" + password + "']";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                //System.Windows.Forms.MessageBox.Show(Nodes.Count.ToString(),"XMLAdmin");
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.Message.ToString());
                return false;
            }
        }
    }
}
