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
    public class AreaDaoTest
    {
        private UnityContainer _container;

        [TestInitialize]
        public void Init()
        {
            GraphClient graphClient = new GraphClient(new Uri("http://localhost:7475/db/data"), "neo4j", "extra");
            graphClient.Connect();
            graphClient.BeginTransaction();
            _container = new UnityContainer();
            _container.RegisterInstance(graphClient);
            _container.RegisterInstance(new DbTestDataGenerator(graphClient));
        }

        [TestCleanup]
        public void Cleanup()
        {
            GraphClient client = _container.Resolve<GraphClient>();
            client.Transaction.Rollback();
        }

        [TestMethod]
        public void TestCreateAndGetAll()
        {
            Country country = _container.Resolve<DbTestDataGenerator>().CreateCountry();
            AreaDao dao = _container.Resolve<AreaDao>();
            Area created = _container.Resolve<DbTestDataGenerator>().CreateArea(country:country);

            IEnumerable<Area> areasInCountry = dao.GetAllIn(country);
            Assert.AreEqual(1, areasInCountry.Count());
            Assert.AreEqual(created.Name, areasInCountry.First().Name);
            Assert.AreEqual(created.Id, areasInCountry.First().Id);
            Assert.AreEqual(created.Id, areasInCountry.First().Id);
        }

        [TestMethod]
        public void TestIsInUseBySummitGroup()
        {
            Area area = _dataGenerator.CreateArea();
            SummitGroup summitGroup = _dataGenerator.CreateSummitGroup(area: area);

            IAreaDao areaDao = new AreaDao();
            bool isInUse = areaDao.IsInUse(area);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsInUseByRoute()
        {
            Area area = _dataGenerator.CreateArea();
            Route route = _dataGenerator.CreateRouteInArea(area: area);

            IAreaDao areaDao = new AreaDao(_graphClient);
            bool isInUse = areaDao.IsInUse(area);

            Assert.IsTrue(isInUse);
        }

        [TestMethod]
        public void TestIsNotInUse()
        {
            Area area = _dataGenerator.CreateArea();
            Area areaWithRoutes = _dataGenerator.CreateArea();
            Route route = _dataGenerator.CreateRouteInArea(area: areaWithRoutes);

            IAreaDao areaDao = new AreaDao(_graphClient);
            bool isInUse = areaDao.IsInUse(area);

            Assert.IsFalse(isInUse);
        }

        [TestMethod]
        public void TestDeleteNotInUse()
        {
            Country country = _dataGenerator.CreateCountry();
            Area area = _dataGenerator.CreateArea(country:country);
            IAreaDao areaDao = new AreaDao(_graphClient);
            areaDao.Delete(area);

            Assert.AreEqual(0,areaDao.GetAllIn(country).Count);
        }

        [TestMethod]
        public void TestDeleteInUse()
        {
            Area area = _dataGenerator.CreateArea();
            Route route = _dataGenerator.CreateRouteInArea(area: area);

            IAreaDao areaDao = new AreaDao(_graphClient);
            Action action = ()=>areaDao.Delete(area);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestUpdate()
        {
            Country country = _dataGenerator.CreateCountry();
            Area area = _dataGenerator.CreateArea(country:country, name:"oldname");

            area.Name = "newname";

            IAreaDao areaDao = new AreaDao(_graphClient);
            areaDao.Save(area);

            Assert.AreEqual("newname", areaDao.GetAllIn(country).First().Name);
        }
    }
}
