using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Mvvm;

namespace Infrastructure.Base
{
    //TODO All ViewModels have to inherit from ViewModelBase
    public abstract class ViewModelBase : BindableBase
    {
        //private readonly IEventAggregator _eventAggregator;
        //protected ViewModelBase(IEventAggregator eventAggregator)
        //{
        //    _eventAggregator = eventAggregator;
        //}

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        //public void 
    }
}
