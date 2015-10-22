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
    public class RouteServiceTestArea
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void TestCreateRouteInAreaMissingName(string routeName)
        {
            Action act = () => new RouteService(null).CreateIn(new Area(), routeName);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestCreateRouteInArea()
        {
            Area fakeArea = new Area();

            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.CreateIn(It.IsAny<Area>(), It.IsAny<Route>()));

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            routeService.CreateIn(fakeArea, "RouteInCountry");

            routeDaoMock.Verify(x => x.CreateIn(fakeArea, It.Is<Route>(y => y.Name == "RouteInCountry")));
        }

        [Test]
        public void TestCreateRouteInAreaMissingArea()
        {
            Action act = () => new RouteService(null).CreateIn((Area) null, "route name");
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetRoutesInArea()
        {
            Area fakeArea = new Area();

            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.GetRoutesIn(It.IsAny<Area>())).Returns(new List<Route> {new Route()});

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            IList<Route> routesInCountry = routeService.GetRoutesIn(fakeArea);

            Assert.AreEqual(routesInCountry.Count, 1);
            routeDaoMock.Verify(x => x.GetRoutesIn(fakeArea), Times.Once);
        }

        [Test]
        public void TestGetRoutesInNullArea()
        {
            Action act = () => new RouteService(null).GetRoutesIn((Area)null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}