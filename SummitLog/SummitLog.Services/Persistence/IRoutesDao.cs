using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Routen
    /// </summary>
    public interface IRoutesDao
    {
        /// <summary>
        ///     Liefert alle Routen direkt in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(Country country);

        /// <summary>
        ///     Liefert alle Routen direkt in einer Gegend
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(Area area);

        /// <summary>
        ///     Erstellt eine neue Route in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <param name="route"></param>
        Route CreateIn(Country country, Route route);

        /// <summary>
        ///     Erstellt eine neue Route in einer Gegend
        /// </summary>
        /// <param name="area"></param>
        /// <param name="route"></param>
        Route CreateIn(Area area, Route route);

        /// <summary>
        ///     Erstellt eine neue Route in einer Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <param name="route"></param>
        Route CreateIn(SummitGroup summitGroup, Route route);

        /// <summary>
        ///     Liefert alle Routen direkt in der Gipfelgruppe, nicht jedoch die der Einzelnen Gipfel
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(SummitGroup summitGroup);

        /// <summary>
        ///     Erstellt eine neue Route an einem Gipfel
        /// </summary>
        /// <param name="summit"></param>
        /// <param name="route"></param>
        Route CreateIn(Summit summit, Route route);

        /// <summary>
        ///     Liefert alle Routen eines Gipfels
        /// </summary>
        /// <param name="summit"></param>
        /// <returns></returns>
        IList<Route> GetRoutesIn(Summit summit);
    }
}