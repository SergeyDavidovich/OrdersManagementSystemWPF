using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using Prism.Regions;

namespace Orders.Main
{
    public class OrderManageViewModel : NavigationAwareViewModelBase
    {
        public OrderManageViewModel(IEventAggregator eventAggregator):base(eventAggregator)
        {
            Title = "Orders";
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            UpdateBannerTitle(Title);
        }
    }
}
