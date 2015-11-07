using System.Collections.ObjectModel;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelScaleManagement.DesignViewModels
{
    /// <summary>
    /// Design View Model einer Ansicht zur Verwaltung der Schwierigkeitsgradskalen
    /// </summary>
    public class DifficultyLevelScaleManagementDesignViewModel: ReactiveObject, IDifficultyLevelScaleManagementViewModel
    {
        /// <summary>
        /// Liefert eine neue Instanz des Design View Models
        /// </summary>
        public DifficultyLevelScaleManagementDesignViewModel()
        {
            DifficultyLevelScales.Add(new DifficultyLevelScale() {Name = "Weltweit"});
            DifficultyLevelScales.Add(new DifficultyLevelScale() {Name = "Sächsich"});
        }

        /// <summary>
        ///     LIefert ein Command um eine neue Schwierigkeitsgradskala hinzuzufügen
        /// </summary>
        public RelayCommand AddDifficultyLevelScaleCommand { get; }

        /// <summary>
        ///     Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        public ObservableCollection<DifficultyLevelScale> DifficultyLevelScales { get; } =new ObservableCollection<DifficultyLevelScale>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        public DifficultyLevelScale SelectedDifficultyLevelScale { get; set; }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            throw new System.NotImplementedException();
        }
    }
}