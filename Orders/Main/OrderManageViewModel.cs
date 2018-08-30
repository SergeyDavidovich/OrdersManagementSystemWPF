using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Base;
using Infrastructure.Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Orders.Main
{
    public class OrderManageViewModel : NavigationAwareViewModelBase, IRegionManagerAware
    {
        public OrderManageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            ShowJournalCommand = new DelegateCommand(() => RegionManager?.RequestNavigate(RegionNames.OrdersContentRegion, "JournalView"));
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            UpdateBannerTitle("Orders");        
        }

        public DelegateCommand ShowJournalCommand { get; set; }
        public IRegionManager RegionManager { get; set; }
    }
}
