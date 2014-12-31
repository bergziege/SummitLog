using Com.QueoFlow.Commons.MVVM.ViewModels;

namespace De.BerndNet2000.SummitLog.Wpf.Factories {
    /// <summary>
    ///     Schnittstelle für Klassen zur Erzeugung von View Models
    /// </summary>
    public interface IViewModelFactory {
        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zurück mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        T Get<T>(object argument = null) where T : ViewModelBase;

        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zurück mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        T Get<T>(params object[] arguments) where T : ViewModelBase;
    }
}