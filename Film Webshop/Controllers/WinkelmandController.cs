using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Film_Webshop.Context.MSSQL;
using Film_Webshop.Helpers;
using Film_Webshop.Models;
using Film_Webshop.Repository;
using Film_Webshop.Viewmodels;

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
        [Authorize]
        public ActionResult Index()
        {
            int accId = _auth.Decrypt();
            List<Film> listFilms = _winkelmandRepository.GetFilmsInWinkelmand(_winkelmandRepository.GetWinkelmandId(accId));
            AccountWinkelmandGenreViewmodel viewmodel = new AccountWinkelmandGenreViewmodel
            {
                Winkelmand = new Winkelmand(listFilms, _winkelmandRepository.GetPrijs(listFilms)),
                Genres = _genreRepository.GetAllGenres(),
                Account = _accountRepository.GetAccountById(accId)
            };
            return View(viewmodel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(string gekozenGenre)
        {
            if (gekozenGenre == "Alles")
            {
                return RedirectToAction("Index");
            }
            int accId = _auth.Decrypt();
            List<Film> listFilms = _winkelmandRepository.GetFilmsInWinkelmand(_winkelmandRepository.GetWinkelmandId(accId));
            AccountWinkelmandGenreViewmodel viewmodel = new AccountWinkelmandGenreViewmodel
            {
                Winkelmand = new Winkelmand(listFilms.Where(x => x.ListGenres.Exists(y => y.Naam == gekozenGenre)).ToList(), _winkelmandRepository.GetPrijs(listFilms)),
                Genres = _genreRepository.GetAllGenres(),
                Account = _accountRepository.GetAccountById(accId)
            };
            return View(viewmodel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Toevoegen(int filmId)
        {
            int winkelmandId = _winkelmandRepository.GetWinkelmandId(_auth.Decrypt());
            _winkelmandRepository.AddFilmWithId(winkelmandId, filmId);
            return RedirectToAction("Index", "Film");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Verwijderen(int filmId)
        {
            int winkelmandId = _winkelmandRepository.GetWinkelmandId(_auth.Decrypt());
            _winkelmandRepository.RemoveFilmWithId(winkelmandId, filmId);
            return RedirectToAction("Index", "Film");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Kopen(AccountWinkelmandGenreViewmodel viewmodel)
        {
            viewmodel.Account = _accountRepository.GetAccountById(viewmodel.Account.Id);
            viewmodel.Winkelmand.Id = _winkelmandRepository.GetWinkelmandId(viewmodel.Account.Id);
            viewmodel.Winkelmand.Films = _winkelmandRepository.GetFilmsInWinkelmand(viewmodel.Winkelmand.Id);
            viewmodel.Winkelmand.Totaalprijs = _winkelmandRepository.GetPrijs(viewmodel.Winkelmand.Films);
            if (viewmodel.Account.Credits >= viewmodel.Winkelmand.Totaalprijs)
            {
                foreach (Film f in viewmodel.Winkelmand.Films)
                {
                    _filmRepository.BuyFilm(viewmodel.Account, f);
                }
                return RedirectToAction("Films", "Account");
            }
            return RedirectToAction("Index", "Winkelmand");
        }
    }
}