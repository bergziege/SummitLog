using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Services;
using SummitLog.Services.Services.Impl;
using Assert = NUnit.Framework.Assert;

namespace SummitLog.Services.Test.ServiceTests
{
    [TestFixture]
    public class RouteServiceTest
    {
        [Test]
        public void TestIsInUse()
        {
            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.IsInUse(It.IsAny<Route>())).Returns(true);

            Route route = new Route();

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            bool isInUse = routeService.IsInUse(route);
            Assert.IsTrue(isInUse);
            routeDaoMock.Verify(x=>x.IsInUse(route), Times.Once);
        }

        [Test]
        public void TestDelete()
        {
            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.IsInUse(It.IsAny<Route>())).Returns(false);
            routeDaoMock.Setup(x => x.Delete(It.IsAny<Route>()));

            Route route = new Route();

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            routeService.Delete(route);

            routeDaoMock.Verify(x=>x.Delete(route), Times.Once);
            routeDaoMock.Verify(x=>x.IsInUse(route), Times.Once);
        }

        [Test]
        public void TestDeleteWhileInUse()
        {
            Mock<IRoutesDao> routeDaoMock = new Mock<IRoutesDao>();
            routeDaoMock.Setup(x => x.IsInUse(It.IsAny<Route>())).Returns(true);
            routeDaoMock.Setup(x => x.Delete(It.IsAny<Route>()));

            Route route = new Route();

            IRouteService routeService = new RouteService(routeDaoMock.Object);
            Action action = ()=> routeService.Delete(route);

            action.ShouldThrow<NodeInUseException>();
        }

        [Test]
        public void TestSave()
        {
            Mock<IRoutesDao> routesDaoMock = new Mock<IRoutesDao>();
            routesDaoMock.Setup(x => x.Save(It.IsAny<Route>()));

            Route route = new Route();

            IRouteService routeService = new RouteService(routesDaoMock.Object);
            routeService.Save(route);

            routesDaoMock.Verify(x=>x.Save(route), Times.Once);
        }
    }
}
