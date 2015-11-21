using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
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
    }
}
