using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace Products.RegionCommands
{
   public class ProductsRegionCommands
    {
        public static CompositeCommand Read = new CompositeCommand();

        public static CompositeCommand Create = new CompositeCommand();
        public static CompositeCommand Update = new CompositeCommand();
        public static CompositeCommand Delete = new CompositeCommand();



    }
}
