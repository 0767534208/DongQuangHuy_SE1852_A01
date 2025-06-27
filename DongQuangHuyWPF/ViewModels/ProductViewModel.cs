using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DongQuangHuyWPF.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        private Product? _selectedProduct;
        private string _productSearchTerm = string.Empty;
        private List<Product> _products;

        public ProductViewModel(ProductService productService)
        {
            _productService = productService;
            LoadProducts();

            AddProductCommand = new ReplyCommand(param => { /* Handled by Click event in View */ });
            UpdateProductCommand = new ReplyCommand(param => { /* Handled by Click event in View */ });
            DeleteProductCommand = new ReplyCommand(param => { /* Handled by Click event in View */ });
        }

        public List<Product> Products
        {
            get => _products;
            set => SetField(ref _products, value);
        }

        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set => SetField(ref _selectedProduct, value);
        }

        public ICommand AddProductCommand { get; }
        public ICommand UpdateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public string ProductSearchTerm
        {
            get => _productSearchTerm;
            set
            {
                SetField(ref _productSearchTerm, value);
                SearchProducts();
            }
        }

        public void AddProduct(Product newProduct)
        {
            if (newProduct != null)
            {
                _productService.AddProduct(newProduct);
                LoadProducts();
                SelectedProduct = newProduct;
            }
        }

        public void UpdateProduct(Product updatedProduct)
        {
            if (updatedProduct != null)
            {
                _productService.UpdateProduct(updatedProduct);
                LoadProducts();
                SelectedProduct = Products.FirstOrDefault(p => p.ProductID == updatedProduct.ProductID);
            }
        }

        public void DeleteProduct(Product productToDelete)
        {
            if (productToDelete != null)
            {
                _productService.DeleteProduct(productToDelete.ProductID);
                LoadProducts();
                SelectedProduct = null;
            }
        }

        private void LoadProducts()
        {
            Products = _productService.GetAllProducts().ToList();
            Console.WriteLine($"Loaded {Products.Count} products.");
        }

        private void SearchProducts()
        {
            if (string.IsNullOrWhiteSpace(ProductSearchTerm))
            {
                LoadProducts();
            }
            else
            {
                Products = _productService.GetAllProducts()
                    .Where(p => p.ProductName.Contains(ProductSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                               p.ProductID.Contains(ProductSearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }
    }
}
