using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileError.Model.Model
{
    public class SalesDetail
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
   
        public int Quantity { set; get; }
        public double MRP { set; get; }
        public double TotalMRP { set; get; }

        public int SaleId { set; get; }
        public virtual Sale Sale { get; set; }

    }
}
