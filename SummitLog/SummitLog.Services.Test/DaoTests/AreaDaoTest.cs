using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    [TestClass]
    public class AreaDaoTest : DbTestBase
    {
        [TestMethod]
        public void TestCreateAndGetAll()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();
            AreaDao dao = Container.Resolve<AreaDao>();
            Area created = Container.Resolve<DbTestDataGenerator>().CreateArea(country: country);

            IEnumerable<Area> areasInCountry = dao.GetAllIn(country);
            Assert.AreEqual(1, areasInCountry.Count());
            Assert.AreEqual(created.Name, areasInCountry.First().Name);
            Assert.AreEqual(created.Id, areasInCountry.First().Id);
            Assert.AreEqual(created.Id, areasInCountry.First().Id);
        }

        [TestMethod]
        public void TestIsInUseBySummitGroup()
        {
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea();
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup(area: area);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            bool isInUse = areaDao.IsInUse(area);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsInUseByRoute()
        {
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInArea(area: area);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            bool isInUse = areaDao.IsInUse(area);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea();
            Area areaWithRoutes = Container.Resolve<DbTestDataGenerator>().CreateArea();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInArea(area: areaWithRoutes);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            bool isInUse = areaDao.IsInUse(area);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteNotInUse()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea(country: country);
            IAreaDao areaDao = Container.Resolve<AreaDao>();
            areaDao.Delete(area);

            Assert.AreEqual(0, areaDao.GetAllIn(country).Count);
        }

        [TestMethod]
        public void TestDeleteInUse()
        {
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInArea(area: area);

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            Action action = () => areaDao.Delete(area);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestUpdate()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea(country: country, name: "oldname");

            area.Name = "newname";

            IAreaDao areaDao = Container.Resolve<AreaDao>();
            areaDao.Save(area);

            Assert.AreEqual("newname", areaDao.GetAllIn(country).First().Name);
        }
    }
}