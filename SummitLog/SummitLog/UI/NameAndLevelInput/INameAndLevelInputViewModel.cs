using System.Collections.ObjectModel;
using SummitLog.Services.Model;
using SummitLog.UI.NameInput;

namespace SummitLog.UI.NameAndLevelInput
{
    /// <summary>
    /// Schnittstelle für View Models iener View zur Eingabe eines Namens und eines Schwerigekeitsgrades
    /// </summary>
    public interface INameAndLevelInputViewModel : INameInputViewModel
    {
        /// <summary>
        /// Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        ObservableCollection<DifficultyLevelScale> DifficultyLevelScales { get; }

        /// <summary>
        /// Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        DifficultyLevelScale SelectedDifficultyLevelScale { get; set; }

        /// <summary>
        /// Liefert die Liste der Schwierigkeitsgrade der gewählten Skala
        /// </summary>
        ObservableCollection<DifficultyLevel> DifficultyLevels { get; }

        /// <summary>
        /// Liefert oder setzt den gesählten Schwierigkeitsgrad
        /// Liefert oder setzt den gesählten Schwierigkeitsgrad
        /// </summary>
        DifficultyLevel SelectedDifficultyLevel { get; set; }

        /// <summary>
        /// Lädt die VM relevanten Daten
        /// </summary>
        void LoadData();
    }
}