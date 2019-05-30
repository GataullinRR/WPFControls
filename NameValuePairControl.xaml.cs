using MVVMUtilities.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WPFUtilities.Types;

namespace WPFControls
{
    public partial class NameValuePairControl : UserControl
    {
        public static readonly DependencyProperty ValueNameProperty =
            DependencyProperty.Register(nameof(ValueName), typeof(string),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly DependencyProperty ValueSourceProperty =
            DependencyProperty.Register(nameof(ValueSource), typeof(IViewValueProvider<string>),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            new PropertyMetadata(new DoubleMarshaller(default)));
        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register(nameof(TextBoxWidth), typeof(GridLength),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            new PropertyMetadata(new GridLength(100)));
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register(nameof(LabelWidth), typeof(GridLength),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

        public string ValueName
        {
            get { return (String)GetValue(ValueNameProperty); }
            set { SetValue(ValueNameProperty, value); }
        }

        public IViewValueProvider<string> ValueSource
        {
            get { return (IViewValueProvider<string>)GetValue(ValueSourceProperty); }
            set { SetValue(ValueSourceProperty, value); }
        }

        public GridLength TextBoxWidth
        {
            get { return (GridLength)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        public GridLength LabelWidth
        {
            get { return (GridLength)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        public NameValuePairControl()
        {
            InitializeComponent();
        }
    }
}
