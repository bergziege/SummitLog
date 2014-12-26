using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Wpf.Ui.Main;
using De.BerndNet2000.SummitLog.Wpf.Ui.Settings;
using De.BerndNet2000.SummitLog.Wpf.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NHibernate.Bytecode;

using Spring.Context.Support;

using TechTalk.SpecFlow;

namespace SummitLog.Wpf.Test.Integration {
    [Binding]
    public class SelectSettingsViewSteps : ViewModelTestBase {
        [Resource]
        private IMainViewModel _mainViewModel;

        [Given]
        public void Given_Ich_befinde_mich_im_Hauptfenster() {
            ContextRegistry.RegisterContext(base.Context);
        }

        [Then]
        public void Then_muss_die_Stammdatenansicht_geöffnet_werden() {
            Assert.IsTrue(_mainViewModel.CurrentViewModel is ISettingsViewModel);
        }

        [When]
        public void When_Ich_auf_Einstellungen_klicke() {
            _mainViewModel.ShowSettingsCommand.Execute(null);
        }
    }
}