using System.Collections.Generic;
using Film_Webshop.Context;
using Film_Webshop.Context.MSSQL;
using Film_Webshop.Models;

namespace Film_Webshop.Repository
{
    public class GenreRepository
    {
        private readonly MssqlGenreContext _genreContext;

        public GenreRepository(MssqlGenreContext genreContext)
        {
            this._genreContext = genreContext;
        }

        public List<Genre> GetAllGenres()
        {
            return _genreContext.SelectAll();
        }

        public List<Genre> GetFilmGenres(int filmId)
        {
            return _genreContext.SelectByFilmId(filmId);
        }

        public void InsertFilmGenres(Film film)
        {
            foreach (Genre genre in film.ListGenres)
            {
                int genreId = _genreContext.GetGenreId(genre.Naam.Trim());
                _genreContext.Insert(film.Id, genreId);
            }
        }

        public List<int> GetFilmsWithGenre(string genre)
        {
            int genreId = _genreContext.GetGenreId(genre);
            return _genreContext.SelectByGenreId(genreId);
        }

        public void DeleteFilmGenres(Film film)
        {
            _genreContext.Delete(film);
        }
    }
}