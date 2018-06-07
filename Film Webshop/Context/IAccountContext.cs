using System.Collections.Generic;
using Film_Webshop.Models;

namespace Film_Webshop.Context
{
    public interface IAccountContext
    {
        bool Insert(Account account);
        List<Account> SelectAll();
    }
}