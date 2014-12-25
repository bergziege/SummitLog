using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Persistence;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace De.BerndNet2000.SummitLog.Service {
    
    /// <summary>
    /// Basisklasse für alle ServiceTest-Klassen
    /// </summary>
    [TestClass]
    public class ServiceTestBase : PersistenceBaseTest {
        
        protected override ApplicationContextResources GetApplicationContextResources() {
            ApplicationContextResources applicationContextRessources = base.GetApplicationContextResources();
            applicationContextRessources.AddContextLocation("file://Spring.Services.xml");
            return applicationContextRessources;
        }
    }
}