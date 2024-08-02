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
