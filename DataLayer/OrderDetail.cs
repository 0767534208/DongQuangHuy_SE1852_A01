using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderDetail
    {
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
}
