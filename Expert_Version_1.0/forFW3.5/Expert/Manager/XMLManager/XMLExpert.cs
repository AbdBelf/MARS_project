using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using MARS_Expert.Manager;

namespace MARS_Expert.Manager.XMLManager
{
    class XMLExpert :Manager.XMLManager.XMLActor
    {
        /// <summary>
        /// Creates the file "Experts.xml" and inserts the first Expert to it.
        /// </summary>
        /// <param name="expert">The object holding the data.</param>
        /// <returns></returns>
        public static bool firstAdd(Manager.ExpertManager.Expert expert)
        {
            try
            {
                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration
                xw.WriteStartDocument();

                // Write the root node
                xw.WriteStartElement("Experts");

                // Write the expert and the expert elements
                xw.WriteStartElement("Expert");

                xw.WriteStartAttribute("id");
                xw.WriteString("0");
                xw.WriteEndAttribute();

                xw.WriteStartAttribute("login");
                xw.WriteString(expert.getLogin());
                xw.WriteEndAttribute();

                xw.WriteStartAttribute("password");
                xw.WriteString(expert.getPassword());
                xw.WriteEndAttribute();
                //----------------
                xw.WriteStartElement("firstName");
                xw.WriteString(expert.getFirstName());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("lastName");
                xw.WriteString(expert.getLastName());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("email");
                xw.WriteString(expert.getEmail());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("phoneNumber");
                xw.WriteString(expert.getPhoneNumber());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("address");
                xw.WriteString(expert.getAddress());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("role");
                xw.WriteString(expert.getRole());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("specialty");
                xw.WriteString(expert.getSpecialty());
                xw.WriteEndElement();
                //-----------------
                xw.WriteStartElement("status");
                xw.WriteString(expert.getStatus().ToString());
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

                //File.WriteAllText(path+"Experts.xml", xmlOutput);
                Helper.service.CreateXmlFile("Experts.xml", xmlOutput);
            }
            catch (Exception x) { return false; }
            return true;
        }

        /// <summary>
        /// inserts an Expert to "Experts.xml".
        /// </summary>
        /// <param name="expert">The object holding the data</param>
        /// <returns></returns>
        public static bool insert(Manager.ExpertManager.Expert expert)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                Int32 ID; /* Variable utilisée pour savoir quel est l'ID qu'il faut affecter au nouveau
                       * noeud créé */
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");


                Navigator = XmlDoc.CreateNavigator();
                /* Recherche du noeud MaxID pour déterminer quelle sera l'ID du nouveau
                 * expert. */
                string ExpXPath = "//MaxID";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                Nodes.MoveNext();
                /* On place l'ID le plus élevé du document dans la variable ID */
                ID = Nodes.Current.ValueAsInt;
                /* On incrémente la valeur du noeud MaxID car une fois notre nouveau noeud 
                 * créé, l'ID le plus élevé du document sera aussi incrémenté */
                Nodes.Current.SetValue((ID + 1).ToString());
                /* On se place sur le noeud ayant l'ID le plus élevé */
                ExpXPath = "//Expert[@id='" + ID.ToString() + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext();
                    /* On crée le noeud principal (Client). */
                    Nodes.Current.InsertElementAfter("", "Expert", "", "");
                    /* On se place sur le noeud ainsi créé. */
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    ID++; /* On incrémente ID pour que sa valeur soit identique à celle se
                       * trouvant dans le noeud MaxID. */
                    /* Encodage des données */
                    Nodes.Current.CreateAttribute("", "id", "", ID.ToString());
                    Nodes.Current.CreateAttribute("", "login", "", expert.getLogin());
                    Nodes.Current.CreateAttribute("", "password", "", expert.getPassword());
                    Nodes.Current.AppendChildElement("", "firstName", "", expert.getFirstName());
                    Nodes.Current.AppendChildElement("", "lastName", "", expert.getLastName());
                    Nodes.Current.AppendChildElement("", "email", "", expert.getEmail());
                    Nodes.Current.AppendChildElement("", "phoneNumber", "", expert.getPhoneNumber());
                    Nodes.Current.AppendChildElement("", "address", "", expert.getAddress());
                    Nodes.Current.AppendChildElement("", "role", "", expert.getRole());
                    Nodes.Current.AppendChildElement("", "specialty", "", expert.getSpecialty());
                    Nodes.Current.AppendChildElement("", "status", "", expert.getStatus().ToString());

