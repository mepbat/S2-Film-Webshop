using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Film_Webshop.Database
{
    public class Database
    {
        public static string ConnectionString { get; set; }

        static Database()
        {
            ConnectionString = "Data Source=mssql.fhict.local;Initial Catalog = dbi383656; Persist Security Info=True;User ID = dbi383656; Password=wachtwoord123";
        }
    }
}