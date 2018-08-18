using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Prism.Commands;
using Prism.Regions;

namespace Dashboard.CustomerStatistics
{
    public class CustomerStatsViewModel
    {
        private readonly IRegionManager _regionManager;

        public CustomerStatsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region Commands

        //public DelegateCommand NavigateToManageCustomerViewCommand { get; set; }

        //private void NavigateToManageCustomerView()
        //{
        //    _regionManager.RequestNavigate(RegionNames.ContentRegion, "ManageEntityView");
        //    _regionManager.AddToRegion(RegionNames.EntityListRegion, )
        //}
        #endregion
    }
}
