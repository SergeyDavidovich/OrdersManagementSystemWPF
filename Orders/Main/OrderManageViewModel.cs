using Infrastructure;
using Infrastructure.Base;
using Infrastructure.Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Orders.Main
{
    public class OrderManageViewModel : NavigationAwareViewModelBase, IRegionManagerAware
    {
        #region Declarations

        private OrdersContentRegionState _ordersContentRegionState;

        private string _ordersContentRegionSwitchViewButtonText;

        #endregion

        public OrderManageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            SwitchOrdersContentStateCommand = new DelegateCommand(SwitchOrdersContentState);
            _ordersContentRegionState = OrdersContentRegionState.Creation;
            ProcessOrdersContentRegionState(_ordersContentRegionState);
        }

        #region Bindable properties

        public string OrdersContentRegionSwitchViewButtonText
        {
            get => _ordersContentRegionSwitchViewButtonText;
            set => SetProperty(ref _ordersContentRegionSwitchViewButtonText, value);
        }

        #endregion

        #region IRegionManagerAware implementation

        public IRegionManager RegionManager { get; set; }

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
                    RegionManager?.RequestNavigate(RegionNames.OrdersContentRegion, "CreateView");
                    break;
                case OrdersContentRegionState.Journal:
                    OrdersContentRegionSwitchViewButtonText = "TO CREATION";
                    RegionManager?.RequestNavigate(RegionNames.OrdersContentRegion, "JournalView");
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