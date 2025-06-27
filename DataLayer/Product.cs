using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string CategoryID { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
