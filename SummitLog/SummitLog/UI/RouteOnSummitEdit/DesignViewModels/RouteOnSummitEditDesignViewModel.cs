using SummitLog.UI.NameInput.DesignViewModels;

namespace SummitLog.UI.RouteOnSummitEdit.DesignViewModels
{
    public class RouteOnSummitEditDesignViewModel : NameInputDesignViewModel, IRouteOnSummitEditViewModel
    {
        public RouteOnSummitEditDesignViewModel()
        {
            Name = "Gipfelname";
            Rating = 3.5;
        }

        public double Rating { get; set; }
    }
}