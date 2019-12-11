using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;
using AutoMapper;
using System.ComponentModel.DataAnnotations;



namespace CompilerError.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="can not empty")]
        [MaxLength(4, ErrorMessage ="lenght is 4")]
        [MinLength(4, ErrorMessage ="lenght is 4")]

        public string Code { get; set; }

        [Required(ErrorMessage = "can not empty")]
        [MaxLength(50, ErrorMessage = "lenght is 4")]
        public string Name { get; set; }
        [Required(ErrorMessage = "can not empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "can not empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "can not empty")]
        [MaxLength(11, ErrorMessage = "lenght is 11")]
        public string Contact { get; set; }
        [Display(Name ="Loyality Point")]
        public float LoyalityPoint { get; set; }
        public List<Customer> Customers { get; set; }
    }
}