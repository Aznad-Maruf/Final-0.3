using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.Repository.Repository;

namespace CompileError.Manager.Manager
{
    public class PurchaseDetailManager
    {
        PurchaseDetailRepository _purchaseDetailRepository = new PurchaseDetailRepository();
        public List<PurchasedProduct> GetAll()
        {
            return _purchaseDetailRepository.GetAll();

        }
    }
}
