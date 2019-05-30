using Logging;
using MVVMUtilities.Types;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFUtilities.Converters;
using WPFUtilities.Extensions;

namespace WPFControls
{
    public class LogEntry
    {
        public LogEntry(LogType type, DateTime time, string message)
        {
            Type = type;
            Time = time;
            Message = message;
        }

        public LogType Type { get; }
        public DateTime Time { get; }
        public string Message { get; }
    }

    public class LogTypeToIconConverter : SmartConverterTemplate<LogType, BitmapImage>
    {
        readonly BitmapImage _information = Properties.Resources.InformationMark.ToBitmapImage();
        readonly BitmapImage _warning = Properties.Resources.WarningMark.ToBitmapImage();
        readonly BitmapImage _error = Properties.Resources.ErrorMark.ToBitmapImage();

        public override BitmapImage Convert(LogType value)
        {
            switch (value)
            {
                case LogType.INFO:
                    return _information;
                case LogType.WARNING:
                    return _warning;
                case LogType.ERROR:
                    return _error;

                default:
                    throw new NotSupportedException();
            }
        }
    }

    public class DateTimeToTimeStringConverter : SmartConverterTemplate<DateTime, string>
    {
        public override string Convert(DateTime time)
        {
            return time.ToString("hh:mm");
        }
    }

    /// <summary>
    /// Interaction logic for LogControl.xaml
    /// </summary>
    public partial class LogControl : UserControl
    {
        // Using a DependencyProperty as the backing store for Entries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntriesProperty =
            DependencyProperty.Register("Entries", typeof(EnhancedObservableCollection<LogEntry>), typeof(LogControl), new PropertyMetadata(null, entriesChanged));

        public EnhancedObservableCollection<LogEntry> Entries
        {
            get { return (EnhancedObservableCollection<LogEntry>)GetValue(EntriesProperty); }
            set { SetValue(EntriesProperty, value); }
        }

        public LogControl()
        {
            InitializeComponent();
        }

        static void entriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ui = ((LogControl)d);
            var old = (EnhancedObservableCollection<LogEntry>)e.OldValue;
            var @new = (EnhancedObservableCollection<LogEntry>)e.NewValue;
            @new.CollectionChanged += ui.scrollToEnd;
            if (old != null)
            {
                old.CollectionChanged -= ui.scrollToEnd;
            }
            ui.scrollToEnd(null, null);
        }

        void scrollToEnd(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            sv_Scroll.ScrollToEnd();
        }
    }
}
