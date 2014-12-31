using Com.QueoFlow.Commons.MVVM.ViewModels;

using DocumentFormat.OpenXml.Bibliography;

using Spring.Context;
using Spring.Context.Support;

namespace De.BerndNet2000.SummitLog.Wpf.Factories {
    /// <summary>
    /// Factory Klasse zur Erzegung der View Models.
    /// </summary>
    public class ViewModelFactory: IViewModelFactory {
        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zurück mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        public T Get<T>(object argument = null) where T : ViewModelBase {
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
        public T Get<T>(params object[] arguments) where T : ViewModelBase {
            IApplicationContext context = ContextRegistry.GetContext();
            string typeName = typeof(T).Name;
            if (typeName.Length > 0) {
                string firstLetter = typeName[0].ToString().ToLower();
                typeName = typeName.Remove(0, 1);
                typeName = typeName.Insert(0, firstLetter);
            }
            T vm = context.GetObject(typeName, arguments) as T;
            return vm;
        }

    }
}