using System;

using Com.QueoFlow.Commons.MVVM.Commands;
using Com.QueoFlow.Commons.MVVM.ViewModels;

using De.BerndNet2000.SummitLog.Wpf.Ui.Settings.DesignViewModels;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.Main.DesignViewModels {
    /// <summary>
    ///     Design View Model für die Hauptansicht
    /// </summary>
    public class MainDesignViewModel : ViewModelBase, IMainViewModel {
        /// <summary>
        ///     Erstellt ein neues Design View Model
        /// </summary>
        public MainDesignViewModel() {
            CurrentViewModel = new SettingsDesignViewModel();
        }

        /// <summary>
        ///     Wird gefeuert wenn das ViewModel geschlossen werden soll.
        /// </summary>
        public event EventHandler RequestClose;
        /// <summary>
        ///     Command um die Anwendung zu beenden.
        /// </summary>
        public RelayCommand ApplicationExitCommand { get; private set; }
        /// <summary>
        ///     Der Command zum Schließen des ViewModels.
        /// </summary>
        public RelayCommand CloseCommand { get; private set; }
        /// <summary>
        ///     Liefert oder setzt das aktuelle View Model
        /// </summary>
        public IPageViewModel CurrentViewModel { get; set; }
        /// <summary>
        ///     Liefert das Command um die Bibliotheksansicht anzuzeigen
        /// </summary>
        public RelayCommand ShowLibraryCommand { get; private set; }
        /// <summary>
        ///     Liefert das Command um die Einstellungen anzuzeigen
        /// </summary>
        public RelayCommand ShowSettingsCommand { get; private set; }

        /// <summary>
        ///     Die Methode in welcher alle wichtigen Daten für das ViewModel geladen werden sollten.
        ///     Diese Methode wird aufgerufen wenn eine View über einen Create/EditCommand angefordert wird.
        /// </summary>
        public void LoadData() {
        }
    }
}