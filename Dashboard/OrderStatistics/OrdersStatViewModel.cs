using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism;
using DAL_LocalDb;
using BLL;
using Prism.Regions;
using System.Data.Entity;

namespace Dashboard.OrderStatistics
{
    public class OrdersStatViewModel : NavigationAwareViewModelBase
    {
        LocalDbContext _context;

        readonly List<Order> orders;
        readonly List<Customer> customers;
        readonly List<Order_Details> orderDetails;
        readonly List<Category> categories;
        readonly List<Product> products;

        public OrdersStatViewModel(LocalDbContext context)
        {
            base.Title = "Orders";
            _context = context;

            //get data for all queries
            _context.Orders.Load();
            orders = _context.Orders.Local.ToList<Order>();

            _context.Customers.Load();
            customers = _context.Customers.ToList<Customer>();

            _context.Order_Details.Load();
            orderDetails = _context.Order_Details.ToList<Order_Details>();

            _context.Categories.Load();
            categories = _context.Categories.ToList<Category>();

            _context.Products.Load();
            products = _context.Products.ToList<Product>();

            //query for OrderByCountryGroups
            var OrdersAndCountries =
                orders.Join(customers,
                order => order.CustomerID,
                cust => cust.CustomerID,
               (order, cust) => new { order.OrderID, cust.Country });

            //projection on OrderByCountryObject
            this.OrderByCountryGroups =
                new List<OrderByCountryObject>(OrdersAndCountries.GroupBy(c => c.Country).
                Select(g => new OrderByCountryObject { Country = g.Key, Quantity = g.Count() }).
                OrderByDescending(g => g.Quantity).
                Take(10));

            //query for OrderByCategoryGroups
            var OrdersAndCategories =
                categories.Join(products,
                cat => cat.CategoryID,
                pro => pro.CategoryID,
                (cat, pro) => new { cat.CategoryName, pro.ProductID }).
                Join(orderDetails,
                pro => pro.ProductID,
                det => det.ProductID,
                (pro, det) => new { name = pro.CategoryName, price = det.UnitPrice *det.Quantity });

            //projection on OrderByCategoryObject
            this.OrderByCategoryGroups =
            new List<OrderByCategoryObject>(OrdersAndCategories.GroupBy(c => c.name).
            Select(g => new OrderByCategoryObject { CategoryName = g.Key, SumOfSale = g.Sum(c => c.price) }));

            //query for OrderByCountriesGroups
            var OrdersAndCustomers =
            customers.Join(orders,
            cus => cus.CustomerID,
            ord => ord.CustomerID,
            (cus, ord) => new { cus.Country, ord.OrderID }).
            Join(orderDetails,
            cus => cus.OrderID,
            det => det.OrderID,
            (cus, det) => new { country = cus.Country, price = det.UnitPrice *det.Quantity});

            //projection on SalesByCountryGroups
            this.SalesByCountryGroups =
            new List<SalesByCountryObject>(OrdersAndCustomers.GroupBy(c => c.country).
            Select(g => new SalesByCountryObject { Country = g.Key, SumOfSale = g.Sum(c => c.price) }).
            OrderByDescending(g => g.SumOfSale));
        }

        #region Summaries
        string format = "$ ###,###.##";

        public string OverAllSalesSum { get => orderDetails.Sum(o => o.UnitPrice * o.Quantity).ToString(format); }
        public string OverAllSalesCount { get => orderDetails.GroupBy(o => o.OrderID).Count().ToString(); }



        public string AverageCheck
        {
            get => orderDetails.GroupBy(od => od.OrderID).AsQueryable()
            .Select(g => new { order = g.Key, sum = g.Sum(o => (decimal)o.UnitPrice *o.Quantity) }).Average(a => a.sum).ToString(format);
        }

        public string MaxCheck
        {
            get => orderDetails.GroupBy(od => od.OrderID).AsQueryable()
                .Select(g => new { order = g.Key, sum = g.Sum(o => (decimal)o.UnitPrice * o.Quantity) }).Max(a => a.sum).ToString(format);
        }
        public string MinCheck
        {
            get => orderDetails.GroupBy(od => od.OrderID).AsQueryable()
                .Select(g => new { order = g.Key, sum = g.Sum(o => (decimal)o.UnitPrice * o.Quantity) }).Min(a => a.sum).ToString(format);
        }
        #endregion

        public class OrderByCountryObject
        {
            public string Country { get; set; }
            public int Quantity { get; set; }
        }
        public class OrderByCategoryObject
        {
            public string CategoryName { get; set; }
            public decimal SumOfSale { get; set; }
        }

        public class SalesByCountryObject
        {
            public string Country { get; set; }
            public decimal SumOfSale { get; set; }
        }

        public List<SalesByCountryObject> SalesByCountryGroups { get; set; }

        public List<OrderByCountryObject> OrderByCountryGroups { get; set; }

        public List<OrderByCategoryObject> OrderByCategoryGroups { get; set; }

    }
}
