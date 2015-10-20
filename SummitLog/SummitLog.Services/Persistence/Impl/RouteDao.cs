using System.Collections.Generic;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    public class RouteDao: IRoutesDao
    {
        private readonly GraphClient _graphClient;

        public RouteDao(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public IList<Route> GetRoutesIn(Country country)
        {
            throw new System.NotImplementedException();
        }

        public void CreateIn(Country country, Route route)
        {
            throw new System.NotImplementedException();
        }
    }
}