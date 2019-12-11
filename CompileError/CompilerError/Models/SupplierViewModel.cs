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

        [Required(ErrorMessage =  "Code Can't be Empty")]
        [MaxLength(4, ErrorMessage = "lenght is 4")]
        [MinLength(4, ErrorMessage = "lenght is 4")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Can't be Empty")]      
        public string Name { get; set; }

        [Required(ErrorMessage = "Address Can't be Empty")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Email Can't be Empty ")]
        [RegularExpression (@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Contact Can't be Empty")]
        [MaxLength(11, ErrorMessage = "lenght is 11")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Contact Person Can't be Empty")]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        public List<Supplier> Suppliers { get; set; }
        public List<SelectListItem> SupplierSelectedItems { get; set; }



    }
}