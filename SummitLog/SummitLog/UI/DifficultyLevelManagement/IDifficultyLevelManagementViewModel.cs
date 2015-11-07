using System.Collections.ObjectModel;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelManagement
{
    /// <summary>
    ///     Schnittstelle für View Models zur Verwaltung von Schwierigkeitsgraden
    /// </summary>
    public interface IDifficultyLevelManagementViewModel
    {
        /// <summary>
        ///     Liefert ein Command um ein neuen Schwierigkeitsgrad hinzuzufügen
        /// </summary>
        RelayCommand AddDifficultyLevelCommand { get; }

        /// <summary>
        ///     Liefert die Liste aller Schwierigkeitsgrade
        /// </summary>
        ObservableCollection<DifficultyLevel> DifficultyLevels { get; }

        /// <summary>
        ///     LÄdt die VM relevanten Daten zu einer Schwierigkeitsgradskale
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        void LoadData(DifficultyLevelScale difficultyLevelScale);
    }
}