using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Events
{
    public class OnProductSelectedEvent : PubSubEvent<string> { }
}
