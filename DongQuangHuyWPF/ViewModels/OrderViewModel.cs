using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DongQuangHuyWPF.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;
        private Order? _selectedOrder;
        private string _orderSearchTerm = string.Empty;
        private List<Order> _orders;

        public OrderViewModel(OrderService orderService)
        {
            _orderService = orderService;
            LoadOrders();

            AddOrderCommand = new ReplyCommand(param => { /* Handled by Click event in View */ });
            UpdateOrderCommand = new ReplyCommand(UpdateOrder);
            DeleteOrderCommand = new ReplyCommand(DeleteOrder);
        }

        public List<Order> Orders
        {
            get => _orders;
            set => SetField(ref _orders, value);
        }

        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set => SetField(ref _selectedOrder, value);
        }

        public ICommand AddOrderCommand { get; }
        public ICommand UpdateOrderCommand { get; }
        public ICommand DeleteOrderCommand { get; }

        public string OrderSearchTerm
        {
            get => _orderSearchTerm;
            set
            {
                SetField(ref _orderSearchTerm, value);
                SearchOrders();
            }
        }

        public void AddOrder(Order newOrder)
        {
            if (newOrder != null)
            {
                _orderService.AddOrder(newOrder);
                LoadOrders();
                SelectedOrder = newOrder;
            }
        }

        public void UpdateOrder(object parameter)
        {
            if (SelectedOrder != null)
            {
                _orderService.UpdateOrder(SelectedOrder);
            }
        }

        public void DeleteOrder(object parameter)
        {
            if (SelectedOrder != null)
            {
                _orderService.DeleteOrder(SelectedOrder.OrderID);
                LoadOrders();
                SelectedOrder = null;
            }
        }

        private void LoadOrders()
        {
            Orders = _orderService.GetAllOrders().ToList();
            Console.WriteLine($"Loaded {Orders.Count} orders.");
        }

        private void SearchOrders()
        {
            if (string.IsNullOrWhiteSpace(OrderSearchTerm))
            {
                LoadOrders();
            }
            else
            {
                Orders = _orderService.GetAllOrders()
                    .Where(o => o.OrderID.Contains(OrderSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                               o.CustomerID.Contains(OrderSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                               (o.OrderDate.HasValue && o.OrderDate.Value.ToShortDateString().Contains(OrderSearchTerm)))
                    .ToList();
            }
        }
    }
}
