using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Routen
    /// </summary>
    public class RouteDao : IRoutesDao
    {
        private readonly GraphClient _graphClient;

        /// <summary>
        ///     Erstellt eine neue Instanz des Daos
        /// </summary>
        /// <param name="graphClient"></param>
        public RouteDao(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        /// <summary>
        ///     Liefert alle Routen direkt in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(Country country)
        {
            return
                _graphClient.Cypher.Match("(c:Country)-[:HAS]->(route:Route)")
                    .Where((Country c) => c.Id == country.Id)
                    .Return(route => route.As<Route>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Liefert alle Routen direkt in einer Gegend
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(Area area)
        {
            return
                _graphClient.Cypher.Match("(a:Area)-[:HAS]->(route:Route)")
                    .Where((Area a) => a.Id == area.Id)
                    .Return(route => route.As<Route>())
                    .Results.ToList();
        }

        public void CreateIn(Country country, Route route)
        {
            var query = _graphClient.Cypher
                .Match("(c:Country)")
                .Where((Country c) => c.Id == country.Id)
                .Create("c-[:HAS]->(route:Route {route})")
                .WithParam("route", route);

            query.ExecuteWithoutResults();
        }

        /// <summary>
        ///     Erstellt eine neue Route in einer Gegend
        /// </summary>
        /// <param name="area"></param>
        /// <param name="route"></param>
        public void CreateIn(Area area, Route route)
        {
            var query = _graphClient.Cypher
                .Match("(a:Area)")
                .Where((Area a) => a.Id == area.Id)
                .Create("a-[:HAS]->(route:Route {route})")
                .WithParam("route", route);

            query.ExecuteWithoutResults();
        }

        /// <summary>
        ///     Erstellt eine neue Route in einer Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <param name="route"></param>
        public void CreateIn(SummitGroup summitGroup, Route route)
        {
            var query = _graphClient.Cypher
                .Match("(sg:SummitGroup)")
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                .Create("sg-[:HAS]->(route:Route {route})")
                .WithParam("route", route);

            query.ExecuteWithoutResults();
        }

        public IList<Route> GetRoutesIn(SummitGroup summitGroup)
        {
            return
                _graphClient.Cypher.Match("(sg:SummitGroup)-[:HAS]->(route:Route)")
                    .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                    .Return(route => route.As<Route>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Route an einem Gipfel
        /// </summary>
        /// <param name="summit"></param>
        /// <param name="route"></param>
        public void CreateIn(Summit summit, Route route)
        {
            var query = _graphClient.Cypher
                .Match("(s:Summit)")
                .Where((Summit s) => s.Id == summit.Id)
                .Create("s-[:HAS]->(route:Route {route})")
                .WithParam("route", route);

            query.ExecuteWithoutResults();
        }

        public IList<Route> GetRoutesIn(Summit summit)
        {
            return
                _graphClient.Cypher.Match("(s:Summit)-[:HAS]->(route:Route)")
                    .Where((Summit s) => s.Id == summit.Id)
                    .Return(route => route.As<Route>())
                    .Results.ToList();
        }
    }
}