using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Base;
using Prism.Commands;
using Prism.Regions;
using DAL_LocalDb;
using System.Data.Entity;

namespace Dashboard.CustomerStatistics
{
    public class CustomerStatsViewModel : ViewModelBase
    {
        LocalDbContext _context;
        readonly List<Customer> customers;
        readonly List<Order> orders;
        readonly List<Order_Details> orderDetails;


        public CustomerStatsViewModel(LocalDbContext context)
        {
            Title = "Customers";
            _context = context;

            _context.Customers.Load();
            customers = _context.Customers.ToList<Customer>();

            _context.Orders.Load();
            orders = _context.Orders.Local.ToList<Order>();

            _context.Order_Details.Load();
            orderDetails = _context.Order_Details.ToList<Order_Details>();

            // SELECT top(10)
            // COUNT(CustomerID), Country
            // FROM[NORTHWIND].[dbo].[Customers]
            // GROUP BY Country ORDER BY COUNT(CustomerID) DESC

            //projection on CustomerByCountryObject
            CustomerByCountryGroups =
                new List<CustomerByCountryObject>(customers.GroupBy(c => c.Country).
                Select(g => new CustomerByCountryObject { Country = g.Key, Quantity = g.Count() }));

            //SELECT TOP 10
            //Customers.CompanyName, SUM([Order Details].UnitPrice * [Order Details].Quantity) AS Purchaces
            //FROM
            //[Orders] INNER JOIN[Order Details] ON[Order Details].OrderID = Orders.OrderID
            //INNER JOIN Customers ON Customers.CustomerID = Orders.CustomerID
            //GROUP BY Customers.CompanyName
            //order by Purchaces DESC

            var CustomersAndOrders =
                orders.Join(orderDetails,
                or => or.OrderID,
                det => det.OrderID,
                (or, det) => new { or.CustomerID, det.UnitPrice, det.Quantity })
                .Join(customers,
                o => o.CustomerID,
                cust => cust.CustomerID,
                (or, cust) => new { name = cust.CompanyName, price = or.UnitPrice * or.Quantity });

            //projection on PurchasingByCustomerGroups
            this.PurchasingByCustomerGroups =
                new List<PurchasingByCustomerObject>(CustomersAndOrders.GroupBy(c => c.name)
                .Select(g=> new PurchasingByCustomerObject {Name=g.Key, SumPurchasing=g.Sum(c=>c.price) })
                .OrderByDescending(p=>p.SumPurchasing)
                .Take(10));

        }
        public class CustomerByCountryObject
        {
            public int Quantity { get; set; }
            public string Country { get; set; }
        }
        public List<CustomerByCountryObject> CustomerByCountryGroups { get; set; }

        public class PurchasingByCustomerObject
        {
            public string Name { get; set; }
            public decimal SumPurchasing { get; set; }
        }
        public List<PurchasingByCustomerObject> PurchasingByCustomerGroups { get; set; }




    }
}
