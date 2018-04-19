using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Film_Webshop.Models
{
    public class Genre
    {
        public string Naam { get; set; }
        public bool Checked { get; set; }

        public Genre()
        {
            
        }

        public Genre(string naam)
        {
            this.Naam = naam;
        }
    }
}