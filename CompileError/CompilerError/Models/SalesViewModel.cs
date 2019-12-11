using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CompilerError.Models
{
    public class SalesViewModel
    {

        public List<Customer> Customers { get; set; }
        public List<Category> Categorys { get; set; }
        public List<Product> Products { get; set; }

        public int DepartmentId { get; set; }
        public Customer Customer { get; set; }

        public List<SelectListItem> CustomersSelectListItem { get; set; }
        public List<SelectListItem> CategorySelectListItem { get; set; }



    }
}