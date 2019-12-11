using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace CompilerError.Models
{
    public class SalesReportViewModel
    {
        public List<Sale> Sales { set; get; }
        public List<SalesDetail> SalesDetails { set; get; }
    }
}