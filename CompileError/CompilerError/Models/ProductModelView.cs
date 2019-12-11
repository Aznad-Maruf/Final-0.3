using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompileError.Model.Model;

namespace CompileError.Models
{
    public class ProductModelView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Can't be Empty")]
        [MaxLength(30, ErrorMessage = "Maximum length is 10")]
        [Display(Name = "Product Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        [MaxLength(30, ErrorMessage = "Maximum length is 10")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        public Category Category { get; set; }
        [Display(Name = "Product Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
    }
}