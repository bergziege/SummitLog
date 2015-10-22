using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Services;
using SummitLog.Services.Services.Impl;

namespace SummitLog.Services.Test.ServiceTests
{
    [TestFixture]
    public class RouteServiceTestCountry
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void TestCreateRouteInCountryMissingName(string routeName)
        {
            Action act = () => new RouteService(null).CreateIn(new Country(), routeName);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestCreateRouteInCountry()
        {
            Country fakeCountry = new Country();

            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.CreateIn(It.IsAny<Country>(), It.IsAny<Route>()));

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            routeService.CreateIn(fakeCountry, "RouteInCountry");

            routeDaoMock.Verify(x => x.CreateIn(fakeCountry, It.Is<Route>(y => y.Name == "RouteInCountry")));
        }

        [Test]
        public void TestCreateRouteInCountryMissingCountry()
        {
            Action act = () => new RouteService(null).CreateIn(null, "route name");
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetRoutesInCountry()
        {
            Country fakeCountry = new Country();

            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.GetRoutesIn(It.IsAny<Country>())).Returns(new List<Route> {new Route()});

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            IList<Route> routesInCountry = routeService.GetRoutesIn(fakeCountry);

            Assert.AreEqual(routesInCountry.Count, 1);
            routeDaoMock.Verify(x => x.GetRoutesIn(fakeCountry), Times.Once);
        }

        [Test]
        public void TestGetRoutesInNullCountry()
        {
            Action act = () => new RouteService(null).GetRoutesIn(null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}