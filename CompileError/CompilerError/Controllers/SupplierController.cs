using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompileError.Model.Model;
using CompilerError.Models;
using CompileError.Manager.Manager;
using AutoMapper;


namespace CompilerError.Controllers
{
    public class SupplierController : Controller
    {
        readonly SupplierManager _supplierManager = new SupplierManager();
        readonly SupplierViewModel supplierViewModel = new SupplierViewModel();

        string message = " ";

        [HttpGet]
        public ActionResult AddSupplier()
        {          
            supplierViewModel.Suppliers = _supplierManager.GetAll();                    
            return View(supplierViewModel);
        }


        [HttpPost]
        public ActionResult AddSupplier(SupplierViewModel supplierViewModel)
        {
           
            if(ModelState.IsValid )
            {
                Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);

                if (_supplierManager.Add(supplier))
                {
                    message = "Saved.";
                }
                else
                {
                    message = "Not Saved.";
                }

            }
            else
            {
                message = "Model State Failed.";
            }

            ViewBag.Message = message;
            supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
            
        }
        [HttpGet]
        public ActionResult SearchSupplier()
        {
            supplierViewModel.Suppliers = _supplierManager.GetAll();

            return View(supplierViewModel);
        }

        [HttpPost]
        public ActionResult SearchSupplier(string searchBy, string search)
        {
            List<Supplier> suppliers = _supplierManager.GetAll();

            if (searchBy == "General")
            {
                suppliers = suppliers.Where(p => p.Name.ToUpper().Contains(search.ToUpper()) || p.Code.Contains(search) || p.ContactPerson.ToUpper().Contains(search.ToUpper())
                || p.Contact.ToUpper().Contains(search.ToUpper()) || p.Email.ToUpper().Contains(search.ToUpper()) || p.Address.ToUpper().Contains(search.ToUpper()) || search == null).ToList();
            }

            else if (searchBy == "Name")
            {
                suppliers = suppliers.Where(p => p.Name.ToUpper().StartsWith(search.ToUpper()) || search == null).ToList();
            }

            else if (searchBy == "Code")
            {
                suppliers = suppliers.Where(c => c.Code.Contains(search) || search == null).ToList();
            }


            supplierViewModel.Suppliers = suppliers;
           

            return View(supplierViewModel);
        }

        [HttpGet]
        public ActionResult DeleteSupplier(int id)
        {
            _supplierManager.Delete(id);
            return RedirectToAction("AddSupplier");
        }

        [HttpGet]
        public ActionResult UpdateSupplier(int id)
        {
            SupplierViewModel supplierViewModel = Mapper.Map<SupplierViewModel>(_supplierManager.Search(id));
            supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }

        [HttpPost]
        public ActionResult UpdateSupplier(SupplierViewModel supplierViewModel)
        {
            Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);
            _supplierManager.Update(supplier);
            supplierViewModel.Suppliers = _supplierManager.GetAll();

           //return View(supplierViewModel);
            return RedirectToAction("AddSupplier");
        }

    }
}