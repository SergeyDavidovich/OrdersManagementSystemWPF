using Microsoft.Practices.Unity;
using Orders.ViewModels;
using System.Windows.Controls;
using Orders.Events;
using Prism.Events;
using System.IO;
using System.Diagnostics;

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
            var vm = unityContainer.Resolve<InvoiceViewModel>();
            this.DataContext = vm;

            eventAggregator.GetEvent<OrderDataCreated>().Subscribe(SetDataSources);

            //this.Viewer.ReportPath = @"..\..\..\Orders\Reports\OrderReport.rdlc";
            string currentDir = Directory.GetCurrentDirectory() + "\\Reports\\OrderReport.rdlc";

#if DEBUG
            Debug.WriteLine($"Текущая директория: {Directory.GetCurrentDirectory()}");
            Debug.WriteLine($"Путь к отчету: { currentDir}");
#endif
            this.Viewer.ReportPath = currentDir;
        }


        private void SetDataSources()
        {
            this.Viewer.DataSources.Clear();

            var orders = (this.DataContext as InvoiceViewModel).Orders;
            var orderDetails = (this.DataContext as InvoiceViewModel).OrderDetails;

            this.Viewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "Orders",
                Value = orders
            });

            this.Viewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "OrderDetails",
                Value = orderDetails
            });

            this.Viewer.RefreshReport();
        }
    }
}
