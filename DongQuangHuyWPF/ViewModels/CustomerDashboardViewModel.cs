using BusinessLayer;
using DataLayer;
using System.Windows.Input;

namespace DongQuangHuyWPF.ViewModels
{
    public class CustomerDashboardViewModel : BaseViewModel
    {
        private Customer _loggedInCustomer;
        private CustomerService _customerService;
        private OrderService _orderService;

        public Action? ViewOrderHistoryAction { get; set; }
        public Action<Customer>? OpenEditProfileWindowAction { get; set; }
        public Action? ViewProfileViewAction { get; set; }

        public Customer LoggedInCustomer
        {
            get => _loggedInCustomer;
            set => SetField(ref _loggedInCustomer, value);
        }

        public CustomerDashboardViewModel(Customer customer, CustomerService customerService, OrderService orderService)
        {
            _loggedInCustomer = customer;
            _customerService = customerService;
            _orderService = orderService;

            // Initialize commands for navigation
            ViewOrderHistoryCommand = new ReplyCommand(ExecuteViewOrderHistoryCommand);
            EditProfileCommand = new ReplyCommand(ExecuteEditProfileCommand);
            ViewProfileCommand = new ReplyCommand(ExecuteViewProfileCommand);
        }

        public ICommand ViewOrderHistoryCommand { get; }
        private void ExecuteViewOrderHistoryCommand(object parameter)
        {
            ViewOrderHistoryAction?.Invoke();
        }

        public ICommand EditProfileCommand { get; }
        private void ExecuteEditProfileCommand(object parameter)
        {
            OpenEditProfileWindowAction?.Invoke(LoggedInCustomer);
        }

        public ICommand ViewProfileCommand { get; }
        private void ExecuteViewProfileCommand(object parameter)
        {
            ViewProfileViewAction?.Invoke();
        }
    }
} 