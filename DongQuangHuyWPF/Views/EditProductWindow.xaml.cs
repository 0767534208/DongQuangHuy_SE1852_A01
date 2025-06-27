using System.Windows;
using DataLayer;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        public Product UpdatedProduct { get; private set; }

        public EditProductWindow(Product product)
        {
            InitializeComponent();
            // Tạo một bản sao của sản phẩm để chỉnh sửa
            UpdatedProduct = new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };
            DataContext = UpdatedProduct;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm hợp lệ.\n\nLưu ý:\n- Tên sản phẩm không được trống.\n- Mã danh mục không được trống.\n- Đơn giá và số lượng tồn kho phải là số và lớn hơn 0.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool IsValid()
        {
            // Basic validation for required fields
            if (string.IsNullOrWhiteSpace(UpdatedProduct.ProductName) ||
                string.IsNullOrWhiteSpace(UpdatedProduct.CategoryID))
            {
                return false;
            }

            // Validate UnitPrice and UnitsInStock are valid numbers and greater than 0
            if (UpdatedProduct.UnitPrice <= 0 || UpdatedProduct.UnitsInStock <= 0)
            {
                return false;
            }

            return true;
        }
    }
} 