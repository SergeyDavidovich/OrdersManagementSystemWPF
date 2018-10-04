using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL_LocalDb;
namespace BLL
{
    public class ProductRepository : IGenericRepository<Product>
    {
        LocalDbContext db;
        public ProductRepository(LocalDbContext context)
        {
            db = context;
        }
        public List<Product> GetAll()
        {
            using (db)
            {
                var list = (from b in db.Products
                            orderby b.ProductName
                            select b).ToList();
                return list;
            }
        }

        public async Task<List<Product>> GetALlAsync()
        {
            using (db)
            {
                return await (from b in db.Products
                              orderby b.ProductName
                              select b).ToListAsync();
            }
            //return list as List<Products>;
        }

        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

