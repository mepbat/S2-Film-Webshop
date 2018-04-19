using System.Collections.Generic;
using Film_Webshop.Models;

namespace Film_Webshop.Context
{
    public interface IGenreContext
    {
        int GetGenreId(string genre);
        List<Genre> SelectByFilmId(int id);
        List<int> SelectByGenreId(int id);
        List<Genre> SelectAll();
        void Insert(int filmId, int genreId);
        void Delete(Film film);
    }
}