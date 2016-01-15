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
    public class SummitGroupServiceTest
    {
        [Test]
        public void TestGetAll()
        {
            Mock<ISummitGroupDao> summitGroupDaoMock = new Mock<ISummitGroupDao>();
            summitGroupDaoMock.Setup(x => x.GetAllIn(It.IsAny<Area>())).Returns(new List<SummitGroup> {new SummitGroup{Name = "Gruppe 1"}});

            Area fakeArea = new Area() {Name = "Gebiet 1"};

            ISummitGroupService summitGroupService = new SummitGroupService(summitGroupDaoMock.Object);
            IList<SummitGroup> groupsInArea = summitGroupService.GetAllIn(fakeArea);
            Assert.AreEqual(1, groupsInArea.Count);

            summitGroupDaoMock.Verify(x=>x.GetAllIn(It.Is<Area>(y=>y.Name == fakeArea.Name)));
        }

        [Test]
        public void TestCreate()
        {
            Mock<ISummitGroupDao> summitGroupDaoMock = new Mock<ISummitGroupDao>();
            summitGroupDaoMock.Setup(x => x.Create(It.IsAny<Area>(), It.IsAny<SummitGroup>()));

            string areaName = "Gebiet 1";
            string groupName = "Gruppe 1";
            Area fakeArea = new Area() { Name = areaName };

            ISummitGroupService summitGroupService = new SummitGroupService(summitGroupDaoMock.Object);
            summitGroupService.Create(fakeArea, groupName);

            summitGroupDaoMock.Verify(x=>x.Create(It.Is<Area>(y=>y.Name == areaName), It.Is<SummitGroup>(y=>y.Name == groupName)), Times.Once);
        }

        [TestCase(true,"")]
        [TestCase(true, " ")]
        [TestCase(true, "    ")]
        [TestCase(true, null)]
        [TestCase(false, "Gebiet 1")]
        public void TestCreateMissingName(bool useArea, string name)
        {
            Area fakeArea = null;
            if (useArea)
            {
                fakeArea = new Area();
            }

            Action act = ()=>new SummitGroupService(null).Create(fakeArea, name);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetAllWithoutCountry()
        {
            Action act = ()=> new SummitGroupService(null).GetAllIn(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestIsInUse()
        {
            Mock<ISummitGroupDao> summitGroupDaoMock = new Mock<ISummitGroupDao>();
            summitGroupDaoMock.Setup(x => x.IsInUse(It.IsAny<SummitGroup>())).Returns(true);

            SummitGroup summitGroup = new SummitGroup();

            ISummitGroupService summitGroupService = new SummitGroupService(summitGroupDaoMock.Object);
            bool isInUse = summitGroupService.IsInUse(summitGroup);

            Assert.IsTrue(isInUse);
            summitGroupDaoMock.Verify(x=>x.IsInUse(summitGroup), Times.Once);
        }

        [Test]
        public void TestDelete()
        {
            Mock<ISummitGroupDao> summitGroupDaoMock = new Mock<ISummitGroupDao>();
            summitGroupDaoMock.Setup(x => x.IsInUse(It.IsAny<SummitGroup>())).Returns(false);
            summitGroupDaoMock.Setup(x => x.Delete(It.IsAny<SummitGroup>()));

            SummitGroup summitGroup = new SummitGroup();

            ISummitGroupService summitGroupService = new SummitGroupService(summitGroupDaoMock.Object);
            summitGroupService.Delete(summitGroup);

            summitGroupDaoMock.Verify(x => x.IsInUse(summitGroup), Times.Once);
            summitGroupDaoMock.Verify(x => x.Delete(summitGroup), Times.Once);
        }

        [Test]
        public void TestDeleteWhileInUse()
        {
            Mock<ISummitGroupDao> summitGroupDaoMock = new Mock<ISummitGroupDao>();
            summitGroupDaoMock.Setup(x => x.IsInUse(It.IsAny<SummitGroup>())).Returns(true);
            summitGroupDaoMock.Setup(x => x.Delete(It.IsAny<SummitGroup>()));

            SummitGroup summitGroup = new SummitGroup();

            ISummitGroupService summitGroupService = new SummitGroupService(summitGroupDaoMock.Object);
            Action action = ()=>summitGroupService.Delete(summitGroup);

            action.ShouldThrow<NodeInUseException>();            
        }

        [Test]
        public void TestSave()
        {
            Mock<ISummitGroupDao> summitGroupDaoMock = new Mock<ISummitGroupDao>();
            summitGroupDaoMock.Setup(x => x.Save(It.IsAny<SummitGroup>()));

            SummitGroup summitGroup = new SummitGroup();

            ISummitGroupService summitGroupService = new SummitGroupService(summitGroupDaoMock.Object);
            summitGroupService.Save(summitGroup);

            summitGroupDaoMock.Verify(x=>x.Save(summitGroup), Times.Once);
        }

        [Test]
        public void TestSaveNull()
        {
            ISummitGroupService summitGroupService = new SummitGroupService(null);
            Assert.Throws<ArgumentNullException>(() => summitGroupService.Save(null));
        }
    }
}