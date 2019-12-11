using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AutoMapper;
using CompileError.DatabaseContext.DatabaseContext;
using CompileError.Model.Model;

namespace CompileError.Repository.Repository
{
    public class PurchaseRepository
    {
        private readonly ProjectDbContext _projectDbContext = new ProjectDbContext();

        public bool Add(Purchase purchase)
        {
            _projectDbContext.Purchases.Add(purchase);
            return _projectDbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Purchase purchase = _projectDbContext.Purchases.FirstOrDefault(c => c.Id == id);
            if (purchase != null) _projectDbContext.Purchases.Remove(purchase);
            return _projectDbContext.SaveChanges() > 0;
        }

        public bool Update(Purchase purchase)
        {
            Purchase cuPurchase = _projectDbContext.Purchases.FirstOrDefault(c => c.Id == purchase.Id);
            //if(cuPurchase!=null)cuPurchase = Mapper.Map<Purchase>(purchase);

            return _projectDbContext.SaveChanges() > 0;
        }

        public Purchase GetById(int id)
        {
            return _projectDbContext.Purchases.FirstOrDefault(c => c.Id == id);
        }

        public List<Purchase> GetAll()
        {
            return _projectDbContext.Purchases.ToList();
        }
    }
}
