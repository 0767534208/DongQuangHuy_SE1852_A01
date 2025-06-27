using BusinessLayer;
using DataLayer;
using DongQuangHuyWPF.ViewModels;
using System.Windows.Controls;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerOrderHistoryView.xaml
    /// </summary>
    public partial class CustomerOrderHistoryView : UserControl
    {
        private CustomerOrderHistoryViewModel _viewModel;

        public CustomerOrderHistoryView(Customer loggedInCustomer)
        {
            InitializeComponent();

            var dataContext = new DataContext(); // Using new DataContext()
            var orderService = new OrderService(new OrderRepository(dataContext));
            
            _viewModel = new CustomerOrderHistoryViewModel(loggedInCustomer, orderService);
            DataContext = _viewModel;
        }
    }
} 