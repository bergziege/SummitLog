using System.Windows;
using System.Windows.Input;

namespace SummitLog.UI.NameAndScoreInput
{
    /// <summary>
    ///     Interaction logic for NameAndScoreInputView.xaml
    /// </summary>
    public partial class NameAndScoreInputView : Window
    {
        public NameAndScoreInputView()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}