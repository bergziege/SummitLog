using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main
{
    public interface IRouteViewModel : IItemWithNameViewModel<Route>
    {
        /// <summary>
        ///     Liefert die Bewertung
        /// </summary>
        double Rating { get; }
    }
}