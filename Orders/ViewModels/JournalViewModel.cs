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
using System.Reactive.Linq;
using System.ComponentModel;

namespace Orders.ViewModels
{
    public class JournalViewModel : ViewModelBase//, INavigationAware, IRegionMemberLifetime
    {
        LocalDbContext _context;
        IEventAggregator _eventAggregator;

        readonly IEnumerable<Order> cachedOrders;

        public JournalViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            Title = "ORDERS JOURNAL";
            _context = context;
            _eventAggregator = eventAggregator;

            cachedOrders = context.Orders;
            Orders = new ReadOnlyCollection<Order>(cachedOrders.ToList());
            var propertyChangedObservable = Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>
                              (handler => this.PropertyChanged += handler, handler => this.PropertyChanged -= handler);
            var searchTermObservable = propertyChangedObservable
                                      .Where(p => p.EventArgs.PropertyName == nameof(SearchTerm))
                                      .Select(_ => _searchTerm).Throttle(TimeSpan.FromMilliseconds(250)).Subscribe
                                      (s =>
                                      {
                                          if (string.IsNullOrEmpty(s))
                                              this.Orders = new ReadOnlyCollection<Order>(cachedOrders.ToList());
                                          else
                                              this.Orders = new ReadOnlyCollection<Order>(cachedOrders.Where(o => o.CustomerID.Contains(s)).ToList());
                                      }

                                      );

        }
        private ReadOnlyCollection<Order> _orders;
        public ReadOnlyCollection<Order> Orders
        {
            get { return _orders; }
            set { SetProperty(ref _orders, value); }
        }

        private Order selectedOrder;
        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                SetProperty(ref selectedOrder, value);
                if (selectedOrder != null)
                {
                    _eventAggregator.GetEvent<NewOrderCreated>().Publish(selectedOrder.OrderID);

                }
            }
        }

        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value); }
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
