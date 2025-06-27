using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataLayer;
using BusinessLayer;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        public Order NewOrder { get; private set; }
        private List<DisplayOrderDetail> _temporaryOrderDetails;

        private readonly CustomerService _customerService;
        private readonly ProductService _productService;

        public CreateOrderWindow()
        {
            InitializeComponent();

            var dataContext = new DataContext();
            _customerService = new CustomerService(new CustomerRepository(dataContext));
            _productService = new ProductService(new ProductRepository(dataContext));

            _temporaryOrderDetails = new List<DisplayOrderDetail>();
            NewOrder = new Order { OrderDate = DateTime.Now };
            
            CustomerComboBox.ItemsSource = _customerService.GetAllCustomers();
            ProductComboBox.ItemsSource = _productService.GetAllProducts();

            OrderDetailsDataGrid.ItemsSource = _temporaryOrderDetails;
            OrderDatePicker.SelectedDate = NewOrder.OrderDate;
        }

        private void AddProductToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ. Vui lòng nhập số nguyên dương.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(DiscountTextBox.Text, out double discount) || discount < 0 || discount > 100)
            {
                MessageBox.Show("Chiết khấu không hợp lệ. Vui lòng nhập số từ 0 đến 100.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedProduct = (Product)ProductComboBox.SelectedItem;

            var existingDetail = _temporaryOrderDetails.FirstOrDefault(od => od.ProductID == selectedProduct.ProductID);
            if (existingDetail != null)
            {
                existingDetail.Quantity += quantity;
                existingDetail.Discount = discount / 100.0;
            }
            else
            {
                _temporaryOrderDetails.Add(new DisplayOrderDetail
                {
                    ProductID = selectedProduct.ProductID,
                    ProductName = selectedProduct.ProductName,
                    Quantity = quantity,
                    UnitPrice = selectedProduct.UnitPrice,
                    Discount = discount / 100.0
                });
            }
            
            RefreshOrderDetailsDataGrid();
            MessageBox.Show("Sản phẩm đã được thêm vào chi tiết đơn hàng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_temporaryOrderDetails.Count == 0)
            {
                MessageBox.Show("Đơn hàng phải có ít nhất một sản phẩm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (OrderDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày đặt hàng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewOrder.OrderID = Guid.NewGuid().ToString();
            NewOrder.CustomerID = ((Customer)CustomerComboBox.SelectedItem).CustomerID;
            NewOrder.OrderDate = OrderDatePicker.SelectedDate;
            NewOrder.OrderDetails = _temporaryOrderDetails.Select(od => new OrderDetail
            {
                ProductID = od.ProductID,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                Discount = od.Discount
            }).ToList();

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void RefreshOrderDetailsDataGrid()
        {
            OrderDetailsDataGrid.ItemsSource = null;
            OrderDetailsDataGrid.ItemsSource = _temporaryOrderDetails;
        }

        public class DisplayOrderDetail : OrderDetail
        {
            public string ProductName { get; set; }
        }
    }
} 