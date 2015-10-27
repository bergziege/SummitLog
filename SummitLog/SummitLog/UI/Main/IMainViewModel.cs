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
        ObservableCollection<Country> Countries { get; }

        /// <summary>
        ///     Liefert oder setzt das gewählte Land
        /// </summary>
        Country SelectedCountry { get; set; }

        /// <summary>
        ///     Liefert die Liste aller Gebiete im gewählten Land
        /// </summary>
        ObservableCollection<Area> AreasInSelectedCountry { get; }

        /// <summary>
        ///     Liefert oder setzt das gewählte Gebiet
        /// </summary>
        Area SelectedArea { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Gipfelgruppen im gewählten Gebiet
        /// </summary>
        ObservableCollection<SummitGroup> SummitGroupsInSelectedArea { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Gipfelgruppe
        /// </summary>
        SummitGroup SelectedSummitGroup { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Gipfel in der Gewählten Gipfelgruppe
        /// </summary>
        ObservableCollection<Summit> SummitsInSelectedSummitGroup { get; }

        /// <summary>
        ///     Liefert oder setzt den gewählten Gipfel
        /// </summary>
        Summit SelectedSummit { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Land
        /// </summary>
        ObservableCollection<Route> RoutesInSelectedCountry { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route eines Landes
        /// </summary>
        Route SelectedRouteInCountry { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen im gewählten Gebiet
        /// </summary>
        ObservableCollection<Route> RoutesInSelectedArea { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einem Gebiet
        /// </summary>
        Route SelectedRouteInArea { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen in der gewählten Gipfelgruppe
        /// </summary>
        ObservableCollection<Route> RoutesInSelectedSummitGroup { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route in einer Gipfelgruppe
        /// </summary>
        Route SelectedRouteInSummitGroup { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Routen an einem gewählten Gipfel
        /// </summary>
        ObservableCollection<Route> RoutesInSelectedSummit { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Route an einem Gipfel
        /// </summary>
        Route SelectedRouteInSummit { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Variationen einer gewählten Route (Land, Gebiet, Gruppe ODER Gipfel)
        /// </summary>
        ObservableCollection<Variation> VariationsOnSelectedRoute { get; }

        /// <summary>
        ///     Liefert oder setzt die gewählte Variation
        /// </summary>
        Variation SelectedVariation { get; set; }

        /// <summary>
        ///     Liefert eine Liste aller Logeinträge zur gewählten Variation
        /// </summary>
        ObservableCollection<LogEntry> LogEntriesOnSelectedVariation { get; }

        /// <summary>
        ///     Liefert oder setzt den gewählten Logeintrag
        /// </summary>
        LogEntry SelectedLogEntry { get; set; }

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
        /// Lädt die relevanten Daten des View Models
        /// </summary>
        void LoadData();
    }
}