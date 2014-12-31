using System.Threading.Tasks;

using Com.QueoFlow.Commons;
using Com.QueoFlow.Commons.MVVM.Commands;
using Com.QueoFlow.Commons.MVVM.ViewModels;

using De.BerndNet2000.SummitLog.Wpf.Factories;
using De.BerndNet2000.SummitLog.Wpf.Ui.Library.ViewModels;
using De.BerndNet2000.SummitLog.Wpf.Ui.Settings.ViewModels;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.Main.ViewModel {
    /// <summary>
    ///     View Model eines Hauptfensters.
    /// </summary>
    public class MainViewModel : WindowViewModelBase, IMainViewModel {
        private RelayCommand _applicationExitCommand;
        private IPageViewModel _currentViewModel;
        private RelayCommand _showLibraryCommand;
        private RelayCommand _showSettingsCommand;

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
        ///     Liefert oder setzt das aktuelle View Model
        /// </summary>
        public IPageViewModel CurrentViewModel {
            get { return _currentViewModel; }
            set {
                SetAndWaitForLoading(value);
            }
        }

        private async void SetAndWaitForLoading(IPageViewModel value) {
            _currentViewModel = value;
            await _currentViewModel.LoadData();
            OnPropertyChanged(this.GetPropertyName(x => x.CurrentViewModel));
        }

        /// <summary>
        ///     Liefert das Command um die Bibliotheksansicht anzuzeigen
        /// </summary>
        public RelayCommand ShowLibraryCommand {
            get {
                if (_showLibraryCommand == null) {
                    _showLibraryCommand = new RelayCommand(ShowLibrary);
                }

                return _showLibraryCommand;
            }
        }

        /// <summary>
        ///     Liefert das Command um die Einstellungen anzuzeigen
        /// </summary>
        public RelayCommand ShowSettingsCommand {
            get {
                if (_showSettingsCommand == null) {
                    _showSettingsCommand = new RelayCommand("LABEL", ShowSettings, CanShowSettings);
                }

                return _showSettingsCommand;
            }
        }

        /// <summary>
        ///     Die Methode in welcher alle wichtigen Daten für das ViewModel geladen werden sollten. Diese Methode wird aufgerufen
        ///     wenn eine View über einen Create/EditCommand angefordert wird.
        /// </summary>
        public override void LoadData() {
            ShowLibrary();
        }

        private void ApplicationExit() {
            /* Speichert die Fenstereinstellungen */
            Properties.Settings.Default.Save();

            RequestClosing();
        }

        private bool CanShowSettings() {
            return true;
        }

        private void ShowLibrary() {
            CurrentViewModel = ViewModelFactory.Get<LibraryViewModel>();
        }

        private void ShowSettings() {
            CurrentViewModel = ViewModelFactory.Get<SettingsViewModel>();
        }
    }
}