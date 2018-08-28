using Products.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Products.Add;
using Products.List;

namespace Products
{
    public class ProductsModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public ProductsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<ProductAddView>();
            _container.RegisterTypeForNavigation<ProductListView>();
        }
    }
}