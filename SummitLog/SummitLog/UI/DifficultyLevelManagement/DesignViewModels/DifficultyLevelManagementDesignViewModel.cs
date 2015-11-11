using System.Collections.ObjectModel;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelManagement.DesignViewModels
{
    /// <summary>
    ///     Design View Model einer Ansicht zur Verwaltung der Schwierigkeitagrade zu einer Skala
    /// </summary>
    public class DifficultyLevelManagementDesignViewModel: ReactiveObject, IDifficultyLevelManagementViewModel
    {
        /// <summary>
        /// Liefert eine neue Instanz des Design View Models
        /// </summary>
        public DifficultyLevelManagementDesignViewModel()
        {
            DifficultyLevels.Add(new DifficultyLevel() {Name = "Level 1", Score = 100});
            DifficultyLevels.Add(new DifficultyLevel() {Name = "Level 2", Score = 200});
        }

        /// <summary>
        ///     Liefert ein Command um ein neuen Schwierigkeitsgrad hinzuzufügen
        /// </summary>
        public RelayCommand AddDifficultyLevelCommand { get; }

        /// <summary>
        ///     Liefert die Liste aller Schwierigkeitsgrade
        /// </summary>
        public ObservableCollection<DifficultyLevel> DifficultyLevels { get; } = new ObservableCollection<DifficultyLevel>();

        /// <summary>
        ///     LÄdt die VM relevanten Daten zu einer Schwierigkeitsgradskale
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        public void LoadData(DifficultyLevelScale difficultyLevelScale)
        {
            throw new System.NotImplementedException();
        }
    }
}