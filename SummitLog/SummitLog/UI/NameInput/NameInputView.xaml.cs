using System.Windows;
using System.Windows.Input;

namespace SummitLog.UI.NameInput
{
    /// <summary>
    ///     Interaction logic for NameInputView.xaml
    /// </summary>
    public partial class NameInputView : Window
    {
        public NameInputView()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}