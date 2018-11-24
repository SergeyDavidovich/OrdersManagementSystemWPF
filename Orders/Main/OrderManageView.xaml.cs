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
using Infrastructure;
using Microsoft.Practices.Unity;
using Orders.Main;

namespace Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderManageView.xaml
    /// </summary>
    public partial class OrderManageView : UserControl
    {
        public OrderManageView(IUnityContainer unityContainer)
        {
            InitializeComponent();
            this.DataContext = unityContainer.Resolve<OrderManageViewModel>();
        }
     
    }
}
