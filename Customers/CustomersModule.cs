using Prism.Modularity;
using Prism.Regions;
using System;
using Customers.Add;
using Customers.List;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;

namespace Customers
{
    public class CustomersModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public CustomersModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            
        }
    }
}