using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.UI.DbSettings.ViewModels;
using SummitLog.UI.Main;

namespace SummitLog.UI.DbSettings.ViewCommands
{
    public class DbSettingsViewCommand
    {
        public void Execute()
        {
            DbSettingsView view = AppContext.Container.Resolve<DbSettingsView>();
            IDbSettingsViewModel vm = AppContext.Container.Resolve<IDbSettingsViewModel>();
            view.DataContext = vm;
            vm.LoadData();

            vm.RequestClose += delegate { view.Close(); };

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof (MainView));
            view.ShowDialog();
        }
    }
}