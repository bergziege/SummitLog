using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Test.DaoTests;

namespace SummitLog.Services.Test
{
    [TestClass]
    public abstract class DbTestBase: TestWithContainerBase
    {
        [TestInitialize]
        public override void Init()
        {
            base.Init();
            GraphClient graphClient = new GraphClient(new Uri("http://localhost:7475/db/data"), "neo4j", "extra");
            graphClient.Connect();
            graphClient.BeginTransaction();
            Container.RegisterInstance(graphClient);
            Container.RegisterInstance(Container.Resolve<DbTestDataGenerator>());
        }

        [TestCleanup]
        public void Rollback()
        {
            Container.Resolve<GraphClient>().Transaction.Rollback();
        }
    }
}