using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.Repository.Repository;

namespace CompileError.Manager.Manager
{
    public class SaleManager
    {
        SaleRepository _saleRepository = new SaleRepository();

        public bool Add(Sale sale)
        {
            return _saleRepository.Add(sale);
        }
        public List<Sale> GetAll()
        {
            return _saleRepository.GetAll();

        }
    }
}
