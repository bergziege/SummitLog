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
    public class AreaDaoTest
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
            Country country = _dataGenerator.CreateCountry();
            AreaDao dao = new AreaDao(_graphClient);
            Area created = _dataGenerator.CreateArea(country:country);

            IEnumerable<Area> areasInCountry = dao.GetAllIn(country);
            Assert.AreEqual(1, areasInCountry.Count());
            Assert.AreEqual(created.Name, areasInCountry.First().Name);
            Assert.AreEqual(created.Id, areasInCountry.First().Id);
            Assert.AreEqual(created.Id, areasInCountry.First().Id);
        }
    }
}
