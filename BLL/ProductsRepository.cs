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
        LocalDbContext db;
        public ProductRepository(LocalDbContext context)
        {
            db = context;
        }
        public List<Products> GetAll()
        {
            using (db)
            {
                var list = (from b in db.Products
                            orderby b.ProductName
                            select b).ToList();
                return list;
            }
        }

        public async Task<List<Products>> GetALlAsync()
        {
            using (db)
            {
                return await (from b in db.Products
                              orderby b.ProductName
                              select b).ToListAsync();
            }
            //return list as List<Products>;
        }

        public void Add(Products entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Products entity)
        {
            throw new NotImplementedException();
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

