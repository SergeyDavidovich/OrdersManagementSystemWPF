using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.Practices.Unity;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace OMS.ViewModels
{
    public class CommonViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public CommonViewModel(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
            NavigateToCommand = new DelegateCommand<string>(NavigateContentTo);
            NavigateToManageEntityViewCommand = new DelegateCommand<string>(NavigateToManageEntityView);
            GlobalCommands.NavigateToManageEntityViewCompositeCommand.RegisterCommand(NavigateToManageEntityViewCommand);
            GlobalCommands.NavigateToCompositeCommand.RegisterCommand(NavigateToCommand);
        }

        #region Commands
        public DelegateCommand<string> NavigateToManageEntityViewCommand { get; set; }
        private void NavigateToManageEntityView(string entityName)
        {
            var navParamaters = new NavigationParameters() { { "Entity", entityName } };
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ManageEntityView", navParamaters);
        }
        public DelegateCommand<string> NavigateToCommand { get; set; }

        private void NavigateContentTo(string target)
        {
            _regionManager.RequestNavigate("ContentRegion", target);
        }

        #endregion

    }
}
