using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.DatabaseContext.DatabaseContext;

namespace CompileError.Repository.Repository
{
    public class ProductRepository
    {
        private readonly ProjectDbContext _projectDbContext = new ProjectDbContext();

        public bool Add(Product product)
        {
            _projectDbContext.Products.Add(product);
            return _projectDbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Product product = _projectDbContext.Products.FirstOrDefault(c => c.Id == id);
            if (product != null) _projectDbContext.Products.Remove(product);
            return _projectDbContext.SaveChanges() > 0;
        }

        public bool Update(Product product)
        {
            Product cuProductModel = _projectDbContext.Products.FirstOrDefault(c => c.Id == product.Id);

            if (cuProductModel != null) CopyValues(product, cuProductModel);

            return _projectDbContext.SaveChanges() > 0;
        }

        public List<Product> GetAll()
        {
            return _projectDbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _projectDbContext.Products.FirstOrDefault(c => c.Id == id);
        }

        private void CopyValues(Product product, Product cuProductModel)
        {
            cuProductModel.Category = product.Category;
            cuProductModel.Code = product.Code;
            cuProductModel.Description = product.Description;
            cuProductModel.Name = product.Name;
            cuProductModel.ReorderLevel = product.ReorderLevel;
        }
    }
}
