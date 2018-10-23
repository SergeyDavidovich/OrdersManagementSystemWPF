using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using DAL_LocalDb;
using Prism.Regions;
using Products.Events;
using Prism.Commands;
using System.ComponentModel;

namespace Products.Add
{
    public class ProductAddViewModel : NavigationAwareViewModelBase, IDataErrorInfo
    {
        IEventAggregator _eventAggregator;
        LocalDbContext _context;
        bool _isGroupBoxEnabled=false;
        Product currentProduct;

        public ProductAddViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            //IsGroupBoxEnabled = false;
            _eventAggregator = eventAggregator;
            _context = context;
            _eventAggregator.GetEvent<OnProductSelectedEvent>().Subscribe(SetSelectedID);
            _eventAggregator.GetEvent<OnProductAddEvent>().Subscribe(AddProduct);
            _eventAggregator.GetEvent<OnProductEditEvent>().Subscribe(EditProduct);

            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        #region IDataErrorInfo implemntation
        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();
        #endregion

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        #region Commands

        public DelegateCommand CancelCommand { get; set; }
        private void Cancel()
        {
            IsGroupBoxEnabled = false;
            _eventAggregator.GetEvent<OnProductCompleted>().Publish();

            Title = "Cancel";
        }

        public DelegateCommand SaveCommand { get; set; }
        private void Save()
        {
            IsGroupBoxEnabled = false;
            _eventAggregator.GetEvent<OnProductCompleted>().Publish();
            Title = "Cancel";


        }
        private bool CanSave() { return true; }

        #endregion

        #region PubSubEvents
        private void AddProduct()
        {
            Title = "Add new product";
            currentProduct = new Product();
            IsGroupBoxEnabled = true;
        }
        private void EditProduct(int id)
        {
            currentProduct = _context.Products.Find(id);
            Title = "Edit product: ";
            Title += $"{currentProduct.ProductName}";
            IsGroupBoxEnabled = true;
        }
        private void SetSelectedID(int id)
        {
            currentProduct = _context.Products.Find(id);

            Title = "View product: ";
            Title += $"{currentProduct.ProductName}"; IsGroupBoxEnabled = false;
        }

        public bool IsGroupBoxEnabled
        {
            get => _isGroupBoxEnabled;
            set => SetProperty(ref _isGroupBoxEnabled, value);

        }
        #endregion
    }
}
