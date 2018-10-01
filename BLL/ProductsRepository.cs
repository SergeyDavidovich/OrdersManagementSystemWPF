using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL_LocalDb;
namespace BLL
{
    public class ProductRepository : IGenericRepository<Products>
    {
        LocalDbContext _context;
        public ProductRepository(LocalDbContext context)
        {
            _context = context;
        }

        public void Add(Products entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Products entity)
        {
            throw new NotImplementedException();
        }

        public List<Products> GetAll()
        {
            var result = new List<Products>(_context.Products);
                //this.CustomerGroups = new List<PieObject>(customersList.GroupBy(c => c.Country)
                //.Select(g => new PieObject { Country = g.Key, Quantity = g.Count() }));

            return result;
        }

        public Products GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
