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

        [TestMethod]
        public void TestIsInUseByArea()
        {
            Country country = _dataGenerator.CreateCountry();
            Area area = _dataGenerator.CreateArea(country: country);

            ICountryDao countryDao = new CountryDao(_graphClient);
            bool isInUse = countryDao.IsInUse(country);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsInUseByRoute()
        {
            Country country = _dataGenerator.CreateCountry();
            Route route = _dataGenerator.CreateRouteInCountry(country:country);

            ICountryDao countryDao = new CountryDao(_graphClient);
            bool isInUse = countryDao.IsInUse(country);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {
            Country country = _dataGenerator.CreateCountry();

            ICountryDao countryDao = new CountryDao(_graphClient);
            bool isInUse = countryDao.IsInUse(country);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteNotInUse()
        {
            Country country = _dataGenerator.CreateCountry();

            ICountryDao countryDao = new CountryDao(_graphClient);
            countryDao.Delete(country);

            Assert.AreEqual(0, countryDao.GetAll().Count);
        }

        [TestMethod]
        public void TestDeleteInUse()
        {
            Country country = _dataGenerator.CreateCountry();
            Route route = _dataGenerator.CreateRouteInCountry(country: country);

            ICountryDao countryDao = new CountryDao(_graphClient);
            Action action = ()=>countryDao.Delete(country);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestUpdate()
        {
            Country country = _dataGenerator.CreateCountry();

            country.Name = "newname";

            ICountryDao countryDao = new CountryDao(_graphClient);
            countryDao.Save(country);

            Assert.AreEqual("newname", countryDao.GetAll().First().Name);
        }
    }
}
