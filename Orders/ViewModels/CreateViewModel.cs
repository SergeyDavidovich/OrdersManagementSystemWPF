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

namespace Orders.ViewModels
{
    public class CreateViewModel : ViewModelBase
    {
        LocalDbContext _context;
        IEventAggregator _eventAggregator;

        public CreateViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            _context = context;
            _eventAggregator = eventAggregator;

            Products = new ObservableCollection<Product>(context.Products.
                Where(p => p.Discontinued == false && p.UnitsInStock > 0));
            SelectCommand = new DelegateCommand(Select, CanSelect);
            UnselectCommand = new DelegateCommand(Unselect, CanUnselect);
            CreateOrderCommand = new DelegateCommand(CreateOrder, CanCreateOrder);
        }

        #region Commands

        public DelegateCommand SelectCommand { get; set; }
        private void Select()
        {
            var productInOrderCollection = new ObservableCollection<ProductInOrder>(_SelectedProducts.
                Select(o => new ProductInOrder
                {
                    ID = ((Product)o).ProductID,
                    Name = ((Product)o).ProductName,
                    Discount = 1,
                    Quantity = 1,
                    UnitPrice = ((Product)o).UnitPrice.Value
                }));


            ProductInOrderCollection = productInOrderCollection;


            SelectedProducts.Clear();
        }
        private bool CanSelect()
        {
            if (SelectedProducts != null)
            {
                if (SelectedProducts.Count == 0)
                    return false;
                else
                    return true;
            }
            return false;
        }

        public DelegateCommand UnselectCommand { get; set; }
        private void Unselect() { _ProductInOrderCollection.Clear(); }
        private bool CanUnselect()
        {
            if (_ProductInOrderCollection != null)
            {
                if (_ProductInOrderCollection.Count == 0)
                    return false;
                else
                    return true;
            }
            return false;
        }

        public DelegateCommand CreateOrderCommand { get; set; }
        private void CreateOrder()
        {
            _eventAggregator.GetEvent<OnOrderCreate>().Publish(new List<ProductInOrder>(ProductInOrderCollection));

        }
        private bool CanCreateOrder()
        { return true; }
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
                UnselectCommand.RaiseCanExecuteChanged();

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
            }
        }


        #endregion

        #region Screen objects


        //public class ProductInOrder
        //{
        //    public int ID { get; set; }
        //    public string Name { get; set; }
        //    public decimal UnitPrice { get; set; }
        //    public int Quantity { get; set; }
        //    public float Discount { get; set; }
        //}

        #endregion
    }
}