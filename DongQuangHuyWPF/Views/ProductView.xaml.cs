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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        private ProductViewModel _viewModel;

        public ProductView()
        {
            InitializeComponent();

            var dataContext = new DataContext();
            var productService = new ProductService(new ProductRepository(dataContext));
            _viewModel = new ProductViewModel(productService);
            DataContext = _viewModel;
        }

        // Xử lý sự kiện khi nhấn nút Add
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow();
            if (addProductWindow.ShowDialog() == true)
            {
                _viewModel.AddProduct(addProductWindow.NewProduct);
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshProductDataGrid();
            }
        }

        // Xử lý sự kiện khi nhấn nút Edit
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để chỉnh sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editProductWindow = new EditProductWindow(_viewModel.SelectedProduct);
            if (editProductWindow.ShowDialog() == true)
            {
                _viewModel.UpdateProduct(editProductWindow.UpdatedProduct);
                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshProductDataGrid();
            }
        }

        // Xử lý sự kiện khi nhấn nút Delete
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm '{_viewModel.SelectedProduct.ProductName}'?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmResult == MessageBoxResult.Yes)
            {
                _viewModel.DeleteProduct(_viewModel.SelectedProduct);
                MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshProductDataGrid();
            }
        }

        // Xử lý sự kiện khi thay đổi lựa chọn trên DataGrid
        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _viewModel.SelectedProduct = (Product)e.AddedItems[0];
            }
            else
            {
                _viewModel.SelectedProduct = null;
            }
        }

        // Phương thức để làm mới DataGrid
        private void RefreshProductDataGrid()
        {
            ProductDataGrid.ItemsSource = null;
            ProductDataGrid.ItemsSource = _viewModel.Products;
        }
    }
}
