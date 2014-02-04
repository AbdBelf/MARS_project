using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Administrator.Manager
{
    public class Administrator : Actor
    {
        public bool noMatch;
        public Administrator()
        { }

        public Administrator(string firstName, string lastName, string email, string phoneNumber,
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
        }

        public bool getnoMatch()
        {
            return this.noMatch;
        }

        public void setnoMatch(bool noMatch)
        {
            this.noMatch = noMatch;
        }

        public bool Add()
        {
            return XML_Manager.XMLAdministrator.Add(this);
        }

        public bool Modify()
        {
            return XML_Manager.XMLAdministrator.Modify(this);
        }

        public Manager.Administrator GetAdmin()
        {
            return XML_Manager.XMLAdministrator.GetAdmin();
        }

    }
}
