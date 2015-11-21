using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    [TestClass]
    public class CountryDaoTest
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
        public void TestGetAll()
        {
            CountryDao dao = new CountryDao(_graphClient);
            Country created = _dataGenerator.CreateCountry();
            IEnumerable<Country> allCountries = dao.GetAll();
            Assert.AreEqual(1, allCountries.Count());
            Assert.AreEqual(created.Name, allCountries.First().Name);
            Assert.AreEqual(created.Id, allCountries.First().Id);
        }

        [TestMethod]
        public void TestCreateAndReturn()
        {
            CountryDao dao = new CountryDao(_graphClient);
            Country newCountry = new Country() { Name = "Deutschland" };
            Country created = dao.Create(newCountry);
            IEnumerable<Country> allCountries = dao.GetAll();
            Assert.AreEqual(1, allCountries.Count());
            Assert.AreEqual(created.Id, allCountries.First().Id);
        }
    }
}
