using System;
using System.Collections.Generic;
using Film_Webshop.Models;

namespace Film_Webshop.Context
{
    public interface IFilmContext
    {
        bool Insert(Film film);
        List<Film> Select();
        bool Delete(Film film);
        Film GetById(int id);
        bool Update(Film film);
        bool BuyFilm(int filmId, int accId, int credits, DateTime datetime);
        List<Film> GetBoughtFilms(int accountId);
    }
}