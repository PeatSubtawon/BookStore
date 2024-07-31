using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    internal class UsersData
    {
        //-----CREATE USERS TABLE-----
        public static void InitializeUsersDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Users (User_Id NVARCHAR(50) PRIMARY KEY, " +
                    "User_Password NVARCHAR(50) NOT NULL) ";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        //-----ADD USER ACCOUNT-----
        public static void AddUserAccount(string inputId, string inputPassword)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks //
                insertCommand.CommandText = "INSERT INTO Users (User_Id, User_Password) VALUES (@User_Id, @User_Password);";
                insertCommand.Parameters.AddWithValue("@User_Id", inputId);
                insertCommand.Parameters.AddWithValue("@User_Password", inputPassword);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        //-----VALIDATE USER ID FOR REGISTER-----
        public static bool UserCheck(string CheckUserId)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteData.db"))
            {
                bool checker;
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                ("SELECT User_Id FROM Users WHERE User_Id = @UserId", db);
                selectCommand.Parameters.AddWithValue("@UserId", CheckUserId);
                SqliteDataReader query = selectCommand.ExecuteReader();
                checker = query.Read();
                db.Close();
                return checker;
            }
        }

        //-----VALIDATE USER FOR LOGIN-----
        public static bool ValidateUser(string inputId, string inputPassword) 
        {
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteData.db"))
            {
                bool checker;
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                ("SELECT User_Id FROM Users WHERE User_Id = @UserId AND User_Password = @UserPassword", db);
                selectCommand.Parameters.AddWithValue("@UserId", inputId);
                selectCommand.Parameters.AddWithValue("@UserPassword", inputPassword);
                SqliteDataReader query = selectCommand.ExecuteReader();
                checker = query.Read();
                db.Close();
                return checker;
            }
        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Customers", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add(query.GetString(0) + " " + query.GetString(1) + " " + query.GetString(2) + " " + query.GetString(3));
                }
                db.Close();
            }
            return entries;
        }
    }
}
