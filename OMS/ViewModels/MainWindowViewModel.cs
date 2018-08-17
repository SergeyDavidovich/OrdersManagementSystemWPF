using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;

namespace OMS.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IUnityContainer _unityContainer;

        public MainWindowViewModel(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.State = "Dashboard";
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }
        public void ConfigureRegionManager()
        {
            _regionManager.AddToRegion("ContentRegion", _unityContainer.Resolve<Dashboard.Views.DashboardView>());
        }

        #region Bindable properties

        private string _title = "Orders Management";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }
        #endregion
    }
}
