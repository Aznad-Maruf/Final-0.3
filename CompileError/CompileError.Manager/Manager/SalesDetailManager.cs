using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.Repository.Repository;

namespace CompileError.Manager.Manager
{
    public class SalesDetailManager
    {
        SalesDetailRepository _salesDetailRepository = new SalesDetailRepository();
  
        public bool Add(SalesDetail salesDetail)
        {
            return _salesDetailRepository.Add(salesDetail);
        }
        public List<SalesDetail> GetAll()
        {
            return _salesDetailRepository.GetAll();

        }
    }
}
