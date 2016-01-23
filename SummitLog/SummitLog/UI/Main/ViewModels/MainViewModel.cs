using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DryIoc;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.Common;
using SummitLog.UI.DbSettings.ViewCommands;
using SummitLog.UI.DifficultyManagement.ViewCommands;
using SummitLog.UI.LogEntryInput.ViewCommands;
using SummitLog.UI.NameAndLevelInput.ViewCommands;
using SummitLog.UI.NameInput;
using SummitLog.UI.RouteOnSummitEdit.ViewCommands;
using SummitLog.UI.SummitEdit.ViewCommands;

namespace SummitLog.UI.Main.ViewModels
{
    /// <summary>
    ///     View Model des Hauptfensters
    /// </summary>
    public class MainViewModel : ReactiveObject, IMainViewModel, IWeakEventListener
    {
        private readonly IAreaService _areaService;
        private readonly ICountryService _countryService;
        private readonly DbSettingsViewCommand _dbSettingsViewCommand;
        private readonly IDifficultyLevelScaleService _difficultyLevelScaleService;
        private readonly IDifficultyLevelService _difficultyLevelService;
        private readonly DifficultyManagementViewCommand _difficultyManagementViewCommand;
        private readonly LogEntryInputViewCommand _logEntryInputViewCommand;
        private readonly ILogEntryService _logEntryService;
        private readonly NameAndLevelInputViewCommand _nameAndLevelInputViewCommand;
        private readonly NameInputViewCommand _nameInputViewCommand;
        private readonly RouteOnSummitEditViewCommand _routeOnSummitEditViewCommand;
        private readonly IRouteService _routeService;
        private readonly SummitEditViewCommand _summitEditViewCommand;
        private readonly ISummitGroupService _summitGroupService;
        private readonly ISummitService _summitService;
        private readonly IVariationService _variationService;

        private RelayCommand _addAreaInSelectedCountryCommand;
        private RelayCommand _addCountryCommand;
        private RelayCommand _addLogEntryToSelectedVariationCommand;
        private RelayCommand _addRouteInSelectedAreaCommand;
        private RelayCommand _addRouteInSelectedCountryCommand;
        private RelayCommand _addRouteInSelectedSummitCommand;
        private RelayCommand _addRouteInSelectedSummitGroupCommnad;
        private RelayCommand _addSummitGroupInSelectedAreaCommand;
        private RelayCommand _addSummitInSelectedSummitGroupCommand;
        private RelayCommand _addVariationToSelectedRouteCommand;
        private RelayCommand _editSelectedAreaCommand;
        private RelayCommand _editSelectedCountryCommand;
        private RelayCommand _editSelectedLogEntryCommand;
        private RelayCommand _editSelectedRouteInAreaCommand;
        private RelayCommand _editSelectedRouteInCountryCommand;
        private RelayCommand _editSelectedRouteInSummitCommand;
        private RelayCommand _editSelectedRouteInSummitGroupCommand;
        private RelayCommand _editSelectedSummitCommand;
        private RelayCommand _editSelectedSummitGroupCommand;
        private RelayCommand _editSelectedVariationCommand;
        private RelayCommand _manageDifficultiesCommand;
        private RelayCommand _removeAreaCommand;
        private RelayCommand _removeCountryCommand;
        private RelayCommand _removeRouteInAreaCommand;
        private RelayCommand _removeRouteInCountryCommand;
        private RelayCommand _removeRouteInSummitCommand;
        private RelayCommand _removeRouteInSummitGroupCommand;
        private RelayCommand _removeSelectedLogEntryCommand;
        private RelayCommand _removeSelectedVariationCommand;
        private RelayCommand _removeSummitCommand;
        private RelayCommand _removeSummitGroupCommand;
        private IItemWithNameViewModel<Area> _selectedArea;
        private IItemWithNameViewModel<Country> _selectedCountry;
        private ILogItemViewModel _selectedLogEntry;
        private IItemWithNameViewModel<Route> _selectedRouteInArea;
        private IItemWithNameViewModel<Route> _selectedRouteInCountry;
        private IRouteViewModel _selectedRouteInSummit;
        private IItemWithNameViewModel<Route> _selectedRouteInSummitGroup;
        private ISummitViewModel _selectedSummit;
        private IItemWithNameViewModel<SummitGroup> _selectedSummitGroup;
        private IVariationItemViewModel _selectedVariation;
        private RelayCommand _showDbSettingsCommand;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="countryService"></param>
        /// <param name="areaService"></param>
        /// <param name="summitGroupService"></param>
        /// <param name="summitService"></param>
        /// <param name="routeService"></param>
        /// <param name="variationService"></param>
        /// <param name="logEntryService"></param>
        /// <param name="difficultyLevelService"></param>
        /// <param name="difficultyLevelScaleService"></param>
        /// <param name="nameInputViewCommand"></param>
        /// <param name="difficultyManagementViewCommand"></param>
        /// <param name="nameAndLevelInputViewCommand"></param>
        /// <param name="logEntryInputViewCommand"></param>
        /// <param name="summitEditViewCommand"></param>
        /// <param name="routeOnSummitEditViewCommand"></param>
        /// <param name="dbSettingsViewCommand"></param>
        public MainViewModel(ICountryService countryService, IAreaService areaService,
            ISummitGroupService summitGroupService, ISummitService summitService, IRouteService routeService,
            IVariationService variationService, ILogEntryService logEntryService,
            IDifficultyLevelService difficultyLevelService,
            IDifficultyLevelScaleService difficultyLevelScaleService,
            NameInputViewCommand nameInputViewCommand, DifficultyManagementViewCommand difficultyManagementViewCommand,
            NameAndLevelInputViewCommand nameAndLevelInputViewCommand
            , LogEntryInputViewCommand logEntryInputViewCommand, SummitEditViewCommand summitEditViewCommand,
            RouteOnSummitEditViewCommand routeOnSummitEditViewCommand, DbSettingsViewCommand dbSettingsViewCommand)
        {
            _countryService = countryService;
            _areaService = areaService;
            _summitGroupService = summitGroupService;
            _summitService = summitService;
            _routeService = routeService;
            _variationService = variationService;
            _logEntryService = logEntryService;
            _difficultyLevelService = difficultyLevelService;
            _difficultyLevelScaleService = difficultyLevelScaleService;
            _nameInputViewCommand = nameInputViewCommand;
            _difficultyManagementViewCommand = difficultyManagementViewCommand;
            _nameAndLevelInputViewCommand = nameAndLevelInputViewCommand;
            _logEntryInputViewCommand = logEntryInputViewCommand;
            _summitEditViewCommand = summitEditViewCommand;
            _routeOnSummitEditViewCommand = routeOnSummitEditViewCommand;
            _dbSettingsViewCommand = dbSettingsViewCommand;
        }

