using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
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
            Route created = routeDao.CreateIn(country, new Route {Name = "Jakobsweg"});

            IList<Route> routesInCountry = routeDao.GetRoutesIn(country);
            Assert.AreEqual(1, routesInCountry.Count);
            Assert.AreEqual("Jakobsweg", routesInCountry.First().Name);
            Assert.AreEqual(created.Name, routesInCountry.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInArea()
        {
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IAreaDao areaDao = new AreaDao(_graphClient);
            Area area = new Area();
            areaDao.Create(country, area);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            Route created = routeDao.CreateIn(area, new Route {Name = "Jakobsweg"});

            IList<Route> routesInArea = routeDao.GetRoutesIn(area);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
            Assert.AreEqual(created.Name, routesInArea.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInSummitGroup()
        {
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IAreaDao areaDao = new AreaDao(_graphClient);
            Area area = new Area();
            areaDao.Create(country, area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            SummitGroup summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            Route created = routeDao.CreateIn(summitGroup, new Route {Name = "Jakobsweg"});

            IList<Route> routesInArea = routeDao.GetRoutesIn(summitGroup);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
            Assert.AreEqual(created.Name, routesInArea.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInSummit()
        {
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IAreaDao areaDao = new AreaDao(_graphClient);
            Area area = new Area();
            areaDao.Create(country, area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            SummitGroup summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            ISummitDao summitDao = new SummitDao(_graphClient);
            Summit summit = new Summit {Name = "Gipfel"};
            summitDao.Create(summitGroup, summit);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            Route created = routeDao.CreateIn(summit, new Route {Name = "Jakobsweg"});

            IList<Route> routesInArea = routeDao.GetRoutesIn(summit);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
            Assert.AreEqual(created.Name, routesInArea.First().Name);
        }

        [TestMethod]
        public void TestCreateRouteInCountry()
        {
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            Route newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(newCountry, newRoute);

            IList<Route> allRoutes = _graphClient.Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInArea()
        {
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IAreaDao areaDao = new AreaDao(_graphClient);
            Area area = new Area();
            areaDao.Create(newCountry, area);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            Route newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(area, newRoute);

            IList<Route> allRoutes = _graphClient.Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInSummitGroup()
        {
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IAreaDao areaDao = new AreaDao(_graphClient);
            Area area = new Area();
            areaDao.Create(newCountry, area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            SummitGroup summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            Route newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(summitGroup, newRoute);

            IList<Route> allRoutes = _graphClient.Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInSummit()
        {
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IAreaDao areaDao = new AreaDao(_graphClient);
            Area area = new Area();
            areaDao.Create(newCountry, area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            SummitGroup summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            ISummitDao summitDao = new SummitDao(_graphClient);
            Summit summit = new Summit {Name = "Gipfel"};
            summitDao.Create(summitGroup, summit);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            Route newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(summit, newRoute);

            IList<Route> allRoutes = _graphClient.Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }
    }
}