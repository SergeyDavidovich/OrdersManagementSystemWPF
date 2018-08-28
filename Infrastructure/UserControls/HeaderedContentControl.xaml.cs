using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Infrastructure.UserControls
{
    /// <summary>
    /// Interaction logic for HeaderedContentControl.xaml
    /// </summary>
    public partial class HeaderedContentControl : UserControl
    {
        public HeaderedContentControl()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ChildContentProperty =
            DependencyProperty.Register("ChildContent", typeof(UIElement), typeof(HeaderedContentControl), new PropertyMetadata(null));

        public UIElement ChildContent
        {
            get { return (UIElement)GetValue(ChildContentProperty); }
            set { SetValue(ChildContentProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(HeaderedContentControl));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ManageButtonCommandProperty = DependencyProperty.Register(
            "ManageButtonCommand", typeof(ICommand), typeof(HeaderedContentControl), new PropertyMetadata(default(ICommand)));

        public ICommand ManageButtonCommand
        {
            get { return (ICommand)GetValue(ManageButtonCommandProperty); }
            set { SetValue(ManageButtonCommandProperty, value); }
        }

        public static readonly DependencyProperty ManageButtonCommandParamaterProperty = DependencyProperty.Register(
            "ManageButtonCommandParamater", typeof(object), typeof(HeaderedContentControl), new PropertyMetadata(default(object)));

        public object ManageButtonCommandParamater
        {
            get { return (object)GetValue(ManageButtonCommandParamaterProperty); }
            set { SetValue(ManageButtonCommandParamaterProperty, value); }
        }

        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.Register(
            "AccentColor", typeof(Brush), typeof(HeaderedContentControl), new PropertyMetadata(default(Brush)));

        public Brush AccentColor
        {
            get { return (Brush) GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }
    }
}
