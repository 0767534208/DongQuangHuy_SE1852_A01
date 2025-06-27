using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public List<Category> GetAll() => _context.Categories;

        public Category GetById(string id) => _context.Categories.FirstOrDefault(c => c.CategoryID == id);

        public void Add(Category category) => _context.Categories.Add(category);

        public void Update(Category category)
        {
            var existingCategory = GetById(category.CategoryID);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.Description = category.Description;
            }
        }

        public void Delete(string id)
        {
            var category = GetById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }
    }
}
