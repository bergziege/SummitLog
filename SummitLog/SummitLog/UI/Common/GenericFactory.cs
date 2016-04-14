using System.Runtime.CompilerServices;
using Microsoft.Practices.Unity;

namespace SummitLog.UI.Common
{
    public class GenericFactory : IGenericFactory
    {
        public T Resolve<T>()
        {
            return AppContext.Container.Resolve<T>();
        }
    }
}