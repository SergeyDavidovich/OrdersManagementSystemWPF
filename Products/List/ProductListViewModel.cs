using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using Prism.Regions;

using System.Data.Entity;

using DAL_LocalDb;
using System.Collections.ObjectModel;
using Products.Events;
using Prism.Commands;

namespace Products.List
{
    public class ProductListViewModel : NavigationAwareViewModelBase
    {
        IEventAggregator _eventAggregator;
        LocalDbContext _context;
        IEnumerable<Product> products;

        public ProductListViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            _context = context;
            _eventAggregator = eventAggregator;
            this.Title = "PRODUCTS ON STORE";
            AddCommand = new DelegateCommand(Add);
            EditCommand = new DelegateCommand(Edit, CanEdit);
            //DeleteCommand = new DelegateCommand(Delete, CanDelete);

            _eventAggregator.GetEvent<OnProductCompleted>().Subscribe(() => IsGroupBoxEnabled = true);

            products = _context.Products;
        }

        #region Commands
        public DelegateCommand AddCommand { get; set; }
        private void Add()
        {
            _eventAggregator.GetEvent<OnProductAddEvent>().Publish();
            IsGroupBoxEnabled = false;
        }

        public DelegateCommand EditCommand { get; set; }
        private void Edit()
        {
            _eventAggregator.GetEvent<OnProductEditEvent>().Publish(SelectedProduct.ProductId);

            IsGroupBoxEnabled = false;
        }
        private bool CanEdit()
        {
            bool result = (SelectedProduct == null) ? false : true;
            return result;
        }

        //public DelegateCommand DeleteCommand { get; set; }
        //private void Delete()
        //{
        //    //_products.Remove(SelectedProduct);
        //    DeleteCommand.RaiseCanExecuteChanged();
        //    EditCommand.RaiseCanExecuteChanged();
        //    _context.SaveChanges();
        //}
        //private bool CanDelete()
        //{
        //    bool result = (SelectedProduct == null) ? false : true;
        //    return result;
        //}
        #endregion

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            var productList = new ObservableCollection<ProductViewObject>(products.
                  Select(p => new ProductViewObject
                  {
                      ProductId = p.ProductID,
                      ProductName = p.ProductName,
                      SupplierName = p.Suppliers.CompanyName,
                      CategoryName = p.Categories.CategoryName,
                      UnitPrice = p.UnitPrice,
                      QuantityPerUnit = p.QuantityPerUnit,
                      Discontinued = p.Discontinued
                  }));

            this.Products = productList;
        }

        #region Bindable properties

        /// <summary>
        /// экранный объект Product
        /// </summary>
        public class ProductViewObject
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public string SupplierName { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal? UnitPrice { get; set; }
            public bool Discontinued { get; set; }
        }

        ObservableCollection<ProductViewObject> _products;
        public ObservableCollection<ProductViewObject> Products
        {
            get { return _products; }
            set => SetProperty(ref _products, value);
        }

        ProductViewObject _selectedProduct;
        public ProductViewObject SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                SetProperty(ref _selectedProduct, value);
                _eventAggregator.GetEvent<OnProductSelectedEvent>().Publish(_selectedProduct.ProductId);

                //DeleteCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        bool _isGroupBoxEnabled = true;
        public bool IsGroupBoxEnabled
        {
            get => _isGroupBoxEnabled;
            set => SetProperty(ref _isGroupBoxEnabled, value);

        }
        #endregion
    }
}
