using System.Collections.ObjectModel;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelScaleManagement
{
    /// <summary>
    ///     Schnittstelle für View Models zur Verwaltung von Schwierigkeitsgradskalen
    /// </summary>
    public interface IDifficultyLevelScaleManagementViewModel: IReactiveObject
    {
        /// <summary>
        ///     LIefert ein Command um eine neue Schwierigkeitsgradskala hinzuzufügen
        /// </summary>
        RelayCommand AddDifficultyLevelScaleCommand { get; }

        /// <summary>
        ///     Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        ObservableCollection<DifficultyLevelScale> DifficultyLevelScales { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        DifficultyLevelScale SelectedDifficultyLevelScale { get; set; }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        void LoadData();
    }
}