using Com.QueoFlow.Commons.MVVM.Commands;
using Com.QueoFlow.Commons.MVVM.ViewModels;

namespace De.BerndNet2000.SummitLog.Ui.Main {
    /// <summary>
    ///     Interface für ein View Model des Hauptfensters.
    /// </summary>
    public interface IMainViewModel : IWindowViewModelBase {
        /// <summary>
        ///     Command um die Anwendung zu beenden.
        /// </summary>
        RelayCommand ApplicationExitCommand { get; }
    }
}