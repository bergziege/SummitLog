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
    public class LogEntryDaoTest
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
            variationDao.Create(variation, route, level);

            ILogEntryDao logEntryDao = new LogEntryDao(_graphClient);
            LogEntry entry = new LogEntry() {DateTime = DateTime.Today, Memo = "solo"};
            logEntryDao.Create(variation, entry);

            IList<LogEntry> logsOnVariation = logEntryDao.GetAllIn(variation);
            Assert.AreEqual(1, logsOnVariation.Count);
            Assert.AreEqual(DateTime.Today, logsOnVariation.First().DateTime);
            Assert.AreEqual(entry.Id, logsOnVariation.First().Id);
            Assert.AreEqual(entry.Memo, logsOnVariation.First().Memo);
        }
    }
}
