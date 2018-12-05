using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_LocalDb;
using Infrastructure.Base;
//using Orders.Events;
using Prism.Events;
using Orders.CommonTypes;
using Prism.Commands;
using Prism.Regions;
using System.Data.Entity;

namespace Orders.ViewModels
{
    public class InvoiceViewModel : INavigationAware//, IRegionMemberLifetime
    {
        LocalDbContext _context;
        IEventAggregator _eventAggregator;

        public InvoiceViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            _context = context;
            _eventAggregator = eventAggregator;
        }

        public DbSet<Order_Details> ProductList { get; set;}
        public DbSet<Order> OrderList { get; set; }


        #region INavigationAware implementation
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var Orders = _context.Orders;
            var OrderDetails = _context.Order_Details;

            var Customers = _context.Customers;
            var Employees = _context.Employees;

            OrderList = Orders;
            ProductList = OrderDetails;
        }
        #endregion

        //#region Screen objects

        //public class OrderObject
        //{
        //    public int OrderId { get; set; }
        //    public DateTime OrderDate { get; set; }
        //    public string CustomerName { get; set; }
        //    public string EmployeeName { get; set; }
        //}
        //public class OrdersDetailsObject
        //{
        //    public int OrderId { get; set; }
        //    public string ProductName { get; set; }
        //    public short Quantity { get; set; }
        //    public float Discount { get; set; }
        //    public decimal UnitPrice { get; set; }
        //}
        //#endregion
    }
}
