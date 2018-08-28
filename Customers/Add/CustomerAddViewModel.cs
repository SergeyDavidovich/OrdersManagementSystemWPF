using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using Prism.Regions;

namespace Customers.Add
{
    public class CustomerAddViewModel : NavigationAwareViewModelBase
    {
        public CustomerAddViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}