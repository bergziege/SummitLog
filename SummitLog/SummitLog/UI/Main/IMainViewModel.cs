using System.Collections.ObjectModel;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main
{
    /// <summary>
    ///     Schnittstelle für View Models für die Hauptansicht
    /// </summary>
    public interface IMainViewModel
    {
        /// <summary>
        ///     Liefert die Liste aller Länder
        /// </summary>
        ObservableCollection<IItemWithNameViewModel<Country>> Countries { get; }

        /// <summary>
        ///     Liefrt oder setzt das gewählte Land
        /// </summary>
        IItemWithNameViewModel<Country> SelectedCountry { get; set; }

        /// <summary>
        ///     Liefert die Liste aller Gebiete im gewählten Land
        /// </summary>
        ObservableCollection<IItemWithNameViewModel<Area>> AreasInSelectedCountry { get; }

        /// <summary>
        ///     Liefert oder setzt das gewählte Gebiet
        /// </summary>
        IItemWithNameViewModel<Area> SelectedArea { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Gipfelgruppen im gewählten Gebiet
        /// </summary>
        ObservableCollection<IItemWithNameViewModel<SummitGroup>> SummitGroupsInSelectedArea { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Gipfelgruppe
        /// </summary>
        IItemWithNameViewModel<SummitGroup> SelectedSummitGroup { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Gipfel in der Gewählten Gipfelgruppe
        /// </summary>
        ObservableCollection<ISummitViewModel> SummitsInSelectedSummitGroup { get; }

        /// <summary>
        ///     Liefert oder setzt den gewählten Gipfel
        /// </summary>
        ISummitViewModel SelectedSummit { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Land
        /// </summary>
        ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedCountry { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route eines Landes
        /// </summary>
        IItemWithNameViewModel<Route> SelectedRouteInCountry { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Gebiet
        /// </summary>
        ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedArea { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einem Gebiet
        /// </summary>
        IItemWithNameViewModel<Route> SelectedRouteInArea { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen in der gewählten Gipfelgruppe
        /// </summary>
        ObservableCollection<IItemWithNameViewModel<Route>> RoutesInSelectedSummitGroup { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einer Gipfelgruppe
        /// </summary>
        IItemWithNameViewModel<Route> SelectedRouteInSummitGroup { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen an einem gewählten Gipfel
        /// </summary>
        ObservableCollection<IRouteViewModel> RoutesInSelectedSummit { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route an einem Gipfel
        /// </summary>
        IRouteViewModel SelectedRouteInSummit { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Variationen einer gewählten Route (Land, Gebiet, Gruppe ODER Gipfel)
        /// </summary>
        ObservableCollection<IVariationItemViewModel> VariationsOnSelectedRoute { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Variation
        /// </summary>
        IVariationItemViewModel SelectedVariation { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Logeinträge zur gewählten Variation
        /// </summary>
        ObservableCollection<ILogItemViewModel> LogEntriesOnSelectedVariation { get; }

        /// <summary>
        ///     Liefert oder setzt den gewählten Logeintrag
        /// </summary>
        ILogItemViewModel SelectedLogEntry { get; set; }

        /// <summary>
        ///     Liefert ein Command um ein Land hinzuzufügen
        /// </summary>
        RelayCommand AddCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um ein Gebiet zu einem gewählten Land hinzuzufügen
        /// </summary>
        RelayCommand AddAreaInSelectedCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Gipfelgruppe zu einem gewählten Gebiet hinzuzufügen
        /// </summary>
        RelayCommand AddSummitGroupInSelectedAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um ein Gipfel einer gewählten Gipfelgruppe hinzuzufügen
        /// </summary>
        RelayCommand AddSummitInSelectedSummitGroupCommand { get; }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Land eine Route hinzuzufügen
        /// </summary>
        RelayCommand AddRouteInSelectedCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Gebiet eine Route hinzuzufügen
        /// </summary>
        RelayCommand AddRouteInSelectedAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um der gewählten Gipfelgruppe eine Route hinzuzufügen
        /// </summary>
        RelayCommand AddRouteInSelectedSummitGroupCommnad { get; }

        /// <summary>
        ///     Liefert ein Command um dem gewählten Gipfel eine Route hinzuzufügen
        /// </summary>
        RelayCommand AddRouteInSelectedSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um einer gewählten Route eine Variation hinzuzufügen
        /// </summary>
        RelayCommand AddVariationToSelectedRouteCommand { get; }

        /// <summary>
        ///     Liefert ein Command um der gewählten Variation ein Logeintrag hinzuzufügen
        /// </summary>
        RelayCommand AddLogEntryToSelectedVariationCommand { get; }

        /// <summary>
        ///     Liefert ein Command für die Verwaltung der Schwierigkeitsgrad und Skalen
        /// </summary>
        RelayCommand ManageDifficultiesCommand { get; }

        /// <summary>
        ///     Liefert ob Länder geladen werden
        /// </summary>
        bool IsLoadingCountries { get; }

        /// <summary>
        ///     Liefert ob Gegenden geladen werden
        /// </summary>
        bool IsLoadingAreas { get; }

        /// <summary>
        ///     Liefert ob Gipfelgruppen geladen werden
        /// </summary>
        bool IsLoadingSummitGroups { get; }

        /// <summary>
        ///     Liefert ob Gipfel geladen werden
        /// </summary>
        bool IsLoadingSummits { get; }

        /// <summary>
        ///     Liefert ob Wege im Land geladen werden
        /// </summary>
        bool IsLoadingRoutesInCountry { get; }

        /// <summary>
        ///     Liefert ob Wege in Gegend geladen werden
        /// </summary>
        bool IsLoadingRoutesInArea { get; }

        /// <summary>
        ///     Liefert ob Wege in Gipfelgruppe geladen werden
        /// </summary>
        bool IsLoadingRoutesInSummitGroup { get; }

        /// <summary>
        ///     Liefert ob Wege in Gipfel geladen werden
        /// </summary>
        bool IsLoadingRoutesInSummit { get; }

        /// <summary>
        ///     Liefert ob Variationen geladen werden
        /// </summary>
        bool IsLoadingVariations { get; }

        /// <summary>
        ///     Liefert ob Logeinträge geladen werden
        /// </summary>
        bool IsLoadingLogs { get; }

        /// <summary>
        ///     Liefert ein Command um den gewählten Logeintrag zu löschen.
        /// </summary>
        RelayCommand RemoveSelectedLogEntryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Variation zu löschen
        /// </summary>
        RelayCommand RemoveSelectedVariationCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Route in einem Land zu löschen
        /// </summary>
        RelayCommand RemoveRouteInCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Route in einem Gebiet zu löschen
        /// </summary>
        RelayCommand RemoveRouteInAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Route in einer Gipflegruppe zu löschen
        /// </summary>
        RelayCommand RemoveRouteInSummitGroupCommand { get; }

        /// <summary>
        ///     Liefert ein Command um eine Route an einem Gipfel zu löschen.
        /// </summary>
        RelayCommand RemoveRouteInSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um den Gipfel zu entfernen
        /// </summary>
        RelayCommand RemoveSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gipfelgruppe zu löschen
        /// </summary>
        RelayCommand RemoveSummitGroupCommand { get; }

        /// <summary>
        ///     Líefert ein Command um Gebiet zu löschen
        /// </summary>
        RelayCommand RemoveAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um ein Land zu löschen
        /// </summary>
        RelayCommand RemoveCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um das gewählte Land zu speichern
        /// </summary>
        RelayCommand EditSelectedCountryCommand { get; }

        /// <summary>
        ///     Liefert Command um die gewählte Gegend zu bearbeiten
        /// </summary>
        RelayCommand EditSelectedAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gipfelgruppe zu bearbeiten
        /// </summary>
        RelayCommand EditSelectedSummitGroupCommand { get; }

        /// <summary>
        ///     Liefert ein Command um den gewählten Gipfel zu bearbeiten
        /// </summary>
        RelayCommand EditSelectedSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route im Land zu bearbeiten
        /// </summary>
        RelayCommand EditSelectedRouteInCountryCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route ineinem Gebiet zu bearbeiten
        /// </summary>
        RelayCommand EditSelectedRouteInAreaCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Route in einer Gipfelgruppe zu bearbeiten
        /// </summary>
        RelayCommand EditSelectedRouteInSummitGroupCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Gruppe in einem Gipfel zu bearbeiten.
        /// </summary>
        RelayCommand EditSelectedRouteInSummitCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die gewählte Variation zu bearbeiten.
        /// </summary>
        RelayCommand EditSelectedVariationCommand { get; }

        /// <summary>
        ///     Liefert ein Command um den gewählten Logeintrag zu bearbeiten.
        /// </summary>
        RelayCommand EditSelectedLogEntryCommand { get; }

        /// <summary>
        ///     Liefert ein Command zur Anzeige der DB Einstellungen
        /// </summary>
        RelayCommand ShowDbSettingsCommand { get; }

        /// <summary>
        ///     Lädt die relevanten Daten des View Models
        /// </summary>
        void LoadData();
    }
}