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
    public class SummitDaoTest
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
            SummitGroup group = _dataGenerator.CreateSummitGroup();
            ISummitDao summitDao = new SummitDao(_graphClient);
            Summit created = _dataGenerator.CreateSummit(summitGroup: group);

            IList<Summit> summitsInGroup = summitDao.GetAllIn(group);
            Assert.AreEqual(1, summitsInGroup.Count);
            Assert.AreEqual(created.Name, summitsInGroup.First().Name);
            Assert.AreEqual(created.Id, summitsInGroup.First().Id);
            Assert.AreEqual(created.Id, summitsInGroup.First().Id);
        }
    }
}
