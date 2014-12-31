using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Wpf.Ui.Library;
using De.BerndNet2000.SummitLog.Wpf.Ui.Main;
using De.BerndNet2000.SummitLog.Wpf.Ui.Settings;
using De.BerndNet2000.SummitLog.Wpf.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spring.Context.Support;

using TechTalk.SpecFlow;

namespace SummitLog.Wpf.Test.Integration {
    [Binding]
    public class NavigatingTheViewsSteps : ViewModelTestBase {
        [Resource]
        private IMainViewModel _mainViewModel;

        [Given(@"ich habe das Programm soeben gestartet")]
        public void GivenIchHabeDasProgrammSoebenGestartet() {
            ContextRegistry.RegisterContext(base.Context);
            _mainViewModel.LoadData();
        }

        [Given]
        public void Given_Ich_befinde_mich_im_Hauptfenster() {
            ContextRegistry.RegisterContext(base.Context);
        }

        [Then(@"muss die Bibliotheksansicht zu sehen sein")]
        public void ThenMussDieBibliotheksansichtZuSehenSein() {
            Assert.IsTrue(_mainViewModel.CurrentViewModel is ILibraryViewModel);
        }

        [Then]
        public void Then_muss_die_Stammdatenansicht_geöffnet_werden() {
            Assert.IsTrue(_mainViewModel.CurrentViewModel is ISettingsViewModel);
        }

        [When(@"ich noch nicht zu einer anderen Seite navigiert habe")]
        public void WhenIchNochNichtZuEinerAnderenSeiteNavigiertHabe() {
        }

        [When]
        public void When_Ich_auf_Einstellungen_klicke() {
            _mainViewModel.ShowSettingsCommand.Execute(null);
        }
    }
}