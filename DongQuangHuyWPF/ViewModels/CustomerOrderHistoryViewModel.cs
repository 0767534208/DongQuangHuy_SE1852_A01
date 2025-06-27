using BusinessLayer;
using DataLayer;
using System.Collections.Generic;
using System.Linq;

namespace DongQuangHuyWPF.ViewModels
{
    public class CustomerOrderHistoryViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;
        private readonly Customer _loggedInCustomer;
        private List<Order> _customerOrders;

        public List<Order> CustomerOrders
        {
            get => _customerOrders;
            set => SetField(ref _customerOrders, value);
        }

        public CustomerOrderHistoryViewModel(Customer loggedInCustomer, OrderService orderService)
        {
            _loggedInCustomer = loggedInCustomer;
            _orderService = orderService;
            LoadCustomerOrders();
        }

        private void LoadCustomerOrders()
        {
            // Load orders based on the loggedInCustomer's ID
            CustomerOrders = _orderService.GetAllOrders()
                                        .Where(o => o.CustomerID == _loggedInCustomer.CustomerID)
                                        .ToList();
        }
    }
} 