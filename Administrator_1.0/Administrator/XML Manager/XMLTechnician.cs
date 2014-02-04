using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.XPath;

namespace Administrator.XML_Manager
{
    class XMLTechnician : XMLActor
    {

        /// <summary>
        /// Creates the file "Technician.xml" and inserts the first Technician to it.
        /// </summary>
        /// <param name="technician">The object holding the data.</param>
        /// <returns></returns>
        public static bool firstAdd(Manager.Technician technician)
        {
            try
            {
                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration
                xw.WriteStartDocument();

                // Write the root node
                xw.WriteStartElement("Technicians");

                // Write the technician and the technician elements
                xw.WriteStartElement("Technician");

                xw.WriteStartAttribute("id");
                xw.WriteString("0");
                xw.WriteEndAttribute();

                xw.WriteStartAttribute("login");
                xw.WriteString(technician.getLogin());
                xw.WriteEndAttribute();

                xw.WriteStartAttribute("password");
                xw.WriteString(technician.getPassword());
                xw.WriteEndAttribute();
                //----------------
                xw.WriteStartElement("firstName");
                xw.WriteString(technician.getFirstName());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("lastName");
                xw.WriteString(technician.getLastName());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("email");
                xw.WriteString(technician.getEmail());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("phoneNumber");
                xw.WriteString(technician.getPhoneNumber());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("address");
                xw.WriteString(technician.getAddress());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("role");
                xw.WriteString(technician.getRole());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("specialty");
                xw.WriteString(technician.getSpecialty());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("status");
                xw.WriteString(technician.getStatus().ToString());
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

                //File.WriteAllText(path+"Technicians.xml", xmlOutput);
                Program.service.CreateXmlFile("Technicians.xml", xmlOutput);
            }
            catch (Exception x) { return false; }
            return true;
        }

        /// <summary>
        /// inserts an Technician to "Technicians.xml".
        /// </summary>
        /// <param name="technician">The object holding the data</param>
        /// <returns></returns>
        public static bool insert(Manager.Technician technician)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                Int32 ID; /* Variable utilisée pour savoir quel est l'ID qu'il faut affecter au nouveau
                       * noeud créé */
                XmlDoc.Load(path+"Technicians.xml");
                Navigator = XmlDoc.CreateNavigator();
                /* Recherche du noeud MaxID pour déterminer quelle sera l'ID du nouveau
                 * technician. */
                string ExpXPath = "//MaxID";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                Nodes.MoveNext();
                /* On place l'ID le plus élevé du document dans la variable ID */
                ID = Nodes.Current.ValueAsInt;
                /* On incrémente la valeur du noeud MaxID car une fois notre nouveau noeud 
                 * créé, l'ID le plus élevé du document sera aussi incrémenté */
                Nodes.Current.SetValue((ID + 1).ToString());
                /* On se place sur le noeud ayant l'ID le plus élevé */
                //ExpXPath = "//Technician[@id='" + ID.ToString() + "']";
                //Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    //Nodes.MoveNext();
                    Nodes.Current.MoveToPrevious();
                    System.Windows.Forms.MessageBox.Show("Current: "+Nodes.Current.Name);
                    /* On crée le noeud principal (Client). */
                    Nodes.Current.InsertElementAfter("", "Technician", "", "");
                    /* On se place sur le noeud ainsi créé. */
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    ID++; /* On incrémente ID pour que sa valeur soit identique à celle se
                       * trouvant dans le noeud MaxID. */
                    /* Encodage des données */
                    Nodes.Current.CreateAttribute("", "id", "", ID.ToString());
                    Nodes.Current.CreateAttribute("", "login", "", technician.getLogin());
                    Nodes.Current.CreateAttribute("", "password", "", technician.getPassword());
                    Nodes.Current.AppendChildElement("", "firstName", "", technician.getFirstName());
                    Nodes.Current.AppendChildElement("", "lastName", "", technician.getLastName());
                    Nodes.Current.AppendChildElement("", "email", "", technician.getEmail());
                    Nodes.Current.AppendChildElement("", "phoneNumber", "", technician.getPhoneNumber());
                    Nodes.Current.AppendChildElement("", "address", "", technician.getAddress());
                    Nodes.Current.AppendChildElement("", "role", "", technician.getRole());
                    Nodes.Current.AppendChildElement("", "specialty", "", technician.getSpecialty());
                    Nodes.Current.AppendChildElement("", "status", "", technician.getStatus().ToString());

