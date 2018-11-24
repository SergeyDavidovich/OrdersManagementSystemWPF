using Infrastructure;
using Infrastructure.Base;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Orders.Main
{
    public class OrderManageViewModel : NavigationAwareViewModelBase
    {
        #region Declarations

        private OrdersContentRegionState _ordersContentRegionState;
        private IRegionManager _regionManager;
        private string _ordersContentRegionSwitchViewButtonText;

        #endregion

        public OrderManageViewModel(IEventAggregator eventAggregator, IRegionManager regionManager) : base(eventAggregator)
        {
            SwitchOrdersContentStateCommand = new DelegateCommand(SwitchOrdersContentState);
            _ordersContentRegionState = OrdersContentRegionState.Creation;
            ProcessOrdersContentRegionState(_ordersContentRegionState);
            _regionManager = regionManager;
        }

        #region Bindable properties

        public string OrdersContentRegionSwitchViewButtonText
        {
            get => _ordersContentRegionSwitchViewButtonText;
            set => SetProperty(ref _ordersContentRegionSwitchViewButtonText, value);
        }

        #endregion

        #region Commands

        public DelegateCommand SwitchOrdersContentStateCommand { get; set; }

        private void SwitchOrdersContentState()
        {
            if (_ordersContentRegionState == OrdersContentRegionState.Creation)
                ProcessOrdersContentRegionState(OrdersContentRegionState.Journal);
            else
                ProcessOrdersContentRegionState(OrdersContentRegionState.Creation);
        }
        #endregion

        #region Navigatet Events handlers
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _regionManager.RequestNavigate(RegionNames.OrdersContentRegion, "CreateView");
            _regionManager.RequestNavigate(RegionNames.OrderDetailsRegion, "InvoiceView");
            UpdateBannerTitle("Orders");
        }


        #endregion

        #region State Processing

        private void ProcessOrdersContentRegionState(OrdersContentRegionState state)
        {
            _ordersContentRegionState = state;
            switch (state)
            {
                case OrdersContentRegionState.Creation:
                    OrdersContentRegionSwitchViewButtonText = "TO JOURNAL";
                    _regionManager?.RequestNavigate(RegionNames.OrdersContentRegion, "CreateView");
                    break;
                case OrdersContentRegionState.Journal:
                    OrdersContentRegionSwitchViewButtonText = "TO CREATION";
                    _regionManager?.RequestNavigate(RegionNames.OrdersContentRegion, "JournalView");
                    break;
            }
        }

        private enum OrdersContentRegionState
        {
            Creation,
            Journal
        }

        #endregion
    }
}