using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;

namespace Employees.Add
{
    public class EmployeeAddViewModel : NavigationAwareViewModelBase
    {
        public EmployeeAddViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Title = "Add employee";
        }
    }
}
