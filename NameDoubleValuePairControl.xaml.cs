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
    public partial class NameDoubleValuePairControl : UserControl
    {
        public static readonly DependencyProperty ValueNameProperty =
            DependencyProperty.Register(nameof(ValueName), typeof(string),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(DoubleMarshaller),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            new PropertyMetadata(new DoubleMarshaller(default)));

        public string ValueName
        {
            get { return (String)GetValue(ValueNameProperty); }
            set { SetValue(ValueNameProperty, value); }
        }

        public DoubleMarshaller Value
        {
            get { return (DoubleMarshaller)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public NameDoubleValuePairControl()
        {
            InitializeComponent();
        }
    }
}
