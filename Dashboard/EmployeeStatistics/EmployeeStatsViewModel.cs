using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using DAL_LocalDb;
using System.Data.Entity;

namespace Dashboard.EmployeeStatistics
{
    public class EmployeeStatsViewModel : ViewModelBase
    {
        LocalDbContext _context;

        readonly List<Order> orders;
        readonly List<Order_Details> orderDetails;
        readonly List<Employee> employees;

        public EmployeeStatsViewModel(LocalDbContext context)
        {
            Title = "Employees";
            _context = context;

            //get data for all queries
            _context.Orders.Load();
            orders = _context.Orders.Local.ToList<Order>();

            _context.Order_Details.Load();
            orderDetails = _context.Order_Details.ToList<Order_Details>();

            _context.Employees.Load();
            employees = _context.Employees.Local.ToList<Employee>();

            //query for SalesByEmployeeGroups
            var EmployeesAndOrders =
                employees.Join(orders,
                emp => emp.EmployeeID,
                ord => ord.EmployeeID,
               (emp, ord) => new { LastName = emp.LastName, OrderId = ord.OrderID }).
               Join(orderDetails,
               emp => emp.OrderId,
               det => det.OrderID,
               (emp, det) => new { name = emp.LastName, sale = det.UnitPrice * det.Quantity });

            //projection on SalesByEmployeeObject
            this.SalesByEmployeeGroups =
            new List<SalesByEmployeeObject>(EmployeesAndOrders.GroupBy(c => c.name).
            Select(g => new SalesByEmployeeObject { LastName = g.Key, SumOfSale = g.Sum(c => c.sale) }).OrderBy(g => g.SumOfSale));
        }

        public class SalesByEmployeeObject
        {
            public string LastName { get; set; }
            public decimal SumOfSale { get; set; }
        }
        private List<SalesByEmployeeObject> _salesByEmployeeGroups;
        public List<SalesByEmployeeObject> SalesByEmployeeGroups
        {
            get => _salesByEmployeeGroups;
            set => SetProperty(ref _salesByEmployeeGroups, value);
        }
    }
}
