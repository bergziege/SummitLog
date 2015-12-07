using System.Collections.ObjectModel;
using System.Linq;
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
            DifficultyLevels.Add(new ItemWIthNameAndScoreDesignViewModel());
            DifficultyLevels.Add(new ItemWIthNameAndScoreDesignViewModel());

            SelectedDifficultyLevel = DifficultyLevels.First();
        }

        /// <summary>
        ///     Liefert ein Command um ein neuen Schwierigkeitsgrad hinzuzufügen
        /// </summary>
        public RelayCommand AddDifficultyLevelCommand { get; }

        /// <summary>
        ///     Liefert die Liste aller Schwierigkeitsgrade
        /// </summary>
        public ObservableCollection<IItemWithNameAndScoreViewModel> DifficultyLevels { get; } = new ObservableCollection<IItemWithNameAndScoreViewModel>();

        /// <summary>
        ///     Liefert oder setzt das gewählte <see cref="DifficultyLevel" />
        /// </summary>
        public IItemWithNameAndScoreViewModel SelectedDifficultyLevel { get; set; }

        /// <summary>
        ///     Liefert ein Command um das gewählte <see cref="DifficultyLevel" /> zu löschen
        /// </summary>
        public RelayCommand DeleteSelectedDifficultyLevelCommand { get; }

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