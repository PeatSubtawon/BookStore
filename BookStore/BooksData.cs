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
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteBooks.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Books (ISBN INTEGER PRIMARY KEY, " +
                    "Title NVARCHAR(100) NULL, " +
                    "Description NVARCHAR(255) NULL, " +
                    "Price float NULL) ";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
    }
}
