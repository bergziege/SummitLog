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
    public class DifficultyLevelScaleDaoTest
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
            IDifficultyLevelScaleDao dao = new DifficultyLevelScaleDao(_graphClient);
            DifficultyLevelScale difficultyLevelScale = new DifficultyLevelScale() {Name = "sächsisch"};
            DifficultyLevelScale created = dao.Create(difficultyLevelScale);
            IList<DifficultyLevelScale> allDifficultyLevelScales = dao.GetAll();
            Assert.AreEqual(1, allDifficultyLevelScales.Count);
            Assert.AreEqual(difficultyLevelScale.Name, allDifficultyLevelScales.First().Name);
            Assert.AreEqual(difficultyLevelScale.Id, allDifficultyLevelScales.First().Id);
            Assert.AreEqual(created.Id, allDifficultyLevelScales.First().Id);
        }

        [TestMethod]
        public void TestIfScaleIsInUse()
        {
            DifficultyLevelScale scale = _dataGenerator.CreateDifficultyLevelScale();
            DifficultyLevel levelWithScale = _dataGenerator.CreateDifficultyLevel(difficultyLevelScale:scale);
            IDifficultyLevelScaleDao difficultyLevelScaleDao = new DifficultyLevelScaleDao(_graphClient);
            Assert.IsTrue(difficultyLevelScaleDao.IsInUse(scale));
        }

        [TestMethod]
        public void TestDeleteScaleNotInUse()
        {
            DifficultyLevelScale scale = _dataGenerator.CreateDifficultyLevelScale();
            IDifficultyLevelScaleDao difficultyLevelScaleDao = new DifficultyLevelScaleDao(_graphClient);
            difficultyLevelScaleDao.Delete(scale);
            Assert.AreEqual(0, difficultyLevelScaleDao.GetAll().Count);
        }
    }
}
