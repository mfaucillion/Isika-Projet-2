﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Projet2Crowdfunding.Models;

namespace Projet2Crowdfunding.Service
{
    public class AccountService
    {
        private BddContext bddContext;
        public AccountService()
        {
            bddContext = new BddContext();
        }

        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "Projet2Crowdfunding" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }


        public int CreateParticipant(string lastName, string firstname, string mail, string password)
        {
            string encodedPassword = EncodeMD5(password);
     
            Account account = new Account()
            {
                Mail = mail,
                Password = encodedPassword
            };

            Participant participant = new Participant()
            {
                FirstName = firstname,
                LastName = lastName,           
                ParticipantAccount = new ParticipantAccount()
                {
                    Newsletter = false,
                    ConfidentialityCharter = true,
                    Account = account
                },          
            };

            this.bddContext.Participants.Add(participant);
            this.bddContext.SaveChanges();
            return account.Id;
        }


        public int CreateProjectOwner(string name, string phoneNumber, string mail, string password
            , string summary, string description, string hyperlink, string volunteerDescritpion
        , string patnerShip, ProjectOwnerType type, string image, string associationProof, string streetNumber
            , string streetName, string zipCode, string city, string country)

        {
            string encodedPassword = EncodeMD5(password);
            Account account = new Account()
            {
                Mail = mail,
                Password = encodedPassword
            };
            Address address = new Address()
            {
                StreetNumber = streetNumber,
                StreetName = streetName,
                ZipCode = zipCode,
                City = city,
                Country = country
            };

            ProjectOwner projectOwner = new ProjectOwner()
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Summary = summary,
                Description = description,
                HyperLink = hyperlink,
                VolunteerDescritpion = volunteerDescritpion,
                Partnership = patnerShip,
                AssociationProof = associationProof,
                Image = image,
                Type = type,
                Status = AssoStatus.registered,


                ProjectOwnerAccount = new ProjectOwnerAccount()
                {
                    Newsletter = false,
                    ConfidentialityCharter = true,
                    Account = account
                },
                Address = address


            };

            this.bddContext.ProjectOwners.Add(projectOwner);
            this.bddContext.SaveChanges();
            return account.Id;
        }

        public int CreateAdministrator(string lastName, string firstName, string mail, string password
        , string phoneNumber)
        {
            string encodedPassword = EncodeMD5(password);
            Account account = new Account()
            {
                Mail = mail,
                Password = encodedPassword
            };

            Administrator administrator = new Administrator()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                AdministratorAccount = new AdministratorAccount()
                {
                    Account = account
                },

            };


            this.bddContext.Administrators.Add(administrator);
            this.bddContext.SaveChanges();
            return account.Id;

        }

        public Account Login(string mail, string password)
        {
            string encodedPassword = EncodeMD5(password);
            Account account = this.bddContext.Accounts.FirstOrDefault
                (a => a.Mail == mail && a.Password == password);
            return account;
        }


        public Participant GetParticipant(int id)
        {
            return this.bddContext.Participants.Find(id);
        }

        public Participant GetParticipant(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetParticipant(id);
            }
            return null;
        }


        public ProjectOwner GetProjectOwner(int id)
        {
            return this.bddContext.ProjectOwners.Find(id);
        }

        public ProjectOwner GetProjectOwner(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetProjectOwner(id);
            }
            return null;
        }


        public Administrator GetAdministrator(int id)
        {
            return this.bddContext.Administrators.Find(id);
        }

        public Administrator GetAdministrator(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetAdministrator(id);
            }
            return null;
        }




        public void Dispose()
        {
            bddContext.Dispose();
        }
    }
}


//        public int CreateParticipant(string lastName, string firstname, string mail, string password
//      , Gender gender, DateTime birthdate, string phoneNumber, string streetNumber
//          , string streetName, string zipCode, string city, string country)
//        {
//            string encodedPassword = EncodeMD5(password);
//            Address address = new Address()
//            {
//                StreetNumber = streetNumber,
//                StreetName = streetName,
//                ZipCode = zipCode,
//                City = city,
//                Country = country
//            };
//            Account account = new Account()
//            {
//                Mail = mail,
//                Password = encodedPassword
//            };

//            Participant participant = new Participant()
//            {
//                FirstName = firstname,
//                LastName = lastName,
//                Gender = gender,
//                Birthdate = birthdate,

//                PhoneNumber = phoneNumber,

//                ParticipantAccount = new ParticipantAccount()
//                {
//                    Newsletter = false,
//                    ConfidentialityCharter = true,
//                    Account = account
//                },
//                Address = address
//            };

//            this.bddContext.Participants.Add(participant);
//            this.bddContext.SaveChanges();
//            return participant.Id;
//        }


//        public int CreateProjectOwner(string name, string phoneNumber, string mail, string password
//            , string summary, string description, string hyperlink, string volunteerDescritpion
//        , string patnerShip, ProjectOwnerType type, string image, AssoStatus status, string associationProof, string streetNumber
//            , string streetName, string zipCode, string city, string country)

//        {
//            string encodedPassword = EncodeMD5(password);
//            Account account = new Account()
//            {
//                Mail = mail,
//                Password = encodedPassword
//            };
//            Address address = new Address()
//            {
//                StreetNumber = streetNumber,
//                StreetName = streetName,
//                ZipCode = zipCode,
//                City = city,
//                Country = country
//            };

//            ProjectOwner projectOwner = new ProjectOwner()
//            {
//                Name = name,
//                PhoneNumber = phoneNumber,
//                Summary = summary,
//                Description = description,
//                HyperLink = hyperlink,
//                VolunteerDescritpion = volunteerDescritpion,
//                Partnership = patnerShip,
//                AssociationProof = associationProof,
//                Image = image,
//                Type = type,
//                Status = status,


//                ProjectOwnerAccount = new ProjectOwnerAccount()
//                {
//                    Newsletter = false,
//                    ConfidentialityCharter = true,
//                    Account = account
//                },
//                Address = address


//            };

//            this.bddContext.ProjectOwners.Add(projectOwner);
//            this.bddContext.SaveChanges();
//            return projectOwner.Id;
//        }

//        public int CreateAdministrator(string lastName, string firstName, string mail, string password
//        , string phoneNumber, string streetNumber
//            , string streetName, string zipCode, string city, string country)
//        {
//            string encodedPassword = EncodeMD5(password);
//            Account account = new Account()
//            {
//                Mail = mail,
//                Password = encodedPassword
//            };
//            Address address = new Address()
//            {
//                StreetNumber = streetNumber,
//                StreetName = streetName,
//                ZipCode = zipCode,
//                City = city,
//                Country = country
//            };

//            Administrator administrator = new Administrator()
//            {
//                FirstName = firstName,
//                LastName = lastName,
//                PhoneNumber = phoneNumber,
//                AdministratorAccount = new AdministratorAccount()
//                {
//                    Account = account
//                },

//                Address = address

//            };


//            this.bddContext.Administrators.Add(administrator);
//            this.bddContext.SaveChanges();
//            return administrator.Id;

//        }


  

