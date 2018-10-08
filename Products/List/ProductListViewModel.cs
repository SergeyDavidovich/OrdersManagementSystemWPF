using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using Prism.Regions;

using BLL;
using DAL_LocalDb;

namespace Products.List
{
    public class ProductListViewModel : NavigationAwareViewModelBase
    {

        IGenericRepository<Product> _repository;

        public ProductListViewModel(IGenericRepository<Product> repository)
        {
            Title = "PRODUCTS ON STORE";
            _repository = repository;
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (Products == null) Products = await _repository.GetALlAsync();

            Title += $" ({_products.Count().ToString()} Items)";
        }

        List<Product> _products;

        public List<Product> Products
        {
            get { return _products; }
            set => SetProperty(ref _products, value);
        }
    }
}
