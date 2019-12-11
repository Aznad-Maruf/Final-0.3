using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;

namespace CompilerError.Models
{
    public class PurchaseReportViewModel
    {
        public List<Purchase> Purchases { set; get; }
        public List<PurchasedProduct> PurchasedProducts { set; get; }
    }
}