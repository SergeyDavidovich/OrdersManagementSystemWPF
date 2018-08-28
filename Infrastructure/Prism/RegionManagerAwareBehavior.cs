using System;
using System.Collections.Specialized;
using System.Windows;
using Prism.Regions;

namespace Infrastructure.Prism
{
    public class RegionManagerAwareBehavior : RegionBehavior
    {
        public const String BehaviorKey = "RegionManagerAwareBehavior";

        protected override void OnAttach()
        {
            Region.Views.CollectionChanged += Views_CollectionChanged;
        }

        void Views_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    IRegionManager regionManager = Region.RegionManager;

                    // If the view was created with a scoped region manager, the behavior uses that region manager instead.
                    FrameworkElement element = item as FrameworkElement;
                    if (element != null)
                    {
                        IRegionManager scopedRegionManager = element.GetValue(RegionManager.RegionManagerProperty) as IRegionManager;
                        if (scopedRegionManager != null)
                        {
                            regionManager = scopedRegionManager;
                        }
                    }

                    InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = regionManager);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = null);
                }
            }
        }

        private static void InvokeOnRegionManagerAwareElement(object item, Action<IRegionManagerAware> invocation)
        {
            var regionManagerAwareItem = item as IRegionManagerAware;
            if (regionManagerAwareItem != null)
            {
                invocation(regionManagerAwareItem);
            }

            FrameworkElement frameworkElement = item as FrameworkElement;
            if (frameworkElement != null)
            {
                IRegionManagerAware regionManagerAwareDataContext = frameworkElement.DataContext as IRegionManagerAware;
                if (regionManagerAwareDataContext != null)
                {
                    // If a view doesn't have a data context (view model) it will inherit the data context from the parent view.
                    // The following check is done to avoid setting the RegionManager property in the view model of the parent view by mistake. 
                    var frameworkElementParent = frameworkElement.Parent as FrameworkElement;
                    if (frameworkElementParent != null)
                    {
                        var regionManagerAwareDataContextParent = frameworkElementParent.DataContext as IRegionManagerAware;
                        if (regionManagerAwareDataContextParent != null)
                        {
                            if (regionManagerAwareDataContext == regionManagerAwareDataContextParent)
                            {
                                // If all of the previous conditions are true, it means that this view doesn't have a view model
                                // and is using the view model of its visual parent.
                                return;
                            }
                        }
                    }

                    invocation(regionManagerAwareDataContext);
                }
            }
        }
    }
}
