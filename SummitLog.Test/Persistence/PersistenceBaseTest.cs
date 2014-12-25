using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace De.BerndNet2000.SummitLog.Persistence {
    [TestClass]
    public class PersistenceBaseTest : SpringTransactionalTest {
        
        [Resource]
        private CreatorUtil _creatorUtil;

        public PersistenceBaseTest()
            : base("summitLogTransactionManager") {
        }

        protected CreatorUtil DomainObjectCreatorUtil {
            get { return _creatorUtil; }
        }

        protected override ApplicationContextResources GetApplicationContextResources() {
            ApplicationContextResources applicationContextRessources = base.GetApplicationContextResources();
            applicationContextRessources.AddContextLocation("file://~/Config/Spring.Database.Sqlite.Test.config.xml");
            applicationContextRessources.AddContextLocation("file://~/Config/Spring.Persistence.xml");
            applicationContextRessources.AddContextLocation("file://~/Config/Spring.Test.xml");

            return applicationContextRessources;
        }
    }
}
