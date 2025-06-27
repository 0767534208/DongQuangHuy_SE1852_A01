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
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        public Customer NewCustomer { get; private set; }

        public AddCustomerWindow()
        {
            InitializeComponent();
            NewCustomer = new Customer();
            DataContext = NewCustomer;
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
            return !string.IsNullOrWhiteSpace(NewCustomer.CompanyName) &&
                   !string.IsNullOrWhiteSpace(NewCustomer.ContactName) &&
                   !string.IsNullOrWhiteSpace(NewCustomer.Phone);
        }

    }
}
