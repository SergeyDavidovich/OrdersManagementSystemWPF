using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using Prism.Regions;

namespace Dashboard.Main
{
    public class DashboardViewModel : NavigationAwareViewModelBase
    {
        public DashboardViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Title = "Dashboard";
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            UpdateBannerTitle(Title);
        }
    }
}