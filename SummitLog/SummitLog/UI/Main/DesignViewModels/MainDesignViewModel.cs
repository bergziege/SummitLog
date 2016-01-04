using System;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main.DesignViewModels
{
    /// <summary>
    /// Design View Model der Hauptansicht
    /// </summary>
    public class MainDesignViewModel: ReactiveObject, IMainViewModel
    {
        private RelayCommand _removeSummitCommand;

        /// <summary>
        /// Erstellt eine neue Instanz des Design View Models
        /// </summary>
        public MainDesignViewModel()
        {
            Countries = new ObservableCollection<IItemWithNameViewModel<Country>>
            {
                new ItemWithNameDesignViewModel<Country>(),
                new ItemWithNameDesignViewModel<Country>()
            };

            AreasInSelectedCountry = new ObservableCollection<IItemWithNameViewModel<Area>>
            {
                new ItemWithNameDesignViewModel<Area>(),
                new ItemWithNameDesignViewModel<Area>()
            };

            SummitGroupsInSelectedArea = new ObservableCollection<IItemWithNameViewModel<SummitGroup>>
            {
               new ItemWithNameDesignViewModel<SummitGroup>(),
               new ItemWithNameDesignViewModel<SummitGroup>()
            };

            SummitsInSelectedSummitGroup = new ObservableCollection<IItemWithNameViewModel<Summit>>
            {
                new ItemWithNameDesignViewModel<Summit>(),
                new ItemWithNameDesignViewModel<Summit>()
            };

            RoutesInSelectedCountry = new ObservableCollection<IItemWithNameViewModel<Route>>
            {
                new ItemWithNameDesignViewModel<Route>(),
                new ItemWithNameDesignViewModel<Route>()
            };

            RoutesInSelectedArea = new ObservableCollection<IItemWithNameViewModel<Route>>
            {
                new ItemWithNameDesignViewModel<Route>(),
                new ItemWithNameDesignViewModel<Route>()
            };

            RoutesInSelectedSummitGroup = new ObservableCollection<IItemWithNameViewModel<Route>>
            {
                new ItemWithNameDesignViewModel<Route>(),
                new ItemWithNameDesignViewModel<Route>()
            };

            RoutesInSelectedSummit = new ObservableCollection<IItemWithNameViewModel<Route>>
            {
                new ItemWithNameDesignViewModel<Route>(),
                new ItemWithNameDesignViewModel<Route>()
            };

            VariationsOnSelectedRoute = new ObservableCollection<IVariationItemViewModel>
            {
                new VariationItemDesignViewModel(),
                new VariationItemDesignViewModel()
            };

            LogEntriesOnSelectedVariation = new ObservableCollection<ILogItemViewModel>
            {
                new LogItemDesignViewModel(),
                new LogItemDesignViewModel()
            };

            SelectedCountry = Countries.First();
            SelectedArea = AreasInSelectedCountry.First();
            SelectedSummitGroup = SummitGroupsInSelectedArea.First();
            SelectedSummit = SummitsInSelectedSummitGroup.First();

            SelectedRouteInCountry = RoutesInSelectedCountry.First();
            SelectedRouteInArea = RoutesInSelectedArea.First();
            SelectedRouteInSummitGroup = RoutesInSelectedSummitGroup.First();
            SelectedRouteInSummit = RoutesInSelectedSummit.First();

            SelectedVariation = VariationsOnSelectedRoute.First();

            SelectedLogEntry = LogEntriesOnSelectedVariation.First();
        }

        /// <summary>
        ///     Liefert die Liste aller Länder
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Country>> Countries { get; }

        /// <summary>
        ///     Liefert oder setzt das gewählte Land
        /// </summary>
        public IItemWithNameViewModel<Country> SelectedCountry { get; set; }

        /// <summary>
        ///     Liefert die Liste aller Gebiete im gewählten Land
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Area>> AreasInSelectedCountry { get; }

        /// <summary>
        ///     Liefert oder setzt das gewählte Gebiet
        /// </summary>
        public IItemWithNameViewModel<Area> SelectedArea { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Gipfelgruppen im gewählten Gebiet
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<SummitGroup>> SummitGroupsInSelectedArea { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Gipfelgruppe
        /// </summary>
        public IItemWithNameViewModel<SummitGroup> SelectedSummitGroup { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Gipfel in der Gewählten Gipfelgruppe
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Summit>> SummitsInSelectedSummitGroup { get; }

        /// <summary>
        ///     Liefert oder setzt den gewählten Gipfel
        /// </summary>
        public IItemWithNameViewModel<Summit> SelectedSummit { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Land
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedCountry { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route eines Landes
        /// </summary>
        public IItemWithNameViewModel<Route> SelectedRouteInCountry { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Gebiet
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedArea { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einem Gebiet
        /// </summary>
        public IItemWithNameViewModel<Route> SelectedRouteInArea { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen in der gewählten Gipfelgruppe
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedSummitGroup { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einer Gipfelgruppe
        /// </summary>
        public IItemWithNameViewModel<Route> SelectedRouteInSummitGroup { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen an einem gewählten Gipfel
        /// </summary>
        public ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedSummit { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route an einem Gipfel
        /// </summary>
        public IItemWithNameViewModel<Route> SelectedRouteInSummit { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Variationen einer gewählten Route (Land, Gebiet, Gruppe ODER Gipfel)
        /// </summary>
        public ObservableCollection<IVariationItemViewModel> VariationsOnSelectedRoute { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Variation
        /// </summary>
        public IVariationItemViewModel SelectedVariation { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Logeinträge zur gewählten Variation
        /// </summary>
        public ObservableCollection<ILogItemViewModel> LogEntriesOnSelectedVariation { get; }

        /// <summary>
        ///     Liefert oder setzt den gewählten Logeintrag
        /// </summary>
        public ILogItemViewModel SelectedLogEntry { get; set; }

        /// <summary>
        ///     Liefert ein Command um ein Land hinzuzufügen
        /// </summary>
        public RelayCommand AddCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um ein Gebiet zu einem gewählten Land hinzuzufügen
        /// </summary>
        public RelayCommand AddAreaInSelectedCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Gipfelgruppe zu einem gewählten Gebiet hinzuzufügen
        /// </summary>
        public RelayCommand AddSummitGroupInSelectedAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um ein Gipfel einer gewählten Gipfelgruppe hinzuzufügen
        /// </summary>
        public RelayCommand AddSummitInSelectedSummitGroupCommand { get; }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Land eine Route hinzuzufügen
        /// </summary>
        public RelayCommand AddRouteInSelectedCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Gebiet eine Route hinzuzufügen
        /// </summary>
        public RelayCommand AddRouteInSelectedAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um der gewählten Gipfelgruppe eine Route hinzuzufügen
        /// </summary>
        public RelayCommand AddRouteInSelectedSummitGroupCommnad { get; }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Gipfel eine Route hinzuzufügen
        /// </summary>
        public RelayCommand AddRouteInSelectedSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um einer gewählten Route eine Variation hinzuzufügen
        /// </summary>
        public RelayCommand AddVariationToSelectedRouteCommand { get; }

        /// <summary>
        ///     Liefert ein Command um der gewählten Variation ein Logeintrag hinzuzufügen
        /// </summary>
        public RelayCommand AddLogEntryToSelectedVariationCommand { get; }

        /// <summary>
        ///     Liefert ein Command für die Verwaltung der Schwierigkeitsgrad und Skalen
        /// </summary>
        public RelayCommand ManageDifficultiesCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Variation zu löschen
        /// </summary>
        public RelayCommand RemoveSelectedVariationCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Route in einem Land zu löschen
        /// </summary>
        public RelayCommand RemoveRouteInCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Route in einem Gebiet zu löschen
        /// </summary>
        public RelayCommand RemoveRouteInAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Route in einer Gipflegruppe zu löschen
        /// </summary>
        public RelayCommand RemoveRouteInSummitGroupCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Route an einem Gipfel zu löschen.
        /// </summary>
        public RelayCommand RemoveRouteInSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um den Gipfel zu entfernen
        /// </summary>
        public RelayCommand RemoveSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gipfelgruppe zu löschen
        /// </summary>
        public RelayCommand RemoveSummitGroupCommand { get; }

        /// <summary>
        ///     Líefert ein Command um Gebiet zu löschen
        /// </summary>
        public RelayCommand RemoveAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um ein Land zu löschen
        /// </summary>
        public RelayCommand RemoveCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um das gewählte Land zu speichern
        /// </summary>
        public RelayCommand EditSelectedCountryCommand { get; }

        /// <summary>
        ///     Liefert Command um die gewählte Gegend zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gipfelgruppe zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedSummitGroupCommand { get; }

        /// <summary>
        ///     Liefert ein Command um den gewählten Gipfel zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route im Land zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedRouteInCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route ineinem Gebiet zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedRouteInAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route in einer Gipfelgruppe zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedRouteInSummitGroupCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gruppe in einem Gipfel zu bearbeiten.
        /// </summary>
        public RelayCommand EditSelectedRouteInSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Variation zu bearbeiten.
        /// </summary>
        public RelayCommand EditSelectedVariationCommand { get; }

        /// <summary>
        ///     Liefert ein Command um den gewählten Logeintrag zu bearbeiten.
        /// </summary>
        public RelayCommand EditSelectedLogEntryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route zu bearbeiten
        /// </summary>
        public RelayCommand EditSelectedRouteCommand { get; }

        public void LoadData()
        {
            
        }

        /// <summary>
        /// Liefert ob Länder geladen werden
        /// </summary>
        public bool IsLoadingCountries { get; }

        /// <summary>
        /// Liefert ob Gegenden geladen werden
        /// </summary>
        public bool IsLoadingAreas { get; }

        /// <summary>
        /// Liefert ob Gipfelgruppen geladen werden
        /// </summary>
        public bool IsLoadingSummitGroups { get; }

        /// <summary>
        /// Liefert ob Gipfel geladen werden
        /// </summary>
        public bool IsLoadingSummits { get; }

        /// <summary>
        /// Liefert ob Wege im Land geladen werden
        /// </summary>
        public bool IsLoadingRoutesInCountry { get; }

        /// <summary>
        /// Liefert ob Wege in Gegend geladen werden
        /// </summary>
        public bool IsLoadingRoutesInArea { get; }

        /// <summary>
        /// Liefert ob Wege in Gipfelgruppe geladen werden
        /// </summary>
        public bool IsLoadingRoutesInSummitGroup { get; }

        /// <summary>
        /// Liefert ob Wege in Gipfel geladen werden
        /// </summary>
        public bool IsLoadingRoutesInSummit { get; }

        /// <summary>
        /// Liefert ob Variationen geladen werden
        /// </summary>
        public bool IsLoadingVariations { get; }

        /// <summary>
        /// Liefert ob Logeinträge geladen werden
        /// </summary>
        public bool IsLoadingLogs { get; }

        /// <summary>
        /// Liefert ein Command um den gewählten Logeintrag zu löschen.
        /// </summary>
        public RelayCommand RemoveSelectedLogEntryCommand { get; }
    }
}