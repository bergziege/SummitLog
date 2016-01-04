using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main
{
    /// <summary>
    ///     Schnittstelle für View Models einer Variation
    /// </summary>
    public interface IVariationItemViewModel : IItemWithNameViewModel<Variation>
    {
        /// <summary>
        ///     Liefert den Schwierigkeitsgrad der Variation
        /// </summary>
        DifficultyLevel DifficultyLevel { get; }
    }
}