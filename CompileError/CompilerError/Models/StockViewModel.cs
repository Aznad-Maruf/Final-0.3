using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;
using System.Web.Mvc;

namespace CompilerError.Models
{
    public class StockViewModel
    {
        public int Id { set; get; }
        public string ProductCode { get; set; }

        public string ReorderLevel { get; set; }
        public string ExpiredDate { get; set; }
        public string ExpiredQuantity { get; set; }
        public string OpeningBalance { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string In { get; set; }
        public string Out { get; set; }
        public string ClosingBalance { get; set; }

        public int CategoryId { get; set; }
        public  string CategoryName { get; set; }
        public Category Category { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }



        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }

        public List<Purchase> Purchases { get; set; }
        public List<PurchasedProduct> PurchaseProducts { get; set; }
        public List<SelectListItem> PurchaseSelectListItems { get; set; }

        public List<Sale>Sales { get; set; }
        public List<SalesDetail> SalesProducts { get; set; }
        public List<SelectListItem> SalesSelectListItems { get; set; }
        
    }
}