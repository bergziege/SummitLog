using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main.ViewModels
{
    /// <summary>
    ///     View Model des Hauptfensters
    /// </summary>
    public class MainViewModel : ReactiveObject, IMainViewModel
    {
        private readonly IAreaService _areaService;
        private readonly ICountryService _countryService;
        private readonly ILogEntryService _logEntryService;
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
        protected MainViewModel(ICountryService countryService, IAreaService areaService,
            ISummitGroupService summitGroupService, ISummitService summitService, IRouteService routeService,
            IVariationService variationService, ILogEntryService logEntryService)
        {
            _countryService = countryService;
            _areaService = areaService;
            _summitGroupService = summitGroupService;
            _summitService = summitService;
            _routeService = routeService;
            _variationService = variationService;
            _logEntryService = logEntryService;
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
            set { this.RaiseAndSetIfChanged(ref _selectedCountry, value); }
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
            set { this.RaiseAndSetIfChanged(ref _selectedArea, value); }
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
            set { this.RaiseAndSetIfChanged(ref _selectedSummitGroup, value); }
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
            set { this.RaiseAndSetIfChanged(ref _selectedSummit, value); }
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
            set { this.RaiseAndSetIfChanged(ref _selectedRouteInCountry, value); }
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
            set { this.RaiseAndSetIfChanged(ref _selectedRouteInArea, value); }
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
            set { this.RaiseAndSetIfChanged(ref _selectedRouteInSummitGroup, value); }
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
            set { this.RaiseAndSetIfChanged(ref _selectedRouteInSummit, value); }
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
            set { this.RaiseAndSetIfChanged(ref _selectedVariation, value); }
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
        ///     Lädt die relevanten Daten des View Models
        /// </summary>
        public void LoadData()
        {
        }

        private bool CanAddCountry()
        {
            return true;
        }

        private void AddCountry()
        {
            throw new NotImplementedException();
        }

        private bool CanAddAreaInSelectedCountry()
        {
            return SelectedCountry != null;
        }

        private void AddAreaInSelectedCountry()
        {
            throw new NotImplementedException();
        }

        private void AddSummitGroupInSelectedArea()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private bool CanAddRouteInSelectedCountry()
        {
            return SelectedCountry != null;
        }

        private void AddRouteInSelectedCountry()
        {
            throw new NotImplementedException();
        }

        private bool CanAddRouteInSelectedArea()
        {
            return SelectedArea != null;
        }

        private void AddRouteInSelctedArea()
        {
            throw new NotImplementedException();
        }

        private bool CanAddRouteInSelectedSummmitGroup()
        {
            return SelectedSummitGroup != null;
        }

        private void AddRouteInSlectedSummitGroup()
        {
            throw new NotImplementedException();
        }

        private bool CanAddRouteInSelectedSummit()
        {
            return SelectedSummit != null;
        }

        private void AddRouteInSelectedSummit()
        {
            throw new NotImplementedException();
        }

        private bool CanAddVariationToSelectedRoute()
        {
            return SelectedRouteInCountry != null || SelectedRouteInArea != null || SelectedRouteInSummitGroup != null ||
                   SelectedRouteInSummit != null;
        }

        private void AddVariationToSelectedRoute()
        {
            throw new NotImplementedException();
        }

        private bool CanAddLogEntryToSelectedVariation()
        {
            return SelectedVariation != null;
        }

        private void AddLogEntryToSelectedVariation()
        {
            throw new NotImplementedException();
        }

        private bool CanManageDifficulties()
        {
            return true;
        }

        private void ManageDifficulties()
        {
            throw new NotImplementedException();
        }
    }
}