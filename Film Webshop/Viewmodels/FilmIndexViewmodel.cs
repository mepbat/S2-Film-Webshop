using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Film_Webshop.Models;

namespace Film_Webshop.Viewmodels
{
    public class FilmIndexViewmodel
    {
        public List<Film> ListFilm { get; set; }
        public List<Genre> ListGenres { get; set; }
        public string GekozenGenre { get; set; }
        public Account Account { get; set; }
    }
}