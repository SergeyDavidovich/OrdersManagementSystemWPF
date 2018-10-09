using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism;
using DAL_LocalDb;
using BLL;


namespace Dashboard.OrderStatistics
{
    public class OrdersStatViewModel : ViewModelBase
    {
        List<Order> orders;
        List<Customer> customers;
        public OrdersStatViewModel(IGenericRepository<Order> orderRepo, IGenericRepository<Customer> customerRepo)
        {
            base.Title = "Orders";

            orders = orderRepo.GetAll();
            customers = customerRepo.GetAll();

            //var OrdersAndCountriess =
            //    orders.Join(customers,
            //    order => order.CustomerID,
            //    cust => cust.CustomerID,
            //   (order, cust) => new { Id = order.OrderID, Country = cust.Country });

            //this.OrderByCountryGroups = new List<OrderGroupsObject>(OrdersAndCountriess.GroupBy(c => c.Country).
            //    Select(g => new OrderGroupsObject { Country = g.Key, Quantity = g.Count() }));


            this.OrderByCountryGroups = new List<OrderGroupsObject>(
               orders.Join(customers,
               order => order.CustomerID,
               cust => cust.CustomerID,
              (order, cust) => new { Id = order.OrderID, Country = cust.Country }).GroupBy(c => c.Country).
              Select(g => new OrderGroupsObject { Country = g.Key, Quantity = g.Count() }).OrderByDescending(g=>g.Quantity).Take(10));


            //this.OrderGroupsMock = new List<OrderGroupsObject>()
            //{
            //    new OrderGroupsObject(){Country="USA", Quantity=10},
            //    new OrderGroupsObject(){Country="GB", Quantity=20},
            //    new OrderGroupsObject(){Country="Germany", Quantity=30},
            //    new OrderGroupsObject(){Country="Poland", Quantity=15},
            //    new OrderGroupsObject(){Country="Czech Republic", Quantity=25}
            //};
        }
        public class OrderGroupsObject
        {
            public string Country { get; set; }
            public int Quantity { get; set; }
        }
        public List<OrderGroupsObject> OrderGroupsMock { get; set; }

        public List<OrderGroupsObject> OrderByCountryGroups { get; set; }
    }
}
