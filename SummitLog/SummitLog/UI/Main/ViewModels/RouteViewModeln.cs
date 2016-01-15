using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main.ViewModels
{
    public class RouteViewModel : ItemWithNameViewModel<Route>, IRouteViewModel
    {
        private double _rating;
        private string _summitNumber;

        /// <summary>
        ///     LÄdt die VM relevanten Daten
        /// </summary>
        /// <param name="item"></param>
        public override void LoadData(Route item)
        {
            base.LoadData(item);
            Rating = item.Rating;
        }

        /// <summary>
        ///     Aktualisiert das View Model
        /// </summary>
        public override void DoUpdate()
        {
            base.DoUpdate();
            this.RaisePropertyChanged("Rating");
        }

        /// <summary>
        ///     Liefert die Bewertung
        /// </summary>
        public double Rating
        {
            get { return _rating; }
            private set { this.RaiseAndSetIfChanged(ref _rating, value); }
        }
    }
}