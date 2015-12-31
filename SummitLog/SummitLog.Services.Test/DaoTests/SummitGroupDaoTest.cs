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
    public class SummitGroupDaoTest
    {
        private GraphClient _graphClient;
        private DbTestDataGenerator _dataGenerator;

        [TestInitialize]
        public void Init()
        {
            _graphClient = new GraphClient(new Uri("http://localhost:7475/db/data"), "neo4j", "extra");
            _graphClient.Connect();
            _graphClient.BeginTransaction();
            _dataGenerator = new DbTestDataGenerator(_graphClient);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _graphClient.Transaction.Rollback();
        }

        [TestMethod]
        public void TestCreateAndGetAll()
        {
            Area area = _dataGenerator.CreateArea();
            SummitGroupDao groupDao = new SummitGroupDao(_graphClient);
            SummitGroup created = _dataGenerator.CreateSummitGroup(area: area);

            IList<SummitGroup> groupsInArea = groupDao.GetAllIn(area);
            Assert.AreEqual(1, groupsInArea.Count);
            Assert.AreEqual(created.Name, groupsInArea.First().Name);
            Assert.AreEqual(created.Id, groupsInArea.First().Id);
            Assert.AreEqual(created.Id, groupsInArea.First().Id);
        }

        [TestMethod]
        public void TestIsInUseBySummit()
        {
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup();
            Summit summit = _dataGenerator.CreateSummit(summitGroup: summitGroup);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            bool isInUse = summitGroupDao.IsInUse(summitGroup);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsInUseByRoute()
        {
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup();
            Route route = _dataGenerator.CreateRouteInSummitGroup(summitGroup: summitGroup);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            bool isInUse = summitGroupDao.IsInUse(summitGroup);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup();

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            bool isInUse = summitGroupDao.IsInUse(summitGroup);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteInUse()
        {
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup();
            Route route = _dataGenerator.CreateRouteInSummitGroup(summitGroup: summitGroup);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            Action action = ()=> summitGroupDao.Delete(summitGroup);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestDeleteNotInUse()
        {
            Area area = _dataGenerator.CreateArea();
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup(area:area);

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            summitGroupDao.Delete(summitGroup);

            Assert.AreEqual(0, summitGroupDao.GetAllIn(area).Count);
        }

        [TestMethod]
        public void TestUpdate()
        {
            Area area = _dataGenerator.CreateArea();
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup("oldname", area);

            summitGroup.Name = "newname";

            ISummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            summitGroupDao.Save(summitGroup);

            Assert.AreEqual("newname", summitGroupDao.GetAllIn(area).First().Name);
        }
    }
}
