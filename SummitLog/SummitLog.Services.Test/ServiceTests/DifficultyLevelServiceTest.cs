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
    public class DifficultyLevelServiceTest
    {
        [Test]
        public void TestGetAll()
        {
            Mock<IDifficultyLevelDao> levelDaoMock = new Mock<IDifficultyLevelDao>();
            levelDaoMock.Setup(x => x.GetAllIn(It.IsAny<DifficultyLevelScale>())).Returns(new List<DifficultyLevel> {new DifficultyLevel() {Name = "Level 1"}});

            DifficultyLevelScale fakeScale = new DifficultyLevelScale {Name = "D"};

            IDifficultyLevelService levelService = new DifficultyLevelService(levelDaoMock.Object);
            IList<DifficultyLevel> levelInScale = levelService.GetAllIn(fakeScale);
            Assert.AreEqual(1, levelInScale.Count);

            levelDaoMock.Verify(x=>x.GetAllIn(It.Is<DifficultyLevelScale>(y=>y.Name == fakeScale.Name)));
        }

        [Test]
        public void TestCreate()
        {
            Mock<IDifficultyLevelDao> levelDaoMock = new Mock<IDifficultyLevelDao>();
            levelDaoMock.Setup(x => x.Create(It.IsAny<DifficultyLevelScale>(), It.IsAny<DifficultyLevel>()));

            string scaleName = "D";
            string levelName = "Gebiet 1";
            DifficultyLevelScale fakeScale = new DifficultyLevelScale(){ Name = scaleName };

            IDifficultyLevelService levelService = new DifficultyLevelService(levelDaoMock.Object);
            levelService.Create(fakeScale, levelName, 1000);

            levelDaoMock.Verify(x=>x.Create(It.Is<DifficultyLevelScale>(y=>y.Name == scaleName), It.Is<DifficultyLevel>(y=>y.Name == levelName && y.Score == 1000)), Times.Once);
        }

        [TestCase(true,"")]
        [TestCase(true, " ")]
        [TestCase(true, "    ")]
        [TestCase(true, null)]
        [TestCase(false, "Gebiet 1")]
        public void TestCreateMissingName(bool useScale, string name)
        {
            DifficultyLevelScale fakeScale = null;
            if (useScale)
            {
                fakeScale = new DifficultyLevelScale();
            }

            Action act = ()=>new DifficultyLevelService(null).Create(fakeScale, name, 1000);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetAllWithoutCountry()
        {
            Action act = ()=> new DifficultyLevelService(null).GetAllIn(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestIsInUseNormal()
        {
            Mock<IDifficultyLevelDao> difficultyLevelDaoMock = new Mock<IDifficultyLevelDao>();
            difficultyLevelDaoMock.Setup(x => x.IsInUse(It.IsAny<DifficultyLevel>())).Returns(true);

            DifficultyLevel difficultyLevel = new DifficultyLevel();
            IDifficultyLevelService difficultyLevelService = new DifficultyLevelService(difficultyLevelDaoMock.Object);
            bool isInUse = difficultyLevelService.IsInUse(difficultyLevel);

            difficultyLevelDaoMock.Verify(x=>x.IsInUse(difficultyLevel), Times.Once);
            Assert.IsTrue(isInUse);
        }

        [TestCase(true, ExpectedException = typeof(NodeInUseException))]
        [TestCase(false)]
        public void TestDelete(bool isInUse)
        {
            Mock<IDifficultyLevelDao> difficultyLevelDaoMock = new Mock<IDifficultyLevelDao>();
            difficultyLevelDaoMock.Setup(x => x.IsInUse(It.IsAny<DifficultyLevel>())).Returns(isInUse);
            difficultyLevelDaoMock.Setup(x => x.Delete(It.IsAny<DifficultyLevel>()));

            DifficultyLevel difficultyLevel = new DifficultyLevel();
            IDifficultyLevelService difficultyLevelService = new DifficultyLevelService(difficultyLevelDaoMock.Object);
            difficultyLevelService.Delete(difficultyLevel);

            difficultyLevelDaoMock.Verify(x=>x.IsInUse(difficultyLevel), Times.Once);
            difficultyLevelDaoMock.Verify(x=>x.Delete(difficultyLevel), Times.Once);
        }

        [Test]
        public void TestSave()
        {
            Mock<IDifficultyLevelDao> difficultyLevelDaoMock = new Mock<IDifficultyLevelDao>();
            difficultyLevelDaoMock.Setup(x => x.Save(It.IsAny<DifficultyLevel>()));

            DifficultyLevel level = new DifficultyLevel();
            new DifficultyLevelService(difficultyLevelDaoMock.Object).Save(level);

            difficultyLevelDaoMock.Verify(x=>x.Save(level), Times.Once);
        }

        [Test]
        public void TestSaveNull()
        {
            Action action = ()=>new DifficultyLevelService(null).Save(null);
            action.ShouldThrow<ArgumentNullException>();
        }
    }
}