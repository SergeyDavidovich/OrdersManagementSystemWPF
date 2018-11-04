using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using DAL_LocalDb;
using System.Collections.ObjectModel;

namespace Orders.ViewModels
{
    public class CreateViewModel : ViewModelBase
    {
        LocalDbContext _context;

        public CreateViewModel(LocalDbContext context)
        {
            //Title = "Orders management";
            _context = context;

            Products = new ObservableCollection<Product>(context.Products);

        }
        public ObservableCollection<Product> Products
        {
            get; set;
        }

        public Product SelectedProduct
        {
            get; set;
        }
    }
}
