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
    public class SummitDaoTest: DbTestBase
    {

        [TestMethod]
        public void TestCreateAndGetAll()
        {
            SummitGroup group = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup();
            ISummitDao summitDao = Container.Resolve<SummitDao>();
            Summit created = Container.Resolve<DbTestDataGenerator>().CreateSummit(summitGroup: group, rating:4.5);

            IList<Summit> summitsInGroup = summitDao.GetAllIn(group);
            summitsInGroup.Should().HaveCount(1);
            created.Name.Should().Be(summitsInGroup.First().Name);
            created.Id.Should().Be(summitsInGroup.First().Id);
            created.SummitNumber.Should().Be(summitsInGroup.First().SummitNumber);
            created.Rating.Should().Be(4.5);

        }

        [TestMethod]
        public void TestIsInUseByRoute()
        {
            Summit summit = Container.Resolve<DbTestDataGenerator>().CreateSummit();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInSummit(summit: summit);
            
            ISummitDao summitDao = Container.Resolve<SummitDao>();
            bool isInUse = summitDao.IsInUse(summit);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {
            Summit summit = Container.Resolve<DbTestDataGenerator>().CreateSummit();

            ISummitDao summitDao = Container.Resolve<SummitDao>();
            bool isInUse = summitDao.IsInUse(summit);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteUnused()
        {
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup();
            Summit summit = Container.Resolve<DbTestDataGenerator>().CreateSummit(summitGroup:summitGroup);

            ISummitDao summitDao = Container.Resolve<SummitDao>();
            summitDao.Delete(summit);

            Assert.AreEqual(0, summitDao.GetAllIn(summitGroup).Count);
            
        }

        [TestMethod]
        public void TestDeleteUsed()
        {
            Summit summit = Container.Resolve<DbTestDataGenerator>().CreateSummit();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInSummit(summit: summit);

            ISummitDao summitDao = Container.Resolve<SummitDao>();
            Action action = ()=>summitDao.Delete(summit);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestUpdate()
        {
            SummitGroup summitGroup = Container.Resolve<DbTestDataGenerator>().CreateSummitGroup();
            Summit summit = Container.Resolve<DbTestDataGenerator>().CreateSummit(name:"oldname", summitNumber:"100C", summitGroup:summitGroup);


            summit.Name = "newname";
            summit.SummitNumber = "200A";
            summit.Rating = 2.3;

            ISummitDao summitDao = Container.Resolve<SummitDao>();
            summitDao.Save(summit);

            Summit savedSummit = summitDao.GetAllIn(summitGroup).First();
            savedSummit.Name.Should().Be("newname");
            savedSummit.SummitNumber.Should().Be("200A");
            savedSummit.Rating.Should().Be(2.3);


        }
    }
}
