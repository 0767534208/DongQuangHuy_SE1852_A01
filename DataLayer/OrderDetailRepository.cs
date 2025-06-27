using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderDetailRepository
    {
        private readonly DataContext _context;

        public OrderDetailRepository(DataContext context)
        {
            _context = context;
        }

        public List<OrderDetail> GetAll() => _context.OrderDetails;

        public OrderDetail GetById(string orderId, string productId) => _context.OrderDetails.FirstOrDefault(od => od.OrderID == orderId && od.ProductID == productId);

        public void Add(OrderDetail orderDetail) => _context.OrderDetails.Add(orderDetail);

        public void Update(OrderDetail orderDetail)
        {
            var existingOrderDetail = GetById(orderDetail.OrderID, orderDetail.ProductID);
            if (existingOrderDetail != null)
            {
                existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.Discount = orderDetail.Discount;
            }
        }

        public void Delete(string orderId, string productId)
        {
            var orderDetail = GetById(orderId, productId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }
        }
    }
}
