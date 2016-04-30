using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class DifficultyLevelDaoTest: DbTestBase
    {

        [TestMethod]
        public void TestCreateAndGetAll()
        {
            
            DifficultyLevelDao difficultyLevelDao = Container.Resolve<DifficultyLevelDao>();
            DifficultyLevelScale scale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevelScale();
            DifficultyLevel created = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel(difficultyLevelScale:scale);
            
            IList<DifficultyLevel> levelsInScale = difficultyLevelDao.GetAllIn(scale);
            Assert.AreEqual(1, levelsInScale.Count);
            Assert.AreEqual(created.Name, levelsInScale.First().Name);
            Assert.AreEqual(created.Id, levelsInScale.First().Id);
            Assert.AreEqual(created.Id, levelsInScale.First().Id);
            Assert.AreEqual(created.Score, levelsInScale.First().Score);
        }

        [TestMethod]
        public void TestDeleteInUse()
        {
            DifficultyLevel level = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel();
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation(difficultyLevel: level);

            IDifficultyLevelDao dao = Container.Resolve<DifficultyLevelDao>();
            Action action = ()=> dao.Delete(level);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestDeleteNotInUse()
        {
            DifficultyLevel level = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel();
            IDifficultyLevelDao dao = Container.Resolve<DifficultyLevelDao>();
            dao.Delete(level);
        }

        [TestMethod]
        public void TestGetLevelOnVariation()
        {
            Country country = Container.Resolve<CountryDao>().Create(new Country() {Name = "var test"});
            Route route = Container.Resolve<RouteDao>().CreateIn(country,new Route() {Name = "r var test"});
            DifficultyLevelScale scale = Container.Resolve<DifficultyLevelScaleDao>().Create(new DifficultyLevelScale());
            DifficultyLevel level = Container.Resolve<DifficultyLevelDao>().Create(scale, new DifficultyLevel() {Name = "dl var test"});
            Variation variation = Container.Resolve<VariationDao>().Create(new Variation() {Name = "v var test"}, route,level);
            
            Variation variationOnRoute = Container.Resolve<VariationDao>().GetAllOn(route).First();

            DifficultyLevel levelOnVariation = Container.Resolve<DifficultyLevelDao>().GetLevelOnVariation(variationOnRoute);

            Assert.AreEqual(level.Id, levelOnVariation.Id);
        }

        [TestMethod]
        public void TestIsInUseWithUsedLevel()
        {
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInCountry();
            DifficultyLevel levelInUse = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel();
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation(levelInUse, route);

            IVariationDao variationDao = Container.Resolve<VariationDao>();
            IList<Variation> variationsOnRoute = variationDao.GetAllOn(route);
            Assert.AreEqual(1, variationsOnRoute.Count);
            
            IDifficultyLevelDao difficultyLevelDao = Container.Resolve<DifficultyLevelDao>();
            
            bool isLevelInUse = difficultyLevelDao.IsInUse(levelInUse);

            Assert.IsTrue(isLevelInUse);
        }

        [TestMethod]
        public void TestIsInUseWithUnusedLevel()
        {
            DifficultyLevel levelNotInUse = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel();

            IDifficultyLevelDao difficultyLevelDao = Container.Resolve<DifficultyLevelDao>();
            bool isLevelInUse = difficultyLevelDao.IsInUse(levelNotInUse);

            Assert.IsFalse(isLevelInUse);
        }

        [TestMethod]
        public void TestUpdate()
        {
            DifficultyLevelScale scale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevelScale();
            DifficultyLevel difficultyLevel= Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel(difficultyLevelScale:scale);

            difficultyLevel.Name = "newname";

            IDifficultyLevelDao difficultyLevelDao = Container.Resolve<DifficultyLevelDao>();
            difficultyLevelDao.Save(difficultyLevel);

            Assert.AreEqual("newname", difficultyLevelDao.GetAllIn(scale).First().Name);
        }
    }
}
