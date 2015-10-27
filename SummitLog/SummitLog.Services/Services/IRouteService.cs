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
        void CreateIn(Country country, string routeName);

        /// <summary>
        ///     Erstellt eine neue Route in einem Gebiet
        /// </summary>
        /// <param name="area"></param>
        /// <param name="routeName"></param>
        void CreateIn(Area area, string routeName);

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
        void CreateIn(SummitGroup summitGroup, string routeName);

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
        void CreateIn(Summit summit, string routeName);

        /// <summary>
        ///     Liefert alle Routen eines Gipfels
        /// </summary>
        /// <param name="summit"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(Summit summit);
    }
}