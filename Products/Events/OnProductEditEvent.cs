using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_LocalDb;
using Prism.Events;

namespace Products.Events
{
   public class OnProductEditEvent :PubSubEvent<int>
    {
    }
}
