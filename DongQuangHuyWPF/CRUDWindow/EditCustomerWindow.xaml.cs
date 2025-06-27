using DataLayer;
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

namespace DongQuangHuyWPF.CRUDWindow
{
    /// <summary>
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        public Customer OriginalCustomer { get; private set; }

        public EditCustomerWindow(Customer customer)
        {
            InitializeComponent();
            OriginalCustomer = customer;
            DataContext = OriginalCustomer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(OriginalCustomer.CompanyName) &&
                   !string.IsNullOrWhiteSpace(OriginalCustomer.ContactName) &&
                   !string.IsNullOrWhiteSpace(OriginalCustomer.Phone);
        }
    }
}
