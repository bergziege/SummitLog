using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.Common;
using SummitLog.UI.DifficultyManagement.ViewCommands;
using SummitLog.UI.LogEntryInput.ViewCommands;
using SummitLog.UI.NameAndLevelInput.ViewCommands;
using SummitLog.UI.NameInput;

namespace SummitLog.UI.Main.ViewModels
{
    /// <summary>
    ///     View Model des Hauptfensters
    /// </summary>
    public class MainViewModel : ReactiveObject, IMainViewModel, IWeakEventListener
    {
        private readonly IAreaService _areaService;
        private readonly ICountryService _countryService;
        private readonly DifficultyManagementViewCommand _difficultyManagementViewCommand;
        private readonly LogEntryInputViewCommand _logEntryInputViewCommand;
        private readonly ILogEntryService _logEntryService;
        private readonly NameAndLevelInputViewCommand _nameAndLevelInputViewCommand;
        private readonly NameInputViewCommand _nameInputViewCommand;
        private readonly IRouteService _routeService;
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
        private RelayCommand _manageDifficultiesCommand;
        private RelayCommand _removeRouteInAreaCommand;
        private RelayCommand _removeRouteInCountryCommand;
        private RelayCommand _removeRouteInSummitCommand;
        private RelayCommand _removeRouteInSummitGroupCommand;
        private RelayCommand _removeSelectedLogEntryCommand;
        private RelayCommand _removeSelectedVariationCommand;
        private Area _selectedArea;
        private Country _selectedCountry;
        private LogEntry _selectedLogEntry;
        private Route _selectedRouteInArea;
        private Route _selectedRouteInCountry;
        private Route _selectedRouteInSummit;
        private Route _selectedRouteInSummitGroup;
        private Summit _selectedSummit;
        private SummitGroup _selectedSummitGroup;
        private Variation _selectedVariation;
        private RelayCommand _removeSummitCommand;
        private RelayCommand _removeSummitGroupCommand;

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
        /// <param name="nameInputViewCommand"></param>
        /// <param name="difficultyManagementViewCommand"></param>
        /// <param name="nameAndLevelInputViewCommand"></param>
        /// <param name="logEntryInputViewCommand"></param>
        public MainViewModel(ICountryService countryService, IAreaService areaService,
            ISummitGroupService summitGroupService, ISummitService summitService, IRouteService routeService,
            IVariationService variationService, ILogEntryService logEntryService,
            NameInputViewCommand nameInputViewCommand, DifficultyManagementViewCommand difficultyManagementViewCommand,
            NameAndLevelInputViewCommand nameAndLevelInputViewCommand
            , LogEntryInputViewCommand logEntryInputViewCommand)
        {
            _countryService = countryService;
            _areaService = areaService;
            _summitGroupService = summitGroupService;
            _summitService = summitService;
            _routeService = routeService;
            _variationService = variationService;
            _logEntryService = logEntryService;
            _nameInputViewCommand = nameInputViewCommand;
            _difficultyManagementViewCommand = difficultyManagementViewCommand;
            _nameAndLevelInputViewCommand = nameAndLevelInputViewCommand;
            _logEntryInputViewCommand = logEntryInputViewCommand;
        }

        /// <summary>
        ///     Liefert die Liste aller Länder
        /// </summary>
        public ObservableCollection<Country> Countries { get; } = new ObservableCollection<Country>();

        /// <summary>
        ///     Liefert oder setzt das gewählte Land
        /// </summary>
        public Country SelectedCountry
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
        public ObservableCollection<Area> AreasInSelectedCountry { get; } = new ObservableCollection<Area>();

        /// <summary>
        ///     Liefert oder setzt das gewählte Gebiet
        /// </summary>
        public Area SelectedArea
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
        public ObservableCollection<SummitGroup> SummitGroupsInSelectedArea { get; } =
            new ObservableCollection<SummitGroup>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Gipfelgruppe
        /// </summary>
        public SummitGroup SelectedSummitGroup
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
        public ObservableCollection<Summit> SummitsInSelectedSummitGroup { get; } = new ObservableCollection<Summit>();

