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
    }
}