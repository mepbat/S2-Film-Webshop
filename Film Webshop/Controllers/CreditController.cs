using System.Web.Mvc;
using Film_Webshop.Context.MSSQL;
using Film_Webshop.Helpers;
using Film_Webshop.Models;
using Film_Webshop.Repository;

namespace Film_Webshop.Controllers
{
    public class CreditController : Controller
    {
        private readonly AccountRepository _accountRepository = new AccountRepository(new MssqlAccountContext());
        private readonly CreditRepository _creditContext = new CreditRepository(new MssqlCreditContext());
        private readonly TicketAuthenticator _auth = new TicketAuthenticator();

        [HttpGet]
        [Authorize]
        public ActionResult Credits()
        {
            Account acc = _accountRepository.GetAccountById(_auth.Decrypt());
            return View(acc);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Credits(Account acc)
        {
            Account a = _accountRepository.GetAccountById(_auth.Decrypt());
            _creditContext.AddCredits(a, 10);
            a = _accountRepository.GetAccountById(_auth.Decrypt());
            return View(a);
        }
    }
}