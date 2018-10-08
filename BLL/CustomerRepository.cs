using DAL_LocalDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CustomerRepository : IGenericRepository<Customer>
    {
        LocalDbContext db;
        public CustomerRepository(LocalDbContext context)
        {
            db = context;
        }
        public List<Customer> GetAll()
        {
            using (db)
            {
                var list = (from b in db.Customers
                            select b).ToList();
                return list;
            }
        }
        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }


        public Task<List<Customer>> GetALlAsync()
        {
            throw new NotImplementedException();
        }

        public Customer GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
