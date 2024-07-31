using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookStore
{
    internal class CustomersData
    {
        //-----CREATE CUSTOMERS TABLE-----
        public static void InitializeCustomersDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Customers (Customers_Id INTEGER PRIMARY KEY, " +
                    "Customers_Name NVARCHAR(100) NOT NULL, " +
                    "Address NVARCHAR(255) NULL, " +
                    "Email NVARCHAR(100) NOT NULL) ";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
        //-----ADD CUSTOMER ACCOUNT-----
        public static void AddCustomersAccount(string inputCustomers_Name, string inputAddress, string inputEmail)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks //
                insertCommand.CommandText = "INSERT INTO Customers (Customers_Id, Customers_Name, Address, Email) VALUES (NULL, @Customers_Name, @Address, @Email);";
                insertCommand.Parameters.AddWithValue("@Customers_Name", inputCustomers_Name);
                insertCommand.Parameters.AddWithValue("@Address", inputAddress);
                insertCommand.Parameters.AddWithValue("@Email", inputEmail);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        //-----SEARCH CUSTOMER ID-----
        public static List<String> GetData(string customersId)
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT * from Customers WHERE Customers_Id = @CustomerId", db);
                selectCommand.Parameters.AddWithValue("@CustomerId", customersId);

                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add("ID: " + query.GetString(0) + "\n" + "ชื่อ: " + query.GetString(1) + "\n" + "ที่อยู่: " + query.GetString(2) + "\n" + "อีเมล: " + query.GetString(3));
                }
                db.Close();
            }
            return entries;
        }

        //-----SEARCH ALL CUSTOMER ID-----
        public static List<String> GetAllData()
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT * from Customers", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add("ID: " + query.GetString(0) + "\n" + "ชื่อ: " + query.GetString(1) + "\n" + "ที่อยู่: " + query.GetString(2) + "\n" + "อีเมล: " + query.GetString(3));
                }
                db.Close();
            }
            return entries;
        }
        //-----EDIT CUSTOMER DATA-----
        public static void EditCustomersData(string inputCustomersId, string inputCustomers_Name, string inputAddress, string inputEmail)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();
                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks //
                updateCommand.CommandText = "UPDATE Customers SET Customers_Name = @Customers_Name, Address = @Address, Email = @Email WHERE Customers_Id = @Customers_Id;";
                updateCommand.Parameters.AddWithValue("@Customers_Id", inputCustomersId); // ID ของลูกค้าที่จะแก้ไข
                updateCommand.Parameters.AddWithValue("@Customers_Name", inputCustomers_Name); // ชื่อใหม่
                updateCommand.Parameters.AddWithValue("@Address", inputAddress); // ที่อยู่ใหม่
                updateCommand.Parameters.AddWithValue("@Email", inputEmail); // อีเมลใหม่
                updateCommand.ExecuteNonQuery();
                db.Close();
            }
        }
        //-----DELETE CUSTOMER DATA-----
        public static void DelteCustomersData(string inputCustomersId)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=sqliteData.db"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;

                deleteCommand.CommandText = "DELETE FROM Customers WHERE Customers_Id = @Customers_Id";
                deleteCommand.Parameters.AddWithValue("@Customers_Id", inputCustomersId);

                deleteCommand.ExecuteNonQuery();

                db.Close();
            }
        }
    }
}
