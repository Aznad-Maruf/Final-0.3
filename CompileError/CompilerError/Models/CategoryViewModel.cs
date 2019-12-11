using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace CompilerError.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "can not empty")]
        [MaxLength(4, ErrorMessage = "lenght is 4")]
        [MinLength(4, ErrorMessage = "lenght is 4")]

        public string Code { get; set; }

        [Required(ErrorMessage = "can not empty")]
        [MaxLength(50, ErrorMessage = "lenght is 4")]
        public string Name { get; set; }
        public List<Category> Categories { set; get; }
    }
}