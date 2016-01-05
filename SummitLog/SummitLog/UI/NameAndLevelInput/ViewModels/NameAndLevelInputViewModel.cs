using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.NameInput.ViewModels;

namespace SummitLog.UI.NameAndLevelInput.ViewModels
{
    /// <summary>
    /// View Model einer Ansicht zur Eingabe eines Namens sowie Auswahl einer Schwierigkeit einer Skala
    /// </summary>
    public class NameAndLevelInputViewModel : NameInputViewModel, INameAndLevelInputViewModel
    {
        private readonly IDifficultyLevelScaleService _difficultyLevelScaleService;
        private readonly IDifficultyLevelService _difficultyLevelService;
        private DifficultyLevelScale _selectedDifficultyLevelScale;
        private DifficultyLevel _selectedDifficultyLevel;

        /// <summary>
        ///     Liefert ob der Name benötigt wird
        /// </summary>
        public override bool RequiresName => false;

        /// <summary>
        /// Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="difficultyLevelScaleService"></param>
        /// <param name="difficultyLevelService"></param>
        public NameAndLevelInputViewModel(IDifficultyLevelScaleService difficultyLevelScaleService, IDifficultyLevelService difficultyLevelService)
        {
            _difficultyLevelScaleService = difficultyLevelScaleService;
            _difficultyLevelService = difficultyLevelService;
        }

        /// <summary>
        /// Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        public ObservableCollection<DifficultyLevelScale> DifficultyLevelScales { get; } = new ObservableCollection<DifficultyLevelScale>();

        /// <summary>
        /// Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        public DifficultyLevelScale SelectedDifficultyLevelScale
        {
            get { return _selectedDifficultyLevelScale; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedDifficultyLevelScale, value);
                UpdateLevels();
            }
        }

        private void UpdateLevels()
        {
            if (SelectedDifficultyLevelScale != null)
            {
                DifficultyLevels.Clear();
                foreach (DifficultyLevel difficultyLevel in _difficultyLevelService.GetAllIn(SelectedDifficultyLevelScale))
                {
                    DifficultyLevels.Add(difficultyLevel);
                }
            }
        }

        /// <summary>
        /// Liefert die Liste der Schwierigkeitsgrade der gewählten Skala
        /// </summary>
        public ObservableCollection<DifficultyLevel> DifficultyLevels { get; } = new ObservableCollection<DifficultyLevel>();

        /// <summary>
        /// Liefert oder setzt den gesählten Schwierigkeitsgrad
        /// Liefert oder setzt den gesählten Schwierigkeitsgrad
        /// </summary>
        public DifficultyLevel SelectedDifficultyLevel
        {
            get { return _selectedDifficultyLevel; }
            set { this.RaiseAndSetIfChanged(ref _selectedDifficultyLevel, value); }
        }

        /// <summary>
        /// Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            foreach (DifficultyLevelScale difficultyLevelScale in _difficultyLevelScaleService.GetAll())
            {
                DifficultyLevelScales.Add(difficultyLevelScale);
            }
        }

        /// <summary>
        ///     Setzt vorbestimmte Werte
        /// </summary>
        /// <param name="name"></param>
        /// <param name="scale"></param>
        /// <param name="level"></param>
        public void PresetValues(string name, DifficultyLevelScale scale, DifficultyLevel level)
        {
            Name = name;
            if (DifficultyLevelScales.Any())
            {
                SelectedDifficultyLevelScale = DifficultyLevelScales.FirstOrDefault(x => x.Id == scale.Id);
            }
            if (SelectedDifficultyLevelScale != null)
            {
                SelectedDifficultyLevel = DifficultyLevels.FirstOrDefault(x => x.Id == level.Id);
            }
        }

        /// <summary>
        /// Weitere Kriterien, die zur Prüfung herangezogen werden ob das OK Command ausgeführt werden darf.
        /// </summary>
        /// <returns></returns>
        public override bool MoreCanOkCriterias()
        {
            return SelectedDifficultyLevel != null;
        }
    }
}