using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_LocalDb;
using System.Collections.ObjectModel;
using Prism.Regions;
using Prism.Events;
using Orders.Events;

namespace Orders.ViewModels
{
    public class JournalViewModel : ViewModelBase//, INavigationAware, IRegionMemberLifetime
    {
        LocalDbContext _context;
        IEventAggregator _eventAggregator;

        //readonly ObservableCollection<Order> orders;

        public JournalViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            Title = "ORDERS JOURNAL";
            _context = context;
            _eventAggregator = eventAggregator;

            Orders = new ObservableCollection<Order>(context.Orders);

        }
        public ObservableCollection<Order> Orders
        {
            get; set;
        }

        private Order selectedOrder;
        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                SetProperty(ref selectedOrder, value);
                if (selectedOrder !=null)
                {
                _eventAggregator.GetEvent<OnOrderRequest>().Publish(selectedOrder.OrderID);

                }
            }
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
