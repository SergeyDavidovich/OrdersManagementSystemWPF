using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace Infrastructure
{
    public class GlobalCommands
    {
        public static CompositeCommand NavigateToCompositeCommand = new CompositeCommand();
        public static CompositeCommand NavigateToManageEntityViewCompositeCommand = new CompositeCommand();
    }
}
