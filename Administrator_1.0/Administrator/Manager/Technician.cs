using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Administrator.Manager
{
    class Technician : Actor
    {
        int id;
        Status status;
        bool noMatch;

        public Technician()
        { }

        public Technician(string firstName, string lastName, string email, string phoneNumber,
        string address, string role, string specialty, string login,
        string password)
            : base(firstName, lastName, email, phoneNumber,
                address, role, specialty, login, password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.role = role;
            this.specialty = specialty;
            this.login = login;
            this.password = password;
            this.status = Status.Activated;
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public bool getnoMatch()
        {
            return this.noMatch;
        }

        public void setnoMatch(bool noMatch)
        {
            this.noMatch = noMatch;
        }

        public Status getStatus()
        {
            return this.status;
        }

        public void setStatus(Status status)
        {
            this.status = status;
        }

        public bool Exists()
        {
            return XML_Manager.XMLTechnician.IdExists(this.login);
        }

        public bool Add()
        {
            //if (File.Exists(XML_Manager.XMLActor.path + "Technicians.xml"))
            if (Program.service.FileExists("Technicians.xml"))
            {
                //System.Windows.Forms.MessageBox.Show(XML_Manager.XMLActor.path + "Technicians.xml "+"insert");
                return XML_Manager.XMLTechnician.insert(this);
            }
            else
            {
                //System.Windows.Forms.MessageBox.Show(XML_Manager.XMLActor.path + "Technicians.xml "+"Add");
                return XML_Manager.XMLTechnician.firstAdd(this);
            }
        }

        public bool Modify(string login)
        {
            return XML_Manager.XMLTechnician.Modify(login, this);
        }

        public bool Delete()
        {
            return XML_Manager.XMLTechnician.Delete(this.login);
        }

        public bool Activate()
        {
            return XML_Manager.XMLTechnician.Activate(this.login);
        }

        public Technician SearchById(int id)
        {
            return XML_Manager.XMLTechnician.SearchById(id);
        }

        public Technician SearchById(string login)
        {
            return XML_Manager.XMLTechnician.SearchById(login);
        }

        public List<Technician> SearchByName(string name)
        {
            return XML_Manager.XMLTechnician.SearchByName(name);
        }

        public List<Technician> GetAll(bool includeDeleted)
        {
            return XML_Manager.XMLTechnician.GetAll(includeDeleted);
        }
    }
}
