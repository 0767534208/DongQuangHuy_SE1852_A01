using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DongQuangHuyWPF.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly EmployeeService _employeeService;
        private readonly CustomerService _customerService;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _phone;
        private bool _isAdmin;
        private string _errorMessage = string.Empty;

        public Action<Customer?, Employee?>? LoginSuccessAction { get; set; }

        public AuthViewModel(EmployeeService employeeService, CustomerService customerService)
        {
            _employeeService = employeeService;
            _customerService = customerService;
            LoginCommand = new ReplyCommand(ExecuteLoginCommand);
        }

        public string Username { get => _username; set => SetField(ref _username, value); }
        public string Password { get => _password; set => SetField(ref _password, value); }
        public string Phone { get => _phone; set => SetField(ref _phone, value); }
        public bool IsAdmin { get => _isAdmin; set => SetField(ref _isAdmin, value); }
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetField(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        private void ExecuteLoginCommand(object parameter)
        {
            string? actualPassword = parameter as string;

            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Vui lòng nhập tên người dùng.";
                return;
            }

            if (Username.ToLower().Contains("admin"))
            {
                if (string.IsNullOrWhiteSpace(actualPassword))
                {
                    ErrorMessage = "Vui lòng nhập mật khẩu Admin.";
                    return;
                }

                var employee = _employeeService.GetAllEmployees().FirstOrDefault(e =>
                    e.UserName.Equals(Username, StringComparison.OrdinalIgnoreCase) &&
                    e.Password == actualPassword);

                if (employee != null)
                {
                    ErrorMessage = string.Empty;
                    LoginSuccessAction?.Invoke(null, employee);
                }
                else
                {
                    ErrorMessage = "Tên người dùng hoặc mật khẩu Admin không đúng.";
                }
            }
            else
            {
                var customer = _customerService.GetAllCustomers().FirstOrDefault(c =>
                    c.Phone.Equals(Username, StringComparison.OrdinalIgnoreCase));

                if (customer != null)
                {
                    if (string.IsNullOrEmpty(actualPassword) || 
                        (!string.IsNullOrEmpty(actualPassword) && customer.Password == actualPassword))
                    {
                        ErrorMessage = string.Empty;
                        LoginSuccessAction?.Invoke(customer, null);
                    }
                    else
                    {
                        ErrorMessage = "Mật khẩu khách hàng không đúng.";
                    }
                }
                else
                {
                    ErrorMessage = "Số điện thoại không đúng.";
                }
            }
        }
    }
}
