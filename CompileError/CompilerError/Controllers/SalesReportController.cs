using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompileError.Model.Model;

using CompilerError.Models;

using AutoMapper;
using CompileError.Manager.Manager;

namespace CompilerError.Controllers
{
    public class SalesReportController : Controller
    {
        SaleManager _saleManager = new SaleManager();
        SalesDetailManager _salesDetailManager = new SalesDetailManager();
        PurchasedProductManager _purchasedProductManager = new PurchasedProductManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();

          [HttpGet]
        public ActionResult Show()
        {
            SalesReportViewModel salesReportViewModel = new SalesReportViewModel();
            salesReportViewModel.Sales = _saleManager.GetAll();
            return View();
        }
        public ActionResult GetSalesReportByDate(string startDate, string endDate)
        {
            SalesReportViewModel salesReportViewModel = new SalesReportViewModel();
            var sales = _saleManager.GetAll().ToList();
            var saledetails = _salesDetailManager.GetAll().ToList();
            var purchase = _purchasedProductManager.GetAll().ToList();
            var product = _productManager.GetAll().ToList();
            var category = _categoryManager.GetAll().ToList();

            // purchase = purchase.Where(c => c.Date == StartDate).ToList();
            var sale = (from p in sales where p.Date.CompareTo(startDate) >= 0 && p.Date.CompareTo(endDate) < 0 select p).ToList();

            var count = (from sd in saledetails
                         join s in sale on sd.SaleId equals s.Id
                         join pu in purchase on sd.ProductId equals pu.ProductId
                         join p in product on sd.ProductId equals p.Id
                         join c in category on p.CategoryId equals c.Id
                         orderby sd.Id
                         select new SalesReport
                         {

                             ProductID = sd.ProductId,
                             Code=p.Code,
                             ProductName=p.Name,
                             CategoryName=c.Name,
                             Quantity = sd.Quantity,
                             UnitPrice =sd.Quantity* pu.UnitPrice,
                             MRP = sd.MRP*sd.Quantity,
                             Profit = sd.Quantity * (sd.MRP -pu.UnitPrice)
                         }).ToList();


            var Sum = (from c in count
                           group c by c.Code into egroup
                           select new SalesReportShow {
                               ProductID =egroup.First().ProductID,
                               Code=egroup.First().Code,
                               ProductName=egroup.First().ProductName,
                               CategoryName=egroup.First().CategoryName,
                               Quantity=egroup.Sum(s=>s.Quantity),
                               UnitPrice=egroup.Sum(s=>s.UnitPrice),
                               MRP=egroup.Sum(s=>s.MRP),
                               Profit=egroup.Sum(s=>s.Profit)

                           }).ToList();
            ViewBag.salesDetails = Sum;

            return PartialView("Report/_SalesReport");
        }
       
        public class SalesReport
        {
      
            public int ProductID { get; set; }
            public string Code { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public string Category { get; set; }
            public double UnitPrice { get; set; }
            public int Quantity { get; set; }
            public double MRP { get; set; }
      
            public double Profit { get; set; }
     


        }
        public class SalesReportShow
        {

            public int ProductID { get; set; }
            public string Code { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public double UnitPrice { get; set; }
            public int Quantity { get; set; }
            public double MRP { get; set; }
         
            public double Profit { get; set; }



        }

    }
}