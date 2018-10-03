using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        Task<List<T>> GetALlAsync();

        T GetByID(string id);

        void Add(T entity);

        void Delete(T entity);

        int SaveChanges();
    }
}
