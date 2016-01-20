using SummitLog.UI.NameInput;

namespace SummitLog.UI.RouteOnSummitEdit
{
    public interface IRouteOnSummitEditViewModel : INameInputViewModel
    {
        double Rating { get; set; }
    }
}