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
    /// Interaction logic for Transaction.xaml
    /// </summary>
    public partial class Transaction : Window
    {
        public Transaction()
        {
            InitializeComponent();
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

       
    }

    
}
