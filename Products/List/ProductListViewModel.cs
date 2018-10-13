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
            EditCommand = new DelegateCommand(Edit, CanEdit);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }

        #region Commands
        public DelegateCommand AddCommand { get; set; }
        private void Add() => _eventAggregator.GetEvent<OnProductAddEvent>().Publish();

        public DelegateCommand EditCommand { get; set; }
        private void Edit() => _eventAggregator.GetEvent<OnProductEditEvent>().Publish(SelectedProduct);
        private bool CanEdit()
        {
            bool result = (SelectedProduct == null) ? false : true;
            return result;
        }

        public DelegateCommand DeleteCommand { get; set; }
        private void Delete()
        {
            _products.Remove(SelectedProduct);
            DeleteCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            //_context.SaveChanges();
        }
        private bool CanDelete()
        {
            bool result = (SelectedProduct == null) ? false : true;
            return result;
        }
        #endregion

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            await _context.Products.LoadAsync();

            this.Products = _context.Products.Local;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
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

                DeleteCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
            
        }

    }
}
