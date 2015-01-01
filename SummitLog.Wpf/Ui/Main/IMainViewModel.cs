using System.Threading.Tasks;

using Com.QueoFlow.Commons.MVVM.Commands;
using Com.QueoFlow.Commons.MVVM.ViewModels;

using De.BerndNet2000.SummitLog.Wpf.Factories;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.Main {
    /// <summary>
    ///     Interface für ein View Model des Hauptfensters.
    /// </summary>
    public interface IMainViewModel : IWindowViewModelBase {
        /// <summary>
        ///     Command um die Anwendung zu beenden.
        /// </summary>
        RelayCommand ApplicationExitCommand { get; }

        /// <summary>
        ///     Liefert oder setzt das aktuelle View Model
        /// </summary>
        IPageViewModel CurrentViewModel { get; set; }

        /// <summary>
        ///     Liefert das Command um die Bibliotheksansicht anzuzeigen
        /// </summary>
        RelayCommand ShowLibraryCommand { get; }
        /// <summary>
        ///     Liefert ein Command um die Reports anzuzeigen
        /// </summary>
        RelayCommand ShowReportsCommand { get; }
        /// <summary>
        ///     Liefert das Command um die Einstellungen anzuzeigen
        /// </summary>
        RelayCommand ShowSettingsCommand { get; }

        /// <summary>
        ///     Setzt eine <see cref="IViewModelFactory" />
        /// </summary>
        IViewModelFactory ViewModelFactory { set; get; }

        /// <summary>
        ///     Lädt asynchron die VM relevanten Daten.
        /// </summary>
        /// <returns></returns>
        Task LoadDataAsync();
    }
}