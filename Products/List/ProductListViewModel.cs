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
        string _testText = "Initial text";
        IGenericRepository<Product> _repository;

        public ProductListViewModel(IGenericRepository<Product> repository)
        {
            Title = "Products management";
            _repository = repository;
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            //Products = null;
            if (Products == null) Products = await _repository.GetALlAsync();
        }
        public async override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
            //if (Products == null) Products = await _repository.GetALlAsync();
        }

        List<Product> _products;

        public List<Product> Products
        {
            get { return _products; }
            set => SetProperty(ref _products, value);
        }
    }

}
