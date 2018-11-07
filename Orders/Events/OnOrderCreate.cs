using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Orders.ViewModels;
using Orders.CommonTypes;

namespace Orders.Events
{
    internal class OnOrderCreate : PubSubEvent<List<ProductInOrder>>
    {
    }
}
