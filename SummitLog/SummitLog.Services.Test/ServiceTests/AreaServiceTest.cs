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
    public class AreaServiceTest
    {
        [Test]
        public void TestGetAll()
        {
            AreaService areaService = new AreaService();

            Mock<IAreaDao> areaDaoMock = new Mock<IAreaDao>();
            areaDaoMock.Setup(x => x.GetAllIn(It.IsAny<Country>())).Returns(new List<Area> {new Area {Name = "Gebiet 1"}});

            Country fakeCountry = new Country() {Name = "D"};

            areaService.AreaDao = areaDaoMock.Object;
            IList<Area> areasInCountry = areaService.GetAllIn(fakeCountry);
            Assert.AreEqual(1, areasInCountry.Count);

            areaDaoMock.Verify(x=>x.GetAllIn(It.Is<Country>(y=>y.Name == fakeCountry.Name)));
        }

        [Test]
        public void TestCreate()
        {
            AreaService areaService = new AreaService();

            Mock<IAreaDao> areaDaoMock = new Mock<IAreaDao>();
            areaDaoMock.Setup(x => x.Create(It.IsAny<Country>(), It.IsAny<Area>()));

            string countryName = "D";
            string areaName = "Gebiet 1";
            Country fakeCountry = new Country() { Name = countryName };

            areaService.AreaDao = areaDaoMock.Object;
            areaService.Create(fakeCountry, areaName);

            areaDaoMock.Verify(x=>x.Create(It.Is<Country>(y=>y.Name == countryName), It.Is<Area>(y=>y.Name == areaName)), Times.Once);
        }

        [TestCase(true,"")]
        [TestCase(true, " ")]
        [TestCase(true, "    ")]
        [TestCase(true, null)]
        [TestCase(false, "Gebiet 1")]
        public void TestCreateMissingName(bool useCountry, string name)
        {
            Country fakeCountry = null;
            if (useCountry)
            {
                fakeCountry = new Country();
            }

            Action act = ()=>new AreaService().Create(fakeCountry, name);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetAllWithoutCountry()
        {
            Action act = ()=> new AreaService().GetAllIn(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestIsInUse()
        {
            AreaService areaService = new AreaService();

            Mock<IAreaDao> areaDaoMock = new Mock<IAreaDao>();
            areaDaoMock.Setup(x => x.IsInUse(It.IsAny<Area>())).Returns(true);

            Area area = new Area();
            
            areaService.AreaDao = areaDaoMock.Object;
            bool isInUse = areaService.IsInUse(area);

            Assert.IsTrue(isInUse);
            areaDaoMock.Verify(x=>x.IsInUse(area), Times.Once);
        }

        [Test]
        public void TestDelete()
        {
            AreaService areaService = new AreaService();

            Mock<IAreaDao> areaDaoMock = new Mock<IAreaDao>();
            areaDaoMock.Setup(x => x.IsInUse(It.IsAny<Area>())).Returns(false);
            areaDaoMock.Setup(x => x.Delete(It.IsAny<Area>()));

            Area area = new Area();
            
            areaService.AreaDao = areaDaoMock.Object;
            areaService.Delete(area);

            areaDaoMock.Verify(x => x.IsInUse(area), Times.Once);
            areaDaoMock.Verify(x => x.Delete(area), Times.Once);
        }

        [Test]
        public void TestDeleteWhenInUse()
        {
            AreaService areaService = new AreaService();

            Mock<IAreaDao> areaDaoMock = new Mock<IAreaDao>();
            areaDaoMock.Setup(x => x.IsInUse(It.IsAny<Area>())).Returns(true);
            areaDaoMock.Setup(x => x.Delete(It.IsAny<Area>()));

            Area area = new Area();

            areaService.AreaDao = areaDaoMock.Object;
            Action deleteWithAreaInUse = ()=> areaService.Delete(area);

            deleteWithAreaInUse.ShouldThrow<NodeInUseException>();
        }

        [Test]
        public void TestSave()
        {
            Mock<IAreaDao> areaDaoMock = new Mock<IAreaDao>();
            areaDaoMock.Setup(x => x.Save(It.IsAny<Area>()));

            Area area = new Area();

            AreaService areaService = new AreaService();
            areaService.AreaDao = areaDaoMock.Object;
            areaService.Save(area);

            areaDaoMock.Verify(x=>x.Save(area), Times.Once);
        }

        [Test]
        public void TestSaveNull()
        {
            IAreaService areaService = new AreaService();
            Assert.Throws<ArgumentNullException>(() => areaService.Save(null));
        }
    }
}