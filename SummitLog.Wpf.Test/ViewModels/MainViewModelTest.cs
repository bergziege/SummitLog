using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Wpf.Ui.Library;
using De.BerndNet2000.SummitLog.Wpf.Ui.Main;
using De.BerndNet2000.SummitLog.Wpf.Ui.Settings;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace De.BerndNet2000.SummitLog.Wpf.ViewModels {
    [TestClass]
    public class MainViewModelTest : ViewModelTestBase {
        [Resource]
        private IMainViewModel _mainViewModel;

        /// <summary>
        ///     Testet ob die Stammdaten aufgerufen werden können
        /// </summary>
        [TestMethod]
        public void TestCanGotoSettings() {
            // Given: ein komplett geladenes MainViewMOdel
            _mainViewModel.LoadDataAsync().Wait();

            // When: geprüft wird ob die Stammdaten geöffnet werden können
            bool canOpenSettings = _mainViewModel.ShowSettingsCommand.CanExecute(null);

            // Then: muss dies möglich sein
            Assert.IsTrue(canOpenSettings);
        }

        /// <summary>
        ///     Testet den Aufruf der Stammdatenseite
        /// </summary>
        [TestMethod]
        public void TestGotoSettings() {
            // Given: ein geladenes MainViewModel

            // When: die Stammdatanansicht aufgerufen wird
            _mainViewModel.ShowSettingsCommand.Execute(null);

            // Then: muss dieses als aktuelles ViewModel zur Verfügung stehen.
            // Und die LOadData Methode muss gelaufen sein.
            Assert.IsTrue(_mainViewModel.CurrentViewModel is ISettingsViewModel);
            Assert.Fail();
        }

        /// <summary>
        ///     Testet das Injecten der ViewModelFactory
        /// </summary>
        [TestMethod]
        public void TestInjectingViewModelFactory() {
            // Given: ein View Model über Spring

            // When: nix weiter gemacht

            // Then: muss die ViewMOdelFactory im ViewModel gesetzt sein
            Assert.IsNotNull(_mainViewModel.ViewModelFactory);
        }

        /// <summary>
        ///     Standardmäßig muss die Bibliotheksansicht geladen sien
        /// </summary>
        [TestMethod]
        public void TestLibraryViewModelIsDefault() {
            // Given: ein MainViewModel 
            Assert.IsNull(_mainViewModel.CurrentViewModel);

            // When: nur LoadDataAsync gelaufen ist
            _mainViewModel.LoadDataAsync().Wait();

            // Then: muss die Bibliothek das aktuelle ViewModel sein.
            Assert.IsTrue(_mainViewModel.CurrentViewModel is ILibraryViewModel);
        }
    }
}