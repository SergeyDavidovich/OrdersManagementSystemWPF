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
        public InvoiceView(IUnityContainer unityContainer)
        {
            InitializeComponent();


            //var vm = unityContainer.Resolve<InvoiceViewModel>();
            ////var vm = unityContainer.Resolve(typeof(InvoiceViewModel));

            //this.DataContext = vm;

        }

        private void InvoiceView_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Viewer.ReportPath = @"..\..\..\Orders\Reports\OrderReport.rdlc";

            this.Viewer.DataSources.Clear();

            var db = new LocalDbContext();

            this.Viewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "Order",
                Value = db.Orders
            });

            this.Viewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "OrderDetails",
                Value = db.Order_Details
            });

            //this.Viewer.RefreshReport();

        }

        private void Viewer_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            //this.Viewer.RefreshReport();

        }
    }
}
