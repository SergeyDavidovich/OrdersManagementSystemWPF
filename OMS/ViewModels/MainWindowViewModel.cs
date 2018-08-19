using System.Linq;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace OMS.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public MainWindowViewModel(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.State = "Dashboard";
            _regionManager = regionManager;
            _unityContainer = unityContainer;
            NavigateToCommand = new DelegateCommand<string>(NavigateContentTo);
            NavigateHomeCommand = new DelegateCommand(NavigateHome);
            NavigateToManageEntityViewCommand = new DelegateCommand<string>(NavigateToManageEntityView);
            GlobalCommands.NavigateToManageEntityViewCompositeCommand.RegisterCommand(NavigateToManageEntityViewCommand);
            GlobalCommands.NavigateToCompositeCommand.RegisterCommand(NavigateToCommand);
        }
        public void ConfigureRegionManager()
        {
            _regionManager.AddToRegion("ContentRegion", _unityContainer.Resolve<Dashboard.Views.DashboardView>());
        }

        #region Commands
        
        public DelegateCommand<string> NavigateToManageEntityViewCommand { get; set; }
        private void NavigateToManageEntityView(string entityName)
        {
            var navParamaters = new NavigationParameters(){ {"Entity", entityName } };
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ManageEntityView", navParamaters);
        }
        public DelegateCommand<string> NavigateToCommand { get; set; }
        private void NavigateContentTo(string target)
        {
            _regionManager.RequestNavigate("ContentRegion", target);
        }

        public DelegateCommand NavigateHomeCommand { get; set; }
        private void NavigateHome()
        {
            var contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            foreach (var view in contentRegion.ActiveViews)
            {
                contentRegion.Deactivate(view);
            }
            contentRegion.Activate(contentRegion.Views.FirstOrDefault(v => v is Dashboard.Views.DashboardView));
             
        }
        #endregion
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
