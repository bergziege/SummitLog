using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.Common;
using SummitLog.UI.NameAndScoreInput.ViewCommands;

namespace SummitLog.UI.DifficultyLevelManagement.ViewModels
{
    /// <summary>
    ///     View Model zur Verwaltung von Schwierigkeitsgraden zu einer Skale
    /// </summary>
    public class DifficultyLevelManagementViewModel : ReactiveObject, IDifficultyLevelManagementViewModel
    {
        private readonly IDifficultyLevelService _difficultyLevelService;
        private readonly NameAndScoreInputViewCommand _nameAndScoreInputViewCommand;
        private RelayCommand _addDifficultyLevelCommand;
        private RelayCommand _deleteSelectedDifficultyLevelCommand;
        private DifficultyLevelScale _difficultyLevelScale;
        private IItemWithNameAndScoreViewModel _selectedDifficultyLevel;

        /// <summary>
        ///     Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="nameAndScoreInputViewCommand"></param>
        /// <param name="difficultyLevelService"></param>
        public DifficultyLevelManagementViewModel(NameAndScoreInputViewCommand nameAndScoreInputViewCommand,
            IDifficultyLevelService difficultyLevelService)
        {
            _nameAndScoreInputViewCommand = nameAndScoreInputViewCommand;
            _difficultyLevelService = difficultyLevelService;
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        ///     Liefert ein Command um ein neuen Schwierigkeitsgrad hinzuzufügen
        /// </summary>
        public RelayCommand AddDifficultyLevelCommand
        {
            get
            {
                if (_addDifficultyLevelCommand == null)
                {
                    _addDifficultyLevelCommand = new RelayCommand(AddDifficultyLevel, CanAddDifficultyLevel);
                }
                return _addDifficultyLevelCommand;
            }
        }

        /// <summary>
        ///     Liefert die Liste aller Schwierigkeitsgrade
        /// </summary>
        public ObservableCollection<IItemWithNameAndScoreViewModel> DifficultyLevels { get; } =
            new ObservableCollection<IItemWithNameAndScoreViewModel>();

        /// <summary>
        ///     Liefert oder setzt das gewählte <see cref="DifficultyLevel" />
        /// </summary>
        public IItemWithNameAndScoreViewModel SelectedDifficultyLevel
        {
            get { return _selectedDifficultyLevel; }
            set { this.RaiseAndSetIfChanged(ref _selectedDifficultyLevel, value); }
        }

        /// <summary>
        ///     Liefert ein Command um das gewählte <see cref="DifficultyLevel" /> zu löschen
        /// </summary>
        public RelayCommand DeleteSelectedDifficultyLevelCommand
        {
            get
            {
                if (_deleteSelectedDifficultyLevelCommand == null)
                {
                    _deleteSelectedDifficultyLevelCommand = new RelayCommand(DeleteSelected, CanDeleteSelected);
                }
                return _deleteSelectedDifficultyLevelCommand;
            }
        }

        /// <summary>
        ///     LÄdt die VM relevanten Daten zu einer Schwierigkeitsgradskale
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        public void LoadData(DifficultyLevelScale difficultyLevelScale)
        {
            if (difficultyLevelScale == null) throw new ArgumentNullException(nameof(difficultyLevelScale));
            _difficultyLevelScale = difficultyLevelScale;

            DifficultyLevels.Clear();
            foreach (DifficultyLevel difficultyLevel in _difficultyLevelService.GetAllIn(_difficultyLevelScale))
            {
                IItemWithNameAndScoreViewModel itemViewModel = new ItemWithNameAndScoreViewModel();
                itemViewModel.LoadData(difficultyLevel);
                DifficultyLevels.Add(itemViewModel);
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanAddDifficultyLevel()
        {
            return _difficultyLevelScale != null;
        }

        private void AddDifficultyLevel()
        {
            _nameAndScoreInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameAndScoreInputViewCommand.Name))
            {
                _difficultyLevelService.Create(_difficultyLevelScale, _nameAndScoreInputViewCommand.Name,
                    _nameAndScoreInputViewCommand.Score);
            }
            LoadData(_difficultyLevelScale);
        }

        private void DeleteSelected()
        {
            _difficultyLevelService.Delete(SelectedDifficultyLevel.Item);
            LoadData(_difficultyLevelScale);
        }

        private bool CanDeleteSelected()
        {
            return SelectedDifficultyLevel != null && !_difficultyLevelService.IsInUse(SelectedDifficultyLevel.Item);
        }
    }
}