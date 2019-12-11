using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.DatabaseContext.DatabaseContext;



namespace CompileError.Repository.Repository
{
    public class CustomerRepository
    {
        ProjectDbContext _projectDbContext =new ProjectDbContext();
        public bool Add(Customer customer)
        {
            _projectDbContext.Customers.Add(customer);

            return _projectDbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            Customer acustomer= _projectDbContext.Customers.FirstOrDefault(c => c.Id == id);
            _projectDbContext.Customers.Remove(acustomer);

            return _projectDbContext.SaveChanges() > 0;
        }
        public bool Update(Customer customer)
        {
            Customer acustomer = _projectDbContext.Customers.FirstOrDefault(c => c.Id ==customer.Id);
            if(acustomer !=null)
            {
                acustomer.Code = customer.Code;
                acustomer.Name = customer.Name;
                acustomer.Address = customer.Address;
                acustomer.Email = customer.Email;
                acustomer.Contact = customer.Contact;
                acustomer.LoyalityPoint = customer.LoyalityPoint;

            }


            return _projectDbContext.SaveChanges() > 0;
        }
        public List<Customer>GetAll()
        {

            return _projectDbContext.Customers.ToList();


        }
        public Customer Search(int id)
        {
            return _projectDbContext.Customers.FirstOrDefault(c => c.Id == id);
        }


    }
}
