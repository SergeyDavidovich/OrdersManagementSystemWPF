using DAL_LocalDb;
using Microsoft.Practices.Unity;
using Orders.ViewModels;
using Prism.Regions;
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

using Prism.Events;

namespace Orders.Views
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    /// 
    public partial class InvoiceView : UserControl
    {
        public InvoiceView(IUnityContainer unityContainer, EventAggregator eventAggregator)
        {
            InitializeComponent();

            eventAggregator.GetEvent<Orders.Events.OnOrderRequest>().Subscribe(OnOrderRequestHandle);

            //var vm = unityContainer.Resolve<InvoiceViewModel>();
            ////var vm = unityContainer.Resolve(typeof(InvoiceViewModel));

            //this.DataContext = vm;

        }

        private void InvoiceView_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Viewer.ReportPath = @"..\..\..\Orders\Reports\OrderReport.rdlc";

            //this.Viewer.DataSources.Clear();

            //this.Viewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            //{
            //    Name = "Orders",
            //    Value = (this.DataContext as InvoiceViewModel).OrderList
            //});


            //this.Viewer.RefreshReport();

        }

        private void SetDataSources(int orderId)
        {
            this.Viewer.DataSources.Clear();

            var db = new LocalDbContext();

            this.Viewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "OrderDetails",
                Value = db.Order_Details.Where(o => o.OrderID == orderId)
            });

            this.Viewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "Orders",
                Value = db.Orders.Where(o => o.OrderID == orderId)
            });

        }

        private void OnOrderRequestHandle(int orderId)
        {
            SetDataSources(orderId);
            this.Viewer.RefreshReport();
        }
    }
}
