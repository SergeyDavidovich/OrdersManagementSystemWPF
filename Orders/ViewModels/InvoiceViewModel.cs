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

namespace Orders.ViewModels
{
    public class InvoiceViewModel : ViewModelBase, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations

        LocalDbContext _context;
        IEventAggregator _eventAggregator;
        Order order;

        #endregion

        public InvoiceViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            _context = context;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OnOrderCreate>().Subscribe(OnOrderCreateHandle);

            SaveCommand = new DelegateCommand(Save, CanSave);
            order = new Order();
        }

        #region Commands
        public DelegateCommand SaveCommand { get; set; }
        private void Save()
        {
            using (var contextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    _context.Order_Details.AddRange(
                        new List<Order_Details>(
                            ProductList.Select(p => new Order_Details
                            {
                                OrderID = order.OrderID,
                                ProductID = p.ID,
                                UnitPrice = p.UnitPrice,
                                Quantity=p.Quantity,
                                Discount=p.Discount
                            })));
                    _context.SaveChanges();

                    contextTransaction.Commit();

                    OrderID = order.OrderID;
                }
                catch (Exception)
                {
                    contextTransaction.Rollback();
                }
            }
        }
        private bool CanSave() { return true; }
        #endregion

        #region Bindable properties

        private List<ProductInOrder> _ProductList;
        public List<ProductInOrder> ProductList
        {
            get => _ProductList;
            set => SetProperty(ref _ProductList, value);
        }

        int orderID;
        public int OrderID
        {
            get => orderID;
            set
            {
                SetProperty(ref orderID, value);
            }
        }

        private DateTime orderDate;
        public DateTime OrderDate
        {
            get => orderDate;
            set
            {
                SetProperty(ref orderDate, value);
                order.OrderDate = orderDate;
            }
        }

        string customerID;
        public string CustomerID
        {
            get => customerID;
            set
            {
                SetProperty(ref customerID, value);
                order.CustomerID = customerID;
            }
        }

        bool IRegionMemberLifetime.KeepAlive => true;

        #endregion

        #region Events handlers

        private void OnOrderCreateHandle(List<ProductInOrder> list)
        {
            ProductList = list;
            OrderDate = DateTime.Now;
        }


        #endregion

        #region Navigatable Events

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Utilites

        #endregion
    }
}
