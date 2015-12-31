using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Services;
using SummitLog.Services.Services.Impl;

namespace SummitLog.Services.Test.ServiceTests
{
    [TestFixture]
    public class VariationServiceTest
    {
        [TestCase(null, true, true)]
        [TestCase("", true, true)]
        [TestCase(" ", true, true)]
        [TestCase("   ", true, true)]
        [TestCase("var", false, true)]
        [TestCase("var", true, false)]
        public void TestCreateMissingName(string name, bool useRoute, bool useLevel)
        {
            Route route = null;
            if (useRoute)
            {
                route = new Route();
            }
            DifficultyLevel level = null;
            if (useLevel)
            {
                level = new DifficultyLevel();
            }

            Action act = () => new VariationService(null).Create(name, route, level);
            act.ShouldThrow<ArgumentNullException>();
        }

        [TestCase(false)]
        [TestCase(true, ExpectedException = typeof (NodeInUseException))]
        public void TestDelete(bool isInUse)
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.IsInUse(It.IsAny<Variation>())).Returns(isInUse);
            variationDaoMock.Setup(x => x.Delete(It.IsAny<Variation>()));

            Variation variation = new Variation();

            IVariationService service = new VariationService(variationDaoMock.Object);
            service.Delete(variation);

            variationDaoMock.Verify(x => x.IsInUse(variation), Times.Once);
            variationDaoMock.Verify(x => x.Delete(variation), Times.Once);
        }

        [Test]
        public void TestCreate()
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.Create(It.IsAny<Variation>(), It.IsAny<Route>(), It.IsAny<DifficultyLevel>()));

            Route fakeRoute = new Route();
            DifficultyLevel fakeLevel = new DifficultyLevel();

            IVariationService variationService = new VariationService(variationDaoMock.Object);
            variationService.Create("Variation 1", fakeRoute, fakeLevel);

            variationDaoMock.Verify(
                x => x.Create(It.Is<Variation>(y => y.Name == "Variation 1"), fakeRoute, fakeLevel), Times.Once);
        }

        [Test]
        public void TestGetAllOnRoute()
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.GetAllOn(It.IsAny<Route>()))
                .Returns(new List<Variation> {new Variation {Name = "Gebiet 1"}});

            Route fakeRoute = new Route {Name = "D"};

            IVariationService variationService = new VariationService(variationDaoMock.Object);
            IList<Variation> variationsOnRoute = variationService.GetAllOn(fakeRoute);
            Assert.AreEqual(1, variationsOnRoute.Count);

            variationDaoMock.Verify(x => x.GetAllOn(fakeRoute), Times.Once);
        }

        [Test]
        public void TestGetAllWithoutRoute()
        {
            Action act = () => new VariationService(null).GetAllOn(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestIsInUse()
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.IsInUse(It.IsAny<Variation>())).Returns(true);

            Variation variation = new Variation();

            IVariationService service = new VariationService(variationDaoMock.Object);
            bool isInUse = service.IsInUse(variation);

            Assert.IsTrue(isInUse);
            variationDaoMock.Verify(x => x.IsInUse(variation), Times.Once);
        }
    }
}