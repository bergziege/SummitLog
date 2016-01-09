using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main.DesignViewModels
{
    public class SummitDesignViewModel:ItemWithNameViewModel<Summit>, ISummitViewModel
    {
        public SummitDesignViewModel()
        {
            base.LoadData(new Summit() {Name = "Gipfel 1"});
            base.DoUpdate();
            SummitNumber = "100A";
            Rating = 3.5;
        }

        /// <summary>
        ///     Liefert die Gipfelnummer
        /// </summary>
        public string SummitNumber { get; }

        /// <summary>
        ///     Liefert die Bewertung
        /// </summary>
        public double Rating { get; }
    }
}