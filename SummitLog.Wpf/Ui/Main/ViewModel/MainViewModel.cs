using Com.QueoFlow.Commons.MVVM.Commands;
using Com.QueoFlow.Commons.MVVM.ViewModels;

using De.BerndNet2000.SummitLog.Wpf.Properties;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.Main.ViewModel {
    /// <summary>
    ///     View Model eines Hauptfensters.
    /// </summary>
    public class MainViewModel : WindowViewModelBase, IMainViewModel {
        private RelayCommand _applicationExitCommand;

        /// <summary>
        ///     Command um die Anwendung zu beenden.
        /// </summary>
        public RelayCommand ApplicationExitCommand {
            get {
                if (_applicationExitCommand == null) {
                    _applicationExitCommand = new RelayCommand("application exit", ApplicationExit);
                }

                return _applicationExitCommand;
            }
        }

        /// <summary>
        ///     Die Methode in welcher alle wichtigen Daten für das ViewModel geladen werden sollten. Diese Methode wird aufgerufen
        ///     wenn eine View über einen Create/EditCommand angefordert wird.
        /// </summary>
        public override void LoadData() {
        }

        private void ApplicationExit() {
            /* Speichert die Fenstereinstellungen */
            Settings.Default.Save();

            RequestClosing();
        }
    }
}