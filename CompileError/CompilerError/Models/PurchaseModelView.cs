using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CompileError.Models
{
    public class PurchaseModelView
    {
        public PurchaseModelView()
        {
            PurchasedProducts = new List<PurchasedProduct>();
            PurchaseModelViews = new List<PurchaseModelView>();
            CategorySelectListItems = new List<SelectListItem>();
            ProductSelectListItems = new List<SelectListItem>();
            SupplierSelectListItems = new List<SelectListItem>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Can't be empty"),MinLength(3,ErrorMessage = "Minimum length is 3"),MaxLength(20,ErrorMessage = "Maximum length is 20")]
        public string BillNo { get; set; }

        public Supplier Supplier { get; set; }
        [Required(ErrorMessage = "Select a supplier")]
        public int SupplierId { get; set; }
        public List<SelectListItem> SupplierSelectListItems { get; set; }

        [Required(ErrorMessage = "Select a Date")]
        public string Date { get; set; }

        public PurchasedProduct PurchasedProduct;
        public List<PurchasedProduct> PurchasedProducts { get; set; }

        /*...Purchased Products....*/

        public int PurchaseId { get; set; }

        [Required(ErrorMessage = "Select a category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }

        [Required(ErrorMessage = "Select a product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }

        public string ProductCode { get; set; }

        public double AvailableQuantity { get; set; }

        [Required(ErrorMessage = "Select a Date")]
        public string ManufactureDate { get; set; }

        [Required(ErrorMessage = "Select a Date")]
        public string ExpireDate { get; set; }

        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public double PreviousUnitPrice { get; set; }
        public double PreviousMrp { get; set; }
        public double Mrp { get; set; }
        public string Remarks { get; set; }

        public List<PurchaseModelView> PurchaseModelViews { get; set; }

    }
}