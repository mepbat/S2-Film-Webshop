﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Film_Webshop.Models;

namespace Film_Webshop.Context.MSSQL
{
    public class MssqlAccountContext : Database.Database, IAccountContext
    {
        public bool Insert(Account account)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO dbo.Account (Email, Wachtwoord, Admin, Credits) VALUES (@Email, @Wachtwoord, @Admin, @Credits)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", account.Email);
                    cmd.Parameters.AddWithValue("@Wachtwoord", account.Wachtwoord);
                    cmd.Parameters.AddWithValue("@Admin", account.Admin);
                    cmd.Parameters.AddWithValue("@Credits", account.Credits);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<Account> SelectAll()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    List<Account> accounts = new List<Account>();
                    string query = "SELECT * FROM dbo.account";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string email = reader.GetString(reader.GetOrdinal("Email"));
                        string wachtwoord = reader.GetString(reader.GetOrdinal("Wachtwoord"));
                        bool admin = reader.GetBoolean(reader.GetOrdinal("Admin"));
                        int credits = reader.GetInt32(reader.GetOrdinal("Credits"));
                        Account acc = new Account(id, credits, email, wachtwoord, admin);
                        accounts.Add(acc);
                    }

                    conn.Close();
                    return accounts;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Account>();
            }
        }
    }
}