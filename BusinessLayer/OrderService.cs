using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders() => _orderRepository.GetAll();

        public Order GetOrderById(string id) => _orderRepository.GetById(id);

        public void AddOrder(Order order)
        {
            _orderRepository.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
        }

        public void DeleteOrder(string id)
        {
            _orderRepository.Delete(id);
        }
    }
}
