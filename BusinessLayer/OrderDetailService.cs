using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OrderDetailService
    {
        private readonly OrderDetailRepository _orderDetailRepository;

        public OrderDetailService(OrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public List<OrderDetail> GetAllOrderDetails() => _orderDetailRepository.GetAll();

        public OrderDetail GetOrderDetailById(string orderId, string productId) => _orderDetailRepository.GetById(orderId, productId);

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _orderDetailRepository.Add(orderDetail);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _orderDetailRepository.Update(orderDetail);
        }

        public void DeleteOrderDetail(string orderId, string productId)
        {
            _orderDetailRepository.Delete(orderId, productId);
        }
    }
}
