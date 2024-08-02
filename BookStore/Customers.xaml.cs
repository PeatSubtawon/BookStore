using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Window
    {
        public Customers()
        {
            InitializeComponent();
            EditIdTxt.TextChanged += EditIdTxt_TextChanged;
        }
        private void CustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
        }
        private void BooksBtn_Click(object sender, RoutedEventArgs e)
        {
            Books books = new Books();
            books.Show();
        }
        private void TransactionBtn_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = new Transaction();
            transaction.Show();
        }

        private void AddCustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomers_Name = AddCustomers_NameTxt.Text;
            string inputAddress = AddAddressTxt.Text;
            string inputEmail = AddEmailTxt.Text;
            if (inputCustomers_Name.Length == 0)
            {
                MessageBox.Show("กรุณากรอกชื่อ");
            }
            else if (inputAddress.Length == 0)
            {
                MessageBox.Show("กรุณากรอกที่อยู่");
            }
            else if (inputEmail.Length == 0)
            {
                MessageBox.Show("กรุณากรอกอีเมล");
            }
            else
            {
                CustomersData.AddCustomersAccount(inputCustomers_Name, inputAddress, inputEmail);
                MessageBox.Show("เพิ่มข้อมูลลูกค้าสำเร็จ!", "Success");
                // รีเซ็ตข้อความใน TextBox ทั้งหมด
                AddCustomers_NameTxt.Text = "";
                AddAddressTxt.Text = "";
                AddEmailTxt.Text = "";
            }
        }
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomersId = Customers_IdTxt.Text;

            List<string> data = CustomersData.GetData(inputCustomersId);
            if (data != null && data.Count > 0) 
            {
                string showData = "";
                foreach (string value in data)
                {
                    showData += value + "\n";
                }
                MessageBox.Show(showData);
                // รีเซ็ตข้อความใน TextBox ทั้งหมด
                Customers_IdTxt.Text = "";
            }
            else 
            {
                MessageBox.Show("ไม่มี ID ที่กรอก", "Error");
                // รีเซ็ตข้อความใน TextBox ทั้งหมด
                Customers_IdTxt.Text = "";
            }
        }

        private void SearchAllBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> allData = CustomersData.GetAllData();
            string showAllData = "ข้อมูลทั้งหมด:\n";
            foreach (string value in allData)
            {
                showAllData += value + "\n";
            }
            MessageBox.Show(showAllData);
        }

        private void EditCustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomersId = EditIdTxt.Text;
            string inputCustomers_Name = EditCustomers_NameTxt.Text;
            string inputAddress = EditAddressTxt.Text;
            string inputEmail = EditEmailTxt.Text;

            // ดึงข้อมูลเก่าก่อนการอัปเดต
            List<string> oldCustomerData = CustomersData.GetData(inputCustomersId);

            if (oldCustomerData.Count == 0)
            {
                // ถ้าไม่มีข้อมูลลูกค้าเก่า ให้แสดงข้อความว่าไม่มี ID ที่กรอก
                MessageBox.Show("ไม่มี ID ที่กรอก", "Error");
                // รีเซ็ตข้อความใน TextBox ทั้งหมด
                EditIdTxt.Text = "";
                return; // ออกจากเมธอดไม่ทำการอัปเดตและไม่แสดงข้อมูลใหม่
            }
            else 
            {
                string showOldData = "";
                foreach (string value in oldCustomerData)
                {
                    showOldData += value + "\n";
                }
                // อัปเดตข้อมูลใหม่
                CustomersData.EditCustomersData(inputCustomersId, inputCustomers_Name, inputAddress, inputEmail);
                // ดึงข้อมูลใหม่หลังจากการอัปเดต
                List<string> newCustomerData = CustomersData.GetData(inputCustomersId);
                string showNewData = "";
                foreach (string value in newCustomerData)
                {
                    showNewData += value + "\n";
                }
                // สร้างข้อความเพื่อแสดงข้อมูลเก่าและใหม่
                string message = $"ข้อมูลเก่า:\n{showOldData}\n\nข้อมูลใหม่:\n{showNewData}";
                MessageBox.Show(message, "Success");
                // รีเซ็ตข้อความใน TextBox ทั้งหมด
                EditIdTxt.Text = "";
                EditCustomers_NameTxt.Text = "";
                EditAddressTxt.Text = "";
                EditEmailTxt.Text = "";
            }
        }

        private void DeleteCustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomersId = DeleteIdTxt.Text;
            // ตรวจสอบว่ามีข้อมูลลูกค้าในฐานข้อมูลก่อนที่จะลบ
            List<string> customersData = CustomersData.GetData(inputCustomersId);

            if (customersData.Count == 0)
            {
                // แสดงข้อความหาก ID ไม่พบในฐานข้อมูล
                MessageBox.Show("ไม่มี ID ที่กรอก", "Error");
                DeleteIdTxt.Text = "";
                return;
            }
            else 
            {
                // ลบข้อมูลลูกค้า
                CustomersData.DeleteCustomersData(inputCustomersId);

                // แสดงข้อความยืนยันการลบ
                MessageBox.Show("ข้อมูลลูกค้าถูกลบเรียบร้อยแล้ว", "Success");
                DeleteIdTxt.Text = "";
            }
        }


        //-----TEXT CHANGE IN EDIT-----
        private void EditIdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string id = EditIdTxt.Text;

            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    List<string> customerInfo = CustomersData.GetData(id);

                    if (customerInfo.Count > 0)
                    {
                        // Assuming you have corresponding TextBox for displaying Customers_Name, Address, and Email
                        string[] customerDetails = customerInfo[0].Split('\n');
                        EditCustomers_NameTxt.Text = customerDetails[1].Split(':')[1].Trim();
                        EditAddressTxt.Text = customerDetails[2].Split(':')[1].Trim();
                        EditEmailTxt.Text = customerDetails[3].Split(':')[1].Trim();
                    }
                    else
                    {
                        ClearTextBoxes();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that might occur
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
            else
            {
                ClearTextBoxes();
            }
        }
        private void ClearTextBoxes()
        {
            if (EditCustomers_NameTxt != null) EditCustomers_NameTxt.Text = string.Empty;
            if (EditAddressTxt != null) EditAddressTxt.Text = string.Empty;
            if (EditEmailTxt != null) EditEmailTxt.Text = string.Empty;
        }
    }
}
