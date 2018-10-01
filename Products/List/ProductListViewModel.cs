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

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _products = _repository.GetAll() as List<DAL_LocalDb.Products>;
        }
       
        List<DAL_LocalDb.Products> _products;
        public List<DAL_LocalDb.Products> Products
        {
            get { return _products; }
        }
    }

}
