using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_LocalDb;
using Infrastructure.Base;
using Prism.Events;
using Prism.Regions;

namespace Customers.List
{
    public class CustomerListViewModel : NavigationAwareViewModelBase
    {
        IEventAggregator _eventAggregator;
        LocalDbContext _context;
        IEnumerable<Customer> customers;

        public CustomerListViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            Title = "CUSTOMERS VIEW (- Under construction -)";
            _context = context;
            _eventAggregator = eventAggregator;

        }
        #region Bindable properties

        ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set => SetProperty(ref _customers, value);
        }
        Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                SetProperty(ref _selectedCustomer, value);
            }
        }
        #endregion

        #region Navigation events

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Customers = new ObservableCollection<Customer>(_context.Customers);
        }
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return base.IsNavigationTarget(navigationContext);
        }
        #endregion
    }
}
