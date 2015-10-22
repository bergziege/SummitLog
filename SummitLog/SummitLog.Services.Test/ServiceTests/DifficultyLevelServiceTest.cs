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
    }
}