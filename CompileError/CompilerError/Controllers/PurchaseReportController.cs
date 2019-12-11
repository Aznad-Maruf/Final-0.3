using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CompileError.Model.Model;

using CompilerError.Models;

using AutoMapper;
using CompileError.Manager.Manager;
//using Rotativa;

namespace CompilerError.Controllers
{
    public class PurchaseReportController : Controller
    {
        PurchaseManager _purchaseManager = new PurchaseManager();
        PurchasedProductManager _purchasedProductManager = new PurchasedProductManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        //SaleManager _saleManager = new SaleManager();
        //SalesDetailManager _salesDetailManager = new SalesDetailManager();


        // GET: PurchaseReport
        [HttpGet]
        public ActionResult Show()
        {
            PurchaseReportViewModel purchaseReportViewModel = new PurchaseReportViewModel();
            purchaseReportViewModel.Purchases = _purchaseManager.GetAll();
            return View();
        }

        public ActionResult GetPurchaseReportByDate(string startDate, string endDate)
        {
            PurchaseReportViewModel purchaseReportViewModel = new PurchaseReportViewModel();
            var purchases = _purchaseManager.GetAll().ToList();
            var purchaseDetails = _purchasedProductManager.GetAll().ToList();
            var products = _productManager.GetAll().ToList();
            var categories = _categoryManager.GetAll().ToList();
            //var sales = _saleManager.GetAll().ToList();
            //var salesDetails = _salesDetailManager.GetAll().ToList();

            var purchase = (from p in purchases where p.Date.CompareTo(startDate) >= 0 && p.Date.CompareTo(endDate) <= 0 select p).ToList();

            var count = (from pd in purchaseDetails
                         join p in purchase on pd.PurchaseId equals p.Id
                         join pr in products on pd.ProductId equals pr.Id
                         join cat in categories on pr.CategoryId equals cat.Id
                         orderby pd.Id
                         select new PurchaseReport
                         {
                             ProductId = pd.ProductId,
                             ProductCode = pr.Code,
                             ProductName = pr.Name,
                             Category = cat.Name,
                             Quantity = pd.Quantity,
                             UnitPrice = pd.UnitPrice * pd.Quantity,
                             MRP = pd.Mrp * pd.Quantity,
                             Profit = pd.Quantity * (pd.Mrp - pd.UnitPrice)
                         }).ToList();

            var sum = (from c in count
                       group c by c.ProductId into egroup
                       select new PurchaseReportShow
                       {
                           ProductId = egroup.First().ProductId,
                           ProductName = egroup.First().ProductName,
                           ProductCode = egroup.First().ProductCode,
                           Category = egroup.First().Category,
                           Quantity = egroup.Sum(p => p.Quantity),
                           UnitPrice = egroup.Sum(p => p.UnitPrice),
                           MRP = egroup.Sum(p => p.MRP),
                           Profit = egroup.Sum(p => p.Profit)
                       }).ToList();
            ViewBag.purchaseDetails = sum;
            return PartialView("PurchaseReportPartial/_PurchaseReport");
        }

        //Print PDF
        //public ActionResult PrintViewToPdf()
        //{
          //  var report = new ActionAsPdf("Show");
            //var report = new ActionAsPdf("PurchaseReportPartial/_PurchaseReport");
            //var report = new ActionAsPdf("~/Views/Shared/PurchaseReportPartial/_PurchaseReport");
            //return report;
        //}

        public class PurchaseReport
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string Category { get; set; }
            public double UnitPrice { get; set; }
            public double Quantity { get; set; }
            public double MRP { get; set; }
            public double TotalMRP { get; set; }

            public double Profit { get; set; }
        }

        public class PurchaseReportShow
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string Category { get; set; }

            public double UnitPrice { get; set; }
            public double Quantity { get; set; }
            public double MRP { get; set; }
            public double TotalMRP { get; set; }

            public double Profit { get; set; }
        }

    }
}