        /// <summary>
        ///     Liefert oder setzt den gewählten Gipfel
        /// </summary>
        public Summit SelectedSummit
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
        public ObservableCollection<Route> RoutesInSelectedCountry { get; } = new ObservableCollection<Route>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Route eines Landes
        /// </summary>
        public Route SelectedRouteInCountry
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

                    RefreshVariationsOnCountryRoute();
                    LogEntriesOnSelectedVariation.Clear();
                }
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Gebiet
        /// </summary>
        public ObservableCollection<Route> RoutesInSelectedArea { get; } = new ObservableCollection<Route>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einem Gebiet
        /// </summary>
        public Route SelectedRouteInArea
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

                    RefreshVariationsOnAreaRoute();
                    LogEntriesOnSelectedVariation.Clear();
                }
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Routen in der gewählten Gipfelgruppe
        /// </summary>
        public ObservableCollection<Route> RoutesInSelectedSummitGroup { get; } = new ObservableCollection<Route>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einer Gipfelgruppe
        /// </summary>
        public Route SelectedRouteInSummitGroup
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

                    RefreshVariationsOnSummitGroupRoute();
                    LogEntriesOnSelectedVariation.Clear();
                }
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Routen an einem gewählten Gipfel
        /// </summary>
        public ObservableCollection<Route> RoutesInSelectedSummit { get; } = new ObservableCollection<Route>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Route an einem Gipfel
        /// </summary>
        public Route SelectedRouteInSummit
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

                    RefreshVariationsOnSummitRoute();
                    LogEntriesOnSelectedVariation.Clear();
                }
            }
        }

        /// <summary>
        ///     Liefert eine Liste aller Variationen einer gewählten Route (Land, Gebiet, Gruppe ODER Gipfel)
        /// </summary>
        public ObservableCollection<Variation> VariationsOnSelectedRoute { get; } =
            new ObservableCollection<Variation>();

        /// <summary>
        ///     Liefert oder setzt die gewählte Variation
        /// </summary>
        public Variation SelectedVariation
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
        public ObservableCollection<LogEntry> LogEntriesOnSelectedVariation { get; } =
            new ObservableCollection<LogEntry>();

        /// <summary>
        ///     Liefert oder setzt den gewählten Logeintrag
        /// </summary>
        public LogEntry SelectedLogEntry
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

        private void RemoveSummitGroup()
        {
            _summitGroupService.Delete(SelectedSummitGroup);
            RefreshSummitGroups();
        }

        private bool CanRemoveSummitGroup()
        {
            return SelectedSummitGroup != null && !_summitGroupService.IsInUse(SelectedSummitGroup);
        }

        private bool CanRemoveSummit()
        {
            return SelectedSummit != null && !_summitService.IsInUse(SelectedSummit);
        }

        private void RemoveSummit()
        {
            _summitService.Delete(SelectedSummit);
            RefreshSummitGroups();
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

        private void RemoveRouteInCountry()
        {
            _routeService.Delete(SelectedRouteInCountry);
            RefreshRoutesInSelectedCountry();
        }

        private bool CanRemoveRouteInCountry()
        {
            return SelectedRouteInCountry != null && !_routeService.IsInUse(SelectedRouteInCountry);
        }

        private void RemoveRouteInArea()
        {
            _routeService.Delete(SelectedRouteInArea);
            RefreshRoutesInSelectedArea();
        }

        private bool CanRemoveRouteInArea()
        {
            return SelectedRouteInArea != null && !_routeService.IsInUse(SelectedRouteInArea);
        }

        private void RemoveRouteInSummitGroup()
        {
            _routeService.Delete(SelectedRouteInSummitGroup);
            RefreshRoutesInSelectedSummitGroup();
        }

        private bool CanRemoveRouteInSummitGroup()
        {
            return SelectedRouteInSummitGroup != null && !_routeService.IsInUse(SelectedRouteInSummitGroup);
        }

        private void RemoveRouteInSummit()
        {
            _routeService.Delete(SelectedRouteInSummit);
            RefreshRoutesInSelectedSummit();
        }

        private bool CanRemoveRouteInSummit()
        {
            return SelectedRouteInSummit != null && !_routeService.IsInUse(SelectedRouteInSummit);
        }

        private bool CanRemoveSelectedVariation()
        {
            return SelectedVariation != null && !_variationService.IsInUse(SelectedVariation);
        }

        private void RemoveSelectedVariation()
        {
            _variationService.Delete(SelectedVariation);
            RefreshVariationsOnLastSelectedRoute();
        }

        private void RefreshLogEntriesOnSelectedVariation()
        {
            if (SelectedVariation != null)
            {
                LogEntriesOnSelectedVariation.Clear();
                foreach (var logEntry in _logEntryService.GetAllIn(SelectedVariation))
                {
                    LogEntriesOnSelectedVariation.Add(logEntry);
                }
            }
        }

        private bool CanRemoveSelectedLogEntry()
        {
            return SelectedLogEntry != null;
        }

        private void RemoveSelectedLogEntry()
        {
            _logEntryService.Delete(SelectedLogEntry);
            RefreshLogEntriesOnSelectedVariation();
        }

        private void RefreshAreas()
        {
            AreasInSelectedCountry.Clear();
            if (SelectedCountry != null)
            {
                foreach (var area in _areaService.GetAllIn(SelectedCountry))
                {
                    AreasInSelectedCountry.Add(area);
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
                foreach (var route in _routeService.GetRoutesIn(SelectedArea))
                {
                    RoutesInSelectedArea.Add(route);
                }
            }
        }

        private void RefreshSummitGroups()
        {
            SummitGroupsInSelectedArea.Clear();
            if (SelectedArea != null)
            {
                foreach (var summitGroup in _summitGroupService.GetAllIn(SelectedArea))
                {
                    SummitGroupsInSelectedArea.Add(summitGroup);
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
                foreach (var route in _routeService.GetRoutesIn(SelectedSummitGroup))
                {
                    RoutesInSelectedSummitGroup.Add(route);
                }
            }
        }

        private void RefreshSummits()
        {
            SummitsInSelectedSummitGroup.Clear();
            if (SelectedSummitGroup != null)
            {
                foreach (var summit in _summitService.GetAllIn(SelectedSummitGroup))
                {
                    SummitsInSelectedSummitGroup.Add(summit);
                }
            }

            RefreshRoutesInSelectedSummit();
        }

        private void RefreshRoutesInSelectedSummit()
        {
            RoutesInSelectedSummit.Clear();
            if (SelectedSummit != null)
            {
                foreach (var route in _routeService.GetRoutesIn(SelectedSummit))
                {
                    RoutesInSelectedSummit.Add(route);
                }
            }
        }

        private void RefreshVariationsOnCountryRoute()
        {
            VariationsOnSelectedRoute.Clear();
            if (SelectedRouteInCountry != null)
            {
                foreach (var variation in _variationService.GetAllOn(SelectedRouteInCountry))
                {
                    VariationsOnSelectedRoute.Add(variation);
                }
            }
        }

        private void RefreshVariationsOnAreaRoute()
        {
            VariationsOnSelectedRoute.Clear();
            if (SelectedRouteInArea != null)
            {
                foreach (var variation in _variationService.GetAllOn(SelectedRouteInArea))
                {
                    VariationsOnSelectedRoute.Add(variation);
                }
            }
        }

        private void RefreshVariationsOnSummitGroupRoute()
        {
            VariationsOnSelectedRoute.Clear();
            if (SelectedRouteInSummitGroup != null)
            {
                foreach (var variation in _variationService.GetAllOn(SelectedRouteInSummitGroup))
                {
                    VariationsOnSelectedRoute.Add(variation);
                }
            }
        }

        private void RefreshVariationsOnSummitRoute()
        {
            VariationsOnSelectedRoute.Clear();
            if (SelectedRouteInSummit != null)
            {
                foreach (var variation in _variationService.GetAllOn(SelectedRouteInSummit))
                {
                    VariationsOnSelectedRoute.Add(variation);
                }
            }
        }

        private void RefreshCountries()
        {
            Countries.Clear();
            foreach (var country in _countryService.GetAll())
            {
                Countries.Add(country);
            }
            RefreshAreas();
            RefreshRoutesInSelectedCountry();
        }

        private void RefreshRoutesInSelectedCountry()
        {
            RoutesInSelectedCountry.Clear();
            if (SelectedCountry != null)
            {
                foreach (var route in _routeService.GetRoutesIn(SelectedCountry))
                {
                    RoutesInSelectedCountry.Add(route);
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
                _countryService.Create(_nameInputViewCommand.Name);
            }
            RefreshCountries();
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
                _areaService.Create(SelectedCountry, _nameInputViewCommand.Name);
            }
            RefreshAreas();
        }

        private void AddSummitGroupInSelectedArea()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                _summitGroupService.Create(SelectedArea, _nameInputViewCommand.Name);
            }
            RefreshSummitGroups();
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
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                _summitService.Create(SelectedSummitGroup, _nameInputViewCommand.Name);
            }
            RefreshSummits();
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
                _routeService.CreateIn(SelectedCountry, _nameInputViewCommand.Name);
            }
            RefreshRoutesInSelectedCountry();
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
                _routeService.CreateIn(SelectedArea, _nameInputViewCommand.Name);
            }
            RefreshRoutesInSelectedArea();
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
                _routeService.CreateIn(SelectedSummitGroup, _nameInputViewCommand.Name);
            }
            RefreshRoutesInSelectedSummitGroup();
        }

        private bool CanAddRouteInSelectedSummit()
        {
            return SelectedSummit != null;
        }

        private void AddRouteInSelectedSummit()
        {
            _nameInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameInputViewCommand.Name))
            {
                _routeService.CreateIn(SelectedSummit, _nameInputViewCommand.Name);
            }
            RefreshRoutesInSelectedSummit();
        }

        private bool CanAddVariationToSelectedRoute()
        {
            return SelectedRouteInCountry != null || SelectedRouteInArea != null || SelectedRouteInSummitGroup != null ||
                   SelectedRouteInSummit != null;
        }

        private void AddVariationToSelectedRoute()
        {
            _nameAndLevelInputViewCommand.Execute();
            if (!string.IsNullOrWhiteSpace(_nameAndLevelInputViewCommand.Name) &&
                _nameAndLevelInputViewCommand.DifficultyLevel != null)
            {
                var selectedRoute = GetSelectedRoute();
                if (selectedRoute != null)
                {
                    _variationService.Create(_nameAndLevelInputViewCommand.Name, selectedRoute,
                        _nameAndLevelInputViewCommand.DifficultyLevel);
                }
            }
            RefreshVariationsOnLastSelectedRoute();
        }

        private void RefreshVariationsOnLastSelectedRoute()
        {
            VariationsOnSelectedRoute.Clear();
            var selectedRoute = GetSelectedRoute();
            if (selectedRoute != null)
            {
                foreach (var variation in _variationService.GetAllOn(selectedRoute))
                {
                    VariationsOnSelectedRoute.Add(variation);
                }
            }
        }

        private Route GetSelectedRoute()
        {
            return
                new List<Route>
                {
                    SelectedRouteInCountry,
                    SelectedRouteInArea,
                    SelectedRouteInSummitGroup,
                    SelectedRouteInSummit
                }.FirstOrDefault(x => x != null);
        }

        private bool CanAddLogEntryToSelectedVariation()
        {
            return SelectedVariation != null;
        }

        private void AddLogEntryToSelectedVariation()
        {
            if (_logEntryInputViewCommand.Execute())
            {
                _logEntryService.Create(_logEntryInputViewCommand.Memo, _logEntryInputViewCommand.Date,
                    SelectedVariation);
                RefreshLogEntriesOnSelectedVariation();
            }
        }

        private bool CanManageDifficulties()
        {
            return true;
        }

        private void ManageDifficulties()
        {
            _difficultyManagementViewCommand.Execute();
        }
    }
}