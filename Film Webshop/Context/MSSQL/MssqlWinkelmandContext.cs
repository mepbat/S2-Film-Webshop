using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Film_Webshop.Models;

namespace Film_Webshop.Context.MSSQL
{
    public class MssqlWinkelmandContext : Database.Database, IWinkelmandContext
    {
        public List<int> SelectAll(int winkelmandId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.WinkelmandFilm WHERE Winkelmand_ID = @winkelmandId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@winkelmandId", winkelmandId);
                SqlDataReader reader = cmd.ExecuteReader();
                List<int> listFilmIds = new List<int>();
                while (reader.Read())
                {
                    listFilmIds.Add(reader.GetInt32(reader.GetOrdinal("Film_ID")));
                }
                conn.Close();
                return listFilmIds;
            }
        }

        public int GetWinkelmandId(int accId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.Winkelmand WHERE Account_ID = @accId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accId", accId);
                SqlDataReader reader = cmd.ExecuteReader();
                int winkelmandId = 0;
                while (reader.Read())
                {
                    winkelmandId = reader.GetInt32(reader.GetOrdinal("ID"));
                }
                conn.Close();
                return winkelmandId;
            }
        }

        public void Delete(int winkelmandId, int filmId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM dbo.WinkelmandFilm WHERE Film_ID = @Film_ID AND Winkelmand_ID = @Winkelmand_ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Winkelmand_ID", winkelmandId);
                cmd.Parameters.AddWithValue("@Film_ID", filmId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Insert(int winkelmandId, int filmId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO dbo.WinkelmandFilm (Winkelmand_ID, Film_ID) VALUES (@Winkelmand_ID, @Film_ID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Winkelmand_ID", winkelmandId);
                cmd.Parameters.AddWithValue("@Film_ID", filmId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        public void Kopen(int accountId, int filmId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand("dbo.spFilmsKopen", conn) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@accountID", accountId));
                    command.Parameters.Add(new SqlParameter("@filmID", filmId));
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}