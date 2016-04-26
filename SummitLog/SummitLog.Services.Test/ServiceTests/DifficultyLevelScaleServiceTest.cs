using System;
using System.Collections.Generic;
using FluentAssert;
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

        [Test]
        public void TestDelete()
        {
            Mock<IDifficultyLevelScaleDao> difficultyLevelScaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            difficultyLevelScaleDaoMock.Setup(x => x.IsInUse(It.IsAny<DifficultyLevelScale>())).Returns(false);
            difficultyLevelScaleDaoMock.Setup(x => x.Delete(It.IsAny<DifficultyLevelScale>()));

            DifficultyLevelScale difficultyLevelScale = new DifficultyLevelScale();
            IDifficultyLevelScaleService difficultyLevelScaleService = new DifficultyLevelScaleService(difficultyLevelScaleDaoMock.Object);
            difficultyLevelScaleService.Delete(difficultyLevelScale);

            difficultyLevelScaleDaoMock.Verify(x=>x.IsInUse(difficultyLevelScale), Times.Once);
            difficultyLevelScaleDaoMock.Verify(x=>x.Delete(difficultyLevelScale), Times.Once);
        }

        [Test]
        public void TestDeleteWhileInUse()
        {
            Mock<IDifficultyLevelScaleDao> difficultyLevelScaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            difficultyLevelScaleDaoMock.Setup(x => x.IsInUse(It.IsAny<DifficultyLevelScale>())).Returns(true);
            difficultyLevelScaleDaoMock.Setup(x => x.Delete(It.IsAny<DifficultyLevelScale>()));

            DifficultyLevelScale difficultyLevelScale = new DifficultyLevelScale();
            IDifficultyLevelScaleService difficultyLevelScaleService = new DifficultyLevelScaleService(difficultyLevelScaleDaoMock.Object);
            Action action = ()=> difficultyLevelScaleService.Delete(difficultyLevelScale);
            action.ShouldThrow<NodeInUseException>();
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

        [Test]
        public void TestSetAsDefault()
        {
            DifficultyLevelScale formerDefaultScale = new DifficultyLevelScale();
            formerDefaultScale.SetAsDefault();
            DifficultyLevelScale scaleToSetDefault = new DifficultyLevelScale();

            Mock<IDifficultyLevelScaleDao> difficultyLevelScaleDao = new Mock<IDifficultyLevelScaleDao>();
            difficultyLevelScaleDao.Setup(x => x.Save(scaleToSetDefault));
            difficultyLevelScaleDao.Setup(x => x.Save(formerDefaultScale));
            difficultyLevelScaleDao.Setup(x => x.GetDefaultScale()).Returns(formerDefaultScale);

            IDifficultyLevelScaleService service = new DifficultyLevelScaleService(difficultyLevelScaleDao.Object);
            service.SetAsDefault(scaleToSetDefault);

            formerDefaultScale.IsDefault.ShouldBeFalse();
            scaleToSetDefault.IsDefault.ShouldBeTrue();
            difficultyLevelScaleDao.Verify(x=>x.Save(formerDefaultScale), Times.Once);
            difficultyLevelScaleDao.Verify(x=>x.Save(scaleToSetDefault), Times.Once);
        }

        [Test]
        public void TestSetAsDefaultWithoutExisting()
        {
            
            DifficultyLevelScale scaleToSetDefault = new DifficultyLevelScale();

            Mock<IDifficultyLevelScaleDao> difficultyLevelScaleDao = new Mock<IDifficultyLevelScaleDao>();
            difficultyLevelScaleDao.Setup(x => x.Save(scaleToSetDefault));
            difficultyLevelScaleDao.Setup(x => x.GetDefaultScale()).Returns((DifficultyLevelScale) null);

            IDifficultyLevelScaleService service = new DifficultyLevelScaleService(difficultyLevelScaleDao.Object);
            service.SetAsDefault(scaleToSetDefault);

            scaleToSetDefault.IsDefault.ShouldBeTrue();
            difficultyLevelScaleDao.Verify(x=>x.Save(scaleToSetDefault), Times.Once);
            difficultyLevelScaleDao.Verify(x=>x.Save(null), Times.Never);
        }

        [Test]
        public void TestSetAsDefaultWithNull()
        {
            Action setNullAsDefault = ()=> new DifficultyLevelScaleService(null).SetAsDefault(null);
            setNullAsDefault.ShouldThrow<ArgumentNullException>();
        }
    }
}