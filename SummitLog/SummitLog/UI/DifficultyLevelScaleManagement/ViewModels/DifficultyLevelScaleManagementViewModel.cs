using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.Common;
using SummitLog.UI.NameInput.ViewCommands;

namespace SummitLog.UI.DifficultyLevelScaleManagement.ViewModels
{
    /// <summary>
    ///     View Model einer Ansicht zur Verwaltung von Schwierigkeitsgradskalen
    /// </summary>
    public class DifficultyLevelScaleManagementViewModel : ReactiveObject, IDifficultyLevelScaleManagementViewModel
    {
        private readonly IDifficultyLevelScaleService _difficultyLevelScaleService;
        private readonly NameInputViewCommand _nameInputViewCommand;
        private RelayCommand _addDifficultyLevelScaleCommand;
        private RelayCommand _deleteSelectedDifficultyLevelScaleCommand;
        private RelayCommand _editSelectedDifficultyLevelScaleCommand;
        private IDifficultyLevelScaleItemViewModel _selectedDifficultyLevelScale;
        private RelayCommand _setSelectedAsDefaultCommand;

        /// <summary>
        ///     Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="nameInputViewCommand"></param>
        /// <param name="difficultyLevelScaleService"></param>
        public DifficultyLevelScaleManagementViewModel(NameInputViewCommand nameInputViewCommand,
            IDifficultyLevelScaleService difficultyLevelScaleService)
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

        /// <summary>
        ///     Liefert die Liste der Schwierigkeitsgradskalen
        /// </summary>
        public ObservableCollection<IDifficultyLevelScaleItemViewModel> DifficultyLevelScales { get; } =
            new ObservableCollection<IDifficultyLevelScaleItemViewModel>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Schwierigkeitsgradskala
        /// </summary>
        public IDifficultyLevelScaleItemViewModel SelectedDifficultyLevelScale
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

        /// <summary>
        ///     Liefert ein Command um die gewählte Schwierigkeitsgradskala zu bearbeiten.
        /// </summary>
        public RelayCommand EditSelectedDifficultyLevelScaleCommand
        {
            get
            {
                if (_editSelectedDifficultyLevelScaleCommand == null)
                {
                    _editSelectedDifficultyLevelScaleCommand = new RelayCommand(EditSelectedDifficultyLevelScale,
                        CanEditSelectedDifficultyLevelScale);
                }
                return _editSelectedDifficultyLevelScaleCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Relay Command um die gewählte Schwierigkeitsgradskale als Standard zu setzen.
        /// </summary>
        public RelayCommand SetSelectedAsDefaultCommand
        {
            get
            {
                if (_setSelectedAsDefaultCommand == null)
                {
                    _setSelectedAsDefaultCommand = new RelayCommand(SetSelectedAsDefault, CanSetSelectedAsDefault);
                }
                return _setSelectedAsDefaultCommand;
            }
        }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            DifficultyLevelScales.Clear();
            foreach (DifficultyLevelScale difficultyLevelScale in _difficultyLevelScaleService.GetAll())
            {
                IDifficultyLevelScaleItemViewModel difficultyLevelScaleViewModel =
                    new DifficultyLevelScaleItemViewModel();
                difficultyLevelScaleViewModel.LoadData(difficultyLevelScale);
                DifficultyLevelScales.Add(difficultyLevelScaleViewModel);
            }
        }

        private void AddDifficultyLevelScale()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                DifficultyLevelScale created = _difficultyLevelScaleService.Create(_nameInputViewCommand.Name);
                LoadData();
                SelectedDifficultyLevelScale = DifficultyLevelScales.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private void SetSelectedAsDefault()
        {
            _difficultyLevelScaleService.SetAsDefault(SelectedDifficultyLevelScale.Item);
            LoadData();
        }

        private bool CanSetSelectedAsDefault()
        {
            return SelectedDifficultyLevelScale != null;
        }

        private bool CanEditSelectedDifficultyLevelScale()
        {
            return SelectedDifficultyLevelScale != null;
        }

        private void EditSelectedDifficultyLevelScale()
        {
            _nameInputViewCommand.Execute(SelectedDifficultyLevelScale.Name);
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                SelectedDifficultyLevelScale.Item.Name = _nameInputViewCommand.Name;
                _difficultyLevelScaleService.Save(SelectedDifficultyLevelScale.Item);
                SelectedDifficultyLevelScale.DoUpdate();
            }
        }

        private void DeleteSelected()
        {
            _difficultyLevelScaleService.Delete(SelectedDifficultyLevelScale.Item);
            LoadData();
        }

        private bool CanDeleteSelected()
        {
            return SelectedDifficultyLevelScale != null &&
                   !_difficultyLevelScaleService.IsInUse(SelectedDifficultyLevelScale.Item);
        }
    }
}