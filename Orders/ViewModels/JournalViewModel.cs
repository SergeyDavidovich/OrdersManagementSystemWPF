using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_LocalDb;
using System.Collections.ObjectModel;
using Prism.Regions;

namespace Orders.ViewModels
{
    public class JournalViewModel : ViewModelBase//, INavigationAware, IRegionMemberLifetime
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


        #region INavigationAware, IRegionMemberLifetime implementation

        //public bool KeepAlive => false;

        //public bool IsNavigationTarget(NavigationContext navigationContext) => false;

        //public void OnNavigatedFrom(NavigationContext navigationContext)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
