using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Utilities;

namespace WPFControls
{
    public partial class ImmutableNameValuePair : UserControl
    {
        public static readonly DependencyProperty ValueNameProperty =
            DependencyProperty.Register(nameof(ValueName), typeof(string),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(string),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly DependencyProperty LinkProperty =
            DependencyProperty.Register(nameof(Link), typeof(string),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string ValueName
        {
            get { return (string)GetValue(ValueNameProperty); }
            set { SetValue(ValueNameProperty, value); }
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string Link
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public ImmutableNameValuePair()
        {
            InitializeComponent();
        }

        void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            CommonUtils.Try(() => Process.Start(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
