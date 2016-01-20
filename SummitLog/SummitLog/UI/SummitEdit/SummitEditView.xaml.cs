using System.Windows;
using System.Windows.Input;

namespace SummitLog.UI.SummitEdit
{
    /// <summary>
    ///     Interaktionslogik für SummitEditView.xaml
    /// </summary>
    public partial class SummitEditView : Window
    {
        public SummitEditView()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}