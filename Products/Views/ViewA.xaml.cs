using Products.ViewModels;
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

namespace Products.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewA : UserControl
    {
        public ViewA()
        {
            //var a = new Page();
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        ViewAViewModel _viewModel;
        public ViewAViewModel ViewModel
        {
            get { return _viewModel ?? (_viewModel = (ViewAViewModel)DataContext); }
        }
    }
}
