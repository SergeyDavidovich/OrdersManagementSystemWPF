using Prism.Mvvm;

namespace OMS.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            this.State = "Dashboard";
        }

        #region Bindable properties

        private string _title = "Orders Management System";
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
