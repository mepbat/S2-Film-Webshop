using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Film_Webshop.Models
{
    public class Account
    {
        public int Id { get; set; }
        public Winkelmand Winkelmand { get; set; }
        public List<Film> Films { get; set; }
        public int Credits { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string HerhalingWachtwoord { get; set; }
        public bool Admin { get; set; }

        public Account()
        {

        }

        public Account(int id)
        {
            this.Id = id;
        }

        public Account(int id, int credits, string email, string wachtwoord, string herhalingWachtwoord, bool admin)
        {
            this.Id = id;
            this.Credits = credits;
            this.Email = email;
            this.Wachtwoord = wachtwoord;
            this.HerhalingWachtwoord = herhalingWachtwoord;
            this.Admin = admin;
        }

        public Account(int id, int credits, string email, string wachtwoord, bool admin)
        {
            this.Id = id;
            this.Credits = credits;
            this.Email = email;
            this.Wachtwoord = wachtwoord;
            this.Admin = admin;
        }
        public Account(string email, string wachtwoord, string herhalingWachtwoord, bool admin)
        {
            this.Email = email;
            this.Wachtwoord = wachtwoord;
            this.HerhalingWachtwoord = herhalingWachtwoord;
            this.Admin = admin;
        }

        public Account(string email, string wachtwoord)
        {
            this.Email = email;
            this.Wachtwoord = wachtwoord;
        }
    }
}