using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using DAL_LocalDb;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using Orders.Events;
using Orders.CommonTypes;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Orders.ViewModels
{
    public class CreateViewModel : ViewModelBase
    {
        LocalDbContext _context;
        IEventAggregator _eventAggregator;
        Order order;


        public CreateViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            _context = context;
            _eventAggregator = eventAggregator;

            Products = new ObservableCollection<Product>(context.Products.
                Where(p => p.Discontinued == false && p.UnitsInStock > 0));
            SelectCommand = new DelegateCommand(Select, CanSelect);
            UnselectCommand = new DelegateCommand(Unselect, CanUnselect);
            CreateOrderCommand = new DelegateCommand(CreateOrder, CanCreateOrder);

            Customers = context.Customers.ToList<Customer>();
            Employees = context.Employees.ToList<Employee>();
            order = new Order();
        }

        #region Select

        private void OnProductInOrderCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var producInOrder in e.NewItems)
                {
                    (producInOrder as ProductInOrder).PropertyChanged -= ProductInOrder_PropertyChanged;
                }
            }
            TotalSum = GetTotalSum();
        }

        private void ProductInOrder_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TotalSum = GetTotalSum();
        }

        #region Commands

        public DelegateCommand SelectCommand { get; set; }
        private void Select()
        {
            ProductInOrderCollection = new ObservableCollection<ProductInOrder>(_SelectedProducts.
                Select(o => new ProductInOrder
                {
                    ID = ((Product)o).ProductID,
                    Name = ((Product)o).ProductName,
                    Discount = 1,
                    Quantity = 1,
                    UnitPrice = ((Product)o).UnitPrice.Value
                    
                }));
            foreach (var productInOrder in ProductInOrderCollection)
            {
                (productInOrder as ProductInOrder).PropertyChanged += ProductInOrder_PropertyChanged;
            }
            ProductInOrderCollection.CollectionChanged += OnProductInOrderCollectionChanged;

            TotalSum = GetTotalSum();

            SelectedProducts.Clear();
            OrderDate = DateTime.Now.ToLongDateString();
        }
        private bool CanSelect()
        {
            if (SelectedProducts != null)
                if (SelectedProducts.Count != 0)
                    return true;
            return false;
        }

        public DelegateCommand UnselectCommand { get; set; }
        private void Unselect()
        {
            _ProductInOrderCollection.Clear();
            CreateOrderCommand.RaiseCanExecuteChanged();
            UnselectCommand.RaiseCanExecuteChanged();

            OrderDate = String.Empty;
            SelectedCustomer = null;
            SelectedEmployee = null;
        }
        private bool CanUnselect() { return ProductsInOrderIsNullOrEmpty(); }


        public DelegateCommand CreateOrderCommand { get; set; }
        private void CreateOrder()
        {

            //using (var contextTransaction = _context.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        _context.Orders.Add(order);
            //        _context.SaveChanges();

            //_context.Order_Details.AddRange(
            //    new List<Order_Details>(
            //        ProductList.Select(p => new Order_Details
            //        {
            //            OrderID = order.OrderID,
            //            ProductID = p.ID,
            //            UnitPrice = p.UnitPrice,
            //            Quantity = p.Quantity,
            //            Discount = p.Discount
            //        })));
            //_context.SaveChanges();

            //        contextTransaction.Commit();

            //        OrderID = order.OrderID;
            //    }
            //    catch (Exception)
            //    {
            //        contextTransaction.Rollback();
            //    }
            //}

            _eventAggregator.GetEvent<OnOrderCreate>().Publish(new List<ProductInOrder>(ProductInOrderCollection));
            ProductInOrderCollection.Clear();
            CreateOrderCommand.RaiseCanExecuteChanged();
            UnselectCommand.RaiseCanExecuteChanged();

        }
        private bool CanCreateOrder() { return ProductsInOrderIsNullOrEmpty(); }

        #endregion

        #region Bindable properties

        public ObservableCollection<Product> Products { get; set; }

        private ObservableCollection<Object> _SelectedProducts;
        public ObservableCollection<Object> SelectedProducts
        {
            get { return _SelectedProducts; }
            set
            {
                SetProperty(ref _SelectedProducts, value);
                SelectCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<ProductInOrder> _ProductInOrderCollection;

        public ObservableCollection<ProductInOrder> ProductInOrderCollection
        {
            get => _ProductInOrderCollection;
            set
            {
                SetProperty(ref _ProductInOrderCollection, value);

                UnselectCommand.RaiseCanExecuteChanged();
                CreateOrderCommand.RaiseCanExecuteChanged();

            }
        }

        public List<Employee> Employees { get; set; }

        Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set => SetProperty(ref selectedEmployee, value);
        }

        public List<Customer> Customers { get; set; }

        Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set => SetProperty(ref selectedCustomer, value);
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

        private string orderDate;
        public string OrderDate
        {
            get { return orderDate; }
            set { SetProperty(ref orderDate, value); }
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

        private string totalSum;
        public string TotalSum
        {
            set { SetProperty(ref totalSum, value); }
            get { return totalSum; }
        }
        #endregion

        #region Screen objects


        //public class ProductInOrder:INotifyPropertyChanged
        //{
        //    public int ID { get; set; }
        //    public string Name { get; set; }
        //    public decimal UnitPrice { get; set; }
        //    public int Quantity { get; set; }
        //    public float Discount { get; set; }

        //    public event PropertyChangedEventHandler PropertyChanged;
        //}

        #endregion

        #endregion

        #region Create
        #endregion

        #region Utilites
        private bool ProductsInOrderIsNullOrEmpty()
        {
            if (ProductInOrderCollection != null)
                if (ProductInOrderCollection.Count != 0)
                    return true;
            return false;
        }
        private string GetTotalSum()
        {
            return ProductInOrderCollection.Select(p => ((Double)p.UnitPrice) * p.Quantity * p.Discount).Sum().ToString("C2");
        }
        #endregion
    }
}