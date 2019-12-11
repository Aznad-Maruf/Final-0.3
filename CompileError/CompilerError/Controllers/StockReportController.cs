using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompileError.Manager.Manager;
using CompilerError.Models;
using CompileError.Model.Model;
using CompileError.Models;

namespace CompilerError.Controllers
{
    public class StockReportController : Controller
    {
        SaleManager _saleManager = new SaleManager();
        SalesDetailManager _salesDetailManager = new SalesDetailManager();
        SalesViewModel _salesViewModel = new SalesViewModel();    

        PurchaseModelView _purchaseModelView = new PurchaseModelView();
        PurchaseManager _purchaseManager = new PurchaseManager();
        PurchasedProductManager _purchasedProductManager = new PurchasedProductManager();

        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();


        public ActionResult StockRepot()
        {

            _purchaseModelView.CategorySelectListItems = _categoryManager
                .GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            ViewBag.Category = _purchaseModelView.CategorySelectListItems;

            return View();
        }

        public ActionResult GetStockByDate(int ProductId, string startDate, string endDate)
        {

            var Product = _productManager.GetAll().ToList();
            var Category = _categoryManager.GetAll().ToList();
            var PurchaseProducts = _purchasedProductManager.GetAll().ToList();
            var Purchase = _purchaseManager.GetAll().ToList();

            var q = (from PurPro in PurchaseProducts
                     join Pur in Purchase on PurPro.Purchase.Id equals Pur.Id
                     join Prod in Product on PurPro.ProductId equals Prod.Id
                     join Cat in Category on Prod.CategoryId equals Cat.Id
                     orderby Pur.Date
                     where (Prod.Id == ProductId && (Pur.Date.Contains(startDate) && endDate.Contains(Pur.Date)))
                     select new Stock
                     {
                         Id = PurPro.Id,
                         Code = Prod.Code,
                         Product = Prod.Name,
                         Category = Cat.Name,
                         ReorderLevel = Prod.ReorderLevel,
                         Expdate = PurPro.ExpireDate,
                         OpeningBalance = GetOpeningBal(ProductId, Pur.Date),
                         In = stockIn(ProductId, Pur.Date),
                         Out = stockout(ProductId, Pur.Date),
                         ClosingBalance = GetOpeningBal(ProductId, Pur.Date)
                                       + (stockIn(ProductId, Pur.Date)
                                       - stockout(ProductId, Pur.Date))

                     }).ToList();




            ViewBag.stockDetails = q;
            return PartialView("Stock/_stockDetails");
        }

        public int stockIn(int ProductId, string Date)
        {
            var PurchaseProducts = _purchasedProductManager.GetAll().ToList();
            var Purchase = _purchaseManager.GetAll().ToList();
            int quant;
            try
            {
                var q = (from PurPro in PurchaseProducts
                         join Pur in Purchase on PurPro.Purchase.Id equals Pur.Id
                         where PurPro.ProductId == ProductId && (Pur.Date == Date)
                         select PurPro.Quantity).Sum();
                quant = (int)q;

            }
            catch
            {
                quant = 0;
            }


            return quant;
        }

        public int stockout(int ProductId, string Date)
        {
            var SaleProducts = _salesDetailManager.GetAll().ToList();
            var Sales = _saleManager.GetAll().ToList();
            int quant;
            try
            {
                var q = (from salePro in SaleProducts
                         join sal in Sales on salePro.Sale.Id equals sal.Id
                         where salePro.ProductId == ProductId && (sal.Date == Date)
                         select salePro.Quantity).Sum();
                quant = q;

            }
            catch
            {
                quant = 0;
            }

            return quant;
        }
        public int GetOpeningBal(int ProductId, string startDate)
        {
            var Product = _productManager.GetAll().ToList();
            var Category = _categoryManager.GetAll().ToList();
            var PurchaseProducts = _purchasedProductManager.GetAll().ToList();
            var Purchase = _purchaseManager.GetAll().ToList();
            int quant;
            try
            {
                var q = (from PurPro in PurchaseProducts
                         join Pur in Purchase on PurPro.Purchase.Id equals Pur.Id
                         where PurPro.ProductId == ProductId && (Pur.Date.Contains(startDate))
                         select PurPro.Quantity).Sum();
                quant = Convert.ToInt32(q - GetSale(ProductId, startDate));

            }
            catch
            {
                quant = 0;
            }


            return quant;
        }
        public int GetSale(int ProductId, string startDate)
        {

            var SaleProducts = _salesDetailManager.GetAll().ToList();
            var Sales = _saleManager.GetAll().ToList();
            int quant;
            try
            {
                var q = (from salePro in SaleProducts
                         join sal in Sales on salePro.Sale.Id equals sal.Id
                         where salePro.ProductId == ProductId && (sal.Date.Contains(startDate))
                         select salePro.Quantity).Sum();
                quant = q;

            }
            catch
            {
                quant = 0;
            }

            return quant;
        }

        public class Stock
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Product { get; set; }
            public string Category { get; set; }
            public int ReorderLevel { get; set; }
            public string Expdate { get; set; }
            public int OpeningBalance { get; set; }
            public int In { get; set; }
            public int Out { get; set; }
            public int ClosingBalance { get; set; }
        }

    }
}