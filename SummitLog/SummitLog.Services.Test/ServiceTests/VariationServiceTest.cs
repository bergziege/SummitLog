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
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("   ")]
        public void TestCreateMissingNameShouldSucced(string name)
        {
            Route route = new Route();
            
            DifficultyLevel level = new DifficultyLevel();

            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();

            new VariationService(variationDaoMock.Object).Create(route, level, name);            
        }

        [Test]
        public void TestCreateMissingRouteShouldThrow()
        {
            Route route = null;
            
            DifficultyLevel level = new DifficultyLevel();
            
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();

            Action action = ()=> new VariationService(variationDaoMock.Object).Create(route, level, "name");
            action.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestCreateMissingLevelShouldThrow()
        {
            Route route  = new Route();
            
            DifficultyLevel level = null;

            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();

            Action action = ()=> new VariationService(variationDaoMock.Object).Create(route, level, "name");
            action.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestDelete()
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.IsInUse(It.IsAny<Variation>())).Returns(false);
            variationDaoMock.Setup(x => x.Delete(It.IsAny<Variation>()));

            Variation variation = new Variation();

            IVariationService service = new VariationService(variationDaoMock.Object);
            service.Delete(variation);

            variationDaoMock.Verify(x => x.IsInUse(variation), Times.Once);
            variationDaoMock.Verify(x => x.Delete(variation), Times.Once);
        }

        [Test]
        public void TestDeleteWhileInUse()
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.IsInUse(It.IsAny<Variation>())).Returns(true);
            variationDaoMock.Setup(x => x.Delete(It.IsAny<Variation>()));

            Variation variation = new Variation();

            IVariationService service = new VariationService(variationDaoMock.Object);
            Action action = ()=> service.Delete(variation);

            action.ShouldThrow<NodeInUseException>();
        }

        [TestCase(true, false)]
        [TestCase(false, true)]
        public void TestChangeDifficultyLevelWithNull(bool variationExists, bool levelExists)
        {
            Variation variation = null;
            DifficultyLevel level = null;

            if (variationExists)
            {
                variation = new Variation();
            }

            if (levelExists)
            {
                level = new DifficultyLevel();
            }

            Action action = () => new VariationService(null).ChangeDifficultyLevel(variation, level);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestChangeDifficultyLevel()
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.ChangeDifficultyLevel(It.IsAny<Variation>(), It.IsAny<DifficultyLevel>()));

            Variation variation = new Variation();
            DifficultyLevel newLevel = new DifficultyLevel();

            IVariationService variationService = new VariationService(variationDaoMock.Object);
            variationService.ChangeDifficultyLevel(variation, newLevel);

            variationDaoMock.Verify(x => x.ChangeDifficultyLevel(variation, newLevel), Times.Once);
        }

        [Test]
        public void TestCreate()
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.Create(It.IsAny<Variation>(), It.IsAny<Route>(), It.IsAny<DifficultyLevel>()));

            Route fakeRoute = new Route();
            DifficultyLevel fakeLevel = new DifficultyLevel();

            IVariationService variationService = new VariationService(variationDaoMock.Object);
            variationService.Create(fakeRoute, fakeLevel, "Variation 1");

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

        [Test]
        public void TestSave()
        {
            Mock<IVariationDao> variationDaoMock = new Mock<IVariationDao>();
            variationDaoMock.Setup(x => x.Save(It.IsAny<Variation>()));

            Variation variation = new Variation();

            IVariationService variationService = new VariationService(variationDaoMock.Object);
            variationService.Save(variation);

            variationDaoMock.Verify(x => x.Save(variation), Times.Once);
        }

        [Test]
        public void TestSaveNull()
        {
            Action action = () => new VariationService(null).Save(null);
            action.ShouldThrow<ArgumentNullException>();
        }
    }
}