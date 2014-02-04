using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MARS_Expert.Manager.ExpertManager
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
            return XMLManager.XMLExpert.IdExists(this.login);
        }

        public bool Add()
        {
            if (Helper.service.FileExists("Data\\Experts.xml"))
                return XMLManager.XMLExpert.insert(this);
            else
                return XMLManager.XMLExpert.firstAdd(this);
        }

        public bool Modify(string login)
        {
            return XMLManager.XMLExpert.Modify(login, this);
        }

        public bool Delete()
        {
            return XMLManager.XMLExpert.Delete(this.login);
        }

        public Expert SearchById(int id)
        {
            return XMLManager.XMLExpert.SearchById(id);
        }

        public Expert SearchById(string login)
        {
            return XMLManager.XMLExpert.SearchById(login);
        }

        public List<Expert> SearchByName(string name)
        {
            return XMLManager.XMLExpert.SearchByName(name);
        }

        public List<Expert> GetAll(bool includeDeleted)
        {
            return XMLManager.XMLExpert.GetAll(includeDeleted);
        }

        public ExpertManager.Expert getExpert(string login)
        {
            return XMLManager.XMLExpert.getExpert(login);
        }
    }
}
