using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.Repository.Repository;
namespace CompileError.Manager.Manager
{
    public class PurchaseManager
    {
        private readonly PurchaseRepository _purchaseRepository = new PurchaseRepository();

        public bool Add(Purchase purchase)
        {
            return _purchaseRepository.Add(purchase);
        }

        public bool Delete(int id)
        {
            return _purchaseRepository.Delete(id);
        }

        public bool Update(Purchase purchase)
        {
            return _purchaseRepository.Update(purchase);
        }

        public Purchase GetById(int id)
        {
            return _purchaseRepository.GetById(id);
        }

        public List<Purchase> GetAll()
        {
            return _purchaseRepository.GetAll();
        }
    }
}
