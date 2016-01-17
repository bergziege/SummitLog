using System.Windows;
using System.Windows.Input;

namespace SummitLog.UI.LogEntryInput
{
    /// <summary>
    ///     Interaction logic for LogEntryInputView.xaml
    /// </summary>
    public partial class LogEntryInputView : Window
    {
        public LogEntryInputView()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}