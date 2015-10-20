using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test
{
    [TestClass]
    public class RouteDaoTest
    {
        private GraphClient _graphClient;

        [TestInitialize]
        public void Init()
        {
            _graphClient = new GraphClient(new Uri("http://localhost:7475/db/data"), "neo4j", "extra");
            _graphClient.Connect();
            _graphClient.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _graphClient.Transaction.Rollback();
        }

        [TestMethod]
        public void TestGetRoutesInCountry()
        {
            
            CountryDao countryDao = new CountryDao(_graphClient);
            countryDao.Create(new Country() {Name = "Deutschland"});
            Country country = countryDao.GetAll().First();

            RouteDao routeDao = new RouteDao(_graphClient);
            routeDao.CreateIn(country, new Route() {Name = "Jakobsweg"});

            IList<Route> routesInCountry = routeDao.GetRoutesIn(country);
            Assert.AreEqual(1, routesInCountry.Count);
            Assert.AreEqual("Jakobsweg", routesInCountry.First().Name);
        }
    }
}
