using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism;

namespace Dashboard.OrderStatistics
{
    public class  OrdersStatViewModel: ViewModelBase, IActiveAware
    {
        public OrdersStatViewModel()
        {
            Title = "Orders";
            IsActiveChanged += OrdersStatViewModel_IsActiveChanged;
        }

        private void OrdersStatViewModel_IsActiveChanged(object sender, EventArgs e)
        {
            var a = 1;
            
        }

        public bool IsActive { get; set; }
        public event EventHandler IsActiveChanged;
    }
}
