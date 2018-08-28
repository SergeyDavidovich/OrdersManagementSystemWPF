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
using Microsoft.Practices.Unity;

namespace Customers.Add
{
    /// <summary>
    /// Interaction logic for CustomerAddView.xaml
    /// </summary>
    public partial class CustomerAddView : UserControl
    {
        public CustomerAddView(IUnityContainer unityContainer)
        {
            InitializeComponent();
            this.DataContext = unityContainer.Resolve<CustomerAddViewModel>();
        }
    }
}
