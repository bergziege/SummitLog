using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test
{
    [TestClass]
    public class DifficultyLevelScaleDaoTest
    {
        private GraphClient _graphClient;

        [TestInitialize]
        public void Init()
        {
            _graphClient = new GraphClient(new Uri("http://localhost:7475/db/data"), "neo4j", "extra");
            _graphClient.Connect();
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
            IDifficultyLevelScaleDao dao = new DifficultyLevelScaleDao(_graphClient);
            DifficultyLevelScale difficultyLevelScale = new DifficultyLevelScale() {Name = "sächsisch"};
            dao.Create(difficultyLevelScale);
            IList<DifficultyLevelScale> allDifficultyLevelScales = dao.GetAll();
            Assert.AreEqual(1, allDifficultyLevelScales.Count);
            Assert.AreEqual(difficultyLevelScale.Name, allDifficultyLevelScales.First().Name);
            Assert.AreEqual(difficultyLevelScale.Id, allDifficultyLevelScales.First().Id);
        }
    }
}
