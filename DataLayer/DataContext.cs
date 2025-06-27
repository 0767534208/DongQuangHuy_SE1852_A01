using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataContext
    {
        private static DataContext _instance = null;
        private static readonly object _lock = new object();

        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Category> Categories { get; set; }

        public DataContext()
        {
            Categories = new List<Category>
    {
        new Category { CategoryID = "1", CategoryName = "Điện tử", Description = "Thiết bị điện tử công nghệ" },
        new Category { CategoryID = "2", CategoryName = "Gia dụng", Description = "Đồ dùng gia đình" },
        new Category { CategoryID = "3", CategoryName = "Văn phòng phẩm", Description = "Dụng cụ văn phòng" },
        new Category { CategoryID = "4", CategoryName = "Thời trang", Description = "Quần áo, phụ kiện" },
        new Category { CategoryID = "5", CategoryName = "Thực phẩm", Description = "Đồ ăn, thức uống" }
    };

            Products = new List<Product>
    {
        new Product { ProductID = "1", ProductName = "Laptop Dell XPS 15", CategoryID = "1", UnitPrice = 25000000, UnitsInStock = 15 },
        new Product { ProductID = "2", ProductName = "iPhone 14 Pro Max", CategoryID = "1", UnitPrice = 28000000, UnitsInStock = 8 },
        new Product { ProductID = "3", ProductName = "Máy giặt Toshiba 10kg", CategoryID = "2", UnitPrice = 12000000, UnitsInStock = 6 },

    };


            Customers = new List<Customer>
    {
        new Customer { CustomerID = "1", CompanyName = "FPT Software", ContactName = "Nguyễn Văn A", Phone = "0912345678", Address = "Hà Nội", Password = "" },
        new Customer { CustomerID = "2", CompanyName = "Viettel Group", ContactName = "Trần Thị B", Phone = "0987654321", Address = "TP.HCM" , Password = ""},

    };


            Employees = new List<Employee>
    {
        new Employee { EmployeeID = "1", Name = "Admin System", UserName = "admin", Password = "123456", JobTitle = "Quản trị hệ thống" },
        new Employee { EmployeeID = "2", Name = "Nguyễn Văn B", UserName = "sales1", Password = "123456", JobTitle = "Nhân viên bán hàng" },

    };


            Orders = new List<Order>
    {
        new Order { OrderID = "1", CustomerID = "1", EmployeeID = "2", OrderDate = DateTime.Now.AddDays(-10) },

    };


            OrderDetails = new List<OrderDetail>
    {
        new OrderDetail { OrderID = "1", ProductID = "1", UnitPrice = 25000000, Quantity = 1, Discount = 3 },
        new OrderDetail { OrderID = "1", ProductID = "3", UnitPrice = 12000000, Quantity = 2, Discount = 4 },

    };
        }

        public static DataContext Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataContext();
                    }
                    return _instance;
                }
            }
        }
    }
}
