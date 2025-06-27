using BusinessLayer;
using DataLayer;
using DongQuangHuyWPF.CRUDWindow;
using DongQuangHuyWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerDashboardView.xaml
    /// </summary>
    public partial class CustomerDashboardView : Window
    {
        private CustomerDashboardViewModel _viewModel;

        public CustomerDashboardView(Customer loggedInCustomer)
        {
            InitializeComponent();

            var dataContext = new DataContext();
            var customerService = new CustomerService(new CustomerRepository(dataContext));
            var orderService = new OrderService(new OrderRepository(dataContext));

            _viewModel = new CustomerDashboardViewModel(loggedInCustomer, customerService, orderService);
            DataContext = _viewModel;

            // Subscribe to ViewModel actions for UI navigation
            _viewModel.ViewOrderHistoryAction = () =>
            {
                CustomerContentFrame.Content = new CustomerOrderHistoryView(_viewModel.LoggedInCustomer);
            };

            _viewModel.OpenEditProfileWindowAction = (customerToEdit) =>
            {
                var editCustomerWindow = new EditCustomerWindow(customerToEdit);
                if (editCustomerWindow.ShowDialog() == true)
                {
                    MessageBox.Show("Cập nhật hồ sơ thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            };

            _viewModel.ViewProfileViewAction = () =>
            {
                CustomerContentFrame.Content = new CustomerProfileView(_viewModel.LoggedInCustomer);
            };

            // Optionally load a default view (e.g., show profile by default)
            CustomerContentFrame.Content = new CustomerProfileView(loggedInCustomer);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
} 