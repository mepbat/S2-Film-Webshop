using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Film_Webshop.Models;

namespace Film_Webshop.Context
{
    public interface ICreditContext
    {
        bool Update(Account acc, int credits);
    }
}