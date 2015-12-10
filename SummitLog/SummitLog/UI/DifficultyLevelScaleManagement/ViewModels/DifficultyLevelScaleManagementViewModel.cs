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
        private IItemWithNameViewModel<DifficultyLevelScale> _selectedDifficultyLevelScale;
        private RelayCommand _deleteSelectedDifficultyLevelScaleCommand;

        /// <summary>
        /// Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="nameInputViewCommand"></param>
        /// <param name="difficultyLevelScaleService"></param>
        public DifficultyLevelScaleManagementViewModel(NameInputViewCommand nameInputViewCommand, IDifficultyLevelScaleService difficultyLevelScaleService)
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
        public ObservableCollection<IItemWithNameViewModel<DifficultyLevelScale>> DifficultyLevelScales { get; } = new ObservableCollection<IItemWithNameViewModel<DifficultyLevelScale>>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        public IItemWithNameViewModel<DifficultyLevelScale> SelectedDifficultyLevelScale
        {
            get { return _selectedDifficultyLevelScale; }
            set { this.RaiseAndSetIfChanged(ref _selectedDifficultyLevelScale, value); }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Schwierigkeitsgradskala zu löschen, wenn diese nicht mehr verwendet wird.
        /// </summary>
        public RelayCommand DeleteSelectedDifficultyLevelScaleCommand
        {
            get
            {
                if (_deleteSelectedDifficultyLevelScaleCommand == null)
                {
                    _deleteSelectedDifficultyLevelScaleCommand = new RelayCommand(DeleteSelected, CanDeleteSelected);
                }
                return _deleteSelectedDifficultyLevelScaleCommand;
            }
        }

        private void DeleteSelected()
        {
            _difficultyLevelScaleService.Delete(SelectedDifficultyLevelScale.Item);
            LoadData();
        }

        private bool CanDeleteSelected()
        {
            return SelectedDifficultyLevelScale != null && !_difficultyLevelScaleService.IsInUse(SelectedDifficultyLevelScale.Item);
        }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            DifficultyLevelScales.Clear();
            foreach (DifficultyLevelScale difficultyLevelScale in _difficultyLevelScaleService.GetAll())
            {
                IItemWithNameViewModel<DifficultyLevelScale> difficultyLevelScaleViewModel = new ItemWithNameViewModel<DifficultyLevelScale>();
                difficultyLevelScaleViewModel.LoadData(difficultyLevelScale);
                DifficultyLevelScales.Add(difficultyLevelScaleViewModel);
            }
        }
    }
}