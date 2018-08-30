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
using Infrastructure.Prism;
using Microsoft.Practices.Unity;
using Orders.Main;

namespace Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderManageView.xaml
    /// </summary>
    public partial class OrderManageView : UserControl, ICreateRegionManagerScope
    {
        public OrderManageView(IUnityContainer unityContainer)
        {
            InitializeComponent();
            this.DataContext = unityContainer.Resolve<OrderManageViewModel>();
            this.Loaded += OnLoaded;
        }
        /// <summary>
        /// Загружаем Views во вложенные регионы так как регионы в RegionManager 
        /// добавляются после загрузки View, в котором они находятся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var regionManager = (this.DataContext as IRegionManagerAware).RegionManager;
            regionManager?.RequestNavigate(RegionNames.OrdersContentRegion, "OrderItemsManageView");
            regionManager?.RequestNavigate(RegionNames.OrderDetailsRegion, "InvoiceView");
        }

        public bool CreateRegionManagerScope => true;
    }
}
