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
    
    public class SalesController : Controller
    {
        SalesViewModel salesViewModel = new SalesViewModel();
        CustomerManager _customerManager = new CustomerManager();
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        PurchaseDetailManager _purchaseDetailManager = new PurchaseDetailManager();
        SalesDetailManager _salesDetailManager = new SalesDetailManager();
        SaleManager _saleManager = new SaleManager();
        SalesDetail salesDetail = new SalesDetail();
        PurchasedProductManager _purchasedProductManager = new PurchasedProductManager();
        PurchasedProduct purchasedProduct = new PurchasedProduct();

        [HttpGet]
        public ActionResult Sales()
        {
         
           var customers = _customerManager.GetAll();
          //  var customer = from c in customers select (new { c.Id, c.Name });
            salesViewModel.CustomersSelectListItem = customers
               
                .Select(c => new SelectListItem()
                { 
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();


            ViewBag.Customer = salesViewModel.CustomersSelectListItem;

            var category = _categoryManager.GetAll();
            salesViewModel.CategorySelectListItem = category

                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();


            ViewBag.Category = salesViewModel.CategorySelectListItem;
            return View(salesViewModel);

        }
        

        public JsonResult GetLoyalityPointByCustomer(int customerid)
        {
            var customerList = _customerManager.GetAll().Where(c => c.Id == customerid).ToList();
            var customers = from s in customerList select (s.LoyalityPoint);
            return Json(customers, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetProductByCategory(int categoryId)
        {
            var productList = _productManager.GetAll().Where(c => c.CategoryId== categoryId).ToList();
            var products = from s in productList select (new { s.Id, s.Name});
            return Json(products, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetQuantityByProduct(int productcode)
        {
            var productList = _purchasedProductManager.GetAll().Where(c => c.ProductId == productcode).ToList();
            //var productList = _purchaseDetailManager.GetAll().Where(c => c.Code == productcode).ToList();
            var products = from s in productList select (new { s.Quantity, s.Mrp});
            return Json(products, JsonRequestBehavior.AllowGet);

        }
        //public JsonResult AddSalesDetails(string ProductCode, int Quantity, int MRP, int TotalMRP)
        public JsonResult AddSales(int CustomerID, string Date, int Loyalitypoint)
        {
            
            var sales = _saleManager.GetAll().ToList();
            var code = from s in sales orderby s.Id descending select s.Code;
            string salecode = code.ToString();
            

            // if (code == null || code == "")
            if (sales.Count()>0)
            {
                salecode = code.ToString();
                string sub = salecode.Substring(5, 4);
                int c = Convert.ToInt32(sub);
                c++;
                string s = c.ToString("0000");
                salecode = "2019-" + s;
            }
            else
            {
                salecode = "2019-0001";
                
                
            }

            Sale sale = new Sale();
            sale.CustomerId = CustomerID;
            sale.Date = Date;
           
           
            sale.Code = salecode;

            _saleManager.Add(sale);
            

                var productList = _saleManager.GetAll().Where(c => c.Code == salecode).ToList();
                var salesId = from s in productList select (s.Id);


                return Json(salesId, JsonRequestBehavior.AllowGet);
            
            
        }
        public JsonResult AddSalesDetails(int ProductCode, int Quantity, double MRP, double TotalMRP, int SalesID, double Aquantity)
        {

            salesDetail.ProductId = ProductCode;
            salesDetail.Quantity = Quantity;
            salesDetail.MRP = MRP;
            salesDetail.TotalMRP = TotalMRP;
            salesDetail.SaleId = SalesID;

            _salesDetailManager.Add(salesDetail);

            var qu = Convert.ToDouble(Quantity);
            var aq = Aquantity;

            purchasedProduct.Quantity =aq-qu;
            purchasedProduct.ProductId = ProductCode;
            _purchasedProductManager.Update(purchasedProduct);
            string mess = "success";
           
            return Json(mess, JsonRequestBehavior.AllowGet);

        }
     
    }
}