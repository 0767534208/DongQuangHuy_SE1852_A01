using BusinessLayer;
using DataLayer;
using DongQuangHuyWPF.CRUDWindow;
using DongQuangHuyWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        private CustomerViewModel _viewModel;
        public CustomerView()
        {
            InitializeComponent();

            var dataContext = new DataContext();
            var customerRepository = new CustomerRepository(dataContext);

            var customerService = new CustomerService(customerRepository);

            _viewModel = new CustomerViewModel(customerService);
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addCustomerWindow = new AddCustomerWindow();
            if (addCustomerWindow.ShowDialog() == true)
            {
                _viewModel.AddCustomer(addCustomerWindow.NewCustomer);
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshCustomerDataGrid();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedCustomer == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để chỉnh sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editCustomerWindow = new EditCustomerWindow(_viewModel.SelectedCustomer);
            if (editCustomerWindow.ShowDialog() == true)
            {
                _viewModel.UpdateCustomer(editCustomerWindow.UpdatedCustomer);
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshCustomerDataGrid();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedCustomer == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng '{_viewModel.SelectedCustomer.CompanyName}'?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult == MessageBoxResult.Yes)
            {
                _viewModel.DeleteCustomer(_viewModel.SelectedCustomer);
                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshCustomerDataGrid();
            }
        }

        private void CustomerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _viewModel.SelectedCustomer = (Customer)e.AddedItems[0];
            }
            else
            {
                _viewModel.SelectedCustomer = null;
            }
        }

        private void RefreshCustomerDataGrid()
        {
            CustomerDataGrid.ItemsSource = null;
            CustomerDataGrid.ItemsSource = _viewModel.Customers;
        }
    }
}
