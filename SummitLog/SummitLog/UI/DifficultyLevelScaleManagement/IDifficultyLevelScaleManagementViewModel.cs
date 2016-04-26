using System.Collections.ObjectModel;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelScaleManagement
{
    /// <summary>
    ///     Schnittstelle für View Models zur Verwaltung von Schwierigkeitsgradskalen
    /// </summary>
    public interface IDifficultyLevelScaleManagementViewModel : IReactiveObject
    {
        /// <summary>
        ///     Liefert ein Command um eine neue Schwierigkeitsgradskala hinzuzufügen
        /// </summary>
        RelayCommand AddDifficultyLevelScaleCommand { get; }

        /// <summary>
        ///     Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        ObservableCollection<IDifficultyLevelScaleItemViewModel> DifficultyLevelScales { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        IDifficultyLevelScaleItemViewModel SelectedDifficultyLevelScale { get; set; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Schwierigkeitsgradskala zu löschen, wenn diese nicht mehr verwendet wird.
        /// </summary>
        RelayCommand DeleteSelectedDifficultyLevelScaleCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Schwierigkeitsgradskala zu bearbeiten.
        /// </summary>
        RelayCommand EditSelectedDifficultyLevelScaleCommand { get; }

        /// <summary>
        ///     Liefert ein Relay Command um die gewählte Schwierigkeitsgradskale als Standard zu setzen.
        /// </summary>
        RelayCommand SetSelectedAsDefaultCommand { get; }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        void LoadData();
    }
}