                    //XmlDoc.Save(path+"Experts.xml");
                    Helper.service.SaveXmlFile("Experts.xml",XmlDoc);
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
        /// Modifies the Expert Data.
        /// </summary>
        /// <param name="login">The login of the target Expert object.</param>
        /// <param name="admin">The object that holds the new data.</param>
        /// <returns></returns>
        public static bool Modify(string login, Manager.ExpertManager.Expert expert)
        {
            //System.Windows.Forms.MessageBox.Show(expert.getFirstName(),"XMLExpert");
            try
            {
                /* On utilise un XmlDocument et non un XPathDocument car ce dernier ne permet
            * pas l'édition des données XML. */
                XmlDocument XmlDoc = new XmlDocument();
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//Expert[@login='" + login + "']";
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                if (Nodes.Count != 0)
                {
                    /* Encodage des nouvelles données */
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstAttribute();
                    Nodes.Current.MoveToNextAttribute();
                    Nodes.Current.SetValue(expert.getLogin());
                    Nodes.Current.MoveToNextAttribute();
                    Nodes.Current.SetValue(expert.getPassword());
                    Nodes.Current.MoveToParent();

                    Nodes.Current.MoveToFirstChild();
                    //System.Windows.Forms.MessageBox.Show(Nodes.Current.Name.ToString() + " | " + Nodes.Current.Value.ToString());
                    Nodes.Current.SetValue(expert.getFirstName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(expert.getLastName());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(expert.getEmail());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(expert.getPhoneNumber());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(expert.getAddress());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(expert.getRole());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(expert.getSpecialty());
                    Nodes.Current.MoveToNext(XPathNodeType.Element);
                    Nodes.Current.SetValue(expert.getStatus().ToString());
                    //XmlDoc.Save(path+"Experts.xml");
                    Helper.service.SaveXmlFile("Experts.xml", XmlDoc);
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
        /// Deletes an Expert but not permanently.It changes the Status only (Activated to Deactivated).
        /// </summary>
        /// <param name="login">The login of the target Expert object.</param>
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
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                XmlDoc.Load(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                Navigator = XmlDoc.CreateNavigator();
                string ExpXPath = "//Expert[@login='" + login + "']";
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
                    //XmlDoc.Save(path+"Experts.xml");
                    Helper.service.SaveXmlFile("Experts.xml", XmlDoc);
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
        /// Search for an Expert data using its id.
        /// </summary>
        /// <param name="id">The id of the targeted Expert.</param>
        /// <returns></returns>
        public static Manager.ExpertManager.Expert SearchById(int id)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Manager.ExpertManager.Expert expert = new Manager.ExpertManager.Expert();
            try
            {
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On affecte false à  la variable NoMatches afin de vérifier par la suite
               * si la recherche a été fructueuse*/
                expert.setnoMatch(false);
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'expert à  partir du login */
                string ExpXPath = "//Expert[@id='" + id + "' and status != 'Deactivated']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Expert */
                    expert.setId(id);
                    expert.setLogin(Nodes.Current.GetAttribute("login", ""));
                    expert.setPassword(Nodes.Current.GetAttribute("password", ""));
                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                    expert.setFirstName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                    expert.setLastName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setEmail(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setPhoneNumber(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setAddress(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setRole(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setSpecialty(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setStatus(Helper.StringToStatus(Nodes.Current.Value));
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    expert.setnoMatch(true);
                }
            }
            catch (Exception x)
            {
                expert.setnoMatch(true);
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return expert;
        }

        /// <summary>
        /// Search for an Expert data using its login.
        /// </summary>
        /// <param name="login">The login of the targeted Expert.</param>
        /// <returns></returns>
        public static Manager.ExpertManager.Expert SearchById(string login)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            Manager.ExpertManager.Expert expert = new Manager.ExpertManager.Expert();
            try
            {
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On affecte false à  la variable NoMatches afin de vérifier par la suite
               * si la recherche a été fructueuse*/
                expert.setnoMatch(false);
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'expert à  partir du login */
                string ExpXPath = "//Expert[@login='" + login + "' and status != 'Deactivated']";
                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Expert */
                    expert.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", "")));
                    expert.setLogin(login);
                    expert.setPassword(Nodes.Current.GetAttribute("password", ""));
                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                    expert.setFirstName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                    expert.setLastName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setEmail(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setPhoneNumber(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setAddress(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setRole(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setSpecialty(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setStatus(Helper.StringToStatus(Nodes.Current.Value));
                }
                /* Si aucun expert n'a été trouvé */
                else
                {
                    expert.setnoMatch(true);
                }
            }
            catch (Exception x)
            { expert.setnoMatch(true); }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return expert;
        }

        /// <summary>
        /// Search an Expert using his firstname or lastname.
        /// </summary>
        /// <param name="name">The first/last name of the Expert.</param>
        /// <returns></returns>
        public static List<Manager.ExpertManager.Expert> SearchByName(string name)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Manager.ExpertManager.Expert> experts = new List<Manager.ExpertManager.Expert>();
            try
            {
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

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
                //string ExpXPath = "//Expert[(firstName='" + name + "' or lastName='" + name + "') and status != 'Deactivated']";

                //To eleminate the case sensetive in XPath we use the methode translate().
                string ExpXPath = "//Expert[(translate(firstName,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + name.ToUpper() +
                                               "' or translate(lastName,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + name.ToUpper() +
                                               "') and status != 'Deactivated']";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    while (Nodes.MoveNext()) // NOTE: Necéssaire pour se placer sur le noeud recherché
                    {
                        Manager.ExpertManager.Expert expert = new Manager.ExpertManager.Expert();
                        /* Encodage des données dans la classe Expert */
                        expert.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", ""))); /* Pas besoin de chercher cette donnée vu que c'est notre 
                                   * critère de recherche, on peut donc directement
                                   * l'encoder. */
                        expert.setLogin(Nodes.Current.GetAttribute("login", ""));
                        expert.setPassword(Nodes.Current.GetAttribute("password", ""));
                        Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                        expert.setFirstName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                        expert.setLastName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setEmail(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setPhoneNumber(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setAddress(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setRole(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setSpecialty(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setStatus(Helper.StringToStatus(Nodes.Current.Value));
                        experts.Add(expert);
                    }
                }
            }
            catch (Exception x)
            {
                //If there was a problem we re-instanciate the list so no element is sent.
                //It returns an empty list instead of null, then we make our test on list.count.
                experts = new List<Manager.ExpertManager.Expert>();
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return experts;
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
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;
                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'technician à  partir de l'id */
                string ExpXPath = "//Expert[@login='" + login + "']";

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
                //System.Windows.Forms.MessageBox.Show("Fichier "+path+"Data\\Technicians.xml est introuvable.","Message d'erreur",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Returns all the elements or only the Activated onces from Experts.xml depending on the value of the boolean argument.
        /// </summary>
        /// <param name="evenDeactivated">When true get all elments, otherwise get only the activated onces.</param>
        /// <returns></returns>
        public static List<Manager.ExpertManager.Expert> GetAll(bool evenDeactivated)
        {
            /* On déclare et on crée une instance des variables nécéssaires pour la recherche */
            List<Manager.ExpertManager.Expert> experts = new List<Manager.ExpertManager.Expert>();
            try
            {
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

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
                //string ExpXPath = "//Expert[(firstName='" + name + "' or lastName='" + name + "') and status != 'Deactivated']";

                //To eleminate the case sensetive in XPath we use the methode translate().
                string ExpXPath = "";
                if (evenDeactivated)
                    ExpXPath = "//Expert";
                else
                    ExpXPath = "//Expert[status != 'Deactivated']";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                /* On vérifie si la recherche a été fructueuse */
                if (Nodes.Count != 0)
                {
                    while (Nodes.MoveNext()) // NOTE: Necéssaire pour se placer sur le noeud recherché
                    {
                        Manager.ExpertManager.Expert expert = new Manager.ExpertManager.Expert();
                        /* Encodage des données dans la classe Expert */
                        expert.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", ""))); /* Pas besoin de chercher cette donnée vu que c'est notre 
                                   * critère de recherche, on peut donc directement
                                   * l'encoder. */
                        expert.setLogin(Nodes.Current.GetAttribute("login", ""));
                        expert.setPassword(Nodes.Current.GetAttribute("password", ""));

                        //System.Windows.Forms.MessageBox.Show(expert.getPassword(),"XMLExpert");

                        Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                        expert.setFirstName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                        expert.setLastName(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setEmail(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setPhoneNumber(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setAddress(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setRole(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setSpecialty(Nodes.Current.Value);
                        Nodes.Current.MoveToNext();
                        expert.setStatus(Helper.StringToStatus(Nodes.Current.Value));
                        experts.Add(expert);
                    }
                }
            }
            catch (Exception x)
            {
                //If there was a problem we re-instanciate the list so no element is sent.
                //It returns an empty list instead of null, then we make our test on list.count.
                experts = new List<Manager.ExpertManager.Expert>();
            }
            /* Renvoi de toutes les données dans une instance de la classe "Client" */
            return experts;
        }
        
        /// <summary>
        /// Checks the expert login.
        /// </summary>
        /// <returns></returns>
        public static bool Login(string login, string password)
        {
            try
            {
                //XPathDocument XPathDocu = new XPathDocument(@"F:\Android3\Experts.xml");
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();
                

                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;

                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'admin*/
                string ExpXPath = "//Expert[@login='" + login + "' and @password='" + password + "' and status='Activated']";

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
                System.Windows.Forms.MessageBox.Show(x.ToString(),"XMLExpert.Login");
                return false;
            }
        }

       /// <summary>
        /// Returns an expert using its login.
       /// </summary>
       /// <param name="login">Login de l'expert.</param>
       /// <param name="password"></param>
        /// <returns>The expert if exists.</returns>
        public static ExpertManager.Expert getExpert(string login)
        {
            Manager.ExpertManager.Expert expert = new Manager.ExpertManager.Expert();
            try
            {
                string rslt = Helper.service.LoadFile("Experts.xml").ToString();

                StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                sw.Write(rslt);
                sw.Close();


                //XPathDocument XPathDocu = new XPathDocument((Stream)Helper.service.LoadFile("Experts.xml"));
                XPathDocument XPathDocu = new XPathDocument(System.Windows.Forms.Application.StartupPath + "\\temp.xml");
                
                XPathNavigator Navigator;
                XPathNodeIterator Nodes;

                /* On crée un navigateur */
                Navigator = XPathDocu.CreateNavigator();
                /* On crée ici l'expression XPath de recherche d'admin*/
                string ExpXPath = "//Expert[@login='" + login + "']";

                /* On lance la recherche */
                Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
                //System.Windows.Forms.MessageBox.Show(Nodes.Count.ToString(),"XMLAdmin");
                if (Nodes.Count != 0)
                {
                    Nodes.MoveNext(); // NOTE: Necéssaire pour se placer sur le noeud recherché
                    /* Encodage des données dans la classe Expert */
                    expert.setId(Convert.ToInt32(Nodes.Current.GetAttribute("id", ""))); /* Pas besoin de chercher cette donnée vu que c'est notre 
                                   * critère de recherche, on peut donc directement
                                   * l'encoder. */
                    expert.setLogin(Nodes.Current.GetAttribute("login", ""));
                    expert.setPassword(Nodes.Current.GetAttribute("password", ""));

                    Nodes.Current.MoveToFirstChild(); /* On se déplace sur le premier noeud 
                                                   * enfant "Prenom" */
                    expert.setFirstName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext(); // On se déplace sur le noeud suivant "Nom"
                    expert.setLastName(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setEmail(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setPhoneNumber(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setAddress(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setRole(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setSpecialty(Nodes.Current.Value);
                    Nodes.Current.MoveToNext();
                    expert.setStatus(Helper.StringToStatus(Nodes.Current.Value));

                }
            }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.Message.ToString());
                return null;
            }
            return expert;
        }
    }
}
