using System.Windows;
using DataLayer;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public Product NewProduct { get; private set; }

        public AddProductWindow()
        {
            InitializeComponent();
            NewProduct = new Product();
            DataContext = NewProduct;
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
            if (string.IsNullOrWhiteSpace(NewProduct.ProductName) ||
                string.IsNullOrWhiteSpace(NewProduct.CategoryID))
            {
                return false;
            }

            // Validate UnitPrice and UnitsInStock are valid numbers and greater than 0
            if (NewProduct.UnitPrice <= 0 || NewProduct.UnitsInStock <= 0)
            {
                return false;
            }

            return true;
        }
    }
} 