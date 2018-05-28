using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Film_Webshop.Context.MSSQL;
using Film_Webshop.Models;
using Microsoft.Ajax.Utilities;

namespace Film_Webshop.Repository
{
    public class CreditRepository
    {
        private readonly MssqlCreditContext _creditContext;

        public CreditRepository(MssqlCreditContext context)
        {
            _creditContext = context;
        }

        public void AddCredits(Account acc, int credits)
        {
            _creditContext.Update(acc, credits);
        }

        public bool CheckInt(string credits)
        {
            if (!String.IsNullOrWhiteSpace(credits))
            {
                try
                {
                    int creditsInt = Convert.ToInt32(credits);
                    if (creditsInt > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}