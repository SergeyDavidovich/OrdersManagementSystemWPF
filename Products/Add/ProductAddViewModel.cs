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

namespace Products.Add
{
    public class ProductAddViewModel : NavigationAwareViewModelBase
    {
        IEventAggregator _eventAggregator;
        LocalDbContext _context;
        bool _isGroupBoxEnabled=false;

        public ProductAddViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            //IsGroupBoxEnabled = false;
            _eventAggregator = eventAggregator;
            _context = context;
            _eventAggregator.GetEvent<OnProductSelectedEvent>().Subscribe(SetSelectedID);
            _eventAggregator.GetEvent<OnProductAddEvent>().Subscribe(AddProduct);
            _eventAggregator.GetEvent<OnProductEditEvent>().Subscribe(EditProduct);

            CancelCommand = new DelegateCommand(Cancel);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        public DelegateCommand CancelCommand { get; set; }
        private void Cancel()
        {
            IsGroupBoxEnabled = false;
            Title = "Cancel";
        }

        #region PubSubEvents
        private void AddProduct()
        {
            Title = "Add new product";
            IsGroupBoxEnabled = true;
        }
        private void EditProduct(Product product)
        {
            Title = $"Edit - {product.ProductID.ToString()}";
            IsGroupBoxEnabled = true;
        }
        private void SetSelectedID(string id)
        {
            Title = id;
            IsGroupBoxEnabled = false;
        }

        public bool IsGroupBoxEnabled
        {
            get => _isGroupBoxEnabled;
            set => SetProperty(ref _isGroupBoxEnabled, value);

        }
        #endregion
    }
}
