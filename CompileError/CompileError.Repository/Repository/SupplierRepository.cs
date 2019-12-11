using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.DatabaseContext.DatabaseContext;

namespace CompileError.Repository.Repository
{
    public class SupplierRepository
    {
        ProjectDbContext _projectDbContext = new ProjectDbContext();

        public bool Add(Supplier supplier)
        {
             _projectDbContext.Suppliers.Add(supplier);
            return _projectDbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Supplier asupplier = _projectDbContext.Suppliers.FirstOrDefault( c => c.Id == id);
            _projectDbContext.Suppliers.Remove(asupplier);
            return _projectDbContext.SaveChanges() > 0; 
        }

        public bool Update(Supplier supplier)
        {
            Supplier asupplier = _projectDbContext.Suppliers.FirstOrDefault(c => c.Id == supplier.Id);
            if(asupplier != null)
            {
                asupplier.Code = supplier.Code;
                asupplier.Name = supplier.Name;
                asupplier.Address = supplier.Address;
                asupplier.Email = supplier.Email;
                asupplier.Contact = supplier.Contact;
                asupplier.ContactPerson = supplier.ContactPerson;
            }
            return _projectDbContext.SaveChanges() > 0;
        }

        public Supplier Search(int id)
        {
            return _projectDbContext.Suppliers.FirstOrDefault(c => c.Id == id);
        }

        public Supplier GetById(int id)
        {
            return _projectDbContext.Suppliers.FirstOrDefault(c => c.Id == id);
        }

        public List<Supplier> GetAll()
        {
            return _projectDbContext.Suppliers.ToList();
        }



    }
}
