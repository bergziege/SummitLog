using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    [TestClass]
    public class RouteDaoTest: DbTestBase
    {


        [TestMethod]
        public void TestGetRoutesInCountry()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IRoutesDao routeDao = Container.Resolve<RouteDao>();
            Route created = routeDao.CreateIn(country, new Route {Name = "Jakobsweg"});

            IList<Route> routesInCountry = routeDao.GetRoutesIn(country);
            Assert.AreEqual(1, routesInCountry.Count);
            Assert.AreEqual("Jakobsweg", routesInCountry.First().Name);
            Assert.AreEqual(created.Name, routesInCountry.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInArea()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            Area area = new Area();
            areaDao.Create(country, area);

            IRoutesDao routeDao = Container.Resolve<RouteDao>();
            Route created = routeDao.CreateIn(area, new Route {Name = "Jakobsweg"});

            IList<Route> routesInArea = routeDao.GetRoutesIn(area);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
            Assert.AreEqual(created.Name, routesInArea.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInSummitGroup()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            Area area = new Area();
            areaDao.Create(country, area);

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            SummitGroup summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            IRoutesDao routeDao = Container.Resolve<RouteDao>();
            Route created = routeDao.CreateIn(summitGroup, new Route {Name = "Jakobsweg"});

            IList<Route> routesInArea = routeDao.GetRoutesIn(summitGroup);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
            Assert.AreEqual(created.Name, routesInArea.First().Name);
        }

        [TestMethod]
        public void TestGetRoutesInSummit()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country country = new Country {Name = "Deutschland"};
            countryDao.Create(country);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            Area area = new Area();
            areaDao.Create(country, area);

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            SummitGroup summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            ISummitDao summitDao = Container.Resolve<SummitDao>();
            Summit summit = new Summit {Name = "Gipfel"};
            summitDao.Create(summitGroup, summit);

            IRoutesDao routeDao = Container.Resolve<RouteDao>();
            Route created = routeDao.CreateIn(summit, new Route {Name = "Jakobsweg"});

            IList<Route> routesInArea = routeDao.GetRoutesIn(summit);
            Assert.AreEqual(1, routesInArea.Count);
            Assert.AreEqual("Jakobsweg", routesInArea.First().Name);
            Assert.AreEqual(created.Name, routesInArea.First().Name);
        }

        [TestMethod]
        public void TestCreateRouteInCountry()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IRoutesDao routeDao = Container.Resolve<RouteDao>();
            Route newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(newCountry, newRoute);

            IList<Route> allRoutes = Container.Resolve<GraphClient>().Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInArea()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            Area area = new Area();
            areaDao.Create(newCountry, area);

            IRoutesDao routeDao = Container.Resolve<RouteDao>();
            Route newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(area, newRoute);

            IList<Route> allRoutes = Container.Resolve<GraphClient>().Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInSummitGroup()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            Area area = new Area();
            areaDao.Create(newCountry, area);

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            SummitGroup summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            IRoutesDao routeDao = Container.Resolve<RouteDao>();
            Route newRoute = new Route {Name = "Jakobsweg"};
            routeDao.CreateIn(summitGroup, newRoute);

            IList<Route> allRoutes = Container.Resolve<GraphClient>().Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
        }

        [TestMethod]
        public void TestCreateRouteInSummit()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country newCountry = new Country {Name = "Deutschland"};
            countryDao.Create(newCountry);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            Area area = new Area();
            areaDao.Create(newCountry, area);

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            SummitGroup summitGroup = new SummitGroup {Name = "Gipfelgruppe"};
            summitGroupDao.Create(area, summitGroup);

            ISummitDao summitDao = Container.Resolve<SummitDao>();
            Summit summit = new Summit {Name = "Gipfel"};
            summitDao.Create(summitGroup, summit);

            IRoutesDao routeDao = Container.Resolve<RouteDao>();
            Route newRoute = new Route {Name = "Jakobsweg", Rating = 4.0};
            routeDao.CreateIn(summit, newRoute);

            IList<Route> allRoutes = Container.Resolve<GraphClient>().Cypher.Match("(route:Route)")
                .Return(route => route.As<Route>())
                .Results.ToList();
            Assert.AreEqual(1, allRoutes.Count);
            allRoutes.First().Rating.Should().Be(4);
        }

        [TestMethod]
        public void TestRouteIsInUse()
        {
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInArea();
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation(route: route);

            IRoutesDao routesDao = Container.Resolve<RouteDao>();
            bool isInUse = routesDao.IsInUse(route);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestRouteIsNotInUse()
        {
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInArea();

            IRoutesDao routesDao = Container.Resolve<RouteDao>();
            bool isInUse = routesDao.IsInUse(route);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteRouteNotInUse()
        {
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInArea(area:area);

            IRoutesDao routesDao = Container.Resolve<RouteDao>();
            routesDao.Delete(route);

            Assert.AreEqual(0, routesDao.GetRoutesIn(area).Count);
        }

        [TestMethod]
        public void TestDeleteRouteInUse()
        {
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInArea();
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation(route: route);

            IRoutesDao routesDao = Container.Resolve<RouteDao>();
            Action action = ()=> routesDao.Delete(route);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestSave()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInCountry("oldname", 3.0, country);

            IRoutesDao routesDao = Container.Resolve<RouteDao>();
            Assert.AreEqual(1, routesDao.GetRoutesIn(country).Count);

            route.Name = "newname";
            route.Rating = 2;
            routesDao.Save(route);

            Route reloaded = routesDao.GetRoutesIn(country).First();
            reloaded.Name.Should().Be("newname");
            reloaded.Rating.Should().Be(2);
        }
    }
}