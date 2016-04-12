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
    public class CountryDaoTest: DbTestBase
    {
        
        [TestMethod]
        public void TestGetAll()
        {
            CountryDao dao = Container.Resolve<CountryDao>();
            Country created = Container.Resolve<DbTestDataGenerator>().CreateCountry();
            IEnumerable<Country> allCountries = dao.GetAll();
            Assert.AreEqual(1, allCountries.Count());
            Assert.AreEqual(created.Name, allCountries.First().Name);
            Assert.AreEqual(created.Id, allCountries.First().Id);
        }

        [TestMethod]
        public void TestCreateAndReturn()
        {
            CountryDao dao = Container.Resolve<CountryDao>();
            Country newCountry = new Country() { Name = "Deutschland" };
            Country created = dao.Create(newCountry);
            IEnumerable<Country> allCountries = dao.GetAll();
            Assert.AreEqual(1, allCountries.Count());
            Assert.AreEqual(created.Id, allCountries.First().Id);
        }

        [TestMethod]
        public void TestIsInUseByArea()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();
            Area area = Container.Resolve<DbTestDataGenerator>().CreateArea(country: country);

            ICountryDao countryDao = Container.Resolve<CountryDao>();
            bool isInUse = countryDao.IsInUse(country);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsInUseByRoute()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInCountry(country:country);

            ICountryDao countryDao = Container.Resolve<CountryDao>();
            bool isInUse = countryDao.IsInUse(country);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();

            ICountryDao countryDao = Container.Resolve<CountryDao>();
            bool isInUse = countryDao.IsInUse(country);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteNotInUse()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();

            ICountryDao countryDao = Container.Resolve<CountryDao>();
            countryDao.Delete(country);

            Assert.AreEqual(0, countryDao.GetAll().Count);
        }

        [TestMethod]
        public void TestDeleteInUse()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();
            Route route = Container.Resolve<DbTestDataGenerator>().CreateRouteInCountry(country: country);

            ICountryDao countryDao = Container.Resolve<CountryDao>();
            Action action = ()=>countryDao.Delete(country);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestUpdate()
        {
            Country country = Container.Resolve<DbTestDataGenerator>().CreateCountry();

            country.Name = "newname";

            ICountryDao countryDao = Container.Resolve<CountryDao>();
            countryDao.Save(country);

            Assert.AreEqual("newname", countryDao.GetAll().First().Name);
        }
    }
}
