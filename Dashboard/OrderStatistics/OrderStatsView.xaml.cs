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

namespace Dashboard.OrderStatistics
{
    /// <summary>
    /// Interaction logic for OrdersStat.xaml
    /// </summary>
    public partial class OrderStatsView : UserControl
    {
        public OrderStatsView(IUnityContainer unityContainer)
        {
            InitializeComponent();
            this.DataContext = unityContainer.Resolve<OrdersStatViewModel>();
        }
    }
}
