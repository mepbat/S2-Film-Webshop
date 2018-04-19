using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Film_Webshop.Models;

namespace Film_Webshop.Viewmodels
{
    public class ToevoegenViewmodel
    {
        public string genreNaam(int i)
        {
            return Genres[i].Naam.ToString();
        }
        public Film Film { get; set; }
        public List<Genre> Genres { get; set; }
    }
}