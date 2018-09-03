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

        private void SwitchOrdersContentState()
        {
            if (_ordersContentRegionState == OrdersContentRegionState.Creation)
                ProcessOrdersContentRegionState(OrdersContentRegionState.Journal);
            else
                ProcessOrdersContentRegionState(OrdersContentRegionState.Creation);
        }
        public IRegionManager RegionManager { get; set; }

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
                    OrdersContentRegionSwitchViewButtonText = "Go to journal";
                    RegionManager?.RequestNavigate(RegionNames.OrdersContentRegion, "OrderItemsManageView");
                    break;
                case OrdersContentRegionState.Journal:
                    OrdersContentRegionSwitchViewButtonText = "Go to creation";
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