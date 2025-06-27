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
    public class OrderDetailViewModel : BaseViewModel
    {
        private readonly OrderDetailService _orderDetailService;
        private OrderDetail _selectedOrderDetail;
        private string _orderDetailSearchTerm;
        private List<OrderDetail> _orderDetails;

        public OrderDetailViewModel(OrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
            OrderDetails = _orderDetailService.GetAllOrderDetails().ToList();

            AddOrderDetailCommand = new ReplyCommand(AddOrderDetail);
            UpdateOrderDetailCommand = new ReplyCommand(UpdateOrderDetail);
            DeleteOrderDetailCommand = new ReplyCommand(DeleteOrderDetail);
        }

        public List<OrderDetail> OrderDetails
        {
            get => _orderDetails;
            set => SetField(ref _orderDetails, value);
        }

        public OrderDetail SelectedOrderDetail
        {
            get => _selectedOrderDetail;
            set => SetField(ref _selectedOrderDetail, value);
        }

        public ICommand AddOrderDetailCommand { get; }
        public ICommand UpdateOrderDetailCommand { get; }
        public ICommand DeleteOrderDetailCommand { get; }

        public string OrderDetailSearchTerm
        {
            get => _orderDetailSearchTerm;
            set
            {
                SetField(ref _orderDetailSearchTerm, value);
                SearchOrderDetails();
            }
        }

        public void AddOrderDetail(object parameter)
        {
            var newOrderDetail = new OrderDetail();
            _orderDetailService.AddOrderDetail(newOrderDetail);
            OrderDetails.Add(newOrderDetail);
            SelectedOrderDetail = newOrderDetail;
        }

        public void UpdateOrderDetail(object parameter)
        {
            if (SelectedOrderDetail != null)
            {
                _orderDetailService.UpdateOrderDetail(SelectedOrderDetail);
            }
        }

        public void DeleteOrderDetail(object parameter)
        {
            if (SelectedOrderDetail != null)
            {
                // Sử dụng OrderID và ProductID làm composite key
                _orderDetailService.DeleteOrderDetail(
                    SelectedOrderDetail.OrderID,
                    SelectedOrderDetail.ProductID);
                OrderDetails.Remove(SelectedOrderDetail);
                SelectedOrderDetail = null;
            }
        }

        public void SearchOrderDetails()
        {
            OrderDetails = string.IsNullOrEmpty(OrderDetailSearchTerm)
                ? _orderDetailService.GetAllOrderDetails().ToList()
                : _orderDetailService.GetAllOrderDetails()
                    .Where(od =>
                        od.OrderID.Contains(OrderDetailSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                        od.ProductID.Contains(OrderDetailSearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }
    }
}
