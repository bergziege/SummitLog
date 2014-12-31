using System;
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
                _currentViewModel = value;
                OnPropertyChanged(this.GetPropertyName(x => x.CurrentViewModel));
            }
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
        ///     Setzt eine <see cref="IViewModelFactory" />
        /// </summary>
        public IViewModelFactory ViewModelFactory { set; get; }

        public override void LoadData() {
            throw new InvalidOperationException("use LoadDataAsync");
        }

        /// <summary>
        ///     Die Methode in welcher alle wichtigen Daten für das ViewModel geladen werden sollten. Diese Methode wird aufgerufen
        ///     wenn eine View über einen Create/EditCommand angefordert wird.
        /// </summary>
        public async Task LoadDataAsync() {
            await ShowLibraryAsync();
        }

        private void ApplicationExit() {
            /* Speichert die Fenstereinstellungen */
            Properties.Settings.Default.Save();

            RequestClosing();
        }

        private bool CanShowSettings() {
            return true;
        }

        private async Task SetCurrentAndLoad(IPageViewModel page) {
            IsLoading = true;
            CurrentViewModel = page;
            await CurrentViewModel.LoadData();
            IsLoading = false;
        }

        private async void ShowLibrary() {
            await ShowLibraryAsync();
        }

        private async Task ShowLibraryAsync() {
            await SetCurrentAndLoad(ViewModelFactory.Get<LibraryViewModel>());
        }

        private async void ShowSettings() {
            await SetCurrentAndLoad(ViewModelFactory.Get<SettingsViewModel>());
        }
    }
}