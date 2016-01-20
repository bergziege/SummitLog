using System.Windows;
using System.Windows.Input;

namespace SummitLog.UI.RouteOnSummitEdit
{
    /// <summary>
    ///     Interaktionslogik für RouteOnSummitEditView.xaml
    /// </summary>
    public partial class RouteOnSummitEditView : Window
    {
        public RouteOnSummitEditView()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}