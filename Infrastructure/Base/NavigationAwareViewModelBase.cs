using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Events;
using Prism.Events;
using Prism.Regions;

namespace Infrastructure.Base
{
    public abstract class NavigationAwareViewModelBase : ViewModelBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;
        protected NavigationAwareViewModelBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        protected NavigationAwareViewModelBase()
        {

        }
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void UpdateBannerTitle(string title)
        {
            _eventAggregator?.GetEvent<OnNavigatedToEvent>().Publish(title);
        }
    }
}
