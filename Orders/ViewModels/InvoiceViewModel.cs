using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_LocalDb;
using Infrastructure.Base;
using Orders.Events;
using Prism.Events;
using Orders.CommonTypes;

namespace Orders.ViewModels
{
    public class InvoiceViewModel : ViewModelBase
    {
        LocalDbContext _context;
        IEventAggregator _eventAggregator;

        public InvoiceViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            Title = "Invoice view";
            _context = context;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OnOrderCreate>().Subscribe(SetProductsList);


        }

        private List<ProductInOrder> _ProductList;
        public List<ProductInOrder> ProductList
        {
            get => _ProductList;
            set => SetProperty(ref _ProductList, value);
        }

        private void SetProductsList(List<ProductInOrder> list)
        {
            ProductList = list;
        }
    }
}
