using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customers.Add;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism;
using Prism.Regions;
using Customers.List;
using Employees.Add;
using Employees.List;

namespace Entities.ViewModels
{
    public class ManageEntityViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ManageEntityViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;

            _container = container;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var entityListRegionViews = _regionManager.Regions[RegionNames.EntityListRegion].Views;
            var entityAddEditRegionViews = _regionManager.Regions[RegionNames.EntityViewAddEditRegion].Views;

            switch (navigationContext.Parameters["Entity"])
            {
                case "Customers":
                    if (!entityListRegionViews.Contains(entityListRegionViews.FirstOrDefault(v => v is CustomerListView)))
                    {
                        _regionManager.AddToRegion(RegionNames.EntityListRegion, _container.Resolve<CustomerListView>());
                        _regionManager.AddToRegion(RegionNames.EntityViewAddEditRegion, _container.Resolve<CustomerAddView>());
                    }
               
                        _regionManager.Regions[RegionNames.EntityListRegion].Activate(entityListRegionViews.FirstOrDefault(v => v is CustomerListView));
                        _regionManager.Regions[RegionNames.EntityViewAddEditRegion].Activate(entityAddEditRegionViews.FirstOrDefault(v => v is CustomerAddView));

                    
                    break;
                case "Employees":
                    if (!entityListRegionViews.Contains(entityListRegionViews.FirstOrDefault(v => v is EmployeeListView)))
                    {
                        _regionManager.AddToRegion(RegionNames.EntityListRegion, _container.Resolve<EmployeeListView>());
                        _regionManager.AddToRegion(RegionNames.EntityViewAddEditRegion, _container.Resolve<EmployeeAddView>());
                    }
                   
                        _regionManager.Regions[RegionNames.EntityListRegion].Activate(entityListRegionViews.FirstOrDefault(v => v is EmployeeListView));
                        _regionManager.Regions[RegionNames.EntityViewAddEditRegion].Activate(entityAddEditRegionViews.FirstOrDefault(v => v is EmployeeAddView));
                    break;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
         
        }
    }
}
