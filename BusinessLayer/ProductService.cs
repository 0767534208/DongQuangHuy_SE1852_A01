using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts() => _productRepository.GetAll();

        public Product GetProductById(string id) => _productRepository.GetById(id);

        public void AddProduct(Product product)
        {
            _productRepository.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
        }

        public void DeleteProduct(string id)
        {
            _productRepository.Delete(id);
        }
    }
}
