using System;
using System.Web.Mvc;
using Film_Webshop.Context.MSSQL;
using Film_Webshop.Helpers;
using Film_Webshop.Models;
using Film_Webshop.Repository;
using Film_Webshop.Viewmodels;

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
            CreditsViewmodel vm = new CreditsViewmodel();
            vm.Account = acc;
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Credits(CreditsViewmodel vm)
        {
            if (_creditContext.CheckInt(vm.Credits))
            {
                vm.Account = _accountRepository.GetAccountById(_auth.Decrypt());
                _creditContext.AddCredits(vm.Account, Convert.ToInt32(vm.Credits));
                vm.Account = _accountRepository.GetAccountById(_auth.Decrypt());
                return View(vm);
            }
            vm.Account = _accountRepository.GetAccountById(_auth.Decrypt());
            ViewBag.GeenInt = "Vul een geheel getal in boven de 0.";
            return View(vm);
        }
    }
}