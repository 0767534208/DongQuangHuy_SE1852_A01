using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DongQuangHuyWPF.ViewModels;
using BusinessLayer;
using DataLayer;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        private OrderViewModel _viewModel;

        public OrderView()
        {
            InitializeComponent();

            var dataContext = new DataContext();
            var orderService = new OrderService(new OrderRepository(dataContext));
            _viewModel = new OrderViewModel(orderService);
            DataContext = _viewModel;
        }

        // Xử lý sự kiện khi nhấn nút Create Order
        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var createOrderWindow = new CreateOrderWindow();
            if (createOrderWindow.ShowDialog() == true)
            {
                // Logic để thêm đơn hàng từ CreateOrderWindow
                _viewModel.AddOrder(createOrderWindow.NewOrder); // Giả định NewOrder là thuộc tính trong CreateOrderWindow
                MessageBox.Show("Tạo đơn hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshOrderDataGrid();
            }
        }

        // Xử lý sự kiện khi thay đổi lựa chọn trên DataGrid
        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _viewModel.SelectedOrder = (Order)e.AddedItems[0];
            }
            else
            {
                _viewModel.SelectedOrder = null;
            }
        }

        // Phương thức để làm mới DataGrid
        private void RefreshOrderDataGrid()
        {
            OrderDataGrid.ItemsSource = null;
            OrderDataGrid.ItemsSource = _viewModel.Orders;
        }
    }
}
