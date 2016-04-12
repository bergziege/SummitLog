using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    [TestClass]
    public class LogEntryDaoTest: DbTestBase
    {

        [TestMethod]
        public void TestCreateAndGetAll()
        {
            ILogEntryDao logEntryDao = Container.Resolve<LogEntryDao>();
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation();
            LogEntry created = Container.Resolve<DbTestDataGenerator>().CreateLogEntry(variation: variation);

            IList<LogEntry> logsOnVariation = logEntryDao.GetAllIn(variation);
            Assert.AreEqual(1, logsOnVariation.Count);
            Assert.AreEqual(created.DateTime, logsOnVariation.First().DateTime);
            Assert.AreEqual(created.Id, logsOnVariation.First().Id);
            Assert.AreEqual(created.Memo, logsOnVariation.First().Memo);
        }

        [TestMethod]
        public void TestDeleteNull()
        {
            ILogEntryDao dao = Container.Resolve<LogEntryDao>();
            Action action = () => dao.Delete(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void TestDeleteNormal()
        {

            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInCountry();
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation(route: route);

            IVariationDao variationDao = Container.Resolve<VariationDao>();
            ILogEntryDao logEntryDao = Container.Resolve<LogEntryDao>();
            LogEntry created = Container.Resolve<DbTestDataGenerator>().CreateLogEntry(variation);

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
            Variation variation = Container.Resolve<DbTestDataGenerator>().CreateVariation();
            LogEntry logEntry = Container.Resolve<DbTestDataGenerator>().CreateLogEntry(variation: variation);

            logEntry.Memo.Should().NotBe("newmemo");
            logEntry.DateTime.Should().NotBe(1.April(2015));

            logEntry.DateTime = 1.April(2015);
            logEntry.Memo = "newmemo";

            ILogEntryDao logEntryDao = Container.Resolve<LogEntryDao>();
            logEntryDao.Save(logEntry);

            LogEntry reloaded = logEntryDao.GetAllIn(variation).First();
            reloaded.DateTime.Should().Be(1.April(2015));
            reloaded.Memo.Should().Be("newmemo");

        }
    }
}
