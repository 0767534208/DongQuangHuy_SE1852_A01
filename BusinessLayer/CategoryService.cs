using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories() => _categoryRepository.GetAll();

        public Category GetCategoryById(string id) => _categoryRepository.GetById(id);

        public void AddCategory(Category category)
        {

            _categoryRepository.Add(category);
        }

        public void UpdateCategory(Category category)
        {

            _categoryRepository.Update(category);
        }

        public void DeleteCategory(string id)
        {

            _categoryRepository.Delete(id);
        }
    }
}
