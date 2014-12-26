using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Service;

namespace De.BerndNet2000.SummitLog.Wpf.ViewModels {
    public abstract class ViewModelTestBase : ServiceTestBase {
        protected override ApplicationContextResources GetApplicationContextResources() {
            ApplicationContextResources applicationContextRessources = base.GetApplicationContextResources();
            applicationContextRessources.AddContextLocation("file://~/Config/Spring.ViewModels.xml");
            return applicationContextRessources;
        }
    }
}