﻿using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Extensions;

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
        public Route CreateIn(Country country, Route route)
        {
            var query = GraphClient.Cypher
                .Match("(c:Country)")
                .Where((Country c) => c.Id == country.Id)
                .Create("c-[:HAS]->(r:Route {route})")
                .WithParam("route", route);

            return query.Return(r=>r.As<Route>()).Results.First();
        }

        /// <summary>
        ///     Erstellt eine neue Route in einer Gegend
        /// </summary>
        /// <param name="area"></param>
        /// <param name="route"></param>
        public Route CreateIn(Area area, Route route)
        {
            var query = GraphClient.Cypher
                .Match("(a:Area)")
                .Where((Area a) => a.Id == area.Id)
                .Create("a-[:HAS]->(r:Route {route})")
                .WithParam("route", route);

            return query.Return(r=>r.As<Route>()).Results.First();
        }

        /// <summary>
        ///     Erstellt eine neue Route in einer Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <param name="route"></param>
        public Route CreateIn(SummitGroup summitGroup, Route route)
        {
            var query = GraphClient.Cypher
                .Match("(sg:SummitGroup)")
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                .Create("sg-[:HAS]->(r:Route {route})")
                .WithParam("route", route);

            return query.Return(r=>r.As<Route>()).Results.First();
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
        public Route CreateIn(Summit summit, Route route)
        {
            var query = GraphClient.Cypher
                .Match("(s:Summit)")
                .Where((Summit s) => s.Id == summit.Id)
                .Create("s-[:HAS]->(r:Route {route})")
                .WithParam("route", route);

            return query.Return(r=>r.As<Route>()).Results.First();
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

        /// <summary>
        ///     Liefert ob die Route verwendet wird
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public bool IsInUse(Route route)
        {
            return
                GraphClient.Cypher.Match("".Route("r").AnyOutboundRelationAs("usage").Variation())
                    .Where((Route r)=>r.Id == route.Id)
                    .Return(usage => usage.Count())
                    .Results.First() > 0;
        }

        /// <summary>
        ///     Löscht eine Route wenn diese nicht mehr verwendet wird
        /// </summary>
        /// <param name="route"></param>
        public void Delete(Route route)
        {
            if (IsInUse(route))
            {
                throw new NodeInUseException();
            }
            GraphClient.Cypher.Match("(n)-[parentAssignment:HAS]->(r:Route)").Where((Route r)=>r.Id == route.Id).Delete("parentAssignment, r").ExecuteWithoutResults();
        }
    }
}