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
    public class VariationDaoTest
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
            ICountryDao countryDao = new CountryDao(_graphClient);
            Country country = new Country() {Name = "D"};
            countryDao.Create(country);

            IRoutesDao routeDao = new RouteDao(_graphClient);
            Route route = new Route() {Name = "Route1"};
            routeDao.CreateIn(country, route);

            IDifficultyLevelScaleDao scaleDao = new DifficultyLevelScaleDao(_graphClient);
            DifficultyLevelScale scale = new DifficultyLevelScale() {Name = "sächsisch"};
            scaleDao.Create(scale);

            IDifficultyLevelDao levelDao = new DifficultyLevelDao(_graphClient);
            DifficultyLevel level = new DifficultyLevel() {Name = "7b"};
            levelDao.Create(scale, level);

            IVariationDao variationDao = new VariationDao(_graphClient);
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
            Variation variationWithLogEntry = _dataGenerator.CreateVariation();
            LogEntry logEntry = _dataGenerator.CreateLogEntry(variationWithLogEntry);

            bool isInUse = new VariationDao(_graphClient).IsInUse(variationWithLogEntry);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {

            Variation variationWithoutLogEntries = _dataGenerator.CreateVariation();

            bool isInUse = new VariationDao(_graphClient).IsInUse(variationWithoutLogEntries);
            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeInUseException))]
        public void TestDeleteInUse()
        {
            Variation variationWithLogEntry = _dataGenerator.CreateVariation();
            LogEntry logEntry = _dataGenerator.CreateLogEntry(variationWithLogEntry);

            IVariationDao variationDao = new VariationDao(_graphClient);
            variationDao.Delete(variationWithLogEntry);
        }

        [TestMethod]
        public void TestDeleteNormal()
        {
            Route route = _dataGenerator.CreateRouteInArea();
            Variation variationWithoutLogEntries = _dataGenerator.CreateVariation(route:route);
            IVariationDao variationDao = new VariationDao(_graphClient);
            variationDao.Delete(variationWithoutLogEntries);
            Assert.AreEqual(0, variationDao.GetAllOn(route).Count);
        }

        [TestMethod]
        public void TestSave()
        {
            Route route = _dataGenerator.CreateRouteInCountry();
            Variation variation = _dataGenerator.CreateVariation(route:route);

            variation.Name.Should().NotBe("newname");

            variation.Name = "newname";

            IVariationDao variationDao = new VariationDao(_graphClient);
            variationDao.Save(variation);

            variationDao.GetAllOn(route).First().Name.Should().Be("newname");
        }

        [TestMethod]
        public void TestChangeDifficultyLevelToNewValue()
        {
            Route route = _dataGenerator.CreateRouteInCountry();
            DifficultyLevel level = _dataGenerator.CreateDifficultyLevel();
            Variation variation = _dataGenerator.CreateVariation(route: route, difficultyLevel:level);

            DifficultyLevel newLevel = _dataGenerator.CreateDifficultyLevel(name: "neues Level");

            IVariationDao variationDao = new VariationDao(_graphClient);
            variationDao.ChangeDifficultyLevel(variation, newLevel);

            IDifficultyLevelDao difficultyLevelDao = new DifficultyLevelDao(_graphClient);
            difficultyLevelDao.GetLevelOnVariation(variation).Id.Should().Be(newLevel.Id);
        }
    }
}
