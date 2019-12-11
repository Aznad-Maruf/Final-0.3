using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.DatabaseContext.DatabaseContext;

namespace CompileError.Repository.Repository
{
    public class PurchaseDetailRepository
    {
        ProjectDbContext _projectDbContext = new ProjectDbContext();
        public List<PurchasedProduct> GetAll()
        {

            return _projectDbContext.PurchasedProducts.ToList();


        }
    }
}
