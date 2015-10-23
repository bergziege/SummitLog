using System.Windows;
using DryIoc;
using SummitLog.Services;

namespace SummitLog
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            SetupContainer();

            base.OnStartup(e);
        }

        private static void SetupContainer()
        {
            var container = new Container();
            container = ServicesBootloader.Init(container);
            container = UiBootloader.Init(container);
            AppContext.Container = container;
        }
    }
}