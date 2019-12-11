using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileError.Model.Model
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        List<Product> Products { set; get; }

    }
}
