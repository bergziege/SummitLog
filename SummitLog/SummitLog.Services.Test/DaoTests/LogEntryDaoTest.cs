using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    [TestClass]
    public class LogEntryDaoTest
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
            ILogEntryDao logEntryDao = new LogEntryDao(_graphClient);
            Variation variation = _dataGenerator.CreateVariation();
            LogEntry created = _dataGenerator.CreateLogEntry(variation: variation);

            IList<LogEntry> logsOnVariation = logEntryDao.GetAllIn(variation);
            Assert.AreEqual(1, logsOnVariation.Count);
            Assert.AreEqual(created.DateTime, logsOnVariation.First().DateTime);
            Assert.AreEqual(created.Id, logsOnVariation.First().Id);
            Assert.AreEqual(created.Memo, logsOnVariation.First().Memo);
        }

        [TestMethod]
        public void TestDeleteNull()
        {
            ILogEntryDao dao = new LogEntryDao(_graphClient);
            Action action = () => dao.Delete(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void TestDeleteNormal()
        {

            Route route = _dataGenerator.CreateRouteInCountry();
            Variation variation = _dataGenerator.CreateVariation(route: route);

            IVariationDao variationDao = new VariationDao(_graphClient);
            ILogEntryDao logEntryDao = new LogEntryDao(_graphClient);
            LogEntry created = _dataGenerator.CreateLogEntry(variation);

            LogEntry logEntry = logEntryDao.GetAllIn(variation).First();

            /* Wenn ein Logeintrag einer Variation gelöscht wird */
            logEntryDao.Delete(logEntry);

            /* Muss der Logeintrag verschwunden, aber die Variation noch vorhanden sein */
            Assert.AreEqual(0, logEntryDao.GetAllIn(variation).Count);
            Assert.AreEqual(1, variationDao.GetAllOn(route).Count);
        }

        [TestMethod]
        public void TestSave()
        {
            Variation variation = _dataGenerator.CreateVariation();
            LogEntry logEntry = _dataGenerator.CreateLogEntry(variation: variation);

            logEntry.Memo.Should().NotBe("newmemo");
            logEntry.DateTime.Should().NotBe(1.April(2015));

            logEntry.DateTime = 1.April(2015);
            logEntry.Memo = "newmemo";

            ILogEntryDao logEntryDao = new LogEntryDao(_graphClient);
            logEntryDao.Save(logEntry);

            LogEntry reloaded = logEntryDao.GetAllIn(variation).First();
            reloaded.DateTime.Should().Be(1.April(2015));
            reloaded.Memo.Should().Be("newmemo");

        }
    }
}
