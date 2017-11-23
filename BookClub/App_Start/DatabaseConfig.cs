using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.IO;

namespace BookClub
{
    public class DatabaseConfig
    {
        public static SQLiteConnection myConn;
        public static void RegisterDatabase()
        {
            myConn = new SQLiteConnection(@"Data Source=C:\Users\fredr\source\repos\BookClub\BookClub\Database\database.sqlite3");

            if (!File.Exists(@"C:\Users\fredr\source\repos\BookClub\BookClub\Database\database.sqlite3"))
            {
                SQLiteConnection.CreateFile(@"C:\Users\fredr\source\repos\BookClub\BookClub\Database\database.sqlite3");
            }
        }
        private static void OpenConnection()
        {
            if (myConn.State != System.Data.ConnectionState.Open)
            {
                myConn.Open();
            }
        }
        private static void CloseConnection()
        {
            if (myConn.State != System.Data.ConnectionState.Closed)
            {
                myConn.Close();
            }
        }
        public static void ExecuteQuery(string txtQuery)
        {
            OpenConnection();
            SQLiteCommand sql_cmd = myConn.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public static List<string> ExecuteReader(string txtQuery)
        {
            List<string> response = new List<string>();
            OpenConnection();
            SQLiteCommand sql_cmd = myConn.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            SQLiteDataReader r = sql_cmd.ExecuteReader();
            while (r.Read())
            {
                response.Add(r["Title"].ToString() + "," + r["ImagePath"].ToString());
            }
            CloseConnection();
            return response;
        }
    }
}