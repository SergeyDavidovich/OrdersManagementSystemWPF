//https://www.syncfusion.com/kb/3071/how-to-bind-the-selecteditems-property-of-sfdatagrid-to-viewmodel-property
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Behaviours
{
    public class SfDataGridHelper
    {
        public static void SetSelectedItems(DependencyObject element, object value)
        {
            if (element is SfDataGrid)
                element.SetValue(SelectedItemsProperty, value);
            else
                throw new NotSupportedException("This property can be used only with SfDataGrid");
        }

        public static object GetSelectedItems(DependencyObject element)
        {
            return (object)element.GetValue(SelectedItemsProperty);
        }

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached(
        "SelectedItems", typeof(ObservableCollection<Object>), typeof(SfDataGridHelper), new PropertyMetadata(new ObservableCollection<Object>(), OnSelectedItemsChanged));

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var sfDataGrid = d as SfDataGrid;
            if (sfDataGrid == null)
                return;
            //SfDataGridHelper.SelectedItems property updated based on SfDataGrid.SelectedItems Collectionchanged event.
            sfDataGrid.SelectedItems.CollectionChanged += (sender, e) =>
            {
                SfDataGridHelper.SetSelectedItems(sfDataGrid, sfDataGrid.SelectedItems);
            };
        }
    }
}
