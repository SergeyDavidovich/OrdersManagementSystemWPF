using Orders.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Orders.ViewModels;

namespace Orders
{
    public class OrdersModule : IModule
    {
        //private IRegionManager _regionManager;
        private IUnityContainer _container;

        public OrdersModule(IUnityContainer container)//, IRegionManager regionManager)
        {
            _container = container;
            //_regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<OrderManageView>();

            _container.RegisterTypeForNavigation<CreateView>();
            _container.RegisterTypeForNavigation<InvoiceView>();
            _container.RegisterTypeForNavigation<JournalView>();

            //_container.RegisterInstance(typeof(InvoiceViewModel), new TransientLifetimeManager());

            //_regionManager.RegisterViewWithRegion(RegionNames.OrdersContentRegion, typeof(CreateView));

        }
    }
}