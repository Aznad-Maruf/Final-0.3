using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.DatabaseContext;
using CompileError.DatabaseContext.DatabaseContext;
//using AutoMapper;

namespace CompileError.Repository.Repository
{
    public class PurchasedProductRepository
    {
        private readonly ProjectDbContext _projectDbContext = new ProjectDbContext();
        public bool Add(PurchasedProduct purchasedProduct)
        {
            _projectDbContext.PurchasedProducts.Add(purchasedProduct);
            return _projectDbContext.SaveChanges() > 0;
        }

        public bool Update(PurchasedProduct purchasedProduct)
        {
            PurchasedProduct cuPurchasedProduct =
                _projectDbContext.PurchasedProducts.FirstOrDefault(c => c.ProductId == purchasedProduct.ProductId);

            //if(cuPurchasedProduct!=null) cuPurchasedProduct = Mapper.Map<PurchasedProduct>(purchasedProduct);
            if (cuPurchasedProduct != null)
            {
                cuPurchasedProduct.PurchaseId = cuPurchasedProduct.PurchaseId;
                cuPurchasedProduct.Quantity = purchasedProduct.Quantity;
                cuPurchasedProduct.ProductId = cuPurchasedProduct.ProductId;
                cuPurchasedProduct.Mrp = cuPurchasedProduct.Mrp;
                cuPurchasedProduct.ManufactureDate = cuPurchasedProduct.ManufactureDate;
                cuPurchasedProduct.ExpireDate = cuPurchasedProduct.ExpireDate;
                cuPurchasedProduct.UnitPrice= cuPurchasedProduct.UnitPrice;
                cuPurchasedProduct.Remarks = cuPurchasedProduct.Remarks;

            }

            return _projectDbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            PurchasedProduct purchasedProduct = _projectDbContext.PurchasedProducts.FirstOrDefault(c => c.Id == id);
            if(purchasedProduct!=null)_projectDbContext.PurchasedProducts.Remove(purchasedProduct);

            return _projectDbContext.SaveChanges() > 0;
        }

        public PurchasedProduct GetById(int id)
        {
            return _projectDbContext.PurchasedProducts.FirstOrDefault(c => c.Id == id);
        }

        public List<PurchasedProduct> GetAll()
        {
            return _projectDbContext.PurchasedProducts.ToList();
        }

    }
}
