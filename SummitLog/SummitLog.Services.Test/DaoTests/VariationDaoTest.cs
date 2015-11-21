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
    }
}
