using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompileError.Model.Model;
using CompileError.Repository.Repository;

namespace CompileError.Manager.Manager
{
    public class CategoryManager
    {
        private readonly CategoryRepository _categoryRepository = new CategoryRepository();

        public bool Add(Category category)
        {
            return _categoryRepository.Add(category);
        }

        public bool Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public bool Update(Category category)
        {
            return _categoryRepository.Update(category);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category Search(int id)
        {
            return _categoryRepository.Search(id);
        }
    }
}
