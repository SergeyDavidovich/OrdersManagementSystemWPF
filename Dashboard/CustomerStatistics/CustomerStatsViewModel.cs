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

        public CustomerStatsViewModel(LocalDbContext context)
        {
            Title = "Customers";
            _context = context;

            _context.Customers.Load();
            customers = _context.Customers.ToList<Customer>();


            // SELECT top(10)
            // COUNT(CustomerID), Country
            // FROM[NORTHWIND].[dbo].[Customers]
            // GROUP BY Country ORDER BY COUNT(CustomerID) DESC

            //projection on CustomerByCountryObject
            CustomerByCountryGroups = 
                new List<CustomerByCountryObject>(customers.GroupBy(c => c.Country).
                Select(g => new CustomerByCountryObject { Country = g.Key, Quantity = g.Count() }));
        }
        public class CustomerByCountryObject
        {
            public int Quantity { get; set; }
            public string Country { get; set; }
        }
        public List<CustomerByCountryObject> CustomerByCountryGroups { get; set; }




    }
}
