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
    public class SummitServiceTest
    {
        [Test]
        public void TestGetAll()
        {
            Mock<ISummitDao> summitDaoMock = new Mock<ISummitDao>();
            summitDaoMock.Setup(x => x.GetAllIn(It.IsAny<SummitGroup>())).Returns(new List<Summit> {new Summit{Name = "Gipfel 1"}});

            SummitGroup fakeGroup = new SummitGroup {Name = "Gruppe 1"};

            ISummitService summitService = new SummitService(summitDaoMock.Object);
            IList<Summit> summitsInGroup = summitService.GetAllIn(fakeGroup);
            Assert.AreEqual(1, summitsInGroup.Count);

            summitDaoMock.Verify(x=>x.GetAllIn(It.Is<SummitGroup>(y=>y.Name == fakeGroup.Name)));
        }

        [Test]
        public void TestCreate()
        {
            Mock<ISummitDao> summitDaoMock = new Mock<ISummitDao>();
            summitDaoMock.Setup(x => x.Create(It.IsAny<SummitGroup>(), It.IsAny<Summit>()));

            string groupName = "Gruppe 1";
            string summitName = "Gipfel 1";
            SummitGroup fakeGroup = new SummitGroup { Name = groupName };

            ISummitService summitService = new SummitService(summitDaoMock.Object);
            summitService.Create(fakeGroup, summitName);

            summitDaoMock.Verify(x=>x.Create(It.Is<SummitGroup>(y=>y.Name == groupName), It.Is<Summit>(y=>y.Name == summitName)), Times.Once);
        }

        [TestCase(true,"")]
        [TestCase(true, " ")]
        [TestCase(true, "    ")]
        [TestCase(true, null)]
        [TestCase(false, "Gebiet 1")]
        public void TestCreateMissingName(bool useGroup, string name)
        {
            SummitGroup fakeGroup = null;
            if (useGroup)
            {
                fakeGroup = new SummitGroup();
            }

            Action act = ()=>new SummitService(null).Create(fakeGroup, name);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetAllWithoutCountry()
        {
            Action act = ()=> new SummitService(null).GetAllIn(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestIsInUse()
        {
            Mock<ISummitDao> summitDaoMock = new Mock<ISummitDao>();
            summitDaoMock.Setup(x => x.IsInUse(It.IsAny<Summit>())).Returns(true);

            Summit summit = new Summit();

            ISummitService summitService = new SummitService(summitDaoMock.Object);
            bool isInUse = summitService.IsInUse(summit);

            Assert.IsTrue(isInUse);
            summitDaoMock.Verify(x=>x.IsInUse(summit), Times.Once);
        }

        [TestCase(false)]
        [TestCase(true, ExpectedException = typeof(NodeInUseException))]
        public void TestDelete(bool isInUse)
        {
            Mock<ISummitDao> summitDaoMock = new Mock<ISummitDao>();
            summitDaoMock.Setup(x => x.IsInUse(It.IsAny<Summit>())).Returns(isInUse);
            summitDaoMock.Setup(x => x.Delete(It.IsAny<Summit>()));

            Summit summit = new Summit();

            ISummitService summitService = new SummitService(summitDaoMock.Object);
            summitService.Delete(summit);

            summitDaoMock.Verify(x => x.IsInUse(summit), Times.Once);
            summitDaoMock.Verify(x => x.Delete(summit), Times.Once);
        }
    }
}