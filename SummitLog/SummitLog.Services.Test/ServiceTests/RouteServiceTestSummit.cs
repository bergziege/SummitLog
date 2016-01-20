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
    public class RouteServiceTestSummit
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void TestCreateRouteInSummitMissingName(string routeName)
        {
            Action act = () => new RouteService(null).CreateIn(new Summit(), routeName);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestCreateRouteInSummit()
        {
            Summit fakeSummit = new Summit();

            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.CreateIn(It.IsAny<Summit>(), It.IsAny<Route>()));

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            routeService.CreateIn(fakeSummit, "Route", 3);

            routeDaoMock.Verify(x => x.CreateIn(fakeSummit, It.Is<Route>(y => y.Name == "Route" && Math.Abs(y.Rating - 3) < 0.0001)));
        }

        [Test]
        public void TestCreateRouteInMissingSummit()
        {
            Action act = () => new RouteService(null).CreateIn((Summit) null, "route name");
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetRoutesInSummit()
        {
            Summit fakeSummit = new Summit();

            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.GetRoutesIn(It.IsAny<Summit>())).Returns(new List<Route> {new Route()});

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            IList<Route> routesInCountry = routeService.GetRoutesIn(fakeSummit);

            Assert.AreEqual(routesInCountry.Count, 1);
            routeDaoMock.Verify(x => x.GetRoutesIn(fakeSummit), Times.Once);
        }

        [Test]
        public void TestGetRoutesInNullSummit()
        {
            Action act = () => new RouteService(null).GetRoutesIn((Summit)null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}