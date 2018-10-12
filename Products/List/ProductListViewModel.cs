using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using Prism.Regions;

using System.Data.Entity;

using BLL;
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
        Product _selectedProduct;
        ObservableCollection<Product> _products;

        public ProductListViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            _context = context;
            _eventAggregator = eventAggregator;
            this.Title = "PRODUCTS ON STORE";
            AddCommand = new DelegateCommand(Add);
            EditCommand = new DelegateCommand<Product>(Edit);
            DeleteCommand = new DelegateCommand<Product>(Delete);

        }

        public DelegateCommand AddCommand { get; set; }
        private void Add()
        {
            _eventAggregator.GetEvent<OnProductAddEvent>().Publish();
        }

        public DelegateCommand<Product> EditCommand { get; set; }
        private void Edit(Product product)
        {
            _eventAggregator.GetEvent<OnProductEditEvent>().Publish(product);
        }
        public DelegateCommand<Product> DeleteCommand { get; set; }
        private void Delete(Product product)
        {
            _products.Remove(product);
            //_context.SaveChanges();
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            await _context.Products.LoadAsync();

            this.Products = _context.Products.Local;
        }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set => SetProperty(ref _products, value);
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                SetProperty(ref _selectedProduct, value);
                this.Title = _selectedProduct.ProductID.ToString();
                _eventAggregator.GetEvent<OnProductSelectedEvent>().Publish(_selectedProduct.ProductID.ToString());
            }
        }

    }
}
