using System.Collections.ObjectModel;
using System.Linq;
using SummitLog.Services.Model;
using SummitLog.UI.NameInput.DesignViewModels;

namespace SummitLog.UI.NameAndLevelInput.DesignViewModels
{
    /// <summary>
    /// Design View Model einer Ansicht zur Eingabe eines Namens und Auswahl eines Schwierigkeitsgrades einer Skala
    /// </summary>
    public class NameAndLevelInputDesignViewModel : NameInputDesignViewModel, INameAndLevelInputViewModel
    {
        /// <summary>
        /// Liefert eine neue INstanz des Design View Models
        /// </summary>
        public NameAndLevelInputDesignViewModel()
        {
            Name = "Weg 1";

            DifficultyLevelScales.Add(new DifficultyLevelScale() {Name = "Sächsisch"});
            SelectedDifficultyLevelScale = DifficultyLevelScales.First();

            DifficultyLevels.Add(new DifficultyLevel() {Name = "IV", Score = 500});
            SelectedDifficultyLevel = DifficultyLevels.First();
        }

        /// <summary>
        /// Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        public ObservableCollection<DifficultyLevelScale> DifficultyLevelScales { get; } = new ObservableCollection<DifficultyLevelScale>();

        /// <summary>
        /// Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        public DifficultyLevelScale SelectedDifficultyLevelScale { get; set; }

        /// <summary>
        /// Liefert die Liste der Schwierigkeitsgrade der gewählten Skala
        /// </summary>
        public ObservableCollection<DifficultyLevel> DifficultyLevels { get; } = new ObservableCollection<DifficultyLevel>();

        /// <summary>
        /// Liefert oder setzt den gesählten Schwierigkeitsgrad
        /// Liefert oder setzt den gesählten Schwierigkeitsgrad
        /// </summary>
        public DifficultyLevel SelectedDifficultyLevel { get; set; }

        /// <summary>
        /// Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
        }
    }
}