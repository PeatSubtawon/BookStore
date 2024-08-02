using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    internal class TransactionsData
    {
        //-----CREATE TRANSACTIONS TABLE-----
        public static void InitializeTransactionsDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Transactions (Order_ID INTEGER PRIMARY KEY , " +
                    "ISBN INTEGER NOT NULL, " +
                    "Customer_Id INTEGER NOT NULL, " +
                    "Quatity INTEGER NOT NULL, " +
                    "Total_Price INTEGER NOT NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        //-----ADD ORDER-----
        public static void AddOrder(string inputISBN, string inputCustomer_Id, string inputQuantity, string inputTotal_Price)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks //
                insertCommand.CommandText = "INSERT INTO Transactions (Order_ID, ISBN, Customer_Id, Quatity, Total_Price) VALUES (NULL, @ISBN, @Customer_Id, @Quatity, @Total_Price);";
                insertCommand.Parameters.AddWithValue("@ISBN", inputISBN);
                insertCommand.Parameters.AddWithValue("@Customer_Id", inputCustomer_Id);
                insertCommand.Parameters.AddWithValue("@Quatity", inputQuantity);
                insertCommand.Parameters.AddWithValue("@Total_Price", inputTotal_Price);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        //-----CHECK ISBN-----
        public static bool IsISBNExists(string inputISBN)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                string query = "SELECT COUNT(*) FROM Books WHERE ISBN = @ISBN";
                SqliteCommand command = new SqliteCommand(query, db);
                command.Parameters.AddWithValue("@ISBN", inputISBN);
                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }
        //-----CHECK ID-----
        public static bool IsCustomerIdExists(string inputCustomer_Id)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                string query = "SELECT COUNT(*) FROM Customers WHERE Customers_Id = @Customers_Id";
                SqliteCommand command = new SqliteCommand(query, db);
                command.Parameters.AddWithValue("@Customers_Id", inputCustomer_Id);
                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
