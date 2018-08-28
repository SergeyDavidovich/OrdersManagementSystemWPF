using Banner.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace Banner
{
    public class BannerModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public BannerModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
        }
    }
}