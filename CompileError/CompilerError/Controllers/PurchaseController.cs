using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using CompileError.Manager.Manager;
using CompileError.Model.Model;
using CompileError.Models;
using AutoMapper;

namespace CompileError.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly CategoryManager _categoryManager = new CategoryManager();
        private readonly ProductManager _productManager = new ProductManager();
        private readonly SupplierManager _supplierManager = new SupplierManager();
        private readonly PurchaseManager _purchaseManager = new PurchaseManager();
        private readonly PurchasedProductManager _purchasedProductManager = new PurchasedProductManager();
        private readonly SalesDetailManager _salesDetailManager = new SalesDetailManager();

        [HttpGet]
        public ActionResult Add()
        {
            PurchaseModelView purchaseModelView = new PurchaseModelView();
            FillComboBox(purchaseModelView);

            return View(purchaseModelView);
        }

        [HttpPost]
        public ActionResult Add(PurchaseModelView purchaseModelView)
        {

            if (purchaseModelView.PurchasedProducts.Count > 0)
            {
                var purchase = Mapper.Map<Purchase>(purchaseModelView);
                _purchaseManager.Add(purchase);

                var purchaseId = 0;
                foreach (Purchase purchase1 in _purchaseManager.GetAll())
                {
                    purchaseId = Math.Max(purchaseId, purchase1.Id);
                }

                foreach (PurchasedProduct purchasedProduct in purchaseModelView.PurchasedProducts)
                {
                    purchasedProduct.PurchaseId = purchaseId;
                    _purchasedProductManager.Add(purchasedProduct);
                }
            }




            FillComboBox(purchaseModelView);
            return View(purchaseModelView);
        }

        private void FillComboBox(PurchaseModelView purchaseModelView)
        {
            purchaseModelView.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            purchaseModelView.SupplierSelectListItems = _supplierManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            //purchaseModelView.ProductSelectListItems = new List<SelectListItem>();


            purchaseModelView.ProductSelectListItems = _productManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
        }

        private void FillPurchaseModelViews(PurchaseModelView purchaseModelView)
        {
            var purchasedProducts = _purchasedProductManager.GetAll();

            foreach (PurchasedProduct purchasedProduct in purchasedProducts)
            {
                PurchaseModelView purchase = new PurchaseModelView
                {
                    Id = purchasedProduct.Id,
                    Date = _purchaseManager.GetById(purchasedProduct.PurchaseId).Date,
                    BillNo = _purchaseManager.GetById(purchasedProduct.PurchaseId).BillNo,
                    SupplierId = _purchaseManager.GetById(purchasedProduct.PurchaseId).SupplierId,
                    CategoryId = _productManager.GetById(purchasedProduct.ProductId).CategoryId,
                    ProductId = purchasedProduct.ProductId,
                    ExpireDate = purchasedProduct.ExpireDate,
                    ManufactureDate = purchasedProduct.ManufactureDate,
                    Mrp = purchasedProduct.Mrp,
                    Quantity = purchasedProduct.Quantity,
                    Remarks = purchasedProduct.Remarks,
                    TotalPrice = purchasedProduct.Quantity * purchasedProduct.UnitPrice
                };


                purchaseModelView.PurchaseModelViews.Add(purchase);
            }
        }

        public JsonResult GetProductJsonResult(int categoryId)
        {
            List<Product> products = _productManager.GetAll()
                .Where(c => c.CategoryId == categoryId).ToList();

            var productIdValue = products.Select(c => new { c.Id, c.Name });

            return Json(productIdValue, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductInfoJsonResult(int productId)
        {
            PurchaseModelView purchaseModelView = new PurchaseModelView
            {
                ProductCode = _productManager.GetById(productId).Code
            };

            purchaseModelView.PurchasedProducts = _purchasedProductManager.GetAll()
                .Where(c => c.ProductId == productId).ToList();

            if (purchaseModelView.PurchasedProducts.Count != 0)
            {
                var lId = 0;
                foreach (var purchasedProduct in purchaseModelView.PurchasedProducts)
                {
                    if (purchasedProduct.Id > lId)
                    {
                        purchaseModelView.PreviousMrp = purchasedProduct.Mrp;
                        purchaseModelView.PreviousUnitPrice = purchasedProduct.UnitPrice;
                    }
                }
            }

            purchaseModelView.AvailableQuantity = 0;

            var allPurchased = _purchasedProductManager.GetAll().Where(c => c.ProductId == productId).ToList();
            var productCode = _productManager.GetById(productId).Code;
            var allSold = _salesDetailManager.GetAll().Where(c => c.ProductId == productId).ToList();
            foreach (var d in allPurchased)
            {
                purchaseModelView.AvailableQuantity += d.Quantity;
            }
            foreach (var d in allSold)
            {
                purchaseModelView.AvailableQuantity -= d.Quantity;
            }
            return Json(purchaseModelView, JsonRequestBehavior.AllowGet);
        }

        public void AddToPurchaseCart(PurchaseModelView purchaseModelView)
        {

            PurchasedProduct purchasedProduct = Mapper.Map<PurchasedProduct>(purchaseModelView);


            purchaseModelView.PurchasedProducts.Add(purchasedProduct);
        }

        public string TestF()
        {
            return "abc";
        }

        [HttpGet]
        public ActionResult Search()
        {
            PurchaseModelView purchaseModelView = new PurchaseModelView();

            FillComboBox(purchaseModelView);
            FillPurchaseModelViews(purchaseModelView);
            return View(purchaseModelView);
        }

        [HttpPost]
        public ActionResult Search(PurchaseModelView purchaseModelView)
        {

            FillPurchaseModelViews(purchaseModelView);
            FillComboBox(purchaseModelView);
            if (!string.IsNullOrEmpty(purchaseModelView.BillNo))
            {
                purchaseModelView.PurchaseModelViews = purchaseModelView.PurchaseModelViews.Where(p =>
                    p.Date.ToLower().Contains(purchaseModelView.BillNo.ToLower()) ||
                    p.BillNo.ToLower().Contains(purchaseModelView.BillNo.ToLower()) || p.ExpireDate.ToLower().Contains(purchaseModelView.BillNo.ToLower()) || p.ManufactureDate.ToLower().Contains(purchaseModelView.BillNo.ToLower()) ||
                    p.Mrp.ToString().ToLower().Contains(purchaseModelView.BillNo.ToLower()) || p.Quantity.ToString().ToLower().Contains(purchaseModelView.BillNo.ToLower()) || p.Remarks.ToLower().Contains(purchaseModelView.BillNo.ToLower()) || p.TotalPrice.ToString().ToLower().Contains(purchaseModelView.BillNo.ToLower()) ||
                    (_supplierManager.GetById(p.SupplierId).Name).ToLower().Contains(purchaseModelView.BillNo.ToLower())
                    ||
                    (_productManager.GetById(p.ProductId).Name).ToLower().Contains(purchaseModelView.BillNo.ToLower())
                    ||
                    (_categoryManager.GetById(p.CategoryId).Name).ToLower().Contains(purchaseModelView.BillNo.ToLower())).ToList();

            }

            return View(purchaseModelView);
        }

        public bool BillNoUniqueCheck(string billNo)
        {
            if (_purchaseManager.GetAll().Where(c => c.BillNo == billNo).ToList().Any()) return false;
            return true;
        }

    }
}