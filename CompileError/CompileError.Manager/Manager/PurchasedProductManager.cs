using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.Repository.Repository;

namespace CompileError.Manager.Manager
{
    public class PurchasedProductManager
    {
        private readonly PurchasedProductRepository _purchasedProductRepository = new PurchasedProductRepository();

        public bool Add(PurchasedProduct purchasedProduct)
        {
            return _purchasedProductRepository.Add(purchasedProduct);
        }

        public bool Delete(int id)
        {
            return _purchasedProductRepository.Delete(id);
        }

        public bool Update(PurchasedProduct purchasedProduct)
        {
            return _purchasedProductRepository.Update(purchasedProduct);
        }

        public PurchasedProduct GetById(int id)
        {
            return _purchasedProductRepository.GetById(id);
        }

        public List<PurchasedProduct> GetAll()
        {
            return _purchasedProductRepository.GetAll();
        }
    }
}
