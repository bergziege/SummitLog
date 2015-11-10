using System.Collections.ObjectModel;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.Common;
using SummitLog.UI.NameInput;

namespace SummitLog.UI.DifficultyLevelScaleManagement.ViewModels
{
    /// <summary>
    /// View Model einer Ansicht zur Verwaltung von Schwierigkeitsgradskalen
    /// </summary>
    public class DifficultyLevelScaleManagementViewModel:ReactiveObject, IDifficultyLevelScaleManagementViewModel
    {
        private readonly NameInputViewCommand _nameInputViewCommand;
        private readonly IDifficultyLevelScaleService _difficultyLevelScaleService;
        private RelayCommand _addDifficultyLevelScaleCommand;
        private DifficultyLevelScale _selectedDifficultyLevelScale;

        /// <summary>
        /// Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="nameInputViewCommand"></param>
        /// <param name="difficultyLevelScaleService"></param>
        protected DifficultyLevelScaleManagementViewModel(NameInputViewCommand nameInputViewCommand, IDifficultyLevelScaleService difficultyLevelScaleService)
        {
            _nameInputViewCommand = nameInputViewCommand;
            _difficultyLevelScaleService = difficultyLevelScaleService;
        }

        /// <summary>
        ///     LIefert ein Command um eine neue Schwierigkeitsgradskala hinzuzufügen
        /// </summary>
        public RelayCommand AddDifficultyLevelScaleCommand
        {
            get
            {
                if (_addDifficultyLevelScaleCommand == null)
                {
                    _addDifficultyLevelScaleCommand = new RelayCommand(AddDifficultyLevelScale, null);
                }
                return _addDifficultyLevelScaleCommand;
            }
        }

        private void AddDifficultyLevelScale()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                _difficultyLevelScaleService.Create(_nameInputViewCommand.Name);
            }
            LoadData();
        }

        /// <summary>
        ///     Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        public ObservableCollection<DifficultyLevelScale> DifficultyLevelScales { get; } = new ObservableCollection<DifficultyLevelScale>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        public DifficultyLevelScale SelectedDifficultyLevelScale
        {
            get { return _selectedDifficultyLevelScale; }
            set { this.RaiseAndSetIfChanged(ref _selectedDifficultyLevelScale, value); }
        }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            DifficultyLevelScales.Clear();
            foreach (DifficultyLevelScale difficultyLevelScale in _difficultyLevelScaleService.GetAll())
            {
                DifficultyLevelScales.Add(difficultyLevelScale);
            }
        }
    }
}