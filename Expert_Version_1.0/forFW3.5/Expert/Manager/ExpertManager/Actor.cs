using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MARS_Expert.Manager.ExpertManager
{
    public class Actor
    {
        protected string firstName;
        protected string lastName;
        protected string email;
        protected string phoneNumber;
        protected string address;
        protected string role;//Expert or Technician
        protected string specialty;//Domaine de travail
        protected string login;// Ne doit pas être un entier, il doit avoir un sens. On peut utiliser l'email comme Id mais il est trop long pour les testes à chaque fois qu'on veut verifier.
        protected string password;

        public Actor()
        {
        }

        public Actor(string firstName, string lastName, string email, string phoneNumber,
        string address, string role, string specialty, string login,
        string password)
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

        public string getFirstName()
        {
            return firstName;
        }

        public void setFirstName(string firstName)
        {
            this.firstName = firstName;
        }

        public string getLastName()
        {
            return lastName;
        }

        public void setLastName(string lastName)
        {
            this.lastName = lastName;
        }

        public string getEmail()
        {
            return email;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }

        public string getPhoneNumber()
        {
            return phoneNumber;
        }

        public void setPhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public string getAddress()
        {
            return address;
        }

        public void setAddress(string address)
        {
            this.address = address;
        }

        public string getRole()
        {
            return role;
        }

        public void setRole(string role)
        {
            this.role = role;
        }

        public string getSpecialty()
        {
            return specialty;
        }

        public void setSpecialty(string specialty)
        {
            this.specialty = specialty;
        }

        public string getLogin()
        {
            return login;
        }

        public void setLogin(string login)
        {
            this.login = login;
        }

        public string getPassword()
        {
            return password;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public bool HasValidFirstName()
        {
            return Check.IsWord(this.firstName);
        }

        public bool HasValidLastName()
        {
            return Check.IsWord(this.lastName);
        }

        public bool HasValidEmail()
        {
            return Check.IsEmailAddress(this.email);
        }

        public bool HasValidPhoneNumber()
        {
            return Check.IsPhoneNumber(this.phoneNumber);
        }

        public bool HasValidAddress()
        {
            return !Check.ContainsInvalidChar(this.address) && !Check.ContainsSpecialCharacter(this.address);
        }

        public bool HasValidSpecialty()
        {
            //Specialty should be a set of letters and white spaces only.
            return Check.IsWord(this.specialty);
        }

        public bool HasValidLogin()
        {
            return !Check.ContainsInvalidChar(this.login) && !Check.ContainsSpecialCharacter(this.login);
        }

        public bool HasValidPassword()
        {
            return (!Check.ContainsInvalidChar(this.password) && this.password.Length >= 6);
        }
    }
}
