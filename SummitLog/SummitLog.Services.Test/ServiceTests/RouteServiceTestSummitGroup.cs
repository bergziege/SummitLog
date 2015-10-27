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
    public class RouteServiceTestSummitGroup
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void TestCreateRouteInSummitGroupMissingName(string routeName)
        {
            Action act = () => new RouteService(null).CreateIn(new SummitGroup(), routeName);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestCreateRouteInSummitGroup()
        {
            SummitGroup fakeGroup = new SummitGroup();

            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.CreateIn(It.IsAny<SummitGroup>(), It.IsAny<Route>()));

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            routeService.CreateIn(fakeGroup, "Route");

            routeDaoMock.Verify(x => x.CreateIn(fakeGroup, It.Is<Route>(y => y.Name == "Route")));
        }

        [Test]
        public void TestCreateRouteInAreaMissingGroup()
        {
            Action act = () => new RouteService(null).CreateIn((SummitGroup) null, "route name");
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetRoutesInGroup()
        {
            SummitGroup fakeGroup = new SummitGroup();

            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.GetRoutesIn(It.IsAny<SummitGroup>())).Returns(new List<Route> {new Route()});

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            IList<Route> routesInCountry = routeService.GetRoutesIn(fakeGroup);

            Assert.AreEqual(routesInCountry.Count, 1);
            routeDaoMock.Verify(x => x.GetRoutesIn(fakeGroup), Times.Once);
        }

        [Test]
        public void TestGetRoutesInNullGroup()
        {
            Action act = () => new RouteService(null).GetRoutesIn((SummitGroup)null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}