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
            //local
            //ConnectionString = "Data Source=mssql.fhict.local;Initial Catalog = dbi383656; Persist Security Info=True;User ID = dbi383656; Password=Wachtwoord123";
            //azure
            ConnectionString = "Server=tcp:filmwebshop.database.windows.net,1433;Initial Catalog=FilmWebshop;Persist Security Info=False;User ID=simon4224;Password=Wachtwoord4224;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}