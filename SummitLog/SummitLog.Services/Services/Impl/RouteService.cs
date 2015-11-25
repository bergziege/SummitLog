using System;
using System.Collections.Generic;
using System.Linq;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Routen
    /// </summary>
    public class RouteService : IRouteService
    {
        private readonly IRoutesDao _routesDao;

        /// <summary>
        ///     Liefert eine neue Instanz des Services
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
            return _routesDao.GetRoutesIn(country).OrderBy(x=>x.Name).ToList();
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

        /// <summary>
        ///     Erstellt eine neue Route in einem Gebiet
        /// </summary>
        /// <param name="area"></param>
        /// <param name="routeName"></param>
        public void CreateIn(Area area, string routeName)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));
            if (string.IsNullOrWhiteSpace(routeName)) throw new ArgumentNullException(nameof(routeName));
            _routesDao.CreateIn(area, new Route {Name = routeName});
        }

        /// <summary>
        ///     LIefert alle Routen in einem Gebiet
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(Area area)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));
            return _routesDao.GetRoutesIn(area).OrderBy(x=>x.Name).ToList();
        }

        /// <summary>
        ///     Erstellt eine Route in einer Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <param name="routeName"></param>
        public void CreateIn(SummitGroup summitGroup, string routeName)
        {
            if (summitGroup == null) throw new ArgumentNullException(nameof(summitGroup));
            if (string.IsNullOrWhiteSpace(routeName)) throw new ArgumentNullException(nameof(routeName));
            _routesDao.CreateIn(summitGroup, new Route {Name = routeName});
        }

        /// <summary>
        ///     Liefert alle Routen einer Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(SummitGroup summitGroup)
        {
            if (summitGroup == null) throw new ArgumentNullException(nameof(summitGroup));
            return _routesDao.GetRoutesIn(summitGroup).OrderBy(x=>x.Name).ToList();
        }

        /// <summary>
        ///     Erstellt eine Route auf einem Gipfel
        /// </summary>
        /// <param name="summit"></param>
        /// <param name="routeName"></param>
        public void CreateIn(Summit summit, string routeName)
        {
            if (summit == null) throw new ArgumentNullException(nameof(summit));
            if (string.IsNullOrWhiteSpace(routeName)) throw new ArgumentNullException(nameof(routeName));
            _routesDao.CreateIn(summit, new Route {Name = routeName});
        }

        /// <summary>
        ///     Liefert alle Routen eines Gipfels
        /// </summary>
        /// <param name="summit"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(Summit summit)
        {
            if (summit == null) throw new ArgumentNullException(nameof(summit));
            return _routesDao.GetRoutesIn(summit).OrderBy(x=>x.Name).ToList();
        }

        /// <summary>
        ///     Liefert ob die Route in Verwendung ist
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public bool IsInUse(Route route)
        {
            return _routesDao.IsInUse(route);
        }

        /// <summary>
        ///     Löscht eine Route wenn idese nicht mehr in Verwendung ist
        /// </summary>
        /// <param name="route"></param>
        public void Delete(Route route)
        {
            if (_routesDao.IsInUse(route))
            {
                throw new NodeInUseException();
            }
            _routesDao.Delete(route);
        }
    }
}