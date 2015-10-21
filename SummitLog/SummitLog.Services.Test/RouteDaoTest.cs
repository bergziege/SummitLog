using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            routeDao.CreateIn(country, new Route {Name = "Jakobsweg"});

            IList<Route> routesInCountry = routeDao.GetRoutesIn(country);
            Assert.AreEqual(1, routesInCountry.Count);
            Assert.AreEqual("Jakobsweg", routesInCountry.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInArea()
        {
            var countryDao = new CountryDao(_graphClient);
            var country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IAreaDao areaDao = new AreaDao(_graphClient);
            var area = new Area();
            areaDao.Create(country, area);

            var routeDao = new RouteDao(_graphClient);
            routeDao.CreateIn(area, new Route {Name = "Jakobsweg"});

            var routesInArea = routeDao.GetRoutesIn(area);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInSummitGroup()
        {
            var countryDao = new CountryDao(_graphClient);
            var country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IAreaDao areaDao = new AreaDao(_graphClient);
            var area = new Area();
            areaDao.Create(country, area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            var summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            routeDao.CreateIn(summitGroup, new Route {Name = "Jakobsweg"});

            var routesInArea = routeDao.GetRoutesIn(summitGroup);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInSummit()
        {
            var countryDao = new CountryDao(_graphClient);
            var country = new Country { Name = "Deutschland" };
            countryDao.Create(country);

            IAreaDao areaDao = new AreaDao(_graphClient);
            var area = new Area();
            areaDao.Create(country, area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            var summitGroup = new SummitGroup { Name = "Gipfelgruppe" };
            summitGroupDao.Create(area, summitGroup);

            ISummitDao summitDao = new SummitDao(_graphClient);
            Summit summit = new Summit() { Name = "Gipfel" };
            summitDao.Create(summitGroup, summit);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            routeDao.CreateIn(summit, new Route { Name = "Jakobsweg" });

            var routesInArea = routeDao.GetRoutesIn(summit);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
        }

        [TestMethod]
        public void TestCreateRouteInCountry()
        {
            var countryDao = new CountryDao(_graphClient);
            var newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            var routeDao = new RouteDao(_graphClient);
            var newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(newCountry, newRoute);

            IList<Route> allRoutes = _graphClient.Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInArea()
        {
            var countryDao = new CountryDao(_graphClient);
            var newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IAreaDao areaDao = new AreaDao(_graphClient);
            var area = new Area();
            areaDao.Create(newCountry, area);

            var routeDao = new RouteDao(_graphClient);
            var newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(area, newRoute);

            IList<Route> allRoutes = _graphClient.Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInSummitGroup()
        {
            var countryDao = new CountryDao(_graphClient);
            var newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IAreaDao areaDao = new AreaDao(_graphClient);
            var area = new Area();
            areaDao.Create(newCountry, area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            var summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            var newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(summitGroup, newRoute);

            IList<Route> allRoutes = _graphClient.Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInSummit()
        {
            var countryDao = new CountryDao(_graphClient);
            var newCountry = new Country { Name = "Deutschland" };
            countryDao.Create(newCountry);

            IAreaDao areaDao = new AreaDao(_graphClient);
            var area = new Area();
            areaDao.Create(newCountry, area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            var summitGroup = new SummitGroup { Name = "Gipfelgruppe" };
            summitGroupDao.Create(area, summitGroup);

            ISummitDao summitDao = new SummitDao(_graphClient);
            Summit summit = new Summit() {Name = "Gipfel"};
            summitDao.Create(summitGroup, summit);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            var newRoute = new Route { Name = "Jakobsweg" };
            routeDao.CreateIn(summit, newRoute);

            IList<Route> allRoutes = _graphClient.Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }
    }
}