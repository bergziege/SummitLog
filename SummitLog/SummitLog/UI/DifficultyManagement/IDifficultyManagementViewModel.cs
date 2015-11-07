using System.Windows;
using SummitLog.UI.DifficultyLevelManagement;
using SummitLog.UI.DifficultyLevelScaleManagement;

namespace SummitLog.UI.DifficultyManagement
{
    /// <summary>
    ///     Schnitstelle für View Models für die Verwaltung von Schwierigkeitsgraden und Skalen
    /// </summary>
    public interface IDifficultyManagementViewModel: IWeakEventListener
    {
        /// <summary>
        ///     Liefert ein View Model zur Schwierigkeitsgradverwaltung
        /// </summary>
        IDifficultyLevelManagementViewModel DifficultyLevelManagementViewModel { get; }

        /// <summary>
        ///     Liefert ein View Model zu Schwierigkeitsgradskalenverwaltung
        /// </summary>
        IDifficultyLevelScaleManagementViewModel DifficultyLevelScaleManagementViewModel { get; }
    }
}