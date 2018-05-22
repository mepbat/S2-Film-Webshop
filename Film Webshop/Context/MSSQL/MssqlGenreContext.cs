﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Film_Webshop.Models;

namespace Film_Webshop.Context.MSSQL
{
    public class MssqlGenreContext : Database.Database, IGenreContext
    {
        public int GetGenreId(string genre)
        {
            try
            {
                int genreId = 0;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.Genre WHERE Naam = @genre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@genre", genre);
                    cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genreId = reader.GetInt32(reader.GetOrdinal("ID"));
                        }
                    }
                }

                return genreId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public List<int> SelectByGenreId(int id)
        {
            try
            {
                List<int> idList = new List<int>();
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.FilmGenre WHERE Genre_ID = @genreID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@genreID", id);
                    cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idList.Add(reader.GetInt32(reader.GetOrdinal("Film_ID")));
                        }
                    }
                }
                return idList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<int>();
            }
        }

        public List<Genre> SelectByFilmId(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.FilmGenre INNER JOIN dbo.Genre ON dbo.FilmGenre.Genre_ID = dbo.Genre.ID WHERE Film_ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Genre> genres = new List<Genre>();
                    while (reader.Read())
                    {
                        Genre fg = new Genre(reader.GetString(reader.GetOrdinal("Naam")));
                        genres.Add(fg);
                    }
                    conn.Close();
                    return genres;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Genre>();
            }
        }

        public List<Genre> SelectAll()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Genre.Naam, COUNT(Film_ID) AS Aantal FROM FilmGenre RIGHT JOIN Genre ON FilmGenre.Genre_ID = Genre.ID GROUP BY Genre.Naam ORDER BY Genre.Naam ASC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    List<Genre> genres = new List<Genre>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string naam = reader.GetString(reader.GetOrdinal("Naam"));
                        int aantal = reader.GetInt32(reader.GetOrdinal("Aantal"));
                        Genre fg = new Genre(naam, aantal);
                        genres.Add(fg);
                    }

                    conn.Close();
                    return genres;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Genre>();
            }
        }

        public void Insert(int filmId, int genreId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO dbo.FilmGenre (Film_ID, Genre_ID) VALUES (@Film_ID, @Genre_ID)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Genre_ID", genreId);
                    cmd.Parameters.AddWithValue("@Film_ID", filmId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Delete(Film film)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM dbo.FilmGenre WHERE Film_ID = @filmID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@filmID", film.Id);
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