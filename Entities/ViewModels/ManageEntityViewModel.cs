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
using Infrastructure.Base;
using Infrastructure.Prism;
using Prism.Events;
using Products.Add;
using Products.List;

namespace Entities.ViewModels
{
    public class ManageEntityViewModel : NavigationAwareViewModelBase, IRegionManagerAware
    {
        public ManageEntityViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            switch (navigationContext.Parameters["Entity"])
            {
                case "Customers":
                    UpdateBannerTitle("Customers");
                    RegionManager.RequestNavigate(RegionNames.EntityListRegion, "CustomerListView");
                    RegionManager.RequestNavigate(RegionNames.EntityViewAddEditRegion, "CustomerAddView");
                 
                    break;
                case "Employees":
                    UpdateBannerTitle("Employees");
                    RegionManager.RequestNavigate(RegionNames.EntityListRegion, "EmployeeListView");
                    RegionManager.RequestNavigate(RegionNames.EntityViewAddEditRegion, "EmployeeAddView");
                    break;
                case "Products":
                    UpdateBannerTitle("Products");
                    RegionManager.RequestNavigate(RegionNames.EntityListRegion, "ProductListView");
                    RegionManager.RequestNavigate(RegionNames.EntityViewAddEditRegion, "ProductAddView");
                    break;
            }
            //var entityListRegionViews = _regionManager.Regions[RegionNames.EntityListRegion].Views;
            //var entityAddEditRegionViews = _regionManager.Regions[RegionNames.EntityViewAddEditRegion].Views;

            //switch (navigationContext.Parameters["Entity"])
            //{
            //    case "Customers":
            //        if (!entityListRegionViews.Contains(entityListRegionViews.FirstOrDefault(v => v is CustomerListView)))
            //        {
            //            _regionManager.AddToRegion(RegionNames.EntityListRegion, _container.Resolve<CustomerListView>());
            //            _regionManager.AddToRegion(RegionNames.EntityViewAddEditRegion, _container.Resolve<CustomerAddView>());
            //        }

            //            _regionManager.Regions[RegionNames.EntityListRegion].Activate(entityListRegionViews.FirstOrDefault(v => v is CustomerListView));
            //            _regionManager.Regions[RegionNames.EntityViewAddEditRegion].Activate(entityAddEditRegionViews.FirstOrDefault(v => v is CustomerAddView));
            //        break;
            //    case "Employees":
            //        if (!entityListRegionViews.Contains(entityListRegionViews.FirstOrDefault(v => v is EmployeeListView)))
            //        {
            //            _regionManager.AddToRegion(RegionNames.EntityListRegion, _container.Resolve<EmployeeListView>());
            //            _regionManager.AddToRegion(RegionNames.EntityViewAddEditRegion, _container.Resolve<EmployeeAddView>());
            //        }

            //            _regionManager.Regions[RegionNames.EntityListRegion].Activate(entityListRegionViews.FirstOrDefault(v => v is EmployeeListView));
            //            _regionManager.Regions[RegionNames.EntityViewAddEditRegion].Activate(entityAddEditRegionViews.FirstOrDefault(v => v is EmployeeAddView));
            //        break;
            //    case  "Products":
            //        if (!entityListRegionViews.Contains(entityListRegionViews.FirstOrDefault(v => v is ProductListView)))
            //        {
            //            _regionManager.AddToRegion(RegionNames.EntityListRegion, _container.Resolve<ProductListView>());
            //            _regionManager.AddToRegion(RegionNames.EntityViewAddEditRegion, _container.Resolve<ProductAddView>());
            //        }

            //        _regionManager.Regions[RegionNames.EntityListRegion].Activate(entityListRegionViews.FirstOrDefault(v => v is ProductListView));
            //        _regionManager.Regions[RegionNames.EntityViewAddEditRegion].Activate(entityAddEditRegionViews.FirstOrDefault(v => v is ProductAddView));

            //        break;

            //}
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public IRegionManager RegionManager { get; set; }
    }
}
