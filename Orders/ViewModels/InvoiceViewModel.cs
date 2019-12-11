using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_LocalDb;
using Infrastructure.Base;
using Orders.Events;
using Prism.Events;
using Orders.CommonTypes;
using Prism.Commands;
using Prism.Regions;
//using System.Data.Entity;
using System.ComponentModel;
using System.Collections.ObjectModel;


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
            eventAggregator.GetEvent<NewOrderCreated>().Subscribe(OnNewOrderCreated);
        }

        public ObservableCollection<OrderObject> Orders { get; set; }
        public ObservableCollection<OrderDetailObject> OrderDetails { get; set; }

        #region INavigationAware implementation
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext) { }

        #endregion


        private void OnNewOrderCreated(int id)
        {
            var orders = _context.Orders.Where(o => o.OrderID == id).ToList();
            var orderDetails = _context.Order_Details.Where(o => o.OrderID == id).ToList();

            Orders = new ObservableCollection<OrderObject>
                (orders.Select(o => new OrderObject()
                {
                    OrderId = o.OrderID,
                    OrderDate = o.OrderDate.Value,
                    EmployeeName = o.Employees.FirstName + " " + o.Employees.LastName,
                    CustomerName = o.Customers.CompanyName
                }
                ));

            OrderDetails = new ObservableCollection<OrderDetailObject>
                (orderDetails.Select(o => new OrderDetailObject()
                {
                    OrderId = o.OrderID,
                    ProductName = o.Products.ProductName,
                    UnitPrice = o.UnitPrice,
                    Quantity = o.Quantity,
                    Discount = o.Discount,
                    SubTotal = (decimal)((float)o.UnitPrice * o.Quantity * (1 - o.Discount))
                }
                ));
            _eventAggregator.GetEvent<OrderDataCreated>().Publish();
        }

        #region Screen objects

        public class OrderObject
        {
            public int OrderId { get; set; }
            public string CustomerName { get; set; }
            public string EmployeeName { get; set; }
            public DateTime OrderDate { get; set; }
        }
        public class OrderDetailObject
        {
            public int OrderId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public short Quantity { get; set; }
            public float Discount { get; set; }
            public decimal SubTotal { get; set; }
        }
        #endregion
    }
}
