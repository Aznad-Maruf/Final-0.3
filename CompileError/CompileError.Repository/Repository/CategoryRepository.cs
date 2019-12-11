using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompileError.Model.Model;
using CompileError.DatabaseContext.DatabaseContext;

namespace CompileError.Repository.Repository
{
    public class CategoryRepository
    {
        private readonly ProjectDbContext _dbContext = new ProjectDbContext();

        public bool Add(Category category)
        {
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges()>0;
            
        }

        public bool Delete(int id)
        {
            Category aCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            _dbContext.Categories.Remove(aCategory);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(Category category)
        {
            Category aCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (aCategory != null)
            {
                aCategory.Code = category.Code;
                aCategory.Name = category.Name;
            }
            return _dbContext.SaveChanges() > 0;
        }

        public Category GetById(int id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        public List<Category> GetAll()
        {
           return _dbContext.Categories.ToList();
        }
        
        public Category Search(int id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
