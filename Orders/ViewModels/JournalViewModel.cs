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
using Prism.Commands;
using Infrastructure.Extensions;
using System.Data.Entity;

namespace Orders.ViewModels
{
    public class JournalViewModel : ViewModelBase, INavigationAware//, IRegionMemberLifetime
    {
        #region Fields
        LocalDbContext _context;
        IEventAggregator _eventAggregator;

        readonly IEnumerable<Order> cachedOrders;

        #endregion

        #region Constructor
        public JournalViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            Title = "ORDERS JOURNAL";
            _context = context;
            _eventAggregator = eventAggregator;

            cachedOrders = context.Orders;
            //Orders = new ReadOnlyCollection<Order>(cachedOrders.ToList());

            var propertyChangedObservable = Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>
                              (handler => this.PropertyChanged += handler, handler => this.PropertyChanged -= handler);

            var searchTermObservable = propertyChangedObservable
                                      .Where(p => p.EventArgs.PropertyName == nameof(SearchTerm))
                                      .Select(_ => _searchTerm).Throttle(TimeSpan.FromMilliseconds(50)).Subscribe
                                      (s =>
                                      {
                                          if (string.IsNullOrEmpty(s))
                                              this.Orders = new ReadOnlyCollection<Order>(cachedOrders.ToList());
                                          else
                                              this.Orders =
                                              new ReadOnlyCollection<Order>(cachedOrders
                                              .Where(o => o.OrderID.ToString().SafeSubstring(0, s.Length).ToLower() == s.ToLower()).OrderBy(o => o.CustomerID)
                                              .ToList());
                                      });
            ClearSearchCommand = new DelegateCommand(() => SearchTerm = "");
        }
        #endregion
        #region Properties
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

        #endregion
        #region Commands
        private DelegateCommand _clearSearchCommand;
        public DelegateCommand ClearSearchCommand
        {
            get { return _clearSearchCommand; }
            set { SetProperty(ref _clearSearchCommand, value); }
        }
        #endregion

        #region INavigationAware, IRegionMemberLifetime implementation

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext) => false;

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            Orders = new ReadOnlyCollection<Order>(cachedOrders.ToList());
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
            //throw new NotImplementedException();
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
