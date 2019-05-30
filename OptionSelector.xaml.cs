using MVVMUtilities.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Utilities.Extensions;
using WPFUtilities.Types;

namespace WPFControls
{
    public class OptionCheckedEventArgs
    {
        public BoolOption Option { get; }

        public OptionCheckedEventArgs(BoolOption option)
        {
            Option = option;
        }
    }

    public class BoolOptionsCollection : ObservableCollection<BoolOption>
    {

    }

    public class BoolOption : NotifiableObjectTemplate
    {
        public object Tag { get; set; }

        public bool IsEnabled
        {
            get => _propertyHolder.Get(() => true);
            set => _propertyHolder.Set(value);
        }

        public bool IsChecked
        {
            get => _propertyHolder.Get(() => false);
            set => _propertyHolder.Set(value);
        }

        public string OptionName
        {
            get => _propertyHolder.GetOrNull();
            set => _propertyHolder.Set(value);
        }
    }

    public partial class OptionSelector : UserControl
    { 
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register(nameof(GroupName), typeof(string),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register(nameof(Options), typeof(ObservableCollection<BoolOption>),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            new PropertyMetadata(optionsChanged));
        public static readonly DependencyPropertyKey OptionPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Option), typeof(BoolOption),
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            new PropertyMetadata(null));
        public static readonly DependencyProperty OptionProperty
            = OptionPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ShowGroupNameProperty =
            DependencyProperty.Register("ShowGroupName", typeof(bool), typeof(OptionSelector), new PropertyMetadata(true));

        public event EventHandler<OptionCheckedEventArgs> Checked;

        public bool ShowGroupName
        {
            get { return (bool)GetValue(ShowGroupNameProperty); }
            set { SetValue(ShowGroupNameProperty, value); }
        }
        public string GroupName
        {
            get { return (String)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }
        public ObservableCollection<BoolOption> Options
        {
            get { return (ObservableCollection<BoolOption>)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }
        public BoolOption Option
        {
            get { return (BoolOption)GetValue(OptionProperty); }
            protected set { SetValue(OptionPropertyKey, value); }
        }

        public OptionSelector()
        {
            InitializeComponent();
        }

        void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var checkedOption = ((BoolOption)((RadioButton)sender).Tag);
            Option = checkedOption;
            Checked?.Invoke(this, new OptionCheckedEventArgs(checkedOption));
        }

        static void optionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var os = ((OptionSelector)d);
            os.Option = os.Options.FirstOrDefault(o => o.IsChecked && o.IsEnabled) ?? null;
        }
    }
}
