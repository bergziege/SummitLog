using Microsoft.Practices.Unity;

namespace SummitLog.UI.Common
{
    /// <summary>
    ///     Erzeugt Objekte mittels einem Unity Container
    /// </summary>
    public class UnityResolver : IGenericFactory
    {
        private readonly IUnityContainer _unityContainer;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UnityResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }
        

        /// <summary>
        ///     Liefert ein Objekt vom Typ T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }
    }
}