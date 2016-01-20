using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main.ViewModels
{
    public class SummitViewModel:ItemWithNameViewModel<Summit>, ISummitViewModel
    {
        private string _summitNumber;
        private double _rating;

        /// <summary>
        ///     LÄdt die VM relevanten Daten
        /// </summary>
        /// <param name="item"></param>
        public override void LoadData(Summit item)
        {
            base.LoadData(item);
            SummitNumber = item.SummitNumber;
            Rating = item.Rating;
        }

        /// <summary>
        ///     Aktualisiert das View Model
        /// </summary>
        public override void DoUpdate()
        {
            base.DoUpdate();
            this.RaisePropertyChanged("SummitNumber");
            this.RaisePropertyChanged("Rating");
        }

        /// <summary>
        ///     Liefert die Gipfelnummer
        /// </summary>
        public string SummitNumber
        {
            get { return _summitNumber; }
            private set { this.RaiseAndSetIfChanged(ref _summitNumber, value); }
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