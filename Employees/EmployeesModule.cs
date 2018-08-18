using Employees.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Employees.Add;
using Employees.List;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace Employees
{
    public class EmployeesModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public EmployeesModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<EmployeeAddView>();
            _container.RegisterTypeForNavigation<EmployeeListView>();

        }
    }
}