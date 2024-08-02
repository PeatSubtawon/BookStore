using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Transaction.xaml
    /// </summary>
    public partial class Transaction : Window
    {
        public Transaction()
        {
            InitializeComponent();
            TransactionISBNTxt.TextChanged += TransactionISBNTxt_TextChanged;
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

        //-----TEXT CHANGE IN EDIT-----
        private void TransactionISBNTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string isbn = TransactionISBNTxt.Text;

            if (!string.IsNullOrEmpty(isbn))
            {
                try
                {
                    List<string> bookInfo = BooksData.GetBooks(isbn);

                    if (bookInfo.Count > 0)
                    {
                        // Assuming you have corresponding TextBox for displaying
                        string[] bookDetails = bookInfo[0].Split('\n');
                        ShowTitleTxt.Text = bookDetails[1].Split(':')[1].Trim();
                        ShowDescriptionTxt.Text = bookDetails[2].Split(':')[1].Trim();
                        ShowPriceTxt.Text = bookDetails[3].Split(':')[1].Trim();
                        // Update total price
                        UpdateTotalPrice();
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
        private void TransactionIdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = TransactionIdTxt.Text;

            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    List<string> nameInfo = CustomersData.GetData(name);

                    if (nameInfo.Count > 0)
                    {
                      
                        string[] nameDetails = nameInfo[0].Split('\n');
                        ShowCustomerNameTxt.Text = nameDetails[1].Split(':')[1].Trim();
           
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
            if (ShowTitleTxt != null) ShowTitleTxt.Text = string.Empty;
            if (ShowDescriptionTxt != null) ShowDescriptionTxt.Text = string.Empty;
            if (ShowPriceTxt != null) ShowPriceTxt.Text = string.Empty;
            if (ShowCustomerNameTxt != null) ShowCustomerNameTxt.Text = string.Empty;
        }

        private void TransactionQuantityTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            int quantity;
            decimal price, totalPrice;

            if (decimal.TryParse(ShowPriceTxt.Text, out price) && int.TryParse(TransactionQuantityTxt.Text, out quantity))
            {
                totalPrice = price * quantity;
                ShowTotalPriceTxt.Text = totalPrice.ToString("F2"); // Formatting to 2 decimal places
            }
            else
            {
                ShowTotalPriceTxt.Text = "0.00";
            }
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputISBN = TransactionISBNTxt.Text;
            string inputCustomer_Id = TransactionIdTxt.Text;
            string inputCustomerName = ShowCustomerNameTxt.Text;
            string inputQuantity = TransactionQuantityTxt.Text;
            string inputTotal_Price = ShowTotalPriceTxt.Text;

            string showTitle = ShowTitleTxt.Text;
            string showPrice = ShowPriceTxt.Text;
            
            if (inputISBN.Length == 0)
            {
                MessageBox.Show("กรุณากรอกISBNของหนังสือ");
            }
            else if (inputCustomer_Id.Length == 0)
            {
                MessageBox.Show("กรุณากรอกID");
            }
            else if (inputQuantity.Length == 0)
            {
                MessageBox.Show("กรุณากรอกจำนวนหนังสือ");
            }
            else if (!int.TryParse(TransactionQuantityTxt.Text, out int Quantity) || Quantity < 1)
            {
                MessageBox.Show("จำนวนหนังสือต้องเป็นเลขจำนวนเต็มที่มากกว่าหรือเท่ากับ 1");
                return;
            }
            // ตรวจสอบการมีอยู่ของหมายเลข ISBN และ ID ของลูกค้า
            else if (!TransactionsData.IsISBNExists(inputISBN))
            {
                MessageBox.Show("ไม่มีเลข ISBN นี้ในฐานข้อมูล");
                return;
            }

            else if (!TransactionsData.IsCustomerIdExists(inputCustomer_Id))
            {
                MessageBox.Show("ไม่มีเลข ID ของลูกค้านี้ในฐานข้อมูล");
                return;
            }
            else
            {
                TransactionsData.AddOrder(inputISBN, inputCustomer_Id, inputQuantity, inputTotal_Price);
                
                // แสดงข้อมูลสรุป
                string summary = $"เลขIDของลูกค้า: {inputCustomer_Id}\n" +
                                 $"ชื่อของลูกค้า: {inputCustomerName}\n" +
                                 $"เลขISBNของหนังสือ: {inputISBN}\n" +
                                 $"ชื่อหนังสือ: {showTitle}\n" +
                                 $"ราคา: {showPrice:F2}\n" +
                                 $"จำนวน: {inputQuantity}\n" +
                                 $"ราคารวม: {inputTotal_Price:F2}";

                MessageBox.Show(summary, "สรุปรายการ");
                MessageBox.Show("การสั่งซื้อสำเร็จและบันทึกข้อมูลเรียบร้อยแล้ว!", "Success");
                // รีเซ็ตข้อความใน TextBox ทั้งหมด
                ClearTextBoxes();
            }
        }


    }

    
}
