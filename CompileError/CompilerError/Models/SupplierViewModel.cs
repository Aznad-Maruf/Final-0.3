using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace CompilerError.Models
{
    public class SupplierViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Field Cannot be empty")]//annotations
        [MaxLength(4, ErrorMessage = "Max length is 4")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Code must be numeric")]
        [Display(Name = "Supplier Code:")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Cannot be empty")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [Display(Name = "Supplier Name:")]
        public string Name { get; set; }

        public string Address { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Invalid Email")]
        [Display(Name = "Email :")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact No. Cannot be empty")]//annotations
        [MaxLength(11, ErrorMessage = "Max length is 11")]
        [MinLength(11, ErrorMessage = "Min length is 11")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact must contain numbers only")]
        [Display(Name = "Contact No.:")]
        public string Contact { get; set; }
        [Display(Name = "Contact Person:")]
        public string ContactPerson { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<SelectListItem> SupplierSelectedItems { get; set; }

        //public int Id { get; set; }

        //[Required(ErrorMessage =  "Code Can't be Empty")]
        //[MaxLength(4, ErrorMessage = "lenght is 4")]
        //[MinLength(4, ErrorMessage = "lenght is 4")]
        //public string Code { get; set; }

        //[Required(ErrorMessage = "Name Can't be Empty")]      
        //public string Name { get; set; }

        //[Required(ErrorMessage = "Address Can't be Empty")]
        //public string Address { get; set; }

        //[Required(ErrorMessage ="Email Can't be Empty ")]
        //[RegularExpression (@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage ="Invalid Email")]
        //public string Email { get; set; }

        //[Required(ErrorMessage ="Contact Can't be Empty")]
        //[MaxLength(11, ErrorMessage = "lenght is 11")]
        //public string Contact { get; set; }

        //[Required(ErrorMessage = "Contact Person Can't be Empty")]
        //[Display(Name = "Contact Person")]
        //public string ContactPerson { get; set; }

        //public List<Supplier> Suppliers { get; set; }
        //public List<SelectListItem> SupplierSelectedItems { get; set; }



    }
}