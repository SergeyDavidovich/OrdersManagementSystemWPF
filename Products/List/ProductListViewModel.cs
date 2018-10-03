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
        IGenericRepository<DAL_LocalDb.Products> _repository;

        public ProductListViewModel(IGenericRepository<DAL_LocalDb.Products> repository)
        {
            Title = "Products management";
            _repository = repository;
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            //Products = await _repository.GetALlAsync();
            if (Products == null) Products = await _repository.GetALlAsync();
        }

        List<DAL_LocalDb.Products> _products;

        public List<DAL_LocalDb.Products> Products
        {
            get { return _products; }
            set => SetProperty(ref _products, value);
        }
    }

}
