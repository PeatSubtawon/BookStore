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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        public Books()
        {
            InitializeComponent();
            EditISBNTxt.TextChanged += EditISBNTxt_TextChanged;
            
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

        private void AddBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputTitle = AddTitleTxt.Text;
            string inputDescription = AddDescriptionTxt.Text;
            string inputPrice = AddPriceTxt.Text;
            if (inputTitle.Length == 0) 
            {
                MessageBox.Show("กรุณากรอกชื่อหนังสือ");
            }
            else if (inputDescription.Length == 0)
            {
                MessageBox.Show("กรุณากรอกคำอธิบายหนังสือ");
            }
            else if (inputPrice.Length == 0)
            {
                MessageBox.Show("กรุณากรอกราคาหนังสือ");
            }
            else
            {
                BooksData.AddBooks(inputTitle, inputDescription, inputPrice);
                MessageBox.Show("เพิ่มข้อมูลหนังสือสำเร็จ!", "Success");
            }  
        }
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputISBN = ISBNTxt.Text;
            List<string> data = BooksData.GetBooks(inputISBN);
            if (data != null && data.Count > 0)
            {
                string showData = "";
                foreach (string value in data)
                {
                    showData += value + "\n";
                }
                MessageBox.Show(showData);
            }
            else
            {
                MessageBox.Show("ไม่มี ISBN ที่กรอก", "Error");
            }
        }

        private void SearchAllBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> allData = BooksData.GetAllBooks();
            string showAllData = "ข้อมูลทั้งหมด:\n";
            foreach (string value in allData)
            {
                showAllData += value + "\n";
            }
            MessageBox.Show(showAllData);
        }

        private void EditBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputISBN = EditISBNTxt.Text;
            string inputTitle = EditTitleTxt.Text;
            string inputDescription = EditDescriptionTxt.Text;
            string inputPrice = EditPriceTxt.Text;

            // ดึงข้อมูลเก่าก่อนการอัปเดต
            List<string> oldBooksData = BooksData.GetBooks(inputISBN);

            if (oldBooksData.Count == 0)
            {
                // ถ้าไม่มีข้อมูลหนังสือเก่า ให้แสดงข้อความว่าไม่มี ISBN ที่กรอก
                MessageBox.Show("ไม่มี ISBN ที่กรอก", "Error");
                return; // ออกจากเมธอดไม่ทำการอัปเดตและไม่แสดงข้อมูลใหม่
            }

            string showOldData = "";
            foreach (string value in oldBooksData)
            {
                showOldData += value + "\n";
            }
            // อัปเดตข้อมูลใหม่
            BooksData.EditBooksData(inputISBN, inputTitle, inputDescription, inputPrice);
            // ดึงข้อมูลใหม่หลังจากการอัปเดต
            List<string> newBooksData = BooksData.GetBooks(inputISBN);
            string showNewData = "";
            foreach (string value in newBooksData)
            {
                showNewData += value + "\n";
            }
            // สร้างข้อความเพื่อแสดงข้อมูลเก่าและใหม่
            string message = $"ข้อมูลเก่า:\n{showOldData}\n\nข้อมูลใหม่:\n{showNewData}";
            MessageBox.Show(message, "Success");
        }

        private void DeleteBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputISBN = DeleteISBNTxt.Text;
            // ตรวจสอบว่ามีข้อมูลลูกค้าในฐานข้อมูลก่อนที่จะลบ
            List<string> data = BooksData.GetBooks(inputISBN);

            if (data.Count == 0)
            {
                // แสดงข้อความหาก ISBN ไม่พบในฐานข้อมูล
                MessageBox.Show("ไม่มี ISBN ที่กรอก", "Error");
                return;
            }

            // ลบข้อมูลหนังสือ
            BooksData.DeleteBooksData(inputISBN);

            // แสดงข้อความยืนยันการลบ
            MessageBox.Show("ข้อมูลหนังสือถูกลบเรียบร้อยแล้ว", "Success");
        }

        //-----TEXT CHANGE IN EDIT-----
        private void EditISBNTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string isbn = EditISBNTxt.Text;

            if (!string.IsNullOrEmpty(isbn))
            {
                try
                {
                    List<string> bookInfo = BooksData.GetBooks(isbn);

                    if (bookInfo.Count > 0)
                    {
                        // Assuming you have corresponding TextBox for displaying Title, Description, and Price
                        string[] bookDetails = bookInfo[0].Split('\n');
                        EditTitleTxt.Text = bookDetails[1].Split(':')[1].Trim();
                        EditDescriptionTxt.Text = bookDetails[2].Split(':')[1].Trim();
                        EditPriceTxt.Text = bookDetails[3].Split(':')[1].Trim();
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
            if (EditTitleTxt != null) EditTitleTxt.Text = string.Empty;
            if (EditDescriptionTxt != null) EditDescriptionTxt.Text = string.Empty;
            if (EditPriceTxt != null) EditPriceTxt.Text = string.Empty;
        }
    }
}
