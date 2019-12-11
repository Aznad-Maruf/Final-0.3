using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileError.Model.Model
{
    public class PurchasedProduct
    {
        public int Id { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ManufactureDate { get; set; }
        public string ExpireDate { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Mrp { get; set; }
        public string Remarks { get; set; }
    }
}
