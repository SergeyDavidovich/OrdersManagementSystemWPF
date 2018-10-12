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

namespace Products.List
{
    public class ProductListViewModel : NavigationAwareViewModelBase
    {

        LocalDbContext _context;
        Product _selectedProduct;
        ObservableCollection<Product> _products;

        public ProductListViewModel(LocalDbContext context)
        {
            this.Title = "PRODUCTS ON STORE";
            _context = context;
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
            }
        }

    }
}
