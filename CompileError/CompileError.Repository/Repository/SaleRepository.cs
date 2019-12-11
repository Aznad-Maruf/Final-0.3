using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.DatabaseContext.DatabaseContext;

namespace CompileError.Repository.Repository
{
    public class SaleRepository
    {
        ProjectDbContext _projectDbContext = new ProjectDbContext();
        public bool Add(Sale sale)
        {
            _projectDbContext.Sales.Add(sale);

            return _projectDbContext.SaveChanges() > 0;
        }
        public List<Sale> GetAll()
        {

            return _projectDbContext.Sales.ToList();


        }
    }
}
