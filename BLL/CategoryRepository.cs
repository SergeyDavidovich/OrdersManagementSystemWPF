using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL_LocalDb;
namespace BLL
{
    public class CategoryRepository:IGenericRepository<Category>
    {
        LocalDbContext _context;
        public CategoryRepository(LocalDbContext context)
        {
            _context = context;
        }

        public void Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            var result =new List<Category>(_context.Categories);
            return result;
        }

        public Task<List<Category>> GetALlAsync()
        {
            throw new NotImplementedException();
        }

        public Category GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
