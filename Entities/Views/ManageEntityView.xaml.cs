using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entities.ViewModels;
using Microsoft.Practices.Unity;

namespace Entities.Views
{
    /// <summary>
    /// Interaction logic for ManageEntityView.xaml
    /// </summary>
    public partial class ManageEntityView : UserControl
    {
        public ManageEntityView(IUnityContainer container)
        {
            InitializeComponent();
            this.DataContext = container.Resolve<ManageEntityViewModel>();
        }
    }
}
