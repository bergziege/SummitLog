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
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SummitLog.Services.Test.ServiceTests
{
    [TestFixture]
    public class DifficultyLevelScaleServiceTest
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void TestMissingName(string name)
        {
            Action act = () => new DifficultyLevelScaleService(null).Create(name);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestCreate()
        {
            var scaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            scaleDaoMock.Setup(x => x.Create(It.IsAny<DifficultyLevelScale>()));

            var scaleName = "D";

            IDifficultyLevelScaleService scaleService = new DifficultyLevelScaleService(scaleDaoMock.Object);
            scaleService.Create(scaleName);

            scaleDaoMock.Verify(x => x.Create(It.Is<DifficultyLevelScale>(y => y.Name == scaleName)));
        }

        [Test]
        public void TestGetAll()
        {
            var scaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            scaleDaoMock.Setup(x => x.GetAll())
                .Returns(new List<DifficultyLevelScale> {new DifficultyLevelScale {Name = "D"}});

            IDifficultyLevelScaleService scaleService = new DifficultyLevelScaleService(scaleDaoMock.Object);
            var result = scaleService.GetAll();
            Assert.AreEqual(1, result.Count);
            scaleDaoMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void TestIsInUse()
        {
            Mock<IDifficultyLevelScaleDao> difficultyLevelScaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            difficultyLevelScaleDaoMock.Setup(x => x.IsInUse(It.IsAny<DifficultyLevelScale>())).Returns(true);

            DifficultyLevelScale difficultyLevelScale = new DifficultyLevelScale();
            IDifficultyLevelScaleService difficultyLevelScaleService = new DifficultyLevelScaleService(difficultyLevelScaleDaoMock.Object);
            bool isInUse = difficultyLevelScaleService.IsInUse(difficultyLevelScale);

            Assert.IsTrue(isInUse);
            difficultyLevelScaleDaoMock.Verify(x=>x.IsInUse(difficultyLevelScale), Times.Once);
        }

        [TestCase(false)]
        [TestCase(true, ExpectedException = typeof(NodeInUseException))]
        public void TestDelete(bool isInUse)
        {
            Mock<IDifficultyLevelScaleDao> difficultyLevelScaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            difficultyLevelScaleDaoMock.Setup(x => x.IsInUse(It.IsAny<DifficultyLevelScale>())).Returns(isInUse);
            difficultyLevelScaleDaoMock.Setup(x => x.Delete(It.IsAny<DifficultyLevelScale>()));

            DifficultyLevelScale difficultyLevelScale = new DifficultyLevelScale();
            IDifficultyLevelScaleService difficultyLevelScaleService = new DifficultyLevelScaleService(difficultyLevelScaleDaoMock.Object);
            difficultyLevelScaleService.Delete(difficultyLevelScale);

            difficultyLevelScaleDaoMock.Verify(x=>x.IsInUse(difficultyLevelScale), Times.Once);
            difficultyLevelScaleDaoMock.Verify(x=>x.Delete(difficultyLevelScale), Times.Once);
        }

        [Test]
        public void TestSave()
        {
            Mock<IDifficultyLevelScaleDao> difficultyLevelScaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            difficultyLevelScaleDaoMock.Setup(x => x.Save(It.IsAny<DifficultyLevelScale>()));

            DifficultyLevelScale scaleToSave = new DifficultyLevelScale();

            IDifficultyLevelScaleService difficultyLevelScaleService = new DifficultyLevelScaleService(difficultyLevelScaleDaoMock.Object);
            difficultyLevelScaleService.Save(scaleToSave);

            difficultyLevelScaleDaoMock.Verify(x=>x.Save(scaleToSave), Times.Once);
        }

        [Test]
        public void TestGetForLevel()
        {
            DifficultyLevelScale scaleToReturn = new DifficultyLevelScale();
            Mock<IDifficultyLevelScaleDao> difficultyLevelScaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            difficultyLevelScaleDaoMock.Setup(x => x.GetForDifficultyLevel(It.IsAny<DifficultyLevel>()))
                .Returns(scaleToReturn);

            DifficultyLevel levelToGetScaleFor = new DifficultyLevel();

            DifficultyLevelScale scale = new DifficultyLevelScaleService(difficultyLevelScaleDaoMock.Object).GetForDifficultyLevel(levelToGetScaleFor);

            scale.Should().Be(scaleToReturn);
            difficultyLevelScaleDaoMock.Verify(x=>x.GetForDifficultyLevel(levelToGetScaleFor), Times.Once);
        }

        [Test]
        public void TestGetForLevelNull()
        {
            Action action = () => new DifficultyLevelScaleService(null).GetForDifficultyLevel(null);
            action.ShouldThrow<ArgumentNullException>();
        }
    }
}