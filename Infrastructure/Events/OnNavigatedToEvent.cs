using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Regions;

namespace Infrastructure.Events
{
    public class OnNavigatedToEvent : PubSubEvent<string>
    {
        
    }
}
