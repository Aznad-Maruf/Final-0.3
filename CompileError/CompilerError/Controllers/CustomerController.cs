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
    public class CustomerController : Controller
    {
        CustomerManager _customerManager = new CustomerManager();
      
        [HttpGet]
        public ActionResult Add()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
        }
        [HttpPost]
        public ActionResult Add(CustomerViewModel customerViewModel)
        {
           
            string message = "";
           if (ModelState.IsValid)
           {

                Customer customer = Mapper.Map < Customer > (customerViewModel);
              
                if (_customerManager.Add(customer))
                {
                    message = "save";

                }
                else
                {
                    message = "not saved";
                }
                return RedirectToAction("Search");

            }

            customerViewModel.Customers = _customerManager.GetAll();
            ViewBag.Message = message;
            return View(customerViewModel);
            
        }

        [HttpGet]
        public ActionResult Search()
        {
            CustomerViewModel customerViewModel=new CustomerViewModel();
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
           // return View();
        }
        [HttpPost]
        public ActionResult Search(string option, string search)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            List<Customer> customers = _customerManager.GetAll();

            if (option == "Name")
            {
                if (!string.IsNullOrEmpty(search))

                    customers = customers
                    .Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();
                

            }
            if (option == "Code")
            {
                if (!string.IsNullOrEmpty(search))

                    customers = customers
                    .Where(c => c.Code.ToLower().Contains(search.ToLower())).ToList();
                
            }
            if (option == "Contact")
            {
                if (!string.IsNullOrEmpty(search))

                    customers = customers
                    .Where(c => c.Contact.ToLower().Contains(search.ToLower())).ToList();
                
            }
            if (option == "Email")
            {
                if (!string.IsNullOrEmpty(search))

                    customers = customers
                    .Where(c => c.Email.ToLower().Contains(search.ToLower())).ToList();
               
            }

            customerViewModel.Customers = customers;

            return View(customerViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
           _customerManager.Delete(id);
            return RedirectToAction("Add");

        }
        [HttpGet]
        public ActionResult Update(int id)
        {

            CustomerViewModel customerViewModel = Mapper.Map<CustomerViewModel>(_customerManager.Search(id));
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);

        }
        [HttpPost]
        public ActionResult Update(CustomerViewModel customerViewModel)
        {
            Customer customer = Mapper.Map<Customer>(customerViewModel);
            if (ModelState.IsValid)
            {
                _customerManager.Update(customer);
            }
            customerViewModel.Customers = _customerManager.GetAll();
          //  return View(customerViewModel);
            return RedirectToAction("Search");

        }
        public JsonResult GetCodeUnique(string CustomerCode)
        {
            bool isHas = false;
           
            var customercode = _customerManager.GetAll().Where(c => c.Code == CustomerCode);
            if(customercode.Count()>0)
            {
                isHas=true;
            }

            return Json(isHas, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetContactUnique(string CustomerContact)
        {

            bool isHas = false;

            var customercontact = _customerManager.GetAll().Where(c => c.Contact == CustomerContact);
            if (customercontact.Count() > 0)
            {
                isHas = true;
            }

            return Json(isHas, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmailUnique(string CustomerEmail)
        {

            bool isHas = false;

            var customeremail = _customerManager.GetAll().Where(c => c.Email == CustomerEmail);
            if (customeremail.Count() > 0)
            {
                isHas = true;
            }

            return Json(isHas, JsonRequestBehavior.AllowGet);
        }

    }
}