                    //XmlDoc.Save(path+"Technicians.xml");
                    Program.service.SaveXmlFile("Technicians.xml", XmlDoc);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception x) { return false; }
            return true;
        }

        /// <summary>
        /// Modifies the technician Data.
        /// </summary>
        /// <param name="login">The login of the target technician object.</param>
        /// <param name="admin">The object that holds the new data.</param>
        /// <returns></returns>
        public static bool Modify(string login, Manager.Technician technician)
        {
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                XmlDoc.Load(path+"Technicians.xml");
                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//Technician[@login='" + login + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstAttribute();
                    Nodes.Current.MoveToNextAttribute();
                    Nodes.Current.SetValue(technician.getLogin());
                    Nodes.Current.MoveToNextAttribute();
                    Nodes.Current.SetValue(technician.getPassword());
                    Nodes.Current.MoveToParent();

                    Nodes.Current.MoveToFirstChild();
                    Nodes.Current.SetValue(technician.getFirstName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(technician.getLastName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(technician.getEmail());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(technician.getPhoneNumber());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(technician.getAddress());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(technician.getRole());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(technician.getSpecialty());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(technician.getStatus().ToString());
                    //XmlDoc.Save(path+"Technicians.xml");
                    Program.service.SaveXmlFile("Technicians.xml", XmlDoc);
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
        /// Deletes an technician but not permanently.It changes the Status only (Activated to Deactivated).
        /// </summary>
        /// <param name="login">The login of the target technician object.</param>
        /// <returns></returns>
        public static bool Delete(string login)
        {
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                XmlDoc.Load(path+"Technicians.xml");
                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//Technician[@login='" + login + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstChild();

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(Status.Deactivated.ToString());
                    //XmlDoc.Save(path+"Technicians.xml");
                    Program.service.SaveXmlFile("Technicians.xml", XmlDoc);
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

        public static bool Activate(string login)
        {
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                XmlDoc.Load(path + "Technicians.xml");
                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//Technician[@login='" + login + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstChild();

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);

                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(Status.Activated.ToString());
                    //XmlDoc.Save(path+"Technicians.xml");
                    Program.service.SaveXmlFile("Technicians.xml", XmlDoc);
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
        /// Search for a technician data using its id.
        /// </summary>
        /// <param name="id">The id of the targeted technician.</param>
        /// <returns></returns>
        public static Manager.Technician SearchById(int id)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Manager.Technician technician = new Manager.Technician();
            try
            {                
                XPathDocument XPathDocu = new XPathDocument(path + "Technicians.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On affecte false à  la variable NoMatches afin de vérifier par la suite
               * si la recherche a été fructueuse*/
                technician.setnoMatch(false);
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'technician à  partir de l'id */
                string ExpXPath = "//Technician[@id='" + id + "' and status != 'Deactivated']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe technician */
                    technician.setId(id); /* Pas besoin de chercher cette donnée vu que c'est notre 
                                   * critère de recherche, on peut donc directement
                                   * l'encoder. */
                    technician.setLogin(Nodes.Current.GetAttribute("login", ""));
                    technician.setPassword(Nodes.Current.GetAttribute("password", ""));
                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                    technician.setFirstName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                    technician.setLastName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setEmail(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setPhoneNumber(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setAddress(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setRole(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setSpecialty(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setStatus(Helper.StringToStatus(Nodes.Current.Value));
                }
                /* Si aucun technician n'a été trouvé */
                else
                {
                    technician.setnoMatch(true);
                }
            }
            catch (Exception x) { technician.setnoMatch(true); }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return technician;
        }

        /// <summary>
        /// Search for a technician data using its login.
        /// </summary>
        /// <param name="login">The login of the targeted technician.</param>
        /// <returns></returns>
        public static Manager.Technician SearchById(string login)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Manager.Technician technician = new Manager.Technician();
            try
            {
                XPathDocument XPathDocu = new XPathDocument(path + "Technicians.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On affecte false à  la variable NoMatches afin de vérifier par la suite
               * si la recherche a été fructueuse*/
                technician.setnoMatch(false);
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'technician à  partir de l'id */
                string ExpXPath = "//Technician[@login='" + login + "' and status != 'Deactivated']";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe technician */
                    technician.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", ""))); /* Pas besoin de chercher cette donnée vu que c'est notre 
                                   * critère de recherche, on peut donc directement
                                   * l'encoder. */
                    technician.setLogin(login);
                    technician.setPassword(Nodes.Current.GetAttribute("password", ""));
                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                    technician.setFirstName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                    technician.setLastName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setEmail(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setPhoneNumber(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setAddress(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setRole(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setSpecialty(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    technician.setStatus(Helper.StringToStatus(Nodes.Current.Value));
                }
                /* Si aucun technician n'a été trouvé */
                else
                {
                    technician.setnoMatch(true);
                }
            }
            catch (Exception x)
            {
                technician.setnoMatch(true);
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return technician;
        }

        /// <summary>
        /// Search a technician using his firstname or lastname.
        /// </summary>
        /// <param name="name">The first/last name of the technician.</param>
        /// <returns></returns>
        public static List<Manager.Technician> SearchByName(string name)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Manager.Technician> technicians = new List<Manager.Technician>();
            try
            {
                XPathDocument XPathDocu = new XPathDocument(path + "Technicians.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;

                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'technician à  partir de l'id */
                //string ExpXPath = "//Technician[(firstName='" + name + "' or lastName='" + name + "') and status != 'Deactivated']";

                //To eleminate the case sensetive in XPath we use the methode translate().
                string ExpXPath = "//Technician[(translate(firstName,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + name.ToUpper() +
                                               "' or translate(lastName,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + name.ToUpper() +
                                               "') and status != 'Deactivated']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    while (Nodes.MoveNext()) // NOTE: Necéssaire pour se placer sur le noeud recherché
                    {
                        Manager.Technician technician = new Manager.Technician();
                        /* Encodage des données dans la classe technician */
                        technician.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", ""))); /* Pas besoin de chercher cette donnée vu que c'est notre 
                                   * critère de recherche, on peut donc directement
                                   * l'encoder. */
                        technician.setLogin(Nodes.Current.GetAttribute("login", ""));
                        technician.setPassword(Nodes.Current.GetAttribute("password", ""));
                        Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                        technician.setFirstName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                        technician.setLastName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setEmail(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setPhoneNumber(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setAddress(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setRole(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setSpecialty(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setStatus(Helper.StringToStatus(Nodes.Current.Value));
                        technicians.Add(technician);
                    }
                }
            }
            catch (Exception x)
            { 
                //If there was a problem we re-instanciate the list so no element is sent.
                //It returns an empty list instead of null, then we make our test on list.count.
                technicians = new List<Manager.Technician>();
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return technicians;
        }

        /// <summary>
        /// Check if an id already exists in the database.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static bool IdExists(string login)
        {
            try
            {
                XPathDocument XPathDocu = new XPathDocument(path + "Technicians.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'technician à  partir de l'id */
                string ExpXPath = "//Technician[@login='" + login + "']";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    return true;
                }
            }
            catch (IOException x)
            {
                //This exception happens only for the first time when there is no xml file.
                //System.Windows.Forms.MessageBox.Show("Fichier "+path+"Technicians.xml est introuvable.","Message d'erreur",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Returns all the elements or only the Activated onces from Technicians.xml depending on the value of the boolean argument.
        /// </summary>
        /// <param name="evenDeactivated">When true get all elments, otherwise get only the activated onces.</param>
        /// <returns></returns>
        public static List<Manager.Technician> GetAll(bool evenDeactivated)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Manager.Technician> technicians = new List<Manager.Technician>();
            try
            {
                XPathDocument XPathDocu = new XPathDocument(path + "Technicians.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;

                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                //To eleminate the case sensetive in XPath we use the methode translate().
                string ExpXPath = "";
                if (evenDeactivated)
                    ExpXPath = "//Technician";
                else
                    ExpXPath = "//Technician[status != 'Deactivated']";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                //System.Windows.Forms.MessageBox.Show("XMLTechnician: "+Nodes.Count.ToString());
                if (Nodes.Count != 0)
                {
                    while (Nodes.MoveNext()) // NOTE: Necéssaire pour se placer sur le noeud recherché
                    {
                        Manager.Technician technician = new Manager.Technician();
                        /* Encodage des données dans la classe Technician */
                        technician.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", ""))); /* Pas besoin de chercher cette donnée vu que c'est notre 
                                   * critère de recherche, on peut donc directement
                                   * l'encoder. */
                        technician.setLogin(Nodes.Current.GetAttribute("login", ""));
                        technician.setPassword(Nodes.Current.GetAttribute("password", ""));
                        Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                        technician.setFirstName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                        technician.setLastName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setEmail(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setPhoneNumber(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setAddress(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setRole(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setSpecialty(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        technician.setStatus(Helper.StringToStatus(Nodes.Current.Value));
                        technicians.Add(technician);
                    }
                }
            }
            catch (Exception x)
            {
                //If there was a problem we re-instanciate the list so no element is sent.
                //It returns an empty list instead of null, then we make our test on list.count.
                technicians = new List<Manager.Technician>();
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return technicians;
        }
    }
}
