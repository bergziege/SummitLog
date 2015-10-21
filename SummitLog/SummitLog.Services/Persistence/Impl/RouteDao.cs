using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Routen
    /// </summary>
    public class RouteDao :BaseDao, IRoutesDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des Daos
        /// </summary>
        /// <param name="graphClient"></param>
        public RouteDao(GraphClient graphClient): base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Routen direkt in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(Country country)
        {
            return
                GraphClient.Cypher.Match("(c:Country)-[:HAS]->(route:Route)")
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
                GraphClient.Cypher.Match("(a:Area)-[:HAS]->(route:Route)")
                    .Where((Area a) => a.Id == area.Id)
                    .Return(route => route.As<Route>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Route in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <param name="route"></param>
        public void CreateIn(Country country, Route route)
        {
            var query = GraphClient.Cypher
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
            var query = GraphClient.Cypher
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
            var query = GraphClient.Cypher
                .Match("(sg:SummitGroup)")
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                .Create("sg-[:HAS]->(route:Route {route})")
                .WithParam("route", route);

            query.ExecuteWithoutResults();
        }

        /// <summary>
        ///     Liefert alle Routen direkt in der Gipfelgruppe, nicht jedoch die der Einzelnen Gipfel
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(SummitGroup summitGroup)
        {
            return
                GraphClient.Cypher.Match("(sg:SummitGroup)-[:HAS]->(route:Route)")
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
            var query = GraphClient.Cypher
                .Match("(s:Summit)")
                .Where((Summit s) => s.Id == summit.Id)
                .Create("s-[:HAS]->(route:Route {route})")
                .WithParam("route", route);

            query.ExecuteWithoutResults();
        }

        /// <summary>
        ///     Liefert alle Routen eines Gipfels
        /// </summary>
        /// <param name="summit"></param>
        /// <returns></returns>
        public IList<Route> GetRoutesIn(Summit summit)
        {
            return
                GraphClient.Cypher.Match("(s:Summit)-[:HAS]->(route:Route)")
                    .Where((Summit s) => s.Id == summit.Id)
                    .Return(route => route.As<Route>())
                    .Results.ToList();
        }
    }
}