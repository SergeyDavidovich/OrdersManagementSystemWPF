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

namespace Products.Add
{
    public class ProductAddViewModel : NavigationAwareViewModelBase
    {
        IEventAggregator _eventAggregator;
        LocalDbContext _context;

        public ProductAddViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            Title = "Add product";
            _eventAggregator = eventAggregator;
            _context = context;
            _eventAggregator.GetEvent<OnProductSelectedEvent>().Subscribe(SetSelectedID);
            _eventAggregator.GetEvent<OnProductAddEvent>().Subscribe(AddProduct);
            _eventAggregator.GetEvent<OnProductEditEvent>().Subscribe(EditProduct);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

        }
        private void AddProduct()
        {
            Title = "Add";
        }
        private void EditProduct(Product product)
        {
            Title = $"Edit - {product.ProductID.ToString()}";
        }
        private void SetSelectedID(string id)
        {
            Title = id;
        }

    }
}
