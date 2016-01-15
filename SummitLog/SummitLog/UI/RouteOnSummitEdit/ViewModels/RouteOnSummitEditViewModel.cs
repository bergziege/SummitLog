using ReactiveUI;
using SummitLog.UI.NameInput.ViewModels;

namespace SummitLog.UI.RouteOnSummitEdit.ViewModels
{
    public class RouteOnSummitEditViewModel : NameInputViewModel, IRouteOnSummitEditViewModel
    {
        private double _rating;

        public double Rating
        {
            get { return _rating; }
            set { this.RaiseAndSetIfChanged(ref _rating, value); }
        }
    }
}