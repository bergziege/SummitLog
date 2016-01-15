using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main.DesignViewModels
{
    public class RouteDesignViewModel:ItemWithNameViewModel<Route>, IRouteViewModel
    {
        public RouteDesignViewModel()
        {
            base.LoadData(new Route() {Name = "Weg 1"});
            base.DoUpdate();
            Rating = 3.5;
        }
        
        /// <summary>
        ///     Liefert die Bewertung
        /// </summary>
        public double Rating { get; }
    }
}