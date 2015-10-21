using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test
{
    [TestClass]
    public class CountryDaoTest
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
            CountryDao dao = new CountryDao(_graphClient);
            Country newCountry = new Country() {Name = "Deutschland"};
            dao.Create(newCountry);
            IEnumerable<Country> allCountries = dao.GetAll();
            Assert.AreEqual(1, allCountries.Count());
            Assert.AreEqual(newCountry.Name, allCountries.First().Name);
            Assert.AreEqual(newCountry.Id, allCountries.First().Id);
        }
    }
}
