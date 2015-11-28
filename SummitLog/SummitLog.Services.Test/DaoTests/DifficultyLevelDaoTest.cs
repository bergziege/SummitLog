using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Exceptions;
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

        [TestMethod]
        [ExpectedException(typeof(NodeInUseException))]
        public void TestDeleteInUse()
        {
            DifficultyLevel level = _dataGenerator.CreateDifficultyLevel();
            Variation variation = _dataGenerator.CreateVariation(difficultyLevel: level);

            IDifficultyLevelDao dao = new DifficultyLevelDao(_graphClient);
            dao.Delete(level);
        }

        [TestMethod]
        public void TestDeleteNotInUse()
        {
            DifficultyLevel level = _dataGenerator.CreateDifficultyLevel();
            IDifficultyLevelDao dao = new DifficultyLevelDao(_graphClient);
            dao.Delete(level);
        }

        [TestMethod]
        public void TestGetLevelOnVariation()
        {
            Country country = new CountryDao(_graphClient).Create(new Country() {Name = "var test"});
            Route route = new RouteDao(_graphClient).CreateIn(country,new Route() {Name = "r var test"});
            DifficultyLevelScale scale = new DifficultyLevelScaleDao(_graphClient).Create(new DifficultyLevelScale());
            DifficultyLevel level = new DifficultyLevelDao(_graphClient).Create(scale, new DifficultyLevel() {Name = "dl var test"});
            Variation variation = new VariationDao(_graphClient).Create(new Variation() {Name = "v var test"}, route,level);
            
            Variation variationOnRoute = new VariationDao(_graphClient).GetAllOn(route).First();

            DifficultyLevel levelOnVariation = new DifficultyLevelDao(_graphClient).GetLevelOnVariation(variationOnRoute);

            Assert.AreEqual(level.Id, levelOnVariation.Id);
        }

        [TestMethod]
        public void TestIsInUseWithUsedLevel()
        {
            Route route = _dataGenerator.CreateRouteInCountry();
            DifficultyLevel levelInUse = _dataGenerator.CreateDifficultyLevel();
            Variation variation = _dataGenerator.CreateVariation(levelInUse, route);

            IVariationDao variationDao = new VariationDao(_graphClient);
            IList<Variation> variationsOnRoute = variationDao.GetAllOn(route);
            Assert.AreEqual(1, variationsOnRoute.Count);
            
            IDifficultyLevelDao difficultyLevelDao = new DifficultyLevelDao(_graphClient);
            


            bool isLevelInUse = difficultyLevelDao.IsInUse(levelInUse);

            Assert.IsTrue(isLevelInUse);
        }

        [TestMethod]
        public void TestIsInUseWithUnusedLevel()
        {
            DifficultyLevel levelNotInUse = _dataGenerator.CreateDifficultyLevel();

            IDifficultyLevelDao difficultyLevelDao = new DifficultyLevelDao(_graphClient);
            bool isLevelInUse = difficultyLevelDao.IsInUse(levelNotInUse);

            Assert.IsFalse(isLevelInUse);
        }
    }
}