        /// <summary>
        ///     Liefert die Liste aller Länder
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Country>> Countries { get; } =
            new ObservableCollection<IItemWithNameViewModel<Country>>();

        /// <summary>
        ///     Liefert oder setzt das gewählte Land
        /// </summary>
        public IItemWithNameViewModel<Country> SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedCountry, value);
                RefreshAreas();
                RefreshRoutesInSelectedCountry();
            }
        }

        /// <summary>
        ///     Liefert die Liste aller Gebiete im gewählten Land
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Area>> AreasInSelectedCountry { get; } =
            new ObservableCollection<IItemWithNameViewModel<Area>>();

        /// <summary>
        ///     Liefert oder setzt das gewählte Gebiet
        /// </summary>
        public IItemWithNameViewModel<Area> SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedArea, value);
                RefreshSummitGroups();
                RefreshRoutesInSelectedArea();
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Gipfelgruppen im gewählten Gebiet
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<SummitGroup>> SummitGroupsInSelectedArea { get; } =
            new ObservableCollection<IItemWithNameViewModel<SummitGroup>>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Gipfelgruppe
        /// </summary>
        public IItemWithNameViewModel<SummitGroup> SelectedSummitGroup
        {
            get { return _selectedSummitGroup; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSummitGroup, value);
                RefreshSummits();
                RefreshRoutesInSelectedSummitGroup();
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Gipfel in der Gewählten Gipfelgruppe
        /// </summary>
        public ObservableCollection<ISummitViewModel> SummitsInSelectedSummitGroup { get; } =
            new ObservableCollection<ISummitViewModel>();

        /// <summary>
        ///     Liefert oder setzt den gewählten Gipfel
        /// </summary>
        public ISummitViewModel SelectedSummit
        {
            get { return _selectedSummit; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSummit, value);
                RefreshRoutesInSelectedSummit();
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Land
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedCountry { get; } =
            new ObservableCollection<IItemWithNameViewModel<Route>>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Route eines Landes
        /// </summary>
        public IItemWithNameViewModel<Route> SelectedRouteInCountry
        {
            get { return _selectedRouteInCountry; }
            set
            {
                if (!IsLoadingRoutesInCountry)
                {
                    this.RaiseAndSetIfChanged(ref _selectedRouteInCountry, value);

                    IsLoadingRoutesInCountry = true;
                    SelectedRouteInArea = null;
                    SelectedRouteInSummitGroup = null;
                    SelectedRouteInSummit = null;
                    IsLoadingRoutesInCountry = false;

                    ClearLogEntries();
                    RefreshVariationsOnCountryRoute();
                }
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Gebiet
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedArea { get; } =
            new ObservableCollection<IItemWithNameViewModel<Route>>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einem Gebiet
        /// </summary>
        public IItemWithNameViewModel<Route> SelectedRouteInArea
        {
            get { return _selectedRouteInArea; }
            set
            {
                if (!IsLoadingRoutesInArea)
                {
                    this.RaiseAndSetIfChanged(ref _selectedRouteInArea, value);

                    IsLoadingRoutesInArea = true;
                    SelectedRouteInCountry = null;
                    SelectedRouteInSummitGroup = null;
                    SelectedRouteInSummit = null;
                    IsLoadingRoutesInArea = false;

                    ClearLogEntries();
                    RefreshVariationsOnAreaRoute();
                }
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Routen in der gewählten Gipfelgruppe
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedSummitGroup { get; } =
            new ObservableCollection<IItemWithNameViewModel<Route>>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einer Gipfelgruppe
        /// </summary>
        public IItemWithNameViewModel<Route> SelectedRouteInSummitGroup
        {
            get { return _selectedRouteInSummitGroup; }
            set
            {
                if (!IsLoadingRoutesInSummitGroup)
                {
                    this.RaiseAndSetIfChanged(ref _selectedRouteInSummitGroup, value);

                    IsLoadingRoutesInSummitGroup = true;
                    SelectedRouteInCountry = null;
                    SelectedRouteInArea = null;
                    SelectedRouteInSummit = null;
                    IsLoadingRoutesInSummitGroup = false;

                    ClearLogEntries();
                    RefreshVariationsOnSummitGroupRoute();
                }
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Routen an einem gewählten Gipfel
        /// </summary>
        public ObservableCollection<IRouteViewModel> RoutesInSelectedSummit { get; } =
            new ObservableCollection<IRouteViewModel>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Route an einem Gipfel
        /// </summary>
        public IRouteViewModel SelectedRouteInSummit
        {
            get { return _selectedRouteInSummit; }
            set
            {
                if (!IsLoadingRoutesInSummit)
                {
                    this.RaiseAndSetIfChanged(ref _selectedRouteInSummit, value);

                    IsLoadingRoutesInSummit = true;
                    SelectedRouteInCountry = null;
                    SelectedRouteInArea = null;
                    SelectedRouteInSummitGroup = null;
                    IsLoadingRoutesInSummit = false;

                    ClearLogEntries();
                    RefreshVariationsOnSummitRoute();
                }
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Variationen einer gewählten Route (Land, Gebiet, Gruppe ODER Gipfel)
        /// </summary>
        public ObservableCollection<IVariationItemViewModel> VariationsOnSelectedRoute { get; } =
            new ObservableCollection<IVariationItemViewModel>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Variation
        /// </summary>
        public IVariationItemViewModel SelectedVariation
        {
            get { return _selectedVariation; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedVariation, value);
                RefreshLogEntriesOnSelectedVariation();
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Logeinträge zur gewählten Variation
        /// </summary>
        public ObservableCollection<ILogItemViewModel> LogEntriesOnSelectedVariation { get; } =
            new ObservableCollection<ILogItemViewModel>();

        /// <summary>
        ///     Liefert oder setzt den gewählten Logeintrag
        /// </summary>
        public ILogItemViewModel SelectedLogEntry
        {
            get { return _selectedLogEntry; }
            set { this.RaiseAndSetIfChanged(ref _selectedLogEntry, value); }
        }

        /// <summary>
        ///     Liefert ein Command um ein Land hinzuzufügen
        /// </summary>
        public RelayCommand AddCountryCommand
        {
            get
            {
                if (_addCountryCommand == null)
                {
                    _addCountryCommand = new RelayCommand(AddCountry, CanAddCountry);
                }
                return _addCountryCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um ein Gebiet zu einem gewählten Land hinzuzufügen
        /// </summary>
        public RelayCommand AddAreaInSelectedCountryCommand
        {
            get
            {
                if (_addAreaInSelectedCountryCommand == null)
                {
                    _addAreaInSelectedCountryCommand = new RelayCommand(AddAreaInSelectedCountry,
                        CanAddAreaInSelectedCountry);
                }
                return _addAreaInSelectedCountryCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um eine Gipfelgruppe zu einem gewählten Gebiet hinzuzufügen
        /// </summary>
        public RelayCommand AddSummitGroupInSelectedAreaCommand
        {
            get
            {
                if (_addSummitGroupInSelectedAreaCommand == null)
                {
                    _addSummitGroupInSelectedAreaCommand = new RelayCommand(AddSummitGroupInSelectedArea,
                        CanAddSummitGroupInSelectedArea);
                }
                return _addSummitGroupInSelectedAreaCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um ein Gipfel einer gewählten Gipfelgruppe hinzuzufügen
        /// </summary>
        public RelayCommand AddSummitInSelectedSummitGroupCommand
        {
            get
            {
                if (_addSummitInSelectedSummitGroupCommand == null)
                {
                    _addSummitInSelectedSummitGroupCommand = new RelayCommand(AddSummitInSelectedSummitGroup,
                        CanAddSummitInSelectedSummitGroup);
                }
                return _addSummitInSelectedSummitGroupCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Land eine Route hinzuzufügen
        /// </summary>
        public RelayCommand AddRouteInSelectedCountryCommand
        {
            get
            {
                if (_addRouteInSelectedCountryCommand == null)
                {
                    _addRouteInSelectedCountryCommand = new RelayCommand(AddRouteInSelectedCountry,
                        CanAddRouteInSelectedCountry);
                }
                return _addRouteInSelectedCountryCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Gebiet eine Route hinzuzufügen
        /// </summary>
        public RelayCommand AddRouteInSelectedAreaCommand
        {
            get
            {
                if (_addRouteInSelectedAreaCommand == null)
                {
                    _addRouteInSelectedAreaCommand = new RelayCommand(AddRouteInSelctedArea, CanAddRouteInSelectedArea);
                }
                return _addRouteInSelectedAreaCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um der gewählten Gipfelgruppe eine Route hinzuzufügen
        /// </summary>
        public RelayCommand AddRouteInSelectedSummitGroupCommnad
        {
            get
            {
                if (_addRouteInSelectedSummitGroupCommnad == null)
                {
                    _addRouteInSelectedSummitGroupCommnad = new RelayCommand(AddRouteInSlectedSummitGroup,
                        CanAddRouteInSelectedSummmitGroup);
                }
                return _addRouteInSelectedSummitGroupCommnad;
            }
        }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Gipfel eine Route hinzuzufügen
        /// </summary>
        public RelayCommand AddRouteInSelectedSummitCommand
        {
            get
            {
                if (_addRouteInSelectedSummitCommand == null)
                {
                    _addRouteInSelectedSummitCommand = new RelayCommand(AddRouteInSelectedSummit,
                        CanAddRouteInSelectedSummit);
                }
                return _addRouteInSelectedSummitCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um einer gewählten Route eine Variation hinzuzufügen
        /// </summary>
        public RelayCommand AddVariationToSelectedRouteCommand
        {
            get
            {
                if (_addVariationToSelectedRouteCommand == null)
                {
                    _addVariationToSelectedRouteCommand = new RelayCommand(AddVariationToSelectedRoute,
                        CanAddVariationToSelectedRoute);
                }
                return _addVariationToSelectedRouteCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um der gewählten Variation ein Logeintrag hinzuzufügen
        /// </summary>
        public RelayCommand AddLogEntryToSelectedVariationCommand
        {
            get
            {
                if (_addLogEntryToSelectedVariationCommand == null)
                {
                    _addLogEntryToSelectedVariationCommand = new RelayCommand(AddLogEntryToSelectedVariation,
                        CanAddLogEntryToSelectedVariation);
                }
                return _addLogEntryToSelectedVariationCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command für die Verwaltung der Schwierigkeitsgrad und Skalen
        /// </summary>
        public RelayCommand ManageDifficultiesCommand
        {
            get
            {
                if (_manageDifficultiesCommand == null)
                {
                    _manageDifficultiesCommand = new RelayCommand(ManageDifficulties, CanManageDifficulties);
                }
                return _manageDifficultiesCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Variation zu löschen
        /// </summary>
        public RelayCommand RemoveSelectedVariationCommand
        {
            get
            {
                if (_removeSelectedVariationCommand == null)
                {
                    _removeSelectedVariationCommand = new RelayCommand(RemoveSelectedVariation,
                        CanRemoveSelectedVariation);
                }
                return _removeSelectedVariationCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um eine Route in einem Land zu löschen
        /// </summary>
        public RelayCommand RemoveRouteInCountryCommand
        {
            get
            {
                if (_removeRouteInCountryCommand == null)
                {
                    _removeRouteInCountryCommand = new RelayCommand(RemoveRouteInCountry, CanRemoveRouteInCountry);
                }
                return _removeRouteInCountryCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um eine Route in einem Gebiet zu löschen
        /// </summary>
        public RelayCommand RemoveRouteInAreaCommand
        {
            get
            {
                if (_removeRouteInAreaCommand == null)
                {
                    _removeRouteInAreaCommand = new RelayCommand(RemoveRouteInArea, CanRemoveRouteInArea);
                }
                return _removeRouteInAreaCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um eine Route in einer Gipflegruppe zu löschen
        /// </summary>
        public RelayCommand RemoveRouteInSummitGroupCommand
        {
            get
            {
                if (_removeRouteInSummitGroupCommand == null)
                {
                    _removeRouteInSummitGroupCommand = new RelayCommand(RemoveRouteInSummitGroup,
                        CanRemoveRouteInSummitGroup);
                }
                return _removeRouteInSummitGroupCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um eine Route an einem Gipfel zu löschen.
        /// </summary>
        public RelayCommand RemoveRouteInSummitCommand
        {
            get
            {
                if (_removeRouteInSummitCommand == null)
                {
                    _removeRouteInSummitCommand = new RelayCommand(RemoveRouteInSummit, CanRemoveRouteInSummit);
                }
                return _removeRouteInSummitCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um den Gipfel zu entfernen
        /// </summary>
        public RelayCommand RemoveSummitCommand
        {
            get
            {
                if (_removeSummitCommand == null)
                {
                    _removeSummitCommand = new RelayCommand(RemoveSummit, CanRemoveSummit);
                }
                return _removeSummitCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gipfelgruppe zu löschen
        /// </summary>
        public RelayCommand RemoveSummitGroupCommand
        {
            get
            {
                if (_removeSummitGroupCommand == null)
                {
                    _removeSummitGroupCommand = new RelayCommand(RemoveSummitGroup, CanRemoveSummitGroup);
                }
                return _removeSummitGroupCommand;
            }
        }

        /// <summary>
        ///     Líefert ein Command um Gebiet zu löschen
        /// </summary>
        public RelayCommand RemoveAreaCommand
        {
            get
            {
                if (_removeAreaCommand == null)
                {
                    _removeAreaCommand = new RelayCommand(RemoveArea, CanRemoveArea);
                }
                return _removeAreaCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um ein Land zu löschen
        /// </summary>
        public RelayCommand RemoveCountryCommand
        {
            get
            {
                if (_removeCountryCommand == null)
                {
                    _removeCountryCommand = new RelayCommand(RemoveCountry, CanRemoveCountry);
                }
                return _removeCountryCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um das gewählte Land zu speichern
        /// </summary>
        public RelayCommand EditSelectedCountryCommand
        {
            get
            {
                if (_editSelectedCountryCommand == null)
                {
                    _editSelectedCountryCommand = new RelayCommand(EditSelectedCountry, CanEditSelectedCountry);
                }
                return _editSelectedCountryCommand;
            }
        }

        /// <summary>
        ///     Liefert Command um die gewählte Gegend zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedAreaCommand
        {
            get
            {
                if (_editSelectedAreaCommand == null)
                {
                    _editSelectedAreaCommand = new RelayCommand(EditSelectedArea, CanEditSelectedArea);
                }
                return _editSelectedAreaCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gipfelgruppe zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedSummitGroupCommand
        {
            get
            {
                if (_editSelectedSummitGroupCommand == null)
                {
                    _editSelectedSummitGroupCommand = new RelayCommand(EditSelectedSummitGroup,
                        CanEditSelectedSummitGroup);
                }
                return _editSelectedSummitGroupCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um den gewählten Gipfel zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedSummitCommand
        {
            get
            {
                if (_editSelectedSummitCommand == null)
                {
                    _editSelectedSummitCommand = new RelayCommand(EditSelectedSummit, CanEditSelectedSummit);
                }
                return _editSelectedSummitCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route im Land zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedRouteInCountryCommand
        {
            get
            {
                if (_editSelectedRouteInCountryCommand == null)
                {
                    _editSelectedRouteInCountryCommand = new RelayCommand(EditSelectedRouteInCountry,
                        CanEditSelectedRouteInCountry);
                }
                return _editSelectedRouteInCountryCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route ineinem Gebiet zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedRouteInAreaCommand
        {
            get
            {
                if (_editSelectedRouteInAreaCommand == null)
                {
                    _editSelectedRouteInAreaCommand = new RelayCommand(EditSelectedRouteInArea,
                        CanEditSelectedRouteInArea);
                }
                return _editSelectedRouteInAreaCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route in einer Gipfelgruppe zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedRouteInSummitGroupCommand
        {
            get
            {
                if (_editSelectedRouteInSummitGroupCommand == null)
                {
                    _editSelectedRouteInSummitGroupCommand = new RelayCommand(EditSelectedRouteInSummitGroup,
                        CanEditSelectedRouteInSummitGroup);
                }
                return _editSelectedRouteInSummitGroupCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gruppe in einem Gipfel zu bearbeiten.
        /// </summary>
        public RelayCommand EditSelectedRouteInSummitCommand
        {
            get
            {
                if (_editSelectedRouteInSummitCommand == null)
                {
                    _editSelectedRouteInSummitCommand = new RelayCommand(EditSelectedRouteInSummit,
                        CanEditSelectedRouteInSummit);
                }
                return _editSelectedRouteInSummitCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die gewählte Variation zu bearbeiten.
        /// </summary>
        public RelayCommand EditSelectedVariationCommand
        {
            get
            {
                if (_editSelectedVariationCommand == null)
                {
                    _editSelectedVariationCommand = new RelayCommand(EditSelectedVariation, CanEditSelectedVariation);
                }
                return _editSelectedVariationCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um den gewählten Logeintrag zu bearbeiten.
        /// </summary>
        public RelayCommand EditSelectedLogEntryCommand
        {
            get
            {
                if (_editSelectedLogEntryCommand == null)
                {
                    _editSelectedLogEntryCommand = new RelayCommand(EditSelectedLogEntry, CanEditSelectedLogEntry);
                }
                return _editSelectedLogEntryCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command zur Anzeige der DB Einstellungen
        /// </summary>
        public RelayCommand ShowDbSettingsCommand
        {
            get
            {
                if (_showDbSettingsCommand == null)
                {
                    _showDbSettingsCommand = new RelayCommand(ShowDbSettings, CanShowDbSettings);
                }
                return _showDbSettingsCommand;
            }
        }

        /// <summary>
        ///     Lädt die relevanten Daten des View Models
        /// </summary>
        public void LoadData()
        {
            RefreshCountries();
        }

        /// <summary>
        ///     Liefert ob Länder geladen werden
        /// </summary>
        public bool IsLoadingCountries { get; private set; }

        /// <summary>
        ///     Liefert ob Gegenden geladen werden
        /// </summary>
        public bool IsLoadingAreas { get; private set; }

        /// <summary>
        ///     Liefert ob Gipfelgruppen geladen werden
        /// </summary>
        public bool IsLoadingSummitGroups { get; private set; }

        /// <summary>
        ///     Liefert ob Gipfel geladen werden
        /// </summary>
        public bool IsLoadingSummits { get; private set; }

        /// <summary>
        ///     Liefert ob Wege im Land geladen werden
        /// </summary>
        public bool IsLoadingRoutesInCountry { get; private set; }

        /// <summary>
        ///     Liefert ob Wege in Gegend geladen werden
        /// </summary>
        public bool IsLoadingRoutesInArea { get; private set; }

        /// <summary>
        ///     Liefert ob Wege in Gipfelgruppe geladen werden
        /// </summary>
        public bool IsLoadingRoutesInSummitGroup { get; private set; }

        /// <summary>
        ///     Liefert ob Wege in Gipfel geladen werden
        /// </summary>
        public bool IsLoadingRoutesInSummit { get; private set; }

        /// <summary>
        ///     Liefert ob Variationen geladen werden
        /// </summary>
        public bool IsLoadingVariations { get; private set; }

        /// <summary>
        ///     Liefert ob Logeinträge geladen werden
        /// </summary>
        public bool IsLoadingLogs { get; private set; }

        /// <summary>
        ///     Liefert ein Command um den gewählten Logeintrag zu löschen.
        /// </summary>
        public RelayCommand RemoveSelectedLogEntryCommand
        {
            get
            {
                if (_removeSelectedLogEntryCommand == null)
                {
                    _removeSelectedLogEntryCommand = new RelayCommand(RemoveSelectedLogEntry, CanRemoveSelectedLogEntry);
                }
                return _removeSelectedLogEntryCommand;
            }
        }

        /// <summary>
        ///     Receives events from the centralized event manager.
        /// </summary>
        /// <returns>
        ///     true if the listener handled the event. It is considered an error by the
        ///     <see cref="T:System.Windows.WeakEventManager" /> handling in WPF to register a listener for an event that the
        ///     listener does not handle. Regardless, the method should return false if it receives an event that it does not
        ///     recognize or handle.
        /// </returns>
        /// <param name="managerType">The type of the <see cref="T:System.Windows.WeakEventManager" /> calling this method.</param>
        /// <param name="sender">Object that originated the event.</param>
        /// <param name="e">Event data.</param>
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            return true;
        }

        private bool CanShowDbSettings()
        {
            return true;
        }

        private void ShowDbSettings()
        {
            _dbSettingsViewCommand.Execute();
        }

        private bool CanEditSelectedLogEntry()
        {
            return SelectedLogEntry != null;
        }

        private void EditSelectedLogEntry()
        {
            _logEntryInputViewCommand.Execute(SelectedLogEntry.Memo, SelectedLogEntry.Date.DateTime);
            SelectedLogEntry.Update(_logEntryInputViewCommand.Memo, _logEntryInputViewCommand.Date);
            _logEntryService.Save(SelectedLogEntry.LogEntry);
        }

        private bool CanEditSelectedVariation()
        {
            return SelectedVariation != null;
        }

        private void EditSelectedVariation()
        {
            DifficultyLevel level = _difficultyLevelService.GetForVariation(SelectedVariation.Item);
            if (level != null)
            {
                DifficultyLevelScale scale = _difficultyLevelScaleService.GetForDifficultyLevel(level);
                _nameAndLevelInputViewCommand.Execute(SelectedVariation.Name, scale, level);
            }
            SelectedVariation.Item.Name = _nameAndLevelInputViewCommand.Name;
            _variationService.ChangeDifficultyLevel(SelectedVariation.Item,
                _nameAndLevelInputViewCommand.DifficultyLevel);
            _variationService.Save(SelectedVariation.Item);
            SelectedVariation.DoUpdate();
        }

        private bool CanEditSelectedRouteInCountry()
        {
            return SelectedRouteInCountry != null;
        }

        private void EditSelectedRouteInCountry()
        {
            _nameInputViewCommand.Execute(SelectedRouteInCountry.Name);
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                SelectedRouteInCountry.Item.Name = _nameInputViewCommand.Name;
                _routeService.Save(SelectedRouteInCountry.Item);
                SelectedRouteInCountry.DoUpdate();
            }
        }

        private bool CanEditSelectedRouteInArea()
        {
            return SelectedRouteInArea != null;
        }

        private void EditSelectedRouteInArea()
        {
            _nameInputViewCommand.Execute(SelectedRouteInArea.Name);
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                SelectedRouteInArea.Item.Name = _nameInputViewCommand.Name;
                _routeService.Save(SelectedRouteInArea.Item);
                SelectedRouteInArea.DoUpdate();
            }
        }

        private bool CanEditSelectedRouteInSummitGroup()
        {
            return SelectedRouteInSummitGroup != null;
        }

        private void EditSelectedRouteInSummitGroup()
        {
            _nameInputViewCommand.Execute(SelectedRouteInSummitGroup.Name);
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                SelectedRouteInSummitGroup.Item.Name = _nameInputViewCommand.Name;
                _routeService.Save(SelectedRouteInSummitGroup.Item);
                SelectedRouteInSummitGroup.DoUpdate();
            }
        }

        private bool CanEditSelectedRouteInSummit()
        {
            return SelectedRouteInSummit != null;
        }

        private void EditSelectedRouteInSummit()
        {
            SelectedRouteInSummit.LoadData(_routeOnSummitEditViewCommand.Execute(SelectedRouteInSummit.Item));
            if (!string.IsNullOrWhiteSpace(SelectedRouteInSummit.Name))
            {
                _routeService.Save(SelectedRouteInSummit.Item);
                SelectedRouteInSummit.DoUpdate();
            }
        }

        private bool CanEditSelectedArea()
        {
            return SelectedArea != null;
        }

        private void EditSelectedArea()
        {
            _nameInputViewCommand.Execute(SelectedArea.Name);
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                SelectedArea.Item.Name = _nameInputViewCommand.Name;
                _areaService.Save(SelectedArea.Item);
                SelectedArea.DoUpdate();
            }
        }

        private bool CanEditSelectedSummitGroup()
        {
            return SelectedSummitGroup != null;
        }

        private void EditSelectedSummitGroup()
        {
            _nameInputViewCommand.Execute(SelectedSummitGroup.Name);
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                SelectedSummitGroup.Item.Name = _nameInputViewCommand.Name;
                _summitGroupService.Save(SelectedSummitGroup.Item);
                SelectedSummitGroup.DoUpdate();
            }
        }

        private bool CanEditSelectedSummit()
        {
            return SelectedSummit != null;
        }

        private void EditSelectedSummit()
        {
            SelectedSummit.LoadData(_summitEditViewCommand.Execute(SelectedSummit.Item));
            if (!string.IsNullOrWhiteSpace(SelectedSummit.Item.Name))
            {
                _summitService.Save(SelectedSummit.Item);
            }
            SelectedSummit.DoUpdate();
        }

        private bool CanEditSelectedCountry()
        {
            return SelectedCountry != null;
        }

        private void EditSelectedCountry()
        {
            _nameInputViewCommand.Execute(SelectedCountry.Name);
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                SelectedCountry.Item.Name = _nameInputViewCommand.Name;
                _countryService.Save(SelectedCountry.Item);
                SelectedCountry.DoUpdate();
            }
        }

        private void RemoveArea()
        {
            _areaService.Delete(SelectedArea.Item);
            RefreshAreas();
        }

        private bool CanRemoveArea()
        {
            return SelectedArea != null && !_areaService.IsInUse(SelectedArea.Item);
        }

        private void RemoveCountry()
        {
            _countryService.Delete(SelectedCountry.Item);
            RefreshCountries();
        }

        private bool CanRemoveCountry()
        {
            return SelectedCountry != null && !_countryService.IsInUse(SelectedCountry.Item);
        }

        private void RemoveSummitGroup()
        {
            _summitGroupService.Delete(SelectedSummitGroup.Item);
            RefreshSummitGroups();
        }

        private bool CanRemoveSummitGroup()
        {
            return SelectedSummitGroup != null && !_summitGroupService.IsInUse(SelectedSummitGroup.Item);
        }

        private bool CanRemoveSummit()
        {
            return SelectedSummit != null && !_summitService.IsInUse(SelectedSummit.Item);
        }

        private void RemoveSummit()
        {
            _summitService.Delete(SelectedSummit.Item);
            RefreshSummitGroups();
        }

        private void RemoveRouteInCountry()
        {
            _routeService.Delete(SelectedRouteInCountry.Item);
            RefreshRoutesInSelectedCountry();
        }

        private bool CanRemoveRouteInCountry()
        {
            return SelectedRouteInCountry != null && !_routeService.IsInUse(SelectedRouteInCountry.Item);
        }

        private void RemoveRouteInArea()
        {
            _routeService.Delete(SelectedRouteInArea.Item);
            RefreshRoutesInSelectedArea();
        }

        private bool CanRemoveRouteInArea()
        {
            return SelectedRouteInArea != null && !_routeService.IsInUse(SelectedRouteInArea.Item);
        }

        private void RemoveRouteInSummitGroup()
        {
            _routeService.Delete(SelectedRouteInSummitGroup.Item);
            RefreshRoutesInSelectedSummitGroup();
        }

        private bool CanRemoveRouteInSummitGroup()
        {
            return SelectedRouteInSummitGroup != null && !_routeService.IsInUse(SelectedRouteInSummitGroup.Item);
        }

        private void RemoveRouteInSummit()
        {
            _routeService.Delete(SelectedRouteInSummit.Item);
            RefreshRoutesInSelectedSummit();
        }

        private bool CanRemoveRouteInSummit()
        {
            return SelectedRouteInSummit != null && !_routeService.IsInUse(SelectedRouteInSummit.Item);
        }

        private bool CanRemoveSelectedVariation()
        {
            return SelectedVariation != null && !_variationService.IsInUse(SelectedVariation.Item);
        }

        private void RemoveSelectedVariation()
        {
            _variationService.Delete(SelectedVariation.Item);
            RefreshVariationsOnLastSelectedRoute();
        }

        private void RefreshLogEntriesOnSelectedVariation()
        {
            if (SelectedVariation != null)
            {
                ClearLogEntries();
                foreach (LogEntry logEntry in _logEntryService.GetAllIn(SelectedVariation.Item))
                {
                    LogEntriesOnSelectedVariation.Add(
                        AppContext.Container.Resolve<ILogItemViewModel>().LoadData(logEntry));
                }
            }
        }

        private void ClearLogEntries()
        {
            LogEntriesOnSelectedVariation.Clear();
        }

        private bool CanRemoveSelectedLogEntry()
        {
            return SelectedLogEntry != null;
        }

        private void RemoveSelectedLogEntry()
        {
            _logEntryService.Delete(SelectedLogEntry.LogEntry);
            RefreshLogEntriesOnSelectedVariation();
        }

        private void RefreshAreas()
        {
            AreasInSelectedCountry.Clear();
            if (SelectedCountry != null)
            {
                foreach (Area area in _areaService.GetAllIn(SelectedCountry.Item))
                {
                    IItemWithNameViewModel<Area> areaViewModel = new ItemWithNameViewModel<Area>();
                    areaViewModel.LoadData(area);
                    AreasInSelectedCountry.Add(areaViewModel);
                }
            }
            RefreshSummitGroups();
            RefreshRoutesInSelectedArea();
        }

        private void RefreshRoutesInSelectedArea()
        {
            RoutesInSelectedArea.Clear();
            if (SelectedArea != null)
            {
                foreach (Route route in _routeService.GetRoutesIn(SelectedArea.Item))
                {
                    IItemWithNameViewModel<Route> routeViewModel = new ItemWithNameViewModel<Route>();
                    routeViewModel.LoadData(route);
                    RoutesInSelectedArea.Add(routeViewModel);
                }
            }
        }

        private void RefreshSummitGroups()
        {
            SummitGroupsInSelectedArea.Clear();
            if (SelectedArea != null)
            {
                foreach (SummitGroup summitGroup in _summitGroupService.GetAllIn(SelectedArea.Item))
                {
                    IItemWithNameViewModel<SummitGroup> summitGroupViewModel = new ItemWithNameViewModel<SummitGroup>();
                    summitGroupViewModel.LoadData(summitGroup);
                    SummitGroupsInSelectedArea.Add(summitGroupViewModel);
                }
            }
            RefreshSummits();
            RefreshRoutesInSelectedSummitGroup();
        }

        private void RefreshRoutesInSelectedSummitGroup()
        {
            RoutesInSelectedSummitGroup.Clear();
            if (SelectedSummitGroup != null)
            {
                foreach (Route route in _routeService.GetRoutesIn(SelectedSummitGroup.Item))
                {
                    IItemWithNameViewModel<Route> routeViewModel = new ItemWithNameViewModel<Route>();
                    routeViewModel.LoadData(route);
                    RoutesInSelectedSummitGroup.Add(routeViewModel);
                }
            }
        }

        private void RefreshSummits()
        {
            SummitsInSelectedSummitGroup.Clear();
            if (SelectedSummitGroup != null)
            {
                foreach (Summit summit in _summitService.GetAllIn(SelectedSummitGroup.Item))
                {
                    ISummitViewModel summitViewModel = new SummitViewModel();
                    summitViewModel.LoadData(summit);
                    SummitsInSelectedSummitGroup.Add(summitViewModel);
                }
            }

            RefreshRoutesInSelectedSummit();
        }

        private void RefreshRoutesInSelectedSummit()
        {
            RoutesInSelectedSummit.Clear();
            if (SelectedSummit != null)
            {
                foreach (Route route in _routeService.GetRoutesIn(SelectedSummit.Item))
                {
                    IRouteViewModel routeViewModel = new RouteViewModel();
                    routeViewModel.LoadData(route);
                    RoutesInSelectedSummit.Add(routeViewModel);
                }
            }
        }

        private void RefreshVariationsOnCountryRoute()
        {
            VariationsOnSelectedRoute.Clear();
            if (SelectedRouteInCountry != null)
            {
                foreach (Variation variation in _variationService.GetAllOn(SelectedRouteInCountry.Item))
                {
                    IVariationItemViewModel variationItemViewModel =
                        AppContext.Container.Resolve<IVariationItemViewModel>();
                    variationItemViewModel.LoadData(variation);
                    VariationsOnSelectedRoute.Add(variationItemViewModel);
                }
            }
            AutoShowLogEntries();
        }

        private void RefreshVariationsOnAreaRoute()
        {
            VariationsOnSelectedRoute.Clear();
            if (SelectedRouteInArea != null)
            {
                foreach (Variation variation in _variationService.GetAllOn(SelectedRouteInArea.Item))
                {
                    IVariationItemViewModel variationItemViewModel =
                        AppContext.Container.Resolve<IVariationItemViewModel>();
                    variationItemViewModel.LoadData(variation);
                    VariationsOnSelectedRoute.Add(variationItemViewModel);
                }
            }
            AutoShowLogEntries();
        }

        private void RefreshVariationsOnSummitGroupRoute()
        {
            VariationsOnSelectedRoute.Clear();
            if (SelectedRouteInSummitGroup != null)
            {
                foreach (Variation variation in _variationService.GetAllOn(SelectedRouteInSummitGroup.Item))
                {
                    IVariationItemViewModel variationItemViewModel =
                        AppContext.Container.Resolve<IVariationItemViewModel>();
                    variationItemViewModel.LoadData(variation);
                    VariationsOnSelectedRoute.Add(variationItemViewModel);
                }
            }
            AutoShowLogEntries();
        }

        private void RefreshVariationsOnSummitRoute()
        {
            VariationsOnSelectedRoute.Clear();
            if (SelectedRouteInSummit != null)
            {
                foreach (Variation variation in _variationService.GetAllOn(SelectedRouteInSummit.Item))
                {
                    IVariationItemViewModel variationItemViewModel =
                        AppContext.Container.Resolve<IVariationItemViewModel>();
                    variationItemViewModel.LoadData(variation);
                    VariationsOnSelectedRoute.Add(variationItemViewModel);
                }
            }
            AutoShowLogEntries();
        }

        private void AutoShowLogEntries()
        {
            if (VariationsOnSelectedRoute.Count == 1)
            {
                SelectedVariation = VariationsOnSelectedRoute.First();
            }
        }

        private void RefreshCountries()
        {
            Countries.Clear();
            foreach (Country country in _countryService.GetAll())
            {
                IItemWithNameViewModel<Country> itemViewModel = new ItemWithNameViewModel<Country>();
                itemViewModel.LoadData(country);
                Countries.Add(itemViewModel);
            }
            RefreshAreas();
            RefreshRoutesInSelectedCountry();
        }

        private void RefreshRoutesInSelectedCountry()
        {
            RoutesInSelectedCountry.Clear();
            if (SelectedCountry != null)
            {
                foreach (Route route in _routeService.GetRoutesIn(SelectedCountry.Item))
                {
                    IItemWithNameViewModel<Route> routeViewModel = new ItemWithNameViewModel<Route>();
                    routeViewModel.LoadData(route);
                    RoutesInSelectedCountry.Add(routeViewModel);
                }
            }
        }

        private bool CanAddCountry()
        {
            return true;
        }

        private void AddCountry()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                Country created = _countryService.Create(_nameInputViewCommand.Name);
                RefreshCountries();
                SelectedCountry = Countries.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private bool CanAddAreaInSelectedCountry()
        {
            return SelectedCountry != null;
        }

        private void AddAreaInSelectedCountry()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                Area created = _areaService.Create(SelectedCountry.Item, _nameInputViewCommand.Name);
                RefreshAreas();
                SelectedArea = AreasInSelectedCountry.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private void AddSummitGroupInSelectedArea()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                SummitGroup created = _summitGroupService.Create(SelectedArea.Item, _nameInputViewCommand.Name);
                RefreshSummitGroups();
                SelectedSummitGroup = SummitGroupsInSelectedArea.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private bool CanAddSummitGroupInSelectedArea()
        {
            return SelectedArea != null;
        }

        private bool CanAddSummitInSelectedSummitGroup()
        {
            return SelectedSummitGroup != null;
        }

        private void AddSummitInSelectedSummitGroup()
        {
            Summit summitData = _summitEditViewCommand.Execute(new Summit());
            if (!string.IsNullOrWhiteSpace(summitData.Name))
            {
                Summit created = _summitService.Create(SelectedSummitGroup.Item, summitData.Name,
                    summitData.SummitNumber, summitData.Rating);
                RefreshSummits();
                SelectedSummit = SummitsInSelectedSummitGroup.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private bool CanAddRouteInSelectedCountry()
        {
            return SelectedCountry != null;
        }

        private void AddRouteInSelectedCountry()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                Route created = _routeService.CreateIn(SelectedCountry.Item, _nameInputViewCommand.Name);
                RefreshRoutesInSelectedCountry();
                SelectedRouteInCountry = RoutesInSelectedCountry.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private bool CanAddRouteInSelectedArea()
        {
            return SelectedArea != null;
        }

        private void AddRouteInSelctedArea()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                Route created = _routeService.CreateIn(SelectedArea.Item, _nameInputViewCommand.Name);
                RefreshRoutesInSelectedArea();
                SelectedRouteInArea = RoutesInSelectedArea.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private bool CanAddRouteInSelectedSummmitGroup()
        {
            return SelectedSummitGroup != null;
        }

        private void AddRouteInSlectedSummitGroup()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                Route created = _routeService.CreateIn(SelectedSummitGroup.Item, _nameInputViewCommand.Name);
                RefreshRoutesInSelectedSummitGroup();
                SelectedRouteInSummitGroup = RoutesInSelectedSummitGroup.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private bool CanAddRouteInSelectedSummit()
        {
            return SelectedSummit != null;
        }

        private void AddRouteInSelectedSummit()
        {
            Route editedRoute = _routeOnSummitEditViewCommand.Execute(new Route());
            if (!string.IsNullOrWhiteSpace(editedRoute.Name))
            {
                Route created = _routeService.CreateIn(SelectedSummit.Item, editedRoute.Name, editedRoute.Rating);
                RefreshRoutesInSelectedSummit();
                SelectedRouteInSummit = RoutesInSelectedSummit.FirstOrDefault(x => x.Item.Id == created.Id);
            }
        }

        private bool CanAddVariationToSelectedRoute()
        {
            return SelectedRouteInCountry != null || SelectedRouteInArea != null || SelectedRouteInSummitGroup != null ||
                   SelectedRouteInSummit != null;
        }

        private void AddVariationToSelectedRoute()
        {
            _nameAndLevelInputViewCommand.Execute();
            if (_nameAndLevelInputViewCommand.DifficultyLevel != null)
            {
                Route selectedRoute = GetSelectedRoute();
                if (selectedRoute != null)
                {
                    Variation created = _variationService.Create(selectedRoute,
                        _nameAndLevelInputViewCommand.DifficultyLevel, _nameAndLevelInputViewCommand.Name);
                    RefreshVariationsOnLastSelectedRoute();
                    SelectedVariation = VariationsOnSelectedRoute.FirstOrDefault(x => x.Item.Id == created.Id);
                }
            }
        }

        private void RefreshVariationsOnLastSelectedRoute()
        {
            VariationsOnSelectedRoute.Clear();
            Route selectedRoute = GetSelectedRoute();
            if (selectedRoute != null)
            {
                foreach (Variation variation in _variationService.GetAllOn(selectedRoute))
                {
                    IVariationItemViewModel variationItemViewModel =
                        AppContext.Container.Resolve<IVariationItemViewModel>();
                    variationItemViewModel.LoadData(variation);
                    VariationsOnSelectedRoute.Add(variationItemViewModel);
                }
            }
        }

        private Route GetSelectedRoute()
        {
            IItemWithNameViewModel<Route> selectedRouteViewModel = new List<IItemWithNameViewModel<Route>>
            {
                SelectedRouteInCountry,
                SelectedRouteInArea,
                SelectedRouteInSummitGroup,
                SelectedRouteInSummit
            }.FirstOrDefault(x => x != null);

            return selectedRouteViewModel?.Item;
        }

        private bool CanAddLogEntryToSelectedVariation()
        {
            return SelectedVariation != null;
        }

        private void AddLogEntryToSelectedVariation()
        {
            if (_logEntryInputViewCommand.Execute())
            {
                LogEntry created = _logEntryService.Create(SelectedVariation.Item, _logEntryInputViewCommand.Date,
                    _logEntryInputViewCommand.Memo);
                RefreshLogEntriesOnSelectedVariation();
                SelectedLogEntry = LogEntriesOnSelectedVariation.FirstOrDefault(x => x.LogEntry.Id == created.Id);
            }
        }

        private bool CanManageDifficulties()
        {
            return true;
        }

        private void ManageDifficulties()
        {
            _difficultyManagementViewCommand.Execute();
            RefreshVariationsOnLastSelectedRoute();
        }
    }
}