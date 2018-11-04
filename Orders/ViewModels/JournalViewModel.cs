using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_LocalDb;
using System.Collections.ObjectModel;

namespace Orders.ViewModels
{
    public class JournalViewModel : ViewModelBase
    {
        LocalDbContext _context;
        //readonly ObservableCollection<Order> orders;

        public JournalViewModel(LocalDbContext context)
        {
            Title = "ORDERS JOURNAL";
            _context = context;

            Orders = new ObservableCollection<Order>(context.Orders);
        }
        public ObservableCollection<Order> Orders
        {
            get; set;
        }

        public Order SelectedOrder
        {
            get;set;
        }
    }
}
