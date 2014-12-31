using Com.QueoFlow.Commons.MVVM.Commands.ViewCommands;

using De.BerndNet2000.SummitLog.Wpf.Factories;
using De.BerndNet2000.SummitLog.Wpf.Ui.About.ViewModel;

using Spring.Context.Support;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.About.ViewCommand {
    /// <summary>
    /// View Command um die <see cref="AboutView"/> anzuzeigen.
    /// </summary>
    public class AboutViewCommand :ViewCommandBase{
        /// <summary>
        /// Definiert die Methode, die aufgerufen werden soll, wenn der Befehl aufgerufen wird.
        /// </summary>
        /// <param name="parameter">Daten, die vom Befehl verwendet werden.Wenn der Befehl keine Datenübergabe erfordert, kann das Objekt auf null festgelegt werden.</param>
        public override void Execute(object parameter) {
            AboutViewModel viewModel = ((IViewModelFactory)ContextRegistry.GetContext().GetObject("viewModelFactory")).Get<AboutViewModel>();
            AboutView view = ViewFactory.Get<AboutView>();

            view.DataContext = viewModel;
            view.ShowDialog();
        }
    }
}