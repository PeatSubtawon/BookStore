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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BooksData.InitializeBooksDatabase();
            CustomersData.InitializeCustomersDatabase();
            TransactionsData.InitializeTransactionsDatabase();
            UsersData.InitializeUsersDatabase();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // สร้าง StackPanel เพื่อจัดเรียงองค์ประกอบ
            StackPanel stackPanel = new StackPanel();

            // สร้าง TextBox สำหรับการกรอกข้อมูล
            TextBox inputTextBox = new TextBox();
            inputTextBox.Width = 200;
            inputTextBox.Margin = new Thickness(5);
            stackPanel.Children.Add(inputTextBox);

            TextBox inputTextBox1 = new TextBox();
            inputTextBox1.Width = 200;
            inputTextBox1.Margin = new Thickness(5);
            stackPanel.Children.Add(inputTextBox1);

            // สร้างปุ่มสำหรับยืนยันข้อมูลที่กรอก
            Button submitButton = new Button();
            submitButton.Content = "ยืนยัน";
            submitButton.Margin = new Thickness(5);
            submitButton.Click += (s, ev) =>
            {
                // ดึงข้อมูลจาก TextBox และแสดงข้อความยืนยัน
                string inputText = inputTextBox.Text;
                MessageBox.Show($"คุณกรอกข้อมูลว่า: {inputText}");
            };
            stackPanel.Children.Add(submitButton);

            // แทนที่เนื้อหาปัจจุบันด้วย StackPanel ที่มี TextBox และปุ่มยืนยัน
            this.Content = stackPanel;
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (User_IdTxt.Text.Length == 0)
            {
                MessageBox.Show("กรุณากรอกID");
            }
            else if (User_PasswordTxt.Text.Length == 0)
            {
                MessageBox.Show("กรุณากรอกรหัสผ่าน");
            }
            else
            {
                bool isUserExists = UsersData.UserCheck(User_IdTxt.Text);
                if (isUserExists)
                {
                    MessageBox.Show("ID นี้มีอยู่ในระบบแล้ว");
                }
                else
                {
                    UsersData.AddUserAccount(User_IdTxt.Text, User_PasswordTxt.Text);
                    MessageBox.Show("ลงทะเบียนสำเร็จ");
                    Menu menu = new Menu();
                    menu.Show();  
                }
            }  
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (User_IdTxt.Text.Length == 0)
            {
                MessageBox.Show("กรุณากรอกID");
            }
            else if (User_PasswordTxt.Text.Length == 0)
            {
                MessageBox.Show("กรุณากรอกรหัสผ่าน");
            }
            else 
            {
                bool isValidUser = UsersData.ValidateUser(User_IdTxt.Text, User_PasswordTxt.Text);
                if (isValidUser) 
                {
                    MessageBox.Show("เข้าสู่ระบบสำเร็จ!");
                    Menu menu = new Menu();
                    menu.Show();
                }
                else
                {
                    MessageBox.Show("กรอกIDหรือรหัสผ่านผิดพลาด");
                }
            }
        }


    }
}
