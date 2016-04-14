using System.Runtime.CompilerServices;
using System.Windows.Media.Effects;
using Microsoft.Practices.Unity;

namespace SummitLog.UI.Common
{
    public class GenericFactory : IGenericFactory
    {
        public T Resolve<T>()
        {
            return AppContext.Container.Resolve<T>();
        }

        public IWindow ResolveAsIWindow<T>()
        {
            return Resolve<T>() as IWindow;
        }
    }
}