using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileError.Model.Model
{
    public class Purchase
    {
        public int Id { get; set; }
        public string BillNo { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } 
        public string Date { get; set; }

        public List<PurchasedProduct> purchasedProducts { get; set; }
    }
}
