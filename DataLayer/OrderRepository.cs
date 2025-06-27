using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public List<Order> GetAll() => _context.Orders;

        public Order GetById(string id) => _context.Orders.FirstOrDefault(o => o.OrderID == id);

        public void Add(Order order) => _context.Orders.Add(order);

        public void Update(Order order)
        {
            var existingOrder = GetById(order.OrderID);
            if (existingOrder != null)
            {
                existingOrder.CustomerID = order.CustomerID;
                existingOrder.EmployeeID = order.EmployeeID;
                existingOrder.OrderDate = order.OrderDate;
            }
        }

        public void Delete(string id)
        {
            var order = GetById(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }
    }
}
