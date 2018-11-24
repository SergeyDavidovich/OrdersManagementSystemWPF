using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Bootstrapper bootstrapper;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            
        }
        //protected override void OnExit(ExitEventArgs e)
        //{
        //    base.OnExit(e);
        //    bootstrapper.Context.Database.Connection.Close();
        //    Debug.WriteLine(bootstrapper.Context.Database.Connection.State);
        //}
    }
}
