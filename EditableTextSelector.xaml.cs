using MVVMUtilities.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPFControls
{
    /// <summary>
    /// Interaction logic for CheckEditableTextSelector.xaml
    /// </summary>
    public partial class EditableTextSelector : UserControl
    {
        public class Option : NotifiableObjectTemplate
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

            public IViewValueProvider<string> Value
            {
                get => _propertyHolder.GetOrNull();
                set => _propertyHolder.Set(value);
            }
        }

        public ObservableCollection<Option> Options
        {
            get { return (ObservableCollection<Option>)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        public Option SelectedOption
        {
            get { return (Option)GetValue(SelectedOptionProperty); }
            set { SetValue(SelectedOptionProperty, value); }
        }

        public bool DisableWhenSelected
        {
            get { return (bool)GetValue(DisableWhenSelectedProperty); }
            set { SetValue(DisableWhenSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisableWhenSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisableWhenSelectedProperty =
            DependencyProperty.Register("DisableWhenSelected", typeof(bool), typeof(EditableTextSelector), 
                new PropertyMetadata(true));

        // Using a DependencyProperty as the backing store for SelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedOptionProperty =
            DependencyProperty.Register("SelectedOption", typeof(Option), 
                typeof(EditableTextSelector), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register("Options", typeof(ObservableCollection<Option>),
                typeof(EditableTextSelector), new PropertyMetadata(new ObservableCollection<Option>(), optionsChanged));

        public EditableTextSelector()
        {
            InitializeComponent();
        }

        void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectedOption = (Option)((RadioButton)sender).Tag;
        }

        static void optionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ui = ((EditableTextSelector)d);
            var newOptions = (ObservableCollection<Option>)e.NewValue;
            ui.SelectedOption = newOptions.FirstOrDefault(o => o.IsChecked) ?? null;
        }

        void RadioButton_Initialized(object sender, EventArgs e)
        {
            var rb = (RadioButton)sender;
            SelectedOption = (rb.IsChecked ?? false) 
                ? (Option)rb.Tag 
                : SelectedOption;
        }
    }
}
