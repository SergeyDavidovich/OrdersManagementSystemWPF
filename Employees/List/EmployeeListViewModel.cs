using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using DAL_LocalDb;
using System.Collections.ObjectModel;

namespace Employees.List
{
    public class EmployeeListViewModel : NavigationAwareViewModelBase
    {
        LocalDbContext _context;

        public EmployeeListViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            Title = "Employees management";
            _context = context;
            Employees = new ObservableCollection<Employee>(context.Employees);
        }

        public ObservableCollection<Employee> Employees
        {
            get; set;
        }

        Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }
    }
}
