using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Film_Webshop.Context;
using Film_Webshop.Context.MSSQL;
using Film_Webshop.Helpers;
using Film_Webshop.Models;
using Film_Webshop.Repository;
using Film_Webshop.Viewmodels;
using Microsoft.Ajax.Utilities;

namespace Film_Webshop.Controllers
{
    public class WinkelmandController : Controller
    {
        private readonly WinkelmandRepository _winkelmandRepository = new WinkelmandRepository(new MssqlWinkelmandContext());
        private readonly GenreRepository _genreRepository = new GenreRepository(new MssqlGenreContext());
        private readonly FilmRepository _filmRepository = new FilmRepository(new MssqlFilmContext());
        private readonly AccountRepository _accountRepository = new AccountRepository(new MssqlAccountContext());
        readonly TicketAuthenticator _auth = new TicketAuthenticator();

        [HttpGet]
        public ActionResult Index()
        {
            int accId = _auth.Decrypt();
            List<Film> listFilms = new List<Film>();
            foreach (int filmId in _winkelmandRepository.GetFilmsIdsInWinkelmand(
                _winkelmandRepository.GetWinkelmandId(accId)))
            {
                listFilms.Add(_filmRepository.GetById(filmId));
            }
            AccountWinkelmandGenreViewmodel viewmodel = new AccountWinkelmandGenreViewmodel
            {
                Winkelmand = new Winkelmand(listFilms, _filmRepository.GetPrijs(listFilms)),
                Genres = _genreRepository.GetAllGenres(),
                Account = _accountRepository.GetAccountById(accId)
            };
            return View(viewmodel);
        }

        [HttpGet]
        public ActionResult Toevoegen(int filmId)
        {
            int winkelmandId = _winkelmandRepository.GetWinkelmandId(_auth.Decrypt());
            _winkelmandRepository.AddFilmWithId(winkelmandId, filmId);
            return RedirectToAction("Index", "Film");
        }

        [HttpGet]
        public ActionResult Verwijderen(int filmId)
        {
            int winkelmandId = _winkelmandRepository.GetWinkelmandId(_auth.Decrypt());
            _winkelmandRepository.RemoveFilmWithId(winkelmandId, filmId);
            return RedirectToAction("Index", "Film");
        }

        [HttpPost]
        public ActionResult Kopen(AccountWinkelmandGenreViewmodel viewmodel)
        {
            viewmodel.Account = _accountRepository.GetAccountById(viewmodel.Account.Id);
            viewmodel.Winkelmand.Id = _winkelmandRepository.GetWinkelmandId(viewmodel.Account.Id);
            List<int> filmIds = _winkelmandRepository.GetFilmsIdsInWinkelmand(viewmodel.Winkelmand.Id);
            List<Film> films = new List<Film>();
            foreach (int id in filmIds)
            {
                films.Add(_filmRepository.GetById(id));
            }
            int totaalprijs = 0;
            foreach (Film f in films)
            {
                totaalprijs += f.Prijs;
            }
            viewmodel.Winkelmand.Films = films;
            viewmodel.Winkelmand.Totaalprijs = totaalprijs;
            if (viewmodel.Account.Credits >= viewmodel.Winkelmand.Totaalprijs)
            {
                foreach (Film f in viewmodel.Winkelmand.Films)
                {
                    if (!_accountRepository.HasFilm(viewmodel.Account.Id, f))
                    {
                        _accountRepository.BuyFilm(viewmodel.Account, f);
                        _winkelmandRepository.RemoveFilmWithId(viewmodel.Winkelmand.Id, f.Id);
                    }
                }
                return RedirectToAction("Films","Account");
            }
            return RedirectToAction("Index", "Winkelmand");
        }
    }
}