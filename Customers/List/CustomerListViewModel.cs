using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Regions;

namespace Customers.List
{
    public class CustomerListViewModel : NavigationAwareViewModelBase
    {
        public CustomerListViewModel()
        {
            Title = "CUSTOMERS VIEW";
        }
    }
}
