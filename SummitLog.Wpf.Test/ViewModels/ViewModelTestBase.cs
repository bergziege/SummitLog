using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Service;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spring.Context.Support;

namespace De.BerndNet2000.SummitLog.Wpf.ViewModels {
    public abstract class ViewModelTestBase : ServiceTestBase {
        protected override ApplicationContextResources GetApplicationContextResources() {
            ApplicationContextResources applicationContextRessources = base.GetApplicationContextResources();
            applicationContextRessources.AddContextLocation("file://~/Config/Spring.Commons.xml");
            applicationContextRessources.AddContextLocation("file://~/Config/Spring.ViewModels.xml");
            return applicationContextRessources;
        }

        [TestInitialize]
        public override void InitializeTest() {
            base.InitializeTest();
            ContextRegistry.Clear();
            ContextRegistry.RegisterContext(base.Context);
        }
    }
}