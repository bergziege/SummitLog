using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    [TestClass]
    public class SummitDaoTest
    {
        private GraphClient _graphClient;
        private DbTestDataGenerator _dataGenerator;

        [TestInitialize]
        public void Init()
        {
            _graphClient = new GraphClient(new Uri("http://localhost:7475/db/data"), "neo4j", "extra");
            _graphClient.Connect();
            _dataGenerator = new DbTestDataGenerator(_graphClient);
            _graphClient.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _graphClient.Transaction.Rollback();
        }

        [TestMethod]
        public void TestCreateAndGetAll()
        {
            SummitGroup group = _dataGenerator.CreateSummitGroup();
            ISummitDao summitDao = new SummitDao(_graphClient);
            Summit created = _dataGenerator.CreateSummit(summitGroup: group);

            IList<Summit> summitsInGroup = summitDao.GetAllIn(group);
            Assert.AreEqual(1, summitsInGroup.Count);
            Assert.AreEqual(created.Name, summitsInGroup.First().Name);
            Assert.AreEqual(created.Id, summitsInGroup.First().Id);
            Assert.AreEqual(created.Id, summitsInGroup.First().Id);
        }

        [TestMethod]
        public void TestIsInUseByRoute()
        {
            Summit summit = _dataGenerator.CreateSummit();
            Route route = _dataGenerator.CreateRouteInSummit(summit: summit);
            
            ISummitDao summitDao = new SummitDao(_graphClient);
            bool isInUse = summitDao.IsInUse(summit);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {
            Summit summit = _dataGenerator.CreateSummit();

            ISummitDao summitDao = new SummitDao(_graphClient);
            bool isInUse = summitDao.IsInUse(summit);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteUnused()
        {
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup();
            Summit summit = _dataGenerator.CreateSummit(summitGroup:summitGroup);

            ISummitDao summitDao = new SummitDao(_graphClient);
            summitDao.Delete(summit);

            Assert.AreEqual(0, summitDao.GetAllIn(summitGroup).Count);
            
        }

        [TestMethod]
        public void TestDeleteUsed()
        {
            Summit summit = _dataGenerator.CreateSummit();
            Route route = _dataGenerator.CreateRouteInSummit(summit: summit);

            ISummitDao summitDao = new SummitDao(_graphClient);
            Action action = ()=>summitDao.Delete(summit);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestUpdate()
        {
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup();
            Summit summit = _dataGenerator.CreateSummit("oldname", summitGroup);


            summit.Name = "newname";

            ISummitDao summitDao = new SummitDao(_graphClient);
            summitDao.Save(summit);

            Assert.AreEqual("newname", summitDao.GetAllIn(summitGroup).First().Name);

        }
    }
}
