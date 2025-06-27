using BusinessLayer;
using DataLayer;

namespace DongQuangHuyWPF.ViewModels
{
    public class AdminDashboardViewModel : BaseViewModel
    {
        public CustomerViewModel CustomerVM { get; set; }
        public ProductViewModel ProductVM { get; set; }
        public OrderViewModel OrderVM { get; set; }

        public AdminDashboardViewModel()
        {
            var dataContext = DataContext.Instance;
            
            // Initialize CustomerViewModel
            var customerService = new CustomerService(new CustomerRepository(dataContext));
            CustomerVM = new CustomerViewModel(customerService);

            // Initialize ProductViewModel (Assuming ProductService and ProductRepository exist)
            var productService = new ProductService(new ProductRepository(dataContext));
            ProductVM = new ProductViewModel(productService);

            // Initialize OrderViewModel (Assuming OrderService and OrderRepository exist)
            var orderService = new OrderService(new OrderRepository(dataContext));
            OrderVM = new OrderViewModel(orderService);
        }
    }
} 