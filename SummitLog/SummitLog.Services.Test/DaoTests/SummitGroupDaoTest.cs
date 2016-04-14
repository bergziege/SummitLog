using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    [TestClass]
    public class SummitGroupDaoTest: DbTestBase
    {
        
        [TestMethod]
        public void TestCreateAndGetAll()
        {
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea();
            SummitGroupDao groupDao = Container.Resolve<SummitGroupDao>();
            SummitGroup created = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup(area: area);

            IList<SummitGroup> groupsInArea = groupDao.GetAllIn(area);
            Assert.AreEqual(1, groupsInArea.Count);
            Assert.AreEqual(created.Name, groupsInArea.First().Name);
            Assert.AreEqual(created.Id, groupsInArea.First().Id);
            Assert.AreEqual(created.Id, groupsInArea.First().Id);
        }

        [TestMethod]
        public void TestIsInUseBySummit()
        {
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup();
            Summit summit = Container.Resolve<DbTestDataGenerator>().CreateSummit(summitGroup: summitGroup);

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            bool isInUse = summitGroupDao.IsInUse(summitGroup);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsInUseByRoute()
        {
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInSummitGroup(summitGroup: summitGroup);

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            bool isInUse = summitGroupDao.IsInUse(summitGroup);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup();

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            bool isInUse = summitGroupDao.IsInUse(summitGroup);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteInUse()
        {
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInSummitGroup(summitGroup: summitGroup);

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            Action action = ()=> summitGroupDao.Delete(summitGroup);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestDeleteNotInUse()
        {
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea();
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup(area:area);

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            summitGroupDao.Delete(summitGroup);

            Assert.AreEqual(0, summitGroupDao.GetAllIn(area).Count);
        }

        [TestMethod]
        public void TestUpdate()
        {
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea();
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup("oldname", area);

            summitGroup.Name = "newname";

            ISummitGroupDao summitGroupDao = Container.Resolve<SummitGroupDao>();
            summitGroupDao.Save(summitGroup);

            Assert.AreEqual("newname", summitGroupDao.GetAllIn(area).First().Name);
        }
    }
}
