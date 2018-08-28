using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;

namespace Employees.List
{
    public class EmployeeListViewModel : NavigationAwareViewModelBase
    {
        public EmployeeListViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Title = "Employees management";
        }
    }
}
