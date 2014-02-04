using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Administrator.Manager
{
    class Expert : Actor
    {
        int id;
        Status status;
        bool noMatch;

        public Expert()
        {
        }

        public Expert(string firstName, string lastName, string email, string phoneNumber,
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
            return XML_Manager.XMLExpert.IdExists(this.login);
        }

        public bool Add()
        {
            //if (File.Exists(XML_Manager.XMLActor.path + "Experts.xml"))
            if (Program.service.FileExists("Experts.xml"))
                return XML_Manager.XMLExpert.insert(this);
            else
                return XML_Manager.XMLExpert.firstAdd(this);
        }

        public bool Modify(string login)
        {
            return XML_Manager.XMLExpert.Modify(login, this);
        }

        public bool Delete()
        {
            return XML_Manager.XMLExpert.Delete(this.login);
        }

        public bool Activate()
        {
            return XML_Manager.XMLExpert.Activate(this.login);
        }

        public Expert SearchById(int id)
        {
            return XML_Manager.XMLExpert.SearchById(id);
        }

        public Expert SearchById(string login)
        {
            return XML_Manager.XMLExpert.SearchById(login);
        }

        public List<Expert> SearchByName(string name)
        {
            return XML_Manager.XMLExpert.SearchByName(name);
        }

        public List<Expert> GetAll(bool includeDeleted)
        {
            return XML_Manager.XMLExpert.GetAll(includeDeleted);
        }
    }
}
