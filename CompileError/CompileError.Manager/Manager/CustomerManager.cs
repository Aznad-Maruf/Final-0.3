using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.Repository.Repository;

namespace CompileError.Manager.Manager
{
    public class CustomerManager
    {
        private readonly CustomerRepository _customerRepository = new CustomerRepository();
        public bool Add(Customer customer)
        {
            return _customerRepository.Add(customer);
        }
        public bool Delete(int id)
        {
            return _customerRepository.Delete(id);
        }
        public bool Update(Customer customer)
        {
            return _customerRepository.Update(customer);
        }
        public List<Customer>GetAll()
        {
            return _customerRepository.GetAll();

        }
        public Customer Search(int id)
        {
            return _customerRepository.Search(id);
        }
        

    }
}
