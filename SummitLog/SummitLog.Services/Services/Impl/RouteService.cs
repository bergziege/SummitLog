using System;
using System.Collections.Generic;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    /// Service für Routen
    /// </summary>
    public class RouteService: IRouteService
    {
        private readonly IRoutesDao _routesDao;

        /// <summary>
        /// Liefert eine neue Instanz des Services
        /// </summary>
        /// <param name="routesDao"></param>
        public RouteService(IRoutesDao routesDao)
        {
            _routesDao = routesDao;
        }

        /// <summary>
        ///     Liefert alle Routen in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(Country country)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            return _routesDao.GetRoutesIn(country);
        }

        /// <summary>
        ///     Erstellt eine Route mit dem übergebenen Namen im Land
        /// </summary>
        /// <param name="country"></param>
        /// <param name="routeName"></param>
        public void CreateIn(Country country, string routeName)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            if (string.IsNullOrWhiteSpace(routeName)) throw new ArgumentNullException(nameof(routeName));
            _routesDao.CreateIn(country, new Route {Name = routeName});
        }
    }
}