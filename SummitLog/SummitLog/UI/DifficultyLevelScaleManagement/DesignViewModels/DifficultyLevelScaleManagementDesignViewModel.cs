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
            DifficultyLevelScales.Add(new DifficultyLevelScaleItemDesignViewModel());
            DifficultyLevelScales.Add(new DifficultyLevelScaleItemDesignViewModel());
        }

        /// <summary>
        ///     LIefert ein Command um eine neue Schwierigkeitsgradskala hinzuzufügen
        /// </summary>
        public RelayCommand AddDifficultyLevelScaleCommand { get; }

        /// <summary>
        ///     Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        public ObservableCollection<IDifficultyLevelScaleItemViewModel> DifficultyLevelScales { get; } =new ObservableCollection<IDifficultyLevelScaleItemViewModel>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        public IDifficultyLevelScaleItemViewModel SelectedDifficultyLevelScale { get; set; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Schwierigkeitsgradskala zu löschen, wenn diese nicht mehr verwendet wird.
        /// </summary>
        public RelayCommand DeleteSelectedDifficultyLevelScaleCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Schwierigkeitsgradskala zu bearbeiten.
        /// </summary>
        public RelayCommand EditSelectedDifficultyLevelScaleCommand { get; }

        /// <summary>
        ///     Liefert ein Relay Command um die gewählte Schwierigkeitsgradskale als Standard zu setzen.
        /// </summary>
        public RelayCommand SetSelectedAsDefaultCommand { get; }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            throw new System.NotImplementedException();
        }
    }
}