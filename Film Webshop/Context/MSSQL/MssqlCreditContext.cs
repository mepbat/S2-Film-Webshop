using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Film_Webshop.Models;

namespace Film_Webshop.Context.MSSQL
{
    public class MssqlCreditContext: Database.Database, Context.ICreditContext
    {

        public void Update(Account account, int credits)
        {
            try
            {
                int newcredits = account.Credits + credits;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE dbo.Account SET Credits = @newcredits WHERE ID = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@newcredits", newcredits);
                    cmd.Parameters.AddWithValue("@id", account.Id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}