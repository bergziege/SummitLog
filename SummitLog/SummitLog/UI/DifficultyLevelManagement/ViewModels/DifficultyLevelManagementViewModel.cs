using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.Common;
using SummitLog.UI.NameAndScoreInput.ViewCommands;
using SummitLog.UI.NameInput;

namespace SummitLog.UI.DifficultyLevelManagement.ViewModels
{
    /// <summary>
    /// View Model zur Verwaltung von Schwierigkeitsgraden zu einer Skale
    /// </summary>
    public class DifficultyLevelManagementViewModel: ReactiveObject, IDifficultyLevelManagementViewModel
    {
        private readonly NameAndScoreInputViewCommand _nameAndScoreInputViewCommand;
        private readonly IDifficultyLevelService _difficultyLevelService;
        private DifficultyLevelScale _difficultyLevelScale;
        private RelayCommand _addDifficultyLevelCommand;

        /// <summary>
        /// Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="nameAndScoreInputViewCommand"></param>
        /// <param name="difficultyLevelService"></param>
        protected DifficultyLevelManagementViewModel(NameAndScoreInputViewCommand nameAndScoreInputViewCommand, IDifficultyLevelService difficultyLevelService)
        {
            _nameAndScoreInputViewCommand = nameAndScoreInputViewCommand;
            _difficultyLevelService = difficultyLevelService;
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
                    _addDifficultyLevelCommand = new RelayCommand(AddDifficultyLevel, null);
                }
                return _addDifficultyLevelCommand;
            }
        }

        private void AddDifficultyLevel()
        {
            _nameAndScoreInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameAndScoreInputViewCommand.Name))
            {
                _difficultyLevelService.Create(_difficultyLevelScale, _nameAndScoreInputViewCommand.Name,_nameAndScoreInputViewCommand.Score);
            }
            LoadData(_difficultyLevelScale);
        }

        /// <summary>
        ///     Liefert die Liste aller Schwierigkeitsgrade
        /// </summary>
        public ObservableCollection<DifficultyLevel> DifficultyLevels { get; } = new ObservableCollection<DifficultyLevel>();

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
                DifficultyLevels.Add(difficultyLevel);
            }
        }
    }
}