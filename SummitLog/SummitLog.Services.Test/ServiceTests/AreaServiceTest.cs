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
    public class AreaServiceTest
    {
        [Test]
        public void TestGetAll()
        {
            Mock<IAreaDao> areaDaoMock = new Mock<IAreaDao>();
            areaDaoMock.Setup(x => x.GetAllIn(It.IsAny<Country>())).Returns(new List<Area> {new Area {Name = "Gebiet 1"}});

            Country fakeCountry = new Country() {Name = "D"};

            IAreaService areaService = new AreaService(areaDaoMock.Object);
            IList<Area> areasInCountry = areaService.GetAllIn(fakeCountry);
            Assert.AreEqual(1, areasInCountry.Count);

            areaDaoMock.Verify(x=>x.GetAllIn(It.Is<Country>(y=>y.Name == fakeCountry.Name)));
        }

        [Test]
        public void TestCreate()
        {
            Mock<IAreaDao> areaDaoMock = new Mock<IAreaDao>();
            areaDaoMock.Setup(x => x.Create(It.IsAny<Country>(), It.IsAny<Area>()));

            string countryName = "D";
            string areaName = "Gebiet 1";
            Country fakeCountry = new Country() { Name = countryName };

            IAreaService areaService = new AreaService(areaDaoMock.Object);
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

            Action act = ()=>new AreaService(null).Create(fakeCountry, name);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestGetAllWithoutCountry()
        {
            Action act = ()=> new AreaService(null).GetAllIn(null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}