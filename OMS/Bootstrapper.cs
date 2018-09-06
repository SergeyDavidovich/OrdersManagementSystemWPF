using System.Linq;
using OMS.Views;
using System.Windows;
using Banner;
using Customers;
using Dashboard;
using Employees;
using Entities;
using Infrastructure;
using Infrastructure.Prism;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using OMS.ViewModels;
using Orders;
using Prism.Regions;
using Products;

namespace OMS
{
    class Bootstrapper : UnityBootstrapper
    {
        private MainWindowViewModel shellViewModel;

        /// <summary>
        /// 1 step of bootstrapping
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(DashboardModule));
            moduleCatalog.AddModule(typeof(OrdersModule));
            moduleCatalog.AddModule(typeof(CustomersModule));
            moduleCatalog.AddModule(typeof(EntitiesModule));
            moduleCatalog.AddModule(typeof(EmployeesModule));
            moduleCatalog.AddModule(typeof(ProductsModule));
            moduleCatalog.AddModule(typeof(BannerModule));
        }

        /// <summary>
        /// 2 step of bootstrapping
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IRegionNavigationContentLoader, ScopedRegionNavigationContentLoader>(
                new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// 3 step of bootstrapping
        /// </summary>
        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            return behaviors;
        }

        /// <summary>
        /// 4 step of bootstrapping
        /// </summary>
        protected override DependencyObject CreateShell()
        {
            var mainWindow = Container.Resolve<MainWindow>();
            shellViewModel = Container.Resolve<MainWindowViewModel>();
            mainWindow.DataContext = shellViewModel;
            return mainWindow;
        }

        /// <summary>
        /// 5 step of bootstrapping
        /// </summary>
        protected override void InitializeShell()
        {
            shellViewModel.ConfigureRegionManager();
            Application.Current.MainWindow.Show();
        }
    }
}
