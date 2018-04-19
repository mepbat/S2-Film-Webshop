using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Film_Webshop.Models
{
    public class Winkelmand
    {
        public int Id { get; set; }
        public List<Film> Films { get; set; }
        public int Totaalprijs { get; set; }

        public Winkelmand()
        {

        }
        public Winkelmand(List<Film> films)
        {
            this.Films = films;            
        }

        public Winkelmand(List<Film> films, int totaalprijs)
        {
            this.Films = films;
            this.Totaalprijs = totaalprijs;
        }

        public Winkelmand(int id, List<Film> films, int totaalprijs)
        {
            this.Id = id;
            this.Films = films;
            this.Totaalprijs = totaalprijs;
        }
    }
}