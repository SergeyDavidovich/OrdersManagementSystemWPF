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

            this.OrderGroupsMock = new List<OrderGroupsObject>()
            {
                new OrderGroupsObject(){Country="USA", Quantity=10},
                new OrderGroupsObject(){Country="GB", Quantity=20},
                new OrderGroupsObject(){Country="Germany", Quantity=30},
                new OrderGroupsObject(){Country="Poland", Quantity=15},
                new OrderGroupsObject(){Country="Czech Republic", Quantity=25}
            };
        }
        public class OrderGroupsObject
        {
            public string Country { get; set; }
            public int Quantity { get; set; }
        }
        public List<OrderGroupsObject> OrderGroupsMock { get; set; }






        private void OrdersStatViewModel_IsActiveChanged(object sender, EventArgs e)
        {
            var a = 1;
            
        }

        public bool IsActive { get; set; }
        public event EventHandler IsActiveChanged;
    }
}
