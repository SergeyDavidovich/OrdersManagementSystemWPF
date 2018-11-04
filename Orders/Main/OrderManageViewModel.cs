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
        private OrdersContentRegionState _ordersContentRegionState;

        private string _ordersContentRegionSwitchViewButtonText;

        public OrderManageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            SwitchOrdersContentStateCommand = new DelegateCommand(SwitchOrdersContentState);
            _ordersContentRegionState = OrdersContentRegionState.Creation;
            ProcessOrdersContentRegionState(_ordersContentRegionState);
        }

        public string OrdersContentRegionSwitchViewButtonText
        {
            get => _ordersContentRegionSwitchViewButtonText;
            set => SetProperty(ref _ordersContentRegionSwitchViewButtonText, value);
        }

        public DelegateCommand SwitchOrdersContentStateCommand { get; set; }

        public IRegionManager RegionManager { get; set; }

        private void SwitchOrdersContentState()
        {
            if (_ordersContentRegionState == OrdersContentRegionState.Creation)
                ProcessOrdersContentRegionState(OrdersContentRegionState.Journal);
            else
                ProcessOrdersContentRegionState(OrdersContentRegionState.Creation);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            UpdateBannerTitle("Orders");
        }

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
    }
}