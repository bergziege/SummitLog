using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelScaleManagement
{
    /// <summary>
    ///     Schnittstelle für View Models einer Schwierigkeitsgrad Skala
    /// </summary>
    public interface IDifficultyLevelScaleItemViewModel : IItemWithNameViewModel<DifficultyLevelScale>
    {
        /// <summary>
        ///     Liefert ob es sich um eine Standardskala handelt.
        /// </summary>
        bool IsDefault { get; }
    }
}