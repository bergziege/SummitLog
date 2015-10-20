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
    public class SummitGroupDaoTest
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

            IList<SummitGroup> groupsInArea = groupDao.GetAllIn(newArea);
            Assert.AreEqual(1, groupsInArea.Count);
            Assert.AreEqual(newGroup.Name, groupsInArea.First().Name);
            Assert.AreEqual(newGroup.Id, groupsInArea.First().Id);
        }
    }
}
