using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Routen
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        ///     Liefert alle Routen in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(Country country);

        /// <summary>
        ///     Erstellt eine Route mit dem übergebenen Namen im Land
        /// </summary>
        /// <param name="country"></param>
        /// <param name="routeName"></param>
        Route CreateIn(Country country, string routeName);

        /// <summary>
        ///     Erstellt eine neue Route in einem Gebiet
        /// </summary>
        /// <param name="area"></param>
        /// <param name="routeName"></param>
        Route CreateIn(Area area, string routeName);

        /// <summary>
        ///     LIefert alle Routen in einem Gebiet
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(Area area);

        /// <summary>
        ///     Erstellt eine Route in einer Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <param name="routeName"></param>
        Route CreateIn(SummitGroup summitGroup, string routeName);

        /// <summary>
        ///     Liefert alle Routen einer Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(SummitGroup summitGroup);

        /// <summary>
        ///     Erstellt eine Route auf einem Gipfel
        /// </summary>
        /// <param name="summit"></param>
        /// <param name="routeName"></param>
        /// <param name="rating"></param>
        Route CreateIn(Summit summit, string routeName, double rating = 0);

        /// <summary>
        ///     Liefert alle Routen eines Gipfels
        /// </summary>
        /// <param name="summit"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(Summit summit);

        /// <summary>
        ///     Liefert ob die Route in Verwendung ist
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        bool IsInUse(Route route);

        /// <summary>
        ///     Löscht eine Route wenn idese nicht mehr in Verwendung ist
        /// </summary>
        /// <param name="route"></param>
        void Delete(Route route);

        /// <summary>
        /// Speichert eine Route
        /// </summary>
        /// <param name="item"></param>
        void Save(Route item);
    }
}