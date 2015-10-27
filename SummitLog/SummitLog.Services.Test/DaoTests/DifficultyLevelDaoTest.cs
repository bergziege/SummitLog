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
    public class DifficultyLevelDaoTest
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
            IDifficultyLevelScaleDao difficultyLevelScaleDao = new DifficultyLevelScaleDao(_graphClient);
            DifficultyLevelScale scale = new DifficultyLevelScale() {Name = "sächsisch"};
            difficultyLevelScaleDao.Create(scale);

            IDifficultyLevelDao difficultyLevelDao = new DifficultyLevelDao(_graphClient);
            DifficultyLevel level = new DifficultyLevel() {Name = "7b", Score = 2000};
            difficultyLevelDao.Create(scale,level);
            
            IList<DifficultyLevel> levelsInScale = difficultyLevelDao.GetAllIn(scale);
            Assert.AreEqual(1, levelsInScale.Count);
            Assert.AreEqual(level.Name, levelsInScale.First().Name);
            Assert.AreEqual(level.Id, levelsInScale.First().Id);
            Assert.AreEqual(level.Score, levelsInScale.First().Score);
        }
    }
}
