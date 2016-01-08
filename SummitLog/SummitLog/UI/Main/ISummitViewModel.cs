using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main
{
    public interface ISummitViewModel : IItemWithNameViewModel<Summit>
    {
        /// <summary>
        ///     Liefert die Gipfelnummer
        /// </summary>
        string SummitNumber { get; }
    }
}