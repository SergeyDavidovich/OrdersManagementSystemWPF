using System.Linq;
using Banner.Views;
using Dashboard.Main;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.Practices.Unity;
using OMS.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

using DAL_LocalDb;

namespace OMS.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public MainWindowViewModel(IRegionManager regionManager, IUnityContainer unityContainer) 
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
            //Title = "OMS " + System.DateTime.Now.ToShortTimeString();
        }
        public void ConfigureRegionManager()
        {
            //Inject views in regions
            _regionManager.Regions[RegionNames.GlobalRegion].Add(_unityContainer.Resolve<ContentView>());
            //Don't change order, because BannerRegion becomes available after CommonView instantiated 
            _regionManager.Regions[RegionNames.BannerRegion].Add(_unityContainer.Resolve<BannerView>());

        }
    }
}
