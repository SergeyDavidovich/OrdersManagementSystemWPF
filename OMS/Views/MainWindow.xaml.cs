using System.Windows;
using OMS.ViewModels;
using System;
using System.Deployment.Application;

namespace OMS.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                this.Title =
                   $"Deployment version:  {ad.CurrentVersion.ToString()}";
            }
        }
    }
}
