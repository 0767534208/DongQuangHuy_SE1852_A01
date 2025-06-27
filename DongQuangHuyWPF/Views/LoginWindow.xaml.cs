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
using DongQuangHuyWPF.ViewModels;
using BusinessLayer;
using DataLayer;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private AuthViewModel _authViewModel;

        public LoginWindow()
        {
            InitializeComponent();

            try
            {
                // Initialize services and ViewModel
                Console.WriteLine("Attempting to get DataContext.Instance...");
                var dataContext = new DataContext(); // Sử dụng new DataContext() thay vì Instance
                if (dataContext == null) Console.WriteLine("DataContext.Instance returned NULL.");
                else Console.WriteLine("DataContext.Instance successfully obtained.");

                Console.WriteLine("Attempting to initialize EmployeeService...");
                var employeeService = new EmployeeService(new EmployeeRepository(dataContext));
                if (employeeService == null) Console.WriteLine("EmployeeService initialized as NULL.");
                else Console.WriteLine("EmployeeService successfully initialized.");

                Console.WriteLine("Attempting to initialize CustomerService...");
                var customerService = new CustomerService(new CustomerRepository(dataContext));
                if (customerService == null) Console.WriteLine("CustomerService initialized as NULL.");
                else Console.WriteLine("CustomerService successfully initialized.");

                Console.WriteLine("Attempting to initialize AuthViewModel...");
                _authViewModel = new AuthViewModel(employeeService, customerService);
                if (_authViewModel == null) Console.WriteLine("AuthViewModel initialized as NULL.");
                else Console.WriteLine("AuthViewModel successfully initialized.");

                DataContext = _authViewModel;

                // Subscribe to LoginSuccessAction event from ViewModel
                _authViewModel.LoginSuccessAction = (customer, employee) =>
                {
                    if (employee != null) // Admin Login
                    {
                        AdminDashboardView adminDashboard = new AdminDashboardView();
                        adminDashboard.Show();
                        this.Close(); // Close Login Window
                    }
                    else if (customer != null) // Customer Login
                    {
                        // We will create CustomerDashboardView in the next steps
                        // For now, let's keep it simple or redirect to a placeholder
                        CustomerDashboardView customerDashboard = new CustomerDashboardView(customer);
                        customerDashboard.Show();
                        this.Close(); // Close Login Window
                    }
                };

                txtPassword.PasswordChanged += TxtPassword_PasswordChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo đăng nhập: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Lỗi nghiêm trọng", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"EXCEPTION DURING LOGIN WINDOW INITIALIZATION: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _authViewModel.Password = txtPassword.Password;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_authViewModel != null)
            {
                _authViewModel.LoginCommand.Execute(txtPassword.Password);
            }
            else
            {
                MessageBox.Show("Hệ thống đăng nhập chưa sẵn sàng. Vui lòng khởi động lại ứng dụng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
