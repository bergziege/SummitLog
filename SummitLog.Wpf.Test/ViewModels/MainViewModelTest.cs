using System.Threading.Tasks;

using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Wpf.Factories;
using De.BerndNet2000.SummitLog.Wpf.Ui.Library;
using De.BerndNet2000.SummitLog.Wpf.Ui.Library.ViewModels;
using De.BerndNet2000.SummitLog.Wpf.Ui.Main;
using De.BerndNet2000.SummitLog.Wpf.Ui.Reporting;
using De.BerndNet2000.SummitLog.Wpf.Ui.Reporting.ViewModels;
using De.BerndNet2000.SummitLog.Wpf.Ui.Settings;
using De.BerndNet2000.SummitLog.Wpf.Ui.Settings.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace De.BerndNet2000.SummitLog.Wpf.ViewModels {
    [TestClass]
    public class MainViewModelTest : ViewModelTestBase {
        [Resource]
        private IMainViewModel _mainViewModel;

        /// <summary>
        ///     Testet ob die Bibliothek aufgerufen werden kann
        /// </summary>
        [TestMethod]
        public void TestCanGotoLibrary() {
            // Given: ein komplett geladeness ViewModel
            _mainViewModel.LoadDataAsync().Wait();

            // When: gerpfüt wird, ob die Bibliothek aufgerufen werden kann
            bool canShowLibrary = _mainViewModel.ShowLibraryCommand.CanExecute(null);

            // Then: muss dies grundsätzlich möglich sein
            Assert.IsTrue(canShowLibrary);
        }

        /// <summary>
        ///     Testet ob die Reports aufgerufen wqerden können
        /// </summary>
        [TestMethod]
        public void TestCanGotoReports() {
            // Given: ein komplett geladenes ViewModel
            _mainViewModel.LoadDataAsync().Wait();

            // When: geprüft wird ob die Reports geöffnet werden können
            bool canOpenReports = _mainViewModel.ShowReportsCommand.CanExecute(null);

            // Then: muss dies grundsätzlich möglich sein
            Assert.IsTrue(canOpenReports);
        }

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
        ///     Testet den Aufruf der Bibliotheksansicht
        /// </summary>
        [TestMethod]
        public void TestGotoLibrary() {
            // Given: ein geladenes ViewModel
            Mock<LibraryViewModel> libraryViewModel = new Mock<LibraryViewModel>() { CallBase = true };
            libraryViewModel.Setup(x => x.LoadData()).Returns(Task.Delay(0));

            Mock<IViewModelFactory> viewModelFactory = new Mock<IViewModelFactory>();
            viewModelFactory.Setup(x => x.Get<LibraryViewModel>(null)).Returns(libraryViewModel.Object);

            _mainViewModel.ViewModelFactory = viewModelFactory.Object;

            // When: die Bibliothek aufgerufen werden
            _mainViewModel.ShowLibraryCommand.Execute(null);

            // Then: muss die LoadData Methode der Bibliothek gelaufen sein und das ViewModel zur Verfügung stehen
            viewModelFactory.Verify(x => x.Get<LibraryViewModel>(null), Times.Once());
            libraryViewModel.Verify(x => x.LoadData(), Times.Once());
            Assert.IsTrue(_mainViewModel.CurrentViewModel is ILibraryViewModel);
        }

        /// <summary>
        ///     Testet den Aufruf der Auswertungsansicht
        /// </summary>
        [TestMethod]
        public void TestGotoReports() {
            // Given: ein geladenes ViewModel
            Mock<ReportingViewModel> reportingViewModel = new Mock<ReportingViewModel>() { CallBase = true };
            reportingViewModel.Setup(x => x.LoadData()).Returns(Task.Delay(0));

            Mock<IViewModelFactory> viewModelFactory = new Mock<IViewModelFactory>();
            viewModelFactory.Setup(x => x.Get<ReportingViewModel>(null)).Returns(reportingViewModel.Object);

            _mainViewModel.ViewModelFactory = viewModelFactory.Object;

            // When: die Reports aufgerufen werden
            _mainViewModel.ShowReportsCommand.Execute(null);

            // Then: muss die LoadData Methode der Reports gelaufen sein und das ViewModel zur Verfügung stehen
            viewModelFactory.Verify(x => x.Get<ReportingViewModel>(null), Times.Once());
            reportingViewModel.Verify(x => x.LoadData(), Times.Once());
            Assert.IsTrue(_mainViewModel.CurrentViewModel is IReportingViewModel);
        }

        /// <summary>
        ///     Testet den Aufruf der Stammdatenseite
        /// </summary>
        [TestMethod]
        public void TestGotoSettings() {
            // Given: ein geladenes MainViewModel
            Mock<SettingsViewModel> settingsViewModel = new Mock<SettingsViewModel>() { CallBase = true };
            settingsViewModel.Setup(x => x.LoadData()).Returns(Task.Delay(0));

            Mock<IViewModelFactory> viewModelFactory = new Mock<IViewModelFactory>();
            viewModelFactory.Setup(x => x.Get<SettingsViewModel>(null)).Returns(settingsViewModel.Object);

            _mainViewModel.ViewModelFactory = viewModelFactory.Object;

            // When: die Stammdatanansicht aufgerufen wird
            _mainViewModel.ShowSettingsCommand.Execute(null);

            // Then: muss dieses als aktuelles ViewModel zur Verfügung stehen.
            // Und die LoadData Methode muss gelaufen sein.
            viewModelFactory.Verify(x => x.Get<SettingsViewModel>(null), Times.Once());
            settingsViewModel.Verify(x => x.LoadData(), Times.Once());
            Assert.IsTrue(_mainViewModel.CurrentViewModel is ISettingsViewModel);
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