using Dashboard.Main;
using Prism.Modularity;
using Prism.Regions;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Dashboard.OrderStatistics;
using Infrastructure;

namespace Dashboard
{
    public class DashboardModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public DashboardModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<DashboardView>("DashboardView");
            
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "DashboardView");
            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
            _regionManager.RegisterViewWithRegion(RegionNames.CustomersStatRegion, typeof(CustomerStatistics.CustomerStatsView));
            _regionManager.RegisterViewWithRegion(RegionNames.ProductsStatRegion, typeof(ProductStatistics.ProductStatsView));
            _regionManager.RegisterViewWithRegion(RegionNames.EmployeesStatRegion, typeof(EmployeeStatistics.EmployeeStatsView));
            _regionManager.RegisterViewWithRegion(RegionNames.OrdersStatRegion, typeof(OrderStatistics.OrderStatsView));
        }

    }
}