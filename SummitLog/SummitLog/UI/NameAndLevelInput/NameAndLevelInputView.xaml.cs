using System.Windows;
using System.Windows.Input;

namespace SummitLog.UI.NameAndLevelInput
{
    /// <summary>
    ///     Interaction logic for NameAndLevelInputView.xaml
    /// </summary>
    public partial class NameAndLevelInputView : Window
    {
        public NameAndLevelInputView()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}