using DAL_LocalDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderRepository : IGenericRepository<Order>
    {
        LocalDbContext db;
        public OrderRepository(LocalDbContext context)
        {
            db = context;
            //var state = context.Database.Connection.State;
        }

        public List<Order> GetAll()
        {
            //using (db)
            //{
                var list = (from b in db.Orders
                            select b).ToList();
                return list;
            //}
        }

        public void Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order entity)
        {
            throw new NotImplementedException();
        }


        public Task<List<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Order GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
