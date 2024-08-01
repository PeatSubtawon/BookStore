using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    internal class BooksData
    {
        //-----CREATE BOOKS TABLE-----
        public static void InitializeBooksDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Books (ISBN INTEGER PRIMARY KEY, " +
                    "Title NVARCHAR(100) NOT NULL, " +
                    "Description NVARCHAR(255) NULL, " +
                    "Price DOUBLE NOT NULL) ";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
        //-----ADD BOOKS-----
        public static void AddBooks(string inputTitle, string inputDescription, string inputPrice)
        { 
            using (SqliteConnection db= new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks //
                insertCommand.CommandText = "INSERT INTO Books (ISBN, Title, Description, Price) VALUES (NULL, @Title, @Description, @Price);";
                insertCommand.Parameters.AddWithValue("@Title", inputTitle);
                insertCommand.Parameters.AddWithValue("@Description", inputDescription);
                insertCommand.Parameters.AddWithValue("@Price", inputPrice);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        //-----SEARCH BOOKS ISBN-----
        public static List<String> GetBooks(string inputISBN)
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT * from Books WHERE ISBN = @ISBN", db);
                selectCommand.Parameters.AddWithValue("@ISBN", inputISBN);

                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add("ISBN: " + query.GetString(0) + "\n" + "ชื่อหนังสือ: " + query.GetString(1) + "\n" + "คำอธิบายหนังสือ: " + query.GetString(2) + "\n" + "ราคา: " + query.GetString(3));
                }
                db.Close();
            }
            return entries;
        }

        //-----SEARCH ALL BOOKS-----
        public static List<String> GetAllBooks() 
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT * from Books", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add("ISBN: " + query.GetString(0) + "\n" + "ชื่อหนังสือ: " + query.GetString(1) + "\n" + "คำอธิบายหนังสือ: " + query.GetString(2) + "\n" + "ราคา: " + query.GetString(3));
                }
                db.Close();
            }
            return entries;
        }
        
        //-----EDIT BOOKS DATA-----
        public static void EditBooksData(string inputISBN, string inputTitle, string inputDescription, string inputPrice) 
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks //
                updateCommand.CommandText = "UPDATE Books SET Title = @Title, Description = @Description, Price = @Price WHERE ISBN = @ISBN;";
                updateCommand.Parameters.AddWithValue("@ISBN", inputISBN);
                updateCommand.Parameters.AddWithValue("@Title", inputTitle); 
                updateCommand.Parameters.AddWithValue("@Description", inputDescription); 
                updateCommand.Parameters.AddWithValue("@Price", inputPrice); 
                updateCommand.ExecuteNonQuery();
                db.Close();
            }
        }

        //-----DELETE BOOKS DATA
        public static void DeleteBooksData(string inputISBN)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;

                deleteCommand.CommandText = "DELETE FROM Books WHERE ISBN = @ISBN";
                deleteCommand.Parameters.AddWithValue("@ISBN", inputISBN);

                deleteCommand.ExecuteNonQuery();

                db.Close();
            }
        }
    }
}
