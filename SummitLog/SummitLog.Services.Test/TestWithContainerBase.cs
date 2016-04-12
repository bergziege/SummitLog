using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SummitLog.Services.Test
{
    [TestClass]
    public abstract class TestWithContainerBase
    {
        [TestInitialize]
        public virtual void Init()
        {
            Container = new UnityContainer();
        }

        protected UnityContainer Container { get; private set; }
    }
}