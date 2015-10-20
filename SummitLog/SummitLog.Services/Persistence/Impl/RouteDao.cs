using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    public class RouteDao : IRoutesDao
    {
        private readonly GraphClient _graphClient;

        public RouteDao(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public IList<Route> GetRoutesIn(Country country)
        {
            return
                _graphClient.Cypher.Match("(c:Country)-[:HAS]->(route:Route)")
                    .Where((Country c) => c.Id == country.Id)
                    .Return(route => route.As<Route>())
                    .Results.ToList();
        }

        public void CreateIn(Country country, Route route)
        {
            ICypherFluentQuery query =_graphClient.Cypher
                .Match("(c:Country)")
                .Where((Country c) => c.Id == country.Id)
                .Create("c-[:HAS]->(route:Route {route})")
                .WithParam("route", route);

            query.ExecuteWithoutResults();
        }
    }
}