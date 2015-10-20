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
    public class SummitDaoTest
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
            Country newCountry = new Country() { Name = "Deutschland" };
            countryDao.Create(newCountry);

            IAreaDao dao = new AreaDao(_graphClient);
            Area newArea = new Area() {Name = "Sächsiche Schweiz"};
            dao.Create(newCountry, newArea);

            ISummitGroupDao groupDao = new SummitGroupDao(_graphClient);
            SummitGroup newGroup = new SummitGroup() {Name = "Gipfelgruppe"};
            groupDao.Create(newArea, newGroup);

            ISummitDao summitDao = new SummitDao(_graphClient);
            Summit newSummit = new Summit() {Name = "Gipfel"};
            summitDao.Create(newGroup, newSummit);

            IList<Summit> summitsInGroup = summitDao.GetAllIn(newGroup);
            Assert.AreEqual(1, summitsInGroup.Count);
            Assert.AreEqual(newSummit.Name, summitsInGroup.First().Name);
            Assert.AreEqual(newSummit.Id, summitsInGroup.First().Id);
        }
    }
}
