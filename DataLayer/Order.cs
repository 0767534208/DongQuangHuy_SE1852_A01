using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Order
    {
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
