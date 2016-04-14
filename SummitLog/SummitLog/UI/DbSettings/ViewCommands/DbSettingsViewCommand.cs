using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.UI.Common;
using SummitLog.UI.DbSettings.ViewModels;
using SummitLog.UI.Main;

namespace SummitLog.UI.DbSettings.ViewCommands
{
    public class DbSettingsViewCommand: ViewCommandBase
    {

        public void Execute()
        {
            DbSettingsView view = GenericFactory.Resolve<DbSettingsView>();
            IDbSettingsViewModel vm = GenericFactory.Resolve<IDbSettingsViewModel>();
            view.DataContext = vm;
            vm.LoadData();

            vm.RequestClose += delegate { view.Close(); };

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof (MainView));
            view.ShowDialog();
        }
    }
}