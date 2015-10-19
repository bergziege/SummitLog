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
        protected override void OnStartup(StartupEventArgs e)
        {
            SetupContainer();

            base.OnStartup(e);
        }

        private void SetupContainer()
        {
            var container = new Container();
            container = ServicesBootloader.Init(container);
            container = UiBootloader.Init(container);
            AppContext.Container = container;
        }
    }
}