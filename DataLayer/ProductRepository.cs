using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public List<Product> GetAll() => _context.Products;

        public Product GetById(string id) => _context.Products.FirstOrDefault(p => p.ProductID == id);

        public void Add(Product product) => _context.Products.Add(product);

        public void Update(Product product)
        {
            var existingProduct = GetById(product.ProductID);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.CategoryID = product.CategoryID;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.UnitsInStock = product.UnitsInStock;
            }
        }

        public void Delete(string id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }
    }
}
