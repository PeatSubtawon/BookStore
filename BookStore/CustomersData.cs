using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    internal class CustomersData
    {
        //-----CREATE CUSTOMERS TABLE-----
        public static void InitializeCustomersDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteCustomers.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Customers (Customers_Id INTEGER PRIMARY KEY, " +
                    "Customers_Name NVARCHAR(100) NULL, " +
                    "Address NVARCHAR(255) NULL, " +
                    "Email NVARCHAR(100) NULL) ";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
    }
}
