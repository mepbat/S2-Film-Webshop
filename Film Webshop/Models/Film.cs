using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Film_Webshop.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public List<Genre> ListGenres { get; set; }
        public int Lengte { get; set; }
        public int Prijs { get; set; }
        public double Rating { get; set; }
        public int Jaar { get; set; }
        public byte[] Image { get; set; }
        public string GenresString { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }


        public Film()
        {

        }

        public Film(int id, string naam, string beschrijving, List<Genre> listGenres, int lengte, int prijs, double rating, byte[] image, int jaar)
        {
            this.Id = id;
            this.Naam = naam;
            this.Beschrijving = beschrijving;
            this.ListGenres = listGenres;
            this.Lengte = lengte;
            this.Prijs = prijs;
            this.Rating = rating;
            this.Image = image;
            this.Jaar = jaar;
        }
    }
}