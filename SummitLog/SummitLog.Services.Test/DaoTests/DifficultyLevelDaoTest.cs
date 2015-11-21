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
            
            DifficultyLevelDao difficultyLevelDao = new DifficultyLevelDao(_graphClient);
            DifficultyLevelScale scale = _dataGenerator.CreateDifficultyLevelScale();
            DifficultyLevel created = _dataGenerator.CreateDifficultyLevel(difficultyLevelScale:scale);
            
            IList<DifficultyLevel> levelsInScale = difficultyLevelDao.GetAllIn(scale);
            Assert.AreEqual(1, levelsInScale.Count);
            Assert.AreEqual(created.Name, levelsInScale.First().Name);
            Assert.AreEqual(created.Id, levelsInScale.First().Id);
            Assert.AreEqual(created.Id, levelsInScale.First().Id);
            Assert.AreEqual(created.Score, levelsInScale.First().Score);
        }
    }
}
