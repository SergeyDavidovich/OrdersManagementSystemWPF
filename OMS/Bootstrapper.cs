using OMS.Views;
using System.Windows;
using Customers;
using Dashboard.OrderStatistics;
using Employees;
using Entities;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using OMS.ViewModels;
using Orders;

namespace OMS
{
    class Bootstrapper : UnityBootstrapper
    {
        private MainWindowViewModel shellViewModel;

        protected override DependencyObject CreateShell()
        {
            var mainWindow = Container.Resolve<MainWindow>();
            shellViewModel = Container.Resolve<MainWindowViewModel>();
            mainWindow.DataContext = shellViewModel;
            return mainWindow;
        }

        protected override void InitializeShell()
        {
            shellViewModel.ConfigureRegionManager();
            Application.Current.MainWindow.Show();
        }
         
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(Dashboard.DashboardModule));
            moduleCatalog.AddModule(typeof(OrdersModule));
            moduleCatalog.AddModule(typeof(CustomersModule));
            moduleCatalog.AddModule(typeof(EntitiesModule));
            moduleCatalog.AddModule(typeof(EmployeesModule));

        }
    }
}
