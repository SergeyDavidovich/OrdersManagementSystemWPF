using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Main;
using Infrastructure;
using Prism.Regions;
using Infrastructure.Base;
using Infrastructure.Events;
using MaterialDesignColors;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using Banner.Properties;

namespace Banner.ViewModels
{
    public class BannerViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private PaletteHelper helper =new PaletteHelper();

        public BannerViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            Title = "Orders Management System";
            _regionManager = regionManager;
            NavigateHomeCommand = new DelegateCommand(NavigateHome);
            ChangePaletteCommand = new DelegateCommand<string>(ChangePalete);
            ChangeThemeCommand = new DelegateCommand<bool?>(ChangeTheme);

            IsDarkTheme = Settings.Default.IsDark;
            ChangeTheme(IsDarkTheme);

            ChangePalete(Settings.Default.PaletteColor);

            eventAggregator.GetEvent<OnNavigatedToEvent>().Subscribe(title => State = title);
        }
        #region Commands
        public DelegateCommand NavigateHomeCommand { get; set; }
        private void NavigateHome()
        {
            var contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            while (contentRegion.NavigationService.Journal.CanGoBack)
            {
                contentRegion.NavigationService.Journal.GoBack();
            }
        }

        public DelegateCommand<string> ChangePaletteCommand { get; set; }
        private void ChangePalete(string color)
        {
            helper = new PaletteHelper();
            var swatches = new SwatchesProvider().Swatches;
            helper.ReplacePrimaryColor(swatches.FirstOrDefault(s => s.Name == color.ToLower()));
            Settings.Default.PaletteColor = color;
            Settings.Default.Save();
        }

        public DelegateCommand<bool?> ChangeThemeCommand { get; set; }
        private void ChangeTheme(bool? isDark)
        {
            Settings.Default.IsDark = isDark.Value;
            Settings.Default.Save();

            this.IsDarkTheme = isDark.Value;

            helper.SetLightDark(isDark.Value);
        }
        #endregion

        #region Bindable properties

        private bool? isDarkTheme;
        public bool? IsDarkTheme
        {
            get => isDarkTheme.Value;
            set => SetProperty(ref isDarkTheme, value);
        }

        #endregion

        private string _state;
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

    }
}
