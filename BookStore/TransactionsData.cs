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
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteTransactions.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Transactions (Order_ID INTEGER PRIMARY KEY , " +
                    "ISBN INTEGER NULL, " +
                    "Customer_Id INTEGER NULL, " +
                    "Quatity INTEGER NULL, " +
                    "Total_Price INTEGER NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
    }
}
