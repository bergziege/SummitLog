using System.Windows;

using Spring.Context;
using Spring.Context.Support;

namespace De.BerndNet2000.SummitLog.Factories {
    /// <summary>
    ///   Factory zur Erzeugung der Views.
    /// </summary>
    public static class ViewFactory {
        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zurück mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        public static T Get<T>(object argument = null) where T : Window {
            object[] arguments = new object[] { };
            if (argument != null) {
                arguments = new[] { argument };
            }
            return Get<T>(arguments);
        }

        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zurück mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        public static T Get<T>(params object[] arguments) where T : Window {
            IApplicationContext context = ContextRegistry.GetContext();
            T vm = context.GetObject(typeof(T).Name, arguments) as T;
            return vm;
        }
    }
}