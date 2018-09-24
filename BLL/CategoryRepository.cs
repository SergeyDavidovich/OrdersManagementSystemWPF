using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL_LocalDb;
namespace BLL
{
    public class CategoryRepository:IGenericRepository<Categories>
    {
        LocalDbContext _context;
        public CategoryRepository(LocalDbContext context)
        {
            _context = context;
        }

        public void Add(Categories entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Categories entity)
        {
            throw new NotImplementedException();
        }

        public List<Categories> GetAll()
        {
            return new List<Categories>(_context.Categories);
        }

        public Categories GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
