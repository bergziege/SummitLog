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
        }

        /// <summary>
        ///     Liefert die Gipfelnummer
        /// </summary>
        public string SummitNumber { get; }
    }
}