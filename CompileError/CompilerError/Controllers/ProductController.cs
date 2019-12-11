using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CompileError.Model.Model;
using CompileError.Manager.Manager;
using CompileError.Models;
using CompilerError.Models;

namespace CompileError.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager = new ProductManager();
        private readonly CategoryManager _categoryManager = new CategoryManager();

        [HttpGet]
        public ActionResult AddProduct()
        {
            ProductModelView productModelView = new ProductModelView()
            {
                Products = _productManager.GetAll(),
                CategorySelectListItems = _categoryManager.GetAll()
                    .Select(c=>new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList()
            };

            

            return View(productModelView);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModelView productModelView)
        {
            Product product = Mapper.Map<Product>(productModelView);

            if(ModelState.IsValid)
            {
                ViewBag.Message = _productManager.Add(product) ? "Saved" : "Not Saved";
            }
            else
            {
                ViewBag.Message = "Model State Error";
            }

            productModelView.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            productModelView.Products = _productManager.GetAll();
            return View(productModelView);
        }

        [HttpGet]
        public ActionResult SearchProduct()
        {
            ProductModelView productModelView = new ProductModelView()
            {
                Products = _productManager.GetAll(),
                CategorySelectListItems = _categoryManager.GetAll()
                    .Select(c=>new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                            
                    }).ToList()
            };

            return View(productModelView);
        }

        [HttpPost]
        public ActionResult SearchProduct(ProductModelView productModelView)
        {


            var products = _productManager.GetAll();

            if (!string.IsNullOrEmpty(productModelView.Code))
            {
                products = products.Where(p =>
                    p.Code.ToLower().Contains(productModelView.Code.ToLower())||
                p.Name.ToLower().Contains(productModelView.Code.ToLower())|| p.Description.ToLower().Contains(productModelView.Code.ToLower())|| p.ReorderLevel.ToString().Contains(productModelView.Code.ToString())||(_categoryManager.GetById(p.CategoryId).Name).ToLower().Contains(productModelView.Code.ToLower())).ToList();
                //products = products.Where(p =>
                //    p.Name.ToLower().Contains(productModelView.Code.ToLower())).ToList();
                //products = products.Where(p => p.CategoryId.ToString().Contains(productModelView.Code)).ToList();
                //products = products.Where(p =>
                //    p.Description.ToLower().Contains(productModelView.Code.ToLower())).ToList();
                //products = products.Where(p =>
                //    p.ReorderLevel.ToString().Contains(productModelView.Code.ToString())).ToList();
            }

            productModelView.Products = products;
            productModelView.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            return View(productModelView);
        }

        [HttpGet]
        public ActionResult EditProduct(int Id)
        {
            Product product = _productManager.GetById(Id);

            ProductModelView productModelView =  Mapper.Map<ProductModelView>(product);

            productModelView.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            productModelView.Products = _productManager.GetAll();

            return View(productModelView);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductModelView productModelView)
        {
            Product product = Mapper.Map<Product>(productModelView);

            if (ModelState.IsValid)
            {
                _productManager.Update(product);
            }

            productModelView.Products = _productManager.GetAll();
            productModelView.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(productModelView);
        }

        public bool CodeUniqueCheck(string code)
        {
            if (_productManager.GetAll().Where(c => c.Code == code).ToList().Any()) return false;
            return true;
        }

        public bool NameUniqueCheck(string name)
        {
            if (_productManager.GetAll().Where(c => c.Name == name).ToList().Any()) return false;
            return true;
        }

        public bool CodeEditUniqueCheck(string code, int id)
        {
            var products = _productManager.GetAll().Where(c => c.Code == code).ToList();
            if (products.Any())
            {
                if (products[0].Id != id) return false;
                return true;
            }
            else return true;
        }

        public bool NameEditUniqueCheck(string name, int id)
        {
            if (_productManager.GetAll().Where(c => c.Name == name).ToList().Any()) return false;
            return true;
        }

        public int GetId(string code)
        {
            return _productManager.GetAll().FirstOrDefault(c => c.Code == code).Id;
        }


    }
}