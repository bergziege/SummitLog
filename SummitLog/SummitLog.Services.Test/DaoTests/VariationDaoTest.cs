using System;
using System.Collections.Generic;
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
    public class VariationDaoTest: DbTestBase
    {
       
        [TestMethod]
        public void TestCreateAndGetAll()
        {
            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Country country = new Country() {Name = "D"};
            countryDao.Create(country);

            IRoutesDao routeDao = Container.Resolve<RouteDao>(); ;
            Route route = new Route() {Name = "Route1"};
            routeDao.CreateIn(country, route);

            IDifficultyLevelScaleDao scaleDao = Container.Resolve<DifficultyLevelScaleDao>();
            DifficultyLevelScale scale = new DifficultyLevelScale() {Name = "sächsisch"};
            scaleDao.Create(scale);

            IDifficultyLevelDao levelDao = Container.Resolve<DifficultyLevelDao>();
            DifficultyLevel level = new DifficultyLevel() {Name = "7b"};
            levelDao.Create(scale, level);

            IVariationDao variationDao = Container.Resolve<VariationDao>();
            Variation variation = new Variation() {Name = "Ein Weg der Route1 als 7b"};
            Variation created = variationDao.Create(variation, route, level);

            IList<Variation> variationsOnRoute = variationDao.GetAllOn(route);
            Assert.AreEqual(1, variationsOnRoute.Count);
            Assert.AreEqual(variation.Name, variationsOnRoute.First().Name);
            Assert.AreEqual(variation.Id, variationsOnRoute.First().Id);
            Assert.AreEqual(created.Id, variationsOnRoute.First().Id);
        }

        [TestMethod]
        public void TestIsInUse()
        {
            Variation variationWithLogEntry = Container.Resolve<DbTestDataGenerator>().CreateVariation();
            LogEntry logEntry = Container.Resolve<DbTestDataGenerator>().CreateLogEntry(variationWithLogEntry);

            bool isInUse = Container.Resolve<VariationDao>().IsInUse(variationWithLogEntry);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {

            Variation variationWithoutLogEntries = Container.Resolve<DbTestDataGenerator>().CreateVariation();

            bool isInUse = Container.Resolve<VariationDao>().IsInUse(variationWithoutLogEntries);
            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteInUse()
        {
            Variation variationWithLogEntry = Container.Resolve<DbTestDataGenerator>().CreateVariation();
            LogEntry logEntry = Container.Resolve<DbTestDataGenerator>().CreateLogEntry(variationWithLogEntry);

            IVariationDao variationDao = Container.Resolve<VariationDao>();
            Action action = ()=> variationDao.Delete(variationWithLogEntry);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestDeleteNormal()
        {
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInArea();
            Variation variationWithoutLogEntries = Container.Resolve<DbTestDataGenerator>().CreateVariation(route:route);
            IVariationDao variationDao = Container.Resolve<VariationDao>();
            variationDao.Delete(variationWithoutLogEntries);
            Assert.AreEqual(0, variationDao.GetAllOn(route).Count);
        }

        [TestMethod]
        public void TestSave()
        {
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInCountry();
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation(route:route);

            variation.Name.Should().NotBe("newname");

            variation.Name = "newname";

            IVariationDao variationDao = Container.Resolve<VariationDao>();
            variationDao.Save(variation);

            variationDao.GetAllOn(route).First().Name.Should().Be("newname");
        }

        [TestMethod]
        public void TestChangeDifficultyLevelToNewValue()
        {
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInCountry();
            DifficultyLevel level = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel();
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation(route: route, difficultyLevel:level);

            DifficultyLevel newLevel = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel(name: "neues Level");

            IVariationDao variationDao = Container.Resolve<VariationDao>();
            variationDao.ChangeDifficultyLevel(variation, newLevel);

            IDifficultyLevelDao difficultyLevelDao = Container.Resolve<DifficultyLevelDao>();
            difficultyLevelDao.GetLevelOnVariation(variation).Id.Should().Be(newLevel.Id);
        }
    }
}
