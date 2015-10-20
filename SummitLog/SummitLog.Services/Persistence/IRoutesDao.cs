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
        ///     Erstellt eine neue Route in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <param name="route"></param>
        void CreateIn(Country country, Route route);
    }
}