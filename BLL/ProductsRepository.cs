using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL_LocalDb;
namespace BLL
{
    public class ProductsRepository : IGenericRepository<Products>
    {
        LocalDbContext _context;
        public ProductsRepository(LocalDbContext context)
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
            return (new List<Products>(_context.Products));
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
