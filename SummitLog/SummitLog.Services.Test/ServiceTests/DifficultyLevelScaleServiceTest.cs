using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using FluentAssert;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
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
        [Test]
        public void TestGetAll()
        {
            Mock<IDifficultyLevelScaleDao> scaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            scaleDaoMock.Setup(x => x.GetAll()).Returns(new List<DifficultyLevelScale> {new DifficultyLevelScale{Name = "D"}});

            IDifficultyLevelScaleService scaleService = new DifficultyLevelScaleService(scaleDaoMock.Object);
            IList<DifficultyLevelScale> result = scaleService.GetAll();
            Assert.AreEqual(1, result.Count);
            scaleDaoMock.Verify(x=>x.GetAll(), Times.Once);
        }

        [Test]
        public void TestCreate()
        {
            Mock<IDifficultyLevelScaleDao> scaleDaoMock = new Mock<IDifficultyLevelScaleDao>();
            scaleDaoMock.Setup(x => x.Create(It.IsAny<DifficultyLevelScale>()));

            string scaleName = "D";

            IDifficultyLevelScaleService scaleService= new DifficultyLevelScaleService(scaleDaoMock.Object);
            scaleService.Create(scaleName);

            scaleDaoMock.Verify(x=>x.Create(It.Is<DifficultyLevelScale>(y=>y.Name == scaleName)));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void TestMissingName(string name)
        {
            Action act = ()=>new DifficultyLevelScaleService(null).Create(name);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}